Imports master_new.ModFunction

Public Class FPackingSheetSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _obj As Object
    Public _interval As Integer
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FPackingSheetSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Packing Code", "pcs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Packing Date Date", "pcs_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT" _
                        & "  pcs_oid, " _
                        & "  pcs_en_id, " _
                        & "  pcs_code, " _
                        & "  pcs_bill_to, " _
                        & "  ptnr_name, " _
                        & "  pcs_date " _
                        & "FROM  " _
                        & "  pcs_mstr " _
                        & "  INNER JOIN public.ptnr_mstr ON ptnr_id = pcs_bill_to " _
                        & "  where pcs_en_id = " & _en_id.ToString _
                        & "  and pcs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and pcs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  order by pcs_code " _
                        & "  ASC "

        'If _interval = 0 Then
        '    get_sequel = get_sequel + "and coalesce(so_interval,-1) = 0"
        'Else
        '    get_sequel = get_sequel + "and coalesce(so_interval,-1) > 0"
        'End If

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

        _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pcs_code")
        ''fobject.gv_edit_so.SetRowCellValue(_row, "pcso_oid", Guid.NewGuid.ToString)
        ''fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid"))
        'fobject.gv_edit_so.SetRowCellValue(_row, "be_first", ds.Tables(0).Rows(_row_gv).Item("pcs_code"))
        'fobject.gv_edit_so.SetRowCellValue(_row, "be_to", ds.Tables(0).Rows(_row_gv).Item("pcs_code"))
        ''fobject.gv_edit_so.SetRowCellValue(_row, "pcso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
        'fobject.gv_edit_so.BestFitColumns()

    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
