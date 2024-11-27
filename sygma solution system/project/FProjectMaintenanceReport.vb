Imports master_new.ModFunction
Public Class FProjectMaintenanceReport
    Public func_coll As New function_collection
    Dim _now As DateTime
    Private Sub FProjectMaintenanceReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub
    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sold To", "sold_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Bill To", "bill_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sales Person", "sall_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Project Type", "project_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Order Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Start Date", "prj_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "End Date", "prj_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Site", "si_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Credit Terms", "credit_term", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Currency", "cu_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Exchange Rate", "prj_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Taxable", "prj_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Include", "prj_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Total", "prj_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "PPN", "prj_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "PPH", "prj_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "After Tax", "prj_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Ext. Total", "prj_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view1, "Ext. PPN", "prj_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view1, "Ext. PPH", "prj_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view1, "Ext. After Tax", "prj_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view1, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "User Create", "prj_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "prj_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "User Update", "prj_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "prj_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_view1, "prjd_oid", False)
        add_column(gv_view1, "prjd_prj_oid", False)
        add_column(gv_view1, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view1, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view1, "Type", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view1, "prjd_type", False)
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Qty * Cost", "prjd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_view1, "Qty PAO", "prjd_qty_pao", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Qty Open PAO", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Qty MO", "prjd_qty_mo", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Qty DO", "prjd_qty_mo", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Trans ID", "prjd_trans_id", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_view2, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Sold To", "sold_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Bill To", "bill_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Sales Person", "sall_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Project Type", "project_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Order Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "Start Date", "prj_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "End Date", "prj_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "Site", "si_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Credit Terms", "credit_term", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Currency", "cu_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Exchange Rate", "prj_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view2, "Taxable", "prj_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Tax Include", "prj_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Total", "prj_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view2, "PPN", "prj_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view2, "PPH", "prj_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view2, "After Tax", "prj_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view2, "Ext. Total", "prj_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view2, "Ext. PPN", "prj_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view2, "Ext. PPH", "prj_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view2, "Ext. After Tax", "prj_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_view2, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "User Create", "prj_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Date Create", "prj_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "User Update", "prj_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Date Update", "prj_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_view2, "prjc_oid", False)
        add_column(gv_view2, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view2, "prjc_prj_oid", False)
        add_column(gv_view2, "prjc_seq", False)
        add_column(gv_view2, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view2, "Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_view2, "Description1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_view2, "Description2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view2, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_view2, "Qty", "prjc_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_view2, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_view2, "Cost", "prjc_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_view2, "Price", "prjc_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_view2, "Discount", "prjc_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_view2, "UM Conversion", "prjc_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_view2, "Qty Real", "prjc_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_view2, "Qty * Cost", "prjc_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_edit(gv_view2, "Taxable", "prjc_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_view2, "Tax Include", "prjc_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_view2, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)

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

                            .SQL = "SELECT  " _
                            & "  prj_oid, " _
                            & "  prj_dom_id, " _
                            & "  prj_en_id,en_mstr.en_desc, " _
                            & "  prj_add_by, " _
                            & "  prj_add_date, " _
                            & "  prj_upd_by, " _
                            & "  prj_upd_date, " _
                            & "  prj_dt, " _
                            & "  prj_code, " _
                            & "  prj_ptnr_id_sold,sold.ptnr_name as sold_ptnr_name, " _
                            & "  prj_ptnr_id_bill,bill.ptnr_name as bill_ptnr_name, " _
                            & "  prj_sales_person_id,sal.ptnr_name as sall_ptnr_name, " _
                            & "  prj_pjt_code_id,prj_type.code_name as project_type, " _
                            & "  prj_ord_date, " _
                            & "  prj_start_date, " _
                            & "  prj_end_date, " _
                            & "  prj_si_id,si_mstr.si_code, " _
                            & "  prj_cu_id,cu_code, " _
                            & "  prj_exc_rate, " _
                            & "  prj_credit_term,ct.code_name as credit_term, " _
                            & "  prj_taxable, " _
                            & "  prj_tax_inc, " _
                            & "  prj_tax_class,tclass.code_name as tax_class, " _
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
                            & "  prj_ar_ac_id,ac_code,ac_name, " _
                            & "  prj_ar_sb_id,sb_code,sb_desc, " _
                            & "  prj_ar_cc_id,cc_code,cc_desc, " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, en_mstr_detail.en_desc as en_desc_detail, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id,si_mstr_detail.si_desc as si_desc_detail, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id,loc_code,loc_desc, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  ((prjd_qty * prjd_cost) - (prjd_qty * prjd_cost * prjd_disc)) as prjd_qty_cost, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class,tclass_detail.code_name as tax_class_detail, " _
                            & "  prjd_trans_id, " _
                            & "  prjd_qty_pao, " _
                            & "  prjd_qty - prjd_qty_pao as qty_open, " _
                            & "  prjd_qty_mo, " _
                            & "  coalesce(prjd_qty_do,0) as prjd_qty_do, " _
                            & "  prjd_type,type.code_code as code_code " _
                            & "FROM  " _
                            & "  public.prj_mstr  " _
                            & "  inner join en_mstr on en_id = prj_en_id " _
                            & "  inner join prjd_det on prjd_prj_oid = prj_oid " _
                            & "  inner join ptnr_mstr sold on sold.ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join ptnr_mstr bill on bill.ptnr_id = prj_ptnr_id_bill " _
                            & "  inner join ptnr_mstr sal on sal.ptnr_id = prj_sales_person_id " _
                            & "  inner join code_mstr prj_type on prj_type.code_id = prj_pjt_code_id " _
                            & "  inner join si_mstr on si_id = prj_si_id " _
                            & "  inner join cu_mstr on cu_id = prj_cu_id " _
                            & "  inner join code_mstr ct on ct.code_id = prj_credit_term " _
                            & "  inner join code_mstr tclass on tclass.code_id = prj_tax_class " _
                            & "  inner join ac_mstr on ac_id = prj_ar_ac_id " _
                            & "  inner join sb_mstr on sb_id = prj_ar_sb_id " _
                            & "  inner join cc_mstr on cc_id = prj_ar_cc_id " _
                            & "  inner join tran_mstr on tran_id = prj_tran_id " _
                            & "  inner join en_mstr en_mstr_detail on en_mstr_detail.en_id = prjd_en_id " _
                            & "  inner join si_mstr si_mstr_detail on si_mstr_detail.si_id = prjd_si_id " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  inner join code_mstr um on um.code_id =  prjd_um " _
                            & "  inner join code_mstr tclass_detail on tclass_detail.code_id = prjd_tax_class " _
                            & "  inner join code_mstr type on type.code_id = prjd_type " _
                            & " where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")

                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then

                            .SQL = "SELECT  " _
                            & "  prj_oid, " _
                            & "  prj_dom_id, " _
                            & "  prj_en_id,en_mstr.en_desc, " _
                            & "  prj_add_by, " _
                            & "  prj_add_date, " _
                            & "  prj_upd_by, " _
                            & "  prj_upd_date, " _
                            & "  prj_dt, " _
                            & "  prj_code, " _
                            & "  prj_ptnr_id_sold,sold.ptnr_name as sold_ptnr_name, " _
                            & "  prj_ptnr_id_bill,bill.ptnr_name as bill_ptnr_name, " _
                            & "  prj_sales_person_id,sal.ptnr_name as sall_ptnr_name, " _
                            & "  prj_pjt_code_id,prj_type.code_name as project_type, " _
                            & "  prj_ord_date, " _
                            & "  prj_start_date, " _
                            & "  prj_end_date, " _
                            & "  prj_si_id,si_mstr.si_code, " _
                            & "  prj_cu_id,cu_code, " _
                            & "  prj_exc_rate, " _
                            & "  prj_credit_term,ct.code_name as credit_term, " _
                            & "  prj_taxable, " _
                            & "  prj_tax_inc, " _
                            & "  prj_tax_class,tclass.code_name as tax_class, " _
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
                            & "  prj_ar_ac_id,ac_code,ac_name, " _
                            & "  prj_ar_sb_id,sb_code,sb_desc, " _
                            & "  prj_ar_cc_id,cc_code,cc_desc, " _
                            & "  prjc_oid, " _
                            & "  prjc_dom_id, " _
                            & "  prjc_en_id,en_mstr_detail.en_desc, " _
                            & "  prjc_add_by, " _
                            & "  prjc_add_date, " _
                            & "  prjc_upd_by, " _
                            & "  prjc_upd_date, " _
                            & "  prjc_dt, " _
                            & "  prjc_prj_oid, " _
                            & "  prjc_seq, " _
                            & "  prjc_si_id,si_mstr_detail.si_desc, " _
                            & "  prjc_cp_id,cp_code, " _
                            & "  prjc_pt_desc1, " _
                            & "  prjc_pt_desc2, " _
                            & "  prjc_loc_id,loc_code,loc_desc, " _
                            & "  prjc_qty, " _
                            & "  prjc_qty_full, " _
                            & "  prjc_um,um.code_name as unit_measure, " _
                            & "  prjc_cost, " _
                            & "  prjc_price, " _
                            & "  prjc_disc, " _
                            & "  prjc_um_conv, " _
                            & "  prjc_qty_real, " _
                            & "  ((prjc_qty * prjc_cost) - (prjc_qty * prjc_cost * prjc_disc)) as prjc_qty_cost, " _
                            & "  prjc_taxable, " _
                            & "  prjc_tax_inc, " _
                            & "  prjc_tax_class,tclass_detail.code_name as tax_class, " _
                            & "  prjc_trans_id, " _
                            & "  prjc_qty_inv " _
                            & "FROM  " _
                            & "  public.prj_mstr  " _
                            & "  inner join en_mstr on en_id = prj_en_id " _
                            & "  inner join prjc_cust on prjc_prj_oid = prj_oid " _
                            & "  inner join ptnr_mstr sold on sold.ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join ptnr_mstr bill on bill.ptnr_id = prj_ptnr_id_bill " _
                            & "  inner join ptnr_mstr sal on sal.ptnr_id = prj_sales_person_id " _
                            & "  inner join code_mstr prj_type on prj_type.code_id = prj_pjt_code_id " _
                            & "  inner join si_mstr on si_id = prj_si_id " _
                            & "  inner join cu_mstr on cu_id = prj_cu_id " _
                            & "  inner join code_mstr ct on ct.code_id = prj_credit_term " _
                            & "  inner join code_mstr tclass on tclass.code_id = prj_tax_class " _
                            & "  inner join ac_mstr on ac_id = prj_ar_ac_id " _
                            & "  inner join sb_mstr on sb_id = prj_ar_sb_id " _
                            & "  inner join cc_mstr on cc_id = prj_ar_cc_id " _
                            & "  inner join tran_mstr on tran_id = prj_tran_id " _
                            & "  inner join en_mstr en_mstr_detail on en_mstr_detail.en_id = prjc_en_id " _
                            & "  inner join si_mstr si_mstr_detail on si_mstr_detail.si_id = prjc_si_id " _
                            & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                            & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                            & "  inner join code_mstr um on um.code_id =  prjc_um " _
                            & "  inner join code_mstr tclass_detail on tclass_detail.code_id = prjc_tax_class " _
                            & " where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                            .InitializeCommand()
                            .FillDataSet(ds, "view2")
                            gc_view2.DataSource = ds.Tables("view2")
                        End If
                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
End Class
