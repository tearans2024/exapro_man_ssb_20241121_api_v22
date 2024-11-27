Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FCostSet

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _oid_mstr As String
    Dim dt_edit As New DataTable

    Private Sub FOrganizationStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        cs_en_id.Properties.DataSource = dt_bantu
        cs_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cs_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cs_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "cs_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "cs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "cs_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Methode", "cs_methode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Default", "cs_is_default", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "cs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "cs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "csd_oid", False)
        add_column_copy(gv_detail, "Category Name", "csc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Element", "csd_element", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "csd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "csd_oid", False)
        add_column_edit_le(gv_edit, "Category Name", "csd_csc_id", DevExpress.Utils.HorzAlignment.Default, init_le_repo("cost_category", cs_en_id.EditValue))
        add_column_edit(gv_edit, "Element", "csd_element", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description", "csd_desc", DevExpress.Utils.HorzAlignment.Default)

        init_le(cs_type, "type_cost")
        init_le(cs_methode, "methode_cost")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  a.cs_oid, " _
                    & "  a.cs_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.cs_id, " _
                    & "  a.cs_name, " _
                    & "  a.cs_desc, " _
                    & "  a.cs_type, " _
                    & "  a.cs_methode, " _
                    & "  a.cs_dt, " _
                    & "  a.cs_add_by, " _
                    & "  a.cs_upd_by, " _
                    & "  a.cs_add_date, " _
                    & "  a.cs_upd_date,a.cs_is_default " _
                    & "FROM " _
                    & "  public.cs_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.cs_en_id = b.en_id) " _
                    & " where cs_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

    End Sub

    Private Sub load_detail()
        Dim sSQL As String
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            sSQL = "SELECT  " _
                & "  a.csd_oid, " _
                & "  a.csd_element, " _
                & "  a.csd_desc, " _
                & "  a.csd_seq, " _
                & "  a.csd_csc_id, " _
                & "  b.csc_name, " _
                & "  a.csd_add_by, " _
                & "  a.csd_add_date, " _
                & "  a.csd_upd_by, " _
                & "  a.csd_upd_date, " _
                & "  b.csc_ac_id, " _
                & "  c.ac_code, " _
                & "  c.ac_name " _
                & "FROM " _
                & "  public.csd_det a " _
                & "  INNER JOIN public.csc_category b ON (a.csd_csc_id = b.csc_id) " _
                & "  INNER JOIN public.ac_mstr c ON (b.csc_ac_id = c.ac_id) " _
                & "WHERE " _
                & "  a.csd_cs_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cs_oid").ToString & "'"

            gc_detail.DataSource = master_new.PGSqlConn.GetTableData(sSQL)
            gv_detail.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Overrides Sub relation_detail()
        Try

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Dim sSQL As String
        cs_en_id.Focus()
        cs_en_id.ItemIndex = 0
        cs_type.ItemIndex = 0
        cs_methode.ItemIndex = 0
        cs_name.Text = ""
        cs_desc.Text = ""
        cs_is_default.Text = "N"

        sSQL = "SELECT  " _
              & "  a.csd_oid, " _
              & "  a.csd_element, " _
              & "  a.csd_desc, " _
              & "  a.csd_seq, " _
              & "  a.csd_csc_id, " _
              & "  b.csc_name, " _
              & "  a.csd_add_by, " _
              & "  a.csd_add_date, " _
              & "  a.csd_upd_by, " _
              & "  a.csd_upd_date " _
              & "FROM " _
              & "  public.csd_det a " _
              & "  INNER JOIN public.csc_category b ON (a.csd_csc_id = b.csc_id) " _
              & "WHERE " _
              & "  a.csd_cs_oid is null"

        dt_edit = master_new.PGSqlConn.GetTableData(sSQL)

        gc_edit.DataSource = dt_edit
        gv_edit.BestFitColumns()

    End Sub

    Public Overrides Function insert() As Boolean

        _oid_mstr = Guid.NewGuid.ToString
        Try
            gv_edit.UpdateCurrentRow()
            dt_edit.AcceptChanges()

            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                    & "  public.cs_mstr " _
                                    & "( " _
                                    & "  cs_oid, " _
                                    & "  cs_dom_id, " _
                                    & "  cs_en_id, " _
                                    & "  cs_add_by, " _
                                    & "  cs_add_date, " _
                                    & "  cs_dt, " _
                                    & "  cs_id, " _
                                    & "  cs_name, " _
                                    & "  cs_desc, " _
                                    & "  cs_type, " _
                                    & "  cs_methode,cs_is_default " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(_oid_mstr) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(cs_en_id.EditValue) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & "current_date" & ",  " _
                                    & SetInteger(func_coll.GetID("cs_mstr", cs_en_id.GetColumnValue("en_code"), "cs_id", "cs_en_id", cs_en_id.EditValue.ToString)) & ",  " _
                                    & SetSetring(cs_name.Text) & ",  " _
                                    & SetSetring(cs_desc.Text) & ",  " _
                                    & SetSetring(cs_type.EditValue) & ",  " _
                                    & SetSetring(cs_methode.EditValue) & ",  " _
                                    & SetSetring(cs_is_default.EditValue) & "  " _
                                    & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For i As Integer = 0 To dt_edit.Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.csd_det " _
                                    & "( " _
                                    & "  csd_oid, " _
                                    & "  csd_cs_oid, " _
                                    & "  csd_add_by, " _
                                    & "  csd_add_date, " _
                                    & "  csd_dt, " _
                                    & "  csd_seq, " _
                                    & "  csd_element, " _
                                    & "  csd_csc_id, " _
                                    & "  csd_desc " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_oid_mstr) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & i & ",  " _
                                    & SetSetring(dt_edit.Rows(i).Item("csd_element")) & ",  " _
                                    & SetInteger(dt_edit.Rows(i).Item("csd_csc_id")) & ",  " _
                                    & SetSetring(dt_edit.Rows(i).Item("csd_desc")) & "  " _
                                    & ")"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()

                        after_success()
                        set_row(_oid_mstr, "cs_oid")
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

    Public Overrides Function edit_data() As Boolean
        Dim sSQL As String
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _oid_mstr = .Item("cs_oid")
                cs_en_id.EditValue = .Item("cs_en_id")
                cs_name.Text = SetString(.Item("cs_name"))
                cs_desc.Text = SetString(.Item("cs_desc"))
                cs_type.EditValue = .Item("cs_type")
                cs_methode.EditValue = .Item("cs_methode")
                cs_is_default.EditValue = .Item("cs_is_default")
            End With
            sSQL = "SELECT  " _
                   & "  a.csd_oid, " _
                   & "  a.csd_element, " _
                   & "  a.csd_desc, " _
                   & "  a.csd_seq, " _
                   & "  a.csd_csc_id, " _
                   & "  b.csc_name, " _
                   & "  a.csd_add_by, " _
                   & "  a.csd_add_date, " _
                   & "  a.csd_upd_by, " _
                   & "  a.csd_upd_date " _
                   & "FROM " _
                   & "  public.csd_det a " _
                   & "  INNER JOIN public.csc_category b ON (a.csd_csc_id = b.csc_id) " _
                   & "WHERE " _
                   & "  a.csd_cs_oid='" & ds.Tables(0).Rows(row).Item("cs_oid") & "' " _
                   & " ORDER BY csd_seq "

            dt_edit = master_new.PGSqlConn.GetTableData(sSQL)

            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()

            cs_en_id.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Try
            gv_edit.UpdateCurrentRow()
            dt_edit.AcceptChanges()
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                & "  public.cs_mstr   " _
                                & "SET  " _
                                & "  cs_en_id = " & SetInteger(cs_en_id.EditValue) & ",  " _
                                & "  cs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  cs_upd_date = " & "current_timestamp" & ",  " _
                                & "  cs_name = " & SetSetring(cs_name.Text) & ",  " _
                                & "  cs_desc = " & SetSetring(cs_desc.Text) & ",  " _
                                & "  cs_type = " & SetSetring(cs_type.EditValue) & ",  " _
                                & "  cs_is_default = " & SetSetring(cs_is_default.EditValue) & ",  " _
                                & "  cs_methode = " & SetSetring(cs_methode.EditValue) & "  " _
                                & "WHERE  " _
                                & "  cs_oid = " & SetSetring(_oid_mstr) & " "


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE FROM  " _
                                & "  public.csd_det  " _
                                & "WHERE  " _
                                & " csd_cs_oid = '" & _oid_mstr & "' "

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i As Integer = 0 To dt_edit.Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.csd_det " _
                                    & "( " _
                                    & "  csd_oid, " _
                                    & "  csd_cs_oid, " _
                                    & "  csd_add_by, " _
                                    & "  csd_add_date, " _
                                    & "  csd_dt, " _
                                    & "  csd_seq, " _
                                    & "  csd_element, " _
                                    & "  csd_csc_id, " _
                                    & "  csd_desc " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_oid_mstr) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & i & ",  " _
                                    & SetSetring(dt_edit.Rows(i).Item("csd_element")) & ",  " _
                                    & SetInteger(dt_edit.Rows(i).Item("csd_csc_id")) & ",  " _
                                    & SetSetring(dt_edit.Rows(i).Item("csd_desc")) & "  " _
                                    & ")"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()

                        after_success()
                        set_row(_oid_mstr, "cs_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
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
                            .Command.CommandText = "delete from cs_mstr where cs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cs_oid") + "'"
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

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_detail()
    End Sub

    Private Sub cs_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cs_en_id.EditValueChanged
        gv_edit.Columns("csd_csc_id").ColumnEdit = init_le_repo("cost_category", cs_en_id.EditValue)
    End Sub

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
End Class
