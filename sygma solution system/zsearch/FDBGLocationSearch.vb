Imports master_new.ModFunction
Imports DevExpress.XtraEditors.Controls
Imports master_new.PGSqlConn


Public Class FDBGLocationSearch
    Public _row As Integer
    Public _en_id, _dbg_id As Integer
    Public _ptnr_id, _sq_ptnr_id_sold As Integer
    Public _cu_id As Integer
    Public _obj, _objk, _objks As Object
    Public _sq_code, _ppn_type, _sq_trans_rmks, _sq_oid As String
    Public _sq_ptnr_id_sold_mstr
    Public _interval As Integer
    Public _loc_id As Integer
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Dim _conf_value As String
    Dim ds_attr As New DataSet
    Public _date As Date

    Private Sub FDBGLocationSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
        _conf_value = func_coll.get_conf_file("wf_sales_quotation")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Ship to", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT distinct  " _
                    & "  public.dbgd_det.dbgd_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.dbgd_det.dbgd_ptnr_id, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  public.loc_mstr.loc_id, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.dbgd_det.dbgd_parent_id, " _
                    & "  public.dbg_group.dbg_id " _
                    & "FROM " _
                    & "  public.dbgd_det " _
                    & "  LEFT OUTER JOIN public.loc_mstr ON (public.dbgd_det.dbgd_ptnr_id = public.loc_mstr.loc_ptnr_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.dbgd_det.dbgd_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.dbg_group ON (public.dbgd_det.dbgd_dbg_oid = public.dbg_group.dbg_oid) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.dbgd_det.dbgd_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                    & "  WHERE dbg_id = " + _dbg_id.ToString

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
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

        Dim ds_bantu As New DataSet
        Dim i As Integer
        Dim _exc_rate As Double = 0

        'If fobject.name = FTransferIssues.Name Then
        _objks.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
        _objks.tag = ds.Tables(0).Rows(_row_gv).Item("dbgd_ptnr_id")

        fobject.ptsfr_en_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("en_desc"))
        fobject.ptsfr_en_id.tag = SetString(ds.Tables(0).Rows(_row_gv).Item("dbgd_en_id"))

        fobject.ptsfr_en_to_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("en_desc"))
        fobject.ptsfr_en_to_id.tag = SetString(ds.Tables(0).Rows(_row_gv).Item("dbgd_en_id"))

        fobject.ptsfr_loc_to_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
        fobject.ptsfr_loc_to_id.tag = SetString(ds.Tables(0).Rows(_row_gv).Item("loc_id"))

        'If ds.Tables(0).Rows(_row_gv).Item("sq_booking") = "Y" Then
        '    fobject.ptsfr_booking.Checked = True
        'End If

        'If ds.Tables(0).Rows(_row_gv).Item("sq_cons") = "Y" Then
        '    fobject.ptsfr_cons.Checked = True
        'End If

        'If ds.Tables(0).Rows(_row_gv).Item("sq_dropshipper") = "Y" Then
        '    fobject.ptsfr_cons.Checked = True

        '    _objk.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold")
        '    _objk.tag = ds.Tables(0).Rows(_row_gv).Item("sq_ptnr_id_sold")

        '    _objks.text = ds.Tables(0).Rows(_row_gv).Item("dbg_name")
        '    _objks.tag = ds.Tables(0).Rows(_row_gv).Item("sq_dbg_ptnr_id")
        'End If

        'If fobject.ptsfr_dropship.Checked = True Then

        'End If

        'fobject.ptsfr_sq_dbg_id.tag = SetString(ds.Tables(0).Rows(_row_gv).Item("sq_dbg_ptnr_id"))
        'fobject.ptsfr_sq_dbg_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name_parent"))

        'fobject.ptsfr_loc_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_id")
        'fobject.ptsfr_loc_git.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_git")
        'fobject.ptsfr_loc_to_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_id")

        'If ds.Tables(0).Rows(_row_gv).Item("sq_cons") = "Y" Then
        '    'fobject.ptsfr_loc_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_id")
        '    'fobject.ptsfr_loc_git.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_git")
        '    'fobject.ptsfr_loc_to_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_to_id")
        'End If

        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  sqd_oid, " _
                        & "  sqd_dom_id, " _
                        & "  sqd_en_id, " _
                        & "  sqd_add_by, " _
                        & "  sqd_add_date, " _
                        & "  sqd_upd_by, " _
                        & "  sqd_upd_date, " _
                        & "  sqd_sq_oid, " _
                        & "  sqd_seq, " _
                        & "  sqd_is_additional_charge, " _
                        & "  sqd_si_id, " _
                        & "  sqd_pt_id, " _
                        & "  sqd_rmks, " _
                        & "  sqd_qty, " _
                        & "  sqd_qty - coalesce(sqd_qty_transfer,0) as sqd_qty_open, " _
                        & "  sqd_um, " _
                        & "  sqd_cost, " _
                        & "  sqd_price, " _
                        & "  sqd_disc, " _
                        & "  sqd_sales_ac_id, " _
                        & "  sqd_sales_sb_id, " _
                        & "  sqd_sales_cc_id, " _
                        & "  sqd_disc_ac_id, " _
                        & "  sqd_um_conv, " _
                        & "  sqd_qty_real, " _
                        & "  sqd_taxable, " _
                        & "  sqd_tax_inc, " _
                        & "  sqd_tax_class, " _
                        & "  sqd_ppn_type, " _
                        & "  sqd_status, " _
                        & "  sqd_dt, " _
                        & "  sqd_payment, " _
                        & "  sqd_dp, " _
                        & "  sqd_sales_unit, " _
                        & "  sqd_loc_id, " _
                        & "  sqd_serial, " _
                        & "  en_desc, " _
                        & "  si_desc, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_type, " _
                        & "  pt_ls, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                        & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                        & "  sb_desc, " _
                        & "  cc_desc, " _
                        & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                        & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                        & "  tax_class.code_name as sqd_tax_class_name, " _
                        & "  loc_desc, " _
                        & "  sqd_pod_oid, " _
                        & "  sqd_invc_oid " _
                        & "FROM  " _
                        & "  public.sqd_det " _
                        & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                        & "  inner join en_mstr on en_id = sqd_en_id " _
                        & "  inner join si_mstr on si_id = sqd_si_id " _
                        & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                        & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                        & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                        & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                        & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                        & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                        & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                        & "  where (sqd_qty - coalesce(sqd_qty_transfer,0)) > 0 " _
                        & "  and sqd_en_id = '" + _en_id.ToString + "'" _
                        & "  and sqd_sq_oid = '" + _sq_oid.ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sqd_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        fobject.ds_edit.tables(0).clear()

        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = fobject.ds_edit.Tables(0).NewRow
            _dtrow("ptsfrd_oid") = Guid.NewGuid.ToString
            _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_pt_id")
            _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
            _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
            _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
            _dtrow("ptsfrd_qty_open") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_open")
            _dtrow("ptsfrd_qty") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_open")
            _dtrow("ptsfrd_um") = ds_bantu.Tables(0).Rows(i).Item("sqd_um")
            _dtrow("ptsfrd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
            _dtrow("ptsfrd_cost") = ds_bantu.Tables(0).Rows(i).Item("sqd_cost")
            _dtrow("ptsfrd_pbd_oid") = ""
            _dtrow("ptsfrd_sqd_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_oid")
            _dtrow("ptsfrd_invc_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_invc_oid")
            fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
        Next
        fobject.ds_edit.Tables(0).AcceptChanges()
        fobject.gv_edit.BestFitColumns()


        Dim ssql As String
        ssql = "select loc_id,loc_code, loc_desc, code_name from loc_mstr" _
                & " inner join code_mstr on code_id = loc_type " _
                & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
                & " and loc_en_id in (0," + _en_id.ToString & ") and loc_active ~~* 'y' and loc_ptnr_id=" _
                & ds.Tables(0).Rows(_row_gv).Item("dbgd_ptnr_id") & " order by loc_desc"

        Dim dt2 As New DataTable
        dt2 = master_new.PGSqlConn.GetTableData(ssql)

        With fobject.ptsfr_loc_to_id
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New LookUpColumnInfo("loc_id", "ID", 20))
                .Properties.Columns.Add(New LookUpColumnInfo("loc_code", "Code", 20))
                .Properties.Columns.Add(New LookUpColumnInfo("loc_desc", "Description", 20))
            End If
            .Properties.DataSource = dt2
            .Properties.DisplayMember = dt2.Columns("loc_desc").ToString
            .Properties.ValueMember = dt2.Columns("loc_id").ToString
            If dt2.Rows.Count > 0 Then
                .EditValue = dt2.Rows(0).Item("loc_id")
            End If

            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            .Properties.BestFit()
            .Properties.DropDownRows = 30
            .Properties.PopupWidth = 300
        End With


        'End If
    End Sub
End Class