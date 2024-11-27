Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FAlts
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _alts_oid_mstr As String


    Private Sub FAlts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        alts_en_id.Properties.DataSource = dt_bantu
        alts_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        alts_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        alts_en_id.ItemIndex = 0
    End Sub

    Private Sub alts_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles alts_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Function load_pt_mstr_inv(ByVal par_en_id As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pt_id, pt_code, pt_desc1, pt_desc2, pt_um from pt_mstr " + _
                               " where pt_en_id in (0," + par_en_id.ToString + ")" + _
                               " and pt_dom_id = " + master_new.ClsVar.sdom_id + _
                               " and pt_type = 'I'" + _
                               " order by pt_code "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pt_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (load_pt_mstr_inv(alts_en_id.EditValue))
        alts_pt_id.Properties.DataSource = dt_bantu
        alts_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_desc1").ToString
        alts_pt_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        alts_pt_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bom_mstr(alts_en_id.EditValue))
        alts_bom_id.Properties.DataSource = dt_bantu
        alts_bom_id.Properties.DisplayMember = dt_bantu.Columns("bom_desc").ToString
        alts_bom_id.Properties.ValueMember = dt_bantu.Columns("bom_id").ToString
        alts_bom_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bom Description", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reference", "alts_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "alts_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "alts_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "alts_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "alts_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "alts_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  alts_oid, " _
                & "  alts_dom_id, " _
                & "  alts_en_id, " _
                & "  alts_add_by, " _
                & "  alts_add_date, " _
                & "  alts_upd_by, " _
                & "  alts_upd_date, " _
                & "  alts_pt_id, " _
                & "  alts_bom_id, " _
                & "  alts_ref, " _
                & "  alts_active, " _
                & "  alts_dt, " _
                & "  en_desc, " _
                & "  pt_desc1, " _
                & "  bom_desc " _
                & "FROM  " _
                & "  public.alts_mstr " _
                & "  inner join en_mstr on en_id = alts_en_id " _
                & "  inner join pt_mstr on pt_id = alts_pt_id " _
                & "  inner join bom_mstr on bom_id = alts_bom_id"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        alts_en_id.ItemIndex = 0
        alts_active.EditValue = False
        alts_pt_id.ItemIndex = 0
        alts_bom_id.ItemIndex = 0
        alts_ref.Text = ""
        alts_en_id.Focus()
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _alts_oid As Guid
        _alts_oid = Guid.NewGuid

        'Dim _alts_id As Integer
        '_alts_id = SetInteger(func_coll.GetID("alts_mstr", alts_en_id.GetColumnValue("en_code"), "alts_en_id", alts_en_id.EditValue.ToString))

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
                                                & "  public.alts_mstr " _
                                                & "( " _
                                                & "  alts_oid, " _
                                                & "  alts_dom_id, " _
                                                & "  alts_en_id, " _
                                                & "  alts_add_by, " _
                                                & "  alts_add_date, " _
                                                & "  alts_pt_id, " _
                                                & "  alts_bom_id, " _
                                                & "  alts_ref, " _
                                                & "  alts_active, " _
                                                & "  alts_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_alts_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(alts_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetInteger(alts_pt_id.EditValue) & ",  " _
                                                & SetInteger(alts_bom_id.EditValue) & ",  " _
                                                & SetSetring(alts_ref.Text) & ",  " _
                                                & SetBitYN(alts_active.EditValue) & ",  " _
                                                & " current_timestamp " & "  " _
                                                & ");"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_alts_oid.ToString), "alts_oid")
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
            alts_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _alts_oid_mstr = .Item("alts_oid")
                alts_en_id.EditValue = .Item("alts_en_id")
                alts_pt_id.EditValue = .Item("alts_pt_id")
                alts_bom_id.EditValue = .Item("alts_bom_id")
                alts_ref.Text = SetString(.Item("alts_ref"))
                alts_active.EditValue = SetBitYNB(.Item("alts_active"))
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
                                                & "  public.alts_mstr   " _
                                                & "SET  " _
                                                & "  alts_en_id = " & SetInteger(alts_en_id.EditValue) & ",  " _
                                                & "  alts_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  alts_upd_date = current_timestamp,  " _
                                                & "  alts_pt_id = " & SetInteger(alts_pt_id.EditValue) & ",  " _
                                                & "  alts_bom_id = " & SetInteger(alts_bom_id.EditValue) & ",  " _
                                                & "  alts_ref = " & SetSetring(alts_ref.Text) & ",  " _
                                                & "  alts_active = " & SetBitYN(alts_active.EditValue) & ",  " _
                                                & "  alts_dt = current_timestamp " & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  alts_oid = " & SetSetring(_alts_oid_mstr.ToString) & "  " _
                                                & ";"


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_alts_oid_mstr.ToString), "alts_oid")
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Akan Menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from alts_mstr where alts_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("alts_oid") + "'"
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
