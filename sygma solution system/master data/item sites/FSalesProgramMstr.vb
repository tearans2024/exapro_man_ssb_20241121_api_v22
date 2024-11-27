Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FSalesProgramMstr
    Dim _sls_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FSalesProgramMstr_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("entity", ""))
        'sls_en_id.Properties.DataSource = dt_bantu
        'sls_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'sls_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'sls_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "sls_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "sls_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Desc", "sls_Desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "sls_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "sls_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sls_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sls_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sls_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  sls_add_by, " _
                    & "  sls_add_date, " _
                    & "  sls_upd_by, " _
                    & "  sls_upd_date, " _
                    & "  sls_code, " _
                    & "  sls_name, " _
                    & "  sls_desc, " _
                    & "  sls_active, " _
                    & "  sls_dt " _
                    & " FROM  " _
                    & "  sls_program"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        'sls_en_id.Focus()
        'sls_en_id.ItemIndex = 0
        sls_code.Text = ""
        sls_name.Text = ""
        sls_desc.Text = ""
        sls_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _sls_oid As Guid
        _sls_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

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
                                            & "  public.sls_program " _
                                            & "( " _
                                             & "  sls_oid, " _
                                            & "  sls_add_by, " _
                                            & "  sls_add_date, " _
                                            & "  sls_code, " _
                                            & "  sls_name, " _
                                            & "  sls_desc, " _
                                            & "  sls_active, " _
                                            & "  sls_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_sls_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(sls_code.Text) & ",  " _
                                            & SetSetring(sls_name.Text) & ",  " _
                                            & SetSetring(sls_desc.Text) & ",  " _
                                            & SetBitYN(sls_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ");"
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
                        set_row(Trim(sls_code.Text), "sls_code")
                        insert = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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
                _sls_oid = .Item("sls_oid")
                sls_code.Text = .Item("sls_code")
                sls_name.Text = .Item("sls_name")
                sls_desc.Text = .Item("sls_desc")
                sls_active.EditValue = IIf(.Item("sls_active") = "Y", True, False)
            End With
            sls_code.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList

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
                                            & "  public.sls_program   " _
                                            & "SET  " _
                                            & "  sls_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  sls_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  sls_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sls_code = " & SetSetring(sls_code.Text) & ",  " _
                                            & "  sls_desc = " & SetSetring(sls_desc.Text) & ",  " _
                                            & "  sls_active = " & SetBitYN(sls_active.EditValue) & ",  " _
                                            & "  sls_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sls_oid = " & SetSetring(_sls_oid.ToString) & " "
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
                        set_row(Trim(sls_code.Text), "sls_code")
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
                Using objinsert As New master_new.CustomCommand
                    With objinsert
.Command.Open()
                        ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            '.Command = .Connection.CreateCommand
                            '.Command.Transaction = sqlTran

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from sls_program where sls_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid") + "'"
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

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim _hasil As Double

            _hasil = DateDiff(DateInterval.Minute, CDate("19/09/2015"), Now)

            MsgBox(_hasil)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
