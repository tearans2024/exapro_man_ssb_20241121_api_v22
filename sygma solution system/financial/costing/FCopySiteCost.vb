Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FCopySiteCost
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
        init_le(cs_id_from, "cost_set", en_id.EditValue)
        init_le(cs_id_to, "cost_set", en_id.EditValue)
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
        Dim dt_sctd As New DataTable
        Dim dt_temp As New DataTable
        Dim _exist As Boolean = False
        Dim dt_sct_to As New DataTable
        Dim _oid_master As String = ""
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
                   & " from ""public"".""GetAllProductStructure2""(" _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & 1 & ", " _
                   & 1000 & ",'Y',current_date) ORDER BY psd_seq desc "

                'data product structure dr paling ujung
                dt_ps = GetTableData(sSQL)

                For Each dr_ps As DataRow In dt_ps.Rows

                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                sSQL = "SELECT  " _
                                    & "  a.sct_total, " _
                                    & "  a.sct_rollup_date, " _
                                    & "  a.sct_mtl_tl, " _
                                    & "  a.sct_lbr_tl, " _
                                    & "  a.sct_bdn_tl, " _
                                    & "  a.sct_ovh_tl, " _
                                    & "  a.sct_sub_tl, " _
                                    & "  a.sct_mtl_ll, " _
                                    & "  a.sct_lbr_ll, " _
                                    & "  a.sct_bdn_ll, " _
                                    & "  a.sct_ovh_ll, " _
                                    & "  a.sct_sub_ll, " _
                                    & "  a.sct_rollup_ps, " _
                                    & "  a.sct_rollup_routing, " _
                                    & "  a.sct_oid, " _
                                    & "  a.sct_en_id, " _
                                    & "  a.sct_si_id, " _
                                    & "  a.sct_pt_id, " _
                                    & "  a.sct_cs_id " _
                                    & "FROM " _
                                    & "  public.sct_mstr a " _
                                    & "WHERE " _
                                    & "  a.sct_cs_id =  " & cs_id_from.EditValue & " AND  " _
                                    & "  a.sct_pt_id = (select pt_id from pt_mstr where pt_code='" _
                                   & dr_ps("psd_comp") & "') AND  " _
                                    & "  a.sct_en_id = " & en_id.EditValue & " AND  " _
                                    & "  a.sct_si_id = " & si_id.EditValue

                                'cari site cost dr partnumber itu 
                                dt_sct = GetTableData(sSQL)

                                For Each dr_sct As DataRow In dt_sct.Rows
                                    'cari sctd dr sct itu
                                    sSQL = "SELECT  " _
                                        & "  a.sctd_oid, " _
                                        & "  a.sctd_sct_oid, " _
                                        & "  a.sctd_csd_oid, " _
                                        & "  a.sctd_tl_amount, " _
                                        & "  a.sctd_ll_amount, " _
                                        & "  a.sctd_amount, " _
                                        & "  b.csd_csc_id, " _
                                        & "  c.csc_code " _
                                        & "FROM " _
                                        & "  public.sctd_det a " _
                                        & "  INNER JOIN public.csd_det b ON (a.sctd_csd_oid = b.csd_oid) " _
                                        & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                        & "WHERE " _
                                        & "  a.sctd_sct_oid = '" & dr_sct("sct_oid").ToString & "' " _
                                        & "ORDER BY " _
                                        & "  b.csd_seq"

                                    dt_sctd = GetTableData(sSQL)

                                    'cek dulu sudah ada apa belum, jika belum maka lakukan insert

                                    sSQL = "SELECT  " _
                                        & "  a.sct_total, " _
                                        & "  a.sct_rollup_date, " _
                                        & "  a.sct_mtl_tl, " _
                                        & "  a.sct_lbr_tl, " _
                                        & "  a.sct_bdn_tl, " _
                                        & "  a.sct_ovh_tl, " _
                                        & "  a.sct_sub_tl, " _
                                        & "  a.sct_mtl_ll, " _
                                        & "  a.sct_lbr_ll, " _
                                        & "  a.sct_bdn_ll, " _
                                        & "  a.sct_ovh_ll, " _
                                        & "  a.sct_sub_ll, " _
                                        & "  a.sct_rollup_ps, " _
                                        & "  a.sct_rollup_routing, " _
                                        & "  a.sct_oid, " _
                                        & "  a.sct_en_id, " _
                                        & "  a.sct_si_id, " _
                                        & "  a.sct_pt_id, " _
                                        & "  a.sct_cs_id " _
                                        & "FROM " _
                                        & "  public.sct_mstr a " _
                                        & "WHERE " _
                                        & "  a.sct_cs_id =  " & cs_id_to.EditValue & " AND  " _
                                        & "  a.sct_pt_id = (select pt_id from pt_mstr where pt_code='" _
                                       & dr_ps("psd_comp") & "') AND  " _
                                        & "  a.sct_en_id = " & en_id.EditValue & " AND  " _
                                        & "  a.sct_si_id = " & si_id.EditValue

                                    dt_sct_to = GetTableData(sSQL)

                                    If dt_sct_to.Rows.Count = 0 Then
                                        _oid_master = Guid.NewGuid.ToString
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sct_mstr " _
                                                & "( " _
                                                & "  sct_oid, " _
                                                & "  sct_dom_id, " _
                                                & "  sct_en_id, " _
                                                & "  sct_add_by, " _
                                                & "  sct_add_date, " _
                                                & "  sct_dt, " _
                                                & "  sct_si_id, " _
                                                & "  sct_pt_id, " _
                                                & "  sct_cs_id, " _
                                                & "  sct_total, " _
                                                & "  sct_mtl_tl, " _
                                                & "  sct_lbr_tl, " _
                                                & "  sct_bdn_tl, " _
                                                & "  sct_ovh_tl, " _
                                                & "  sct_sub_tl, " _
                                                & "  sct_mtl_ll, " _
                                                & "  sct_lbr_ll, " _
                                                & "  sct_bdn_ll, " _
                                                & "  sct_ovh_ll, " _
                                                & "  sct_sub_ll " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_oid_master) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(si_id.EditValue) & ",  " _
                                                & SetInteger(dr_sct("sct_pt_id")) & ",  " _
                                                & SetInteger(cs_id_to.EditValue) & ",  " _
                                                & SetDec(dr_sct("sct_total")) & ",  " _
                                                & SetDec(dr_sct("sct_mtl_tl")) & ",  " _
                                                & SetDec(dr_sct("sct_lbr_tl")) & ",  " _
                                                & SetDec(dr_sct("sct_bdn_tl")) & ",  " _
                                                & SetDec(dr_sct("sct_ovh_tl")) & ",  " _
                                                & SetDec(dr_sct("sct_sub_tl")) & ",  " _
                                                & SetDec(dr_sct("sct_mtl_ll")) & ",  " _
                                                & SetDec(dr_sct("sct_lbr_ll")) & ",  " _
                                                & SetDec(dr_sct("sct_bdn_ll")) & ",  " _
                                                & SetDec(dr_sct("sct_ovh_ll")) & ",  " _
                                                & SetDec(dr_sct("sct_sub_ll")) & "  " _
                                                & ")"

                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                        For Each dr_sctd As DataRow In dt_sctd.Rows

                                            sSQL = "INSERT INTO  " _
                                                  & "  public.sctd_det " _
                                                  & "( " _
                                                  & "  sctd_oid, " _
                                                  & "  sctd_add_by, " _
                                                  & "  sctd_add_date, " _
                                                  & "  sctd_dt, " _
                                                  & "  sctd_sct_oid, " _
                                                  & "  sctd_csd_oid, " _
                                                  & "  sctd_tl_amount, " _
                                                  & "  sctd_ll_amount, " _
                                                  & "  sctd_amount " _
                                                  & ")  " _
                                                  & "VALUES ( " _
                                                  & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                  & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                  & "current_timestamp" & ",  " _
                                                  & "current_timestamp" & ",  " _
                                                  & SetSetring(_oid_master) & ",  " _
                                                  & SetSetring(dr_sctd("sctd_csd_oid")) & ",  " _
                                                  & SetDec(dr_sctd("sctd_tl_amount")) & ",  " _
                                                  & SetDec(dr_sctd("sctd_ll_amount")) & ",  " _
                                                  & SetDec(dr_sctd("sctd_amount")) & "  " _
                                                  & ")"


                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = sSQL

                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()

                                        Next

                                    Else
                                        For Each dr_sctd As DataRow In dt_sctd.Rows

                                            sSQL = "UPDATE  " _
                                                & "  public.sctd_det   " _
                                                & "SET  " _
                                                & "  sctd_tl_amount = " & SetDec(dr_sctd("sctd_tl_amount")) & ",  " _
                                                & "  sctd_ll_amount = " & SetDec(dr_sctd("sctd_ll_amount")) & ",  " _
                                                & "  sctd_amount = " & SetDec(dr_sctd("sctd_amount")) & "  " _
                                                & "WHERE  " _
                                                & " sctd_sct_oid = (select sct_oid from sct_mstr where  sct_pt_id=" _
                                                & dr_sct("sct_pt_id") & " and sct_cs_id=" & SetInteger(cs_id_to.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ")   " _
                                                & "and " _
                                                & " sctd_csd_oid = (SELECT  " _
                                                & "  b.csd_oid " _
                                                & "FROM " _
                                                & "  public.csd_det b " _
                                                & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                                & "  INNER JOIN public.cs_mstr a ON (b.csd_cs_oid = a.cs_oid) " _
                                                & "WHERE " _
                                                & "  c.csc_code = " & SetSetring(dr_sctd("csc_code")) & "  AND  " _
                                                & "  a.cs_id = " & SetInteger(cs_id_to.EditValue) & ")"

                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = sSQL

                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()

                                        Next


                                        sSQL = "UPDATE  " _
                                            & "  public.sct_mstr   " _
                                            & "SET  " _
                                            & "  sct_total = " & SetDec(dr_sct("sct_total")) & ",  " _
                                            & "  sct_rollup_date = " & SetDate(dr_sct("sct_rollup_date")) & ",  " _
                                            & "  sct_mtl_tl = " & SetDec(dr_sct("sct_mtl_tl")) & ",  " _
                                            & "  sct_lbr_tl = " & SetDec(dr_sct("sct_lbr_tl")) & ",  " _
                                            & "  sct_bdn_tl = " & SetDec(dr_sct("sct_bdn_tl")) & ",  " _
                                            & "  sct_ovh_tl = " & SetDec(dr_sct("sct_ovh_tl")) & ",  " _
                                            & "  sct_sub_tl = " & SetDec(dr_sct("sct_sub_tl")) & ",  " _
                                            & "  sct_mtl_ll = " & SetDec(dr_sct("sct_mtl_ll")) & ",  " _
                                            & "  sct_lbr_ll = " & SetDec(dr_sct("sct_lbr_ll")) & ",  " _
                                            & "  sct_bdn_ll = " & SetDec(dr_sct("sct_bdn_ll")) & ",  " _
                                            & "  sct_ovh_ll = " & SetDec(dr_sct("sct_ovh_ll")) & ",  " _
                                            & "  sct_sub_ll = " & SetDec(dr_sct("sct_sub_ll")) & ",  " _
                                            & "  sct_rollup_ps = " & SetSetring(dr_sct("sct_rollup_ps")) & ",  " _
                                            & "  sct_rollup_routing = " & SetSetring(dr_sct("sct_rollup_routing")) & "  " _
                                            & "WHERE  " _
                                            & "  sct_en_id = " & SetInteger(dr_sct("sct_en_id")) & " And  " _
                                            & "  sct_si_id = " & SetInteger(dr_sct("sct_si_id")) & " And  " _
                                            & "  sct_pt_id = " & SetInteger(dr_sct("sct_pt_id")) & " And " _
                                            & "  sct_cs_id = " & SetSetring(cs_id_to.EditValue) & " "

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = sSQL

                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                    End If

                                Next

                                sqlTran.Commit()
                            Catch ex As PgSqlException
                                sqlTran.Rollback()
                                MessageBox.Show(ex.Message)
                            End Try
                        End With
                    End Using

                Next
            Next

            Box("Copy success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bt_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_copy.Click
        If MessageBox.Show("Copy Site Cost..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        generate_site_cost()
    End Sub

    Private Sub bt_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Reset.Click

        Dim dt_ps As New DataTable
        Dim dt_sct As New DataTable
        Dim dt_sctd As New DataTable
        Dim dt_temp As New DataTable
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
                   & " from ""public"".""GetAllProductStructure2""(" _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & 1 & ", " _
                   & 1000 & ",'Y',current_date) ORDER BY psd_seq desc "

                'data product structure dr paling ujung
                dt_ps = GetTableData(sSQL)

                For Each dr_ps As DataRow In dt_ps.Rows

                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                sSQL = "SELECT  " _
                                    & "  a.sct_total, " _
                                    & "  a.sct_rollup_date, " _
                                    & "  a.sct_mtl_tl, " _
                                    & "  a.sct_lbr_tl, " _
                                    & "  a.sct_bdn_tl, " _
                                    & "  a.sct_ovh_tl, " _
                                    & "  a.sct_sub_tl, " _
                                    & "  a.sct_mtl_ll, " _
                                    & "  a.sct_lbr_ll, " _
                                    & "  a.sct_bdn_ll, " _
                                    & "  a.sct_ovh_ll, " _
                                    & "  a.sct_sub_ll, " _
                                    & "  a.sct_rollup_ps, " _
                                    & "  a.sct_rollup_routing, " _
                                    & "  a.sct_oid, " _
                                    & "  a.sct_en_id, " _
                                    & "  a.sct_si_id, " _
                                    & "  a.sct_pt_id, " _
                                    & "  a.sct_cs_id " _
                                    & "FROM " _
                                    & "  public.sct_mstr a " _
                                    & "WHERE " _
                                    & "  a.sct_cs_id =  " & cs_id_from.EditValue & " AND  " _
                                    & "  a.sct_pt_id = (select pt_id from pt_mstr where pt_code='" _
                                   & dr_ps("psd_comp") & "') AND  " _
                                    & "  a.sct_en_id = " & en_id.EditValue & " AND  " _
                                    & "  a.sct_si_id = " & si_id.EditValue

                                'cari site cost dr partnumber itu 
                                dt_sct = GetTableData(sSQL)

                                For Each dr_sct As DataRow In dt_sct.Rows

                                    'If dr_sct("sct_total") > 0 Then
                                    '    Dim aa As String = ""
                                    'End If
                                    'cari sctd dr sct itu

                                    sSQL = "SELECT  " _
                                        & "  a.sctd_oid, " _
                                        & "  a.sctd_sct_oid, " _
                                        & "  a.sctd_csd_oid, " _
                                        & "  a.sctd_tl_amount, " _
                                        & "  a.sctd_ll_amount, " _
                                        & "  a.sctd_amount, " _
                                        & "  b.csd_csc_id, " _
                                        & "  c.csc_code " _
                                        & "FROM " _
                                        & "  public.sctd_det a " _
                                        & "  INNER JOIN public.csd_det b ON (a.sctd_csd_oid = b.csd_oid) " _
                                        & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                        & "WHERE " _
                                        & "  a.sctd_sct_oid = '" & dr_sct("sct_oid").ToString & "' " _
                                        & "ORDER BY " _
                                        & "  b.csd_seq"

                                    dt_sctd = GetTableData(sSQL)

                                    For Each dr_sctd As DataRow In dt_sctd.Rows

                                        sSQL = "UPDATE  " _
                                            & "  public.sctd_det   " _
                                            & "SET  " _
                                            & "  sctd_tl_amount = 0,  " _
                                            & "  sctd_ll_amount = 0,  " _
                                            & "  sctd_amount = 0  " _
                                            & "WHERE  " _
                                            & " sctd_sct_oid = (select sct_oid from sct_mstr where  sct_pt_id=" _
                                            & dr_sct("sct_pt_id") & " and sct_cs_id=" & SetInteger(cs_id_to.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ")   " _
                                            & "and " _
                                            & " sctd_csd_oid = (SELECT  " _
                                            & "  b.csd_oid " _
                                            & "FROM " _
                                            & "  public.csd_det b " _
                                            & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                                            & "  INNER JOIN public.cs_mstr a ON (b.csd_cs_oid = a.cs_oid) " _
                                            & "WHERE " _
                                            & "  c.csc_code = " & SetSetring(dr_sctd("csc_code")) & "  AND  " _
                                            & "  a.cs_id = " & SetInteger(cs_id_to.EditValue) & ")"

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = sSQL

                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                    Next

                                    sSQL = "UPDATE  " _
                                        & "  public.sct_mstr   " _
                                        & "SET  " _
                                        & "  sct_total = 0,  " _
                                        & "  sct_rollup_date = null,  " _
                                        & "  sct_mtl_tl = 0,  " _
                                        & "  sct_lbr_tl = 0,  " _
                                        & "  sct_bdn_tl = 0,  " _
                                        & "  sct_ovh_tl = 0,  " _
                                        & "  sct_sub_tl = 0,  " _
                                        & "  sct_mtl_ll = 0,  " _
                                        & "  sct_lbr_ll = 0,  " _
                                        & "  sct_bdn_ll = 0,  " _
                                        & "  sct_ovh_ll = 0,  " _
                                        & "  sct_sub_ll = 0,  " _
                                        & "  sct_rollup_ps = null,  " _
                                        & "  sct_rollup_routing = null  " _
                                        & "WHERE  " _
                                        & "  sct_en_id = " & SetInteger(dr_sct("sct_en_id")) & " And  " _
                                        & "  sct_si_id = " & SetInteger(dr_sct("sct_si_id")) & " And  " _
                                        & "  sct_pt_id = " & SetInteger(dr_sct("sct_pt_id")) & " And " _
                                        & "  sct_cs_id = " & SetSetring(cs_id_to.EditValue) & " "

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

                Next
            Next

            'Box("Reset success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
