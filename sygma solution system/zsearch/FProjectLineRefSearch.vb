Public Class FProjectLineRefSearch
    Public _grid_call, _prj_code As String
    Public _row As Integer

    Private Sub FProjectLineRefSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FProjectMaintenance" And _grid_call = "gv_edit" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Line", "prjd_seq", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description 1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description 2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Default)
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FProjectMaintenance" And _grid_call = "gv_edit" Then
            get_sequel = "SELECT " _
                    & "  prjd_oid,   " _
                    & "  prj_code,   " _
                    & "  prj_ord_date, " _
                    & "  prjd_seq, " _
                    & "  pt_code, " _
                    & "  prjd_pt_desc1, " _
                    & "  prjd_pt_desc2, " _
                    & "  loc_desc, " _
                    & "  prjd_qty, " _
                    & "  prjd_cost, " _
                    & "  prjd_price " _
                    & "FROM  " _
                    & "  public.prjd_det " _
                    & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                    & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                    & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                    & "  where coalesce(prjd_trans_id,'D') not in ('X','C') " _
                    & "  and coalesce(prjd_qty_shipment,0) = 0 " _
                    & "  and coalesce(prjd_qty_pao,0) = 0 " _
                    & "  and prj_code ~~* '" + _prj_code + "'"
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

        If fobject.name = "FProjectMaintenance" And _grid_call = "gv_edit" Then
            fobject.gv_edit.SetRowCellValue(_row, "prjd_prjd_oid", ds.Tables(0).Rows(_row_gv).Item("prjd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "prjd_ref_line", ds.Tables(0).Rows(_row_gv).Item("prjd_seq"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
