Imports master_new.ModFunction

Public Class FSalesOrderShipmentDetSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _soo_code As String
    Public _ptnr_id As Integer
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FSalesOrderShipmentDetSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'If FDPackingSheetPrintOut Then
        add_column(gv_master, "soshipd_oid", "soshipd_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_master, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Qty Open", "pcss_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_master, "Qty Shipment", "qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_master, "Qty Packing", "pcss_packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_master, "Collie Number", "pcss_collie_number", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Close Line", "pcss_close_line", DevExpress.Utils.HorzAlignment.Default)
        'End If
        'add_column_edit(gv_master, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        'add_column(gv_master, "SO Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "SO Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FSalesOrderShipmentPrint" Then
            get_sequel = "SELECT  " _
                            & "  soship_oid, " _
                            & "  soship_code, " _
                            & "  so_oid, " _
                            & "  so_code, " _
                            & "  soship_date, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.soship_mstr " _
                            & "  inner join so_mstr on so_oid = soship_so_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                            & "  where soship_is_shipment ~~* 'Y' " _
                            & "  and soship_en_id = " & _en_id.ToString _
                            & "  and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & "  order by soship_code "

        ElseIf fobject.name = FDPackingSheetPrintOut.Name Then
            get_sequel = "SELECT  " _
                            & "  public.soship_mstr.soship_oid, " _
                            & "  public.soshipd_det.soshipd_oid, " _
                            & "  public.soship_mstr.soship_code, " _
                            & "  (public.soshipd_det.soshipd_qty *-1) AS qty_shipment, " _
                            & "  public.soshipd_det.soshipd_qty_inv, " _
                            & "  public.pt_mstr.pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.ptnr_mstr.ptnr_id, " _
                            & "  public.ptnr_mstr.ptnr_name " _
                            & "FROM " _
                            & "  public.soship_mstr " _
                            & "  INNER JOIN public.soshipd_det ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
                            & "  INNER JOIN public.sod_det ON (public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid) " _
                            & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.so_mstr ON (public.soship_mstr.soship_so_oid = public.so_mstr.so_oid) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.ptnr_mstr.ptnr_id = public.so_mstr.so_ptnr_id_bill) " _
                            & "  where soship_is_shipment ~~* 'Y' " _
                            & "  and soshipd_qty_inv > '0' " _
                            & "  and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & "  and ptnr_id = " & _ptnr_id.ToString _
                            & "  and so_code in (" + _soo_code + ") " _
                            & "  order by soship_code "

        End If
        Return get_sequel
    End Function

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

        If fobject.name = "FSalesOrderShipmentPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")

        ElseIf fobject.name = FDPackingSheetPrintOut.Name Then
            If _obj.name = "gv_edit_shipment" Then
                fobject.gv_edit_shipment.SetRowCellValue(_row, "pcss_oid", Guid.NewGuid.ToString)
                'fobject.gv_edit_shipment.SetRowCellValue(_row, "ceklist", ds.Tables(0).Rows(_row_gv).Item("ceklist"))
                fobject.gv_edit_shipment.SetRowCellValue(_row, "pcss_soshipd_oid", ds.Tables(0).Rows(_row_gv).Item("soshipd_oid"))
                fobject.gv_edit_shipment.SetRowCellValue(_row, "soship_code", ds.Tables(0).Rows(_row_gv).Item("soship_code"))
                fobject.gv_edit_shipment.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit_shipment.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit_shipment.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit_shipment.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                'fobject.gv_edit_shipment.SetRowCellValue(_row, "pcss_open", ds.Tables(0).Rows(_row_gv).Item("qty_shipment"))
                'fobject.gv_edit_shipment.SetRowCellValue(_row, "pcss_shipment", ds.Tables(0).Rows(_row_gv).Item("qty_shipment"))
                'fobject.gv_edit_shipment.SetRowCellValue(_row, "pcss_packing", ds.Tables(0).Rows(_row_gv).Item("qty_packing"))
                'fobject.gv_edit_shipment.SetRowCellValue(_row, "pcss_collie_number", ds.Tables(0).Rows(_row_gv).Item("1"))
                'fobject.gv_edit_shipment.SetRowCellValue(_row, "pcss_close_line", ds.Tables(0).Rows(_row_gv).Item("N"))
                fobject.gv_edit_so.BestFitColumns()
            End If
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
