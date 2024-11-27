Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FRollUpRoutingCost
    Public func_data As New function_data
    Dim dt_bantu As DataTable
    Dim sSQL As String
    Private Sub FGenerateSiteCost_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        en_id.Properties.DataSource = dt_bantu
        en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        en_id.ItemIndex = 0
    End Sub

    Private Sub en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles en_id.EditValueChanged
        init_le(si_id, "site", en_id.EditValue)
        init_le(cs_id, "cost_set", en_id.EditValue)
    End Sub

    Private Sub pt_id_from_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id_from.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._en_id = en_id.EditValue
            frm._obj = pt_id_from
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub pt_id_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id_to.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._en_id = en_id.EditValue
            frm._obj = pt_id_to
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub generate_site_cost()

        Dim dt_ps As New DataTable
        Dim dt_sct As New DataTable
        Dim dr_ps_virtual() As DataRow
        Dim _total_bdn As Double
        Dim _total_sub As Double
        Dim _total_lbr As Double
        Dim _pt_code_parent As String = ""
        Dim _ps_proses As String = ""
        Dim _nilai_burden As Double = 0
        Dim _nilai_subcont As Double = 0
        Dim _nilai_labor As Double = 0
        Dim dt_temp As New DataTable
        Dim dt_routing As New DataTable
        Dim _ro_id As Integer = 0
        Try
            sSQL = "select pt_id,pt_code  " _
                & "from pt_mstr  " _
                & "where  " _
                & "lower(pt_type)='i' and pt_code between '" & pt_id_from.Text & "' and '" & pt_id_to.Text & "' " _
                & "order by pt_code"

            Dim dt_pt As New DataTable
            dt_pt = GetTableData(sSQL)

            For Each dr_pt As DataRow In dt_pt.Rows
                sSQL = "SELECT psd_pt_bom_id,par_pt_phantom,ps_seq, psd_seq, psd_comp, psd_ref, psd_desc, psd_qty, psdbomdesc, " _
                   & " psd_op, psdstrname, psd_start_date, psd_end_date, psd_scrp_pct, psd_lt_off, psdgroupdesc ,pt_pm_code " _
                   & " from get_all_simulated_cost(" _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & "1,'Y',1,'Y',current_date" & ") as temp inner join pt_mstr on (psd_pt_bom_id=pt_id)   ORDER BY psd_seq desc "

                'data product structure dr paling ujung
                dt_ps = GetTableData(sSQL)
                'Exit Sub

                For Each dr_ps As DataRow In dt_ps.Rows
                    'If _pt_code_parent = "NF10S013R013C29AM" Then
                    '    Dim x As String = ""
                    'End If



                    If _ps_proses <> dr_ps("ps_seq").ToString Then
                        sSQL = " UPDATE  " _
                             & "  public.sct_mstr   " _
                             & "SET  " _
                             & "  sct_rollup_date =current_date,sct_rollup_routing='Y' " _
                             & "WHERE  " _
                             & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                             & "') and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                             & "  sct_en_id = " & SetInteger(en_id.EditValue) & " and sct_rollup_routing is null "
                        DbRun(sSQL)
                        ' Exit Sub
                    End If

                    sSQL = "select ro_id from ro_mstr inner join pt_mstr on (pt_id=ro_pt_id) where  ro_cs_id=" _
                        & SetInteger(cs_id.EditValue) & " and ro_is_default='Y' and ro_pt_id=" & dr_ps("psd_pt_bom_id")

                    dt_routing = GetTableData(sSQL)
                    'Exit Sub
                    _ro_id = 0
                    For Each dr_routing As DataRow In dt_routing.Rows
                        _ro_id = dr_routing("ro_id")
                    Next

                    'update tl burden
                    If SetString(dr_ps("pt_pm_code")).ToUpper = "M" And _ro_id > 0 Then

                        sSQL = "SELECT  " _
                            & "  sum(coalesce(c.wc_mch_bdn_rate, 0) * coalesce(c.wc_mch_op, 0) * coalesce(b.rod_run, 0)) AS jml " _
                            & "FROM " _
                            & "  public.ro_mstr a " _
                            & "  INNER JOIN public.rod_det b ON (a.ro_oid = b.rod_ro_oid) " _
                            & "  INNER JOIN public.wc_mstr c ON (b.rod_wc_id = c.wc_id) " _
                            & "WHERE " _
                            & "  a.ro_id =" & SetInteger(_ro_id) & "  and ro_is_default='Y' "

                        dt_temp = GetTableData(sSQL)

                        If dt_temp.Rows.Count > 0 Then
                            _nilai_burden = SetNumber(dt_temp.Rows(0).Item(0))

                            If _nilai_burden > 0 Then
                                'update tl disini
                                sSQL = "UPDATE  " _
                                    & "  public.sctd_det   " _
                                    & "SET  " _
                                    & "  sctd_tl_amount =  " & SetDec(_nilai_burden) & ",  " _
                                    & "  sctd_amount =  + sctd_ll_amount + " & SetDec(_nilai_burden) & "  " _
                                    & "WHERE  " _
                                    & "  sctd_sct_oid = (select sct_oid from sct_mstr where  sct_pt_id=" _
                                    & " (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                    & "') and sct_cs_id=" & SetInteger(cs_id.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ") and  " _
                                    & "  sctd_csd_oid = (SELECT  " _
                                    & "  b.csd_oid " _
                                    & "FROM " _
                                    & "  public.csd_det b " _
                                    & "  INNER JOIN public.cs_mstr a ON (b.csd_cs_oid = a.cs_oid) " _
                                    & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                    & "WHERE " _
                                    & "  c.csc_code = 'CBDN' AND  " _
                                    & "  a.cs_en_id = " & SetInteger(en_id.EditValue) & " AND  " _
                                    & "  a.cs_id = " & SetInteger(cs_id.EditValue) & ") "

                                DbRun(sSQL)


                                sSQL = " UPDATE  " _
                                    & "  public.sct_mstr   " _
                                    & "SET  " _
                                    & "  sct_bdn_tl =   " & SetDec(_nilai_burden) & ", " _
                                      & "   " _
                                    & "  sct_total = sct_mtl_tl+sct_lbr_tl+sct_ovh_tl+sct_sub_tl+sct_lbr_ll " _
                                    & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll+sct_mtl_ll+  " & SetDec(_nilai_burden) & " " _
                                    & "WHERE  " _
                                    & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                    & "' ) and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                    & "  sct_en_id = " & SetInteger(en_id.EditValue) & "  "
                                DbRun(sSQL)
                            End If
                        End If

                        sSQL = "SELECT  " _
                           & "  sum(coalesce(b.rod_sub_cost, 0)) AS jml " _
                           & "FROM " _
                           & "  public.ro_mstr a " _
                           & "  INNER JOIN public.rod_det b ON (a.ro_oid = b.rod_ro_oid) " _
                           & "WHERE " _
                           & "  a.ro_id =" & SetInteger(_ro_id) & "  and ro_is_default='Y' "

                        dt_temp = GetTableData(sSQL)

                        If dt_temp.Rows.Count > 0 Then
                            _nilai_subcont = SetNumber(dt_temp.Rows(0).Item(0))

                            If _nilai_subcont > 0 Then
                                'update tl disini
                                sSQL = "UPDATE  " _
                                    & "  public.sctd_det   " _
                                    & "SET  " _
                                    & "  sctd_tl_amount =  " & SetDec(_nilai_subcont) & ",  " _
                                    & "  sctd_amount =  + sctd_ll_amount + " & SetDec(_nilai_subcont) & "  " _
                                    & "WHERE  " _
                                    & "  sctd_sct_oid = (select sct_oid from sct_mstr where  sct_pt_id=" _
                                    & " (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                    & "') and sct_cs_id=" & SetInteger(cs_id.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ") and  " _
                                    & "  sctd_csd_oid = (SELECT  " _
                                    & "  b.csd_oid " _
                                    & "FROM " _
                                    & "  public.csd_det b " _
                                    & "  INNER JOIN public.cs_mstr a ON (b.csd_cs_oid = a.cs_oid) " _
                                    & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                    & "WHERE " _
                                    & "  c.csc_code = 'CSBC' AND  " _
                                    & "  a.cs_en_id = " & SetInteger(en_id.EditValue) & " AND  " _
                                    & "  a.cs_id = " & SetInteger(cs_id.EditValue) & ") "

                                DbRun(sSQL)


                                sSQL = " UPDATE  " _
                                    & "  public.sct_mstr   " _
                                    & "SET  " _
                                    & "  sct_sub_tl =   " & SetDec(_nilai_subcont) & ", " _
                                      & "   " _
                                    & "  sct_total = sct_mtl_tl+sct_lbr_tl+sct_ovh_tl+sct_bdn_tl+sct_lbr_ll " _
                                    & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll+sct_mtl_ll+  " & SetDec(_nilai_subcont) & " " _
                                    & "WHERE  " _
                                    & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                    & "' ) and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                    & "  sct_en_id = " & SetInteger(en_id.EditValue) & "  "
                                DbRun(sSQL)
                            End If
                        End If

                        sSQL = "SELECT  " _
                          & "  sum(coalesce(c.wc_setup_men, 0) * coalesce(c.wc_setup_rate, 0) * coalesce(b.rod_setup, 0)) " _
                          & " + sum(coalesce(c.wc_men_mch, 0) * coalesce(c.wc_lbr_rate, 0) * coalesce(b.rod_run, 0)) AS jml " _
                          & "FROM " _
                          & "  public.ro_mstr a " _
                          & "  INNER JOIN public.rod_det b ON (a.ro_oid = b.rod_ro_oid) " _
                          & "  INNER JOIN public.wc_mstr c ON (b.rod_wc_id = c.wc_id) " _
                          & "WHERE " _
                          & "  a.ro_id =" & SetInteger(_ro_id) & "  and ro_is_default='Y' "

                        dt_temp = GetTableData(sSQL)

                        If dt_temp.Rows.Count > 0 Then
                            _nilai_labor = SetNumber(dt_temp.Rows(0).Item(0))

                            If _nilai_labor > 0 Then
                                'update tl disini
                                sSQL = "UPDATE  " _
                                    & "  public.sctd_det   " _
                                    & "SET  " _
                                    & "  sctd_tl_amount =  " & SetDec(_nilai_labor) & ",  " _
                                    & "  sctd_amount =  + sctd_ll_amount + " & SetDec(_nilai_labor) & "  " _
                                    & "WHERE  " _
                                    & "  sctd_sct_oid = (select sct_oid from sct_mstr where  sct_pt_id=" _
                                    & " (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                    & "') and sct_cs_id=" & SetInteger(cs_id.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ") and  " _
                                    & "  sctd_csd_oid = (SELECT  " _
                                    & "  b.csd_oid " _
                                    & "FROM " _
                                    & "  public.csd_det b " _
                                    & "  INNER JOIN public.cs_mstr a ON (b.csd_cs_oid = a.cs_oid) " _
                                    & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                    & "WHERE " _
                                    & "  c.csc_code = 'CLBR' AND  " _
                                    & "  a.cs_en_id = " & SetInteger(en_id.EditValue) & " AND  " _
                                    & "  a.cs_id = " & SetInteger(cs_id.EditValue) & ") "

                                DbRun(sSQL)


                                sSQL = " UPDATE  " _
                                    & "  public.sct_mstr   " _
                                    & "SET  " _
                                    & "  sct_lbr_tl =   " & SetDec(_nilai_labor) & ", " _
                                      & "   " _
                                    & "  sct_total = sct_mtl_tl+sct_bdn_tl+sct_ovh_tl+sct_sub_tl+sct_lbr_ll " _
                                    & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll+sct_mtl_ll+  " & SetDec(_nilai_labor) & " " _
                                    & "WHERE  " _
                                    & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                    & "' ) and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                    & "  sct_en_id = " & SetInteger(en_id.EditValue) & "  "
                                DbRun(sSQL)
                            End If
                        End If
                    End If


                    If dr_ps("psd_comp") <> dr_pt("pt_code") Then

                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    sSQL = "SELECT  " _
                                       & "  b.sctd_tl_amount + sctd_ll_amount as sctd_tl_amount, d.csc_code, " _
                                       & "  b.sctd_csd_oid " _
                                       & "FROM " _
                                       & "  public.sct_mstr a " _
                                       & "  INNER JOIN public.sctd_det b ON (a.sct_oid = b.sctd_sct_oid) " _
                                       & "  INNER JOIN public.csd_det c ON (b.sctd_csd_oid = c.csd_oid) " _
                                       & "  INNER JOIN public.csc_category d ON (c.csd_csc_id = d.csc_id) " _
                                       & "WHERE " _
                                       & "  a.sct_cs_id = " & cs_id.EditValue & " AND  " _
                                       & "  a.sct_pt_id = (select pt_id from pt_mstr where pt_code='" _
                                       & dr_ps("psd_comp") & "') AND  " _
                                       & " ( d.csc_code = 'CBDN' or d.csc_code = 'CSBC'  or d.csc_code = 'CLBR' ) and (b.sctd_tl_amount <> 0 or b.sctd_ll_amount <> 0) "

                                    'cari site cost dr partnumber itu 
                                    dt_sct = GetTableData(sSQL)

                                    _total_bdn = 0
                                    _total_sub = 0
                                    _total_lbr = 0
                                    dr_ps_virtual = dt_ps.Select("psd_seq = " & dr_ps("ps_seq"))

                                    If dr_ps_virtual.Length > 0 Then
                                        _pt_code_parent = dr_ps_virtual(0)("psd_comp")

                                        For Each dr_sct As DataRow In dt_sct.Rows
                                            If dr_sct("csc_code") = "CBDN" Then
                                                _total_bdn += dr_sct("sctd_tl_amount") * dr_ps("psd_qty")
                                            ElseIf dr_sct("csc_code") = "CSBC" Then
                                                _total_bdn += dr_sct("sctd_tl_amount") * dr_ps("psd_qty")
                                            ElseIf dr_sct("csc_code") = "CLBR" Then
                                                _total_lbr += dr_sct("sctd_tl_amount") * dr_ps("psd_qty")
                                            End If

                                            'update dataset parent disini
                                            sSQL = "UPDATE  " _
                                                & "  public.sctd_det   " _
                                                & "SET  " _
                                                & "  sctd_ll_amount = sctd_ll_amount + " & SetDec(dr_sct("sctd_tl_amount") * dr_ps("psd_qty")) & ",  " _
                                                & "  sctd_amount = sctd_tl_amount + sctd_ll_amount + " & SetDec(dr_sct("sctd_tl_amount") * dr_ps("psd_qty")) & "  " _
                                                & "WHERE  " _
                                                & "  sctd_sct_oid = (select sct_oid from sct_mstr where sct_rollup_routing is null and  sct_pt_id=" _
                                                & " (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                                                & "' and lower(pt_pm_code)='m') and sct_cs_id=" & SetInteger(cs_id.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ") and  " _
                                                & "  sctd_csd_oid = " & SetSetring(dr_sct("sctd_csd_oid")) & " "

                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = sSQL

                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()

                                        Next

                                        If _total_bdn <> 0 Or _total_sub <> 0 Or _total_lbr <> 0 Then

                                            sSQL = " UPDATE  " _
                                             & "  public.sct_mstr   " _
                                             & "SET  " _
                                             & "  sct_bdn_ll = sct_bdn_ll +  " & SetDec(_total_bdn) & ", " _
                                               & "  sct_sub_ll = sct_sub_ll +  " & SetDec(_total_sub) & ", " _
                                                & "  sct_lbr_ll = sct_lbr_ll +  " & SetDec(_total_lbr) & ", " _
                                             & "  sct_total = sct_mtl_tl+sct_lbr_tl+sct_bdn_tl+sct_ovh_tl+sct_sub_tl+sct_lbr_ll " _
                                             & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll+sct_mtl_ll+  " & SetDec(_total_bdn + _total_sub + _total_lbr) & " " _
                                             & "WHERE  " _
                                             & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                                             & "' and lower(pt_pm_code)='m') and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                             & "  sct_en_id = " & SetInteger(en_id.EditValue) & " and sct_rollup_routing is null "

                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = sSQL

                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()


                                        End If
                                    End If
                                   


                                    sqlTran.Commit()
                                Catch ex As PgSqlException
                                    sqlTran.Rollback()
                                    MessageBox.Show(ex.Message)
                                End Try
                            End With
                        End Using
                        _ps_proses = dr_ps("ps_seq").ToString
                    End If
                Next
            Next

            Box("Rollup success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bt_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_generate.Click
        bt_Reset_Click(Nothing, Nothing)
        generate_site_cost()
    End Sub

    Private Sub bt_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Reset.Click
        Dim dt_ps As New DataTable
        Dim dt_sct As New DataTable
        Dim dr_ps_virtual() As DataRow
        Dim _total As Double = 0
        Dim _pt_code_parent As String = ""

        Try
            sSQL = "select pt_id,pt_code  " _
                & "from pt_mstr  " _
                & "where  " _
                & "lower(pt_type)='i' and pt_code between '" & pt_id_from.Text & "' and '" & pt_id_to.Text & "' " _
                & "order by pt_code"

            Dim dt_pt As New DataTable
            dt_pt = GetTableData(sSQL)

            For Each dr_pt As DataRow In dt_pt.Rows
                sSQL = "SELECT ps_seq, psd_seq, psd_comp, psd_ref, psd_desc, psd_qty, psdbomdesc, " _
                   & " psd_op, psdstrname, psd_start_date, psd_end_date, psd_scrp_pct, psd_lt_off, psdgroupdesc" _
                   & " from get_all_simulated_cost(" _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & "1,'Y',1,'Y',current_date" & ") ORDER BY psd_seq desc "

                'data product structure dr paling ujung
                dt_ps = GetTableData(sSQL)

                For Each dr_ps As DataRow In dt_ps.Rows


                    If dr_ps("psd_comp") <> dr_pt("pt_code") Then
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    sSQL = "SELECT  " _
                                       & "  b.sctd_tl_amount, " _
                                       & "  b.sctd_csd_oid,d.csc_code " _
                                       & "FROM " _
                                       & "  public.sct_mstr a " _
                                       & "  INNER JOIN public.sctd_det b ON (a.sct_oid = b.sctd_sct_oid) " _
                                       & "  INNER JOIN public.csd_det c ON (b.sctd_csd_oid = c.csd_oid) " _
                                       & "  INNER JOIN public.csc_category d ON (c.csd_csc_id = d.csc_id) " _
                                       & "WHERE " _
                                       & "  a.sct_cs_id = " & cs_id.EditValue & " AND  " _
                                       & "  a.sct_pt_id = (select pt_id from pt_mstr where pt_code='" _
                                       & dr_ps("psd_comp") & "') AND  " _
                                       & "  (d.csc_code = 'CBDN' or d.csc_code = 'CLBR' or d.csc_code = 'CSBC') "

                                    'cari site cost dr partnumber itu 
                                    dt_sct = GetTableData(sSQL)

                                    _total = 0
                                    dr_ps_virtual = dt_ps.Select("psd_seq = " & dr_ps("ps_seq"))
                                    _pt_code_parent = dr_ps_virtual(0)("psd_comp")
                                    'sct_rollup_routing
                                    'ini untuk mereset nilai tlnya
                                    sSQL = "UPDATE  " _
                                       & "  public.sctd_det   " _
                                       & "SET  " _
                                       & "  sctd_tl_amount =  0,  " _
                                       & "  sctd_amount =  sctd_ll_amount   " _
                                       & "WHERE  " _
                                       & "  sctd_sct_oid in (select sct_oid from sct_mstr where  sct_pt_id=" _
                                       & " (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                       & "') and sct_cs_id=" & SetInteger(cs_id.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ") and  " _
                                       & "  sctd_csd_oid in (SELECT  " _
                                       & "  b.csd_oid " _
                                       & "FROM " _
                                       & "  public.csd_det b " _
                                       & "  INNER JOIN public.cs_mstr a ON (b.csd_cs_oid = a.cs_oid) " _
                                       & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                       & "WHERE " _
                                       & "  (c.csc_code = 'CBDN' or c.csc_code = 'CLBR' or c.csc_code = 'CSBC') AND  " _
                                       & "  a.cs_en_id = " & SetInteger(en_id.EditValue) & " AND  " _
                                       & "  a.cs_id = " & SetInteger(cs_id.EditValue) & ") "

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = sSQL

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()


                                    sSQL = " UPDATE  " _
                                          & "  public.sct_mstr   " _
                                          & "SET  " _
                                          & "  sct_bdn_tl = 0, " _
                                              & "  sct_lbr_tl = 0, " _
                                                & "  sct_sub_tl = 0, " _
                                          & "  sct_total = sct_mtl_tl+sct_ovh_tl+sct_lbr_ll " _
                                          & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll+sct_mtl_ll " _
                                          & "WHERE  " _
                                          & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & dr_ps("psd_comp") _
                                          & "' ) and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                          & "  sct_en_id = " & SetInteger(en_id.EditValue) & "  "

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = sSQL

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    For Each dr_sct As DataRow In dt_sct.Rows

                                        'update dataset parent disini
                                        sSQL = "UPDATE  " _
                                            & "  public.sctd_det   " _
                                            & "SET  " _
                                            & "  sctd_ll_amount = 0, " _
                                            & "  sctd_amount = sctd_tl_amount   " _
                                            & "WHERE  " _
                                            & "  sctd_sct_oid = (select sct_oid from sct_mstr where sct_pt_id=" _
                                            & " (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                                            & "') and sct_cs_id=" & SetInteger(cs_id.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ") and  " _
                                            & "  sctd_csd_oid = " & SetSetring(dr_sct("sctd_csd_oid")) & " "

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = sSQL

                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()


                                        sSQL = " UPDATE  " _
                                            & "  public.sct_mstr   " _
                                            & "SET  " _
                                            & "  sct_rollup_date = null,sct_rollup_routing=null,  " _
                                            & "  sct_lbr_ll = 0,sct_sub_ll = 0,sct_bdn_ll = 0, " _
                                            & "  sct_total = sct_mtl_tl+sct_ovh_tl+sct_mtl_ll " _
                                            & "  		+sct_ovh_ll " _
                                            & "WHERE  " _
                                            & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                                            & "') and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                            & "  sct_en_id = " & SetInteger(en_id.EditValue) & " "

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = sSQL

                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                    Next

                                    sqlTran.Commit()
                                Catch ex As PgSqlException
                                    sqlTran.Rollback()
                                    MessageBox.Show(ex.Message)
                                End Try
                            End With
                        End Using
                    End If
                Next
            Next

            'Box("Reset success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
