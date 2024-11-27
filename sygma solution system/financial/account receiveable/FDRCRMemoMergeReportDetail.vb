Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FDRCRMemoMergeReportDetail
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FDRCRMemoMergeReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_req1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_req1.DataTable1)
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Merge Number", "arp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Merge Date", "arp_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_view1, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Effective Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "SO Date", "arso_so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Shipment", "shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "Exc. Rate", "ar_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Price", "harga_sebelum_diskon", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Disc", "diskon", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Price After Disc", "harga_setelah_diskon", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Price Before Disc", "total_bruto", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Total Disc", "total_diskon", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_view1, "Invoiced", "total_invoiced", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        'add_column_copy(gv_view1, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Expected Date", "ar_expt_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Taxable", "ar_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Tax Include", "ar_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Type", "ar_type_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Prepayment", "ar_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_view1, "Prepayment IDR", "ar_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        'add_column_copy(gv_view1, "Account Code Prepayment", "ar_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Account Name Prepayment", "ar_code_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Remarks", "arp_remarks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Total", "ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_view1, "Total Payment", "ar_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_view1, "Outstanding Payment", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_view1, "Total IDR", "ar_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        'add_column_copy(gv_view1, "Total Payment IDR", "ar_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        'add_column_copy(gv_view1, "Outstanding Payment IDR", "ar_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        'add_column_copy(gv_view1, "Status", "ar_status", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_view1, "arso_ar_oid", False)
        'add_column(gv_view1, "sokp_ar_oid", False)
        'add_column(gv_view1, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Seq", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_view1, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Payment Amount", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_view1, "Payment Date", "sokp_date_payment", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "Status", "sokp_status", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "User Create", "ar_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Date Create", "ar_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view1, "User Update", "ar_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Date Update", "ar_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

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
                            & "ar_en_id, " _
                            & "en_desc, " _
                            & "arp_code, " _
                            & "arp_date, " _
                            & "ar_code, " _
                            & "arso_so_code, " _
                            & "arso_so_date, " _
                            & "ar_eff_date, " _
                            & "arp_due_date, " _
                            & "arp_remarks, " _
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
                            & "INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                            & "INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
                            & "INNER JOIN arpd_det ON arpd_ar_oid = ar_oid " _
                            & "INNER JOIN arp_print ON arp_oid = arpd_arp_oid " _
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
                            & " and arp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and arp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and arp_en_id in (select user_en_id from tconfuserentity " _
                            & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "GROUP BY " _
                            & "ar_en_id, " _
                            & "en_desc, " _
                            & "arp_code, " _
                            & "arp_date, " _
                            & "ar_eff_date, " _
                            & "ar_code, " _
                            & "arp_remarks, " _
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
                            & "arp_due_date, " _
                            & "bk_name, " _
                            & "bk_code, " _
                            & "credit_term_name, " _
                            & "arso_so_code, " _
                            & "arso_so_date, " _
                            & "um_name " _
                            & "ORDER BY pt_desc1"
                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")
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