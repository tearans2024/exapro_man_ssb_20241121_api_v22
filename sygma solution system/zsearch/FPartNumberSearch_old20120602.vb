Imports master_new.ModFunction

Public Class FPartNumberSearch
    Public _row, _en_id, _si_id As Integer
    Public _obj, _obj_loc, _obj_um, _obj_cost As Object
    Public _so_type As String, _vat_free As String
    Public _tran_oid As String = ""
    Dim func_data As New function_data
    Public grid_call As String = ""
    Public _qty_receive As Double
    Public _pt_id As Integer

    Private Sub FPartNumberSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  coalesce(pt_desc2,'') as pt_desc2, " _
                    & "  pt_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  um_mstr.code_name as um_name,coalesce(sct_total,0) as standard_cost " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " left OUTER join sct_mstr on ( sct_pt_id=pt_id and sct_en_id=en_id and sct_cs_id = (SELECT r.cs_id FROM public.cs_mstr r WHERE r.cs_type = 'G')) " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_en_id in (0," + _en_id.ToString + ")"

        If fobject.name = "" Then
            get_sequel = get_sequel + ""
        End If

        If fobject.name = "FPurchaseOrder" Then
            'rev by hendrik 2011-06-18 -----------------------
        ElseIf fobject.name = "FPurchaseOrder_NonBudget" Then

        ElseIf fobject.name = "FPurchaseOrderExpense" Then
            'get_sequel = get_sequel + " and pt_type ~~* 'E' " 
            'gak jadi 20110705 karena po dibagi 2 aja..po project dan po office
            '-------------------------------------------------
        ElseIf fobject.name = "FPaymentOrder" Then
        ElseIf fobject.name = "FRequisition" Then
        ElseIf fobject.name = "FInventoryRequest" Or fobject.name = FSiteCost.Name Or fobject.name = FSiteCostAccount.Name _
        Or fobject.name = FGenerateSiteCost.Name Or fobject.name = FRollUpPSCost.Name _
        Or fobject.name = FRollUpRoutingCost.Name Or fobject.name = FCopySiteCost.Name Or fobject.name = FCopyRouting.Name Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FPriceList" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryReceipts" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryIssues" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FTransferIssues" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FAssetTransferIssue" Then
            get_sequel = get_sequel + "and pt_type ~~* 'A'"
        ElseIf fobject.name = "FSalesOrder" Then
        ElseIf fobject.name = FTransferIssuesWIP.Name Or fobject.name = FWIPIssues.Name Then
            get_sequel = get_sequel + " and pt_id in (SELECT  " _
                    & "  a.invw_pt_id " _
                    & "FROM " _
                    & "  public.invw_wip a " _
                    & "WHERE " _
                    & "  a.invw_en_id = " & _en_id & " AND  " _
                    & "  a.invw_dom_id = " & master_new.ClsVar.sdom_id & " AND  " _
                    & "  a.invw_wo_oid = " & SetSetring(_tran_oid) & " AND  " _
                    & "  a.invw_wc_id = " & _pt_id & ")"
            'ant 17 maret 2011
        ElseIf fobject.name = "FReqTransferIssue" Then
            get_sequel = get_sequel + "and (pt_type ~~* 'I' or pt_type ~~* 'A')"
            '-----------------------------
        ElseIf fobject.name = FPartnumberSubtitute.Name Then
            get_sequel = get_sequel + " and pt_type ~~* 'I' "
        ElseIf fobject.name = FWOBillMaintenance.Name Or fobject.name = FBillOfQuantity.Name Then
            get_sequel = get_sequel + " and pt_type ~~* 'I' "
        ElseIf fobject.name = FWorkInProgress.Name Then
            Dim sSQL As String
            sSQL = "SELECT  " _
              & "  b.pt_id, " _
              & "  b.pt_code, " _
              & "  b.pt_desc1, " _
              & "  b.pt_desc2, " _
              & "  b.pt_um , " _
              & "  c.code_code as um_desc,a.psd_qty " _
              & "FROM " _
              & "  public.psd_det a " _
              & "  INNER JOIN public.pt_mstr b ON (a.psd_pt_bom_id = b.pt_id) " _
              & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
              & "  INNER JOIN public.ps_mstr d ON (a.psd_ps_oid = d.ps_oid) " _
              & "WHERE " _
              & "  d.ps_pt_bom_id=" & _pt_id

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)
            Dim _pt_id_to As String = ""
            For Each dr As DataRow In dt.Rows
                _pt_id_to += dr("pt_id") & ","
            Next
            _pt_id_to = _pt_id_to.Substring(0, Microsoft.VisualBasic.Len(_pt_id_to) - 1)
            get_sequel = get_sequel + " and pt_id in (" & _pt_id_to & ")"
        ElseIf fobject.name = FProjectMaintenance.Name Then
            get_sequel = "SELECT  " _
                        & "  en_id, " _
                        & "  en_desc, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  coalesce(pt_desc2,'') as pt_desc2, " _
                        & "  pt_cost, " _
                        & "  pt_price, " _
                        & "  pt_type, " _
                        & "  pt_um, " _
                        & "  pt_pl_id, " _
                        & "  pt_ls, " _
                        & "  pt_loc_id, " _
                        & "  loc_desc, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  coalesce(sct_total,0) as sct_total, " _
                        & "  coalesce(sct_price,0) as sct_price " _
                        & "FROM  " _
                        & "  public.pt_mstr" _
                        & " inner join en_mstr on en_id = pt_en_id " _
                        & " inner join loc_mstr on loc_id = pt_loc_id " _
                        & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                        & " left outer join sct_mstr on sct_pt_id = pt_id " _
                        & " left outer join cs_mstr on cs_id = sct_cs_id " _
                        & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and en_active ~~* 'Y'" _
                        & " and cs_name ~~* 'standard'" _
                        & " and pt_vat_free ~~* " + SetBitYN(_vat_free) _
                        & " and pt_en_id in (0," + _en_id.ToString + ")" _
                        & " union " _
                        & "SELECT  " _
                        & "  en_id, " _
                        & "  en_desc, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  coalesce(pt_desc2,'') as pt_desc2, " _
                        & "  pt_cost, " _
                        & "  pt_price, " _
                        & "  pt_type, " _
                        & "  pt_um, " _
                        & "  pt_pl_id, " _
                        & "  pt_ls, " _
                        & "  pt_loc_id, " _
                        & "  loc_desc, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  0 as sct_total, " _
                        & "  0 as sct_price " _
                        & "FROM  " _
                        & "  public.pt_mstr" _
                        & " inner join en_mstr on en_id = pt_en_id " _
                        & " inner join loc_mstr on loc_id = pt_loc_id " _
                        & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                        & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                        & " and en_active ~~* 'Y'" _
                        & " and pt_type ~~* 'E' " _
                        & " and pt_vat_free ~~* " + SetBitYN(_vat_free) _
                        & " and pt_en_id in (0," + _en_id.ToString + ")"
        End If

        get_sequel = get_sequel + " order by pt_code "

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    
    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrder_NonBudget" Or fobject.name = "FPurchaseOrderExpense" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))

            If fobject.name = "FPurchaseOrder" And _obj.ToString = "" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
                fobject.gv_edit.SetRowCellValue(_row, "pod_standard_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            Else
                fobject.gv_edit.SetRowCellValue(_row, "pod_cost", 0)
                fobject.gv_edit.SetRowCellValue(_row, "pod_standard_cost", 0)
            End If

            'fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_standard_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))

            fobject.gv_edit.SetRowCellValue(_row, "pod_variance", 0)
            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            If ds.Tables(0).Rows(_row_gv).Item("pt_type").ToString.ToUpper = "E" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "M")
            Else
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "")
            End If

            'fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type")) sudah ada diatas
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))

            'fobject.gv_edit.SetRowCellValue(_row, "pod_taxable", ds.Tables(0).Rows(_row_gv).Item("pt_taxable"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_tax_inc", ds.Tables(0).Rows(_row_gv).Item("pt_tax_inc"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class", ds.Tables(0).Rows(_row_gv).Item("pt_tax_class"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("tax_class_name"))

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPaymentOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            If ds.Tables(0).Rows(_row_gv).Item("pt_type").ToString.ToUpper = "E" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "M")
            Else
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "")
            End If

            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FInventoryRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pbd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRequisition" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRequisitionGoods" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRequisitionEquipment" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPriceList" Then
            fobject.gv_edit_item.SetRowCellValue(_row, "pid_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit_item.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit_item.BestFitColumns()
        ElseIf fobject.name = "FInventoryCycleCount" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            _obj_loc.EditValue = ds.Tables(0).Rows(_row_gv).Item("pt_loc_id")
            _obj_um.EditValue = ds.Tables(0).Rows(_row_gv).Item("pt_um")
            _obj_cost.EditValue = ds.Tables(0).Rows(_row_gv).Item("standard_cost")
            fobject._pt_id_global = ds.Tables(0).Rows(_row_gv).Item("pt_id")
        ElseIf fobject.name = "FInventoryReceipts" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryReceiptsNew" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "invrcpd_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "invrcpd_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "invrcpd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "invrcpd_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssues" Then
            'dt_bantu = (func_coll.get_prodline_account(ds.Tables(0).Rows(_row_gv).Item("pt_pl_id"), "WO_COP-"))

            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            'fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", dt_bantu.Rows(0).Item("pla_ac_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "ac_code", dt_bantu.Rows(0).Item("ac_code"))
            'fobject.gv_edit.SetRowCellValue(_row, "ac_name", dt_bantu.Rows(0).Item("ac_name"))
            'fobject.gv_edit.SetRowCellValue(_row, "riud_sb_id", dt_bantu.Rows(0).Item("pla_sb_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "sb_desc", "-")
            'fobject.gv_edit.SetRowCellValue(_row, "riud_cc_id", dt_bantu.Rows(0).Item("pla_cc_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "cc_desc", "-")

            If _tran_oid.ToString <> "" Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_pbd_oid", ds.Tables(0).Rows(_row_gv).Item("pbd_oid"))
            End If

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FTransferIssues" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Or fobject.name = "FSalesOrderProject" Then
            Dim ds_bantu As New DataSet

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                            & "From pla_mstr  " _
                                            & "inner join ac_mstr on ac_id = pla_ac_id " _
                                            & "inner join sb_mstr on sb_id = pla_sb_id " _
                                            & "inner join cc_mstr on cc_id = pla_cc_id " _
                                            & "where pla_pl_id = " + ds.Tables(0).Rows(_row_gv).Item("pt_pl_id").ToString _
                                            & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "prodline")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.gv_edit.SetRowCellValue(_row, "sod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            'fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))

            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

            fobject.gv_edit.SetRowCellValue(_row, "sod_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FCreateInvoice" Then
            fobject.gv_edit.SetRowCellValue(_row, "arinvd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetTransferIssue" Then

            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSoPj" Then
            Dim ds_bantu As New DataSet

            fobject.gv_edit.SetRowCellValue(_row, "sopjd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
            fobject.gv_edit.BestFitColumns()

            'rev by hendrik 2011-02-20 ==================================================================
        ElseIf fobject.name = "FProjectMaintenance" Then
            If grid_call = "gv_edit" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                'fobject.gv_edit.SetRowCellValue(_row, "sopjd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
                'fobject.gv_edit.SetRowCellValue(_row, "sopjd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_cost", ds.Tables(0).Rows(_row_gv).Item("sct_total"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_price", ds.Tables(0).Rows(_row_gv).Item("sct_price"))
            ElseIf grid_call = "gv_edit_cust" Then
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                'fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                fobject.gv_edit_cust.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            End If
            '==================================================================
        ElseIf fobject.name = "FBussinessPlan" Then
            Dim ds_bantu As New DataSet

            fobject.gv_edit.SetRowCellValue(_row, "bpd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "bpd_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "bpd_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "bpd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "bpd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.BestFitColumns()
            'ant 17 maret 2011
        ElseIf fobject.name = "FReqTransferIssue" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_qty_open", 0)
            fobject.gv_edit.SetRowCellValue(_row, "reqds_qty", 0)
            fobject.gv_edit.BestFitColumns()
            '-------------------------------------
        ElseIf fobject.name = FPartnumberSubtitute.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
        ElseIf fobject.name = FWOBillMaintenance.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wod_pt_bom_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "wod_qty_per", 0.0)
            fobject.gv_edit.SetRowCellValue(_row, "wod_qty_req", 0.0)
        ElseIf fobject.name = FBillOfQuantity.Name Then
            fobject.gv_stand_edit.SetRowCellValue(_row, "boqs_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_stand_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_stand_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_stand_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_stand_edit.SetRowCellValue(_row, "code_code", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_stand_edit.SetRowCellValue(_row, "boqs_qty", 0.0)
            fobject.gv_stand_edit.SetRowCellValue(_row, "boqs_is_manual", "Y")
            'ant 16 mei 2011
        ElseIf fobject.name = "FInstalationRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
            '------------------------------------------
        ElseIf fobject.name = FWorkInProgress.Name Then
            Dim sSQL As String
            sSQL = "SELECT  " _
                & "  b.pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  b.pt_um , " _
                & "  c.code_code as um_desc,a.psd_qty " _
                & "FROM " _
                & "  public.psd_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.psd_pt_bom_id = b.pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                & "  INNER JOIN public.ps_mstr d ON (a.psd_ps_oid = d.ps_oid) " _
                & "WHERE " _
                & "  b.pt_id=" & ds.Tables(0).Rows(_row_gv).Item("pt_id")
            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            fobject.gv_edit_wip.SetRowCellValue(_row, "wimd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "wimd_pt_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "um_desc", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "wimd_qty_base", dt.Rows(0).Item("psd_qty"))
            fobject.gv_edit_wip.SetRowCellValue(_row, "wimd_qty_issue", dt.Rows(0).Item("psd_qty") * _qty_receive)
            fobject.gv_edit_wip.BestFitColumns()
        ElseIf fobject.name = FTransferIssuesWIP.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wimtrd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "wimtrd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um_desc", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "wimtrd_qty", 1)
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FWIPIssues.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "wimisd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "wimisd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um_desc", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "wimisd_qty", 1)
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FSiteCost.Name Then
            If _obj.name = "pt_id_from" Then
                fobject.pt_id_from.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            ElseIf _obj.name = "pt_id_to" Then
                fobject.pt_id_to.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            Else
                fobject.sct_pt_id.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.sct_pt_id.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject.pt_desc1.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.pt_desc2.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
            End If
        ElseIf fobject.name = FSiteCostAccount.Name Then
            If _obj.name = "pt_id_from" Then
                fobject.pt_id_from.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            ElseIf _obj.name = "pt_id_to" Then
                fobject.pt_id_to.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            End If
        ElseIf fobject.name = FGenerateSiteCost.Name Then
            If _obj.name = "pt_id_from" Then
                fobject.pt_id_from.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject.pt_desc1_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.pt_desc2_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
            Else
                fobject.pt_id_to.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject.pt_desc1_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.pt_desc2_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
            End If
        ElseIf fobject.name = FGenerateSiteCost.Name Or fobject.name = FRollUpPSCost.Name _
        Or fobject.name = FRollUpRoutingCost.Name Or fobject.name = FCopySiteCost.Name Or fobject.name = FCopyRouting.Name Then
            If _obj.name = "pt_id_from" Then
                fobject.pt_id_from.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject.pt_desc1_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.pt_desc2_from.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
            Else
                fobject.pt_id_to.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
                fobject.pt_id_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                fobject.pt_desc1_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                fobject.pt_desc2_to.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
            End If
        ElseIf fobject.name = FRouting.Name Then
            fobject.ro_pt_id.tag = ds.Tables(0).Rows(_row_gv).Item("pt_id")
            fobject.ro_pt_id.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            fobject.pt_desc1.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.pt_desc2.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
        ElseIf fobject.name = "FQualityControl" Then
            fobject.gv_edit.SetRowCellValue(_row, "qcd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "qcd_cost", ds.Tables(0).Rows(_row_gv).Item("standard_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "qcd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "qcd_qty", 1)
            fobject.gv_edit.SetRowCellValue(_row, "qcd_qty_pass", 0)
            fobject.gv_edit.SetRowCellValue(_row, "qcd_qty_reject", 0)
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
