Imports master_new.ModFunction

Public Class FDRCRMemoSearch
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public _obj As Object
    Public func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FDRCRMemoSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_dmergeesc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Shipment Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        If fobject.name = FARMerge.Name Then
            add_column(gv_master, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        End If
        add_column(gv_master, "Shipment Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Total", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column(gv_master, "Shipping Charges", "ar_shipping_charges", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FARPayment" Then
            get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date,ar_eff_date, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount,coalesce(ar_shipping_charges,0) as ar_shipping_charges, " _
                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                & "  public.ar_mstr.ar_status, " _
                & "  public.ar_mstr.ar_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.cc_mstr.cc_desc, ar_exc_rate " _
                & "FROM " _
                & "  public.ar_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & " where coalesce(ar_status,'') = '' " _
                & " and ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id = " + _en_id.ToString _
                & " and ar_bill_to = " + _ptnr_id.ToString

        ElseIf fobject.name = "FDRCRMemoPrint" Then
            get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date,ar_eff_date, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                & "  public.ar_mstr.ar_status, " _
                & "  public.ar_mstr.ar_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.cc_mstr.cc_desc " _
                & "FROM " _
                & "  public.ar_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & " where coalesce(ar_status,'') = '' " _
                & " and ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id = " + _en_id.ToString

        ElseIf fobject.name = "FFakturPajak" Then
            get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date,ar_eff_date, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                & "  public.ar_mstr.ar_status, " _
                & "  public.ar_mstr.ar_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.cc_mstr.cc_desc, " _
                & "  public.ptnr_mstr.ptnr_transaction_code_id, " _
                & "  tran_code_mstr.code_code as tran_code, " _
                & "  public.ar_mstr.ar_tax_inc " _
                & "FROM " _
                & "  public.ar_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & "  INNER JOIN public.code_mstr tran_code_mstr ON (public.ptnr_mstr.ptnr_transaction_code_id = tran_code_mstr.code_id) " _
                & " where ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id = " + _en_id.ToString _
                & " and ar_bill_to = " + _ptnr_id.ToString

        ElseIf fobject.name = FARMerge.Name Or fobject.name = "FInventoryReturns" Then
            get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date,ar_eff_date, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                & "  public.ar_mstr.ar_status, " _
                & "  public.ar_mstr.ar_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.cc_mstr.cc_desc, ar_exc_rate,arso_so_code " _
                & "FROM " _
                & "  public.ar_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
                & " where coalesce(ar_status,'') = '' and ar_oid not in (SELECT   b.arpd_ar_oid FROM  public.arpd_det b " _
                & "INNER JOIN public.arp_print a ON (b.arpd_arp_oid = a.arp_oid) " _
                & "WHERE  a.arp_ptnr_id = " & _ptnr_id.ToString & " AND   a.arp_en_id = " & _en_id.ToString & ") " _
                & " and ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id = " + _en_id.ToString _
                & " and ar_bill_to = " + _ptnr_id.ToString

        ElseIf fobject.name = FDBCRReScheduleSDI.Name Then
            get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date,ar_eff_date, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                & "  public.ar_mstr.ar_status, " _
                & "  public.ar_mstr.ar_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.cc_mstr.cc_desc, ar_exc_rate " _
                & "FROM " _
                & "  public.ar_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & " where coalesce(ar_status,'') = '' " _
                & " and ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id = " + _en_id.ToString _
                & " and ar_bill_to = " + _ptnr_id.ToString
        End If
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        Dim _exr_cu_rate_1 As Double = 0
        If _cu_id = master_new.ClsVar.ibase_cur_id Then
            _exr_cu_rate_1 = 1
        Else
            '_exr_cu_rate_1 = func_data.get_exchange_rate(ds.Tables(0).Rows(_row_gv).Item("ap_cu_id").ToString)
            _exr_cu_rate_1 = func_data.get_exchange_rate(_cu_id)
        End If

        Dim ds_bantu As New DataSet
        If fobject.name = "FARPayment" Then
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_ref", ds.Tables(0).Rows(_row_gv).Item("ar_code"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_oid", ds.Tables(0).Rows(_row_gv).Item("ar_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ar_ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_sb_id", ds.Tables(0).Rows(_row_gv).Item("ar_sb_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds.Tables(0).Rows(_row_gv).Item("sb_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_cc_id", ds.Tables(0).Rows(_row_gv).Item("ar_cc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds.Tables(0).Rows(_row_gv).Item("cc_desc"))

            'If ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id Then
            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
            'Else
            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            '    fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", 1)
            'End If

            If ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") = _cu_id Then
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id And _
               _cu_id <> master_new.ClsVar.ibase_cur_id Then
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            Else
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", 1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            End If

            fobject.arpay_shipping_charges.editvalue = ds.Tables(0).Rows(_row_gv).Item("ar_shipping_charges")
            fobject.gv_edit.SetRowCellValue(_row, "ar_cu_id", ds.Tables(0).Rows(_row_gv).Item("ar_cu_id"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_disc_amount", 0)
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_exp_amount", 0)
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_cur_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FDRCRMemoPrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ar_code")

        ElseIf fobject.name = FDBCRReScheduleSDI.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ar_code")

        ElseIf fobject.name = "FFakturPajak" Then
            fobject.gv_edit_ar.SetRowCellValue(_row, "fpar_oid", Guid.NewGuid.ToString)
            fobject.gv_edit_ar.SetRowCellValue(_row, "fpar_ar_oid", ds.Tables(0).Rows(_row_gv).Item("ar_oid"))
            fobject.gv_edit_ar.SetRowCellValue(_row, "ar_code", ds.Tables(0).Rows(_row_gv).Item("ar_code"))
            fobject.gv_edit_ar.BestFitColumns()

            If ds.Tables(0).Rows(_row_gv).Item("ar_tax_inc").ToString.ToUpper = "N" Then
                fobject._type_tax = "exclude"
            Else
                fobject._type_tax = "include"
            End If

        ElseIf fobject.name = FARMerge.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "arpd_ar_oid", ds.Tables(0).Rows(_row_gv).Item("ar_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "arpd_ar_code", ds.Tables(0).Rows(_row_gv).Item("ar_code"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = FInventoryReturns.Name Then
            fobject._riu_ref_ar_oid = ds.Tables(0).Rows(_row_gv).Item("ar_oid")
            fobject.riu_ref_ar_code.text = ds.Tables(0).Rows(_row_gv).Item("ar_code")

            'Try
            '    Using objcb As New master_new.CustomCommand
            '        With objcb
            '            .SQL = "SELECT " _
            '                & "  public.sod_det.sod_si_id, " _
            '                & "  public.si_mstr.si_desc, " _
            '                & "  public.ar_mstr.ar_oid, " _
            '                & "  public.ar_mstr.ar_code, " _
            '                & "  public.ar_mstr.ar_bill_to, " _
            '                & "  public.sod_det.sod_pt_id, " _
            '                & "  public.ptnr_mstr.ptnr_name, " _
            '                & "  public.pt_mstr.pt_desc1, " _
            '                & "  public.pt_mstr.pt_desc2, " _
            '                & "  public.soshipd_det.soshipd_loc_id, " _
            '                & "  public.loc_mstr.loc_desc, " _
            '                & "  public.ars_ship.ars_invoice, " _
            '                & "  public.soshipd_det.soshipd_um, " _
            '                & "  public.code_mstr.code_name, " _
            '                & "  public.sod_det.sod_um_conv, " _
            '                & "  public.invct_table.invct_cost, " _
            '                & "  public.ar_mstr.ar_ac_id, " _
            '                & "  public.ac_mstr.ac_code, " _
            '                & "  public.ac_mstr.ac_name, " _
            '                & "  public.ar_mstr.ar_sb_id, " _
            '                & "  public.ar_mstr.ar_cc_id " _
            '                & "FROM " _
            '                & "  public.ar_mstr " _
            '                & "  INNER JOIN public.ars_ship ON (public.ar_mstr.ar_oid = public.ars_ship.ars_ar_oid) " _
            '                & "  INNER JOIN public.soshipd_det ON (public.ars_ship.ars_soshipd_oid = public.soshipd_det.soshipd_oid) " _
            '                & "  INNER JOIN public.sod_det ON (public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid) " _
            '                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
            '                & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
            '                & "  INNER JOIN public.invct_table ON (public.pt_mstr.pt_id = public.invct_table.invct_pt_id) " _
            '                & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_prepaid = public.ac_mstr.ac_id) " _
            '                & "  INNER JOIN public.si_mstr ON (public.invct_table.invct_si_id = public.si_mstr.si_id) " _
            '                & "  INNER JOIN public.loc_mstr ON (public.soshipd_det.soshipd_loc_id = public.loc_mstr.loc_id) " _
            '                & "  INNER JOIN public.code_mstr ON (public.sod_det.sod_um = public.code_mstr.code_id)" _
            '                & "  where ar_oid = '" + ds.Tables(0).Rows(_row_gv).Item("ar_oid") + "'"
            '            .InitializeCommand()
            '            .FillDataSet(ds_bantu, "riud_det")
            '        End With
            '    End Using

            '    Dim _dtrow As DataRow
            '    For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            '        _dtrow = fobject.ds_edit.Tables(0).NewRow
            '        _dtrow("riud_oid") = Guid.NewGuid.ToString
            '        _dtrow("riud_riu_oid") = ds_bantu.Tables(0).Rows(i).Item("ar_oid")
            '        _dtrow("riud_pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
            '        _dtrow("riud_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
            '        _dtrow("riud_um") = ds_bantu.Tables(0).Rows(i).Item("soshipd_um")
            '        _dtrow("riud_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
            '        _dtrow("riud_qty_real") = ds_bantu.Tables(0).Rows(i).Item("ars_invoice")
            '        _dtrow("riud_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
            '        _dtrow("riud_loc_id") = ds_bantu.Tables(0).Rows(i).Item("soshipd_loc_id")
            '        _dtrow("riud_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")
            '        _dtrow("riud_ac_id") = ds_bantu.Tables(0).Rows(i).Item("ar_ac_id")
            '        _dtrow("riud_sb_id") = ds_bantu.Tables(0).Rows(i).Item("ar_sb_id")
            '        _dtrow("riud_cc_id") = ds_bantu.Tables(0).Rows(i).Item("ar_cc_id")
            '        '_dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")

            '        fobject.ds_edit.Tables(0).AcceptChanges()
            '        fobject.gv_edit.BestFitColumns()
            '    Next

            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

            'fobject.ds_edit.tables(0).clear()

        End If
    End Sub
End Class
