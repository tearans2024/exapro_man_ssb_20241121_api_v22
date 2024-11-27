Imports master_new.ModFunction

Public Class FSOShipMergeSearch
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
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        'If fobject.name = FSOShipMerge.Name Then
        '    add_column(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        'End If
        add_column(gv_master, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Total", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column(gv_master, "Shipping Charges", "ar_shipping_charges", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = FSOShipMerge.Name Then
            get_sequel = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.so_mstr.so_dom_id, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_add_by, " _
                    & "  public.so_mstr.so_add_date, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_si_id, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_invoiced, " _
                    & "  public.soship_mstr.soship_oid, " _
                    & "  public.soship_mstr.soship_code, " _
                    & "  public.soship_mstr.soship_date, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  so_si_id " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.soship_mstr ON (public.so_mstr.so_oid = public.soship_mstr.soship_so_oid)" _
                    & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid)" _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_trans_id <> 'X' and   soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_return IS NULL " _
                    & "  and so_ptnr_id_sold = " + _ptnr_id.ToString _
                    & "  and so_en_id = " + _en_id.ToString
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

        ElseIf fobject.name = FSOShipMerge.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "arpsd_soship_oid", ds.Tables(0).Rows(_row_gv).Item("soship_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "arpsd_soship_code", ds.Tables(0).Rows(_row_gv).Item("soship_code"))
            fobject.gv_edit.BestFitColumns()

            'ElseIf fobject.name = FARMergeByShipment.Name Then
            '    fobject.gv_edit.SetRowCellValue(_row, "dop_ss_oid", ds.Tables(0).Rows(_row_gv).Item("soship_oid"))
            '    fobject.gv_edit.SetRowCellValue(_row, "dop_ss_code", ds.Tables(0).Rows(_row_gv).Item("soship_code"))
            '    fobject.gv_edit.BestFitColumns()
        End If

    End Sub
End Class
