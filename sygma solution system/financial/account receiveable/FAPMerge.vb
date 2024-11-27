Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FAPMerge
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As DataSet
    Dim _paythen As DateTime

    Private Sub FAPMerge_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    '    app_credit_term.Properties.DataSource = dt_bantu
    '    app_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
    '    app_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
    '    app_credit_term.ItemIndex = 0

    'End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
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

        'add_column(gv_detail, "appd_app_oid", False)
        'add_column(gv_detail, "Voucher Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ''add_column(gv_detail, "AR Number", "appd_ar_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "Payment Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column(gv_detail, "AP Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "appd_app_oid", False)
        add_column(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Voucher Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Voucher Date", "ap_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Total", "ap_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")

        add_column(gv_edit, "appd_oid", False)
        add_column(gv_edit, "appd_app_oid", False)
        add_column(gv_edit, "appd_ap_oid", False)
        'add_column(gv_edit, "Voucher Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Payment Due Date", "ar_due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Voucher Number", "appd_ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Voucher Date", "ap_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Total", "ap_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.app_oid, " _
                    & "  a.app_en_id, " _
                    & "  c.en_desc, " _
                    & "  a.app_code, " _
                    & "  a.app_date, " _
                    & "  a.app_due_date, " _
                    & "  a.app_credit_term, " _
                    & "  a.app_duedate_pay, " _
                    & "  a.app_add_by, " _
                    & "  a.app_add_date, " _
                    & "  a.app_upd_by, " _
                    & "  a.app_upd_date, " _
                    & "  app_remarks " _
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

        app_date.DateTime = CekTanggal()
        'app_due_date.DateTime = CekTanggal()
        app_due_date.EditValue = CekTanggal()

        'app_ship_receipt.Enabled = False
        'app_ship_receipt.EditValue = CekTanggal()
        app_duedate_pay.Enabled = False

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
                        & "  a.appd_oid, " _
                        & "  a.appd_app_oid, " _
                        & "  a.appd_ap_oid, " _
                        & "  a.appd_ap_code " _
                        & "FROM " _
                        & "  public.appd_det a " _
                        & "WHERE " _
                        & "  a.appd_app_oid IS NULL "

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
                & "  app_code, " _
                & "  app_date, " _
                & "  app_due_date, " _
                & "  app_credit_term, " _
                & "  app_add_by, " _
                & "  app_add_date, " _
                & "  app_remarks " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetDate(app_date.DateTime) & ",  " _
                & SetDate(app_due_date.DateTime) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & ")"

            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.appd_det " _
                        & "( " _
                        & "  appd_oid, " _
                        & "  appd_app_oid, " _
                        & "  appd_ap_oid, " _
                        & "  appd_ap_code " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetSetring(.Item("appd_ap_oid")) & ",  " _
                        & SetSetring(.Item("appd_ap_code")) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                    'If ds_edit.Tables(0).Rows(i).Item("appd_ar_oid").ToString <> "" Then
                    '    'update karena ada hubungan antara so dan po antar group perusahaan
                    '    ssql = "update ar_mstr set ar_due_date = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("app_due_date")) _
                    '                         & " where sod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid"))
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

                'app_ship_receipt.EditValue = CekTanggal()
                'app_ship_receipt.Enabled = True

                app_duedate_pay.EditValue = _paythen
                app_duedate_pay.Enabled = True

            End With
            app_en_id.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                        & "  a.appd_oid, " _
                        & "  a.appd_app_oid, " _
                        & "  a.appd_ar_oid, " _
                        & "  a.appd_ar_code, " _
                        & "  a.appd_duedate_pay " _
                        & "FROM " _
                        & "  public.appd_det a " _
                        & "WHERE " _
                        & "  a.appd_app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()


                        'If ds_edit.Tables(0).Rows(i).Item("appd_ar_oid").ToString <> "" Then
                        '    'update karena ada hubungan antara so dan po antar group perusahaan
                        '    .SQL = "update ar_mstr set ar_due_date = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("app_due_date")) _
                        '                         & " where sod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid"))
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
                        .Command.CommandText = "Delete from appd_det " _
                                            & "WHERE  " _
                                            & "  appd_app_oid = " & SetSetring(_mstr_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            'With ds_edit.Tables(0).Rows(i)
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.appd_det " _
                                & "( " _
                                & "  appd_oid, " _
                                & "  appd_app_oid, " _
                                & "  appd_ar_oid, " _
                                & "  appd_ar_code, " _
                                & "  appd_duedate_pay " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_mstr_oid) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid")) & ",  " _
                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_code")) & ",  " _
                                & SetDate(app_duedate_pay.DateTime) & "  " _
                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'If ds_edit.Tables(0).Rows(i).Item("appd_ar_oid").ToString <> "" Then
                            'update karena ada hubungan antara so dan po antar group perusahaan

                            ''.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update ar_mstr set ar_expt_date = " + SetDate(app_ship_receipt.DateTime) _
                            '                     & " where ar_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid"))
                            ''ssqls.Add(ssql)
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            ''.Command.Parameters.Clear()

                            ''.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update ar_mstr set ar_due_date = " + SetDate(app_duedate_pay.DateTime) _
                            '                     & " where ar_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid"))
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
                            '            & "  ap_eff_date = " & SetDate(app_date.DateTime) & ",  " _
                            '            & "  ap_expt_date = " & SetDate(app_due_date.DateTime) & ",  " _
                            '            & "  ap_due_date = " & SetDate(app_duedate_pay.DateTime) & "  " _
                            '            & "WHERE  " _
                            '            & "  ar_oid  = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid")) & " "
                            'ssqls.Add(.Command.CommandText)
                            '.Command.ExecuteNonQuery()
                            ''.Command.Parameters.Clear()

                            ''.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "UPDATE  " _
                            '            & "  public.glt_det   " _
                            '            & "SET  " _
                            '            & "  glt_add_date = " & SetDate(app_ship_receipt.DateTime) & "  " _
                            '            & "WHERE  " _
                            '            & "  glt_ref_oid  = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid")) & " "
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
        '    If SetString(ds_edit.Tables(0).Rows(i).Item("appd_ar_oid")) = "" Then
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
            '    & "  public.appd_det.appd_oid, " _
            '    & "  public.appd_det.appd_app_oid, " _
            '    & "  public.appd_det.appd_ar_oid, " _
            '    & "  public.appd_det.appd_ar_code, " _
            '    & "  public.appd_det.appd_duedate_pay, " _
            '    & "  public.ar_mstr.ar_due_date, " _
            '    & "  public.ar_mstr.ar_status " _
            '    & "FROM " _
            '    & "  public.appd_det " _
            '    & "  INNER JOIN public.ar_mstr ON (public.appd_det.appd_ar_oid = public.ar_mstr.ar_oid)" _
            '    & "WHERE " _
            '    & "  public.appd_det.appd_app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "

            sql = "SELECT  " _
                & "  a.appd_oid, " _
                & "  a.appd_app_oid, " _
                & "  a.appd_ar_oid, " _
                & "  a.appd_ar_code, " _
                & "  a.appd_duedate_pay " _
                & "FROM " _
                & "  public.appd_det a " _
                & "WHERE " _
                & "  a.appd_app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "

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
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    'Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
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

    Public Overrides Sub preview()

        Dim _ar_code, _code As String

        ssql = "SELECT   b.appd_ar_code FROM  public.appd_det b WHERE  b.appd_app_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_oid") & "' "
        Dim dt As New DataTable
        dt = GetTableData(ssql)
        _ar_code = ""
        _code = ""
        For Each dr As DataRow In dt.Rows
            _ar_code = _ar_code & "'" & dr(0) & "',"
            _code = dr(0)
        Next

        _ar_code = Microsoft.VisualBasic.Left(_ar_code, _ar_code.Length - 1)
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_en_id")
        _type = 13
        _table = "app_print"
        _initial = "app"
        _code_awal = _code
        _code_akhir = _code

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT app_code, " _
            & "app_date, " _
            & "app_due_date, " _
            & "app_remarks, " _
            & "sod_pt_id, " _
            & "ar_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "cu_symbol, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
            & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
            & "bk_name, " _
            & "bk_code, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "cu_symbol, " _
            & "pt_code, " _
            & "pt_desc1, " _
            & "sum(ars_shipment) AS shipment, " _
            & "um_master.code_name as um_name, " _
            & "soshipd_um, " _
            & "ar_credit_term, " _
            & "ars_so_price AS harga_sebelum_diskon, " _
            & "sod_disc AS diskon, " _
            & "ars_so_disc_value AS nilai_diskon, " _
            & "ars_invoice_price AS harga_setelah_diskon, " _
            & "sum(ars_shipment * ars_invoice_price) AS total_invoiced, " _
            & "sum(ars_shipment * ars_so_price) AS total_bruto, " _
            & "sum(ars_shipment * ars_so_disc_value) AS total_diskon, " _
            & "sum(ars_shipment * ars_invoice_price)/1000000 AS total_point " _
            & "FROM ar_mstr " _
            & "INNER JOIN appd_det ON appd_ar_oid = ar_oid " _
            & "INNER JOIN app_print ON app_oid = appd_app_oid " _
            & "INNER JOIN ars_ship ON ars_ar_oid = ar_oid " _
            & "INNER JOIN soshipd_det ON soshipd_oid = ars_soshipd_oid " _
            & "INNER JOIN soship_mstr ON soship_oid = soshipd_soship_oid " _
            & "INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
            & "INNER JOIN so_mstr ON so_oid = sod_so_oid AND (so_oid = soship_so_oid) " _
            & "INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
            & "INNER JOIN ptnr_mstr ON ptnr_id = ar_bill_to " _
            & "INNER JOIN ptnra_addr ON ptnra_ptnr_oid = ptnr_oid " _
            & "INNER JOIN cu_mstr ON cu_id = ar_cu_id " _
            & "inner join code_mstr um_master on um_master.code_id = sod_um " _
            & "inner join bk_mstr on bk_id = ar_bk_id " _
            & "inner join ac_mstr on ac_id = bk_ac_id " _
            & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "WHERE soship_mstr.soship_code NOT LIKE 'ST%' " _
            & "and ar_code in (" & _ar_code & ")" _
            & "GROUP BY app_code, " _
            & "app_date, " _
            & "app_remarks, " _
            & "sod_pt_id, " _
            & "ar_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "cu_symbol, " _
            & "pt_code, " _
            & "pt_desc1, " _
            & "soshipd_um, " _
            & "ar_credit_term, " _
            & "ars_so_price, " _
            & "sod_disc, " _
            & "ars_so_disc_value, " _
            & "ars_invoice_price, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_phone_1, " _
            & "cmaddr_phone_2, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "app_due_date, " _
            & "bk_name, " _
            & "bk_code, " _
            & "credit_term_name, " _
            & "um_name " _
            & "ORDER BY pt_desc1"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoiceMergeDetail"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("app_code")
        frm.ShowDialog()

    End Sub
End Class
