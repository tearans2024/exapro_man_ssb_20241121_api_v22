Imports master_new.ModFunction

Public Class FProjectShipment_so_Search
    Public _obj As Object
    Public _row, _cu_id, _cc_id, _en_id, _ptnr_id As Integer

    Private Sub FProjectShipmentSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        de_1.EditValue = Today()
        de_2.EditValue = Today()

        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.Name = "FProjectReturns_so" Then
            add_column(gv_master, "Shipment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectShipmentPrintSign" Then
            add_column(gv_master, "Shipment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectPrepaymentPrintSign" Then
            add_column(gv_master, "Prepayment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Prepayment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectReturnPrintSign" Then
            add_column(gv_master, "Return Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.Name = "FProjectReturns_so" Then
            get_sequel = "SELECT DISTINCT " _
                        & "  public.soship_mstr.soship_oid, " _
                        & "  public.soship_mstr.soship_dom_id, " _
                        & "  public.soship_mstr.soship_en_id, " _
                        & "  public.soship_mstr.soship_add_by, " _
                        & "  public.soship_mstr.soship_add_date, " _
                        & "  public.soship_mstr.soship_upd_by, " _
                        & "  public.soship_mstr.soship_upd_date, " _
                        & "  public.soship_mstr.soship_code, " _
                        & "  public.soship_mstr.soship_date, " _
                        & "  public.soship_mstr.soship_prj_oid, " _
                        & "  public.soship_mstr.soship_remark, " _
                        & "  public.soship_mstr.soship_dt, " _
                        & "  public.prj_mstr.prj_code, " _
                        & "  prj_ord_date, " _
                        & "  ptnr_sold.ptnr_name as ptnr_name_sold, " _
                        & "  ptnr_bill.ptnr_name as ptnr_name_bill " _
                        & "FROM " _
                        & "  public.soship_mstr " _
                        & "  INNER JOIN public.prj_mstr ON (public.soship_mstr.soship_prj_oid = public.prj_mstr.prj_oid) " _
                        & "  INNER JOIN public.ptnr_mstr ptnr_sold ON (public.prj_mstr.prj_ptnr_id_sold = ptnr_sold.ptnr_id)" _
                        & "  INNER JOIN public.ptnr_mstr ptnr_bill ON (public.prj_mstr.prj_ptnr_id_bill = ptnr_bill.ptnr_id)" _
                        & "  INNER JOIN public.soshipd_det ON (public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid) " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and ((soshipd_qty * -1) - coalesce(soshipd_qty_inv,0) - coalesce(soshipd_qty_return,0)) > 0  " _
                        & " and soship_trans_id = 'I' " _
                        & " and soship_is_shipment ~~* 'Y'" _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
        ElseIf fobject.Name = "FProjectShipmentPrintSign" Then
            get_sequel = "SELECT DISTINCT " _
                        & "  public.soship_mstr.soship_oid, " _
                        & "  public.soship_mstr.soship_dom_id, " _
                        & "  public.soship_mstr.soship_en_id, " _
                        & "  public.soship_mstr.soship_add_by, " _
                        & "  public.soship_mstr.soship_add_date, " _
                        & "  public.soship_mstr.soship_upd_by, " _
                        & "  public.soship_mstr.soship_upd_date, " _
                        & "  public.soship_mstr.soship_code, " _
                        & "  public.soship_mstr.soship_date, " _
                        & "  public.soship_mstr.soship_prj_oid, " _
                        & "  public.soship_mstr.soship_remark, " _
                        & "  public.soship_mstr.soship_dt, " _
                        & "  public.prj_mstr.prj_code, " _
                        & "  prj_ord_date, " _
                        & "  ptnr_sold.ptnr_name as ptnr_name_sold, " _
                        & "  ptnr_bill.ptnr_name as ptnr_name_bill " _
                        & "FROM " _
                        & "  public.soship_mstr " _
                        & "  INNER JOIN public.prj_mstr ON (public.soship_mstr.soship_prj_oid = public.prj_mstr.prj_oid) " _
                        & "  INNER JOIN public.ptnr_mstr ptnr_sold ON (public.prj_mstr.prj_ptnr_id_sold = ptnr_sold.ptnr_id)" _
                        & "  INNER JOIN public.ptnr_mstr ptnr_bill ON (public.prj_mstr.prj_ptnr_id_bill = ptnr_bill.ptnr_id)" _
                        & " where soship_en_id = " + SetInteger(_en_id) _
                        & " and soship_trans_id ~~* 'I' " _
                        & " and soship_trans_id ~~* 'I' " _
                        & " and coalesce(soship_is_prepayment,'N') ~~* 'N' " _
                        & " and coalesce(soship_is_shipment,'N') ~~* 'Y'" _
                        & " and (soship_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and soship_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by soship_code asc "
        ElseIf fobject.Name = "FProjectPrepaymentPrintSign" Then
            get_sequel = "SELECT DISTINCT " _
                        & "  public.soship_mstr.soship_oid, " _
                        & "  public.soship_mstr.soship_dom_id, " _
                        & "  public.soship_mstr.soship_en_id, " _
                        & "  public.soship_mstr.soship_add_by, " _
                        & "  public.soship_mstr.soship_add_date, " _
                        & "  public.soship_mstr.soship_upd_by, " _
                        & "  public.soship_mstr.soship_upd_date, " _
                        & "  public.soship_mstr.soship_code, " _
                        & "  public.soship_mstr.soship_date, " _
                        & "  public.soship_mstr.soship_prj_oid, " _
                        & "  public.soship_mstr.soship_remark, " _
                        & "  public.soship_mstr.soship_dt, " _
                        & "  public.prj_mstr.prj_code, " _
                        & "  prj_ord_date, " _
                        & "  ptnr_sold.ptnr_name as ptnr_name_sold, " _
                        & "  ptnr_bill.ptnr_name as ptnr_name_bill " _
                        & "FROM " _
                        & "  public.soship_mstr " _
                        & "  INNER JOIN public.prj_mstr ON (public.soship_mstr.soship_prj_oid = public.prj_mstr.prj_oid) " _
                        & "  INNER JOIN public.ptnr_mstr ptnr_sold ON (public.prj_mstr.prj_ptnr_id_sold = ptnr_sold.ptnr_id)" _
                        & "  INNER JOIN public.ptnr_mstr ptnr_bill ON (public.prj_mstr.prj_ptnr_id_bill = ptnr_bill.ptnr_id)" _
                        & " where soship_en_id = " + SetInteger(_en_id) _
                        & " and soship_trans_id ~~* 'I' " _
                        & " and soship_trans_id ~~* 'I' " _
                        & " and coalesce(soship_is_prepayment,'N') ~~* 'Y' " _
                        & " and coalesce(soship_is_shipment,'N') ~~* 'Y'" _
                        & " and (soship_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and soship_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by soship_code asc "
        ElseIf fobject.Name = "FProjectReturnPrintSign" Then
            get_sequel = "SELECT DISTINCT " _
                        & "  public.soship_mstr.soship_oid, " _
                        & "  public.soship_mstr.soship_dom_id, " _
                        & "  public.soship_mstr.soship_en_id, " _
                        & "  public.soship_mstr.soship_add_by, " _
                        & "  public.soship_mstr.soship_add_date, " _
                        & "  public.soship_mstr.soship_upd_by, " _
                        & "  public.soship_mstr.soship_upd_date, " _
                        & "  public.soship_mstr.soship_code, " _
                        & "  public.soship_mstr.soship_date, " _
                        & "  public.soship_mstr.soship_prj_oid, " _
                        & "  public.soship_mstr.soship_remark, " _
                        & "  public.soship_mstr.soship_dt, " _
                        & "  public.prj_mstr.prj_code, " _
                        & "  prj_ord_date, " _
                        & "  ptnr_sold.ptnr_name as ptnr_name_sold, " _
                        & "  ptnr_bill.ptnr_name as ptnr_name_bill " _
                        & "FROM " _
                        & "  public.soship_mstr " _
                        & "  INNER JOIN public.prj_mstr ON (public.soship_mstr.soship_prj_oid = public.prj_mstr.prj_oid) " _
                        & "  INNER JOIN public.ptnr_mstr ptnr_sold ON (public.prj_mstr.prj_ptnr_id_sold = ptnr_sold.ptnr_id)" _
                        & "  INNER JOIN public.ptnr_mstr ptnr_bill ON (public.prj_mstr.prj_ptnr_id_bill = ptnr_bill.ptnr_id)" _
                        & " where soship_en_id = " + SetInteger(_en_id) _
                        & " and soship_trans_id ~~* 'I' " _
                        & " and coalesce(soship_is_shipment,'N') ~~* 'N'" _
                        & " and (soship_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and soship_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by soship_code asc "
        End If

        Return get_sequel
    End Function

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

        If fobject.Name = "FProjectReturns_so" Then
            fobject.soship_oid_soship.text = ds.Tables(0).Rows(_row_gv).Item("soship_code")
            fobject._soship_soship_oid = ds.Tables(0).Rows(_row_gv).Item("soship_oid")
            fobject.soship_prj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("soship_prj_oid")
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  soshipd_det.soshipd_oid, " _
                            & "  soshipd_det.soshipd_soship_oid, " _
                            & "  soshipd_det.soshipd_prjd_oid, " _
                            & "  pt_mstr.pt_id, " _
                            & "  pt_mstr.pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  pt_mstr.pt_type, " _
                            & "  soshipd_det.soshipd_qty, " _
                            & "  soshipd_det.soshipd_qty_return, " _
                            & "  soshipd_det.soshipd_qty_inv, " _
                            & "  ((soshipd_qty * -1) - coalesce(soshipd_qty_return,0) - coalesce(soshipd_qty_inv,0)) as qty_open, " _
                            & "  soshipd_det.soshipd_um, " _
                            & "  um.code_name as soshipd_um_name, " _
                            & "  soshipd_det.soshipd_um_conv, " _
                            & "  soshipd_det.soshipd_qty_real, " _
                            & "  soshipd_det.soshipd_si_id, " _
                            & "  soshipd_det.soshipd_loc_id, " _
                            & "  soshipd_det.soshipd_lot_serial, " _
                            & "  soshipd_det.soshipd_dt, " _
                            & "  si_mstr.si_desc, " _
                            & "  loc_mstr.loc_desc, " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, " _
                            & "  prjd_prj_oid, " _
                            & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                            & "  prjd_seq, " _
                            & "  pt_ls, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class, " _
                            & "  prjd_trans_id " _
                            & "FROM  " _
                            & "  public.soshipd_det " _
                            & "  INNER JOIN public.soship_mstr ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
                            & "  INNER JOIN public.prjd_det ON (public.prjd_det.prjd_oid = public.soshipd_det.soshipd_prjd_oid ) " _
                            & "  INNER JOIN public.prj_mstr ON (public.prj_mstr.prj_oid = public.prjd_det.prjd_prj_oid) " _
                            & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = soshipd_um " _
                            & "  inner join si_mstr on si_id = soshipd_si_id " _
                            & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                            & "  where soship_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("soship_oid").ToString()) + "" _
                            & "  and ((soshipd_qty * -1) - coalesce(soshipd_qty_return,0) - coalesce(soshipd_qty_inv,0)) > 0 " _
                            & "  order by soshipd_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "prjd_det")
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
                _dtrow("soshipd_oid_shipment") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid")
                _dtrow("soshipd_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                _dtrow("prjd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '_dtrow("progress_pay_open") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                '_dtrow("soshipd_progress_pay") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("soshipd_um")
                _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("soshipd_um_name")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("soshipd_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("soshipd_um_conv"))
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("soshipd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("soshipd_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("soshipd_lot_serial") = "N"
                '_dtrow("soshipd_close_line") = "Y"
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
            '-------------------------------------------------------
        ElseIf fobject.Name = "FProjectShipmentPrintSign" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("soship_code")
        ElseIf fobject.Name = "FProjectPrepaymentPrintSign" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("soship_code")
        ElseIf fobject.Name = "FProjectReturnPrintSign" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("soship_code")
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
