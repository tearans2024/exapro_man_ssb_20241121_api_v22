﻿Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FSalesVerificationApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FSalesOrderApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        gv_os.Columns("status").VisibleIndex = 0

        AddHandler gv_os.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_os.ColumnFilterChanged, AddressOf relation_detail

        AddHandler gv_all.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_all.ColumnFilterChanged, AddressOf relation_detail

        _user_approval = get_user_approval()
    End Sub

    Private Function get_user_approval() As String
        get_user_approval = "'" + master_new.ClsVar.sNama + "',"
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "select groupnama from tconfusergroup ug " + _
                           " inner join tconfgroup g on g.groupid = ug.groupid " + _
                           " where userid = " + master_new.ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wfs_status")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            get_user_approval = get_user_approval + "'" + ds_bantu.Tables(0).Rows(i).Item("groupnama") + "',"
        Next
        get_user_approval = get_user_approval.Substring(0, Len(get_user_approval) - 1)
        Return get_user_approval
    End Function

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wfs_status())
        le_wfs_status.Properties.DataSource = dt_bantu
        le_wfs_status.Properties.DisplayMember = dt_bantu.Columns("wfs_desc").ToString
        le_wfs_status.Properties.ValueMember = dt_bantu.Columns("wfs_id").ToString
        le_wfs_status.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_os, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "SV Number", "sv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Effective Date", "sv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "SV Type", "sv_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_os, "Taxable", "sv_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_os, "Tax Include", "sv_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_os, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_os, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Exchange Rate", "sv_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Consignment", "sv_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Prepayment", "sv_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "Discount", "sv_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_os, "Payment", "sv_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "Payment Date", "sv_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Close Date", "sv_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Remarks", "sv_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sales Unit", "sv_sales_unit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Total", "sv_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "PPN", "sv_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "PPH", "sv_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "After Tax", "sv_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "Ext. Total", "sv_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_os, "Ext. PPN", "sv_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_os, "Ext. PPH", "sv_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_os, "Ext. After Tax", "sv_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_os, "User Create", "sv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "sv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "sv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "sv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Status", "sv_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "SV Number", "sv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Effective Date", "sv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "SV Type", "sv_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Taxable", "sv_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Include", "sv_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Exchange Rate", "sv_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Consignment", "sv_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Prepayment", "sv_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "Discount", "sv_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_all, "Payment", "sv_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "Payment Date", "sv_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Close Date", "sv_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Remarks", "sv_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sales Unit", "sv_sales_unit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Total", "sv_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "PPN", "sv_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "PPH", "sv_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "After Tax", "sv_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "Ext. Total", "sv_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_all, "Ext. PPN", "sv_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_all, "Ext. PPH", "sv_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_all, "Ext. After Tax", "sv_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_all, "User Create", "sv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "sv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "sv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "sv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column(gv_detail, "svd_oid", False)
        add_column(gv_detail, "svd_sv_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Additional Charge", "svd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "svd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "svd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Shipment", "svd_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail, "Qty Pending Invoice", "svd_qty_pending_inv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Invoice  ", "svd_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "svd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Price", "svd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Discount", "svd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "svd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Real", "svd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Taxable", "svd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "svd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "svd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Prepayment", "svd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Payment", "svd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Sales Unit", "svd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Serial", "svd_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status", "svd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_attr, "sqa_sv_oid", False)
        add_column_copy(gv_detail_attr, "Tempat Bekerja", "sqa_bekerja_pada", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jabatan", "sqa_jabatan_bagian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Rumah Tinggal", "status_kepemilikan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Lama Tinggal", "lama_tinggal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Masa Kerja", "masa_kerja", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Income", "income", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kepribadian", "kepribadian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Tanggungan", "tanggungan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jaminan", "jaminan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Total Point", "total_point", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Alamat1", "sqa_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Alamat2", "sqa_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Lantai Bekerja", "sqa_kantor_lantai", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Telephone", "sqa_kantor_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "KTP", "sqa_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Email", "sqa_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Alamat1", "sqa_rumah_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Alamat2", "sqa_rumah_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Kode Pos", "sqa_rumah_kode_pos", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Telephone", "sqa_rumah_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Telephone HP", "sqa_rumah_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Alamat Kirim", "sqa_status_alamat_kirim", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Alamat Tagih", "sqa_status_alamat_tagih", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Nama", "sqa_suami_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Tempat Bekerja", "sqa_suami_bekerja", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Jabatan", "sqa_suami_jabatan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Kantor Alamat1", "sqa_suami_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Kantor Alamat2", "sqa_suami_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Telephone", "sqa_suami_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri HP", "sqa_suami_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak Pertama Nama", "sqa_anak_nama_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak Pertama Tgl Lahir", "sqa_anak_tgl_lahir_1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak Pertama Sekolah", "sqa_anak_sekolah_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak kedua Nama", "sqa_anak_nama_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak kedua Tgl Lahir", "sqa_anak_tgl_lahir_2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak kedua Sekolah", "sqa_anak_sekolah_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak ketiga Nama", "sqa_anak_nama_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak ketiga Tgl Lahir", "sqa_anak_tgl_lahir_3", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak ketiga Sekolah", "sqa_anak_sekolah_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Nama", "sqa_keluarga_dekat_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Alamat1", "sqa_keluarga_dekat_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Alamat2", "sqa_keluarga_dekat_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Telephone", "sqa_keluarga_dekat_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat HP", "sqa_keluarga_dekat_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Tempat Tinggal", "sqa_status_tempat_tinggal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jenis Kartu Kredit", "sqa_jenis_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "No Kartu Kredit", "sqa_no_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Berlaku s/d", "sqa_berlaku_sd", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Bank", "sqa_bank", DevExpress.Utils.HorzAlignment.Default)


        add_column(gv_detail_kp, "sokp_sv_oid", False)
        add_column_copy(gv_detail_kp, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_kp, "Amount Pay", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail_kp, "Description", "sokp_description", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "sv_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "SQ Number", "sv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Effective Date", "sv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Exchange Rate", "sv_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Consignment", "sv_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "sv_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Discount", "sv_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Payment Date", "sv_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Close Date", "sv_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Remarks", "sv_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Total", "sv_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "PPN", "sv_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "PPH", "sv_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "After Tax", "sv_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Ext. Total", "sv_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_email, "Ext. PPN", "sv_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_email, "Ext. PPH", "sv_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_email, "Ext. After Tax", "sv_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_email, "User Create", "sv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "sv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "sv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "sv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_email, "svd_sv_oid", False)
        add_column_copy(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "svd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "svd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost", "svd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Price", "svd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Discount", "svd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "UM Conversion", "svd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Qty Real", "svd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Taxable", "svd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Include", "svd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Class", "svd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "svd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Payment", "svd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Sales Unit", "svd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Status", "svd_status", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                'ds = New DataSet
                Using objload As New master_new.CustomCommand
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            Try
                                ds.Tables("os").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  false as status, " _
                                & "  public.sv_mstr.sv_oid, " _
                                & "  public.sv_mstr.sv_dom_id, " _
                                & "  public.sv_mstr.sv_en_id, " _
                                & "  en_desc, " _
                                & "  public.sv_mstr.sv_add_by, " _
                                & "  public.sv_mstr.sv_add_date, " _
                                & "  public.sv_mstr.sv_upd_by, " _
                                & "  public.sv_mstr.sv_code, " _
                                & "  public.sv_mstr.sv_upd_date, " _
                                & "  public.sv_mstr.sv_ptnr_id_sold, " _
                                & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                                & "  public.sv_mstr.sv_ptnr_id_bill, " _
                                & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                                & "  public.sv_mstr.sv_date, " _
                                & "  public.sv_mstr.sv_credit_term, " _
                                & "  public.sv_mstr.sv_taxable, " _
                                & "  public.sv_mstr.sv_tax_class, " _
                                & "  public.sv_mstr.sv_si_id, " _
                                & "  si_desc, " _
                                & "  public.sv_mstr.sv_type, " _
                                & "  public.sv_mstr.sv_sales_person, " _
                                & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                                & "  public.sv_mstr.sv_pi_id, " _
                                & "  public.sv_mstr.sv_pay_type, " _
                                & "  public.sv_mstr.sv_pay_method, " _
                                & "  public.sv_mstr.sv_dp, " _
                                & "  public.sv_mstr.sv_disc_header, " _
                                & "  public.sv_mstr.sv_total, " _
                                & "  public.sv_mstr.sv_due_date, " _
                                & "  public.sv_mstr.sv_print_count, " _
                                & "  public.sv_mstr.sv_close_date, " _
                                & "  public.sv_mstr.sv_tran_id, " _
                                & "  public.sv_mstr.sv_trans_id, " _
                                & "  public.sv_mstr.sv_trans_rmks, " _
                                & "  public.sv_mstr.sv_current_route, " _
                                & "  public.sv_mstr.sv_next_route, " _
                                & "  public.sv_mstr.sv_dt, " _
                                & "  public.sv_mstr.sv_bk_appr, " _
                                & "  public.sv_mstr.sv_cu_id, " _
                                & "  public.sv_mstr.sv_total_ppn, " _
                                & "  public.sv_mstr.sv_payment, " _
                                & "  public.sv_mstr.sv_total_pph, " _
                                & "  public.sv_mstr.sv_exc_rate, " _
                                & "  public.sv_mstr.sv_tax_inc, " _
                                & "  public.sv_mstr.sv_cons, " _
                                & "  public.sv_mstr.sv_terbilang, " _
                                & "  public.sv_mstr.sv_bk_id, " _
                                & "  public.sv_mstr.sv_ref_po_code, " _
                                & "  public.sv_mstr.sv_interval, " _
                                & "  public.sv_mstr.sv_ref_po_oid, " _
                                & "  public.sv_mstr.sv_ppn_type, " _
                                & "  public.sv_mstr.sv_ac_prepaid, " _
                                & "  public.sv_mstr.sv_pay_prepaod, " _
                                & "  public.sv_mstr.sv_ar_sb_id, " _
                                & "  public.sv_mstr.sv_ar_ac_id, " _
                                & "  public.sv_mstr.sv_ar_cc_id, " _
                                & "  public.sv_mstr.sv_sq_ref_oid, " _
                                & "  ac_mstr_ar.ac_code, " _
                                & "  ac_mstr_ar.ac_name, " _
                                & "  sb_mstr_ar.sb_desc, " _
                                & "  cc_mstr_ar.cc_desc, " _
                                & "  public.sv_mstr.sv_need_date, " _
                                & "  public.sv_mstr.sv_payment_date, " _
                                & "  public.sv_mstr.sv_last_transaction, " _
                                & "  cu_name, " _
                                & "  pay_type.code_name as pay_type_name, " _
                                & "  pay_method.code_name as pay_method_name, " _
                                & "  (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax, " _
                                & "  sv_exc_rate * sv_total as sv_total_ext,  sv_exc_rate * sv_total_ppn as sv_total_ppn_ext, " _
                                & "  sv_exc_rate * sv_total_pph as sv_total_pph_ext,  sv_exc_rate * (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax_ext, " _
                                & "  tran_name ,wf_seq " _
                                & "FROM " _
                                & "  public.sv_mstr" _
                                & "  inner join en_mstr on en_id = sv_en_id " _
                                & "  inner join si_mstr on si_id = sv_si_id " _
                                & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sv_ptnr_id_sold " _
                                & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sv_ptnr_id_bill " _
                                & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sv_sales_person " _
                                & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sv_ar_ac_id " _
                                & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sv_ar_sb_id " _
                                & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sv_ar_cc_id " _
                                & "  inner join cu_mstr on cu_id = sv_cu_id " _
                                & "  inner join tran_mstr on tran_id = sv_tran_id" _
                                & "  inner join code_mstr pay_type on pay_type.code_id = sv_pay_type " _
                                & "  inner join code_mstr pay_method on pay_method.code_id = sv_pay_method " _
                                & "  inner join wf_mstr on wf_ref_oid=sv_oid " _
                                & "  where sv_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & "  and wf_user_id in (" + _user_approval + ")" _
                                & "  and wf_iscurrent ~~* 'Y'"
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  sv_oid, " _
                                & "  sv_dom_id, " _
                                & "  sv_en_id, " _
                                & "  sv_add_by, " _
                                & "  sv_add_date, " _
                                & "  sv_upd_by, " _
                                & "  sv_upd_date, " _
                                & "  sv_code, " _
                                & "  sv_ptnr_id_sold, " _
                                & "  sv_ptnr_id_bill, " _
                                & "  sv_date, " _
                                & "  sv_credit_term, " _
                                & "  sv_taxable, " _
                                & "  sv_tax_class, " _
                                & "  sv_si_id, " _
                                & "  sv_type, " _
                                & "  sv_sales_person, " _
                                & "  sv_pi_id, " _
                                & "  sv_pay_type, " _
                                & "  sv_pay_method, " _
                                & "  sv_ar_ac_id, " _
                                & "  sv_ar_sb_id, " _
                                & "  sv_ar_cc_id, " _
                                & "  sv_dp, " _
                                & "  sv_disc_header, " _
                                & "  sv_total, " _
                                & "  sv_print_count, " _
                                & "  sv_payment_date, " _
                                & "  sv_cons, " _
                                & "  sv_close_date, " _
                                & "  sv_tran_id, " _
                                & "  sv_trans_id, " _
                                & "  sv_trans_rmks, " _
                                & "  sv_current_route, " _
                                & "  sv_next_route, " _
                                & "  sv_dt, " _
                                & "  sv_cu_id, " _
                                & "  sv_total_ppn, " _
                                & "  sv_total_pph, " _
                                & "  sv_payment, " _
                                & "  sv_exc_rate, " _
                                & "  sv_tax_inc, " _
                                & "  (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax, " _
                                & "  sv_exc_rate * sv_total as sv_total_ext,  sv_exc_rate * sv_total_ppn as sv_total_ppn_ext, " _
                                & "  sv_exc_rate * sv_total_pph as sv_total_pph_ext,  sv_exc_rate * (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax_ext, " _
                                & "  en_desc, " _
                                & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                                & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                                & "  credit_term.code_name as credit_term_name, " _
                                & "  tax_class.code_name as tax_class_name, " _
                                & "  si_desc, " _
                                & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                                & "  pi_desc, " _
                                & "  pay_type.code_name as pay_type_name, " _
                                & "  pay_method.code_name as pay_method_name, " _
                                & "  ac_mstr_ar.ac_code, " _
                                & "  ac_mstr_ar.ac_name, " _
                                & "  sb_mstr_ar.sb_desc, " _
                                & "  cc_mstr_ar.cc_desc, " _
                                & "  cu_name, " _
                                & "  tran_name " _
                                & "FROM  " _
                                & "  public.sv_mstr " _
                                & "  inner join en_mstr on en_id = sv_en_id " _
                                & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sv_ptnr_id_sold " _
                                & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sv_ptnr_id_bill " _
                                & "  inner join code_mstr credit_term on credit_term.code_id = sv_credit_term " _
                                & "  inner join code_mstr tax_class on tax_class.code_id = sv_tax_class " _
                                & "  inner join si_mstr on si_id = sv_si_id " _
                                & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sv_sales_person " _
                                & "  inner join pi_mstr on pi_id = sv_pi_id " _
                                & "  inner join code_mstr pay_type on pay_type.code_id = sv_pay_type " _
                                & "  inner join code_mstr pay_method on pay_method.code_id = sv_pay_method " _
                                & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sv_ar_ac_id " _
                                & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sv_ar_sb_id " _
                                & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sv_ar_cc_id " _
                                & "  inner join cu_mstr on cu_id = sv_cu_id " _
                                & "  inner join tran_mstr on tran_id = sv_tran_id" _
                                & " inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                                & " where sv_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and sv_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and sv_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and sv_trans_id <> 'D' "
                            .InitializeCommand()
                            .FillDataSet(ds, "all")
                            gc_all.DataSource = ds.Tables("all")
                        End If

                        bestfit_column()
                        load_data_grid_detail()
                        relation_detail()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub load_data_grid_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            If ds.Tables("all").Rows.Count = 0 Then
                Exit Sub
            End If
        End If

        ds_detail = New DataSet
        Try
            Using objload As New master_new.CustomCommand
                With objload
                    If xtc_master.SelectedTabPageIndex = 0 Then
                        .SQL = "SELECT  " _
                        & "  svd_oid, " _
                        & "  svd_dom_id, " _
                        & "  svd_en_id, " _
                        & "  svd_add_by, " _
                        & "  svd_add_date, " _
                        & "  svd_upd_by, " _
                        & "  svd_upd_date, " _
                        & "  svd_sv_oid, " _
                        & "  svd_seq, " _
                        & "  svd_is_additional_charge, " _
                        & "  svd_si_id, " _
                        & "  svd_pt_id, " _
                        & "  svd_rmks, " _
                        & "  svd_qty, " _
                        & "  svd_qty_allocated, " _
                        & "  svd_qty_picked, " _
                        & "  svd_qty_shipment, " _
                        & "  svd_qty_pending_inv, " _
                        & "  svd_qty_invoice, " _
                        & "  svd_um, " _
                        & "  svd_cost, " _
                        & "  svd_price, " _
                        & "  svd_disc, " _
                        & "  svd_sales_ac_id, " _
                        & "  svd_sales_sb_id, " _
                        & "  svd_sales_cc_id, " _
                        & "  svd_disc_ac_id, " _
                        & "  svd_um_conv, " _
                        & "  svd_qty_real, " _
                        & "  svd_taxable, " _
                        & "  svd_tax_inc, " _
                        & "  svd_tax_class, " _
                        & "  svd_status, " _
                        & "  svd_dt, " _
                        & "  svd_payment, " _
                        & "  svd_dp, " _
                        & "  svd_sales_unit, " _
                        & "  svd_loc_id, " _
                        & "  svd_serial, " _
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
                        & "  tax_class.code_name as svd_tax_class_name, " _
                        & "  loc_desc " _
                        & "FROM  " _
                        & "  public.svd_det " _
                        & "  inner join sv_mstr on sv_oid = svd_sv_oid " _
                        & "  inner join en_mstr on en_id = svd_en_id " _
                        & "  inner join si_mstr on si_id = svd_si_id " _
                        & "  inner join pt_mstr on pt_id = svd_pt_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                        & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                        & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                        & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = svd_sales_cc_id " _
                        & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                        & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                        & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                        & "  inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                        & "  where sv_en_id in (select user_en_id from tconfuserentity " _
                                             & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        & "  and wf_user_id in (" + _user_approval + ")" _
                        & "  and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join sv_mstr on sv_code = wf_ref_code " + _
                              " inner join svd_det dt on dt.svd_sv_oid = sv_oid " _
                            & " where wf_ref_code in (select sv_code from sv_mstr inner join wf_mstr on wf_ref_code = sv_code " _
                                                  & " where sv_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                        & "  sqa_oid, " _
                        & "  sqa_bekerja_pada, " _
                        & "  sqa_jabatan_bagian, " _
                        & "  sqa_kantor_alamat_1, " _
                        & "  sqa_kantor_alamat_2, " _
                        & "  sqa_kantor_lantai, " _
                        & "  sqa_kantor_telp, " _
                        & "  sqa_ktp, " _
                        & "  sqa_email, " _
                        & "  sqa_rumah_alamat_1, " _
                        & "  sqa_rumah_alamat_2, " _
                        & "  sqa_rumah_kode_pos, " _
                        & "  sqa_rumah_telp, " _
                        & "  sqa_rumah_hp, " _
                        & "  sqa_status_alamat_kirim, " _
                        & "  sqa_status_alamat_tagih, " _
                        & "  sqa_suami_nama, " _
                        & "  sqa_suami_bekerja, " _
                        & "  sqa_suami_jabatan, " _
                        & "  sqa_suami_kantor_alamat_1, " _
                        & "  sqa_suami_kantor_alamat_2, " _
                        & "  sqa_suami_telp, " _
                        & "  sqa_suami_hp, " _
                        & "  sqa_anak_nama_1, " _
                        & "  sqa_anak_tgl_lahir_1, " _
                        & "  sqa_anak_sekolah_1, " _
                        & "  sqa_anak_nama_2, " _
                        & "  sqa_anak_tgl_lahir_2, " _
                        & "  sqa_anak_sekolah_2, " _
                        & "  sqa_anak_nama_3, " _
                        & "  sqa_anak_tgl_lahir_3, " _
                        & "  sqa_anak_sekolah_3, " _
                        & "  sqa_keluarga_dekat_nama, " _
                        & "  sqa_keluarga_dekat_alamat_1, " _
                        & "  sqa_keluarga_dekat_alamat_2, " _
                        & "  sqa_keluarga_dekat_telp, " _
                        & "  sqa_keluarga_dekat_hp, " _
                        & "  sqa_status_tempat_tinggal, " _
                        & "  sqa_jenis_kartu_kredit, " _
                        & "  sqa_no_kartu_kredit, " _
                        & "  sqa_berlaku_sd, " _
                        & "  sqa_dt, " _
                        & "  sr.code_name as status_kepemilikan, " _
                        & "  lt.code_name as lama_tinggal, " _
                        & "  mk.code_name as masa_kerja, " _
                        & "  ic.code_name as income, " _
                        & "  kp.code_name as kepribadian, " _
                        & "  tg.code_name as tanggungan, " _
                        & "  ja.code_name as jaminan, " _
                        & "  ((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=sqa_status_rumah_id)) + " _
                        & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=sqa_lama_tinggal_id))+ " _
                        & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=sqa_masa_kerja_id))+ " _
                        & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=sqa_income_id))+ " _
                        & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=sqa_kepribadian_id))+ " _
                        & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=sqa_jaminan_id))- " _
                        & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=sqa_tanggungan_id)))as total_point, " _
                        & "  sqa_bank " _
                        & "FROM  " _
                        & "  public.sqa_attr" _
                        & "  inner join sq_mstr on sq_oid = sqa_sq_oid " _
                        & "  inner join sv_mstr on sv_sq_ref_oid = sq_mstr.sq_oid " _
                        & "  inner join code_mstr sr on sr.code_id = sqa_status_rumah_id " _
                        & "  inner join code_mstr lt on lt.code_id = sqa_lama_tinggal_id " _
                        & "  inner join code_mstr mk on mk.code_id = sqa_masa_kerja_id " _
                        & "  inner join code_mstr ic on ic.code_id = sqa_income_id " _
                        & "  inner join code_mstr kp on kp.code_id = sqa_kepribadian_id " _
                        & "  inner join code_mstr tg on tg.code_id = sqa_tanggungan_id " _
                        & "  inner join code_mstr ja on ja.code_id = sqa_jaminan_id " _
                        & "  inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                        & "  where sv_en_id in (select user_en_id from tconfuserentity " _
                                             & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        & "  and wf_user_id in (" + _user_approval + ")" _
                        & "  and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "customer")
                        gc_detail_attr.DataSource = ds_detail.Tables("customer")
                        gv_detail_attr.BestFitColumns()

                        ' ''.SQL = "SELECT  " _
                        ' ''         & "  sokp_oid, " _
                        ' ''         & "  sokp_sv_oid, " _
                        ' ''         & "  sokp_seq, " _
                        ' ''         & "  sokp_ref, " _
                        ' ''         & "  sokp_amount, " _
                        ' ''         & "  sokp_amount_pay, " _
                        ' ''         & "  sokp_description " _
                        ' ''         & "FROM  " _
                        ' ''         & "  public.sokp_piutang " _
                        ' ''         & "  inner join public.sv_mstr on sv_oid = sokp_sv_oid " _
                        ' ''         & "  inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                        ' ''         & "  where sv_en_id in (select user_en_id from tconfuserentity " _
                        ' ''                             & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        ' ''         & "  and wf_user_id in (" + _user_approval + ")" _
                        ' ''         & "  and wf_iscurrent ~~* 'Y'"

                        ' ''.InitializeCommand()
                        ' ''.FillDataSet(ds_detail, "kartu_piutang")
                        ' ''gc_detail_kp.DataSource = ds_detail.Tables("kartu_piutang")
                        ' ''gv_detail_kp.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  sv_oid, " _
                            & "  sv_en_id, " _
                            & "  sv_add_by, " _
                            & "  sv_add_date, " _
                            & "  sv_upd_by, " _
                            & "  sv_upd_date, " _
                            & "  sv_code, " _
                            & "  sv_date, " _
                            & "  sv_dp, " _
                            & "  sv_disc_header, " _
                            & "  sv_total, " _
                            & "  sv_payment_date, " _
                            & "  sv_cons, " _
                            & "  sv_close_date, " _
                            & "  sv_tran_id, " _
                            & "  sv_trans_id, " _
                            & "  sv_trans_rmks, " _
                            & "  sv_cu_id, " _
                            & "  sv_total_ppn, " _
                            & "  sv_total_pph, " _
                            & "  sv_payment, " _
                            & "  sv_exc_rate, " _
                            & "  sv_tax_inc, " _
                            & "  (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax, " _
                            & "  sv_exc_rate * sv_total as sv_total_ext,  sv_exc_rate * sv_total_ppn as sv_total_ppn_ext, " _
                            & "  sv_exc_rate * sv_total_pph as sv_total_pph_ext,  sv_exc_rate * (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax_ext, " _
                            & "  en_desc, " _
                            & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                            & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                            & "  credit_term.code_name as credit_term_name, " _
                            & "  tax_class.code_name as tax_class_name, " _
                            & "  si_desc, " _
                            & "  pi_desc, " _
                            & "  pay_type.code_name as pay_type_name, " _
                            & "  pay_method.code_name as pay_method_name, " _
                            & "  cc_mstr_ar.cc_desc, " _
                            & "  cu_name, " _
                            & "  tran_name, " _
                            & "  svd_oid, " _
                            & "  svd_sv_oid, " _
                            & "  svd_pt_id, " _
                            & "  svd_rmks, " _
                            & "  svd_qty, " _
                            & "  svd_um, " _
                            & "  svd_cost, " _
                            & "  svd_price, " _
                            & "  svd_disc, " _
                            & "  svd_sales_ac_id, " _
                            & "  svd_sales_sb_id, " _
                            & "  svd_sales_cc_id, " _
                            & "  svd_disc_ac_id, " _
                            & "  svd_um_conv, " _
                            & "  svd_qty_real, " _
                            & "  svd_taxable, " _
                            & "  svd_tax_inc, " _
                            & "  svd_tax_class, " _
                            & "  svd_status, " _
                            & "  svd_dt, " _
                            & "  svd_payment, " _
                            & "  svd_dp, " _
                            & "  svd_sales_unit, " _
                            & "  svd_loc_id, " _
                            & "  svd_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_mstr_ar.sb_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as svd_tax_class_name, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sv_mstr " _
                            & "  inner join en_mstr on en_id = sv_en_id " _
                            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sv_ptnr_id_sold " _
                            & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sv_ptnr_id_bill " _
                            & "  inner join code_mstr credit_term on credit_term.code_id = sv_credit_term " _
                            & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sv_sales_person " _
                            & "  inner join pi_mstr on pi_id = sv_pi_id " _
                            & "  inner join code_mstr pay_type on pay_type.code_id = sv_pay_type " _
                            & "  inner join code_mstr pay_method on pay_method.code_id = sv_pay_method " _
                            & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sv_ar_sb_id " _
                            & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sv_ar_cc_id " _
                            & "  inner join cu_mstr on cu_id = sv_cu_id " _
                            & "  inner join tran_mstr on tran_id = sv_tran_id" _
                            & "  inner join svd_det on sv_oid = svd_sv_oid " _
                            & "  inner join si_mstr on si_id = svd_si_id " _
                            & "  inner join pt_mstr on pt_id = svd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                            & " and sv_en_id in (select user_en_id from tconfuserentity " _
                                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and sv_ar_cc_id in (select ccr_cc_id from ccr_restrc " _
                                               & " where ccr_user_id = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                            & " where sv_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()

                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        .SQL = "SELECT  " _
                            & "  svd_oid, " _
                            & "  svd_dom_id, " _
                            & "  svd_en_id, " _
                            & "  svd_add_by, " _
                            & "  svd_add_date, " _
                            & "  svd_upd_by, " _
                            & "  svd_upd_date, " _
                            & "  svd_sv_oid, " _
                            & "  svd_seq, " _
                            & "  svd_is_additional_charge, " _
                            & "  svd_si_id, " _
                            & "  svd_pt_id, " _
                            & "  svd_rmks, " _
                            & "  svd_qty, " _
                            & "  svd_qty_allocated, " _
                            & "  svd_qty_picked, " _
                            & "  svd_qty_shipment, " _
                            & "  svd_qty_pending_inv, " _
                            & "  svd_qty_invoice, " _
                            & "  svd_um, " _
                            & "  svd_cost, " _
                            & "  svd_price, " _
                            & "  svd_disc, " _
                            & "  svd_sales_ac_id, " _
                            & "  svd_sales_sb_id, " _
                            & "  svd_sales_cc_id, " _
                            & "  svd_disc_ac_id, " _
                            & "  svd_um_conv, " _
                            & "  svd_qty_real, " _
                            & "  svd_taxable, " _
                            & "  svd_tax_inc, " _
                            & "  svd_tax_class, " _
                            & "  svd_status, " _
                            & "  svd_dt, " _
                            & "  svd_payment, " _
                            & "  svd_dp, " _
                            & "  svd_sales_unit, " _
                            & "  svd_loc_id, " _
                            & "  svd_serial, " _
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
                            & "  tax_class.code_name as svd_tax_class_name, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.svd_det " _
                            & "  inner join sv_mstr on sv_oid = svd_sv_oid " _
                            & "  inner join en_mstr on en_id = svd_en_id " _
                            & "  inner join si_mstr on si_id = svd_si_id " _
                            & "  inner join pt_mstr on pt_id = svd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = svd_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                            & "  inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                            & "  where sv_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and sv_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and sv_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join sv_mstr on sv_code = wf_ref_code " + _
                              " inner join svd_det dt on dt.svd_sv_oid = sv_oid " _
                            & " where wf_ref_code in (select sv_code from sv_mstr inner join wf_mstr on wf_ref_code = sv_code " _
                                                  & " where sv_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and sv_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and sv_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  sqa_oid, " _
                            & "  sqa_bekerja_pada, " _
                            & "  sqa_jabatan_bagian, " _
                            & "  sqa_kantor_alamat_1, " _
                            & "  sqa_kantor_alamat_2, " _
                            & "  sqa_kantor_lantai, " _
                            & "  sqa_kantor_telp, " _
                            & "  sqa_ktp, " _
                            & "  sqa_email, " _
                            & "  sqa_rumah_alamat_1, " _
                            & "  sqa_rumah_alamat_2, " _
                            & "  sqa_rumah_kode_pos, " _
                            & "  sqa_rumah_telp, " _
                            & "  sqa_rumah_hp, " _
                            & "  sqa_status_alamat_kirim, " _
                            & "  sqa_status_alamat_tagih, " _
                            & "  sqa_suami_nama, " _
                            & "  sqa_suami_bekerja, " _
                            & "  sqa_suami_jabatan, " _
                            & "  sqa_suami_kantor_alamat_1, " _
                            & "  sqa_suami_kantor_alamat_2, " _
                            & "  sqa_suami_telp, " _
                            & "  sqa_suami_hp, " _
                            & "  sqa_anak_nama_1, " _
                            & "  sqa_anak_tgl_lahir_1, " _
                            & "  sqa_anak_sekolah_1, " _
                            & "  sqa_anak_nama_2, " _
                            & "  sqa_anak_tgl_lahir_2, " _
                            & "  sqa_anak_sekolah_2, " _
                            & "  sqa_anak_nama_3, " _
                            & "  sqa_anak_tgl_lahir_3, " _
                            & "  sqa_anak_sekolah_3, " _
                            & "  sqa_keluarga_dekat_nama, " _
                            & "  sqa_keluarga_dekat_alamat_1, " _
                            & "  sqa_keluarga_dekat_alamat_2, " _
                            & "  sqa_keluarga_dekat_telp, " _
                            & "  sqa_keluarga_dekat_hp, " _
                            & "  sqa_status_tempat_tinggal, " _
                            & "  sqa_jenis_kartu_kredit, " _
                            & "  sqa_no_kartu_kredit, " _
                            & "  sqa_berlaku_sd, " _
                            & "  sqa_dt, " _
                            & "  sqa_bank " _
                            & "FROM  " _
                            & "  public.sqa_attr" _
                            & "  inner join sv_mstr on sv_oid = sqa_sv_oid " _
                            & "  inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                            & "  where sv_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and sv_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and sv_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_detail, "customer")
                        gc_detail_attr.DataSource = ds_detail.Tables("customer")
                        gv_detail_attr.BestFitColumns()



                        '.SQL = "SELECT  " _
                        '     & "  sokp_oid, " _
                        '     & "  sokp_sv_oid, " _
                        '     & "  sokp_seq, " _
                        '     & "  sokp_ref, " _
                        '     & "  sokp_amount, " _
                        '     & "  sokp_amount_pay, " _
                        '     & "  sokp_description " _
                        '     & "FROM  " _
                        '     & "  public.sokp_piutang " _
                        '     & "  inner join public.sv_mstr on sv_oid = sokp_sv_oid " _
                        '    & "  inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                        '    & "  where sv_en_id in (select user_en_id from tconfuserentity " _
                        '                         & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        '    & "  and sv_date >= " + SetDate(pr_txttglawal.DateTime) _
                        '    & "  and sv_date <= " + SetDate(pr_txttglakhir.DateTime) _
                        '    & "  and wf_user_id in (" + _user_approval + ")"

                        '.InitializeCommand()
                        '.FillDataSet(ds_detail, "kartu_piutang")
                        'gc_detail_kp.DataSource = ds_detail.Tables("kartu_piutang")
                        'gv_detail_kp.BestFitColumns()



                        .SQL = "SELECT  " _
                            & "  sv_oid, " _
                            & "  sv_en_id, " _
                            & "  sv_add_by, " _
                            & "  sv_add_date, " _
                            & "  sv_upd_by, " _
                            & "  sv_upd_date, " _
                            & "  sv_code, " _
                            & "  sv_date, " _
                            & "  sv_dp, " _
                            & "  sv_disc_header, " _
                            & "  sv_total, " _
                            & "  sv_payment_date, " _
                            & "  sv_cons, " _
                            & "  sv_close_date, " _
                            & "  sv_tran_id, " _
                            & "  sv_trans_id, " _
                            & "  sv_trans_rmks, " _
                            & "  sv_cu_id, " _
                            & "  sv_total_ppn, " _
                            & "  sv_total_pph, " _
                            & "  sv_payment, " _
                            & "  sv_exc_rate, " _
                            & "  sv_tax_inc, " _
                            & "  (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax, " _
                            & "  sv_exc_rate * sv_total as sv_total_ext,  sv_exc_rate * sv_total_ppn as sv_total_ppn_ext, " _
                            & "  sv_exc_rate * sv_total_pph as sv_total_pph_ext,  sv_exc_rate * (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax_ext, " _
                            & "  en_desc, " _
                            & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                            & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                            & "  credit_term.code_name as credit_term_name, " _
                            & "  tax_class.code_name as tax_class_name, " _
                            & "  si_desc, " _
                            & "  pi_desc, " _
                            & "  pay_type.code_name as pay_type_name, " _
                            & "  pay_method.code_name as pay_method_name, " _
                            & "  cc_mstr_ar.cc_desc, " _
                            & "  cu_name, " _
                            & "  tran_name, " _
                            & "  svd_oid, " _
                            & "  svd_sv_oid, " _
                            & "  svd_pt_id, " _
                            & "  svd_rmks, " _
                            & "  svd_qty, " _
                            & "  svd_um, " _
                            & "  svd_cost, " _
                            & "  svd_price, " _
                            & "  svd_disc, " _
                            & "  svd_sales_ac_id, " _
                            & "  svd_sales_sb_id, " _
                            & "  svd_sales_cc_id, " _
                            & "  svd_disc_ac_id, " _
                            & "  svd_um_conv, " _
                            & "  svd_qty_real, " _
                            & "  svd_taxable, " _
                            & "  svd_tax_inc, " _
                            & "  svd_tax_class, " _
                            & "  svd_status, " _
                            & "  svd_dt, " _
                            & "  svd_payment, " _
                            & "  svd_dp, " _
                            & "  svd_sales_unit, " _
                            & "  svd_loc_id, " _
                            & "  svd_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_mstr_ar.sb_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as svd_tax_class_name, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sv_mstr " _
                            & "  inner join en_mstr on en_id = sv_en_id " _
                            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sv_ptnr_id_sold " _
                            & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sv_ptnr_id_bill " _
                            & "  inner join code_mstr credit_term on credit_term.code_id = sv_credit_term " _
                            & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sv_sales_person " _
                            & "  inner join pi_mstr on pi_id = sv_pi_id " _
                            & "  inner join code_mstr pay_type on pay_type.code_id = sv_pay_type " _
                            & "  inner join code_mstr pay_method on pay_method.code_id = sv_pay_method " _
                            & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sv_ar_sb_id " _
                            & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sv_ar_cc_id " _
                            & "  inner join cu_mstr on cu_id = sv_cu_id " _
                            & "  inner join tran_mstr on tran_id = sv_tran_id" _
                            & "  inner join svd_det on sv_oid = svd_sv_oid " _
                            & "  inner join si_mstr on si_id = svd_si_id " _
                            & "  inner join pt_mstr on pt_id = svd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                            & " inner join wf_mstr wm on wf_ref_oid = sv_oid " _
                            & " where sv_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and sv_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and sv_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub relation_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            Try
                gv_detail.Columns("svd_sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("svd_sv_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("sv_oid").ToString & "'")
                gv_detail.BestFitColumns()

                ' ''gv_detail_attr.Columns("sqa_sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sqa_sv_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("sv_oid").ToString & "'")
                ' ''gv_detail.BestFitColumns()

                ' ''gv_detail_kp.Columns("sokp_sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sokp_sv_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("sv_oid").ToString & "'")
                ' ''gv_detail_kp.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("sv_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sv_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("sv_oid").ToString & "'")
                gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail.Columns("svd_sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("svd_sv_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("sv_oid").ToString & "'")
                gv_detail.BestFitColumns()

                ' ''gv_detail_attr.Columns("sqa_sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sqa_sv_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("sv_oid").ToString & "'")
                ' ''gv_detail.BestFitColumns()

                ' ''gv_detail_kp.Columns("sokp_sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sokp_sv_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("sv_oid").ToString & "'")
                ' ''gv_detail_kp.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("sv_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sv_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("sv_oid").ToString & "'")
                gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub xtc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xtc_master.Click
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        End If
    End Sub

    Private Sub sb_process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_process.Click
        Dim i, j As Integer
        j = 0

        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables.Count = 0 Then
                Exit Sub
            ElseIf ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If

            For i = 0 To ds.Tables("os").Rows.Count - 1
                If ds.Tables("os").Rows(i).Item("status") = True Then
                    j += 1
                    If le_wfs_status.EditValue = 1 And xtc_master.SelectedTabPageIndex = 0 Then
                        Try
                            Using objinsert As New master_new.CustomCommand
                                With objinsert
.Command.Open()
                                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                                    Try
                                        '.Command = .Connection.CreateCommand
                                        '.Command.Transaction = sqlTran
                                        '.Command.CommandType = CommandType.Text

                                        ds.Tables("os").AcceptChanges()

                                        Dim _max_seq As Integer
                                        _max_seq = mf.get_max_seq(ds.Tables("os").Rows(i).Item("sv_code"))

                                        If _max_seq = ds.Tables("os").Rows(i).Item("wf_seq") Then
                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update sq_mstr set sq_trans_id = 'I'" + _
                                                           " where sq_oid = '" + ds.Tables("os").Rows(i).Item("sv_sq_ref_oid") + "'"
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()

                                        End If

                                        .Command.Commit()
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
                End If

            Next

            If j = 0 Then
                MessageBox.Show("Please Select Data, First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        Dim _colom, _table, _initial, _type, _title As String


        _colom = "sv_trans_id"
        _table = "sv_mstr"
        _initial = "sv"
        _type = "sv"
        _title = "Sales Verification"

        If le_wfs_status.EditValue = 1 And xtc_master.SelectedTabPageIndex = 0 Then
            approve_wf(_colom, _table, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, gv_email, _title)
        ElseIf le_wfs_status.EditValue = 2 And xtc_master.SelectedTabPageIndex = 0 Then
            hold_wf(_initial, le_wfs_status.EditValue, Trim(te_remark.Text), nud_hold_day.Value, _user_approval, ds)
        ElseIf le_wfs_status.EditValue = 3 And xtc_master.SelectedTabPageIndex = 0 Then
            cancel_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        ElseIf le_wfs_status.EditValue = 4 And xtc_master.SelectedTabPageIndex = 0 Then
            rollback_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        End If


    End Sub

    Private Sub le_wfs_status_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles le_wfs_status.EditValueChanged
        If le_wfs_status.EditValue = 2 Then
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub


End Class
