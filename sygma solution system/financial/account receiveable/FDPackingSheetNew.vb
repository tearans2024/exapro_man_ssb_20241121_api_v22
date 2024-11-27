Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FDPackingSheetNew
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit_so, ds_edit_shipment As DataSet

    Private Sub FDPackingSheetNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(dop_en_id, "en_mstr")
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "dop_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "dop_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Payment Date", "dop_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "dop_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "dop_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "dop_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "dop_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "dop_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_so, "dopd_dop_oid", False)
        add_column(gv_detail_so, "dopd_so_oid", False)
        add_column(gv_detail_so, "SO Number", "dopd_so_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "Shipment Number", "dopd_shipment_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_shipment, "dopdd_dop_oid", False)
        add_column_copy(gv_detail_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Qty Open", "ars_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "Qty Shipment", "ars_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "Close Line", "ars_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_so, "dopd_oid", False)
        add_column(gv_edit_so, "dopd_dop_oid", False)
        add_column(gv_edit_so, "dopd_so_oid", False)
        add_column(gv_edit_so, "SO Number", "dopd_so_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_so, "dopd_shipment_oid", False)
        'add_column(gv_edit_so, "Shipment Number", "dopd_shipment_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_shipment, "dopdd_oid", False)
        add_column(gv_edit_shipment, "dopdd_dop_oid", False)
        add_column_edit(gv_edit_shipment, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "pt_id", False)
        add_column(gv_edit_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Qty Open", "ars_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_shipment, "Qty Shipment", "ars_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit_shipment, "Close Line", "ars_close_line", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  public.dop_print.dop_oid, " _
                & "  public.dop_print.dop_en_id, " _
                & "  public.dop_print.dop_code, " _
                & "  public.dop_print.dop_date, " _
                & "  public.dop_print.dop_add_by, " _
                & "  public.dop_print.dop_add_date, " _
                & "  public.dop_print.dop_upd_by, " _
                & "  public.dop_print.dop_upd_date, " _
                & "  public.dop_print.dop_ptnr_id, " _
                & "  public.dop_print.dop_remarks, " _
                & "  public.dop_print.dop_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name " _
                & "FROM " _
                & "  public.dop_print " _
                & "  INNER JOIN public.en_mstr ON (public.dop_print.dop_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.dop_print.dop_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                & "WHERE " _
                & " dop_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") and dop_date BETWEEN  " _
                & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & " ORDER BY " _
                & "  dop_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        dop_en_id.EditValue = ""
        dop_ptnr_id.EditValue = ""
        dop_date.DateTime = CekTanggal()
        dop_due_date.DateTime = CekTanggal()
        dop_remarks.EditValue = ""
        dop_en_id.Focus()

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_so = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb

                    .SQL = "SELECT  " _
                            & "  public.dopd_det.dopd_oid, " _
                            & "  public.dopd_det.dopd_dop_oid, " _
                            & "  public.dopd_det.dopd_so_oid, " _
                            & "  public.dopd_det.dopd_so_code, " _
                            & "  public.dopd_det.dopd_shipment_oid, " _
                            & "  public.dopd_det.dopd_shipment_code " _
                            & "FROM " _
                            & "  public.dopd_det " _
                            & "WHERE " _
                            & "  public.dopd_det.dopd_dop_oid IS NULL"

                    .InitializeCommand()
                    .FillDataSet(ds_edit_so, "edit")
                    gc_edit_so.DataSource = ds_edit_so.Tables(0)
                    gv_edit_so.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_shipment = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  dopdd_oid, True as ceklist, " _
                        & "  dopdd_dop_oid, " _
                        & "  dopdd_seq, " _
                        & "  dopdd_soshipd_oid, " _
                        & "  soship_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  dopdd_open,dopdd_shipment, " _
                        & "  dopdd_lose_line, " _
                        & "  dopdd_dt " _
                        & "FROM  " _
                        & "  public.ars_ship " _
                        & "  inner join public.soshipd_det on public.dopdd_det.dopdd_soshipd_oid = public.soshipd_det.soshipd_oid " _
                        & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                        & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                        & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                        & "  inner join public.dop_print on public.dopdd_det.dopdd_dop_oid = public.dop_print.dop_oid" _
                        & " where ars_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_shipment, "shipment")
                    gc_edit_shipment.DataSource = ds_edit_shipment.Tables(0)
                    gv_edit_shipment.BestFitColumns()
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

        _code = GetNewNumberYM("dop_print", "dop_code", 5, "DOP" & dop_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit_so.EmbeddedNavigator.Buttons.DoClick(gc_edit_so.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit_so.Tables(0).AcceptChanges()

        Try

            ssql = "INSERT INTO  " _
                & "  public.dop_print " _
                & "( " _
                & "  dop_oid, " _
                & "  dop_en_id, " _
                & "  dop_code, " _
                & "  dop_date, " _
                & "  dop_due_date, " _
                & "  dop_add_by, " _
                & "  dop_add_date, " _
                & "  dop_ptnr_id, " _
                & "  dop_remarks " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetInteger(dop_en_id.EditValue) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetDate(dop_date.DateTime) & ",  " _
                & SetDate(dop_due_date.DateTime) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetInteger(dop_ptnr_id.Tag) & ",  " _
                & SetSetring(dop_remarks.Text) & "  " _
                & ")"


            ssqls.Add(ssql)

            For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                With ds_edit_so.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.dopd_det " _
                        & "( " _
                        & "  dopd_oid, " _
                        & "  dopd_dop_oid, " _
                        & "  dopd_so_oid, " _
                        & "  dopd_so_code " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetSetring(.Item("dopd_so_oid")) & ",  " _
                        & SetSetring(.Item("dopd_so_code")) & "  " _
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
            set_row(_mstr_oid, "dop_oid")
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
                _mstr_oid = .Item("dop_oid")
                dop_en_id.EditValue = .Item("dop_en_id")
                dop_date.DateTime = .Item("dop_date")
                dop_due_date.DateTime = .Item("dop_due_date")
                dop_ptnr_id.Tag = .Item("dop_ptnr_id")
                dop_ptnr_id.EditValue = .Item("ptnr_name")
                dop_remarks.EditValue = .Item("dop_remarks")
            End With
            dop_en_id.Focus()

            ds_edit_so = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                        & "  a.dopd_oid, " _
                        & "  a.dopd_dop_oid, " _
                        & "  a.dopd_so_oid, " _
                        & "  a.dopd_so_code " _
                        & "FROM " _
                        & "  public.dopd_det a " _
                        & "WHERE " _
                        & "  a.dopd_dop_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dop_oid") & "' "

                        .InitializeCommand()
                        .FillDataSet(ds_edit_so, "detail")
                        gc_edit_so.DataSource = ds_edit_so.Tables(0)
                        gv_edit_so.BestFitColumns()
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
        gv_edit_so.UpdateCurrentRow()
        ds_edit_so.AcceptChanges()
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
                                        & "  public.dop_print   " _
                                        & "SET  " _
                                        & "  dop_en_id = " & SetInteger(dop_en_id.EditValue) & ",  " _
                                        & "  dop_date = " & SetDate(dop_date.DateTime) & ",  " _
                                        & "  dop_due_date = " & SetDate(dop_due_date.DateTime) & ",  " _
                                        & "  dop_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  dop_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                        & "  dop_ptnr_id = " & SetInteger(dop_ptnr_id.Tag) & ",  " _
                                        & "  dop_remarks = " & SetSetring(dop_remarks.Text) & "  " _
                                        & "WHERE  " _
                                        & "  dop_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from dopd_det " _
                                            & "WHERE  " _
                                            & "  dopd_dop_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)

                            ssql = "INSERT INTO  " _
                                & "  public.dopd_det " _
                                & "( " _
                                & "  dopd_oid, " _
                                & "  dopd_dop_oid, " _
                                & "  dopd_so_oid, " _
                                & "  dopd_so_code " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_mstr_oid) & ",  " _
                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("dopd_so_oid")) & ",  " _
                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("dopd_so_code")) & "  " _
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
                        set_row(_mstr_oid, "dop_oid")
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
                        & "  public.dop_print  " _
                        & "WHERE  " _
                        & "  dop_oid ='" & .Item("dop_oid") & "'"

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


        If ds_edit_so.Tables(0).Rows.Count = 0 Then
            Box("Detail can't blank")
            Return False
            Exit Function
        End If

        'For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If SetString(ds_edit.Tables(0).Rows(i).Item("dopd_so_oid")) = "" Then
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
                & "  a.dopd_oid, " _
                & "  a.dopd_dop_oid, " _
                & "  a.dopd_so_oid, " _
                & "  a.dopd_so_code " _
                & "FROM " _
                & "  public.dopd_det a " _
                & "WHERE " _
                & "  a.dopd_dop_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dop_oid") & "' "

            load_data_detail(sql, gc_detail_so, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_so.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit_so.FocusedColumn.Name
        Dim _row As Integer = gv_edit_so.FocusedRowHandle
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "dopd_so_code" Then
            Dim frm As New FSalesOrderPackingSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = dop_en_id.EditValue
            frm._ptnr_id = dop_ptnr_id.Tag
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    Private Sub sb_retrieve_shipment_item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_receive_item.Click
        'Try
        '    If SetNumber(ar_pay_prepaid.EditValue) > 0 Then
        '        Box("Pencatatan Debit Credit Memo DP hanya untuk membukukan DP (belum dengan piutangnya), silahkan buat debit credit memo lagi untuk mencatat piutangnya.")
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try

        If ds_edit_so.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_so.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim _so_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
            _so_code = _so_code + "'" + ds_edit_so.Tables(0).Rows(i).Item("dopd_so_code") + "',"
        Next

        _so_code = _so_code.Substring(0, _so_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  soshipd_oid, True as ceklist, " _
                        & "  soshipd_soship_oid, " _
                        & "  soshipd_sod_oid, " _
                        & "  soship_code, " _
                        & "  soship_date, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  coalesce(sod_qty_shipment,0) as qty_shipment,  " _
                        & "  ((soshipd_qty * -1) - coalesce(soshipd_qty_inv,0))  as qty_open " _
                        & "FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join so_mstr on so_oid = sod_so_oid " _
                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
                        & "  inner join code_mstr on code_id = sod_tax_class " _
                        & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                        & "  and so_code in (" + _so_code + ") " _
                        & "  order by soship_date "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "soship_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'Dim ssql As String
        'ssql = "select sum(coalesce(so_shipping_charges,0)) as jml from so_mstr where so_code in (" & _so_code & ")"

        Dim dt_so As New DataTable
        dt_so = GetTableData(ssql)

        'For Each dr_so As DataRow In dt_so.Rows
        '    ar_shipping_charges.EditValue = dr_so(0)
        'Next


        ds_edit_shipment.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("qty_open") <> 0 Then
                _dtrow = ds_edit_shipment.Tables(0).NewRow
                _dtrow("ars_oid") = Guid.NewGuid.ToString
                _dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
                _dtrow("ars_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid")
                _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("ars_taxable") = ds_bantu.Tables(0).Rows(i).Item("sod_taxable")
                _dtrow("ars_tax_class_id") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class")
                _dtrow("taxclass_name") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class_name")
                _dtrow("ars_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc")
                _dtrow("ars_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("ars_shipment") = ds_bantu.Tables(0).Rows(i).Item("qty_shipment")
                _dtrow("ars_invoice") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("ars_so_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
                _dtrow("ars_so_disc_value") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                _dtrow("ars_invoice_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                _dtrow("tot_inv_price") = _dtrow("ars_invoice_price") * _dtrow("ars_invoice")
                _dtrow("ars_close_line") = "N"
                ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
            End If
        Next

        'Dim ssql As String
        ssql = "SELECT  " _
            & "  soship_date " _
            & "FROM  " _
            & "  public.soshipd_det " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "  inner join so_mstr on so_oid = sod_so_oid " _
            & "  inner join pt_mstr on pt_id = sod_pt_id " _
            & "  inner join code_mstr on code_id = sod_tax_class " _
            & "  where coalesce(soshipd_close_line,'N') = 'N' " _
            & "  and so_code in (" + _so_code + ") and soship_is_shipment='Y'   " _
            & "  order by soship_date desc"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)


        'ar_eff_date.DateTime = dt.Rows(0).Item("soship_date")
        '(i) disini pasti line yang terakhir

        ds_edit_shipment.Tables(0).AcceptChanges()

        gv_edit_shipment.BestFitColumns()

    End Sub

    Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles dop_ptnr_id.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = dop_ptnr_id
            'frm._ptnr_id = dop_ptnr_id.EditValue
            frm._en_id = dop_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_so.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_so.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_so.DeleteSelectedRows()
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

    Private Sub casho_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dop_en_id.EditValueChanged
        Try
            ' init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub preview()
        'Dim ds_bantu As New DataSet
        'Dim _sql As String

        '_sql = "SELECT  " _
        '    & "  cashi_oid, " _
        '    & "  cashi_dom_id, " _
        '    & "  cashi_en_id, " _
        '    & "  cashi_add_by, " _
        '    & "  cashi_add_date, " _
        '    & "  cashi_upd_by, " _
        '    & "  cashi_upd_date, " _
        '    & "  cashi_bk_id, " _
        '    & "  cashi_ptnr_id, " _
        '    & "  cashi_code, " _
        '    & "  cashi_date, " _
        '    & "  cashi_remarks, " _
        '    & "  cashi_reff, " _
        '    & "  cashi_cu_id, " _
        '    & "  cashi_exc_rate, " _
        '    & "  cashi_amount, " _
        '    & "  cashi_amount * cashi_exc_rate as cashi_amount_ext, " _
        '    & "  cashi_check_number, " _
        '    & "  cashi_post_dated_check, " _
        '    & "  cashid_oid, " _
        '    & "  cashid_cashi_oid, " _
        '    & "  cashid_ac_id, " _
        '    & "  cashid_amount, " _
        '    & "  cashid_amount * cashi_exc_rate as cashid_amount_ext, " _
        '    & "  cashid_remarks, " _
        '    & "  cashid_seq, " _
        '    & "  bk_name, " _
        '    & "  ptnr_name, " _
        '    & "  ac_code, " _
        '    & "  ac_name, " _
        '    & "  cmaddr_name, " _
        '    & "  cmaddr_line_1, " _
        '    & "  cmaddr_line_2, " _
        '    & "  cmaddr_line_3 " _
        '    & "FROM  " _
        '    & "  cashi_in " _
        '    & "inner join cashid_detail on cashid_cashi_oid = cashi_oid " _
        '    & "inner join bk_mstr on bk_id = cashi_bk_id " _
        '    & "inner join ptnr_mstr on ptnr_id = cashi_ptnr_id " _
        '    & "inner join cu_mstr on cu_id = cashi_cu_id " _
        '    & "inner join ac_mstr on ac_id = cashid_ac_id " _
        '    & "inner join cmaddr_mstr on cmaddr_en_id = cashi_en_id" _
        '    & "  where cashi_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code") + "'"

        'Dim frm As New frmPrintDialog
        'frm._ssql = _sql
        'frm._report = "XRCashInPrint"
        'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code")
        'frm.ShowDialog()


        Dim _ar_code, _code As String

        ssql = "SELECT   b.dopd_so_code FROM  public.dopd_det b WHERE  b.dopd_dop_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dop_oid") & "' "
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

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dop_en_id")
        _type = 13
        _table = "ar_mstr"
        _initial = "ar"
        _code_awal = _code
        _code_akhir = _code

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "select  " _
            & "dop_code, " _
            & "dop_date, " _
            & "dop_due_date, " _
            & "dop_remarks, " _
            & "sod_pt_id, " _
            & "ar_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cu_symbol, " _
            & "um_master.code_name as um_name, " _
            & "pt_code, " _
            & "pt_desc1, " _
            & "pt_desc2, " _
            & "sod_tax_inc, " _
            & "tax_class_mstr.code_name tax_class_name, " _
            & "tax_type_mstr.code_name tax_type_name, " _
            & "taxr_rate, " _
            & "sum(ars_invoice) as qty_total, " _
            & "ars_invoice_price +(sod_price * sod_disc) as ars_invoice_price2, " _
            & "ars_so_price, " _
            & "sod_disc, " _
            & "ars_so_disc_value, " _
            & "ars_invoice_price, " _
            & "sum(ars_invoice * ars_invoice_price) as ars_invoice_price3, " _
            & "sum(ars_invoice * (ars_invoice_price +(sod_price * sod_disc))) as ars_invoice_price4, " _
            & "sum(ars_invoice * ars_so_disc_value) as ars_invoice_price5, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
            & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
            & "trim(cmaddr_tax_line_1 || ' ' || cmaddr_tax_line_2 || ' ' || cmaddr_tax_line_3) as cmaddr_line_1_pusat, " _
            & "'Telp : ' || cmaddr_tax_phone_1 || ' ' || ' Fax : ' || cmaddr_tax_phone_2 as cmaddr_line_2_pusat, " _
            & "bk_name, " _
            & "bk_code, " _
            & "ac_name, " _
            & "coalesce(tranaprvd_name_1, '') as tranaprvd_name_1, " _
            & "coalesce(tranaprvd_name_2, '') as tranaprvd_name_2, " _
            & "coalesce(tranaprvd_name_3, '') as tranaprvd_name_3, " _
            & "coalesce(tranaprvd_name_4, '') as tranaprvd_name_4, " _
            & "tranaprvd_pos_1, " _
            & "tranaprvd_pos_2, " _
            & "tranaprvd_pos_3, " _
            & "tranaprvd_pos_4 " _
            & "from ars_ship " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
            & "inner join ar_mstr on ar_oid = ars_ar_oid " _
            & "INNER JOIN public.dopd_det ON (public.ar_mstr.ar_oid = public.dopd_det.dopd_so_oid) " _
            & "INNER JOIN public.dop_print ON (public.dopd_det.dopd_dop_oid = public.dop_print.dop_oid) " _
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
            & "and (ars_invoice) > '0' " _
            & "group by sod_pt_id, " _
            & "dop_code, " _
            & "dop_date, " _
            & "dop_due_date, " _
            & "pt_code, " _
            & "ar_bill_to, " _
            & "ptnra_line_1, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "cu_name, " _
            & "credit_term_name, " _
            & "cu_symbol, " _
            & "um_name, " _
            & "pt_desc1, " _
            & "pt_desc2, " _
            & "sod_disc, " _
            & "sod_tax_inc, " _
            & "tax_class_name, " _
            & "tax_type_name, " _
            & "taxr_rate, " _
            & "ars_so_price, " _
            & "ars_so_disc_value, " _
            & "ars_invoice_price, " _
            & "sod_price, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_phone_1, " _
            & "cmaddr_phone_2, " _
            & "cmaddr_tax_phone_1, " _
            & "cmaddr_tax_phone_2, " _
            & "cmaddr_tax_line_1, " _
            & "cmaddr_tax_line_2, " _
            & "cmaddr_tax_line_3, " _
            & "bk_name, " _
            & "bk_code, " _
            & "ac_name, " _
            & "tranaprvd_name_1, " _
            & "tranaprvd_name_2, " _
            & "tranaprvd_name_3, " _
            & "tranaprvd_name_4, " _
            & "tranaprvd_pos_1, " _
            & "tranaprvd_pos_2, " _
            & "tranaprvd_pos_3, " _
            & "tranaprvd_pos_4, " _
            & "dop_remarks, " _
            & "ar_cu_id, " _
            & "cu_name " _
            & "order by pt_desc1 ASC"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoiceMergeDetail"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dop_code")
        frm.ShowDialog()

    End Sub
End Class
