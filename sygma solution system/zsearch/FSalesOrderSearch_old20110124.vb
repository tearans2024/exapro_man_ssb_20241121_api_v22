Imports master_new.ModFunction

Public Class FSalesOrderSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FSalesOrderSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_sales_order")
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FSalesOrderProjectShipment" Or fobject.name = "FSalesOrderProjectReturn" Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "SO Number", "sopj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Customer", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        End If
        
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FSalesOrderShipment" Or fobject.name = "FSalesOrderInst" _
           Or fobject.name = "FSalesOrderATP" Or fobject.name = "FSalesOrderBAST" _
           Or fobject.name = "FSalesOrderBAUT" Or fobject.name = "FSalesOrderMT" Then
            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and so_trans_id ~~* 'I' "
            End If

        ElseIf fobject.name = "FSalesOrderReturn" Then
            get_sequel = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_date, " _
                    & "  so_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and so_en_id = " + _en_id.ToString
        ElseIf fobject.name = "FDRCRMemo" Then
            get_sequel = "SELECT  " _
                & "  public.so_mstr.so_oid, " _
                & "  public.so_mstr.so_en_id, " _
                & "  public.so_mstr.so_code, " _
                & "  public.so_mstr.so_ptnr_id_sold, " _
                & "  public.so_mstr.so_date, " _
                & "  public.so_mstr.so_si_id, " _
                & "  public.so_mstr.so_close_date, " _
                & "  public.ptnr_mstr.ptnr_name as ptnr_name_sold, " _
                & "  public.si_mstr.si_desc, en_desc " _
                & "FROM " _
                & "  public.so_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_sold = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.si_mstr ON (public.so_mstr.so_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.en_mstr ON (public.so_mstr.so_en_id = public.en_mstr.en_id)" _
                & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and so_ptnr_id_bill = " + _ptnr_id.ToString _
                & "  and so_en_id = " + _en_id.ToString _
                & "  and so_cu_id = " + _cu_id.ToString
        ElseIf fobject.name = "FSalesOrderProjectShipment" Or fobject.name = "FSalesOrderProjectReturn" Then
            get_sequel = "SELECT  " _
                    & "  sopj_oid, " _
                    & "  sopj_dom_id, " _
                    & "  sopj_en_id, " _
                    & "  sopj_add_by, " _
                    & "  sopj_add_date, " _
                    & "  sopj_upd_by, " _
                    & "  sopj_upd_date, " _
                    & "  sopj_code, " _
                    & "  sopj_ptnr_id, " _
                    & "  sopj_date, " _
                    & "  sopj_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.sopj_mstr " _
                    & "  inner join en_mstr on en_id = sopj_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sopj_ptnr_id " _
                    & "  inner join si_mstr on si_id = sopj_si_id " _
                    & "  where sopj_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and sopj_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and sopj_en_id = " + _en_id.ToString
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

        Dim ds_bantu As New DataSet
        Dim i As Integer

        If fobject.name = "FSalesOrderShipment" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid")
            fobject.soship_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_shipment,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_shipment,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                _dtrow("soshipd_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderReturn" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid")
            fobject.soship_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty_shipment - coalesce(sod_qty_return,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty_shipment - coalesce(sod_qty_return,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                _dtrow("soshipd_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FDRCRMemo" Then
            fobject.gv_edit_so.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
            fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_oid", ds.Tables(0).Rows(_row_gv).Item("so_oid"))
            fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("so_code"))
            fobject.gv_edit_so.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("so_date"))
            fobject.gv_edit_so.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderInst" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid")
            fobject.sotran_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_inst,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_inst,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("sotrand_oid") = Guid.NewGuid.ToString
                _dtrow("sotrand_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("sotrand_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("sotrand_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("sotrand_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("sotrand_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("sotrand_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderATP" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid")
            fobject.sotran_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_atp,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_atp,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("sotrand_oid") = Guid.NewGuid.ToString
                _dtrow("sotrand_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("sotrand_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("sotrand_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("sotrand_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("sotrand_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("sotrand_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderBAST" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid")
            fobject.sotran_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_bast,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_bast,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("sotrand_oid") = Guid.NewGuid.ToString
                _dtrow("sotrand_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("sotrand_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("sotrand_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("sotrand_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("sotrand_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("sotrand_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderBAUT" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid")
            fobject.sotran_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_baut,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_baut,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("sotrand_oid") = Guid.NewGuid.ToString
                _dtrow("sotrand_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("sotrand_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("sotrand_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("sotrand_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("sotrand_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("sotrand_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderMT" Then
            fobject._so_oid = ds.Tables(0).Rows(_row_gv).Item("so_oid")
            fobject.sotran_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sod_oid, " _
                            & "  sod_dom_id, " _
                            & "  sod_en_id, " _
                            & "  sod_add_by, " _
                            & "  sod_add_date, " _
                            & "  sod_upd_by, " _
                            & "  sod_upd_date, " _
                            & "  sod_so_oid, " _
                            & "  sod_seq, " _
                            & "  sod_is_additional_charge, " _
                            & "  sod_si_id, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, sod_qty - coalesce(sod_qty_mt,0) as sod_qty_open, " _
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
                            & "  sod_um, " _
                            & "  sod_cost, " _
                            & "  sod_price, " _
                            & "  sod_disc, " _
                            & "  sod_sales_ac_id, " _
                            & "  sod_sales_sb_id, " _
                            & "  sod_sales_cc_id, " _
                            & "  sod_disc_ac_id, " _
                            & "  sod_um_conv, " _
                            & "  sod_qty_real, " _
                            & "  sod_taxable, " _
                            & "  sod_tax_inc, " _
                            & "  sod_tax_class, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
                            & "  inner join en_mstr on en_id = sod_en_id " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class " _
                            & "  where (sod_qty - coalesce(sod_qty_mt,0)) > 0 " _
                            & "  and sod_so_oid = '" + ds.Tables(0).Rows(_row_gv).Item("so_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("sotrand_oid") = Guid.NewGuid.ToString
                _dtrow("sotrand_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("sotrand_si_id") = ds_bantu.Tables(0).Rows(i).Item("sod_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("sotrand_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_qty") = ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")
                _dtrow("sotrand_um") = ds_bantu.Tables(0).Rows(i).Item("sod_um")
                _dtrow("sotrand_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("sotrand_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sod_um_conv")
                _dtrow("sotrand_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sod_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sod_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderProjectShipment" Then
            fobject._sopj_oid = ds.Tables(0).Rows(_row_gv).Item("sopj_oid")
            fobject.soship_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("sopj_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sopjd_oid, " _
                            & "  sopjd_dom_id, " _
                            & "  sopjd_en_id, " _
                            & "  sopjd_add_by, " _
                            & "  sopjd_add_date, " _
                            & "  sopjd_upd_by, " _
                            & "  sopjd_upd_date, " _
                            & "  sopjd_sopj_oid, " _
                            & "  sopjd_seq, " _
                            & "  sopjd_si_id, " _
                            & "  sopjd_pt_id, " _
                            & "  sopjd_rmks, " _
                            & "  sopjd_qty, sopjd_qty - coalesce(sopjd_qty_shipment,0) as sopjd_qty_open, " _
                            & "  sopjd_qty_allocated, " _
                            & "  sopjd_qty_shipment, " _
                            & "  sopjd_um, " _
                            & "  sopjd_cost, " _
                            & "  sopjd_price, " _
                            & "  sopjd_disc, " _
                            & "  sopjd_um_conv, " _
                            & "  sopjd_qty_real, " _
                            & "  sopjd_taxable, " _
                            & "  sopjd_tax_inc, " _
                            & "  sopjd_tax_class, " _
                            & "  sopjd_status, " _
                            & "  sopjd_dt, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  tax_class.code_name as sopjd_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sopjd_det " _
                            & "  inner join sopj_mstr on sopj_oid = sopjd_sopj_oid " _
                            & "  inner join en_mstr on en_id = sopjd_en_id " _
                            & "  inner join si_mstr on si_id = sopjd_si_id " _
                            & "  inner join pt_mstr on pt_id = sopjd_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sopjd_um	 " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sopjd_tax_class " _
                            & "  where (sopjd_qty - coalesce(sopjd_qty_shipment,0)) > 0 " _
                            & "  and sopjd_sopj_oid = '" + ds.Tables(0).Rows(_row_gv).Item("sopj_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                _dtrow("soshipd_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sopjd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sopjd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("sopjd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sopjd_qty_open")
                _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("sopjd_qty_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("sopjd_um")
                _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sopjd_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sopjd_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sopjd_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderProjectReturn" Then
            fobject._sopj_oid = ds.Tables(0).Rows(_row_gv).Item("sopj_oid")
            fobject.soship_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("sopj_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  sopjd_oid, " _
                            & "  sopjd_dom_id, " _
                            & "  sopjd_en_id, " _
                            & "  sopjd_add_by, " _
                            & "  sopjd_add_date, " _
                            & "  sopjd_upd_by, " _
                            & "  sopjd_upd_date, " _
                            & "  sopjd_sopj_oid, " _
                            & "  sopjd_seq, " _
                            & "  sopjd_si_id, " _
                            & "  sopjd_pt_id, " _
                            & "  sopjd_rmks, " _
                            & "  sopjd_qty, sopjd_qty_shipment - coalesce(sopjd_qty_return,0) as sopjd_qty_open, " _
                            & "  sopjd_qty_allocated, " _
                            & "  sopjd_qty_shipment, " _
                            & "  sopjd_qty_invoice, " _
                            & "  sopjd_um, " _
                            & "  sopjd_cost, " _
                            & "  sopjd_price, " _
                            & "  sopjd_disc, " _
                            & "  sopjd_um_conv, " _
                            & "  sopjd_qty_real, " _
                            & "  sopjd_taxable, " _
                            & "  sopjd_tax_inc, " _
                            & "  sopjd_tax_class, " _
                            & "  sopjd_status, " _
                            & "  sopjd_dt, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_ls, " _
                            & "  pt_type, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  tax_class.code_name as sopjd_tax_class_name, " _
                            & "  pt_loc_id, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sopjd_det " _
                            & "  inner join sopj_mstr on sopj_oid = sopjd_sopj_oid " _
                            & "  inner join en_mstr on en_id = sopjd_en_id " _
                            & "  inner join si_mstr on si_id = sopjd_si_id " _
                            & "  inner join pt_mstr on pt_id = sopjd_pt_id " _
                            & "  inner join loc_mstr on loc_id = pt_loc_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sopjd_um	 " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sopjd_tax_class " _
                            & "  where (sopjd_qty_shipment - coalesce(sopjd_qty_return,0)) > 0 " _
                            & "  and sopjd_sopj_oid = '" + ds.Tables(0).Rows(_row_gv).Item("sopj_oid") + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                _dtrow("soshipd_sod_oid") = ds_bantu.Tables(0).Rows(i).Item("sopjd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sopjd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("sopjd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("pt_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("sopjd_qty_open")
                _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("sopjd_qty_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("sopjd_um")
                _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sopjd_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("sopjd_qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("sopjd_um_conv"))

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class

