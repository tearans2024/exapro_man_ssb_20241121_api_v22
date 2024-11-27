Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FTransactionCollection
    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FTransactionCollection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("entity", ""))
        'si_en_id.Properties.DataSource = dt_bantu
        'si_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'si_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'si_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Tran Table", "tranc_table", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "tranc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "tranc_active", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  tranc_oid, " _
                    & "  tranc_id, " _
                    & "  tranc_table, " _
                    & "  tranc_desc, " _
                    & "  tranc_dt, " _
                    & "  tranc_active " _
                    & "FROM  " _
                    & "  public.tranc_coll ;"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        tranc_table.Focus()
        tranc_table.Text = ""
        tranc_desc.Text = ""
        tranc_active.EditValue = True
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _tranc_oid As Guid
        _tranc_oid = Guid.NewGuid
        Dim _tranc_id As Integer
        _tranc_id = SetNewID_OLD("tranc_coll", "tranc_id")
        Dim ssqls As New ArrayList

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
                                            & "  public.tranc_coll " _
                                            & "( " _
                                            & "  tranc_oid, " _
                                            & "  tranc_id, " _
                                            & "  tranc_table, " _
                                            & "  tranc_desc, " _
                                            & "  tranc_dt, " _
                                            & "  tranc_active " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_tranc_oid.ToString()) & ",  " _
                                            & SetInteger(_tranc_id) & ",  " _
                                            & SetSetring(tranc_table.Text) & ",  " _
                                            & SetSetring(tranc_desc.Text) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetBitYN(tranc_active.EditValue) & "  " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        after_success()
                        set_row(Trim(tranc_table.Text), "tranc_table")
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
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                tranc_table.Text = .Item("tranc_table")
                tranc_desc.Text = .Item("tranc_desc")
                tranc_active.EditValue = SetBitYNB(.Item("tranc_active"))
            End With
            tranc_table.Focus()
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
                                            & "  public.tranc_coll   " _
                                            & "SET  " _
                                            & "  tranc_table = " & SetSetring(tranc_table.Text) & ",  " _
                                            & "  tranc_desc = " & SetSetring(tranc_desc.Text) & ",  " _
                                            & "  tranc_dt = current_timestamp, " _
                                            & "  tranc_active = " & SetBitYN(tranc_active.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  tranc_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tranc_oid").ToString()) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(Trim(tranc_table.Text), "tranc_table")
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
                            .Command.CommandText = "delete from tranc_coll where tranc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tranc_oid") + "'"
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
