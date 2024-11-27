Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FSOGenerateConfiguration

    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        init_le(so_gen_conf_en_id, "en_id")
    End Sub



    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "App Code", "so_gen_conf_app_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "so_gen_conf_location", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Term", "so_gen_conf_credit_term", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "so_gen_conf_tax_class", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Site ID", "so_gen_conf_si_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Type", "so_gen_conf_pay_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "payment Method", "so_gen_conf_pay_method", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Acount ID", "so_gen_conf_ar_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency Code", "so_gen_conf_cu_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank ID", "so_gen_conf_bk_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "so_gen_conf_sales_person", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List ID", "so_gen_conf_pricelist_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PPN Type", "so_gen_conf_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group ID", "so_gen_conf_ptnr_group_id", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String


        get_sequel = "SELECT  " _
                & "  public.so_gen_conf.so_gen_conf_location, " _
                & "  public.so_gen_conf.so_gen_conf_credit_term, " _
                & "  public.so_gen_conf.so_gen_conf_tax_class, " _
                & "  public.so_gen_conf.so_gen_conf_si_id, " _
                & "  public.so_gen_conf.so_gen_conf_pay_type, " _
                & "  public.so_gen_conf.so_gen_conf_pay_method, " _
                & "  public.so_gen_conf.so_gen_conf_ar_ac_id, " _
                & "  public.so_gen_conf.so_gen_conf_cu_id, " _
                & "  public.so_gen_conf.so_gen_conf_bk_id, " _
                & "  public.so_gen_conf.so_gen_conf_sales_person, " _
                & "  public.so_gen_conf.so_gen_conf_app_code, " _
                & "  public.so_gen_conf.so_gen_conf_pricelist_id, " _
                & "  public.so_gen_conf.so_gen_conf_ppn_type, " _
                & "  public.so_gen_conf.so_gen_conf_ptnr_group_id, " _
                & "  public.so_gen_conf.so_gen_conf_en_id, " _
                & "  public.en_mstr.en_desc " _
                & "FROM " _
                & "  public.so_gen_conf " _
                & "  INNER JOIN public.en_mstr ON (public.so_gen_conf.so_gen_conf_en_id = public.en_mstr.en_id) " _
                & "ORDER BY " _
                & "  public.en_mstr.en_desc"




        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        so_gen_conf_en_id.ItemIndex = 0
        so_gen_conf_en_id.Focus()
        so_gen_conf_app_code.Text = ""
        so_gen_conf_location.Text = ""
        so_gen_conf_credit_term.Text = ""
        so_gen_conf_tax_class.Text = ""
        so_gen_conf_si_id.Text = ""
        so_gen_conf_pay_type.Text = ""
        so_gen_conf_pay_method.Text = ""
        so_gen_conf_ar_ac_id.Text = ""
        so_gen_conf_cu_id.Text = ""
        so_gen_conf_bk_id.Text = ""
        so_gen_conf_sales_person.Text = ""
        so_gen_conf_app_code.Text = ""
        so_gen_conf_pricelist_id.Text = ""
        so_gen_conf_ppn_type.Text = ""
        so_gen_conf_ptnr_group_id.Text = ""


        'sog_gen_ptnr_mstr_ptnr_code.Enabled = True

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



        If so_gen_conf_location.EditValue = "" Then
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
                                        & "  public.so_gen_conf " _
                                        & "( " _
                                        & "  so_gen_conf_en_id, " _
                                        & "  so_gen_conf_location, " _
                                        & "  so_gen_conf_credit_term, " _
                                        & "  so_gen_conf_tax_class, " _
                                        & "  so_gen_conf_si_id, " _
                                        & "  so_gen_conf_pay_type, " _
                                        & "  so_gen_conf_pay_method, " _
                                        & "  so_gen_conf_ar_ac_id, " _
                                        & "  so_gen_conf_cu_id, " _
                                        & "  so_gen_conf_bk_id, " _
                                        & "  so_gen_conf_sales_person, " _
                                        & "  so_gen_conf_app_code, " _
                                        & "  so_gen_conf_pricelist_id, " _
                                        & "  so_gen_conf_ppn_type, " _
                                        & "  so_gen_conf_ptnr_group_id " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetInteger(so_gen_conf_en_id.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_location.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_credit_term.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_tax_class.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_si_id.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_pay_type.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_pay_method.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_ar_ac_id.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_cu_id.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_bk_id.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_sales_person.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_app_code.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_pricelist_id.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_ppn_type.EditValue) & ",  " _
                                        & SetSetring(so_gen_conf_ptnr_group_id.EditValue) & "  " _
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
                        .Command.CommandText = insert_log("Insert Configuration Generate SO(" & so_gen_conf_app_code.Text)
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
            so_gen_conf_app_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                'so_gen_conf_app_code.EditValue = .Item("ptnr_name")
                'so_gen_conf_location.EditValue = .Item("sog_gen_ptnr_mstr_customer_id")
                'so_gen_conf_credit_term.EditValue = SetString(.Item("sog_gen_ptnr_mstr_customer_name"))


                so_gen_conf_en_id.EditValue = .Item("so_gen_conf_en_id")
                so_gen_conf_app_code.EditValue = .Item("so_gen_conf_app_code")
                so_gen_conf_location.EditValue = .Item("so_gen_conf_location")
                so_gen_conf_credit_term.EditValue = .Item("so_gen_conf_credit_term")
                so_gen_conf_tax_class.EditValue = .Item("so_gen_conf_tax_class")
                so_gen_conf_si_id.EditValue = .Item("so_gen_conf_si_id")
                so_gen_conf_pay_type.EditValue = .Item("so_gen_conf_pay_type")
                so_gen_conf_pay_method.EditValue = .Item("so_gen_conf_pay_method")
                so_gen_conf_ar_ac_id.EditValue = .Item("so_gen_conf_ar_ac_id")
                so_gen_conf_cu_id.EditValue = .Item("so_gen_conf_cu_id")
                so_gen_conf_bk_id.EditValue = .Item("so_gen_conf_bk_id")
                so_gen_conf_sales_person.EditValue = .Item("so_gen_conf_sales_person")
                so_gen_conf_pricelist_id.EditValue = .Item("so_gen_conf_pricelist_id")
                so_gen_conf_ppn_type.EditValue = .Item("so_gen_conf_ppn_type")
                so_gen_conf_ptnr_group_id.EditValue = .Item("so_gen_conf_ptnr_group_id")

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
                                & "  public.so_gen_conf  " _
                                & "SET  " _
                                & "  so_gen_conf_location = " & SetSetring(so_gen_conf_location.EditValue) & ",  " _
                                & "  so_gen_conf_credit_term = " & SetSetring(so_gen_conf_credit_term.EditValue) & ",  " _
                                & "  so_gen_conf_tax_class = " & SetSetring(so_gen_conf_tax_class.EditValue) & ",  " _
                                & "  so_gen_conf_si_id = " & SetSetring(so_gen_conf_si_id.EditValue) & ",  " _
                                & "  so_gen_conf_pay_type = " & SetSetring(so_gen_conf_pay_type.EditValue) & ",  " _
                                & "  so_gen_conf_pay_method = " & SetSetring(so_gen_conf_pay_method.EditValue) & ",  " _
                                & "  so_gen_conf_ar_ac_id = " & SetSetring(so_gen_conf_ar_ac_id.EditValue) & ",  " _
                                & "  so_gen_conf_cu_id = " & SetSetring(so_gen_conf_cu_id.EditValue) & ",  " _
                                & "  so_gen_conf_bk_id = " & SetSetring(so_gen_conf_bk_id.EditValue) & ",  " _
                                & "  so_gen_conf_sales_person = " & SetSetring(so_gen_conf_sales_person.EditValue) & ",  " _
                                & "  so_gen_conf_pricelist_id = " & SetSetring(so_gen_conf_pricelist_id.EditValue) & ",  " _
                                & "  so_gen_conf_ppn_type = " & SetSetring(so_gen_conf_ppn_type.EditValue) & ",  " _
                                & "  so_gen_conf_ptnr_group_id = " & SetSetring(so_gen_conf_ptnr_group_id.EditValue) & "  " _
                                & "WHERE  " _
                                & "  so_gen_conf_en_id = " & SetInteger(so_gen_conf_en_id.EditValue) & " "


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
                        .Command.CommandText = insert_log("Edit Configuration Generate SO " & so_gen_conf_app_code.Text)
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
                            .Command.CommandText = "delete from so_gen_conf where so_gen_conf_en_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_gen_conf_en_id")) + ""
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete SO Conf " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_gen_conf_en_id"))
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

    Private Sub loc_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try
            'Dim frm As New FPartNumberSearch
            'frm.set_win(Me)
            'frm._obj = sog_gen_ptnr_mstr_ptnr_code
            'frm._en_id = so_gen_conf_en_id.EditValue
            'frm.type_form = True
            'frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
