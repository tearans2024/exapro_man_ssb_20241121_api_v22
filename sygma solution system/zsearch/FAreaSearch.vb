Public Class FAreaSearch
    Public _row, _en_id As Integer

    Private Sub FAreaSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "area_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "area_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.area_mstr.area_oid, " _
                    & "  public.area_mstr.area_dom_id, " _
                    & "  public.area_mstr.area_add_by, " _
                    & "  public.area_mstr.area_code, " _
                    & "  public.area_mstr.area_name, " _
                    & "  public.area_mstr.area_desc, " _
                    & "  public.area_mstr.area_id, " _
                    & "  public.area_mstr.area_add_date, " _
                    & "  public.area_mstr.area_upd_by, " _
                    & "  public.area_mstr.area_upd_date, " _
                    & "  public.area_mstr.area_parent, " _
                    & "  public.area_mstr.area_dt " _
                    & "FROM " _
                    & "  public.area_mstr"

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
            fobject.gv_edit_rule.SetRowCellValue(_row, "pidd_area_id", ds.Tables(0).Rows(_row_gv).Item("area_id"))
            fobject.gv_edit_rule.SetRowCellValue(_row, "area_name", ds.Tables(0).Rows(_row_gv).Item("area_name"))
        ElseIf fobject.name = "FPriceListDetail" Then
            fobject.gv_edit_rule.SetRowCellValue(_row, "pidd_area_id", ds.Tables(0).Rows(_row_gv).Item("area_id"))
            fobject.gv_edit_rule.SetRowCellValue(_row, "area_name", ds.Tables(0).Rows(_row_gv).Item("area_name"))
        ElseIf fobject.name = "FParnerAll" Then
            fobject.gv_edit_rule.SetRowCellValue(_row, "ptnr_area_id", ds.Tables(0).Rows(_row_gv).Item("area_id"))
            fobject.gv_edit_rule.SetRowCellValue(_row, "area_name", ds.Tables(0).Rows(_row_gv).Item("area_name"))
        ElseIf fobject.name = "FSalesQuotationConsigmentAlocated" Then
            fobject.gv_edit_rule.SetRowCellValue(_row, "sq_pi_area_id", ds.Tables(0).Rows(_row_gv).Item("area_id"))
            fobject.gv_edit_rule.SetRowCellValue(_row, "area_name", ds.Tables(0).Rows(_row_gv).Item("area_name"))
            End If
    End Sub
End Class
