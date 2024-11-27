Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FUserTranColumn

    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FUserTranColumn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        trancusr_en_id.Properties.DataSource = dt_bantu
        trancusr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        trancusr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        trancusr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_user())
        trancusr_usr_id.Properties.DataSource = dt_bantu
        trancusr_usr_id.Properties.DisplayMember = dt_bantu.Columns("name").ToString
        trancusr_usr_id.Properties.ValueMember = dt_bantu.Columns("userid").ToString
        trancusr_usr_id.ItemIndex = 0
    End Sub

    Private Sub trancusr_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trancusr_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_tran_column())
        trancusr_tranc_id.Properties.DataSource = dt_bantu
        trancusr_tranc_id.Properties.DisplayMember = dt_bantu.Columns("tranc_desc").ToString
        trancusr_tranc_id.Properties.ValueMember = dt_bantu.Columns("tranc_id").ToString
        trancusr_tranc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tran Coll", "tranc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User ID", "usernama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "trancusr_active", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  trancusr_id, " _
                    & "  trancusr_tranc_id,tranc_desc, " _
                    & "  trancusr_usr_id,usernama, " _
                    & "  trancusr_active, " _
                    & "  trancusr_en_id,en_desc " _
                    & "FROM  " _
                    & "  public.trancusr_user  " _
                    & "  inner join en_mstr on en_id = trancusr_en_id " _
                    & "  inner join tranc_coll on tranc_id = trancusr_tranc_id " _
                    & "  inner join tconfuser on userid = trancusr_usr_id " _
                    & " where trancusr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        trancusr_en_id.Focus()
        trancusr_en_id.ItemIndex = 0
        trancusr_tranc_id.ItemIndex = 0
        trancusr_usr_id.ItemIndex = 0
        trancusr_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _trancusr_id As Integer
        _trancusr_id = SetNewID_OLD("trancusr_user", "trancusr_id")
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
                                            & "  public.trancusr_user " _
                                            & "( " _
                                            & "  trancusr_id, " _
                                            & "  trancusr_tranc_id, " _
                                            & "  trancusr_usr_id, " _
                                            & "  trancusr_active, " _
                                            & "  trancusr_en_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetInteger(_trancusr_id) & ",  " _
                                            & SetInteger(trancusr_tranc_id.EditValue) & ",  " _
                                            & SetInteger(trancusr_usr_id.EditValue) & ",  " _
                                            & SetBitYN(trancusr_active.EditValue) & ",  " _
                                            & SetInteger(trancusr_en_id.EditValue) & "  " _
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
                        'set_row(Trim(si_code.Text), "si_code")
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
                trancusr_en_id.EditValue = .Item("trancusr_en_id")
                trancusr_tranc_id.EditValue = .Item("trancusr_tranc_id")
                trancusr_usr_id.EditValue = .Item("trancusr_tranc_id")
                trancusr_active.EditValue = SetBitYNB(.Item("trancusr_active"))
            End With
            trancusr_en_id.Focus()
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
                                            & "  public.trancusr_user   " _
                                            & "SET  " _
                                            & "  trancusr_tranc_id = " & SetInteger(trancusr_tranc_id.EditValue) & ",  " _
                                            & "  trancusr_usr_id = " & SetInteger(trancusr_usr_id.EditValue) & ",  " _
                                            & "  trancusr_active = " & SetBitYN(trancusr_active.EditValue) & ",  " _
                                            & "  trancusr_en_id = " & SetInteger(trancusr_en_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  trancusr_id = " & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("trancusr_id")) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        'set_row(Trim(si_code.Text), "si_code")
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
                            .Command.CommandText = "delete from trancusr_user where trancusr_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("trancusr_id")) + " "
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
