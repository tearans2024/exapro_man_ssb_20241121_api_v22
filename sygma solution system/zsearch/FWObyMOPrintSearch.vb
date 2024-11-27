Imports master_new.ModFunction

Public Class FWObyMOPrintSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _obj As Object
    Public _interval As Integer
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FWObyMOPrintSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "WO No.", "wo_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "Partnumber Project", "pt_code_prj", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partnumber Description Project", "pt_desc1_prj", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "Partnumber", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partnumber Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partnumber Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Bom Description", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Routing Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                        & "  a.wo_oid, " _
                        & "  a.wo_dom_id, " _
                        & "  a.wo_en_id, " _
                        & "  a.wo_si_id, " _
                        & "  a.wo_id, " _
                        & "  a.wo_code, " _
                        & "  a.wo_type, " _
                        & "  a.wo_pt_id, " _
                        & "  a.wo_qty_ord, " _
                        & "  a.wo_qty_comp, " _
                        & "  a.wo_qty_rjc,coalesce(wo_qty_ord,0) - coalesce(wo_qty_rjc,0) - coalesce(wo_qty_rjc,0) as wo_qty_remaining, " _
                        & "  a.wo_ord_date, " _
                        & "  a.wo_rel_date, " _
                        & "  a.wo_due_date, " _
                        & "  a.wo_insheet_pct, " _
                        & "  a.wo_ro_id, " _
                        & "  a.wo_status, " _
                        & "  a.wo_remarks, " _
                        & "  a.wo_dt, " _
                        & "  a.wo_date_close, " _
                        & "  a.wo_pjc_id, " _
                        & "  a.wo_ref_rework, " _
                        & "  a.wo_qty, " _
                        & "  b.en_desc, " _
                        & "  c.pt_code, " _
                        & "  c.pt_desc1, " _
                        & "  c.pt_desc2,a.wo_ps_id,ps_par,c.pt_ls,c.pt_um,k.code_name as unit_measure,wo_cost, " _
                        & "  e.ro_code, " _
                        & "  e.ro_desc, a.wo_pjc_oid, " _
                        & "  g.pjc_code, " _
                        & "  g.pjc_date, " _
                        & "  '' AS wo_ref_rework, " _
                        & "  l.pt_code as pt_code_prj,wo_pt_id_prj, " _
                        & "  l.pt_desc1 as pt_desc1_prj, " _
                        & "  j.si_desc " _
                        & "FROM " _
                        & "  public.wo_mstr a " _
                        & "  INNER JOIN public.en_mstr b ON (a.wo_en_id = b.en_id) " _
                        & "  INNER JOIN public.pt_mstr c ON (a.wo_pt_id = c.pt_id) " _
                        & "  INNER JOIN public.ps_mstr d ON (a.wo_ps_id = d.ps_id) " _
                        & "  INNER JOIN public.ro_mstr e ON (a.wo_ro_id = e.ro_id) " _
                        & "  INNER JOIN public.pjc_mstr g ON (a.wo_pjc_oid = g.pjc_oid) " _
                        & "  LEFT OUTER JOIN public.si_mstr j ON (a.wo_si_id = j.si_id) " _
                        & "  LEFT OUTER JOIN public.code_mstr k ON (c.pt_um = k.code_id) " _
                        & "  LEFT OUTER JOIN public.pt_mstr l ON (a.wo_pt_id_prj = l.pt_id) " _
                        & "where a.wo_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and a.wo_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  and a.wo_en_id = " & _en_id.ToString _
                        & "  and a.wo_status = 'R' " _
                        & " order by wo_code"

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

        _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code")
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
