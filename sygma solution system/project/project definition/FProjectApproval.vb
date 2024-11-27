Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FProjectApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FProjectApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        gv_os.Columns("status").VisibleIndex = 0

        AddHandler gv_os.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_os.ColumnFilterChanged, AddressOf relation_detail

        AddHandler gv_all.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_all.ColumnFilterChanged, AddressOf relation_detail

        _user_approval = get_user_approval()

        gv_wf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    Private Function get_user_approval() As String
        get_user_approval = "'" + master_new.ClsVar.sNama + "',"
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "select groupnama from tconfusergroup ug " + _
                           " inner join tconfgroup g on g.groupid = ug.groupid " + _
                           " where userid = " + master_new.ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wfs_status")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            get_user_approval = get_user_approval + "'" + ds_bantu.Tables(0).Rows(i).Item("groupnama") + "',"
        Next
        get_user_approval = get_user_approval.Substring(0, Len(get_user_approval) - 1)
        Return get_user_approval
    End Function

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wfs_status())
        le_wfs_status.Properties.DataSource = dt_bantu
        le_wfs_status.Properties.DisplayMember = dt_bantu.Columns("wfs_desc").ToString
        le_wfs_status.Properties.ValueMember = dt_bantu.Columns("wfs_id").ToString
        le_wfs_status.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_os, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sold To", "sold_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Bill To", "bill_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sales Person", "sall_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Project Type", "project_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Order Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Credit Terms", "credit_term", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Currency", "cu_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_os, "prjd_oid", False)
        add_column(gv_os, "prjd_prj_oid", False)
        add_column(gv_os, "prjd_seq", False)
        add_column_copy(gv_os, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_os, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_os, "Qty * Cost", "prjd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_os, "Taxable", "prjd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Include", "prjd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_oid", False)
        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_all, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sold To", "sold_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Bill To", "bill_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sales Person", "sall_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Project Type", "project_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Order Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Credit Terms", "credit_term", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Currency", "cu_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_all, "prjd_oid", False)
        add_column(gv_all, "prjd_prj_oid", False)
        add_column(gv_all, "prjd_seq", False)
        add_column_copy(gv_all, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_all, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_all, "Qty * Cost", "prjd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_all, "Taxable", "prjd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Include", "prjd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            Try
                Using objload As New master_new.CustomCommand
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            Try
                                ds.Tables("os").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  false as status," _
                                & "  prjd_oid, " _
                                & "  prjd_dom_id, " _
                                & "  prjd_en_id, en_desc, " _
                                & "  prjd_add_by, " _
                                & "  prjd_add_date, " _
                                & "  prjd_upd_by, " _
                                & "  prjd_upd_date, " _
                                & "  prjd_dt, " _
                                & "  prjd_prj_oid, " _
                                & "  prj_oid,prj_code,prj_ord_date, " _
                                & "  sold.ptnr_name as sold_ptnr_name, " _
                                & "  bill.ptnr_name as bill_ptnr_name, " _
                                & "  sal.ptnr_name as sall_ptnr_name, " _
                                & "  prj_type.code_name as project_type, " _
                                & "  ct.code_name as credit_term,cu_code, " _
                                & "  prjd_seq, " _
                                & "  prjd_si_id,si_desc, " _
                                & "  prjd_pt_id,pt_code, " _
                                & "  prjd_pt_desc1, " _
                                & "  prjd_pt_desc2, " _
                                & "  prjd_loc_id,loc_code,loc_desc, " _
                                & "  prjd_qty, " _
                                & "  prjd_qty_full, " _
                                & "  prjd_um,um.code_name as unit_measure, " _
                                & "  prjd_cost, " _
                                & "  prjd_price, " _
                                & "  prjd_disc, " _
                                & "  prjd_um_conv, " _
                                & "  prjd_qty_real, " _
                                & "  ((prjd_qty * prjd_cost) - (prjd_qty * prjd_cost * prjd_disc)) as prjd_qty_cost, " _
                                & "  prjd_taxable, " _
                                & "  prjd_tax_inc, " _
                                & "  prjd_tax_class,tclass.code_name as tax_class, " _
                                & "  prjd_trans_id, " _
                                & "  prjd_qty_pao, " _
                                & "  prjd_qty_mo, wf_seq, coalesce(useremail,'') as useremail " _
                                & "FROM  " _
                                & "  public.prjd_det " _
                                & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                                & "  inner join ptnr_mstr sold on sold.ptnr_id = prj_ptnr_id_sold " _
                                & "  inner join ptnr_mstr bill on bill.ptnr_id = prj_ptnr_id_bill " _
                                & "  inner join ptnr_mstr sal on sal.ptnr_id = prj_sales_person_id " _
                                & "  inner join code_mstr prj_type on prj_type.code_id = prj_pjt_code_id " _
                                & "  inner join cu_mstr on cu_id = prj_cu_id " _
                                & "  inner join code_mstr ct on ct.code_id = prj_credit_term " _
                                & "  inner join si_mstr on si_id = prj_si_id " _
                                & "  inner join en_mstr on en_id = prjd_en_id " _
                                & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                                & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                                & "  inner join code_mstr um on um.code_id =  prjd_um " _
                                & "  inner join code_mstr tclass on tclass.code_id = prjd_tax_class " _
                                & "  inner join wf_mstr wm on wf_ref_oid = prjd_oid " _
                                & "  inner join tconfuser on usernama = prj_add_by " _
                                & "  where prjd_en_id in (select user_en_id from tconfuserentity " _
                                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & "  and wf_iscurrent = 'Y' and wf_user_id in (" + _user_approval + ")"

                            '& "  and (prjd_trans_id = 'D' or coalesce(prjd_trans_id,'') = '') " _
                            '& "  and prjd_add_by ~~* " + SetSetring(master_new.ClsVar.sNama) _
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                            gv_os.BestFitColumns()
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  false as status," _
                                & "  prjd_oid, " _
                                & "  prjd_dom_id, " _
                                & "  prjd_en_id, en_desc, " _
                                & "  prjd_add_by, " _
                                & "  prjd_add_date, " _
                                & "  prjd_upd_by, " _
                                & "  prjd_upd_date, " _
                                & "  prjd_dt, " _
                                & "  prjd_prj_oid, " _
                                & "  prj_code,prj_ord_date, " _
                                & "  sold.ptnr_name as sold_ptnr_name, " _
                                & "  bill.ptnr_name as bill_ptnr_name, " _
                                & "  sal.ptnr_name as sall_ptnr_name, " _
                                & "  prj_type.code_name as project_type, " _
                                & "  ct.code_name as credit_term,cu_code, " _
                                & "  prjd_seq, " _
                                & "  prjd_si_id,si_desc, " _
                                & "  prjd_pt_id,pt_code, " _
                                & "  prjd_pt_desc1, " _
                                & "  prjd_pt_desc2, " _
                                & "  prjd_loc_id,loc_code,loc_desc, " _
                                & "  prjd_qty, " _
                                & "  prjd_qty_full, " _
                                & "  prjd_um,um.code_name as unit_measure, " _
                                & "  prjd_cost, " _
                                & "  prjd_price, " _
                                & "  prjd_disc, " _
                                & "  prjd_um_conv, " _
                                & "  prjd_qty_real, " _
                                & "  ((prjd_qty * prjd_cost) - (prjd_qty * prjd_cost * prjd_disc)) as prjd_qty_cost, " _
                                & "  prjd_taxable, " _
                                & "  prjd_tax_inc, " _
                                & "  prjd_tax_class,tclass.code_name as tax_class, " _
                                & "  prjd_trans_id, " _
                                & "  prjd_qty_pao, " _
                                & "  prjd_qty_mo " _
                                & "FROM  " _
                                & "  public.prjd_det " _
                                & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                                & "  inner join ptnr_mstr sold on sold.ptnr_id = prj_ptnr_id_sold " _
                                & "  inner join ptnr_mstr bill on bill.ptnr_id = prj_ptnr_id_bill " _
                                & "  inner join ptnr_mstr sal on sal.ptnr_id = prj_sales_person_id " _
                                & "  inner join code_mstr prj_type on prj_type.code_id = prj_pjt_code_id " _
                                & "  inner join cu_mstr on cu_id = prj_cu_id " _
                                & "  inner join code_mstr ct on ct.code_id = prj_credit_term " _
                                & "  inner join si_mstr on si_id = prj_si_id " _
                                & "  inner join en_mstr on en_id = prjd_en_id " _
                                & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                                & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                                & "  inner join code_mstr um on um.code_id =  prjd_um " _
                                & "  inner join code_mstr tclass on tclass.code_id = prjd_tax_class " _
                                & "  inner join wf_mstr wm on wf_ref_oid = prjd_oid " _
                                & "  inner join tconfuser on usernama = prj_add_by " _
                                & "  where prjd_en_id in (select user_en_id from tconfuserentity " _
                                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and prj_ord_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime)
                            .InitializeCommand()
                            .FillDataSet(ds, "all")
                            gc_all.DataSource = ds.Tables("all")
                        End If

                        bestfit_column()
                        load_data_grid_detail()
                        relation_detail()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    'Public Overrides Sub load_data_grid_detail()
    '    If xtc_master.SelectedTabPageIndex = 0 Then
    '        If ds.Tables("os").Rows.Count = 0 Then
    '            Exit Sub
    '        End If
    '    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
    '        If ds.Tables("all").Rows.Count = 0 Then
    '            Exit Sub
    '        End If
    '    End If

    'End Sub

    Public Overrides Sub load_data_grid_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            If ds.Tables("all").Rows.Count = 0 Then
                Exit Sub
            End If
        End If

        ds_detail = New DataSet
        Try
            Using objload As New master_new.CustomCommand
                With objload
                    If xtc_master.SelectedTabPageIndex = 0 Then
                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join prjd_det dt on dt.prjd_oid = wf_ref_oid " + _
                              " inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & " where wf_ref_oid in (select prjd_oid from prjd_det inner join wf_mstr on wf_ref_oid = prjd_oid " _
                                                  & " where prjd_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join prjd_det dt on dt.prjd_oid = wf_ref_oid " + _
                              " inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & " where wf_ref_oid in (select prjd_oid from prjd_det inner join wf_mstr on wf_ref_oid = prjd_oid " _
                                                  & " where prjd_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                            & " and reqs_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and reqs_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        gv_wf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    Public Overrides Sub relation_detail()
        'MessageBox.Show(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("prjd_oid").ToString)
        If xtc_master.SelectedTabPageIndex = 0 Then
            Try
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("prjd_oid").ToString & "'")

                gv_wf.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("prjd_oid").ToString & "'")
                gv_wf.BestFitColumns()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub xtc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xtc_master.Click
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        End If
    End Sub

    Private Sub sb_process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_process.Click
        Dim i, j As Integer
        j = 0

        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables.Count = 0 Then
                Exit Sub
            ElseIf ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If

            For i = 0 To ds.Tables("os").Rows.Count - 1
                If ds.Tables("os").Rows(i).Item("status") = True Then
                    j += 1
                End If
            Next

            If j = 0 Then
                MessageBox.Show("Please Select Data, First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        'If le_wfs_status.EditValue = 1 And xtc_master.SelectedTabPageIndex = 0 Then
        '    approve()
        'ElseIf le_wfs_status.EditValue = 3 And xtc_master.SelectedTabPageIndex = 1 Then
        '    If ds.Tables("all").Rows.Count = 0 Then
        '        Exit Sub
        '    End If


        'End If

        Dim _colom, _table, _initial, _type, _title As String
        _colom = "prjd_trans_id"
        _table = "prjd_det"
        _initial = "prjd"
        _type = "prjd"
        _title = "Project Definition"

        If le_wfs_status.EditValue = 1 And xtc_master.SelectedTabPageIndex = 0 Then
            approve_wf(_colom, _table, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, gv_os, _title)
        ElseIf le_wfs_status.EditValue = 2 And xtc_master.SelectedTabPageIndex = 0 Then
            hold_wf(_initial, le_wfs_status.EditValue, Trim(te_remark.Text), nud_hold_day.Value, _user_approval, ds)
        ElseIf le_wfs_status.EditValue = 3 And xtc_master.SelectedTabPageIndex = 0 Then
            cancel_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        ElseIf le_wfs_status.EditValue = 4 And xtc_master.SelectedTabPageIndex = 0 Then
            rollback_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        End If
    End Sub

    Private Sub le_wfs_status_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles le_wfs_status.EditValueChanged
        If le_wfs_status.EditValue = 2 Then
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub approve()
        Dim _max_seq As Integer
        Dim i As Integer
        If MessageBox.Show("Approve All Selection Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("os").AcceptChanges()

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        For i = 0 To ds.Tables("os").Rows.Count - 1
                            If ds.Tables("os").Rows(i).Item("status") = True Then
                                '.Command = .Connection.CreateCommand
                                '.Command.Transaction = sqlTran

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '1'," + _
                                           " wf_aprv_date = current_timestamp, " + _
                                           " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
                                           " wf_desc = '" + Trim(te_remark.Text) + "', " + _
                                           " wf_iscurrent = 'N'," + _
                                           " wf_dt = current_timestamp, " + _
                                           " wf_date_to = null " + _
                                           " where wf_ref_oid = '" + ds.Tables("os").Rows(i).Item("prjd_oid") + "'" + _
                                           " and wf_user_id in (" + _user_approval + ")"
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_oid = '" + ds.Tables("os").Rows(i).Item("prjd_oid") + "'" + _
                                                       " and wf_seq = " + (ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_trans_id = 'I', " + _
                                                    " prjd_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                                    " prjd_upd_date = current_timestamp " + _
                                                    " where prjd_oid = '" + ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                _max_seq = get_max_seq(ds.Tables("os").Rows(i).Item("prjd_oid"))
                                If _max_seq = ds.Tables("os").Rows(i).Item("wf_seq") Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update prjd_det set prjd_trans_id = 'I'" + _
                                                   " where prjd_oid = '" + ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update prj_mstr set prj_trans_id = 'I'" + _
                                                   " where prj_code = '" + ds.Tables("os").Rows(i).Item("prj_code") + "'"
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                End If

                            End If
                        Next

                        .Command.Commit()
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        Exit Sub
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        load_data_many(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function get_status_wf(ByVal par_ref_oid As String) As String
        get_status_wf = "-1"
        Try
            Using objkalendar As New master_new.CustomCommand
                With objkalendar
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_wfs_id from wf_mstr  " + _
                                           " where wf_ref_oid = '" + par_ref_oid + "'" + _
                                           " and wf_seq = 0"
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        get_status_wf = .DataReader.Item("wf_wfs_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_status_wf
    End Function

    Public Function CekStatusRollBack(ByVal par_ref_oid As String) As Boolean
        CekStatusRollBack = False
        Try
            Using objkalendar As New master_new.CustomCommand
                With objkalendar
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_wfs_id from wf_mstr  " + _
                                           " where wf_ref_oid = '" + par_ref_oid + "'" + _
                                           " and wf_wfs_id = '4' "
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        CekStatusRollBack = True '.DataReader.Item("wf_wfs_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return CekStatusRollBack
    End Function

    Public Overrides Sub approve_wf(ByVal par_colom As String, ByVal par_table As String, ByVal par_initial As String, ByVal par_type As String, _
                                   ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_user_approval As String, _
                                   ByVal par_ds As DataSet, ByVal par_gv As Object, ByVal par_title As String)
        If MessageBox.Show("Approve Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim wf_seq, i, _max_seq As Integer
        Dim filename, user_wf, user_wf_email, format_email_bantu As String

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then

                                'Try
                                '    par_gv.Columns(par_initial + "_oid").FilterInfo = _
                                '    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" + par_initial + "_oid] = '" & ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString & "'")
                                '    par_gv.BestFitColumns()

                                '    'par_gv.Columns(par_initial + "_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(i).Item(par_initial + "_oid"))
                                'Catch ex As Exception
                                'End Try

                                If get_status_wf(par_ds.Tables("os").Rows(i).Item("prjd_oid").ToString()) = 0 Then
                                    If CekStatusRollBack(par_ds.Tables("os").Rows(i).Item("prjd_oid").ToString()) = True Then 'Jika ada user approval yg nge rollback
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update wf_mstr set " + _
                                                               " wf_iscurrent = 'Y', " + _
                                                               " wf_wfs_id = '0', " + _
                                                               " wf_desc = '', " + _
                                                               " wf_date_to = null, " + _
                                                               " wf_aprv_user = '', " + _
                                                               " wf_aprv_date = null " + _
                                                               " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid").ToString() + "'" + _
                                                               " and wf_wfs_id = '4' "
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        '=======================================================
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                   " wf_aprv_date = current_timestamp, " + _
                                                   " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
                                                   " wf_desc = '" + Trim(par_desc) + "', " + _
                                                   " wf_iscurrent = 'N'," + _
                                                   " wf_dt = current_timestamp, " + _
                                                   " wf_date_to = null " + _
                                                   " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'" + _
                                                   " and wf_user_id in (" + par_user_approval + ")"
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update prjd_det set prjd_trans_id = 'W'" + _
                                                       " where prjd_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update prj_mstr set prj_trans_id = 'W'" + _
                                                       " where prj_code = '" + par_ds.Tables("os").Rows(i).Item("prj_code") + "'"
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                    Else
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                   " wf_aprv_date = current_timestamp, " + _
                                                   " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
                                                   " wf_desc = '" + Trim(par_desc) + "', " + _
                                                   " wf_iscurrent = 'N'," + _
                                                   " wf_dt = current_timestamp, " + _
                                                   " wf_date_to = null " + _
                                                   " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'" + _
                                                   " and wf_user_id in (" + par_user_approval + ")"
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                               " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'" + _
                                                               " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update prjd_det set prjd_trans_id = 'W'" + _
                                                       " where prjd_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update prj_mstr set prj_trans_id = 'W'" + _
                                                       " where prj_code = '" + par_ds.Tables("os").Rows(i).Item("prj_code") + "'"
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()
                                    End If

                                ElseIf get_status_wf(par_ds.Tables("os").Rows(i).Item("prjd_oid").ToString()) > 0 Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                               " wf_aprv_date = current_timestamp, " + _
                                               " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
                                               " wf_desc = '" + Trim(par_desc) + "', " + _
                                               " wf_iscurrent = 'N'," + _
                                               " wf_dt = current_timestamp, " + _
                                               " wf_date_to = null " + _
                                               " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'" + _
                                               " and wf_user_id in (" + par_user_approval + ")"
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                           " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'" + _
                                                           " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update prjd_det set prjd_trans_id = 'W'" + _
                                                   " where prjd_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update prj_mstr set prj_trans_id = 'W'" + _
                                                   " where prj_code = '" + par_ds.Tables("os").Rows(i).Item("prj_code") + "'"
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    'ElseIf get_status_wf(par_ds.Tables("os").Rows(i).Item("prjd_oid").ToString()) = 4 Then
                                    '    '.Command.CommandType = CommandType.Text
                                    '    .Command.CommandText = "update wf_mstr set " + _
                                    '                           " wf_iscurrent = 'Y', " + _
                                    '                           " wf_wfs_id = '0', " + _
                                    '                           " wf_desc = '', " + _
                                    '                           " wf_date_to = null, " + _
                                    '                           " wf_aprv_user = '', " + _
                                    '                           " wf_aprv_date = null " + _
                                    '                           " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid").ToString() + "'" + _
                                    '                           " and wf_wfs_id = '4' "
                                    '    .Command.ExecuteNonQuery()
                                    '    '.Command.Parameters.Clear()
                                End If

                                _max_seq = get_max_seq(par_ds.Tables("os").Rows(i).Item("prjd_oid"))
                                If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update prjd_det set prjd_trans_id = 'I'" + _
                                                   " where prjd_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update prj_mstr set prj_trans_id = 'I'" + _
                                                   " where prj_code = '" + par_ds.Tables("os").Rows(i).Item("prj_code") + "'"
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                End If

                                wf_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") ' + 1
                                'MessageBox.Show(par_ds.Tables("os").Rows(i).Item("prjd_oid").ToString())
                                user_wf = get_user_wf(par_ds.Tables("os").Rows(i).Item("prjd_oid"), wf_seq)
                                user_wf_email = mf.get_email_address(user_wf)

                                If user_wf_email <> "" Then
                                    filename = "c:\syspro\" + par_ds.Tables("os").Rows(i).Item("prj_code") + ".xls"
                                    ExportTo(par_gv, New ExportXlsProvider(filename))

                                    format_email_bantu = mf.format_email(user_wf, par_ds.Tables("os").Rows(i).Item("prj_code"), par_type)
                                    mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item("prj_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                End If
                            End If
                        Next

                        .Command.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function get_max_seq(ByVal par_code As String) As Integer
        get_max_seq = -1
        Try
            Using objkalendar As New master_new.CustomCommand
                With objkalendar
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select max(wf_seq) as max_seq from wf_mstr where wf_ref_oid = '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        get_max_seq = .DataReader.Item("max_seq")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_max_seq
    End Function

    Public Function get_user_wf(ByVal par_code As String, ByVal par_wf_seq As Integer) As String
        get_user_wf = ""
        Try
            Using objkalendar As New master_new.CustomCommand
                With objkalendar
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_user_id from wf_mstr  " + _
                                           " where wf_ref_oid = '" + par_code + "'" + _
                                           " and wf_seq = " + par_wf_seq.ToString
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        get_user_wf = .DataReader.Item("wf_user_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_user_wf
    End Function

    Public Overrides Sub cancel_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Cancel Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim i As Integer

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran
                        '.Command.CommandType = CommandType.Text

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then

                                '.Command = .Connection.CreateCommand
                                '.Command.Transaction = sqlTran
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                       " wf_aprv_date = current_timestamp, " + _
                                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                                       " wf_desc = '" + Trim(par_desc) + "'," + _
                                                       " wf_iscurrent = 'N'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and wf_user_id in (" + par_user_approval + ")" + _
                                                       " and wf_iscurrent ~~* 'Y'"

                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid") + "'"

                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_trans_id = 'X' " + _
                                                       " where prjd_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Cancel Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
                        Next

                        .Command.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Canceled..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub rollback_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Rollback Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim i As Integer
        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran
                        '.Command.CommandType = CommandType.Text

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then
                                '.Command = .Connection.CreateCommand
                                '.Command.Transaction = sqlTran
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                       " wf_aprv_date = current_timestamp, " + _
                                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                                       " wf_desc = '" + Trim(par_desc) + "'," + _
                                                       " wf_iscurrent = 'N'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid") + "'" + _
                                                       " and wf_user_id in (" + par_user_approval + ")" + _
                                                       " and wf_iscurrent ~~* 'Y'"
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                'ini update untuk seq 0 is currentnya jadi Y (artinya hrus di approve lagi sma orng yg bikinyanya)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '0', " + _
                                                       " wf_aprv_date = null, " + _
                                                       " wf_aprv_user = ''," + _
                                                       " wf_desc = ''," + _
                                                       " wf_iscurrent = 'Y'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid") + "'" + _
                                                       " and wf_seq = 0 "
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'D' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid") + "'"
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_trans_id = 'D' " + _
                                                       " where prjd_oid = '" + par_ds.Tables("os").Rows(i).Item("prjd_oid") + "'"
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item("prj_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item("prj_code") + " Rollback Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
                        Next

                        .Command.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Rollbacked..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function get_prjd_trans_id(ByVal par_oid As String) As String
        get_prjd_trans_id = ""

        Try
            Using objkalendar As New master_new.CustomCommand
                With objkalendar
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select prjd_trans_id from prjd_det  " + _
                                           " where prjd_oid = '" + par_oid + "'"
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        get_prjd_trans_id = .DataReader.Item("prjd_trans_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_prjd_trans_id
    End Function

    Private Function get_user_wf_local(ByVal par_oid As String, ByVal par_wf_seq As Integer) As String
        get_user_wf_local = ""
        Try
            Using objkalendar As New master_new.CustomCommand
                With objkalendar
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_user_id from wf_mstr  " + _
                                           " where wf_ref_oid = '" + par_oid + "'" + _
                                           " and wf_seq = " + par_wf_seq.ToString
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        get_user_wf_local = .DataReader.Item("wf_user_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_user_wf_local
    End Function

End Class
