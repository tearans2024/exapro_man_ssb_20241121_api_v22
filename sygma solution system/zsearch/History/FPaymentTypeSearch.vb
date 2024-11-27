﻿Public Class FPaymentTypeSearch
    Public _row, _en_id As Integer

    Private Sub FPaymentTypeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select code_oid, code_dom_id, code_en_id, en_desc, " + _
                     " code_id, code_seq, code_field, " + _
                     " code_code, code_name, code_desc, code_default, " + _
                     " code_active from code_mstr " + _
                     " inner join en_mstr on code_en_id = en_id " + _
                     " where code_field ~~* 'payment_type' and code_active ~~* 'y' " + _
                     " and code_en_id in (0," + _en_id.ToString + ")"
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
        Dim _row_gv As Integer = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FPriceList" Then
            fobject.gv_edit_rule.SetRowCellValue(_row, "pidd_payment_type", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit_rule.SetRowCellValue(_row, "payment_type_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
        ElseIf fobject.name = "FPriceListDetail" Then
            fobject.gv_edit_rule.SetRowCellValue(_row, "pidd_payment_type", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit_rule.SetRowCellValue(_row, "payment_type_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
        End If
    End Sub
End Class
