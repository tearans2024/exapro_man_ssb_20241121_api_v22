Imports master_new.ModFunction

Public Class FBudgetQtySearch
    Public _row, _cu_id, _cc_id, _en_id As Integer

    Private Sub FBudgetQtySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FBudgetQtyMaintenance" Then
            add_column(gv_master, "Budget Code", "bdgtq_code", DevExpress.Utils.HorzAlignment.Default)
        End If

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FBudgetQtyMaintenance" Then
            get_sequel = "SELECT  " _
                        & "  bdgtq_oid, " _
                        & "  bdgtq_dom_id, " _
                        & "  bdgtq_en_id, " _
                        & "  bdgtq_add_by, " _
                        & "  bdgtq_add_date, " _
                        & "  bdgtq_upd_by, " _
                        & "  bdgtq_upd_date, " _
                        & "  bdgtq_date, " _
                        & "  bdgtq_year, " _
                        & "  bdgtq_remarks, " _
                        & "  bdgtq_trans_id, " _
                        & "  bdgtq_dt, " _
                        & "  bdgtq_tran_id, " _
                        & "  bdgtq_code, " _
                        & "  bdgtq_rev, " _
                        & "  bdgtq_active " _
                        & "FROM  " _
                        & "  public.bdgtq_qty " _
                        & " where bdgtq_code ~~* '%" + Trim(te_search.Text) + "%'" _
                        & " and bdgtq_active = 'Y' " _
                        & "order by bdgtq_code asc "
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

        If fobject.name = "FBudgetQtyMaintenance" Then
            fobject.pr_budget_code.Text = ds.Tables(0).Rows(_row_gv).Item("bdgtq_code")
            fobject._bdgtq_oid = ds.Tables(0).Rows(_row_gv).Item("bdgtq_oid")
            fobject._en_id = ds.Tables(0).Rows(_row_gv).Item("bdgtq_en_id")
            fobject._trans_id = ds.Tables(0).Rows(_row_gv).Item("bdgtq_trans_id")
            fobject._year = ds.Tables(0).Rows(_row_gv).Item("bdgtq_year")
            fobject._active = ds.Tables(0).Rows(_row_gv).Item("bdgtq_active")

            If ds.Tables(0).Rows(_row_gv).Item("bdgtq_trans_id") <> "D" Then
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
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
