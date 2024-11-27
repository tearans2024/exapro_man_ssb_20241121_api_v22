Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FLayoutName
    Dim _tran_oid_mstr As String
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet

    Private Sub FRoutingApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "tran_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Review Amount", "tran_review_amount", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Active", "tran_active", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  tran_oid, " _
                    & "  tran_id, " _
                    & "  tran_table, " _
                    & "  tran_name, " _
                    & "  tran_desc, " _
                    & "  tran_review_amount, " _
                    & "  tran_dt, " _
                    & "  tran_active " _
                    & "FROM  " _
                    & "  public.tran_mstr where tran_table = 'layout'"
        Return get_sequel
    End Function


    Public Overrides Sub insert_data_awal()
        tran_name.Focus()
        tran_name.Text = ""
        tran_desc.Text = ""
        tran_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _tran_oid As Guid
        _tran_oid = Guid.NewGuid

        'Dim i As Integer
        Dim _trans_id As String = ""

        Dim _tran_id As Integer
        _tran_id = func_coll.GetID("tran_mstr", "tran_id")

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tran_mstr " _
                                            & "( " _
                                            & "  tran_oid, " _
                                            & "  tran_id, " _
                                            & "  tran_table, " _
                                            & "  tran_name, " _
                                            & "  tran_desc, " _
                                            & "  tran_review_amount, " _
                                            & "  tran_active, " _
                                            & "  tran_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_tran_oid.ToString) & ",  " _
                                            & SetInteger(_tran_id) & ",  " _
                                            & SetSetring("layout") & ",  " _
                                            & SetSetring(tran_name.Text) & ",  " _
                                            & SetSetring(tran_desc.Text) & ",  " _
                                            & SetBitYN(False) & ",  " _
                                            & SetBitYN(tran_active.EditValue) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ")"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.Commit()

                        after_success()
                        set_row(Trim(_tran_oid.ToString), "tran_oid")
                        insert = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
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
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _tran_oid_mstr = .Item("tran_oid")
                tran_name.Text = SetString(.Item("tran_name"))
                tran_desc.Text = SetString(.Item("tran_desc"))
                tran_active.EditValue = SetBitYNB(.Item("tran_active"))
            End With
            tran_name.Focus()

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        'Dim i As Integer
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
                                            & "  public.tran_mstr   " _
                                            & "SET  " _
                                            & "  tran_name = " & SetSetring(tran_name.Text) & ",  " _
                                            & "  tran_desc = " & SetSetring(tran_desc.Text) & ",  " _
                                            & "  tran_active = " & SetBitYN(tran_active.EditValue) & ",  " _
                                            & "  tran_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  tran_oid = " & SetSetring(_tran_oid_mstr.ToString) & " "

                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.Commit()

                        after_success()
                        set_row(Trim(_tran_oid_mstr.ToString), "tran_oid")
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
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
                Using objinsert As New master_new.CustomCommand
                    With objinsert
.Command.Open()
                        ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            '.Command = .Connection.CreateCommand
                            '.Command.Transaction = sqlTran

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from tran_mstr where tran_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid") + "'"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            .Command.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            'sqlTran.Rollback()
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
