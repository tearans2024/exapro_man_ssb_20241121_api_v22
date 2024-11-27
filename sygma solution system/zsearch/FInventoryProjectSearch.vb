Public Class FInventoryProjectSearch
    Public _row As Integer
    Public _en_id As Integer

    Private Sub FEntitySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 700
        'Me.Height = 400
        te_search.Focus()

        If fobject.name = FInventoryTransferMultiItem.Name Then
            BtOK.Visible = False
            BtCancel.Visible = False
            sb_fill.Visible = True
        Else
            BtOK.Visible = True
            BtCancel.Visible = True
            sb_fill.Visible = False
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Project Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty", "invc_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = FChangeInventoryAllocation.Name Then
            get_sequel = "SELECT false as pilih, " _
                & "  a.invc_oid, " _
                & "  b.en_desc, " _
                & "  a.invc_en_id, " _
                & "  a.invc_si_id, " _
                & "  f.si_desc, " _
                & "  a.invc_loc_id, " _
                & "  c.loc_desc, " _
                & "  a.invc_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  d.pt_desc2, " _
                & "  d.pt_ls, " _
                & "  a.invc_qty, " _
                & "  a.invc_pjc_id, " _
                & "  cost_g.sct_total as invct_cost, " _
                & "  e.pjc_code " _
                & "FROM " _
                & "  public.invc_mstr a " _
                & "  INNER JOIN public.loc_mstr c ON (a.invc_loc_id = c.loc_id) " _
                & "  INNER JOIN public.pt_mstr d ON (a.invc_pt_id = d.pt_id) " _
                & "  INNER JOIN public.en_mstr b ON (a.invc_en_id = b.en_id) " _
                & "  INNER JOIN public.pjc_mstr e ON (a.invc_pjc_id = e.pjc_id) " _
                & "  INNER JOIN public.si_mstr f ON (a.invc_si_id = f.si_id) " _
                & "  INNER JOIN public.is_mstr g ON (c.loc_is_id = g.is_id) " _
                & "  left OUTER join sct_mstr cost_g on ( cost_g.sct_pt_id=pt_id and cost_g.sct_en_id=en_id and cost_g.sct_cs_id = (SELECT r.cs_id FROM public.cs_mstr r WHERE r.cs_type = 'G')) " _
                & "WHERE " _
                & " (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%') " _
                & " AND a.invc_en_id in (0, " & _en_id.ToString & ") AND  " _
                & "  g.is_avail = 'Y' " _
                & " and invc_qty <> 0 " _
                & "ORDER BY " _
                & "  d.pt_desc1"
        ElseIf fobject.name = FInventoryTransferMultiItem.Name Then
            get_sequel = "SELECT  false as pilih, " _
                        & "  invc_dom_id, " _
                        & "  invc_en_id, " _
                        & "  invc_si_id, " _
                        & "  invc_loc_id, " _
                        & "  invc_pt_id, " _
                        & "  sum(invc_qty) as invc_qty, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  en_desc, " _
                        & "  si_desc, " _
                        & "  loc_code, " _
                        & "  loc_desc, " _
                        & "  pjc_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_ls, " _
                        & "  pl_desc, " _
                        & "  pjc_code, " _
                        & "  pt_is_active, " _
                        & "  cost_g.sct_total as invct_cost " _
                        & "FROM  " _
                        & "  invc_mstr " _
                        & "  inner join en_mstr on en_id = invc_en_id " _
                        & "  inner join si_mstr on si_id = invc_si_id " _
                        & "  inner join loc_mstr on loc_id = invc_loc_id " _
                        & "  inner join pt_mstr on pt_id = invc_pt_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = pt_um " _
                        & "  inner join pl_mstr on pt_pl_id = pl_id " _
                        & "  left outer join pjc_mstr on pjc_id = invc_pjc_id " _
                        & "  left OUTER join sct_mstr cost_g on ( cost_g.sct_pt_id=pt_id and cost_g.sct_en_id=en_id and cost_g.sct_cs_id = (SELECT r.cs_id FROM public.cs_mstr r WHERE r.cs_type = 'G')) " _
                        & "  where invc_en_id = " + _en_id.ToString _
                        & "  and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%') " _
                        & "  group by false, " _
                        & "  invc_dom_id, " _
                        & "  invc_en_id, " _
                        & "  invc_si_id, " _
                        & "  invc_loc_id, " _
                        & "  invc_pt_id, " _
                        & "  en_desc, " _
                        & "  si_desc, " _
                        & "  loc_code, " _
                        & "  loc_desc, " _
                        & "  pjc_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pl_desc,  " _
                        & "  pt_ls, " _
                        & "  pjc_code,  " _
                        & "  pt_is_active,  " _
                        & "  cost_g.sct_total,  " _
                        & "  code_name " _
                        & "  having sum(invc_qty) > 0 " _
                        & "  order by pt_code "
        End If

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        'Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            'Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim _cost_standar As Double = 0
        Dim sSQL As String

        fobject.gv_edit.CancelUpdateCurrentRow()

        If fobject.name = FChangeInventoryAllocation.Name Then
            For Each _dr As DataRow In ds.Tables(0).Rows
                If _dr("pilih") = True Then
                    sSQL = "select f_get_cost(" & _dr("invc_en_id") & "," _
                        & _dr("invc_pt_id") & ",'G') as cost "
                    _cost_standar = master_new.PGSqlConn.GetRowInfo(sSQL)(0)

                    Dim _dtrow As DataRow
                    _dtrow = fobject.ds_edit.Tables(0).NewRow
                    _dtrow("ciad_invc_oid") = _dr("invc_oid")
                    _dtrow("pt_code") = _dr("pt_code")
                    _dtrow("pt_desc1") = _dr("pt_desc1")
                    _dtrow("pt_desc2") = _dr("pt_desc2")
                    _dtrow("loc_desc") = _dr("loc_desc")
                    _dtrow("qty_inv") = _dr("invc_qty")
                    _dtrow("ciad_cost") = _cost_standar
                    _dtrow("ciad_qty") = 0
                    _dtrow("ciad_from_pjc_id") = _dr("invc_pjc_id")
                    _dtrow("pjc_code") = _dr("pjc_code")

                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                End If
            Next

            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FInventoryTransferMultiItem.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "invmtd_pt_id", ds.Tables(0).Rows(_row_gv).Item("invc_pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "invmtd_pjc_id_from", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_code_from", ds.Tables(0).Rows(_row_gv).Item("pjc_code"))
            fobject.gv_edit.SetRowCellValue(_row, "invmtd_loc_id_from", ds.Tables(0).Rows(_row_gv).Item("invc_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc_from", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "invmtd_qty", ds.Tables(0).Rows(_row_gv).Item("invc_qty"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "invmtd_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))

            fobject.gv_edit.BestFitColumns()
        End If
        Me.Close()
    End Sub

    Private Sub CE_All_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CE_All.EditValueChanged
        Try
            For i As Integer = 0 To gv_master.RowCount - 1
                gv_master.SetRowCellValue(i, "pilih", CE_All.EditValue)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtOK.Click
        Try
            fill_data()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub sb_fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_fill.Click
        Try
            Dim _row_pos As Integer
            Dim jml As Integer = 0
            ds.Tables(0).AcceptChanges()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("pilih") = True Then
                    If fobject.name = "FInventoryTransferMultiItem" Then
                        If jml = 0 Then
                            fobject.gv_edit.SetRowCellValue(_row, "invmtd_pt_id", ds.Tables(0).Rows(i).Item("invc_pt_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(i).Item("pt_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(i).Item("pt_desc1"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(i).Item("pt_desc2"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(i).Item("pt_ls"))
                            fobject.gv_edit.SetRowCellValue(_row, "invmtd_pjc_id_from", ds.Tables(0).Rows(i).Item("pjc_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "pjc_code_from", ds.Tables(0).Rows(i).Item("pjc_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "invmtd_loc_id_from", ds.Tables(0).Rows(i).Item("invc_loc_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "loc_desc_from", ds.Tables(0).Rows(i).Item("loc_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "invmtd_qty", ds.Tables(0).Rows(i).Item("invc_qty"))
                            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(i).Item("um_name"))
                            fobject.gv_edit.SetRowCellValue(_row, "invmtd_cost", ds.Tables(0).Rows(i).Item("invct_cost"))
                            fobject.gv_edit.BestFitColumns()
                            jml = jml + 1
                        Else
                            fobject.gv_edit.AddNewRow()
                            _row_pos = fobject.gv_edit.FocusedRowHandle()
                            fobject.gv_edit.SetRowCellValue(_row_pos, "invmtd_pt_id", ds.Tables(0).Rows(i).Item("invc_pt_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_code", ds.Tables(0).Rows(i).Item("pt_code"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_desc1", ds.Tables(0).Rows(i).Item("pt_desc1"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_desc2", ds.Tables(0).Rows(i).Item("pt_desc2"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_ls", ds.Tables(0).Rows(i).Item("pt_ls"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "invmtd_pjc_id_from", ds.Tables(0).Rows(i).Item("pjc_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pjc_code_from", ds.Tables(0).Rows(i).Item("pjc_code"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "invmtd_loc_id_from", ds.Tables(0).Rows(i).Item("invc_loc_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "loc_desc_from", ds.Tables(0).Rows(i).Item("loc_desc"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "invmtd_qty", ds.Tables(0).Rows(i).Item("invc_qty"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "um_name", ds.Tables(0).Rows(i).Item("um_name"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "invmtd_cost", ds.Tables(0).Rows(i).Item("invct_cost"))
                            fobject.gv_edit.BestFitColumns()
                        End If

                        fobject.gv_edit.BestFitColumns()

                    End If
                End If
            Next
        Catch
        End Try

        Me.Close()
    End Sub
End Class
