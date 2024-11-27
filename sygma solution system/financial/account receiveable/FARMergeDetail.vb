Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FARMergeDetail
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As DataSet

    Private Sub FARMergeDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(arp_en_id, "en_mstr")

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "arp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "arp_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "arp_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "arp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "arp_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "arp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "arp_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "arpd_arp_oid", False)
        add_column(gv_detail, "AR Number", "arpd_ar_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "arpd_oid", False)
        add_column(gv_edit, "arpd_arp_oid", False)
        add_column(gv_edit, "arpd_ar_oid", False)
        add_column(gv_edit, "AR Number", "arpd_ar_code", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.arp_oid, " _
                & "  a.arp_en_id, " _
                & "  c.en_desc, " _
                & "  a.arp_code, " _
                & "  a.arp_date, " _
                & "  a.arp_add_by, " _
                & "  a.arp_add_date, " _
                & "  a.arp_upd_by, " _
                & "  a.arp_upd_date, " _
                & "  a.arp_ptnr_id, " _
                & "  b.ptnr_name,arp_remarks " _
                & "FROM " _
                & "  public.arp_print a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.arp_ptnr_id = b.ptnr_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.arp_en_id = c.en_id) " _
                & "WHERE " _
                & " a.arp_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") and a.arp_date BETWEEN  " _
                & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & " ORDER BY " _
                & "  a.arp_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        arp_en_id.EditValue = ""
        arp_ptnr_id.EditValue = ""
        arp_date.DateTime = CekTanggal()
        arp_remarks.EditValue = ""
        arp_en_id.Focus()

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    .SQL = "SELECT  " _
                        & "  a.arpd_oid, " _
                        & "  a.arpd_arp_oid, " _
                        & "  a.arpd_ar_oid, " _
                        & "  a.arpd_ar_code " _
                        & "FROM " _
                        & "  public.arpd_det a " _
                        & "WHERE " _
                        & "  a.arpd_arp_oid IS NULL "

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

        _code = GetNewNumberYM("arp_print", "arp_code", 5, "ARP" & arp_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try

            ssql = "INSERT INTO  " _
                & "  public.arp_print " _
                & "( " _
                & "  arp_oid, " _
                & "  arp_en_id, " _
                & "  arp_code, " _
                & "  arp_date, " _
                & "  arp_add_by, " _
                & "  arp_add_date, " _
                & "  arp_ptnr_id, " _
                & "  arp_remarks " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetInteger(arp_en_id.EditValue) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetDate(arp_date.DateTime) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetInteger(arp_ptnr_id.Tag) & ",  " _
                & SetSetring(arp_remarks.Text) & "  " _
                & ")"


            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.arpd_det " _
                        & "( " _
                        & "  arpd_oid, " _
                        & "  arpd_arp_oid, " _
                        & "  arpd_ar_oid, " _
                        & "  arpd_ar_code " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetSetring(.Item("arpd_ar_oid")) & ",  " _
                        & SetSetring(.Item("arpd_ar_code")) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                End With
            Next


            If MyPGDll.PGSqlConn.status_sync = True Then
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
            set_row(_mstr_oid, "arp_oid")
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
                _mstr_oid = .Item("arp_oid")
                arp_en_id.EditValue = .Item("arp_en_id")
                arp_date.DateTime = .Item("arp_date")
                arp_ptnr_id.Tag = .Item("arp_ptnr_id")
                arp_ptnr_id.EditValue = .Item("ptnr_name")
                arp_remarks.EditValue = .Item("arp_remarks")
            End With
            arp_en_id.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                        & "  a.arpd_oid, " _
                        & "  a.arpd_arp_oid, " _
                        & "  a.arpd_ar_oid, " _
                        & "  a.arpd_ar_code " _
                        & "FROM " _
                        & "  public.arpd_det a " _
                        & "WHERE " _
                        & "  a.arpd_arp_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arp_oid") & "' "

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
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        .Command.CommandText = "UPDATE  " _
                                        & "  public.arp_print   " _
                                        & "SET  " _
                                        & "  arp_en_id = " & SetInteger(arp_en_id.EditValue) & ",  " _
                                        & "  arp_date = " & SetDate(arp_date.DateTime) & ",  " _
                                        & "  arp_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  arp_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                        & "  arp_ptnr_id = " & SetInteger(arp_ptnr_id.Tag) & ",  " _
                                        & "  arp_remarks = " & SetSetring(arp_remarks.Text) & "  " _
                                        & "WHERE  " _
                                        & "  arp_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from arpd_det " _
                                            & "WHERE  " _
                                            & "  arpd_arp_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)

                            ssql = "INSERT INTO  " _
                                & "  public.arpd_det " _
                                & "( " _
                                & "  arpd_oid, " _
                                & "  arpd_arp_oid, " _
                                & "  arpd_ar_oid, " _
                                & "  arpd_ar_code " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_mstr_oid) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpd_ar_oid")) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("arpd_ar_code")) & "  " _
                                & ")"

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'End With
                        Next


                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If
                        sqlTran.Commit()

                        after_success()
                        set_row(_mstr_oid, "arp_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
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
                        & "  public.arp_print  " _
                        & "WHERE  " _
                        & "  arp_oid ='" & .Item("arp_oid") & "'"

                    ssqls.Add(sSQL)


                End With

                If MyPGDll.PGSqlConn.status_sync = True Then
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
        '    If SetString(ds_edit.Tables(0).Rows(i).Item("arpd_ar_oid")) = "" Then
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
                & "  a.arpd_oid, " _
                & "  a.arpd_arp_oid, " _
                & "  a.arpd_ar_oid, " _
                & "  a.arpd_ar_code " _
                & "FROM " _
                & "  public.arpd_det a " _
                & "WHERE " _
                & "  a.arpd_arp_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arp_oid") & "' "

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

        If _col = "arpd_ar_code" Then
            Dim frm As New FDRCRMemoSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = arp_en_id.EditValue
            frm._ptnr_id = arp_ptnr_id.Tag
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles arp_ptnr_id.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = arp_ptnr_id
            frm._en_id = arp_en_id.EditValue
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

    Private Sub casho_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles arp_en_id.EditValueChanged
        Try
            ' init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub preview()
        Dim _ar_code, _code As String

        ssql = "SELECT   b.arpd_ar_code FROM  public.arpd_det b WHERE  b.arpd_arp_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arp_oid") & "' "
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

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arp_en_id")
        _type = 13
        _table = "arp_print"
        _initial = "arp"
        _code_awal = _code
        _code_akhir = _code

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
                & "  public.sod_det.sod_pt_id, " _
                & "  public.arp_print.arp_code, " _
                & "  public.arp_print.arp_date, " _
                & "  public.arp_print.arp_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.ptnra_addr.ptnra_line, " _
                & "  public.ptnra_addr.ptnra_line_1, " _
                & "  public.ptnra_addr.ptnra_line_2, " _
                & "  public.ptnra_addr.ptnra_line_3, " _
                & "  public.ptnra_addr.ptnra_zip, " _
                & "  public.sod_det.sod_um, " _
                & "  public.ar_mstr.ar_date, " _
                & "  public.ar_mstr.ar_eff_date, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.pt_mstr.pt_code, " _
                & "  public.pt_mstr.pt_desc1, " _
                & "  public.pt_mstr.pt_desc2, " _
                & "  public.ars_ship.ars_so_price, " _
                & "  SUM(public.ars_ship.ars_invoice) AS total_sub, " _
                & "  SUM(public.ars_ship.ars_invoice * public.ars_ship.ars_so_price) AS total_before, " _
                & "  public.sod_det.sod_disc, " _
                & "  SUM((public.ars_ship.ars_invoice * public.ars_ship.ars_so_price) - (public.ars_ship.ars_invoice * public.ars_ship.ars_so_price * sod_disc)) AS total_after, " _
                & "  public.cmaddr_mstr.cmaddr_code, " _
                & "  public.cmaddr_mstr.cmaddr_name, " _
                & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
                & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
                & "trim(cmaddr_tax_line_1 || ' ' || cmaddr_tax_line_2 || ' ' || cmaddr_tax_line_3) as cmaddr_line_1_pusat, " _
                & "'Telp : ' || cmaddr_tax_phone_1 || ' ' || ' Fax : ' || cmaddr_tax_phone_2 as cmaddr_line_2_pusat, " _
                & "  public.bk_mstr.bk_name, " _
                & "  public.bk_mstr.bk_code " _
                & "ac_name, " _
                & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "FROM " _
                & "  public.arp_print " _
                & "  INNER JOIN public.arpd_det ON (public.arp_print.arp_oid = public.arpd_det.arpd_arp_oid) " _
                & "  INNER JOIN public.ar_mstr ON (public.arpd_det.arpd_ar_oid = public.ar_mstr.ar_oid) " _
                & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
                & "  INNER JOIN public.so_mstr ON (public.arso_so.arso_so_oid = public.so_mstr.so_oid) " _
                & "  INNER JOIN public.soship_mstr ON (public.so_mstr.so_oid = public.soship_mstr.soship_so_oid) " _
                & "  INNER JOIN public.soshipd_det ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
                & "  INNER JOIN public.ars_ship ON (public.ar_mstr.ar_oid = public.ars_ship.ars_ar_oid) " _
                & "  AND (public.soshipd_det.soshipd_oid = public.ars_ship.ars_soshipd_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.ptnra_addr ON (public.ptnr_mstr.ptnr_oid = public.ptnra_addr.ptnra_ptnr_oid) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.sod_det ON (public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid) " _
                & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                & "  INNER JOIN public.en_mstr ON (public.arp_print.arp_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.cmaddr_mstr ON (public.cmaddr_mstr.cmaddr_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = public.bk_mstr.bk_id) " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = ar_oid " _
                & "where ar_code in (" & _ar_code & ")" _
                & "and (ars_invoice) > '0', " _
                & "and (so_return) <> 'Y' " _
                & "GROUP BY " _
                & "  public.sod_det.sod_pt_id, " _
                & "  public.arp_print.arp_code, " _
                & "  public.arp_print.arp_date, " _
                & "  public.arp_print.arp_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.ptnra_addr.ptnra_line, " _
                & "  public.ptnra_addr.ptnra_line_1, " _
                & "  public.ptnra_addr.ptnra_line_2, " _
                & "  public.ptnra_addr.ptnra_line_3, " _
                & "  public.ptnra_addr.ptnra_zip, " _
                & "  public.sod_det.sod_um, " _
                & "  public.ar_mstr.ar_date, " _
                & "  public.ar_mstr.ar_eff_date, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.pt_mstr.pt_code, " _
                & "  public.pt_mstr.pt_desc1, " _
                & "  public.pt_mstr.pt_desc2, " _
                & "  public.ars_ship.ars_so_price, " _
                & "  public.sod_det.sod_disc, " _
                & "  public.cmaddr_mstr.cmaddr_code, " _
                & "  public.cmaddr_mstr.cmaddr_name, " _
                & "  cmaddr_line_1, " _
                & " cmaddr_line_2, " _
                & " cmaddr_line_3, " _
                & " cmaddr_phone_1, " _
                & " cmaddr_phone_2, " _
                & " cmaddr_tax_phone_1, " _
                & " cmaddr_tax_phone_2, " _
                & " cmaddr_tax_line_1, " _
                & " cmaddr_tax_line_2, " _
                & " cmaddr_tax_line_3," _
                & "  public.bk_mstr.bk_name, " _
                & "  public.bk_mstr.bk_code, " _
                & " tranaprvd_name_1, " _
                & " tranaprvd_name_2, " _
                & " tranaprvd_name_3, " _
                & " tranaprvd_name_4, " _
                & " tranaprvd_pos_1, " _
                & " tranaprvd_pos_2, " _
                & " tranaprvd_pos_3, " _
                & " tranaprvd_pos_4 " _
                & "ORDER BY " _
                & "  public.arp_print.arp_code, " _
                & "  public.sod_det.sod_pt_id, " _
                & "  public.pt_mstr.pt_code"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoiceMergeDetail"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("arp_code")
        frm.ShowDialog()

    End Sub
End Class
