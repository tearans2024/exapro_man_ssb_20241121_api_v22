Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FWOMobileRecieptNew
    Dim ssql As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public __qty_outstanding As Double
    Public _pjc_id As Integer
    Public _cost As Double
    Public ds_edit As DataSet
    Dim ssqls As New ArrayList

    Private Sub FWOMobileRecieptNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        Me.RelDate.DateTime = Now

        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Private Sub wor_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "wor_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO. Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO. Receipt Code", "wor_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Code", "lbrf_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Date", "wor_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Effective Date", "wor_date_eff", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")

        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "WO Cost", "wor_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Item Cost", "wo_ext_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Real Cost (Qty)", "wor_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        add_column_copy(gv_master, "Qty Complete", "wor_qty_comp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Qty Reject", "wor_qty_reject", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "Qty Inspection", "wor_qty_qc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Unit Measure", "unit_measure", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location To", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'If _conf_value = "1" Then
        '    add_column_copy(gv_master, "Work Center From", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        'End If

        add_column_copy(gv_master, "Remarks", "wor_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "wor_close", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "wor_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wor_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "wor_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wor_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        'add_column(gv_detail_serial, "wors_oid", False)
        'add_column_copy(gv_detail_serial, "Serial/Lot Number", "wors_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_serial, "Qty Serial", "wors_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")


        'add_column(gv_serial, "wors_oid", False)
        'add_column_edit(gv_serial, "Lot/Serial Number", "wors_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_serial, "Qty", "wors_qty", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "WO Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Wo Type", "wo_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "BOQ Code", "boq_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Reference Rework", "wo_ref_rework", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Part Number WO", "pt_code_bom", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description1", "pt_desc1_bom", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description2", "pt_desc2_bom", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Qty", "wo_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "Yield Percent", "wo_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_master, "Qty Prod", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Project Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Project Date", "pjc_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sold to", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_master, "wo_ro_id", False)
        'add_column_copy(gv_master, "Routing", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Routing", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status", "wo_status", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Remark", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "Componect Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Componect Description", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Indirect", "psd_indirect", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_detail, "Insheet", "psd_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Sequence", "psd_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")

        'add_column_copy(gv_routing, "Workcenter", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Description", "rod_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_routing, "Insheet", "rod_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_routing, "Run", "rod_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        'add_column_copy(gv_routing, "Qty Out", "rod_conversion", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  false as pilih, " _
                & "  a.wor_oid, " _
                & "  a.wor_en_id, " _
                & "  e.en_desc, " _
                & "  a.wor_add_by, " _
                & "  a.wor_add_date, " _
                & "  a.wor_upd_by, " _
                & "  a.wor_upd_date, " _
                & "  a.wor_code, " _
                & "  a.wor_date, " _
                & "  a.wor_date_eff, " _
                & "  a.wor_wo_id, " _
                & "  b.wo_code, " _
                & "  b.wo_remarks, " _
                & "  a.wor_loc_id, " _
                & "  f.loc_code, " _
                & "  f.loc_desc, " _
                & "  a.wor_si_id, " _
                & "  g.si_code, " _
                & "  g.si_desc, " _
                & "  coalesce(a.wor_qty_comp,0) as wor_qty_comp, " _
                & "  coalesce(a.wor_qty_reject,0) as wor_qty_reject, " _
                & "  a.wor_remarks, " _
                & "  b.wo_remarks, " _
                & "  c.pt_code, " _
                & "  c.pt_id, " _
                & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
                & "  c.pt_um, " _
                & "  d.code_name AS unit_measure, " _
                & "  wo_pjc_id, " _
                & "  a.wor_close, a.wor_is_mobile, a.wor_rcv_status, pjc_code,wor_cost,wor_ext_cost,wo_cost,wo_ext_cost,wo_pt_id,en_code, " _
                & "  (coalesce(a.wor_qty_comp,0) + coalesce(a.wor_qty_reject,0)) * wo_ext_cost as wor_qty_cost " _
                & "FROM " _
                & "  public.wor_mstr a " _
                & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
                & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
                & "  LEFT OUTER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
                & "  INNER JOIN public.loc_mstr f ON (a.wor_loc_id = f.loc_id) " _
                & "  INNER JOIN public.si_mstr g ON (a.wor_si_id = g.si_id) " _
                & "  LEFT OUTER JOIN public.pjc_mstr ON pjc_id = wo_pjc_id " _
                & " Where wor_date between " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and  " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and  a.wor_is_mobile = 'Y' " _
                & "  and  a.wor_rcv_status = 'N' " _
                & " AND a.wor_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " ORDER BY wor_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
    End Sub

    Public Overrides Function insert() As Boolean
        Box("This menu is not available")
        Return False
        Exit Function
    End Function

    Public Overrides Function edit_data() As Boolean
        Box("This menu is not available")
        Return False
        Exit Function
    End Function

    Public Overrides Function edit()
        Return True
    End Function

    Public Overrides Function delete_data() As Boolean
        Return True
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = False

        Return before_save
    End Function

    Public Sub show_detail()
        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim sql As String

        'Try
        '    ds.Tables("detail").Clear()
        'Catch ex As Exception
        'End Try
        'sql = "SELECT  " _
        '          & "  psd_det.psd_oid, " _
        '          & "  psd_det.psd_ps_oid, " _
        '          & "  psd_det.psd_use_bom, " _
        '          & "  psd_det.psd_pt_bom_id, " _
        '          & "  psd_det.psd_comp, " _
        '          & "  psd_det.psd_ref,psd_indirect, " _
        '          & "  psd_det.psd_desc, " _
        '          & "  psd_det.psd_start_date, " _
        '          & "  psd_det.psd_end_date, " _
        '          & "  psd_det.psd_qty,psd_det.psd_qty_plan,psd_det.psd_qty_variance, " _
        '          & "  psd_det.psd_str_type, " _
        '          & "  psd_det.psd_insheet_pct, " _
        '          & "  psd_det.psd_lt_off, " _
        '          & "  psd_det.psd_op,op_name, " _
        '          & "  psd_det.psd_seq, " _
        '          & "  psd_det.psd_fcst_pct, " _
        '          & "  psd_det.psd_group, " _
        '          & "  psd_det.psd_process, " _
        '          & "  pt_code, " _
        '          & "  coalesce(pt_desc1,'') || coalesce(pt_desc2,'') as pt_desc, " _
        '          & "  code_mstr_group.code_en_id AS code_group_en_id, " _
        '          & "  code_mstr_group.code_id AS code_group_id, " _
        '          & "  code_mstr_group.code_field AS code_group_field, " _
        '          & "  code_mstr_group.code_code AS code_group_code, " _
        '          & "  code_mstr_group.code_name AS code_group_name, " _
        '          & "  code_mstr_proc.code_en_id AS code_proc_en_id, " _
        '          & "  code_mstr_proc.code_id AS code_proc_id, " _
        '          & "  code_mstr_proc.code_field AS code_proc_field, " _
        '          & "  code_mstr_proc.code_code AS code_proc_code, " _
        '          & "  code_mstr_proc.code_name AS code_proc_name,code_mstr.code_name as um_desc " _
        '          & " FROM " _
        '          & "  psd_det inner join ps_mstr on (ps_oid=psd_ps_oid)  " _
        '          & "  LEFT OUTER JOIN code_mstr code_mstr_group ON (psd_det.psd_group = code_mstr_group.code_id) " _
        '          & "  LEFT OUTER JOIN code_mstr code_mstr_proc ON (psd_det.psd_process = code_mstr_proc.code_id)" _
        '          & "  INNER JOIN pt_mstr ON pt_id = psd_pt_bom_id " _
        '          & "  INNER JOIN code_mstr ON code_mstr.code_id = pt_mstr.pt_um " _
        '          & "  LEFT OUTER JOIN op_mstr on psd_op=op_code " _
        '          & " WHERE ps_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_ps_id").ToString _
        '          & " order by psd_seq"



        'load_data_detail(sql, gc_detail, "detail")

        'Try
        '    ds.Tables("routing").Clear()
        'Catch ex As Exception
        'End Try


        'sql = "SELECT  " _
        '    & "  rod_oid, " _
        '    & "  rod_ro_oid, " _
        '    & "  rod_add_by, " _
        '    & "  rod_add_date, " _
        '    & "  rod_upd_by, " _
        '    & "  rod_upd_date, " _
        '    & "  rod_op, " _
        '    & "  rod_start_date, " _
        '    & "  rod_end_date, " _
        '    & "  rod_wc_id, " _
        '    & "  rod_desc, " _
        '    & "  rod_mch_op, " _
        '    & "  rod_tran_qty, " _
        '    & "  rod_queue, " _
        '    & "  rod_wait, " _
        '    & "  rod_move, " _
        '    & "  rod_run, " _
        '    & "  rod_setup, " _
        '    & "  rod_insheet_pct, " _
        '    & "  rod_milestone, " _
        '    & "  rod_sub_lead, " _
        '    & "  rod_setup_men, " _
        '    & "  rod_men_mch, " _
        '    & "  rod_tool_code, " _
        '    & "  rod_ptnr_id, " _
        '    & "  rod_sub_cost,rod_seq,rod_conversion, " _
        '    & "  wc_desc, " _
        '    & "  code_name, " _
        '    & "  ptnr_name, " _
        '    & "  rod_dt " _
        '    & "FROM  " _
        '    & "  public.rod_det " _
        '    & " INNER JOIN wc_mstr on rod_wc_id= wc_id" _
        '    & " LEFT OUTER JOIN code_mstr on rod_tool_code= code_id" _
        '    & " LEFT OUTER JOIN ptnr_mstr on rod_ptnr_id= ptnr_id" _
        '    & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid " _
        '    & " where ro_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_ro_id").ToString _
        '    & "  order by rod_seq "


        'load_data_detail(sql, gc_routing, "routing")
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        show_detail()
    End Sub

    Private Sub BtRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRelease.Click
        Try
            Dim sSQLs As New ArrayList

            ssql = "SELECT DISTINCT " _
                & "  a.wor_oid, " _
                & "  a.wor_en_id, " _
                & "  e.en_desc, " _
                & "  a.wor_add_by, " _
                & "  a.wor_add_date, " _
                & "  a.wor_upd_by, " _
                & "  a.wor_upd_date, " _
                & "  a.wor_code, " _
                & "  a.wor_date, " _
                & "  a.wor_date_eff, " _
                & "  a.wor_wo_id, " _
                & "  b.wo_code, " _
                & "  b.wo_remarks, " _
                & "  a.wor_loc_id, " _
                & "  f.loc_code, " _
                & "  f.loc_desc, " _
                & "  a.wor_si_id, " _
                & "  g.si_code, " _
                & "  g.si_desc, " _
                & "  coalesce(a.wor_qty_comp,0) as wor_qty_comp, " _
                & "  coalesce(a.wor_qty_reject,0) as wor_qty_reject, " _
                & "  a.wor_remarks, " _
                & "  b.wo_remarks, " _
                & "  c.pt_code, " _
                & "  c.pt_id, " _
                & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
                & "  c.pt_um, " _
                & "  d.code_name AS unit_measure, " _
                & "  b.wo_pjc_id, " _
                & "  a.wor_close, a.wor_is_mobile, a.wor_rcv_status, b.pjc_code, a.wor_cost, a.wo_cost, a.wo_ext_cost, a.wo_pt_id, e.en_code, " _
                & "  (coalesce(a.wor_qty_comp,0) + coalesce(a.wor_qty_reject,0)) * wo_ext_cost as wor_qty_cost " _
                & "FROM " _
                & "  public.wor_mstr a " _
                & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
                & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
                & "  LEFT OUTER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
                & "  INNER JOIN public.loc_mstr f ON (a.wor_loc_id = f.loc_id) " _
                & "  INNER JOIN public.si_mstr g ON (a.wor_si_id = g.si_id) " _
                & "  LEFT OUTER JOIN public.pjc_mstr ON pjc_id = wo_pjc_id " _
                & " Where wor_date between " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and  " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and  a.wor_is_mobile = 'Y' " _
                & "  and  a.wor_rcv_status = 'N' " _
                & " AND a.wor_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " ORDER BY wor_code"

            Dim dt_transaksi_master As New DataTable
            dt_transaksi_master = GetTableData(ssql)
            
            Using objinsert As New master_new.CustomCommand
                With objinsert
                    Try
                        For Each dr_master As DataRow In ds.Tables(0).Rows
                            If dr_master("pilih") = True Then
                                Dim _wor_code As String = (dr_master("wor_code"))
                                Dim _wor_oid As String = (dr_master("wor_oid"))
                                Dim _wor_en_id As String = (dr_master("wor_en_id"))
                                Dim _wor_en_code As String = (dr_master("en_code"))
                                Dim _wor_date_now As Date = func_coll.get_tanggal_sistem
                                'Dim ssqls As New ArrayList


                                Dim _en_id, _si_id, _loc_id, _pt_id, _tran_id As Integer
                                Dim _qty, _cost_avg As Double
                                Dim _serial As String
                                Dim _wor_date As DateTime

                                Dim i As Integer

                                ssql = "SELECT DISTINCT " _
                                    & "  a.wor_oid, " _
                                    & "  a.wor_en_id, " _
                                    & "  e.en_desc, " _
                                    & "  a.wor_add_by, " _
                                    & "  a.wor_add_date, " _
                                    & "  a.wor_upd_by, " _
                                    & "  a.wor_upd_date, " _
                                    & "  a.wor_code, " _
                                    & "  a.wor_date, " _
                                    & "  a.wor_date_eff, " _
                                    & "  a.wor_wo_id, " _
                                    & "  b.wo_code, " _
                                    & "  b.wo_remarks, " _
                                    & "  a.wor_loc_id, " _
                                    & "  f.loc_code, " _
                                    & "  f.loc_desc, " _
                                    & "  a.wor_si_id, " _
                                    & "  g.si_code, " _
                                    & "  g.si_desc, " _
                                    & "  coalesce(a.wor_qty_comp,0) as wor_qty_comp, " _
                                    & "  coalesce(a.wor_qty_reject,0) as wor_qty_reject, " _
                                    & "  a.wor_remarks, " _
                                    & "  b.wo_remarks, " _
                                    & "  c.pt_code, " _
                                    & "  c.pt_id, " _
                                    & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
                                    & "  c.pt_um, " _
                                    & "  d.code_name AS unit_measure, " _
                                    & "  wo_pjc_id, " _
                                    & "  a.wor_close, a.wor_is_mobile, a.wor_rcv_status, pjc_code,wor_cost,wor_ext_cost,wo_cost,wo_ext_cost,wo_pt_id,en_code, " _
                                    & "  (coalesce(a.wor_qty_comp,0) + coalesce(a.wor_qty_reject,0)) * wo_ext_cost as wor_qty_cost " _
                                    & "FROM " _
                                    & "  public.wor_mstr a " _
                                    & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
                                    & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
                                    & "  LEFT OUTER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                                    & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
                                    & "  INNER JOIN public.loc_mstr f ON (a.wor_loc_id = f.loc_id) " _
                                    & "  INNER JOIN public.si_mstr g ON (a.wor_si_id = g.si_id) " _
                                    & "  LEFT OUTER JOIN public.pjc_mstr ON pjc_id = wo_pjc_id " _
                                    & " Where wor_date between " + SetDate(pr_txttglawal.DateTime.Date) _
                                    & "  and  " + SetDate(pr_txttglakhir.DateTime.Date) _
                                    & "  and  a.wor_is_mobile = 'Y' " _
                                    & "  and  a.wor_rcv_status = 'N' " _
                                    & " AND a.wor_en_id in (select user_en_id from tconfuserentity " _
                                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                                    & " ORDER BY wor_code"

                                Dim dr_transaksi As New DataTable
                                dr_transaksi = GetTableData(ssql)

                                'Ubah status WOR Receive
                                .Command.CommandText = "UPDATE  " _
                                                        & " wor_mstr " _
                                                        & " set " _
                                                        & " wor_rcv_status='Y', " _
                                                        & " wor_upd_date=" & SetDateNTime(CekTanggal) & ",  " _
                                                        & " wor_upd_by = " & SetSetring(master_new.ClsVar.sNama) & "  " _
                                                        & " where wor_oid=" & SetSetring(dr_master("wor_oid"))
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                'update ke WO
                                .Command.CommandText = "UPDATE  " _
                                    & "  public.wo_mstr  " _
                                    & "SET  " _
                                    & "  wo_qty_comp = coalesce(wo_qty_comp,0) + " & SetDecDB(dr_master("wor_qty_comp")) & ",  " _
                                    & "  wo_qty_rjc = coalesce(wo_qty_rjc,0) + " & SetDecDB(dr_master("wor_qty_reject")) & "  " _
                                    & "WHERE  " _
                                    & "  wo_id = " & SetInteger(dr_master("wor_wo_id")) & " "
                                .Command.ExecuteNonQuery()
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = ssql
                                'sSQLs.Add(.Command.CommandText)
                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                'ssql = "UPDATE  " _
                                '    & "  public.pjc_mstr  " _
                                '    & "SET  " _
                                '    & "  wo_qty_comp = coalesce(wo_qty_comp,0) + " & SetDecDB(wor_qty_comp.EditValue) & ",  " _
                                '    & "  wo_qty_rjc = coalesce(wo_qty_rjc,0) + " & SetDecDB(wor_qty_reject.EditValue) & "  " _
                                '    & "WHERE  " _
                                '    & "  wo_id = " & SetInteger(wor_wo_id.Tag) & " "


                                ''.Command.CommandType = CommandType.Text
                                '.Command.CommandText = ssql
                                'ssqls.Add(.Command.CommandText)
                                '.Command.ExecuteNonQuery()
                                ''.Command.Parameters.Clear()

                                'cek apakah sudah full, kalo sudah maka close dengan otomatis

                                'di comment sementara untuk multi receipt 20241016
                                'If __qty_outstanding <= ((dr_master("wor_qty_comp")) + (dr_master("wor_qty_reject"))) Then
                                '    .Command.CommandText = "UPDATE  " _
                                '         & "  public.wo_mstr  " _
                                '         & "SET  " _
                                '         & "  wo_status = 'C', " _
                                '         & "  wo_date_close=" & SetDateNTime(CekTanggal) _
                                '         & "WHERE  " _
                                '         & "  wo_id = " & SetInteger(dr_master("wor_wo_id")) & " "
                                '    .Command.ExecuteNonQuery()
                                '    '.Command.CommandType = CommandType.Text
                                '    '.Command.CommandText = ssql
                                '    'sSQLs.Add(.Command.CommandText)
                                '    '.Command.ExecuteNonQuery()
                                '    '.Command.Parameters.Clear()
                                'End If

                                'update ke stok
                                'If ds_edit.Tables(0).Rows(i).Item("wor_qty_comp") > 0 Then
                                If SetString(dr_master("wor_qty_comp").ToString).ToUpper > 0 Then
                                    'If SetString(pt_ls.EditValue.ToString).ToUpper <> "S" Then

                                    _en_id = (dr_master("wor_en_id"))
                                    _si_id = (dr_master("wor_si_id"))
                                    _loc_id = (dr_master("wor_loc_id"))
                                    _wor_date = (dr_master("wor_date"))
                                    _pt_id = (dr_master("pt_id"))
                                    _serial = "''"
                                    _qty = (dr_master("wor_qty_comp"))

                                    'If func_coll.update_invc_mstr_plus(ssqls, objinsert, wor_en_id.EditValue, wor_si_id.EditValue, _
                                    '                                   wor_loc_id.EditValue, _pjc_id, pt_code.Tag, "''", _
                                    '                                   wor_qty_comp.EditValue + wor_qty_reject.EditValue) = False Then
                                    '    'sqlTran.Rollback()
                                    '    insert = False
                                    '    Exit Function
                                    'End If

                                    If func_coll.update_invc_mstr_plus(sSQLs, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        'sqlTran.Rollback()
                                        BtRelease.Enabled = False
                                        Exit Sub
                                    End If
                                    'End If


                                    '_en_id = wor_en_id.EditValue
                                    '_si_id = wor_si_id.EditValue
                                    '_loc_id = wor_loc_id.EditValue
                                    '_pt_id = pt_code.Tag
                                    '_serial = "''"
                                    '_qty = wor_qty_comp.EditValue
                                    _tran_id = func_coll.get_id_tran_mstr("rct-wo")

                                    '20120101 ini sudah gak dipake lagi ...cost avg
                                    _cost_avg = 0 'func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost) 

                                    If func_coll.update_invh_mstr(sSQLs, objinsert, _tran_id, 0, _en_id, _wor_code, _wor_oid, _
                                                                  "WO Receipt", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _
                                                                  _cost, _cost_avg, "", func_coll.get_tanggal_sistem) = False Then
                                        'sqlTran.Rollback()
                                        BtRelease.Enabled = False
                                        Exit Sub
                                    End If

                                Else
                                    Dim x As Integer = 0
                                    'For Each dr As DataRow In dt_serial.Rows
                                    '    If func_coll.update_invc_mstr_plus(sSQLs, objinsert, wor_en_id.EditValue, _
                                    '                                       wor_si_id.EditValue, wor_loc_id.EditValue, _pjc_id, _
                                    '                                       pt_code.Tag, dr("wors_lot_serial"), dr("wors_qty")) = False Then
                                    '        'sqlTran.Rollback()
                                    '        insert = False
                                    '        Exit Sub
                                    '    End If

                                    '    _en_id = wor_en_id.EditValue
                                    '    _si_id = wor_si_id.EditValue
                                    '    _loc_id = wor_loc_id.EditValue
                                    '    _pt_id = pt_code.Tag
                                    '    _serial = dr("wors_lot_serial")
                                    '    _qty = dr("wors_qty")
                                    '    _tran_id = func_coll.get_id_tran_mstr("rct-wo")

                                    '    '20120101 ini sudah gak dipake lagi ...cost avg
                                    '    _cost_avg = 0 'func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)

                                    '    If func_coll.update_invh_mstr(sSQLs, objinsert, _tran_id, x, _en_id, _wor_code, _wor_oid, _
                                    '                                  "WO Receipt", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _
                                    '                                  _cost, _cost_avg, _serial, func_coll.get_tanggal_sistem) = False Then
                                    '        'sqlTran.Rollback()
                                    '        BtRelease.Enabled = False
                                    '        Exit Sub
                                    '    End If
                                    '    x += 1
                                    'Next
                                End If



                                'Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
                                'If _create_jurnal = True Then
                                '    'If SetNumber(wor_cost.EditValue) > 0.0 Then
                                '    If insert_glt_det_wo_receipt(sSQLs, objinsert, ds_edit, _
                                '                     _wor_en_id, _wor_en_code, _
                                '                     _wor_oid.ToString, _wor_code, _
                                '                     _wor_date, _
                                '                     "WR", "ISS-WIP") = False Then

                                '        'sqlTran.Rollback()
                                '        BtRelease.Enabled = False
                                '        Exit Sub
                                '    End If
                                '    'Else
                                '    '    MsgBox("Cost can't blank")
                                '    '    insert = False
                                '    'End If

                                'End If

                                Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
                                If _create_jurnal = True Then
                                    'If SetNumber(wor_cost.EditValue) > 0.0 Then
                                    If insert_glt_det_wo_receipt(sSQLs, objinsert, ds_edit, _
                                                     _wor_en_id, _wor_en_code, _
                                                     _wor_oid.ToString, _wor_code, _
                                                     _wor_date_now, _
                                                     "WR", "ISS-WIP") = False Then

                                        'sqlTran.Rollback()
                                        BtRelease.Enabled = False
                                        Exit Sub
                                    End If
                                    'Else
                                    '    MsgBox("Cost can't blank")
                                    '    insert = False
                                    'End If

                                End If

                                If master_new.PGSqlConn.status_sync = True Then
                                    For Each Data As String In master_new.ModFunction.FinsertSQL2Array(sSQLs)
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = Data
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()
                                    Next
                                End If

                                BtRelease.Enabled = True
                            End If
                        Next

                        .Command.Commit()
                        MsgBox("Received success")
                        help_load_data(True)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        'sqlTran.Rollback()
                    End Try

                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function insert_glt_det_wo_receipt(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                ByVal par_oid As String, ByVal par_trans_code As String, _
                                ByVal par_date As Date, ByVal par_type As String, ByVal par_daybook As String) As Boolean

        insert_glt_det_wo_receipt = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _cost As Double
        _glt_code = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1


        'Return False

        'If _nilai_kalkulasi <= 0 Then
        '    Return True
        '    Exit Function

        'End If

        'For i = 0 To par_ds.Tables(0).Rows.Count - 1
        '    _seq = _seq + 1

        With par_obj
            Try
                For Each dr_master As DataRow In ds.Tables(0).Rows
                    If dr_master("pilih") = True Then
                        Dim _wor_code As String
                        Dim _wor_oid As String
                        'Dim ssqls As New ArrayList


                        Dim _en_id, _si_id, _loc_id, _pt_id, _tran_id As Integer
                        Dim _qty, _cost_avg As Double
                        Dim _serial As String
                        Dim _wor_date As DateTime

                        Dim _nilai_kalkulasi As Double = SetNumber(dr_master("wor_cost"))

                        'Dim i As Integer

                        ssql = "SELECT DISTINCT " _
                            & "  a.wor_oid, " _
                            & "  a.wor_en_id, " _
                            & "  e.en_desc, " _
                            & "  a.wor_add_by, " _
                            & "  a.wor_add_date, " _
                            & "  a.wor_upd_by, " _
                            & "  a.wor_upd_date, " _
                            & "  a.wor_code, " _
                            & "  a.wor_date, " _
                            & "  a.wor_date_eff, " _
                            & "  a.wor_wo_id, " _
                            & "  b.wo_code, " _
                            & "  b.wo_remarks, " _
                            & "  a.wor_loc_id, " _
                            & "  f.loc_code, " _
                            & "  f.loc_desc, " _
                            & "  a.wor_si_id, " _
                            & "  g.si_code, " _
                            & "  g.si_desc, " _
                            & "  coalesce(a.wor_qty_comp,0) as wor_qty_comp, " _
                            & "  coalesce(a.wor_qty_reject,0) as wor_qty_reject, " _
                            & "  a.wor_remarks, " _
                            & "  b.wo_remarks, " _
                            & "  c.pt_code, " _
                            & "  c.pt_id, " _
                            & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
                            & "  c.pt_um, " _
                            & "  d.code_name AS unit_measure, " _
                            & "  wo_pjc_id, " _
                            & "  a.wor_close, a.wor_is_mobile, a.wor_rcv_status, pjc_code,wor_cost,wor_ext_cost,wo_ext_cost,wo_cost,wo_ext_cost,wo_pt_id,en_code " _
                            & "FROM " _
                            & "  public.wor_mstr a " _
                            & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
                            & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
                            & "  LEFT OUTER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                            & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
                            & "  INNER JOIN public.loc_mstr f ON (a.wor_loc_id = f.loc_id) " _
                            & "  INNER JOIN public.si_mstr g ON (a.wor_si_id = g.si_id) " _
                            & "  LEFT OUTER JOIN public.pjc_mstr ON pjc_id = wo_pjc_id " _
                            & " Where wor_date between " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and  " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & "  and  a.wor_is_mobile = 'Y' " _
                            & "  and  a.wor_rcv_status = 'N' " _
                            & " AND a.wor_en_id in (select user_en_id from tconfuserentity " _
                            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                            & " ORDER BY wor_code"

                        Dim dr_transaksi As New DataTable
                        dr_transaksi = GetTableData(ssql)


                        If ((dr_master("wor_qty_comp")) + (dr_master("wor_qty_reject"))) > 0 Then

                            dt_bantu = New DataTable
                            '_pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id_wod"))
                            '_cost = SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_qty")) * SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_cost"))


                            _pl_id = func_coll.get_prodline(dr_master("pt_id"))
                            _cost = SetNumber((dr_master("wor_qty_comp")) + (dr_master("wor_qty_reject"))) * SetNumber(dr_master("wo_ext_cost"))

                            '********************** finish untuk yang debet
                            dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.glt_det " _
                                                & "( " _
                                                & "  glt_oid, " _
                                                & "  glt_dom_id, " _
                                                & "  glt_en_id, " _
                                                & "  glt_add_by, " _
                                                & "  glt_add_date, " _
                                                & "  glt_code, " _
                                                & "  glt_date, " _
                                                & "  glt_type, " _
                                                & "  glt_cu_id, " _
                                                & "  glt_exc_rate, " _
                                                & "  glt_seq, " _
                                                & "  glt_ac_id, " _
                                                & "  glt_sb_id, " _
                                                & "  glt_cc_id, " _
                                                & "  glt_desc, " _
                                                & "  glt_debit, " _
                                                & "  glt_credit, " _
                                                & "  glt_ref_oid, " _
                                                & "  glt_ref_trans_code, " _
                                                & "  glt_posted, " _
                                                & "  glt_dt, " _
                                                & "  glt_daybook " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(par_en_id) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_glt_code) & ",  " _
                                                & SetDate(par_date) & ",  " _
                                                & SetSetring(par_type) & ",  " _
                                                & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                                & SetDbl(1) & ",  " _
                                                & SetInteger(_seq + 1) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                                & SetSetringDB("Work Order Receipt") & ",  " _
                                                & SetDblDB(_cost) & ",  " _
                                                & SetDblDB(0) & ",  " _
                                                & SetSetring(par_oid) & ",  " _
                                                & SetSetring(par_trans_code) & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(par_daybook) & "  " _
                                                & ")"
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                             dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                             dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                             dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                             par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                             1, _cost, "D") = False Then

                                Return False
                                Exit Function
                            End If

                            dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_WIPACC")
                            'Insert Untuk Yang credit 
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.glt_det " _
                                                & "( " _
                                                & "  glt_oid, " _
                                                & "  glt_dom_id, " _
                                                & "  glt_en_id, " _
                                                & "  glt_add_by, " _
                                                & "  glt_add_date, " _
                                                & "  glt_code, " _
                                                & "  glt_date, " _
                                                & "  glt_type, " _
                                                & "  glt_cu_id, " _
                                                & "  glt_exc_rate, " _
                                                & "  glt_seq, " _
                                                & "  glt_ac_id, " _
                                                & "  glt_sb_id, " _
                                                & "  glt_cc_id, " _
                                                & "  glt_desc, " _
                                                & "  glt_debit, " _
                                                & "  glt_credit, " _
                                                & "  glt_ref_oid, " _
                                                & "  glt_ref_trans_code, " _
                                                & "  glt_posted, " _
                                                & "  glt_dt, " _
                                                & "  glt_daybook " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(par_en_id) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_glt_code) & ",  " _
                                                & SetDate(par_date) & ",  " _
                                                & SetSetring(par_type) & ",  " _
                                                & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                                & SetDbl(1) & ",  " _
                                                & SetInteger(_seq) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                                & SetSetring("Work Order Receipt") & ",  " _
                                                & SetDblDB(0) & ",  " _
                                                & SetDblDB(_cost) & ",  " _
                                                & SetSetring(par_oid) & ",  " _
                                                & SetSetring(par_trans_code) & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(par_daybook) & "  " _
                                                & ")"
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                             dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                             dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                             dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                             par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                             1, _cost, "C") = False Then

                                Return False
                                Exit Function
                            End If
                        End If
                    End If

                Next


                ''===================================================================================

                'If par_ds.Tables(0).Rows(i).Item("wocid_qty") > 0 Then
                '    dt_bantu = New DataTable
                '    _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pt_id_wod"))
                '    _cost = SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_qty")) * SetNumber(par_ds.Tables(0).Rows(i).Item("wocid_cost"))

                '    '********************** finish untuk yang debet
                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_DET_ACC")

                '    'Insert Untuk Debet nya....
                '    _seq = _seq + 1
                '    '.Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq + 1) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetringDB("Work Order Issue (Step 1)") & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    '.Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "D") = False Then

                '        Return False
                '        Exit Function
                '    End If

                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")
                '    'Insert Untuk Yang credit 
                '    '.Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetring("Work Order Issue (Step 1)") & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    '.Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "C") = False Then

                '        Return False
                '        Exit Function
                '    End If


                '    'jurnal baris kedua
                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_HED_ACC")
                '    'Insert Untuk Debet nya....
                '    _seq = _seq + 1
                '    '.Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq + 1) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetringDB("Work Order Issue (Step 2)") & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    '.Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                      dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "D") = False Then

                '        Return False
                '        Exit Function
                '    End If

                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_DET_ACC")
                '    'Insert Untuk Yang credit 
                '    '.Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetring("Work Order Issue (Step 2)") & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    '.Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                       dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "C") = False Then

                '        Return False
                '        Exit Function
                '    End If


                '    'jurnal baris ketiga
                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_WIPACC")
                '    'Insert Untuk Debet nya....
                '    _seq = _seq + 1
                '    '.Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq + 1) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetringDB("Work Order Issue (Step 3)") & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    '.Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                       dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "D") = False Then

                '        Return False
                '        Exit Function
                '    End If

                '    dt_bantu = func_coll.get_prodline_account(_pl_id, "WO_EXP_HED_ACC")
                '    'Insert Untuk Yang credit 
                '    '.Command.CommandType = CommandType.Text
                '    .Command.CommandText = "INSERT INTO  " _
                '                        & "  public.glt_det " _
                '                        & "( " _
                '                        & "  glt_oid, " _
                '                        & "  glt_dom_id, " _
                '                        & "  glt_en_id, " _
                '                        & "  glt_add_by, " _
                '                        & "  glt_add_date, " _
                '                        & "  glt_code, " _
                '                        & "  glt_date, " _
                '                        & "  glt_type, " _
                '                        & "  glt_cu_id, " _
                '                        & "  glt_exc_rate, " _
                '                        & "  glt_seq, " _
                '                        & "  glt_ac_id, " _
                '                        & "  glt_sb_id, " _
                '                        & "  glt_cc_id, " _
                '                        & "  glt_desc, " _
                '                        & "  glt_debit, " _
                '                        & "  glt_credit, " _
                '                        & "  glt_ref_oid, " _
                '                        & "  glt_ref_trans_code, " _
                '                        & "  glt_posted, " _
                '                        & "  glt_dt, " _
                '                        & "  glt_daybook " _
                '                        & ")  " _
                '                        & "VALUES ( " _
                '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                '                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                '                        & SetInteger(par_en_id) & ",  " _
                '                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(_glt_code) & ",  " _
                '                        & SetDate(par_date) & ",  " _
                '                        & SetSetring(par_type) & ",  " _
                '                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                '                        & SetDbl(1) & ",  " _
                '                        & SetInteger(_seq) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                '                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                '                        & SetSetring("Work Order Issue (Step 3)") & ",  " _
                '                        & SetDblDB(0) & ",  " _
                '                        & SetDblDB(_cost) & ",  " _
                '                        & SetSetring(par_oid) & ",  " _
                '                        & SetSetring(par_trans_code) & ",  " _
                '                        & SetSetring("N") & ",  " _
                '                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                '                        & SetSetring(par_daybook) & "  " _
                '                        & ")"
                '    par_ssqls.Add(.Command.CommandText)
                '    .Command.ExecuteNonQuery()
                '    '.Command.Parameters.Clear()

                '    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                '                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                '                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                '                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                '                                     1, _cost, "C") = False Then

                '        Return False
                '        Exit Function
                '    End If
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
        'Next
    End Function

End Class
