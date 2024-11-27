Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FCCAcount

    Dim _cca_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FCCAcount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        cca_en_id.Properties.DataSource = dt_bantu
        cca_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cca_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cca_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        cca_ac_id.Properties.DataSource = dt_bantu
        cca_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        cca_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        cca_ac_id.ItemIndex = 0
    End Sub

    Private Sub cca_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cca_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cc_mstr", cca_en_id.EditValue))
        cca_cc_id.Properties.DataSource = dt_bantu
        cca_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        cca_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        cca_cc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cca_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cca_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "cca_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cca_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  cca_oid, " _
                    & "  cca_dom_id, " _
                    & "  cca_en_id, en_code, " _
                    & "  cca_add_by, " _
                    & "  cca_add_date, " _
                    & "  cca_upd_by, " _
                    & "  cca_upd_date, " _
                    & "  cca_cc_id, cc_code,cc_desc, " _
                    & "  cca_ac_id,ac_code,ac_name, " _
                    & "  cca_remarks, " _
                    & "  cca_dt " _
                    & "FROM  " _
                    & "  public.cca_acount " _
                    & " inner join en_mstr on en_id = cca_en_id " _
                    & " inner join cc_mstr on cc_id = cca_cc_id " _
                    & " inner join ac_mstr on ac_id = cca_ac_id "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        cca_en_id.ItemIndex = 0
        cca_en_id.Focus()
        cca_cc_id.ItemIndex = 0
        cca_ac_id.ItemIndex = 0
        cca_remarks.Text = ""
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _cca_oid As Guid
        _cca_oid = Guid.NewGuid
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
                                            & "  public.cca_acount " _
                                            & "( " _
                                            & "  cca_oid, " _
                                            & "  cca_dom_id, " _
                                            & "  cca_en_id, " _
                                            & "  cca_add_by, " _
                                            & "  cca_add_date, " _
                                            & "  cca_cc_id, " _
                                            & "  cca_ac_id, " _
                                            & "  cca_remarks, " _
                                            & "  cca_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_cca_oid.ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(cca_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(cca_cc_id.EditValue) & ",  " _
                                            & SetInteger(cca_ac_id.EditValue) & ",  " _
                                            & SetSetring(cca_remarks.Text) & ",  " _
                                            & " current_timestamp " & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
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
        If MyBase.edit_data = True Then
            cca_en_id.Focus()
            row = BindingContext(ds.Tables(0)).Position
            With ds.Tables(0).Rows(row)
                _cca_oid_mstr = .Item("cca_oid")
                cca_en_id.EditValue = .Item("cca_en_id")
                cca_cc_id.EditValue = .Item("cca_cc_id")
                cca_ac_id.EditValue = .Item("cca_ac_id")
                cca_remarks.Text = SetString(.Item("cca_remarks"))
            End With
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
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
                                            & "  public.cca_acount   " _
                                            & "SET  " _
                                            & "  cca_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  cca_en_id = " & SetInteger(cca_en_id.EditValue) & ",  " _
                                            & "  cca_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  cca_upd_date = current_timestamp,  " _
                                            & "  cca_cc_id = " & SetInteger(cca_cc_id.EditValue) & ",  " _
                                            & "  cca_ac_id = " & SetInteger(cca_ac_id.EditValue) & ",  " _
                                            & "  cca_remarks = " & SetSetring(cca_remarks.Text) & ",  " _
                                            & "  cca_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  cca_oid = " & SetSetring(_cca_oid_mstr.ToString()) & "  " _
                                            & ";"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
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
                            .Command.CommandText = "delete from cca_acount where cca_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cca_oid") + "'"
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


End Class
