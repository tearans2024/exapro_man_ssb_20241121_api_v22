Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FRequisitionApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FRequisitionApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        add_column_copy(gv_os, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Need Date", "req_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Due Date", "req_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Type", "req_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Close Date", "req_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_os, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Need Date", "req_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Due Date", "req_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Type", "req_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Close Date", "req_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_all, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "reqd_oid", False)
        add_column(gv_detail, "reqd_req_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Code Relation", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Komplit", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Discount", "reqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_detail, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "req_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_email, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_email, "reqd_req_oid", False)
        add_column_copy(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                'ds = New DataSet
                Using objload As New master_new.CustomCommand
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            Try
                                ds.Tables("os").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  false as status, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.req_mstr.req_oid, " _
                                & "  public.req_mstr.req_dom_id, " _
                                & "  public.req_mstr.req_en_id, " _
                                & "  public.req_mstr.req_upd_date, " _
                                & "  public.req_mstr.req_upd_by, " _
                                & "  public.req_mstr.req_add_date, " _
                                & "  public.req_mstr.req_add_by, " _
                                & "  public.req_mstr.req_code, " _
                                & "  public.req_mstr.req_ptnr_id, " _
                                & "  public.req_mstr.req_cmaddr_id, " _
                                & "  public.req_mstr.req_date, " _
                                & "  public.req_mstr.req_need_date, " _
                                & "  public.req_mstr.req_due_date, " _
                                & "  public.req_mstr.req_requested, " _
                                & "  public.req_mstr.req_end_user, " _
                                & "  public.req_mstr.req_rmks, " _
                                & "  public.req_mstr.req_sb_id, " _
                                & "  public.req_mstr.req_cc_id, " _
                                & "  public.req_mstr.req_si_id, " _
                                & "  public.req_mstr.req_type, " _
                                & "  public.req_mstr.req_pjc_id, " _
                                & "  public.req_mstr.req_close_date, " _
                                & "  public.req_mstr.req_total, " _
                                & "  public.req_mstr.req_tran_id, " _
                                & "  public.req_mstr.req_trans_id, " _
                                & "  public.req_mstr.req_trans_rmks, " _
                                & "  public.req_mstr.req_current_route, " _
                                & "  public.req_mstr.req_next_route, " _
                                & "  public.req_mstr.req_dt, " _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  cmaddr_name, " _
                                & "  tran_name, " _
                                & "  public.pjc_mstr.pjc_code, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.cc_mstr.cc_desc, wf_seq, coalesce(useremail,'') as useremail " _
                                & "FROM " _
                                & "  public.req_mstr " _
                                & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                                & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                                & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                                & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                                & " inner join wf_mstr wm on wf_ref_oid = req_oid " _
                                & " inner join tconfuser on usernama = req_add_by " _
                                & " where req_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and wf_iscurrent ~~* 'Y'"
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.req_mstr.req_oid, " _
                                & "  public.req_mstr.req_dom_id, " _
                                & "  public.req_mstr.req_en_id, " _
                                & "  public.req_mstr.req_upd_date, " _
                                & "  public.req_mstr.req_upd_by, " _
                                & "  public.req_mstr.req_add_date, " _
                                & "  public.req_mstr.req_add_by, " _
                                & "  public.req_mstr.req_code, " _
                                & "  public.req_mstr.req_ptnr_id, " _
                                & "  public.req_mstr.req_cmaddr_id, " _
                                & "  public.req_mstr.req_date, " _
                                & "  public.req_mstr.req_need_date, " _
                                & "  public.req_mstr.req_due_date, " _
                                & "  public.req_mstr.req_requested, " _
                                & "  public.req_mstr.req_end_user, " _
                                & "  public.req_mstr.req_rmks, " _
                                & "  public.req_mstr.req_sb_id, " _
                                & "  public.req_mstr.req_cc_id, " _
                                & "  public.req_mstr.req_si_id, " _
                                & "  public.req_mstr.req_type, " _
                                & "  public.req_mstr.req_pjc_id, " _
                                & "  public.req_mstr.req_close_date, " _
                                & "  public.req_mstr.req_total, " _
                                & "  public.req_mstr.req_tran_id, " _
                                & "  public.req_mstr.req_trans_id, " _
                                & "  public.req_mstr.req_trans_rmks, " _
                                & "  public.req_mstr.req_current_route, " _
                                & "  public.req_mstr.req_next_route, " _
                                & "  public.req_mstr.req_dt, " _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  cmaddr_name, " _
                                & "  tran_name, " _
                                & "  public.pjc_mstr.pjc_code, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.cc_mstr.cc_desc " _
                                & "FROM " _
                                & "  public.req_mstr " _
                                & " inner join wf_mstr wm on wf_ref_oid = req_oid " _
                                & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                                & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                                & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                                & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                                & " where req_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and req_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and req_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and req_trans_id <> 'D' "
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
                        .SQL = "SELECT  " _
                        & "  public.reqd_det.reqd_oid, " _
                        & "  public.reqd_det.reqd_dom_id, " _
                        & "  public.reqd_det.reqd_en_id, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.reqd_det.reqd_add_by, " _
                        & "  public.reqd_det.reqd_add_date, " _
                        & "  public.reqd_det.reqd_upd_by, " _
                        & "  public.reqd_det.reqd_upd_date, " _
                        & "  public.reqd_det.reqd_req_oid, " _
                        & "  public.reqd_det.reqd_seq, " _
                        & "  public.reqd_det.reqd_related_oid, " _
                        & "  req_mstr_relation.req_code as req_code_relation, " _
                        & "  public.reqd_det.reqd_ptnr_id, " _
                        & "  public.ptnr_mstr.ptnr_name, " _
                        & "  public.reqd_det.reqd_si_id, " _
                        & "  public.si_mstr.si_desc, " _
                        & "  public.reqd_det.reqd_pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2, " _
                        & "  public.reqd_det.reqd_rmks, " _
                        & "  public.reqd_det.reqd_end_user, " _
                        & "  public.reqd_det.reqd_qty, " _
                        & "  public.reqd_det.reqd_qty_processed, " _
                        & "  public.reqd_det.reqd_qty_completed, " _
                        & "  public.reqd_det.reqd_um, " _
                        & "  public.code_mstr.code_name, " _
                        & "  public.reqd_det.reqd_cost, " _
                        & "  public.reqd_det.reqd_disc, " _
                        & "  public.reqd_det.reqd_need_date, " _
                        & "  public.reqd_det.reqd_due_date, " _
                        & "  public.reqd_det.reqd_um_conv, " _
                        & "  public.reqd_det.reqd_qty_real, " _
                        & "  public.reqd_det.reqd_pt_class, " _
                        & "  public.reqd_det.reqd_status, " _
                        & "  public.reqd_det.reqd_dt, " _
                        & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
                        & "  FROM " _
                        & "  public.reqd_det " _
                        & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
                        & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id)               " _
                        & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                        & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
                        & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid " _
                        & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join req_mstr on req_code = wf_ref_code " + _
                              " inner join reqd_det dt on dt.reqd_req_oid = req_oid " _
                            & " where wf_ref_code in (select req_code from req_mstr inner join wf_mstr on wf_ref_code = req_code " _
                                                  & " where req_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.req_mstr.req_oid, " _
                            & "  public.req_mstr.req_upd_date, " _
                            & "  public.req_mstr.req_upd_by, " _
                            & "  public.req_mstr.req_add_date, " _
                            & "  public.req_mstr.req_add_by, " _
                            & "  public.req_mstr.req_code, " _
                            & "  public.req_mstr.req_date, " _
                            & "  public.req_mstr.req_requested, " _
                            & "  public.req_mstr.req_rmks, " _
                            & "  public.req_mstr.req_pjc_id, " _
                            & "  public.req_mstr.req_total, " _
                            & "  public.req_mstr.req_trans_rmks, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  cmaddr_name, " _
                            & "  tran_name, " _
                            & "  public.pjc_mstr.pjc_code, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.sb_mstr.sb_desc, " _
                            & "  public.cc_mstr.cc_desc, " _
                            & "  public.reqd_det.reqd_oid, " _
                            & "  public.reqd_det.reqd_req_oid, " _
                            & "  public.reqd_det.reqd_seq, " _
                            & "  public.reqd_det.reqd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.reqd_det.reqd_rmks, " _
                            & "  public.reqd_det.reqd_end_user, " _
                            & "  public.reqd_det.reqd_qty, " _
                            & "  public.reqd_det.reqd_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.reqd_det.reqd_due_date, " _
                            & "  public.reqd_det.reqd_um_conv, " _
                            & "  public.reqd_det.reqd_qty_real, " _
                            & "  public.reqd_det.reqd_pt_class, " _
                            & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
                            & "FROM " _
                            & "  public.req_mstr " _
                            & "  INNER JOIN public.reqd_det ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
                            & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                            & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                            & " inner join wf_mstr wm on wf_ref_oid = req_oid " _
                            & " where req_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        .SQL = "SELECT  " _
                            & "  public.reqd_det.reqd_oid, " _
                            & "  public.reqd_det.reqd_dom_id, " _
                            & "  public.reqd_det.reqd_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.reqd_det.reqd_add_by, " _
                            & "  public.reqd_det.reqd_add_date, " _
                            & "  public.reqd_det.reqd_upd_by, " _
                            & "  public.reqd_det.reqd_upd_date, " _
                            & "  public.reqd_det.reqd_req_oid, " _
                            & "  public.reqd_det.reqd_seq, " _
                            & "  public.reqd_det.reqd_related_oid, " _
                            & "  req_mstr_relation.req_code as req_code_relation, " _
                            & "  public.reqd_det.reqd_ptnr_id, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.reqd_det.reqd_si_id, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.reqd_det.reqd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.reqd_det.reqd_rmks, " _
                            & "  public.reqd_det.reqd_end_user, " _
                            & "  public.reqd_det.reqd_qty, " _
                            & "  public.reqd_det.reqd_qty_processed, " _
                            & "  public.reqd_det.reqd_qty_completed, " _
                            & "  public.reqd_det.reqd_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.reqd_det.reqd_cost, " _
                            & "  public.reqd_det.reqd_disc, " _
                            & "  public.reqd_det.reqd_need_date, " _
                            & "  public.reqd_det.reqd_due_date, " _
                            & "  public.reqd_det.reqd_um_conv, " _
                            & "  public.reqd_det.reqd_qty_real, " _
                            & "  public.reqd_det.reqd_pt_class, " _
                            & "  public.reqd_det.reqd_status, " _
                            & "  public.reqd_det.reqd_dt, " _
                            & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
                            & "  FROM " _
                            & "  public.reqd_det " _
                            & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id)               " _
                            & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
                            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid               " _
                            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join req_mstr on req_code = wf_ref_code " + _
                              " inner join reqd_det dt on dt.reqd_req_oid = req_oid " _
                            & " where wf_ref_code in (select req_code from req_mstr inner join wf_mstr on wf_ref_code = req_code " _
                                                  & " where req_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and req_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and req_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.req_mstr.req_oid, " _
                            & "  public.req_mstr.req_upd_date, " _
                            & "  public.req_mstr.req_upd_by, " _
                            & "  public.req_mstr.req_add_date, " _
                            & "  public.req_mstr.req_add_by, " _
                            & "  public.req_mstr.req_code, " _
                            & "  public.req_mstr.req_date, " _
                            & "  public.req_mstr.req_requested, " _
                            & "  public.req_mstr.req_rmks, " _
                            & "  public.req_mstr.req_pjc_id, " _
                            & "  public.req_mstr.req_total, " _
                            & "  public.req_mstr.req_trans_rmks, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  cmaddr_name, " _
                            & "  tran_name, " _
                            & "  public.pjc_mstr.pjc_code, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.sb_mstr.sb_desc, " _
                            & "  public.cc_mstr.cc_desc, " _
                            & "  public.reqd_det.reqd_oid, " _
                            & "  public.reqd_det.reqd_req_oid, " _
                            & "  public.reqd_det.reqd_seq, " _
                            & "  public.reqd_det.reqd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.reqd_det.reqd_rmks, " _
                            & "  public.reqd_det.reqd_end_user, " _
                            & "  public.reqd_det.reqd_qty, " _
                            & "  public.reqd_det.reqd_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.reqd_det.reqd_due_date, " _
                            & "  public.reqd_det.reqd_um_conv, " _
                            & "  public.reqd_det.reqd_qty_real, " _
                            & "  public.reqd_det.reqd_pt_class, " _
                            & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
                            & "FROM " _
                            & "  public.req_mstr " _
                            & "  INNER JOIN public.reqd_det ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
                            & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                            & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                            & " inner join wf_mstr wm on wf_ref_oid = req_oid " _
                            & " where req_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and req_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and req_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub relation_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            Try
                gv_detail.Columns("reqd_req_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqd_req_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("req_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("reqs_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("req_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("req_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("req_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_detail.Columns("reqd_req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("req_oid"))
                'gv_detail.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("req_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("req_oid"))
                'gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail.Columns("reqd_req_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqd_req_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("req_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("reqs_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("req_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("req_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("req_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_detail.Columns("reqd_req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("req_oid"))
                'gv_detail.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("req_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("req_oid"))
                'gv_email.BestFitColumns()
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

        Dim _colom, _table, _initial, _type, _title As String

        _colom = "req_trans_id"
        _table = "req_mstr"
        _initial = "req"
        _type = "pr"
        _title = "Requisition"

        If le_wfs_status.EditValue = 1 And xtc_master.SelectedTabPageIndex = 0 Then
            approve_wf(_colom, _table, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, gv_email, _title)
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
End Class
