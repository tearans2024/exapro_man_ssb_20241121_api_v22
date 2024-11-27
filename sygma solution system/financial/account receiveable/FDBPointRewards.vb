Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FDBPointRewards
    Dim ssql As String
    Dim _mstr_oid, _dbr_dbg_name_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit, ds_edit_dbg, ds_edit_shipment, ds_edit_dist As DataSet
    Dim _paythen As DateTime

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'pr_txttglawal.DateTime = CekTanggal()
        'pr_txttglakhir.DateTime = CekTanggal()

        '_paythen = func_coll.get_pay

    End Sub

    Public Overrides Sub load_cb()
        'init_le(dbr_en_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_city("city"))
        With dbr_city
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = func_data.load_sales_program()
        With dbr_program
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("sls_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("sls_code").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = func_data.load_periode_mstr()
        With dbr_periode
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("periode_code").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = func_data.load_grouping_mstr()
        With dbr_dbg_name
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("dbg_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("dbg_code").ToString
            '.Properties.ValueMember = dt_bantu.Columns("dbg_oid").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_creditterms_mstr(dbr_en_id.EditValue))
        'dbr_credit_term.Properties.DataSource = dt_bantu
        'dbr_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'dbr_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'dbr_credit_term.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Code", "dbr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Programs", "sls_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group Name", "dbg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "City", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "dbr_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Periode", "periode_code", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Start Date", "dbr_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "dbr_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "dbr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "dbr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "dbr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "dbr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "dbr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "dbrd_dbr_oid", False)
        add_column(gv_detail, "Brand", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "goods receipt", "dbrd_ar_eff_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "invoice date", "dbrd_ar_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "AR Code", "dbrd_ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Qty", "dbrd_ars_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "purchase amount", "dbrd_ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_detail, "payment due", "dbrd_ar_duedate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "transfer date", "dbrd_arpayd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "payment", "dbrd_apyad_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_detail, "Point", "dbrd_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        'add_column(gv_edit, "dbrd_oid", False)
        'add_column(gv_edit, "dbrd_dbr_oid", False)
        'add_column(gv_edit, "dbrd_ar_oid", False)
        'add_column(gv_edit, "AR Number", "dbrd_ar_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Payment Due Date", "dbrd_duedate_pay", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")

        add_column(gv_edit, "dbrd_dbr_oid", False)
        add_column(gv_edit, "goods receipt", "dbrd_ar_eff_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "invoice date", "dbrd_ar_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "AR Code", "dbrd_ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty", "dbrd_ars_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "purchase amount", "dbrd_ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "payment due", "dbrd_ar_duedate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "transfer date", "dbrd_apyad_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "payment", "dbrd_apyad_amount", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Point", "dbrd_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  public.dbr_mstr.dbr_oid, " _
                & "  public.dbr_mstr.dbr_code, " _
                & "  public.dbg_group.dbg_name, " _
                & "  public.dbr_mstr.dbr_date, " _
                & "  public.code_mstr.code_name, " _
                & "  public.psperiode_mstr.periode_code, " _
                & "  public.sls_program.sls_desc, " _
                & "  public.dbr_mstr.dbr_slsprog_code, " _
                & "  public.sls_program.sls_code, " _
                & "  public.dbr_mstr.dbr_start_date, " _
                & "  public.dbr_mstr.dbr_end_date " _
                & "FROM " _
                & "  public.dbr_mstr " _
                & "  INNER JOIN public.dbg_group ON (public.dbr_mstr.dbr_dbg_oid = public.dbg_group.dbg_oid) " _
                & "  INNER JOIN public.code_mstr ON (public.dbr_mstr.dbr_dbgcity_id = public.code_mstr.code_id) " _
                & "  INNER JOIN public.psperiode_mstr ON (public.dbr_mstr.dbr_periode_oid = public.psperiode_mstr.periode_oid) " _
                & "  INNER JOIN public.sls_program ON (public.dbr_mstr.dbr_slsprog_oid = public.sls_program.sls_oid)"
                


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        'dbr_en_id.EditValue = ""
        dbr_code.EditValue = ""
        dbr_program.ItemIndex = 0
        dbr_dbg_name.EditValue = ""

        dbr_dbg_name.Text = ""
        _dbr_dbg_name_oid = ""
        dbr_dbg_name.Enabled = True
        dbr_city.ItemIndex = 0
        dbr_periode.ItemIndex = 0
        dbr_start_date.DateTime = CekTanggal()
        dbr_end_date.EditValue = CekTanggal()

        dbr_remarks.EditValue = ""
        dbr_dbg_name.Focus()

        dbr_process.Enabled = True

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
                        & "  public.dbrd_det.dbrd_oid, " _
                        & "  public.dbrd_det.dbrd_dbr_oid, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.dbrd_det.dbg_en_id, " _
                        & "  public.dbrd_det.dbrd_ar_oid, " _
                        & "  public.dbrd_det.dbrd_ar_eff_date, " _
                        & "  public.dbrd_det.dbrd_ar_date, " _
                        & "  public.dbrd_det.dbrd_ars_invoice, " _
                        & "  public.dbrd_det.dbrd_ar_code, " _
                        & "  public.dbrd_det.dbrd_ar_amount, " _
                        & "  public.dbrd_det.dbrd_ar_duedate, " _
                        & "  public.dbrd_det.dbrd_arpayd_date, " _
                        & "  public.dbrd_det.dbrd_apyad_amount, " _
                        & "  public.dbrd_det.dbrd_point " _
                        & "FROM " _
                        & "  public.dbrd_det " _
                        & "  INNER JOIN public.en_mstr ON (public.dbrd_det.dbg_en_id = public.en_mstr.en_id)" _
                        & "WHERE " _
                        & " dbrd_dbr_oid IS NULL "

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

    Private Sub dbr_process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbr_process.Click

        'If ds_edit_dbg.Tables.Count = 0 Then
        '    '    Exit Sub
        'ElseIf ds_edit_dbg.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        Dim _dbg_code As String = ""
        Dim i As Integer

        'For i = 0 To ds_edit_dbg.Tables(0).Rows.Count - 1
        _dbg_code = "'" + SetSetring(dbr_dbg_name.EditValue) + "',"
        'Next

        '_dbr_code = _dbr_code.Substring(0, _dbr_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT public.dbg_group.dbg_code, " _
                        & "public.dbg_group.dbg_name, " _
                        & "public.dbgd_det.dbgd_ptnr_id, " _
                        & "public.ptnr_mstr.ptnr_name, " _
                        & "public.dbgd_det.dbgd_en_id, " _
                        & "public.en_mstr.en_desc, " _
                        & "public.ar_mstr.ar_date, " _
                        & "public.ar_mstr.ar_eff_date, " _
                        & "public.ar_mstr.ar_code, " _
                        & "public.arpay_payment.arpay_total_final, " _
                        & "public.arpayd_det.arpayd_dt, " _
                        & "SUM(public.ars_ship.ars_invoice) AS qty, " _
                        & "public.ar_mstr.ar_total_final, " _
                        & "FLOOR (ar_total_final / 1000000) AS point, " _
                        & "FLOOR (sum(ar_total_final) over(order by public.ar_mstr.ar_date asc rows between unbounded preceding and current row)) as total_ar, " _
                        & "FLOOR (sum(ar_total_final) over(order by public.ar_mstr.ar_date asc rows between unbounded preceding and current row))/1000000 as total_point, " _
                        & "public.ar_mstr.ar_due_date " _
                        & "FROM public.dbg_group " _
                        & "INNER JOIN public.dbgd_det ON (public.dbg_group.dbg_oid = public.dbgd_det.dbgd_dbg_oid) " _
                        & "INNER JOIN public.ptnr_mstr ON (public.dbgd_det.dbgd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                        & "INNER JOIN public.ar_mstr ON (public.ptnr_mstr.ptnr_id = public.ar_mstr.ar_bill_to) " _
                        & "INNER JOIN public.ars_ship ON (public.ar_mstr.ar_oid = public.ars_ship.ars_ar_oid) " _
                        & "INNER JOIN public.en_mstr ON (public.dbgd_det.dbgd_en_id = public.en_mstr.en_id) " _
                        & "INNER JOIN public.arpayd_det ON (public.ar_mstr.ar_oid = public.arpayd_det.arpayd_ar_oid) " _
                        & "INNER JOIN public.arpay_payment ON (public.arpay_payment.arpay_oid = public.arpayd_det.arpayd_arpay_oid) " _
                        & "WHERE public.dbg_group.dbg_code = " + SetSetring(dbr_dbg_name.EditValue) + " AND " _
                        & " public.ar_mstr.ar_date BETWEEN '01/07/2021' AND '31/07/2021' " _
                        & " and public.arpayd_det.arpayd_dt <= public.ar_mstr.ar_due_date " _
                        & "GROUP BY public.dbg_group.dbg_code, " _
                        & "  public.dbg_group.dbg_name, " _
                        & "  public.dbgd_det.dbgd_ptnr_id, " _
                        & "  public.ptnr_mstr.ptnr_name, " _
                        & "  public.dbgd_det.dbgd_en_id, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.ar_mstr.ar_date, " _
                        & "  public.ar_mstr.ar_eff_date, " _
                        & "  public.ar_mstr.ar_code, " _
                        & "  public.arpay_payment.arpay_total_final, " _
                        & "  public.ar_mstr.ar_total_final, " _
                        & "  public.arpayd_det.arpayd_dt, " _
                        & "  point, " _
                        & "  public.ar_mstr.ar_due_date " _
                        & "ORDER BY public.ar_mstr.ar_date ASC"


                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "dbr_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Dim ssql As String
        'ssql = "select sum(coalesce(so_shipping_charges,0)) as jml from so_mstr where so_code in (" & _so_code & ")"

        'Dim dt_so As New DataTable
        'dt_so = GetTableData(ssql)


        ds_edit_shipment.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("qty_open") <> 0 Then
                _dtrow = ds_edit_shipment.Tables(0).NewRow
                _dtrow("pcss_oid") = Guid.NewGuid.ToString
                _dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
                _dtrow("pcss_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid")
                _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pcss_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("pcss_shipment") = ds_bantu.Tables(0).Rows(i).Item("qty_shipment")
                _dtrow("pcss_packing") = ds_bantu.Tables(0).Rows(i).Item("qty_packing")
                _dtrow("pcss_collie_number") = "1"
                _dtrow("pcss_close_line") = "N"
                ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
            End If
        Next

        ''Dim ssql As String
        'ssql = "SELECT  " _
        '    & "  soship_date " _
        '    & "FROM  " _
        '    & "  public.soshipd_det " _
        '    & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
        '    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
        '    & "  inner join so_mstr on so_oid = sod_so_oid " _
        '    & "  inner join pt_mstr on pt_id = sod_pt_id " _
        '    & "  where coalesce(soshipd_close_line,'N') = 'N' " _
        '    & "  and so_code in (" + _so_code + ") and soship_is_shipment='Y'   " _
        '    & "  order by soship_date desc"

        'Dim dt As New DataTable
        'dt = master_new.PGSqlConn.GetTableData(ssql)


        ''pcs_eff_date.DateTime = dt.Rows(0).Item("soship_date")
        ''(i) disini pasti line yang terakhir

        'ds_edit_shipment.Tables(0).AcceptChanges()

        'gv_edit.BestFitColumns()

    End Sub

    Public Overrides Function insert() As Boolean

        Dim _mstr_oid As String = Guid.NewGuid.ToString
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _code As String

        _code = GetNewNumberYM("dbr_print", "dbr_code", 5, "DBR" & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try

            ssql = "INSERT INTO  " _
                & "  public.dbr_mstr " _
                & "( " _
                & "  dbr_oid, " _
                & "  dbr_code, " _
                & "  dbr_slsprog_code, " _
                & "  dbr_dbg_code, " _
                & "  dbr_dbgcity_id, " _
                & "  dbr_periode_oid, " _
                & "  dbr_start_date, " _
                & "  dbr_end_date, " _
                & "  dbr_remarks, " _
                & "  dbr_add_by, " _
                & "  dbr_add_date " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetSetring(_code) & ",  " _
                & SetSetring(dbr_program.EditValue) & ",  " _
                & SetSetring(dbr_dbg_name.EditValue) & ",  " _
                & SetInteger(dbr_city.EditValue) & ",  " _
                & SetDate(dbr_start_date.DateTime) & ",  " _
                & SetDate(dbr_end_date.DateTime) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetSetring(dbr_remarks.Text) & "  " _
                & ")"

            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)

                    ssql = "INSERT INTO  " _
                        & "  public.dbrd_det " _
                        & "( " _
                        & "  dbrd_oid, " _
                        & "  dbrd_dbr_oid, " _
                        & "  dbrd_ar_oid, " _
                        & "  dbrd_ar_code " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetSetring(.Item("dbrd_ar_oid")) & ",  " _
                        & SetSetring(.Item("dbrd_ar_code")) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                    'If ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid").ToString <> "" Then
                    '    'update karena ada hubungan antara so dan po antar group perusahaan
                    '    ssql = "update ar_mstr set ar_due_date = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("dbr_due_date")) _
                    '                         & " where sod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid"))
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
            set_row(_mstr_oid, "dbr_oid")
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

        'If MyBase.edit_data = True Then
        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _mstr_oid = .Item("dbr_oid")
        '        dbr_en_id.EditValue = .Item("dbr_en_id")
        '        dbr_en_id.Enabled = False

        '        dbr_start_date.DateTime = .Item("dbr_date")
        '        dbr_start_date.Enabled = False

        '        dbr_end_date.DateTime = .Item("dbr_due_date")
        '        dbr_end_date.Enabled = False

        '        dbr_credit_term.EditValue = .Item("dbr_credit_term")
        '        dbr_credit_term.Enabled = False

        '        dbr_dbg_id.Tag = .Item("dbr_ptnr_id")
        '        dbr_dbg_id.Enabled = False

        '        dbr_dbg_id.EditValue = .Item("ptnr_name")
        '        dbr_dbg_id.Enabled = False

        '        dbr_remarks.EditValue = .Item("dbr_remarks")

        '        dbr_ship_receipt.EditValue = CekTanggal()
        '        dbr_ship_receipt.Enabled = True

        '        dbr_duedate_pay.EditValue = _paythen
        '        dbr_duedate_pay.Enabled = True

        '    End With
        '    dbr_en_id.Focus()

        '    ds_edit = New DataSet
        '    Try
        '        Using objcb As New master_new.CustomCommand
        '            With objcb
        '                .SQL = "SELECT  " _
        '                & "  a.dbrd_oid, " _
        '                & "  a.dbrd_dbr_oid, " _
        '                & "  a.dbrd_ar_oid, " _
        '                & "  a.dbrd_ar_code, " _
        '                & "  a.dbrd_duedate_pay " _
        '                & "FROM " _
        '                & "  public.dbrd_det a " _
        '                & "WHERE " _
        '                & "  a.dbrd_dbr_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_oid") & "' "

        '                .InitializeCommand()
        '                .FillDataSet(ds_edit, "detail")
        '                gc_edit.DataSource = ds_edit.Tables(0)
        '                gv_edit.BestFitColumns()


        '                'If ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid").ToString <> "" Then
        '                '    'update karena ada hubungan antara so dan po antar group perusahaan
        '                '    .SQL = "update ar_mstr set ar_due_date = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("dbr_due_date")) _
        '                '                         & " where sod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid"))
        '                '    ssqls.Add(ssql)
        '                'End If

        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try

        '    edit_data = True
        'End If
    End Function

    Public Overrides Function edit()
        'edit = True
        'Dim ssqls As New ArrayList
        'Dim i As Integer
        'gv_edit.UpdateCurrentRow()
        'ds_edit.AcceptChanges()
        'Try
        '    Using objinsert As New master_new.CustomCommand
        '        With objinsert
        '.Command.Open()
        '            ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                '.Command = .Connection.CreateCommand
        '                '.Command.Transaction = sqlTran
        '                '.Command.CommandType = CommandType.Text

        '                .Command.CommandText = "UPDATE  " _
        '                                & "  public.dbr_print   " _
        '                                & "SET  " _
        '                                & "  dbr_en_id = " & SetInteger(dbr_en_id.EditValue) & ",  " _
        '                                & "  dbr_ship_receipt = " & SetDate(dbr_ship_receipt.DateTime) & ",  " _
        '                                & "  dbr_duedate_pay = " & SetDate(dbr_duedate_pay.DateTime) & ",  " _
        '                                & "  dbr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
        '                                & "  dbr_credit_term = " & SetInteger(dbr_credit_term.EditValue) & ",  " _
        '                                & "  dbr_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
        '                                & "  dbr_remarks = " & SetSetring(dbr_remarks.Text) & "  " _
        '                                & "WHERE  " _
        '                                & "  dbr_oid = " & SetSetring(_mstr_oid) & " "

        '                ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                '.Command.Parameters.Clear()

        '                '.Command.CommandType = CommandType.Text
        '                .Command.CommandText = "Delete from dbrd_det " _
        '                                    & "WHERE  " _
        '                                    & "  dbrd_dbr_oid = " & SetSetring(_mstr_oid) & " "

        '                ssqls.Add(.Command.CommandText)
        '                .Command.ExecuteNonQuery()
        '                '.Command.Parameters.Clear()

        '                For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '                    'With ds_edit.Tables(0).Rows(i)
        '                    '.Command.CommandType = CommandType.Text
        '                    .Command.CommandText = "INSERT INTO  " _
        '                        & "  public.dbrd_det " _
        '                        & "( " _
        '                        & "  dbrd_oid, " _
        '                        & "  dbrd_dbr_oid, " _
        '                        & "  dbrd_ar_oid, " _
        '                        & "  dbrd_ar_code, " _
        '                        & "  dbrd_duedate_pay " _
        '                        & ")  " _
        '                        & "VALUES ( " _
        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                        & SetSetring(_mstr_oid) & ",  " _
        '                        & SetSetring(ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid")) & ",  " _
        '                        & SetSetring(ds_edit.Tables(0).Rows(i).Item("dbrd_ar_code")) & ",  " _
        '                        & SetDate(dbr_duedate_pay.DateTime) & "  " _
        '                        & ")"

        '                    ssqls.Add(.Command.CommandText)
        '                    .Command.ExecuteNonQuery()
        '                    '.Command.Parameters.Clear()

        '                    'If ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid").ToString <> "" Then
        '                    'update karena ada hubungan antara so dan po antar group perusahaan
        '                    '.Command.CommandType = CommandType.Text
        '                    .Command.CommandText = "update ar_mstr set ar_due_date = " + SetDate(dbr_duedate_pay.DateTime) _
        '                                         & " where ar_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid"))
        '                    'ssqls.Add(ssql)
        '                    ssqls.Add(.Command.CommandText)
        '                    .Command.ExecuteNonQuery()
        '                    '.Command.Parameters.Clear()
        '                    'End If

        '                    'End With

        '                Next

        '                'For i = 0 To ds_edit.Tables(0).Rows.Count - 1

        '                'Next

        '                If master_new.PGSqlConn.status_sync = True Then
        '                    For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
        '                        '.Command.CommandType = CommandType.Text
        '                        .Command.CommandText = Data
        '                        .Command.ExecuteNonQuery()
        '                        '.Command.Parameters.Clear()
        '                    Next
        '                End If


        '                .Command.Commit()

        '                after_success()
        '                set_row(_mstr_oid, "dbr_oid")
        '                dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        '                edit = True
        '            Catch ex As PgSqlException
        '                'sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '                edit = False
        '            End Try



        '        End With
        '    End Using
        'Catch ex As Exception
        '    edit = False
        '    MessageBox.Show(ex.Message)
        'End Try
        'Return edit
    End Function
    Public Overrides Function before_delete() As Boolean
        before_delete = True


    End Function

    Public Overrides Function delete_data() As Boolean
        'delete_data = False

        'gv_master_SelectionChanged(Nothing, Nothing)

        'Dim sSQL As String
        'delete_data = True
        'If ds.Tables.Count = 0 Then
        '    delete_data = False
        '    Exit Function
        'ElseIf ds.Tables(0).Rows.Count = 0 Then
        '    delete_data = False
        '    Exit Function
        'End If

        'If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
        '    Exit Function
        'End If

        'Dim ssqls As New ArrayList

        'If before_delete() = True Then
        '    row = BindingContext(ds.Tables(0)).Position

        '    If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
        '        row = row - 1
        '    ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
        '        row = 0
        '    End If

        '    Try
        '        With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

        '            sSQL = "DELETE FROM  " _
        '                & "  public.dbr_print  " _
        '                & "WHERE  " _
        '                & "  dbr_oid ='" & .Item("dbr_oid") & "'"

        '            ssqls.Add(sSQL)


        '        End With

        '        If master_new.PGSqlConn.status_sync = True Then
        '            If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then
        '                Return False
        '                Exit Function
        '            End If
        '            ssqls.Clear()
        '        Else
        '            If DbRunTran(ssqls, "") = False Then
        '                Return False
        '                Exit Function
        '            End If
        '            ssqls.Clear()
        '        End If

        '        help_load_data(True)
        '        MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'End If

        'Return delete_data
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
        '    If SetString(ds_edit.Tables(0).Rows(i).Item("dbrd_ar_oid")) = "" Then
        '        Box("AR can't blank")
        '        Return False
        '        Exit Function
        '    End If

        'Next
        Return before_save
    End Function

    Private Sub dbr_periode_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbr_periode.EditValueChanged
        Try
            dbr_start_date.EditValue = dbr_periode.GetColumnValue("seperiode_start_date")
            dbr_end_date.EditValue = dbr_periode.GetColumnValue("seperiode_end_date")
            'dbr_remarks.EditValue = dbr_periode.GetColumnValue("seperiode_remarks")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

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

            sql = "SELECT  " _
                & "  a.dbrd_oid, " _
                & "  a.dbrd_dbr_oid, " _
                & "  a.dbrd_ar_oid, " _
                & "  a.dbrd_ar_code " _
                & "FROM " _
                & "  public.dbrd_det a " _
                & "WHERE " _
                & "  a.dbrd_dbr_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_oid") & "' "

            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub



    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub browse_data()
        'Dim _col As String = gv_edit.FocusedColumn.Name
        'Dim _row As Integer = gv_edit.FocusedRowHandle
        'Dim _en_id As Integer = casho_en_id.EditValue

        'If _col = "dbrd_ar_code" Then
        '    Dim frm As New FDRCRMemoSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    'frm._en_id = dbr_en_id.EditValue
        '    frm._ptnr_id = dbr_dbg_name.Tag
        '    frm.type_form = True
        '    frm.ShowDialog()

        'End If
    End Sub

    Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try

            Dim frm As New FPartnerSearchNE
            frm.set_win(Me)
            frm._obj = dbr_dbg_name
            'frm._en_id = dbr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    'Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
    '    Try
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

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'Private Sub casho_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        ' init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'Public Overrides Sub preview()
    '    'Dim ds_bantu As New DataSet
    '    'Dim _sql As String

    '    '_sql = "SELECT  " _
    '    '    & "  cashi_oid, " _
    '    '    & "  cashi_dom_id, " _
    '    '    & "  cashi_en_id, " _
    '    '    & "  cashi_add_by, " _
    '    '    & "  cashi_add_date, " _
    '    '    & "  cashi_upd_by, " _
    '    '    & "  cashi_upd_date, " _
    '    '    & "  cashi_bk_id, " _
    '    '    & "  cashi_ptnr_id, " _
    '    '    & "  cashi_code, " _
    '    '    & "  cashi_date, " _
    '    '    & "  cashi_remarks, " _
    '    '    & "  cashi_reff, " _
    '    '    & "  cashi_cu_id, " _
    '    '    & "  cashi_exc_rate, " _
    '    '    & "  cashi_amount, " _
    '    '    & "  cashi_amount * cashi_exc_rate as cashi_amount_ext, " _
    '    '    & "  cashi_check_number, " _
    '    '    & "  cashi_post_dated_check, " _
    '    '    & "  cashid_oid, " _
    '    '    & "  cashid_cashi_oid, " _
    '    '    & "  cashid_ac_id, " _
    '    '    & "  cashid_amount, " _
    '    '    & "  cashid_amount * cashi_exc_rate as cashid_amount_ext, " _
    '    '    & "  cashid_remarks, " _
    '    '    & "  cashid_seq, " _
    '    '    & "  bk_name, " _
    '    '    & "  ptnr_name, " _
    '    '    & "  ac_code, " _
    '    '    & "  ac_name, " _
    '    '    & "  cmaddr_name, " _
    '    '    & "  cmaddr_line_1, " _
    '    '    & "  cmaddr_line_2, " _
    '    '    & "  cmaddr_line_3 " _
    '    '    & "FROM  " _
    '    '    & "  cashi_in " _
    '    '    & "inner join cashid_detail on cashid_cashi_oid = cashi_oid " _
    '    '    & "inner join bk_mstr on bk_id = cashi_bk_id " _
    '    '    & "inner join ptnr_mstr on ptnr_id = cashi_ptnr_id " _
    '    '    & "inner join cu_mstr on cu_id = cashi_cu_id " _
    '    '    & "inner join ac_mstr on ac_id = cashid_ac_id " _
    '    '    & "inner join cmaddr_mstr on cmaddr_en_id = cashi_en_id" _
    '    '    & "  where cashi_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code") + "'"

    '    'Dim frm As New frmPrintDialog
    '    'frm._ssql = _sql
    '    'frm._report = "XRCashInPrint"
    '    'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code")
    '    'frm.ShowDialog()


    '    Dim _ar_code, _code As String

    '    ssql = "SELECT   b.dbrd_ar_code FROM  public.dbrd_det b WHERE  b.dbrd_dbr_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_oid") & "' "
    '    Dim dt As New DataTable
    '    dt = GetTableData(ssql)
    '    _ar_code = ""
    '    _code = ""
    '    For Each dr As DataRow In dt.Rows
    '        _ar_code = _ar_code & "'" & dr(0) & "',"
    '        _code = dr(0)
    '    Next

    '    _ar_code = Microsoft.VisualBasic.Left(_ar_code, _ar_code.Length - 1)
    '    Dim _en_id As Integer
    '    Dim _type, _table, _initial, _code_awal, _code_akhir As String
    '    Dim func_coll As New function_collection

    '    _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_en_id")
    '    _type = 13
    '    _table = "ar_mstr"
    '    _initial = "ar"
    '    _code_awal = _code
    '    _code_akhir = _code

    '    func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

    '    Dim ds_bantu As New DataSet
    '    Dim _sql As String

    '    '_sql = "select  " _
    '    '    & "dbr_code, " _
    '    '    & "dbr_date, " _
    '    '    & "dbr_due_date, " _
    '    '    & "dbr_remarks, " _
    '    '    & "sod_pt_id, " _
    '    '    & "ar_bill_to, " _
    '    '    & "ptnr_name, " _
    '    '    & "ptnra_line_1, " _
    '    '    & "ptnra_line_2, " _
    '    '    & "ptnra_line_3, " _
    '    '    & "ptnra_zip, " _
    '    '    & "ar_cu_id, " _
    '    '    & "cu_name, " _
    '    '    & "credit_term_mstr.code_name as credit_term_name, " _
    '    '    & "cu_symbol, " _
    '    '    & "um_master.code_name as um_name, " _
    '    '    & "pt_code, " _
    '    '    & "pt_desc1, " _
    '    '    & "pt_desc2, " _
    '    '    & "sod_tax_inc, " _
    '    '    & "tax_class_mstr.code_name tax_class_name, " _
    '    '    & "tax_type_mstr.code_name tax_type_name, " _
    '    '    & "taxr_rate, " _
    '    '    & "sum(ars_invoice) as qty_total, " _
    '    '    & "ars_invoice_price +(sod_price * sod_disc) as ars_invoice_price2, " _
    '    '    & "ars_so_price, " _
    '    '    & "sod_disc, " _
    '    '    & "ars_so_disc_value, " _
    '    '    & "ars_invoice_price, " _
    '    '    & "sum(ars_invoice * ars_invoice_price) as ars_invoice_price3, " _
    '    '    & "sum(ars_invoice * (ars_invoice_price +(sod_price * sod_disc))) as ars_invoice_price4, " _
    '    '    & "sum(ars_invoice * ars_so_disc_value) as ars_invoice_price5, " _
    '    '    & "cmaddr_code, " _
    '    '    & "cmaddr_name, " _
    '    '    & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
    '    '    & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
    '    '    & "trim(cmaddr_tax_line_1 || ' ' || cmaddr_tax_line_2 || ' ' || cmaddr_tax_line_3) as cmaddr_line_1_pusat, " _
    '    '    & "'Telp : ' || cmaddr_tax_phone_1 || ' ' || ' Fax : ' || cmaddr_tax_phone_2 as cmaddr_line_2_pusat, " _
    '    '    & "bk_name, " _
    '    '    & "bk_code, " _
    '    '    & "ac_name, " _
    '    '    & "coalesce(tranaprvd_name_1, '') as tranaprvd_name_1, " _
    '    '    & "coalesce(tranaprvd_name_2, '') as tranaprvd_name_2, " _
    '    '    & "coalesce(tranaprvd_name_3, '') as tranaprvd_name_3, " _
    '    '    & "coalesce(tranaprvd_name_4, '') as tranaprvd_name_4, " _
    '    '    & "tranaprvd_pos_1, " _
    '    '    & "tranaprvd_pos_2, " _
    '    '    & "tranaprvd_pos_3, " _
    '    '    & "tranaprvd_pos_4 " _
    '    '    & "from ars_ship " _
    '    '    & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
    '    '    & "inner join ar_mstr on ar_oid = ars_ar_oid " _
    '    '    & "INNER JOIN public.dbrd_det ON (public.ar_mstr.ar_oid = public.dbrd_det.dbrd_ar_oid) " _
    '    '    & "INNER JOIN public.dbr_print ON (public.dbrd_det.dbrd_dbr_oid = public.dbr_print.dbr_oid) " _
    '    '    & "inner join sod_det on sod_oid = soshipd_sod_oid " _
    '    '    & "inner join so_mstr on so_oid = sod_so_oid " _
    '    '    & "inner join pt_mstr on pt_id = sod_pt_id " _
    '    '    & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = ars_tax_class_id " _
    '    '    & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = ars_tax_class_id " _
    '    '    & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
    '    '    & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
    '    '    & "inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
    '    '    & "inner join cu_mstr on cu_id = ar_cu_id " _
    '    '    & "inner join code_mstr um_master on um_master.code_id = sod_um " _
    '    '    & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
    '    '    & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
    '    '    & "inner join bk_mstr on bk_id = ar_bk_id " _
    '    '    & "inner join ac_mstr on ac_id = bk_ac_id " _
    '    '    & "left outer join tranaprvd_dok on tranaprvd_tran_oid = ar_oid " _
    '    '    & "where tax_type_mstr.code_name = 'PPN'" _
    '    '    & "and ar_code in (" & _ar_code & ")" _
    '    '    & "and (ars_invoice) > '0' " _
    '    '    & "group by sod_pt_id, " _
    '    '    & "dbr_code, " _
    '    '    & "dbr_date, " _
    '    '    & "dbr_due_date, " _
    '    '    & "pt_code, " _
    '    '    & "ar_bill_to, " _
    '    '    & "ptnra_line_1, " _
    '    '    & "ptnr_name, " _
    '    '    & "ptnra_line_1, " _
    '    '    & "ptnra_line_2, " _
    '    '    & "ptnra_line_3, " _
    '    '    & "ptnra_zip, " _
    '    '    & "cu_name, " _
    '    '    & "credit_term_name, " _
    '    '    & "cu_symbol, " _
    '    '    & "um_name, " _
    '    '    & "pt_desc1, " _
    '    '    & "pt_desc2, " _
    '    '    & "sod_disc, " _
    '    '    & "sod_tax_inc, " _
    '    '    & "tax_class_name, " _
    '    '    & "tax_type_name, " _
    '    '    & "taxr_rate, " _
    '    '    & "ars_so_price, " _
    '    '    & "ars_so_disc_value, " _
    '    '    & "ars_invoice_price, " _
    '    '    & "sod_price, " _
    '    '    & "cmaddr_code, " _
    '    '    & "cmaddr_name, " _
    '    '    & "cmaddr_line_1, " _
    '    '    & "cmaddr_line_2, " _
    '    '    & "cmaddr_line_3, " _
    '    '    & "cmaddr_phone_1, " _
    '    '    & "cmaddr_phone_2, " _
    '    '    & "cmaddr_tax_phone_1, " _
    '    '    & "cmaddr_tax_phone_2, " _
    '    '    & "cmaddr_tax_line_1, " _
    '    '    & "cmaddr_tax_line_2, " _
    '    '    & "cmaddr_tax_line_3, " _
    '    '    & "bk_name, " _
    '    '    & "bk_code, " _
    '    '    & "ac_name, " _
    '    '    & "tranaprvd_name_1, " _
    '    '    & "tranaprvd_name_2, " _
    '    '    & "tranaprvd_name_3, " _
    '    '    & "tranaprvd_name_4, " _
    '    '    & "tranaprvd_pos_1, " _
    '    '    & "tranaprvd_pos_2, " _
    '    '    & "tranaprvd_pos_3, " _
    '    '    & "tranaprvd_pos_4, " _
    '    '    & "dbr_remarks, " _
    '    '    & "ar_cu_id, " _
    '    '    & "cu_name " _
    '    '    & "order by pt_desc1 ASC"

    '    _sql = "SELECT dbr_code, " _
    '        & "dbr_date, " _
    '        & "dbr_due_date, " _
    '        & "dbr_remarks, " _
    '        & "sod_pt_id, " _
    '        & "ar_bill_to, " _
    '        & "ptnr_name, " _
    '        & "ptnra_line_1, " _
    '        & "ptnra_line_2, " _
    '        & "ptnra_line_3, " _
    '        & "ptnra_zip, " _
    '        & "ar_cu_id, " _
    '        & "cu_name, " _
    '        & "cu_symbol, " _
    '        & "credit_term_mstr.code_name as credit_term_name, " _
    '        & "cmaddr_code, " _
    '        & "cmaddr_name, " _
    '        & "trim(cmaddr_line_1 || ' ' || cmaddr_line_2 || ' ' || cmaddr_line_3) as cmaddr_line_1, " _
    '        & "'Telp : ' || cmaddr_phone_1 || ' ' || ' Fax : ' || cmaddr_phone_2 as cmaddr_line_2, " _
    '        & "bk_name, " _
    '        & "bk_code, " _
    '        & "ar_cu_id, " _
    '        & "cu_name, " _
    '        & "cu_symbol, " _
    '        & "pt_code, " _
    '        & "pt_desc1, " _
    '        & "sum(ars_shipment) AS shipment, " _
    '        & "um_master.code_name as um_name, " _
    '        & "soshipd_um, " _
    '        & "ar_credit_term, " _
    '        & "ars_so_price AS harga_sebelum_diskon, " _
    '        & "sod_disc AS diskon, " _
    '        & "ars_so_disc_value AS nilai_diskon, " _
    '        & "ars_invoice_price AS harga_setelah_diskon, " _
    '        & "sum(ars_shipment * ars_invoice_price) AS total_invoiced, " _
    '        & "sum(ars_shipment * ars_so_price) AS total_bruto, " _
    '        & "sum(ars_shipment * ars_so_disc_value) AS total_diskon, " _
    '        & "sum(ars_shipment * ars_invoice_price)/1000000 AS total_point " _
    '        & "FROM ar_mstr " _
    '        & "INNER JOIN dbrd_det ON dbrd_ar_oid = ar_oid " _
    '        & "INNER JOIN dbr_print ON dbr_oid = dbrd_dbr_oid " _
    '        & "INNER JOIN ars_ship ON ars_ar_oid = ar_oid " _
    '        & "INNER JOIN soshipd_det ON soshipd_oid = ars_soshipd_oid " _
    '        & "INNER JOIN soship_mstr ON soship_oid = soshipd_soship_oid " _
    '        & "INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
    '        & "INNER JOIN so_mstr ON so_oid = sod_so_oid AND (so_oid = soship_so_oid) " _
    '        & "INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
    '        & "INNER JOIN ptnr_mstr ON ptnr_id = ar_bill_to " _
    '        & "INNER JOIN ptnra_addr ON ptnra_ptnr_oid = ptnr_oid " _
    '        & "INNER JOIN cu_mstr ON cu_id = ar_cu_id " _
    '        & "inner join code_mstr um_master on um_master.code_id = sod_um " _
    '        & "inner join bk_mstr on bk_id = ar_bk_id " _
    '        & "inner join ac_mstr on ac_id = bk_ac_id " _
    '        & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
    '        & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
    '        & "WHERE soship_mstr.soship_code NOT LIKE 'ST%' " _
    '        & "and ar_code in (" & _ar_code & ")" _
    '        & "GROUP BY dbr_code, " _
    '        & "dbr_date, " _
    '        & "dbr_remarks, " _
    '        & "sod_pt_id, " _
    '        & "ar_bill_to, " _
    '        & "ptnr_name, " _
    '        & "ptnra_line_1, " _
    '        & "ptnra_line_2, " _
    '        & "ptnra_line_3, " _
    '        & "ptnra_zip, " _
    '        & "ar_cu_id, " _
    '        & "cu_name, " _
    '        & "cu_symbol, " _
    '        & "pt_code, " _
    '        & "pt_desc1, " _
    '        & "soshipd_um, " _
    '        & "ar_credit_term, " _
    '        & "ars_so_price, " _
    '        & "sod_disc, " _
    '        & "ars_so_disc_value, " _
    '        & "ars_invoice_price, " _
    '        & "cmaddr_code, " _
    '        & "cmaddr_name, " _
    '        & "cmaddr_line_1, " _
    '        & "cmaddr_line_2, " _
    '        & "cmaddr_line_3, " _
    '        & "cmaddr_phone_1, " _
    '        & "cmaddr_phone_2, " _
    '        & "cmaddr_code, " _
    '        & "cmaddr_name, " _
    '        & "dbr_due_date, " _
    '        & "bk_name, " _
    '        & "bk_code, " _
    '        & "credit_term_name, " _
    '        & "um_name " _
    '        & "ORDER BY pt_desc1"


    '    Dim frm As New frmPrintDialog
    '    frm._ssql = _sql
    '    frm._report = "XRInvoiceMergeDetail"
    '    frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_code")
    '    frm.ShowDialog()

    'End Sub
End Class
