Imports master_new.ModFunction

Public Class FARPSearch
    Public _row As Integer
    Public _en_id, _ptnr_id, _cu_id As Integer
    Public _obj As Object
    Public func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARPSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "ARP Number", "arp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "ARP Date", "arp_date", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FDPackingSheetPrintOut" Then
            get_sequel = "SELECT DISTINCT " _
                & "  public.arp_print.arp_oid, " _
                & "  public.arp_print.arp_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.arp_print.arp_code, " _
                & "  public.arp_print.arp_date, " _
                & "  public.arpd_det.arpd_arp_oid, " _
                & "  public.arpd_det.arpd_ar_oid, " _
                & "  public.arpd_det.arpd_ar_code " _
                & "   " _
                & "FROM " _
                & "  public.arp_print " _
                & "  INNER JOIN public.arpd_det ON (public.arp_print.arp_oid = public.arpd_det.arpd_arp_oid) " _
                & "  INNER JOIN public.en_mstr ON (public.arp_print.arp_en_id = public.en_mstr.en_id)" _
                & " where arp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and arp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and arp_en_id = " + _en_id.ToString
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
        If fobject.name = "FDPackingSheetPrintOut" Then
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
        End If
    End Sub
End Class
