Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FRequisition

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _req_oid_mstr As String = ""
    Public __ptnr_id As String = ""
    Public __end_ptnr_id As String = ""
    Public ds_edit As DataSet
    Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _reqd_related_oid As String = ""
    Dim _conf_value As String
    Dim _now As Date
    Dim mf As New master_new.ModFunction

    Private Sub FRequisition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_requisition")
        form_first_load()
        _now = func_coll.get_tanggal_sistem
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        If _conf_value = "0" Then
            xtc_detail.TabPages(1).PageVisible = False
            xtc_detail.TabPages(3).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(1).PageVisible = True
            xtc_detail.TabPages(3).PageVisible = True
        End If

        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        req_en_id.Properties.DataSource = dt_bantu
        req_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        req_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        req_en_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            req_tran_id.Properties.DataSource = dt_bantu
            req_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            req_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            req_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            req_tran_id.Properties.DataSource = dt_bantu
            req_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            req_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            req_tran_id.ItemIndex = 0
        End If
        
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", req_en_id.EditValue))
        req_ptnr_id.Properties.DataSource = dt_bantu
        req_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        req_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        req_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cmaddr_mstr", req_en_id.EditValue))
        req_cmaddr_id.Properties.DataSource = dt_bantu
        req_cmaddr_id.Properties.DisplayMember = dt_bantu.Columns("cmaddr_name").ToString
        req_cmaddr_id.Properties.ValueMember = dt_bantu.Columns("cmaddr_id").ToString
        req_cmaddr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("sb_mstr", req_en_id.EditValue))
        req_sb_id.Properties.DataSource = dt_bantu
        req_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        req_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        req_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cc_mstr", req_en_id.EditValue))
        req_cc_id.Properties.DataSource = dt_bantu
        req_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        req_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        req_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", req_en_id.EditValue))
        req_si_id.Properties.DataSource = dt_bantu
        req_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        req_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        req_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pjc_mstr", req_en_id.EditValue))
        req_pjc_id.Properties.DataSource = dt_bantu
        req_pjc_id.Properties.DisplayMember = dt_bantu.Columns("pjc_desc").ToString
        req_pjc_id.Properties.ValueMember = dt_bantu.Columns("pjc_id").ToString
        req_pjc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(req_en_id.EditValue))
        req_bk_id.Properties.DataSource = dt_bantu
        req_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        req_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        req_bk_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("bk_mstr", req_en_id.EditValue))
        'req_bk_id.Properties.DataSource = dt_bantu
        'req_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        'req_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        'req_bk_id.ItemIndex = 0

    End Sub

    Private Sub req_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles req_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "req_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Need Date", "req_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Due Date", "req_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Requested", "req_requested_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End User", "req_end_user_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "req_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Close Date", "req_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n4}")
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "req_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

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
        add_column_copy(gv_detail, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_detail, "reqd_cost", False) 'cost
        add_column(gv_detail, "reqd_disc", False) 'diskon
        add_column_copy(gv_detail, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_detail, "reqd_qty_cost", False) 'qty * cost
        add_column_copy(gv_detail, "Total Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
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
        add_column_copy(gv_email, "Description1", "reqd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "reqd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_email, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")

        add_column_copy(gv_smart, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "reqd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_related_oid", False)
        add_column(gv_edit, "Code Relation", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_ptnr_id", False)
        add_column(gv_edit, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "reqd_ptnr_id", False)
        add_column_edit(gv_edit, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "reqd_qty_processed", False)
        add_column(gv_edit, "reqd_um", False)
        add_column(gv_edit, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "reqd_cost", False) 'cost
        add_column_edit(gv_edit, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_edit(gv_edit, "Total Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_edit, "reqd_disc", False) 'Diskon
        'add_column_edit(gv_edit, "Discount", "reqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_edit(gv_edit, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_edit, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_edit, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column(gv_edit, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column(gv_edit, "reqd_qty_cost", False) 'Qty * Cost
    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT " _
                    & " public.en_mstr.en_desc, " _
                    & " public.req_mstr.req_oid, " _
                    & " public.req_mstr.req_dom_id, " _
                    & " public.req_mstr.req_en_id, " _
                    & " public.req_mstr.req_upd_date, " _
                    & " public.req_mstr.req_upd_by, " _
                    & " public.req_mstr.req_add_date, " _
                    & " public.req_mstr.req_add_by, " _
                    & " public.req_mstr.req_code, " _
                    & " public.req_mstr.req_ptnr_id, " _
                    & " req_vendor.ptnr_name AS req_ptnr_name, " _
                    & " public.req_mstr.req_cmaddr_id, " _
                    & " public.req_mstr.req_date, " _
                    & " public.req_mstr.req_need_date, " _
                    & " public.req_mstr.req_due_date, " _
                    & " public.req_mstr.req_requested_ptnr_id, " _
                    & " req_requester.ptnr_name AS req_requested_ptnr_name, " _
                    & " public.req_mstr.req_requested, " _
                    & " public.req_mstr.req_end_user_ptnr_id, " _
                    & " req_user.ptnr_name AS req_end_user_ptnr_name, " _
                    & " public.req_mstr.req_end_user, " _
                    & " public.req_mstr.req_rmks, " _
                    & "  public.req_mstr.req_bk_id, " _
                    & "  public.bk_mstr.bk_name, " _
                    & " public.req_mstr.req_sb_id, " _
                    & " public.req_mstr.req_cc_id, " _
                    & " public.req_mstr.req_si_id, " _
                    & " public.req_mstr.req_type, " _
                    & " public.req_mstr.req_pjc_id, " _
                    & " public.req_mstr.req_close_date, " _
                    & " public.req_mstr.req_total, " _
                    & " public.req_mstr.req_tran_id, " _
                    & " public.req_mstr.req_trans_id, " _
                    & " public.req_mstr.req_trans_rmks, " _
                    & " public.req_mstr.req_current_route, " _
                    & " public.req_mstr.req_next_route, " _
                    & " public.req_mstr.req_dt, " _
                    & " cmaddr_name, " _
                    & " tran_name, " _
                    & " public.pjc_mstr.pjc_code, " _
                    & " public.si_mstr.si_desc, " _
                    & " public.sb_mstr.sb_desc, " _
                    & " public.cc_mstr.cc_desc " _
                    & " FROM public.req_mstr " _
                    & " INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                    & " INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & " INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                    & " INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                    & " INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                    & " INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                    & " LEFT OUTER JOIN ptnr_mstr req_vendor ON req_vendor.ptnr_id = req_ptnr_id " _
                    & " LEFT OUTER JOIN ptnr_mstr req_requester ON req_requester.ptnr_id = req_requested_ptnr_id " _
                    & " LEFT OUTER JOIN ptnr_mstr req_user ON req_user.ptnr_id = req_end_user_ptnr_id " _
                    & " LEFT OUTER JOIN public.bk_mstr ON (public.req_mstr.req_bk_id = public.bk_mstr.bk_id) " _
                    & " left outer join public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id)" _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and req_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        If TxtPRDetail.Text.Length > 0 Then
            get_sequel = "SELECT distinct " _
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
                    & "  public.req_mstr.req_requested_ptnr_id, " _
                    & "  public.req_mstr.req_requested, " _
                    & "  public.req_mstr.req_end_user_ptnr_id, " _
                    & "  public.req_mstr.req_end_user, " _
                    & "  public.req_mstr.req_rmks, " _
                    & "  public.req_mstr.req_bk_id, " _
                    & "  public.bk_mstr.bk_name, " _
                    & "  public.req_mstr.req_sb_id, " _
                    & "  public.req_mstr.req_cc_id, " _
                    & "  public.req_mstr.req_si_id, " _
                    & "  public.req_mstr.req_type, " _
                    & "  public.req_mstr.req_is_memo, " _
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
                    & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                    & "  INNER JOIN public.reqd_det ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
                    & "  left outer join public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                    & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid  " _
                    & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                    & "  LEFT OUTER JOIN public.bk_mstr ON (public.req_mstr.req_bk_id = public.bk_mstr.bk_id) " _
                    & " where  public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and public.req_mstr.req_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " and req_mstr_relation.req_code='" & TxtPRDetail.Text & "'"
        End If

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
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
            & "  public.reqd_det.reqd_dt,  " _
             & "  public.reqd_det.reqd_dt, " _
            & "  public.reqd_det.reqd_pt_desc1, " _
            & "  public.reqd_det.reqd_pt_desc2,  public.reqd_det.reqd_boqs_oid, " _
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
            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
            & "  where req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail, "detail")

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join req_mstr on req_code = wf_ref_code " + _
                  " inner join reqd_det dt on dt.reqd_req_oid = req_oid " _
                & " where req_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and req_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and req_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
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
                    & "  public.req_mstr.req_bk_id, " _
                    & "  public.bk_mstr.bk_name, " _
                    & "  public.req_mstr.req_bk_id, " _
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
                    & "  LEFT OUTER JOIN public.bk_mstr ON (public.req_mstr.req_bk_id = public.bk_mstr.bk_id) " _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and req_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select req_oid, req_code, req_trans_id, false as status from req_mstr " _
                & " where req_trans_id ~~* 'd' and req_add_by ~~* '" + master_new.ClsVar.sNama + "'"

            load_data_detail(sql, gc_smart, "smart")
        End If

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("reqd_req_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqd_req_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("reqd_req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid"))
            'gv_detail.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("req_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("req_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid"))
                'gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

    
#Region "DML"
    Public Overrides Sub insert_data_awal()

        __ptnr_id = ""
        req_requested_ptnr_id.Text = ""

        __end_ptnr_id = ""
        req_end_user_ptnr_id.Text = ""

        req_en_id.Focus()
        req_en_id.ItemIndex = 0
        req_date.DateTime = _now
        req_due_date.DateTime = _now
        req_need_date.DateTime = _now
        req_requested.Text = ""
        req_end_user.Text = ""
        req_rmks.Text = ""
        req_tran_id.ItemIndex = 0
        req_type.SelectedIndex = 0

        req_is_memo.EditValue = False

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
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
                              & "  public.reqd_det.reqd_related_type, " _
                            & "  req_mstr_relation.req_code as req_code_relation, " _
                            & "  public.reqd_det.reqd_ptnr_id, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.reqd_det.reqd_si_id, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.reqd_det.reqd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                             & "  public.reqd_det.reqd_pt_desc1, " _
                            & "  public.reqd_det.reqd_pt_desc2, " _
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
                            & "  public.reqd_det.reqd_dt, 0.0 as reqd_qty_cost " _
                            & "FROM " _
                            & "  public.reqd_det " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid)" _
                            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid " _
                            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                            & " where public.reqd_det.reqd_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If req_cc_id.ItemIndex = -1 Then
            MessageBox.Show("Cost Center can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If req_requested_ptnr_id.EditValue = "" Then
            MessageBox.Show("Requester can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If req_end_user_ptnr_id.EditValue = "" Then
            MessageBox.Show("End User can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("reqd_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If func_coll.get_conf_file("restrict_pr_bp") = "1" Then
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_type")) = True Then
                    MessageBox.Show("Relation Code Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Next
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _req_oid As Guid
        _req_oid = Guid.NewGuid

        Dim _req_code As String
        Dim _req_total As Double
        Dim i, j As Integer
        Dim _req_trn_id As Integer
        Dim ssqls As New ArrayList
        Dim _req_trn_status As String
        _req_trn_status = "D" 'Lansung Default Ke Draft

        Dim ds_bantu As New DataSet
        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(req_tran_id.EditValue)
        End If
        _req_trn_id = req_tran_id.EditValue

        _req_code = func_coll.get_transaction_number("PR", req_en_id.GetColumnValue("en_code"), "req_mstr", "req_code")

        _req_total = 0
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _req_total = _req_total + (CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) * CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")))
        Next

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.req_mstr " _
                                            & "( " _
                                            & "  req_oid, " _
                                            & "  req_dom_id, " _
                                            & "  req_en_id, " _
                                            & "  req_add_by, " _
                                            & "  req_add_date, " _
                                            & "  req_code, " _
                                            & "  req_ptnr_id, " _
                                            & "  req_cmaddr_id, " _
                                            & "  req_date, " _
                                            & "  req_need_date, " _
                                            & "  req_due_date, " _
                                            & "  req_requested_ptnr_id, " _
                                            & "  req_requested, " _
                                            & "  req_end_user_ptnr_id, " _
                                            & "  req_end_user, " _
                                            & "  req_rmks, " _
                                            & "  req_bk_id, " _
                                            & "  req_sb_id, " _
                                            & "  req_cc_id, " _
                                            & "  req_si_id, " _
                                            & "  req_type, " _
                                            & "  req_is_memo, " _
                                            & "  req_pjc_id, " _
                                            & "  req_total, " _
                                            & "  req_tran_id, " _
                                            & "  req_trans_id, " _
                                            & "  req_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_req_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & req_en_id.EditValue & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ", " _
                                            & SetSetring(_req_code) & ",  " _
                                            & SetInteger(req_ptnr_id.EditValue) & ",  " _
                                            & req_cmaddr_id.EditValue & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(req_need_date.DateTime) & ",  " _
                                            & SetDate(req_due_date.DateTime) & ",  " _
                                            & SetInteger(__ptnr_id) & ",  " _
                                            & SetSetring(req_requested.Text) & ",  " _
                                            & SetInteger(__end_ptnr_id) & ",  " _
                                            & SetSetring(req_end_user.Text) & ",  " _
                                            & SetSetring(req_rmks.Text) & ",  " _
                                            & SetInteger(req_bk_id.EditValue) & ",  " _
                                            & SetInteger(req_sb_id.EditValue) & ",  " _
                                            & SetInteger(req_cc_id.EditValue) & ",  " _
                                            & SetInteger(req_si_id.EditValue) & ",  " _
                                            & SetSetring(req_type.Text.Substring(0, 1)) & ",  " _
                                            & SetBitYN(req_is_memo.EditValue) & ",  " _
                                            & SetInteger(req_pjc_id.EditValue) & ",  " _
                                            & SetDec(_req_total) & ",  " _
                                            & SetSetring(req_tran_id.EditValue) & ",  " _
                                            & SetSetring(_req_trn_status) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.reqd_det " _
                                                & "( " _
                                                & "  reqd_oid, " _
                                                & "  reqd_dom_id, " _
                                                & "  reqd_en_id, " _
                                                & "  reqd_add_by, " _
                                                & "  reqd_add_date, " _
                                                & "  reqd_req_oid, " _
                                                & "  reqd_seq, " _
                                                & "  reqd_related_oid, " _
                                                & "  reqd_related_type, " _
                                                & "  reqd_ptnr_id, " _
                                                & "  reqd_si_id, " _
                                                & "  reqd_pt_id, " _
                                                & "  reqd_rmks, " _
                                                & "  reqd_end_user, " _
                                                & "  reqd_qty, " _
                                                & "  reqd_um, " _
                                                & "  reqd_cost, " _
                                                & "  reqd_disc, " _
                                                & "  reqd_need_date, " _
                                                & "  reqd_due_date, " _
                                                & "  reqd_um_conv, " _
                                                & "  reqd_qty_real, " _
                                                   & "  reqd_pt_desc1, " _
                                                & "  reqd_pt_desc2, " _
                                                & "  reqd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & master_new.ClsVar.sdom_id & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_en_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_req_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString)) & ",  " _
                                                   & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_related_type").ToString)) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_ptnr_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_rmks")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_end_user")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_disc")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_need_date")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_due_date")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) & ",  " _
                                                  & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_pt_desc1")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_pt_desc2")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            'Update relation PR apabila terdapat relasi pr
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = False Then
                                ''.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString) _
                                '                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"
                                'ssqls.Add(.Command.CommandText)
                                '.Command.ExecuteNonQuery()
                                ''.Command.Parameters.Clear()

                                ''ant 13 maret 2011
                                'If ds_edit.Tables(0).Rows(i).Item("reqd_related_type") = "R" Then
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString _
                                                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                'ElseIf ds_edit.Tables(0).Rows(i).Item("reqd_related_type") = "B" Then
                                '    '.Command.CommandType = CommandType.Text
                                '    .Command.CommandText = "update bpd_det set bpd_qty_consume = coalesce(bpd_qty_consume,0) + " + ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString _
                                '                         & " where bpd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"

                                '    .Command.ExecuteNonQuery()
                                '    '.Command.Parameters.Clear()
                                'End If
                                ''-------


                                'Ini tidak dilakukan karena close pada saat user sudah terima barang dan ada menu khusus
                                ''.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update reqd_det set reqd_status = 'c'" _
                                '                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'" _
                                '                     & " and reqd_qty_processed = reqd_qty "

                                '.Command.ExecuteNonQuery()
                                ''.Command.Parameters.Clear()
                            End If
                        Next

                        If _conf_value = "1" Then
                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.wf_mstr " _
                                                        & "( " _
                                                        & "  wf_oid, " _
                                                        & "  wf_dom_id, " _
                                                        & "  wf_en_id, " _
                                                        & "  wf_tran_id, " _
                                                        & "  wf_ref_oid, " _
                                                        & "  wf_ref_code, " _
                                                        & "  wf_ref_desc, " _
                                                        & "  wf_seq, " _
                                                        & "  wf_user_id, " _
                                                        & "  wf_wfs_id, " _
                                                        & "  wf_iscurrent, " _
                                                        & "  wf_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(req_en_id.EditValue) & ",  " _
                                                        & SetSetring(req_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_req_oid.ToString) & ",  " _
                                                        & SetSetring(_req_code) & ",  " _
                                                        & SetSetring("Requisition") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, req_en_id.EditValue, 1, _req_oid.ToString, _req_code, req_date.DateTime) = False Then
                        '    ''sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Purchase Request " & _req_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        .Command.Commit()

                        after_success()
                        set_row(_req_oid.ToString, "req_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        Dim func_coll As New function_collection
        _conf_value = func_coll.get_conf_file("wf_requisition")

        If _conf_value = "0" Then
            If func_coll.get_conf_file("wf_requisition_editable") = "0" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) > 0 Then
                    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        End If

        relation_detail()
        Dim ssql As String
        ssql = "select reqd_qty_processed from reqd_det where reqd_req_oid= '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") & "'"
        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)
        For n As Integer = 0 To dt.Rows.Count - 1
            If SetNumber(dt.Rows(n).Item("reqd_qty_processed")) > 0 Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        Next


        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _req_oid_mstr = .Item("req_oid")
                req_en_id.EditValue = .Item("req_en_id")
                req_ptnr_id.EditValue = .Item("req_ptnr_id")
                req_cmaddr_id.EditValue = .Item("req_cmaddr_id")
                req_date.DateTime = .Item("req_date")
                req_need_date.DateTime = .Item("req_need_date")
                req_due_date.DateTime = .Item("req_due_date")
                req_requested_ptnr_id.EditValue = .Item("req_requested_ptnr_id")
                req_requested.Text = SetString(.Item("req_requested"))
                req_end_user_ptnr_id.EditValue = .Item("req_end_user_ptnr_id")
                req_end_user.Text = SetString(.Item("req_end_user"))
                req_rmks.Text = SetString(.Item("req_rmks"))
                req_sb_id.EditValue = .Item("req_sb_id")
                req_cc_id.EditValue = .Item("req_cc_id")
                req_si_id.EditValue = .Item("req_si_id")

                If .Item("req_type") = "B" Then
                    req_type.SelectedIndex = 0
                ElseIf .Item("req_type") = "N" Then
                    req_type.SelectedIndex = 1
                ElseIf .Item("req_type") = "R" Then
                    req_type.SelectedIndex = 2
                End If
                'req_is_memo.EditValue = .Item("req_is_memo")
                req_pjc_id.EditValue = .Item("req_pjc_id")
                req_tran_id.EditValue = .Item("req_tran_id")
            End With
            req_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            req_tran_id.Enabled = True

            ds_edit = New DataSet
            ds_update_related = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
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
                                & "  public.reqd_det.reqd_related_type, " _
                                & "  CASE  coalesce(public.reqd_det.reqd_related_type,'X') " _
                                & "  WHEN 'R' THEN (SELECT req_code from req_mstr reqm inner join reqd_det reqd on reqd.reqd_req_oid=reqm.req_oid where reqd.reqd_oid = public.reqd_det.reqd_related_oid) " _
                                & "  WHEN 'B' THEN (SELECT bp_code from bp_mstr inner join bpd_det on bpd_bp_oid=bp_oid where bpd_oid = public.reqd_det.reqd_related_oid) " _
                                & "  ELSE  '-' " _
                                & "  END AS req_code_relation, " _
                                & "  public.reqd_det.reqd_ptnr_id, " _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  public.reqd_det.reqd_si_id, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.reqd_det.reqd_pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.reqd_det.reqd_pt_desc1, " _
                                & "  public.reqd_det.reqd_pt_desc2, " _
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
                                & "  public.reqd_det.reqd_status, public.reqd_det.reqd_boqs_oid, " _
                                & "  public.reqd_det.reqd_dt, ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
                                & "  FROM " _
                                & "  public.reqd_det " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                                & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid)" _
                                & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid " _
                                & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                                & "  where public.reqd_det.reqd_req_oid = '" + ds.Tables(0).Rows(row).Item("req_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        'ds_update_related adalah dataset untuk membantu update reqd_qty_processed kembali ke posisi semula dulu...
                        .FillDataSet(ds_update_related, "update_related")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _req_total, _reqd_qty_processed As Double
        Dim i, j As Integer
        Dim ssqls As New ArrayList
        Dim _req_trn_status As String
        _req_trn_status = "D" 'set default langsung ke D
        Dim _req_trn_id As Integer

        _req_trn_id = req_tran_id.EditValue

        Dim ds_bantu As New DataSet
        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(req_tran_id.EditValue)
        End If

        _req_total = 0
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _req_total = _req_total + (CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) * CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")))
        Next

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '=====================================================
                        'har 20110615 
                        'pengecekan apakah nilai yg diupdate lebih besar dr qty stand

                        Dim sSQL As String
                        Dim _qty_os, _qty_delete, _qty_add As Double

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid")) = False Then
                                _qty_os = 0
                                _qty_delete = 0
                                _qty_add = 0

                                sSQL = "SELECT  " _
                                    & "  a.boqs_oid, " _
                                    & "  a.boqs_seq, " _
                                    & "  a.boqs_pt_id, " _
                                    & "  a.boqs_qty,coalesce(a.boqs_qty_pr,0) as boqs_qty_pr, a.boqs_qty + coalesce(a.boqs_qty_pr,0) as boqs_qty_os " _
                                    & "FROM " _
                                    & "  public.boqs_stand a " _
                                    & "  INNER JOIN public.boq_mstr b ON (a.boqs_boq_oid = b.boq_oid) " _
                                    & "WHERE " _
                                    & "  a.boqs_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid"))

                                Dim dt As New DataTable
                                dt = master_new.PGSqlConn.GetTableData(sSQL)

                                For Each dr As DataRow In dt.Rows
                                    _qty_os = dr("boqs_qty_os")
                                Next

                                For n As Integer = 0 To ds_update_related.Tables(0).Rows.Count - 1
                                    If SetString(ds_update_related.Tables(0).Rows(n).Item("reqd_boqs_oid")) <> "" Then
                                        If ds_update_related.Tables(0).Rows(n).Item("reqd_boqs_oid") = ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid") Then
                                            _qty_delete = ds_update_related.Tables(0).Rows(n).Item("reqd_qty")
                                        End If
                                    End If
                                Next

                                _qty_add = ds_edit.Tables(0).Rows(i).Item("reqd_qty")

                                If _qty_add > (_qty_os - _qty_delete) Then
                                    Box("Qty PR higher than Qty Bill of Quantity")
                                    Return False
                                    Exit Function
                                End If
                            End If

                        Next
                        'har 20110615 end
                        '=====================================================

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.req_mstr   " _
                                            & "SET  " _
                                            & "  req_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  req_en_id = " & req_en_id.EditValue & ",  " _
                                            & "  req_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  req_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  req_ptnr_id = " & req_ptnr_id.EditValue & ",  " _
                                            & "  req_cmaddr_id = " & req_cmaddr_id.EditValue & ",  " _
                                            & "  req_date = " & SetDate(req_date.DateTime) & ",  " _
                                            & "  req_need_date = " & SetDate(req_need_date.DateTime) & ",  " _
                                            & "  req_due_date = " & SetDate(req_due_date.DateTime) & ",  " _
                                            & "  req_requested = " & SetSetringDB(req_requested.Text) & ",  " _
                                            & "  req_end_user = " & SetSetringDB(req_end_user.Text) & ",  " _
                                            & "  req_rmks = " & SetSetringDB(req_rmks.Text) & ",  " _
                                            & "  req_bk_id = " & SetInteger(req_bk_id.EditValue) & ",  " _
                                            & "  req_sb_id = " & SetInteger(req_sb_id.EditValue) & ",  " _
                                            & "  req_cc_id = " & SetInteger(req_cc_id.EditValue) & ",  " _
                                            & "  req_si_id = " & SetInteger(req_si_id.EditValue) & ",  " _
                                            & "  req_type = " & SetSetring(req_type.Text.Substring(0, 1)) & ",  " _
                                            & "  req_is_memo = " & SetInteger(req_is_memo.EditValue) & ",  " _
                                            & "  req_pjc_id = " & SetInteger(req_pjc_id.EditValue) & ",  " _
                                            & "  req_total = " & SetDec(_req_total.ToString) & ",  " _
                                            & "  req_tran_id = " & req_tran_id.EditValue & ",  " _
                                            & "  req_trans_id = " & SetSetring(_req_trn_status) & ",  " _
                                            & "  req_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  req_oid = " & SetSetring(_req_oid_mstr.ToString) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_update_related.Tables(0).Rows.Count - 1
                            'update boqs_stand on edit================
                            'har 20110615 begin
                            If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_related_oid")) = False Then
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " _
                                                    + SetDec(ds.Tables("detail").Rows(i).Item("reqd_qty").ToString) + ", " _
                                                    + " reqd_status = null" _
                                                    & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update req_mstr set req_close_date = null where req_oid = (select reqd_req_oid from reqd_det where reqd_oid = '" _
                                                    + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "')"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If

                            If SetString(ds_update_related.Tables(0).Rows(i).Item("reqd_boqs_oid")) <> "" Then
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) - " _
                                            & SetDbl(ds_update_related.Tables(0).Rows(i).Item("reqd_qty")) & " where boqs_oid = '" & ds_update_related.Tables(0).Rows(i).Item("reqd_boqs_oid") & "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If
                            'har 20110615 end
                            '=========================================
                        Next

                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table po
                        'kalau sudah relasi ke table po jadi nya error...dan harusnya tidak didelete
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from reqd_det where coalesce(reqd_qty_processed,0) = 0 and reqd_req_oid = '" + _req_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        '******************************************************

                        'Insert data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _reqd_qty_processed = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_qty_processed")) = True, 0, ds_edit.Tables(0).Rows(i).Item("reqd_qty_processed"))
                            If _reqd_qty_processed = 0 Then
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.reqd_det " _
                                                    & "( " _
                                                    & "  reqd_oid, " _
                                                    & "  reqd_dom_id, " _
                                                    & "  reqd_en_id, " _
                                                    & "  reqd_upd_by, " _
                                                    & "  reqd_upd_date, " _
                                                    & "  reqd_req_oid, " _
                                                    & "  reqd_seq, " _
                                                    & "  reqd_related_oid, " _
                                                    & "  reqd_ptnr_id, " _
                                                    & "  reqd_si_id,  " _
                                                    & "  reqd_pt_id, " _
                                                    & "  reqd_rmks, " _
                                                    & "  reqd_end_user, " _
                                                    & "  reqd_qty, " _
                                                    & "  reqd_qty_processed, " _
                                                    & "  reqd_um, " _
                                                    & "  reqd_cost, " _
                                                    & "  reqd_disc, " _
                                                    & "  reqd_need_date, " _
                                                    & "  reqd_due_date, " _
                                                    & "  reqd_um_conv, " _
                                                    & "  reqd_qty_real, " _
                                                    & "  reqd_boqs_oid, " _
                                                    & "  reqd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & master_new.ClsVar.sdom_id & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_req_oid_mstr.ToString) & ",  " _
                                                    & i & ",  " _
                                                    & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString)) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_ptnr_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_pt_id")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_rmks").ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_end_user").ToString) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("reqd_qty_processed")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_um").ToString) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_cost")) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_disc")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_need_date")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_due_date")) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_um_conv")) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                'Update relation PR apabila terdapat relasi pr
                                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = False Then
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString) _
                                                         & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                End If
                            Else
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.reqd_det   " _
                                                    & "SET  " _
                                                    & "  reqd_ptnr_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_ptnr_id")) & ",  " _
                                                    & "  reqd_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_si_id")) & ",  " _
                                                    & "  reqd_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_rmks")) & ",  " _
                                                    & "  reqd_end_user = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_end_user")) & ",  " _
                                                    & "  reqd_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & ",  " _
                                                    & "  reqd_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_um")) & ",  " _
                                                    & "  reqd_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")) & ",  " _
                                                    & "  reqd_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_disc")) & ",  " _
                                                    & "  reqd_need_date = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_need_date")) & ",  " _
                                                    & "  reqd_due_date = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_due_date")) & ",  " _
                                                    & "  reqd_um_conv = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_um_conv")) & ",  " _
                                                    & "  reqd_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) & ",  " _
                                                    & "  reqd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  reqd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_oid")) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If

                            'update boqs_stand on edit================
                            'har 20110615 begin

                            If SetString(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid")) <> "" Then
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) + " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & " where boqs_oid = '" & ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid") & "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If
                            'har 20110615 end
                            '=========================================

                        Next

                        If _conf_value = "1" Then
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.wf_mstr " _
                                                        & "( " _
                                                        & "  wf_oid, " _
                                                        & "  wf_dom_id, " _
                                                        & "  wf_en_id, " _
                                                        & "  wf_tran_id, " _
                                                        & "  wf_ref_oid, " _
                                                        & "  wf_ref_code, " _
                                                        & "  wf_ref_desc, " _
                                                        & "  wf_seq, " _
                                                        & "  wf_user_id, " _
                                                        & "  wf_wfs_id, " _
                                                        & "  wf_iscurrent, " _
                                                        & "  wf_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(req_en_id.EditValue) & ",  " _
                                                        & SetSetring(req_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_req_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) & ",  " _
                                                        & SetSetring("Requisition") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, req_en_id.EditValue, 1, _req_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"), req_date.DateTime) = False Then
                        '    ''sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Purchase Request " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        .Command.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_req_oid_mstr, "req_oid")
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "select coalesce(reqd_qty_processed,0) as reqd_qty_processed from reqd_det " + _
                           " where reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "reqd_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("reqd_qty_processed") > 0 Then
                MessageBox.Show("Can't Delete Processed Requisition...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        '14 maret 2011
        If _conf_value = "0" Then
            'MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Exit Function
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) > 0 Then
                    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        End If
        '----------------------------------------

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim i As Integer
        Dim ssqls As New ArrayList

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.CustomCommand
                    With objinsert
.Command.Open()
                        ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            '.Command = .Connection.CreateCommand
                            '.Command.Transaction = sqlTran

                            'ant 14 maret 2011
                            Dim _is_proc_comp As Boolean

                            _is_proc_comp = False
                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqd_req_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") Then
                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_processed")) = False Then
                                        If ds.Tables("detail").Rows(i).Item("reqd_qty_processed") > 0 Then
                                            _is_proc_comp = True
                                        End If
                                    End If

                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_completed")) = False Then
                                        If ds.Tables("detail").Rows(i).Item("reqd_qty_completed") > 0 Then
                                            _is_proc_comp = True
                                        End If
                                    End If

                                End If
                            Next

                            If _is_proc_comp = False Then
                                For i = 0 To ds.Tables("detail").Rows.Count - 1
                                    If ds.Tables("detail").Rows(i).Item("reqd_req_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") Then
                                        If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_related_oid")) = False Then
                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + SetDec(ds.Tables("detail").Rows(i).Item("reqd_qty").ToString) + ", " + _
                                                                   " reqd_status = null" _
                                                                 & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "'"
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()

                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update req_mstr set req_close_date = null where req_oid = (select reqd_req_oid from reqd_det where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "')"
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()
                                        End If

                                        'update boqs_stand on delete###############
                                        'har 20110606
                                        Dim sSQL As String
                                        sSQL = "SELECT  " _
                                            & "  a.reqd_oid, " _
                                            & "  a.reqd_req_oid, " _
                                            & "  a.reqd_boqs_oid, " _
                                            & "  a.reqd_qty, " _
                                            & "  a.reqd_pt_id " _
                                            & "FROM " _
                                            & "  public.reqd_det a " _
                                            & "WHERE " _
                                            & "  a.reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'"

                                        Dim dt As New DataTable
                                        dt = master_new.PGSqlConn.GetTableData(sSQL)

                                        For Each dr As DataRow In dt.Rows
                                            If SetString(dr("reqd_boqs_oid")) <> "" Then
                                                '.Command.CommandType = CommandType.Text
                                                .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) - " _
                                                            & SetDbl(dr("reqd_qty")) & " where boqs_oid = '" & dr("reqd_boqs_oid") & "'"
                                                ssqls.Add(.Command.CommandText)
                                                .Command.ExecuteNonQuery()
                                                '.Command.Parameters.Clear()
                                            End If
                                        Next
                                        '###########################################
                                    End If
                                Next

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from req_mstr where req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = insert_log("Delete Purchase Request " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Else
                                MsgBox("Unable to Delete.., Part Already Processed or Completed!", MsgBoxStyle.Critical, "Delete Canceled")
                            End If
                            '----------------------------------

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                Next
                            End If

                            .Command.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            'sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region

#Region "gv_edit"
    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_en_id")
        Dim _pod_si_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_si_id")

        If _col = "req_code_relation" Then
            Dim frm As New FRequisitionSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._obj = gv_edit
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._si_id = _pod_si_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name" Then
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._pt_id = gv_edit.GetRowCellValue(_row, "reqd_pt_id")
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _reqd_qty, _reqd_qty_real, _reqd_um_conv, _reqd_qty_cost, _reqd_cost, _reqd_disc, _reqd_qty_processed As Double
        _reqd_um_conv = 1
        _reqd_qty = 1
        _reqd_cost = 0
        _reqd_disc = 0

        If e.Column.Name = "reqd_qty" Then
            '********* Cek Qty Processed
            Try
                _reqd_qty_processed = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty_processed"))
            Catch ex As Exception
            End Try

            If e.Value < _reqd_qty_processed Then
                MessageBox.Show("Qty Requistion Can't Lower Than Qty Processed..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If
            '********************************

            Try
                _reqd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _reqd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_cost"))
            Catch ex As Exception
            End Try

            Try
                _reqd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_disc"))
            Catch ex As Exception
            End Try

            _reqd_qty_real = e.Value * _reqd_um_conv
            _reqd_qty_cost = (e.Value * _reqd_cost) - (e.Value * _reqd_cost * _reqd_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_real", _reqd_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)

        ElseIf e.Column.Name = "reqd_cost" Then
            Try
                _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
            Catch ex As Exception
            End Try

            Try
                _reqd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_disc")))
            Catch ex As Exception
            End Try

            _reqd_qty_cost = (e.Value * _reqd_qty) - (e.Value * _reqd_qty * _reqd_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)

        ElseIf e.Column.Name = "reqd_disc" Then
            Try
                _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
            Catch ex As Exception
            End Try

            Try
                _reqd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_cost")))
            Catch ex As Exception
            End Try

            _reqd_qty_cost = (_reqd_cost * _reqd_qty) - (_reqd_cost * _reqd_qty * e.Value)
            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)

        ElseIf e.Column.Name = "reqd_um_conv" Then
            Try
                _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
            Catch ex As Exception
            End Try

            _reqd_qty_real = e.Value * _reqd_qty

            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_real", _reqd_qty_real)
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "reqd_en_id", req_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", req_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "reqd_ptnr_id", req_ptnr_id.GetColumnValue("ptnr_id"))
            .SetRowCellValue(e.RowHandle, "ptnr_name", req_ptnr_id.GetColumnValue("ptnr_name"))
            .SetRowCellValue(e.RowHandle, "reqd_si_id", req_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", req_si_id.GetColumnValue("si_desc"))
            '.SetRowCellValue(e.RowHandle, "reqd_end_user", Trim(req_end_user.Text))
            '.SetRowCellValue(e.RowHandle, "reqd_end_ptnr_id", req_ptnr_id.GetColumnValue("ptnr_id"))
            '.SetRowCellValue(e.RowHandle, "ptnr_name", req_ptnr_id.GetColumnValue("ptnr_name"))
            .SetRowCellValue(e.RowHandle, "reqd_end_user", req_end_user_ptnr_id.Text)
            .SetRowCellValue(e.RowHandle, "reqd_qty", 0)
            .SetRowCellValue(e.RowHandle, "reqd_cost", 0)
            .SetRowCellValue(e.RowHandle, "reqd_disc", 0)
            .SetRowCellValue(e.RowHandle, "reqd_need_date", req_need_date.DateTime)
            .SetRowCellValue(e.RowHandle, "reqd_due_date", req_due_date.DateTime)
            .SetRowCellValue(e.RowHandle, "reqd_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "reqd_qty_real", 0)
            .SetRowCellValue(e.RowHandle, "reqd_qty_cost", 0)
            .BestFitColumns()
        End With
    End Sub
#End Region

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Dim _reqd_qty_processed As Double = 0

        Try
            _reqd_qty_processed = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "reqd_qty_processed")))
        Catch ex As Exception
        End Try

        If _reqd_qty_processed <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        Dim _reqd_qty_processed As Double = 0

        Try
            _reqd_qty_processed = ((gv_edit.GetRowCellValue(0, "reqd_qty_processed")))
        Catch ex As Exception
        End Try

        If _reqd_qty_processed <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_en_id")
        _type = 1
        _table = "req_mstr"
        _initial = "req"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  req_oid, " _
            & "  req_dom_id, " _
            & "  req_en_id, " _
            & "  req_upd_date, " _
            & "  req_upd_by, " _
            & "  req_add_date, " _
            & "  req_add_by, " _
            & "  req_code, " _
            & "  req_ptnr_id, " _
            & "  req_cmaddr_id, " _
            & "  req_date, " _
            & "  req_need_date, " _
            & "  req_due_date, " _
            & "  req_requested, " _
            & "  req_end_user, " _
            & "  req_rmks, " _
            & "  req_sb_id, " _
            & "  req_cc_id, " _
            & "  req_si_id, " _
            & "  req_type, " _
            & "  req_pjc_id, " _
            & "  req_close_date, " _
            & "  req_total, " _
            & "  req_tran_id, " _
            & "  req_trans_id, " _
            & "  req_trans_rmks, " _
            & "  req_current_route, " _
            & "  req_next_route, " _
            & "  req_dt, " _
            & "  reqd_ptnr_id, " _
            & "  reqd_pt_id, " _
            & "  reqd_rmks, " _
            & "  reqd_end_user, " _
            & "  reqd_qty, " _
            & "  reqd_um, " _
            & "  reqd_cost, " _
            & "  reqd_disc, " _
            & "  reqd_need_date, " _
            & "  reqd_due_date, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3, " _
            & "  ptnr_name, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  um_master.code_name as um_name , " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  req_mstr " _
            & "  inner join reqd_det on reqd_req_oid = req_oid " _
            & "  left outer join cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
            & "  left outer join ptnr_mstr on ptnr_id = reqd_ptnr_id " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = reqd_um" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = req_oid  " _
            & "  where req_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code") + "'"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRRequisition"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        frm.ShowDialog()
    End Sub

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")
        _colom = "req_trans_id"
        _table = "req_mstr"
        _criteria = "req_code"
        _initial = "req"
        _type = "pr"
        _title = "Requisition"

        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")
        _colom = "req_trans_id"
        _table = "req_mstr"
        _criteria = "req_code"
        _initial = "req"
        _type = "pr"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""
        Dim ssqls As New ArrayList

        Try
            Using objcek As New master_new.CustomCommand
                With objcek
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where " + par_criteria + " = '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        'ant 14 maret 2011
                        Dim _is_proc_comp As Boolean

                        _is_proc_comp = False
                        For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
                            If ds.Tables("detail").Rows(i).Item("reqd_req_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") Then
                                If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_processed")) = False Then
                                    If ds.Tables("detail").Rows(i).Item("reqd_qty_processed") > 0 Then
                                        _is_proc_comp = True
                                    End If
                                End If

                                If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_completed")) = False Then
                                    If ds.Tables("detail").Rows(i).Item("reqd_qty_completed") > 0 Then
                                        _is_proc_comp = True
                                    End If
                                End If

                            End If
                        Next

                        If _is_proc_comp = False Then
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                                   " where " + par_criteria + " = '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                                   " where wf_ref_code = '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqd_req_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") Then
                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_related_oid")) = False Then
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + ds.Tables("detail").Rows(i).Item("reqd_qty").ToString + ", " + _
                                                               " reqd_status = null" _
                                                             & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "'"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update req_mstr set req_close_date = null where req_oid = (select reqd_req_oid from reqd_det where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "')"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()
                                    End If

                                    'update boqs_stand on cancel approve ================
                                    'har 20110615 begin

                                    If SetString(ds.Tables("detail").Rows(i).Item("reqd_boqs_oid")) <> "" Then
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) - " _
                                                    & SetDbl(ds.Tables("detail").Rows(i).Item("reqd_qty")) & " where boqs_oid = '" & ds.Tables("detail").Rows(i).Item("reqd_boqs_oid") & "'"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()
                                    End If
                                    'har 20110615 end
                                    '=========================================
                                End If
                            Next

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                Next
                            End If

                            .Command.Commit()
                            MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            row = BindingContext(ds.Tables(0)).Position
                            help_load_data(True)
                            set_row(Trim(par_oid), par_initial + "_oid")
                        Else
                            MsgBox("Unable to Canceled.., Part Already Processed or Completed!", MsgBoxStyle.Critical, "Delete Canceled")
                        End If
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

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        _type = "pr"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Requisition"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = "True" Then

                Try
                    gv_email.Columns("req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("req_oid").ToString)
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("req_code"), 0)
                user_wf_email = mf.get_email_address(user_wf)

                Try
                    Using objinsert As New master_new.CustomCommand
                        With objinsert
.Command.Open()
                            ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                '.Command = .Connection.CreateCommand
                                '.Command.Transaction = sqlTran
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update req_mstr set req_trans_id = '" + _trans_id + "'," + _
                                               " req_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " req_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where req_oid = '" + ds.Tables("smart").Rows(i).Item("req_oid") + "'"

                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("req_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                .Command.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("req_code"), "pr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("req_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Requisition", ds.Tables("smart").Rows(i).Item("req_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

                            Catch ex As PgSqlException
                                'sqlTran.Rollback()
                                MessageBox.Show(ex.Message)
                            End Try
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        Next

        help_load_data(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT  " _
                & "  c.en_desc, " _
                & "  a.req_code, " _
                & "  d.ptnr_code, " _
                & "  d.ptnr_name, " _
                & "  a.req_date, " _
                & "  a.req_need_date, " _
                & "  a.req_due_date, " _
                & "  a.req_requested, " _
                & "  a.req_end_user, " _
                & "  a.req_rmks, " _
                & "  e.pt_code, " _
                & "  e.pt_desc1, " _
                & "  e.pt_desc2, " _
                & "  b.reqd_rmks, " _
                & "  b.reqd_qty, " _
                & "  f.code_code, " _
                & "  b.reqd_qty_processed, " _
                & "  b.reqd_qty_completed " _
                & "FROM " _
                & "  public.req_mstr a " _
                & "  INNER JOIN public.reqd_det b ON (a.req_oid = b.reqd_req_oid) " _
                & "  INNER JOIN public.en_mstr c ON (a.req_en_id = c.en_id) " _
                & "  INNER JOIN public.ptnr_mstr d ON (a.req_ptnr_id = d.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr e ON (b.reqd_pt_id = e.pt_id) " _
                & "  INNER JOIN public.code_mstr f ON (b.reqd_um = f.code_id) " _
                & "WHERE " _
                & "  a.req_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " AND  " _
                & "  a.req_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            Dim frm As New frmExport
            Dim _file As String = AskSaveAsFile("Excel Files | *.xls")
          
            With frm
                add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "PR Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Entity", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date", "req_date", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Duedate", "req_due_date", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remark Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
                add_column_copy(.gv_export, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
                add_column_copy(.gv_export, "Qty Komplit", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
                add_column_copy(.gv_export, "UM", "code_code", DevExpress.Utils.HorzAlignment.Default)

                .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
                .gv_export.BestFitColumns()
                .gv_export.ExportToXls(_file)
            End With

            frm.Dispose()
            Box("Export data sucess")

            OpenFile(_file)
           
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

    Private Sub BEPRCopy_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEPRCopy.ButtonClick
        Try
            Dim frm As New FRequisitionSearch
            frm.set_win(Me)
            frm._en_id = req_en_id.EditValue
            frm._obj = BEPRCopy
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub req_end_user_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles req_end_user_ptnr_id.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = req_en_id.EditValue
        frm._obj = req_end_user_ptnr_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub req_requested_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles req_requested_ptnr_id.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = req_en_id.EditValue
        frm._obj = req_requested_ptnr_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub

End Class

