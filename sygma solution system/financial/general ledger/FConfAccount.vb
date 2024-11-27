Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FConfAccount
    Dim _conf_oid_mstr, _value As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssqls As New ArrayList

    Private Sub FConfFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Configuration Name", "confa_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "confa_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.confa_oid, " _
                & "  a.confa_code, " _
                & "  a.confa_desc, " _
                & "  a.confa_ac_id, " _
                & "  b.ac_code, " _
                & "  b.ac_name " _
                & "FROM " _
                & "  public.confa_account a " _
                & "  INNER JOIN public.ac_mstr b ON (a.confa_ac_id = b.ac_id) " _
                & "ORDER BY " _
                & "  a.confa_code"

        Return get_sequel
    End Function


    Public Overrides Sub insert_data_awal()
        confa_code.Enabled = True
        confa_code.Properties.ReadOnly = False
        confa_code.Focus()
        confa_code.Text = ""
        confa_desc.Text = ""
        confa_ac_id.Tag = ""
        confa_ac_id.Text = ""
    End Sub

    'Public Overrides Function insert_data() As Boolean
    '    MessageBox.Show("Insert Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Function

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList
        Dim _conf_oid As Guid
        _conf_oid = Guid.NewGuid

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    'Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                    & "  public.confa_account " _
                                    & "( " _
                                    & "  confa_oid, " _
                                    & "  confa_code, " _
                                    & "  confa_desc, " _
                                    & "  confa_ac_id, " _
                                    & "  confa_add_by, " _
                                    & "  confa_add_date " _
                                    & ") " _
                                    & "VALUES ( " _
                                    & SetSetring(_conf_oid.ToString) & ",  " _
                                    & SetSetring(confa_code.EditValue) & ",  " _
                                    & SetSetring(confa_desc.EditValue) & ",  " _
                                    & SetInteger(confa_ac_id.Tag) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                    & ")"


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Configuration Account " & confa_code.EditValue & " " & confa_desc.EditValue)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        .Command.Commit()
                        set_to_data_insert()
                        after_success()
                        set_row(_conf_oid.ToString, "confa_oid")
                        insert = True
                    Catch ex As CoreLab.PostgreSql.PgSqlException
                        row = 0
                        insert = False
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As CoreLab.PostgreSql.PgSqlException
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        'gc_mstr.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _conf_oid_mstr = .Item("confa_oid")
                confa_code.EditValue = .Item("confa_code")
                confa_desc.EditValue = .Item("confa_desc")
                confa_ac_id.Tag = .Item("confa_ac_id")
                confa_ac_id.Text = SetString(.Item("ac_code")) & " - " & SetString(.Item("ac_name"))
            End With

            confa_code.Enabled = False
            confa_code.Properties.ReadOnly = True
            confa_desc.Focus()
            edit_data = True
        End If
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

        If MessageBox.Show("Are you sure " + master_new.ClsVar.sNama + " to delete this data..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        ssqls.Clear()

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        'Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE from confa_account where confa_oid= " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("confa_oid").ToString) + ""
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        '******************************************************


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

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


        Return delete_data

    End Function
    Public Overrides Function edit()
        edit = True
        Try
            ' _conf_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("conf_oid")
            '_value = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("conf_value")
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                    & "  public.confa_account  " _
                                    & "SET  " _
                                    & "  confa_code = " & SetSetring(confa_code.EditValue) & ",  " _
                                    & "  confa_desc = " & SetSetring(confa_desc.EditValue) & ",  " _
                                    & "  confa_ac_id = " & SetInteger(confa_ac_id.EditValue) & ",  " _
                                    & "  confa_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  confa_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                    & "WHERE  " _
                                    & "  confa_oid = " & SetSetring(_conf_oid_mstr.ToString) & " "


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Configuration Account " & confa_code.EditValue & " " & confa_desc.EditValue)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

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
                        set_row(Trim(_conf_oid_mstr), "confa_oid")
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

    Private Sub confa_ac_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles confa_ac_id.ButtonClick
        Try
            Dim frm As New FAccountSearch()
            frm.set_win(Me)
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
End Class
