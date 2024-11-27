Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FSOShipMerge
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As DataSet

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(arps_en_id, "en_mstr")

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "arps_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "arps_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "arps_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "arps_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "arps_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "arps_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "arps_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "arpsd_arps_oid", False)
        add_column(gv_detail, "AR Number", "arpsd_soship_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "arpsd_oid", False)
        add_column(gv_edit, "arpsd_arps_oid", False)
        add_column(gv_edit, "arpsd_soship_oid", False)
        add_column(gv_edit, "AR Number", "arpsd_soship_code", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.arps_oid, " _
                & "  a.arps_en_id, " _
                & "  c.en_desc, " _
                & "  a.arps_code, " _
                & "  a.arps_date, " _
                & "  a.arps_add_by, " _
                & "  a.arps_add_date, " _
                & "  a.arps_upd_by, " _
                & "  a.arps_upd_date, " _
                & "  a.arps_ptnr_id, " _
                & "  b.ptnr_name,arps_remarks " _
                & "FROM " _
                & "  public.arps_print a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.arps_ptnr_id = b.ptnr_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.arps_en_id = c.en_id) " _
                & "WHERE " _
                & " a.arps_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") and a.arps_date BETWEEN  " _
                & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & " ORDER BY " _
                & "  a.arps_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        arps_en_id.EditValue = ""
        arps_ptnr_id.EditValue = ""
        arps_date.DateTime = CekTanggal()
        arps_remarks.EditValue = ""
        arps_en_id.Focus()

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb

                    .SQL = "SELECT  " _
                        & "  a.arpsd_oid, " _
                        & "  a.arpsd_arps_oid, " _
                        & "  a.arpsd_soship_oid, " _
                        & "  a.arpsd_soship_code " _
                        & "FROM " _
                        & "  public.arpsd_det a " _
                        & "WHERE " _
                        & "  a.arpsd_arps_oid IS NULL "

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function insert() As Boolean

        Dim _mstr_oid As String = Guid.NewGuid.ToString
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _code As String

        _code = GetNewNumberYM("arps_print", "arps_code", 5, "ARP" & arps_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try

            ssql = "INSERT INTO  " _
                & "  public.arps_print " _
                & "( " _
                & "  arps_oid, " _
                & "  arps_en_id, " _
                & "  arps_code, " _
                & "  arps_date, " _
                & "  arps_add_by, " _
                & "  arps_add_date, " _
                & "  arps_ptnr_id, " _
                & "  arps_remarks " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetInteger(arps_en_id.EditValue) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetDate(arps_date.DateTime) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetInteger(arps_ptnr_id.Tag) & ",  " _
                & SetSetring(arps_remarks.Text) & "  " _
                & ")"


            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.arpsd_det " _
                        & "( " _
                        & "  arpsd_oid, " _
                        & "  arpsd_arps_oid, " _
                        & "  arpsd_soship_oid, " _
                        & "  arpsd_soship_code " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetSetring(.Item("arpsd_soship_oid")) & ",  " _
                        & SetSetring(.Item("arpsd_soship_code")) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                End With
            Next


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If

            after_success()
            set_row(_mstr_oid, "arps_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True


        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _mstr_oid = .Item("arps_oid")
                arps_en_id.EditValue = .Item("arps_en_id")
                arps_date.DateTime = .Item("arps_date")
                arps_ptnr_id.Tag = .Item("arps_ptnr_id")
                arps_ptnr_id.EditValue = .Item("ptnr_name")
                arps_remarks.EditValue = .Item("arps_remarks")
            End With
            arps_en_id.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                        & "  a.arpsd_oid, " _
                        & "  a.arpsd_arps_oid, " _
                        & "  a.arpsd_soship_oid, " _
                        & "  a.arpsd_soship_code " _
                        & "FROM " _
                        & "  public.arpsd_det a " _
                        & "WHERE " _
                        & "  a.arpsd_arps_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arps_oid") & "' "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        Dim i As Integer
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran
                        '.Command.CommandType = CommandType.Text

                        .Command.CommandText = "UPDATE  " _
                                        & "  public.arps_print   " _
                                        & "SET  " _
                                        & "  arps_en_id = " & SetInteger(arps_en_id.EditValue) & ",  " _
                                        & "  arps_date = " & SetDate(arps_date.DateTime) & ",  " _
                                        & "  arps_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  arps_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                        & "  arps_ptnr_id = " & SetInteger(arps_ptnr_id.Tag) & ",  " _
                                        & "  arps_remarks = " & SetSetring(arps_remarks.Text) & "  " _
                                        & "WHERE  " _
                                        & "  arps_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from arpsd_det " _
                                            & "WHERE  " _
                                            & "  arpsd_arps_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)

                            ssql = "INSERT INTO  " _
                                & "  public.arpsd_det " _
                                & "( " _
                                & "  arpsd_oid, " _
                                & "  arpsd_arps_oid, " _
                                & "  arpsd_soship_oid, " _
                                & "  arpsd_soship_code " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_mstr_oid) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpsd_soship_oid")) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpsd_soship_code")) & "  " _
                                & ")"

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'End With
                        Next


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If
                        .Command.Commit()

                        after_success()
                        set_row(_mstr_oid, "arps_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function
    Public Overrides Function before_delete() As Boolean
        before_delete = True


    End Function
    Public Overrides Function delete_data() As Boolean
        delete_data = False

        gv_master_SelectionChanged(Nothing, Nothing)

        Dim sSQL As String
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

                    sSQL = "DELETE FROM  " _
                        & "  public.arps_print  " _
                        & "WHERE  " _
                        & "  arps_oid ='" & .Item("arps_oid") & "'"

                    ssqls.Add(sSQL)


                End With

                If master_new.PGSqlConn.status_sync = True Then
                    If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                End If

                help_load_data(True)
                MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True


        If ds_edit.Tables(0).Rows.Count = 0 Then
            Box("Detail can't blank")
            Return False
            Exit Function
        End If

        'For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If SetString(ds_edit.Tables(0).Rows(i).Item("arpsd_soship_oid")) = "" Then
        '        Box("AR can't blank")
        '        Return False
        '        Exit Function
        '    End If

        'Next
        Return before_save
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_SelectionChanged(sender, Nothing)
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String = ""

            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  a.arpsd_oid, " _
                & "  a.arpsd_arps_oid, " _
                & "  a.arpsd_soship_oid, " _
                & "  a.arpsd_soship_code " _
                & "FROM " _
                & "  public.arpsd_det a " _
                & "WHERE " _
                & "  a.arpsd_arps_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arps_oid") & "' "

            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "arpsd_soship_code" Then
            Dim frm As New FSOShipMergeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = arps_en_id.EditValue
            frm._ptnr_id = arps_ptnr_id.Tag
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles arps_ptnr_id.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = arps_ptnr_id
            frm._en_id = arps_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            'gv_edit.UpdateCurrentRow()
            'Dim _col As String = gv_edit.FocusedColumn.Name
            'Dim _row As Integer = gv_edit.FocusedRowHandle

            'If _col = "si_desc" Then

            '    For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            '        ds_edit.Tables(0).Rows(i).Item("si_desc") = gv_edit.GetRowCellValue(_row, "si_desc")
            '        ds_edit.Tables(0).Rows(i).Item("wocid_si_id") = gv_edit.GetRowCellValue(_row, "wocid_si_id")
            '    Next

            'ElseIf _col = "loc_desc" Then
            '    For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            '        ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
            '        ds_edit.Tables(0).Rows(i).Item("wocid_loc_id") = gv_edit.GetRowCellValue(_row, "wocid_loc_id")
            '    Next


            'End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub casho_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles arps_en_id.EditValueChanged
        Try
            ' init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub preview()

        Dim _ar_code, _code As String

        ssql = "SELECT   b.arpsd_soship_code FROM  public.arpsd_det b WHERE  b.arpsd_arps_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arps_oid") & "' "
        Dim dt As New DataTable
        dt = GetTableData(ssql)
        _ar_code = ""
        _code = ""
        For Each dr As DataRow In dt.Rows
            _ar_code = _ar_code & "'" & dr(0) & "',"
            _code = dr(0)
        Next

        _ar_code = Microsoft.VisualBasic.Left(_ar_code, _ar_code.Length - 1)
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arps_en_id")
        _type = 13
        _table = "ar_mstr"
        _initial = "ar"
        _code_awal = _code
        _code_akhir = _code

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "select  " _
            & "ar_oid, " _
            & "ar_code, " _
            & "ar_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "ar_date, " _
            & "ar_eff_date, " _
            & "ar_amount, " _
            & "ar_pay_amount, " _
            & "ar_due_date, " _
            & "ar_expt_date, " _
            & "ar_exc_rate, " _
            & "ar_remarks, " _
            & "ar_status, " _
            & "ar_type, " _
            & "ar_credit_term, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cu_symbol, " _
            & "so_code, " _
            & "um_master.code_name as um_name , " _
            & "pt_code,  " _
            & "pt_desc1,  " _
            & "pt_desc2,  " _
            & "sod_disc, sod_tax_inc,  " _
            & "tax_class_mstr.code_name tax_class_name, " _
            & "tax_type_mstr.code_name tax_type_name,  " _
            & "taxr_rate,  " _
            & "ars_invoice,ars_so_price, ars_so_disc_value, " _
            & "ars_invoice_price, " _
            & "ars_invoice_price + (sod_price * sod_disc) as ars_invoice_price2, " _
            & "(ars_invoice * (ars_invoice_price * coalesce(taxr_rate,0) / 100)) as ars_invoice_price_ppn, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
            & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
            & "trim(cmaddr_tax_line_1 || ' ' || cmaddr_tax_line_2 || ' ' || cmaddr_tax_line_3) as cmaddr_line_1_pusat, " _
            & "'Telp : ' || cmaddr_tax_phone_1 || ' ' || ' Fax : ' || cmaddr_tax_phone_2 as cmaddr_line_2_pusat, " _
            & "bk_name,bk_code, " _
            & "ac_name, " _
            & "ar_terbilang, " _
            & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "from ars_ship " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid  " _
            & "inner join ar_mstr on ar_oid = ars_ar_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join so_mstr on so_oid = sod_so_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = ars_tax_class_id " _
            & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = ars_tax_class_id " _
            & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
            & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "inner join cu_mstr on cu_id = ar_cu_id " _
            & "inner join code_mstr um_master on um_master.code_id = sod_um " _
            & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "inner join bk_mstr on bk_id = ar_bk_id " _
            & "inner join ac_mstr on ac_id = bk_ac_id " _
            & "left outer join tranaprvd_dok on tranaprvd_tran_oid = ar_oid " _
            & "where tax_type_mstr.code_name = 'PPN'" _
            & "and ar_code in (" & _ar_code & ")" _
            & "order by ar_code "


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoiceMerge"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arps_code")
        frm.ShowDialog()

    End Sub
End Class
