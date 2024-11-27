Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPartnerEntityGroup
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As DataSet

    Private Sub FPartnerEntityGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        'init_le(dbg_en_id, "en_mstr")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_province("province"))
        'With dbg_province
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        '    .ItemIndex = 0
        'End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_city("city"))
        With dbg_city
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_dbgptnrg("ptnrg_grp_dbg"))
        With dbg_ptnrg
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
            .ItemIndex = 0
        End With

    End Sub

    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_creditterms_mstr(dbg_en_id.EditValue))
        'dbg_credit_term.Properties.DataSource = dt_bantu
        'dbg_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'dbg_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'dbg_credit_term.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "dbg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name Group", "dbg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "City", "city", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "dbg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "dbg_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "dbg_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "dbg_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "dbg_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "dbg_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "dbgd_dbg_oid", False)
        add_column(gv_edit, "dbgd_en_id", False)
        add_column(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "dbgd_ptnr_id", False)
        add_column(gv_detail, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "dbgd_oid", False)
        add_column(gv_edit, "dbgd_dbg_oid", False)
        add_column(gv_edit, "dbdg_en_id", False)
        add_column(gv_edit, "dbgd_ptnr_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.dbg_oid, " _
                & "  a.dbg_code, " _
                & "  a.dbg_name, " _
                & "  a.dbg_desc, " _
                & "  a.dbg_city_id, " _
                & "  b.code_name as city, " _
                & "  a.dbg_add_by, " _
                & "  a.dbg_add_date, " _
                & "  a.dbg_upd_by, " _
                & "  a.dbg_upd_date, " _
                & "  a.dbg_ptnrg_id, " _
                & "  c.ptnrg_name, " _
                & "  a.dbg_remarks " _
                & "FROM " _
                & "  public.dbg_group a " _
                & "  INNER JOIN public.code_mstr b ON (a.dbg_city_id = b.code_id) " _
                & "  INNER JOIN public.ptnrg_grp c ON (a.dbg_ptnrg_id = c.ptnrg_id) " _
                & "ORDER BY " _
                & "  a.dbg_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        dbg_code.Text = ""
        dbg_code.Enabled = False
        dbg_name.Text = ""
        dbg_desc.Text = ""
        dbg_remarks.EditValue = ""

        dbg_code.Focus()

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
                        & "  a.dbgd_oid, " _
                        & "  a.dbgd_dbg_oid, " _
                        & "  a.dbgd_en_id, " _
                        & "  b.en_desc, " _
                        & "  a.dbgd_ptnr_id, " _
                        & "  c.ptnr_name " _
                        & "FROM " _
                        & "  public.dbgd_det a " _
                        & "   INNER JOIN public.en_mstr b ON (a.dbgd_en_id = b.en_id) " _
                        & "   INNER JOIN public.ptnr_mstr c ON (a.dbgd_ptnr_id = c.ptnr_id) " _
                        & "WHERE " _
                        & "  a.dbgd_dbg_oid IS NULL "

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

        _code = GetNewNumberYM("dbg_group", "dbg_code", 5, "DBG" & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try

            ssql = "INSERT INTO  " _
                & "  public.dbg_group " _
                & "( " _
                & "  dbg_oid, " _
                & "  dbg_code, " _
                & "  dbg_name, " _
                & "  dbg_desc, " _
                & "  dbg_ptnrg_id, " _
                & "  dbg_city_id, " _
                & "  dbg_add_by, " _
                & "  dbg_add_date, " _
                & "  dbg_remarks " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetSetring(dbg_name.Text) & ",  " _
                & SetSetring(dbg_desc.Text) & ",  " _
                & SetInteger(dbg_ptnrg.EditValue) & ",  " _
                & SetInteger(dbg_city.EditValue) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetSetring(dbg_remarks.Text) & "  " _
                & ")"




            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.dbgd_det " _
                        & "( " _
                        & "  dbgd_oid, " _
                        & "  dbgd_dbg_oid, " _
                        & "  dbgd_en_id, " _
                        & "  dbgd_ptnr_id " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetInteger(.Item("dbgd_en_id")) & ",  " _
                        & SetSetring(.Item("dbgd_ptnr_id")) & "  " _
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
            set_row(_mstr_oid, "dbg_oid")
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
                _mstr_oid = .Item("dbg_oid")
                dbg_code.Text = .Item("dbg_code")
                dbg_name.Text = .Item("dbg_name")
                dbg_desc.EditValue = .Item("dbg_desc")
                dbg_ptnrg.EditValue = .Item("dbg_ptnrg_id")
                dbg_city.EditValue = .Item("dbg_city_id")
                dbg_remarks.EditValue = .Item("dbg_remarks")
            End With
            dbg_code.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                        & "  a.dbgd_oid, " _
                        & "  a.dbgd_dbg_oid, " _
                        & "  a.dbgd_en_id, " _
                        & "  b.en_desc, " _
                        & "  a.dbgd_ptnr_id, " _
                        & "  c.ptnr_name " _
                        & "FROM " _
                        & "  public.dbgd_det a " _
                        & "   INNER JOIN public.en_mstr b ON (a.dbgd_en_id = b.en_id) " _
                        & "   INNER JOIN public.ptnr_mstr c ON (a.dbgd_ptnr_id = c.ptnr_id) " _
                        & "WHERE " _
                        & "  a.dbgd_dbg_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbg_oid") & "' "

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
                                        & "  public.dbg_group   " _
                                        & "SET  " _
                                        & "  dbg_name = " & SetSetring(dbg_name.Text) & ",  " _
                                        & "  dbg_desc = " & SetSetring(dbg_desc.Text) & ",  " _
                                        & "  dbg_ptnrg_id = " & SetInteger(dbg_ptnrg.EditValue) & ",  " _
                                        & "  dbg_city_id = " & SetInteger(dbg_city.EditValue) & ",  " _
                                        & "  dbg_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  dbg_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                        & "  dbg_remarks = " & SetSetring(dbg_remarks.Text) & "  " _
                                        & "WHERE  " _
                                        & "  dbg_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from dbgd_det " _
                                            & "WHERE  " _
                                            & "  dbgd_dbg_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)

                            ssql = "INSERT INTO  " _
                                & "  public.dbgd_det " _
                                & "( " _
                                & "  dbgd_oid, " _
                                & "  dbgd_dbg_oid, " _
                                & "  dbgd_en_id, " _
                                & "  dbgd_ptnr_id " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_mstr_oid) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("dbgd_en_id")) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("dbgd_ptnr_id")) & "  " _
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
                        set_row(_mstr_oid, "dbg_oid")
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
                'With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

                'sSQL = "DELETE FROM  " _
                '    & "  public.dbg_group  " _
                '    & "WHERE  " _
                '    & "  dbg_oid ='" & .Item("dbg_oid") & "'"

                sSQL = "delete from dbg_group where dbg_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbg_oid") + "'"

                ssqls.Add(sSQL)


                'End With

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

        If dbg_name.EditValue = "" Then
            Box("Group Name can't empty")
            Return False
            Exit Function
        End If

        If dbg_desc.EditValue = "" Then
            Box("Description can't empty")
            Return False
            Exit Function
        End If

        'For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If SetString(ds_edit.Tables(0).Rows(i).Item("dbgd_ar_oid")) = "" Then
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
                        & "  a.dbgd_oid, " _
                        & "  a.dbgd_dbg_oid, " _
                        & "  a.dbgd_en_id, " _
                        & "  b.en_desc, " _
                        & "  a.dbgd_ptnr_id, " _
                        & "  c.ptnr_name " _
                        & "FROM " _
                        & "  public.dbgd_det a " _
                        & "   INNER JOIN public.en_mstr b ON (a.dbgd_en_id = b.en_id) " _
                        & "   INNER JOIN public.ptnr_mstr c ON (a.dbgd_ptnr_id = c.ptnr_id) " _
                        & "WHERE " _
                        & "  a.dbgd_dbg_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbg_oid") & "' "

            'sql = "SELECT  " _
            '    & "  a.dbgd_oid, " _
            '    & "  a.dbgd_dbg_oid, " _
            '    & "  a.dbgd_en_id, " _
            '    & "  a.dbgd_ptnr_id " _
            '    & "FROM " _
            '    & "  public.dbgd_det a " _
            '    & "WHERE " _
            '    & "  a.dbgd_dbg_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbg_oid") & "' "

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
        'Dim _dbg_ptnrg_id As Integer = gv_edit.GetRowCellValue(_row, "dbg_ptnrg_id")

        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "ptnr_name" Then
            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = gv_edit.GetRowCellValue(_row, "dbgd_en_id")
            frm._ptnrg_id = dbg_ptnrg.EditValue
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    'Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles dbg_ptnr_id.ButtonClick
    '    Try

    '        Dim frm As New FPartnerSearch
    '        frm.set_win(Me)
    '        frm._obj = dbg_ptnr_id
    '        frm._en_id = dbg_en_id.EditValue
    '        frm.type_form = True
    '        frm.ShowDialog()

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

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

    'Private Sub casho_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbg_en_id.EditValueChanged
    '    Try
    '        ' init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    Public Overrides Sub preview()
        ''Dim ds_bantu As New DataSet
        ''Dim _sql As String

        ''_sql = "SELECT  " _
        ''    & "  cashi_oid, " _
        ''    & "  cashi_dom_id, " _
        ''    & "  cashi_en_id, " _
        ''    & "  cashi_add_by, " _
        ''    & "  cashi_add_date, " _
        ''    & "  cashi_upd_by, " _
        ''    & "  cashi_upd_date, " _
        ''    & "  cashi_bk_id, " _
        ''    & "  cashi_ptnr_id, " _
        ''    & "  cashi_code, " _
        ''    & "  cashi_date, " _
        ''    & "  cashi_remarks, " _
        ''    & "  cashi_reff, " _
        ''    & "  cashi_cu_id, " _
        ''    & "  cashi_exc_rate, " _
        ''    & "  cashi_amount, " _
        ''    & "  cashi_amount * cashi_exc_rate as cashi_amount_ext, " _
        ''    & "  cashi_check_number, " _
        ''    & "  cashi_post_dated_check, " _
        ''    & "  cashid_oid, " _
        ''    & "  cashid_cashi_oid, " _
        ''    & "  cashid_ac_id, " _
        ''    & "  cashid_amount, " _
        ''    & "  cashid_amount * cashi_exc_rate as cashid_amount_ext, " _
        ''    & "  cashid_remarks, " _
        ''    & "  cashid_seq, " _
        ''    & "  bk_name, " _
        ''    & "  ptnr_name, " _
        ''    & "  ac_code, " _
        ''    & "  ac_name, " _
        ''    & "  cmaddr_name, " _
        ''    & "  cmaddr_line_1, " _
        ''    & "  cmaddr_line_2, " _
        ''    & "  cmaddr_line_3 " _
        ''    & "FROM  " _
        ''    & "  cashi_in " _
        ''    & "inner join cashid_detail on cashid_cashi_oid = cashi_oid " _
        ''    & "inner join bk_mstr on bk_id = cashi_bk_id " _
        ''    & "inner join ptnr_mstr on ptnr_id = cashi_ptnr_id " _
        ''    & "inner join cu_mstr on cu_id = cashi_cu_id " _
        ''    & "inner join ac_mstr on ac_id = cashid_ac_id " _
        ''    & "inner join cmaddr_mstr on cmaddr_en_id = cashi_en_id" _
        ''    & "  where cashi_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code") + "'"

        ''Dim frm As New frmPrintDialog
        ''frm._ssql = _sql
        ''frm._report = "XRCashInPrint"
        ''frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code")
        ''frm.ShowDialog()


        'Dim _ar_code, _code As String

        'ssql = "SELECT   b.dbgd_ar_code FROM  public.dbgd_det b WHERE  b.dbgd_dbg_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbg_oid") & "' "
        'Dim dt As New DataTable
        'dt = GetTableData(ssql)
        '_ar_code = ""
        '_code = ""
        'For Each dr As DataRow In dt.Rows
        '    _ar_code = _ar_code & "'" & dr(0) & "',"
        '    _code = dr(0)
        'Next

        '_ar_code = Microsoft.VisualBasic.Left(_ar_code, _ar_code.Length - 1)
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String
        'Dim func_coll As New function_collection

        '_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbg_en_id")
        '_type = 13
        '_table = "ar_mstr"
        '_initial = "ar"
        '_code_awal = _code
        '_code_akhir = _code

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        'Dim ds_bantu As New DataSet
        'Dim _sql As String

        ''_sql = "select  " _
        ''    & "dbg_code, " _
        ''    & "dbg_date, " _
        ''    & "dbg_due_date, " _
        ''    & "dbg_remarks, " _
        ''    & "sod_pt_id, " _
        ''    & "ar_bill_to, " _
        ''    & "ptnr_name, " _
        ''    & "ptnra_line_1, " _
        ''    & "ptnra_line_2, " _
        ''    & "ptnra_line_3, " _
        ''    & "ptnra_zip, " _
        ''    & "ar_cu_id, " _
        ''    & "cu_name, " _
        ''    & "credit_term_mstr.code_name as credit_term_name, " _
        ''    & "cu_symbol, " _
        ''    & "um_master.code_name as um_name, " _
        ''    & "pt_code, " _
        ''    & "pt_desc1, " _
        ''    & "pt_desc2, " _
        ''    & "sod_tax_inc, " _
        ''    & "tax_class_mstr.code_name tax_class_name, " _
        ''    & "tax_type_mstr.code_name tax_type_name, " _
        ''    & "taxr_rate, " _
        ''    & "sum(ars_invoice) as qty_total, " _
        ''    & "ars_invoice_price +(sod_price * sod_disc) as ars_invoice_price2, " _
        ''    & "ars_so_price, " _
        ''    & "sod_disc, " _
        ''    & "ars_so_disc_value, " _
        ''    & "ars_invoice_price, " _
        ''    & "sum(ars_invoice * ars_invoice_price) as ars_invoice_price3, " _
        ''    & "sum(ars_invoice * (ars_invoice_price +(sod_price * sod_disc))) as ars_invoice_price4, " _
        ''    & "sum(ars_invoice * ars_so_disc_value) as ars_invoice_price5, " _
        ''    & "cmaddr_code, " _
        ''    & "cmaddr_name, " _
        ''    & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
        ''    & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
        ''    & "trim(cmaddr_tax_line_1 || ' ' || cmaddr_tax_line_2 || ' ' || cmaddr_tax_line_3) as cmaddr_line_1_pusat, " _
        ''    & "'Telp : ' || cmaddr_tax_phone_1 || ' ' || ' Fax : ' || cmaddr_tax_phone_2 as cmaddr_line_2_pusat, " _
        ''    & "bk_name, " _
        ''    & "bk_code, " _
        ''    & "ac_name, " _
        ''    & "coalesce(tranaprvd_name_1, '') as tranaprvd_name_1, " _
        ''    & "coalesce(tranaprvd_name_2, '') as tranaprvd_name_2, " _
        ''    & "coalesce(tranaprvd_name_3, '') as tranaprvd_name_3, " _
        ''    & "coalesce(tranaprvd_name_4, '') as tranaprvd_name_4, " _
        ''    & "tranaprvd_pos_1, " _
        ''    & "tranaprvd_pos_2, " _
        ''    & "tranaprvd_pos_3, " _
        ''    & "tranaprvd_pos_4 " _
        ''    & "from ars_ship " _
        ''    & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
        ''    & "inner join ar_mstr on ar_oid = ars_ar_oid " _
        ''    & "INNER JOIN public.dbgd_det ON (public.ar_mstr.ar_oid = public.dbgd_det.dbgd_ar_oid) " _
        ''    & "INNER JOIN public.dbg_group ON (public.dbgd_det.dbgd_dbg_oid = public.dbg_group.dbg_oid) " _
        ''    & "inner join sod_det on sod_oid = soshipd_sod_oid " _
        ''    & "inner join so_mstr on so_oid = sod_so_oid " _
        ''    & "inner join pt_mstr on pt_id = sod_pt_id " _
        ''    & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = ars_tax_class_id " _
        ''    & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = ars_tax_class_id " _
        ''    & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
        ''    & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
        ''    & "inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
        ''    & "inner join cu_mstr on cu_id = ar_cu_id " _
        ''    & "inner join code_mstr um_master on um_master.code_id = sod_um " _
        ''    & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
        ''    & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
        ''    & "inner join bk_mstr on bk_id = ar_bk_id " _
        ''    & "inner join ac_mstr on ac_id = bk_ac_id " _
        ''    & "left outer join tranaprvd_dok on tranaprvd_tran_oid = ar_oid " _
        ''    & "where tax_type_mstr.code_name = 'PPN'" _
        ''    & "and ar_code in (" & _ar_code & ")" _
        ''    & "and (ars_invoice) > '0' " _
        ''    & "group by sod_pt_id, " _
        ''    & "dbg_code, " _
        ''    & "dbg_date, " _
        ''    & "dbg_due_date, " _
        ''    & "pt_code, " _
        ''    & "ar_bill_to, " _
        ''    & "ptnra_line_1, " _
        ''    & "ptnr_name, " _
        ''    & "ptnra_line_1, " _
        ''    & "ptnra_line_2, " _
        ''    & "ptnra_line_3, " _
        ''    & "ptnra_zip, " _
        ''    & "cu_name, " _
        ''    & "credit_term_name, " _
        ''    & "cu_symbol, " _
        ''    & "um_name, " _
        ''    & "pt_desc1, " _
        ''    & "pt_desc2, " _
        ''    & "sod_disc, " _
        ''    & "sod_tax_inc, " _
        ''    & "tax_class_name, " _
        ''    & "tax_type_name, " _
        ''    & "taxr_rate, " _
        ''    & "ars_so_price, " _
        ''    & "ars_so_disc_value, " _
        ''    & "ars_invoice_price, " _
        ''    & "sod_price, " _
        ''    & "cmaddr_code, " _
        ''    & "cmaddr_name, " _
        ''    & "cmaddr_line_1, " _
        ''    & "cmaddr_line_2, " _
        ''    & "cmaddr_line_3, " _
        ''    & "cmaddr_phone_1, " _
        ''    & "cmaddr_phone_2, " _
        ''    & "cmaddr_tax_phone_1, " _
        ''    & "cmaddr_tax_phone_2, " _
        ''    & "cmaddr_tax_line_1, " _
        ''    & "cmaddr_tax_line_2, " _
        ''    & "cmaddr_tax_line_3, " _
        ''    & "bk_name, " _
        ''    & "bk_code, " _
        ''    & "ac_name, " _
        ''    & "tranaprvd_name_1, " _
        ''    & "tranaprvd_name_2, " _
        ''    & "tranaprvd_name_3, " _
        ''    & "tranaprvd_name_4, " _
        ''    & "tranaprvd_pos_1, " _
        ''    & "tranaprvd_pos_2, " _
        ''    & "tranaprvd_pos_3, " _
        ''    & "tranaprvd_pos_4, " _
        ''    & "dbg_remarks, " _
        ''    & "ar_cu_id, " _
        ''    & "cu_name " _
        ''    & "order by pt_desc1 ASC"

        '_sql = "SELECT dbg_code, " _
        '    & "dbg_date, " _
        '    & "dbg_due_date, " _
        '    & "dbg_remarks, " _
        '    & "sod_pt_id, " _
        '    & "ar_bill_to, " _
        '    & "ptnr_name, " _
        '    & "ptnra_line_1, " _
        '    & "ptnra_line_2, " _
        '    & "ptnra_line_3, " _
        '    & "ptnra_zip, " _
        '    & "ar_cu_id, " _
        '    & "cu_name, " _
        '    & "cu_symbol, " _
        '    & "credit_term_mstr.code_name as credit_term_name, " _
        '    & "cmaddr_code, " _
        '    & "cmaddr_name, " _
        '    & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
        '    & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
        '    & "bk_name, " _
        '    & "bk_code, " _
        '    & "ar_cu_id, " _
        '    & "cu_name, " _
        '    & "cu_symbol, " _
        '    & "pt_code, " _
        '    & "pt_desc1, " _
        '    & "sum(ars_shipment) AS shipment, " _
        '    & "um_master.code_name as um_name, " _
        '    & "soshipd_um, " _
        '    & "ar_credit_term, " _
        '    & "ars_so_price AS harga_sebelum_diskon, " _
        '    & "sod_disc AS diskon, " _
        '    & "ars_so_disc_value AS nilai_diskon, " _
        '    & "ars_invoice_price AS harga_setelah_diskon, " _
        '    & "sum(ars_shipment * ars_invoice_price) AS total_invoiced, " _
        '    & "sum(ars_shipment * ars_so_price) AS total_bruto, " _
        '    & "sum(ars_shipment * ars_so_disc_value) AS total_diskon " _
        '    & "FROM ar_mstr " _
        '    & "INNER JOIN dbgd_det ON dbgd_ar_oid = ar_oid " _
        '    & "INNER JOIN dbg_group ON dbg_oid = dbgd_dbg_oid " _
        '    & "INNER JOIN ars_ship ON ars_ar_oid = ar_oid " _
        '    & "INNER JOIN soshipd_det ON soshipd_oid = ars_soshipd_oid " _
        '    & "INNER JOIN soship_mstr ON soship_oid = soshipd_soship_oid " _
        '    & "INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
        '    & "INNER JOIN so_mstr ON so_oid = sod_so_oid AND (so_oid = soship_so_oid) " _
        '    & "INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
        '    & "INNER JOIN ptnr_mstr ON ptnr_id = ar_bill_to " _
        '    & "INNER JOIN ptnra_addr ON ptnra_ptnr_oid = ptnr_oid " _
        '    & "INNER JOIN cu_mstr ON cu_id = ar_cu_id " _
        '    & "inner join code_mstr um_master on um_master.code_id = sod_um " _
        '    & "inner join bk_mstr on bk_id = ar_bk_id " _
        '    & "inner join ac_mstr on ac_id = bk_ac_id " _
        '    & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
        '    & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
        '    & "WHERE soship_mstr.soship_code NOT LIKE 'ST%' " _
        '    & "and ar_code in (" & _ar_code & ")" _
        '    & "GROUP BY dbg_code, " _
        '    & "dbg_date, " _
        '    & "dbg_remarks, " _
        '    & "sod_pt_id, " _
        '    & "ar_bill_to, " _
        '    & "ptnr_name, " _
        '    & "ptnra_line_1, " _
        '    & "ptnra_line_2, " _
        '    & "ptnra_line_3, " _
        '    & "ptnra_zip, " _
        '    & "ar_cu_id, " _
        '    & "cu_name, " _
        '    & "cu_symbol, " _
        '    & "pt_code, " _
        '    & "pt_desc1, " _
        '    & "soshipd_um, " _
        '    & "ar_credit_term, " _
        '    & "ars_so_price, " _
        '    & "sod_disc, " _
        '    & "ars_so_disc_value, " _
        '    & "ars_invoice_price, " _
        '    & "cmaddr_code, " _
        '    & "cmaddr_name, " _
        '    & "cmaddr_line_1, " _
        '    & "cmaddr_line_2, " _
        '    & "cmaddr_line_3, " _
        '    & "cmaddr_phone_1, " _
        '    & "cmaddr_phone_2, " _
        '    & "cmaddr_code, " _
        '    & "cmaddr_name, " _
        '    & "dbg_due_date, " _
        '    & "bk_name, " _
        '    & "bk_code, " _
        '    & "credit_term_name, " _
        '    & "um_name " _
        '    & "ORDER BY pt_desc1"


        'Dim frm As New frmPrintDialog
        'frm._ssql = _sql
        'frm._report = "XRInvoiceMergeDetail"
        'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbg_code")
        'frm.ShowDialog()

    End Sub
End Class
