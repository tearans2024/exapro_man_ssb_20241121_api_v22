Imports master_new.ModFunction

Public Class FProjectSearch
    Public _row, _cu_id, _cc_id, _en_id, _ptnr_id As Integer
    Public __from_edit As Boolean

    Private Sub FProjectSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'format_grid()
        form_first_load()
        de_1.EditValue = Today()
        de_2.EditValue = Today()

        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FProjectRelated" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectActivityOrder" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectGroup" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectShipment" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectShipment_so" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectATP" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectBAST" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FTransferIssues" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FProjectRelated" Or fobject.name = FBillOfQuantity.Name _
        Or fobject.name = FProfitLossProjectPrint.Name Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ") " _
                        & "order by prj_code asc "
        ElseIf fobject.name = "FProjectActivityOrder" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_ptnr_id_sold = " + SetInteger(_ptnr_id) _
                        & " and prj_trans_id = 'I' " _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
        ElseIf fobject.name = "FProjectGroup" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_trans_id = 'I' " _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
        ElseIf fobject.Name = "FProjectShipment" Or fobject.name = "FProjectShipments_so" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_trans_id = 'I' " _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
        ElseIf fobject.name = FBoQtoPR.Name Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id,boq_oid,pjc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & "  inner join boq_mstr on prj_oid = boq_sopj_oid " _
                         & "  inner join pjc_mstr on prj_code = pjc_code " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ") " _
                        & " and boq_trans_id='I' " _
                        & "order by prj_code asc "
            'ant 16 juli 2011
        ElseIf fobject.Name = "FDRCRMemoProject" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ord_date, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_trans_id = 'I' " _
                        & " and prj_ptnr_id_bill = " + SetInteger(_ptnr_id) _
                        & " and prj_cu_id = " + SetInteger(_cu_id) _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
            'rev by hendrik 2011-08-03--------------------------------------------------------------------
        ElseIf fobject.Name = "FProjectATP" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_trans_id = 'I' " _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
        ElseIf fobject.Name = "FProjectBAST" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_trans_id = 'I' " _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
            '---------------------------------------------------------------------------------
        ElseIf fobject.Name = "FTransferIssues" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold,ptnr_name, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and (prj_ord_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prj_ord_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by prj_code asc "
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

        If fobject.name = "FProjectRelated" Then
            fobject.gv_edit.SetRowCellValue(_row, "prjr_prj_oid", ds.Tables(0).Rows(_row_gv).Item("prj_oid"))
            fobject.prjr_prj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FProjectActivityOrder" Then
            fobject.pao_project_code.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")

            If __from_edit=True
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  false as status, " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id,loc_desc, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class, " _
                            & "  prjd_trans_id, " _
                            & "  coalesce(prjd_qty_pao,0) as prjd_qty_pao, " _
                            & "  prjd_qty - coalesce(prjd_qty_pao,0) as prjd_qty_sisa, " _
                            & "  prjd_qty_mo " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = prjd_um " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  where prjd_prj_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString()) + "" _
                            & "  and upper(prjd_trans_id) <> 'X' " _
                            & "  order by prjd_seq asc "
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
                    _dtrow("status") = ds_bantu.Tables(0).Rows(i).Item("status")
                    _dtrow("paod_oid") = Guid.NewGuid.ToString
                    _dtrow("paod_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("prjd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_loc_id")
                    _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                    _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                    _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                    _dtrow("prj_code") = ds_bantu.Tables(0).Rows(i).Item("prj_code")
                    _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("prjd_qty_sisa")
                    _dtrow("paod_qty") = ds_bantu.Tables(0).Rows(i).Item("prjd_qty_pao")
                    _dtrow("paod_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                    _dtrow("unit_measure") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                    _dtrow("paod_eta_target") = Today()
                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                    If ds_bantu.Tables(0).Rows(i).Item("prjd_qty_pao") > 0 Then
                        _dtrow("status") = True
                    End If
                Next
                fobject.ds_edit.Tables(0).AcceptChanges()
                fobject.gv_edit.BestFitColumns()
            Else
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "SELECT  " _
                                & "  false as status, " _
                                & "  prjd_oid, " _
                                & "  prjd_dom_id, " _
                                & "  prjd_en_id, " _
                                & "  prjd_add_by, " _
                                & "  prjd_add_date, " _
                                & "  prjd_upd_by, " _
                                & "  prjd_upd_date, " _
                                & "  prjd_dt, " _
                                & "  prjd_prj_oid, " _
                                & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                                & "  prjd_seq, " _
                                & "  prjd_si_id, " _
                                & "  prjd_pt_id,pt_code, " _
                                & "  prjd_pt_desc1, " _
                                & "  prjd_pt_desc2, " _
                                & "  prjd_loc_id,loc_desc, " _
                                & "  prjd_qty, " _
                                & "  prjd_qty_full, " _
                                & "  prjd_um,um.code_name as unit_measure, " _
                                & "  prjd_cost, " _
                                & "  prjd_price, " _
                                & "  prjd_disc, " _
                                & "  prjd_um_conv, " _
                                & "  prjd_qty_real, " _
                                & "  prjd_taxable, " _
                                & "  prjd_tax_inc, " _
                                & "  prjd_tax_class, " _
                                & "  prjd_trans_id, " _
                                & "  coalesce(prjd_qty_pao,0) as prjd_qty_pao, " _
                                & "  prjd_qty - coalesce(prjd_qty_pao,0) as prjd_qty_sisa, " _
                                & "  prjd_qty_mo " _
                                & "FROM  " _
                                & "  public.prjd_det " _
                                & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                                & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                                & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                                & "  inner join code_mstr um on um.code_id = prjd_um " _
                                & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                                & "  where prjd_prj_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString()) + "" _
                                & "  and prjd_qty - coalesce(prjd_qty_pao,0) > 0 " _
                                & "  and upper(prjd_trans_id) <> 'X' " _
                                & "  order by prjd_seq asc "
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
                    _dtrow("status") = ds_bantu.Tables(0).Rows(i).Item("status")
                    _dtrow("paod_oid") = Guid.NewGuid.ToString
                    _dtrow("paod_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("prjd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_loc_id")
                    _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                    _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                    _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                    _dtrow("prj_code") = ds_bantu.Tables(0).Rows(i).Item("prj_code")
                    _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("prjd_qty_sisa")
                    _dtrow("paod_qty") = 0
                    _dtrow("paod_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                    _dtrow("unit_measure") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                    _dtrow("paod_eta_target") = Today()
                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                    If ds_bantu.Tables(0).Rows(i).Item("prjd_qty_pao") > 0 Then
                        _dtrow("status") = True
                    End If
                Next
                fobject.ds_edit.Tables(0).AcceptChanges()
                fobject.gv_edit.BestFitColumns()
            End If
           

        ElseIf fobject.name = "FProjectGroup" Then
            fobject.prjg_prj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
            'rev by hendrik 2011-06-03
        ElseIf fobject.Name = "FProjectShipment" Or fobject.Name = "FProjectShipments_so" Then
            fobject.soship_prj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id,si_desc, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id,loc_desc, " _
                            & "  pt_ls,pt_type, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_qty - coalesce(prjd_qty_shipment,0) + coalesce(prjd_qty_return,0) as qty_open, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class, " _
                            & "  prjd_trans_id, " _
                            & "  prjd_qty_pao, " _
                            & "  prjd_qty - coalesce(prjd_qty_shipment,0) + coalesce(prjd_qty_return,0) as prjd_qty_sisa, " _
                            & "  prjd_qty_mo, " _
                            & "  prjd_qty_shipment, " _
                            & "  prjd_qty_inv, " _
                            & "  prjd_progress_pay, " _
                            & "  1 - coalesce(prjd_progress_pay,0) as progress_pay_open, " _
                            & "  prjd_progress_pay_inv " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = prjd_um " _
                            & "  inner join si_mstr on si_id = prjd_si_id " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  where prjd_prj_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString()) + "" _
                            & "  and coalesce(prjd_progress_pay,0) < 1 " _
                            & "  and upper(prjd_trans_id) <> 'X' " _
                            & "  order by prjd_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "prjd_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()
            Dim _dtrow As DataRow

            If fobject.Name = "FProjectShipment" Then
                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    _dtrow = fobject.ds_edit.Tables(0).NewRow
                    _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                    _dtrow("soshipd_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                    _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_id")
                    _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                    _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                    _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                    _dtrow("prjd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost")
                    _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("soshipd_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("progress_pay_open") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                    _dtrow("soshipd_progress_pay") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                    _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                    _dtrow("unit_measure") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                    _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv")
                    _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv"))
                    _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_si_id")
                    _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                    _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_loc_id")
                    _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                    _dtrow("soshipd_lot_serial") = "N"
                    _dtrow("soshipd_close_line") = "Y"
                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                Next
            ElseIf fobject.Name = "FProjectShipments_so" Then
                For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    _dtrow = fobject.ds_edit.Tables(0).NewRow
                    _dtrow("soshipd_oid") = Guid.NewGuid.ToString
                    _dtrow("soshipd_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                    _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_id")
                    _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                    _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                    _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                    _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                    _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                    _dtrow("prjd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost")
                    _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("soshipd_qty") = 0 'ds_bantu.Tables(0).Rows(i).Item("qty_open")
                    _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                    _dtrow("soshipd_um_name") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                    _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv")
                    _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv"))
                    _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_si_id")
                    _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                    _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_loc_id")
                    _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                    _dtrow("soshipd_lot_serial") = "N"
                    fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
                Next
            End If

            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FBillOfQuantity.Name Then
            fobject.boq_sopj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject.boq_sopj_oid.tag = ds.Tables(0).Rows(_row_gv).Item("prj_oid")

            'Dim ssql As String
            'ssql = "SELECT  " _
            '    & "  a.prjd_pt_id, " _
            '    & "  b.pt_code, " _
            '    & "  b.pt_desc1, " _
            '    & "  b.pt_desc2, " _
            '    & "  a.prjd_um, " _
            '    & "  a.prjd_qty as boqd_qty_plan, " _
            '    & "  c.code_code,a.prjd_oid " _
            '    & "FROM " _
            '    & "  public.prjd_det a " _
            '    & "  INNER JOIN public.pt_mstr b ON (a.prjd_pt_id = b.pt_id) " _
            '    & "  INNER JOIN public.code_mstr c ON (a.prjd_um = c.code_id) " _
            '    & "WHERE " _
            '    & "  a.prjd_prj_oid = '" & ds.Tables(0).Rows(_row_gv).Item("prj_oid") & "' " _
            '    & " ORDER by prjd_seq "

            'Dim dt As New DataTable
            'dt = master_new.PGSqlConn.GetTableData(ssql)

            'fobject.gc_edit.datasource = dt
            'fobject.gv_edit.BestFitColumns()

            'ssql = ""
            'For x As Integer = 0 To dt.Rows.Count - 1
            '    With dt.Rows(x)

            '        ssql += "SELECT  psd_pt_bom_id as pt_id,psd_comp as pt_code,   " _
            '           & "psd_desc as pt_desc1,psd_ref as pt_desc2, psdgroupdesc as code_code,psd_group as pt_um, " _
            '           & "psd_qty as boqs_qty_plan,cast('N' as CHAR) as boqs_is_manual " _
            '           & " from public.get_all_simulated('" & .Item("pt_code") & "',  " _
            '           & SetNumber(.Item("boqd_qty_plan")) & ", " & "'Y'" & "," _
            '           & SetNumber(ds.Tables(0).Rows(_row_gv).Item("prj_en_id")) & ",'Y', CURRENT_DATE) "

            '        If x <> dt.Rows.Count - 1 Then
            '            ssql += " UNION "
            '        End If

            '    End With
            'Next

            'ssql = "SELECT pt_id,pt_code,pt_desc1,pt_desc2,code_code,boqs_is_manual,sum(boqs_qty_plan) as boqs_qty_plan from (" & ssql _
            '    & ") as temp group by pt_id,pt_code,pt_desc1,pt_desc2,code_code,boqs_is_manual"

            'dt = master_new.PGSqlConn.GetTableData(ssql)

            'fobject.gc_stand_edit.datasource = dt
            'fobject.gv_stand_edit.BestFitColumns()

            'fobject.gc_stand_edit.EmbeddedNavigator.Buttons.Append.visible = False
            'fobject.gc_stand_edit.EmbeddedNavigator.Buttons.Remove.visible = False
            'fobject.gv_stand_edit.OptionsBehavior.Editable = False
        ElseIf fobject.name = FBoQtoPR.Name Then

            fobject.par_project.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject.par_project.tag = ds.Tables(0).Rows(_row_gv).Item("pjc_id")
            fobject._boq_oid = ds.Tables(0).Rows(_row_gv).Item("boq_oid")

            Dim sSql As String

            sSql = "SELECT  " _
                & "  false as status,a.boqs_oid, " _
                & "  a.boqs_boq_oid, " _
                & "  a.boqs_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  b.pt_um, " _
                & "  c.code_code, " _
                & "  a.boqs_qty_plan, " _
                & "  coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0) as qty_open , coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0) as qty_generate, " _
                & "  a.boqs_qty_pr, " _
                & "  a.boqs_qty_po, " _
                & "  a.boqs_qty_receipt, " _
                & "  a.boqs_qty_wo, " _
                & "  a.boqs_is_manual " _
                & "FROM " _
                & "  public.boqs_stand a " _
                & "  INNER JOIN public.boq_mstr d ON (a.boqs_boq_oid = d.boq_oid) " _
                & "  INNER JOIN public.pt_mstr b ON (a.boqs_pt_id = b.pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                & "WHERE " _
                & "  a.boqs_boq_oid ='" & ds.Tables(0).Rows(_row_gv).Item("boq_oid") & "' " _
                & " and d.boq_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " and coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0)>0 " _
                & "ORDER BY " _
                & "  a.boqs_seq"

            fobject.gc_outstanding.datasource = master_new.PGSqlConn.GetTableData(sSql)
            fobject.gv_outstanding.BestFitColumns()


            sSql = "SELECT  " _
                & "  public.en_mstr.en_desc, " _
                & "  public.req_mstr.req_oid, " _
                & "  public.req_mstr.req_dom_id, " _
                & "  public.req_mstr.req_en_id, " _
                & "  public.req_mstr.req_upd_date, " _
                & "  public.req_mstr.req_upd_by, " _
                & "  public.req_mstr.req_add_date, " _
                & "  public.req_mstr.req_add_by, " _
                & "  public.req_mstr.req_code, " _
                & "  public.req_mstr.req_ptnr_id, " _
                & "  public.req_mstr.req_cmaddr_id, " _
                & "  public.req_mstr.req_date, " _
                & "  public.req_mstr.req_need_date, " _
                & "  public.req_mstr.req_due_date, " _
                & "  public.req_mstr.req_requested, " _
                & "  public.req_mstr.req_end_user, " _
                & "  public.req_mstr.req_rmks, " _
                & "  public.req_mstr.req_sb_id, " _
                & "  public.req_mstr.req_cc_id, " _
                & "  public.req_mstr.req_si_id, " _
                & "  public.req_mstr.req_type, " _
                & "  public.req_mstr.req_pjc_id, " _
                & "  public.req_mstr.req_close_date, " _
                & "  public.req_mstr.req_total, " _
                & "  public.req_mstr.req_tran_id, " _
                & "  public.req_mstr.req_trans_id, " _
                & "  public.req_mstr.req_trans_rmks, " _
                & "  public.req_mstr.req_current_route, " _
                & "  public.req_mstr.req_next_route, " _
                & "  public.req_mstr.req_dt, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  cmaddr_name, " _
                & "  tran_name, " _
                & "  public.pjc_mstr.pjc_code, " _
                & "  public.si_mstr.si_desc, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.cc_mstr.cc_desc " _
                & "FROM " _
                & "  public.req_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                & " where req_pjc_id= " & ds.Tables(0).Rows(_row_gv).Item("pjc_id") _
                & " and req_en_id in (select user_en_id from tconfuserentity " _
                       & " where userid = " & master_new.ClsVar.sUserID.ToString & ") " _
                & " Order by req_code"

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSql)

            fobject.gc_master.datasource = dt
            fobject.gv_master.BestFitColumns()
            'ant 16 juli 2011
        ElseIf fobject.name = "FDRCRMemoProject" Then
            fobject.gv_edit_project.SetRowCellValue(_row, "arso_oid", Guid.NewGuid.ToString)
            fobject.gv_edit_project.SetRowCellValue(_row, "arso_prj_oid", ds.Tables(0).Rows(_row_gv).Item("prj_oid"))
            fobject.gv_edit_project.SetRowCellValue(_row, "arso_so_code", ds.Tables(0).Rows(_row_gv).Item("prj_code"))
            fobject.gv_edit_project.SetRowCellValue(_row, "arso_so_date", ds.Tables(0).Rows(_row_gv).Item("prj_ord_date"))
            fobject.gv_edit_project.BestFitColumns()
            '---------------------------------------------
            'rev by hendrik 2011-08-03--------------------------------------------------------------------
        ElseIf fobject.Name = "FProjectATP" Then
            fobject.soship_prj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id,si_desc, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id,loc_desc, " _
                            & "  pt_ls,pt_type, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_qty - coalesce(prjd_qty_atp,0) as qty_open, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prj_exc_rate, " _
                            & "  (prjd_price - (prjd_disc * prjd_price)) * prj_exc_rate as ext_price, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class, " _
                            & "  prjd_trans_id, " _
                            & "  prjd_qty_pao, " _
                            & "  prjd_qty - coalesce(prjd_qty_atp,0) as prjd_qty_sisa, " _
                            & "  prjd_qty_mo, " _
                            & "  prjd_qty_shipment, " _
                            & "  prjd_qty_inv, " _
                            & "  prjd_progress_pay, " _
                            & "  1 - coalesce(prjd_progress_pay,0) as progress_pay_open, " _
                            & "  prjd_progress_pay_inv " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = prjd_um " _
                            & "  inner join si_mstr on si_id = prjd_si_id " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  where prjd_prj_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString()) + "" _
                            & "  and prjd_qty - coalesce(prjd_qty_atp,0) <> 0 " _
                            & "  order by prjd_seq asc "
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
                _dtrow("soshipd_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_id")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                _dtrow("prjd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("soshipd_qty") = 0 'ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("progress_pay_open") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                _dtrow("soshipd_progress_pay") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                _dtrow("unit_measure") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv"))
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("soshipd_lot_serial") = "N"
                _dtrow("soshipd_close_line") = "Y"
                _dtrow("prjd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost")
                _dtrow("prjd_price") = ds_bantu.Tables(0).Rows(i).Item("prjd_price")
                _dtrow("prjd_disc") = ds_bantu.Tables(0).Rows(i).Item("prjd_disc")
                _dtrow("prj_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("prj_exc_rate")
                _dtrow("ext_price") = ds_bantu.Tables(0).Rows(i).Item("ext_price")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.Name = "FProjectBAST" Then
            fobject.soship_prj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id,si_desc, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id,loc_desc, " _
                            & "  pt_ls,pt_type, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_qty - coalesce(prjd_qty_bast,0) as qty_open, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prj_exc_rate, " _
                            & "  (prjd_price - (prjd_disc * prjd_price)) * prj_exc_rate as ext_price, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class, " _
                            & "  prjd_trans_id, " _
                            & "  prjd_qty_pao, " _
                            & "  prjd_qty - coalesce(prjd_qty_bast,0) as prjd_qty_sisa, " _
                            & "  prjd_qty_mo, " _
                            & "  prjd_qty_shipment, " _
                            & "  prjd_qty_inv, " _
                            & "  prjd_progress_pay, " _
                            & "  1 - coalesce(prjd_progress_pay,0) as progress_pay_open, " _
                            & "  prjd_progress_pay_inv " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = prjd_um " _
                            & "  inner join si_mstr on si_id = prjd_si_id " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  where prjd_prj_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString()) + "" _
                            & "  and prjd_qty - coalesce(prjd_qty_bast,0) <> 0 " _
                            & "  order by prjd_seq asc "
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
                _dtrow("soshipd_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_id")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                _dtrow("prjd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("soshipd_qty") = 0 'ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("progress_pay_open") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                _dtrow("soshipd_progress_pay") = ds_bantu.Tables(0).Rows(i).Item("progress_pay_open")
                _dtrow("soshipd_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                _dtrow("unit_measure") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                _dtrow("soshipd_um_conv") = ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv")
                _dtrow("soshipd_qty_real") = CDbl(ds_bantu.Tables(0).Rows(i).Item("qty_open")) * (ds_bantu.Tables(0).Rows(i).Item("prjd_um_conv"))
                _dtrow("soshipd_si_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("soshipd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("soshipd_lot_serial") = "N"
                _dtrow("soshipd_close_line") = "Y"
                _dtrow("prjd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost")
                _dtrow("prjd_price") = ds_bantu.Tables(0).Rows(i).Item("prjd_price")
                _dtrow("prjd_disc") = ds_bantu.Tables(0).Rows(i).Item("prjd_disc")
                _dtrow("prj_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("prj_exc_rate")
                _dtrow("ext_price") = ds_bantu.Tables(0).Rows(i).Item("ext_price")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
            '------------------------------------------------------------------------------ 
        ElseIf fobject.name = FProfitLossProjectPrint.Name Then
            fobject.le_project.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject.le_project.tag = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
        ElseIf fobject.name = "FTransferIssues" Then
            fobject._pb_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
            fobject.ptsfr_pb_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id,si_desc, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id,loc_desc, " _
                            & "  pt_ls,pt_type, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prj_exc_rate, " _
                            & "  (prjd_price - (prjd_disc * prjd_price)) * prj_exc_rate as ext_price, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class, " _
                            & "  prjd_trans_id, " _
                            & "  prjd_qty_pao, " _
                            & "  prjd_qty - coalesce(prjd_qty_transfer,0) as qty_open, " _
                            & "  prjd_qty_mo, " _
                            & "  prjd_qty_shipment, " _
                            & "  prjd_qty_inv, " _
                            & "  prjd_progress_pay, " _
                            & "  prjd_progress_pay_inv " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = prjd_um " _
                            & "  inner join si_mstr on si_id = prjd_si_id " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  where prjd_prj_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString()) + "" _
                            & "  and (prjd_qty - coalesce(prjd_qty_transfer,0)) > 0 " _
                            & "  order by prjd_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pbd_det")
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
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("ptsfrd_qty") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                _dtrow("ptsfrd_qty_receive") = 0
                _dtrow("ptsfrd_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                _dtrow("ptsfrd_um_name") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                _dtrow("ptsfrd_cost") = ds_bantu.Tables(0).Rows(i).Item("prjd_cost") 'prjd cost ini sudah ambil dari standard cost coba aja cek di form project definition
                _dtrow("ptsfrd_pbd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

            fobject.ptsfr_pb_oid.enabled = False
            fobject.ptsfr_so_oid.enabled = False
            fobject.ptsfr_so_oid.text = ""
            fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
