Imports master_new.ModFunction
Imports CoreLab.PostgreSql

Public Class FCostCategory
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _oid_mstr As String

    Private Sub FAreaMstr_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))

        With csc_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        csc_ac_id.Properties.DataSource = dt_bantu
        csc_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        csc_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        csc_ac_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Category Code", "csc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Category Name", "csc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "csc_add_by", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Date Create", "csc_add_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Update", "csc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "csc_upd_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.csc_oid, " _
            & "  a.csc_en_id, " _
            & "  b.en_desc, " _
            & "  a.csc_id, " _
            & "  a.csc_code, " _
            & "  a.csc_name, " _
            & "  a.csc_add_by, " _
            & "  a.csc_add_date, " _
            & "  a.csc_upd_by, " _
            & "  a.csc_upd_date, " _
            & "  a.csc_ac_id, " _
            & "  c.ac_code, " _
            & "  c.ac_name " _
            & "FROM " _
            & "  public.csc_category a " _
            & "  INNER JOIN public.en_mstr b ON (a.csc_en_id = b.en_id) " _
            & "  INNER JOIN public.ac_mstr c ON (a.csc_ac_id = c.ac_id) " _
            & " where csc_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " ORDER BY csc_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        csc_en_id.Focus()
        csc_code.Text = ""
        csc_name.Text = ""
        csc_ac_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _oid_master As String
        _oid_master = Guid.NewGuid.ToString
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
                                        & "  public.csc_category " _
                                        & "( " _
                                        & "  csc_oid, " _
                                        & "  csc_dom_id, " _
                                        & "  csc_en_id, " _
                                        & "  csc_add_by, " _
                                        & "  csc_add_date, " _
                                        & "  csc_dt, " _
                                        & "  csc_id, " _
                                        & "  csc_code, " _
                                        & "  csc_name, " _
                                        & "  csc_ac_id " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_oid_master) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(csc_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "current_timestamp" & ",  " _
                                        & "current_timestamp" & ",  " _
                                        & SetSetring(func_coll.GetID("csc_category", csc_en_id.GetColumnValue("en_code"), "csc_id", "csc_en_id", csc_en_id.EditValue.ToString)) & ",  " _
                                        & SetSetring(csc_code.Text) & ",  " _
                                        & SetSetring(csc_name.Text) & ",  " _
                                        & SetInteger(csc_ac_id.EditValue) & "  " _
                                        & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(_oid_master, "csc_oid")
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
            csc_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _oid_mstr = .Item("csc_oid")
                csc_en_id.EditValue = .Item("csc_en_id")
                csc_code.Text = SetString(.Item("csc_code"))
                csc_name.Text = SetString(.Item("csc_name"))
                csc_ac_id.EditValue = .Item("csc_ac_id")
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
                                & "  public.csc_category   " _
                                & "SET  " _
                                & "  csc_en_id = " & SetInteger(csc_en_id.EditValue) & ",  " _
                                & "  csc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  csc_upd_date = " & "current_timestamp " & ",  " _
                                & "  csc_code = " & SetSetring(csc_code.Text) & ",  " _
                                & "  csc_name = " & SetSetring(csc_name.Text) & ",  " _
                                & "  csc_ac_id = " & SetInteger(csc_ac_id.EditValue) & "  " _
                                & "WHERE  " _
                                & "  csc_oid = " & SetSetring(_oid_mstr) & " "


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(_oid_mstr, "csc_oid")
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
                            .Command.CommandText = "delete from csc_category where csc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("csc_oid") + "'"
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

    Private Sub csc_code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles csc_code.EditValueChanged
        Dim _par As String = ""

        If csc_code.EditValue = "CMTL" Then
            _par = "dom_mtl_ac_id"
        ElseIf csc_code.EditValue = "CBDN" Then
            _par = "dom_bdn_ac_id"
        ElseIf csc_code.EditValue = "COVH" Then
            _par = "dom_ovh_ac_id"
        ElseIf csc_code.EditValue = "CLBR" Then
            _par = "dom_lbr_ac_id"
        ElseIf csc_code.EditValue = "CSBC" Then
            _par = "dom_sbc_ac_id"
        ElseIf csc_code.EditValue = "" Then
            csc_ac_id.ItemIndex = 0
            Exit Sub
        End If

        Dim _sql As String = "select " + _par + " as col1 from dom_mstr where dom_id = " + master_new.ClsVar.sdom_id.ToString

        csc_ac_id.EditValue = func_coll.get_query_integer(_sql)
    End Sub
End Class
