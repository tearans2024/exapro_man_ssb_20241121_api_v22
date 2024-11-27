Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FColumnDescription
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _po_oid As String
    Public ds_edit, ds_serial, ds_get As DataSet
    Dim _now As DateTime
    Dim _conf_budget As String
    Public _po_no, _req_no As String
    Public _prj_oid As String
    Dim _qty_sisa As Double
    Dim _progress_pay_sisa As Double
    Dim _conf_value As String
    Dim mf As New master_new.ModFunction
    Dim _code_oid_master As String
    Dim _code_id_master, _code_seq_master As Integer

#Region "SettingAwal"
    Private Sub FColumnDescription_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        code_en_id.Properties.DataSource = dt_bantu
        code_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        code_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        code_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("si_mstr", code_en_id.EditValue))
        'soship_si_id.Properties.DataSource = dt_bantu
        'soship_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        'soship_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        'soship_si_id.ItemIndex = 0
    End Sub

    Private Sub soship_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles code_en_id.EditValueChanged
        'load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "code_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "code_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "code_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "code_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail1, "code_parent", False)
        add_column_copy(gv_detail1, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "code_oid", False)
        add_column(gv_edit, "code_parent", False)
        add_column_edit(gv_edit, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  code_oid, " _
                    & "  code_dom_id, " _
                    & "  code_en_id,en_desc, " _
                    & "  code_add_by, " _
                    & "  code_add_date, " _
                    & "  code_upd_by, " _
                    & "  code_upd_date, " _
                    & "  code_id, " _
                    & "  code_seq, " _
                    & "  code_field, " _
                    & "  code_code, " _
                    & "  code_name, " _
                    & "  code_desc, " _
                    & "  code_default, " _
                    & "  code_parent, " _
                    & "  code_usr_1, " _
                    & "  code_usr_2, " _
                    & "  code_usr_3, " _
                    & "  code_usr_4, " _
                    & "  code_usr_5, " _
                    & "  code_active, " _
                    & "  code_dt " _
                    & "FROM  " _
                    & "  public.code_mstr " _
                    & "  inner join en_mstr on en_id = code_en_id " _
                    & "  where code_field = 'project_layout' " _
                    & "  order by code_name asc "
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  code_oid, " _
            & "  code_dom_id, " _
            & "  code_en_id,en_desc, " _
            & "  code_add_by, " _
            & "  code_add_date, " _
            & "  code_upd_by, " _
            & "  code_upd_date, " _
            & "  code_id, " _
            & "  code_seq, " _
            & "  code_field, " _
            & "  code_code, " _
            & "  code_name, " _
            & "  code_desc, " _
            & "  code_default, " _
            & "  code_parent, " _
            & "  code_usr_1, " _
            & "  code_usr_2, " _
            & "  code_usr_3, " _
            & "  code_usr_4, " _
            & "  code_usr_5, " _
            & "  code_active, " _
            & "  code_dt " _
            & "FROM  " _
            & "  public.code_mstr " _
            & "  inner join en_mstr on en_id = code_en_id " _
            & "  where code_field = 'project_layout_detail' " _
            & "  order by code_name asc "
        load_data_detail(sql, gc_detail1, "detail")
        gv_detail1.BestFitColumns()
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail1.Columns("code_parent").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[code_parent] = " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_id").ToString())
            gv_detail1.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        code_en_id.Focus()
        code_en_id.ItemIndex = 0
        code_code.Text = ""
        code_name.Text = ""
        code_desc.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
            'tcg_detail.SelectedTabPageIndex = 0
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
                        & "  code_oid, " _
                        & "  code_dom_id, " _
                        & "  code_en_id,en_desc, " _
                        & "  code_add_by, " _
                        & "  code_add_date, " _
                        & "  code_upd_by, " _
                        & "  code_upd_date, " _
                        & "  code_id, " _
                        & "  code_seq, " _
                        & "  code_field, " _
                        & "  code_code, " _
                        & "  code_name, " _
                        & "  code_desc, " _
                        & "  code_default, " _
                        & "  code_parent, " _
                        & "  code_usr_1, " _
                        & "  code_usr_2, " _
                        & "  code_usr_3, " _
                        & "  code_usr_4, " _
                        & "  code_usr_5, " _
                        & "  code_active, " _
                        & "  code_dt " _
                        & "FROM  " _
                        & "  public.code_mstr " _
                        & "  inner join en_mstr on en_id = code_en_id " _
                        & "  where code_field = 'project_layout_detail' " _
                        & "  and code_parent = -5365 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function delete_data() As Boolean
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

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position
            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from code_mstr where code_parent = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_id")) _
                                                  & " and code_field = 'project_layout_detail'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from code_mstr where code_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_oid") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position
            With ds.Tables(0).Rows(row)
                _code_oid_master = .Item("code_oid")
                _code_id_master = .Item("code_id")
                code_en_id.EditValue = .Item("code_en_id")
                code_code.Text = .Item("code_code")
                code_name.Text = SetString(.Item("code_name"))
                code_desc.Text = SetString(.Item("code_desc"))
            End With
            code_en_id.Focus()
            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  code_oid, " _
                            & "  code_dom_id, " _
                            & "  code_en_id,en_desc, " _
                            & "  code_add_by, " _
                            & "  code_add_date, " _
                            & "  code_upd_by, " _
                            & "  code_upd_date, " _
                            & "  code_id, " _
                            & "  code_seq, " _
                            & "  code_field, " _
                            & "  code_code, " _
                            & "  code_name, " _
                            & "  code_desc, " _
                            & "  code_default, " _
                            & "  code_parent, " _
                            & "  code_usr_1, " _
                            & "  code_usr_2, " _
                            & "  code_usr_3, " _
                            & "  code_usr_4, " _
                            & "  code_usr_5, " _
                            & "  code_active, " _
                            & "  code_dt " _
                            & "FROM  " _
                            & "  public.code_mstr " _
                            & "  inner join en_mstr on en_id = code_en_id " _
                            & "  where code_field = 'project_layout_detail' " _
                            & "  and code_parent = " & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_id"))
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")
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
        Dim i As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE FROM code_mstr " _
                                            & " WHERE code_field = 'project_layout_detail' " _
                                            & " and code_parent = " & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_id")) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.code_mstr   " _
                                            & "SET  " _
                                            & "  code_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  code_en_id = " & SetInteger(code_en_id.EditValue) & ",  " _
                                            & "  code_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  code_upd_date = current_timestamp, " _
                                            & "  code_code = " & SetSetring(code_code.Text) & ",  " _
                                            & "  code_name = " & SetSetring(code_name.Text) & ",  " _
                                            & "  code_desc = " & SetSetring(code_desc.Text) & ",  " _
                                            & "  code_active = " & SetSetring("Y") & ",  " _
                                            & "  code_dt = current_timestamp " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  code_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_oid").ToString()) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _code_id_detail As Integer = SetNewID_OLD("code_mstr", "code_id")
                        Dim _code_seq_detail As Integer = GetSequence("project_layout_detail")
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _code_id_detail = _code_id_detail + i
                            _code_seq_detail = _code_seq_detail + i

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.code_mstr " _
                                            & "( " _
                                            & "  code_oid, " _
                                            & "  code_dom_id, " _
                                            & "  code_en_id, " _
                                            & "  code_add_by, " _
                                            & "  code_add_date, " _
                                            & "  code_id, " _
                                            & "  code_seq, " _
                                            & "  code_field, " _
                                            & "  code_code, " _
                                            & "  code_name, " _
                                            & "  code_desc, " _
                                            & "  code_parent, " _
                                            & "  code_active, " _
                                            & "  code_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(code_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(_code_id_detail) & ",  " _
                                            & SetInteger(_code_seq_detail) & ",  " _
                                            & SetSetring("project_layout_detail") & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("code_code")) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("code_name")) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("code_desc")) & ",  " _
                                            & SetInteger(_code_id_master) & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(code_code.Text, "code_code")
                        edit = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        edit = False
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
        Dim _date As Date = func_coll.get_tanggal_sistem
        'If ds_edit.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If
        Return before_save
    End Function

    Public Shared Function GetSequence(ByVal par_code_field As String) As Integer
        GetSequence = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(code_seq),0) as max_seq from code_mstr " _
                                           & " where code_field = " & SetSetring(par_code_field)
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetSequence = .DataReader("max_seq")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        GetSequence = GetSequence + 1

        Return GetSequence
    End Function

    Public Overrides Function insert() As Boolean
        Dim _code_id As Integer = SetNewID_OLD("code_mstr", "code_id")
        Dim _code_seq As Integer = GetSequence("project_layout")
        Dim i As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.code_mstr " _
                                            & "( " _
                                            & "  code_oid, " _
                                            & "  code_dom_id, " _
                                            & "  code_en_id, " _
                                            & "  code_add_by, " _
                                            & "  code_add_date, " _
                                            & "  code_id, " _
                                            & "  code_seq, " _
                                            & "  code_field, " _
                                            & "  code_code, " _
                                            & "  code_name, " _
                                            & "  code_desc, " _
                                            & "  code_parent, " _
                                            & "  code_active, " _
                                            & "  code_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(code_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(_code_id) & ",  " _
                                            & SetInteger(_code_seq) & ",  " _
                                            & SetSetring("project_layout") & ",  " _
                                            & SetSetring(code_code.Text) & ",  " _
                                            & SetSetring(code_name.Text) & ",  " _
                                            & SetSetring(code_desc.Text) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _code_id_detail As Integer = _code_id
                        Dim _code_seq_detail As Integer = GetSequence("project_layout_detail")
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _code_id_detail = _code_id_detail + 1
                            _code_seq_detail = _code_seq_detail + i

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.code_mstr " _
                                            & "( " _
                                            & "  code_oid, " _
                                            & "  code_dom_id, " _
                                            & "  code_en_id, " _
                                            & "  code_add_by, " _
                                            & "  code_add_date, " _
                                            & "  code_id, " _
                                            & "  code_seq, " _
                                            & "  code_field, " _
                                            & "  code_code, " _
                                            & "  code_name, " _
                                            & "  code_desc, " _
                                            & "  code_parent, " _
                                            & "  code_active, " _
                                            & "  code_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(code_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(_code_id_detail) & ",  " _
                                            & SetInteger(_code_seq_detail) & ",  " _
                                            & SetSetring("project_layout_detail") & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("code_code")) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("code_name")) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("code_desc")) & ",  " _
                                            & SetInteger(_code_id) & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        after_success()
                        set_row(code_code.Text, "code_code")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function
#End Region

#Region "gv_edit"
    
    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        'If _col = "loc_desc" Then
        '    Dim frm As New FLocationSearch()
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = soship_en_id.EditValue
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Try
        '    gv_serial.Columns("rcvds_rcvd_oid").FilterInfo = _
        '    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rcvds_rcvd_oid] = '" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("rcvd_oid").ToString & "'")
        '    gv_serial.BestFitColumns()
        'Catch ex As Exception
        'End Try
    End Sub

#End Region

    Public Function get_tanggal_sistem() As Date
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select current_date as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_tanggal_sistem = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_tanggal_sistem
    End Function

End Class
