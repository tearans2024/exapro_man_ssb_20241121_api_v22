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
        Else
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
            get_sequel = "SELECT  " _
                        & "  req_oid, " _
                        & "  req_code, " _
                        & "  req_date " _
                        & "  FROM  " _
                        & "  public.req_mstr " _
                        & "  where req_en_id = " + _en_id.ToString _
                        & "  and req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by req_code  "
        ElseIf fobject.name = "FRequisition" Then
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

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and req_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by req_code  "
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
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPurchaseOrder" Then
            Dim _pod_qty_cost, _qty_open, _reqd_cost, _reqd_disc As Double

            fobject.gv_edit.SetRowCellValue(_row, "pod_reqd_oid", ds.Tables(0).Rows(_row_gv).Item("reqd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "req_code_relation", ds.Tables(0).Rows(_row_gv).Item("req_code"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_si_id", ds.Tables(0).Rows(_row_gv).Item("reqd_si_id"))
            fobject.gv_edit.SetRowCellValue(_row, "si_desc", ds.Tables(0).Rows(_row_gv).Item("si_desc"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_id", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("reqd_pt_desc2"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_rmks", ds.Tables(0).Rows(_row_gv).Item("reqd_rmks"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_end_user", ds.Tables(0).Rows(_row_gv).Item("reqd_end_user"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("reqd_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("reqd_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_disc", ds.Tables(0).Rows(_row_gv).Item("reqd_disc"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_need_date", ds.Tables(0).Rows(_row_gv).Item("reqd_need_date"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_due_date", ds.Tables(0).Rows(_row_gv).Item("reqd_due_date"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_um_conv", ds.Tables(0).Rows(_row_gv).Item("reqd_um_conv"))

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
                            & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and req_en_id = " + _en_id.ToString _
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
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("reqds_oid") = Guid.NewGuid.ToString
                _dtrow("reqds_reqd_oid") = ds_bantu.Tables(0).Rows(i).Item("reqd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("reqd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_cost") = ds_bantu.Tables(0).Rows(i).Item("pt_cost")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("reqds_qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("reqds_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("reqds_um") = ds_bantu.Tables(0).Rows(i).Item("reqd_um")
                _dtrow("reqds_um_name") = ds_bantu.Tables(0).Rows(i).Item("code_name")
                _dtrow("reqds_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
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
                        '.SQL = "SELECT  " _
                        '    & "  rcvds_oid, " _
                        '    & "  rcvds_rcvd_oid, " _
                        '    & "  rcvd_pod_oid,rcvd_rcv_oid,pod_reqd_oid,reqd_req_oid,reqd_oid, " _
                        '    & "  pod_pt_id,pt_code,pod_pt_desc1,pod_pt_desc2,pt_cost, " _
                        '    & "  pod_emp_id,xemp_name, " _
                        '    & "  rcvds_qty, " _
                        '    & "  rcvds_um,um.code_name as um, " _
                        '    & "  rcvds_si_id, " _
                        '    & "  rcvds_loc_id, " _
                        '    & "  rcvds_lot_serial,ass_id, " _
                        '    & "  rcvds_dt " _
                        '    & "FROM  " _
                        '    & "  public.rcvds_serial  " _
                        '    & "  inner join code_mstr um on um.code_id = rcvds_um " _
                        '    & "  inner join rcvd_det on rcvd_oid = rcvds_rcvd_oid " _
                        '    & "  inner join pod_det on pod_oid = rcvd_pod_oid  " _
                        '    & "  inner join pt_mstr on pt_id = pod_pt_id  " _
                        '    & "  inner join reqd_det on reqd_oid = pod_reqd_oid " _
                        '    & "  inner join xemp_mstr on xemp_id = pod_emp_id " _
                        '    & "  inner join ass_mstr on ass_code = rcvds_lot_serial " _
                        '    & "  where reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") + "'" _
                        '    & "  and ass_qty_assgn = 0 and ass_confirm = 'Y' "
                        '.InitializeCommand()
                        '.FillDataSet(ds_bantu, "req")

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
        End If
    End Sub
End Class
