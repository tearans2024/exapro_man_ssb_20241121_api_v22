Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FBom
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _bom_oid_mstr As String

    Private Sub FBom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        bom_en_id.Properties.DataSource = dt_bantu
        bom_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        bom_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        bom_en_id.ItemIndex = 0
    End Sub

    Private Sub bom_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bom_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(bom_en_id.EditValue, "unitmeasure"))
        bom_um_id.Properties.DataSource = dt_bantu
        bom_um_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        bom_um_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        bom_um_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "bom_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "bom_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "bom_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "bom_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "bom_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "bom_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  bom_oid, " _
                    & "  bom_dom_id, " _
                    & "  bom_en_id, " _
                    & "  bom_add_by, " _
                    & "  bom_add_date, " _
                    & "  bom_upd_by, " _
                    & "  bom_upd_date, " _
                    & "  bom_id, " _
                    & "  bom_code, " _
                    & "  bom_desc, " _
                    & "  bom_um_id, " _
                    & "  bom_active, " _
                    & "  bom_dt, " _
                    & "  en_desc, " _
                    & "  code_name as um_name " _
                    & "FROM  " _
                    & "  public.bom_mstr " _
                    & "  inner join en_mstr on en_id = bom_en_id " _
                    & "  inner join code_mstr on code_id = bom_um_id"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        bom_en_id.ItemIndex = 0
        bom_active.EditValue = False
        bom_code.Text = ""
        bom_desc.Text = ""
        bom_um_id.ItemIndex = 0
        bom_en_id.Focus()
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _bom_oid As Guid
        _bom_oid = Guid.NewGuid

        Dim _bom_id As Integer
        _bom_id = SetInteger(func_coll.GetID("bom_mstr", bom_en_id.GetColumnValue("en_code"), "bom_id", "bom_en_id", bom_en_id.EditValue.ToString))

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
                                            & "  public.bom_mstr " _
                                            & "( " _
                                            & "  bom_oid, " _
                                            & "  bom_dom_id, " _
                                            & "  bom_en_id, " _
                                            & "  bom_add_by, " _
                                            & "  bom_add_date, " _
                                            & "  bom_id, " _
                                            & "  bom_code, " _
                                            & "  bom_desc, " _
                                            & "  bom_um_id, " _
                                            & "  bom_active, " _
                                            & "  bom_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_bom_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(bom_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetInteger(_bom_id) & ",  " _
                                            & SetSetring(bom_code.Text) & ",  " _
                                            & SetSetring(bom_desc.Text) & ",  " _
                                            & SetInteger(bom_um_id.EditValue) & ",  " _
                                            & SetBitYN(bom_active.EditValue) & ",  " _
                                            & " current_timestamp " & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_bom_oid.ToString), "bom_oid")
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
            bom_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _bom_oid_mstr = .Item("bom_oid")
                bom_en_id.EditValue = .Item("bom_en_id")
                bom_active.EditValue = SetBitYNB(.Item("bom_active"))
                bom_code.Text = SetString(.Item("bom_code"))
                bom_desc.Text = SetString(.Item("bom_desc"))
                bom_um_id.EditValue = SetString(.Item("bom_um_id"))
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
                                            & "  public.bom_mstr   " _
                                            & "SET  " _
                                            & "  bom_en_id = " & SetInteger(bom_en_id.EditValue) & ",  " _
                                            & "  bom_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  bom_upd_date = current_timestamp,  " _
                                            & "  bom_code = " & SetSetring(bom_code.Text) & ",  " _
                                            & "  bom_desc = " & SetSetring(bom_desc.Text) & ",  " _
                                            & "  bom_um_id = " & SetInteger(bom_um_id.EditValue) & ",  " _
                                            & "  bom_active = " & SetBitYN(bom_active.EditValue) & ",  " _
                                            & "  bom_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bom_oid = " & SetSetring(_bom_oid_mstr.ToString) & "  " _
                                            & ";"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_bom_oid_mstr.ToString), "bom_oid")
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
                            .Command.CommandText = "delete from bom_mstr where bom_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bom_oid") + "'"
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

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub
End Class
