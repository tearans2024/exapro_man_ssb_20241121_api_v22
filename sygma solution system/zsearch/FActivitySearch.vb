Imports master_new.ModFunction

Public Class FActivitySearch
    Public _row, _en_id As Integer

    Private Sub FActivitySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 360
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Deskripsi", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        get_sequel = "SELECT  " _
                    & "  b.code_id, " _
                    & "  b.code_code, " _
                    & "  b.code_name, " _
                    & "  b.code_desc " _
                    & "FROM " _
                    & "  public.code_mstr b " _
                    & " where (code_code ~~* '%' or code_name ~~* '%' or code_desc ~~* '%')" _
                    & " and code_active ~~* 'Y'" _
                    & " and code_en_id = " + _en_id.ToString _
                    & " and code_field ~~* 'activity'" _
                    & " order by code_code"

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

        If fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrder_NonBudget" Or fobject.name = "FPurchaseOrderExpense" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_activity_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "activity_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
        ElseIf fobject.name = "FPaymentOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_activity_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "activity_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
        ElseIf fobject.name = "FDisbursementRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbyd_activity_code_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "activity_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
        ElseIf fobject.name = "FDisbursementRealization" Then
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_activity_code_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "activity_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
        ElseIf fobject.name = "FDisbursementRealization" Then
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_activity_code_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "activity_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
        ElseIf fobject.name = "FDisbursementRealizationVerification" Then
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_activity_code_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "activity_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit_cash.BestFitColumns()
        End If
    End Sub
End Class
