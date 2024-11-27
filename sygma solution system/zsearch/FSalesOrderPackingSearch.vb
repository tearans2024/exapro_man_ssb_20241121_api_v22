Imports master_new.ModFunction

Public Class FSalesOrderPackingSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FSalesOrderPackingSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FDPackingSheetNew" Then
            get_sequel = "SELECT DISTINCT  " _
                            & "  public.so_mstr.so_code, " _
                            & "  public.so_mstr.so_oid, " _
                            & "  public.so_mstr.so_date, " _
                            & "  public.so_mstr.so_ptnr_id_bill, " _
                            & "  public.ptnr_mstr.ptnr_id, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.soship_mstr.soship_oid, " _
                            & "  public.soship_mstr.soship_code " _
                            & "FROM " _
                            & "  public.so_mstr " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                            & "  INNER JOIN public.soship_mstr ON (public.so_mstr.so_oid = public.soship_mstr.soship_so_oid) " _
                            & "WHERE " _
                            & "  public.so_mstr.so_return IS NULL " _
                            & "  and so_en_id = " & _en_id.ToString _
                            & "  and so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
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

        If fobject.name = FDPackingSheetNew.Name Then
            'fobject.gv_edit_so.SetRowCellValue(_row, "dopd_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid"))
            fobject.gv_edit_so.SetRowCellValue(_row, "dopd_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid"))
            fobject.gv_edit_so.SetRowCellValue(_row, "dopd_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
            fobject.gv_edit_so.BestFitColumns()

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
