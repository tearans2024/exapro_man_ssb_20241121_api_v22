Imports master_new.ModFunction

Public Class FDRCRMemoSearchHariff
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public _obj As Object
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FDRCRMemoSearchHariff_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_invoice")

        form_first_load()
        pr_txttglawal.Focus()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "DRCR Memo Number", "arinv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Total", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FInvoicePayment" Then
            get_sequel = "SELECT  " _
                & "  public.arinv_mstr.arinv_oid, " _
                & "  public.arinv_mstr.arinv_en_id, " _
                & "  public.arinv_mstr.arinv_code, " _
                & "  public.arinv_mstr.arinv_date, " _
                & "  public.arinv_mstr.arinv_ptnr_id, " _
                & "  public.arinv_mstr.arinv_cu_id, " _
                & "  public.arinv_mstr.arinv_total, " _
                & "  public.arinv_mstr.arinv_pay_amount, " _
                & "  public.arinv_mstr.arinv_total - coalesce(arinv_pay_amount,0) as arinv_outstanding, " _
                & "  public.arinv_mstr.arinv_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  coalesce(public.arinv_mstr.arinv_exc_rate_reval,public.arinv_mstr.arinv_exc_rate) as arinv_exc_rate, " _
                & "  public.arinv_mstr.arinv_ac_ar_id, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.arinv_mstr.arinv_sb_ar_id, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.arinv_mstr.arinv_cc_ar_id, " _
                & "  public.cc_mstr.cc_desc " _
                & "FROM " _
                & "  public.arinv_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.arinv_mstr.arinv_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.arinv_mstr.arinv_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.arinv_mstr.arinv_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.arinv_mstr.arinv_ac_ar_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.arinv_mstr.arinv_sb_ar_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.arinv_mstr.arinv_cc_ar_id = public.cc_mstr.cc_id) " _
                & " where arinv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and arinv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and arinv_en_id = " + _en_id.ToString _
                & " and arinv_ptnr_id = " + _ptnr_id.ToString _
                & " and coalesce(arinv_receive_date,'1/1/2001') <> '1/1/2001'"

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and arinv_trans_id ~~* 'I'  "
            End If

            If _cu_id <> master_new.ClsVar.ibase_cur_id Then
                get_sequel = get_sequel + " and arinv_cu_id = " + _cu_id.ToString
                'get_sequel = get_sequel + " and ap_cu_id <> " + master_new.ClsVar.ibase_cur_id.ToString
            End If
        End If
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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
        _exr_cu_rate_1 = func_data.get_exchange_rate(ds.Tables(0).Rows(_row_gv).Item("arinv_cu_id").ToString)

        Dim ds_bantu As New DataSet
        If fobject.name = "FInvoicePayment" Then
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_ref", ds.Tables(0).Rows(_row_gv).Item("arinv_code"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_oid", ds.Tables(0).Rows(_row_gv).Item("arinv_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ac_id", ds.Tables(0).Rows(_row_gv).Item("arinv_ac_ar_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_sb_id", ds.Tables(0).Rows(_row_gv).Item("arinv_sb_ar_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds.Tables(0).Rows(_row_gv).Item("sb_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_cc_id", ds.Tables(0).Rows(_row_gv).Item("arinv_cc_ar_id"))
            fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds.Tables(0).Rows(_row_gv).Item("cc_desc"))

            If ds.Tables(0).Rows(_row_gv).Item("arinv_cu_id") = _cu_id Then
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arinv_exc_rate", ds.Tables(0).Rows(_row_gv).Item("arinv_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("arinv_cu_id") <> master_new.ClsVar.ibase_cur_id And _
               _cu_id <> master_new.ClsVar.ibase_cur_id Then
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arinv_exc_rate", ds.Tables(0).Rows(_row_gv).Item("arinv_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("arinv_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding") * _exr_cu_rate_1)
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding") * _exr_cu_rate_1)
                'tidak usah dikalikan kurs 20101127 by sys
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_cur_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arinv_exc_rate", ds.Tables(0).Rows(_row_gv).Item("arinv_exc_rate"))
            Else
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", 1)
                fobject.gv_edit.SetRowCellValue(_row, "arinv_exc_rate", ds.Tables(0).Rows(_row_gv).Item("arinv_exc_rate"))
            End If

            fobject.gv_edit.SetRowCellValue(_row, "arinv_cu_id", ds.Tables(0).Rows(_row_gv).Item("arinv_cu_id"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_disc_amount", 0)
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_other_amount", 0)
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_cur_amount", ds.Tables(0).Rows(_row_gv).Item("arinv_outstanding"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FDRCRMemoPrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ar_code")
        End If
    End Sub
End Class
