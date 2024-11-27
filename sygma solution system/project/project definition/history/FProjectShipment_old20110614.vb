Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FProjectShipment
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _po_oid As String
    Public ds_edit, ds_serial, ds_get As DataSet
    Dim _now As DateTime
    Dim _conf_budget As String
    Public _po_no, _req_no As String
    Public _prj_oid As String
    Dim _qty_sisa As Double
    Dim _progress_pay_sisa As Double
    Dim _conf_value As String
    Dim mf As New master_new.ModFunction

#Region "SettingAwal"
    Private Sub FProjectShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_soship_mstr")

        _po_no = "-"
        _req_no = "-"
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        soship_en_id.Properties.DataSource = dt_bantu
        soship_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        soship_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        soship_en_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            soship_tran_id.Properties.DataSource = dt_bantu
            soship_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            soship_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            soship_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            soship_tran_id.Properties.DataSource = dt_bantu
            soship_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            soship_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            soship_tran_id.ItemIndex = 0
        End If

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_cu_mstr())
        'rcv_cu_id.Properties.DataSource = dt_bantu
        'rcv_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        'rcv_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        'rcv_cu_id.ItemIndex = 0
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
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans ID", "soship_trans_id", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "rcv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "rcv_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "rcv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "rcv_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail1, "soshipd_oid", False)
        add_column(gv_detail1, "soshipd_soship_oid", False)
        add_column_copy(gv_detail1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "Qty Shipment", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "Prog.Pay (%)", "soshipd_progress_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail1, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail1, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_email1, "soshipd_oid", False)
        add_column(gv_email1, "soshipd_soship_oid", False)
        add_column(gv_email1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email1, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email1, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email1, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email1, "Qty Shipment", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email1, "Prog.Pay (%)", "soshipd_progress_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email1, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email1, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email1, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "soshipd_oid", False)
        add_column(gv_edit, "soshipd_prjd_oid", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "soshipd_prjd_oid", False)
        add_column(gv_edit, "soshipd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "soshipd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Shipment", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Prog.Pay Open (%)", "progress_pay_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Prog.Pay (%)", "soshipd_progress_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "IsClose", "soshipd_close_line", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit, "soshipd_um", False)
        add_column(gv_edit, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_wf1, "wf_ref_code", False)
        add_column_copy(gv_wf1, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf1, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf1, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart1, "Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
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
                    & "  soship_tran_id,soship_trans_id,tran_name " _
                    & "FROM  " _
                    & "  public.soship_mstr " _
                    & "  inner join en_mstr on en_id = soship_en_id " _
                    & "  inner join prj_mstr on prj_oid = soship_prj_oid " _
                    & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                    & "  inner join tran_mstr on tran_id = soship_tran_id " _
                    & "  where soship_is_shipment <> 'Y' " _
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
            & "  soshipd_oid, " _
            & "  soshipd_soship_oid, " _
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
            & "  soshipd_dt, " _
            & "  soshipd_qty_inv, " _
            & "  soshipd_close_line, " _
            & "  soshipd_prjd_oid, " _
            & "  pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
            & "  soshipd_progress_pay, " _
            & "  0 as progress_pay_open, " _
            & "  soshipd_progress_pay_inv, " _
            & "  0 as qty_open " _
            & "FROM  " _
            & "  public.soshipd_det " _
            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
            & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
            & "  inner join code_mstr um on um.code_id = soshipd_um " _
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
                & "  soshipd_progress_pay, " _
                & "  0 as progress_pay_open, " _
                & "  soshipd_progress_pay_inv, " _
                & "  0 as qty_open " _
                & "FROM  " _
                & "  public.soshipd_det " _
                & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
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
                        & "  soshipd_progress_pay, " _
                        & "  0 as progress_pay_open, " _
                        & "  soshipd_progress_pay_inv, " _
                        & "  0 as qty_open " _
                        & "FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
                        & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                        & "  inner join code_mstr um on um.code_id = soshipd_um " _
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

        'ds_serial = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            '.SQL = "SELECT  " _
        '            '    & "  rcvds_oid, " _
        '            '    & "  rcvds_rcvd_oid, " _
        '            '    & "  '' as rcvd_pod_oid, " _
        '            '    & "  rcvds_qty, " _
        '            '    & "  rcvds_um, " _
        '            '    & "  rcvds_si_id, " _
        '            '    & "  rcvds_loc_id, " _
        '            '    & "  rcvds_lot_serial, " _
        '            '    & "  rcvds_dt, -1 as pt_id,  " _
        '            '    & "  pod_pt_id,pod_pt_desc1,pod_pt_desc2,pod_pt_class, " _
        '            '    & "  pt_type,po_ptnr_id, " _
        '            '    & "  pt_pl_id,pl_fa_depr " _
        '            '    & "FROM  " _
        '            '    & "  public.rcvds_serial " _
        '            '    & "  inner join rcvd_det on rcvd_oid = rcvds_rcvd_oid " _
        '            '    & "  inner join pod_det on pod_oid = rcvd_pod_oid " _
        '            '    & "  inner join po_mstr on po_oid = pod_po_oid " _
        '            '    & "  inner join pt_mstr on pt_id = pod_pt_id " _
        '            '    & "  inner join pl_mstr on pl_id = pt_pl_id " _
        '            '    & " where rcvds_si_id = -99"
        '            .InitializeCommand()
        '            .FillDataSet(ds_serial, "serial")
        '            gc_serial.DataSource = ds_serial.Tables(0)
        '            gv_serial.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Function

    Public Overrides Function delete_data() As Boolean
        'row = BindingContext(ds.Tables(0)).Position
        'If get_status_project(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid")) <> "D" Then
        '    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Function
        'End If

        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim i As Integer
        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            ds_get = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  soshipd_oid, " _
                            & "  soshipd_soship_oid, " _
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
                            & "  soshipd_dt, " _
                            & "  soshipd_qty_inv, " _
                            & "  soshipd_close_line, " _
                            & "  soshipd_prjd_oid, " _
                            & "  pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
                            & "  soshipd_progress_pay, " _
                            & "  soshipd_progress_pay_inv " _
                            & "FROM  " _
                            & "  public.soshipd_det " _
                            & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                            & "  inner join prjd_det on prjd_oid = soshipd_prjd_oid " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = soshipd_um " _
                            & "  inner join si_mstr on si_id = soshipd_si_id " _
                            & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                            & "  where soshipd_soship_oid = " & SetSetring(ds.Tables(0).Rows(row).Item("soship_oid")) _
                            & "  order by prjd_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_get, "get")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from soship_mstr where soship_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update prjd_det
                            For Each _dr As DataRow In ds_get.Tables("get").Rows
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_qty_shipment = coalesce(prjd_qty_shipment,0) - " & SetDbl(_dr("soshipd_qty")) _
                                                     & " ,prjd_progress_pay = coalesce(prjd_progress_pay,0) - " & SetDbl(_dr("soshipd_progress_pay")) _
                                                     & " ,prjd_is_close = 'N' " _
                                                     & " where prjd_oid = '" + _dr("soshipd_prjd_oid").ToString + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update prjd_det set prjd_is_close = 'N'" _
                                '                     & " where prjd_oid = '" + _dr("soshipd_prjd_oid").ToString + "'"
                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                            
                            sqlTran.Commit()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            sqlTran.Rollback()
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
        'gv_serial.UpdateCurrentRow()

        ds_edit.AcceptChanges()
        'ds_serial.AcceptChanges()

        Dim _date As Date = func_coll.get_tanggal_sistem
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(soship_en_id.EditValue, "gcald_ic", _date)

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

        Dim i, j As Integer
        Dim _qty, _qty_ttl_serial As Double

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

        ''***********************************************************************
        ''Mencari apakah receive barang yang Serial mempunyai qty lebih dari 1
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "S" Then
        '        For j = 0 To ds_serial.Tables(0).Rows.Count - 1
        '            If ds_edit.Tables(0).Rows(i).Item("rcvd_oid") = ds_serial.Tables(0).Rows(j).Item("rcvds_rcvd_oid") Then
        '                If ds_serial.Tables(0).Rows(j).Item("rcvds_qty") > 1 Then
        '                    MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Serial Qty Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                    BindingContext(ds_edit.Tables(0)).Position = i
        '                    Return False
        '                End If
        '            End If
        '        Next
        '    End If
        'Next
        ''***********************************************************************

        ''***********************************************************************
        ''Mencari apakah receive barang yang Serial mempunyai detail nya atau tidak
        ''dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "S" Then
        '        _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
        '        _qty_ttl_serial = 0
        '        For j = 0 To ds_serial.Tables(0).Rows.Count - 1
        '            If ds_edit.Tables(0).Rows(i).Item("rcvd_oid") = ds_serial.Tables(0).Rows(j).Item("rcvds_rcvd_oid") Then
        '                _qty_ttl_serial = _qty_ttl_serial + 1
        '            End If
        '        Next
        '        If _qty <> _qty_ttl_serial Then
        '            MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Serial Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            BindingContext(ds_edit.Tables(0)).Position = i
        '            Return False
        '        End If
        '    End If
        'Next
        ''***********************************************************************

        ''***********************************************************************
        ''Mencari apakah receive barang yang Lot mempunyai detail nya atau tidak
        ''dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "L" Then
        '        _qty = ds_edit.Tables(0).Rows(i).Item("rcvd_qty_real")
        '        _qty_ttl_serial = 0
        '        For j = 0 To ds_serial.Tables(0).Rows.Count - 1
        '            If ds_edit.Tables(0).Rows(i).Item("rcvd_oid") = ds_serial.Tables(0).Rows(j).Item("rcvds_rcvd_oid") Then
        '                _qty_ttl_serial = _qty_ttl_serial + ds_serial.Tables(0).Rows(j).Item("rcvds_qty")
        '            End If
        '        Next
        '        If _qty <> _qty_ttl_serial Then
        '            MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Lot Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            BindingContext(ds_edit.Tables(0)).Position = i
        '            Return False
        '        End If
        '    End If
        'Next

        'Dim _po_ptnr_id As Integer = 0
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select po_ptnr_id from po_mstr where po_oid = " + SetSetring(_po_oid)
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _po_ptnr_id = .DataReader("po_ptnr_id")
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        'If _po_ptnr_id = 0 Then
        '    MessageBox.Show("Data Supplier Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

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

    Public Overrides Function insert() As Boolean
        _conf_value = func_coll.get_conf_file("wf_soship_mstr")

        Dim ssqls As New ArrayList
        Dim _soship_oid As Guid
        _soship_oid = Guid.NewGuid

        Dim _soship_code, _soship_trans_id As String
        Dim _prjd_oid As String
        Dim ds_bantu As New DataSet

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(soship_tran_id.EditValue)
            _soship_trans_id = "D"
        Else
            _soship_trans_id = "I"
        End If

        Dim _is_close As String = ""
        Dim i, i_2, i_3 As Integer
        _soship_code = func_coll.get_transaction_number("SS", soship_en_id.GetColumnValue("en_code"), "soship_mstr", "soship_code", func_coll.get_tanggal_sistem)

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
                                            & SetInteger(soship_tran_id.EditValue) & ",  " _
                                            & SetSetring(_soship_trans_id) & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _prog_pay_open, _qty_open As Double
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("soshipd_progress_pay") > 0 Then
                                _prog_pay_open = ds_edit.Tables(0).Rows(i).Item("progress_pay_open")
                                _qty_open = ds_edit.Tables(0).Rows(i).Item("qty_open")
                                If _prog_pay_open = ds_edit.Tables(0).Rows(i).Item("soshipd_progress_pay") AndAlso _qty_open = ds_edit.Tables(0).Rows(i).Item("soshipd_qty") Then
                                    _is_close = "Y"
                                Else
                                    _is_close = "N"
                                End If
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
                                                    & "  soshipd_progress_pay " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid().ToString()) & ",  " _
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
                                                    & SetSetring(_is_close) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("soshipd_prjd_oid").ToString()) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_progress_pay")) & "  " _
                                                    & ");"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update prjd_det
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_qty_shipment = coalesce(prjd_qty_shipment,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_qty")) _
                                                     & " ,prjd_progress_pay = coalesce(prjd_progress_pay,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("soshipd_progress_pay")) _
                                                     & " where prjd_oid = '" + ds_edit.Tables(0).Rows(i).Item("soshipd_prjd_oid").ToString + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If _is_close = "Y" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update prjd_det set prjd_is_close = 'C'" _
                                                         & " where prjd_oid = '" + ds_edit.Tables(0).Rows(i).Item("soshipd_prjd_oid").ToString + "'"
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                                
                            End If

                        Next

                        If _conf_value = "1" Then
                            Dim a As Integer
                            For a = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
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
                                                        & SetInteger(soship_en_id.EditValue) & ",  " _
                                                        & SetSetring(soship_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_soship_oid.ToString()) & ",  " _
                                                        & SetSetring(_soship_code) & ",  " _
                                                        & SetSetring("Project Shipment") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(a).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(a).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " current_timestamp " & "  " _
                                                        & ")"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

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
            _rcvd_qty_real = e.Value * _rcvd_um_conv
            gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", _rcvd_qty_real)

            _qty_open = (gv_edit.GetRowCellValue(e.RowHandle, "qty_open"))
            _qty_sisa = _qty_open - e.Value
            If _qty_open < e.Value Then
                MessageBox.Show("Qty shipment can't more than qty open..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
            End If

            'If _qty_sisa <= 0 Then
            '    gv_edit.SetRowCellValue(e.RowHandle, "soshipd_close_line", "Y")
            'Else
            '    gv_edit.SetRowCellValue(e.RowHandle, "soshipd_close_line", "N")
            'End If
        ElseIf e.Column.Name = "soshipd_progress_pay" Then
            'Try
            '    _rcvd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "soshipd_um_conv"))
            'Catch ex As Exception
            'End Try
            '_rcvd_qty_real = e.Value * _rcvd_um_conv
            'gv_edit.SetRowCellValue(e.RowHandle, "soshipd_qty_real", _rcvd_qty_real)

            _progress_pay_open = (gv_edit.GetRowCellValue(e.RowHandle, "progress_pay_open"))
            _progress_pay_sisa = _progress_pay_open - e.Value
            If _progress_pay_open < e.Value Then
                MessageBox.Show("Progress payment can't more than Progress payment open..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
            End If

            If _progress_pay_sisa <= 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "soshipd_close_line", "Y")
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "soshipd_close_line", "N")
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

        'If _col = "loc_desc" Then
        '    Dim frm As New FLocationSearch()
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = soship_en_id.EditValue
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Try
        '    gv_serial.Columns("rcvds_rcvd_oid").FilterInfo = _
        '    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rcvds_rcvd_oid] = '" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("rcvd_oid").ToString & "'")
        '    gv_serial.BestFitColumns()
        'Catch ex As Exception
        'End Try
    End Sub

#End Region

    Public Function get_tanggal_sistem() As Date
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select current_date as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_tanggal_sistem = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_tanggal_sistem
    End Function

    Private Function cek_avail_serial(ByVal _par_serial As String) As Boolean
        cek_avail_serial = False
        For i As Integer = 0 To ds_serial.Tables(0).Rows.Count - 1
            If _par_serial = ds_serial.Tables(0).Rows(i).Item("rcvds_lot_serial").ToString Then
                cek_avail_serial = True
            End If
        Next
        Return cek_avail_serial
    End Function


#Region "Approval"

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid")
        _colom = "soship_trans_id"
        _table = "soship_mstr"
        _criteria = "soship_code"
        _initial = "soship"
        _type = "soship"
        _title = "Project Shipment"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    'Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
    '                               ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
    '    Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _pby_code As String

    '    If mf.get_transaction_status(par_colom, par_table, par_criteria, par_code) <> "D" Then
    '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End If

    '    If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If

    '    _pby_code = par_code
    '    _trn_status = "W"
    '    user_wf = mf.get_user_wf(par_code, 0)
    '    user_wf_email = mf.get_email_address(user_wf)

    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
    '                                           " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
    '                                           " " + par_initial + "_upd_date = current_timestamp " + _
    '                                           " where " + par_initial + "_oid = '" + par_oid + "'"

    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    If get_status_wf(par_code.ToString()) = 0 Then
    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
    '                                               " where wf_ref_code ~~* '" + par_code + "'" + _
    '                                               " and wf_seq = 0"

    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                        'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
    '                                               " where wf_ref_code ~~* '" + par_code + "'"

    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                    ElseIf get_status_wf(par_code.ToString()) > 0 Then
    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "update wf_mstr set " + _
    '                                               " wf_iscurrent = 'Y', " + _
    '                                               " wf_wfs_id = '0', " + _
    '                                               " wf_desc = '', " + _
    '                                               " wf_date_to = null, " + _
    '                                               " wf_aprv_user = '', " + _
    '                                               " wf_aprv_date = null " + _
    '                                               " where wf_ref_code ~~* '" + par_code + "'" + _
    '                                               " and wf_wfs_id = '4' "
    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()
    '                    End If


    '                    sqlTran.Commit()
    '                    format_email_bantu = mf.format_email(user_wf, par_code, par_type)

    '                    filename = "c:\syspro\" + par_code + ".xls"
    '                    ExportTo(par_gv, New ExportXlsProvider(filename))

    '                    If user_wf_email <> "" Then
    '                        mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
    '                    Else
    '                        'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                    End If

    '                    MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    help_load_data(True)
    '                    set_row(Trim(par_oid), par_initial + "_oid")
    '                Catch ex As PgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid")
        _colom = "soship_trans_id"
        _table = "soship_mstr"
        _criteria = "soship_code"
        _initial = "soship"
        _type = "soship"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)
        _conf_value = func_coll.get_conf_file("wf_soship_mstr")

        Dim _soship_trans_id As String = ""
        Dim ssqls As New ArrayList

        If _conf_value = "1" Then
            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select soship_trans_id from soship_mstr where soship_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid").ToString)
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _soship_trans_id = .DataReader("soship_trans_id").ToString
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        If _soship_trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _soship_trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _soship_trans_id.ToUpper = "X" Then
            MessageBox.Show("Can't Cancel For Cancel Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For Each _dr As DataRow In ds.Tables("detail").Rows
                            If _dr("soshipd_soship_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_oid") Then
                                'If _dr("paod_qty_mo") > 0 Then
                                '    MessageBox.Show("Can't Cancel For Qty MO more than 0..!", "Conf", MessageBoxButtons.OK)
                                '    sqlTran.Rollback()
                                '    Exit Sub
                                'End If

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update prjd_det set prjd_is_close = 'N', " + _
                                                       " prjd_qty_shipment = coalesce(prjd_qty_shipment,0) - " + SetDbl(_dr("soshipd_qty")) + ", " + _
                                                       " prjd_progress_pay = coalesce(prjd_progress_pay,0) - " + SetDbl(_dr("soshipd_progress_pay")) + " " + _
                                                       " where prjd_oid = " + SetSetring(_dr("soshipd_prjd_oid").ToString())
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where " + par_criteria + " = '" + par_code + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                               " where wf_ref_code = '" + par_code + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
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
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("soship_code")
        _type = "soship"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Project Shipment"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    'Public Overrides Sub smart_approve()
    '    If _conf_value = "0" Then
    '        Exit Sub
    '    End If

    '    Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
    '    Dim i As Integer
    '    Dim _po_is_budget As String = ""

    '    If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If

    '    ds.Tables("smart").AcceptChanges()

    '    For i = 0 To ds.Tables("smart").Rows.Count - 1
    '        If ds.Tables("smart").Rows(i).Item("status") = True Then

    '            Try
    '                gv_email.Columns("soship_oid").FilterInfo = _
    '                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soship_oid] = '" & ds.Tables("smart").Rows(i).Item("soship_oid").ToString & "'")
    '                gv_email.BestFitColumns()

    '                'gv_email.Columns("po_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("po_oid"))
    '            Catch ex As Exception
    '            End Try

    '            _trans_id = "W" 'default langsung ke W 
    '            user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("soship_code"), 0)
    '            user_wf_email = mf.get_email_address(user_wf)

    '            Try
    '                Using objinsert As New master_new.WDABasepgsql("", "")
    '                    With objinsert
    '                        .Connection.Open()
    '                        Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
    '                        Try
    '                            .Command = .Connection.CreateCommand
    '                            .Command.Transaction = sqlTran
    '                            .Command.CommandType = CommandType.Text
    '                            .Command.CommandText = "update pao_mstr set pao_trans_id = '" + _trans_id + "'," + _
    '                                           " pao_upd_by = '" + master_new.ClsVar.sNama + "'," + _
    '                                           " pao_upd_date = current_timestamp " + _
    '                                           " where pao_oid = '" + ds.Tables("smart").Rows(i).Item("pao_oid") + "'"

    '                            .Command.ExecuteNonQuery()
    '                            .Command.Parameters.Clear()

    '                            '============================================================================
    '                            If get_status_wf(ds.Tables("smart").Rows(i).Item("pao_code")) = 0 Then
    '                                .Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
    '                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("pao_code") + "'" + _
    '                                                       " and wf_seq = 0"

    '                                .Command.ExecuteNonQuery()
    '                                .Command.Parameters.Clear()

    '                                'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
    '                                .Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
    '                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("pao_code") + "'"

    '                                .Command.ExecuteNonQuery()
    '                                .Command.Parameters.Clear()

    '                            ElseIf get_status_wf(ds.Tables("smart").Rows(i).Item("pao_code")) > 0 Then
    '                                .Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update wf_mstr set " + _
    '                                                       " wf_iscurrent = 'Y', " + _
    '                                                       " wf_wfs_id = '0', " + _
    '                                                       " wf_desc = '', " + _
    '                                                       " wf_date_to = null, " + _
    '                                                       " wf_aprv_user = '', " + _
    '                                                       " wf_aprv_date = null " + _
    '                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("pao_code") + "'" + _
    '                                                       " and wf_wfs_id = '4' "
    '                                .Command.ExecuteNonQuery()
    '                                .Command.Parameters.Clear()
    '                            End If
    '                            '============================================================================

    '                            sqlTran.Commit()

    '                            format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("pao_code"), "pao")

    '                            filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("pao_code") + ".xls"
    '                            ExportTo(gv_email, New ExportXlsProvider(filename))

    '                            If user_wf_email <> "" Then
    '                                mf.sent_email(user_wf_email, "", mf.title_email("Project", ds.Tables("smart").Rows(i).Item("pao_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
    '                            Else
    '                                MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                            End If

    '                        Catch ex As PgSqlException
    '                            sqlTran.Rollback()
    '                            MessageBox.Show(ex.Message)
    '                        End Try
    '                    End With
    '                End Using
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '            End Try
    '        End If
    '    Next

    '    help_load_data(True)
    '    MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Sub

#End Region

End Class
