Imports master_new.ModFunction

Public Class FAccountSearch
    Public _row, _cu_id As Integer

    Private Sub FAccountSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 360
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Deskripsi", "ac_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FVoucher" Then
            get_sequel = "SELECT  " _
                    & "  b.ac_id, " _
                    & "  b.ac_code, " _
                    & "  b.ac_name, " _
                    & "  b.ac_desc " _
                    & "FROM " _
                    & "  public.ac_mstr b " _
                    & " where (ac_code ~~* '%' or ac_name ~~* '%' or ac_desc ~~* '%')" _
                    & " and ac_active ~~* 'Y'" _
                    & " and ac_is_sumlevel ~~* 'N'" _
                    & " order by ac_code"
        Else
            get_sequel = "SELECT  " _
                    & "  b.ac_id, " _
                    & "  b.ac_code, " _
                    & "  b.ac_name, " _
                    & "  b.ac_desc " _
                    & "FROM " _
                    & "  public.ac_mstr b " _
                    & " where (ac_code ~~* '%' or ac_name ~~* '%' or ac_desc ~~* '%')" _
                    & " and ac_active ~~* 'Y'" _
                    & " and ac_is_sumlevel ~~* 'N'" _
                    & " order by ac_code"
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

        If fobject.name = "FStandardTransaction" Then
            fobject.gv_edit.SetRowCellValue(_row, "glt_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FVoucher" Then
            fobject.gv_edit_dist.SetRowCellValue(_row, "apd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "apd_remarks", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_dist.BestFitColumns()
        ElseIf fobject.name = "FInventoryReceipts" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssues" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FDisbursementRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbyd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pbyd_desc", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FDisbursementVoucher" Then
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_ac_id", ds.Tables(0).Rows(_row_gv).Item("ac_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_desc", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_cash.BestFitColumns()
        End If
    End Sub
End Class
