Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDPcsPrintNew
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _pcs_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist As DataSet
    Dim _now As DateTime
    Public _par_cus_id As String

    Private Sub FDPcsPrintNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'pcs_en_id.Properties.DataSource = dt_bantu
        'pcs_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'pcs_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'pcs_en_id.ItemIndex = 0
        init_le(pcs_en_id, "en_mstr")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_cu_mstr())
        'pcs_cu_id.Properties.DataSource = dt_bantu
        'pcs_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        'pcs_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        'pcs_cu_id.ItemIndex = 0
        init_le(pcs_cu_id, "cu_mstr")


        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(pcs_en_id.EditValue) & " and enacc_code='dbcr_account')"

        dt_bantu = New DataTable

        If limit_account(pcs_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If


        'dt_bantu = (func_data.load_ac_mstr(_filter))
        pcs_ac_id.Properties.DataSource = dt_bantu
        pcs_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        pcs_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        pcs_ac_id.ItemIndex = 0


        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ac_mstr())
        'pcs_ac_prepaid.Properties.DataSource = dt_bantu
        'pcs_ac_prepaid.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        'pcs_ac_prepaid.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        'pcs_ac_prepaid.ItemIndex = 0
        init_le(pcs_ac_prepaid, "account")

        dt_bantu = New DataTable
        dt_bantu = func_data.load_ppn_type()
        pcs_ppn_type.Properties.DataSource = dt_bantu
        pcs_ppn_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        pcs_ppn_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        pcs_ppn_type.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_customer(pcs_en_id.EditValue))
        'pcs_bill_to.Properties.DataSource = dt_bantu
        'pcs_bill_to.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        'pcs_bill_to.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        'pcs_bill_to.ItemIndex = 0

        init_le(pcs_bill_to, "cus_mstr_parent", pcs_en_id.EditValue)


        'pcs_ac_id.EditValue = pcs_bill_to.GetColumnValue("ptnr_ac_pcs_id")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(pcs_en_id.EditValue))
        pcs_credit_term.Properties.DataSource = dt_bantu
        pcs_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        pcs_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        pcs_credit_term.ItemIndex = 0

        pcs_due_date.DateTime = pcs_eff_date.DateTime.AddDays(pcs_credit_term.GetColumnValue("code_usr_1"))
        pcs_expt_date.DateTime = pcs_eff_date.DateTime.AddDays(pcs_credit_term.GetColumnValue("code_usr_1"))

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(pcs_en_id.EditValue))
        pcs_sb_id.Properties.DataSource = dt_bantu
        pcs_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        pcs_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        pcs_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(pcs_en_id.EditValue))
        pcs_cc_id.Properties.DataSource = dt_bantu
        pcs_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        pcs_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        pcs_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_taxclass_mstr(pcs_en_id.EditValue))
        pcs_tax_class_id.Properties.DataSource = dt_bantu
        pcs_tax_class_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        pcs_tax_class_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        pcs_tax_class_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(pcs_en_id.EditValue, "pcs_type"))
        pcs_type.Properties.DataSource = dt_bantu
        pcs_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        pcs_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        pcs_type.ItemIndex = 0

        Dim _filter As String

        _filter = "and bk_id in (SELECT  " _
            & "  bk_id " _
            & "FROM  " _
            & "  public.bk_mstr  " _
            & "WHERE  " _
            & "bk_ac_id in (SELECT  " _
            & "  enacc_ac_id " _
            & "FROM  " _
            & "  public.enacc_mstr  " _
            & "Where enacc_en_id=" & SetInteger(pcs_en_id.EditValue) & " and enacc_code='dbcr_bank'))"

        dt_bantu = New DataTable

        If limit_account(pcs_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_bk_mstr(pcs_en_id.EditValue, _filter, True))
        Else
            dt_bantu = (func_data.load_bk_mstr(pcs_en_id.EditValue))
        End If


        pcs_bk_id.Properties.DataSource = dt_bantu
        pcs_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        pcs_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        pcs_bk_id.ItemIndex = 0


        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(pcs_en_id.EditValue) & " and enacc_code='dbcr_account')"

        dt_bantu = New DataTable

        If limit_account(pcs_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If


        pcs_ac_id.Properties.DataSource = dt_bantu
        pcs_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        pcs_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        pcs_ac_id.ItemIndex = 0

    End Sub

    Private Sub ap_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcs_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Sub ap_cu_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcs_cu_id.EditValueChanged
        If pcs_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            pcs_exc_rate.EditValue = func_data.get_exchange_rate(pcs_cu_id.EditValue)
        Else
            pcs_exc_rate.EditValue = 1
        End If
    End Sub

    Private Sub ap_ptnr_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcs_bill_to.EditValueChanged
        pcs_ac_id.EditValue = pcs_bill_to.GetColumnValue("ptnr_ac_pcs_id")

        Dim _ptnr_prepaid_balance As Double = 0
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(ptnr_prepaid_balance,0) as ptnr_prepaid_balance from ptnr_mstr " + _
                                           " where ptnr_id = " + pcs_bill_to.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _ptnr_prepaid_balance = .DataReader("ptnr_prepaid_balance")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        te_prepayment_balance.EditValue = _ptnr_prepaid_balance
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Invoice Number", "pcs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "pcs_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Exc. Rate", "pcs_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "pcs_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Expected Date", "pcs_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "pcs_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "pcs_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Type", "pcs_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "pcs_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "pcs_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Prepayment IDR", "pcs_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Account Code Prepayment", "pcs_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name Prepayment", "pcs_code_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "pcs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total", "pcs_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Shipping Charges", "pcs_shipping_charges", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Total Final", "pcs_total_final", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        add_column_copy(gv_master, "Total Payment", "pcs_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Outstanding Payment", "pcs_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Total IDR", "pcs_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Total Payment IDR", "pcs_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Outstanding Payment IDR", "pcs_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Status", "pcs_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "pcs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pcs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pcs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pcs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_so, "pcso_pcs_oid", False)
        add_column_copy(gv_detail_so, "SO Number", "pcso_so_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_shipment, "pcss_pcs_oid", False)
        add_column_copy(gv_detail_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Taxable", "pcss_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Tax Include", "pcss_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "PPn Type", "sod_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Qty Open", "pcss_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "Qty Shipment", "pcss_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "Qty Invoice", "pcss_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "SO Price", "pcss_so_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "SO Discount", "pcss_so_disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "Invoice Price", "pcss_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_shipment, "Total Price", "tot_inv_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_detail_shipment, "Close Line", "pcss_close_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Faktur Pajak Status", "pcss_fp_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_dist, "pcsd_pcs_oid", False)
        add_column_copy(gv_detail_dist, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "Taxable", "apd_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "Tax Include", "apd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "TaxClass", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        ''kalau dari po tax_class_name dan tax_include akan kosong, karena datanya di group berdasar account...dan bisa dari taxclass yang berbeda
        add_column_copy(gv_detail_dist, "Remarks", "pcsd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Amount", "pcsd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_dist, "Status", "pcsd_tax_distribution", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_so, "pcso_oid", False)
        add_column(gv_edit_so, "pcso_pcs_oid", False)
        add_column(gv_edit_so, "pcso_so_oid", False)
        add_column(gv_edit_so, "SO Number", "pcso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so, "pcso_so_date", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_shipment, "pcss_oid", False)
        add_column(gv_edit_shipment, "pcss_pcs_oid", False)
        add_column_edit(gv_edit_shipment, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "pt_id", False)
        add_column(gv_edit_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Taxable", "pcss_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Tax Include", "pcss_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "pcss_tax_class_id", False)
        add_column(gv_edit_shipment, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Qty Open", "pcss_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_shipment, "Qty Shipment", "pcss_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_shipment, "Qty Invoice", "pcss_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        If func_coll.get_conf_file("editable_invoice_price") = "1" Then
            add_column_edit(gv_edit_shipment, "Invoice Price", "pcss_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        Else
            add_column(gv_edit_shipment, "Invoice Price", "pcss_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        End If


        'add_column(gv_edit_shipment, "SO Price", "pcss_so_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_shipment, "SO Discount", "pcss_so_disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_shipment, "Invoice Price", "pcss_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_shipment, "Total Price", "tot_inv_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_edit(gv_edit_shipment, "Close Line", "pcss_close_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_shipment, "Faktur Pajak Status", "pcss_fp_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_dist, "pcsd_oid", False)
        add_column(gv_edit_dist, "pcsd_ac_id", False)
        add_column(gv_edit_dist, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "pcsd_sb_id", False)
        add_column(gv_edit_dist, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "pcsd_cc_id", False)
        add_column(gv_edit_dist, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Taxable", "pcsd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Tax Include", "pcsd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "pcsd_tax_class_id", False)
        add_column(gv_edit_dist, "TaxClass", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        'kalau dari po tax_class_name dan tax_include akan kosong, karena datanya di group berdasar account...dan bisa dari taxclass yang berbeda
        add_column(gv_edit_dist, "Remarks", "pcsd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Amount", "pcsd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_edit_dist, "Status", "pcsd_tax_distribution", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_dist, "Recalculate", "apd_recalculate", DevExpress.Utils.HorzAlignment.Default) 'untuk membantu proses recalculate tax u/ yang input ap manual tanpa po
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.pcs_mstr.pcs_oid, " _
                & "  public.pcs_mstr.pcs_dom_id, " _
                & "  public.pcs_mstr.pcs_en_id, " _
                & "  public.pcs_mstr.pcs_add_by, " _
                & "  public.pcs_mstr.pcs_add_date, " _
                & "  public.pcs_mstr.pcs_upd_by, " _
                & "  public.pcs_mstr.pcs_upd_date, " _
                & "  public.pcs_mstr.pcs_code, " _
                & "  public.pcs_mstr.pcs_date, " _
                & "  public.pcs_mstr.pcs_bill_to, " _
                & "  public.pcs_mstr.pcs_cu_id, " _
                & "  public.pcs_mstr.pcs_exc_rate, " _
                & "  public.pcs_mstr.pcs_credit_term, " _
                & "  public.pcs_mstr.pcs_eff_date, " _
                & "  public.pcs_mstr.pcs_disc_date, " _
                & "  public.pcs_mstr.pcs_expt_date, " _
                & "  public.pcs_mstr.pcs_ac_id, " _
                & "  public.pcs_mstr.pcs_sb_id, " _
                & "  public.pcs_mstr.pcs_cc_id, " _
                & "  public.pcs_mstr.pcs_type, " _
                & "  public.pcs_mstr.pcs_taxable, " _
                & "  public.pcs_mstr.pcs_tax_inc, " _
                & "  public.pcs_mstr.pcs_tax_class_id,pcs_shipping_charges,pcs_total_final, " _
                & "  public.pcs_mstr.pcs_pay_prepaid, " _
                & "  public.pcs_mstr.pcs_pay_prepaid * pcs_exc_rate as pcs_pay_prepaid_idr, " _
                & "  public.pcs_mstr.pcs_ac_prepaid, " _
                & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                & "  public.pcs_mstr.pcs_amount, " _
                & "  public.pcs_mstr.pcs_pay_amount, " _
                & "  public.pcs_mstr.pcs_amount - public.pcs_mstr.pcs_pay_amount as pcs_outstanding, " _
                & "  public.pcs_mstr.pcs_amount * pcs_exc_rate as pcs_amount_idr, " _
                & "  public.pcs_mstr.pcs_pay_amount * pcs_exc_rate as pcs_pay_amount_idr, " _
                & "  (public.pcs_mstr.pcs_amount - public.pcs_mstr.pcs_pay_amount) * pcs_exc_rate as pcs_outstanding_idr, " _
                & "  public.pcs_mstr.pcs_remarks, " _
                & "  public.pcs_mstr.pcs_status, " _
                & "  public.pcs_mstr.pcs_dt, " _
                & "  public.pcs_mstr.pcs_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  credit_term_mstr.code_name as credit_terms_name, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.cc_mstr.cc_desc, " _
                & "  public.pcs_mstr.pcs_bk_id, " _
                & "  public.bk_mstr.bk_name, " _
                & "  coalesce(pcs_ppn_type,'N') as pcs_ppn_type, " _
                & "  pcs_type.code_name as pcs_type_name, " _
                & "  taxclass_mstr.code_name as taxclass_name " _
                & "FROM " _
                & "  public.pcs_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.pcs_mstr.pcs_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.pcs_mstr.pcs_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.pcs_mstr.pcs_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.code_mstr credit_term_mstr ON (public.pcs_mstr.pcs_credit_term = credit_term_mstr.code_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.pcs_mstr.pcs_ac_id = public.ac_mstr.ac_id) " _
                & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.pcs_mstr.pcs_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.pcs_mstr.pcs_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.pcs_mstr.pcs_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr pcs_type ON (public.pcs_mstr.pcs_type = pcs_type.code_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.pcs_mstr.pcs_bk_id = bk_mstr.bk_id) " _
                & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.pcs_mstr.pcs_tax_class_id = taxclass_mstr.code_id) " _
                & " where pcs_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and pcs_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and pcs_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        If par_cus.Text <> "" Then
            If par_so.Text <> "" Then
                get_sequel = "SELECT  " _
                    & "  public.pcs_mstr.pcs_oid, " _
                    & "  public.pcs_mstr.pcs_dom_id, " _
                    & "  public.pcs_mstr.pcs_en_id, " _
                    & "  public.pcs_mstr.pcs_add_by, " _
                    & "  public.pcs_mstr.pcs_add_date, " _
                    & "  public.pcs_mstr.pcs_upd_by, " _
                    & "  public.pcs_mstr.pcs_upd_date,pcs_shipping_charges, " _
                    & "  public.pcs_mstr.pcs_code, " _
                    & "  public.pcs_mstr.pcs_date, " _
                    & "  public.pcs_mstr.pcs_bill_to, " _
                    & "  public.pcs_mstr.pcs_cu_id, " _
                    & "  public.pcs_mstr.pcs_exc_rate, " _
                    & "  public.pcs_mstr.pcs_credit_term, " _
                    & "  public.pcs_mstr.pcs_eff_date, " _
                    & "  public.pcs_mstr.pcs_disc_date, " _
                    & "  public.pcs_mstr.pcs_expt_date, " _
                    & "  public.pcs_mstr.pcs_ac_id, " _
                    & "  public.pcs_mstr.pcs_sb_id, " _
                    & "  public.pcs_mstr.pcs_cc_id, " _
                    & "  public.pcs_mstr.pcs_type, " _
                    & "  public.pcs_mstr.pcs_taxable, " _
                    & "  public.pcs_mstr.pcs_tax_inc, " _
                    & "  public.pcs_mstr.pcs_tax_class_id, " _
                    & "  public.pcs_mstr.pcs_pay_prepaid, " _
                    & "  public.pcs_mstr.pcs_pay_prepaid * pcs_exc_rate as pcs_pay_prepaid_idr, " _
                    & "  public.pcs_mstr.pcs_ac_prepaid, " _
                    & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                    & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                    & "  public.pcs_mstr.pcs_amount, " _
                    & "  public.pcs_mstr.pcs_pay_amount, " _
                    & "  public.pcs_mstr.pcs_amount - public.pcs_mstr.pcs_pay_amount as pcs_outstanding, " _
                    & "  public.pcs_mstr.pcs_amount * pcs_exc_rate as pcs_amount_idr, " _
                    & "  public.pcs_mstr.pcs_pay_amount * pcs_exc_rate as pcs_pay_amount_idr, " _
                    & "  (public.pcs_mstr.pcs_amount - public.pcs_mstr.pcs_pay_amount) * pcs_exc_rate as pcs_outstanding_idr, " _
                    & "  public.pcs_mstr.pcs_remarks, " _
                    & "  public.pcs_mstr.pcs_status, " _
                    & "  public.pcs_mstr.pcs_dt, " _
                    & "  public.pcs_mstr.pcs_due_date, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  public.cu_mstr.cu_name, " _
                    & "  credit_term_mstr.code_name as credit_terms_name, " _
                    & "  public.ac_mstr.ac_code, " _
                    & "  public.ac_mstr.ac_name, " _
                    & "  public.sb_mstr.sb_desc, " _
                    & "  public.cc_mstr.cc_desc, " _
                    & "  public.pcs_mstr.pcs_bk_id, " _
                    & "  public.bk_mstr.bk_name, " _
                    & "  public.pcs_mstr.pcs_ppn_type, " _
                    & "  pcs_type.code_name as pcs_type_name, " _
                    & "  taxclass_mstr.code_name as taxclass_name " _
                    & "FROM " _
                    & "  public.pcs_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.pcs_mstr.pcs_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.pcs_mstr.pcs_bill_to = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cu_mstr ON (public.pcs_mstr.pcs_cu_id = public.cu_mstr.cu_id) " _
                    & "  INNER JOIN public.code_mstr credit_term_mstr ON (public.pcs_mstr.pcs_credit_term = credit_term_mstr.code_id) " _
                    & "  INNER JOIN public.ac_mstr ON (public.pcs_mstr.pcs_ac_id = public.ac_mstr.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.pcs_mstr.pcs_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.pcs_mstr.pcs_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.pcs_mstr.pcs_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.code_mstr pcs_type ON (public.pcs_mstr.pcs_type = pcs_type.code_id) " _
                    & "  INNER JOIN public.bk_mstr ON (public.pcs_mstr.pcs_bk_id = bk_mstr.bk_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.pcs_mstr.pcs_tax_class_id = taxclass_mstr.code_id) " _
                    & "  INNER JOIN public.pcso_so ON pcso_pcs_oid = pcs_oid " _
                    & " where pcs_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and pcs_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and pcs_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " and pcso_so_code='" & par_so.Text & "'"
            Else
                get_sequel = "SELECT  " _
                  & "  public.pcs_mstr.pcs_oid, " _
                  & "  public.pcs_mstr.pcs_dom_id, " _
                  & "  public.pcs_mstr.pcs_en_id, " _
                  & "  public.pcs_mstr.pcs_add_by, " _
                  & "  public.pcs_mstr.pcs_add_date, " _
                  & "  public.pcs_mstr.pcs_upd_by, " _
                  & "  public.pcs_mstr.pcs_upd_date, " _
                  & "  public.pcs_mstr.pcs_code, " _
                  & "  public.pcs_mstr.pcs_date, " _
                  & "  public.pcs_mstr.pcs_bill_to, " _
                  & "  public.pcs_mstr.pcs_cu_id, " _
                  & "  public.pcs_mstr.pcs_exc_rate, " _
                  & "  public.pcs_mstr.pcs_credit_term, " _
                  & "  public.pcs_mstr.pcs_eff_date, " _
                  & "  public.pcs_mstr.pcs_disc_date, " _
                  & "  public.pcs_mstr.pcs_expt_date, " _
                  & "  public.pcs_mstr.pcs_ac_id, " _
                  & "  public.pcs_mstr.pcs_sb_id, " _
                  & "  public.pcs_mstr.pcs_cc_id, " _
                  & "  public.pcs_mstr.pcs_type, " _
                  & "  public.pcs_mstr.pcs_taxable, " _
                  & "  public.pcs_mstr.pcs_tax_inc, " _
                  & "  public.pcs_mstr.pcs_tax_class_id,pcs_shipping_charges, " _
                  & "  public.pcs_mstr.pcs_pay_prepaid, " _
                  & "  public.pcs_mstr.pcs_pay_prepaid * pcs_exc_rate as pcs_pay_prepaid_idr, " _
                  & "  public.pcs_mstr.pcs_ac_prepaid, " _
                  & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                  & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                  & "  public.pcs_mstr.pcs_amount, " _
                  & "  public.pcs_mstr.pcs_pay_amount, " _
                  & "  public.pcs_mstr.pcs_amount - public.pcs_mstr.pcs_pay_amount as pcs_outstanding, " _
                  & "  public.pcs_mstr.pcs_amount * pcs_exc_rate as pcs_amount_idr, " _
                  & "  public.pcs_mstr.pcs_pay_amount * pcs_exc_rate as pcs_pay_amount_idr, " _
                  & "  (public.pcs_mstr.pcs_amount - public.pcs_mstr.pcs_pay_amount) * pcs_exc_rate as pcs_outstanding_idr, " _
                  & "  public.pcs_mstr.pcs_remarks, " _
                  & "  public.pcs_mstr.pcs_status, " _
                  & "  public.pcs_mstr.pcs_dt, " _
                  & "  public.pcs_mstr.pcs_due_date, " _
                  & "  public.en_mstr.en_desc, " _
                  & "  public.ptnr_mstr.ptnr_name, " _
                  & "  public.cu_mstr.cu_name, " _
                  & "  credit_term_mstr.code_name as credit_terms_name, " _
                  & "  public.ac_mstr.ac_code, " _
                  & "  public.ac_mstr.ac_name, " _
                  & "  public.sb_mstr.sb_desc, " _
                  & "  public.cc_mstr.cc_desc, " _
                  & "  public.pcs_mstr.pcs_bk_id, " _
                  & "  public.bk_mstr.bk_name, " _
                  & "  pcs_type.code_name as pcs_type_name, " _
                  & "  public.pcs_mstr.pcs_ppn_type, " _
                  & "  taxclass_mstr.code_name as taxclass_name " _
                  & "FROM " _
                  & "  public.pcs_mstr " _
                  & "  INNER JOIN public.en_mstr ON (public.pcs_mstr.pcs_en_id = public.en_mstr.en_id) " _
                  & "  INNER JOIN public.ptnr_mstr ON (public.pcs_mstr.pcs_bill_to = public.ptnr_mstr.ptnr_id) " _
                  & "  INNER JOIN public.cu_mstr ON (public.pcs_mstr.pcs_cu_id = public.cu_mstr.cu_id) " _
                  & "  INNER JOIN public.code_mstr credit_term_mstr ON (public.pcs_mstr.pcs_credit_term = credit_term_mstr.code_id) " _
                  & "  INNER JOIN public.ac_mstr ON (public.pcs_mstr.pcs_ac_id = public.ac_mstr.ac_id) " _
                  & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.pcs_mstr.pcs_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                  & "  INNER JOIN public.sb_mstr ON (public.pcs_mstr.pcs_sb_id = public.sb_mstr.sb_id) " _
                  & "  INNER JOIN public.cc_mstr ON (public.pcs_mstr.pcs_cc_id = public.cc_mstr.cc_id) " _
                  & "  INNER JOIN public.code_mstr pcs_type ON (public.pcs_mstr.pcs_type = pcs_type.code_id) " _
                  & "  INNER JOIN public.bk_mstr ON (public.pcs_mstr.pcs_bk_id = bk_mstr.bk_id) " _
                  & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.pcs_mstr.pcs_tax_class_id = taxclass_mstr.code_id) " _
                  & " where pcs_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                  & " and pcs_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                  & " and pcs_en_id in (select user_en_id from tconfuserentity " _
                  & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                  & " and pcs_bill_to=" & _par_cus_id
            End If
        End If


        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim sql As String

        'Try
        '    ds.Tables("detail_so").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  pcso_oid, " _
        '    & "  pcso_pcs_oid, " _
        '    & "  pcso_seq, " _
        '    & "  pcso_so_oid, " _
        '    & "  pcso_so_code, " _
        '    & "  pcso_so_date, " _
        '    & "  pcso_dt, " _
        '    & "  pcso_amount " _
        '    & "FROM  " _
        '    & "  public.pcso_so  " _
        '    & "  inner join public.pcs_mstr on pcs_mstr.pcs_oid = pcso_pcs_oid" _
        '    & "  inner join public.so_mstr on so_mstr.so_oid = pcso_so_oid" _
        '    & "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        'load_data_detail(sql, gc_detail_so, "detail_so")

        'Try
        '    ds.Tables("detail_shipment").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  pcss_oid, " _
        '    & "  pcss_pcs_oid, " _
        '    & "  pcss_seq, " _
        '    & "  pcss_soshipd_oid, " _
        '    & "  soship_code, " _
        '    & "  pt_code, " _
        '    & "  pt_desc1, " _
        '    & "  pt_desc2, " _
        '    & "  pcss_taxable, " _
        '    & "  pcss_tax_class_id, " _
        '    & "  code_name as taxclass_name, " _
        '    & "  pcss_tax_inc, " _
        '    & "  pcss_open,pcss_shipment, " _
        '    & "  pcss_invoice, " _
        '    & "  pcss_so_price, " _
        '    & "  pcss_so_disc_value, " _
        '    & "  pcss_invoice_price, " _
        '    & "  pcss_invoice_price * pcss_so_price as tot_inv_price, " _
        '    & "  pcss_close_line, " _
        '    & "  pcss_fp_status, " _
        '    & "  pcss_dt " _
        '    & "FROM  " _
        '    & "  public.pcss_ship " _
        '    & "  inner join public.soshipd_det on public.pcss_ship.pcss_soshipd_oid = public.soshipd_det.soshipd_oid " _
        '    & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
        '    & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
        '    & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
        '    & "  inner join public.code_mstr on public.pcss_ship.pcss_tax_class_id = public.code_mstr.code_id" _
        '    & "  inner join public.pcs_mstr on public.pcss_ship.pcss_pcs_oid = public.pcs_mstr.pcs_oid" _
        '    & "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        'load_data_detail(sql, gc_detail_shipment, "detail_shipment")

        'Try
        '    ds.Tables("detail_dist").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  public.pcsd_dist.pcsd_oid, " _
        '    & "  public.pcsd_dist.pcsd_pcs_oid, " _
        '    & "  public.pcsd_dist.pcsd_tax_distribution, " _
        '    & "  public.pcsd_dist.pcsd_taxable, " _
        '    & "  public.pcsd_dist.pcsd_tax_inc, " _
        '    & "  public.pcsd_dist.pcsd_tax_class_id, " _
        '    & "  public.pcsd_dist.pcsd_ac_id, " _
        '    & "  public.pcsd_dist.pcsd_sb_id, " _
        '    & "  public.pcsd_dist.pcsd_cc_id, " _
        '    & "  public.pcsd_dist.pcsd_amount, " _
        '    & "  public.pcsd_dist.pcsd_remarks, " _
        '    & "  public.pcsd_dist.pcsd_dt, " _
        '    & "  public.ac_mstr.ac_code, " _
        '    & "  public.ac_mstr.ac_name, " _
        '    & "  public.sb_mstr.sb_desc, " _
        '    & "  public.code_mstr.code_name, " _
        '    & "  public.cc_mstr.cc_desc " _
        '    & "FROM " _
        '    & "  public.pcsd_dist " _
        '    & "  INNER JOIN public.pcs_mstr ON (public.pcsd_dist.pcsd_pcs_oid = public.pcs_mstr.pcs_oid) " _
        '    & "  INNER JOIN public.ac_mstr ON (public.pcsd_dist.pcsd_ac_id = public.ac_mstr.ac_id) " _
        '    & "  left outer join public.sb_mstr ON (public.pcsd_dist.pcsd_sb_id = public.sb_mstr.sb_id) " _
        '    & "  left outer join public.cc_mstr ON (public.pcsd_dist.pcsd_cc_id = public.cc_mstr.cc_id) " _
        '    & "  left outer join public.code_mstr ON (public.pcsd_dist.pcsd_tax_class_id = public.code_mstr.code_id)" _
        '    & "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        'load_data_detail(sql, gc_detail_dist, "detail_dist")
    End Sub

    Public Overrides Sub relation_detail()
        'Try
        '    gv_detail_so.Columns("pcso_pcs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid"))
        '    gv_detail_so.BestFitColumns()

        '    gv_detail_shipment.Columns("pcss_pcs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid"))
        '    gv_detail_shipment.BestFitColumns()

        '    gv_detail_dist.Columns("pcsd_pcs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid"))
        '    gv_detail_dist.BestFitColumns()
        'Catch ex As Exception
        'End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        'pcs_eff_date.Enabled = False
        'by sys 20110427 permintaan pak aji..ingin eff date diambil dari shipment terakhir
        'pcs_eff_date.DateTime = _now

        pcs_en_id.Focus()
        pcs_en_id.ItemIndex = 0
        pcs_bill_to.ItemIndex = 0
        pcs_ac_id.ItemIndex = 0
        pcs_cu_id.ItemIndex = 0
        pcs_bk_id.ItemIndex = 0
        'pcs_eff_date.DateTime = _now
        pcs_due_date.DateTime = _now
        pcs_expt_date.DateTime = _now
        pcs_taxable.EditValue = False
        pcs_tax_inc.EditValue = False
        pcs_ppn_type.ItemIndex = 0
        pcs_remarks.Text = ""
        pcs_exc_rate.EditValue = 1

        pcs_en_id.Enabled = True
        pcs_bill_to.Enabled = True
        pcs_cu_id.Enabled = True
        pcs_bk_id.Enabled = True
        pcs_ac_id.Enabled = True
        pcs_sb_id.Enabled = True
        pcs_cc_id.Enabled = True
        pcs_exc_rate.Enabled = True
        pcs_tax_inc.Enabled = True
        pcs_ppn_type.Enabled = True
        pcs_tax_class_id.Enabled = True
        pcs_pay_prepaid.EditValue = 0
        pcs_ac_prepaid.ItemIndex = 0
        gc_edit_so.Enabled = True
        gc_edit_shipment.Enabled = True
        gc_edit_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = True
        sb_retrieve_dist.Enabled = True
        pcs_shipping_charges.EditValue = 0.0

        gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = True

        gv_edit_dist.Columns("pcsd_taxable").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("pcsd_tax_inc").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("pcsd_remarks").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("pcsd_remarks").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("pcsd_amount").OptionsColumn.AllowEdit = True
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_so = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  pcso_oid, " _
                        & "  pcso_pcs_oid, " _
                        & "  pcso_seq, " _
                        & "  pcso_so_oid, " _
                        & "  pcso_so_code, " _
                        & "  pcso_so_date, " _
                        & "  pcso_dt, " _
                        & "  pcso_amount " _
                        & "FROM  " _
                        & "  public.pcso_so  " _
                        & "  inner join public.pcs_mstr on pcs_mstr.pcs_oid = pcso_pcs_oid" _
                        & "  inner join public.so_mstr on so_mstr.so_oid = pcso_so_oid" _
                        & "  where pcso_so_code ~~* 'asdfad'"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_so, "list_so")
                    gc_edit_so.DataSource = ds_edit_so.Tables(0)
                    gv_edit_so.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_shipment = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  pcss_oid, True as ceklist, " _
                        & "  pcss_pcs_oid, " _
                        & "  pcss_seq, " _
                        & "  pcss_soshipd_oid, " _
                        & "  soship_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pcss_taxable, " _
                        & "  pcss_tax_class_id, " _
                        & "  code_name as taxclass_name, " _
                        & "  pcss_tax_inc, " _
                        & "  pcss_open,pcss_shipment, " _
                        & "  pcss_invoice, " _
                        & "  pcss_so_price, " _
                        & "  pcss_so_disc_value, " _
                        & "  pcss_invoice_price, " _
                        & "  pcss_invoice_price * pcss_so_price as tot_inv_price, " _
                        & "  pcss_close_line, " _
                        & "  pcss_dt " _
                        & "FROM  " _
                        & "  public.pcss_ship " _
                        & "  inner join public.soshipd_det on public.pcss_ship.pcss_soshipd_oid = public.soshipd_det.soshipd_oid " _
                        & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                        & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                        & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                        & "  inner join public.code_mstr on public.pcss_ship.pcss_tax_class_id = public.code_mstr.code_id" _
                        & "  inner join public.pcs_mstr on public.pcss_ship.pcss_pcs_oid = public.pcs_mstr.pcs_oid" _
                        & " where pcss_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_shipment, "shipment")
                    gc_edit_shipment.DataSource = ds_edit_shipment.Tables(0)
                    gv_edit_shipment.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_dist = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  public.pcsd_dist.pcsd_oid, " _
                        & "  public.pcsd_dist.pcsd_pcs_oid, " _
                        & "  public.pcsd_dist.pcsd_tax_distribution, " _
                        & "  public.pcsd_dist.pcsd_taxable, " _
                        & "  public.pcsd_dist.pcsd_tax_inc, " _
                        & "  public.pcsd_dist.pcsd_tax_class_id, " _
                        & "  public.pcsd_dist.pcsd_ac_id, " _
                        & "  public.pcsd_dist.pcsd_sb_id, " _
                        & "  public.pcsd_dist.pcsd_cc_id, " _
                        & "  public.pcsd_dist.pcsd_amount, " _
                        & "  public.pcsd_dist.pcsd_remarks, " _
                        & "  public.pcsd_dist.pcsd_dt, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.sb_mstr.sb_desc, " _
                        & "  public.code_mstr.code_name as taxclass_name, " _
                        & "  public.cc_mstr.cc_desc " _
                        & "FROM " _
                        & "  public.pcsd_dist " _
                        & "  INNER JOIN public.pcs_mstr ON (public.pcsd_dist.pcsd_pcs_oid = public.pcs_mstr.pcs_oid) " _
                        & "  INNER JOIN public.ac_mstr ON (public.pcsd_dist.pcsd_ac_id = public.ac_mstr.ac_id) " _
                        & "  left outer join public.sb_mstr ON (public.pcsd_dist.pcsd_sb_id = public.sb_mstr.sb_id) " _
                        & "  left outer join public.cc_mstr ON (public.pcsd_dist.pcsd_cc_id = public.cc_mstr.cc_id) " _
                        & "  left outer join public.code_mstr ON (public.pcsd_dist.pcsd_tax_class_id = public.code_mstr.code_id)" _
                        & " where pcsd_amount = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_dist, "dist")
                    gc_edit_dist.DataSource = ds_edit_dist.Tables(0)
                    gv_edit_dist.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit_so.UpdateCurrentRow()
        gv_edit_shipment.UpdateCurrentRow()
        gv_edit_dist.UpdateCurrentRow()

        ds_edit_so.AcceptChanges()
        ds_edit_shipment.AcceptChanges()
        ds_edit_dist.AcceptChanges()

        If pcs_ac_id.ItemIndex = -1 Then
            MessageBox.Show("Account can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If pcs_bk_id.ItemIndex = -1 Then
            MessageBox.Show("Bank can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If pcs_exc_rate.EditValue = 0 Then
            MessageBox.Show("Exc Rate can't 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _gcald_det_status As String = func_data.get_gcald_det_status(pcs_en_id.EditValue, "gcald_ar", pcs_eff_date.DateTime)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + pcs_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + pcs_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        '*********************
        'Cek close line di tab shipment
        Dim i As Integer
        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            With ds_edit_shipment.Tables(0).Rows(i)
                If (.Item("pcss_open") = .Item("pcss_invoice")) And (.Item("pcss_so_price") = .Item("pcss_invoice_price")) Then
                    .Item("pcss_close_line") = "Y"
                End If
            End With
        Next
        '*********************

        If ds_edit_dist.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If ds_edit_so.Tables(0).Rows.Count >= 2 Then
            MessageBox.Show("SO detail can't over than 1 rows", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If te_prepayment_balance.EditValue > 0 And pcs_pay_prepaid.EditValue = 0 Then
            If MessageBox.Show("Do Not Use Prepaid Balance, Continue Saving Data ?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
                Return False
            End If
        End If
        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        row = BindingContext(ds.Tables(0)).Position

        If SetString(ds.Tables(0).Rows(row).Item("pcs_status")).ToString.ToLower = "c" Then
            Box("Can't edit close transaction")
            Return False
            Exit Function
        End If

        If MyBase.edit_data = True Then


            With ds.Tables(0).Rows(row)
                _pcs_oid_mstr = .Item("pcs_oid")
                pcs_en_id.EditValue = .Item("pcs_en_id")
                pcs_bill_to.EditValue = .Item("pcs_bill_to")
                pcs_cu_id.EditValue = .Item("pcs_cu_id")
                pcs_bk_id.EditValue = .Item("pcs_bk_id")
                pcs_eff_date.DateTime = .Item("pcs_eff_date")
                pcs_due_date.DateTime = .Item("pcs_due_date")
                pcs_expt_date.DateTime = .Item("pcs_expt_date")
                pcs_exc_rate.EditValue = .Item("pcs_exc_rate")
                pcs_taxable.EditValue = SetBitYNB(.Item("pcs_taxable"))
                pcs_tax_inc.EditValue = SetBitYNB(.Item("pcs_tax_inc"))
                pcs_tax_class_id.EditValue = .Item("pcs_tax_class_id")
                pcs_ppn_type.EditValue = .Item("pcs_ppn_type")
                pcs_ac_id.EditValue = .Item("pcs_ac_id")
                pcs_sb_id.EditValue = .Item("pcs_sb_id")
                pcs_cc_id.EditValue = .Item("pcs_cc_id")
                pcs_remarks.Text = SetString(.Item("pcs_remarks"))
                pcs_type.EditValue = .Item("pcs_type")
                pcs_credit_term.EditValue = .Item("pcs_credit_term")
                pcs_pay_prepaid.EditValue = .Item("pcs_pay_prepaid")
                pcs_ac_prepaid.EditValue = .Item("pcs_ac_prepaid")
            End With

            pcs_en_id.Focus()
            pcs_en_id.Enabled = False
            pcs_bill_to.Enabled = False
            pcs_cu_id.Enabled = False
            pcs_bk_id.Enabled = False
            pcs_ac_id.Enabled = False
            pcs_exc_rate.Enabled = False
            pcs_tax_inc.Enabled = False
            pcs_tax_class_id.Enabled = False
            pcs_taxable.Enabled = False
            pcs_ppn_type.Enabled = False
            pcs_sb_id.Enabled = False
            pcs_cc_id.Enabled = False
            pcs_pay_prepaid.Enabled = False
            pcs_ac_prepaid.Enabled = False
            gc_edit_so.Enabled = False
            gc_edit_shipment.Enabled = False
            gc_edit_dist.Enabled = False
            pcs_eff_date.Enabled = False
            sb_retrieve_receive_item.Enabled = False
            sb_retrieve_dist.Enabled = False

            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit_so = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pcso_oid, " _
                            & "  pcso_pcs_oid, " _
                            & "  pcso_seq, " _
                            & "  pcso_so_oid, " _
                            & "  pcso_so_code, " _
                            & "  pcso_so_date, " _
                            & "  pcso_dt, " _
                            & "  pcso_amount " _
                            & "FROM  " _
                            & "  public.pcso_so  " _
                            & "  inner join public.pcs_mstr on pcs_mstr.pcs_oid = pcso_pcs_oid" _
                            & "  inner join public.so_mstr on so_mstr.so_oid = pcso_so_oid" _
                            & "  where pcso_pcs_oid = '" + _pcs_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_so, "list_so")
                        gc_edit_so.DataSource = ds_edit_so.Tables(0)
                        gv_edit_so.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_shipment = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pcss_oid, " _
                            & "  pcss_pcs_oid, " _
                            & "  pcss_seq, " _
                            & "  pcss_soshipd_oid, " _
                            & "  soship_code, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pcss_taxable, " _
                            & "  pcss_tax_class_id, " _
                            & "  code_name as taxclass_name, " _
                            & "  pcss_tax_inc, " _
                            & "  pcss_open, " _
                            & "  pcss_invoice,pcss_shipment, " _
                            & "  pcss_so_price, " _
                            & "  pcss_so_disc_value, " _
                            & "  pcss_invoice_price, " _
                            & "  pcss_invoice_price * pcss_so_price as tot_inv_price, " _
                            & "  pcss_close_line, " _
                            & "  pcss_dt " _
                            & "FROM  " _
                            & "  public.pcss_ship " _
                            & "  inner join public.soshipd_det on public.pcss_ship.pcss_soshipd_oid = public.soshipd_det.soshipd_oid " _
                            & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                            & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                            & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                            & "  inner join public.code_mstr on public.pcss_ship.pcss_tax_class_id = public.code_mstr.code_id" _
                            & "  inner join public.pcs_mstr on public.pcss_ship.pcss_pcs_oid = public.pcs_mstr.pcs_oid" _
                            & " where pcss_pcs_oid = '" + _pcs_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_shipment, "shipment")
                        gc_edit_shipment.DataSource = ds_edit_shipment.Tables(0)
                        gv_edit_shipment.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_dist = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                                & "  public.pcsd_dist.pcsd_oid, " _
                                & "  public.pcsd_dist.pcsd_pcs_oid, " _
                                & "  public.pcsd_dist.pcsd_tax_distribution, " _
                                & "  public.pcsd_dist.pcsd_taxable, " _
                                & "  public.pcsd_dist.pcsd_tax_inc, " _
                                & "  public.pcsd_dist.pcsd_tax_class_id, " _
                                & "  public.pcsd_dist.pcsd_ac_id, " _
                                & "  public.pcsd_dist.pcsd_sb_id, " _
                                & "  public.pcsd_dist.pcsd_cc_id, " _
                                & "  public.pcsd_dist.pcsd_amount, " _
                                & "  public.pcsd_dist.pcsd_remarks, " _
                                & "  public.pcsd_dist.pcsd_dt, " _
                                & "  public.ac_mstr.ac_code, " _
                                & "  public.ac_mstr.ac_name, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.code_mstr.code_name, " _
                                & "  public.cc_mstr.cc_desc " _
                                & "FROM " _
                                & "  public.pcsd_dist " _
                                & "  INNER JOIN public.pcs_mstr ON (public.pcsd_dist.pcsd_pcs_oid = public.pcs_mstr.pcs_oid) " _
                                & "  INNER JOIN public.ac_mstr ON (public.pcsd_dist.pcsd_ac_id = public.ac_mstr.ac_id) " _
                                & "  left outer join public.sb_mstr ON (public.pcsd_dist.pcsd_sb_id = public.sb_mstr.sb_id) " _
                                & "  left outer join public.cc_mstr ON (public.pcsd_dist.pcsd_cc_id = public.cc_mstr.cc_id) " _
                                & "  left outer join public.code_mstr ON (public.pcsd_dist.pcsd_tax_class_id = public.code_mstr.code_id)" _
                                & " where pcsd_pcs_oid = '" + _pcs_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_dist, "dist")
                        gc_edit_dist.DataSource = ds_edit_dist.Tables(0)
                        gv_edit_dist.BestFitColumns()
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
                                            & "  public.pcs_mstr   " _
                                            & "SET  " _
                                            & "  pcs_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pcs_en_id = " & SetInteger(pcs_en_id.EditValue) & ",  " _
                                            & "  pcs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pcs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pcs_credit_term = " & SetSetring(pcs_credit_term.EditValue) & ",  " _
                                            & "  pcs_eff_date = " & SetDate(pcs_eff_date.DateTime) & ",  " _
                                            & "  pcs_expt_date = " & SetDate(pcs_expt_date.DateTime) & ",  " _
                                            & "  pcs_type = " & SetSetring(pcs_type.EditValue) & ",  " _
                                            & "  pcs_remarks = " & SetSetring(pcs_remarks.Text) & ",  " _
                                            & "  pcs_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pcs_due_date = " & SetDate(pcs_due_date.DateTime) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pcs_oid = " & SetSetring(_pcs_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, pcs_en_id.EditValue, 13, _pcs_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code"), pcs_eff_date.DateTime) = False Then
                        '    ''sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If

                        'Yang diperbolehkan edit hanya data master saja,
                        'Untuk yang detail terlalu rumit karena sudah generate jurnal
                        'arabila salah untuk detail, lakukan delete aja, maka akan terjadi jurnal balik
                        'Lalu lakukan insert data lagi....

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Debit Credit Memo " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code"))
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
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_pcs_oid_mstr, "pcs_oid")
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

        Dim _pcs_eff_date As Date = master_new.PGSqlConn.CekTanggal
        Dim _pcs_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_en_id")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_pcs_en_id, "gcald_ar", _pcs_eff_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _pcs_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _pcs_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
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

        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

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

                            For i = 0 To ds.Tables("detail_shipment").Rows.Count - 1
                                If ds.Tables("detail_shipment").Rows(i).Item("pcss_pcs_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid") Then
                                    'Update Table shipment Detail untuk kolom soshipd_qty_inv dan soshipd_close_line nya
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update soshipd_det set soshipd_qty_inv = soshipd_qty_inv - " + SetDec(ds.Tables("detail_shipment").Rows(i).Item("pcss_invoice").ToString) + _
                                                           ", soshipd_close_line = 'N'" + _
                                                           " where soshipd_oid = '" + ds.Tables("detail_shipment").Rows(i).Item("pcss_soshipd_oid") + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    'Update Table so Detail untuk kolom sod_qty_invoice
                                    If ds.Tables("detail_shipment").Rows(i).Item("pcss_invoice") <> 0 Then
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update sod_det set sod_qty_invoice = sod_qty_invoice - " + SetDec(ds.Tables("detail_shipment").Rows(i).Item("pcss_invoice").ToString) + _
                                                               " where sod_oid = (select soshipd_sod_oid from soshipd_det where soshipd_oid = '" + ds.Tables("detail_shipment").Rows(i).Item("pcss_soshipd_oid") + "')"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()
                                    End If
                                End If
                            Next

                            'Update Table shipment Detail untuk kolom soshipd_qty_inv dan soshipd_close_line nya
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_prepaid_balance = coalesce(ptnr_prepaid_balance,0) - " + SetDec(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_pay_prepaid").ToString) + _
                                                   " where ptnr_id = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_bill_to").ToString
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from pcs_mstr where pcs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            If _create_jurnal = True Then
                                If func_coll.delete_glt_det_ar(ssqls, objinsert, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid"), _
                                                           ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")) = False Then
                                    'sqlTran.Rollback()
                                    Exit Function
                                End If
                            End If


                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Debit Credit Memo " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code"))
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
                            MessageBox.Show("Data Telah Berhasil Di Harus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            'sqlTran.Rollback()
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_so_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.DoubleClick
        browse_so()
    End Sub

    Private Sub gv_edit_so_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_so.InitNewRow
        Try
            If gv_edit_so.RowCount >= 1 Then
                gc_edit_so.EmbeddedNavigator.Buttons.Append.Visible = False
                gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_edit_so_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_so.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_so.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_so.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_so_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_so.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_so()
        End If
    End Sub

    Private Sub browse_so()
        Dim _col As String = gv_edit_so.FocusedColumn.Name
        Dim _row As Integer = gv_edit_so.FocusedRowHandle

        'Browse PO berdasar kepada entity, patner, currency......
        If _col = "pcso_so_code" Then
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pcs_en_id.EditValue
            frm._ptnr_id = pcs_bill_to.EditValue
            frm._cu_id = pcs_cu_id.EditValue
            frm._obj = gv_edit_so
            frm._ppn_type = pcs_ppn_type.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub sb_retrieve_shipment_item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_receive_item.Click
        Try
            If SetNumber(pcs_pay_prepaid.EditValue) > 0 Then
                Box("Pencatatan Debit Credit Memo DP hanya untuk membukukan DP (belum dengan piutangnya), silahkan buat debit credit memo lagi untuk mencatat piutangnya.")
                Exit Sub
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
        If ds_edit_so.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_so.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim _so_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
            _so_code = _so_code + "'" + ds_edit_so.Tables(0).Rows(i).Item("pcso_so_code") + "',"
        Next

        _so_code = _so_code.Substring(0, _so_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  soshipd_oid, True as ceklist, " _
                        & "  soshipd_soship_oid, " _
                        & "  soshipd_sod_oid, " _
                        & "  soship_code, " _
                        & "  soship_date, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  sod_price,coalesce(sod_qty_shipment,0) as qty_shipment,  " _
                        & "  (sod_price * coalesce(sod_disc,0)) as sod_disc,sod_ppn_type,  " _
                        & "  sod_taxable, " _
                        & "  sod_tax_inc, " _
                        & "  sod_tax_class, " _
                        & "  code_name as sod_tax_class_name, " _
                        & "  ((soshipd_qty * -1) - coalesce(soshipd_qty_inv,0))  as qty_open " _
                        & "FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join so_mstr on so_oid = sod_so_oid " _
                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
                        & "  inner join code_mstr on code_id = sod_tax_class " _
                        & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                        & "  and so_code in (" + _so_code + ")   and sod_ppn_type=" & SetSetring(pcs_ppn_type.EditValue) & " " _
                        & "  order by soship_date "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "soship_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Dim ssql As String
        ssql = "select sum(coalesce(so_shipping_charges,0)) as jml from so_mstr where so_code in (" & _so_code & ")"

        Dim dt_so As New DataTable
        dt_so = GetTableData(ssql)

        For Each dr_so As DataRow In dt_so.Rows
            pcs_shipping_charges.EditValue = dr_so(0)
        Next


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
                _dtrow("pcss_taxable") = ds_bantu.Tables(0).Rows(i).Item("sod_taxable")
                _dtrow("pcss_tax_class_id") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class")
                _dtrow("taxclass_name") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class_name")
                _dtrow("pcss_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc")
                _dtrow("pcss_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("pcss_shipment") = ds_bantu.Tables(0).Rows(i).Item("qty_shipment")
                _dtrow("pcss_invoice") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("pcss_so_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
                _dtrow("pcss_so_disc_value") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                _dtrow("pcss_invoice_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc")
                _dtrow("tot_inv_price") = _dtrow("pcss_invoice_price") * _dtrow("pcss_invoice")
                _dtrow("pcss_close_line") = "N"
                ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
            End If
        Next

        'Dim ssql As String
        ssql = "SELECT  " _
            & "  soship_date " _
            & "FROM  " _
            & "  public.soshipd_det " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "  inner join so_mstr on so_oid = sod_so_oid " _
            & "  inner join pt_mstr on pt_id = sod_pt_id " _
            & "  inner join code_mstr on code_id = sod_tax_class " _
            & "  where coalesce(soshipd_close_line,'N') = 'N' " _
            & "  and so_code in (" + _so_code + ") and soship_is_shipment='Y'   " _
            & "  order by soship_date desc"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)


        pcs_eff_date.DateTime = dt.Rows(0).Item("soship_date")
        '(i) disini pasti line yang terakhir

        ds_edit_shipment.Tables(0).AcceptChanges()

        gv_edit_shipment.BestFitColumns()

    End Sub

    Private Sub retrieve_from_shipment()
        If ds_edit_shipment.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_shipment.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim i, j As Integer

        gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = False
        gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = False
        gv_edit_dist.Columns("taxclass_name").Visible = False
        gv_edit_dist.Columns("pcsd_tax_inc").Visible = False
        gv_edit_dist.Columns("pcsd_taxable").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("pcsd_tax_inc").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("pcsd_remarks").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("pcsd_amount").OptionsColumn.AllowEdit = False

        ds_edit_dist.Tables(0).Clear()
        Dim _search As Boolean = False
        Dim _dtrow As DataRow
        Dim _invoice_price, _line_tr_pph, _line_tr_ppn, _tax_rate As Double
        _invoice_price = 0
        _line_tr_pph = 0
        _line_tr_ppn = 0
        _tax_rate = 0

        'pengulangan tabel dist
        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 Then
                    'Mencari prodline account untuk masing2 line receipt
                    _search = False
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_prodline_account_ar(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                    'pengulangan tabel produk line
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        'If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(0).Item("ac_id")) And _
                        '(ds_edit_dist.Tables(0).Rows(j).Item("pcsd_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id")) Then

                        'jika ketemu dengan akun produk line maka lakukan pencarian
                        If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya line ppn saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * (_invoice_price - _line_tr_ppn)
                            ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _invoice_price
                        Else
                            ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _
                                                                            (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _
                                                                             ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price"))
                            'Exit Sub
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                        _dtrow("pcsd_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("pcsd_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("pcsd_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("pcsd_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_taxable")
                        _dtrow("pcsd_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id")
                        _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                        _dtrow("pcsd_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya dicari ppn nya saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * (_invoice_price - _line_tr_ppn)
                            _dtrow("pcsd_amount") = _invoice_price
                        Else
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")
                            _dtrow("pcsd_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _invoice_price
                        End If


                        _dtrow("pcsd_tax_distribution") = "Y"

                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                    _search = False
                End If
            End If
        Next





        'Untuk PPN dan PPH
        Dim _ppn, _pph As Double
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

                If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("pcss_taxable").ToString.ToUpper = "Y" Then
                    'Mencari taxrate account ar untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_taxrate_pcs_account(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id")))

                    '1. PPN
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next
                    'Exit Sub
                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_sod_cost = ds_edit.Tables(0).Rows(i).Item("sod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_cost") / (1 + _tax_rate)))
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _ppn
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                        If dt_bantu.Rows(0).Item("taxr_ac_sales_id") = 0 Then
                            Box("Tax Class " & ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name") & " dengan Tax Type " & dt_bantu.Rows(0).Item("tax_type_name") & ", belum diseting account AR nya (taxr_ac_pcs_id) " & vbNewLine & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                        End If

                        _dtrow("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("pcsd_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("pcsd_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("pcsd_taxable") = "N"
                        _dtrow("pcsd_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("pcsd_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        _dtrow("pcsd_amount") = _ppn
                        _dtrow("pcsd_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                    _search = False

                    '1. PPH
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _pph
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                        If dt_bantu.Rows(0).Item("taxr_ac_sales_id") = 0 Then
                            Box("Tax Class " & ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name") _
                                & " dengan Tax Type " & dt_bantu.Rows(1).Item("tax_type_name") _
                                & ", belum diseting account AR nya (taxr_ac_pcs_id) " & vbNewLine _
                                & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                        End If

                        _dtrow("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")

                        _dtrow("pcsd_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("pcsd_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("pcsd_taxable") = "N"
                        _dtrow("pcsd_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("pcsd_remarks") = dt_bantu.Rows(1).Item("ac_name")
                        If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                            '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        _dtrow("pcsd_amount") = _pph
                        _dtrow("pcsd_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                    _search = False
                End If
            End If
        Next

        'Ini untuk ar discount
        _search = False
        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value") > 0 Then
                        'Mencari prodline account untuk masing2 line receipt
                        _search = False
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_prodline_account_pcs_discount(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya line ppn saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * (_invoice_price - _line_tr_ppn)
                                ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi

                                ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _
                                                                                (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _
                                                                                 _invoice_price)
                                ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("pcsd_oid") = Guid.NewGuid.ToString


                            _dtrow("pcsd_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("pcsd_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("pcsd_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("pcsd_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_taxable")
                            _dtrow("pcsd_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id")
                            _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                            _dtrow("pcsd_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya dicari ppn nya saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * (_invoice_price - _line_tr_ppn)
                                _dtrow("pcsd_amount") = _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1
                                _dtrow("pcsd_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _invoice_price
                            End If


                            _dtrow("pcsd_tax_distribution") = "Y"

                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                        _search = False
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH yang ar discount
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("pcss_taxable").ToString.ToUpper = "Y" Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value") > 0 Then
                        'Mencari taxrate account ap untuk masing2 line receipt
                        _search = False
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_taxrate_pcs_account(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id")))

                        '1. PPN
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _ppn
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                            If dt_bantu.Rows(0).Item("taxr_ac_sales_id") = 0 Then
                                Box("Tax Class " & ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name") _
                                    & " dengan Tax Type " & dt_bantu.Rows(0).Item("tax_type_name") _
                                    & ", belum diseting account AR nya (taxr_ac_pcs_id) " & vbNewLine _
                                    & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                            End If

                            _dtrow("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("pcsd_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("pcsd_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("pcsd_taxable") = "N"
                            _dtrow("pcsd_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("pcsd_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            _dtrow("pcsd_amount") = _ppn
                            _dtrow("pcsd_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                        _search = False

                        '1. PPH
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")

                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _pph
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                            If dt_bantu.Rows(0).Item("taxr_ac_sales_id") = 0 Then
                                Box("Tax Class " & ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name") _
                                    & " dengan Tax Type " & dt_bantu.Rows(1).Item("tax_type_name") _
                                    & ", belum diseting account AR nya (taxr_ac_pcs_id) " & vbNewLine _
                                    & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                            End If

                            _dtrow("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                            _dtrow("pcsd_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("pcsd_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("pcsd_taxable") = "N"
                            _dtrow("pcsd_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("pcsd_remarks") = dt_bantu.Rows(1).Item("ac_name")
                            If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc").ToString.ToUpper = "Y" Then
                                '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi

                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            End If

                            _dtrow("pcsd_amount") = _pph
                            _dtrow("pcsd_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                        _search = False
                    End If
                End If
            End If
        Next
        '**************************************************

        'Insert ke ds untuk yang prepayment kalau ada
        'If pcs_pay_prepaid.EditValue < 0 Then
        '    _dtrow = ds_edit_dist.Tables(0).NewRow
        '    _dtrow("pcsd_oid") = Guid.NewGuid.ToString

        '    _dtrow("pcsd_ac_id") = pcs_ac_prepaid.EditValue
        '    _dtrow("ac_code") = pcs_ac_prepaid.GetColumnValue("ac_code")

        '    _dtrow("pcsd_sb_id") = 0
        '    _dtrow("sb_desc") = "-"
        '    _dtrow("pcsd_cc_id") = 0
        '    _dtrow("cc_desc") = "-"
        '    _dtrow("pcsd_taxable") = "N"
        '    _dtrow("pcsd_tax_class_id") = DBNull.Value
        '    _dtrow("taxclass_name") = DBNull.Value
        '    _dtrow("pcsd_remarks") = pcs_ac_prepaid.GetColumnValue("ac_name")
        '    _dtrow("pcsd_amount") = pcs_pay_prepaid.EditValue
        '    _dtrow("pcsd_tax_distribution") = "Y"
        '    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
        '    ds_edit_dist.Tables(0).AcceptChanges()
        '    '**********************************************************
        'End If

        If pcs_pay_prepaid.EditValue <> 0 Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.get_taxrate_pcs_account(pcs_tax_class_id.EditValue))

            If pcs_taxable.EditValue = True Then
                If pcs_tax_inc.EditValue = True Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (pcs_pay_prepaid.EditValue / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = pcs_pay_prepaid.EditValue - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (pcs_pay_prepaid.EditValue) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'ini mengacu ke pph
                    _ppn = (pcs_pay_prepaid.EditValue) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If
            End If

            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("pcsd_oid") = Guid.NewGuid.ToString

            _dtrow("pcsd_ac_id") = pcs_ac_prepaid.EditValue
            _dtrow("ac_code") = pcs_ac_prepaid.GetColumnValue("ac_code")
            _dtrow("ac_name") = pcs_ac_prepaid.GetColumnValue("ac_name")

            _dtrow("pcsd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("pcsd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("pcsd_taxable") = IIf(pcs_taxable.EditValue = True, "Y", "N")
            _dtrow("pcsd_tax_inc") = IIf(pcs_tax_inc.EditValue = True, "Y", "N")
            _dtrow("pcsd_tax_class_id") = IIf(pcs_taxable.EditValue = True, pcs_tax_class_id.EditValue, DBNull.Value)
            _dtrow("taxclass_name") = IIf(pcs_taxable.EditValue = True, pcs_tax_class_id.GetColumnValue("code_name"), DBNull.Value)
            _dtrow("pcsd_remarks") = pcs_ac_prepaid.GetColumnValue("ac_name")

            If pcs_tax_inc.EditValue = True Then
                _dtrow("pcsd_amount") = pcs_pay_prepaid.EditValue - _ppn
            Else
                _dtrow("pcsd_amount") = pcs_pay_prepaid.EditValue
            End If


            _dtrow("pcsd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()

            '1. PPN
            _search = False
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _ppn
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                If dt_bantu.Rows(0).Item("taxr_ac_sales_id") = 0 Then
                    Box("Tax Class " & ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name") _
                        & " dengan Tax Type " & dt_bantu.Rows(0).Item("tax_type_name") _
                        & ", belum diseting account AR nya (taxr_ac_pcs_id) " & vbNewLine _
                        & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                End If

                _dtrow("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                _dtrow("pcsd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("pcsd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("pcsd_taxable") = "N"
                _dtrow("pcsd_tax_inc") = "N"
                _dtrow("pcsd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("pcsd_remarks") = dt_bantu.Rows(0).Item("ac_name")

                _dtrow("pcsd_amount") = _ppn
                _dtrow("pcsd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If

            _search = False
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _pph
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                If dt_bantu.Rows(0).Item("taxr_ac_sales_id") = 0 Then
                    Box("Tax Class " & ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name") _
                        & " dengan Tax Type " & dt_bantu.Rows(1).Item("tax_type_name") _
                        & ", belum diseting account AR nya (taxr_ac_pcs_id) " & vbNewLine _
                        & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                End If
                _dtrow("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                _dtrow("pcsd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("pcsd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("pcsd_taxable") = "N"
                _dtrow("pcsd_tax_inc") = "N"
                _dtrow("pcsd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("pcsd_remarks") = dt_bantu.Rows(1).Item("ac_name")

                _dtrow("pcsd_amount") = _pph
                _dtrow("pcsd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If
            _search = False
        End If


        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()

        gv_edit_dist.BestFitColumns()
    End Sub

    Private Sub recalculate()
        Dim _dtrow As DataRow
        Dim i, j As Integer
        Dim ds_copy As New DataSet
        Dim _search As Boolean
        Dim _ppn, _pph, _tax_rate As Double

        ds_edit_dist.AcceptChanges()
        ds_copy = ds_edit_dist.Copy
        _ppn = 0
        _pph = 0

        'ini untuk update line paling awal
        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            If ds_edit_dist.Tables(0).Rows(i).Item("pcsd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_tax_class_id"))
                If ds_edit_dist.Tables(0).Rows(i).Item("pcsd_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                    _ppn = _tax_rate * (ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount") / (1 + _tax_rate))
                    ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount") - _ppn
                    ds_edit_dist.AcceptChanges()
                Else
                    _ppn = ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount") * _tax_rate
                End If
            End If
        Next

        For i = 0 To ds_copy.Tables(0).Rows.Count - 1
            dt_bantu = New DataTable
            dt_bantu = (func_data.get_taxrate_pcs_account(ds_copy.Tables(0).Rows(i).Item("pcsd_tax_class_id")))

            '1. PPN
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPN
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                If ds_copy.Tables(0).Rows(i).Item("pcsd_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("pcsd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                Else
                    _ppn = (ds_copy.Tables(0).Rows(i).Item("pcsd_amount")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If

                ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _ppn
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                _dtrow("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                _dtrow("pcsd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("pcsd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("pcsd_taxable") = "N"
                _dtrow("pcsd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("pcsd_remarks") = dt_bantu.Rows(0).Item("ac_name")

                If ds_copy.Tables(0).Rows(i).Item("pcsd_tax_inc").ToString.ToUpper = "Y" Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("pcsd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                Else
                    _ppn = (ds_copy.Tables(0).Rows(i).Item("pcsd_amount")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If

                _dtrow("pcsd_amount") = _ppn
                _dtrow("pcsd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If
            _search = False
            '2. PPH
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                If ds_copy.Tables(0).Rows(i).Item("pcsd_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("pcsd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ds_copy.Tables(0).Rows(i).Item("pcsd_amount") - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ds_copy.Tables(0).Rows(i).Item("pcsd_amount")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'row(1) artinya mengacu ke pph
                End If

                ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("pcsd_amount") + _pph
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("pcsd_oid") = Guid.NewGuid.ToString

                _dtrow("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")

                _dtrow("pcsd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("pcsd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("pcsd_taxable") = "N"
                _dtrow("pcsd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("pcsd_remarks") = dt_bantu.Rows(1).Item("ac_name")
                If ds_copy.Tables(0).Rows(i).Item("pcsd_tax_inc").ToString.ToUpper = "Y" Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("pcsd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ds_copy.Tables(0).Rows(i).Item("pcsd_amount") - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ds_copy.Tables(0).Rows(i).Item("pcsd_amount")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'ini mengacu ke pph
                End If

                _dtrow("pcsd_amount") = _pph
                _dtrow("pcsd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If
            _search = False
        Next

        'Insert ke ds untuk yang prepayment kalau ada
        'If pcs_pay_prepaid.EditValue <> 0 Then
        '    _dtrow = ds_edit_dist.Tables(0).NewRow
        '    _dtrow("pcsd_oid") = Guid.NewGuid.ToString

        '    _dtrow("pcsd_ac_id") = pcs_ac_prepaid.EditValue
        '    _dtrow("ac_code") = pcs_ac_prepaid.GetColumnValue("ac_code")

        '    _dtrow("pcsd_sb_id") = 0
        '    _dtrow("sb_desc") = "-"
        '    _dtrow("pcsd_cc_id") = 0
        '    _dtrow("cc_desc") = "-"
        '    _dtrow("pcsd_taxable") = "N"
        '    _dtrow("pcsd_tax_class_id") = DBNull.Value
        '    _dtrow("taxclass_name") = DBNull.Value
        '    _dtrow("pcsd_remarks") = pcs_ac_prepaid.GetColumnValue("ac_name")
        '    _dtrow("pcsd_amount") = pcs_pay_prepaid.EditValue
        '    _dtrow("pcsd_tax_distribution") = "Y"
        '    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
        '    ds_edit_dist.Tables(0).AcceptChanges()
        '    '**********************************************************
        'End If

        If pcs_pay_prepaid.EditValue <> 0 Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.get_taxrate_pcs_account(pcs_tax_class_id.EditValue))

            If pcs_taxable.EditValue = True Then
                If pcs_tax_inc.EditValue = True Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (pcs_pay_prepaid.EditValue / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = pcs_pay_prepaid.EditValue - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (pcs_pay_prepaid.EditValue) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'ini mengacu ke pph
                    _ppn = (pcs_pay_prepaid.EditValue) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If
            End If

            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("pcsd_oid") = Guid.NewGuid.ToString

            _dtrow("pcsd_ac_id") = pcs_ac_prepaid.EditValue
            _dtrow("ac_code") = pcs_ac_prepaid.GetColumnValue("ac_code")
            _dtrow("ac_name") = pcs_ac_prepaid.GetColumnValue("ac_name")

            _dtrow("pcsd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("pcsd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("pcsd_taxable") = IIf(pcs_taxable.EditValue = True, "Y", "N")
            _dtrow("pcsd_tax_inc") = IIf(pcs_tax_inc.EditValue = True, "Y", "N")
            _dtrow("pcsd_tax_class_id") = IIf(pcs_taxable.EditValue = True, pcs_tax_class_id.EditValue, DBNull.Value)
            _dtrow("taxclass_name") = IIf(pcs_taxable.EditValue = True, pcs_tax_class_id.GetColumnValue("code_name"), DBNull.Value)
            _dtrow("pcsd_remarks") = pcs_ac_prepaid.GetColumnValue("ac_name")

            If pcs_tax_inc.EditValue = True Then
                _dtrow("pcsd_amount") = pcs_pay_prepaid.EditValue - _ppn
            Else
                _dtrow("pcsd_amount") = pcs_pay_prepaid.EditValue
            End If


            _dtrow("pcsd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()


            '1. PPN
            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("pcsd_oid") = Guid.NewGuid.ToString

            _dtrow("pcsd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

            _dtrow("pcsd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("pcsd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("pcsd_taxable") = "N"
            _dtrow("pcsd_tax_inc") = "N"
            _dtrow("pcsd_tax_class_id") = DBNull.Value
            _dtrow("taxclass_name") = DBNull.Value
            _dtrow("pcsd_remarks") = dt_bantu.Rows(0).Item("ac_name")

            _dtrow("pcsd_amount") = _ppn
            _dtrow("pcsd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()

            '2. PPH

            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("pcsd_oid") = Guid.NewGuid.ToString

            _dtrow("pcsd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
            _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
            _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

            _dtrow("pcsd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("pcsd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("pcsd_taxable") = "N"
            _dtrow("pcsd_tax_inc") = "N"
            _dtrow("pcsd_tax_class_id") = DBNull.Value
            _dtrow("taxclass_name") = DBNull.Value
            _dtrow("pcsd_remarks") = dt_bantu.Rows(1).Item("ac_name")

            _dtrow("pcsd_amount") = _pph
            _dtrow("pcsd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()
        End If

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()

        'gc_edit_dist.Enabled = False
        'sb_retrieve_dist.Enabled = False
        'setelah di recalculate tidak boleh lagi diubah karena sudah hitung tax dan lain sebagainya...
        'kalau salah di cancel aja terus buat lagi dari awal
    End Sub

    Private Sub sb_retrieve_dist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_dist.Click
        If ds_edit_shipment.Tables(0).Rows.Count = 0 Then
            recalculate()
        Else
            retrieve_from_shipment()
        End If
    End Sub

    Private Sub gv_edit_dist_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_dist.CellValueChanged
        If e.Column.Name = "pcsd_taxable" Then
            If gv_edit_dist.GetRowCellValue(e.RowHandle, "pcsd_taxable").ToString.ToUpper = "N" Then
                gv_edit_dist.SetRowCellValue(e.RowHandle, "pcsd_tax_inc", "N")
                gv_edit_dist.SetRowCellValue(e.RowHandle, "pcsd_tax_class_id", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit_dist.SetRowCellValue(e.RowHandle, "taxclass_name", "NON-TAX")
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "pcsd_recalculate", False)
            ElseIf gv_edit_dist.GetRowCellValue(e.RowHandle, "pcsd_taxable").ToString.ToUpper = "Y" Then
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "pcsd_recalculate", True)
            End If
        ElseIf e.Column.Name = "pcsd_tax_inc" Then
            If gv_edit_dist.GetRowCellValue(e.RowHandle, "pcsd_tax_inc").ToString.ToUpper = "Y" And gv_edit_dist.GetRowCellValue(e.RowHandle, "pcsd_taxable").ToString.ToUpper = "N" Then
                gv_edit_dist.SetRowCellValue(e.RowHandle, "pcsd_tax_inc", "N")
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "pcsd_recalculate", False)
            End If
        ElseIf e.Column.Name = "pcsd_amount" Then
            'gv_edit_dist.SetRowCellValue(e.RowHandle, "pcsd_recalculate", True)
        End If
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_dist.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_dist.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_dist.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_dist.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_dist.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit_dist.FocusedColumn.Name
        Dim _row As Integer = gv_edit_dist.FocusedRowHandle

        If _col = "ac_code" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = pcs_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = pcs_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "taxclass_name" Then
            If gv_edit_dist.GetRowCellValue(_row, "pcsd_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = pcs_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_dist_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_dist.InitNewRow
        gv_edit_dist.Columns("taxclass_name").Visible = True
        gv_edit_dist.Columns("pcsd_tax_inc").Visible = True
        'sb_retrieve_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = False

        With gv_edit_dist
            .SetRowCellValue(e.RowHandle, "pcsd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "pcsd_taxable", IIf(pcs_taxable.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "pcsd_tax_class_id", pcs_tax_class_id.EditValue)
            .SetRowCellValue(e.RowHandle, "taxclass_name", pcs_tax_class_id.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "pcsd_tax_inc", IIf(pcs_tax_inc.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "pcsd_amount", 0)
            .SetRowCellValue(e.RowHandle, "pcsd_tax_distribution", "N")

            .SetRowCellValue(e.RowHandle, "pcsd_sb_id", 0)
            .SetRowCellValue(e.RowHandle, "sb_desc", "-")
            .SetRowCellValue(e.RowHandle, "pcsd_cc_id", 0)
            .SetRowCellValue(e.RowHandle, "cc_desc", "-")
            .BestFitColumns()
        End With
    End Sub

    Private Sub pcs_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcs_taxable.CheckedChanged
        If pcs_taxable.EditValue = False Then
            pcs_tax_class_id.Enabled = False
            pcs_tax_class_id.ItemIndex = 0
            pcs_tax_inc.Enabled = False
            pcs_tax_inc.Checked = False
        Else
            pcs_tax_class_id.Enabled = True
            pcs_tax_inc.Enabled = True
            pcs_tax_inc.Checked = False
        End If
    End Sub

    Private Sub be_so_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_so_code.ButtonClick
        'Dim frm As New FSalesOrderSearch
        'frm.set_win(Me)
        'frm._en_id = pcs_en_id.EditValue
        'frm._obj = be_so_code
        'frm.type_form = True
        'frm.ShowDialog()
    End Sub

    Private Sub pcs_credit_term_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcs_credit_term.EditValueChanged
        pcs_due_date.DateTime = pcs_eff_date.DateTime.AddDays(pcs_credit_term.GetColumnValue("code_usr_1"))
        pcs_expt_date.DateTime = pcs_eff_date.DateTime.AddDays(pcs_credit_term.GetColumnValue("code_usr_1"))
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pcs_oid As Guid
        _pcs_oid = Guid.NewGuid

        Dim _pcs_code, _pcs_terbilang As String
        Dim _pcs_amount As Double = 0
        Dim _prepaid As Double = 0
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        _pcs_code = func_coll.get_transaction_number("PCS", pcs_en_id.GetColumnValue("en_code"), "pcs_mstr", "pcs_code")

        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            _pcs_amount = _pcs_amount + ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount")
        Next
        _pcs_terbilang = func_bill.TERBILANG_FIX(_pcs_amount)

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
                                            & "  public.pcs_mstr " _
                                            & "( " _
                                            & "  pcs_oid, " _
                                            & "  pcs_dom_id, " _
                                            & "  pcs_en_id, " _
                                            & "  pcs_add_by, " _
                                            & "  pcs_add_date, " _
                                            & "  pcs_code, " _
                                            & "  pcs_date, " _
                                            & "  pcs_bill_to, " _
                                            & "  pcs_cu_id, " _
                                            & "  pcs_exc_rate, " _
                                            & "  pcs_credit_term, pcs_shipping_charges,pcs_total_final, " _
                                            & "  pcs_eff_date, " _
                                            & "  pcs_disc_date, " _
                                            & "  pcs_expt_date, " _
                                            & "  pcs_ac_id, " _
                                            & "  pcs_sb_id, " _
                                            & "  pcs_cc_id, " _
                                            & "  pcs_type, " _
                                            & "  pcs_ppn_type, " _
                                            & "  pcs_taxable, " _
                                            & "  pcs_tax_inc, " _
                                            & "  pcs_tax_class_id, " _
                                            & "  pcs_ac_prepaid, " _
                                            & "  pcs_pay_prepaid, " _
                                            & "  pcs_amount, " _
                                            & "  pcs_pay_amount, " _
                                            & "  pcs_remarks, " _
                                            & "  pcs_status, " _
                                            & "  pcs_dt, " _
                                            & "  pcs_bk_id, " _
                                            & "  pcs_due_date, " _
                                            & "  pcs_terbilang " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pcs_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pcs_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_pcs_code) & ",  " _
                                            & " " & SetDateNTime00(pcs_eff_date.DateTime) & " " & ",  " _
                                            & SetInteger(pcs_bill_to.EditValue) & ",  " _
                                            & SetInteger(pcs_cu_id.EditValue) & ",  " _
                                            & SetDec(pcs_exc_rate.EditValue) & ",  " _
                                            & SetInteger(pcs_credit_term.EditValue) & ",  " _
                                            & SetDec(pcs_shipping_charges.EditValue) & ",  " _
                                            & SetDec(SetNumber(pcs_shipping_charges.EditValue) + SetNumber(_pcs_amount)) & ",  " _
                                            & SetDate(pcs_eff_date.DateTime) & ",  " _
                                            & "null" & ",  " _
                                            & SetDate(pcs_expt_date.DateTime) & ",  " _
                                            & SetInteger(pcs_ac_id.EditValue) & ",  " _
                                            & SetInteger(pcs_sb_id.EditValue) & ",  " _
                                            & SetInteger(pcs_cc_id.EditValue) & ",  " _
                                            & SetInteger(pcs_type.EditValue) & ",  " _
                                            & SetSetring(pcs_ppn_type.EditValue) & ",  " _
                                            & SetBitYN(pcs_taxable.EditValue) & ",  " _
                                            & SetBitYN(pcs_tax_inc.EditValue) & ",  " _
                                            & SetInteger(pcs_tax_class_id.EditValue) & ",  " _
                                            & SetInteger(pcs_ac_prepaid.EditValue) & ",  " _
                                            & SetDec(pcs_pay_prepaid.EditValue) & ",  " _
                                            & SetDec(_pcs_amount) & ",  " _
                                            & "0" & ",  " _
                                            & SetSetring(pcs_remarks.Text) & ",  " _
                                            & "null" & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(pcs_bk_id.EditValue) & ",  " _
                                            & SetDate(pcs_due_date.DateTime) & ",  " _
                                            & SetSetring(_pcs_terbilang) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()


                        Dim newssqls As New ArrayList
                        If pcs_pay_prepaid.EditValue > 0 Then
                            'update rekonsiliasi kas masuk
                            If update_rec(newssqls, pcs_en_id.EditValue, pcs_bk_id.EditValue, pcs_cu_id.EditValue, _
                                         pcs_exc_rate.EditValue, pcs_pay_prepaid.EditValue, pcs_eff_date.DateTime, _pcs_code, _
                                         pcs_bill_to.Text & " " & pcs_remarks.Text, "PREPAYMENT") = False Then
                                'sqlTran.Rollback()
                                insert = False
                                Exit Function
                            Else
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = newssqls(0).ToString
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If
                        End If


                        'Untuk Insert Data List so
                        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pcso_so " _
                                                & "( " _
                                                & "  pcso_oid, " _
                                                & "  pcso_pcs_oid, " _
                                                & "  pcso_seq, " _
                                                & "  pcso_so_oid, " _
                                                & "  pcso_so_code, " _
                                                & "  pcso_so_date, " _
                                                & "  pcso_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("pcso_oid").ToString) & ",  " _
                                                & SetSetring(_pcs_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("pcso_so_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("pcso_so_code").ToString) & ",  " _
                                                & SetDate(ds_edit_so.Tables(0).Rows(i).Item("pcso_so_date")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List shipment
                        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                                If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.pcss_ship " _
                                                        & "( " _
                                                        & "  pcss_oid, " _
                                                        & "  pcss_pcs_oid, " _
                                                        & "  pcss_seq, " _
                                                        & "  pcss_soshipd_oid, " _
                                                        & "  pcss_taxable, " _
                                                        & "  pcss_tax_class_id, " _
                                                        & "  pcss_tax_inc, " _
                                                        & "  pcss_open,pcss_shipment, " _
                                                        & "  pcss_invoice, " _
                                                        & "  pcss_so_price, " _
                                                        & "  pcss_so_disc_value, " _
                                                        & "  pcss_invoice_price, " _
                                                        & "  pcss_close_line, " _
                                                        & "  pcss_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_oid").ToString) & ",  " _
                                                        & SetSetring(_pcs_oid.ToString) & ",  " _
                                                        & SetInteger(i) & ",  " _
                                                        & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_soshipd_oid").ToString) & ",  " _
                                                        & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_taxable")) & ",  " _
                                                        & SetInteger(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_class_id")) & ",  " _
                                                        & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_tax_inc")) & ",  " _
                                                        & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_open")) & ",  " _
                                                        & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_shipment")) & ",  " _
                                                        & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice")) & ",  " _
                                                        & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_price")) & ",  " _
                                                        & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_so_disc_value")) & ",  " _
                                                        & SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice_price")) & ",  " _
                                                        & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_close_line")) & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                        & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    'Update Table shipment Detail untuk kolom shipd_qty_inv dan shipd_close_line nya
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update soshipd_det set soshipd_qty_inv = coalesce(soshipd_qty_inv,0) + " + SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice").ToString) + _
                                                           ", soshipd_close_line = '" + ds_edit_shipment.Tables(0).Rows(i).Item("pcss_close_line") + "'" + _
                                                           " where soshipd_oid = '" + ds_edit_shipment.Tables(0).Rows(i).Item("pcss_soshipd_oid") + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    If ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice") <> 0 Then
                                        'Update Table so Detail untuk kolom sod_qty_invoice
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update sod_det set sod_qty_invoice = coalesce(sod_qty_invoice,0) + " + SetDec(ds_edit_shipment.Tables(0).Rows(i).Item("pcss_invoice").ToString) + _
                                                               " where sod_oid = (select soshipd_sod_oid from soshipd_det where soshipd_oid = '" + ds_edit_shipment.Tables(0).Rows(i).Item("pcss_soshipd_oid") + "')"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()
                                    End If
                                End If
                            End If
                        Next




                        'Untuk Insert Distribution Line nya
                        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pcsd_dist " _
                                                & "( " _
                                                & "  pcsd_oid, " _
                                                & "  pcsd_pcs_oid, " _
                                                & "  pcsd_tax_distribution, " _
                                                & "  pcsd_taxable, " _
                                                & "  pcsd_tax_class_id, " _
                                                & "  pcsd_ac_id, " _
                                                & "  pcsd_sb_id, " _
                                                & "  pcsd_cc_id, " _
                                                & "  pcsd_amount, " _
                                                & "  pcsd_remarks, " _
                                                & "  pcsd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_oid").ToString) & ",  " _
                                                & SetSetring(_pcs_oid.ToString) & ",  " _
                                                & SetSetringDB(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_tax_distribution")) & ",  " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_taxable")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_tax_class_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_ac_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_sb_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_cc_id")) & ",  " _
                                                & SetDec(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount")) & ",  " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("pcsd_remarks")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        'Update Prepayment Balance.....
                        If pcs_pay_prepaid.EditValue <> 0 Then
                            For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                                If pcs_ac_prepaid.EditValue = ds_edit_dist.Tables(0).Rows(i).Item("pcsd_ac_id") Then
                                    _prepaid = ds_edit_dist.Tables(0).Rows(i).Item("pcsd_amount")
                                End If
                            Next

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_prepaid_balance = coalesce(ptnr_prepaid_balance,0) + " + SetDec(_prepaid.ToString) + _
                                                   " where ptnr_id = " + pcs_bill_to.EditValue.ToString
                            '.Command.CommandText = "update ptnr_mstr set ptnr_prepaid_balance = coalesce(ptnr_prepaid_balance,0) + " + pcs_pay_prepaid.EditValue.ToString + _
                            '                       " where ptnr_id = " + pcs_bill_to.EditValue.ToString
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        End If

                        'Insert Jurnal
                        If _create_jurnal = True Then
                            If insert_glt_det_ar(ssqls, objinsert, ds_edit_dist, _
                                                       pcs_en_id.EditValue, pcs_en_id.GetColumnValue("en_code"), _
                                                       _pcs_oid.ToString, _pcs_code, _
                                                       pcs_eff_date.DateTime, _
                                                       pcs_cu_id.EditValue, pcs_exc_rate.EditValue, _
                                                       "AR", "AR-INV", _
                                                       pcs_ac_id.EditValue, pcs_sb_id.EditValue, pcs_cc_id.EditValue, _
                                                       pcs_ac_id.GetColumnValue("ac_name"), _
                                                       _pcs_amount) = False Then
                                'sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If
                        '************************************************

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, pcs_en_id.EditValue, 13, _pcs_oid.ToString, _pcs_code, pcs_eff_date.DateTime) = False Then
                        '    ''sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Debit Credit Memo " & _pcs_code)
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
                        set_row(_pcs_oid.ToString, "pcs_oid")
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

    Private Function insert_glt_det_ar(ByVal ppcs_ssqls As ArrayList, ByVal ppcs_obj As Object, ByVal ppcs_ds As DataSet, _
                                   ByVal ppcs_en_id As Integer, ByVal ppcs_en_code As String, _
                                   ByVal ppcs_oid As String, ByVal ppcs_trans_code As String, _
                                   ByVal ppcs_date As Date, ByVal ppcs_cu_id As Integer, _
                                   ByVal ppcs_exc_rate As Double, _
                                   ByVal ppcs_type As String, ByVal ppcs_daybook As String, _
                                   ByVal ppcs_ac_id As Integer, ByVal ppcs_sb_id As Integer, _
                                   ByVal ppcs_cc_id As Integer, _
                                   ByVal ppcs_desc As String, ByVal ppcs_amount As Double) As Boolean

        insert_glt_det_ar = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number(ppcs_type, ppcs_en_code, "glt_det", "glt_code")

        'Insert Untuk Yang Debet dan Credit, Looping dari dataset
        For i = 0 To ppcs_ds.Tables(0).Rows.Count - 1
            If ppcs_ds.Tables(0).Rows(i).Item("pcsd_amount") > 0 Then
                With ppcs_obj
                    Try
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ppcs_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(ppcs_date) & ",  " _
                                            & SetSetring(ppcs_type) & ",  " _
                                            & SetInteger(ppcs_cu_id) & ",  " _
                                            & SetDbl(ppcs_exc_rate) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(ppcs_ds.Tables(0).Rows(i).Item("pcsd_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_remarks")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_amount")) & ",  " _
                                            & SetSetring(ppcs_oid) & ",  " _
                                            & SetSetring(ppcs_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(ppcs_daybook) & "  " _
                                            & ")"
                        ppcs_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(ppcs_ssqls, ppcs_obj, ppcs_date, _
                                                             ppcs_ds.Tables(0).Rows(i).Item("pcsd_ac_id"), _
                                                             SetIntegerDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_sb_id")), _
                                                             SetIntegerDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_cc_id")), _
                                                             ppcs_en_id, ppcs_cu_id, _
                                                             ppcs_exc_rate, ppcs_ds.Tables(0).Rows(i).Item("pcsd_amount"), "C") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            ElseIf ppcs_ds.Tables(0).Rows(i).Item("pcsd_amount") < 0 Then
                With ppcs_obj
                    Try
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ppcs_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(ppcs_date) & ",  " _
                                            & SetSetring(ppcs_type) & ",  " _
                                            & SetInteger(ppcs_cu_id) & ",  " _
                                            & SetDbl(ppcs_exc_rate) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(ppcs_ds.Tables(0).Rows(i).Item("pcsd_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_remarks")) & ",  " _
                                            & SetDblDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_amount") * -1) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(ppcs_oid) & ",  " _
                                            & SetSetring(ppcs_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(ppcs_daybook) & "  " _
                                            & ")"
                        ppcs_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(ppcs_ssqls, ppcs_obj, ppcs_date, _
                                                             ppcs_ds.Tables(0).Rows(i).Item("pcsd_ac_id"), _
                                                             SetIntegerDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_sb_id")), _
                                                             SetIntegerDB(ppcs_ds.Tables(0).Rows(i).Item("pcsd_cc_id")), _
                                                             ppcs_en_id, ppcs_cu_id, _
                                                             ppcs_exc_rate, ppcs_ds.Tables(0).Rows(i).Item("pcsd_amount") * -1, "D") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            End If
        Next

        With ppcs_obj
            Try
                'Insert untuk yang credit yang account hutang nya
                '.Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.glt_det " _
                                    & "( " _
                                    & "  glt_oid, " _
                                    & "  glt_dom_id, " _
                                    & "  glt_en_id, " _
                                    & "  glt_add_by, " _
                                    & "  glt_add_date, " _
                                    & "  glt_code, " _
                                    & "  glt_date, " _
                                    & "  glt_type, " _
                                    & "  glt_cu_id, " _
                                    & "  glt_exc_rate, " _
                                    & "  glt_seq, " _
                                    & "  glt_ac_id, " _
                                    & "  glt_sb_id, " _
                                    & "  glt_cc_id, " _
                                    & "  glt_desc, " _
                                    & "  glt_debit, " _
                                    & "  glt_credit, " _
                                    & "  glt_ref_oid, " _
                                    & "  glt_ref_trans_code, " _
                                    & "  glt_posted, " _
                                    & "  glt_dt, " _
                                    & "  glt_daybook " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(ppcs_en_id) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(_glt_code) & ",  " _
                                    & SetDate(ppcs_date) & ",  " _
                                    & SetSetring(ppcs_type) & ",  " _
                                    & SetInteger(ppcs_cu_id) & ",  " _
                                    & SetDbl(ppcs_exc_rate) & ",  " _
                                    & SetInteger(i) & ",  " _
                                    & SetInteger(ppcs_ac_id) & ",  " _
                                    & SetIntegerDB(ppcs_sb_id) & ",  " _
                                    & SetIntegerDB(ppcs_cc_id) & ",  " _
                                    & SetSetringDB(ppcs_desc) & ",  " _
                                    & SetDblDB(ppcs_amount) & ",  " _
                                    & SetDblDB(0) & ",  " _
                                    & SetSetring(ppcs_oid) & ",  " _
                                    & SetSetring(ppcs_trans_code) & ",  " _
                                    & SetSetring("N") & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(ppcs_daybook) & "  " _
                                    & ")"
                ppcs_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                '.Command.Parameters.Clear()

                If func_coll.update_unposted_glbal_balance(ppcs_ssqls, ppcs_obj, ppcs_date, _
                                                 ppcs_ac_id, _
                                                 ppcs_sb_id, _
                                                 ppcs_cc_id, _
                                                 ppcs_en_id, ppcs_cu_id, _
                                                 ppcs_exc_rate, ppcs_amount, "D") = False Then

                    Return False
                    Exit Function
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With


        'insert ongkos kirim
        If SetNumber(pcs_shipping_charges.EditValue) > 0 Then

            Dim ssql As String

            ssql = "select confa_ac_id from confa_account where confa_code ='shipping_charges_account_debit' "

            Dim dt_acc As New DataTable
            dt_acc = GetTableData(ssql)

            Dim _ac_id As Integer = 0
            For Each dr_acc As DataRow In dt_acc.Rows
                _ac_id = dr_acc(0)
            Next

            If _ac_id = 0 Then
                MsgBox("Please setting shipping_charges_account_debit")
                Return False
                Exit Function
            End If


            ssql = "select confa_ac_id from confa_account where confa_code ='shipping_charges_account_credit'"

            'Dim dt_acc As New DataTable
            dt_acc = GetTableData(ssql)

            Dim _ac_id_c As Integer = 0
            For Each dr_acc As DataRow In dt_acc.Rows
                _ac_id_c = dr_acc(0)
            Next

            If _ac_id_c = 0 Then
                MsgBox("Please setting shipping_charges_account_credit")
                Return False
                Exit Function
            End If

            With ppcs_obj
                Try
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.glt_det " _
                                        & "( " _
                                        & "  glt_oid, " _
                                        & "  glt_dom_id, " _
                                        & "  glt_en_id, " _
                                        & "  glt_add_by, " _
                                        & "  glt_add_date, " _
                                        & "  glt_code, " _
                                        & "  glt_date, " _
                                        & "  glt_type, " _
                                        & "  glt_cu_id, " _
                                        & "  glt_exc_rate, " _
                                        & "  glt_seq, " _
                                        & "  glt_ac_id, " _
                                        & "  glt_sb_id, " _
                                        & "  glt_cc_id, " _
                                        & "  glt_desc, " _
                                        & "  glt_debit, " _
                                        & "  glt_credit, " _
                                        & "  glt_ref_oid, " _
                                        & "  glt_ref_trans_code, " _
                                        & "  glt_posted, " _
                                        & "  glt_dt, " _
                                        & "  glt_daybook " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(ppcs_en_id) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(ppcs_date) & ",  " _
                                        & SetSetring(ppcs_type) & ",  " _
                                        & SetInteger(ppcs_cu_id) & ",  " _
                                        & SetDbl(ppcs_exc_rate) & ",  " _
                                        & SetInteger(i + 1) & ",  " _
                                        & SetInteger(_ac_id) & ",  " _
                                        & SetIntegerDB(ppcs_sb_id) & ",  " _
                                        & SetIntegerDB(ppcs_cc_id) & ",  " _
                                        & SetSetringDB("Ongkos kirim") & ",  " _
                                        & SetDblDB(pcs_shipping_charges.EditValue) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetSetring(ppcs_oid) & ",  " _
                                        & SetSetring(ppcs_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(ppcs_daybook) & "  " _
                                        & ")"
                    ppcs_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    '.Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance(ppcs_ssqls, ppcs_obj, ppcs_date, _
                                                _ac_id, _
                                                ppcs_sb_id, _
                                                ppcs_cc_id, _
                                                ppcs_en_id, ppcs_cu_id, _
                                                ppcs_exc_rate, pcs_shipping_charges.EditValue, "D") = False Then

                        Return False
                        Exit Function
                    End If


                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.glt_det " _
                                        & "( " _
                                        & "  glt_oid, " _
                                        & "  glt_dom_id, " _
                                        & "  glt_en_id, " _
                                        & "  glt_add_by, " _
                                        & "  glt_add_date, " _
                                        & "  glt_code, " _
                                        & "  glt_date, " _
                                        & "  glt_type, " _
                                        & "  glt_cu_id, " _
                                        & "  glt_exc_rate, " _
                                        & "  glt_seq, " _
                                        & "  glt_ac_id, " _
                                        & "  glt_sb_id, " _
                                        & "  glt_cc_id, " _
                                        & "  glt_desc, " _
                                        & "  glt_debit, " _
                                        & "  glt_credit, " _
                                        & "  glt_ref_oid, " _
                                        & "  glt_ref_trans_code, " _
                                        & "  glt_posted, " _
                                        & "  glt_dt, " _
                                        & "  glt_daybook " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(ppcs_en_id) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(ppcs_date) & ",  " _
                                        & SetSetring(ppcs_type) & ",  " _
                                        & SetInteger(ppcs_cu_id) & ",  " _
                                        & SetDbl(ppcs_exc_rate) & ",  " _
                                        & SetInteger(i + 2) & ",  " _
                                        & SetInteger(_ac_id_c) & ",  " _
                                        & SetIntegerDB(ppcs_sb_id) & ",  " _
                                        & SetIntegerDB(ppcs_cc_id) & ",  " _
                                        & SetSetringDB("Ongkos kirim") & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetDblDB(pcs_shipping_charges.EditValue) & ",  " _
                                        & SetSetring(ppcs_oid) & ",  " _
                                        & SetSetring(ppcs_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(ppcs_daybook) & "  " _
                                        & ")"
                    ppcs_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    '.Command.Parameters.Clear()


                    If func_coll.update_unposted_glbal_balance(ppcs_ssqls, ppcs_obj, ppcs_date, _
                                            _ac_id_c, _
                                            ppcs_sb_id, _
                                            ppcs_cc_id, _
                                            ppcs_en_id, ppcs_cu_id, _
                                            ppcs_exc_rate, pcs_shipping_charges.EditValue, "C") = False Then

                        Return False
                        Exit Function
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        End If
    End Function

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_en_id")
        _type = 13
        _table = "pcs_mstr"
        _initial = "ar"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "select  " _
            & "pcs_oid, " _
            & "pcs_code, " _
            & "pcs_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "pcs_cu_id, " _
            & "cu_name, " _
            & "pcs_date, " _
            & "pcs_eff_date, " _
            & "pcs_amount, " _
            & "pcs_pay_amount, " _
            & "pcs_due_date, " _
            & "pcs_expt_date, " _
            & "pcs_exc_rate, " _
            & "pcs_remarks, " _
            & "pcs_status, " _
            & "pcs_type, " _
            & "pcs_credit_term, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cu_symbol, " _
            & "so_code, " _
            & "um_master.code_name as um_name , " _
            & "pt_code,  " _
            & "pt_desc1,  " _
            & "pt_desc2,  " _
            & "sod_disc,sod_tax_inc,  " _
            & "tax_class_mstr.code_name tax_class_name, " _
            & "tax_type_mstr.code_name tax_type_name,  " _
            & "taxr_rate,  " _
            & "pcss_invoice,  " _
            & "pcss_invoice_price, " _
            & "pcss_invoice_price + (sod_price * sod_disc) as pcss_invoice_price2, " _
            & "case pcss_taxable when 'Y' then (pcss_invoice * (pcss_invoice_price * taxr_rate / 100))  else 0 end as pcss_invoice_price_ppn, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_phone_1, " _
            & "cmaddr_phone_2, " _
            & "bk_name,bk_account, bk_an, bk_code, " _
            & "ac_name, pcs_shipping_charges, pcs_total_final," _
            & "pcs_terbilang, " _
            & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "from pcss_ship " _
            & "inner join soshipd_det on soshipd_oid = pcss_soshipd_oid  " _
            & "inner join pcs_mstr on pcs_oid = pcss_pcs_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join so_mstr on so_oid = sod_so_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = pcss_tax_class_id " _
            & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = pcss_tax_class_id " _
            & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
            & "inner join ptnr_mstr on ptnr_id = pcs_bill_to " _
            & "inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "inner join cu_mstr on cu_id = pcs_cu_id " _
            & "inner join code_mstr um_master on um_master.code_id = sod_um " _
            & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = pcs_credit_term " _
            & "inner join cmaddr_mstr on cmaddr_en_id = pcs_en_id " _
            & "inner join bk_mstr on bk_id = pcs_bk_id " _
            & "inner join ac_mstr on ac_id = bk_ac_id " _
            & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pcs_oid " _
            & "where tax_type_mstr.code_name = 'PPN' " _
            & "and pcs_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code") + "'"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoice"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")
        frm.ShowDialog()


    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String

            Try
                ds.Tables("detail_so").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  pcso_oid, " _
                & "  pcso_pcs_oid, " _
                & "  pcso_seq, " _
                & "  pcso_so_oid, " _
                & "  pcso_so_code, " _
                & "  pcso_so_date, " _
                & "  pcso_dt, " _
                & "  pcso_amount " _
                & "FROM  " _
                & "  public.pcso_so  " _
                & "  inner join public.pcs_mstr on pcs_mstr.pcs_oid = pcso_pcs_oid" _
                & "  inner join public.so_mstr on so_mstr.so_oid = pcso_so_oid" _
                & " where pcs_mstr.pcs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid").ToString & "'"
            '& "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

            load_data_detail(sql, gc_detail_so, "detail_so")

            Try
                ds.Tables("detail_shipment").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  pcss_oid, " _
                & "  pcss_pcs_oid, " _
                & "  pcss_seq, " _
                & "  pcss_soshipd_oid, " _
                & "  soship_code, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  pcss_taxable, " _
                & "  pcss_tax_class_id, " _
                & "  code_name as taxclass_name, " _
                & "  pcss_tax_inc, " _
                & "  pcss_open,pcss_shipment, " _
                & "  pcss_invoice, " _
                & "  pcss_so_price, " _
                & "  pcss_so_disc_value, " _
                & "  pcss_invoice_price, " _
                & "  pcss_invoice_price * pcss_invoice as tot_inv_price, " _
                & "  pcss_close_line, " _
                & "  pcss_fp_status,sod_ppn_type, " _
                & "  pcss_dt " _
                & "FROM  " _
                & "  public.pcss_ship " _
                & "  inner join public.soshipd_det on public.pcss_ship.pcss_soshipd_oid = public.soshipd_det.soshipd_oid " _
                & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                & "  inner join public.code_mstr on public.pcss_ship.pcss_tax_class_id = public.code_mstr.code_id" _
                & "  inner join public.pcs_mstr on public.pcss_ship.pcss_pcs_oid = public.pcs_mstr.pcs_oid" _
                & " where pcs_mstr.pcs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid").ToString & "'"
            '& "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

            load_data_detail(sql, gc_detail_shipment, "detail_shipment")

            Try
                ds.Tables("detail_dist").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  public.pcsd_dist.pcsd_oid, " _
                & "  public.pcsd_dist.pcsd_pcs_oid, " _
                & "  public.pcsd_dist.pcsd_tax_distribution, " _
                & "  public.pcsd_dist.pcsd_taxable, " _
                & "  public.pcsd_dist.pcsd_tax_inc, " _
                & "  public.pcsd_dist.pcsd_tax_class_id, " _
                & "  public.pcsd_dist.pcsd_ac_id, " _
                & "  public.pcsd_dist.pcsd_sb_id, " _
                & "  public.pcsd_dist.pcsd_cc_id, " _
                & "  public.pcsd_dist.pcsd_amount, " _
                & "  public.pcsd_dist.pcsd_remarks, " _
                & "  public.pcsd_dist.pcsd_dt, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.code_mstr.code_name, " _
                & "  public.cc_mstr.cc_desc " _
                & "FROM " _
                & "  public.pcsd_dist " _
                & "  INNER JOIN public.pcs_mstr ON (public.pcsd_dist.pcsd_pcs_oid = public.pcs_mstr.pcs_oid) " _
                & "  INNER JOIN public.ac_mstr ON (public.pcsd_dist.pcsd_ac_id = public.ac_mstr.ac_id) " _
                & "  left outer join public.sb_mstr ON (public.pcsd_dist.pcsd_sb_id = public.sb_mstr.sb_id) " _
                & "  left outer join public.cc_mstr ON (public.pcsd_dist.pcsd_cc_id = public.cc_mstr.cc_id) " _
                & "  left outer join public.code_mstr ON (public.pcsd_dist.pcsd_tax_class_id = public.code_mstr.code_id)" _
                & " where pcs_mstr.pcs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid").ToString & "'"
            '& "  where pcs_mstr.pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and pcs_mstr.pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

            load_data_detail(sql, gc_detail_dist, "detail_dist")
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub par_so_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_so.ButtonClick
        Try

            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._obj = par_so
            frm._ptnr_id = _par_cus_id
            frm.type_form = True
            frm.ShowDialog()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub par_cus_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_cus.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = par_cus
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub UpdateTerbilangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateTerbilangToolStripMenuItem.Click
        Try
            Dim ssql As String
            Dim _terbilang As String

            _terbilang = func_bill.TERBILANG_FIX(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_amount"))
            ssql = "update pcs_mstr set pcs_terbilang=" & SetSetring(_terbilang) & " where pcs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_oid") & "'"

            Dim ssqls As New ArrayList
            ssqls.Add(ssql)

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If
            Box("Update success, please refresh data")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_so_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.RowCountChanged
        Try
            If gv_edit_so.RowCount >= 1 Then
                gc_edit_so.EmbeddedNavigator.Buttons.Append.Visible = False
                gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = True
            Else
                gc_edit_so.EmbeddedNavigator.Buttons.Append.Visible = True
                gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = False
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

End Class
