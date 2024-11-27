Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDPointRewardReportsOffside
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _dbr_oid_mstr As String
    Public ds_edit_shipment, ds_edit_dist As DataSet
    Public _be_dbr_dbg_name_oid, _be_dbr_periode_code, _dbr_city_id As String
    Public _dbr_arp_oid As String
    Dim _now As DateTime
    Public _par_cus_id As String

    Private Sub FDPointRewardReportsOffside_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        'init_le(dbr_program, "en_mstr")

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
            .Properties.ValueMember = dt_bantu.Columns("sls_id").ToString
            '.Properties.ValueMember = dt_bantu.Columns("sls_oid").ToString
            .ItemIndex = 0
        End With

        'dt_bantu = New DataTable
        'dt_bantu = func_data.load_periode_mstr()
        'With be_dbr_periode
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("periode_id").ToString
        '    .ItemIndex = 0
        'End With

        'dt_bantu = New DataTable
        'dt_bantu = func_data.load_grouping_mstr()
        'With be_dbr_dbg_name
        '    .Properties.DataSource = dt_bantu
        '    .Properties.DisplayMember = dt_bantu.Columns("dbg_name").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("dbg_code").ToString
        '    .Properties.ValueMember = dt_bantu.Columns("dbg_oid").ToString
        '    .ItemIndex = 0
        'End With

    End Sub

    Public Overrides Sub load_cb_en()
        'init_le(dbr_dbg_name, "cus_mstr_parent", dbr_program.EditValue)
    End Sub

    Private Sub ap_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbr_program.EditValueChanged
        'load_cb_en()
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Code", "dbr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Programs", "sls_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group Name", "dbg_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Groups Name", "dbr_dbg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "City", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "dbr_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Periode", "periode_code", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Start Date", "dbr_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "dbr_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Total Periode Sales", "total_so_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Point Estimate", "total_so_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Total Periode Payment", "total_ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Realization Point", "total_ar_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Previous Points", " ", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Total Points", " ", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Point Counting", " ", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Closed", "dbr_close_stat", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "dbr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "dbr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "dbr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "dbr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "dbr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Accounts Receivable", "dbrd_drcr_tot", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")

        'add_column(gv_detail_so, "pcso_dbr_oid", False)
        'add_column_copy(gv_detail_so, "SO Number", "pcso_so_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "dbrd_dbr_oid", False)
        add_column(gv_detail, "Brand", "dbrd_en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Goods Receipt", "dbrd_ar_eff_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "Invoice Date", "dbrd_ar_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "AR Code", "dbrd_ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Qty", "dbrd_ars_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Sales Amount", "dbrd_so_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_detail, "dbrd_ret_amount", False)
        add_column(gv_detail, "Sales Point", "dbrd_so_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_detail, "Payment Due", "dbrd_ar_duedate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "Transfer Date", "dbrd_arpayd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "Payment Date", "dbrd_ar_close_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_detail, "Payment Code", "dbrd_arpay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Payment Amount", "dbrd_ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_detail, "Point", "dbrd_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_detail, "Total Payment", "dbrd_apyad_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_detail, "Sum Point", "dbrd_tot_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_detail, "dbrd_drcr_tot", False)

        'add_column(gv_edit_so, "pcso_oid", False)
        'add_column(gv_edit_so, "pcso_dbr_oid", False)
        'add_column(gv_edit_so, "pcso_so_oid", False)
        'add_column(gv_edit_so, "SO Number", "pcso_so_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_so, "pcso_so_date", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "dbrd_oid", False)
        add_column(gv_edit, "dbrd_dbr_oid", False)
        'add_column(gv_edit, "dbrd_soshipd_oid", False)
        add_column(gv_edit, "dbrd_en_id", False)
        add_column(gv_edit, "Brand", "dbrd_en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Goods Receipt", "dbrd_ar_eff_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "Invoice Date", "dbrd_ar_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "AR Code", "dbrd_ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty", "dbrd_ars_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Sales Cek", "dbrd_ar_final", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_edit, "Sales Amount", "dbrd_so_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_edit, "dbrd_ret_amount", False)
        add_column(gv_edit, "Sales Point", "dbrd_so_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_edit, "Payment Due", "dbrd_ar_duedate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "Transfer Date", "dbrd_arpayd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "Payment Date", "dbrd_ar_close_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_edit, "Payment Code", "dbrd_arpay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Payment Amount", "dbrd_ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_edit, "Total payment", "dbrd_apyad_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "Point", "dbrd_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_edit, "Sum Point", "dbrd_tot_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "dbrd_drcr_tot", False)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT DISTINCT  " _
                & "  public.dbr_mstr.dbr_oid, " _
                & "  public.dbr_mstr.dbr_code, " _
                & "  public.dbr_mstr.dbr_slsprog_id, " _
                & "  public.sls_program.sls_name, " _
                & "  public.dbr_mstr.dbr_dbg_oid, " _
                & "  public.dbg_group.dbg_name, " _
                & "  public.dbr_mstr.dbr_dbgcity_id, " _
                & "  public.code_mstr.code_name, " _
                & "  public.dbr_mstr.dbr_periode_id, " _
                & "  public.psperiode_mstr.periode_code, " _
                & "  public.dbr_mstr.dbr_date, " _
                & "  public.dbr_mstr.dbr_start_date, " _
                & "  public.dbr_mstr.dbr_end_date, " _
                & "  public.dbr_mstr.dbr_remarks, " _
                & "  public.dbr_mstr.dbr_close_stat, " _
                & "  public.dbr_mstr.dbr_add_by, " _
                & "  public.dbr_mstr.dbr_add_date, " _
                & "  public.dbr_mstr.dbr_upd_by, " _
                & "  public.dbr_mstr.dbr_upd_date, " _
                & "  SUM(public.dbrd_det.dbrd_so_amount) AS total_so_amount, " _
                & "  SUM(public.dbrd_det.dbrd_so_point) AS total_so_point, " _
                & "  SUM(public.dbrd_det.dbrd_ar_amount) AS total_ar_amount, " _
                & "  SUM(public.dbrd_det.dbrd_point) AS total_ar_point, " _
                & "  public.dbrd_det.dbrd_drcr_tot " _
                & "FROM " _
                & "  public.dbr_mstr " _
                & "  INNER JOIN public.sls_program ON (public.dbr_mstr.dbr_slsprog_id = public.sls_program.sls_id) " _
                & "  INNER JOIN public.dbg_group ON (public.dbr_mstr.dbr_dbg_oid = public.dbg_group.dbg_oid) " _
                & "  INNER JOIN public.code_mstr ON (public.dbr_mstr.dbr_dbgcity_id = public.code_mstr.code_id) " _
                & "  INNER JOIN public.psperiode_mstr ON (public.dbr_mstr.dbr_periode_id = public.psperiode_mstr.periode_id) " _
                & "  INNER JOIN public.dbrd_det ON (public.dbr_mstr.dbr_oid = public.dbrd_det.dbrd_dbr_oid) " _
                & " where dbr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and dbr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "GROUP BY " _
                & "  public.dbr_mstr.dbr_oid, " _
                & "  public.dbr_mstr.dbr_code, " _
                & "  public.dbr_mstr.dbr_slsprog_id, " _
                & "  public.sls_program.sls_name, " _
                & "  public.dbr_mstr.dbr_dbg_oid, " _
                & "  public.dbg_group.dbg_name, " _
                & "  public.dbr_mstr.dbr_dbgcity_id, " _
                & "  public.code_mstr.code_name, " _
                & "  public.dbr_mstr.dbr_periode_id, " _
                & "  public.psperiode_mstr.periode_code, " _
                & "  public.dbr_mstr.dbr_date, " _
                & "  public.dbr_mstr.dbr_start_date, " _
                & "  public.dbr_mstr.dbr_end_date, " _
                & "  public.dbr_mstr.dbr_remarks, " _
                & "  public.dbr_mstr.dbr_add_by, " _
                & "  public.dbr_mstr.dbr_add_date, " _
                & "  public.dbr_mstr.dbr_upd_by, " _
                & "  public.dbr_mstr.dbr_upd_date, " _
                & "  public.dbrd_det.dbrd_drcr_tot " _
                & " order by public.dbr_mstr.dbr_date DESC "



        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        'Try
        '    tcg_detail.SelectedTabPageIndex = 0
        'Catch ex As Exception
        'End Try

        'ARPCode.Text = ""
        '_dbr_arp_oid = ""
        'dbr_arp_code.Enabled = True

        dbr_close_transaction.EditValue = False


        'dbr_start_date.DateTime = _now

        'dbr_program.Focus()
        'dbr_program.ItemIndex = 0
        'dbr_dbg_name.ItemIndex = 0


        dbr_remarks.Text = ""

        'dbr_program.Enabled = True
        'dbr_dbg_name.Enabled = True

        gc_edit.Enabled = True
        sb_retrieve_receive_item.Enabled = True

        be_dbr_dbg_name.Text = ""
        _be_dbr_dbg_name_oid = ""
        be_dbr_dbg_name.Enabled = True

        dbr_city.Text = ""
        _dbr_city_id = ""
        dbr_city.Enabled = True

        be_dbr_periode.Text = ""
        _be_dbr_periode_code = ""
        be_dbr_periode.Enabled = True

        dbr_start_date.Text = _now
        dbr_start_date.Enabled = False

        dbr_end_date.DateTime = _now
        dbr_end_date.Enabled = False

    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_shipment = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  public.dbrd_det.dbrd_oid, " _
                        & "  public.dbrd_det.dbrd_dbr_oid, " _
                        & "  public.dbrd_det.dbrd_seq, " _
                        & "  public.dbrd_det.dbrd_en_id, " _
                        & "  public.dbrd_det.dbrd_en_desc, " _
                        & "  public.dbrd_det.dbrd_ar_oid, " _
                        & "  public.dbrd_det.dbrd_ar_eff_date, " _
                        & "  public.dbrd_det.dbrd_ar_date, " _
                        & "  public.dbrd_det.dbrd_ars_invoice, " _
                        & "  public.dbrd_det.dbrd_ar_code, " _
                        & "  public.dbrd_det.dbrd_ar_amount, " _
                        & "  public.dbrd_det.dbrd_ar_final, " _
                        & "  public.dbrd_det.dbrd_so_amount, " _
                        & "  public.dbrd_det.dbrd_so_point, " _
                        & "  public.dbrd_det.dbrd_ret_amount, " _
                        & "  public.dbrd_det.dbrd_ar_duedate, " _
                        & "  public.dbrd_det.dbrd_ar_close_date, " _
                        & "  public.dbrd_det.dbrd_arpayd_date, " _
                        & "  public.dbrd_det.dbrd_arpay_code, " _
                        & "  public.dbrd_det.dbrd_apyad_amount, " _
                        & "  public.dbrd_det.dbrd_point, " _
                        & "  public.dbrd_det.dbrd_tot_point, " _
                        & "  public.dbrd_det.dbrd_drcr_tot " _
                        & "FROM " _
                        & "  public.dbrd_det " _
                        & "WHERE " _
                        & " dbrd_dbr_oid IS NULL "
                    .InitializeCommand()
                    .FillDataSet(ds_edit_shipment, "shipment")
                    gc_edit.DataSource = ds_edit_shipment.Tables(0)
                    gv_edit.BestFitColumns()

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        'gv_edit_so.UpdateCurrentRow()
        gv_edit.UpdateCurrentRow()

        'ds_edit_so.AcceptChanges()
        ds_edit_shipment.AcceptChanges()

        '*********************
        'Cek close line di tab shipment
        'Dim i As Integer
        'For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
        '    With ds_edit_shipment.Tables(0).Rows(i)
        '        If (.Item("dbrd_open") = .Item("dbrd_invoice")) And (.Item("dbrd_so_price") = .Item("dbrd_invoice_price")) Then
        '            .Item("dbrd_close_line") = "Y"
        '        End If
        '    End With
        'Next
        '*********************

        'If ds_edit_so.Tables(0).Rows.Count >= 2 Then
        '    MessageBox.Show("SO detail can't over than 1 rows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        row = BindingContext(ds.Tables(0)).Position

        'If SetString(ds.Tables(0).Rows(row).Item("dbr_close_stat")).ToString.ToLower = "c" Then
        '    Box("Can't edit close transaction")
        '    Return False
        '    Exit Function
        'End If

        If MyBase.edit_data = True Then

            With ds.Tables(0).Rows(row)
                _dbr_oid_mstr = .Item("dbr_oid")
                dbr_program.EditValue = .Item("dbr_slsprog_id")
                'be_dbr_dbg_name.EditValue = .Item("dbr_dbg_oid")
                dbr_city.EditValue = .Item("dbr_dbgcity_id")
                be_dbr_periode.EditValue = .Item("dbr_periode_id")
                dbr_start_date.DateTime = .Item("dbr_start_date")
                'dbr_due_date.DateTime = .Item("dbr_due_date")
                dbr_end_date.DateTime = .Item("dbr_end_date")

                be_dbr_dbg_name.Text = SetString(.Item("dbg_name"))
                _dbr_oid_mstr = SetString(.Item("dbr_dbg_oid"))
                'so_ptnr_id_sold.Text = SetString(.Item("ptnr_name_sold"))

                dbr_city.Text = SetString(.Item("code_name"))
                _dbr_city_id = SetString(.Item("dbr_dbg_oid"))

                be_dbr_periode.Text = SetString(.Item("periode_code"))
                _be_dbr_periode_code = SetString(.Item("periode_id"))
                '_be_dbr_periode = SetString(.Item("dbr_periode_id"))
                'so_ref_po_code.Text = SetString(.Item("so_ref_po_code"))
                '_so_ref_po_oid = SetString(.Item("so_ref_po_oid"))

            End With

            dbr_program.Focus()
            dbr_program.Enabled = True
            be_dbr_dbg_name.Enabled = True
            'gc_edit_so.Enabled = False
            gc_edit.Enabled = True
            'gc_edit_dist.Enabled = False
            dbr_start_date.Enabled = True
            dbr_end_date.Enabled = True
            sb_retrieve_receive_item.Enabled = True
            'sb_retrieve_dist.Enabled = False

            ds_edit_shipment = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb

                        .SQL = "SELECT  " _
                        & "  public.dbrd_det.dbrd_oid, " _
                        & "  public.dbrd_det.dbrd_dbr_oid, " _
                        & "  public.dbrd_det.dbrd_seq, " _
                        & "  public.dbrd_det.dbrd_en_id, " _
                        & "  public.dbrd_det.dbrd_en_desc, " _
                        & "  public.dbrd_det.dbrd_ar_oid, " _
                        & "  public.dbrd_det.dbrd_ar_eff_date, " _
                        & "  public.dbrd_det.dbrd_ar_date, " _
                        & "  public.dbrd_det.dbrd_ars_invoice, " _
                        & "  public.dbrd_det.dbrd_ar_code, " _
                        & "  public.dbrd_det.dbrd_ar_amount, " _
                        & "  public.dbrd_det.dbrd_ar_final, " _
                        & "  public.dbrd_det.dbrd_so_amount, " _
                        & "  public.dbrd_det.dbrd_so_point, " _
                        & "  public.dbrd_det.dbrd_ret_amount, " _
                        & "  public.dbrd_det.dbrd_ar_duedate, " _
                        & "  public.dbrd_det.dbrd_ar_close_date, " _
                        & "  public.dbrd_det.dbrd_arpayd_date, " _
                        & "  public.dbrd_det.dbrd_arpay_code, " _
                        & "  public.dbrd_det.dbrd_apyad_amount, " _
                        & "  public.dbrd_det.dbrd_point, " _
                        & "  public.dbrd_det.dbrd_tot_point, " _
                        & "  public.dbrd_det.dbrd_drcr_tot " _
                        & "FROM " _
                        & "  public.dbrd_det " _
                        & "WHERE " _
                        & " dbrd_dbr_oid IS NULL "

                        .InitializeCommand()
                        .FillDataSet(ds_edit_shipment, "shipment")
                        gc_edit.DataSource = ds_edit_shipment.Tables(0)
                        gv_edit.BestFitColumns()

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
        Dim _dbr_max_point As Double

        gv_edit.UpdateCurrentRow()
        ds_edit_shipment.AcceptChanges()

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
                                            & "  public.dbr_mstr   " _
                                            & "SET  " _
                                            & "  dbr_slsprog_id = " & SetInteger(dbr_program.EditValue) & ",  " _
                                            & "  dbr_dbgcity_id = " & SetInteger(dbr_city.EditValue) & ",  " _
                                            & "  dbr_periode_id = " & SetInteger(be_dbr_periode.EditValue) & ",  " _
                                            & "  dbr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  dbr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  dbr_remarks = " & SetSetring(dbr_remarks.Text) & ",  " _
                                            & "  dbr_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "WHERE  " _
                                            & "  dbr_oid = " & SetSetring(_dbr_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from dbrd_det " _
                                            & "WHERE  " _
                                            & "  dbrd_dbr_oid = " & SetSetring(_dbr_oid_mstr) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()


                        'Dim _dbr_oid As Guid
                        '_dbr_oid = " & SetSetring(_dbr_oid_mstr.ToString) & " "

                        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1

                            'If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                            'If ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_invoice") <> 0 Then
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.dbrd_det " _
                                                & "( " _
                                                & "  dbrd_oid, " _
                                                & "  dbrd_dbr_oid, " _
                                                & "  dbrd_seq, " _
                                                & "  dbrd_en_id, " _
                                                & "  dbrd_en_desc, " _
                                                & "  dbrd_ar_oid, " _
                                                & "  dbrd_ar_eff_date, " _
                                                & "  dbrd_ar_date, " _
                                                & "  dbrd_ars_invoice, " _
                                                & "  dbrd_ar_code, " _
                                                & "  dbrd_ar_amount, " _
                                                & "  dbrd_ar_final, " _
                                                & "  dbrd_so_point, " _
                                                & "  dbrd_so_amount, " _
                                                & "  dbrd_ret_amount, " _
                                                & "  dbrd_ar_duedate, " _
                                                & "  dbrd_ar_close_date, " _
                                                & "  dbrd_arpayd_date, " _
                                                & "  dbrd_arpay_code, " _
                                                & "  dbrd_apyad_amount, " _
                                                & "  dbrd_drcr_tot, " _
                                                & "  dbrd_point, " _
                                                & "  dbrd_tot_point " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_oid").ToString) & ",  " _
                                                & SetSetring(_dbr_oid_mstr.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_en_id").ToString) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_en_desc").ToString) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_oid")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_eff_date")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_date")) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ars_invoice")) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_code")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_amount")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_final")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_so_point")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_so_amount")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ret_amount")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_duedate")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_close_date")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_arpayd_date")) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_arpay_code")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_apyad_amount")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_drcr_tot")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_point")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_tot_point")) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            '_dbr_max_point = (sum(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_point")))

                            'If dbr_close_transaction.Checked = True Then
                            '    'insert
                            '    '.Command.CommandType = CommandType.Text
                            '    .Command.CommandText = "INSERT INTO  " _
                            '                        & "  public.dbr_mstr " _
                            '                        & "( " _
                            '                        & "  public.dbr_mstr.dbr_periode_point, " _
                            '                        & "  public.dbr_mstr.dbr_close_stat, " _
                            '                        & "  public.dbr_mstr.dbr_close_date " _
                            '                        & ")  " _
                            '                        & "VALUES ( " _
                            '                        & SetBitYN(dbr_close_transaction.EditValue) & ",  " _
                            '                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                            '                        & ")"

                            '    ssqls.Add(.Command.CommandText)
                            '    .Command.ExecuteNonQuery()
                            '    '.Command.Parameters.Clear()

                            'End If

                            'End If
                        Next

                        ''.Command.CommandType = CommandType.Text
                        '.Command.CommandText = insert_log("Edit Debit Credit Memo " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_code"))
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        ''.Command.Parameters.Clear()

                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                        '        '.Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        '.Command.Parameters.Clear()
                        '    Next
                        'End If

                        .Command.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_dbr_oid_mstr, "dbr_oid")
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

        Dim _dbr_eff_date As Date = master_new.PGSqlConn.CekTanggal
        Dim _dbr_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_en_id")
        'Dim _gcald_det_status As String = func_data.get_gcald_det_status(_dbr_en_id, "gcald_ar", _dbr_eff_date)

        'If _gcald_det_status = "" Then
        '    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _dbr_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'ElseIf _gcald_det_status.ToUpper = "Y" Then
        '    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _dbr_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Harus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub be_dbr_dbg_name_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_dbr_dbg_name.ButtonClick
        Dim frm As New FDBGroupSearch
        frm.set_win(Me)
        'frm._en_id = so_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_dbr_periode_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_dbr_periode.ButtonClick
        Dim frm As New FPSPeriodeSearch
        frm.set_win(Me)
        'frm._en_id = so_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub sb_retrieve_shipment_item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_receive_item.Click

        Dim _dbg_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            _dbg_code = _dbg_code + "'" + SetSetring("dbr_dbg_name.EditValue") + "',"
        Next

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT DISTINCT public.dbg_group.dbg_code, " _
                        & "public.dbg_group.dbg_name, " _
                        & "public.dbgd_det.dbgd_ptnr_id, " _
                        & "public.ptnr_mstr.ptnr_name, " _
                        & "public.dbgd_det.dbgd_en_id, " _
                        & "public.en_mstr.en_desc, " _
                        & "public.ar_mstr.ar_oid, " _
                        & "public.ar_mstr.ar_date, " _
                        & "public.ar_mstr.ar_eff_date, " _
                        & "public.ar_mstr.ar_code, " _
                        & "CASE WHEN public.arpayd_det.arpayd_ret_admin = 'Y' THEN '0' ELSE SUM(public.ars_ship.ars_invoice) END as allqty, " _
                        & "public.ar_mstr.ar_total_final, " _
                        & "CASE WHEN (ar_total_final - arpayd_amount) <> '0' THEN arpayd_amount ELSE ar_total_final END AS so_amount, " _
                        & "CASE WHEN public.arpayd_det.arpayd_ret_admin = 'Y' THEN public.arpayd_det.arpayd_amount ELSE '0' END as ret_amount,  " _
                        & "CASE WHEN (ar_total_final - arpayd_amount) <> '0' THEN FLOOR (arpayd_amount / 1000000) ELSE FLOOR ( ar_total_final / 1000000) END AS point_so, " _
                        & "CASE WHEN public.arpayd_det.arpayd_ret_admin = 'Y' THEN public.arpayd_det.arpayd_amount * - 1 ELSE public.arpayd_det.arpayd_amount END as aramount, " _
                        & "public.ar_mstr.ar_due_date, " _
                        & "public.arpay_payment.arpay_date,  " _
                        & "public.ar_mstr.ar_close_date,  " _
                        & "public.arpay_payment.arpay_code,  " _
                        & "CASE WHEN public.arpayd_det.arpayd_ret_admin = 'Y' THEN public.arpayd_det.arpayd_amount * - 1 ELSE public.arpayd_det.arpayd_amount END as amount,  " _
                        & "CASE WHEN public.ar_mstr.ar_due_date >= public.ar_mstr.ar_close_date THEN FLOOR(public.arpayd_det.arpayd_amount / 1000000) ELSE '0' END AS point, " _
                        & "FLOOR(sum(CASE WHEN public.arpayd_det.arpayd_ret_admin = 'Y' THEN public.arpayd_det.arpayd_amount * - 1 ELSE public.arpayd_det.arpayd_amount END) over(order by public.ar_mstr.ar_date, ar_code asc rows between unbounded preceding and current row)) as total_ar, " _
                        & "FLOOR(sum(CASE WHEN public.ar_mstr.ar_due_date >= public.ar_mstr.ar_close_date THEN public.arpayd_det.arpayd_amount END / 1000000) over(order by public.ar_mstr.ar_date, ar_code asc rows between unbounded preceding and current row)) as total_point, " _
                        & "(SELECT  " _
                        & "SUM(public.ar_mstr.ar_total_final) AS ar_total_final " _
                        & "FROM " _
                        & "public.dbg_group " _
                        & "INNER JOIN public.dbgd_det ON (public.dbg_group.dbg_oid = public.dbgd_det.dbgd_dbg_oid) " _
                        & "INNER JOIN public.ar_mstr ON (public.dbgd_det.dbgd_ptnr_id = public.ar_mstr.ar_bill_to) " _
                        & "WHERE " _
                        & "public.dbg_group.dbg_oid = " + SetSetring(_be_dbr_dbg_name_oid) + " AND " _
                        & "ar_status ISNULL  " _
                        & "GROUP BY " _
                        & "public.dbg_group.dbg_code, " _
                        & "public.dbg_group.dbg_oid, " _
                        & "public.dbg_group.dbg_name) as total_all_drcr_amount," _
                        & "public.arpayd_det.arpayd_ret_admin " _
                        & "FROM public.dbg_group " _
                        & "INNER JOIN public.dbgd_det ON (public.dbg_group.dbg_oid =  public.dbgd_det.dbgd_dbg_oid) " _
                        & "INNER JOIN public.ptnr_mstr ON (public.dbgd_det.dbgd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                        & "INNER JOIN public.ar_mstr ON (public.ptnr_mstr.ptnr_id = public.ar_mstr.ar_bill_to) " _
                        & "INNER JOIN public.ars_ship ON (public.ar_mstr.ar_oid = public.ars_ship.ars_ar_oid) " _
                        & "INNER JOIN public.en_mstr ON (public.dbgd_det.dbgd_en_id = public.en_mstr.en_id) " _
                        & "INNER JOIN public.arpayd_det ON (public.ar_mstr.ar_oid = public.arpayd_det.arpayd_ar_oid) " _
                        & "INNER JOIN public.arpay_payment ON (public.arpay_payment.arpay_oid = public.arpayd_det.arpayd_arpay_oid)" _
                        & "WHERE public.dbg_group.dbg_oid = " + SetSetring(_be_dbr_dbg_name_oid) + " AND " _
                        & "public.ar_mstr.ar_status = 'c' " _
                        & "AND public.ar_mstr.ar_date BETWEEN " + SetDate(dbr_start_date.DateTime.Date) + " AND " + SetDate(dbr_end_date.DateTime.Date) + " " _
                        & "GROUP BY public.dbg_group.dbg_code, " _
                        & "public.dbg_group.dbg_name, " _
                        & "public.dbgd_det.dbgd_ptnr_id, " _
                        & "public.ptnr_mstr.ptnr_name, " _
                        & "public.dbgd_det.dbgd_en_id, " _
                        & "public.en_mstr.en_desc, " _
                        & "public.ar_mstr.ar_oid, " _
                        & "public.ar_mstr.ar_date, " _
                        & "public.ar_mstr.ar_eff_date, " _
                        & "public.ar_mstr.ar_code, " _
                        & "public.arpay_payment.arpay_total_final, " _
                        & "public.ar_mstr.ar_total_final, " _
                        & "public.arpay_payment.arpay_date, " _
                        & "public.arpay_payment.arpay_code,  " _
                        & "point, " _
                        & "public.ar_mstr.ar_due_date, " _
                        & "public.ar_mstr.ar_close_date,  " _
                        & "public.arpayd_det.arpayd_ret_admin, " _
                        & "public.arpayd_det.arpayd_amount " _
                        & "ORDER BY public.ar_mstr.ar_date, ar_code ASC"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "dbrd_det")
                End With
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_shipment.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            'If ds_bantu.Tables(0).Rows(i).Item("qty_open") <> 0 Then
            _dtrow = ds_edit_shipment.Tables(0).NewRow
            _dtrow("dbrd_oid") = Guid.NewGuid.ToString
            _dtrow("dbrd_en_id") = ds_bantu.Tables(0).Rows(i).Item("dbgd_ptnr_id")
            _dtrow("dbrd_en_desc") = ds_bantu.Tables(0).Rows(i).Item("en_desc")
            '_dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
            '_dtrow("dbrd_dbr_oid") = ds_bantu.Tables(0).Rows(i).Item("dbrd_dbr_oid")
            _dtrow("dbrd_en_id") = ds_bantu.Tables(0).Rows(i).Item("dbgd_en_id")
            _dtrow("dbrd_ar_oid") = ds_bantu.Tables(0).Rows(i).Item("ar_oid")
            _dtrow("dbrd_ar_date") = ds_bantu.Tables(0).Rows(i).Item("ar_date")
            _dtrow("dbrd_ar_eff_date") = ds_bantu.Tables(0).Rows(i).Item("ar_eff_date")
            _dtrow("dbrd_ars_invoice") = ds_bantu.Tables(0).Rows(i).Item("allqty")
            _dtrow("dbrd_ar_code") = ds_bantu.Tables(0).Rows(i).Item("ar_code")
            _dtrow("dbrd_ar_amount") = ds_bantu.Tables(0).Rows(i).Item("amount")
            _dtrow("dbrd_so_amount") = ds_bantu.Tables(0).Rows(i).Item("so_amount")
            _dtrow("dbrd_ret_amount") = ds_bantu.Tables(0).Rows(i).Item("ret_amount")
            _dtrow("dbrd_so_point") = ds_bantu.Tables(0).Rows(i).Item("point_so")
            _dtrow("dbrd_ar_final") = ds_bantu.Tables(0).Rows(i).Item("ar_total_final")
            _dtrow("dbrd_ar_duedate") = ds_bantu.Tables(0).Rows(i).Item("ar_due_date")
            _dtrow("dbrd_ar_close_date") = ds_bantu.Tables(0).Rows(i).Item("ar_close_date")
            _dtrow("dbrd_arpayd_date") = ds_bantu.Tables(0).Rows(i).Item("arpay_date")
            _dtrow("dbrd_arpay_code") = ds_bantu.Tables(0).Rows(i).Item("arpay_code")
            _dtrow("dbrd_apyad_amount") = ds_bantu.Tables(0).Rows(i).Item("total_ar")
            _dtrow("dbrd_point") = ds_bantu.Tables(0).Rows(i).Item("point")
            _dtrow("dbrd_tot_point") = ds_bantu.Tables(0).Rows(i).Item("total_point")
            _dtrow("dbrd_drcr_tot") = ds_bantu.Tables(0).Rows(i).Item("total_all_drcr_amount")
            ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
            'End I
        Next

        'Dim ssql As String
        'ssql = "SELECT  " _
        '    & "  soship_date " _
        '    & "FROM  " _
        '    & "  public.soshipd_det " _
        '    & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
        '    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
        '    & "  inner join so_mstr on so_oid = sod_so_oid " _
        '    & "  inner join pt_mstr on pt_id = sod_pt_id " _
        '    & "  where coalesce(soshipd_close_line,'N') = 'N' " _
        '    & "  and so_code in (" + _dbg_code + ") and soship_is_shipment='Y'   " _
        '    & "  order by soship_date desc"

        Dim dt As New DataTable
        'dt = master_new.PGSqlConn.GetTableData(ssql)


        'dbr_eff_date.DateTime = dt.Rows(0).Item("soship_date")
        '(i) disini pasti line yang terakhir

        ds_edit_shipment.Tables(0).AcceptChanges()

        gv_edit.BestFitColumns()

    End Sub


    Public Overrides Function insert() As Boolean
        Dim _dbr_oid As Guid
        _dbr_oid = Guid.NewGuid

        Dim _dbr_code As String
        'Dim _dbr_amount As Double = 0
        'Dim _prepaid As Double = 0
        Dim i As Integer
        Dim ssqls As New ArrayList
        'Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        '_dbr_code = func_coll.get_transaction_number("PCS", dbr_en_id.GetColumnValue("en_code"), "dbr_mstr", "dbr_code")

        _dbr_code = GetNewNumberYM("dbr_mstr", "dbr_code", 5, "DBR" & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        'gc_edit_so.EmbeddedNavigator.Buttons.DoClick(gc_edit_so.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit_shipment.Tables(0).AcceptChanges()

        'For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
        '    _dbr_amount = _dbr_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        'Next
        '_dbr_terbilang = func_bill.TERBILANG_FIX(_ar_amount)
        'Dim _code As String

        '_dbr_code = GetNewNumberYM("arp_print", "arp_code", 5, "ARP" & dbr_en_id.GetColumnValue("en_code") _
        '& CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        'gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        'ds_edit.Tables(0).AcceptChanges()

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text

                        '         "  public.dbr_mstr.dbr_oid, " _
                        '& "  public.dbr_mstr.dbr_code, " _
                        '& "  public.dbg_group.dbg_name, " _
                        '& "  public.dbr_mstr.dbr_date, " _
                        '& "  public.code_mstr.code_name, " _
                        '& "  public.psperiode_mstr&.periode_code, " _
                        '& "  public.sls_program.sls_desc, " _
                        '& "  public.dbr_mstr.dbr_slsprog_code, " _
                        '& "  public.sls_program.sls_code, " _

                        '& "  public.dbr_mstr.dbr_start_date, " _
                        '& "  public.dbr_mstr.dbr_end_date " _

                        .Command.CommandText = "INSERT INTO  " _
                                            & "  dbr_mstr " _
                                            & "( " _
                                            & "  dbr_oid, " _
                                            & "  dbr_code, " _
                                            & "  dbr_slsprog_id, " _
                                            & "  dbr_dbg_oid, " _
                                            & "  dbr_dbgcity_id, " _
                                            & "  dbr_periode_id, " _
                                            & "  dbr_date, " _
                                            & "  dbr_add_by, " _
                                            & "  dbr_add_date, " _
                                            & "  dbr_start_date, " _
                                            & "  dbr_end_date, " _
                                            & "  dbr_remarks " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_dbr_oid.ToString) & ",  " _
                                            & SetSetring(_dbr_code) & ",  " _
                                            & SetInteger(dbr_program.EditValue) & ",  " _
                                            & SetSetring(_be_dbr_dbg_name_oid) & ",  " _
                                            & SetInteger(dbr_city.EditValue) & ",  " _
                                            & SetInteger(_be_dbr_periode_code) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(dbr_start_date.DateTime) & ",  " _
                                            & SetDate(dbr_end_date.DateTime) & ",  " _
                                            & SetSetring(dbr_remarks.Text) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        Update()

                        'Untuk Insert Data List shipment
                        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
                            'If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                            'If ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_invoice") <> 0 Then
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.dbrd_det " _
                                                & "( " _
                                                & "  dbrd_oid, " _
                                                & "  dbrd_dbr_oid, " _
                                                & "  dbrd_seq, " _
                                                & "  dbrd_en_id, " _
                                                & "  dbrd_en_desc, " _
                                                & "  dbrd_ar_oid, " _
                                                & "  dbrd_ar_eff_date, " _
                                                & "  dbrd_ar_date, " _
                                                & "  dbrd_ars_invoice, " _
                                                & "  dbrd_ar_code, " _
                                                & "  dbrd_ar_amount, " _
                                                & "  dbrd_ar_final, " _
                                                & "  dbrd_so_point, " _
                                                & "  dbrd_so_amount, " _
                                                & "  dbrd_ret_amount, " _
                                                & "  dbrd_ar_duedate, " _
                                                & "  dbrd_ar_close_date, " _
                                                & "  dbrd_arpayd_date, " _
                                                & "  dbrd_arpay_code, " _
                                                & "  dbrd_apyad_amount, " _
                                                & "  dbrd_drcr_tot, " _
                                                & "  dbrd_point, " _
                                                & "  dbrd_tot_point " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_oid").ToString) & ",  " _
                                                & SetSetring(_dbr_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_en_id").ToString) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_en_desc").ToString) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_oid")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_eff_date")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_date")) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ars_invoice")) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_code")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_amount")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_final")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_so_point")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_so_amount")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ret_amount")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_duedate")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_ar_close_date")) & ",  " _
                                                & SetDate(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_arpayd_date")) & ",  " _
                                                & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_arpay_code")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_apyad_amount")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_drcr_tot")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_point")) & ",  " _
                                                & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("dbrd_tot_point")) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'End If
                        Next

                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                        '        '.Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        '.Command.Parameters.Clear()
                        '    Next
                        'End If

                        .Command.Commit()
                        after_success()
                        set_row(_dbr_oid.ToString, "dbr_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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


    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        '_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_en_id")
        _type = 13
        _table = "dbr_mstr"
        _initial = "dbr"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT DISTINCT  " _
                & "  public.dbr_mstr.dbr_code, " _
                & "  public.dbr_mstr.dbr_date, " _
                & "  public.dbr_mstr.dbr_dbgcity_id, " _
                & "  public.dbr_mstr.dbr_start_date, " _
                & "  public.dbr_mstr.dbr_end_date, " _
                & "  public.dbr_mstr.dbr_remarks, " _
                & "  public.dbr_mstr.dbr_dbg_name, " _
                & "  public.dbr_mstr.dbr_periode_id, " _
                & "  public.code_mstr.code_name, " _
                & "  public.dbg_group.dbg_name, " _
                & "  public.dbrd_det.dbrd_en_id, " _
                & "  public.dbrd_det.dbrd_en_desc, " _
                & "  public.dbrd_det.dbrd_ar_oid, " _
                & "  public.dbrd_det.dbrd_ar_eff_date, " _
                & "  public.dbrd_det.dbrd_ar_date, " _
                & "  public.dbrd_det.dbrd_ars_invoice, " _
                & "  public.dbrd_det.dbrd_ar_code, " _
                & "  public.dbrd_det.dbrd_ar_amount, " _
                & "  public.dbrd_det.dbrd_ret_amount, " _
                & "  public.dbrd_det.dbrd_ar_final, " _
                & "  public.dbrd_det.dbrd_ar_duedate, " _
                & "  public.dbrd_det.dbrd_arpayd_date, " _
                & "  public.dbrd_det.dbrd_apyad_amount, " _
                & "  public.dbrd_det.dbrd_point, " _
                & "  public.dbrd_det.dbrd_tot_point, " _
                & "  public.sls_program.sls_name, " _
                & "  public.psperiode_mstr.periode_code, " _
                & "  public.psperiode_mstr.periode_start_date, " _
                & "  public.psperiode_mstr.periode_end_date, " _
                & "  public.dbrd_det.dbrd_so_amount, " _
                & "  sum(public.dbrd_det.dbrd_so_amount - public.dbrd_det.dbrd_ret_amount) AS total_so_amount, " _
                & "  sum(public.dbrd_det.dbrd_ar_amount) AS total_ar_amount, " _
                & "  sum(public.dbrd_det.dbrd_ret_amount) AS total_ret_amount, " _
                & "  CASE  " _
                & "    WHEN sum(public.dbrd_det.dbrd_ar_amount) < 0 " _
                & "      THEN (public.dbrd_det.dbrd_so_amount - (public.dbrd_det.dbrd_ar_amount *-1)) " _
                & "    ELSE  sum(public.dbrd_det.dbrd_so_amount - public.dbrd_det.dbrd_ar_amount) " _
                & "  END AS total_drcr_amount, " _
                & "  public.dbrd_det.dbrd_drcr_tot, " _
                & "  public.dbrd_det.dbrd_so_point, " _
                & "  public.dbrd_det.dbrd_ar_close_date " _
                & "FROM " _
                & "  public.dbr_mstr " _
                & "  INNER JOIN public.code_mstr ON (public.dbr_mstr.dbr_dbgcity_id = public.code_mstr.code_id) " _
                & "  INNER JOIN public.dbg_group ON (public.dbr_mstr.dbr_dbg_oid = public.dbg_group.dbg_oid) " _
                & "  INNER JOIN public.dbrd_det ON (public.dbr_mstr.dbr_oid = public.dbrd_det.dbrd_dbr_oid) " _
                & "  INNER JOIN public.sls_program ON (public.dbr_mstr.dbr_slsprog_id = public.sls_program.sls_id) " _
                & "  INNER JOIN public.psperiode_mstr ON (public.dbr_mstr.dbr_periode_id = public.psperiode_mstr.periode_id) " _
                & "and dbr_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_code") + "'" _
                & "GROUP BY " _
                & "  public.dbr_mstr.dbr_code, " _
                & "  public.dbr_mstr.dbr_date, " _
                & "  public.dbr_mstr.dbr_dbgcity_id, " _
                & "  public.dbr_mstr.dbr_start_date, " _
                & "  public.dbr_mstr.dbr_end_date, " _
                & "  public.dbr_mstr.dbr_remarks, " _
                & "  public.dbr_mstr.dbr_dbg_name, " _
                & "  public.dbr_mstr.dbr_periode_id, " _
                & "  public.code_mstr.code_name, " _
                & "  public.dbg_group.dbg_name, " _
                & "  public.dbrd_det.dbrd_en_id, " _
                & "  public.dbrd_det.dbrd_en_desc, " _
                & "  public.dbrd_det.dbrd_ar_oid, " _
                & "  public.dbrd_det.dbrd_ar_eff_date, " _
                & "  public.dbrd_det.dbrd_ar_date, " _
                & "  public.dbrd_det.dbrd_ars_invoice, " _
                & "  public.dbrd_det.dbrd_ar_code, " _
                & "  public.dbrd_det.dbrd_ar_amount, " _
                & "  public.dbrd_det.dbrd_ret_amount, " _
                & "  public.dbrd_det.dbrd_ar_final, " _
                & "  public.dbrd_det.dbrd_ar_duedate, " _
                & "  public.dbrd_det.dbrd_arpayd_date, " _
                & "  public.dbrd_det.dbrd_apyad_amount, " _
                & "  public.dbrd_det.dbrd_point, " _
                & "  public.dbrd_det.dbrd_tot_point, " _
                & "  public.sls_program.sls_name, " _
                & "  public.psperiode_mstr.periode_code, " _
                & "  public.psperiode_mstr.periode_start_date, " _
                & "  public.psperiode_mstr.periode_end_date, " _
                & "  public.dbrd_det.dbrd_so_amount, " _
                & "  public.dbrd_det.dbrd_drcr_tot, " _
                & "  public.dbrd_det.dbrd_so_point, " _
                & "  public.dbrd_det.dbrd_ar_close_date " _
                & "ORDER BY " _
                & "  public.dbrd_det.dbrd_ar_date, " _
                & "  public.dbrd_det.dbrd_ar_code "


        'If ce_dbr_doc.Checked = True Then
        '    Dim frm As New frmPrintDialog
        '    frm._ssql = _sql
        '    frm._report = "XRPackingSheetPrint"
        '    'frm._report = "XRPackingSheetLabel"
        '    frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_code")
        '    frm.ShowDialog()
        'Else

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRPointRewardReports"
        'frm._report = "XRPackingSheetLabel"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_code")
        frm.ShowDialog()
        'End If

    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String

            Try
                ds.Tables("detail_shipment").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                            & "  public.dbrd_det.dbrd_oid, " _
                            & "  public.dbrd_det.dbrd_dbr_oid, " _
                            & "  public.dbrd_det.dbrd_seq, " _
                            & "  public.dbrd_det.dbrd_en_id, " _
                            & "  public.dbrd_det.dbrd_en_desc, " _
                            & "  public.dbrd_det.dbrd_ar_oid, " _
                            & "  public.dbrd_det.dbrd_ar_eff_date, " _
                            & "  public.dbrd_det.dbrd_ar_date, " _
                            & "  public.dbrd_det.dbrd_ars_invoice, " _
                            & "  public.dbrd_det.dbrd_ar_code, " _
                            & "  public.dbrd_det.dbrd_ar_amount, " _
                            & "  public.dbrd_det.dbrd_ar_final, " _
                            & "  public.dbrd_det.dbrd_so_amount, " _
                            & "  public.dbrd_det.dbrd_so_point, " _
                            & "  public.dbrd_det.dbrd_ret_amount, " _
                            & "  public.dbrd_det.dbrd_ar_duedate, " _
                            & "  public.dbrd_det.dbrd_ar_close_date, " _
                            & "  public.dbrd_det.dbrd_arpayd_date, " _
                            & "  public.dbrd_det.dbrd_arpay_code, " _
                            & "  public.dbrd_det.dbrd_apyad_amount, " _
                            & "  public.dbrd_det.dbrd_point, " _
                            & "  public.dbrd_det.dbrd_tot_point, " _
                            & "  public.dbrd_det.dbrd_drcr_tot " _
                            & "FROM " _
                            & "  public.dbrd_det " _
                            & " where dbrd_dbr_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_oid").ToString & "'" _
                            & "ORDER BY " _
                            & "  public.dbrd_det.dbrd_ar_date, " _
                            & "  public.dbrd_det.dbrd_ar_code, " _
                            & "  public.dbrd_det.dbrd_tot_point ASC"

            load_data_detail(sql, gc_detail, "detail_shipment")



        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    'Private Sub par_so_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_so.ButtonClick
    '    Try

    '        Dim frm As New FSalesOrderSearch
    '        frm.set_win(Me)
    '        frm._obj = par_so
    '        frm._ptnr_id = _par_cus_id
    '        frm.type_form = True
    '        frm.ShowDialog()


    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'Private Sub par_cus_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_cus.ButtonClick
    '    Try

    '        Dim frm As New FPartnerSearch
    '        frm.set_win(Me)
    '        frm._obj = par_cus
    '        frm.type_form = True
    '        frm.ShowDialog()

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'Private Sub UpdateTerbilangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateTerbilangToolStripMenuItem.Click
    '    Try
    '        Dim ssql As String
    '        Dim _terbilang As String

    '        _terbilang = func_bill.TERBILANG_FIX(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_amount"))
    '        ssql = "update dbr_mstr set dbr_terbilang=" & SetSetring(_terbilang) & " where dbr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dbr_oid") & "'"

    '        Dim ssqls As New ArrayList
    '        ssqls.Add(ssql)

    '        If master_new.PGSqlConn.status_sync = True Then
    '            If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then

    '                Exit Sub
    '            End If
    '            ssqls.Clear()
    '        Else
    '            If DbRunTran(ssqls, "") = False Then

    '                Exit Sub
    '            End If
    '            ssqls.Clear()
    '        End If
    '        Box("Update success, please refresh data")

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'Private Sub gv_edit_so_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.RowCountChanged
    '    Try
    '        If gv_edit_so.RowCount >= 1 Then
    '            gc_edit_so.EmbeddedNavigator.Buttons.Append.Visible = False
    '            gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = True
    '        Else
    '            gc_edit_so.EmbeddedNavigator.Buttons.Append.Visible = True
    '            gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = False
    '        End If
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    Private Sub xtc_detail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xtc_detail.Click

    End Sub
End Class
