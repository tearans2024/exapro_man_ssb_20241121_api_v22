Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FCustomerWebsite

    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        init_le(sog_gen_ptnr_mstr_en_id, "en_id")
    End Sub



    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Code", "sog_gen_ptnr_mstr_ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer ID (Web)", "sog_gen_ptnr_mstr_customer_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Name (Web)", "sog_gen_ptnr_mstr_customer_name", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String


        get_sequel = "SELECT  " _
                & "  a.sog_gen_ptnr_mstr_en_id, " _
                & "  a.sog_gen_ptnr_mstr_ptnr_code, " _
                & "  b.ptnr_name, " _
                & "  a.sog_gen_ptnr_mstr_customer_id, " _
                & "  a.sog_gen_ptnr_mstr_customer_name, " _
                & "  c.en_desc " _
                & "FROM " _
                & "  public.sog_gen_ptnr_mstr a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.sog_gen_ptnr_mstr_ptnr_code = b.ptnr_code) " _
                & "  INNER JOIN public.en_mstr c ON (a.sog_gen_ptnr_mstr_en_id = c.en_id) " _
                & "ORDER BY " _
                & "  b.ptnr_name"



        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sog_gen_ptnr_mstr_en_id.ItemIndex = 0
        sog_gen_ptnr_mstr_en_id.Focus()
        ptnr_name.Text = ""
        sog_gen_ptnr_mstr_customer_id.Text = ""
        sog_gen_ptnr_mstr_ptnr_code.Text = ""
        sog_gen_ptnr_mstr_customer_name.Text = ""

        sog_gen_ptnr_mstr_ptnr_code.Enabled = True

        'sc_ce_loc_active.EditValue = False
        'loc_git.EditValue = False
    End Sub

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(loc_id as varchar),5,100) as integer)),0) as max_col  from loc_mstr " + _
                                           " where substring(cast(loc_id as varchar),5,100) <> ''"
                    .InitializeCommand()

                    .DataReader = .ExecuteReader
                    While .DataReader.Read

                        GetID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _loc_oid As Guid
        _loc_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim _loc_id As Integer
        '_loc_id = SetInteger(GetID_Local(sc_le_loc_en.GetColumnValue("en_code")))

        Dim ssql As String
        'ssql = "select count(*) as jml from loc_mstr where lower(loc_desc)='" & sc_te_loc_desc.Text.ToLower _
        '    & "' and loc_en_id=" & sc_le_loc_en.EditValue

        'If SetNumber(master_new.PGSqlConn.GetRowInfo(ssql)(0)) > 0 Then
        '    MessageBox.Show("Duplicate location description")
        '    Exit Function
        'End If

        If sog_gen_ptnr_mstr_ptnr_code.EditValue = "" Then
            Box("Partner Code can't empty")
            Return False
            Exit Function
        End If


        If sog_gen_ptnr_mstr_customer_id.EditValue = "" Then
            Box("Customer ID can't empty")
            Return False
            Exit Function
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
                        .Command.CommandText = "INSERT INTO  " _
                                        & "  public.sog_gen_ptnr_mstr " _
                                        & "( " _
                                        & "  sog_gen_ptnr_mstr_ptnr_code, " _
                                        & "  sog_gen_ptnr_mstr_customer_id, " _
                                        & "  sog_gen_ptnr_mstr_customer_name, " _
                                        & "  sog_gen_ptnr_mstr_en_id " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(sog_gen_ptnr_mstr_ptnr_code.EditValue) & ",  " _
                                        & SetSetring(sog_gen_ptnr_mstr_customer_id.EditValue) & ",  " _
                                        & SetSetring(sog_gen_ptnr_mstr_customer_name.EditValue) & ",  " _
                                        & SetInteger(sog_gen_ptnr_mstr_en_id.EditValue) & "  " _
                                        & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        'If loc_git.EditValue = True Then
                        '    '.Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "UPDATE loc_mstr set loc_git='N' where loc_en_id=" & SetInteger(sc_le_loc_en.EditValue) _
                        '                & " and loc_id <> " & SetInteger(_loc_id)

                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    '.Command.Parameters.Clear()
                        'End If

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert customer web " & ptnr_name.Text)
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
            ptnr_name.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                ptnr_name.EditValue = .Item("ptnr_name")
                sog_gen_ptnr_mstr_customer_id.EditValue = .Item("sog_gen_ptnr_mstr_customer_id")
                sog_gen_ptnr_mstr_customer_name.EditValue = SetString(.Item("sog_gen_ptnr_mstr_customer_name"))
                sog_gen_ptnr_mstr_ptnr_code.EditValue = .Item("sog_gen_ptnr_mstr_ptnr_code")

                sog_gen_ptnr_mstr_ptnr_code.Enabled = False
            End With

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
                                            & "  public.sog_gen_ptnr_mstr  " _
                                            & "SET  " _
                                            & "  sog_gen_ptnr_mstr_customer_id = " & SetSetring(sog_gen_ptnr_mstr_customer_id.EditValue) & ",  " _
                                            & "  sog_gen_ptnr_mstr_customer_name = " & SetSetring(sog_gen_ptnr_mstr_customer_name.EditValue) & ",  " _
                                            & "  sog_gen_ptnr_mstr_en_id = " & SetSetring(sog_gen_ptnr_mstr_en_id.EditValue) & "  " _
                                            & "WHERE  " _
                                            & "  sog_gen_ptnr_mstr_ptnr_code = " & SetSetring(sog_gen_ptnr_mstr_ptnr_code.EditValue) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        'If loc_git.EditValue = True Then
                        '    '.Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "UPDATE loc_mstr set loc_git='N' where loc_en_id=" & SetInteger(sc_le_loc_en.EditValue) _
                        '                & " and loc_oid <> " & SetSetring(_loc_oid_mstr)

                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    '.Command.Parameters.Clear()
                        'End If

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit customer Web " & ptnr_name.Text)
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
                            .Command.CommandText = "delete from sog_gen_ptnr_mstr where sog_gen_ptnr_mstr_ptnr_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sog_gen_ptnr_mstr_ptnr_code") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete customer Web " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sog_gen_ptnr_mstr_ptnr_code"))
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
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("wh_mstr", sc_le_loc_en.EditValue))

        'With sc_le_loc_wh
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("wh_desc").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("wh_id").ToString
        '    .ItemIndex = 0
        'End With

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("is_mstr", sc_le_loc_en.EditValue))

        'With sc_le_loc_is
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("is_code").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("is_id").ToString
        '    .ItemIndex = 0
        'End With

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("loc_cat_mstr", sc_le_loc_en.EditValue))

        'With sc_le_loc_cat
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        '    .ItemIndex = 0
        'End With

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("loc_type_mstr", sc_le_loc_en.EditValue))

        'With sc_le_loc_type
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        '    .ItemIndex = 0
        'End With

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("si_mstr", sc_le_loc_en.EditValue))

        'With sc_le_loc_si
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        '    .ItemIndex = 0
        'End With
    End Sub

    Private Sub sc_le_loc_en_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        load_cb_en()
    End Sub

    Private Sub loc_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sog_gen_ptnr_mstr_ptnr_code.ButtonClick
        Try
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._obj = sog_gen_ptnr_mstr_ptnr_code
            frm._en_id = sog_gen_ptnr_mstr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImport.Click
        Try
            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(AskOpenFile("Excel Files | *.xls"))

            'Dim frm As New frmShowExcelData
            'frm._ds = ds
            'frm.Show()
            Dim ssqls As New ArrayList

            Try
                Using objinsert As New master_new.CustomCommand
                    With objinsert
.Command.Open()
                        ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            '.Command = .Connection.CreateCommand
                            '.Command.Transaction = sqlTran
                            Dim _en_id As String = ""
                            For Each dr As DataRow In ds.Tables(0).Rows

                                _en_id = get_id("en_mstr", "lower(en_desc)", dr("Entity").ToString.ToLower, "en_id")

                                If _en_id = "" Then
                                    MsgBox("Entity error")
                                    Exit Sub
                                End If

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sog_gen_ptnr_mstr " _
                                                & "( " _
                                                & "  sog_gen_ptnr_mstr_ptnr_code, " _
                                                & "  sog_gen_ptnr_mstr_customer_id, " _
                                                & "  sog_gen_ptnr_mstr_customer_name, " _
                                                & "  sog_gen_ptnr_mstr_en_id " _
                                                & ") " _
                                                & "VALUES ( " _
                                                & SetSetring(dr("Partner Code")) & ",  " _
                                                & SetSetring(dr("Customer ID")) & ",  " _
                                                & SetSetring(dr("Customer Name")) & ",  " _
                                                & SetInteger(_en_id) & "  " _
                                                & ")"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next



                            'If loc_git.EditValue = True Then
                            '    '.Command.CommandType = CommandType.Text
                            '    .Command.CommandText = "UPDATE loc_mstr set loc_git='N' where loc_en_id=" & SetInteger(sc_le_loc_en.EditValue) _
                            '                & " and loc_id <> " & SetInteger(_loc_id)

                            '    ssqls.Add(.Command.CommandText)
                            '    .Command.ExecuteNonQuery()
                            '    '.Command.Parameters.Clear()
                            'End If

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Import customer web ")
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

                        Catch ex As PgSqlException
                            'sqlTran.Rollback()
                            MessageBox.Show(ex.Message)

                        End Try
                    End With
                End Using
            Catch ex As Exception
                row = 0
                MessageBox.Show(ex.Message)
            End Try


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
