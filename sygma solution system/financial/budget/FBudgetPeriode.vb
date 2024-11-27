Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FBudgetPeriode

    Dim _bdgtp_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FBudgetPeriode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        bdgtp_en_id.Properties.DataSource = dt_bantu
        bdgtp_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        bdgtp_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        bdgtp_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "bdgtp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "bdgtp_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "bdgtp_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "bdgtp_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "bdgtp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "bdgtp_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "bdgtp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "bdgtp_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  bdgtp_oid, " _
                    & "  bdgtp_dom_id, " _
                    & "  bdgtp_en_id,en_desc, " _
                    & "  bdgtp_add_by, " _
                    & "  bdgtp_add_date, " _
                    & "  bdgtp_upd_by, " _
                    & "  bdgtp_upd_date, " _
                    & "  bdgtp_id, " _
                    & "  bdgtp_code, " _
                    & "  bdgtp_remarks, " _
                    & "  bdgtp_start_date, " _
                    & "  bdgtp_end_date, " _
                    & "  bdgtp_dt " _
                    & "FROM  " _
                    & "  public.bdgtp_periode " _
                    & "  inner join en_mstr on en_id = bdgtp_en_id "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        bdgtp_en_id.ItemIndex = 0
        bdgtp_code.Focus()
        bdgtp_code.Text = ""
        bdgtp_remarks.Text = ""
        bdgtp_start_date.EditValue = Today()
        bdgtp_end_date.EditValue = Today()
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        'If Trim(fabk_code.Text) = "" Then
        '    MessageBox.Show("Code Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    fabk_code.Focus()
        '    before_save = False
        '    MessageBox.Show(pt_pl_id.Text)
        'End If
        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _bdgtp_oid As Guid
        _bdgtp_oid = Guid.NewGuid

        Dim _bdgtp_id As Integer
        _bdgtp_id = SetNewID_OLD("bdgtp_periode", "bdgtp_id")

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
                                            & "  public.bdgtp_periode " _
                                            & "( " _
                                            & "  bdgtp_oid, " _
                                            & "  bdgtp_dom_id, " _
                                            & "  bdgtp_en_id, " _
                                            & "  bdgtp_add_by, " _
                                            & "  bdgtp_add_date, " _
                                            & "  bdgtp_id, " _
                                            & "  bdgtp_code, " _
                                            & "  bdgtp_remarks, " _
                                            & "  bdgtp_start_date, " _
                                            & "  bdgtp_end_date, " _
                                            & "  bdgtp_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_bdgtp_oid.ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(bdgtp_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(_bdgtp_id) & ",  " _
                                            & SetSetring(bdgtp_code.Text) & ",  " _
                                            & SetSetring(bdgtp_remarks.Text) & ",  " _
                                            & SetDate(bdgtp_start_date.DateTime) & ",  " _
                                            & SetSetring(bdgtp_end_date.DateTime) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                                            
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(Trim(bdgtp_code.Text), "bdgtp_code")
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
            bdgtp_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _bdgtp_oid = .Item("bdgtp_oid")
                bdgtp_code.Text = .Item("bdgtp_code")
                bdgtp_remarks.Text = .Item("bdgtp_remarks")
                bdgtp_start_date.EditValue = .Item("bdgtp_start_date")
                bdgtp_end_date.EditValue = .Item("bdgtp_end_date")
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
                                            & "  public.bdgtp_periode   " _
                                            & "SET  " _
                                            & "  bdgtp_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  bdgtp_en_id = " & SetInteger(bdgtp_en_id.EditValue) & ",  " _
                                            & "  bdgtp_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  bdgtp_upd_date = current_timestamp,  " _
                                            & "  bdgtp_code = " & SetSetring(bdgtp_code.Text) & ",  " _
                                            & "  bdgtp_remarks = " & SetSetring(bdgtp_remarks.Text) & ",  " _
                                            & "  bdgtp_start_date = " & SetDate(bdgtp_start_date.DateTime) & ",  " _
                                            & "  bdgtp_end_date = " & SetDate(bdgtp_end_date.DateTime) & ",  " _
                                            & "  bdgtp_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bdgtp_oid = " & SetSetring(_bdgtp_oid.ToString()) & "  " _
                                            & ";"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(Trim(bdgtp_code.Text), "bdgtp_code")
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
                            .Command.CommandText = "delete from bdgtp_periode where bdgtp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgtp_oid") + "'"
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


