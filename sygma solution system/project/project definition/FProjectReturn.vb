Imports DevExpress.XtraExport
Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FProjectReturn
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial, ds_get_detail, ds_get_serial As DataSet
    Dim _now As DateTime
    Dim _conf_budget, _soship_oid_mstr As String
    Public _prj_oid As String
    Dim _qty_sisa As Double
    Dim _progress_pay_sisa As Double
    Dim _conf_value As String
    Dim mf As New master_new.ModFunction

#Region "SettingAwal"
    Private Sub FProjectShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '_conf_value = func_coll.get_conf_file("wf_soship_mstr")
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        xtc_detail.SelectedTabPageIndex = 0

        'If _conf_value = "0" Then
        '    xtc_detail.TabPages(2).PageVisible = False
        'ElseIf _conf_value = "1" Then
        '    xtc_detail.TabPages(2).PageVisible = True
        'End If
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        soship_en_id.Properties.DataSource = dt_bantu
        soship_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        soship_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        soship_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", soship_en_id.EditValue))
        soship_si_id.Properties.DataSource = dt_bantu
        soship_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        soship_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        soship_si_id.ItemIndex = 0
    End Sub

    Private Sub soship_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles soship_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Status", "soship_trans_id", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "User Create", "soship_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "soship_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "soship_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "soship_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail1, "soshipd_oid", False)
        add_column(gv_detail1, "soshipd_soship_oid", False)
        add_column_copy(gv_detail1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Qty Return", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "Qty Invoice", "soshipd_qty_inv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_serial, "soshipd_soship_oid", False)
        add_column_copy(gv_detail_serial, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Serial\Lot Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_serial, "Qty Serial", "soshipds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column(gv_email1, "soshipd_oid", False)
        'add_column(gv_email1, "soshipd_soship_oid", False)
        'add_column(gv_email1, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Prepayment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column(gv_email1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "Qty Shipment", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_email1, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_email1, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_email1, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_email1, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_email1, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "soshipd_oid", False)
        add_column(gv_edit, "soshipd_prjd_oid", False)
        add_column(gv_edit, "soshipd_shipment_oid", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "soshipd_prjd_oid", False)
        add_column(gv_edit, "soshipd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "soshipd_loc_id", False)
        add_column_browse(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Return", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "soshipd_um", False)
        add_column(gv_edit, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "soshipd_rea_code_id", False)
        add_column_browse(gv_edit, "Reason Return", "reason_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_serial, "soshipds_soshipd_oid", False)
        add_column(gv_edit_serial, "soshipd_soship_oid", False)
        add_column(gv_edit_serial, "Serial/Lot Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_serial, "Qty", "soshipds_qty", DevExpress.Utils.HorzAlignment.Far)

        'add_column(gv_wf1, "wf_ref_code", False)
        'add_column_copy(gv_wf1, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf1, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf1, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf1, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_wf1, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf1, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf1, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        'add_column_copy(gv_wf1, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_smart1, "Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  soship_oid, " _
                    & "  soship_dom_id, " _
                    & "  soship_en_id,en_desc, " _
                    & "  soship_add_by, " _
                    & "  soship_add_date, " _
                    & "  soship_upd_by, " _
                    & "  soship_upd_date, " _
                    & "  soship_code, " _
                    & "  soship_date, " _
                    & "  soship_so_oid, " _
                    & "  soship_si_id, " _
                    & "  soship_is_shipment, " _
                    & "  soship_dt, " _
                    & "  soship_prj_oid,prj_code,ptnr_name, " _
                    & "  soship_tran_id,soship_trans_id,tran_name, pjc_id " _
                    & "FROM  " _
                    & "  public.soship_mstr " _
                    & "  inner join en_mstr on en_id = soship_en_id " _
                    & "  inner join prj_mstr on prj_oid = soship_prj_oid " _
                    & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                    & "  inner join tran_mstr on tran_id = soship_tran_id " _
                    & "  inner join pjc_mstr on pjc_code = prj_code " _
                    & "  where soship_is_shipment <> 'Y' " _
                    & "  and soship_is_prepayment = 'N' " _
                    & " and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and soship_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
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
            & "  soshipd_oid,soship_en_id, " _
            & "  soshipd_soship_oid,soship_oid,soship_code,soship_date,soship_tran_id, " _
            & "  soshipd_sod_oid, " _
            & "  soshipd_seq, " _
            & "  soshipd_qty,0 as qty_open, " _
            & "  soshipd_um,um.code_name as unit_measure, " _
            & "  soshipd_um_conv, " _
            & "  soshipd_cancel_bo, " _
            & "  soshipd_qty_real, " _
            & "  soshipd_si_id,si_desc, " _
            & "  soshipd_loc_id,loc_desc, " _
            & "  soshipd_lot_serial, " _
            & "  soshipd_rea_code_id, " _
            & "  rc.code_name as reason_name, " _
            & "  soshipd_dt, " _
            & "  coalesce(soshipd_qty_inv,0) as soshipd_qty_inv, " _
            & "  soshipd_close_line, " _
            & "  soshipd_prjd_oid, " _
            & "  pt_type, pt_ls, pt_id, pt_code, prjd_pt_desc1, prjd_pt_desc2, prjd_cost, " _
            & "  prjd_price, " _
            & "  soshipd_progress_pay, " _
            & "  0.00 as progress_pay_open, " _
            & "  soshipd_progress_pay_inv, " _
            & "  0.0 as qty_open, soshipd_shipment_oid " _
            & "FROM  " _
            & "  public.soshipd_det " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
            & "  inner join code_mstr um on um.code_id = soshipd_um " _
            & "  inner join code_mstr rc on rc.code_id = soshipd_rea_code_id " _
            & "  inner join si_mstr on si_id = soshipd_si_id " _
            & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
            & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and soship_is_shipment <> 'Y' " _
            & " and soship_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by soshipd_seq asc "
        load_data_detail(sql, gc_detail1, "detail")
        gv_detail1.BestFitColumns()

        Try
            ds.Tables("detail_serial").Clear()
        Catch ex As Exception
        End Try
        sql = "SELECT  " _
            & "  soshipds_oid, " _
            & "  soshipds_soshipd_oid,soshipd_soship_oid, " _
            & "  soshipds_seq, " _
            & "  soshipds_qty, " _
            & "  soshipds_qty_real, " _
            & "  soshipds_si_id,si_desc, " _
            & "  soshipds_loc_id,loc_desc, " _
            & "  pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
            & "  soshipds_lot_serial, " _
            & "  soshipds_dt " _
            & "FROM  " _
            & "  public.soshipds_serial " _
            & "  inner join soshipd_det on soshipd_oid = soshipds_soshipd_oid " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
            & "  inner join si_mstr on si_id = soshipd_si_id " _
            & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
            & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  and soship_is_shipment <> 'Y' " _
            & " and soship_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & "  order by soshipds_seq asc "
        load_data_detail(sql, gc_detail_serial, "detail_serial")

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
                  " inner join soship_mstr on soship_code = wf_ref_code " + _
                  " inner join soshipd_det dt on dt.soshipd_soship_oid = soship_oid " _
                & " where soship_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and soship_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf1, "wf")
            gv_wf1.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  soshipd_oid, " _
                & "  soshipd_soship_oid, " _
                & "  soshipd_sod_oid, " _
                & "  soship_prj_oid,prj_code,ptnr_name, " _
                & "  soship_code, " _
                & "  soship_date, " _
                & "  soshipd_seq, " _
                & "  soshipd_qty,0 as qty_open, " _
                & "  soshipd_um,um.code_name as unit_measure, " _
                & "  soshipd_um_conv, " _
                & "  soshipd_cancel_bo, " _
                & "  soshipd_qty_real, " _
                & "  soshipd_si_id,si_desc, " _
                & "  soshipd_loc_id,loc_desc, " _
                & "  soshipd_lot_serial, " _
                & "  soshipd_rea_code_id, " _
                & "  soshipd_dt, " _
                & "  soshipd_qty_inv, " _
                & "  soshipd_close_line, " _
                & "  soshipd_prjd_oid, " _
                & "  pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
                & "  prjd_cost, " _
                & "  prjd_price, " _
                & "  soshipd_progress_pay, " _
                & "  0.00 as progress_pay_open, " _
                & "  soshipd_progress_pay_inv, " _
                & "  0 as qty_open " _
                & "FROM  " _
                & "  public.soshipd_det " _
                & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
                & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                & "  inner join code_mstr um on um.code_id = soshipd_um " _
                & "  inner join si_mstr on si_id = soshipd_si_id " _
                & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                & " where soship_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and soship_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            load_data_detail(sql, gc_email1, "email")
            gv_email1.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select soship_oid, soship_code, soship_trans_id, false as status from soship_mstr " _
                & " where soship_trans_id ~~* 'd' " _
                & " and soship_en_id in (select user_en_id from tconfuserentity " _
                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            load_data_detail(sql, gc_smart1, "smart")
        End If

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail1.Columns("soshipd_soship_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipd_soship_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString & "'")
            gv_detail1.BestFitColumns()

            gv_detail_serial.Columns("soshipd_soship_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipd_soship_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString & "'")
            gv_detail_serial.BestFitColumns()

            gv_wf1.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code").ToString & "'")
            gv_wf1.BestFitColumns()

            gv_email1.Columns("soshipd_soship_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipd_soship_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString & "'")
            gv_email1.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        soship_en_id.Focus()
        soship_en_id.ItemIndex = 0
        soship_prj_oid.Text = ""
        soship_date.EditValue = Today()

        Try
            tcg_header.SelectedTabPageIndex = 0
            'tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  soshipd_oid, " _
                        & "  soshipd_soship_oid, " _
                        & "  soshipd_sod_oid, " _
                        & "  soshipd_seq, " _
                        & "  soshipd_qty,0.0 as qty_open, " _
                        & "  soshipd_um,um.code_name as unit_measure, " _
                        & "  soshipd_um_conv, " _
                        & "  soshipd_cancel_bo, " _
                        & "  soshipd_qty_real, " _
                        & "  soshipd_si_id,si_desc, " _
                        & "  soshipd_loc_id,loc_desc, " _
                        & "  soshipd_lot_serial, " _
                        & "  soshipd_rea_code_id, " _
                        & "  rc.code_name as reason_name, " _
                        & "  soshipd_dt, " _
                        & "  soshipd_qty_inv, " _
                        & "  soshipd_close_line, " _
                        & "  soshipd_prjd_oid, " _
                        & "  pt_id,pt_code,prjd_pt_desc1,prjd_pt_desc2,prjd_cost, " _
                        & "  pt_ls,pt_type, " _
                        & "  soshipd_progress_pay, " _
                        & "  0.00 as progress_pay_open, " _
                        & "  soshipd_progress_pay_inv, soshipd_shipment_oid " _
                        & "FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
                        & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                        & "  inner join code_mstr um on um.code_id = soshipd_um " _
                        & "  inner join code_mstr rc on rc.code_id = soshipd_rea_code_id " _
                        & "  inner join si_mstr on si_id = soshipd_si_id " _
                        & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                        & "  where soshipd_lot_serial = 'gda#$@!' "
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_serial = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  soshipds_oid, " _
                        & "  soshipds_soshipd_oid, " _
                        & "  soshipds_seq, " _
                        & "  soshipds_qty, " _
                        & "  soshipds_qty_real, " _
                        & "  soshipds_si_id, " _
                        & "  soshipds_loc_id, " _
                        & "  soshipds_lot_serial, " _
                        & "  soshipds_dt " _
                        & "FROM  " _
                        & "  public.soshipds_serial " _
                        & "  inner join soshipd_det on soshipds_oid = soshipds_soshipd_oid " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & " where soshipds_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_serial, "serial")
                    gc_edit_serial.DataSource = ds_serial.Tables(0)
                    gv_edit_serial.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
          MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        gv_edit_serial.UpdateCurrentRow()

        ds_edit.AcceptChanges()
        ds_serial.AcceptChanges()

        'Dim _date As Date = func_coll.get_tanggal_sistem
        'Dim _gcald_det_status As String = func_data.get_gcald_det_status(soship_en_id.EditValue, "gcald_ic", _date)

        'If _gcald_det_status = "" Then
        '    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'ElseIf _gcald_det_status.ToUpper = "Y" Then
        '    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim i As Integer
        'Dim _qty, _qty_ttl_serial As Double

        '*********************
        'Cek Location
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")) = True Then
                    MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '*********************

        '***********************************************************************
        Dim _jml_serial As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pt_ls") = "S" And ds_edit.Tables(0).Rows(i).Item("soshipd_qty") <> 0 Then
                _jml_serial = 0

                For j As Integer = 0 To ds_serial.Tables(0).Rows.Count - 1
                    If ds_edit.Tables(0).Rows(i).Item("soshipd_oid") = ds_serial.Tables(0).Rows(j).Item("soshipds_soshipd_oid") Then
                        _jml_serial = _jml_serial + 1
                    End If
                Next

                If _jml_serial <> ds_edit.Tables(0).Rows(i).Item("soshipd_qty") Then
                    MessageBox.Show("Serial Qty Count Not Same!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next
        '***********************************************************************

        Return before_save
    End Function

    Public Shared Function GetIDMax() As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(ass_id),0) as max_col from ass_mstr "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetIDMax = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetIDMax
    End Function

    Public Shared Function GetMaxLine(ByVal _par_pt_id As Integer) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(ass_line),0) as max_col from ass_mstr " + _
                                           "where ass_pt_id = " + _par_pt_id.ToString()
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetMaxLine = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetMaxLine
    End Function

    Private Function update_invc_mstr_minus_local(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_pt_code As String, ByVal par_serial As String, ByVal par_qty As Double) As Boolean
        update_invc_mstr_minus_local = True
        Dim _invc_oid As String = ""
        Dim _invc_qty As Double = 0
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select invc_oid, coalesce(invc_serial,'') as invc_serial,invc_qty from invc_mstr " + _
                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                           " and invc_en_id = " + par_en_id.ToString + _
                           " and invc_si_id = " + par_si_id.ToString + _
                           " and invc_loc_id = " + par_loc_id.ToString + _
                           " and invc_pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If (i + 1) <= par_qty Then
                If Trim(ds_bantu.Tables(0).Rows(i).Item("invc_serial")) <> "" Then
                    With par_obj
                        Try
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & " public.invc_mstr   " _
                                                & "SET  " _
                                                & " invc_qty = invc_qty - " & SetDbl(1) _
                                                & " WHERE  " _
                                                & " invc_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("invc_oid")) & " "
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                        End Try
                    End With
                Else
                    With par_obj
                        Try
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & " public.invc_mstr   " _
                                                & "SET  " _
                                                & " invc_qty = invc_qty - " & SetDbl(ds_bantu.Tables(0).Rows(i).Item("invc_qty")) _
                                                & " WHERE  " _
                                                & " invc_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("invc_oid")) & " "
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                        End Try
                    End With
                End If
            End If
        Next

    End Function

    Private Function update_invc_mstr_plus_local(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double) As Boolean
        update_invc_mstr_plus_local = True
        Dim _invc_oid As String = ""
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select invc_oid,coalesce(invc_serial,'') as invc_serial,invc_qty from invc_mstr " + _
                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                           " and invc_en_id = " + par_en_id.ToString + _
                           " and invc_si_id = " + par_si_id.ToString + _
                           " and invc_loc_id = " + par_loc_id.ToString + _
                           " and invc_pt_id = " + par_pt_id.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If (i + 1) <= par_qty Then
                If Trim(ds_bantu.Tables(0).Rows(i).Item("invc_serial")) <> "" Then
                    With par_obj
                        Try
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & " public.invc_mstr   " _
                                                & "SET  " _
                                                & " invc_qty = coalesce(invc_qty,0) + " & SetDbl(1) _
                                                & " WHERE  " _
                                                & " invc_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("invc_oid")) & " "
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                        End Try
                    End With
                Else
                    With par_obj
                        Try
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & " public.invc_mstr   " _
                                                & "SET  " _
                                                & " invc_qty = invc_qty + " & SetDbl(par_qty) _
                                                & " WHERE  " _
                                                & " invc_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("invc_oid")) & " "
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                        End Try
                    End With
                End If
            End If
        Next
    End Function

    Public Overrides Function insert() As Boolean
        _conf_value = func_coll.get_conf_file("wf_soship_mstr")

        Dim ssqls As New ArrayList
        Dim _soship_oid As Guid
        _soship_oid = Guid.NewGuid

        Dim _soship_code, _soship_trans_id As String
        'Dim _prjd_oid As String
        Dim ds_bantu As New DataSet

        '===========================================================
        Dim _serial, _pt_code As String
        Dim _en_id, _si_id, _loc_id, _pt_id, _qty, _pjc_id As Integer
        Dim _cost, _cost_avg As Double
        '===========================================================

        _soship_trans_id = "I"

        Dim i, i_2, _tran_id As Integer
        _soship_code = func_coll.get_transaction_number("ST", soship_en_id.GetColumnValue("en_code"), "soship_mstr", "soship_code", func_coll.get_tanggal_sistem)
        _tran_id = func_coll.get_id_tran_mstr("iss-so")

        _pjc_id = func_coll.get_query_integer("select pjc_id as col1 from pjc_mstr inner join prj_mstr on prj_code = pjc_code where prj_code ~~*'" + soship_prj_oid.Text + "'")

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.soship_mstr " _
                                            & "( " _
                                            & "  soship_oid, " _
                                            & "  soship_dom_id, " _
                                            & "  soship_en_id, " _
                                            & "  soship_add_by, " _
                                            & "  soship_add_date, " _
                                            & "  soship_code, " _
                                            & "  soship_date, " _
                                            & "  soship_si_id, " _
                                            & "  soship_is_shipment, " _
                                            & "  soship_dt, " _
                                            & "  soship_prj_oid, " _
                                            & "  soship_tran_id, " _
                                            & "  soship_trans_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_soship_oid.ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(soship_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_soship_code) & ",  " _
                                            & SetDate(soship_date.DateTime.Date) & ",  " _
                                            & SetInteger(soship_si_id.EditValue) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_prj_oid.ToString()) & ",  " _
                                            & SetInteger(_tran_id) & ",  " _
                                            & SetSetring(_soship_trans_id) & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.soshipd_det " _
                                                    & "( " _
                                                    & "  soshipd_oid, " _
                                                    & "  soshipd_soship_oid, " _
                                                    & "  soshipd_seq, " _
                                                    & "  soshipd_qty, " _
                                                    & "  soshipd_um, " _
                                                    & "  soshipd_um_conv, " _
                                                    & "  soshipd_qty_real, " _
                                                    & "  soshipd_si_id, " _
                                                    & "  soshipd_loc_id, " _
                                                    & "  soshipd_lot_serial, " _
                                                    & "  soshipd_dt, " _
                                                    & "  soshipd_close_line, " _
                                                    & "  soshipd_prjd_oid, " _
                                                    & "  soshipd_shipment_oid, " _
                                                    & "  soshipd_rea_code_id " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_oid").ToString()) & ",  " _
                                                    & SetSetring(_soship_oid.ToString()) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_lot_serial")) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetSetring("N") & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_prjd_oid").ToString()) & ", " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_shipment_oid").ToString()) & ", " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("soshipd_rea_code_id").ToString()) & " " _
                                                    & ");"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_qty_return = coalesce(prjd_qty_return,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty")) + ", " _
                                                     & " prjd_trans_id = 'I' " _
                                                     & " where prjd_oid = '" + ds_edit.Tables(0).Rows(i).Item("soshipd_prjd_oid").ToString + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update soshipd_det set soshipd_qty_return = coalesce(soshipd_qty_return,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty")) _
                                                     & " where soshipd_oid = '" + ds_edit.Tables(0).Rows(i).Item("soshipd_shipment_oid").ToString + "'" _

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()


                                For j As Integer = 0 To ds_serial.Tables(0).Rows.Count - 1
                                    If ds_edit.Tables(0).Rows(i).Item("soshipd_oid").ToString = ds_serial.Tables(0).Rows(j).Item("soshipds_soshipd_oid") Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                                            & "  public.soshipds_serial " _
                                                            & "( " _
                                                            & "  soshipds_oid, " _
                                                            & "  soshipds_soshipd_oid, " _
                                                            & "  soshipds_seq, " _
                                                            & "  soshipds_qty, " _
                                                            & "  soshipds_qty_real, " _
                                                            & "  soshipds_si_id, " _
                                                            & "  soshipds_loc_id, " _
                                                            & "  soshipds_lot_serial, " _
                                                            & "  soshipds_dt " _
                                                            & ")  " _
                                                            & "VALUES ( " _
                                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                            & SetSetring(ds_serial.Tables(0).Rows(j).Item("soshipds_soshipd_oid".ToString())) & ",  " _
                                                            & SetInteger(j) & ", " _
                                                            & SetDbl(ds_serial.Tables(0).Rows(j).Item("soshipds_qty")) & ",  " _
                                                            & SetDbl(ds_serial.Tables(0).Rows(j).Item("soshipds_qty_real")) & ",  " _
                                                            & SetInteger(ds_serial.Tables(0).Rows(j).Item("soshipds_si_id")) & ",  " _
                                                            & SetInteger(ds_serial.Tables(0).Rows(j).Item("soshipds_loc_id")) & ",  " _
                                                            & SetSetring(ds_serial.Tables(0).Rows(j).Item("soshipds_lot_serial")) & ",  " _
                                                            & "current_timestamp" & "  " _
                                                            & ")"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                Next
                            End If
                        Next

                        '------------------------------------------------------------------------------
                        'Proses Pengurangan untuk lokasi tujuan saat issue
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '------------------------------------------------------------------------------
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                                If ds_edit.Tables(0).Rows(i).Item("soshipd_qty") > 0 Then
                                    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                        i_2 += 1
                                        _en_id = soship_en_id.EditValue
                                        _si_id = soship_si_id.EditValue
                                        _loc_id = ds_edit.Tables(0).Rows(i).Item("soshipd_loc_id")
                                        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                        _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                        _serial = "''"
                                        _qty = ds_edit.Tables(0).Rows(i).Item("soshipd_qty")

                                        If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pjc_id, _pt_id, _serial, _qty) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If

                                        'Update History Inventory                                    
                                        _cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
                                        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                        If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "Project Return", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _cost, _cost_avg, "", soship_date.DateTime) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        Dim _a As Integer
                        i_2 = 0
                        For _a = 0 To ds_edit.Tables(0).Rows.Count - 1
                            For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                                If ds_edit.Tables(0).Rows(_a).Item("soshipd_oid") = ds_serial.Tables(0).Rows(i).Item("soshipds_soshipd_oid") Then
                                    If ds_serial.Tables(0).Rows(i).Item("soshipds_qty") > 0 Then
                                        If ds_edit.Tables(0).Rows(_a).Item("pt_type") = "I" Then
                                            If ds_edit.Tables(0).Rows(_a).Item("pt_ls").ToString.ToUpper = "L" Or ds_edit.Tables(0).Rows(_a).Item("pt_ls").ToString.ToUpper = "S" Then
                                                i_2 += 1
                                                _en_id = soship_en_id.EditValue
                                                _si_id = soship_si_id.EditValue
                                                _loc_id = ds_edit.Tables(0).Rows(_a).Item("soshipd_loc_id")
                                                _pt_id = ds_edit.Tables(0).Rows(_a).Item("pt_id")
                                                _pt_code = ds_edit.Tables(0).Rows(_a).Item("pt_code")
                                                _serial = ds_serial.Tables(0).Rows(i).Item("soshipds_lot_serial")
                                                _qty = ds_serial.Tables(0).Rows(i).Item("soshipds_qty")

                                                If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pjc_id, _pt_id, _serial, _qty) = False Then
                                                    sqlTran.Rollback()
                                                    insert = False
                                                    Exit Function
                                                End If

                                                'Update History Inventory                                    
                                                _cost = ds_edit.Tables(0).Rows(_a).Item("prjd_cost")
                                                _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "Project Return", "", _si_id, _loc_id, _pjc_id, _pt_id, _qty, _cost, _cost_avg, _serial, soship_date.DateTime) = False Then
                                                    sqlTran.Rollback()
                                                    insert = False
                                                    Exit Function
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        Next

                        sqlTran.Commit()
                        after_success()
                        set_row(_soship_oid.ToString, "soship_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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
#End Region

#Region "gv_edit"
    Private Sub soship_prj_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles soship_prj_oid.ButtonClick
        Dim frm As New FProjectSearch()
        frm.set_win(Me)
        frm._en_id = soship_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _rcvd_um_conv As Double = 1
        Dim _rcvd_qty As Double = 1
        Dim _rcvd_qty_real As Double

        Dim _qty_open As Double = 0
        _qty_sisa = 0

        Dim _progress_pay_open As Double = 0
        _progress_pay_sisa = 0

        If e.Column.Name = "soshipd_qty" Then
            Try
                _rcvd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "soshipd_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _progress_pay_sisa = (gv_edit.GetRowCellValue(e.RowHandle, "soshipd_progress_pay"))
            Catch ex As Exception
            End Try

            If _progress_pay_sisa <> 0 Then
                'gv_edit.CancelUpdateCurrentRow()
                gv_edit.SetRowCellValue(e.RowHandle, e.Value, 0)
                gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", 0)
                Exit Sub
            Else
                _rcvd_qty_real = e.Value * _rcvd_um_conv
                gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", _rcvd_qty_real)
            End If

            _qty_open = (gv_edit.GetRowCellValue(e.RowHandle, "qty_open"))
            _qty_sisa = _qty_open - e.Value
            If _qty_open < e.Value Then
                MessageBox.Show("Qty shipment can't more than qty open..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
            End If
        ElseIf e.Column.Name = "soshipd_um_conv" Then
            Try
                _rcvd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "soshipd_qty")))
            Catch ex As Exception
            End Try
            _rcvd_qty_real = e.Value * _rcvd_qty
            gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", _rcvd_qty_real)
        End If
    End Sub

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

        If _col = "loc_desc" Then
            Dim frm As New FLocationSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = soship_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "reason_name" Then
            Dim frm As New FProjectReturnReasonSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = soship_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Try
            gv_edit_serial.Columns("soshipds_soshipd_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipds_soshipd_oid] = '" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("soshipd_oid").ToString & "'")
            gv_edit_serial.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

#End Region

    Private Function cek_avail_serial(ByVal _par_serial As String) As Boolean
        cek_avail_serial = False
        For i As Integer = 0 To ds_serial.Tables(0).Rows.Count - 1
            If _par_serial = ds_serial.Tables(0).Rows(i).Item("rcvds_lot_serial").ToString Then
                cek_avail_serial = True
            End If
        Next
        Return cek_avail_serial
    End Function

    Private Sub gv_edit_serial_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit_serial.DoubleClick
        If (gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "pt_ls")) = "S" Then
            browse_data2(gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "pt_type"))
        Else
            MsgBox("Part Lot/Serial '" & (gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "pt_ls")) & "' is Non Serial Part ! ", MsgBoxStyle.OkOnly, "Non Asset Part")
        End If
    End Sub

    Private Sub browse_data2(ByVal par_type As String)
        Dim _col As String = gv_edit_serial.FocusedColumn.Name
        Dim _row As Integer = gv_edit_serial.FocusedRowHandle
        Dim _soship_en_id As Integer = soship_en_id.EditValue

        If _col = "soshipds_lot_serial" Then
            Dim frm As New FInventorySearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _soship_en_id
            frm._pt_id = gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "pt_id")
            frm._loc_id = gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "soshipd_loc_id")
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_serial_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_serial.InitNewRow
        With gv_edit_serial
            .SetRowCellValue(e.RowHandle, "soshipds_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "soshipds_soshipd_oid", gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "soshipd_oid"))
            .SetRowCellValue(e.RowHandle, "soshipds_si_id", gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "soshipd_si_id"))
            .SetRowCellValue(e.RowHandle, "soshipds_loc_id", gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "soshipd_loc_id"))
            .SetRowCellValue(e.RowHandle, "soshipds_qty", 1)
            .BestFitColumns()
        End With

        If gv_edit_serial.RowCount >= gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "soshipd_qty") Then
            gc_edit_serial.EmbeddedNavigator.Buttons.Append.Enabled = False
        Else
            gc_edit_serial.EmbeddedNavigator.Buttons.Append.Enabled = True
        End If
    End Sub

    Private Sub gv_edit_serial_RowCountChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit_serial.RowCountChanged
        If gv_edit_serial.RowCount >= gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "soshipd_qty") Then
            gc_edit_serial.EmbeddedNavigator.Buttons.Append.Enabled = False
        Else
            gc_edit_serial.EmbeddedNavigator.Buttons.Append.Enabled = True
        End If
    End Sub

    Private Sub xtc_edit_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtc_edit.SelectedPageChanged
        If xtc_edit.SelectedTabPageIndex = 1 Then
            If (gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "pt_ls") <> "S") Or (gv_edit_serial.RowCount >= gv_edit.GetRowCellValue(gv_edit.FocusedRowHandle, "soshipd_qty")) Then
                gc_edit_serial.EmbeddedNavigator.Buttons.Append.Enabled = False
            Else
                gc_edit_serial.EmbeddedNavigator.Buttons.Append.Enabled = True
            End If
        End If
    End Sub
End Class
