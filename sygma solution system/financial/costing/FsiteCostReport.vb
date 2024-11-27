Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FsiteCostReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Dim dt_bantu As DataTable
    Dim func_data As New function_data

    Private Sub FRequisitionReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pr_entity.Properties.DataSource = dt_bantu
        pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pr_entity.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Partnumber Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PT Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PT Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Set Type", "cs_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Set Desc", "cs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Rollup Date", "sct_rollup_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Rollup PS Status", "sct_rollup_ps", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Rollup Routing Status", "sct_rollup_routing", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Total", "sct_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Material Total", "sct_mtl_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Labor Total", "sct_lbr_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Burden Total", "sct_bdn_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Overhead Total", "sct_ovh_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Subcont Total", "sct_sub_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Material Lower Level", "sct_mtl_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Labor Lower Level", "sct_lbr_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Burden Lower Level", "sct_bdn_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Overhead Lower Level", "sct_ovh_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Subcont Lower Level", "sct_sub_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_view1, "User Create", "sct_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "sct_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "User Update", "sct_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "sct_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_view1, "Category Code", "csc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Category Name", "csc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Set Element", "csd_element", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Set Desc", "csd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "This Level Amount", "sctd_tl_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Lower Level Amount", "sctd_ll_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Amount", "sctd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT  " _
                        & "  e.sct_en_id, " _
                        & "  b.en_desc, " _
                        & "  f.si_desc, " _
                        & "  g.pt_code, " _
                        & "  g.pt_desc1, " _
                        & "  g.pt_desc2, " _
                        & "  a.cs_type, case when cs_type='G' then 'Standart'  when cs_type='C' then 'Current' else 'Simulation' end as cs_desc, " _
                        & "  a.cs_methode, " _
                        & "  e.sct_total, " _
                        & "  e.sct_rollup_date, " _
                        & "  e.sct_rollup_ps, " _
                        & "  e.sct_rollup_routing, " _
                        & "  e.sct_mtl_tl, " _
                        & "  e.sct_lbr_tl, " _
                        & "  e.sct_bdn_tl, " _
                        & "  e.sct_ovh_tl, " _
                        & "  e.sct_sub_tl, " _
                        & "  e.sct_mtl_ll, " _
                        & "  e.sct_lbr_ll, " _
                        & "  e.sct_bdn_ll, " _
                        & "  e.sct_ovh_ll, " _
                        & "  e.sct_sub_ll, " _
                        & "  c.csd_element, " _
                        & "  d.csc_name, " _
                        & "  h.sctd_tl_amount, " _
                        & "  h.sctd_ll_amount, " _
                        & "  h.sctd_amount, " _
                        & "  a.cs_desc, " _
                        & "  e.sct_add_by, " _
                        & "  e.sct_add_date, " _
                        & "  e.sct_upd_by, " _
                        & "  e.sct_upd_date " _
                        & "FROM " _
                        & "  public.sctd_det h " _
                        & "  INNER JOIN public.csd_det c ON (c.csd_oid = h.sctd_csd_oid) " _
                        & "  INNER JOIN public.csc_category d ON (c.csd_csc_id = d.csc_id) " _
                        & "  INNER JOIN public.sct_mstr e ON (e.sct_oid = h.sctd_sct_oid) " _
                        & "  INNER JOIN public.cs_mstr a ON (a.cs_id = e.sct_cs_id) " _
                        & "  INNER JOIN public.en_mstr b ON (e.sct_en_id = b.en_id) " _
                        & "  INNER JOIN public.si_mstr f ON (e.sct_si_id = f.si_id) " _
                        & "  INNER JOIN public.pt_mstr g ON (e.sct_pt_id = g.pt_id) " _
                        & " Where sct_en_id=" & pr_entity.EditValue _
                        & "  order by pt_code, cs_type, csc_name "

                        .InitializeCommand()
                        .FillDataSet(ds, "view1")
                        gc_view1.DataSource = ds.Tables("view1")

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
End Class
