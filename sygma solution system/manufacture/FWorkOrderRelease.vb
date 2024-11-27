Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FWorkOrderRelease

    Public dt_bantu As DataTable
    Public dt_wod_temp As New DataTable
    Dim dr As DataRow
    Public dr_print As DataRow
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public _wo_oid_mstr As String
    Dim _wo_oid_show As String
    Dim ds_edit As DataSet
    Dim ds_show As DataSet
    Dim ds_show_det As DataSet
    Dim ds_wod_temp As DataSet
    Dim ds_avail As DataSet
    Dim ds_print As DataSet
    Dim ds_printing As DataSet
    Public dt_print As New DataTable
    Dim ds_warehouse As DataSet
    Dim dt_warehouse As DataTable

    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _wod_related_oid As String = ""

    Private Sub FWorkOrderRelease_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        par_entity.Properties.DataSource = dt_bantu
        par_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        par_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        par_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "wo_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Order", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Comp.", "wo_qty_comp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Reject", "wo_qty_rjc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Yield Percent", "wo_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_master, "wo_bom_id", False)
        add_column_copy(gv_master, "Bom Desc.", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "wo_ro_id", False)
        add_column_copy(gv_master, "Routing Desc.", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "wo_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "wod_oid", False)
        add_column(gv_detail, "wod_wo_oid", False)
        add_column_copy(gv_detail, "Use BOM", "wod_use_bom", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_detail, "wod_pt_bom_id", False)
        add_column_copy(gv_detail, "PT/BOM Desc", "ptbomdesc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Component", "wod_comp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Operation", "wod_op", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Per", "wod_qty_per", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Alloc", "wod_qty_alloc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Picked", "wod_qty_picked", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Issued", "wod_qty_issued", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Cost", "wod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_edit, "Use BOM", "psd_use_bom", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit, "psd_pt_bom_id", False)
        add_column_copy(gv_edit, "PT/BOM Desc", "ptbomdesc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Component", "psd_comp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Operation", "psd_op", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Qty Per", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Qty Req", "tot_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
       
       
        add_column_copy(gv_available, "PT/BOM Desc", "pt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_available, "Requirement", "pt_req", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_available, "Available", "pt_avail", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_available, "Warehouse", "pt_ware_desc", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_available, "Status", "pt_status", DevExpress.Utils.HorzAlignment.Center)
        
    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  wo_oid, " _
                    & "  wo_dom_id, " _
                    & "  wo_en_id, " _
                    & "  wo_si_id, " _
                    & "  wo_id, " _
                    & "  wo_code, " _
                    & "  wo_type, " _
                    & "  wo_pt_id, " _
                    & "  wo_qty_ord, " _
                    & "  wo_qty_comp, " _
                    & "  wo_qty_rjc, " _
                    & "  wo_ord_date, " _
                    & "  wo_rel_date, " _
                    & "  wo_due_date, " _
                    & "  wo_yield_pct, " _
                    & "  wo_bom_id, " _
                    & "  wo_ro_id, " _
                    & "  wo_status, " _
                    & "  wo_remarks, " _
                    & "  si_desc, " _
                    & "  pt_desc1, " _
                    & "  bom_desc, " _
                    & "  ro_desc, " _
                    & "  en_desc, " _
                    & "  wo_dt " _
                    & "FROM  " _
                    & "  public.wo_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.wo_mstr.wo_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.wo_mstr.wo_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.bom_mstr ON (public.wo_mstr.wo_bom_id = public.bom_mstr.bom_id) " _
                    & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id) " _
                    & "  where wo_mstr.wo_status = 'R' " _
                    & "  and wo_rel_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and wo_rel_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and wo_en_id in (select user_en_id from tconfuserentity " _
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
            & "  wod_oid, " _
            & "  wod_wo_oid, " _
            & "  wod_use_bom, " _
            & "  wod_pt_bom_id, " _
            & "  wod_comp, " _
            & "  wod_op, " _
            & "  wod_qty_per, " _
            & "  wod_qty_req, " _
            & "  wod_qty_alloc, " _
            & "  wod_qty_picked, " _
            & "  wod_qty_issued, " _
            & "  wod_cost, " _
            & "   CASE WHEN wod_use_bom = 'Y' " _
            & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=wod_pt_bom_id) " _
            & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=wod_pt_bom_id) " _
            & "   END AS ptbomdesc, " _
            & "  wod_dt " _
            & "FROM  " _
            & "  public.wod_det " _
            & "  inner join wo_mstr on wo_mstr.wo_oid = wod_det.wod_wo_oid " _
            & "  where wo_mstr.wo_status = 'R' " _
            & "  and wo_mstr.wo_rel_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and wo_mstr.wo_rel_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("wod_wo_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wod_wo_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "valuechanged"
   
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        wo_code.Focus()
        wo_code.Text = ""
        wo_en_id.Text = ""
        wo_si_id.Text = ""
        part_desc.Text = ""
        wo_qty_ord.Text = ""
        wo_yield_pct.Text = ""
        wo_ord_date.Text = ""
        wo_due_date.Text = ""
        wo_ro_id.Text = ""
        wo_remarks.Text = ""

        

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub


    Public Overrides Function before_save() As Boolean
        before_save = True
        'gv_edit.UpdateCurrentRow()
        'ds_edit.AcceptChanges()
        'If ds_edit.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If ds_edit.Tables(0).Rows(i).Item("reqd_qty") = 0 Then
        '        MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'Next

        If IsDBNull(part_desc.EditValue) Then
            MessageBox.Show("Data cannot null...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return before_save
    End Function

    'Public Overrides Function insert() As Boolean
    '    Dim sql As String
    '    Dim _wo_oid As Guid
    '    _wo_oid = Guid.NewGuid

    '    Dim _wo_code As String

    '    Dim i As Integer

    '    _wo_code = func_coll.get_transaction_number("WO", wo_en_id.GetColumnValue("en_code"), "wo_mstr", "wo_code")



    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.wo_mstr " _
    '                                        & "( " _
    '                                        & "  wo_oid, " _
    '                                        & "  wo_dom_id, " _
    '                                        & "  wo_en_id, " _
    '                                        & "  wo_si_id, " _
    '                                        & "  wo_id, " _
    '                                        & "  wo_code, " _
    '                                        & "  wo_pt_id, " _
    '                                        & "  wo_qty_ord, " _
    '                                        & "  wo_qty_comp, " _
    '                                        & "  wo_qty_rjc, " _
    '                                        & "  wo_ord_date, " _
    '                                        & "  wo_due_date, " _
    '                                        & "  wo_yield_pct, " _
    '                                        & "  wo_bom_id, " _
    '                                        & "  wo_ro_id, " _
    '                                        & "  wo_status, " _
    '                                        & "  wo_remarks, " _
    '                                        & "  wo_dt " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(_wo_oid.ToString) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetIntegerDB(wo_en_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_si_id.EditValue) & ",  " _
    '                                        & SetInteger(func_coll.GetID("wo_mstr", wo_en_id.GetColumnValue("en_code"), "wo_id", "wo_en_id", wo_en_id.EditValue.ToString)) & ",  " _
    '                                        & SetSetring(_wo_code.ToString) & ",  " _
    '                                        & SetIntegerDB(wo_pt_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_qty_ord.EditValue) & ",  " _
    '                                        & "0,  " _
    '                                        & "0,  " _
    '                                        & SetDate(wo_ord_date.EditValue) & ",  " _
    '                                        & SetDate(wo_due_date.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_yield_pct.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_bom_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_ro_id.EditValue) & ",  " _
    '                                        & "'F' ,  " _
    '                                        & SetSetring(wo_remarks.Text) & ",  " _
    '                                        & "current_timestamp " _
    '                                        & ");"

    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    'Untuk generate WO (release)
    '                    '========================================================================
    '                    'ds_edit = New DataSet
    '                    'Try
    '                    '    Using objcb As New master_new.WDABasepgsql("", "")
    '                    '        With objcb
    '                    '            sql = "SELECT  " _
    '                    '                & "  psd_det.psd_oid, " _
    '                    '                & "  psd_det.psd_ps_oid, " _
    '                    '                & "  psd_det.psd_use_bom, " _
    '                    '                & "  psd_det.psd_desc as ptbomdesc, " _
    '                    '                & "  psd_det.psd_pt_bom_id, " _
    '                    '                & "  psd_det.psd_comp, " _
    '                    '                & "  psd_det.psd_ref, " _
    '                    '                & "  psd_det.psd_desc, " _
    '                    '                & "  psd_det.psd_start_date, " _
    '                    '                & "  psd_det.psd_end_date, " _
    '                    '                & "  psd_det.psd_qty, " _
    '                    '                & "  psd_det.psd_str_type, " _
    '                    '                & "  psd_det.psd_scrp_pct, " _
    '                    '                & "  psd_det.psd_lt_off, " _
    '                    '                & "  psd_det.psd_op, " _
    '                    '                & "  psd_det.psd_seq, " _
    '                    '                & "  psd_det.psd_fcst_pct, " _
    '                    '                & "  psd_det.psd_group, " _
    '                    '                & "  psd_det.psd_process, " _
    '                    '                & "   CASE WHEN psd_use_bom = 'Y' " _
    '                    '                & "   THEN 0 " _
    '                    '                & "   ELSE (SELECT pt_mstr.pt_cost from pt_mstr where pt_id=psd_pt_bom_id) " _
    '                    '                & "   END AS ptbomcost " _
    '                    '                & "FROM " _
    '                    '                & "  psd_det " _
    '                    '                & "  INNER JOIN ps_mstr ON (psd_det.psd_ps_oid = ps_mstr.ps_oid) "

    '                    '            If wo_bom_id.Text = "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_pt_id.EditValue
    '                    '            ElseIf wo_bom_id.Text <> "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_bom_id.EditValue
    '                    '            End If

    '                    '            .SQL = sql
    '                    '            .InitializeCommand()
    '                    '            .FillDataSet(ds_edit, "insert_edit")
    '                    '        End With
    '                    '    End Using
    '                    'Catch ex As Exception
    '                    '    MessageBox.Show(ex.Message)
    '                    'End Try

    '                    'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
    '                    '    If SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) = "Y" Then




    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & "(SELECT sum(pt_cost) as totbom " _
    '                    '                               & " from ""public"".""GetAllProductStructureRaw""(" _
    '                    '                               & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ", " _
    '                    '                               & " 1 " & ")),  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    Else

    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("ptbomcost")) & ",  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    End If
    '                    '=======================================================================================================================================================
    '                    'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1

    '                    '    .Command.CommandType = CommandType.Text
    '                    '    .Command.CommandText = "INSERT INTO  " _
    '                    '                        & "  public.wod_det " _
    '                    '                        & "( " _
    '                    '                        & "  wod_oid, " _
    '                    '                        & "  wod_wo_oid, " _
    '                    '                        & "  wod_use_bom, " _
    '                    '                        & "  wod_pt_bom_id, " _
    '                    '                        & "  wod_comp, " _
    '                    '                        & "  wod_op, " _
    '                    '                        & "  wod_qty_per, " _
    '                    '                        & "  wod_qty_req, " _
    '                    '                        & "  wod_cost, " _
    '                    '                        & "  wod_dt " _
    '                    '                        & ")  " _
    '                    '                        & "VALUES ( " _
    '                    '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                        & SetSetring(_wo_oid.ToString) & ",  " _
    '                    '                        & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                        & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                        & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                        & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("ptbomcost")) & ",  " _
    '                    '                        & " current_timestamp " _
    '                    '                        & ");"


    '                    '    .Command.ExecuteNonQuery()
    '                    '    .Command.Parameters.Clear()


    '                    'Next

    '                    sqlTran.Commit()

    '                    after_success()
    '                    set_row(_wo_oid.ToString, "wo_oid")
    '                    'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    '                    insert = True
    '                Catch ex As PgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        row = 0
    '        insert = False
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return insert
    'End Function
    '==========================================================================
    'Public Overrides Function edit_data() As Boolean
    '    If MyBase.edit_data = True Then

    '        row = BindingContext(ds.Tables(0)).Position

    '        With ds.Tables(0).Rows(row)
    '            _wo_oid_mstr = .Item("wo_oid")
    '            wo_en_id.EditValue = .Item("wo_en_id")
    '            wo_si_id.EditValue = .Item("wo_si_id")
    '            wo_pt_id.EditValue = .Item("wo_pt_id")
    '            wo_qty_ord.EditValue = .Item("wo_qty_ord")
    '            wo_ord_date.DateTime = .Item("wo_ord_date")
    '            wo_due_date.DateTime = .Item("wo_due_date")
    '            wo_yield_pct.EditValue = .Item("wo_yield_pct")
    '            wo_bom_id.EditValue = .Item("wo_bom_id")
    '            wo_ro_id.EditValue = .Item("wo_ro_id")
    '            wo_remarks.Text = SetString(.Item("wo_remarks"))


    '        End With
    '        wo_en_id.Focus()

    '        Try
    '            tcg_header.SelectedTabPageIndex = 0
    '        Catch ex As Exception
    '        End Try

    '        'ds_edit = New DataSet
    '        ''ds_update_related = New DataSet
    '        'Try
    '        '    Using objcb As New master_new.WDABasepgsql("", "")
    '        '        With objcb
    '        '            .SQL = "SELECT  " _
    '        '                    & "  public.reqd_det.reqd_oid, " _
    '        '                    & "  public.reqd_det.reqd_dom_id, " _
    '        '                    & "  public.reqd_det.reqd_en_id, " _
    '        '                    & "  public.en_mstr.en_desc, " _
    '        '                    & "  public.reqd_det.reqd_add_by, " _
    '        '                    & "  public.reqd_det.reqd_add_date, " _
    '        '                    & "  public.reqd_det.reqd_upd_by, " _
    '        '                    & "  public.reqd_det.reqd_upd_date, " _
    '        '                    & "  public.reqd_det.reqd_req_oid, " _
    '        '                    & "  public.reqd_det.reqd_seq, " _
    '        '                    & "  public.reqd_det.reqd_related_oid, " _
    '        '                    & "  req_mstr_relation.req_code as req_code_relation, " _
    '        '                    & "  public.reqd_det.reqd_ptnr_id, " _
    '        '                    & "  public.ptnr_mstr.ptnr_name, " _
    '        '                    & "  public.reqd_det.reqd_si_id, " _
    '        '                    & "  public.si_mstr.si_desc, " _
    '        '                    & "  public.reqd_det.reqd_pt_id, " _
    '        '                    & "  public.pt_mstr.pt_code, " _
    '        '                    & "  public.pt_mstr.pt_desc1, " _
    '        '                    & "  public.pt_mstr.pt_desc2, " _
    '        '                    & "  public.reqd_det.reqd_rmks, " _
    '        '                    & "  public.reqd_det.reqd_end_user, " _
    '        '                    & "  public.reqd_det.reqd_qty, " _
    '        '                    & "  public.reqd_det.reqd_qty_processed, " _
    '        '                    & "  public.reqd_det.reqd_qty_completed, " _
    '        '                    & "  public.reqd_det.reqd_um, " _
    '        '                    & "  public.code_mstr.code_name, " _
    '        '                    & "  public.reqd_det.reqd_cost, " _
    '        '                    & "  public.reqd_det.reqd_disc, " _
    '        '                    & "  public.reqd_det.reqd_need_date, " _
    '        '                    & "  public.reqd_det.reqd_due_date, " _
    '        '                    & "  public.reqd_det.reqd_um_conv, " _
    '        '                    & "  public.reqd_det.reqd_qty_real, " _
    '        '                    & "  public.reqd_det.reqd_pt_class, " _
    '        '                    & "  public.reqd_det.reqd_status, " _
    '        '                    & "  public.reqd_det.reqd_dt, ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
    '        '                    & "FROM " _
    '        '                    & "  public.reqd_det " _
    '        '                    & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
    '        '                    & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
    '        '                    & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
    '        '                    & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
    '        '                    & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
    '        '                    & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid)" _
    '        '                    & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid " _
    '        '                    & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
    '        '                    & " where public.reqd_det.reqd_req_oid = '" + ds.Tables(0).Rows(row).Item("req_oid").ToString + "'"

    '        '            .InitializeCommand()
    '        '            .FillDataSet(ds_edit, "detail")
    '        '            'ds_update_related adalah dataset untuk membantu update reqd_qty_processed kembali ke posisi semula dulu...
    '        '            '.FillDataSet(ds_update_related, "update_related")
    '        '            gc_edit.DataSource = ds_edit.Tables(0)
    '        '            gv_edit.BestFitColumns()
    '        '        End With
    '        '    End Using
    '        'Catch ex As Exception
    '        '    MessageBox.Show(ex.Message)
    '        'End Try

    '        edit_data = True
    '    End If
    'End Function
    '================================================================================
    'Public Overrides Function edit()
    '    edit = True
    '    Dim sql As String
    '    Dim i As Integer

    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "UPDATE  " _
    '                                        & "  public.wo_mstr   " _
    '                                        & "SET  " _
    '                                        & "  wo_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
    '                                        & "  wo_en_id = " & wo_en_id.EditValue & ",  " _
    '                                        & "  wo_si_id = " & wo_si_id.EditValue & ",  " _
    '                                        & "  wo_pt_id = " & wo_pt_id.EditValue & ",  " _
    '                                        & "  wo_qty_ord = " & SetIntegerDB(wo_qty_ord.EditValue) & ",  " _
    '                                        & "  wo_ord_date = " & SetDate(wo_due_date.DateTime) & ",  " _
    '                                        & "  wo_due_date = " & SetDate(wo_due_date.DateTime) & ",  " _
    '                                        & "  wo_ord_date = current_timestamp ,  " _
    '                                        & "  wo_yield_pct = " & SetIntegerDB(wo_yield_pct.EditValue) & ",  " _
    '                                        & "  wo_bom_id = " & SetSetringDB(wo_bom_id.EditValue) & ",  " _
    '                                        & "  wo_ro_id = " & SetIntegerDB(wo_ro_id.EditValue) & ",  " _
    '                                        & "  wo_remarks = " & SetSetringDB(wo_remarks.Text) & ",  " _
    '                                        & "  wo_dt = current_timestamp  " _
    '                                        & "  " _
    '                                        & "WHERE  " _
    '                                        & "  wo_oid = " & SetSetring(_wo_oid_mstr.ToString) & "  " _
    '                                        & ";"

    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    'untuk generate WO (release)
    '                    '.Command.CommandText = "delete from wod_det where wod_wo_oid = '" + _wo_oid_mstr + "'"
    '                    '.Command.ExecuteNonQuery()
    '                    '.Command.Parameters.Clear()
    '                    ''******************************************************

    '                    ''Insert data detail
    '                    'ds_edit = New DataSet
    '                    'Try
    '                    '    Using objcb As New master_new.WDABasepgsql("", "")
    '                    '        With objcb
    '                    '            sql = "SELECT  " _
    '                    '                & "  psd_det.psd_oid, " _
    '                    '                & "  psd_det.psd_ps_oid, " _
    '                    '                & "  psd_det.psd_use_bom, " _
    '                    '                & "  psd_det.psd_desc as ptbomdesc, " _
    '                    '                & "  psd_det.psd_pt_bom_id, " _
    '                    '                & "  psd_det.psd_comp, " _
    '                    '                & "  psd_det.psd_ref, " _
    '                    '                & "  psd_det.psd_desc, " _
    '                    '                & "  psd_det.psd_start_date, " _
    '                    '                & "  psd_det.psd_end_date, " _
    '                    '                & "  psd_det.psd_qty, " _
    '                    '                & "  psd_det.psd_str_type, " _
    '                    '                & "  psd_det.psd_scrp_pct, " _
    '                    '                & "  psd_det.psd_lt_off, " _
    '                    '                & "  psd_det.psd_op, " _
    '                    '                & "  psd_det.psd_seq, " _
    '                    '                & "  psd_det.psd_fcst_pct, " _
    '                    '                & "  psd_det.psd_group, " _
    '                    '                & "  psd_det.psd_process, " _
    '                    '                & "   CASE WHEN psd_use_bom = 'Y' " _
    '                    '                & "   THEN 0 " _
    '                    '                & "   ELSE (SELECT pt_mstr.pt_cost from pt_mstr where pt_id=psd_pt_bom_id) " _
    '                    '                & "   END AS ptbomcost " _
    '                    '                & "FROM " _
    '                    '                & "  psd_det " _
    '                    '                & "  INNER JOIN ps_mstr ON (psd_det.psd_ps_oid = ps_mstr.ps_oid) "

    '                    '            If wo_bom_id.Text = "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_pt_id.EditValue
    '                    '            ElseIf wo_bom_id.Text <> "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_bom_id.EditValue
    '                    '            End If

    '                    '            .SQL = sql
    '                    '            .InitializeCommand()
    '                    '            .FillDataSet(ds_edit, "insert_edit")
    '                    '        End With
    '                    '    End Using
    '                    'Catch ex As Exception
    '                    '    MessageBox.Show(ex.Message)
    '                    'End Try

    '                    'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
    '                    '    If SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) = "Y" Then
    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid_mstr.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & "(SELECT sum(pt_cost) as totbom " _
    '                    '                               & " from ""public"".""GetAllProductStructureRaw""(" _
    '                    '                               & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ", " _
    '                    '                               & " 1 " & ")),  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    Else

    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid_mstr.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("ptbomcost")) & ",  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    End If

    '                    'Next

    '                    sqlTran.Commit()
    '                    'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    '                    after_success()
    '                    set_row(_wo_oid_mstr, "wo_oid")
    '                    edit = True
    '                Catch ex As PgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        edit = False
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return edit
    'End Function
    '==============================================================================
    'Public Overrides Function before_delete() As Boolean
    '    before_delete = True
    '    'Dim ds_bantu As New DataSet
    '    'Dim i As Integer
    '    'Try
    '    '    Using objcb As New master_new.WDABasepgsql("", "")
    '    '        With objcb
    '    '            .SQL = "select coalesce(reqd_qty_processed,0) as reqd_qty_processed from reqd_det " + _
    '    '                   " where reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
    '    '            .InitializeCommand()
    '    '            .FillDataSet(ds_bantu, "reqd_det")
    '    '        End With
    '    '    End Using
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.Message)
    '    'End Try

    '    'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
    '    '    If ds_bantu.Tables(0).Rows(i).Item("reqd_qty_processed") > 0 Then
    '    '        MessageBox.Show("Can't Delete Processed WorkOrder...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '        Return False
    '    '    End If
    '    'Next
    'End Function
    '=====================================================================
    'Public Overrides Function delete_data() As Boolean
    '    delete_data = True
    '    If ds.Tables.Count = 0 Then
    '        delete_data = False
    '        Exit Function
    '    ElseIf ds.Tables(0).Rows.Count = 0 Then
    '        delete_data = False
    '        Exit Function
    '    End If

    '    If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
    '        Exit Function
    '    End If

    '    Dim i As Integer

    '    If before_delete() = True Then
    '        row = BindingContext(ds.Tables(0)).Position

    '        If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
    '            row = row - 1
    '        ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
    '            row = 0
    '        End If

    '        Try
    '            Using objinsert As New master_new.WDABasepgsql("", "")
    '                With objinsert
    '                    .Connection.Open()
    '                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
    '                    Try
    '                        .Command = .Connection.CreateCommand
    '                        .Command.Transaction = sqlTran

    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "delete from wo_mstr where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid") + "'"
    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                        '.Command.CommandType = CommandType.Text
    '                        '.Command.CommandText = "delete from wod_mstr where wod_wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid") + "'"
    '                        '.Command.ExecuteNonQuery()
    '                        '.Command.Parameters.Clear()

    '                        sqlTran.Commit()

    '                        help_load_data(True)
    '                        MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Catch ex As PgSqlException
    '                        sqlTran.Rollback()
    '                        MessageBox.Show(ex.Message)
    '                    End Try
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If

    '    Return delete_data
    'End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region

    Private Sub wo_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_code.ButtonClick

        Dim frm As New FWOSearch()
        frm.set_win(Me)
        frm._en_id = par_entity.EditValue
        frm.type_form = True
        frm.ShowDialog()

    End Sub

    
    Private Sub sb_generate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_generate.Click
        If wo_code.EditValue = "" Then
            MsgBox("WO is empty...", MsgBoxStyle.Exclamation, "Load Error..")
        Else
            show_header()
            show_detail()
            check_availability()
        End If
        
    End Sub

    Public Sub create_ds_wod()
        ds_wod_temp = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  wod_use_bom, " _
                    & "  wod_pt_bom_id, " _
                    & "  wod_comp, " _
                    & "  wod_comp as ptbomdesc, " _
                    & "  wod_op, " _
                    & "  wod_qty_per, " _
                    & "  wod_qty_req, " _
                    & "  wod_qty_alloc, " _
                    & "  wod_qty_picked, " _
                    & "  wod_qty_issued, " _
                    & "  wod_cost, " _
                    & "  wod_cost as total_cost " _
                    & " FROM  " _
                    & "  public.wod_det " _
                    & " where wod_use_bom = 'U'"

                    .InitializeCommand()
                    .FillDataSet(ds_wod_temp, "wod_temp")
                   
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub show_header()
        ds_show = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  wo_oid, " _
                    & "  wo_dom_id, " _
                    & "  wo_en_id, " _
                    & "  wo_si_id, " _
                    & "  wo_id, " _
                    & "  wo_code, " _
                    & "  wo_type, " _
                    & "  wo_pt_id, " _
                    & "  wo_qty_ord, " _
                    & "  wo_qty_comp, " _
                    & "  wo_qty_rjc, " _
                    & "  wo_ord_date, " _
                    & "  current_timestamp as wo_rel_date, " _
                    & "  wo_due_date, " _
                    & "  wo_yield_pct, " _
                    & "   " _
                    & "  wo_ro_id, " _
                    & "  wo_status, " _
                    & "  wo_remarks, " _
                    & "  si_id, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "   " _
                    & "   " _
                    & "  ro_id, " _
                    & "  ro_desc, " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  wo_dt " _
                    & "FROM  " _
                    & "  public.wo_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.wo_mstr.wo_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.wo_mstr.wo_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id) " _
                    & " where wo_code = '" & wo_code.EditValue.ToString & "'"

                    .InitializeCommand()
                    .FillDataSet(ds_show, "wo_mstr")

                    If ds_show.Tables("wo_mstr").Rows.Count = 0 Then
                        MsgBox("No Work Order Available To Release.., Please Check WO Number..", MsgBoxStyle.Exclamation, "Warning..!")
                    ElseIf ds_show.Tables("wo_mstr").Rows.Count > 1 Then
                        MsgBox("Work Orders Are More Than One.., Please Check WO Number..", MsgBoxStyle.Exclamation, "Warning..!")
                    Else


                        With ds_show.Tables("wo_mstr").Rows(0)
                            _wo_oid_show = .Item("wo_oid")
                            wo_en_id.EditValue = .Item("en_desc")
                            wo_si_id.EditValue = .Item("si_desc")
                            part_desc.EditValue = .Item("pt_desc1")
                            wo_due_date.EditValue = .Item("wo_due_date")
                            wo_ord_date.EditValue = .Item("wo_ord_date")
                            wo_qty_ord.Text = .Item("wo_qty_ord")
                            wo_yield_pct.EditValue = .Item("wo_yield_pct")
                            wo_ro_id.EditValue = .Item("ro_desc")
                            wo_remarks.EditValue = .Item("wo_remarks")
                        End With
                        wo_code.Focus()
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub show_detail()
        Dim sql As String = ""

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    'cek data dari detail product structure
                    Try
                        ds_edit = New DataSet
                        Try
                            Using objcb As New master_new.WDABasepgsql("", "")
                                With objcb

                                    'sql = "SELECT  " _
                                    '    & "  psd_det.psd_oid, " _
                                    '    & "  psd_det.psd_ps_oid, " _
                                    '    & "  psd_det.psd_use_bom, " _
                                    '    & "  psd_det.psd_desc as ptbomdesc, " _
                                    '    & "  psd_det.psd_pt_bom_id, " _
                                    '    & "  psd_det.psd_comp, " _
                                    '    & "  psd_det.psd_ref, " _
                                    '    & "  psd_det.psd_desc, " _
                                    '    & "  psd_det.psd_start_date, " _
                                    '    & "  psd_det.psd_end_date, " _
                                    '    & "  psd_det.psd_qty, " _
                                    '    & "  psd_det.psd_str_type, " _
                                    '    & "  psd_det.psd_scrp_pct, " _
                                    '    & "  psd_det.psd_lt_off, " _
                                    '    & "  psd_det.psd_op, " _
                                    '    & "  psd_det.psd_seq, " _
                                    '    & "  psd_det.psd_fcst_pct, " _
                                    '    & "  psd_det.psd_group, " _
                                    '    & "  psd_det.psd_process, " _
                                    '    & "   CASE WHEN psd_use_bom = 'Y' " _
                                    '    & "   THEN 0 " _
                                    '    & "   ELSE (SELECT pt_mstr.pt_cost from pt_mstr where pt_id=psd_pt_bom_id) " _
                                    '    & "   END AS ptbomcost, " _
                                    '    & "   CASE WHEN psd_use_bom = 'Y' " _
                                    '    & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=psd_pt_bom_id) " _
                                    '    & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=psd_pt_bom_id) " _
                                    '    & "   END AS ptbomdesc " _
                                    '    & "FROM " _
                                    '    & "  psd_det " _
                                    '    & "  INNER JOIN ps_mstr ON (psd_det.psd_ps_oid = ps_mstr.ps_oid) "

                                    'If wo_bom_id.Text = "-" Then
                                    sql = "SELECT psd_use_bom, psd_pt_bom_id, " _
                                        & "   CASE WHEN psd_use_bom = 'Y' " _
                                        & "   THEN (SELECT bom_mstr.bom_code from bom_mstr where bom_id=psd_pt_bom_id) " _
                                        & "   ELSE (SELECT pt_mstr.pt_code from pt_mstr where pt_id=psd_pt_bom_id) " _
                                        & "   END AS ptbomcode, " _
                                        & "   CASE WHEN psd_use_bom = 'Y' " _
                                        & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=psd_pt_bom_id) " _
                                        & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=psd_pt_bom_id) " _
                                        & "   END AS ptbomdesc, " _
                                        & "   CASE WHEN psd_use_bom = 'Y' " _
                                        & "   THEN ' ' " _
                                        & "   ELSE (SELECT pt_mstr.pt_desc2 from pt_mstr where pt_id=psd_pt_bom_id) " _
                                        & "   END AS ptbomdesc2, " _
                                        & "   CASE WHEN psd_use_bom = 'Y' " _
                                        & "   THEN ' ' " _
                                        & "   ELSE (SELECT code_mstr.code_name from code_mstr inner join pt_mstr on pt_um=code_id where pt_id=psd_pt_bom_id) " _
                                        & "   END AS unit_measure, " _
                                        & "   psd_comp, psd_op, sum(psd_qty) as psd_qty, sum(" + SetInteger(wo_qty_ord.EditValue) + " * psd_qty) as tot_qty, " _
                                        & "   CASE WHEN psd_use_bom = 'Y' " _
                                        & "   THEN 0 " _
                                        & "   ELSE (SELECT pt_mstr.pt_cost from pt_mstr where pt_id=psd_pt_bom_id) " _
                                        & "   END AS ptbomcost " _
                                        & " from ""public"".""GetAllProductStructureRaw3""(" _
                                        & ds_show.Tables("wo_mstr").Rows(0).Item("wo_pt_id") & ") " _
                                        & " group by psd_use_bom, psd_pt_bom_id,ptbomdesc,psd_comp, psd_op,ptbomcost"
                                    '    sql = sql & " where ps_mstr.ps_pt_bom_id = " & ds_show.Tables("wo_mstr").Rows(0).Item("wo_pt_id")

                                    ''ElseIf wo_bom_id.Text <> "-" Then
                                    'sql = "SELECT psd_use_bom, psd_pt_bom_id, " _
                                    '    & "   CASE WHEN psd_use_bom = 'Y' " _
                                    '    & "   THEN (SELECT bom_mstr.bom_code from bom_mstr where bom_id=psd_pt_bom_id) " _
                                    '    & "   ELSE (SELECT pt_mstr.pt_code from pt_mstr where pt_id=psd_pt_bom_id) " _
                                    '    & "   END AS ptbomcode, " _
                                    '    & "   CASE WHEN psd_use_bom = 'Y' " _
                                    '    & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=psd_pt_bom_id) " _
                                    '    & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=psd_pt_bom_id) " _
                                    '    & "   END AS ptbomdesc, " _
                                    '    & "   CASE WHEN psd_use_bom = 'Y' " _
                                    '    & "   THEN ' ' " _
                                    '    & "   ELSE (SELECT pt_mstr.pt_desc2 from pt_mstr where pt_id=psd_pt_bom_id) " _
                                    '    & "   END AS ptbomdesc2, " _
                                    '    & "   CASE WHEN psd_use_bom = 'Y' " _
                                    '    & "   THEN ' ' " _
                                    '    & "   ELSE (SELECT code_mstr.code_name from code_mstr inner join pt_mstr on pt_um=code_id where pt_id=psd_pt_bom_id) " _
                                    '    & "   END AS unit_measure, " _
                                    '    & "   psd_comp, psd_op, sum(psd_qty) as psd_qty, sum(" + SetInteger(wo_qty_ord.EditValue) + " * psd_qty) as tot_qty, " _
                                    '    & "   CASE WHEN psd_use_bom = 'Y' " _
                                    '    & "   THEN 0 " _
                                    '    & "   ELSE (SELECT pt_mstr.pt_cost from pt_mstr where pt_id=psd_pt_bom_id) " _
                                    '    & "   END AS ptbomcost " _
                                    '    & " from ""public"".""GetAllProductStructureRaw3""(" _
                                    '    & ds_show.Tables("wo_mstr").Rows(0).Item("wo_bom_id") & ") " _
                                    '    & " group by psd_use_bom, psd_pt_bom_id,ptbomdesc,psd_comp, psd_op,ptbomcost"
                                    ''    sql = sql & " where ps_mstr.ps_pt_bom_id = " & ds_show.Tables("wo_mstr").Rows(0).Item("wo_bom_id")
                                    '' End If

                                    .SQL = sql
                                    .InitializeCommand()
                                    .FillDataSet(ds_edit, "psd_det")
                                    gc_edit.DataSource = ds_edit.Tables("psd_det")
                                    gv_edit.BestFitColumns()
                                End With
                            End Using
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                        ''buat dataset sementara
                        'create_ds_wod()

                        ''looping data yang ada
                        'For i = 0 To ds_edit.Tables("psd_det").Rows.Count - 1
                        '    'If SetSetringDB(ds_edit.Tables("psd_det").Rows(i).Item("psd_use_bom")) = "Y" Then

                        '    dt_wod_temp = ds_wod_temp.Tables("wod_temp")
                        '    dr = dt_wod_temp.NewRow()
                        '    dr("wod_use_bom") = ds_edit.Tables("psd_det").Rows(i).Item("psd_use_bom")
                        '    dr("wod_pt_bom_id") = SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_pt_bom_id"))
                        '    dr("ptbomdesc") = ds_edit.Tables("psd_det").Rows(i).Item("ptbomdesc")
                        '    dr("wod_comp") = ds_edit.Tables("psd_det").Rows(i).Item("psd_comp")
                        '    dr("wod_op") = SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_op"))
                        '    dr("wod_qty_per") = SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_qty"))
                        '    dr("wod_qty_req") = (SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue))
                        '    dr("wod_qty_alloc") = 0
                        '    dr("wod_qty_picked") = 0
                        '    dr("wod_qty_issued") = 0
                        '    dr("wod_cost") = SetIntegerDB(ds_edit.Tables("psd_det").Rows(i).Item("ptbomcost"))
                        '    dr("total_cost") = SetIntegerDB(ds_edit.Tables("psd_det").Rows(i).Item("ptbomcost")) * (SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue))
                        '    dt_wod_temp.Rows.Add(dr)


                        '    'End If
                        'Next
                        'gc_edit.DataSource = ds_wod_temp.Tables("wod_temp")
                        'gv_edit.BestFitColumns()
                    Catch ex As PgSqlException
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
                        
    End Sub

    Sub check_availability()
        Dim x, y, total, req, qty_wh As Integer
        Dim pt_id As New DataColumn
        Dim pt_desc As New DataColumn
        Dim pt_req As New DataColumn
        Dim pt_avail As New DataColumn
        Dim pt_ware_id As New DataColumn
        Dim pt_ware_desc As New DataColumn
        Dim pt_status As New DataColumn
        Dim sql As String
        ds_warehouse = New DataSet
        dt_warehouse = New DataTable

        pt_id = New DataColumn("pt_id", Type.GetType("System.Int32"))
        pt_desc = New DataColumn("pt_desc", Type.GetType("System.String"))
        pt_req = New DataColumn("pt_req", Type.GetType("System.Int32"))
        pt_avail = New DataColumn("pt_avail", Type.GetType("System.Int32"))
        pt_ware_id = New DataColumn("pt_ware_id", Type.GetType("System.Int32"))
        pt_ware_desc = New DataColumn("pt_ware_desc", Type.GetType("System.String"))
        pt_status = New DataColumn("pt_status", Type.GetType("System.String"))

        dt_warehouse.Columns.Add(pt_id)
        dt_warehouse.Columns.Add(pt_desc)
        dt_warehouse.Columns.Add(pt_req)
        dt_warehouse.Columns.Add(pt_avail)
        dt_warehouse.Columns.Add(pt_ware_id)
        dt_warehouse.Columns.Add(pt_ware_desc)
        dt_warehouse.Columns.Add(pt_status)

        ds_avail = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT loc_oid,  loc_dom_id, loc_en_id,loc_add_by, loc_add_date, " _
                        & "  loc_upd_by, loc_upd_date, loc_id, loc_wh_id, loc_si_id, loc_code, loc_desc, " _
                        & "  loc_type, loc_cat, loc_is_id, loc_active, loc_dt " _
                        & "FROM  " _
                        & "  public.loc_mstr  " _
                        & "  inner join en_mstr on loc_en_id = en_id " _
                        & "  inner join wh_mstr on loc_wh_id = wh_id " _
                        & "  inner join si_mstr on loc_si_id = si_id " _
                        & "  inner join is_mstr on loc_is_id = is_id  " _
                        & " where loc_en_id  in (select user_en_id from tconfuserentity " _
                                        & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        & " AND is_avail = 'Y'" _
                        & " order by loc_desc "

                    .InitializeCommand()
                    .FillDataSet(ds_avail, "loc_mstr")

                End With
            End Using

            For x = 0 To ds_edit.Tables("psd_det").Rows.Count - 1

                total = SetIntegerDB(ds_edit.Tables("psd_det").Rows(x).Item("tot_qty"))
                req = total
                For y = 0 To ds_avail.Tables("loc_mstr").Rows.Count - 1
                    req = total
                    qty_wh = 0

                    Try
                        ds_avail.Tables("invc_mstr").Clear()
                    Catch ex As Exception
                    End Try

                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            sql = "SELECT  " _
                                    & "  invc_pt_id, " _
                                    & "  sum(invc_qty) as invc_qty " _
                                    & " FROM  " _
                                    & "  public.invc_mstr " _
                                    & "  where invc_loc_id = " + SetIntegerDB(ds_avail.Tables("loc_mstr").Rows(y).Item("loc_id")) _
                                    & "  and  invc_pt_id = " + SetIntegerDB(ds_edit.Tables("psd_det").Rows(x).Item("psd_pt_bom_id")) _
                                    & " and invc_en_id  in (select user_en_id from tconfuserentity " _
                                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                    & " group by invc_pt_id "

                            .SQL = sql
                            .InitializeCommand()
                            .FillDataSet(ds_avail, "invc_mstr")
                        End With
                    End Using

                    ' MsgBox(ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty"))
                    If ds_avail.Tables("invc_mstr").Rows.Count > 0 Then
                        If ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty") > 0 Then
                            If ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty") > total Then
                                qty_wh = total
                                total = 0
                            ElseIf ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty") <= total Then
                                qty_wh = ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty")
                                total = total - ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty")
                            End If

                        End If

                        dr = dt_warehouse.NewRow()
                        dr("pt_id") = ds_edit.Tables("psd_det").Rows(x).Item("psd_pt_bom_id")
                        dr("pt_desc") = ds_edit.Tables("psd_det").Rows(x).Item("ptbomdesc")
                        dr("pt_req") = req
                        dr("pt_avail") = qty_wh
                        dr("pt_ware_id") = ds_avail.Tables("loc_mstr").Rows(y).Item("loc_id")
                        dr("pt_ware_desc") = ds_avail.Tables("loc_mstr").Rows(y).Item("loc_desc")
                        dr("pt_status") = "Available"
                        dt_warehouse.Rows.Add(dr)
                    End If
                    'input ke dataset
                    'jika masih ada sisa

                    If total = 0 Then
                        y = ds_avail.Tables("loc_mstr").Rows.Count - 1
                    End If
                Next

                If total <> 0 Then
                    req = total
                    dr = dt_warehouse.NewRow()
                    dr("pt_id") = ds_edit.Tables("psd_det").Rows(x).Item("psd_pt_bom_id")
                    dr("pt_desc") = ds_edit.Tables("psd_det").Rows(x).Item("ptbomdesc")
                    dr("pt_req") = req
                    dr("pt_avail") = 0
                    dr("pt_ware_id") = 0
                    dr("pt_ware_desc") = "-"
                    dr("pt_status") = "Not Available"
                    dt_warehouse.Rows.Add(dr)
                End If
            Next
            ds_warehouse.Tables.Add(dt_warehouse)
            gc_available.DataSource = ds_warehouse.Tables(0)
            gv_available.BestFitColumns()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Overrides Function insert() As Boolean
        insert = True
        Dim i As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.wo_mstr   " _
                                            & "SET  " _
                                            & "  wo_status = 'R' ," _
                                            & "  wo_rel_date = current_timestamp " _
                                            & " WHERE  " _
                                            & "  wo_code = '" & wo_code.EditValue.ToString & "'  " _
                                            & ";"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("psd_det").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wod_det " _
                                                & "( " _
                                                & "  wod_oid, " _
                                                & "  wod_wo_oid, " _
                                                & "  wod_use_bom, " _
                                                & "  wod_pt_bom_id, " _
                                                & "  wod_comp, " _
                                                & "  wod_op, " _
                                                & "  wod_qty_per, " _
                                                & "  wod_qty_req, " _
                                                & "  wod_qty_alloc, " _
                                                & "  wod_qty_picked, " _
                                                & "  wod_qty_issued, " _
                                                & "  wod_cost, " _
                                                & "  wod_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_wo_oid_show.ToString) & ",  " _
                                                & SetSetringDB(ds_edit.Tables("psd_det").Rows(i).Item("psd_use_bom")) & ",  " _
                                                & SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_pt_bom_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables("psd_det").Rows(i).Item("psd_comp")) & ",  " _
                                                & SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_op")) & ",  " _
                                                & SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("tot_qty")) & ",  " _
                                                & " 0,  " _
                                                & " 0,  " _
                                                & " 0,  " _
                                                & SetDbl(ds_edit.Tables("psd_det").Rows(i).Item("ptbomcost")) & ",  " _
                                                & " current_timestamp " _
                                                & ");"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        print_data()
                        after_success()
                        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                        ds_warehouse.Clear()
                        ds_edit.Tables("psd_det").Clear()
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using

        Catch ex As Exception
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Sub print_data()
        Dim x, y, z As Integer
        ds_print = New DataSet
        dt_print = New DataTable
        'dr_print = New DataRow

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "   wo_mstr.wo_oid, " _
                        & "   wo_mstr.wo_en_id, " _
                        & "   wo_mstr.wo_si_id, " _
                        & "   wo_mstr.wo_id, " _
                        & "   wo_mstr.wo_code, " _
                        & "   wo_mstr.wo_type, " _
                        & "   wo_mstr.wo_pt_id, " _
                        & "   wo_mstr.wo_qty_ord, " _
                        & "   wo_mstr.wo_qty_comp, " _
                        & "   wo_mstr.wo_qty_rjc, " _
                        & "   wo_mstr.wo_ord_date, " _
                        & "   wo_rel_date, " _
                        & "   wo_mstr.wo_due_date, " _
                        & "   wo_mstr.wo_yield_pct, " _
                        & "   wo_mstr.wo_bom_id, " _
                        & "   wo_mstr.wo_ro_id, " _
                        & "   wo_mstr.wo_status, " _
                        & "   wo_mstr.wo_remarks, " _
                        & "   wo_mstr.wo_dt, " _
                        & "   si_mstr.si_id, " _
                        & "   si_mstr.si_desc, " _
                        & "   wod_det.wod_oid, " _
                        & "   wod_det.wod_wo_oid, " _
                        & "   wod_det.wod_use_bom, " _
                        & "   wod_det.wod_pt_bom_id, " _
                        & "   wod_det.wod_comp, " _
                        & "   wod_det.wod_op, " _
                        & "   wod_det.wod_qty_per, " _
                        & "   wod_det.wod_qty_req, " _
                        & "   wod_det.wod_qty_alloc, " _
                        & "   wod_det.wod_qty_picked, " _
                        & "   wod_det.wod_qty_issued, " _
                        & "   wod_det.wod_cost, " _
                        & "   wod_det.wod_dt, " _
                        & "   pt_mstr.pt_id, " _
                        & "   pt_mstr.pt_code, " _
                        & "   pt_mstr.pt_desc1, " _
                        & "   pt_mstr.pt_desc2, " _
                        & "   pt_mstr.pt_um, " _
                        & "   bom_mstr.bom_id, " _
                        & "   bom_mstr.bom_desc, " _
                        & "   ro_mstr.ro_id, " _
                        & "   ro_mstr.ro_desc, " _
                        & " (SELECT code_mstr.code_name FROM code_mstr WHERE code_mstr.code_id = pt_mstr.pt_um) AS unit_measure, " _
                        & " CASE  " _
                        & "    WHEN wod_use_bom = 'Y' " _
                        & "      THEN (SELECT bom_mstr.bom_code from bom_mstr where bom_id = wod_pt_bom_id) " _
                        & "    ELSE  (SELECT pt_mstr.pt_code from pt_mstr where pt_id = wod_pt_bom_id) " _
                        & "  END AS ptbomcode, " _
                        & "  CASE  " _
                        & "    WHEN wod_use_bom = 'Y' " _
                        & "      THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id = wod_pt_bom_id) " _
                        & "    ELSE  (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id = wod_pt_bom_id) " _
                        & "  END AS ptbomdesc, " _
                        & "  CASE  " _
                        & "    WHEN wod_use_bom = 'Y' " _
                        & "      THEN ' ' " _
                        & "    ELSE  (SELECT pt_mstr.pt_desc2 from pt_mstr where pt_id = wod_pt_bom_id) " _
                        & "  END AS ptbomdesc2, " _
                        & "   CASE WHEN wod_use_bom = 'Y' " _
                        & "   THEN ' ' " _
                        & "   ELSE (SELECT code_mstr.code_name from code_mstr inner join pt_mstr on pt_um=code_id where pt_id = wod_pt_bom_id) " _
                        & "   END AS unit_measure, " _
                        & "   pt_mstr.pt_desc1 AS avail_desc, " _
                        & "   wod_det.wod_qty_req AS avail_qty " _
                        & "FROM " _
                        & "   wo_mstr " _
                        & "  INNER JOIN  wod_det ON ( wo_mstr.wo_oid =  wod_det.wod_wo_oid) " _
                        & "  INNER JOIN  bom_mstr ON ( wo_mstr.wo_bom_id =  bom_mstr.bom_id) " _
                        & "  INNER JOIN  ro_mstr ON ( wo_mstr.wo_ro_id =  ro_mstr.ro_id) " _
                        & "  INNER JOIN  si_mstr ON ( wo_mstr.wo_si_id =  si_mstr.si_id) " _
                        & "  INNER JOIN  pt_mstr ON ( wo_mstr.wo_pt_id =  pt_mstr.pt_id)   " _
                        & "WHERE wo_mstr.wo_code='#%&a!'"

                    .InitializeCommand()
                    .FillDataSet(ds_print, "print_wo")

                    dt_print = ds_print.Tables("print_wo")
                    ds_print.Tables(0).Rows.Clear()
                    dr_print = dt_print.NewRow()


                    'dr("wod_use_bom") = ds_edit.Tables("psd_det").Rows(i).Item("psd_use_bom")
                    'dr("wod_pt_bom_id") = SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_pt_bom_id"))
                    'dr("ptbomdesc") = ds_edit.Tables("psd_det").Rows(i).Item("ptbomdesc")
                    'dr("wod_comp") = ds_edit.Tables("psd_det").Rows(i).Item("psd_comp")
                    'dr("wod_op") = SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_op"))
                    'dr("wod_qty_per") = SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_qty"))
                    'dr("wod_qty_req") = (SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue))
                    'dr("wod_qty_alloc") = 0
                    'dr("wod_qty_picked") = 0
                    'dr("wod_qty_issued") = 0
                    'dr("wod_cost") = SetIntegerDB(ds_edit.Tables("psd_det").Rows(i).Item("ptbomcost"))
                    'dr("total_cost") = SetIntegerDB(ds_edit.Tables("psd_det").Rows(i).Item("ptbomcost")) * (SetInteger(ds_edit.Tables("psd_det").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue))
                    'dt_wod_temp.Rows.Add(dr)

                    Dim cek_repeat_head As Integer
                    Dim cek_repeat_det As Integer

                    For x = 0 To ds_show.Tables("wo_mstr").Rows.Count - 1
                        cek_repeat_head = 0
                        With ds_show.Tables("wo_mstr").Rows(x)
                            dr_print("wo_oid") = .Item("wo_oid")
                            dr_print("wo_en_id") = .Item("en_id")
                            dr_print("wo_si_id") = .Item("wo_si_id")
                            dr_print("wo_id") = .Item("wo_id")
                            dr_print("wo_code") = .Item("wo_code")
                            dr_print("wo_type") = .Item("wo_type")
                            dr_print("wo_pt_id") = .Item("wo_pt_id")
                            dr_print("wo_qty_ord") = .Item("wo_qty_ord")
                            dr_print("wo_qty_comp") = .Item("wo_qty_comp")
                            dr_print("wo_qty_rjc") = .Item("wo_qty_rjc")
                            dr_print("wo_ord_date") = .Item("wo_ord_date")
                            dr_print("wo_rel_date") = .Item("wo_rel_date")
                            dr_print("wo_due_date") = .Item("wo_due_date")
                            dr_print("wo_yield_pct") = .Item("wo_yield_pct")
                            dr_print("wo_bom_id") = .Item("wo_bom_id")
                            dr_print("wo_ro_id") = .Item("wo_ro_id")
                            dr_print("wo_status") = .Item("wo_status")
                            dr_print("wo_remarks") = .Item("wo_remarks")
                            dr_print("wo_dt") = .Item("wo_dt")
                            dr_print("si_id") = .Item("si_id")
                            dr_print("si_desc") = .Item("si_desc")
                            dr_print("pt_id") = .Item("pt_id")
                            dr_print("pt_code") = .Item("pt_code")
                            dr_print("pt_desc1") = .Item("pt_desc1")
                            dr_print("pt_desc2") = .Item("pt_desc2")
                            'dr_print("pt_um") = .Item("pt_um")
                            dr_print("bom_id") = .Item("bom_id")
                            dr_print("bom_desc") = .Item("bom_desc")
                            dr_print("ro_id") = .Item("ro_id")
                            dr_print("ro_desc") = .Item("ro_desc")
                        End With

                        For y = 0 To ds_edit.Tables("psd_det").Rows.Count - 1
                            cek_repeat_det = 0

                            If cek_repeat_head > 0 Then
                                With ds_show.Tables("wo_mstr").Rows(x)
                                    dr_print("wo_oid") = .Item("wo_oid")
                                    dr_print("wo_en_id") = .Item("en_id")
                                    dr_print("wo_si_id") = .Item("wo_si_id")
                                    dr_print("wo_id") = .Item("wo_id")
                                    dr_print("wo_code") = .Item("wo_code")
                                    dr_print("wo_type") = .Item("wo_type")
                                    dr_print("wo_pt_id") = .Item("wo_pt_id")
                                    dr_print("wo_qty_ord") = .Item("wo_qty_ord")
                                    dr_print("wo_qty_comp") = .Item("wo_qty_comp")
                                    dr_print("wo_qty_rjc") = .Item("wo_qty_rjc")
                                    dr_print("wo_ord_date") = .Item("wo_ord_date")
                                    dr_print("wo_rel_date") = .Item("wo_rel_date")
                                    dr_print("wo_due_date") = .Item("wo_due_date")
                                    dr_print("wo_yield_pct") = .Item("wo_yield_pct")
                                    dr_print("wo_bom_id") = .Item("wo_bom_id")
                                    dr_print("wo_ro_id") = .Item("wo_ro_id")
                                    dr_print("wo_status") = .Item("wo_status")
                                    dr_print("wo_remarks") = .Item("wo_remarks")
                                    dr_print("wo_dt") = .Item("wo_dt")
                                    dr_print("si_id") = .Item("si_id")
                                    dr_print("si_desc") = .Item("si_desc")
                                    dr_print("pt_id") = .Item("pt_id")
                                    dr_print("pt_code") = .Item("pt_code")
                                    dr_print("pt_desc1") = .Item("pt_desc1")
                                    dr_print("pt_desc2") = .Item("pt_desc2")
                                    'dr_print("pt_um") = .Item("pt_um")
                                    dr_print("bom_id") = .Item("bom_id")
                                    dr_print("bom_desc") = .Item("bom_desc")
                                    dr_print("ro_id") = .Item("ro_id")
                                    dr_print("ro_desc") = .Item("ro_desc")
                                End With
                            End If

                            With ds_edit.Tables("psd_det").Rows(y)
                                dr_print("ptbomcode") = .Item("ptbomcode")
                                dr_print("ptbomdesc") = .Item("ptbomdesc")
                                dr_print("ptbomdesc2") = .Item("ptbomdesc2")
                                dr_print("wod_oid") = Guid.NewGuid.ToString
                                dr_print("wod_wo_oid") = ds_show.Tables("wo_mstr").Rows(x).Item("wo_oid")
                                dr_print("wod_use_bom") = .Item("psd_use_bom")
                                dr_print("wod_pt_bom_id") = .Item("psd_pt_bom_id")
                                dr_print("wod_comp") = .Item("psd_comp")
                                dr_print("wod_op") = .Item("psd_op")
                                dr_print("wod_qty_per") = .Item("psd_qty")
                                dr_print("wod_qty_req") = .Item("tot_qty")
                                dr_print("wod_qty_issued") = 0
                                dr_print("unit_measure") = .Item("unit_measure")
                                'dr_print("wod_cost") = .Item("ptbomcost")
                                'dr_print("wod_dt") = .Item("wod_dt")
                            End With

                            For z = 0 To ds_warehouse.Tables(0).Rows.Count - 1

                                If ds_warehouse.Tables(0).Rows(z).Item("pt_id") = ds_edit.Tables("psd_det").Rows(y).Item("psd_pt_bom_id") Then

                                    If cek_repeat_det > 0 Then
                                        With ds_show.Tables("wo_mstr").Rows(x)
                                            dr_print("wo_oid") = .Item("wo_oid")
                                            dr_print("wo_en_id") = .Item("en_id")
                                            dr_print("wo_si_id") = .Item("wo_si_id")
                                            dr_print("wo_id") = .Item("wo_id")
                                            dr_print("wo_code") = .Item("wo_code")
                                            dr_print("wo_type") = .Item("wo_type")
                                            dr_print("wo_pt_id") = .Item("wo_pt_id")
                                            dr_print("wo_qty_ord") = .Item("wo_qty_ord")
                                            dr_print("wo_qty_comp") = .Item("wo_qty_comp")
                                            dr_print("wo_qty_rjc") = .Item("wo_qty_rjc")
                                            dr_print("wo_ord_date") = .Item("wo_ord_date")
                                            dr_print("wo_rel_date") = .Item("wo_rel_date")
                                            dr_print("wo_due_date") = .Item("wo_due_date")
                                            dr_print("wo_yield_pct") = .Item("wo_yield_pct")
                                            dr_print("wo_bom_id") = .Item("wo_bom_id")
                                            dr_print("wo_ro_id") = .Item("wo_ro_id")
                                            dr_print("wo_status") = .Item("wo_status")
                                            dr_print("wo_remarks") = .Item("wo_remarks")
                                            dr_print("wo_dt") = .Item("wo_dt")
                                            dr_print("si_id") = .Item("si_id")
                                            dr_print("si_desc") = .Item("si_desc")
                                            dr_print("pt_id") = .Item("pt_id")
                                            dr_print("pt_code") = .Item("pt_code")
                                            dr_print("pt_desc1") = .Item("pt_desc1")
                                            dr_print("pt_desc2") = .Item("pt_desc2")
                                            'dr_print("pt_um") = .Item("pt_um")
                                            dr_print("bom_id") = .Item("bom_id")
                                            dr_print("bom_desc") = .Item("bom_desc")
                                            dr_print("ro_id") = .Item("ro_id")
                                            dr_print("ro_desc") = .Item("ro_desc")
                                        End With

                                        With ds_edit.Tables("psd_det").Rows(y)
                                            dr_print("ptbomcode") = .Item("ptbomcode")
                                            dr_print("ptbomdesc") = .Item("ptbomdesc")
                                            dr_print("ptbomdesc2") = .Item("ptbomdesc2")
                                            dr_print("wod_oid") = Guid.NewGuid.ToString
                                            dr_print("wod_wo_oid") = ds_show.Tables("wo_mstr").Rows(x).Item("wo_oid")
                                            dr_print("wod_use_bom") = .Item("psd_use_bom")
                                            dr_print("wod_pt_bom_id") = .Item("psd_pt_bom_id")
                                            dr_print("wod_comp") = .Item("psd_comp")
                                            dr_print("wod_op") = .Item("psd_op")
                                            dr_print("wod_qty_per") = .Item("psd_qty")
                                            dr_print("wod_qty_req") = .Item("tot_qty")
                                            dr_print("wod_qty_issued") = 0
                                            dr_print("unit_measure") = .Item("unit_measure")
                                            'dr_print("wod_cost") = .Item("ptbomcost")
                                            'dr_print("wod_dt") = .Item("wod_dt")
                                        End With
                                    End If

                                    If ds_warehouse.Tables(0).Rows(z).Item("pt_ware_desc") = "-" Then
                                        dr_print("avail_desc") = ds_warehouse.Tables(0).Rows(z).Item("pt_status")
                                        dr_print("avail_qty") = ds_warehouse.Tables(0).Rows(z).Item("pt_req")
                                    Else
                                        dr_print("avail_desc") = ds_warehouse.Tables(0).Rows(z).Item("pt_ware_desc")
                                        dr_print("avail_qty") = ds_warehouse.Tables(0).Rows(z).Item("pt_avail")
                                    End If

                                    dt_print.Rows.Add(dr_print)
                                    dr_print = dt_print.NewRow()
                                    cek_repeat_det = cek_repeat_det + 1
                                    'ds_print.Tables(0).Rows.Add(dr_print)

                                End If
                            Next
                            cek_repeat_head = cek_repeat_head + 1
                        Next
                    Next

                    'ds_print.Tables.Add(dt_print)
                End With
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim rpt As New XRWorkOrder
        Try
            With rpt
                '    Try
                '        Using objcb As New master_new.WDABasepgsql("", "")
                '            With objcb
                '                .SQL = _sql
                '                .InitializeCommand()
                '                .FillDataSet(ds_bantu, "data")
                '            End With
                '        End Using
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message)
                '        Exit Sub
                '    End Try

                If ds_print.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_print
                .DataMember = "print_wo"
                .ShowPreview()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Sub check_availability_print()
        Dim x, y, total, req, qty_wh As Integer
        Dim pt_id As New DataColumn
        Dim pt_desc As New DataColumn
        Dim pt_req As New DataColumn
        Dim pt_avail As New DataColumn
        Dim pt_ware_id As New DataColumn
        Dim pt_ware_desc As New DataColumn
        Dim pt_status As New DataColumn
        Dim sql As String
        ds_print = New DataSet
        dt_print = New DataTable
        'dr_print = New DataRow

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "   wo_mstr.wo_oid, " _
                        & "   wo_mstr.wo_en_id, " _
                        & "   wo_mstr.wo_si_id, " _
                        & "   wo_mstr.wo_id, " _
                        & "   wo_mstr.wo_code, " _
                        & "   wo_mstr.wo_type, " _
                        & "   wo_mstr.wo_pt_id, " _
                        & "   wo_mstr.wo_qty_ord, " _
                        & "   wo_mstr.wo_qty_comp, " _
                        & "   wo_mstr.wo_qty_rjc, " _
                        & "   wo_mstr.wo_ord_date, " _
                        & "   wo_rel_date, " _
                        & "   wo_mstr.wo_due_date, " _
                        & "   wo_mstr.wo_yield_pct, " _
                        & "   wo_mstr.wo_bom_id, " _
                        & "   wo_mstr.wo_ro_id, " _
                        & "   wo_mstr.wo_status, " _
                        & "   wo_mstr.wo_remarks, " _
                        & "   wo_mstr.wo_dt, " _
                        & "   si_mstr.si_id, " _
                        & "   si_mstr.si_desc, " _
                        & "   wod_det.wod_oid, " _
                        & "   wod_det.wod_wo_oid, " _
                        & "   wod_det.wod_use_bom, " _
                        & "   wod_det.wod_pt_bom_id, " _
                        & "   wod_det.wod_comp, " _
                        & "   wod_det.wod_op, " _
                        & "   wod_det.wod_qty_per, " _
                        & "   wod_det.wod_qty_req, " _
                        & "   wod_det.wod_qty_alloc, " _
                        & "   wod_det.wod_qty_picked, " _
                        & "   wod_det.wod_qty_issued, " _
                        & "   wod_det.wod_cost, " _
                        & "   wod_det.wod_dt, " _
                        & "   pt_mstr.pt_id, " _
                        & "   pt_mstr.pt_code, " _
                        & "   pt_mstr.pt_desc1, " _
                        & "   pt_mstr.pt_desc2, " _
                        & "   pt_mstr.pt_um, " _
                        & "   bom_mstr.bom_id, " _
                        & "   bom_mstr.bom_desc, " _
                        & "   ro_mstr.ro_id, " _
                        & "   ro_mstr.ro_desc, " _
                        & " (SELECT code_mstr.code_name FROM code_mstr WHERE code_mstr.code_id = pt_mstr.pt_um) AS unit_measure, " _
                        & " CASE  " _
                        & "    WHEN wod_use_bom = 'Y' " _
                        & "      THEN (SELECT bom_mstr.bom_code from bom_mstr where bom_id = wod_pt_bom_id) " _
                        & "    ELSE  (SELECT pt_mstr.pt_code from pt_mstr where pt_id = wod_pt_bom_id) " _
                        & "  END AS ptbomcode, " _
                        & "  CASE  " _
                        & "    WHEN wod_use_bom = 'Y' " _
                        & "      THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id = wod_pt_bom_id) " _
                        & "    ELSE  (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id = wod_pt_bom_id) " _
                        & "  END AS ptbomdesc, " _
                        & "  CASE  " _
                        & "    WHEN wod_use_bom = 'Y' " _
                        & "      THEN ' ' " _
                        & "    ELSE  (SELECT pt_mstr.pt_desc2 from pt_mstr where pt_id = wod_pt_bom_id) " _
                        & "  END AS ptbomdesc2, " _
                        & "   CASE WHEN wod_use_bom = 'Y' " _
                        & "   THEN ' ' " _
                        & "   ELSE (SELECT code_mstr.code_name from code_mstr inner join pt_mstr on pt_um=code_id where pt_id = wod_pt_bom_id) " _
                        & "   END AS unit_measure, " _
                        & "   pt_mstr.pt_desc1 AS avail_desc, " _
                        & "   wod_det.wod_qty_req AS avail_qty " _
                        & "FROM " _
                        & "   wo_mstr " _
                        & "  INNER JOIN  wod_det ON ( wo_mstr.wo_oid =  wod_det.wod_wo_oid) " _
                        & "  INNER JOIN  bom_mstr ON ( wo_mstr.wo_bom_id =  bom_mstr.bom_id) " _
                        & "  INNER JOIN  ro_mstr ON ( wo_mstr.wo_ro_id =  ro_mstr.ro_id) " _
                        & "  INNER JOIN  si_mstr ON ( wo_mstr.wo_si_id =  si_mstr.si_id) " _
                        & "  INNER JOIN  pt_mstr ON ( wo_mstr.wo_pt_id =  pt_mstr.pt_id)   " _
                        & "WHERE wo_mstr.wo_code='#%&a!'"

                    .InitializeCommand()
                    .FillDataSet(ds_print, "print_wo")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        dt_print = ds_print.Tables("print_wo")
        ds_print.Tables(0).Rows.Clear()
        dr_print = dt_print.NewRow()


        ds_avail = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT loc_oid,  loc_dom_id, loc_en_id,loc_add_by, loc_add_date, " _
                        & "  loc_upd_by, loc_upd_date, loc_id, loc_wh_id, loc_si_id, loc_code, loc_desc, " _
                        & "  loc_type, loc_cat, loc_is_id, loc_active, loc_dt " _
                        & "FROM  " _
                        & "  public.loc_mstr  " _
                        & "  inner join en_mstr on loc_en_id = en_id " _
                        & "  inner join wh_mstr on loc_wh_id = wh_id " _
                        & "  inner join si_mstr on loc_si_id = si_id " _
                        & "  inner join is_mstr on loc_is_id = is_id  " _
                        & " where loc_en_id  in (select user_en_id from tconfuserentity " _
                                        & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        & " AND is_avail = 'Y'" _
                        & " order by loc_desc "

                    .InitializeCommand()
                    .FillDataSet(ds_avail, "loc_mstr")

                End With
            End Using

            For x = 0 To ds_printing.Tables("printing_wo").Rows.Count - 1

                total = SetIntegerDB(ds_printing.Tables("printing_wo").Rows(x).Item("wod_qty_req")) - SetIntegerDB(ds_printing.Tables("printing_wo").Rows(x).Item("wod_qty_issued"))
                req = total
                For y = 0 To ds_avail.Tables("loc_mstr").Rows.Count - 1
                    req = total
                    qty_wh = 0

                    Try
                        ds_avail.Tables("invc_mstr").Clear()
                    Catch ex As Exception
                    End Try

                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            sql = "SELECT  " _
                                    & "  invc_pt_id, " _
                                    & "  sum(invc_qty) as invc_qty " _
                                    & " FROM  " _
                                    & "  public.invc_mstr " _
                                    & "  where invc_loc_id = " + SetIntegerDB(ds_avail.Tables("loc_mstr").Rows(y).Item("loc_id")) _
                                    & "  and  invc_pt_id = " + SetIntegerDB(ds_printing.Tables("printing_wo").Rows(x).Item("wod_pt_bom_id")) _
                                    & " and invc_en_id  in (select user_en_id from tconfuserentity " _
                                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                    & " group by invc_pt_id "

                            .SQL = sql
                            .InitializeCommand()
                            .FillDataSet(ds_avail, "invc_mstr")
                        End With
                    End Using

                    If ds_avail.Tables("invc_mstr").Rows.Count > 0 Then
                        If ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty") > 0 Then
                            If ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty") > total Then
                                qty_wh = total
                                total = 0
                            ElseIf ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty") <= total Then
                                qty_wh = ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty")
                                total = total - ds_avail.Tables("invc_mstr").Rows(0).Item("invc_qty")
                            End If

                        End If

                        dr_print = dt_print.NewRow()
                        With ds_printing.Tables("printing_wo").Rows(x)
                            dr_print("wo_oid") = .Item("wo_oid")
                            dr_print("wo_en_id") = .Item("wo_en_id")
                            dr_print("wo_si_id") = .Item("wo_si_id")
                            dr_print("wo_id") = .Item("wo_id")
                            dr_print("wo_code") = .Item("wo_code")
                            dr_print("wo_type") = .Item("wo_type")
                            dr_print("wo_pt_id") = .Item("wo_pt_id")
                            dr_print("wo_qty_ord") = .Item("wo_qty_ord")
                            dr_print("wo_qty_comp") = .Item("wo_qty_comp")
                            dr_print("wo_qty_rjc") = .Item("wo_qty_rjc")
                            dr_print("wo_ord_date") = .Item("wo_ord_date")
                            dr_print("wo_rel_date") = .Item("wo_rel_date")
                            dr_print("wo_due_date") = .Item("wo_due_date")
                            dr_print("wo_yield_pct") = .Item("wo_yield_pct")
                            dr_print("wo_bom_id") = .Item("wo_bom_id")
                            dr_print("wo_ro_id") = .Item("wo_ro_id")
                            dr_print("wo_status") = .Item("wo_status")
                            dr_print("wo_remarks") = .Item("wo_remarks")
                            dr_print("wo_dt") = .Item("wo_dt")
                            dr_print("si_id") = .Item("si_id")
                            dr_print("si_desc") = .Item("si_desc")
                            dr_print("pt_id") = .Item("pt_id")
                            dr_print("pt_code") = .Item("pt_code")
                            dr_print("pt_desc1") = .Item("pt_desc1")
                            dr_print("pt_desc2") = .Item("pt_desc2")
                            'dr_print("pt_um") = .Item("pt_um")
                            dr_print("bom_id") = .Item("bom_id")
                            dr_print("bom_desc") = .Item("bom_desc")
                            dr_print("ro_id") = .Item("ro_id")
                            dr_print("ro_desc") = .Item("ro_desc")
                            dr_print("ptbomcode") = .Item("ptbomcode")
                            dr_print("ptbomdesc") = .Item("ptbomdesc")
                            dr_print("ptbomdesc2") = .Item("ptbomdesc2")
                            dr_print("wod_oid") = Guid.NewGuid.ToString
                            dr_print("wod_wo_oid") = .Item("wo_oid")
                            dr_print("wod_use_bom") = .Item("wod_use_bom")
                            dr_print("wod_pt_bom_id") = .Item("wod_pt_bom_id")
                            dr_print("wod_comp") = .Item("wod_comp")
                            dr_print("wod_op") = .Item("wod_op")
                            dr_print("wod_qty_per") = .Item("wod_qty_per")
                            dr_print("wod_qty_req") = .Item("wod_qty_req")
                            dr_print("wod_qty_issued") = .Item("wod_qty_issued")
                            dr_print("unit_measure") = .Item("unit_measure")
                            'dr_print("wod_cost") = .Item("ptbomcost")
                            'dr_print("wod_dt") = .Item("wod_dt")
                            dr_print("avail_desc") = ds_avail.Tables("loc_mstr").Rows(y).Item("loc_desc")
                            dr_print("avail_qty") = qty_wh
                        End With

                        dt_print.Rows.Add(dr_print)

                    End If

                    If total = 0 Then
                        y = ds_avail.Tables("loc_mstr").Rows.Count - 1
                    End If
                Next

                If total <> 0 Then
                    req = total

                    dr_print = dt_print.NewRow()
                    With ds_printing.Tables("printing_wo").Rows(x)
                        dr_print("wo_oid") = .Item("wo_oid")
                        dr_print("wo_en_id") = .Item("wo_en_id")
                        dr_print("wo_si_id") = .Item("wo_si_id")
                        dr_print("wo_id") = .Item("wo_id")
                        dr_print("wo_code") = .Item("wo_code")
                        dr_print("wo_type") = .Item("wo_type")
                        dr_print("wo_pt_id") = .Item("wo_pt_id")
                        dr_print("wo_qty_ord") = .Item("wo_qty_ord")
                        dr_print("wo_qty_comp") = .Item("wo_qty_comp")
                        dr_print("wo_qty_rjc") = .Item("wo_qty_rjc")
                        dr_print("wo_ord_date") = .Item("wo_ord_date")
                        dr_print("wo_rel_date") = .Item("wo_rel_date")
                        dr_print("wo_due_date") = .Item("wo_due_date")
                        dr_print("wo_yield_pct") = .Item("wo_yield_pct")
                        dr_print("wo_bom_id") = .Item("wo_bom_id")
                        dr_print("wo_ro_id") = .Item("wo_ro_id")
                        dr_print("wo_status") = .Item("wo_status")
                        dr_print("wo_remarks") = .Item("wo_remarks")
                        dr_print("wo_dt") = .Item("wo_dt")
                        dr_print("si_id") = .Item("si_id")
                        dr_print("si_desc") = .Item("si_desc")
                        dr_print("pt_id") = .Item("pt_id")
                        dr_print("pt_code") = .Item("pt_code")
                        dr_print("pt_desc1") = .Item("pt_desc1")
                        dr_print("pt_desc2") = .Item("pt_desc2")
                        'dr_print("pt_um") = .Item("pt_um")
                        dr_print("bom_id") = .Item("bom_id")
                        dr_print("bom_desc") = .Item("bom_desc")
                        dr_print("ro_id") = .Item("ro_id")
                        dr_print("ro_desc") = .Item("ro_desc")
                        dr_print("ptbomcode") = .Item("ptbomcode")
                        dr_print("ptbomdesc") = .Item("ptbomdesc")
                        dr_print("ptbomdesc2") = .Item("ptbomdesc2")
                        dr_print("wod_oid") = Guid.NewGuid.ToString
                        dr_print("wod_wo_oid") = .Item("wo_oid")
                        dr_print("wod_use_bom") = .Item("wod_use_bom")
                        dr_print("wod_pt_bom_id") = .Item("wod_pt_bom_id")
                        dr_print("wod_comp") = .Item("wod_comp")
                        dr_print("wod_op") = .Item("wod_op")
                        dr_print("wod_qty_per") = .Item("wod_qty_per")
                        dr_print("wod_qty_req") = .Item("wod_qty_req")
                        dr_print("wod_qty_issued") = .Item("wod_qty_issued")
                        dr_print("unit_measure") = .Item("unit_measure")
                        'dr_print("wod_cost") = .Item("ptbomcost")
                        'dr_print("wod_dt") = .Item("wod_dt")
                        dr_print("avail_desc") = "Not Available"
                        dr_print("avail_qty") = req
                    End With

                    dt_print.Rows.Add(dr_print)

                End If
            Next

            Dim rpt As New XRWorkOrder

            Try
                With rpt
                    '    Try
                    '        Using objcb As New master_new.WDABasepgsql("", "")
                    '            With objcb
                    '                .SQL = _sql
                    '                .InitializeCommand()
                    '                .FillDataSet(ds_bantu, "data")
                    '            End With
                    '        End Using
                    '    Catch ex As Exception
                    '        MessageBox.Show(ex.Message)
                    '        Exit Sub
                    '    End Try

                    If ds_print.Tables(0).Rows.Count = 0 Then
                        MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    .DataSource = ds_print
                    .DataMember = "print_wo"
                    .ShowPreview()
                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String
        Dim _wo_oid_mstr As String
        Dim field As String = "wo_oid"

        row = BindingContext(ds.Tables(0)).Position

        _wo_oid_mstr = ds.Tables(0).Rows(row).Item("wo_oid")

        ds_printing = New DataSet
        Using objcb As New master_new.WDABasepgsql("", "")
            With objcb
                _sql = "SELECT  " _
                    & "   wo_mstr.wo_oid, " _
                    & "   wo_mstr.wo_en_id, " _
                    & "   wo_mstr.wo_si_id, " _
                    & "   wo_mstr.wo_id, " _
                    & "   wo_mstr.wo_code, " _
                    & "   wo_mstr.wo_type, " _
                    & "   wo_mstr.wo_pt_id, " _
                    & "   wo_mstr.wo_qty_ord, " _
                    & "   wo_mstr.wo_qty_comp, " _
                    & "   wo_mstr.wo_qty_rjc, " _
                    & "   wo_mstr.wo_ord_date, " _
                    & "   wo_rel_date, " _
                    & "   wo_mstr.wo_due_date, " _
                    & "   wo_mstr.wo_yield_pct, " _
                    & "   wo_mstr.wo_bom_id, " _
                    & "   wo_mstr.wo_ro_id, " _
                    & "   wo_mstr.wo_status, " _
                    & "   wo_mstr.wo_remarks, " _
                    & "   wo_mstr.wo_dt, " _
                    & "   si_mstr.si_id, " _
                    & "   si_mstr.si_desc, " _
                    & "   wod_det.wod_oid, " _
                    & "   wod_det.wod_wo_oid, " _
                    & "   wod_det.wod_use_bom, " _
                    & "   wod_det.wod_pt_bom_id, " _
                    & "   wod_det.wod_comp, " _
                    & "   wod_det.wod_op, " _
                    & "   wod_det.wod_qty_per, " _
                    & "   wod_det.wod_qty_req, " _
                    & "   wod_det.wod_qty_alloc, " _
                    & "   wod_det.wod_qty_picked, " _
                    & "   wod_det.wod_qty_issued, " _
                    & "   wod_det.wod_cost, " _
                    & "   wod_det.wod_dt, " _
                    & "   pt_mstr.pt_id, " _
                    & "   pt_mstr.pt_code, " _
                    & "   pt_mstr.pt_desc1, " _
                    & "   pt_mstr.pt_desc2, " _
                    & "   pt_mstr.pt_um, " _
                    & "   bom_mstr.bom_id, " _
                    & "   bom_mstr.bom_desc, " _
                    & "   ro_mstr.ro_id, " _
                    & "   ro_mstr.ro_desc, " _
                    & " (SELECT code_mstr.code_name FROM code_mstr WHERE code_mstr.code_id = pt_mstr.pt_um) AS unit_measure, " _
                    & " CASE  " _
                    & "    WHEN wod_use_bom = 'Y' " _
                    & "      THEN (SELECT bom_mstr.bom_code from bom_mstr where bom_id = wod_pt_bom_id) " _
                    & "    ELSE  (SELECT pt_mstr.pt_code from pt_mstr where pt_id = wod_pt_bom_id) " _
                    & "  END AS ptbomcode, " _
                    & "  CASE  " _
                    & "    WHEN wod_use_bom = 'Y' " _
                    & "      THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id = wod_pt_bom_id) " _
                    & "    ELSE  (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id = wod_pt_bom_id) " _
                    & "  END AS ptbomdesc, " _
                    & "  CASE  " _
                    & "    WHEN wod_use_bom = 'Y' " _
                    & "      THEN ' ' " _
                    & "    ELSE  (SELECT pt_mstr.pt_desc2 from pt_mstr where pt_id = wod_pt_bom_id) " _
                    & "  END AS ptbomdesc2, " _
                    & "   CASE WHEN wod_use_bom = 'Y' " _
                    & "   THEN ' ' " _
                    & "   ELSE (SELECT code_mstr.code_name from code_mstr inner join pt_mstr on pt_um=code_id where pt_id = wod_pt_bom_id) " _
                    & "   END AS unit_measure, " _
                    & "   pt_mstr.pt_desc1 AS avail_desc, " _
                    & "   wod_det.wod_qty_req AS avail_qty " _
                    & " FROM " _
                    & "   wo_mstr " _
                    & "  INNER JOIN  wod_det ON ( wo_mstr.wo_oid =  wod_det.wod_wo_oid) " _
                    & "  INNER JOIN  bom_mstr ON ( wo_mstr.wo_bom_id =  bom_mstr.bom_id) " _
                    & "  INNER JOIN  ro_mstr ON ( wo_mstr.wo_ro_id =  ro_mstr.ro_id) " _
                    & "  INNER JOIN  si_mstr ON ( wo_mstr.wo_si_id =  si_mstr.si_id) " _
                    & "  INNER JOIN  pt_mstr ON ( wo_mstr.wo_pt_id =  pt_mstr.pt_id) " _
                    & " WHERE wo_mstr.wo_oid = " & SetSetringDB(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item(field)) & " " _
                    & " and wod_det.wod_qty_req - wod_det.wod_qty_issued > 0 "

                .SQL = _sql
                .InitializeCommand()
                .FillDataSet(ds_printing, "printing_wo")
            End With
        End Using


        If ds_printing.Tables("printing_wo").Rows.Count = 0 Then
            MsgBox("All Part has issued...", MsgBoxStyle.Critical, "WO Release can't print...")
        Else
            check_availability_print()
        End If
        'Dim rpt As New XRPurchaseOrderPrintOut
        'Try
        '    With rpt
        '        Try
        '            Using objcb As New master_new.WDABasepgsql("", "")
        '                With objcb
        '                    .SQL = _sql
        '                    .InitializeCommand()
        '                    .FillDataSet(ds_bantu, "data")

        '                End With
        '            End Using
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '            Exit Sub
        '        End Try


        '        If ds_bantu.Tables(0).Rows.Count = 0 Then
        '            MessageBox.Show("Data Doesn't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            Exit Sub
        '        End If

        '        .DataSource = ds_bantu
        '        .DataMember = "data"

        '        .ShowPreview()
        '    End With
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

    End Sub
End Class