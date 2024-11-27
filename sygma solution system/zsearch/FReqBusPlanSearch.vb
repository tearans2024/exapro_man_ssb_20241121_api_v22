Imports master_new.ModFunction

Public Class FReqBusPlanSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value_req, _conf_value_bplan As String
    Dim _now As DateTime

    Private Sub FReqBusPlanSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rg_search_type.EditValue = "bplan"
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value_req = func_coll.get_conf_file("wf_requisition")
        _conf_value_bplan = func_coll.get_conf_file("wf_bussiness_plan")
    End Sub

    Public Overrides Sub format_grid()
        If rg_search_type.EditValue = "req" Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Date", "req_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Req. Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        ElseIf rg_search_type.EditValue = "bplan" Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Bussiness Plan Number", "bp_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Start Date", "bp_start_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "End Date", "bp_end_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "bpd_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "bpd_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
            add_column(gv_master, "Cost", "bpd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
            add_column(gv_master, "Remarks", "bpd_remarks", DevExpress.Utils.HorzAlignment.Default)
        End If
        

    End Sub

    Private Sub clear_column_gridview()

        gv_master.Columns.Remove(gv_master.Columns.Item(10))
        gv_master.Columns.Remove(gv_master.Columns.Item(9))
        gv_master.Columns.Remove(gv_master.Columns.Item(8))
        gv_master.Columns.Remove(gv_master.Columns.Item(7))
        gv_master.Columns.Remove(gv_master.Columns.Item(6))
        gv_master.Columns.Remove(gv_master.Columns.Item(5))
        gv_master.Columns.Remove(gv_master.Columns.Item(4))
        gv_master.Columns.Remove(gv_master.Columns.Item(3))
        gv_master.Columns.Remove(gv_master.Columns.Item(2))
        gv_master.Columns.Remove(gv_master.Columns.Item(1))
        gv_master.Columns.Remove(gv_master.Columns.Item(0))

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        clear_column_gridview()
        format_grid()

        If fobject.name = "FRequisition" Then
            If rg_search_type.EditValue = "req" Then

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
                        & "  inner join public.pt_mstr on pt_id = reqd_pt_id " _
                        & "  inner join public.code_mstr on code_id = reqd_um " _
                        & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & " and req_en_id = " + _en_id.ToString _
                        & " and reqd_qty_real - coalesce(reqd_qty_processed,0) > 0 " _
                        & " and coalesce(reqd_status,'') = ''"

                If _conf_value_req = "1" Then
                    get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
                End If

                get_sequel = get_sequel + " order by req_code  "

            ElseIf rg_search_type.EditValue = "bplan" Then
                get_sequel = "SELECT  " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.bp_mstr.bp_oid, " _
                        & "  public.bp_mstr.bp_upd_date, " _
                        & "  public.bp_mstr.bp_upd_by, " _
                        & "  public.bp_mstr.bp_add_date, " _
                        & "  public.bp_mstr.bp_add_by, " _
                        & "  public.bp_mstr.bp_code, " _
                        & "  public.bp_mstr.bp_date, " _
                        & "  public.bp_mstr.bp_start_date, " _
                        & "  public.bp_mstr.bp_end_date, " _
                        & "  public.bp_mstr.bp_remarks, " _
                        & "  tran_name, " _
                        & "  public.bpd_det.bpd_oid, " _
                        & "  public.bpd_det.bpd_bp_oid, " _
                        & "  public.bpd_det.bpd_seq, " _
                        & "  public.bpd_det.bpd_pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.bpd_det.bpd_desc2, " _
                        & "  public.bpd_det.bpd_desc1, " _
                        & "  public.bpd_det.bpd_remarks, " _
                        & "  public.bpd_det.bpd_qty, " _
                        & "  public.bpd_det.bpd_qty_consume, " _
                        & "  (bpd_det.bpd_qty - coalesce(bpd_det.bpd_qty_consume,0)) as qty_open, " _
                        & "  public.bpd_det.bpd_um, " _
                        & "  public.code_mstr.code_name, " _
                        & "  bpd_det.bpd_cost " _
                        & "FROM " _
                        & "  public.bp_mstr " _
                        & "  INNER JOIN public.bpd_det ON (public.bpd_det.bpd_bp_oid = public.bp_mstr.bp_oid) " _
                        & "  INNER JOIN public.en_mstr ON (public.bp_mstr.bp_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.tran_mstr ON (public.bp_mstr.bp_tran_id = public.tran_mstr.tran_id) " _
                        & "  INNER JOIN public.pt_mstr ON (public.bpd_det.bpd_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.bpd_det.bpd_um = public.code_mstr.code_id) " _
                           & " where bp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                           & " and bp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                           & " and bp_en_id = " + _en_id.ToString _
                           & " and (bpd_det.bpd_qty - coalesce(bpd_det.bpd_qty_consume,0)) > 0 " _

                If _conf_value_bplan = "1" Then
                    get_sequel = get_sequel + " and bp_trans_id ~~* 'I' "
                End If

                get_sequel = get_sequel + " order by bp_code  "
            End If

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
            Dim _qty_open As Double

            If rg_search_type.EditValue = "req" Then
                Dim _reqd_qty_cost, _reqd_cost, _reqd_disc As Double

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

            ElseIf rg_search_type.EditValue = "bplan" Then
                Dim _bpd_qty_cost, _bpd_cost, _bpd_disc As Double

                fobject.gv_edit.SetRowCellValue(_row, "reqd_related_oid", ds.Tables(0).Rows(_row_gv).Item("bpd_oid"))
                fobject.gv_edit.SetRowCellValue(_row, "req_code_relation", ds.Tables(0).Rows(_row_gv).Item("bp_code"))

                fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("bpd_pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("bpd_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("bpd_desc2"))

                fobject.gv_edit.SetRowCellValue(_row, "reqd_rmks", ds.Tables(0).Rows(_row_gv).Item("bpd_remarks"))
                fobject.gv_edit.SetRowCellValue(_row, "reqd_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))

                fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("bpd_um"))
                fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))

                fobject.gv_edit.SetRowCellValue(_row, "reqd_cost", ds.Tables(0).Rows(_row_gv).Item("bpd_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "reqd_disc", 0)

                fobject.gv_edit.SetRowCellValue(_row, "reqd_um_conv", 1)

                _qty_open = ds.Tables(0).Rows(_row_gv).Item("qty_open")
                _bpd_cost = ds.Tables(0).Rows(_row_gv).Item("bpd_cost")
                _bpd_disc = 0
                _bpd_qty_cost = (_qty_open * _bpd_cost) - (_qty_open * _bpd_cost * _bpd_disc)

                fobject.gv_edit.SetRowCellValue(_row, "bpd_qty_cost", _bpd_qty_cost)
                fobject.gv_edit.SetRowCellValue(_row, "reqd_related_type", "B")
                fobject.gv_edit.BestFitColumns()

            End If
        End If
    End Sub
End Class
