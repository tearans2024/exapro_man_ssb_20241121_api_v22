Imports master_new.ModFunction

Public Class FInventoryPISearch
    Public _row, _en_id, _pi_id, _so_pi_id, _pt_id, _qty, par_pi_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Dim func_data As New function_data

    Private Sub FInventorySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()

        add_column(gv_master, "Entity", "pi_en_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "pi_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Item", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty Available", "invc_qty", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        'Dim _en_id_coll As String = func_data.entity_parent(_en_id)
        'Dim _pi_id_coll As String = func_data.pricelist_parent(_pi_id)
        get_sequel = ""

        get_sequel = "SELECT  " _
            & "  public.pi_mstr.pi_dom_id, " _
            & "  public.pi_mstr.pi_en_id, " _
            & "  public.pi_mstr.pi_id, " _
            & "  public.pi_mstr.pi_code, " _
            & "  public.pi_mstr.pi_desc, " _
            & "  public.pid_det.pid_pt_id, " _
            & "  public.pt_mstr.pt_code, " _
            & "  public.pt_mstr.pt_desc1, " _
            & "  public.pt_mstr.pt_desc2, " _
            & "  public.loc_mstr.loc_desc, " _
            & "  public.invc_mstr.invc_loc_id, " _
            & "  public.invc_mstr.invc_qty " _
            & "FROM " _
            & "  public.pi_mstr " _
            & "  INNER JOIN public.pid_det ON (public.pi_mstr.pi_oid = public.pid_det.pid_pi_oid) " _
            & "  INNER JOIN public.pt_mstr ON (public.pid_det.pid_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.invc_mstr ON (public.pt_mstr.pt_id = public.invc_mstr.invc_pt_id) " _
            & "  INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id)" _
            & " where (loc_desc ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
            & " and pi_en_id = " + _en_id.ToString _
            & " and pi_id = " + par_pi_id.ToString
        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FSalesOrderManualAllocation" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", ds.Tables(0).Rows(_row_gv).Item("invc_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_loc_id", ds.Tables(0).Rows(_row_gv).Item("invc_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc_invc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))

            If ds.Tables(0).Rows(_row_gv).Item("invc_qty_open") >= _qty Then
                fobject.gv_edit.SetRowCellValue(_row, "sod_qty_allocated", _qty)
            Else
                fobject.gv_edit.SetRowCellValue(_row, "sod_qty_allocated", ds.Tables(0).Rows(_row_gv).Item("invc_qty_open"))
            End If

        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
