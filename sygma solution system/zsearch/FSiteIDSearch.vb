Imports master_new.ModFunction

Public Class FSiteIDSearch
    Public _row, _en_id As Integer

    Private Sub FSiteIDSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Warehouse", "wh_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Type", "loc_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Category", "loc_cat_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
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
                    & "  loc_dt " _
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
                    & " and coalesce(loc_eu_site,'N') ~~* 'Y'" _
                    & " and loc_en_id in (0," + _en_id.ToString + ")"
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

        If fobject.name = "FSalesOrderProject" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_loc_eu_site_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "site_code", ds.Tables(0).Rows(_row_gv).Item("loc_code"))
            fobject.gv_edit.SetRowCellValue(_row, "site_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
