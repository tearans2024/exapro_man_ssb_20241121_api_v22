Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FProjectShipmentApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FProjectShipmentApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        gv_email.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
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

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_os, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Trans ID", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "soship_trans_id", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Trans ID", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "soship_trans_id", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "soshipd_oid", False)
        add_column(gv_detail, "soshipd_soship_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Prog.Pay (%)", "soshipd_progress_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conv", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Lot\Serial", "soshipd_lot_serial", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "soshipd_oid", False)
        add_column(gv_email, "soshipd_soship_oid", False)
        add_column(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Qty", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_email, "Prog.Pay (%)", "soshipd_progress_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_email, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "UM Conv", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_email, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Lot\Serial", "soshipd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                Using objload As New master_new.CustomCommand
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            Try
                                ds.Tables("os").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  false as status," _
                                    & "  soship_oid, " _
                                    & "  soship_dom_id, " _
                                    & "  soship_en_id,en_desc, " _
                                    & "  soship_add_by, " _
                                    & "  soship_add_date, " _
                                    & "  soship_upd_by, " _
                                    & "  soship_upd_date, " _
                                    & "  soship_dt, " _
                                    & "  soship_trans_id, " _
                                    & "  soship_tran_id,tran_name, " _
                                    & "  soship_code, " _
                                    & "  soship_date, " _
                                    & "  soship_si_id,si_desc, " _
                                    & "  soship_prj_oid,prj_code, " _
                                    & "  wf_seq, coalesce(useremail,'') as useremail " _
                                    & "FROM " _
                                    & "  public.soship_mstr " _
                                    & "  inner join en_mstr on en_id = soship_en_id " _
                                    & "  inner join si_mstr on si_id = soship_si_id " _
                                    & "  inner join tran_mstr on tran_id = soship_tran_id " _
                                    & "  inner join prj_mstr on prj_oid = soship_prj_oid " _
                                    & " inner join wf_mstr wm on wf_ref_oid = soship_oid " _
                                    & " inner join tconfuser on usernama = soship_add_by " _
                                    & " where soship_en_id = " + le_entity.EditValue.ToString _
                                    & " and wf_user_id in (" + _user_approval + ")" _
                                    & " and wf_iscurrent ~~* 'Y'"
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
                                    & "  soship_oid, " _
                                    & "  soship_dom_id, " _
                                    & "  soship_en_id,en_desc, " _
                                    & "  soship_add_by, " _
                                    & "  soship_add_date, " _
                                    & "  soship_upd_by, " _
                                    & "  soship_upd_date, " _
                                    & "  soship_dt, " _
                                    & "  soship_trans_id, " _
                                    & "  soship_tran_id,tran_name, " _
                                    & "  soship_code, " _
                                    & "  soship_date, " _
                                    & "  soship_ptnr_id,ptnr_name, " _
                                    & "  soship_remarks " _
                                    & "FROM " _
                                    & "  public.soship_mstr " _
                                    & "  inner join en_mstr on en_id = soship_en_id " _
                                    & "  inner join ptnr_mstr on ptnr_id = soship_ptnr_id " _
                                    & "  inner join tran_mstr on tran_id = soship_tran_id " _
                                    & " inner join wf_mstr wm on wf_ref_oid = soship_oid " _
                                    & " where soship_en_id = " + le_entity.EditValue.ToString _
                                    & " and soship_date >= " + SetDate(pr_txttglawal.DateTime) _
                                    & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                    & " and wf_user_id in (" + _user_approval + ")"

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
        Dim _max_seq As Integer

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

                        _max_seq = mf.get_max_seq(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("soship_code"))
                        If _max_seq = ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("wf_seq") Then
                            .SQL = "SELECT  " _
                                & "  soshipd_oid,soship_en_id, " _
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
                                & "  pt_id,pt_code,pt_type,prjd_pt_desc1,prjd_pt_desc2, " _
                                & "  soshipd_progress_pay, " _
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
                                & " inner join wf_mstr wm on wf_ref_oid = soship_oid " _
                                & " where soship_en_id = " + le_entity.EditValue.ToString _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and soship_is_shipment <> 'Y' " _
                                & " and wf_iscurrent ~~* 'Y'"
                            .InitializeCommand()
                            .FillDataSet(ds_detail, "detail")
                            gc_detail.DataSource = ds_detail.Tables("detail")
                            gv_detail.BestFitColumns()
                        Else
                            .SQL = "SELECT  " _
                                & "  soshipd_oid,soship_en_id, " _
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
                                & "  pt_id,pt_code,pt_type,prjd_pt_desc1,prjd_pt_desc2, " _
                                & "  soshipd_progress_pay, " _
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
                                & " inner join wf_mstr wm on wf_ref_oid = soship_oid " _
                                & " where soship_en_id = " + le_entity.EditValue.ToString _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and soship_is_shipment <> 'Y' " _
                                & " and wf_iscurrent ~~* 'Y'"
                            '& "  CASE paod_is_reconfirm when 'Y' then Null when 'N' then paod_eta_confirm end as paod_eta_confirm, " _
                            '    & "  CASE paod_is_reconfirm when 'Y' then Null when 'N' then paod_etd_target end as paod_etd_target " _
                            .InitializeCommand()
                            .FillDataSet(ds_detail, "detail")
                            gc_detail.DataSource = ds_detail.Tables("detail")
                            gv_detail.BestFitColumns()
                        End If

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join soship_mstr on soship_code = wf_ref_code " + _
                              " inner join soshipd_det dt on dt.soshipd_soship_oid = soship_oid " _
                            & " where wf_ref_code in (select soship_code from soship_mstr inner join wf_mstr on wf_ref_code = soship_code " _
                                                  & " where soship_en_id = " + le_entity.EditValue.ToString _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

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
                                & " inner join wf_mstr wm on wf_ref_oid = soship_oid " _
                                & " where soship_en_id = " + le_entity.EditValue.ToString _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()

                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
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
                                & " inner join wf_mstr wm on wf_ref_oid = soship_oid " _
                                & " where soship_en_id = " + le_entity.EditValue.ToString _
                                & " and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join soship_mstr on soship_code = wf_ref_code " + _
                              " inner join soshipd_det dt on dt.soshipd_soship_oid = soship_oid " _
                            & " where wf_ref_code in (select soship_code from soship_mstr inner join wf_mstr on wf_ref_code = soship_code " _
                                                  & "  where soship_en_id = " + le_entity.EditValue.ToString _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and soship_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "

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
                            & " inner join wf_mstr wm on wf_ref_oid = soship_oid " _
                            & " where soship_en_id = " + le_entity.EditValue.ToString _
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
                gv_detail.Columns("soshipd_soship_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipd_soship_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("soship_oid").ToString & "'")
                gv_detail.BestFitColumns()
                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("soship_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("soshipd_soship_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipd_soship_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("soship_oid").ToString & "'")
                gv_email.BestFitColumns()


            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail.Columns("soshipd_soship_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipd_soship_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("soship_oid").ToString & "'")
                gv_detail.BestFitColumns()
                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("soship_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("soshipd_soship_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[soshipd_soship_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("soship_oid").ToString & "'")
                gv_email.BestFitColumns()
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

        _colom = "soship_trans_id"
        _table = "soship_mstr"
        _initial = "soship"
        _type = "soship"
        _title = "Project Shipment"

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

    'Public Overrides Sub approve_wf(ByVal par_colom As String, ByVal par_table As String, ByVal par_initial As String, ByVal par_type As String, _
    '                               ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_user_approval As String, _
    '                               ByVal par_ds As DataSet, ByVal par_gv As Object, ByVal par_title As String)
    '    If MessageBox.Show("Approve Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If

    '    Dim wf_seq, i, _max_seq As Integer
    '    Dim filename, user_wf, user_wf_email, format_email_bantu As String

    '    Try
    '        Using objinsert As New master_new.CustomCommand
    '            With objinsert
    '.Command.Open()
    '                ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    '.Command = .Connection.CreateCommand
    '                    '.Command.Transaction = sqlTran
    '                    '.Command.CommandType = CommandType.Text

    '                    par_ds.Tables("os").AcceptChanges()
    '                    For i = 0 To par_ds.Tables("os").Rows.Count - 1
    '                        If par_ds.Tables("os").Rows(i).Item("status") = True Then

    '                            Try
    '                                par_gv.Columns(par_initial + "_oid").FilterInfo = _
    '                                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" + par_initial + "_oid] = '" & ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString & "'")
    '                                par_gv.BestFitColumns()

    '                                'par_gv.Columns(par_initial + "_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(i).Item(par_initial + "_oid"))
    '                            Catch ex As Exception
    '                            End Try

    '                            .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
    '                                       " wf_aprv_date = current_timestamp, " + _
    '                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
    '                                       " wf_desc = '" + Trim(par_desc) + "', " + _
    '                                       " wf_iscurrent = 'N'," + _
    '                                       " wf_dt = current_timestamp, " + _
    '                                       " wf_date_to = null " + _
    '                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
    '                                       " and wf_user_id in (" + par_user_approval + ")" + _
    '                                       " and wf_iscurrent ~~* 'Y'"

    '                            .Command.ExecuteNonQuery()
    '                            '.Command.Parameters.Clear()

    '                            '.Command.CommandType = CommandType.Text
    '                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
    '                                                   " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
    '                                                   " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString
    '                            .Command.ExecuteNonQuery()
    '                            '.Command.Parameters.Clear()

    '                            _max_seq = mf.get_max_seq(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"))

    '                            If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then
    '                                If CekConfirm(par_ds.Tables("os").Rows(i).Item("pao_oid")) = False Then
    '                                    MessageBox.Show("Data harus di confirm terlebih dahulu..!", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                                    'sqlTran.Rollback()
    '                                    Exit Sub
    '                                End If

    '                                '.Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'I'" + _
    '                                               " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid") + "'"
    '                                .Command.ExecuteNonQuery()
    '                                '.Command.Parameters.Clear()

    '                                'rev by hendrik 2011-03-02 =======================================================
    '                                'update paod_is_reconfirm = N
    '                                '.Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update paod_det set paod_is_reconfirm = 'N' " + _
    '                                               " where paod_pao_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid") + "'"
    '                                .Command.ExecuteNonQuery()
    '                                '.Command.Parameters.Clear()
    '                                '==================================================================================

    '                            End If

    '                            wf_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") + 1
    '                            user_wf = mf.get_user_wf(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), wf_seq)
    '                            user_wf_email = mf.get_email_address(user_wf)

    '                            If user_wf_email <> "" Then
    '                                filename = "c:\syspro\" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + ".xls"
    '                                ExportTo(par_gv, New ExportXlsProvider(filename))

    '                                format_email_bantu = mf.format_email(user_wf, par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), par_type)
    '                                mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
    '                            End If
    '                        End If
    '                    Next

    '                    .Command.Commit()
    '                    MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                    load_data_many(True)
    '                Catch ex As PgSqlException
    '                    'sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Public Overrides Sub cancel_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Cancel Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ssqls As New ArrayList
        Dim i As Integer
        Dim _serial, _pt_code As String
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _qty As Double
        Dim i_2 As Integer = 0

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

                                For Each _dr As DataRow In ds_detail.Tables("detail").Rows
                                    If _dr("soshipd_soship_oid") = par_ds.Tables("os").Rows(i).Item("soship_oid") Then
                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update prjd_det set prjd_is_close = 'N', " + _
                                                               " prjd_qty_shipment = coalesce(prjd_qty_shipment,0) - " + SetDbl(_dr("soshipd_qty")) + ", " + _
                                                               " prjd_progress_pay = coalesce(prjd_progress_pay,0) - " + SetDbl(_dr("soshipd_progress_pay")) + " " + _
                                                               " where prjd_oid = " + SetSetring(_dr("soshipd_prjd_oid").ToString())
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        If _dr("pt_type") = "I" Then
                                            If _dr("soshipd_qty") > 0 Then
                                                i_2 += 1
                                                _en_id = _dr("soship_en_id")
                                                _si_id = _dr("soshipd_si_id")
                                                _loc_id = _dr("soshipd_loc_id")
                                                _pt_id = _dr("pt_id")
                                                _pt_code = _dr("pt_code")
                                                _serial = "''"
                                                _qty = _dr("soshipd_qty")

                                                If update_invc_mstr_plus_local(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                                    'sqlTran.Rollback()
                                                    Exit Sub
                                                End If

                                            End If
                                        End If

                                    End If
                                Next

                                ''.Command = .Connection.CreateCommand
                                ''.Command.Transaction = sqlTran
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

    Private Function update_invc_mstr_plus_local(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double) As Boolean
        update_invc_mstr_plus_local = True
        Dim _invc_oid As String = ""
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.CustomCommand
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
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & " public.invc_mstr   " _
                                                & "SET  " _
                                                & " invc_qty = coalesce(invc_qty,0) + " & SetDbl(1) _
                                                & " WHERE  " _
                                                & " invc_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("invc_oid")) & " "
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                        End Try
                    End With
                Else
                    With par_obj
                        Try
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & " public.invc_mstr   " _
                                                & "SET  " _
                                                & " invc_qty = invc_qty + " & SetDbl(par_qty) _
                                                & " WHERE  " _
                                                & " invc_oid = " & SetSetring(ds_bantu.Tables(0).Rows(i).Item("invc_oid")) & " "
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Return False
                        End Try
                    End With
                End If
            End If
        Next
    End Function

End Class
