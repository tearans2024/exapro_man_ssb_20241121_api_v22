Imports DevExpress.XtraExport
Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FProjectItemCancel
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _prj_oid_mstr, _prjd_oid As String
    Dim ds_edit, ds_get, ds_comment, ds_ptnr As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim _po_en_id_edit As Integer
    Dim _po_date_edit As Date
    Dim _conf_value As String
    Dim _conf_budget As String
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String

#Region "Seting Awal"
    Private Sub FPurchaseOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_project_maintenance")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "sold_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bill To", "bill_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "sall_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Ref. PO", "pocust_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Type", "project_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Area", "prj_area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Order Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Start Date", "prj_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "prj_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Site", "si_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "credit_term", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "prj_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Taxable", "prj_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "prj_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Reserves", "tax_class_reserves", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total", "prj_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "PPN", "prj_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "PPH", "prj_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "After Tax", "prj_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Ext. Total", "prj_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Ext. PPN", "prj_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Ext. PPH", "prj_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Ext. After Tax", "prj_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "prj_remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "prj_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "prj_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "prj_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "prj_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "prjd_oid", False)
        add_column(gv_detail, "prjd_prj_oid", False)
        add_column(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Type", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "prjd_type", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Location Implementation", "loc_desc_imp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty * Price", "prjd_qty_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_detail, "Qty PAO", "prjd_qty_pao", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Open PAO", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty MO", "prjd_qty_mo", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty DO", "prjd_qty_do", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Shipment", "prjd_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Return", "prjd_qty_return", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Open Shipment", "prjd_qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        If func_coll.get_conf_file("show_column_atp_bast") = "1" Then
            add_column_copy(gv_detail, "Qty ATP", "prjd_qty_atp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
            add_column_copy(gv_detail, "Qty BAST", "prjd_qty_bast", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        End If


        add_column_copy(gv_detail, "Qty Invoice", "prjd_qty_inv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Progress Closing", "prjd_progress_pay", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Progress Closing Invoice", "prjd_progress_pay_inv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Status", "prjd_trans_id", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Line Ref.", "prjd_ref_line", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_edit, "#", "status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_oid", False)
        add_column(gv_edit, "prjd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_prj_oid", False)
        add_column(gv_edit, "prjd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Seq", "prjd_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Type", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_type", False)
        add_column(gv_edit, "prjd_pt_id", False)


        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "prjd_loc_imp_id", False)
        'add_column(gv_edit, "Location Implementation", "loc_desc_imp", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "prjd_qty_pao", False)
        add_column(gv_edit, "prjd_qty_mo", False)
        add_column(gv_edit, "prjd_qty_shipment", False)
        add_column(gv_edit, "prjd_progress_pay", False)
        add_column(gv_edit, "prjd_um", False)
        add_column(gv_edit, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "Qty * Price", "prjd_qty_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_edit(gv_edit, "Taxable", "prjd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Tax Include", "prjd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_tax_class", False)
        add_column(gv_edit, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    'rev by hendrik 2011-07-15-----------------------------------
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  prj_oid, " _
                    & "  prj_dom_id, " _
                    & "  prj_en_id,en_desc, " _
                    & "  prj_add_by, " _
                    & "  prj_add_date, " _
                    & "  prj_upd_by, " _
                    & "  prj_upd_date, " _
                    & "  prj_dt, " _
                    & "  prj_code, pocust_code, " _
                    & "  prj_ptnr_id_sold,sold.ptnr_name as sold_ptnr_name, " _
                    & "  prj_ptnr_id_bill,bill.ptnr_name as bill_ptnr_name, " _
                    & "  prj_sales_person_id,sal.ptnr_name as sall_ptnr_name, " _
                    & "  prj_pjt_code_id,prj_type.code_name as project_type, " _
                    & "  prj_ord_date, " _
                    & "  prj_start_date, " _
                    & "  prj_end_date, " _
                    & "  prj_si_id,si_code, " _
                    & "  prj_cu_id,cu_code, " _
                    & "  prj_exc_rate, " _
                    & "  prj_credit_term,ct.code_name as credit_term, " _
                    & "  prj_taxable, " _
                    & "  prj_tax_inc, " _
                    & "  prj_tax_class,tclass.code_name as tax_class, " _
                    & "  prj_tax_class_reserves,tclassre.code_name as tax_class_reserves, " _
                    & "  prj_area_id, " _
                    & "  prj_area.code_name as prj_area_name, " _
                    & "  prj_total, " _
                    & "  prj_total_ppn, " _
                    & "  prj_total_pph, " _
                    & "  (prj_total + prj_total_ppn + prj_total_pph) as prj_total_after_tax," _
                    & "  prj_exc_rate * prj_total as prj_total_ext, " _
                    & "  prj_exc_rate * prj_total_ppn as prj_total_ppn_ext, " _
                    & "  prj_exc_rate * prj_total_pph as prj_total_pph_ext, " _
                    & "  prj_exc_rate * (prj_total + prj_total_ppn + prj_total_pph) as prj_total_after_tax_ext, " _
                    & "  prj_tran_id, tran_name, " _
                    & "  prj_trans_id, " _
                    & "  prj_pocust_oid, " _
                    & "  pocust_code, " _
                    & "  prj_remark, " _
                    & "  prj_ar_ac_id,ac_code,ac_name, " _
                    & "  prj_ar_sb_id,sb_code,sb_desc, " _
                    & "  prj_ar_cc_id,cc_code,cc_desc " _
                    & "FROM  " _
                    & "  public.prj_mstr  " _
                    & "  inner join en_mstr on en_id = prj_en_id " _
                    & "  inner join ptnr_mstr sold on sold.ptnr_id = prj_ptnr_id_sold " _
                    & "  inner join ptnr_mstr bill on bill.ptnr_id = prj_ptnr_id_bill " _
                    & "  inner join ptnr_mstr sal on sal.ptnr_id = prj_sales_person_id " _
                    & "  inner join code_mstr prj_type on prj_type.code_id = prj_pjt_code_id " _
                    & "  left outer join code_mstr prj_area on prj_area.code_id = prj_area_id " _
                    & "  inner join si_mstr on si_id = prj_si_id " _
                    & "  inner join cu_mstr on cu_id = prj_cu_id " _
                    & "  inner join code_mstr ct on ct.code_id = prj_credit_term " _
                    & "  inner join code_mstr tclass on tclass.code_id = prj_tax_class " _
                    & "  left outer join code_mstr tclassre on tclassre.code_id = prj_tax_class_reserves " _
                    & "  inner join ac_mstr on ac_id = prj_ar_ac_id " _
                    & "  inner join sb_mstr on sb_id = prj_ar_sb_id " _
                    & "  inner join cc_mstr on cc_id = prj_ar_cc_id " _
                    & "  left outer join tran_mstr on tran_id = prj_tran_id " _
                    & "  left outer join pocust_mstr on pocust_oid = prj_pocust_oid " _
                    & " where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function
    '-----------------------------------------------------------------------------------------------------

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  prjd_det.prjd_oid, " _
            & "  prjd_det.prjd_dom_id, " _
            & "  prjd_det.prjd_en_id, en_desc, " _
            & "  prjd_det.prjd_add_by, " _
            & "  prjd_det.prjd_add_date, " _
            & "  prjd_det.prjd_upd_by, " _
            & "  prjd_det.prjd_upd_date, " _
            & "  prjd_det.prjd_dt, " _
            & "  prjd_det.prjd_prj_oid, " _
            & "  prjd_det.prjd_seq, " _
            & "  prjd_det.prjd_si_id,si_desc, " _
            & "  prjd_det.prjd_pt_id,pt_code, " _
            & "  prjd_det.prjd_pt_desc1, " _
            & "  prjd_det.prjd_pt_desc2, " _
            & "  prjd_det.prjd_loc_id,loc_mstr.loc_code,loc_mstr.loc_desc, " _
            & "  prjd_det.prjd_loc_imp_id,loc_imp.loc_code as loc_code_imp,loc_imp.loc_desc as loc_desc_imp, " _
            & "  prjd_det.prjd_qty, " _
            & "  prjd_det.prjd_qty_full, " _
            & "  prjd_det.prjd_um,um.code_name as unit_measure, " _
            & "  prjd_det.prjd_cost, " _
            & "  prjd_det.prjd_price, " _
            & "  prjd_det.prjd_disc, " _
            & "  prjd_det.prjd_um_conv, " _
            & "  prjd_det.prjd_qty_real, " _
            & "  ((prjd_det.prjd_qty * prjd_det.prjd_price) - (prjd_det.prjd_qty * prjd_det.prjd_price * prjd_det.prjd_disc)) as prjd_qty_price, " _
            & "  prjd_det.prjd_taxable, " _
            & "  prjd_det.prjd_tax_inc, " _
            & "  prjd_det.prjd_tax_class,tclass.code_name as tax_class, " _
            & "  prjd_det.prjd_trans_id, " _
            & "  prjd_det.prjd_qty_pao, " _
            & "  prjd_det.prjd_qty - prjd_det.prjd_qty_pao as qty_open, " _
            & "  prjd_det.prjd_qty_mo, " _
            & "  coalesce(prjd_det.prjd_qty_shipment,0) as prjd_qty_shipment, " _
            & "  coalesce(prjd_det.prjd_qty_return,0) as prjd_qty_return, " _
            & "  prjd_det.prjd_qty - (coalesce(prjd_det.prjd_qty_shipment,0) - coalesce(prjd_det.prjd_qty_return,0)) as prjd_qty_open, " _
            & "  coalesce(prjd_det.prjd_qty_inv,0) as prjd_qty_inv, " _
            & "  coalesce(prjd_det.prjd_progress_pay,0) as prjd_progress_pay, " _
            & "  coalesce(prjd_det.prjd_progress_pay_inv,0) as prjd_progress_pay_inv, " _
            & "  coalesce(prjd_det.prjd_qty_do,0) as prjd_qty_do, " _
            & "  coalesce(prjd_det.prjd_qty_atp,0) as prjd_qty_atp, " _
            & "  coalesce(prjd_det.prjd_qty_bast,0) as prjd_qty_bast, " _
            & "  prjd_det.prjd_type,type.code_code as code_code, " _
            & "  prjd_ref.prjd_seq as prjd_ref_line " _
            & "FROM  " _
            & "  public.prjd_det " _
            & "  inner join prj_mstr on prj_oid = prjd_det.prjd_prj_oid " _
            & "  inner join en_mstr on en_id = prjd_det.prjd_en_id " _
            & "  inner join si_mstr on si_id = prjd_det.prjd_si_id " _
            & "  inner join pt_mstr on pt_id = prjd_det.prjd_pt_id " _
            & "  inner join loc_mstr on loc_mstr.loc_id = prjd_det.prjd_loc_id " _
            & "  left outer join loc_mstr as loc_imp on loc_imp.loc_id = prjd_det.prjd_loc_imp_id " _
            & "  inner join code_mstr um on um.code_id =  prjd_det.prjd_um " _
            & "  inner join code_mstr tclass on tclass.code_id = prjd_det.prjd_tax_class " _
            & "  inner join code_mstr type on type.code_id = prjd_det.prjd_type " _
            & "  left outer join prjd_det prjd_ref on prjd_ref.prjd_oid = prjd_det.prjd_prjd_oid " _
            & "  where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        load_data_detail(sql, gc_detail, "detail")


    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("prjd_prj_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjd_prj_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Insert Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If _conf_budget = "0" Then
            If ds_edit.Tables(0).Rows.Count = 0 Then
                MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("pod_qty_release") = 0 Then
        '        MessageBox.Show("Qty Release Can't null..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

   
    Public Overrides Function edit_data() As Boolean
       

        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position
            With ds.Tables(0).Rows(row)
                _prj_oid_mstr = .Item("prj_oid")
            End With
            ds_edit = New DataSet

            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  false as status, " _
                            & "  prjd_det.prjd_oid, " _
                            & "  prjd_det.prjd_dom_id, " _
                            & "  prjd_det.prjd_en_id,en_desc, " _
                            & "  prjd_det.prjd_add_by, " _
                            & "  prjd_det.prjd_add_date, " _
                            & "  prjd_det.prjd_upd_by, " _
                            & "  prjd_det.prjd_upd_date, " _
                            & "  prjd_det.prjd_dt, " _
                            & "  prjd_det.prjd_prj_oid, " _
                            & "  prjd_det.prjd_seq, " _
                            & "  prjd_det.prjd_si_id,si_desc, " _
                            & "  prjd_det.prjd_pt_id,pt_code, " _
                            & "  prjd_det.prjd_pt_desc1, " _
                            & "  prjd_det.prjd_pt_desc2, " _
                            & "  prjd_det.prjd_loc_id,loc_mstr.loc_code,loc_mstr.loc_desc, " _
                            & "  prjd_det.prjd_loc_imp_id,loc_imp.loc_code as loc_code_imp,loc_imp.loc_desc as loc_desc_imp, " _
                            & "  prjd_det.prjd_qty, " _
                            & "  prjd_det.prjd_qty_full, " _
                            & "  prjd_det.prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_det.prjd_cost, " _
                            & "  prjd_det.prjd_price, " _
                            & "  prjd_det.prjd_disc, " _
                            & "  prjd_det.prjd_um_conv, " _
                            & "  prjd_det.prjd_qty_real, " _
                            & "  ((prjd_det.prjd_qty * prjd_det.prjd_price) - (prjd_det.prjd_qty * prjd_det.prjd_price * prjd_det.prjd_disc)) as prjd_qty_price, " _
                            & "  prjd_det.prjd_taxable, " _
                            & "  prjd_det.prjd_tax_inc, " _
                            & "  prjd_det.prjd_tax_class,tclass.code_name as tax_class, " _
                            & "  prjd_det.prjd_trans_id, " _
                            & "  coalesce(prjd_det.prjd_qty_pao,0) as prjd_qty_pao, " _
                            & "  coalesce(prjd_det.prjd_qty_mo,0) as prjd_qty_mo, " _
                            & "  coalesce(prjd_det.prjd_qty_shipment,0) as prjd_qty_shipment, " _
                            & "  coalesce(prjd_det.prjd_progress_pay,0) as prjd_progress_pay, " _
                            & "  coalesce(prjd_det.prjd_qty_atp,0) as prjd_qty_atp, " _
                            & "  coalesce(prjd_det.prjd_qty_bast,0) as prjd_qty_bast, " _
                            & "  prjd_det.prjd_type,type.code_code as code_code, " _
                            & "  prjd_ref.prjd_seq as prjd_ref_line " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_det.prjd_prj_oid " _
                            & "  inner join en_mstr on en_id = prjd_det.prjd_en_id " _
                            & "  inner join si_mstr on si_id = prjd_det.prjd_si_id " _
                            & "  inner join pt_mstr on pt_id = prjd_det.prjd_pt_id " _
                            & "  inner join loc_mstr on loc_mstr.loc_id = prjd_det.prjd_loc_id " _
                            & "  left outer join loc_mstr as loc_imp on loc_imp.loc_id = prjd_det.prjd_loc_imp_id " _
                            & "  inner join code_mstr um on um.code_id =  prjd_det.prjd_um " _
                            & "  inner join code_mstr tclass on tclass.code_id = prjd_det.prjd_tax_class " _
                            & "  inner join code_mstr type on type.code_id = prjd_det.prjd_type " _
                            & "  left outer join prjd_det prjd_ref on prjd_ref.prjd_oid = prjd_det.prjd_prjd_oid " _
                            & "  where prjd_det.prjd_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) _
                            & "  and prjd_det.prjd_trans_id <> 'X' order by prjd_det.prjd_seq asc "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")

                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    'rev by hendrik 2011-07-15 --------------------
    Public Overrides Function edit()
        edit = True

        Dim i As Integer

        Dim _budget_administrasi As String = "0"
        Dim _budget_project As String = "0"

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran


                        Dim _total_release As Double = 0
                        For i = 0 To ds_edit.Tables("detail").Rows.Count - 1
                            If ds_edit.Tables("detail").Rows(i).Item("status") = True Then
                                '.Command = .Connection.CreateCommand
                                '.Command.Transaction = sqlTran



                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_trans_id = 'X' " + _
                                                       " where prjd_oid = '" + ds_edit.Tables("detail").Rows(i).Item("prjd_oid") + "'"
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()


                            End If
                        Next


                        .Command.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_prj_oid_mstr, "prj_oid")
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
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
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    

End Class
