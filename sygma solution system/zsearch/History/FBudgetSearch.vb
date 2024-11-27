Imports master_new.ModFunction

Public Class FBudgetSearch
    Public _row, _cu_id, _cc_id, _en_id As Integer

    Private Sub FBudgetSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FProjectBudgetMaintenance" Then
            add_column(gv_master, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Remark", "prj_remark", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = "FCrossBudget" Then
            add_column(gv_master, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Remark", "prj_remark", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = "FBudgetAdministrasiHistory" Then
            add_column(gv_master, "Code", "bdgth_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Revision", "bdgth_rev", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.name = "FProjectBudgetHistory" Then
            add_column(gv_master, "Code", "bdgth_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Revision", "bdgth_rev", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column(gv_master, "Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        End If

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FCrossBudget" Then
            get_sequel = "SELECT  " _
                        & "  bdgt_oid, " _
                        & "  bdgt_dom_id, " _
                        & "  bdgt_en_id, " _
                        & "  bdgt_add_by, " _
                        & "  bdgt_add_date, " _
                        & "  bdgt_upd_by, " _
                        & "  bdgt_upd_date, " _
                        & "  bdgt_date, " _
                        & "  bdgt_year, " _
                        & "  bdgt_remarks, " _
                        & "  bdgt_trans_id, " _
                        & "  bdgt_dt, " _
                        & "  bdgt_tran_id, " _
                        & "  bdgt_cc_id,cc_desc, " _
                        & "  bdgt_code, " _
                        & "  bdgt_rev, " _
                        & "  bdgt_pjc_id, " _
                        & "  pjc_code, " _
                        & "  bdgt_active,  " _
                        & "  bdgt_year_periode, " _
                        & "  ptnr_name " _
                        & "FROM  " _
                        & "  public.bdgt_mstr " _
                        & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
                        & "  left outer join pjc_mstr on pjc_id = bdgt_pjc_id " _
                        & "  left outer join prj_mstr on prj_code = pjc_code " _
                        & "  left outer join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where bdgt_en_id = " + SetInteger(_en_id) _
                        & "order by bdgt_cc_id asc "
        ElseIf fobject.name = "FBudgetDetail" Then
            get_sequel = "SELECT  " _
                        & "  bdgt_oid, " _
                        & "  bdgt_dom_id, " _
                        & "  bdgt_en_id, " _
                        & "  bdgt_add_by, " _
                        & "  bdgt_add_date, " _
                        & "  bdgt_upd_by, " _
                        & "  bdgt_upd_date, " _
                        & "  bdgt_date, " _
                        & "  bdgt_year, " _
                        & "  bdgt_remarks, " _
                        & "  bdgt_trans_id, " _
                        & "  bdgt_dt, " _
                        & "  bdgt_tran_id, " _
                        & "  bdgt_cc_id,cc_desc, " _
                        & "  bdgt_code, " _
                        & "  bdgt_rev, " _
                        & "  bdgt_pjc_id, " _
                        & "  pjc_code, " _
                        & "  bdgt_active, bdgt_year_periode, ptnr_name, prj_remark " _
                        & "FROM  " _
                        & "  public.bdgt_mstr " _
                        & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
                        & "  left outer join pjc_mstr on pjc_id = bdgt_pjc_id " _
                        & "  left outer join prj_mstr on prj_code = pjc_code " _
                        & "  left outer join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where bdgt_code ~~* '%" + Trim(te_search.Text) + "%'" _
                        & " and bdgt_active = 'Y' " _
                        & " and coalesce(bdgt_pjc_id,0) = 0 " _
                        & "order by bdgt_cc_id asc "
        ElseIf fobject.name = "FProjectBudgetMaintenance" Then
            get_sequel = "SELECT  " _
                    & "  bdgt_oid, " _
                    & "  bdgt_dom_id, " _
                    & "  bdgt_en_id, " _
                    & "  bdgt_add_by, " _
                    & "  bdgt_add_date, " _
                    & "  bdgt_upd_by, " _
                    & "  bdgt_upd_date, " _
                    & "  bdgt_date, " _
                    & "  bdgt_year, " _
                    & "  bdgt_remarks, " _
                    & "  bdgt_trans_id, " _
                    & "  bdgt_dt, " _
                    & "  bdgt_tran_id, " _
                    & "  bdgt_cc_id,cc_desc, " _
                    & "  bdgt_code, " _
                    & "  bdgt_rev, " _
                    & "  bdgt_active, bdgt_year_periode, " _
                    & "  bdgt_pjc_id,pjc_code,pjc_desc, ptnr_name, prj_remark " _
                    & "FROM  " _
                    & "  public.bdgt_mstr " _
                    & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
                    & "  inner join pjc_mstr on pjc_id = bdgt_pjc_id " _
                    & "  left outer join prj_mstr on prj_code = pjc_code " _
                    & "  left outer join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                    & " where bdgt_code ~~* '%" + Trim(te_search.Text) + "%'" _
                    & " and bdgt_active = 'Y' and bdgt_pjc_id > 0 " _
                    & "order by bdgt_cc_id asc "
        ElseIf fobject.name = "FBudgetAdministrasiHistory" Then
            get_sequel = "SELECT  " _
                    & "  bdgth_oid, " _
                    & "  bdgth_cc_id,cc_desc, " _
                    & "  bdgth_code, " _
                    & "  bdgth_rev " _
                    & "FROM  " _
                    & "  public.bdgth_hist " _
                    & "  inner join cc_mstr on cc_id = bdgth_cc_id " _
                    & " where bdgth_code ~~* '%" + Trim(te_search.Text) + "%'" _
                    & " and bdgth_pjc_id = 0 " _
                    & "order by bdgth_code asc "
        ElseIf fobject.name = "FProjectBudgetHistory" Then
            get_sequel = "SELECT  " _
                    & "  bdgth_oid, " _
                    & "  bdgth_pjc_id, pjc_code, " _
                    & "  bdgth_code, " _
                    & "  bdgth_rev " _
                    & "FROM  " _
                    & "  public.bdgth_hist " _
                    & "  inner join pjc_mstr on pjc_id = bdgth_pjc_id " _
                    & " where bdgth_code ~~* '%" + Trim(te_search.Text) + "%'" _
                    & " and bdgth_pjc_id <> 0 " _
                    & "order by bdgth_code asc "
        Else
            get_sequel = "SELECT  " _
                        & "  bdgt_oid, " _
                        & "  bdgt_dom_id, " _
                        & "  bdgt_en_id, " _
                        & "  bdgt_add_by, " _
                        & "  bdgt_add_date, " _
                        & "  bdgt_upd_by, " _
                        & "  bdgt_upd_date, " _
                        & "  bdgt_date, " _
                        & "  bdgt_year, " _
                        & "  bdgt_remarks, " _
                        & "  bdgt_trans_id, " _
                        & "  bdgt_dt, " _
                        & "  bdgt_tran_id, " _
                        & "  bdgt_cc_id,cc_desc, " _
                        & "  bdgt_code, " _
                        & "  bdgt_rev, " _
                        & "  bdgt_pjc_id, " _
                        & "  pjc_code, " _
                        & "  bdgt_active, bdgt_year_periode " _
                        & "FROM  " _
                        & "  public.bdgt_mstr " _
                        & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
                        & "  left outer join pjc_mstr on pjc_id = bdgt_pjc_id " _
                        & " where bdgt_code ~~* '%" + Trim(te_search.Text) + "%'" _
                        & " and bdgt_trans_id = 'I' and bdgt_active = 'Y' " _
                        & "order by bdgt_cc_id asc "
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

        If fobject.name = "FBudget" Then
            fobject.gv_edit.SetRowCellValue(_row, "bdgtd_ac_id", ds.Tables(0).Rows(_row_gv).Item("cca_ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject._en_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_en_id")
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FBudgetDetail" Then
            fobject.pr_budget_code.Text = ds.Tables(0).Rows(_row_gv).Item("bdgt_code")
            fobject._cc_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_cc_id")
            fobject._bdgt_oid = ds.Tables(0).Rows(_row_gv).Item("bdgt_oid")
            fobject._en_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_en_id")
            fobject._year = ds.Tables(0).Rows(_row_gv).Item("bdgt_year_periode")
            fobject._trans_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_trans_id")
            fobject._year = ds.Tables(0).Rows(_row_gv).Item("bdgt_year_periode")
            fobject._active = ds.Tables(0).Rows(_row_gv).Item("bdgt_active")
            fobject.bdgt_cc_id.Text = ds.Tables(0).Rows(_row_gv).Item("cc_desc")

            If ds.Tables(0).Rows(_row_gv).Item("bdgt_trans_id") <> "D" Then
                'fobject.layout_control_add_update.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'fobject.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'fobject.LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                fobject.LayoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                'fobject.layout_control_add_update.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'fobject.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'fobject.LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                fobject.LayoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If

            fobject.load_data_many(True)
            fobject.load_lookup()

        ElseIf fobject.name = "FProjectBudgetMaintenance" Then
            fobject.pr_budget_code.Text = ds.Tables(0).Rows(_row_gv).Item("bdgt_code")
            fobject._cc_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_cc_id")
            fobject._bdgt_oid = ds.Tables(0).Rows(_row_gv).Item("bdgt_oid")
            fobject._en_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_en_id")
            fobject._year = ds.Tables(0).Rows(_row_gv).Item("bdgt_year_periode")
            fobject._trans_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_trans_id")
            'fobject._year = ds.Tables(0).Rows(_row_gv).Item("bdgt_year_periode")
            fobject._active = ds.Tables(0).Rows(_row_gv).Item("bdgt_active")
            fobject.bdgt_pjc_id.Text = ds.Tables(0).Rows(_row_gv).Item("pjc_code")
            fobject._pjc_id = ds.Tables(0).Rows(_row_gv).Item("bdgt_pjc_id")

            If ds.Tables(0).Rows(_row_gv).Item("bdgt_trans_id") <> "D" Then
                'fobject.layout_control_add_update.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'fobject.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'fobject.LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                fobject.LayoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                'fobject.layout_control_add_update.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'fobject.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'fobject.LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                fobject.LayoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            fobject.load_data_many(True)
            fobject.load_lookup()

        ElseIf fobject.name = "FCrossBudget" Then
            fobject._bdgt_oid = ds.Tables(0).Rows(_row_gv).Item("bdgt_oid")
            fobject.cbdgt_bdgt_oid.Text = ds.Tables(0).Rows(_row_gv).Item("bdgt_code")
            fobject.cbdgt_cc_from_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("bdgt_cc_id")
            fobject.cbdgt_cc_to_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("bdgt_cc_id")
            fobject.load_detail(ds.Tables(0).Rows(_row_gv).Item("bdgt_oid").ToString())
        ElseIf fobject.name = "FBudgetAdministrasiHistory" Then
            fobject.bdgth_code.text = ds.Tables(0).Rows(_row_gv).Item("bdgth_code")
            fobject._bdgth_oid = ds.Tables(0).Rows(_row_gv).Item("bdgth_oid")
            fobject.bdgth_rev.text = ds.Tables(0).Rows(_row_gv).Item("bdgth_rev")
            fobject.cc_desc.text = ds.Tables(0).Rows(_row_gv).Item("cc_desc")
            fobject._ds_edit.clear()
        ElseIf fobject.name = "FProjectBudgetHistory" Then
            fobject.bdgth_code.text = ds.Tables(0).Rows(_row_gv).Item("bdgth_code")
            fobject._bdgth_oid = ds.Tables(0).Rows(_row_gv).Item("bdgth_oid")
            fobject.bdgth_rev.text = ds.Tables(0).Rows(_row_gv).Item("bdgth_rev")
            fobject.pjc_desc.text = ds.Tables(0).Rows(_row_gv).Item("pjc_code")
            fobject._ds_edit.clear()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
