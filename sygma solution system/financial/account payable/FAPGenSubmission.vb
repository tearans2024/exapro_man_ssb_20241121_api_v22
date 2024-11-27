Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FAPGenSubmission
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As DataSet
    Dim _paythen As DateTime

    Private Sub FAPGenSubmission_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Apmerge1.ds_ap_merge' table. You can move, or remove it, as needed.
        'Me.Ds_ap_merge_TableAdapter.Fill(Me.Apmerge1.ds_ap_merge)
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()

        _paythen = func_coll.get_pay

    End Sub

    Public Overrides Sub load_cb()
        init_le(app_en_id, "en_mstr")
    End Sub

    'Public Overrides Sub load_cb_en()
    '    dt_bantu = New DataTable
    '    dt_bantu = (func_data.load_creditterms_mstr(app_en_id.EditValue))
    '    'app_credit_term.Properties.DataSource = dt_bantu
    '    'app_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
    '    'app_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
    '    'app_credit_term.ItemIndex = 0

    'End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "app_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "app_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")

        add_column_copy(gv_master, "Payment Date", "app_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Shipment Receipt", "app_ship_receipt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Payment Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "app_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "app_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "app_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "app_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "app_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "appd_app_oid", False)
        'add_column(gv_detail, "appd_ap_oid", "appd_ap_oid", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "appd_en_id", "appd_en_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "appd_ptnr_id", "appd_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Voucher Date", "appd_ap_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "AP Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "AR Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Payment Due Date", "appd_duedate_pay", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "AP Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Bank", "appd_ptnr_bank", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Bank Account Number", "appd_ptnr_no_rek", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Bank Account Name", "appd_ptnr_rek_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Payment Due Date", "appd_ap_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "Amount", "appd_ap_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_detail, "Realization", "appd_realz_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "Realization Number", "appd_realz_nymber", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Realization Amount", "appd_realz_amuunt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column(gv_detail, "Account Number", "ap_status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Remarks", "appd_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "appd_oid", False)
        add_column(gv_edit, "appd_app_oid", False)
        add_column(gv_edit, "appd_ap_oid", False)
        'add_column(gv_edit, "appd_en_id", "appd_en_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "appd_ptnr_id", "appd_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Vendor", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Voucher Date", "appd_ap_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "AP Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "AR Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Payment Due Date", "appd_duedate_pay", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "AR Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Bank", "appd_ptnr_bank", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Bank Account Number", "appd_ptnr_no_rek", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Bank Account Name", "appd_ptnr_rek_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Payment Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "Amount", "appd_ap_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "Remarks", "appd_remarks", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_detail, "Realization", "appd_realize_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column(gv_detail, "Realization Number", "appd_realize_number", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "Realization", "amount", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column(gv_detail, "Account Number", "ap_status", DevExpress.Utils.HorzAlignment.Default)


        'add_column(gv_edit, "appd_oid", False)
        'add_column(gv_edit, "appd_app_oid", False)
        'add_column(gv_edit, "appd_ap_oid", False)
        'add_column(gv_edit, "AR Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Payment Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.app_oid, " _
                & "  a.app_en_id, " _
                & "  c.en_desc, " _
                & "  a.app_code, " _
                & "  a.app_date, " _
                & "  a.app_due_date, " _
                & "  a.app_duedate_pay, " _
                & "  a.app_add_by, " _
                & "  a.app_add_date, " _
                & "  a.app_upd_by, " _
                & "  a.app_upd_date, " _
                & "  a.app_remarks " _
                & "FROM " _
                & "  public.app_print a " _
                & "  INNER JOIN public.en_mstr c ON (a.app_en_id = c.en_id) " _
                & "WHERE " _
                & " a.app_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") and a.app_date BETWEEN  " _
                & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & " ORDER BY " _
                & "  a.app_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        app_en_id.EditValue = ""
        'app_ptnr_id.EditValue = ""
        app_date.DateTime = CekTanggal()
        'app_due_date.DateTime = CekTanggal()
        app_due_date.EditValue = CekTanggal()

        'app_purch_receipt.Enabled = False
        'app_ship_receipt.EditValue = CekTanggal()
        'app_duedate_pay.Enabled = False
        app_duedate_pay.EditValue = CekTanggal()

        app_remarks.EditValue = ""
        app_en_id.Focus()

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb

                    .SQL = "SELECT  " _
                        & "  public.appd_print.appd_oid, " _
                        & "  public.appd_print.appd_app_oid, " _
                        & "  public.appd_print.appd_ap_oid, " _
                        & "  public.appd_print.appd_ap_code, " _
                        & "  public.appd_print.appd_duedate_pay, " _
                        & "  public.appd_print.appd_ptnr_id, " _
                        & "  public.appd_print.appd_ap_date, " _
                        & "  public.appd_print.appd_ap_amount, " _
                        & "  public.appd_print.appd_ap_due_date, " _
                        & "  public.appd_print.appd_en_id, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.ptnr_mstr.ptnr_name, " _
                        & "  public.ptnr_mstr.ptnr_bank, " _
                        & "  public.ptnr_mstr.ptnr_no_rek, " _
                        & "  public.ptnr_mstr.ptnr_rek_name, " _
                        & "  public.appd_print.appd_ptnr_bank, " _
                        & "  public.appd_print.appd_ptnr_no_rek, " _
                        & "  public.appd_print.appd_ptnr_rek_name, " _
                        & "  public.appd_print.appd_remarks " _
                        & "FROM " _
                        & "  public.appd_print " _
                        & "  INNER JOIN public.en_mstr ON (public.appd_print.appd_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.ptnr_mstr ON (public.appd_print.appd_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                        & "WHERE " _
                        & "  public.appd_print.appd_app_oid IS NULL "

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function insert() As Boolean

        Dim _mstr_oid As String = Guid.NewGuid.ToString
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _code As String

        _code = GetNewNumberYM("app_print", "app_code", 5, "APP" & app_en_id.GetColumnValue("en_code") _
                                     & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try

            ssql = "INSERT INTO  " _
                & "  public.app_print " _
                & "( " _
                & "  app_oid, " _
                & "  app_en_id, " _
                & "  app_code, " _
                & "  app_date, " _
                & "  app_due_date, " _
                & "  app_add_by, " _
                & "  app_add_date, " _
                & "  app_remarks " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetInteger(app_en_id.EditValue) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetDate(app_date.DateTime) & ",  " _
                & SetDate(app_due_date.DateTime) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetSetring(app_remarks.Text) & "  " _
                & ")"

            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.appd_print " _
                        & "( " _
                        & "  appd_oid, " _
                        & "  appd_app_oid, " _
                        & "  appd_ap_oid, " _
                        & "  appd_ap_code, " _
                        & "  appd_duedate_pay, " _
                        & "  appd_ptnr_id, " _
                        & "  appd_ptnr_bank, " _
                        & "  appd_ptnr_no_rek, " _
                        & "  appd_ptnr_rek_name, " _
                        & "  appd_ap_date, " _
                        & "  appd_ap_amount, " _
                        & "  appd_ap_due_date, " _
                        & "  appd_en_id, " _
                        & "  appd_remarks, " _
                        & "  appd_seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetSetring(.Item("appd_ap_oid")) & ",  " _
                        & SetSetring(.Item("appd_ap_code")) & ",  " _
                        & SetDate(.Item("appd_duedate_pay")) & ",  " _
                        & SetSetring(.Item("appd_ptnr_id")) & ",  " _
                        & SetSetring(.Item("appd_ptnr_bank")) & ",  " _
                        & SetSetring(.Item("appd_ptnr_no_rek")) & ",  " _
                        & SetSetring(.Item("appd_ptnr_rek_name")) & ",  " _
                        & SetSetring(.Item("appd_ap_date")) & ",  " _
                        & SetDec(.Item("appd_ap_amount")) & ",  " _
                        & SetDate(.Item("appd_ap_due_date")) & ",  " _
                        & SetSetring(.Item("appd_en_id")) & ",  " _
                        & SetSetring(.Item("appd_remarks")) & ",  " _
                        & SetInteger(i) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                    'If ds_edit.Tables(0).Rows(i).Item("appd_ap_oid").ToString <> "" Then
                    '    'update karena ada hubungan antara so dan po antar group perusahaan
                    '    ssql = "update ap_mstr set ap_due_date = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("app_due_date")) _
                    '                         & " where sod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid"))
                    '    ssqls.Add(ssql)
                    'End If

                End With
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If

            after_success()
            set_row(_mstr_oid, "app_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True

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
                _mstr_oid = .Item("app_oid")
                app_en_id.EditValue = .Item("app_en_id")
                app_en_id.Enabled = False

                app_date.DateTime = .Item("app_date")
                app_date.Enabled = False

                app_due_date.DateTime = .Item("app_due_date")
                app_due_date.Enabled = False

                'app_credit_term.EditValue = .Item("app_credit_term")
                'app_credit_term.Enabled = False

                'app_ptnr_id.Tag = .Item("app_ptnr_id")
                'app_ptnr_id.Enabled = False

                'app_ptnr_id.EditValue = .Item("ptnr_name")
                'app_ptnr_id.Enabled = False

                app_remarks.EditValue = .Item("app_remarks")

                'app_purch_receipt.EditValue = CekTanggal()
                'app_purch_receipt.Enabled = True

                app_duedate_pay.EditValue = _paythen
                app_duedate_pay.Enabled = True

            End With
            app_en_id.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  public.appd_print.appd_oid, " _
                            & "  public.appd_print.appd_app_oid, " _
                            & "  public.appd_print.appd_ap_oid, " _
                            & "  public.appd_print.appd_ap_code, " _
                            & "  public.appd_print.appd_duedate_pay, " _
                            & "  public.appd_print.appd_ptnr_id, " _
                            & "  public.appd_print.appd_ap_date, " _
                            & "  public.appd_print.appd_ap_amount, " _
                            & "  public.appd_print.appd_ap_due_date, " _
                            & "  public.appd_print.appd_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.ptnr_mstr.ptnr_bank, " _
                            & "  public.ptnr_mstr.ptnr_no_rek, " _
                            & "  public.ptnr_mstr.ptnr_rek_name, " _
                            & "  public.appd_print.appd_ptnr_bank, " _
                            & "  public.appd_print.appd_ptnr_no_rek, " _
                            & "  public.appd_print.appd_ptnr_rek_name, " _
                            & "  public.appd_print.appd_remarks " _
                            & "FROM " _
                            & "  public.appd_print " _
                            & "  INNER JOIN public.en_mstr ON (public.appd_print.appd_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.appd_print.appd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "WHERE " _
                            & "  public.appd_print.appd_app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()


                        'If ds_edit.Tables(0).Rows(i).Item("appd_ap_oid").ToString <> "" Then
                        '    'update karena ada hubungan antara so dan po antar group perusahaan
                        '    .SQL = "update ap_mstr set ap_due_date = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("app_due_date")) _
                        '                         & " where sod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid"))
                        '    ssqls.Add(ssql)
                        'End If

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        Dim i As Integer
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
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
                                        & "  public.app_print   " _
                                        & "SET  " _
                                        & "  app_en_id = " & SetInteger(app_en_id.EditValue) & ",  " _
                                        & "  app_duedate_pay = " & SetDate(app_duedate_pay.DateTime) & ",  " _
                                        & "  app_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  app_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                                        & "  app_remarks = " & SetSetring(app_remarks.Text) & "  " _
                                        & "WHERE  " _
                                        & "  app_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from appd_print " _
                                            & "WHERE  " _
                                            & "  appd_app_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.appd_print " _
                                & "( " _
                                & "  appd_oid, " _
                                & "  appd_app_oid, " _
                                & "  appd_ap_oid, " _
                                & "  appd_ap_code, " _
                                & "  appd_duedate_pay " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_mstr_oid) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid")) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_code")) & ",  " _
                                & SetDate(app_duedate_pay.DateTime) & "  " _
                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'If ds_edit.Tables(0).Rows(i).Item("appd_ap_oid").ToString <> "" Then
                            'update karena ada hubungan antara so dan po antar group perusahaan

                            ''.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update ap_mstr set ap_expt_date = " + SetDate(app_ship_receipt.DateTime) _
                            '                     & " where ap_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid"))
                            ''ssqls.Add(ssql)
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            ''.Command.Parameters.Clear()

                            ''.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update ap_mstr set ap_due_date = " + SetDate(app_duedate_pay.DateTime) _
                            '                     & " where ap_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid"))
                            ''ssqls.Add(ssql)
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            ''.Command.Parameters.Clear()
                            'End If

                            'End With
                            ''.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "UPDATE  " _
                            '            & "  public.ap_mstr   " _
                            '            & "SET  " _
                            '            & "  ap_eff_date = " & SetDate(app_purch_receipt.DateTime) & ",  " _
                            '            & "  ap_expt_date = " & SetDate(app_due_date.DateTime) & ",  " _
                            '            & "  ap_due_date = " & SetDate(app_duedate_pay.DateTime) & "  " _
                            '            & "WHERE  " _
                            '            & "  ap_oid  = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid")) & " "
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            ''.Command.Parameters.Clear()

                            ''.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "UPDATE  " _
                            '            & "  public.glt_det   " _
                            '            & "SET  " _
                            '            & "  glt_add_date = " & SetDate(app_purch_receipt.DateTime) & "  " _
                            '            & "WHERE  " _
                            '            & "  glt_ref_oid  = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid")) & " "
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            ''.Command.Parameters.Clear()
                        Next

                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1

                        'Next

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
                        set_row(_mstr_oid, "app_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True


    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = False

        gv_master_SelectionChanged(Nothing, Nothing)

        Dim sSQL As String
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
                With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

                    sSQL = "DELETE FROM  " _
                        & "  public.app_print  " _
                        & "WHERE  " _
                        & "  app_oid ='" & .Item("app_oid") & "'"

                    ssqls.Add(sSQL)


                End With

                If master_new.PGSqlConn.status_sync = True Then
                    If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                End If

                help_load_data(True)
                MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True


        If ds_edit.Tables(0).Rows.Count = 0 Then
            Box("Detail can't blank")
            Return False
            Exit Function
        End If

        'For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If SetString(ds_edit.Tables(0).Rows(i).Item("appd_ap_oid")) = "" Then
        '        Box("AR can't blank")
        '        Return False
        '        Exit Function
        '    End If

        'Next
        Return before_save
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_SelectionChanged(sender, Nothing)
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String = ""

            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try

            'sql = "SELECT " _
            '    & "  public.appd_print.appd_oid, " _
            '    & "  public.appd_print.appd_app_oid, " _
            '    & "  public.appd_print.appd_ap_oid, " _
            '    & "  public.appd_print.appd_ap_code, " _
            '    & "  public.appd_print.appd_duedate_pay, " _
            '    & "  public.ap_mstr.ap_due_date, " _
            '    & "  public.ap_mstr.ap_status " _
            '    & "FROM " _
            '    & "  public.appd_print " _
            '    & "  INNER JOIN public.ap_mstr ON (public.appd_print.appd_ap_oid = public.ap_mstr.ap_oid)" _
            '    & "WHERE " _
            '    & "  public.appd_print.appd_app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "

            sql = "SELECT  " _
                    & "  public.appd_print.appd_oid, " _
                    & "  public.appd_print.appd_app_oid, " _
                    & "  public.appd_print.appd_ap_oid, " _
                    & "  public.appd_print.appd_ap_code, " _
                    & "  public.appd_print.appd_duedate_pay, " _
                    & "  public.appd_print.appd_ptnr_id, " _
                    & "  public.appd_print.appd_ap_date, " _
                    & "  public.appd_print.appd_ap_amount, " _
                    & "  public.appd_print.appd_ap_due_date, " _
                    & "  public.appd_print.appd_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  public.ptnr_mstr.ptnr_bank, " _
                    & "  public.ptnr_mstr.ptnr_no_rek, " _
                    & "  public.ptnr_mstr.ptnr_rek_name, " _
                    & "  public.appd_print.appd_ptnr_bank, " _
                    & "  public.appd_print.appd_ptnr_no_rek, " _
                    & "  public.appd_print.appd_ptnr_rek_name, " _
                    & "  public.appd_print.appd_remarks " _
                    & "FROM " _
                    & "  public.appd_print " _
                    & "  INNER JOIN public.en_mstr ON (public.appd_print.appd_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.appd_print.appd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "WHERE " _
                    & "  public.appd_print.appd_app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "

            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "appd_ap_code" Then
            Dim frm As New FVoucherSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = app_en_id.EditValue
            'frm._ptnr_id = app_ptnr_id.Tag
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    'Private Sub app_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Try

    '        Dim frm As New FPartnerSearch
    '        frm.set_win(Me)
    '        frm._obj = app_ptnr_id
    '        frm._en_id = app_en_id.EditValue
    '        frm.type_form = True
    '        frm.ShowDialog()

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            'gv_edit.UpdateCurrentRow()
            'Dim _col As String = gv_edit.FocusedColumn.Name
            'Dim _row As Integer = gv_edit.FocusedRowHandle

            'If _col = "si_desc" Then

            '    For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            '        ds_edit.Tables(0).Rows(i).Item("si_desc") = gv_edit.GetRowCellValue(_row, "si_desc")
            '        ds_edit.Tables(0).Rows(i).Item("wocid_si_id") = gv_edit.GetRowCellValue(_row, "wocid_si_id")
            '    Next

            'ElseIf _col = "loc_desc" Then
            '    For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            '        ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
            '        ds_edit.Tables(0).Rows(i).Item("wocid_loc_id") = gv_edit.GetRowCellValue(_row, "wocid_loc_id")
            '    Next


            'End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub casho_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles app_en_id.EditValueChanged
        Try
            ' init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        Try

            'If SetString(genpr_year.EditValue) = "" Then
            '    Box("Year can't empty")
            '    Exit Sub
            'End If

            Dim _en_id_all As String

            If genap_all_child.EditValue = True Then
                _en_id_all = get_en_id_child(app_en_id.EditValue)
            Else
                _en_id_all = app_en_id.EditValue
            End If

            Dim sSQL As String
            'sSQL = "SELECT  " _
            '    & "  a.fcsd_pt_id, " _
            '    & "  b.pt_code, " _
            '    & "  b.pt_desc1, " _
            '    & "  b.pt_desc2, " _
            '    & "  sum(a.fcsd_01_amount) as fcsd_01_amount, " _
            '    & "  sum(a.fcsd_02_amount) as fcsd_02_amount, " _
            '    & "  sum(a.fcsd_03_amount) as fcsd_03_amount, " _
            '    & "  sum(a.fcsd_total_amount) as fcsd_total_amount, " _
            '    & "  sum(a.fcsd_buffer_amount) as fcsd_buffer_amount,coalesce((SELECT  sum(x.pod_qty - coalesce(x.pod_qty_receive, 0)) AS jml " _
            '    & "FROM  public.pod_det x  INNER JOIN public.po_mstr y ON (x.pod_po_oid = y.po_oid) " _
            '    & "WHERE  x.pod_pt_id = a.fcsd_pt_id AND   y.po_en_id in (" & _en_id_all & ") and coalesce(po_trans_id,'') <> 'X'),0) as qty_po, " _
            '    & " coalesce((SELECT   sum(x.invc_qty) as jml " _
            '    & "FROM  public.invc_mstr x " _
            '    & "WHERE  x.invc_pt_id = a.fcsd_pt_id AND   x.invc_en_id in (" & _en_id_all & ") and x.invc_loc_id in (SELECT   z.locgr_loc_id FROM  public.locgr_mstr z)),0) as  qty_stock " _
            '    & "FROM " _
            '    & "  public.fcsd_det a " _
            '    & "  INNER JOIN public.pt_mstr b ON (a.fcsd_pt_id = b.pt_id) " _
            '    & "  INNER JOIN public.fcs_mstr c ON (a.fcsd_fcs_oid = c.fcs_oid) " _
            '    & "WHERE " _
            '    & "  c.fcs_en_id IN (" & _en_id_all & ") " _
            '    & "  and c.fcs_qrtr_id=" & genpr_qrtr_id.EditValue & " " _
            '    & "  and c.fcs_year=" & genpr_year.EditValue & " " _
            '    & "  group by  " _
            '    & "  a.fcsd_pt_id, " _
            '    & "  b.pt_code, " _
            '    & "  b.pt_desc1, " _
            '    & "  b.pt_desc2"

            sSQL = "SELECT distinct  " _
                & " public.ap_mstr.ap_oid, " _
                & " public.app_po.app_ap_oid, " _
                & "  public.ap_mstr.ap_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ap_mstr.ap_ptnr_id, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.ptnr_mstr.ptnr_bank, " _
                & "  public.ptnr_mstr.ptnr_no_rek, " _
                & "  public.ptnr_mstr.ptnr_rek_name, " _
                & "  public.ap_mstr.ap_code, " _
                & "  public.ap_mstr.ap_date, " _
                & "  public.ap_mstr.ap_due_date, " _
                & "  public.ap_mstr.ap_amount, " _
                & "  public.ap_mstr.ap_remarks " _
                & "FROM " _
                & "  public.ap_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ap_mstr.ap_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.en_mstr ON (public.ap_mstr.ap_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.app_po ON (public.ap_mstr.ap_oid = public.app_po.app_ap_oid) " _
                & "WHERE " _
                & "  public.ap_mstr.ap_en_id IN (" & _en_id_all & ") " _
                & "  and public.ap_mstr.ap_amount <> public.ap_mstr.ap_pay_amount" _
                & "  and public.ap_mstr.ap_date <= '" & app_due_date.EditValue & "' "

            Dim dt_fs As New DataTable
            dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

            ds_edit.Tables(0).Rows.Clear()

            For Each dr As DataRow In dt_fs.Rows
                Dim _row As DataRow
                _row = ds_edit.Tables(0).NewRow

                _row("appd_ap_oid") = dr("app_ap_oid")
                _row("appd_en_id") = dr("ap_en_id")
                _row("en_desc") = dr("en_desc")
                _row("appd_ap_code") = dr("ap_code")
                _row("appd_ptnr_id") = dr("ap_ptnr_id")
                _row("ptnr_name") = dr("ptnr_name")
                _row("appd_ap_date") = dr("ap_date")
                _row("appd_ap_amount") = dr("ap_amount")
                _row("appd_duedate_pay") = dr("ap_due_date")
                _row("appd_ptnr_bank") = dr("ptnr_bank")
                _row("appd_ptnr_no_rek") = dr("ptnr_no_rek")
                _row("appd_ptnr_rek_name") = dr("ptnr_rek_name")
                _row("appd_remarks") = dr("ap_remarks")
                '_row("genprd_buffer_amount") = System.Math.Round(dr("fcsd_buffer_amount"), 0)

                '_row("genprd_fs_amount") = SetNumber(dr("fcsd_total_amount")) + SetNumber(_row("genprd_buffer_amount"))
                '_row("genprd_fs_amount_round") = pembulatan(_row("genprd_fs_amount"))
                '_row("genprd_fs_amount_round_bal") = _row("genprd_fs_amount_round") - _row("genprd_fs_amount")

                '_row("genprd_po") = dr("qty_po")
                '_row("genprd_stock") = dr("qty_stock")
                '_row("genprd_pr_amount") = IIf(_row("genprd_fs_amount_round") - _row("genprd_po") - _row("genprd_stock") < 0, 0, _row("genprd_fs_amount_round") - _row("genprd_po") - _row("genprd_stock"))

                '_row("genprd_01_amount_min") = _row("genprd_01_amount") + _row("genprd_buffer_amount")
                '_row("genprd_02_amount_min") = _row("genprd_02_amount")
                '_row("genprd_03_amount_min") = _row("genprd_03_amount")

                ds_edit.Tables(0).Rows.Add(_row)
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub preview()

        Dim _app_code, _code As String

        ssql = "SELECT   b.app_code FROM  public.app_print b WHERE  b.app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "
        Dim dt As New DataTable
        dt = GetTableData(ssql)
        _app_code = ""
        _code = ""
        For Each dr As DataRow In dt.Rows
            _app_code = _app_code & "'" & dr(0) & "',"
            _code = dr(0)
        Next

        _app_code = Microsoft.VisualBasic.Left(_app_code, _app_code.Length - 1)
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_en_id")
        _type = 13
        _table = "app_print"
        _initial = "app"
        _code_awal = _code
        _code_akhir = _code

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  public.app_print.app_en_id, " _
            & "  public.en_mstr.en_desc, " _
            & "  public.app_print.app_code, " _
            & "  public.app_print.app_date, " _
            & "  public.app_print.app_remarks, " _
            & "  public.appd_print.appd_seq, " _
            & "  public.appd_print.appd_ap_code, " _
            & "  public.appd_print.appd_duedate_pay, " _
            & "  public.appd_print.appd_ptnr_id, " _
            & "  public.ptnr_mstr.ptnr_name, " _
            & "  public.appd_print.appd_ptnr_bank, " _
            & "  public.appd_print.appd_ptnr_no_rek, " _
            & "  public.appd_print.appd_ptnr_rek_name, " _
            & "  public.appd_print.appd_ap_date, " _
            & "  public.appd_print.appd_ap_amount, " _
            & "  public.appd_print.appd_ap_due_date, " _
            & "  public.appd_print.appd_remarks " _
            & "FROM " _
            & "  public.app_print " _
            & "  INNER JOIN public.appd_print ON (public.app_print.app_oid = public.appd_print.appd_app_oid) " _
            & "  INNER JOIN public.en_mstr ON (public.app_print.app_en_id = public.en_mstr.en_id) " _
            & "  INNER JOIN public.ptnr_mstr ON (public.appd_print.appd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
            & "WHERE " _
            & "  public.app_print.app_code in (" & _app_code & ")"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRAPMergeFinal"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_code")
        frm.ShowDialog()

    End Sub
End Class
