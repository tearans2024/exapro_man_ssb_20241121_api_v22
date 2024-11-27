Imports master_new.ModFunction

Public Class FItemCostSearch
    Public _row, _en_id, _pt_id, _col As Integer
    Public grid_call As String = ""

    Private Sub FItemCostSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.invct_table.invct_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.invct_table.invct_cost " _
                    & "FROM " _
                    & "  public.invct_table " _
                    & "  INNER JOIN public.pt_mstr ON (public.invct_table.invct_pt_id = public.pt_mstr.pt_id) " _
                    & " where public.invct_table.invct_pt_id = " + _pt_id.ToString + " "
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
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

        If fobject.name = "FProductStructure" Then
            fobject.gv_edit.SetRowCellValue(_row, "psd_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRequisition" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryReceipts" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssues" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryRequestWO" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderAlocated" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryReturns" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FProjectMaintenance" Then
            If grid_call = "gv_edit" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
                fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf grid_call = "gv_edit_cust" Then
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
                fobject.gv_edit_cust.BestFitColumns()
            End If
        ElseIf fobject.name = FWorkOrderIssue.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wocid_si_id", ds.Tables(0).Rows(_row_gv).Item("si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
