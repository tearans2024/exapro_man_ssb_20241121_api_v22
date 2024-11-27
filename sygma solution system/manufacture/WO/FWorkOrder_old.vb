Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FWorkOrder

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _wo_oid_mstr As String
    Dim ds_edit As DataSet
    Dim _qty_wo_before As Double = 0
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _wod_related_oid As String = ""

    Private Sub FWorkOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        gv_detail.OptionsView.ShowFooter = True
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        wo_en_id.Properties.DataSource = dt_bantu
        wo_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        wo_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        wo_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "wo_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Order", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Comp.", "wo_qty_comp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Reject", "wo_qty_rjc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Yield Percent", "wo_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_master, "wo_ro_id", False)
        'har 20110618
        add_column_copy(gv_master, "Project Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Desc", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Routing Desc.", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "wo_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "wod_oid", False)
        add_column(gv_detail, "wod_wo_oid", False)
        add_column_copy(gv_detail, "Use BOM", "wod_use_bom", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_detail, "wod_pt_bom_id", False)
        'add_column_copy(gv_detail, "Component", "wod_comp", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "PT/BOM Desc", "ptbomdesc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Operation", "wod_op", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Per", "wod_qty_per", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Alloc", "wod_qty_alloc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Picked", "wod_qty_picked", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Issued", "wod_qty_issued", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Remaining", "wod_qty_remaining", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Cost", "wod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
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
                    & "   " _
                    & "  wo_ro_id, " _
                    & "  wo_status, " _
                    & "  wo_remarks, " _
                    & "  si_desc, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  wo_pjc_id,pjc_code,pjc_desc, " _
                    & "  ro_desc, " _
                    & "  en_desc, " _
                    & "  wo_dt " _
                    & "FROM  " _
                    & "  public.wo_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.wo_mstr.wo_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.wo_mstr.wo_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                    & "  LEFT OUTER JOIN public.pjc_mstr ON (public.wo_mstr.wo_pjc_id = public.pjc_mstr.pjc_id) " _
                    & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id) " _
                    & "  where wo_mstr.wo_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and wo_mstr.wo_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
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
            & "  wod_qty_req - coalesce(wod_qty_issued,0) as wod_qty_remaining, " _
            & "  wod_cost, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  wod_dt " _
            & "FROM  " _
            & "  public.wod_det " _
            & "  inner join wo_mstr on wo_mstr.wo_oid = wod_det.wod_wo_oid " _
            & "  inner join pt_mstr on pt_id = wod_pt_bom_id " _
            & "  where wo_mstr.wo_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and wo_mstr.wo_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "  order by pt_code"

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
    Private Sub req_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wo_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(wo_en_id.EditValue))
        wo_si_id.Properties.DataSource = dt_bantu
        wo_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        wo_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        wo_si_id.ItemIndex = 0

       

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ro_mstr(wo_en_id.EditValue))
        'wo_ro_id.Properties.DataSource = dt_bantu
        'wo_ro_id.Properties.DisplayMember = dt_bantu.Columns("ro_desc").ToString
        'wo_ro_id.Properties.ValueMember = dt_bantu.Columns("ro_id").ToString
        'wo_ro_id.ItemIndex = 0


    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        wo_en_id.Focus()
        wo_en_id.ItemIndex = 0
        wo_si_id.ItemIndex = 0
        wo_pt_id.EditValue = ""
        part_desc.Text = ""
        wo_qty_ord.Text = 0
        wo_yield_pct.Text = 0
        wo_ord_date.DateTime = Now
        wo_due_date.DateTime = Now
        'wo_bom_id.ItemIndex = 0
        'wo_ro_id.ItemIndex = 0
        wo_ro_id.EditValue = "-"
        wo_remarks.Text = ""
        wo_pjc_id.Text = ""
        wo_pjc_id.Tag = ""
        pjc_desc.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        If wo_ro_id.Tag.ToString = "" Then
            Box("Routing can't null")
            Return False
        End If

        If IsDBNull(wo_pt_id.EditValue) Then
            MessageBox.Show("Part Code Cannot Null...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _wo_oid As Guid
        _wo_oid = Guid.NewGuid

        Dim _wo_code As String

        _wo_code = func_coll.get_transaction_number("WO", wo_en_id.GetColumnValue("en_code"), "wo_mstr", "wo_code")

        If wo_pjc_id.Text <> "" Then
            '---------------- cek dulu qty di boq_qty_wo nya 
            'sys 20110623
            Dim sSQL As String
            sSQL = "select boqd_qty - coalesce(boqd_qty_wo,0) as boqd_qty_open from boqd_det where boqd_pt_id = " + SetInteger(wo_pt_id.Tag) + _
                   " and boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + wo_pjc_id.Text + "'))"

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            If dt.Rows(0).Item("boqd_qty_open") < wo_qty_ord.EditValue Then
                MessageBox.Show("Qty Can't Higher Than Qty Open WO At Bill Of Quantity...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            '-------------------------------------------------
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.wo_mstr " _
                                            & "( " _
                                            & "  wo_oid, " _
                                            & "  wo_dom_id, " _
                                            & "  wo_en_id, " _
                                            & "  wo_si_id, " _
                                            & "  wo_id, " _
                                            & "  wo_code, " _
                                            & "  wo_pt_id, " _
                                            & "  wo_qty_ord, " _
                                            & "  wo_qty_comp, " _
                                            & "  wo_qty_rjc, " _
                                            & "  wo_ord_date, " _
                                            & "  wo_due_date, " _
                                            & "  wo_yield_pct, " _
                                            & "  wo_pjc_id, " _
                                            & "  wo_ro_id, " _
                                            & "  wo_status, " _
                                            & "  wo_remarks, " _
                                            & "  wo_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_wo_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetIntegerDB(wo_en_id.EditValue) & ",  " _
                                            & SetIntegerDB(wo_si_id.EditValue) & ",  " _
                                            & SetInteger(func_coll.GetID("wo_mstr", wo_en_id.GetColumnValue("en_code"), "wo_id", "wo_en_id", wo_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(_wo_code.ToString) & ",  " _
                                            & SetIntegerDB(wo_pt_id.Tag) & ",  " _
                                            & SetIntegerDB(wo_qty_ord.EditValue) & ",  " _
                                            & "0,  " _
                                            & "0,  " _
                                            & SetDate(wo_ord_date.EditValue) & ",  " _
                                            & SetDate(wo_due_date.EditValue) & ",  " _
                                            & SetIntegerDB(wo_yield_pct.EditValue) & ",  " _
                                             & SetIntegerDB(wo_pjc_id.Tag) & ",  " _
                                            & SetIntegerDB(wo_ro_id.Tag) & ",  " _
                                            & "'F' ,  " _
                                            & SetSetring(wo_remarks.Text) & ",  " _
                                            & "current_timestamp " _
                                            & ");"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update boqd_det set boqd_qty_wo = coalesce(boqd_qty_wo,0) + " + SetIntegerDB(wo_qty_ord.EditValue) + _
                                               " where boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + wo_pjc_id.Text + "'))" + _
                                               " and boqd_pt_id = " + SetInteger(wo_pt_id.Tag)

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(_wo_oid.ToString, "wo_oid")
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

    Public Overrides Function edit_data() As Boolean

        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_status") = "R" Then
            Box("WO has been released")
            Exit Function
        End If
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                _wo_oid_mstr = .Item("wo_oid")
                wo_en_id.EditValue = .Item("wo_en_id")
                wo_si_id.EditValue = .Item("wo_si_id")
                wo_pt_id.EditValue = .Item("wo_pt_id")
                wo_qty_ord.EditValue = .Item("wo_qty_ord")
                _qty_wo_before = .Item("wo_qty_ord")
                wo_ord_date.DateTime = .Item("wo_ord_date")
                wo_due_date.DateTime = .Item("wo_due_date")
                wo_yield_pct.EditValue = .Item("wo_yield_pct")
                wo_ro_id.Text = .Item("ro_desc")
                wo_ro_id.Tag = .Item("wo_ro_id")
                wo_remarks.Text = SetString(.Item("wo_remarks"))
                wo_pjc_id.Text = SetString(.Item("pjc_code"))
                wo_pjc_id.Tag = .Item("wo_pjc_id")
                pjc_desc.Text = SetString(.Item("pjc_desc"))

            End With
            wo_en_id.Focus()

            Try
                dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        If wo_pjc_id.Text <> "" Then
            '---------------- cek dulu qty di boq_qty_wo nya 
            'sys 20110623
            Dim sSQL As String
            sSQL = "select boqd_qty - coalesce(boqd_qty_wo,0) as boqd_qty_open from boqd_det where boqd_pt_id = " + SetInteger(wo_pt_id.Tag) + _
                   " and boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + wo_pjc_id.Text + "'))"

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            If dt.Rows(0).Item("boqd_qty_open") + _qty_wo_before < wo_qty_ord.EditValue Then
                MessageBox.Show("Qty Can't Higher Than Qty Open WO At Bill Of Quantity...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            '-------------------------------------------------
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.wo_mstr   " _
                                            & "SET  " _
                                            & "  wo_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  wo_en_id = " & wo_en_id.EditValue & ",  " _
                                            & "  wo_si_id = " & wo_si_id.EditValue & ",  " _
                                            & "  wo_pt_id = " & wo_pt_id.EditValue & ",  " _
                                            & "  wo_qty_ord = " & SetIntegerDB(wo_qty_ord.EditValue) & ",  " _
                                            & "  wo_ord_date = " & SetDate(wo_due_date.DateTime) & ",  " _
                                            & "  wo_due_date = " & SetDate(wo_due_date.DateTime) & ",  " _
                                            & "  wo_yield_pct = " & SetIntegerDB(wo_yield_pct.EditValue) & ",  " _
                                            & "  wo_ro_id = " & SetIntegerDB(wo_ro_id.Tag) & ",  " _
                                            & "  wo_pjc_id = " & SetIntegerDB(wo_pjc_id.Tag) & ",  " _
                                            & "  wo_remarks = " & SetSetringDB(wo_remarks.Text) & ",  " _
                                            & "  wo_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  wo_oid = " & SetSetring(_wo_oid_mstr.ToString) & "  " _
                                            & ";"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update boqd_det set boqd_qty_wo = coalesce(boqd_qty_wo,0) - " + SetIntegerDB(_qty_wo_before) + _
                                               " where boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + wo_pjc_id.Text + "'))" + _
                                               " and boqd_pt_id = " + SetInteger(wo_pt_id.Tag)

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update boqd_det set boqd_qty_wo = coalesce(boqd_qty_wo,0) + " + SetIntegerDB(wo_qty_ord.EditValue) + _
                                               " where boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + wo_pjc_id.Text + "'))" + _
                                               " and boqd_pt_id = " + SetInteger(wo_pt_id.Tag)

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_wo_oid_mstr, "wo_oid")
                        edit = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
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

        Dim sSQL As String
        sSQL = "select coalesce(wod_qty_issued,0) as wod_qty_issued from wod_det where wod_wo_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid") & "'"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(sSQL)

        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("wod_qty_issued") > 0 Then
                MessageBox.Show("This WO has been issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
                Exit Function
            End If
        Next

        'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_status") = "R" Then
        '    Box("WO has been released")
        '    Exit Function
        'End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

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
                            .Command.CommandText = "delete from wo_mstr where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update boqd_det set boqd_qty_wo = coalesce(boqd_qty_wo,0) - " + SetIntegerDB(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_qty_ord")) + _
                                                   " where boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pjc_code") + "'))" + _
                                                   " and boqd_pt_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_pt_id"))
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
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

    Private Sub wo_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_pt_id.ButtonClick
        Dim frm As New FProdStrucSearch()
        frm.set_win(Me)
        frm._obj = wo_pt_id
        frm._en_id = wo_en_id.EditValue
        frm._prj_code = wo_pjc_id.Text
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub wo_ro_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_ro_id.ButtonClick
        Dim frm As New FRoutingSearch
        frm.set_win(Me)
        frm._en_id = wo_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub wo_pjc_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_pjc_id.ButtonClick
        Dim frm As New FProjectAccountSearch()
        frm.set_win(Me)
        frm._en_id = wo_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class