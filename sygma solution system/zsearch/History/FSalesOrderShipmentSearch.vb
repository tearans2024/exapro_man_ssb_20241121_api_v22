﻿Imports master_new.ModFunction

Public Class FSalesOrderShipmentSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FSalesOrderShipmentSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'If FDPackingSheetPrintOut Then
        add_column_edit(gv_master, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "SO Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
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
            get_sequel = "SELECT  DISTINCT" _
                            & "  soship_oid, " _
                            & "  soship_code, " _
                            & "  soship_date, " _
                            & "  so_oid, " _
                            & "  so_code, " _
                            & "  so_date, " _
                            & "  ptnr_id, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.soship_mstr " _
                            & "  inner join so_mstr on so_oid = soship_so_oid " _
                            & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                            & "  where soship_is_shipment ~~* 'Y' " _
                            & "  and soshipd_qty_inv > '0' " _
                            & "  and soship_en_id = " & _en_id.ToString _
                            & "  and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & "  and ptnr_id = " & _ptnr_id.ToString _
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
            If _obj.name = "gv_edit_so" Then
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_oid", Guid.NewGuid.ToString)
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid"))
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
                fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
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
