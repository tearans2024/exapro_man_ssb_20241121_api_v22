Imports master_new.ModFunction

Public Class FDRCRMemoReport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARReportByAging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_so1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_so1.DataTable1)
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub format_grid()


        add_column_copy(gv_master_header, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Effective Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master_header, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master_header, "Exc. Rate", "ar_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master_header, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Due Date", "ar_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master_header, "Expected Date", "ar_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master_header, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Taxable", "ar_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Tax Include", "ar_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Tax Type", "ar_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Type", "ar_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Prepayment", "ar_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master_header, "Prepayment IDR", "ar_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master_header, "Account Code Prepayment", "ar_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Account Name Prepayment", "ar_code_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Remarks", "ar_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Total", "ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master_header, "Shipping Charges", "ar_shipping_charges", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master_header, "Total Final", "ar_total_final", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        add_column_copy(gv_master_header, "Total Payment", "ar_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master_header, "Outstanding Payment", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master_header, "Total IDR", "ar_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master_header, "Total Payment IDR", "ar_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master_header, "Outstanding Payment IDR", "ar_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master_header, "Status", "ar_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "User Create", "ar_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Date Create", "ar_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master_header, "User Update", "ar_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_header, "Date Update", "ar_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")



        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "AR Eff. Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Is Personal Sales", "ptnr_is_ps", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Credit Terms", "term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Exchange Rate", "ar_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Remarks", "ar_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Shipment / Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Shipment / Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Is Shipment", "soship_is_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Taxable", "ars_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "ars_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "tax_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Invoice Qty", "ars_invoice", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Invoice Price", "ars_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Discount Value", "ars_so_disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "DPP", "dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "PPN", "ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Netto", "netto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Netto Ext.", "netto_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")



    End Sub

    Public Overrides Sub load_cb()
        'init_le(en_id, "en_mstr")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_cu_mstr())
        'cu_id.Properties.DataSource = dt_bantu
        'cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        'cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        'cu_id.ItemIndex = 0

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.CustomCommand
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            With objload
                                .SQL = "SELECT  " _
                                    & "  public.ar_mstr.ar_oid, " _
                                    & "  public.ar_mstr.ar_dom_id, " _
                                    & "  public.ar_mstr.ar_en_id, " _
                                    & "  public.ar_mstr.ar_add_by, " _
                                    & "  public.ar_mstr.ar_add_date, " _
                                    & "  public.ar_mstr.ar_upd_by, " _
                                    & "  public.ar_mstr.ar_upd_date, " _
                                    & "  public.ar_mstr.ar_code, " _
                                    & "  public.ar_mstr.ar_date, " _
                                    & "  public.ar_mstr.ar_bill_to, " _
                                    & "  public.ar_mstr.ar_cu_id, " _
                                    & "  public.ar_mstr.ar_exc_rate, " _
                                    & "  public.ar_mstr.ar_credit_term, " _
                                    & "  public.ar_mstr.ar_eff_date, " _
                                    & "  public.ar_mstr.ar_disc_date, " _
                                    & "  public.ar_mstr.ar_expt_date, " _
                                    & "  public.ar_mstr.ar_ac_id, " _
                                    & "  public.ar_mstr.ar_sb_id, " _
                                    & "  public.ar_mstr.ar_cc_id, " _
                                    & "  public.ar_mstr.ar_type, " _
                                    & "  public.ar_mstr.ar_taxable, " _
                                    & "  public.ar_mstr.ar_tax_inc, " _
                                    & "  public.ar_mstr.ar_tax_class_id,ar_shipping_charges,ar_total_final, " _
                                    & "  public.ar_mstr.ar_pay_prepaid, " _
                                    & "  public.ar_mstr.ar_pay_prepaid * ar_exc_rate as ar_pay_prepaid_idr, " _
                                    & "  public.ar_mstr.ar_ac_prepaid, " _
                                    & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                                    & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                                    & "  public.ar_mstr.ar_amount, " _
                                    & "  public.ar_mstr.ar_pay_amount, " _
                                    & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                                    & "  public.ar_mstr.ar_amount * ar_exc_rate as ar_amount_idr, " _
                                    & "  public.ar_mstr.ar_pay_amount * ar_exc_rate as ar_pay_amount_idr, " _
                                    & "  (public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount) * ar_exc_rate as ar_outstanding_idr, " _
                                    & "  public.ar_mstr.ar_remarks, " _
                                    & "  public.ar_mstr.ar_status, " _
                                    & "  public.ar_mstr.ar_dt, " _
                                    & "  public.ar_mstr.ar_due_date, " _
                                    & "  public.en_mstr.en_desc, " _
                                    & "  public.ptnr_mstr.ptnr_name, " _
                                    & "  public.cu_mstr.cu_name, " _
                                    & "  credit_term_mstr.code_name as credit_terms_name, " _
                                    & "  public.ac_mstr.ac_code, " _
                                    & "  public.ac_mstr.ac_name, " _
                                    & "  public.sb_mstr.sb_desc, " _
                                    & "  public.cc_mstr.cc_desc, " _
                                    & "  public.ar_mstr.ar_bk_id, " _
                                    & "  public.bk_mstr.bk_name, " _
                                    & "  coalesce(ar_ppn_type,'N') as ar_ppn_type, " _
                                    & "  ar_type.code_name as ar_type_name, " _
                                    & "  taxclass_mstr.code_name as taxclass_name " _
                                    & "FROM " _
                                    & "  public.ar_mstr " _
                                    & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                                    & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                                    & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                                    & "  INNER JOIN public.code_mstr credit_term_mstr ON (public.ar_mstr.ar_credit_term = credit_term_mstr.code_id) " _
                                    & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                                    & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ar_mstr.ar_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                                    & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                                    & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                                    & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                                    & "  INNER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = bk_mstr.bk_id) " _
                                    & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.ar_mstr.ar_tax_class_id = taxclass_mstr.code_id) " _
                                    & " where ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                    & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                    & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                                .InitializeCommand()
                                .FillDataSet(ds, "data_ar")
                                gc_master_header.DataSource = ds.Tables("data_ar")
                                gv_master_header.BestFitColumns()
                            End With
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            With objload
                                .SQL = "SELECT  " _
                                    & "  en_desc, " _
                                    & "  ptnr_mstr.ptnr_name, " _
                                    & "  ar_code, " _
                                    & "  ar_date, " _
                                    & "  ar_eff_date, " _
                                    & "  cu_name, " _
                                    & "  term_mstr.code_name as term_name, " _
                                    & "  ar_amount, " _
                                    & "  ar_pay_amount, " _
                                    & "  ar_amount - ar_pay_amount as ar_outstanding, " _
                                    & "  ar_exc_rate, " _
                                    & "  ar_remarks, " _
                                    & "  so_code, " _
                                    & "  so_date, " _
                                    & "  soship_code, " _
                                    & "  soship_date,ptnr_mstr.ptnr_is_ps, " _
                                    & "  soship_is_shipment, " _
                                    & "  pt_code, " _
                                    & "  pt_desc1, " _
                                    & "  pt_desc2, " _
                                    & "  ars_taxable, " _
                                    & "  ars_tax_inc, " _
                                    & "  tax_mstr.code_name as tax_name, " _
                                    & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                                    & "  sod_qty, " _
                                    & "  ars_invoice, " _
                                    & "  ars_invoice_price, " _
                                    & "  ars_so_disc_value, " _
                                    & "  ars_invoice_price - ars_so_disc_value as dpp, " _
                                    & "  case ars_tax_inc " _
                                    & "  when 'N' then (ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/100) as numeric(26,8))  " _
                                    & "  when 'Y' then (ars_invoice_price - ars_so_disc_value) *  cast((taxr_rate/110)  as numeric(26,8))  " _
                                    & "  end as ppn, " _
                                    & "  case ars_tax_inc " _
                                    & "  when 'N' then (ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/100) as numeric(26,8))) " _
                                    & "  when 'Y' then (ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/110)  as numeric(26,8))) " _
                                    & "  end as netto, " _
                                    & "  case ars_tax_inc " _
                                    & "  when 'N' then ars_invoice * ((ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/100) as numeric(26,8)))) " _
                                    & "  when 'Y' then ars_invoice * ((ars_invoice_price - ars_so_disc_value) + ((ars_invoice_price - ars_so_disc_value) * cast((taxr_rate/110)  as numeric(26,8)))) " _
                                    & "  end as netto_ext " _
                                    & "FROM  " _
                                    & "  public.ar_mstr " _
                                    & "  inner join ars_ship on ars_ar_oid = ar_oid " _
                                    & "  inner join en_mstr on en_id = ar_en_id " _
                                    & "  inner join cu_mstr on cu_id = ar_cu_id " _
                                    & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                                    & "  inner join code_mstr term_mstr on term_mstr.code_id = ar_credit_term " _
                                    & "  inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
                                    & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                                    & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                                    & "  inner join so_mstr on so_oid = sod_so_oid " _
                                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                                    & "  inner join code_mstr tax_mstr on tax_mstr.code_id = ars_tax_class_id " _
                                    & "  inner join taxr_mstr on taxr_tax_class = ars_tax_class_id " _
                                    & "  inner join code_mstr ttype_mstr on ttype_mstr.code_id = taxr_tax_type " _
                                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = so_sales_person " _
                                    & "  where ttype_mstr.code_code ~~* 'PPN'" _
                                    & "  and ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                    & "  and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                    & "  and ar_en_id in (select user_en_id from tconfuserentity " _
                                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                                .InitializeCommand()
                                .FillDataSet(ds, "data_ar")
                                gc_detail.DataSource = ds.Tables("data_ar")
                                gv_detail.BestFitColumns()
                            End With
                        End If
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub


    'Public Overrides Sub load_data_many(ByVal arg As Boolean)
    '    Cursor = Cursors.WaitCursor
    '    If arg <> False Then
    '        '================================================================
    '        Try
    '            ds = New DataSet
    '            Using objload As New master_new.CustomCommand
    '                With objload
    '                    If xtc_master.SelectedTabPageIndex = 0 Then
    '                        load_ar(objload)
    '                    End If
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If
    '    Cursor = Cursors.Arrow
    'End Sub

    'Private Sub load_ar(ByVal par_obj As Object)
    '    pgc_ar.DataSource = Nothing
    '    pgc_ar.DataMember = Nothing

    '    With par_obj
    '        .SQL = set_sql()

    '        .InitializeCommand()
    '        .FillDataSet(ds, "data_ar")
    '        pgc_ar.DataSource = ds.Tables("data_ar")
    '        pgc_ar.BestFit()
    '    End With
    'End Sub

    'Private Function set_sql() As String
    '    set_sql = ""
    '    If RBFilterCU.Checked Then
    '        If CBOutstandingType.Text = "Before Outstanding Date" Then
    '            set_sql = "SELECT en_desc, " _
    '               & "  b.ptnr_name,ar_code,ar_date, " _
    '               & "  c.code_name AS ar_type_name, " _
    '               & "  coalesce(a.ar_amount,0) + coalesce(ar_shipping_charges,0) as ar_amount, " _
    '               & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
    '               & "  coalesce(a.ar_amount,0) + coalesce(ar_shipping_charges,0) -coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
    '               & " " _
    '               & "FROM " _
    '               & "  public.ar_mstr a " _
    '               & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '               & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '               & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '               & "WHERE " _
    '               & "  (lower(a.ar_status) <> 'c' OR  " _
    '               & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_due_date<=" _
    '               & SetDateNTime00(outstanding_date.Text)




    '        ElseIf CBOutstandingType.Text = "After Outstanding Date" Then
    '            set_sql = "SELECT  en_desc," _
    '               & "  b.ptnr_name,ar_code,ar_date, " _
    '               & "  c.code_name AS ar_type_name, " _
    '               & "  coalesce(a.ar_amount,0)+ coalesce(ar_shipping_charges,0) as ar_amount, " _
    '               & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
    '               & "  coalesce(a.ar_amount,0)+ coalesce(ar_shipping_charges,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
    '               & " " _
    '               & "FROM " _
    '               & "  public.ar_mstr a " _
    '               & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '               & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '               & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '               & "WHERE " _
    '               & "  (lower(a.ar_status) <> 'c' OR  " _
    '               & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_due_date>=" _
    '               & SetDateNTime00(outstanding_date.Text)



    '        ElseIf CBOutstandingType.Text = "All Data" Then

    '            set_sql = "SELECT en_desc, " _
    '                & "  b.ptnr_name,ar_code,ar_date, " _
    '                & "  c.code_name AS ar_type_name, " _
    '                & "  coalesce(a.ar_amount,0) + coalesce(ar_shipping_charges,0) as ar_amount, " _
    '                & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
    '                & "  coalesce(a.ar_amount,0) + coalesce(ar_shipping_charges,0) -coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
    '                & " " _
    '                & "FROM " _
    '                & "  public.ar_mstr a " _
    '                & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                & "WHERE " _
    '                & "  (lower(a.ar_status) <> 'c' OR  " _
    '                & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue)



    '        ElseIf CBOutstandingType.Text = "Between Date" Then
    '            If Pr_Type.Text = "Due Date" Then
    '                set_sql = "SELECT  en_desc," _
    '                   & "  b.ptnr_name,ar_code,ar_date, " _
    '                   & "  c.code_name AS ar_type_name, " _
    '                   & "  coalesce(a.ar_amount,0)  + coalesce(ar_shipping_charges,0) as ar_amount, " _
    '                   & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
    '                   & "  coalesce(a.ar_amount,0)  + coalesce(ar_shipping_charges,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_due_date" _
    '                   & " " _
    '                   & "FROM " _
    '                   & "  public.ar_mstr a " _
    '                   & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                   & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                   & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                   & "WHERE " _
    '                   & "  (lower(a.ar_status) <> 'c' OR  " _
    '                   & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_due_date between  " _
    '                   & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)


    '            Else
    '                set_sql = "SELECT en_desc, " _
    '                   & "  b.ptnr_name,ar_code,ar_date, " _
    '                   & "  c.code_name AS ar_type_name, " _
    '                   & "  coalesce(a.ar_amount,0) + coalesce(ar_shipping_charges,0) as ar_amount, " _
    '                   & "  coalesce(a.ar_pay_amount,0) as ar_pay_amount, " _
    '                   & "  coalesce(a.ar_amount,0)  + coalesce(ar_shipping_charges,0)-coalesce(a.ar_pay_amount,0) as ar_outstanding , a.ar_date as ar_due_date" _
    '                   & " " _
    '                   & "FROM " _
    '                   & "  public.ar_mstr a " _
    '                   & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                   & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                   & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                   & "WHERE " _
    '                   & "  (lower(a.ar_status) <> 'c' OR  " _
    '                   & "  a.ar_status IS NULL) and a.ar_cu_id=" & SetInteger(cu_id.EditValue) & "  and a.ar_date between  " _
    '                   & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)


    '            End If

    '        End If

    '    Else
    '        If CBOutstandingType.Text = "Before Outstanding Date" Then
    '            set_sql = "SELECT  en_desc," _
    '                & "  b.ptnr_name,ar_code,ar_date, " _
    '                & "  c.code_name AS ar_type_name, " _
    '                & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
    '                & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
    '                & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
    '                & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
    '                & "FROM " _
    '                & "  public.ar_mstr a " _
    '                & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                & "WHERE " _
    '                & "  (lower(a.ar_status) <> 'c' OR  " _
    '                & "  a.ar_status IS NULL) and a.ar_due_date<=" _
    '                & SetDateNTime00(outstanding_date.Text)



    '        ElseIf CBOutstandingType.Text = "After Outstanding Date" Then
    '            set_sql = "SELECT en_desc, " _
    '                & "  b.ptnr_name,ar_code,ar_date, " _
    '                & "  c.code_name AS ar_type_name, " _
    '               & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
    '                & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
    '                & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
    '                & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
    '                & "FROM " _
    '                & "  public.ar_mstr a " _
    '                & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                & "WHERE " _
    '                & "  (lower(a.ar_status) <> 'c' OR  " _
    '                & "  a.ar_status IS NULL) and a.ar_due_date>=" _
    '                & SetDateNTime00(outstanding_date.Text)



    '        ElseIf CBOutstandingType.Text = "All Data" Then
    '            set_sql = "SELECT en_desc, " _
    '                & "  b.ptnr_name,ar_code,ar_date, " _
    '                & "  c.code_name AS ar_type_name, " _
    '                & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
    '                & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
    '                & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
    '                & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
    '                & "FROM " _
    '                & "  public.ar_mstr a " _
    '                & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                & "WHERE " _
    '                & "  (lower(a.ar_status) <> 'c' OR  " _
    '                & "  a.ar_status IS NULL) "



    '        ElseIf CBOutstandingType.Text = "Between Date" Then
    '            If Pr_Type.Text = "Due Date" Then
    '                set_sql = "SELECT en_desc, " _
    '                   & "  b.ptnr_name,ar_code,ar_date, " _
    '                   & "  c.code_name AS ar_type_name, " _
    '                   & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
    '                   & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
    '                   & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
    '                   & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_due_date " _
    '                   & " " _
    '                   & "FROM " _
    '                   & "  public.ar_mstr a " _
    '                   & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                   & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                   & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                   & "WHERE " _
    '                   & "  (lower(a.ar_status) <> 'c' OR  " _
    '                   & "  a.ar_status IS NULL)  and a.ar_due_date between  " _
    '                   & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)



    '            Else
    '                set_sql = "SELECT en_desc, " _
    '                   & "  b.ptnr_name,ar_code,ar_date, " _
    '                   & "  c.code_name AS ar_type_name, " _
    '                   & "  coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0) AS ar_amount, " _
    '                   & "  f_get_ar_payment(a.ar_oid) AS ar_pay_amount, " _
    '                   & "  (coalesce(a.ar_amount, 0) * coalesce(a.ar_exc_rate, 0)) - " _
    '                   & "(f_get_ar_payment(a.ar_oid)) AS ar_outstanding, a.ar_date as ar_due_date " _
    '                   & " " _
    '                   & "FROM " _
    '                   & "  public.ar_mstr a " _
    '                   & "  INNER JOIN public.ptnr_mstr b ON (a.ar_bill_to = b.ptnr_id) " _
    '                   & "  INNER JOIN public.code_mstr c ON (a.ar_type = c.code_id) " _
    '                   & "  INNER JOIN public.en_mstr ON (en_id = ar_en_id) " _
    '                   & "WHERE " _
    '                   & "  (lower(a.ar_status) <> 'c' OR  " _
    '                   & "  a.ar_status IS NULL)   and a.ar_date between  " _
    '                   & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime)



    '            End If
    '        End If
    '    End If

    '    If CeAllEntity.EditValue = False Then
    '        set_sql = set_sql & " and ar_en_id = " + en_id.EditValue.ToString
    '    Else
    '        set_sql = set_sql & " and ar_en_id in (select user_en_id from tconfuserentity " _
    '                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + " ) "
    '    End If

    '    Return set_sql
    'End Function
End Class
