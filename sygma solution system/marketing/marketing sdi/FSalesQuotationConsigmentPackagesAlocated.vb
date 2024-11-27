Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports DevExpress.XtraExport
Imports master_new.PGSqlConn
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FSalesQuotationConsigmentPackagesAlocated
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As New DataSet
    Public _so_oid, _pb_oid As String
    Dim _now As DateTime
    Dim _then As DateTime
    Dim _notime As DateTime
    Dim _sq_oid_mstr As String
    Dim ssqls As New ArrayList
    Dim func_bill As New Cls_Bilangan
    Public _sq_ptnr_id_sold_mstr As Integer
    Public _sq_ref_po_oid, _sq_sq_ref_oid, _sqd_invc_oid As String
    Dim _conf_value As String
    Dim _conf_sq_price As String
    Dim mf As New master_new.ModFunction

    Private Sub FSalesQuotationConsigmentPackagesAlocated_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        form_first_load()
        _now = func_coll.get_now
        _then = func_coll.get_then
        _notime = func_coll.get_notime

        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        xtc_detail.SelectedTabPageIndex = 0
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        init_le(sq_en_id, "en_mstr")
        init_le(sq_en_to_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sq_type())
        sq_type.Properties.DataSource = dt_bantu
        sq_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        sq_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        sq_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        sq_cu_id.Properties.DataSource = dt_bantu
        sq_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        sq_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        sq_cu_id.ItemIndex = 0

        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(sq_en_id.EditValue)

        If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
            _filter += " and enacc_code='so_cash')"
        Else
            _filter += " and enacc_code='so_credit')"
        End If

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        sq_ar_ac_id.Properties.DataSource = dt_bantu
        sq_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        sq_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        sq_ar_ac_id.ItemIndex = 0

        init_le(le_en_id, "en_mstr")

        dt_bantu = New DataTable

        dt_bantu = func_data.load_sales_program()

        With sq_sales_program
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("sls_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("sls_code").ToString
            .ItemIndex = 0
        End With

    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sales(sq_en_id.EditValue))
        sq_sales_person.Properties.DataSource = dt_bantu
        sq_sales_person.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        sq_sales_person.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        sq_sales_person.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", sq_en_id.EditValue))
        ptnrg_id.Properties.DataSource = dt_bantu
        ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(sq_en_id.EditValue))
        sq_si_id.Properties.DataSource = dt_bantu
        sq_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        sq_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        sq_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pay_type(sq_en_id.EditValue))
        sq_pay_type.Properties.DataSource = dt_bantu
        sq_pay_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sq_pay_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sq_pay_type.ItemIndex = 1

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(sq_en_id.EditValue, "payment_methode"))
        sq_pay_method.Properties.DataSource = dt_bantu
        sq_pay_method.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sq_pay_method.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sq_pay_method.ItemIndex = 5

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sq_en_id.EditValue))
        sq_ar_sb_id.Properties.DataSource = dt_bantu
        sq_ar_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        sq_ar_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        sq_ar_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sq_en_id.EditValue))
        sq_ar_cc_id.Properties.DataSource = dt_bantu
        sq_ar_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        sq_ar_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        sq_ar_cc_id.ItemIndex = 0

        'dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
        'sq_pi_id.Properties.DataSource = dt_bantu
        'sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        'sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        'sq_pi_id.ItemIndex = 0

        dt_bantu = New DataTable
        If func_coll.get_conf_file("so_price_list_limit_ptnrg_id") = "1" Then
            dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime, ptnrg_id.EditValue))
        Else
            dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
        End If

        Try
            sq_pi_id.Properties.DataSource = dt_bantu
            sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
            sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
            sq_pi_id.ItemIndex = 0
        Catch ex As Exception

        End Try

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(sq_en_id.EditValue))
        sq_credit_term.Properties.DataSource = dt_bantu
        sq_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sq_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sq_credit_term.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr_default(sq_en_id.EditValue))
        sq_ptsfr_loc_id.Properties.DataSource = dt_bantu
        sq_ptsfr_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        sq_ptsfr_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        sq_ptsfr_loc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(sq_en_id.EditValue))
        sq_ptsfr_loc_to_id.Properties.DataSource = dt_bantu
        sq_ptsfr_loc_to_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        sq_ptsfr_loc_to_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        sq_ptsfr_loc_to_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr_cons(sq_en_id.EditValue))
        sq_ptsfr_loc_to_id.Properties.DataSource = dt_bantu
        sq_ptsfr_loc_to_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        sq_ptsfr_loc_to_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        sq_ptsfr_loc_to_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr_git(sq_en_id.EditValue))
        sq_ptsfr_loc_git.Properties.DataSource = dt_bantu
        sq_ptsfr_loc_git.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        sq_ptsfr_loc_git.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        sq_ptsfr_loc_git.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sq_area_mstr(sq_en_id.EditValue))
        sq_pidd_area_id.Properties.DataSource = dt_bantu
        sq_pidd_area_id.Properties.DisplayMember = dt_bantu.Columns("area_name").ToString
        sq_pidd_area_id.Properties.ValueMember = dt_bantu.Columns("area_id").ToString
        sq_pidd_area_id.ItemIndex = 0

    End Sub

    Private Sub sq_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub sq_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_type.EditValueChanged
        dt_bantu = New DataTable
        If func_coll.get_conf_file("so_price_list_limit_ptnrg_id") = "1" Then
            dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime, ptnrg_id.EditValue))
        Else
            dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
        End If

        Try
            sq_pi_id.Properties.DataSource = dt_bantu
            sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
            sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
            sq_pi_id.ItemIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sq_cu_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_cu_id.EditValueChanged
        Dim _sq_exc_rate As Double
        If sq_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            _sq_exc_rate = func_data.get_exchange_rate(sq_cu_id.EditValue, sq_date.DateTime)
            If _sq_exc_rate = 1 Then
                sq_exc_rate.EditValue = 0
            Else
                sq_exc_rate.EditValue = _sq_exc_rate
            End If

        Else
            sq_exc_rate.EditValue = 1
        End If

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
        sq_pi_id.Properties.DataSource = dt_bantu
        sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        sq_pi_id.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "sq_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SQ Type", "sq_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Referensi Po No.", "sq_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Exchange Rate", "sq_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Area", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Booking", "sq_booking", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Re Booking", "sq_sq_ref_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Consignment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Pre Order", "sq_alocated", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Booking From", "booking_desc_from", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Booking Location", "booking_desc_to", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Location GIT", "booking_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Booking Date", "sq_book_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Expire Date", "sq_book_end_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Prepayment", "sq_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "Discount", "sq_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_master, "Payment", "sq_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Payment Date", "sq_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Close Date", "sq_close_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Approval Type", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "sq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sales Unit", "sq_sales_unit", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master, "Is Packagesq", "sq_is_package", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Price", "sq_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Total", "sq_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "remaining credits", "sq_crlmt_reff", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "PPN", "sq_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "PPH", "sq_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "After Tax", "sq_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        'add_column_copy(gv_master, "Ext. Total", "sq_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        'add_column_copy(gv_master, "Ext. PPN", "sq_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        'add_column_copy(gv_master, "Ext. PPH", "sq_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        'add_column_copy(gv_master, "Ext. After Tax", "sq_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Dropshipper", "sq_dropshipper", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shipment Address", "sq_ship_to", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "sq_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sq_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sq_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "sqd_sq_oid", False)
        'add_column_copy(gv_detail, "invc_oid", "sqd_invc_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Package Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Booking", "sqd_qty_booking", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Allocating", "sqd_qty_allocated", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty SO", "sqd_qty_so", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Transfer", "sqd_qty_transfer", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Shipment", "sqd_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail, "Qty SO", "outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Outstanding", "sqd_qty_outs", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status", "sqd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_packages, "sqd_sq_oid", False)
        'add_column_copy(gv_detail_packages, "invc_oid", "sqd_invc_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Package Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Qty Booking", "sqd_qty_booking", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Qty Allocating", "sqd_qty_allocated", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Qty SO", "sqd_qty_so", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Qty Transfer", "sqd_qty_transfer", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Qty Shipment", "sqd_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail_packages, "Qty SO", "outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Qty Outstanding", "sqd_qty_outs", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_packages, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_packages, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_packages, "Status", "sqd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "sqd_oid", False)
        add_column(gv_edit, "sqd_sq_oid", False)
        add_column(gv_edit, "sqd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_ds_ptnr_id", False)
        add_column(gv_edit, "Partner Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)

        'If ("sqd_is_additional_charge") = "N" Then
        '    add_column(gv_edit, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'Else
        '    add_column_edit(gv_edit, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'End If

        add_column(gv_edit, "sqd_pt_id", False)
        add_column(gv_edit, "sqd_invc_oid", False)
        add_column(gv_edit, "sqd_invc_qty", False)
        'add_column(gv_edit, "invc_oid", "sqd_invc_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "pt_ls", False)
        add_column_edit(gv_edit, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        add_column(gv_edit, "sqd_um", False)
        add_column(gv_edit, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        If func_coll.get_conf_file("editable_sq_price") = "0" Then
            add_column_edit(gv_edit, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        Else
            add_column(gv_edit, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        End If

        If func_coll.get_conf_file("editable_sq_price") = "0" Then
            add_column_edit(gv_edit, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        Else
            add_column(gv_edit, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        End If
        'tambahan untuk menunjukan pricenet ------
        'add_column(gv_edit, "Price Net", "sqd_price_net", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_edit, "Ext Total Before Tax", "sqd_price_ori_aft_disc_before_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_edit, "Ext Total After Tax", "sqd_price_ori_aft_disc_aft_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        '---------
        'add_column_edit(gv_edit, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_edit, "sqd_sales_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_sales_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_sales_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_disc_ac_id", False)
        add_column(gv_edit, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_tax_class", False)
        add_column(gv_edit, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "sqd_pod_oid", False)

        add_column(gv_edit_packages, "sqd_oid", False)
        add_column(gv_edit_packages, "sqd_sq_oid", False)
        add_column(gv_edit_packages, "sqd_en_id", False)
        add_column(gv_edit_packages, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "sqd_ds_ptnr_id", False)
        add_column(gv_edit_packages, "Partner Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "sqd_si_id", False)
        add_column(gv_edit_packages, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)

        'If ("sqd_is_additional_charge") = "N" Then
        '    add_column(gv_edit_packages, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'Else
        '    add_column_edit(gv_edit_packages, "Additional", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'End If

        add_column(gv_edit_packages, "sqd_pt_id", False)
        add_column(gv_edit_packages, "sqd_invc_oid", False)
        add_column(gv_edit_packages, "sqd_invc_qty", False)
        'add_column(gv_edit_packages, "invc_oid", "sqd_invc_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "pt_type", False)
        add_column(gv_edit_packages, "pt_ls", False)
        add_column_edit(gv_edit_packages, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_packages, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        add_column(gv_edit_packages, "sqd_um", False)
        add_column(gv_edit_packages, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "sqd_loc_id", False)
        add_column(gv_edit_packages, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        If func_coll.get_conf_file("editable_sq_price") = "0" Then
            add_column_edit(gv_edit_packages, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        Else
            add_column(gv_edit_packages, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        End If

        If func_coll.get_conf_file("editable_sq_price") = "0" Then
            add_column_edit(gv_edit_packages, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        Else
            add_column(gv_edit_packages, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        End If
        'tambahan untuk menunjukan pricenet ------
        'add_column(gv_edit_packages, "Price Net", "sqd_price_net", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_edit_packages, "Ext Total Before Tax", "sqd_price_ori_aft_disc_before_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_edit_packages, "Ext Total After Tax", "sqd_price_ori_aft_disc_aft_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        '---------
        'add_column_edit(gv_edit_packages, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_edit_packages, "sqd_sales_ac_id", False)
        add_column(gv_edit_packages, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "sqd_sales_sb_id", False)
        add_column(gv_edit_packages, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "sqd_sales_cc_id", False)
        add_column(gv_edit_packages, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "sqd_disc_ac_id", False)
        add_column(gv_edit_packages, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_packages, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_packages, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit_packages, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_packages, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "sqd_tax_class", False)
        add_column(gv_edit_packages, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_packages, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_packages, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit_packages, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit_packages, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit_packages, "sqd_pod_oid", False)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  sq_oid, " _
                    & "  sq_dom_id, " _
                    & "  sq_en_id, " _
                    & "  sq_en_to_id, " _
                    & "  sq_add_by, " _
                    & "  sq_add_date, " _
                    & "  sq_upd_by, " _
                    & "  sq_upd_date, " _
                    & "  sq_code, " _
                    & "  sq_ptnr_id_sold, " _
                    & "  sq_ptnr_id_bill, " _
                    & "  sq_ref_po_code, " _
                    & "  sq_ref_po_oid, " _
                    & "  sq_ref_po_oid, " _
                    & "  sq_sq_ref_code, " _
                    & "  sq_date, " _
                    & "  sq_si_id, " _
                    & "  sq_si_to_id, " _
                    & "  sq_type, " _
                    & "  sq_sales_person, " _
                    & "  sq_pi_id, " _
                    & "  sq_pi_area_id, " _
                    & "  area_name, " _
                    & "  sq_pay_type, " _
                    & "  sq_pay_method, " _
                    & "  sq_ar_ac_id, " _
                    & "  sq_ar_sb_id, " _
                    & "  sq_ar_cc_id, " _
                    & "  sq_dp, " _
                    & "  sq_disc_header, " _
                    & "  sq_total, " _
                    & "  sq_print_count, " _
                    & "  sq_need_date, " _
                    & "  sq_cons, " _
                    & "  sq_booking, " _
                    & "  sq_alocated, " _
                    & "  sq_rebooking, " _
                    & "  sq_ptsfr_loc_id, " _
                    & "  loc_mstr_from.loc_desc as booking_desc_from, " _
                    & "  sq_ptsfr_loc_git, " _
                    & "  loc_mstr_git.loc_desc as booking_desc_git, " _
                    & "  sq_ptsfr_loc_to_id, " _
                    & "  loc_mstr_to.loc_desc as booking_desc_to, " _
                    & "  sq_alocated, " _
                    & "  sq_book_start_date, " _
                    & "  sq_book_end_date, " _
                    & "  sq_close_date, " _
                    & "  sq_tran_id, " _
                    & "  sq_trans_id, " _
                    & "  sq_trans_rmks, " _
                    & "  sq_current_route, " _
                    & "  sq_next_route, " _
                    & "  sq_dt, " _
                    & "  sq_cu_id, " _
                    & "  sq_total_ppn, " _
                    & "  sq_total_pph, " _
                    & "  sq_payment, " _
                    & "  sq_exc_rate, " _
                    & "  (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax, " _
                    & "  sq_exc_rate * sq_total as sq_total_ext,  sq_exc_rate * sq_total_ppn as sq_total_ppn_ext, " _
                    & "  sq_exc_rate * sq_total_pph as sq_total_pph_ext,  sq_exc_rate * (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax_ext, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                    & "  si_desc, " _
                    & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                    & "  pi_desc, " _
                    & "  pay_type.code_name as pay_type_name, coalesce(pay_type.code_usr_1,'-1') as pay_interval, " _
                    & "  pay_method.code_name as pay_method_name, " _
                    & "  sq_sales_program,sls_name, " _
                    & "  ac_mstr_ar.ac_code, " _
                    & "  ac_mstr_ar.ac_name, " _
                    & "  sb_mstr_ar.sb_desc, " _
                    & "  cc_mstr_ar.cc_desc,sq_pt_id,coalesce(sq_is_package,'N') as sq_is_package,pt_code,pt_desc1,pt_desc2,sq_price, " _
                    & "  cu_name, " _
                    & "  tran_name, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3, " _
                    & "  sq_dropshipper, " _
                    & "  sq_ship_to " _
                    & "FROM  " _
                    & "  public.sq_mstr " _
                    & "  inner join en_mstr on en_id = sq_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
                    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
                    & "  inner join si_mstr on si_id = sq_si_id " _
                    & "  left outer join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = sq_ptsfr_loc_id " _
                    & "  left outer join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = sq_ptsfr_loc_git " _
                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
                    & "  left outer join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = sq_ptsfr_loc_to_id " _
                    & "  inner join pi_mstr on pi_id = sq_pi_id " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
                    & "  inner join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
                    & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sq_ar_ac_id " _
                    & "  left outer join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
                    & "  left outer join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
                    & "  inner join cu_mstr on cu_id = sq_cu_id " _
                    & "  left outer join tran_mstr on tran_id = sq_tran_id" _
                    & "  left outer join pt_mstr on sq_pt_id = pt_id " _
                    & "  left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr_sold.ptnr_ptnrg_id " _
                    & "  left outer join sls_program on sq_sales_program = sls_code " _
                    & "  left outer join public.area_mstr ON (public.sq_mstr.sq_pi_area_id = public.area_mstr.area_id) " _
                    & " where sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        If ce_en_id.EditValue = True Then
            get_sequel += " and sq_en_id =" & le_en_id.EditValue
        Else
            get_sequel += " and sq_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        End If
        If ce_cus.EditValue = True Then
            get_sequel += " and sq_ptnr_id_sold =" & le_cus_id.EditValue
        End If

        'If sq_book_end_date.DateTime < _now Then
        '    If sq_trans_id = "D" Then
        '        cancel_line()

        '    End If



        'End If
        Return get_sequel
    End Function

    Public Overrides Sub relation_detail()
        'Dim ssql As String
        Try
            'load semua data detail diawal trus di filter yg tdk efisien sana sekali
            'berattttt booo

            'gv_detail.Columns("sqd_sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString)
            'gv_detail.BestFitColumns()

            'gv_detail_attr.Columns("sqa_sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString)
            'gv_detail.BestFitColumns()

            ''gv_detail_kp.Columns("sokp_sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid"))
            ''gv_detail_kp.BestFitColumns()

            'If _conf_value = "1" Then
            '    gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code").ToString)
            '    gv_wf.BestFitColumns()

            '    gv_email.Columns("sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString)
            '    gv_email.BestFitColumns()
            'End If


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sq_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If sq_taxable.EditValue = False Then
        '    sq_tax_class.Enabled = False
        '    sq_tax_class.ItemIndex = 0
        '    sq_tax_inc.Enabled = False
        '    sq_tax_inc.Checked = False
        'Else
        '    sq_tax_class.Enabled = True
        '    sq_tax_inc.Enabled = True
        '    sq_tax_inc.Checked = False
        'End If
    End Sub

    Public Overrides Sub insert_data_awal()
        sq_ptnr_id_sold.Tag = ""
        sq_en_id.Focus()
        sq_en_id.ItemIndex = 1
        sq_en_to_id.Focus()
        sq_en_to_id.ItemIndex = 1
        sq_date.DateTime = _now
        sq_type.ItemIndex = 0
        sq_ar_ac_id.ItemIndex = 0
        sq_cu_id.ItemIndex = 0
        sq_exc_rate.Text = 1
        sq_trans_rmks.Text = ""
        sq_ptnr_id_sold.Text = ""
        sq_bantu_address.Text = ""
        sq_cu_id.Enabled = True
        sq_type.Enabled = True
        sq_pay_type.Enabled = True
        sq_pi_id.Enabled = True
        sq_pidd_area_id.Enabled = True
        sq_ptnr_id_sold.Enabled = True
        sq_bantu_address.Enabled = True
        sq_ref_po_code.Text = ""
        sq_ref_po_code.Enabled = True
        sq_cons.EditValue = False
        sq_alocated.EditValue = False

        sq_booking.EditValue = True
        sq_rebooking.EditValue = False
        sq_dropshipper.EditValue = False
        sq_ptsfr_loc_id.Text = ""
        sq_ptsfr_loc_to_id.Text = ""
        sq_ptsfr_loc_git.Text = ""
        sq_alocated.EditValue = False
        sq_pay_method.EditValue = 5

        sq_crlmt_reff.Text = ""
        sq_crlmt_reff.Enabled = False
        'sq_book_start_date.EditValue = _now
        'sq_book_end_date.EditValue = _then

        sq_need_date.EditValue = _then
        sq_due_date.EditValue = _then
        sq_sales_program.ItemIndex = 0
        sq_is_package.EditValue = False
        sq_pt_id.Tag = ""
        sq_pt_id.EditValue = ""
        pt_desc1.EditValue = ""
        pt_desc2.EditValue = ""
        sq_price.EditValue = ""
        sq_ship_to.EditValue = ""
        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
            'tcg_customer.SelectedTabPageIndex = 0
        Catch ex As Exception

            sq_ref_po_code.Text = ""
            _sq_ref_po_oid = ""
            sq_ref_po_code.Enabled = True

            sq_sq_ref_code.Text = ""
            _sq_sq_ref_oid = ""
            sq_sq_ref_code.Enabled = True

            _sqd_invc_oid = ""


        End Try

        ''sqd_pt_additional = False

        te_dpp.EditValue = 0.0
        te_ppn.EditValue = 0.0
        te_discount.EditValue = 0.0
        te_total.EditValue = 0.0
    End Sub

    Private Sub sq_booking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sq_booking.CheckedChanged

        'Dim objDate As Object

        'If (sq_book_start_date.Text <> String.Empty) Then
        '    objDate = DateTime.Parse(sq_book_start_date.Text)
        'Else
        '    objDate = DBNull.Value
        'End If

        If sq_booking.EditValue = True Then

            sq_rebooking.Enabled = True
            sq_rebooking.Checked = False

            sq_sq_ref_code.Enabled = False
            sq_alocated.Enabled = False
            sq_alocated.Checked = False

            sq_cons.Enabled = True
            sq_cons.Checked = False

            sq_prepay_cc_id.Enabled = False

            sq_book_start_date.Enabled = True
            sq_book_start_date.EditValue = _now

            sq_book_end_date.Enabled = True
            sq_book_end_date.EditValue = _then

            'sq_ptsfr_loc_id.Enabled = False
            'sq_ptsfr_loc_to_id.Enabled = False
            'sq_ptsfr_loc_git.Enabled = False
            'sq_en_to_id.Enabled = False

        ElseIf sq_cons.Checked = True Then
            sq_ptsfr_loc_id.Enabled = True
            sq_ptsfr_loc_to_id.Enabled = True
            sq_ptsfr_loc_git.Enabled = True
            sq_en_to_id.Enabled = True

        Else
            sq_rebooking.Enabled = False
            sq_rebooking.Checked = False

            sq_alocated.Enabled = True
            sq_alocated.Checked = False

            sq_prepay_cc_id.Enabled = False

            sq_book_start_date.Enabled = False
            sq_book_start_date.EditValue = ""
            sq_book_end_date.Enabled = False
            sq_book_end_date.EditValue = ""
            sq_ptsfr_loc_id.Enabled = True

            sq_ptsfr_loc_to_id.Enabled = True
            'sq_ptsfr_loc_to_id.EditValue = Nothing
            sq_ptsfr_loc_git.Enabled = True
            'sq_ptsfr_loc_git.EditValue = Nothing
            sq_en_to_id.Enabled = True
            'sq_en_to_id.EditValue = Nothing
            'If sq_book_start_date.Text = "" Then
            '    'Dim dt As Nullable(Of DateTime) = Nothing
            '    Dim dt As System.Nullable(Of DateTime)
            '    dt = Nothing
            '    'MessageBox.Show("Need Date Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    'Return False
            'End If


            'Dim _date As Date = sq_book_start_date.DateTime


        End If
    End Sub

    Private Sub sq_alocated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sq_alocated.CheckedChanged
        If sq_alocated.EditValue = True Then
            sq_prepay_cc_id.Enabled = True

        Else
            sq_prepay_cc_id.Enabled = False

        End If
    End Sub

    Private Sub sq_rebooking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sq_rebooking.CheckedChanged
        If sq_rebooking.EditValue = True Then
            sq_sq_ref_code.Enabled = True

        Else
            sq_sq_ref_code.Enabled = False

        End If
    End Sub

    'Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
    '    dateEdit1.EditValue = Nothing
    '    BindingContext(bindingSource1).EndCurrentEdit()
    'End Sub

    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()
        gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
        ds_edit = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  sqd_oid, " _
                        & "  sqd_dom_id, " _
                        & "  sqd_en_id, " _
                        & "  sqd_add_by, " _
                        & "  sqd_add_date, " _
                        & "  sqd_upd_by, " _
                        & "  sqd_upd_date, " _
                        & "  sqd_sq_oid, " _
                        & "  sqd_seq, " _
                        & "  sqd_is_additional_charge, " _
                        & "  sqd_si_id, " _
                        & "  sqd_pt_id, " _
                        & "  sqd_rmks, " _
                        & "  sqd_qty, " _
                        & "  sqd_qty_booking, " _
                        & "  sqd_qty_allocated, " _
                        & "  sqd_qty_transfer, " _
                        & "  sqd_qty_so, " _
                        & "  sqd_qty_shipment, " _
                        & "  sqd_qty_outs, " _
                        & "  public.sqd_det.sqd_qty - coalesce(public.sqd_det.sqd_qty_so,0) as sqd_qty_outstanding, " _
                        & "  coalesce(public.sqd_det.sqd_qty,0) - coalesce(public.sqd_det.sqd_qty_so,0) as sqd_qty_outstandings, " _
                        & "  sqd_um, " _
                        & "  sqd_cost, " _
                        & "  sqd_price, " _
                        & "  sqd_total_amount_price, " _
                        & "  sqd_disc, " _
                        & "  sqd_sales_ac_id, " _
                        & "  sqd_sales_sb_id, " _
                        & "  sqd_sales_cc_id, " _
                        & "  sqd_disc_ac_id, " _
                        & "  sqd_um_conv, " _
                        & "  sqd_qty_real, " _
                        & "  sqd_taxable, " _
                        & "  sqd_tax_inc, " _
                        & "  sqd_tax_class, " _
                        & "  sqd_ppn_type, " _
                        & "  sqd_status, " _
                        & "  sqd_dt, " _
                        & "  sqd_payment, " _
                        & "  sqd_dp, " _
                        & "  sqd_sales_unit, " _
                        & "  sqd_loc_id, " _
                        & "  sqd_serial, " _
                        & "  en_desc, " _
                        & "  si_desc, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_type, " _
                        & "  pt_ls, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                        & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                        & "  sb_desc, " _
                        & "  cc_desc, " _
                        & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                        & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                        & "  tax_class.code_name as sqd_tax_class_name, " _
                        & "  loc_desc, " _
                        & "  sqd_pod_oid, " _
                        & "  sqd_invc_oid, " _
                        & "  sqd_invc_qty " _
                        & "FROM  " _
                        & "  public.sqd_det " _
                        & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                        & "  inner join en_mstr on en_id = sqd_en_id " _
                        & "  inner join si_mstr on si_id = sqd_si_id " _
                        & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                        & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                        & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                        & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                        & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                        & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                        & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                        & " where sqd_det.sqd_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True


        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        'If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
        '    Dim _date As Date = sq_date.DateTime
        '    Dim _gcald_det_status As String = func_data.get_gcald_det_status(sq_en_id.EditValue, "gcald_so", _date)

        '    If _gcald_det_status = "" Then
        '        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    ElseIf _gcald_det_status.ToUpper = "Y" Then
        '        MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        footer()
        If SetString(sq_ptnr_id_sold.Tag) = "" Then
            MessageBox.Show("Customer can't blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If Trim(sq_exc_rate.EditValue) = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim sSql As String
        Dim i As Integer
        Dim _jml As Integer = 0
        Dim _sqd_invc_oid, _ptname As String

        'Try
        '    _sqd_invc_oid = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_invc_oid"))
        'Catch ex As Exception
        'End Try

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

            _sqd_invc_oid = SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))

            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) = True Then
                MessageBox.Show("Location Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False

            ElseIf SetNumber(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) = 0.0 Then
                MessageBox.Show("Cost Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False

                'i_2 = 0
                'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                '    If ds_edit.Tables(0).Rows(i).Item("riud_qty_real") > 0 Then
                '        If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                '            i_2 += 1

                '            _en_id = riu_en_id.EditValue
                '            _si_id = ds_edit.Tables(0).Rows(i).Item("riud_si_id")
                '            _loc_id = ds_edit.Tables(0).Rows(i).Item("riud_loc_id")
                '            _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                '            _serial = "''"
                '            _qty = ds_edit.Tables(0).Rows(i).Item("riud_qty_real")
                '            If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                '                'sqlTran.Rollback()
                '                insert = False
                '                Exit Function
                '            End If

                'ElseIf _sqd_invc_oid <> "" Then
                '    sSql = "SELECT  " _
                '                & "  public.invc_mstr.invc_oid, " _
                '                & "  public.invc_mstr.invc_pt_id, " _
                '                & "  public.invc_mstr.invc_qty_available as _jml, " _
                '                & "  public.pt_mstr.pt_desc1 as _ptname " _
                '                & "FROM " _
                '                & "  public.invc_mstr " _
                '                & "  INNER JOIN public.pt_mstr ON (public.invc_mstr.invc_pt_id = public.pt_mstr.pt_id)" _
                '           & "WHERE " _
                '           & "  invc_oid=" & ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid")

                '    _jml = SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_invc_qty"))
                '    '_ptname = sSql = "SELECT  public.invc_mstr.invc_oid, " _
                '    '            & "  public.invc_mstr.invc_pt_id, " _
                '    '            & "  public.pt_mstr.pt_desc1 as _ptname " _
                '    '            & "FROM " _
                '    '            & "  public.invc_mstr " _
                '    '            & "  INNER JOIN public.pt_mstr ON (public.invc_mstr.invc_pt_id = public.pt_mstr.pt_id)" _
                '    '       & "WHERE " _
                '    '       & "  invc_oid=" & ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid")

                '    If _jml < ds_edit.Tables(0).Rows(i).Item("sqd_qty_available") Then
                '        MessageBox.Show("Qty " + _ptname + " lower than available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        BindingContext(ds_edit.Tables(0)).Position = i
                '        Return False
                '    End If


                'ElseIf SetNumber(ds_edit.Tables(0).Rows(i).Item("sqd_qty_open")) <> "0" Then
                '    If SetNumber(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) < SetNumber(ds_edit.Tables(0).Rows(i).Item("invc_qty")) Then
                '        MessageBox.Show("Qty process higher than qty open..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        BindingContext(ds_edit.Tables(0)).Position = i
                '        Return False
                '    End If

                '    Try
                '        Using objcek As New master_new.CustomCommand
                '            With objcek
                '                '.Connection.Open()
                '                '.Command = .Connection.CreateCommand
                '                '.Command.CommandType = CommandType.Text
                '                .Command.CommandText = "select invc_qty as jml " _
                '                                     & " from invc_mstr " _
                '                                     & " where invc_oid = "+ _sqd_invc_oid"
                '                .InitializeCommand()
                '                .DataReader = .ExecuteReader
                '                While .DataReader.Read
                '                    _jml = .DataReader("jml").ToString
                '                End While
                '            End With
                '        End Using
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message)
                '    End Try

                '    If e.Value < _jml Then
                '        MessageBox.Show("Can't Cancel For Shipment SQ which has been transfered...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Exit Function
                '    End If


            Else
                sSql = "SELECT loc_id " _
                       & "FROM " _
                       & "  public.loc_mstr a " _
                       & "WHERE " _
                       & "  a.loc_id=" & ds_edit.Tables(0).Rows(i).Item("sqd_loc_id") & " AND  " _
                       & "  a.loc_en_id=" & sq_en_id.EditValue

                If master_new.PGSqlConn.CekRowSelect(sSql) = 0 Then
                    MessageBox.Show("Location error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next

        If sq_type.GetColumnValue("value") = "D" Then
            If sq_need_date.Text = "" Then
                MessageBox.Show("Need Date Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        End If

        'If sq_cons.Checked = True Then
        '    Dim _conf_value, _type As String

        '    '_conf_value = func_coll.get_conf_file("err_shipment_consigment")
        '    _type = ""

        '    'If _conf_value = "1" Then
        '    For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '        Try
        '            Using objcb As New master_new.CustomCommand
        '                With objcb
        '                    '.Connection.Open()
        '                    '.Command = .Connection.CreateCommand
        '                    '.Command.CommandType = CommandType.Text
        '                    .Command.CommandText = "select coalesce(code_usr_1,'N') as code_usr_1 from loc_mstr inner join code_mstr on code_id = loc_type " _
        '                                         & " where loc_id = " + ds_edit.Tables(0).Rows(i).Item("sq_ptsfr_loc_to_id").ToString
        '                    .InitializeCommand()
        '                    .DataReader = .ExecuteReader
        '                    While .DataReader.Read
        '                        _type = .DataReader.Item("code_usr_1")
        '                    End While
        '                End With
        '            End Using
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            Return False
        '        End Try

        '        If _type.ToUpper <> "Y" Then
        '            MessageBox.Show("Can't Shipment From Location That Are Not Consignment Location...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Return False
        '        End If
        '    Next
        '    'End If
        'End If

        'cek inventory kalau terjadi Booking
        'If sq_booking.Checked = True Then
        '    For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '        If func_coll.cek_inventory_booking(ds_edit.Tables(0).Rows(i).Item("sqd_en_id"), ds_edit.Tables(0).Rows(i).Item("sqd_si_id"), _
        '                                            ds_edit.Tables(0).Rows(i).Item("sqd_loc_id"), ds_edit.Tables(0).Rows(i).Item("sqd_pt_id"), _
        '                                            ds_edit.Tables(0).Rows(i).Item("pt_code"), ds_edit.Tables(0).Rows(i).Item("sqd_qty"), "''") = False Then
        '            Return False
        '        End If
        '    Next
        'End If


        'If func_coll.get_conf_file("limit_so_by_dbcr") = "1" And func_coll.get_conf_file("limit_so_by_dbcr_date") <> "" Then

        'If so_project.EditValue = False Then
        '    sSql = "SELECT  " _
        '  & "  distinct(public.ar_mstr.ar_oid) as ar_oid " _
        '  & "FROM " _
        '  & "  public.ar_mstr " _
        '  & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
        '  & "  INNER JOIN public.so_mstr ON (public.arso_so.arso_so_oid = public.so_mstr.so_oid) " _
        '  & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
        '  & "WHERE " _
        '  & "  public.ar_mstr.ar_status  is null and  ptnr_is_ps='Y' and public.so_mstr.so_sales_person = " & so_sales_person.EditValue _
        '  & " and ar_date > '" & func_coll.get_conf_file("limit_so_by_dbcr_date") _
        '  & "' and    datediff(CURRENT_DATE,ar_date)>3 and coalesce(so_project,'N')='N' "


        '    Dim dt As New DataTable
        '    dt = GetTableData(sSql)

        '    If dt.Rows.Count > 0 Then
        '        Box("Check Debit Credit Memo for this sales")
        '        Return False
        '    End If

        'If func_coll.get_conf_file("limit_so_by_drcr") = "1" Then

        '    'If so_project.EditValue = False Then
        '    sSql = "SELECT  " _
        '      & "  distinct(public.ar_mstr.ar_oid) as ar_oid " _
        '      & "FROM " _
        '      & "  public.ar_mstr " _
        '      & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
        '      & "  INNER JOIN public.so_mstr ON (public.arso_so.arso_so_oid = public.so_mstr.so_oid) " _
        '      & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
        '      & "WHERE " _
        '      & "  public.ar_mstr.ar_status  is null and public.so_mstr.so_sales_person = " & sq_sales_person.EditValue _
        '      & "' and datediff(CURRENT_DATE,ar_date)>3 and coalesce(so_project,'N')='N' "


        '    Dim dt As New DataTable
        '    dt = GetTableData(sSql)

        '    If dt.Rows.Count > 0 Then
        '        Box("Check Debit Credit Memo for this sales")
        '        Return False
        '    End If
        'End If
        'End If

        Return before_save
    End Function

#Region "GridView"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _sqd_qty, _sqd_qty_real, _sqd_um_conv, _sqd_qty_cost, _sqd_cost, _sqd_disc, _sqd_qty_shipment, _sqd_qty_booking, _sqd_qty_transfer, _sqd_payment, _sqd_dp, _jml As Double
        Dim _sqd_pt_id As Integer
        Dim _sqd_invc_oid As String

        _sqd_um_conv = 1
        _sqd_qty = 1
        _sqd_cost = 0
        _sqd_disc = 0
        _sqd_payment = 0
        _sqd_dp = 0
        _sqd_pt_id = -1

        If e.Column.Name = "sqd_qty" Then
            '********* Cek Qty Processed
            Try
                _sqd_qty_booking = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty_booking"))
            Catch ex As Exception
            End Try

            Try
                _sqd_invc_oid = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_invc_oid"))
            Catch ex As Exception
            End Try

            'Try
            '    Using objcek As New master_new.CustomCommand
            '        With objcek
            '            '.Connection.Open()
            '            '.Command = .Connection.CreateCommand
            '            '.Command.CommandType = CommandType.Text
            '            .Command.CommandText = "select invc_qty as jml " _
            '                                 & " from invc_mstr " _
            '                                 & " where invc_oid = _sqd_invc_oid"
            '            .InitializeCommand()
            '            .DataReader = .ExecuteReader
            '            While .DataReader.Read
            '                _jml = .DataReader("jml").ToString
            '            End While
            '        End With
            '    End Using
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

            'If e.Value < _jml Then
            '    MessageBox.Show("Can't Cancel For Shipment SQ which has been transfered...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If

            'If e.Value < _sqd_qty_shipment Then
            '    MessageBox.Show("Qty SQ Can't Lower Than Qty booking..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    gv_edit.CancelUpdateCurrentRow()
            '    Exit Sub
            'End If
            '********************************

            'If e.Value < _sqd_qty_booking Then
            '    MessageBox.Show("Qty SQ Can't Lower Than Qty booking..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    gv_edit.CancelUpdateCurrentRow()
            '    Exit Sub
            'End If
            ''********************************

            Try
                _sqd_pt_id = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_pt_id"))
            Catch ex As Exception
            End Try

            Try
                _sqd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _sqd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_cost"))
            Catch ex As Exception
            End Try

            Try
                _sqd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_disc"))
            Catch ex As Exception
            End Try

            _sqd_qty_real = e.Value * _sqd_um_conv
            _sqd_qty_cost = (e.Value * _sqd_cost) - (e.Value * _sqd_cost * _sqd_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_real", _sqd_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)

            dt_bantu = New DataTable

            'edit untuk menampilkan price total di sq_mstr edit by ra 25062023
            'ini ot=riginal----------------
            dt_bantu = (load_price_list(sq_pi_id.EditValue, _sqd_pt_id, sq_pay_type.EditValue, e.Value))

            If dt_bantu.Rows.Count > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", _sqd_payment)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", _sqd_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", dt_bantu.Rows(0).Item("pidd_payment"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", dt_bantu.Rows(0).Item("pidd_dp"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
            End If
            '-----------------------

            'ini editan dari so_mstr
            '    Dim _price As Double = 0.0
            '    If _conf_sq_price = "1" Then
            '        Dim ssql As String

            '        ssql = "select calc_biaya_per_buku from calc_mstr where calc_pt_id=" _
            '        & SetInteger(_sqd_pt_id) & " order by calc_date desc limit 1"
            '        Dim dt_price As New DataTable
            '        dt_price = GetTableData(ssql)
            '        For Each dr As DataRow In dt_price.Rows
            '            _price = SetNumber(dr(0))
            '        Next

            '    Else
            '        dt_bantu = (load_price_list(sq_pi_id.EditValue, _sqd_pt_id, sq_pay_type.EditValue, e.Value))

            '    End If

            '    If dt_bantu.Rows.Count > 0 Then

            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
            '        'gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", _sqd_payment)
            '        'gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", _sqd_dp)
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", dt_bantu.Rows(0).Item("pidd_payment"))
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", dt_bantu.Rows(0).Item("pidd_dp"))
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
            '        'gv_edit.SetRowCellValue(e.RowHandle, "sqd_commision", dt_bantu.Rows(0).Item("pidd_commision"))
            '    Else
            '        If _conf_sq_price = "1" Then
            '            gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", _price)
            '        Else
            '            gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", 0.0)
            '        End If

            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", 0.0)
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", 0.0)
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", 0.0)
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0.0)
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_commision", 0.0)
            '    End If

            '    Dim _sqd_price_ori_aft_disc_before_tax_ext As Double = 0.0
            '    Dim _sqd_price_ori_aft_disc_aft_tax_ext As Double = 0.0
            '    Dim _sqd_price_net As Double = 0.0

            '    Dim sqd_price As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sqd_price"))
            '    Dim sqd_disc As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sqd_disc"))
            '    Dim sqd_qty As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty"))
            '    Dim _so_exc_rate As Double = SetNumber(sq_exc_rate.EditValue)
            '    Dim _tax_class As Integer = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_class"))
            '    Dim rate_ppn As Double = func_coll.get_ppn(_tax_class) * 100
            '    Dim rate_pph As Double = func_coll.get_pph(_tax_class) * 100

            '    If SetString(gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_inc")) = "N" Then
            '        _sqd_price_ori_aft_disc_aft_tax_ext = System.Math.Round((((sqd_price - (sqd_price * sqd_disc)) + ((sqd_price - (sqd_price * sqd_disc)) _
            '         * (rate_ppn / 100.0)) - ((sqd_price - (sqd_price * sqd_disc)) * (rate_pph / 100.0))) * sqd_qty), 2) _
            '         * _so_exc_rate
            '    Else
            '        _sqd_price_ori_aft_disc_aft_tax_ext = System.Math.Round(((((sqd_price - ((rate_ppn / 100.0) * (sqd_price / (1.0 + (rate_ppn / 100.0))))) - _
            '        ((sqd_price - ((rate_ppn / 100.0) * (sqd_price / (1.0 + (rate_ppn / 100.0))))) * sqd_disc)) + _
            '        (((sqd_price - ((rate_ppn / 100.0) * (sqd_price / (1.0 + (rate_ppn / 100.0))))) - ((sqd_price - _
            '        ((rate_ppn / 100.0) * (sqd_price / (1.0 + (rate_ppn / 100.0))))) * sqd_disc)) * (rate_ppn / 100.0)) - _
            '        (((sqd_price - ((rate_ppn / 100.0) * (sqd_price / (1.0 + (rate_ppn / 100.0))))) - ((sqd_price - ((rate_ppn / 100.0) * _
            '        (sqd_price / (1.0 + (rate_ppn / 100.0))))) * sqd_disc)) * (rate_pph / 100.0))) * sqd_qty), 2) * _so_exc_rate
            '    End If

            '    _sqd_price_ori_aft_disc_before_tax_ext = System.Math.Round((((sqd_price - (sqd_price * sqd_disc)) + ((sqd_price - (sqd_price * sqd_disc)) _
            '       * (1)) - ((sqd_price - (sqd_price * sqd_disc)) * (1))) * sqd_qty), 2) _
            '       * _so_exc_rate

            '    _sqd_price_net = System.Math.Round((((sqd_price - (sqd_price * sqd_disc)) + ((sqd_price - (sqd_price * sqd_disc)) _
            '       * (1)) - ((sqd_price - (sqd_price * sqd_disc)) * (1)))), 2) _
            '       * _so_exc_rate

            '    gv_edit.SetRowCellValue(e.RowHandle, "sqd_price_net", _sqd_price_net)
            '    gv_edit.SetRowCellValue(e.RowHandle, "sqd_price_ori_aft_disc_before_tax_ext", _sqd_price_ori_aft_disc_aft_tax_ext)
            '    gv_edit.SetRowCellValue(e.RowHandle, "sqd_price_ori_aft_disc_aft_tax_ext", _sqd_price_ori_aft_disc_aft_tax_ext)


            '    'footer()
            'ElseIf e.Column.Name = "sqd_cost" Then
            '    Try
            '        _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            '    Catch ex As Exception
            '    End Try

            '    Try
            '        _sqd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_disc")))
            '    Catch ex As Exception
            '    End Try

            '    _sqd_qty_cost = (e.Value * _sqd_qty) - (e.Value * _sqd_qty * _sqd_disc)
            '    gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)
            '    'footer()
            'ElseIf e.Column.Name = "sqd_disc" Then
            '    Try
            '        _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            '    Catch ex As Exception
            '    End Try

            '    Try
            '        _sqd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_cost")))
            '    Catch ex As Exception
            '    End Try

            '    _sqd_qty_cost = (_sqd_cost * _sqd_qty) - (_sqd_cost * _sqd_qty * e.Value)
            '    gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)
            '    footer()
            'ElseIf e.Column.Name = "sqd_um_conv" Then
            '    'Try
            '    '    _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            '    'Catch ex As Exception
            '    'End Try

            '    '_sqd_qty_real = e.Value * _sqd_qty

            '    'gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_real", _sqd_qty_real)
            '    ''footer()
            'ElseIf e.Column.Name = "sqd_taxable" Then
            '    If gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "N" Then
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_inc", "N")
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_class", func_coll.get_id_tax_class("NON-TAX"))
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_class_name", "NON-TAX")
            '    End If
            '    'footer()
            'ElseIf e.Column.Name = "sqd_tax_inc" Then
            '    If gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_inc").ToString.ToUpper = "Y" And gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "N" Then
            '        gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_inc", "N")
            '    End If
            'footer()
        ElseIf e.Column.Name = "sqd_pt_id" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0
            _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty_real")))

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sq_pi_id.EditValue, e.Value, sq_pay_type.EditValue, _sqd_qty))

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _sqd_payment = 0 * _sqd_qty
                _sqd_dp = 0 * _sqd_qty
            Else
                _sqd_payment = dt_bantu.Rows(0).Item("pidd_payment") * _sqd_qty
                _sqd_dp = dt_bantu.Rows(0).Item("pidd_dp") * _sqd_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_inc").ToString.ToUpper = "N" Then
                _sqd_payment = _sqd_payment + (_sqd_payment * _tax_rate)
                _sqd_dp = _sqd_dp + (_sqd_dp * _tax_rate)
            End If

            'If sq_type.EditValue = "D" Then
            If dt_bantu.Rows.Count > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", _sqd_payment)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", _sqd_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
            End If
            'End If
            'footer()
        ElseIf e.Column.Name = "sqd_qty_real" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0
            Dim _pt_id As Integer
            Try
                _pt_id = gv_edit.GetRowCellValue(e.RowHandle, "sqd_pt_id")
            Catch ex As Exception
                Exit Sub
            End Try

            _sqd_qty = e.Value

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sq_pi_id.EditValue, _pt_id, sq_pay_type.EditValue, _sqd_qty))

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _sqd_payment = 0 * _sqd_qty
                _sqd_dp = 0 * _sqd_qty
            Else
                _sqd_payment = dt_bantu.Rows(0).Item("pidd_payment") * _sqd_qty
                _sqd_dp = dt_bantu.Rows(0).Item("pidd_dp") * _sqd_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_inc").ToString.ToUpper = "N" Then
                _sqd_payment = _sqd_payment + (_sqd_payment * _tax_rate)
                _sqd_dp = _sqd_dp + (_sqd_dp * _tax_rate)
            End If

            If sq_type.EditValue = "D" Then
                If dt_bantu.Rows.Count > 0 Then
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", _sqd_payment)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", _sqd_dp)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
                End If
            End If
            'footer()
        End If

    End Sub

    Private Function load_price_list(ByVal par_pi_id As Integer, ByVal par_pt_id As Integer, ByVal par_payment_type As Integer, ByVal par_min_qty As Double) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pi_id, " _
                            & "  pidd_oid, " _
                            & "  pidd_pid_oid, " _
                            & "  pidd_payment_type, " _
                            & "  pidd_price, " _
                            & "  pidd_disc, " _
                            & "  pidd_dp, " _
                            & "  pidd_interval, " _
                            & "  coalesce(pidd_payment,0) as pidd_payment, " _
                            & "  pidd_min_qty, " _
                            & "  pidd_sales_unit, " _
                            & "  pid_pt_id " _
                            & "FROM  " _
                            & "  public.pidd_det " _
                            & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                            & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                            & "  where pi_id = " + par_pi_id.ToString _
                            & "  and pidd_payment_type = " + par_payment_type.ToString _
                            & "  and pid_pt_id = " + par_pt_id.ToString _
                            & "  and pidd_min_qty <= " + par_min_qty.ToString _
                            & "  order by pidd_min_qty desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pi_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _sqd_en_id As Integer = SetNumber(gv_edit.GetRowCellValue(_row, "sqd_en_id"))
        Dim _sqd_si_id As Integer = SetNumber(gv_edit.GetRowCellValue(_row, "sqd_si_id"))
        'Dim _sqd_ptnr_id_sold As Integer = SetInteger(sq_ptnr_id_sold.EditValue)
        Dim _sq_pi_id As Integer = SetInteger(sq_pi_id.EditValue)
        Dim _sq_pi_area_id As Integer = SetInteger(sq_pidd_area_id.EditValue)
        Dim _sq_ptsfr_loc_id As Integer = SetInteger(sq_ptsfr_loc_id.EditValue)


        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "ptnr_name" Then
            Dim frm As New FPartnerParenntSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm._par_ptnr_id = sq_ptnr_id_sold.Tag
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "ptnr_code" Then
            Dim frm As New FPartnerParenntSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm._par_ptnr_id = sq_ptnr_id_sold.Tag
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "pt_code" Then
            If sq_is_package.EditValue = True Then
                Exit Sub
            End If

            Dim frm As New FSearchPartNumberSearchPi
            'Dim frm As New FInventorySearch
            'Dim frm As New FSearchPartNumberSearchAloc
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm._si_id = _sqd_si_id
            frm._pi_id = _sq_pi_id
            frm._pi_area_id = _sq_pi_area_id
            frm._loc_id = _sq_ptsfr_loc_id
            'frm._sq_type = sq_type.EditValue
            'frm._tran_oid = _sq_ref_po_oid
            frm.type_form = True
            frm.ShowDialog()

            'ElseIf _col = "sqd_is_additional_charge" Then
            '    If sq_is_package.EditValue = True Then
            '        Exit Sub
            '    End If

        ElseIf _col = "sqd_is_additional_charge" Then
            Dim frm As New FSearchPartNumberSearchAdd
            'Dim frm As New FPartNumberSearchAdditional
            'Dim frm As New FInventorySearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm._si_id = _sqd_si_id
            'frm._pi_id = _sq_pi_id
            'frm._loc_id = _sq_ptsfr_loc_id
            'frm._sq_type = sq_type.EditValue
            'frm._tran_oid = _sq_ref_po_oid
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "um_name" Then
        ElseIf _col = "sqd_tax_class_name" Then
            If gv_edit.GetRowCellValue(_row, "sqd_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "sqd_ppn_type" Then
            Dim frm As New FPPNTypeSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "loc_desc" Then
            MessageBox.Show("Please Change Location based on Partnumber", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
            'Dim frm As New FLocationSearch
            'frm.set_win(Me)
            'frm._row = _row
            'frm._en_id = _sqd_en_id
            'frm.type_form = True
            'frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Dim _sqd_qty_shipment As Double = 0

        'Try
        '    _sqd_qty_shipment = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "sqd_qty_shipment")))
        'Catch ex As Exception
        'End Try

        'If _sqd_qty_shipment <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
        'cek_shipment_row()
        footer()
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        'Dim _sqd_qty_shipment As Double = 0

        'Try
        '    _sqd_qty_shipment = ((gv_edit.GetRowCellValue(0, "sqd_qty_shipment")))
        'Catch ex As Exception
        'End Try

        'If _sqd_qty_shipment <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
        'cek_shipment_row()
    End Sub


    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "sqd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "sqd_en_id", sq_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", sq_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "sqd_si_id", sq_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", sq_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "sqd_is_additional_charge", "N")
            .SetRowCellValue(e.RowHandle, "sqd_qty", 1)
            .SetRowCellValue(e.RowHandle, "sqd_qty_outs", 1)
            .SetRowCellValue(e.RowHandle, "sqd_cost", 0)
            .SetRowCellValue(e.RowHandle, "sqd_price", 0)
            .SetRowCellValue(e.RowHandle, "sqd_disc", 0)
            '.SetRowCellValue(e.RowHandle, "sqd_taxable", IIf(sq_taxable.EditValue = True, "Y", "N"))
            '.SetRowCellValue(e.RowHandle, "sqd_tax_inc", IIf(sq_tax_inc.EditValue = True, "Y", "N"))
            '.SetRowCellValue(e.RowHandle, "sqd_tax_class", sq_tax_class.EditValue)
            '.SetRowCellValue(e.RowHandle, "sqd_tax_class_name", sq_tax_class.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "sqd_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "sqd_qty_real", 1)
            .SetRowCellValue(e.RowHandle, "sqd_qty_cost", 0)

            .SetRowCellValue(e.RowHandle, "sqd_payment", 0)
            .SetRowCellValue(e.RowHandle, "sqd_dp", 0)
            .SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
            .BestFitColumns()
        End With

        sq_cu_id.Enabled = False
        sq_type.Enabled = False
        sq_pi_id.Enabled = False
        sq_pay_type.Enabled = False
    End Sub

#End Region

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Sub cancel_line()

        Dim _code, _oid, _colom, _table, _criteria, _trans, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid")
        _colom = "sq_trans_id"
        _table = "sq_mstr"
        _criteria = "sq_code"
        _initial = "sq"
        _type = "sq"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _sq_oid As Guid = Guid.NewGuid
        Dim _sq_code, _sq_terbilang As String
        Dim _trans_id As String = ""
        Dim _jml As Integer = 0
        Dim ssqls As New ArrayList

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Cancel Line SQ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If



        Try
            Using objcek As New master_new.CustomCommand
                With objcek
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where " + par_criteria + " = '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id = "X" Then
            MessageBox.Show("Data Has been Canceled...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If _trans_id = "C" Then
            MessageBox.Show("Data Has been Processed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Try
            Using objcek As New master_new.CustomCommand
                With objcek
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(sq_code) as jml " _
                                         & " from sq_mstr " _
                                         & " inner join sqd_det on sqd_sq_oid = sq_oid " _
                                         & " where sq_code ~~* '" + par_code + "' " _
                                         & " and sqd_qty_transfer >= 1"

                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _jml = .DataReader("jml").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _jml > 0 Then
            MessageBox.Show("Can't Cancel For Shipment SQ which has been transfered...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            Using objcek As New master_new.CustomCommand
                With objcek
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(sq_code) as jml " _
                                         & " from sq_mstr " _
                                         & " inner join sqd_det on sqd_sq_oid = sq_oid " _
                                         & " where sq_code ~~* '" + par_code + "' " _
                                         & " and sqd_qty_so >= 1"

                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _jml = .DataReader("jml").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _jml > 0 Then
            MessageBox.Show("Can't Cancel For Booking SQ which has been Processed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '******
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        If insert_return(objinsert, " + par_criteria + ", " + par_code + ") = False Then
                            'sqlTran.Rollback()
                            'insert = False
                            Exit Sub
                        End If

                        '******

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where " + par_criteria + " = '" + par_code + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()


                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.ModFunction.FinsertSQL2Array(sSQLs)
                        '        '.Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        '.Command.Parameters.Clear()

                        'Next

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "update sq_mstr set sq_close_date = current_date " _
                                             & " where sq_code = '" + par_code + "'"
                        ssqls.Add(.Command.CommandText)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "update sq_mstr set sq_upd_date = current_date " _
                                             & " where sq_code = '" + par_code + "'"
                        ssqls.Add(.Command.CommandText)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        'End If


                        .Command.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try


                    'MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'apabila booking maka langsung bisa generate sales Transfer Issue...


    End Sub

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(ptnr_id as varchar),5,100) as integer)),0) as max_col  from ptnr_mstr " + _
                                           " where substring(cast(ptnr_id as varchar),5,100) <> ''"
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
        Dim _sq_oid As Guid = Guid.NewGuid

        Dim _sq_code, _sq_terbilang, _sqd_invc_qty As String
        Dim _sq_limit, _sq_total, _sq_total_lmt, _sq_total_ppn, _sq_total_pph, _sqd_qty, _sqd_price, _sqd_disc, _sq_total_temp, _tax_rate, _sqd_proc, _sqd_qty_open As Double
        'Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _serial, _pt_code As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id, _qty As Integer
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList


        If cek_duplikat_pt_id(gv_edit, "sqd_pt_id") = False Then
            Return False
        End If

        _sq_code = func_coll.get_transaction_number("SQ", sq_en_id.GetColumnValue("en_code"), "sq_mstr", "sq_code")

        ssqls.Clear()

        Dim _sq_trn_status As String
        Dim ds_bantu As New DataSet

        If sq_booking.Checked = True Then
            _sq_trn_status = "D" 'Lansung Default Ke Draft
            '_sq_receive_date = "current_timestamp"
        Else
            _sq_trn_status = "D" 'Lansung Default Ke Draft
            '_ptsfr_booking_date = "null"
        End If

        'If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
        '    '_sq_trn_status = "C" 'Lansung Default Ke Close
        '    _sq_trn_status = "D"
        'Else
        '    _sq_trn_status = "D" 'Lansung Default Ke Draft
        'End If


        '******* Mencari Total so, Total PPN, Total PPH
        _sq_total = 0
        _sq_total_ppn = 0
        _sq_total_pph = 0
        _sq_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            ''disini hanya line ppn saja
            ' _cost kalau disini diganti jadi price..karena dihitung dari price.....
            If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class"))
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
            Else
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
            End If

            _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
            _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")


            _sq_total = _sq_total + ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
        Next

        'disini dihitung ppn dan pph nya
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
                Else
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
                End If

                _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
                _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")

                _sq_total_temp = ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
                _sq_total_ppn = _sq_total_ppn + (_sq_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
                _sq_total_pph = _sq_total_pph + (_sq_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
            End If
        Next
        '*******************************************************

        _sq_terbilang = func_bill.TERBILANG_FIX(_sq_total + _sq_total_ppn - _sq_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("sqd_dp") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("sqd_payment") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
        Next
        '*************************

        'Deklarasi varible untuk customer baru
        Dim _ptnr_oid As Guid
        _ptnr_oid = Guid.NewGuid


        Dim _ptnr_id As Integer

        _ptnr_id = SetInteger(GetID_Local(sq_en_id.GetColumnValue("en_code")))

        Dim _ptnr_code As String
        _ptnr_code = "CU" + "00"
        'Return False
        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(4, Len(_ptnr_id.ToString) - 4)

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + _ptnr_id_s.ToString
        End If

        _ptnr_code = _ptnr_code + IIf(sq_en_id.GetColumnValue("en_code") = 0, "99", sq_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

        '*************************************

        Dim _id_ptnr_sq As Integer

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()


                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        If sq_booking.Checked = True Then
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sq_mstr " _
                                                & "( " _
                                                & "  sq_oid, " _
                                                & "  sq_dom_id, " _
                                                & "  sq_en_id, " _
                                                & "  sq_en_to_id, " _
                                                & "  sq_add_by, " _
                                                & "  sq_add_date, " _
                                                & "  sq_code, " _
                                                & "  sq_ptnr_id_sold, " _
                                                & "  sq_ptnr_id_bill, " _
                                                & "  sq_ref_po_code, " _
                                                & "  sq_ref_po_oid, " _
                                                & "  sq_sq_ref_code, " _
                                                & "  sq_sq_ref_oid, " _
                                                & "  sq_date, " _
                                                & "  sq_si_id, " _
                                                & "  sq_type, " _
                                                & "  sq_sales_person, " _
                                                & "  sq_pi_id, " _
                                                & "  sq_pi_area_id, " _
                                                & "  sq_pay_type, " _
                                                & "  sq_pay_method, " _
                                                & "  sq_sales_program, " _
                                                & "  sq_cons, " _
                                                & "  sq_booking, " _
                                                & "  sq_ptsfr_loc_id, " _
                                                & "  sq_ptsfr_loc_to_id, " _
                                                & "  sq_ptsfr_loc_git, " _
                                                & "  sq_alocated, " _
                                                & "  sq_book_start_date, " _
                                                & "  sq_book_end_date, " _
                                                & "  sq_ar_ac_id, " _
                                                & "  sq_ar_sb_id, " _
                                                & "  sq_ar_cc_id, " _
                                                & "  sq_dp, " _
                                                & "  sq_disc_header, " _
                                                & "  sq_total, " _
                                                & "  sq_need_date, " _
                                                & "  sq_tran_id, " _
                                                & "  sq_trans_id, " _
                                                & "  sq_trans_rmks, " _
                                                & "  sq_dt, " _
                                                & "  sq_cu_id, " _
                                                & "  sq_total_ppn, " _
                                                & "  sq_total_pph, " _
                                                & "  sq_payment, " _
                                                & "  sq_exc_rate, " _
                                                & "  sq_interval,sq_is_package,sq_pt_id,sq_price, " _
                                                & "  sq_terbilang, " _
                                                & "  sq_dropshipper, " _
                                                & "  sq_ship_to " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_sq_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(sq_en_id.EditValue) & ",  " _
                                                & SetInteger(sq_en_to_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_sq_code) & ",  " _
                                                & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                                & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                                & SetSetring(sq_ref_po_code.Text) & ",  " _
                                                & SetSetring(_sq_ref_po_oid) & ",  " _
                                                & SetSetring(sq_sq_ref_code.Text) & ",  " _
                                                & SetSetring(_sq_sq_ref_oid) & ",  " _
                                                & SetDate(sq_date.DateTime) & ",  " _
                                                & SetInteger(sq_si_id.EditValue) & ",  " _
                                                & SetSetring(sq_type.EditValue) & ",  " _
                                                & SetInteger(sq_sales_person.EditValue) & ",  " _
                                                & SetInteger(sq_pi_id.EditValue) & ",  " _
                                                & SetInteger(sq_pidd_area_id.EditValue) & ",  " _
                                                & SetInteger(sq_pay_type.EditValue) & ",  " _
                                                & SetInteger(sq_pay_method.EditValue) & ",  " _
                                                & SetSetring(sq_sales_program.EditValue) & ",  " _
                                                & SetBitYN(sq_cons.EditValue) & ",  " _
                                                & SetBitYN(sq_booking.EditValue) & ",  " _
                                                & SetInteger(sq_ptsfr_loc_id.EditValue) & ",  " _
                                                & SetInteger(sq_ptsfr_loc_to_id.EditValue) & ",  " _
                                                & SetInteger(sq_ptsfr_loc_git.EditValue) & ",  " _
                                                & SetBitYN(sq_alocated.EditValue) & ",  " _
                                                & SetDate(sq_book_start_date.DateTime) & ",  " _
                                                & SetDate(sq_book_end_date.DateTime) & ",  " _
                                                & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
                                                & SetInteger(sq_ar_sb_id.EditValue) & ",  " _
                                                & SetInteger(sq_ar_cc_id.EditValue) & ",  " _
                                                & SetDbl(_total_dp) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(_sq_total) & ",  " _
                                                & SetDate(sq_need_date.DateTime) & ",  " _
                                                & SetInteger("") & ",  " _
                                                & SetSetring(_sq_trn_status) & ",  " _
                                                & SetSetring(sq_trans_rmks.Text) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(sq_cu_id.EditValue) & ",  " _
                                                & SetDbl(_sq_total_ppn) & ",  " _
                                                & SetDbl(_sq_total_pph) & ",  " _
                                                & SetDbl(_total_payment) & ",  " _
                                                & SetDbl(sq_exc_rate.EditValue) & ",  " _
                                                & SetInteger(sq_pay_type.GetColumnValue("code_usr_1")) & ",  " _
                                                & SetBitYN(sq_is_package.EditValue) & ",  " _
                                                & SetInteger(sq_pt_id.Tag) & ",  " _
                                                & SetDec(sq_price.EditValue) & ",  " _
                                                & SetSetring(_sq_terbilang) & ",  " _
                                                & SetBitYN(sq_dropshipper.EditValue) & ",  " _
                                                & SetSetring(sq_ship_to.Text) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                        Else
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sq_mstr " _
                                                & "( " _
                                                & "  sq_oid, " _
                                                & "  sq_dom_id, " _
                                                & "  sq_en_id, " _
                                                & "  sq_en_to_id, " _
                                                & "  sq_add_by, " _
                                                & "  sq_add_date, " _
                                                & "  sq_code, " _
                                                & "  sq_ptnr_id_sold, " _
                                                & "  sq_ptnr_id_bill, " _
                                                & "  sq_ref_po_code, " _
                                                & "  sq_ref_po_oid, " _
                                                & "  sq_sq_ref_code, " _
                                                & "  sq_sq_ref_oid, " _
                                                & "  sq_date, " _
                                                & "  sq_si_id, " _
                                                & "  sq_type, " _
                                                & "  sq_sales_person, " _
                                                & "  sq_pi_id, " _
                                                & "  sq_pi_area_id, " _
                                                & "  sq_pay_type, " _
                                                & "  sq_pay_method, " _
                                                & "  sq_sales_program, " _
                                                & "  sq_cons, " _
                                                & "  sq_booking, " _
                                                & "  sq_ptsfr_loc_id, " _
                                                & "  sq_ptsfr_loc_to_id, " _
                                                & "  sq_ptsfr_loc_git, " _
                                                & "  sq_alocated, " _
                                                & "  sq_ar_ac_id, " _
                                                & "  sq_ar_sb_id, " _
                                                & "  sq_ar_cc_id, " _
                                                & "  sq_dp, " _
                                                & "  sq_disc_header, " _
                                                & "  sq_total, " _
                                                & "  sq_need_date, " _
                                                & "  sq_tran_id, " _
                                                & "  sq_trans_id, " _
                                                & "  sq_trans_rmks, " _
                                                & "  sq_dt, " _
                                                & "  sq_cu_id, " _
                                                & "  sq_total_ppn, " _
                                                & "  sq_total_pph, " _
                                                & "  sq_payment, " _
                                                & "  sq_exc_rate, " _
                                                & "  sq_interval,sq_is_package,sq_pt_id,sq_price, " _
                                                & "  sq_terbilang, " _
                                                & "  sq_dropshipper, " _
                                                & "  sq_ship_to " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_sq_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(sq_en_id.EditValue) & ",  " _
                                                & SetInteger(sq_en_to_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_sq_code) & ",  " _
                                                & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                                & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                                & SetSetring(sq_ref_po_code.Text) & ",  " _
                                                & SetSetring(_sq_ref_po_oid) & ",  " _
                                                & SetSetring(sq_sq_ref_code.Text) & ",  " _
                                                & SetSetring(_sq_sq_ref_oid) & ",  " _
                                                & SetDate(sq_date.DateTime) & ",  " _
                                                & SetInteger(sq_si_id.EditValue) & ",  " _
                                                & SetSetring(sq_type.EditValue) & ",  " _
                                                & SetInteger(sq_sales_person.EditValue) & ",  " _
                                                & SetInteger(sq_pi_id.EditValue) & ",  " _
                                                & SetInteger(sq_pidd_area_id.EditValue) & ",  " _
                                                & SetInteger(sq_pay_type.EditValue) & ",  " _
                                                & SetInteger(sq_pay_method.EditValue) & ",  " _
                                                & SetSetring(sq_sales_program.EditValue) & ",  " _
                                                & SetBitYN(sq_cons.EditValue) & ",  " _
                                                & SetBitYN(sq_booking.EditValue) & ",  " _
                                                & SetInteger(sq_ptsfr_loc_id.EditValue) & ",  " _
                                                & SetInteger(sq_ptsfr_loc_to_id.EditValue) & ",  " _
                                                & SetInteger(sq_ptsfr_loc_git.EditValue) & ",  " _
                                                & SetBitYN(sq_alocated.EditValue) & ",  " _
                                                & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
                                                & SetInteger(sq_ar_sb_id.EditValue) & ",  " _
                                                & SetInteger(sq_ar_cc_id.EditValue) & ",  " _
                                                & SetDbl(_total_dp) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(_sq_total) & ",  " _
                                                & SetDate(sq_need_date.DateTime) & ",  " _
                                                & SetInteger("") & ",  " _
                                                & SetSetring(_sq_trn_status) & ",  " _
                                                & SetSetring(sq_trans_rmks.Text) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(sq_cu_id.EditValue) & ",  " _
                                                & SetDbl(_sq_total_ppn) & ",  " _
                                                & SetDbl(_sq_total_pph) & ",  " _
                                                & SetDbl(_total_payment) & ",  " _
                                                & SetDbl(sq_exc_rate.EditValue) & ",  " _
                                                & SetInteger(sq_pay_type.GetColumnValue("code_usr_1")) & ",  " _
                                                & SetBitYN(sq_is_package.EditValue) & ",  " _
                                                & SetInteger(sq_pt_id.Tag) & ",  " _
                                                & SetDec(sq_price.EditValue) & ",  " _
                                                & SetSetring(_sq_terbilang) & ",  " _
                                                & SetBitYN(sq_dropshipper.EditValue) & ",  " _
                                                & SetSetring(sq_ship_to.Text) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        End If

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sqd_det " _
                                                & "( " _
                                                & "  sqd_oid, " _
                                                & "  sqd_dom_id, " _
                                                & "  sqd_en_id, " _
                                                & "  sqd_add_by, " _
                                                & "  sqd_add_date, " _
                                                & "  sqd_sq_oid, " _
                                                & "  sqd_seq, " _
                                                & "  sqd_is_additional_charge, " _
                                                & "  sqd_si_id, " _
                                                & "  sqd_pt_id, " _
                                                & "  sqd_rmks, " _
                                                & "  sqd_qty, " _
                                                & "  sqd_um, " _
                                                & "  sqd_cost, " _
                                                & "  sqd_price, " _
                                                & "  sqd_disc, " _
                                                & "  sqd_sales_ac_id, " _
                                                & "  sqd_sales_sb_id, " _
                                                & "  sqd_sales_cc_id, " _
                                                & "  sqd_um_conv, " _
                                                & "  sqd_qty_real, " _
                                                & "  sqd_taxable, " _
                                                & "  sqd_tax_inc, " _
                                                & "  sqd_tax_class, " _
                                                & "  sqd_ppn_type, " _
                                                & "  sqd_dt, " _
                                                & "  sqd_payment, " _
                                                & "  sqd_dp, " _
                                                & "  sqd_loc_id, " _
                                                & "  sqd_sales_unit, " _
                                                & "  sqd_pod_oid, " _
                                                & "  sqd_invc_oid, " _
                                                & "  sqd_invc_qty " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid")) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_sq_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_is_additional_charge")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sqd_rmks")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_price")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_disc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_ac_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_sb_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_cc_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_taxable")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_payment")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_dp")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_sales_unit")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_pod_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_qty").ToString) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            If func_coll.get_conf_file("limit_so_by_drcr") = "1" Then
                                If SetInteger(ptnrg_id.EditValue) = "9911" Then
                                    If SetInteger(sq_crlmt_reff.EditValue) <= 0 Then
                                        Box("Your Have no credit for this transaction")
                                        Return False
                                    ElseIf _sq_total > SetInteger(sq_crlmt_reff.EditValue) Then
                                        Box("Your credit is lacking for this order")
                                        Return False
                                    End If
                                    'End If
                                    '        If SetInteger(sq_crlmt_reff.EditValue) <= 0 Then
                                    '            If _sq_total > SetInteger(sq_crlmt_reff.EditValue) Then
                                    '                Box("Your credit is lacking for this order")
                                    '            End If
                                    '        End If
                                    '    End If
                                    'cek kredit limit

                                    'If func_coll.get_conf_file("limit_so_by_drcr") = "1" Then
                                    If SetInteger(sq_crlmt_reff.EditValue) <> 0 Then
                                        If _sq_total > SetInteger(sq_crlmt_reff.EditValue) Then
                                            Box("Your credit is lacking for this order")
                                            Return False
                                        End If
                                    End If
                                    'End If
                                Else
                                    If SetInteger(sq_crlmt_reff.EditValue) <> 0 Then
                                        If _sq_total > SetInteger(sq_crlmt_reff.EditValue) Then
                                            Box("Your credit is lacking for this order")
                                            Return False
                                        End If
                                    End If
                                End If

                            End If
                            '    Dim sSql As String
                            '    'If so_project.EditValue = False Then

                            '    sSql = "SELECT  " _
                            '        & "  public.ar_mstr.ar_bill_to, " _
                            '        & "  public.ptnr_mstr.ptnr_id, " _
                            '        & "  public.ptnr_mstr.ptnr_name, " _
                            '        & "  SUM(public.ar_mstr.ar_amount) AS ar_amount, " _
                            '        & "  SUM(public.ar_mstr.ar_pay_amount) AS ar_pay_amount, " _
                            '        & "  SUM(public.ar_mstr.ar_amount) - SUM(public.ar_mstr.ar_pay_amount) AS ap_pay_outstanding, " _
                            '        & "  public.ptnr_mstr.ptnr_limit_credit " _
                            '        & "FROM " _
                            '        & "  public.ar_mstr " _
                            '        & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                            '        & "  INNER JOIN public.ptnrg_grp ON (public.ptnr_mstr.ptnr_ptnrg_id = public.ptnrg_grp.ptnrg_id) " _
                            '        & "WHERE " _
                            '        & "  public.ptnr_mstr.ptnr_id = '" + SetInteger(sq_ptnr_id_sold.Tag) + "' " _
                            '        & "GROUP BY " _
                            '        & "  public.ar_mstr.ar_bill_to, " _
                            '        & "  public.ptnr_mstr.ptnr_id, " _
                            '        & "  public.ptnr_mstr.ptnr_name, " _
                            '        & "  public.ptnr_mstr.ptnr_limit_credit"
                            'End If


                            'Dim sSql As String
                            ''Dim i As Integer
                            Dim _jml As Integer = 0
                            'Dim _sqd_invc_oid, _ptname As String

                            'Try
                            '    _sqd_invc_oid = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_invc_oid"))
                            'Catch ex As Exception
                            'End Try

                            'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid")) <> "" Then
                                _jml = SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_invc_qty"))

                                If _jml > ds_edit.Tables(0).Rows(i).Item("sqd_qty") Then
                                    _sqd_proc = _jml - SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
                                    'Return False
                                End If
                            End If
                            'Next


                            If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                If sq_alocated.Checked = True Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update sqd_det set sqd_qty_allocated = coalesce(sqd_qty_allocated,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                         & " where sqd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update invc_mstr set invc_qty_alloc = coalesce(invc_qty_alloc,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                         & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                End If

                                If sq_booking.Checked = True Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update invc_mstr set invc_qty_available = coalesce(invc_qty_available,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                         & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    'End If
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update invc_mstr set invc_qty_booked = coalesce(invc_qty_booked,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                         & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    'If sq_booking.Checked = True Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update sqd_det set sqd_qty_booking = coalesce(sqd_qty_booking,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                         & " where sqd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                End If
                            End If
                        Next
                        '_pt_id = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
                        '_pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")

                        'cek ketersediaan bahan
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") > 0 Then
                                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = sq_en_id.EditValue
                                    _si_id = ds_edit.Tables(0).Rows(i).Item("sqd_si_id")
                                    _loc_id = ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")

                                    If sq_booking.Checked = True Then
                                        If func_coll.cek_inventory_booking(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                            'sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    End If

                                    'If sq_alocated.Checked = True Then
                                    '    If func_coll.cek_inventory_allocation(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                    '        'sqlTran.Rollback()
                                    '        insert = False
                                    '        Exit Function
                                    '    End If
                                    'End If

                                    If sq_cons.Checked = True Then
                                        If sq_booking.Checked = True Then
                                            If func_coll.cek_inventory_booking(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                                'sqlTran.Rollback()
                                                insert = False
                                                Exit Function
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                            'If sq_alocated.Checked = True Then
                            '    If func_coll.cek_inventory_booking(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                            '        'sqlTran.Rollback()
                            '        insert = False
                            '        Exit Function
                            '    End If
                            'End If


                            'Update History Bookin                                    
                            '_qty = _qty * -1.0
                            '_cost = ds_edit.Tables(0).Rows(i).Item("riud_cost")
                            '_cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                            'If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, Trim(_riu_type2), _riu_oid.ToString, "Inventory Adjusment Minus", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", riu_date.DateTime) = False Then
                            '    'sqlTran.Rollback()
                            '    insert = False
                            '    Exit Function
                            'End If
                            'End If
                            'End If
                        Next

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert SQ " & _sq_code)
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
                        set_row(_sq_oid.ToString, "sq_oid")
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

    Private Function insert_return(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_sq_code As String) As Boolean
        insert_return = True

        Dim _ptsfr_oid As Guid = Guid.NewGuid

        'Dim _serial, _pt_code, _ptsfr_code, _ptsfrd_pbd_oid As String

        'Dim _tran_id, _en_id, _si_id, _loc_id, _loc_to_id, _pt_id As Integer
        'Dim _cost, _cost_avg As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList
        'Dim _ptsfr_trn_satatus As String
        'Dim _ptsfr_trn_id As Integer
        Dim ds_bantu As New DataSet
        'Dim _qty As Double

        row = BindingContext(ds.Tables(0)).Position

        With ds.Tables(0).Rows(row)
            _sq_oid_mstr = .Item("sq_oid")
            sq_ar_sb_id.EditValue = CInt(.Item("sq_ar_sb_id"))
            sq_ar_ac_id.EditValue = CInt(.Item("sq_ar_ac_id"))
            sq_ar_cc_id.EditValue = CInt(.Item("sq_ar_cc_id"))
            sq_cu_id.EditValue = .Item("sq_cu_id")
            sq_en_id.EditValue = .Item("sq_en_id")
            sq_date.DateTime = .Item("sq_date")
            sq_exc_rate.EditValue = .Item("sq_exc_rate")
            sq_pay_method.EditValue = .Item("sq_pay_method")
            sq_sales_program.EditValue = .Item("sq_sales_program")
            sq_cons.EditValue = SetBitYNB(.Item("sq_cons"))
            sq_booking.EditValue = SetBitYNB(.Item("sq_booking"))
            sq_ptsfr_loc_id.EditValue = .Item("sq_ptsfr_loc_id")
            sq_ptsfr_loc_to_id.EditValue = .Item("sq_ptsfr_loc_to_id")
            sq_ptsfr_loc_git.EditValue = .Item("sq_ptsfr_loc_git")
            sq_alocated.EditValue = SetBitYNB(.Item("sq_alocated"))
            sq_book_start_date.DateTime = .Item("sq_book_start_date")
            sq_book_end_date.DateTime = .Item("sq_book_end_date")
            sq_pay_type.EditValue = .Item("sq_pay_type")
            sq_need_date.DateTime = .Item("sq_need_date")
            sq_pi_id.EditValue = .Item("sq_pi_id")
            sq_pi_id.EditValue = .Item("sq_pi_area_id")
            sq_ptnr_id_sold.Tag = SetInteger(.Item("sq_ptnr_id_sold"))
            sq_ptnr_id_sold.Text = SetString(.Item("ptnr_name_sold"))
            sq_ref_po_code.Text = SetString(.Item("sq_ref_po_code"))
            sq_ref_po_code.Enabled = True
            sq_bantu_address.Text = Trim(.Item("ptnra_line_1") + .Item("ptnra_line_2") + .Item("ptnra_line_3"))
            sq_sales_person.EditValue = .Item("sq_sales_person")
            sq_si_id.EditValue = .Item("sq_si_id")
            sq_trans_rmks.Text = SetString(.Item("sq_trans_rmks"))
            sq_type.EditValue = .Item("sq_type")

            sq_is_package.EditValue = SetBitYNB(.Item("sq_is_package"))

            sq_pt_id.Tag = SetString(.Item("sq_pt_id"))
            sq_pt_id.EditValue = .Item("pt_code")
            pt_desc1.EditValue = .Item("pt_desc1")
            pt_desc2.EditValue = .Item("pt_desc2")
            sq_price.EditValue = .Item("sq_price")


        End With

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  sqd_oid, " _
                        & "  sqd_dom_id, " _
                        & "  sqd_en_id, " _
                        & "  sqd_add_by, " _
                        & "  sqd_add_date, " _
                        & "  sqd_upd_by, " _
                        & "  sqd_upd_date, " _
                        & "  sqd_sq_oid, " _
                        & "  sqd_seq, " _
                        & "  sqd_is_additional_charge, " _
                        & "  sqd_si_id, " _
                        & "  sqd_pt_id, " _
                        & "  sqd_rmks, " _
                        & "  sqd_qty, " _
                        & "  sqd_qty_booking, " _
                        & "  sqd_qty_shipment, " _
                        & "  sqd_qty_transfer, " _
                        & "  sqd_qty_allocated, " _
                        & "  sqd_qty_so, " _
                        & "  sqd_qty_outs, " _
                        & "  sqd_um, " _
                        & "  sqd_cost, " _
                        & "  sqd_price, " _
                        & "  sqd_total_amount_price, " _
                        & "  sqd_disc, " _
                        & "  sqd_sales_ac_id, " _
                        & "  sqd_sales_sb_id, " _
                        & "  sqd_sales_cc_id, " _
                        & "  sqd_disc_ac_id, " _
                        & "  sqd_um_conv, " _
                        & "  sqd_qty_real, " _
                        & "  sqd_taxable, " _
                        & "  sqd_tax_inc, " _
                        & "  sqd_tax_class, " _
                        & "  sqd_ppn_type, " _
                        & "  sqd_status, " _
                        & "  sqd_dt, " _
                        & "  sqd_payment, " _
                        & "  sqd_dp, " _
                        & "  sqd_sales_unit, " _
                        & "  sqd_loc_id, " _
                        & "  sqd_serial, " _
                        & "  en_desc, " _
                        & "  si_desc, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_type, " _
                        & "  pt_ls, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                        & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                        & "  sb_desc, " _
                        & "  cc_desc, " _
                        & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                        & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                        & "  tax_class.code_name as sqd_tax_class_name, " _
                        & "  loc_desc, " _
                        & "  sqd_pod_oid, " _
                        & "  sqd_invc_oid " _
                        & "FROM  " _
                        & "  public.sqd_det " _
                        & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                        & "  inner join en_mstr on en_id = sqd_en_id " _
                        & "  inner join si_mstr on si_id = sqd_si_id " _
                        & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                        & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                        & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                        & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                        & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                        & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                        & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                        & " where public.sqd_det.sqd_sq_oid = '" + ds.Tables(0).Rows(row).Item("sq_oid").ToString + "'"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail")

                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()


                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit.AcceptChanges()

        With par_obj
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If ds_edit.Tables(0).Rows(i).Item("sqd_qty") > 0 Then
                    If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString) <> "" Then

                        If sq_booking.Checked = True Then
                            'update karena ada hubungan antara so dan po antar group perusahaan
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update sqd_det set sqd_qty_booking = coalesce(sqd_qty_booking,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty").ToString) _
                                             & " where sqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'kurangkan qty booking
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update invc_mstr set invc_qty_booked = coalesce(invc_qty_booked,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                 & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'pindahkan qty booking
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update invc_mstr set invc_qty_available = coalesce(invc_qty_available,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                 & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                        End If


                        'If sq_alocated.Checked = True Then
                        'update karena ada hubungan antara so dan po antar group perusahaan
                        If sq_alocated.Checked = True Then
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update sqd_det set sqd_qty_allocated = coalesce(sqd_qty_allocated,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                 & " where sqd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            ''End If
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update invc_mstr set invc_qty_alloc = coalesce(invc_qty_alloc,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                 & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()




                        End If



                    End If
                    'End If

                    'If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                    '    '.Command.CommandType = CommandType.Text
                    '    .Command.CommandText = "update invc_mstr set invc_qty_available = coalesce(invc_qty_available,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                    '                         & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                    '    ssqls.Add(.Command.CommandText)
                    '    .Command.ExecuteNonQuery()
                    '    '.Command.Parameters.Clear()

                    'End If

                    ''jika booking tambahkan nilai booking
                    'If sq_booking.Checked = True Then
                    '    '.Command.CommandType = CommandType.Text
                    '    .Command.CommandText = "update invc_mstr set invc_qty_booked = coalesce(invc_qty_booked,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                    '                         & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                    '    ssqls.Add(.Command.CommandText)
                    '    .Command.ExecuteNonQuery()
                    '    '.Command.Parameters.Clear()

                    'End If
                End If
            Next
        End With

        Return insert_return
    End Function

    'Private Function insert_transfer(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_sq_code As String) As Boolean
    '    insert_transfer = True

    '    Dim _ptsfr_oid As Guid = Guid.NewGuid
    '    'Dim _ptsfrd_oid As Guid = Guid.NewGuid

    '    Dim _serial, _pt_code, _ptsfr_code, _ptsfrd_pbd_oid As String
    '    'Dim _cost_methode As String
    '    Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
    '    Dim _cost, _cost_avg As Double
    '    Dim i, i_2 As Integer
    '    Dim ssqls As New ArrayList
    '    Dim _ptsfr_trn_satatus As String
    '    Dim _ptsfr_trn_id As Integer
    '    Dim ds_bantu As New DataSet
    '    Dim _qty As Double

    '    '_ptsfr_oid = Guid.NewGuid

    '    ds_edit.AcceptChanges()

    '    'If cek_duplikat_pt_id(gv_edit, "pt_id") = False Then
    '    '    Return False
    '    'End If

    '    _ptsfr_code = func_coll.get_transaction_number("TI", sq_en_id.GetColumnValue("en_code"), "ptsfr_mstr", "ptsfr_code")

    '    'MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)

    '    Dim _ptsfr_receive_date As String

    '    'If sq_booking.Checked = True = True Then
    '    '    _ptsfr_trn_satatus = "C" 'Lansung Default Ke Draft
    '    '    _ptsfr_receive_date = "current_timestamp"
    '    'Else
    '    _ptsfr_trn_satatus = "C" 'Lansung Default Ke Draft
    '    _ptsfr_receive_date = "null"
    '    'End If

    '    'Try
    '    '    Using objinsert As New master_new.CustomCommand
    '    '        With objinsert
    '.Command.Open()
    '    '            ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
    '    '            Try
    '    '                '.Command = .Connection.CreateCommand
    '    '                '.Command.Transaction = sqlTran
    '    With par_obj
    '        '.Command.CommandType = CommandType.Text
    '        .Command.CommandText = "INSERT INTO  " _
    '                            & "  public.ptsfr_mstr " _
    '                            & "( " _
    '                            & "  ptsfr_oid, " _
    '                            & "  ptsfr_dom_id, " _
    '                            & "  ptsfr_en_id, " _
    '                            & "  ptsfr_add_by, " _
    '                            & "  ptsfr_add_date, " _
    '                            & "  ptsfr_en_to_id, " _
    '                            & "  ptsfr_code, " _
    '                            & "  ptsfr_date, " _
    '                            & "  ptsfr_si_id, " _
    '                            & "  ptsfr_loc_id, " _
    '                            & "  ptsfr_loc_git, " _
    '                            & "  ptsfr_remarks, " _
    '                            & "  ptsfr_auto_receipts, " _
    '                            & "  ptsfr_receive_date, " _
    '                            & "  ptsfr_dt, " _
    '                            & "  ptsfr_loc_to_id, " _
    '                            & "  ptsfr_trans_id, " _
    '                            & "  ptsfr_si_to_id, " _
    '                            & "  ptsfr_so_oid, " _
    '                            & "  ptsfr_sq_oid, " _
    '                            & "  ptsfr_pb_oid " _
    '                            & ")  " _
    '                            & "VALUES ( " _
    '                            & SetSetring(_ptsfr_oid.ToString) & ",  " _
    '                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                            & SetInteger(sq_en_id.EditValue) & ",  " _
    '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
    '                            & SetSetring(_ptsfr_code) & ",  " _
    '                            & SetDate(sq_date.DateTime) & ",  " _
    '                            & SetInteger(sq_si_id.EditValue) & ",  " _
    '                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
    '                            & SetInteger(sq_ptsfr_loc_git.EditValue) & ",  " _
    '                            & SetSetring(sq_trans_rmks.Text) & ",  " _
    '                            & SetBitYN(sq_booking.Checked) & ",  " _
    '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                            & SetInteger(sq_ptsfr_loc_to_id.EditValue) & ",  " _
    '                            & SetSetring(_ptsfr_trn_satatus) & ",  " _
    '                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
    '                            & SetSetring(_so_oid) & ",  " _
    '                            & SetSetring(par_sq_oid.ToString) & ",  " _
    '                            & SetSetring(_pb_oid) & "  " _
    '                            & ")"
    '        ssqls.Add(.Command.CommandText)
    '        .Command.ExecuteNonQuery()
    '        '.Command.Parameters.Clear()

    '        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '            If ds_edit.Tables(0).Rows(i).Item("sqd_qty") > 0 Then

    '                '.Command.CommandType = CommandType.Text
    '                .Command.CommandText = "INSERT INTO  " _
    '                                    & "  public.ptsfrd_det " _
    '                                    & "( " _
    '                                    & "  ptsfrd_oid, " _
    '                                    & "  ptsfrd_ptsfr_oid, " _
    '                                    & "  ptsfrd_seq, " _
    '                                    & "  ptsfrd_pt_id, " _
    '                                    & "  ptsfrd_qty,ptsfrd_qty_receive, " _
    '                                    & "  ptsfrd_um, " _
    '                                    & "  ptsfrd_cost, " _
    '                                    & "  ptsfrd_dt," _
    '                                    & "  ptsfrd_remarks, " _
    '                                    & "  ptsfrd_sqd_oid " _
    '                                    & ")  " _
    '                                    & "VALUES ( " _
    '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                    & SetSetring(_ptsfr_oid.ToString) & ",  " _
    '                                    & SetInteger(i) & ",  " _
    '                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")) & ",  " _
    '                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
    '                                    & IIf(sq_booking.Checked = True, SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")), "null") & "," _
    '                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
    '                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
    '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_rmks").ToString) & ", " _
    '                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString) & " " _
    '                                    & ")"

    '                ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                '.Command.Parameters.Clear()

    '                If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString) <> "" Then
    '                    'update karena ada hubungan antara so dan po antar group perusahaan
    '                    '.Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "update sqd_det set sqd_qty_booking = coalesce(sqd_qty_booking,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty").ToString) _
    '                                     & " where sqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString + "'"
    '                    ssqls.Add(.Command.CommandText)
    '                    .Command.ExecuteNonQuery()
    '                    '.Command.Parameters.Clear()

    '                End If

    '                'If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString) <> "" Then
    '                'update location asal ke location tujuan
    '                '.Command.CommandType = CommandType.Text
    '                .Command.CommandText = "update sqd_det set sqd_loc_id = " & SetInteger(sq_ptsfr_loc_to_id.EditValue) & " " + _
    '                                  " where sqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString + "'"
    '                ssqls.Add(.Command.CommandText)
    '                .Command.ExecuteNonQuery()
    '                '.Command.Parameters.Clear()
    '                'End If

    '                ''Update sqd_qty_booking
    '                ''.Command.CommandType = CommandType.Text
    '                '.Command.CommandText = "update sqd_det set sqd_qty_booking = coalesce(sqd_qty_booking,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty").ToString) _
    '                '                     & " where sqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString + "'"
    '                'ssqls.Add(.Command.CommandText)
    '                '.Command.ExecuteNonQuery()
    '                ''.Command.Parameters.Clear()

    '                ''Update sqd_loc_booking
    '                ''.Command.CommandType = CommandType.Text
    '                '.Command.CommandText = "update sqd_det set sqd_loc_id = coalesce(sqd_loc_id,0) + " + SetInteger(sq_ptsfr_loc_to_id.EditValue) _
    '                '                     & " where sqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString + "'"
    '                'ssqls.Add(.Command.CommandText)
    '                '.Command.ExecuteNonQuery()
    '                ''.Command.Parameters.Clear()

    '                'SetInteger(sq_ptsfr_loc_to_id.EditValue) & ",  " _



    '            End If
    '        Next


    '        '*********************************************************************************
    '        'Proses Pengurangan untuk lokasi asal
    '        'Update Table Inventory Dan Cost Inventory Dan History Inventory
    '        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial

    '        i_2 = 0
    '        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '            If ds_edit.Tables(0).Rows(i).Item("sqd_qty") > 0 Then
    '                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
    '                    i_2 += 1

    '                    _en_id = sq_en_id.EditValue
    '                    _si_id = sq_si_id.EditValue
    '                    _loc_id = ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")
    '                    _pt_id = ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")
    '                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
    '                    _serial = "''"
    '                    _qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")

    '                    If func_coll.update_invc_mstr_minus(ssqls, par_obj, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
    '                        Return False
    '                    End If

    '                    'Update History Inventory                                    
    '                    _qty = _qty * -1.0
    '                    _cost = ds_edit.Tables(0).Rows(i).Item("sqd_cost")
    '                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
    '                    If func_coll.update_invh_mstr(ssqls, par_obj, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue Return", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", sq_date.DateTime) = False Then
    '                        Return False
    '                    End If
    '                End If
    '            End If
    '        Next


    '        '*********************************************************************************
    '        'Proses penambahan (+) untuk lokasi git
    '        'Update Table Inventory Dan Cost Inventory Dan History Inventory
    '        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
    '        i_2 = 0
    '        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '            If ds_edit.Tables(0).Rows(i).Item("sqd_qty") > 0 Then
    '                If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
    '                    i_2 += 1

    '                    _en_id = sq_en_id.EditValue
    '                    _si_id = sq_si_id.EditValue
    '                    If sq_booking.Checked = True Then
    '                        _loc_id = sq_ptsfr_loc_to_id.EditValue
    '                    Else
    '                        _loc_id = sq_ptsfr_loc_git.EditValue
    '                    End If

    '                    _pt_id = ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")
    '                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
    '                    _serial = "''"
    '                    _qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
    '                    If func_coll.update_invc_mstr_plus(ssqls, par_obj, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
    '                        ''sqlTran.Rollback()
    '                        'insert_transfer = False
    '                        Return False
    '                    End If

    '                    'Update History Inventory                                    
    '                    _cost = ds_edit.Tables(0).Rows(i).Item("sqd_cost")
    '                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
    '                    If func_coll.update_invh_mstr(ssqls, par_obj, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", sq_date.DateTime) = False Then
    '                        ''sqlTran.Rollback()
    '                        'insert_transfer = False
    '                        Return False
    '                    End If
    '                End If
    '            End If
    '        Next

    '    End With
    '    Return insert_transfer
    'End Function

    Private Function insert_sokp_piutang(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_ref As String, ByVal par_dp As Double, ByVal par_amount As Double) As Boolean
        insert_sokp_piutang = True
        Dim i, _interval As Integer

        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_usr_1 From code_mstr " + _
                                           " where code_field = 'payment_type' " + _
                                           " and code_id = " + sq_pay_type.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _interval = .DataReader("code_usr_1").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If sq_need_date.Text = "" Then
            MessageBox.Show("Need Date Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _date As Date = sq_need_date.DateTime

        With par_obj
            Try
                'Untuk Insert Yang DP
                'DP harus masuk juga ke kartu piutang....agar pada saat ar jadi pas..
                '.Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.sokp_piutang " _
                                    & "( " _
                                    & "  sokp_oid, " _
                                    & "  sokp_sq_oid, " _
                                    & "  sokp_seq, " _
                                    & "  sokp_ref, " _
                                    & "  sokp_amount, " _
                                    & "  sokp_due_date, " _
                                    & "  sokp_amount_pay, " _
                                    & "  sokp_description " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(par_sq_oid) & ",  " _
                                    & SetInteger(0) & ",  " _
                                    & SetSetring(par_ref) & ",  " _
                                    & SetDbl(par_dp) & ",  " _
                                    & SetDate(_date) & "," _
                                    & SetDbl(0) & ",  " _
                                    & SetSetring("-") & "  " _
                                    & ")"
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                '.Command.Parameters.Clear()

                For i = 0 To _interval - 1
                    _date = _date.AddMonths(1)

                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.sokp_piutang " _
                                        & "( " _
                                        & "  sokp_oid, " _
                                        & "  sokp_sq_oid, " _
                                        & "  sokp_seq, " _
                                        & "  sokp_ref, " _
                                        & "  sokp_amount, " _
                                        & "  sokp_due_date, " _
                                        & "  sokp_amount_pay, " _
                                        & "  sokp_description " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(par_sq_oid) & ",  " _
                                        & SetInteger(i + 1) & ",  " _
                                        & SetSetring(par_ref) & ",  " _
                                        & SetDbl(par_amount) & ",  " _
                                        & SetDate(_date) & ",  " _
                                        & SetDbl(0) & ",  " _
                                        & SetSetring("-") & "  " _
                                        & ")"
                    ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    '.Command.Parameters.Clear()
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

        Return insert_sokp_piutang
    End Function

    Public Overrides Function edit_data() As Boolean

        Dim _sq_oid As Guid = Guid.NewGuid
        Dim _sq_code, _sq_terbilang As String
        Dim _sq_total As Double


        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_trans_id") <> "D" Then
            MessageBox.Show("Can't Edit Sales Quotation That Has Been Processed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("detail").Rows.Count - 1
            If SetNumber(ds.Tables("detail").Rows(i).Item("sqd_qty_transfer")) > 0 Then
                MessageBox.Show("Data already transfer..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        'If sq_booking.Checked = True Then
        '    MessageBox.Show("Edit Data Not Available Booking Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Return False
        'End If

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _sq_oid_mstr = .Item("sq_oid")
                sq_ar_sb_id.EditValue = CInt(.Item("sq_ar_sb_id"))
                sq_ar_ac_id.EditValue = CInt(.Item("sq_ar_ac_id"))
                sq_ar_cc_id.EditValue = CInt(.Item("sq_ar_cc_id"))
                sq_cu_id.EditValue = .Item("sq_cu_id")
                sq_en_id.EditValue = .Item("sq_en_id")
                sq_date.DateTime = .Item("sq_date")
                sq_exc_rate.EditValue = .Item("sq_exc_rate")
                sq_pay_method.EditValue = .Item("sq_pay_method")
                sq_sales_program.EditValue = .Item("sq_sales_program")

                sq_rebooking.EditValue = SetBitYNB(.Item("sq_rebooking"))
                sq_booking.EditValue = SetBitYNB(.Item("sq_booking"))
                sq_cons.EditValue = SetBitYNB(.Item("sq_cons"))
                sq_alocated.EditValue = SetBitYNB(.Item("sq_alocated"))

                sq_ptsfr_loc_id.EditValue = .Item("sq_ptsfr_loc_id")
                sq_ptsfr_loc_to_id.EditValue = .Item("sq_ptsfr_loc_to_id")
                sq_ptsfr_loc_git.EditValue = .Item("sq_ptsfr_loc_git")
                sq_alocated.EditValue = SetBitYNB(.Item("sq_alocated"))
                sq_book_start_date.DateTime = .Item("sq_book_start_date")
                sq_book_end_date.DateTime = .Item("sq_book_end_date")
                sq_pay_type.EditValue = .Item("sq_pay_type")
                sq_need_date.DateTime = .Item("sq_need_date")
                sq_pi_id.EditValue = .Item("sq_pi_id")
                sq_pidd_area_id.EditValue = .Item("sq_pi_area_id")
                sq_ptnr_id_sold.Tag = SetInteger(.Item("sq_ptnr_id_sold"))
                sq_ptnr_id_sold.Text = SetString(.Item("ptnr_name_sold"))
                sq_ref_po_code.Text = SetString(.Item("sq_ref_po_code"))
                sq_ref_po_code.Enabled = True
                sq_bantu_address.Text = Trim(.Item("ptnra_line_1") + .Item("ptnra_line_2") + .Item("ptnra_line_3"))
                sq_sales_person.EditValue = .Item("sq_sales_person")
                sq_si_id.EditValue = .Item("sq_si_id")
                sq_trans_rmks.Text = SetString(.Item("sq_trans_rmks"))
                sq_type.EditValue = .Item("sq_type")
                _sq_total = .Item("sq_total")
                'sq_crlmt_reff.Text = .Item("sq_crlmt_reff")
                sq_is_package.EditValue = SetBitYNB(.Item("sq_is_package"))

                sq_pt_id.Tag = SetString(.Item("sq_pt_id"))
                sq_pt_id.EditValue = .Item("pt_code")
                pt_desc1.EditValue = .Item("pt_desc1")
                pt_desc2.EditValue = .Item("pt_desc2")
                sq_price.EditValue = .Item("sq_price")

                If sq_is_package.EditValue = True Then
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
                    gv_edit.BestFitColumns()
                Else
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
                    gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = True
                    gv_edit.BestFitColumns()
                End If
            End With

            sq_en_id.Focus()
            sq_cu_id.Enabled = False
            sq_ptnr_id_sold.Enabled = False
            sq_bantu_address.Enabled = False
            sq_pay_type.Enabled = False
            sq_type.Enabled = False
            sq_pi_id.Enabled = False

            sq_booking.Enabled = False
            sq_rebooking.Enabled = False
            sq_cons.Enabled = False
            sq_alocated.Enabled = False

            sq_ptsfr_loc_id.Enabled = True
            sq_ptsfr_loc_to_id.Enabled = True
            sq_ptsfr_loc_git.Enabled = True

            sq_book_start_date.Enabled = False
            sq_book_end_date.Enabled = False

            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try


            ds_edit = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sqd_oid, " _
                            & "  sqd_dom_id, " _
                            & "  sqd_en_id, " _
                            & "  sqd_add_by, " _
                            & "  sqd_add_date, " _
                            & "  sqd_upd_by, " _
                            & "  sqd_upd_date, " _
                            & "  sqd_sq_oid, " _
                            & "  sqd_seq, " _
                            & "  sqd_is_additional_charge, " _
                            & "  sqd_si_id, " _
                            & "  sqd_pt_id, " _
                            & "  sqd_rmks, " _
                            & "  sqd_qty, " _
                            & "  sqd_qty_booking, " _
                            & "  sqd_qty_shipment, " _
                            & "  sqd_qty_transfer, " _
                            & "  sqd_qty_so, " _
                            & "  sqd_qty_shipment, " _
                            & "  sqd_qty_outs, " _
                            & "  sqd_um, " _
                            & "  sqd_cost, " _
                            & "  sqd_price, " _
                            & "  sqd_total_amount_price, " _
                            & "  sqd_disc, " _
                            & "  sqd_sales_ac_id, " _
                            & "  sqd_sales_sb_id, " _
                            & "  sqd_sales_cc_id, " _
                            & "  sqd_disc_ac_id, " _
                            & "  sqd_um_conv, " _
                            & "  sqd_qty_real, " _
                            & "  sqd_taxable, " _
                            & "  sqd_tax_inc, " _
                            & "  sqd_tax_class, " _
                            & "  sqd_ppn_type, " _
                            & "  sqd_status, " _
                            & "  sqd_dt, " _
                            & "  sqd_payment, " _
                            & "  sqd_dp, " _
                            & "  sqd_sales_unit, " _
                            & "  sqd_loc_id, " _
                            & "  sqd_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_type, " _
                            & "  pt_ls, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sqd_tax_class_name, " _
                            & "  loc_desc, " _
                            & "  sqd_pod_oid, " _
                            & "  sqd_invc_oid, " _
                            & "  sqd_invc_qty " _
                            & "FROM  " _
                            & "  public.sqd_det " _
                            & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                            & "  inner join en_mstr on en_id = sqd_en_id " _
                            & "  inner join si_mstr on si_id = sqd_si_id " _
                            & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                            & " where public.sqd_det.sqd_sq_oid = '" + ds.Tables(0).Rows(row).Item("sq_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")

                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_booking") = "Y" Then
            '    Try
            '        Using objinsert As New master_new.CustomCommand
            '            With objinsert
            '.Command.Open()
            '                ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
            '                Try
            '                    '.Command = .Connection.CreateCommand
            '                    '.Command.Transaction = sqlTran
            '                    'If sq_booking.Checked = True Then
            '                    'If sq_booking.GetColumnValue("code_usr_1") = 0 Then

            '                    If insert_return(objinsert, _sq_oid.ToString, _sq_code) = False Then
            '                        'sqlTran.Rollback()
            '                        'insert = False
            '                        Exit Function
            '                    End If
            '                Catch ex As Exception
            '                End Try
            '            End With
            '        End Using
            '    Catch ex As Exception
            '    End Try
            'End If
            'MessageBox.Show("Can't Edit Sales Quotation Booking", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Return False
            'End If

            edit_data = True
        End If

        'cek kredit limit
        'If func_coll.get_conf_file("limit_so_by_drcr") = "1" Then

        '    If SetInteger(sq_crlmt_reff.EditValue) <> 0 Then
        '        If _sq_total > SetInteger(sq_crlmt_reff.EditValue) Then
        '            Box("Your credit is lacking for this order")
        '            Return False
        '        End If
        '    End If
        'End If

    End Function

    Public Overrides Function edit()
        edit = True
        Dim _sq_total, _sqd_qty, _sqd_price, _sqd_disc, _sq_total_ppn, _sq_total_pph, _sq_total_temp, _sq_total_jml, _tax_rate, _sqd_qty_booking As Double
        Dim _sqd_qty_shipment As Double = 0
        Dim i As Integer
        Dim _sq_terbilang As String

        Dim _sq_trn_status As String
        Dim ds_bantu As New DataSet
        _sq_trn_status = "D" 'set default langsung ke D


        ssqls.Clear()

        '******* Mencari Total so, Total PPN, Total PPH
        _sq_total = 0
        _sq_total_ppn = 0
        _sq_total_pph = 0
        _sq_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class"))
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
            Else
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
            End If

            _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
            _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")


            _sq_total = _sq_total + ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
                Else
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
                End If

                _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
                _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")

                _sq_total_temp = ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
                _sq_total_ppn = _sq_total_ppn + (_sq_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
                _sq_total_pph = _sq_total_pph + (_sq_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
            End If
        Next
        '*******************************************************

        _sq_terbilang = func_bill.TERBILANG_FIX(_sq_total + _sq_total_ppn - _sq_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("sqd_dp") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("sqd_payment") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
        Next
        '*************************

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
                                            & "  public.sq_mstr   " _
                                            & "SET  " _
                                            & "  sq_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  sq_en_id = " & SetInteger(sq_en_id.EditValue) & ",  " _
                                            & "  sq_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  sq_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sq_date = " & SetDate(sq_date.DateTime) & ",  " _
                                            & "  sq_ptnr_id_sold = " & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                            & "  sq_ref_po_code = " & SetSetring(sq_ref_po_code.Text) & ",  " _
                                            & "  sq_ref_po_oid = " & SetSetring("") & ",  " _
                                            & "  sq_si_id = " & SetInteger(sq_si_id.EditValue) & ",  " _
                                            & "  sq_sales_person = " & SetInteger(sq_sales_person.EditValue) & ",  " _
                                            & "  sq_pay_method = " & SetInteger(sq_pay_method.EditValue) & ",  " _
                                            & "  sq_sales_program = " & SetSetring(sq_sales_program.EditValue) & ",  " _
                                            & "  sq_booking = " & SetBitYN(sq_booking.EditValue) & ",  " _
                                            & "  sq_rebooking = " & SetBitYN(sq_rebooking.EditValue) & ",  " _
                                            & "  sq_alocated = " & SetBitYN(sq_alocated.EditValue) & ",  " _
                                            & "  sq_cons = " & SetBitYN(sq_cons.EditValue) & ",  " _
                                            & "  sq_ptsfr_loc_id = " & SetInteger(sq_ptsfr_loc_id.EditValue) & ",  " _
                                            & "  sq_ptsfr_loc_to_id = " & SetInteger(sq_ptsfr_loc_to_id.EditValue) & ",  " _
                                            & "  sq_ptsfr_loc_git = " & SetInteger(sq_ptsfr_loc_git.EditValue) & ",  " _
                                            & "  sq_book_start_date = " & SetDate(sq_book_start_date.DateTime) & ",  " _
                                            & "  sq_book_end_date = " & SetDate(sq_book_end_date.DateTime) & ",  " _
                                            & "  sq_ar_ac_id = " & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
                                            & "  sq_ar_sb_id = " & SetInteger(sq_ar_sb_id.EditValue) & ",  " _
                                            & "  sq_ar_cc_id = " & SetInteger(sq_ar_cc_id.EditValue) & ",  " _
                                            & "  sq_dp = " & SetDbl(_total_dp) & ",  " _
                                            & "  sq_total = " & SetDbl(_sq_total) & ",  " _
                                            & "  sq_need_date = " & SetDate(sq_need_date.DateTime) & ",  " _
                                            & "  sq_tran_id = " & SetInteger("") & ",  " _
                                            & "  sq_trans_id = " & SetSetring(_sq_trn_status) & ",  " _
                                            & "  sq_trans_rmks = " & SetSetring(sq_trans_rmks.Text) & ",  " _
                                            & "  sq_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sq_total_ppn = " & SetDbl(_sq_total_ppn) & ",  " _
                                            & "  sq_total_pph = " & SetDbl(_sq_total_pph) & ",  " _
                                            & "  sq_payment = " & SetDbl(_total_payment) & ",  " _
                                            & "  sq_exc_rate = " & SetDbl(sq_exc_rate.EditValue) & ",  " _
                                            & "  sq_is_package = " & SetBitYN(sq_is_package.EditValue) & ",  " _
                                            & "  sq_pt_id = " & SetInteger(sq_pt_id.Tag) & ",  " _
                                            & "  sq_price = " & SetDec(sq_price.EditValue) & ",  " _
                                            & "  sq_terbilang = " & SetSetring(_sq_terbilang) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sq_oid = " & SetSetring(_sq_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        'If sq_booking.Checked = True Then
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from sqd_det  " _
                                            & "WHERE  " _
                                            & "  sqd_sq_oid = " & SetSetring(_sq_oid_mstr) & " and sqd_qty_booking is null "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        'Else
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from sqd_det  " _
                                            & "WHERE  " _
                                            & "  sqd_sq_oid = " & SetSetring(_sq_oid_mstr) & " and sqd_qty_transfer is null "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        'End If

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            ' _sqd_qty_shipment = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_qty_shipment")) = True, 0, ds_edit.Tables(0).Rows(i).Item("sqd_qty_shipment"))

                            _sqd_qty_booking = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_qty_booking")) = True, 0, ds_edit.Tables(0).Rows(i).Item("sqd_qty_booking"))



                            If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.sqd_det " _
                                                    & "( " _
                                                    & "  sqd_oid, " _
                                                    & "  sqd_dom_id, " _
                                                    & "  sqd_en_id, " _
                                                    & "  sqd_add_by, " _
                                                    & "  sqd_add_date, " _
                                                    & "  sqd_sq_oid, " _
                                                    & "  sqd_seq, " _
                                                    & "  sqd_is_additional_charge, " _
                                                    & "  sqd_si_id, " _
                                                    & "  sqd_pt_id, " _
                                                    & "  sqd_rmks, " _
                                                    & "  sqd_qty, " _
                                                    & "  sqd_um, " _
                                                    & "  sqd_cost, " _
                                                    & "  sqd_price, " _
                                                    & "  sqd_disc, " _
                                                    & "  sqd_sales_ac_id, " _
                                                    & "  sqd_sales_sb_id, " _
                                                    & "  sqd_sales_cc_id, " _
                                                    & "  sqd_um_conv, " _
                                                    & "  sqd_qty_real, " _
                                                    & "  sqd_taxable, " _
                                                    & "  sqd_tax_inc, " _
                                                    & "  sqd_tax_class, " _
                                                    & "  sqd_ppn_type, " _
                                                    & "  sqd_dt, " _
                                                    & "  sqd_payment, " _
                                                    & "  sqd_dp, " _
                                                    & "  sqd_loc_id, " _
                                                    & "  sqd_sales_unit, " _
                                                    & "  sqd_pod_oid, " _
                                                    & "  sqd_invc_oid, " _
                                                    & "  sqd_invc_qty " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid")) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_sq_oid_mstr.ToString) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_is_additional_charge")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sqd_rmks")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_price")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_disc")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_ac_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_sb_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_cc_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_taxable")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_payment")) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_dp")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_sales_unit")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_pod_oid").ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_qty").ToString) & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                                'baris baru tambahan

                                If _sqd_qty_booking = "0" Then
                                    If sq_booking.Checked = True Then
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update sqd_det set sqd_qty_booking = coalesce(sqd_qty_booking,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                             & " where sqd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid"))
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        'kurangkan nilai qty available
                                        If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update invc_mstr set invc_qty_available = coalesce(invc_qty_available,0) - " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                                 & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()
                                        End If

                                        'tambahkan nilai qty booked
                                        If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update invc_mstr set invc_qty_booked = coalesce(invc_qty_booked,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                                 & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()
                                        End If
                                    End If

                                Else
                                    'jika qty booking lebih besar dr nili baru
                                    If _sqd_qty_booking > SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) Then
                                        'hitung nilai pengurang dari qty booking dg nilsi baru
                                        _sq_total_jml = _sqd_qty_booking - SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
                                        'rubah niali booking dengan nilai baru
                                        If sq_booking.Checked = True Then
                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update sqd_det set sqd_qty_booking = coalesce(sqd_qty_booking,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                                 & " where sqd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid"))
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()

                                        End If
                                        'kurangi nilai booking dengan selisih
                                        If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update invc_mstr set invc_qty_booked = coalesce(invc_qty_booked,0) - " + SetDbl(_sq_total_jml) _
                                                                 & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()
                                        End If
                                        'tambahkan qty available dengan selisih
                                        If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update invc_mstr set invc_qty_available = coalesce(invc_qty_available,0) + " + SetDbl(_sq_total_jml) _
                                                                 & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()
                                        End If

                                    Else
                                        'jika nilai baru lebih besar dari nilai lama, hitung jumlah selisih nya
                                        _sq_total_jml = SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) - _sqd_qty_booking

                                        If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                            'tambahkan nilai booking sqd
                                            If sq_booking.Checked = True Then
                                                '.Command.CommandType = CommandType.Text
                                                .Command.CommandText = "update sqd_det set sqd_qty_booking = coalesce(sqd_qty_booking,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                                     & " where sqd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid"))
                                                ssqls.Add(.Command.CommandText)
                                                .Command.ExecuteNonQuery()
                                                '.Command.Parameters.Clear()
                                            End If
                                            'kurangi nilai qty available dengan selisih
                                            If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                                '.Command.CommandType = CommandType.Text
                                                .Command.CommandText = "update invc_mstr set invc_qty_available = coalesce(invc_qty_available,0) - " + SetDbl(_sq_total_jml) _
                                                                     & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                                ssqls.Add(.Command.CommandText)
                                                .Command.ExecuteNonQuery()
                                                '.Command.Parameters.Clear()
                                            End If
                                            'tambahkan nilai booking dengan selisih
                                            If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString) <> "" Then
                                                '.Command.CommandType = CommandType.Text
                                                .Command.CommandText = "update invc_mstr set invc_qty_booked = coalesce(invc_qty_booked,0) + " + SetDbl(_sq_total_jml) _
                                                                     & " where invc_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                                ssqls.Add(.Command.CommandText)
                                                .Command.ExecuteNonQuery()
                                                '.Command.Parameters.Clear()
                                            End If
                                        End If
                                    End If
                                End If

                            Else
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.sqd_det   " _
                                                    & "SET  " _
                                                    & "  sqd_en_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                                                    & "  sqd_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
                                                    & "  sqd_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sqd_rmks")) & ",  " _
                                                    & "  sqd_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
                                                    & "  sqd_qty_booking = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_booking")) & ",  " _
                                                    & "  sqd_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
                                                    & "  sqd_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
                                                    & "  sqd_price = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_price")) & ",  " _
                                                    & "  sqd_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_disc")) & ",  " _
                                                    & "  sqd_sales_ac_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_ac_id")) & ",  " _
                                                    & "  sqd_sales_sb_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_sb_id")) & ",  " _
                                                    & "  sqd_sales_cc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_cc_id")) & ",  " _
                                                    & "  sqd_um_conv = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
                                                    & "  sqd_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")) & ",  " _
                                                    & "  sqd_taxable = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_taxable")) & ",  " _
                                                    & "  sqd_tax_inc = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")) & ",  " _
                                                    & "  sqd_tax_class = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) & ",  " _
                                                    & "  sqd_ppn_type = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type")) & ",  " _
                                                    & "  sqd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                    & "  sqd_payment = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_payment")) & ",  " _
                                                    & "  sqd_dp = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_dp")) & ",  " _
                                                    & "  sqd_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
                                                    & "  sqd_sales_unit = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_sales_unit")) & ",  " _
                                                    & "  sqd_invc_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("invc_oid")) & "  " _
                                                    & "  sqd_invc_qty = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("invc_qty")) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  sqd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid")) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()


                                'If ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString <> "" Then
                                '    'If invc_qty_booked Then
                                'End If

                                ''For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                                'If ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid").ToString <> "" Then
                                '    'update karena ada hubungan antara so dan po antar group perusahaan
                                '    '.Command.CommandType = CommandType.Text
                                '    .Command.CommandText = "update invc_mstr set invc_qty_booked = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                '                         & " where invc_mstr  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_invc_oid"))
                                '    ssqls.Add(.Command.CommandText)
                                '    .Command.ExecuteNonQuery()
                                '    '.Command.Parameters.Clear()
                                'End If

                            End If
                        Next


                        'If sq_type.EditValue.ToString.ToUpper = "D" Then
                        '    If edit_sokp_piutang(objinsert, _sq_oid_mstr.ToString, _total_payment) = False Then
                        '        'sqlTran.Rollback()
                        '        edit = False
                        '        Exit Function
                        '    End If
                        'End If


                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit SQ " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code"))
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
                        set_row(_sq_oid_mstr, "sq_oid")
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

    Private Function edit_sokp_piutang(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_amount As Double) As Boolean
        edit_sokp_piutang = True

        With par_obj
            Try
                '.Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.sokp_piutang   " _
                                    & "SET  " _
                                    & "  sokp_amount = " & SetDbl(par_amount) & "  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  sokp_sq_oid = " & SetSetring(par_sq_oid) & " "
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                '.Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

        Return edit_sokp_piutang
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "select coalesce(sqd_qty_shipment,0) as sqd_qty_shipment from sqd_det " + _
                           " where sqd_sq_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sqd_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("sqd_qty_shipment") > 0 Then
                MessageBox.Show("Can't Delete Shipment Sales Order...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
    End Function

    Public Overrides Function delete_data() As Boolean
        help_load_data(True)

        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_trans_id") <> "D" Then
            MessageBox.Show("Can't Delete Sales Quotation That Has Been Processed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

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

        ssqls.Clear()

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

                            'Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select sqd_pod_oid from sqd_det where sqd_sq_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString) + ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                            '******************************************************

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from sq_mstr where sq_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()


                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete SO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code"))
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
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Private Sub sq_ptnr_id_sold_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sq_ptnr_id_sold.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = sq_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    'Private Sub sq_sq_ref_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sq_sq_ref_oid.ButtonClick
    '    Dim frm As New FSalesQuotationSearchRebook
    '    frm.set_win(Me)
    '    frm._en_id = sq_en_id.EditValue
    '    frm._date = sq_date.DateTime
    '    frm.type_form = True
    '    'frm._tran_oid = _so_sq_ref_oid
    '    frm.ShowDialog()
    'End Sub

    Private Sub sq_sq_ref_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sq_sq_ref_code.ButtonClick
        Dim frm As New FSalesQuotationSearch
        frm.set_win(Me)
        frm._en_id = sq_en_id.EditValue
        frm._date = sq_date.DateTime
        'frm._ptnr_id = _sq_ptnr_id_sold_mstr
        frm.type_form = True
        'frm._tran_oid = _so_sq_ref_oid
        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit.RowUpdated
        footer()
    End Sub

    Private Sub gv_edit_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.RowCountChanged
        footer()
    End Sub

    Private Sub footer()
        Dim i As Integer
        Dim _dpp As Double = 0
        Dim _dpp_line As Double = 0
        Dim _discount As Double = 0
        Dim _discount_line As Double = 0
        Dim _ppn As Double = 0
        Dim _tax_rate As Double = 0
        Dim _sqd_price As Double = 0

        If ds_edit.Tables.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sqd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) = True, 0, func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
            Else
                _tax_rate = 0
            End If

            If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
            Else
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
            End If

            _dpp = _dpp + (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price)
            _dpp_line = (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price)
            _discount = _discount + (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))
            _discount_line = (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))
            _ppn = _ppn + (_tax_rate * (_dpp_line - _discount_line))

            'If ds_edit.Tables(0).Rows(i).Item("sqd_qty_booking").ToString <> "0" Then

            'MessageBox.Show("Please Specipy Customer Data Before..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            'End If
        Next

        te_dpp.EditValue = _dpp
        te_discount.EditValue = _discount
        te_ppn.EditValue = _ppn
        te_total.EditValue = _dpp - _discount + _ppn
    End Sub

    Private Sub sq_ref_po_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If func_coll.get_conf_file("ref_po_sales_order") = "1" Then
            If sq_ptnr_id_sold.Tag = "" Then
                MessageBox.Show("Please Specipy Customer Data Before..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim frm As New FPurchaseOrderSearch()
                frm.set_win(Me)
                frm._obj = sq_ref_po_code
                frm._sq_ptnr_id_sold_mstr = sq_ptnr_id_sold.Tag
                frm.type_form = True
                frm.ShowDialog()
            End If
        End If
    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String

        '_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_en_id")
        '_type = 10
        '_table = "sq_mstr"
        '_initial = "so"
        '_code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        '_code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        'Dim ds_bantu As New DataSet
        'Dim _sql As String

        '_sql = "SELECT  " _
        '    & "  sq_mstr.sq_code, " _
        '    & "  sq_mstr.sq_ptnr_id_sold, " _
        '    & "  sq_mstr.sq_date, " _
        '    & "  sq_mstr.sq_ptnr_id_bill, " _
        '    & "  sq_mstr.sq_pay_type, " _
        '    & "  sq_mstr.sq_pay_method, " _
        '    & "  sqd_det.sqd_pt_id, " _
        '    & "  sqd_det.sqd_qty, " _
        '    & "  sqd_det.sqd_sq_oid, " _
        '    & "  sqd_det.sqd_um, " _
        '    & "  ptnr_mstr.ptnr_code, " _
        '    & "  ptnr_mstr.ptnr_name, " _
        '    & "  ptnr_mstr.ptnr_id, " _
        '    & "  ptnra_addr.ptnra_id, " _
        '    & "  ptnra_addr.ptnra_line, " _
        '    & "  ptnra_addr.ptnra_line_1, " _
        '    & "  ptnra_addr.ptnra_line_2, " _
        '    & "  ptnra_addr.ptnra_line_3, " _
        '    & "  pt_mstr.pt_id, " _
        '    & "  pt_mstr.pt_code, " _
        '    & "  pt_mstr.pt_desc1, " _
        '    & "  pt_mstr.pt_um, " _
        '    & "  code_mstr.code_id, " _
        '    & "  code_mstr.code_code, " _
        '    & "  code_mstr.code_field, " _
        '    & "  sq_mstr.sq_oid, " _
        '    & "  ptnra_addr.ptnra_oid, " _
        '    & "  ptnrac_cntc.ptnrac_oid, " _
        '    & "  ptnrac_cntc.ptnrac_contact_name, " _
        '    & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
        '    & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
        '    & "FROM " _
        '    & "  sq_mstr " _
        '    & "  INNER JOIN sqd_det ON (sq_mstr.sq_oid = sqd_det.sqd_sq_oid) " _
        '    & "  INNER JOIN ptnr_mstr ON (sq_mstr.sq_ptnr_id_sold = ptnr_mstr.ptnr_id) " _
        '    & "  INNER JOIN ptnra_addr ON (ptnra_addr.ptnra_ptnr_oid = ptnr_mstr.ptnr_oid) " _
        '    & "  INNER JOIN pt_mstr ON (sqd_det.sqd_pt_id = pt_mstr.pt_id) " _
        '    & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id) " _
        '    & "  LEFT OUTER JOIN ptnrac_cntc ON (ptnrac_cntc.addrc_ptnra_oid = ptnra_addr.ptnra_ptnr_oid) " _
        '    & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = sq_oid " _
        '    & "WHERE " _
        '    & "sq_mstr.sq_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code") + "'"

        'Dim frm As New frmPrintDialog
        'frm._ssql = _sql
        'frm._report = "XRSO"
        'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        'frm.ShowDialog()

        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_en_id")
        _type = 10
        _table = "sq_mstr"
        _initial = "sq"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  sq_mstr.sq_code, " _
            & "  sq_mstr.sq_ptnr_id_sold, " _
            & "  sq_mstr.sq_date, " _
            & "  sq_mstr.sq_ptnr_id_bill, " _
            & "  sq_mstr.sq_pay_type, " _
            & "  sq_mstr.sq_pay_method, " _
            & "  sq_mstr.sq_sales_program, " _
            & "  sqd_det.sqd_pt_id, " _
            & "  sqd_det.sqd_qty, " _
            & "  sqd_det.sqd_sq_oid, " _
            & "  sqd_det.sqd_um, " _
            & "  ptnr_mstr.ptnr_code, " _
            & "  ptnr_mstr.ptnr_name, " _
            & "  ptnr_mstr.ptnr_id, " _
            & "  ptnra_addr.ptnra_id, " _
            & "  ptnra_addr.ptnra_line, " _
            & "  ptnra_addr.ptnra_line_1, " _
            & "  ptnra_addr.ptnra_line_2, " _
            & "  ptnra_addr.ptnra_line_3, " _
            & "  pt_mstr.pt_id, " _
            & "  pt_mstr.pt_code, " _
            & "  pt_mstr.pt_desc1, " _
            & "  pt_mstr.pt_um, " _
            & "  code_mstr.code_id, " _
            & "  code_mstr.code_code as um_name, " _
            & "  code_mstr.code_field, " _
            & "  sq_mstr.sq_oid, " _
            & "  ptnra_addr.ptnra_oid, " _
            & "  ptnrac_cntc.ptnrac_oid, " _
            & "  ptnrac_cntc.ptnrac_contact_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM " _
            & "  sq_mstr " _
            & "  INNER JOIN sqd_det ON (sq_mstr.sq_oid = sqd_det.sqd_sq_oid) " _
            & "  INNER JOIN ptnr_mstr ON (sq_mstr.sq_ptnr_id_sold = ptnr_mstr.ptnr_id) " _
            & "  INNER JOIN ptnra_addr ON (ptnra_addr.ptnra_ptnr_oid = ptnr_mstr.ptnr_oid) " _
            & "  INNER JOIN pt_mstr ON (sqd_det.sqd_pt_id = pt_mstr.pt_id) " _
            & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id) " _
            & "  LEFT OUTER JOIN ptnrac_cntc ON (ptnrac_cntc.addrc_ptnra_oid = ptnra_addr.ptnra_ptnr_oid) " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = sq_oid " _
            & "WHERE " _
            & "sq_mstr.sq_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRSQ"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        frm.ShowDialog()

    End Sub

    Private Sub le_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_en_id.EditValueChanged
        Try
            init_le(le_cus_id, "cus_mstr", le_en_id.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        Try
            'load_data_to_detail()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub load_data_to_detail()
        Try
            Try
                If ds.Tables(0).Rows.Count = 0 Then
                    Exit Sub
                End If
            Catch ex As Exception
                Exit Sub
            End Try

            Dim sql As String
            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  sqd_oid, " _
                & "  sqd_dom_id, " _
                & "  sqd_en_id, " _
                & "  sqd_add_by, " _
                & "  sqd_add_date, " _
                & "  sqd_upd_by, " _
                & "  sqd_upd_date, " _
                & "  sqd_sq_oid, " _
                & "  sqd_seq, " _
                & "  sqd_is_additional_charge, " _
                & "  sqd_si_id, " _
                & "  sqd_pt_id, " _
                & "  sqd_rmks, " _
                & "  sqd_qty, " _
                & "  sqd_qty_booking, " _
                & "  sqd_qty_shipment, " _
                & "  sqd_qty_transfer, " _
                & "  sqd_qty_allocated, " _
                & "  sqd_qty_so, " _
                & "  sqd_um, " _
                & "  sqd_cost, " _
                & "  sqd_price, " _
                & "  sqd_disc, " _
                & "  sqd_sales_ac_id, " _
                & "  sqd_sales_sb_id, " _
                & "  sqd_sales_cc_id, " _
                & "  sqd_disc_ac_id, " _
                & "  sqd_um_conv, " _
                & "  sqd_qty_real, " _
                & "  sqd_taxable, " _
                & "  sqd_tax_inc, " _
                & "  sqd_tax_class, " _
                & "  sqd_status, " _
                & "  sqd_dt, " _
                & "  sqd_payment, " _
                & "  sqd_dp, " _
                & "  sqd_sales_unit, " _
                & "  sqd_loc_id, " _
                & "  sqd_serial, " _
                & "  en_desc, " _
                & "  si_desc, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  um_mstr.code_name as um_name, " _
                & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                & "  sb_desc, " _
                & "  cc_desc, " _
                & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                & "  tax_class.code_name as sqd_tax_class_name, " _
                & "  sqd_ppn_type, " _
                & "  loc_desc " _
                & "FROM  " _
                & "  public.sqd_det " _
                & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                & "  inner join en_mstr on en_id = sqd_en_id " _
                & "  inner join si_mstr on si_id = sqd_si_id " _
                & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                & "  where sq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString & "'"

            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            load_data_to_detail()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        MyBase.help_load_data(par)
        gc_detail.DataSource = Nothing
        load_data_to_detail()
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            gv_edit.UpdateCurrentRow()
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "loc_desc" Then
                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
                    ds_edit.Tables(0).Rows(i).Item("sqd_loc_id") = gv_edit.GetRowCellValue(_row, "sqd_loc_id")
                Next
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub file_excel_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles file_excel.ButtonClick
        Try
            Dim dt_temp As New DataTable
            Dim _row As Integer = 0

            Dim ssql As String
            Dim ds As New DataSet
            Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")

            If _file = "" Then
                Exit Sub
            End If

            file_excel.EditValue = _file
            ds = master_new.excelconn.ImportExcel(_file)

            ds_edit.Tables(0).Rows.Clear()
            ds_edit.AcceptChanges()

            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

            For Each dr As DataRow In ds.Tables(0).Rows
                ssql = "SELECT DISTINCT  " _
                    & "  public.en_mstr.en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.pt_mstr.pt_en_id, " _
                    & "  public.pi_mstr.pi_en_id, " _
                    & "  public.pi_mstr.pi_code, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.invc_mstr.invc_oid, " _
                    & "  public.pid_det.pid_pt_id, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.invc_mstr.invc_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pt_mstr.pt_type, " _
                    & "  public.pt_mstr.pt_loc_id, " _
                    & "  public.invc_mstr.invc_loc_id, " _
                    & "  invct_table.invct_cost, " _
                    & "  public.loc_mstr.loc_code, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.loc_mstr.loc_type, " _
                    & "  public.pt_mstr.pt_ls, " _
                    & "  public.invc_mstr.invc_qty, " _
                    & "  public.invc_mstr.invc_qty_alloc, " _
                    & "  public.pt_mstr.pt_um, " _
                    & "  public.pt_mstr.pt_pl_id, " _
                    & "  public.pt_mstr.pt_taxable, " _
                    & "  public.pt_mstr.pt_tax_inc, " _
                    & "  public.pt_mstr.pt_tax_class, " _
                    & "  tax_class_mstr.code_name AS tax_class_name, " _
                    & "  coalesce(pt_approval_status, 'A') AS pt_approval_status, " _
                    & "  public.pt_mstr.pt_ppn_type, " _
                    & "  public.pt_mstr.pt_additional, " _
                    & "  um_mstr.code_name AS um_name " _
                    & "FROM " _
                    & "  public.pt_mstr " _
                    & "  INNER JOIN public.pid_det ON (public.pt_mstr.pt_id = public.pid_det.pid_pt_id) " _
                    & "  INNER JOIN public.pi_mstr ON (public.pid_det.pid_pi_oid = public.pi_mstr.pi_oid) " _
                    & "  INNER JOIN public.invc_mstr ON (public.pid_det.pid_pt_id = public.invc_mstr.invc_pt_id) " _
                    & "  INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr um_mstr ON (public.pt_mstr.pt_um = um_mstr.code_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.invc_mstr.invc_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.pt_mstr.pt_si_id = public.si_mstr.si_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr tax_class_mstr ON (public.pt_mstr.pt_tax_class = tax_class_mstr.code_id) " _
                    & "  left outer join invct_table on invct_pt_id = pt_id " _
                    & " where pt_code ='" & dr("pt_code") & "' " _
                    & " and pi_desc ='" & dr("pi_desc") & "' " _
                    & " and loc_desc ='" & dr("loc_desc") & "' "


                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr_temp As DataRow In dt_temp.Rows

                    Dim ds_bantu As New DataSet

                    Try
                        Using objcb As New master_new.CustomCommand
                            With objcb
                                .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                    & "From pla_mstr  " _
                                                    & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                    & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                    & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                    & "where pla_pl_id = " + dr_temp("pt_pl_id").ToString _
                                                    & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                                .InitializeCommand()
                                .FillDataSet(ds_bantu, "prodline")

                                If ds_bantu.Tables(0).Rows.Count = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                    Exit Sub
                                End If

                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    gv_edit.AddNewRow()
                    gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    gv_edit.SetRowCellValue(_row, "sqd_pt_id", dr_temp("invc_pt_id"))
                    gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                    gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                    gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                    gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                    gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                    gv_edit.SetRowCellValue(_row, "sqd_loc_id", dr_temp("invc_loc_id"))
                    gv_edit.SetRowCellValue(_row, "sqd_invc_oid", dr_temp("invc_oid"))
                    gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                    gv_edit.SetRowCellValue(_row, "sqd_um", dr_temp("pt_um"))
                    gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                    gv_edit.SetRowCellValue(_row, "sqd_cost", dr_temp("invct_cost"))

                    'If _so_type <> "D" Then
                    '    fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                    'End If

                    gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                    gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                    gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                    gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                    gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                    gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                    gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                    gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                    gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                    gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                    gv_edit.SetRowCellValue(_row, "sqd_taxable", dr_temp("pt_taxable"))
                    gv_edit.SetRowCellValue(_row, "sqd_tax_inc", dr_temp("pt_tax_inc"))
                    gv_edit.SetRowCellValue(_row, "sqd_tax_class", dr_temp("pt_tax_class"))
                    gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", dr_temp("tax_class_name"))

                    gv_edit.SetRowCellValue(_row, "sqd_ppn_type", dr_temp("pt_ppn_type"))
                    gv_edit.SetRowCellValue(_row, "sqd_qty", dr("qty"))
                    gv_edit.SetRowCellValue(_row, "sqd_disc", dr("disc"))

                    gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    _row = _row + 1
                    System.Windows.Forms.Application.DoEvents()
                    'Exit Sub
                Next
            Next
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub sq_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sq_pt_id.ButtonClick
        Try
            If sq_is_package.EditValue = False Then
                Box("Please check is package")
                Exit Sub
            End If

            Dim frm As New FSearchPartNumberSearchPi
            frm.set_win(Me)
            'frm._pt_id = so_pt_id.Tag
            frm._en_id = sq_en_id.EditValue
            frm._si_id = sq_si_id.EditValue
            frm._sq_type = sq_type.EditValue
            frm._pi_id = sq_pi_id.EditValue
            frm.grid_call = "header"
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub sq_is_package_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_is_package.EditValueChanged
        Try
            If sq_is_package.EditValue = True Then
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
                gv_edit.BestFitColumns()
            Else
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
                gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = True
                gv_edit.BestFitColumns()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sq_ptnr_id_sold_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_ptnr_id_sold.EditValueChanged
        Try

            Dim ssql As String = ""

            ssql = "select ptnr_ptnrg_id from ptnr_mstr where ptnr_id=" & SetInteger(sq_ptnr_id_sold.Tag)

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                ptnrg_id.EditValue = dr(0)
            Next

            dt_bantu = New DataTable
            If func_coll.get_conf_file("so_price_list_limit_ptnrg_id") = "1" Then
                dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime, ptnrg_id.EditValue))
            Else
                dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
            End If

            Try
                sq_pi_id.Properties.DataSource = dt_bantu
                sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
                sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
                sq_pi_id.ItemIndex = 0
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sq_pi_area_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sq_pidd_area_id.EditValueChanged

    End Sub
End Class