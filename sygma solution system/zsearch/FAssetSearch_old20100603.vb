Imports master_new.ModFunction

Public Class FAssetSearch
    Public _row, _en_id As Integer
    Public _pt_id As Integer

    Private Sub FAssetSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        If fobject.name = "FAssetTransferIssue" Then
            get_sequel = "SELECT  " _
                    & "  ass_en_id, " _
                    & "  en_desc, " _
                    & "  ass_id,ass_code, " _
                    & "  ass_pt_id, " _
                    & "  pt_code,pt_desc1,pt_desc2, " _
                    & "  ass_desc, " _
                    & "  ass_qty,ass_qty_assgn, " _
                    & "  ass_confirm,ass_emp_id " _
                    & " FROM  " _
                    & "  public.ass_mstr" _
                    & " inner join en_mstr on en_id = ass_en_id " _
                    & " inner join pt_mstr on pt_id = ass_pt_id " _
                    & " where ass_pt_id = " + _pt_id.ToString() _
                    & " and (ass_code ~~* '%" + Trim(te_search.Text) + "%' or pt_code ~~* '%" + Trim(te_search.Text) + "%' or ass_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and ass_qty_assgn <> 1 and ass_qty_del <> 1 and ass_confirm = 'Y' " _
                    & " and ass_en_id in (0," + _en_id.ToString + ")"
        ElseIf fobject.name = "FAssetRetirement" Then
            get_sequel = "SELECT  " _
                    & "  ass_en_id, " _
                    & "  en_desc, " _
                    & "  ass_id,ass_code, " _
                    & "  ass_pt_id, " _
                    & "  pt_code,pt_desc1,pt_desc2, " _
                    & "  ass_desc, " _
                    & "  ass_qty,ass_qty_assgn, " _
                    & "  ass_confirm,ass_emp_id " _
                    & " FROM  " _
                    & "  public.ass_mstr" _
                    & " inner join en_mstr on en_id = ass_en_id " _
                    & " inner join pt_mstr on pt_id = ass_pt_id " _
                    & " where (ass_code ~~* '%" + Trim(te_search.Text) + "%' or pt_code ~~* '%" + Trim(te_search.Text) + "%' or ass_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and ass_confirm = 'Y' " _
                    & " and ass_en_id in (0," + _en_id.ToString + ")"
        ElseIf fobject.name = "FAssetBack" Then
            get_sequel = "SELECT  " _
                    & "  ass_en_id, " _
                    & "  en_desc, " _
                    & "  ass_id,ass_code, " _
                    & "  ass_pt_id, " _
                    & "  pt_code,pt_desc1,pt_desc2, " _
                    & "  ass_desc, " _
                    & "  ass_qty,ass_qty_assgn, " _
                    & "  ass_confirm,ass_emp_id " _
                    & " FROM  " _
                    & "  public.ass_mstr" _
                    & " inner join en_mstr on en_id = ass_en_id " _
                    & " inner join pt_mstr on pt_id = ass_pt_id " _
                    & " where (ass_code ~~* '%" + Trim(te_search.Text) + "%' or pt_code ~~* '%" + Trim(te_search.Text) + "%' or ass_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and ass_qty_assgn = 1 and ass_confirm = 'Y' " _
                    & " and ass_en_id in (0," + _en_id.ToString + ")"
        Else
            get_sequel = "SELECT  " _
                    & "  ass_en_id, " _
                    & "  en_desc, " _
                    & "  ass_id,ass_code, " _
                    & "  ass_pt_id, " _
                    & "  pt_code,pt_desc1,pt_desc2, " _
                    & "  ass_desc, " _
                    & "  ass_qty,ass_qty_assgn, " _
                    & "  ass_confirm,ass_emp_id " _
                    & " FROM  " _
                    & "  public.ass_mstr" _
                    & " inner join en_mstr on en_id = ass_en_id " _
                    & " inner join pt_mstr on pt_id = ass_pt_id " _
                    & " where (ass_code ~~* '%" + Trim(te_search.Text) + "%' or pt_code ~~* '%" + Trim(te_search.Text) + "%' or ass_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and ass_qty_assgn = 0 and ass_confirm = 'Y' " _
                    & " and ass_en_id in (0," + _en_id.ToString + ")"
        End If

        Return get_sequel

    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FAssetTransferIssue" Then
            fobject.gv_edit_detail.SetRowCellValue(_row, "reqds_ass_id", ds.Tables(0).Rows(_row_gv).Item("ass_id"))
            fobject.gv_edit_detail.SetRowCellValue(_row, "ass_code", ds.Tables(0).Rows(_row_gv).Item("ass_code"))
            fobject.gv_edit_detail.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("ass_pt_id"))
            'fobject.gv_edit_detail.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            'fobject.gv_edit_detail.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            'fobject.gv_edit_detail.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            'fobject.gv_edit_detail.SetRowCellValue(_row, "reqds_qty", ds.Tables(0).Rows(_row_gv).Item("ass_qty"))
            'fobject.gv_edit_detail.SetRowCellValue(_row, "reqds_emp_id", ds.Tables(0).Rows(_row_gv).Item("ass_emp_id"))
            fobject.gv_edit_detail.BestFitColumns()
        ElseIf fobject.name = "FAssetRetirement" Then
            fobject.gv_edit.SetRowCellValue(_row, "asrtrd_ass_id", ds.Tables(0).Rows(_row_gv).Item("ass_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ass_code", ds.Tables(0).Rows(_row_gv).Item("ass_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ass_desc", ds.Tables(0).Rows(_row_gv).Item("ass_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "asrtrd_rmks", "-")
            fobject.gv_edit.SetRowCellValue(_row, "asrtrd_dispose_date", Today())
            fobject.gv_edit.SetRowCellValue(_row, "asrtrd_dispose_cost", 0)
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetBack" Then
            fobject.gv_edit.SetRowCellValue(_row, "asbackd_ass_id", ds.Tables(0).Rows(_row_gv).Item("ass_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ass_code", ds.Tables(0).Rows(_row_gv).Item("ass_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ass_desc", ds.Tables(0).Rows(_row_gv).Item("ass_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "asbackd_rmks", "-")
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
