Imports master_new.ModFunction

Public Class FRequisitionSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FRequisitionSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_requisition")
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FRequisitionPrint" Then
            add_column(gv_master, "Req. Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        ElseIf fobject.name = "FReqTransferIssue" Then
            add_column(gv_master, "Req. Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        ElseIf fobject.name = "FAssetTransferIssue" Then
            add_column(gv_master, "Req. Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Requsted", "req_requested", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
            'ant 16 mei 2011
        ElseIf fobject.name = "FInstalationStart" Then
            add_column(gv_master, "Req. Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Subscriber Number", "ssc_code", DevExpress.Utils.HorzAlignment.Default)
            '-----------------------------------------------
        Else
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Date", "req_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        If fobject.name = "FRequisitionPrint" Then
            get_sequel = "SELECT  " _
                        & "  req_code, " _
                        & "  req_date " _
                        & "FROM  " _
                        & "  public.req_mstr " _
                        & "  where req_en_id = " + _en_id.ToString _
                        & "  and req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  order by req_code "
        ElseIf fobject.name = "FAssetTransferIssue" Then
            get_sequel = "SELECT  " _
                        & "  req_oid, " _
                        & "  req_code, " _
                        & "  req_requested, " _
                        & "  req_end_user, " _
                        & "  req_date " _
                        & "  FROM  " _
                        & "  public.req_mstr " _
                        & "  where req_en_id = " + _en_id.ToString _
                        & "  and req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' order by req_code "
            End If

        ElseIf fobject.name = "FReqTransferIssue" Then
            'ant 5 maret 2011
            get_sequel = "SELECT DISTINCT " _
                        & "  req_oid, " _
                        & "  req_code, " _
                        & "  req_date " _
                        & "  FROM  " _
                        & "  public.req_mstr " _
                        & "  inner join public.reqd_det on reqd_req_oid = req_oid " _
                        & "  where req_en_id = " + _en_id.ToString _
                        & "  and reqd_qty - coalesce(reqd_qty_completed,0) > 0 " _
                        & "  and req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date)
            '----------------------------
            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by req_code  "
        ElseIf fobject.name = "FRequisition" Then
            'ant 14 maret 2011
            get_sequel = "SELECT  " _
                    & "  reqd_oid, " _
                    & "  reqd_en_id,   " _
                    & "  en_desc, " _
                    & "  req_code, " _
                    & "  reqd_req_oid,   " _
                    & "  reqd_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  reqd_si_id, " _
                    & "  si_desc, " _
                    & "  pjc_desc, " _
                    & "  reqd_pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  reqd_rmks, " _
                    & "  reqd_end_user, " _
                    & "  reqd_qty_real - coalesce(reqd_qty_processed,0) as qty_open, " _
                    & "  reqd_um, " _
                    & "  code_name, " _
                    & "  reqd_cost, " _
                    & "  reqd_disc, " _
                    & "  req_date, " _
                    & "  reqd_need_date, " _
                    & "  reqd_due_date, " _
                    & "  reqd_pt_desc1, " _
                    & "  reqd_pt_desc2, " _
                    & "  reqd_um_conv " _
                    & "FROM  " _
                    & "  public.reqd_det  " _
                    & "  inner join public.req_mstr on req_oid = reqd_req_oid " _
                    & "  inner join public.cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
                    & "  inner join public.en_mstr on en_id = reqd_en_id " _
                    & "  inner join public.ptnr_mstr on ptnr_id = reqd_ptnr_id " _
                    & "  inner join public.si_mstr on si_id = reqd_si_id " _
                    & "  inner join public.pjc_mstr on pjc_id = req_pjc_id " _
                    & "  inner join public.pt_mstr on pt_id = reqd_pt_id " _
                    & "  inner join public.code_mstr on code_id = reqd_um " _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and req_en_id = " + _en_id.ToString _
                    & " and reqd_qty_real - coalesce(reqd_qty_processed,0) > 0 " _
                    & " and coalesce(reqd_status,'') = ''"

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by req_code  "

        ElseIf fobject.name = "FPurchaseOrder" Then
            get_sequel = "SELECT  " _
                    & "  reqd_oid, " _
                    & "  reqd_en_id,   " _
                    & "  en_desc, " _
                    & "  req_code, " _
                    & "  reqd_req_oid,   " _
                    & "  reqd_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  reqd_si_id, " _
                    & "  si_desc, " _
                    & "  req_pjc_id, " _
                    & "  pjc_desc, " _
                    & "  reqd_pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_type, " _
                    & "  pt_ls, " _
                    & "  reqd_rmks, " _
                    & "  reqd_end_user, " _
                    & "  reqd_qty_real - coalesce(reqd_qty_processed,0) as qty_open, " _
                    & "  reqd_um, " _
                    & "  code_name, " _
                    & "  reqd_cost, " _
                    & "  reqd_disc, " _
                    & "  req_date, " _
                    & "  reqd_need_date, " _
                    & "  reqd_due_date, " _
                    & "  reqd_pt_desc1, " _
                    & "  reqd_pt_desc2, " _
                    & "  loc_id, " _
                    & "  loc_desc, " _
                    & "  reqd_um_conv,f_get_cost(reqd_en_id,reqd_pt_id,'G') as standard_cost " _
                    & "FROM  " _
                    & "  public.reqd_det  " _
                    & "  inner join public.req_mstr on req_oid = reqd_req_oid " _
                    & "  inner join public.cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
                    & "  inner join public.en_mstr on en_id = reqd_en_id " _
                    & "  inner join public.ptnr_mstr on ptnr_id = reqd_ptnr_id " _
                    & "  inner join public.si_mstr on si_id = reqd_si_id " _
                    & "  inner join public.pjc_mstr on pjc_id = req_pjc_id " _
                    & "  inner join public.pt_mstr on pt_id = reqd_pt_id " _
                    & "  inner join public.loc_mstr on loc_id = pt_loc_id " _
                    & "  inner join public.code_mstr on code_id = reqd_um " _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and req_en_id = " + _en_id.ToString _
                    & " and reqd_qty_real - coalesce(reqd_qty_processed,0) > 0 " _
                    & " and coalesce(reqd_status,'') = ''" _
                    & " and coalesce(req_model,'') <> 'R'"

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by req_code  "
        ElseIf fobject.name = "FPurchaseOrderAsset" Or fobject.name = "FPurchaseOrderExpense" Then
            get_sequel = "SELECT  " _
                    & "  reqd_oid, " _
                    & "  reqd_en_id,   " _
                    & "  en_desc, " _
                    & "  req_code, " _
                    & "  reqd_req_oid,   " _
                    & "  reqd_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  reqd_si_id, " _
                    & "  si_desc, " _
                    & "  req_pjc_id, " _
                    & "  pjc_desc, " _
                    & "  reqd_pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_type, " _
                    & "  pt_ls, " _
                    & "  reqd_rmks, " _
                    & "  reqd_end_user, " _
                    & "  reqd_qty_real - coalesce(reqd_qty_processed,0) as qty_open, " _
                    & "  reqd_um, " _
                    & "  code_name, " _
                    & "  reqd_cost, " _
                    & "  reqd_disc, " _
                    & "  req_date, " _
                    & "  reqd_need_date, " _
                    & "  reqd_due_date, " _
                    & "  reqd_pt_desc1, " _
                    & "  reqd_pt_desc2, " _
                    & "  loc_id, " _
                    & "  loc_desc, " _
                    & "  reqd_um_conv " _
                    & "FROM  " _
                    & "  public.reqd_det  " _
                    & "  inner join public.req_mstr on req_oid = reqd_req_oid " _
                    & "  inner join public.cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
                    & "  inner join public.en_mstr on en_id = reqd_en_id " _
                    & "  inner join public.ptnr_mstr on ptnr_id = reqd_ptnr_id " _
                    & "  inner join public.si_mstr on si_id = reqd_si_id " _
                    & "  inner join public.pjc_mstr on pjc_id = req_pjc_id " _
                    & "  inner join public.pt_mstr on pt_id = reqd_pt_id " _
                    & "  inner join public.loc_mstr on loc_id = pt_loc_id " _
                    & "  inner join public.code_mstr on code_id = reqd_um " _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and req_en_id = " + _en_id.ToString _
                    & " and reqd_qty_real - coalesce(reqd_qty_processed,0) > 0 " _
                    & " and coalesce(reqd_status,'') = ''" _
                    & " and coalesce(req_model,'') <> 'R'" _
                    & " and req_pjc_id = 0 "

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by req_code  "
            'ant 16 mei 2011
        ElseIf fobject.name = "FInstalationStart" Then
            get_sequel = "SELECT  " _
                        & "  req_oid, " _
                        & "  req_code, " _
                        & "  req_date, " _
                        & "  ssc_code, " _
                        & "  ssc_loc_id, " _
                        & "  loc_desc " _
                        & "  FROM  " _
                        & "  public.req_mstr " _
                        & "  INNER JOIN public.ssc_mstr ON (public.req_mstr.req_ssc_oid = public.ssc_mstr.ssc_oid) " _
                        & "  inner join loc_mstr on loc_id = ssc_loc_id" _
                        & "  where req_en_id = " + _en_id.ToString _
                        & "  and req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & " and coalesce(req_model,'') = 'R' "

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by req_code  "
            '------------------------------------------------
        Else
            get_sequel = "SELECT  " _
                    & "  reqd_oid, " _
                    & "  reqd_en_id,   " _
                    & "  en_desc, " _
                    & "  req_code, " _
                    & "  reqd_req_oid,   " _
                    & "  reqd_ptnr_id, " _
                    & "  ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  reqd_si_id, " _
                    & "  si_desc, " _
                    & "  reqd_pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  reqd_rmks, " _
                    & "  reqd_end_user, " _
                    & "  reqd_qty_real - coalesce(reqd_qty_processed,0) as qty_open, " _
                    & "  reqd_um, " _
                    & "  code_name, " _
                    & "  reqd_cost, " _
                    & "  reqd_disc, " _
                    & "  req_date, " _
                    & "  reqd_need_date, " _
                    & "  reqd_due_date, " _
                    & "  reqd_um_conv " _
                    & "FROM  " _
                    & "  public.reqd_det  " _
                    & "  inner join public.req_mstr on req_oid = reqd_req_oid " _
                    & "  inner join public.cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
                    & "  inner join public.en_mstr on en_id = reqd_en_id " _
                    & "  inner join public.ptnr_mstr on ptnr_id = reqd_ptnr_id " _
                    & "  inner join public.si_mstr on si_id = reqd_si_id " _
                    & "  inner join public.pt_mstr on pt_id = reqd_pt_id " _
                    & "  inner join public.code_mstr on code_id = reqd_um " _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and req_en_id = " + _en_id.ToString _
                    & " and reqd_qty_real - coalesce(reqd_qty_processed,0) > 0 " _
                    & " and coalesce(reqd_status,'') = ''"
        End If
        

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

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

        If fobject.name = "FRequisition" Then
            Dim _reqd_qty_cost, _qty_open, _reqd_cost, _reqd_disc As Double

            fobject.gv_edit.SetRowCellValue(_row, "reqd_related_oid", ds.Tables(0).Rows(_row_gv).Item("reqd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "req_code_relation", ds.Tables(0).Rows(_row_gv).Item("req_code"))

            fobject.gv_edit.SetRowCellValue(_row, "reqd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("reqd_ptnr_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))

            fobject.gv_edit.SetRowCellValue(_row, "reqd_si_id", ds.Tables(0).Rows(_row_gv).Item("reqd_si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))

            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_desc2"))

            fobject.gv_edit.SetRowCellValue(_row, "reqd_rmks", ds.Tables(0).Rows(_row_gv).Item("reqd_rmks"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_end_user", ds.Tables(0).Rows(_row_gv).Item("reqd_end_user"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))

            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("reqd_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))

            fobject.gv_edit.SetRowCellValue(_row, "reqd_cost", ds.Tables(0).Rows(_row_gv).Item("reqd_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_disc", ds.Tables(0).Rows(_row_gv).Item("reqd_disc"))

            fobject.gv_edit.SetRowCellValue(_row, "reqd_need_date", ds.Tables(0).Rows(_row_gv).Item("reqd_need_date"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_due_date", ds.Tables(0).Rows(_row_gv).Item("reqd_due_date"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um_conv", ds.Tables(0).Rows(_row_gv).Item("reqd_um_conv"))

            _qty_open = ds.Tables(0).Rows(_row_gv).Item("qty_open")
            _reqd_cost = ds.Tables(0).Rows(_row_gv).Item("reqd_cost")
            _reqd_disc = ds.Tables(0).Rows(_row_gv).Item("reqd_disc")
            _reqd_qty_cost = (_qty_open * _reqd_cost) - (_qty_open * _reqd_cost * _reqd_disc)

            fobject.gv_edit.SetRowCellValue(_row, "reqd_qty_cost", _reqd_qty_cost)
            fobject.gv_edit.SetRowCellValue(_row, "reqd_related_type", "R")
            fobject.gv_edit.BestFitColumns()
            'rev by hendrik 2011-06-18 ----------------------------------------------------------------------------
        ElseIf fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrderAsset" Or fobject.name = "FPurchaseOrderExpense" Then
            Dim _pod_qty_cost, _qty_open, _reqd_cost, _reqd_disc As Double

            fobject.gv_edit.SetRowCellValue(_row, "pod_reqd_oid", ds.Tables(0).Rows(_row_gv).Item("reqd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "req_code_relation", ds.Tables(0).Rows(_row_gv).Item("req_code"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_si_id", ds.Tables(0).Rows(_row_gv).Item("reqd_si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_pjc_id", ds.Tables(0).Rows(_row_gv).Item("req_pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_loc_id", ds.Tables(0).Rows(_row_gv).Item("loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_id", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_rmks", ds.Tables(0).Rows(_row_gv).Item("reqd_rmks"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_end_user", ds.Tables(0).Rows(_row_gv).Item("reqd_end_user"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pr_open", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("reqd_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("reqd_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_disc", ds.Tables(0).Rows(_row_gv).Item("reqd_disc"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_need_date", ds.Tables(0).Rows(_row_gv).Item("reqd_need_date"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_due_date", ds.Tables(0).Rows(_row_gv).Item("reqd_due_date"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_um_conv", ds.Tables(0).Rows(_row_gv).Item("reqd_um_conv"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_standard_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))

            _qty_open = ds.Tables(0).Rows(_row_gv).Item("qty_open")
            _reqd_cost = ds.Tables(0).Rows(_row_gv).Item("reqd_cost")
            _reqd_disc = ds.Tables(0).Rows(_row_gv).Item("reqd_disc")
            _pod_qty_cost = (_qty_open * _reqd_cost) - (_qty_open * _reqd_cost * _reqd_disc)

            fobject.gv_edit.SetRowCellValue(_row, "pod_qty_cost", _pod_qty_cost)
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRequisitionPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        ElseIf fobject.name = "FReqTransferIssue" Then
            fobject._req_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")

            Dim ds_bantu As New DataSet
            Dim i As Integer
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        'ant 14 maret 2011
                        .SQL = "SELECT  " _
                            & "  reqd_oid, " _
                            & "  reqd_en_id,   " _
                            & "  en_desc, " _
                            & "  req_code, " _
                            & "  reqd_req_oid,   " _
                            & "  reqd_ptnr_id, " _
                            & "  ptnr_name, " _
                            & "  cmaddr_name, " _
                            & "  reqd_si_id, " _
                            & "  si_desc, " _
                            & "  reqd_pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_cost, " _
                            & "  pt_type, " _
                            & "  pt_ls, " _
                            & "  reqd_rmks, " _
                            & "  reqd_end_user, " _
                            & "  reqd_qty - coalesce(reqd_qty_completed,0) as qty_open, " _
                            & "  reqd_um, " _
                            & "  code_name, " _
                            & "  reqd_cost, " _
                            & "  reqd_disc, " _
                            & "  req_date, " _
                            & "  reqd_need_date, " _
                            & "  reqd_due_date, " _
                            & "  reqd_um_conv " _
                            & "FROM  " _
                            & "  public.reqd_det  " _
                            & "  inner join public.req_mstr on req_oid = reqd_req_oid " _
                            & "  inner join public.cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
                            & "  inner join public.en_mstr on en_id = reqd_en_id " _
                            & "  inner join public.ptnr_mstr on ptnr_id = reqd_ptnr_id " _
                            & "  inner join public.si_mstr on si_id = reqd_si_id " _
                            & "  inner join public.pt_mstr on pt_id = reqd_pt_id " _
                            & "  inner join public.code_mstr on code_id = reqd_um " _
                            & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and req_en_id = " + _en_id.ToString _
                            & " and reqd_qty - coalesce(reqd_qty_completed,0) > 0 " _
                            & " and req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'"
                        '----------------------------------------------
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "req")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                'ant 14 maret 2011
                'If ds_bantu.Tables(0).Rows(i).Item("pt_type") = "A" Then
                '    For j As Integer = 0 To ds_bantu.Tables(0).Rows(i).Item("qty_open") - 1
                '        _dtrow = fobject.ds_edit.Tables(0).NewRow
                '        _dtrow("reqds_oid") = Guid.NewGuid.ToString
                '        _dtrow("reqds_reqd_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_oid")
                '        _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("reqd_pt_id")
                '        _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                '        _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                '        _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                '        _dtrow("reqds_cost") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                '        _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                '        _dtrow("reqds_qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '        _dtrow("reqds_qty") = 1
                '        _dtrow("reqds_um") = ds_bantu.Tables(0).Rows(i).Item("reqd_um")
                '        _dtrow("reqds_um_name") = ds_bantu.Tables(0).Rows(i).Item("code_name")
                '        _dtrow("reqds_qty_real") = 1
                '        fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                '    Next
                'Else
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("reqds_oid") = Guid.NewGuid.ToString
                _dtrow("reqds_reqd_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("reqd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("reqds_cost") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("reqds_qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("reqds_qty") = 0 'agar mudah dalam hal insert data 'ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("reqds_um") = ds_bantu.Tables(0).Rows(i).Item("reqd_um")
                _dtrow("reqds_um_name") = ds_bantu.Tables(0).Rows(i).Item("code_name")
                _dtrow("reqds_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                'End If
                '-----------------------

            Next
            fobject.ds_edit.Tables(0).AcceptChanges()

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetTransferIssue" Then
            fobject._req_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")

            Dim ds_bantu As New DataSet
            Dim i As Integer
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb

                        .SQL = "SELECT  " _
                            & "  reqd_oid, " _
                            & "  reqd_req_oid, " _
                            & "  reqd_seq, " _
                            & "  reqd_pt_id,pt_code,pt_desc1,pt_desc2, " _
                            & "  reqd_rmks, " _
                            & "  reqd_qty, " _
                            & "  reqd_qty_processed, " _
                            & "  reqd_qty_completed, " _
                            & "  reqd_um,um.code_name as um, " _
                            & "  reqd_cost, " _
                            & "  reqd_disc, " _
                            & "  reqd_um_conv, " _
                            & "  reqd_qty_real, " _
                            & "  reqd_qty,  " _
                            & "  reqd_qty_processed,  " _
                            & "  reqd_qty_completed,  " _
                            & "  (reqd_qty - coalesce(reqd_qty_completed,0)) as reqd_qty_open, " _
                            & "  reqd_pt_class, " _
                            & "  reqd_status, " _
                            & "  reqd_emp_id,xemp_name, " _
                            & "  xemp_rg,rg.code_name as regional, " _
                            & "  xemp_dept,dept.code_name as department, " _
                            & "  xemp_div,div.code_name as divisi " _
                            & "FROM  " _
                            & "  public.reqd_det  " _
                            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = reqd_um " _
                            & "  left outer join xemp_mstr on xemp_id = reqd_emp_id " _
                            & "  left outer join code_mstr rg on rg.code_id = xemp_rg  " _
                            & "  left outer join code_mstr dept on dept.code_id = xemp_dept  " _
                            & "  left outer join code_mstr div on div.code_id = xemp_div  " _
                            & "  where pt_type = 'A' " _
                            & "  and (reqd_qty - coalesce(reqd_qty_completed,0)) > 0 " _
                            & "  and reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "req")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("reqd_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_oid")
                _dtrow("reqd_req_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_req_oid")
                _dtrow("reqd_pt_id") = ds_bantu.Tables(0).Rows(i).Item("reqd_pt_id")
                '_dtrow("reqds_reqd_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_oid")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                '_dtrow("reqds_ass_id") = ds_bantu.Tables(0).Rows(i).Item("ass_id")
                '_dtrow("ass_code") = ds_bantu.Tables(0).Rows(i).Item("rcvds_lot_serial")
                '_dtrow("pt_cost") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                _dtrow("reqd_qty_open") = ds_bantu.Tables(0).Rows(i).Item("reqd_qty_open")
                _dtrow("reqd_qty_assign") = ds_bantu.Tables(0).Rows(i).Item("reqd_qty_open")
                _dtrow("reqd_um") = ds_bantu.Tables(0).Rows(i).Item("reqd_um")
                _dtrow("um") = ds_bantu.Tables(0).Rows(i).Item("um")
                _dtrow("reqd_emp_id") = ds_bantu.Tables(0).Rows(i).Item("reqd_emp_id")
                _dtrow("xemp_name") = ds_bantu.Tables(0).Rows(i).Item("xemp_name")
                _dtrow("xemp_dept") = ds_bantu.Tables(0).Rows(i).Item("xemp_dept")
                _dtrow("department") = ds_bantu.Tables(0).Rows(i).Item("department")
                _dtrow("xemp_rg") = ds_bantu.Tables(0).Rows(i).Item("xemp_rg")
                _dtrow("regional") = ds_bantu.Tables(0).Rows(i).Item("regional")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
            'ant 16 mei 2011
        ElseIf fobject.name = "FInstalationStart" Then
            fobject._req_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")
            fobject._ssc_loc_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ssc_loc_id")
            fobject.loc_desc.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("loc_desc")

            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")

            Dim ds_bantu As New DataSet
            Dim i As Integer
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  reqd_oid, " _
                            & "  reqd_en_id,   " _
                            & "  en_desc, " _
                            & "  req_code, " _
                            & "  reqd_req_oid,   " _
                            & "  reqd_ptnr_id, " _
                            & "  ptnr_name, " _
                            & "  cmaddr_name, " _
                            & "  reqd_si_id, " _
                            & "  si_desc, " _
                            & "  reqd_pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_cost, " _
                            & "  pt_type, " _
                            & "  pt_ls, " _
                            & "  reqd_rmks, " _
                            & "  reqd_end_user, " _
                            & "  reqd_qty_real - coalesce(reqd_qty_completed,0) as qty_open, " _
                            & "  reqd_um, " _
                            & "  code_name, " _
                            & "  reqd_cost, " _
                            & "  reqd_disc, " _
                            & "  req_date, " _
                            & "  reqd_need_date, " _
                            & "  reqd_due_date, " _
                            & "  reqd_um_conv " _
                            & "FROM  " _
                            & "  public.reqd_det  " _
                            & "  inner join public.req_mstr on req_oid = reqd_req_oid " _
                            & "  inner join public.cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
                            & "  inner join public.en_mstr on en_id = reqd_en_id " _
                            & "  inner join public.ptnr_mstr on ptnr_id = reqd_ptnr_id " _
                            & "  inner join public.si_mstr on si_id = reqd_si_id " _
                            & "  inner join public.pt_mstr on pt_id = reqd_pt_id " _
                            & "  inner join public.code_mstr on code_id = reqd_um " _
                            & " where req_en_id = " + _en_id.ToString _
                            & " and reqd_qty_real - coalesce(reqd_qty_completed,0) > 0 " _
                            & " and req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "req")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                'ant 14 maret 2011
                'If ds_bantu.Tables(0).Rows(i).Item("pt_type") = "A" Then
                '    For j As Integer = 0 To ds_bantu.Tables(0).Rows(i).Item("qty_open") - 1
                '        _dtrow = fobject.ds_edit.Tables(0).NewRow
                '        _dtrow("reqds_oid") = Guid.NewGuid.ToString
                '        _dtrow("reqds_reqd_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_oid")
                '        _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("reqd_pt_id")
                '        _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                '        _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                '        _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                '        _dtrow("reqds_cost") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                '        _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                '        _dtrow("reqds_qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '        _dtrow("reqds_qty") = 1
                '        _dtrow("reqds_um") = ds_bantu.Tables(0).Rows(i).Item("reqd_um")
                '        _dtrow("reqds_um_name") = ds_bantu.Tables(0).Rows(i).Item("code_name")
                '        _dtrow("reqds_qty_real") = 1
                '        fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                '    Next
                'Else
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("reqds_oid") = Guid.NewGuid.ToString
                _dtrow("reqds_reqd_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("reqd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("reqds_cost") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("reqds_qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("reqds_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("reqds_um") = ds_bantu.Tables(0).Rows(i).Item("reqd_um")
                _dtrow("reqds_um_name") = ds_bantu.Tables(0).Rows(i).Item("code_name")
                _dtrow("reqds_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                'End If
                '-----------------------

            Next
            fobject.ds_edit.Tables(0).AcceptChanges()

            fobject.gv_edit.BestFitColumns()
            '-------------------------------------------------
        End If
    End Sub
End Class
