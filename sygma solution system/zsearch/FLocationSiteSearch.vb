Imports master_new.ModFunction

Public Class FLocationSiteSearch
    Public _row, _en_id, _pjc_id, _ptnr_id As Integer
    Public grid_call, _colname As String

    Private Sub FLocationSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 600
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        If fobject.name = "FSoPj" Then
            get_sequel = "SELECT  " _
                    & "  loc_oid, " _
                    & "  loc_dom_id, " _
                    & "  loc_en_id, " _
                    & "  en_code, " _
                    & "  en_desc, " _
                    & "  loc_id, " _
                    & "  loc_wh_id, " _
                    & "  wh_desc, " _
                    & "  loc_si_id, " _
                    & "  si_desc, " _
                    & "  loc_code, " _
                    & "  loc_desc, " _
                    & "  loc_type, " _
                    & "  loc_type_mstr.code_name as loc_type_name, " _
                    & "  loc_cat, " _
                    & "  loc_cat_mstr.code_name as loc_cat_name, " _
                    & "  loc_is_id, " _
                    & "  is_desc, " _
                    & "  loc_active, " _
                    & "  loc_dt, " _
                    & "  loc_pjc_id, " _
                    & "  loc_eu_site " _
                    & "FROM  " _
                    & "  public.loc_mstr" _
                    & " inner join en_mstr on en_id = loc_en_id " _
                    & " inner join wh_mstr on wh_id = loc_wh_id " _
                    & " inner join si_mstr on si_id = loc_si_id " _
                    & " inner join is_mstr on is_id = loc_is_id " _
                    & " inner join code_mstr as loc_type_mstr on loc_type_mstr.code_id = loc_type" _
                    & " inner join code_mstr as loc_cat_mstr on loc_cat_mstr.code_id = loc_cat" _
                    & " where (loc_code ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and loc_active ~~* 'Y'" _
                    & " and loc_en_id in (0," + _en_id.ToString + ")" _
                    & " or loc_id = 0 "

            'Rev By Hendrik 2011-02-20 ======================================================================
        ElseIf fobject.name = "FProjectMaintenance" Then
            get_sequel = "SELECT  " _
                    & "  loc_oid, " _
                    & "  loc_dom_id, " _
                    & "  loc_en_id, " _
                    & "  en_code, " _
                    & "  en_desc, " _
                    & "  loc_id, " _
                    & "  loc_wh_id, " _
                    & "  loc_si_id, " _
                    & "  si_desc, " _
                    & "  loc_code, " _
                    & "  loc_desc, " _
                    & "  loc_type, " _
                    & "  loc_cat, " _
                    & "  loc_is_id, " _
                    & "  loc_active, " _
                    & "  loc_dt, " _
                    & "  loc_pjc_id, " _
                    & "  loc_eu_site " _
                    & "FROM  " _
                    & "  public.loc_mstr" _
                    & " inner join en_mstr on en_id = loc_en_id " _
                    & " inner join si_mstr on si_id = loc_si_id " _
                    & " where (loc_code ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and loc_active ~~* 'Y' " _
                    & " and loc_ptnr_id = " + SetInteger(_ptnr_id) + " " _
                    & " and loc_en_id in (0," + _en_id.ToString + ")" _
                    & " or loc_id = 0 "
            '=============================================================================================
        Else
            get_sequel = "SELECT  " _
                    & "  loc_oid, " _
                    & "  loc_dom_id, " _
                    & "  loc_en_id, " _
                    & "  en_code, " _
                    & "  en_desc, " _
                    & "  loc_id, " _
                    & "  loc_wh_id, " _
                    & "  loc_si_id, " _
                    & "  si_desc, " _
                    & "  loc_code, " _
                    & "  loc_desc, " _
                    & "  loc_type, " _
                    & "  loc_cat, " _
                    & "  loc_is_id, " _
                    & "  loc_active, " _
                    & "  loc_dt, " _
                    & "  loc_pjc_id, " _
                    & "  loc_eu_site " _
                    & "FROM  " _
                    & "  public.loc_mstr" _
                    & " inner join en_mstr on en_id = loc_en_id " _
                    & " inner join si_mstr on si_id = loc_si_id " _
                    & " where (loc_code ~~* '%" + Trim(te_search.Text) + "%' or loc_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and loc_active ~~* 'Y' " _
                    & " and loc_en_id in (0," + _en_id.ToString + ")" '_
            '& " and loc_pjc_id = " + _pjc_id.ToString() _
            '& " or loc_id = 0 "
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

        If fobject.name = "FDisbursementRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbyd_loc_eu_site_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FDisbursementRealization" Then
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_loc_eu_site_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit_cash.BestFitColumns()
        ElseIf fobject.name = "FDisbursementRealizationVerification" Then
            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_loc_eu_site_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit_cash.BestFitColumns()
        ElseIf fobject.name = "FSoPj" Then
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_loc_eu_site_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrder_NonBudget" Or fobject.name = "FPurchaseOrderExpense" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_loc_eu_site_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "siteid_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPaymentOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_loc_eu_site_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "siteid_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FProjectMaintenance" Then
            If grid_call = "gv_edit" And _colname = "loc_desc" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf grid_call = "gv_edit" And _colname = "loc_desc_imp" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_loc_imp_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc_imp", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.BestFitColumns()
            ElseIf grid_call = "gv_edit_cust" And _colname = "loc_desc" Then
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit_cust.BestFitColumns()
            ElseIf grid_call = "gv_edit_cust" And _colname = "loc_desc_imp" Then
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_loc_imp_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "loc_desc_imp", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit_cust.BestFitColumns()
            End If
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
