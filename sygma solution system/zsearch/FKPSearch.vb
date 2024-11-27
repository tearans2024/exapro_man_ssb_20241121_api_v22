Imports master_new.ModFunction

Public Class FKPSearch
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public _obj As Object
    Public func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FKPSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "DRCR Memo Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "DRCR Date", "ar_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Ref", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Angsuran Ke-", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Amount", "sokp_amount_open", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Total", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FARPayment" Or fobject.name = "FARPaymentDetail" Then
            get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date, " _
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
                & "  public.cc_mstr.cc_desc, ar_exc_rate, " _
                & "  sokp_oid, " _
                & "  arso_ar_oid, " _
                & "  arso_so_code, " _
                & "  sokp_oid, " _
                & "  sokp_so_oid, " _
                & "  sokp_seq, " _
                & "  sokp_amount, " _
                & "  sokp_due_date, " _
                & "  sokp_amount_pay, " _
                & "  sokp_amount - sokp_amount_pay as sokp_amount_open, " _
                & "  sokp_description " _
                & "FROM " _
                & "  public.ar_mstr " _
                 & "  inner join public.arso_so on ar_mstr.ar_oid = arso_ar_oid " _
                & "  inner join public.sokp_piutang on arso_so_oid = sokp_so_oid" _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & " where coalesce(ar_status,'') = '' " _
                & " and ar_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id = " + _en_id.ToString _
                & " and ar_bill_to = " + _ptnr_id.ToString _
                & " and coalesce(sokp_status,'') = '' "

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
        If fobject.name = "FARPayment" Or fobject.name = "FARPaymentDetail" Then
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_ref", ds.Tables(0).Rows(_row_gv).Item("ar_code"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ar_oid", ds.Tables(0).Rows(_row_gv).Item("ar_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "sokp_oid", ds.Tables(0).Rows(_row_gv).Item("sokp_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ar_ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("arso_so_code"))
            fobject.gv_edit.SetRowCellValue(_row, "sokp_seq", ds.Tables(0).Rows(_row_gv).Item("sokp_seq"))
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
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id And _
               _cu_id <> master_new.ClsVar.ibase_cur_id Then
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            ElseIf ds.Tables(0).Rows(_row_gv).Item("ar_cu_id") <> master_new.ClsVar.ibase_cur_id Then
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding") * _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open") * _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open") * _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", _exr_cu_rate_1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            Else
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                'fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_cash_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open"))
                fobject.gv_edit.SetRowCellValue(_row, "arpayd_exc_rate", 1)
                fobject.gv_edit.SetRowCellValue(_row, "ar_exc_rate", ds.Tables(0).Rows(_row_gv).Item("ar_exc_rate"))
            End If

            fobject.gv_edit.SetRowCellValue(_row, "ar_cu_id", ds.Tables(0).Rows(_row_gv).Item("ar_cu_id"))
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_disc_amount", 0)
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_exp_amount", 0)
            fobject.gv_edit.SetRowCellValue(_row, "arpayd_cur_amount", ds.Tables(0).Rows(_row_gv).Item("sokp_amount_open"))

            'fobject.gv_edit.SetRowCellValue(_row, "arpayd_cur_amount", ds.Tables(0).Rows(_row_gv).Item("ar_outstanding"))
            fobject.gv_edit.BestFitColumns()

        End If
    End Sub
End Class
