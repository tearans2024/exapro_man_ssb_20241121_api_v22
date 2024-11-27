Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports System.IO

Public Class FRollUpPSCost
    Delegate Sub FunctionCall(ByVal param)
    Public func_data As New function_data
    Dim dt_bantu As DataTable
    Dim sSQL As String
    Public Sub Make_Report(ByVal status)
        Invoke(New FunctionCall(AddressOf _MakeReport), status)
    End Sub
    Private Sub _MakeReport(ByVal status)
        Try
            LblStatus.Text = Now.ToString("dd/MM/yyyy HH:mm:ss") & " " & status
            System.Windows.Forms.Application.DoEvents()
        Catch ex As Exception
        End Try
    End Sub
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
        Dim _total_mtl, _total_ovh As Double
        Dim _pt_code_parent As String = ""
        Dim _ps_proses As String = ""
        Dim _now As Date = CekTanggal()
        Try

            sSQL = "select pt_id,pt_code,pt_desc1  " _
                & "from pt_mstr  " _
                & "where  " _
                & "lower(pt_type)='i' and pt_code between '" & pt_id_from.Text & "' and '" & pt_id_to.Text & "' " _
                & "order by pt_code"

            Dim dt_pt As New DataTable
            dt_pt = GetTableData(sSQL)

            Dim x As Integer = 1
            For Each dr_pt As DataRow In dt_pt.Rows
                Make_Report(x & " of " & dt_pt.Rows.Count & " " & dr_pt("pt_code").ToString & " " & dr_pt("pt_desc1"))
                x += 1
                sSQL = "select * from public.sct_mstr " _
                        & "WHERE  " _
                        & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & dr_pt("pt_code").ToString _
                        & "') and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                        & "  sct_en_id = " & SetInteger(en_id.EditValue) & " and sct_rollup_ps > " & SetDateNTime(_now)

                Dim dt_cek As New DataTable
                dt_cek = GetTableData(sSQL)


                If dt_cek.Rows.Count = 0 Then
                    sSQL = "SELECT ps_seq, psd_seq, psd_comp, psd_ref, psd_desc, psd_qty, psdbomdesc, " _
                       & " psd_op, psdstrname, psd_start_date, psd_end_date, psd_scrp_pct, psd_lt_off, psdgroupdesc" _
                       & " from public.get_allproduct_structure(" _
                       & "'" & dr_pt("pt_code") & "'" & ", " _
                       & "'" & dr_pt("pt_code") & "'" & ", " _
                       & 1 & ", " _
                       & 1000 & ",'Y',current_date) ORDER BY psd_seq desc "

                    'data product structure dr paling ujung
                    dt_ps = GetTableData(sSQL)

                    For Each dr_ps As DataRow In dt_ps.Rows
                        'WriteToErrorLog(dr_ps("psd_comp").ToString)
                        Make_Report(dr_ps("psd_comp").ToString)
                        If _ps_proses <> dr_ps("ps_seq").ToString Then
                            sSQL = " UPDATE  " _
                                 & "  public.sct_mstr   " _
                                 & "SET  " _
                                 & "  sct_rollup_date =current_date,sct_rollup_ps='Y' " _
                                 & "WHERE  " _
                                 & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                                 & "') and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                 & "  sct_en_id = " & SetInteger(en_id.EditValue) & " and sct_rollup_ps is null "
                            DbRun(sSQL)
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
                                           & " ( d.csc_code = 'CMTL' or d.csc_code = 'COVH') and (b.sctd_tl_amount <> 0 or b.sctd_ll_amount <> 0) "

                                        'cari site cost dr partnumber itu 
                                        dt_sct = GetTableData(sSQL)

                                        _total_mtl = 0
                                        _total_ovh = 0

                                        dr_ps_virtual = dt_ps.Select("psd_seq = '" & dr_ps("ps_seq") & "'")
                                        _pt_code_parent = dr_ps_virtual(0)("psd_comp")

                                        For Each dr_sct As DataRow In dt_sct.Rows
                                            If dr_sct("csc_code") = "CMTL" Then
                                                _total_mtl += dr_sct("sctd_tl_amount") * dr_ps("psd_qty")
                                            Else
                                                _total_ovh += dr_sct("sctd_tl_amount") * dr_ps("psd_qty")
                                            End If


                                            'update dataset parent disini
                                            sSQL = "UPDATE  " _
                                                & "  public.sctd_det   " _
                                                & "SET  " _
                                                & "  sctd_ll_amount = sctd_ll_amount + " & SetDec(dr_sct("sctd_tl_amount") * dr_ps("psd_qty")) & ",  " _
                                                & "  sctd_amount = sctd_tl_amount + sctd_ll_amount + " & SetDec(dr_sct("sctd_tl_amount") * dr_ps("psd_qty")) & "  " _
                                                & "WHERE  " _
                                                & "  sctd_sct_oid = (select sct_oid from sct_mstr where sct_rollup_ps is null and  sct_pt_id=" _
                                                & " (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                                                & "' and lower(pt_pm_code)='m') and sct_cs_id=" & SetInteger(cs_id.EditValue) & " and sct_en_id=" & SetInteger(en_id.EditValue) & ") and  " _
                                                & "  sctd_csd_oid = " & SetSetring(dr_sct("sctd_csd_oid")) & " "

                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = sSQL

                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()

                                        Next

                                        If _total_mtl <> 0 Or _total_ovh <> 0 Then

                                            sSQL = " UPDATE  " _
                                             & "  public.sct_mstr   " _
                                             & "SET  " _
                                             & "  sct_mtl_ll = sct_mtl_ll +  " & SetDec(_total_mtl) & ", " _
                                               & "  sct_ovh_ll = sct_ovh_ll +  " & SetDec(_total_ovh) & ", " _
                                             & "  sct_total = sct_mtl_tl+sct_lbr_tl+sct_bdn_tl+sct_ovh_tl+sct_sub_tl+sct_lbr_ll " _
                                             & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll+sct_mtl_ll+  " & SetDec(_total_mtl) & "+ " & SetDec(_total_ovh) & " " _
                                             & "WHERE  " _
                                             & "  sct_pt_id = (select pt_id from pt_mstr where pt_code='" & _pt_code_parent _
                                             & "' and lower(pt_pm_code)='m') and  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                                             & "  sct_en_id = " & SetInteger(en_id.EditValue) & " and sct_rollup_ps is null "

                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = sSQL

                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()
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
                Else
                    Make_Report(dr_pt("pt_code").ToString & " " & dr_pt("pt_desc1") & " ignored")

                End If

            Next

            Box("Rollup success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bt_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_generate.Click
        'bt_Reset_Click(Nothing, Nothing)
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
                   & " from ""public"".""GetAllProductStructure2""(" _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & "'" & dr_pt("pt_code") & "'" & ", " _
                   & 1 & ", " _
                   & 1000 & ",'Y',current_date) ORDER BY psd_seq desc "

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
                                       & "  (d.csc_code = 'CMTL' or d.csc_code = 'COVH' ) "

                                    'cari site cost dr partnumber itu 
                                    dt_sct = GetTableData(sSQL)

                                    _total = 0
                                    dr_ps_virtual = dt_ps.Select("psd_seq = " & dr_ps("ps_seq"))
                                    _pt_code_parent = dr_ps_virtual(0)("psd_comp")

                                    For Each dr_sct As DataRow In dt_sct.Rows
                                        '_total += dr_sct("sctd_tl_amount")

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
                                            & "  sct_rollup_date = null,sct_rollup_ps=null,  " _
                                            & "  sct_mtl_ll = 0,sct_ovh_ll = 0, " _
                                            & "  sct_total = sct_mtl_tl+sct_lbr_tl+sct_bdn_tl+sct_ovh_tl+sct_sub_tl+sct_lbr_ll " _
                                            & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll " _
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

            Box("Reset success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Sub WriteToErrorLog(ByVal msg As String)
        Try

            System.Windows.Forms.Application.DoEvents()
            'check and make the directory if necessary; this is set to look in 

            'the application folder, you may wish to place the error log in 
            'another location depending upon the user's role and write access to 

            'different areas of the file system
            If Not System.IO.Directory.Exists(Application.StartupPath & _
            "\Log\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & _
                    "\Log\")
            End If

            'check the file
            Dim fs As FileStream = New FileStream(Application.StartupPath & _
                "\Log\Log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite)

            If fs.Length > 100000000 Then
                Dim s As StreamWriter = New StreamWriter(fs)
                s.Close()
                fs.Close()

                Dim fi As New FileInfo(Application.StartupPath & "\Log\Log.txt")
                fi.MoveTo(Application.StartupPath & "\Log\Log" & Now.ToString("-yyyyMMdd-HHmmss") & ".txt")
            Else
                Dim s As StreamWriter = New StreamWriter(fs)
                s.Close()
                fs.Close()
            End If


            'log it
            Dim fs1 As FileStream = New FileStream(Application.StartupPath & _
                "\Log\Log.txt", FileMode.Append, FileAccess.Write)

            Dim s1 As StreamWriter = New StreamWriter(fs1)
            s1.Write(msg & vbCrLf)
            s1.Close()
            fs1.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtResetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtResetAll.Click
        Using objinsert As New master_new.WDABasepgsql("", "")
            With objinsert
                .Connection.Open()
                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                Try
                    .Command = .Connection.CreateCommand
                    .Command.Transaction = sqlTran

                    sSQL = "UPDATE  " _
                            & "  public.sctd_det   " _
                            & "SET  " _
                            & "  sctd_ll_amount = 0, " _
                            & "  sctd_amount = sctd_tl_amount   " _
                            & "WHERE  " _
                            & " sctd_csd_oid in (SELECT   x.csd_oid FROM  public.csd_det x  INNER JOIN public.csc_category y ON " _
                            & "(x.csd_csc_id = y.csc_id) WHERE  y.csc_code IN ('CMTL','COVH')) and sctd_sct_oid in (SELECT  z.sct_oid FROM  public.sct_mstr z WHERE  z.sct_en_id=" & SetInteger(en_id.EditValue) & " AND   z.sct_cs_id=" & en_id.EditValue & ") "

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = sSQL

                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    sSQL = " UPDATE  " _
                        & "  public.sct_mstr   " _
                        & "SET  " _
                        & "  sct_rollup_date = null,sct_rollup_ps=null,  " _
                        & "  sct_mtl_ll = 0,sct_ovh_ll = 0, " _
                        & "  sct_total = sct_mtl_tl+sct_lbr_tl+sct_bdn_tl+sct_ovh_tl+sct_sub_tl+sct_lbr_ll " _
                        & "  		+sct_bdn_ll+sct_ovh_ll+sct_sub_ll " _
                        & "WHERE  " _
                        & "  sct_cs_id = " & SetInteger(cs_id.EditValue) & " and  " _
                        & "  sct_en_id = " & SetInteger(en_id.EditValue) & " "

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = sSQL

                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                    sqlTran.Commit()
                    Box("Reset success")
                Catch ex As PgSqlException
                    sqlTran.Rollback()
                    MessageBox.Show(ex.Message)
                End Try
            End With
        End Using
    End Sub
End Class
