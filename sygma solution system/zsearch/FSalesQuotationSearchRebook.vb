﻿Imports master_new.ModFunction
Imports DevExpress.XtraEditors.Controls
Imports master_new.PGSqlConn


Public Class FSalesQuotationSearchRebook
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _obj As Object
    Public _sq_code, _ppn_type, _sq_trans_rmks As String
    Public _interval As Integer
    Public _loc_id As Integer
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Dim _conf_value As String
    Dim ds_attr As New DataSet
    Public _date As Date

    Private Sub FSalesOrderSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
        _conf_value = func_coll.get_conf_file("wf_sales_quotation")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SQ Date", "sq_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Consigment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Booking", "sq_booking", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        If fobject.name = "FSalesQuotationConsigmentAloc" Then

            get_sequel = "SELECT  " _
                        & "  sq_oid, " _
                        & "  sq_dom_id, " _
                        & "  sq_en_id, " _
                        & "  sq_add_by, " _
                        & "  sq_add_date, " _
                        & "  sq_upd_by, " _
                        & "  sq_upd_date, " _
                        & "  sq_code,sq_tax_class,sq_bk_id,sq_ppn_type,sq_credit_term, " _
                        & "  sq_ptnr_id_sold, " _
                        & "  sq_date, " _
                        & "  sq_si_id, " _
                        & "  en_desc, " _
                        & "  sq_type, " _
                        & "  sq_sales_person, " _
                        & "  sq_pi_id, " _
                        & "  sq_pay_type, " _
                        & "  sq_pay_method, " _
                        & "  sq_ar_ac_id, " _
                        & "  sq_ar_sb_id, " _
                        & "  sq_ar_cc_id, " _
                        & "  sq_dp, " _
                        & "  sq_disc_header, " _
                        & "  sq_total,sq_sales_program, " _
                        & "  sq_print_count,sq_due_date, " _
                        & "  sq_need_date, " _
                        & "  sq_cons, " _
                        & "  sq_booking, " _
                        & "  sq_ptsfr_loc_id, " _
                        & "  sq_ptsfr_loc_git, " _
                        & "  sq_ptsfr_loc_to_id, " _
                        & "  sq_close_date, " _
                        & "  sq_tran_id, " _
                        & "  sq_trans_id, " _
                        & "  sq_trans_rmks, " _
                        & "  sq_current_route, " _
                        & "  sq_next_route, " _
                        & "  sq_dt, " _
                        & "  sq_cu_id, " _
                        & "  sq_total_ppn, " _
                        & "  sq_total_pph, " _
                        & "  sq_payment, " _
                        & "  sq_exc_rate, " _
                        & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                        & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                        & "  ptnr_mstr_sold.ptnr_ac_ar_id, " _
                        & "  ptnr_mstr_sold.ptnr_oid, " _
                        & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                        & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                        & "  coalesce(ptnra_line_3,'') as ptnra_line_3, " _
                        & "  si_desc " _
                        & "FROM  " _
                        & "  public.sq_mstr " _
                        & "  inner join en_mstr on en_id = sq_en_id " _
                        & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
                        & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
                        & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
                        & "  inner join si_mstr on si_id = sq_si_id " _
                        & "  where sq_trans_id = 'D' " _
                        & "  and sq_en_id = " + _en_id.ToString _
                        & "  and sq_cons<>'Y' " _
                        & "  and sq_booking='X' " _
                        & "  and sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

            '& " AND sq_book_start_date <= " + SetDate(par_date) + " and sq_book_endate_date >= " + SetDate(par_date)

            '**********************************
            'gunakan kalo pencarian hanya meliputi booking saja
            '& "  where sq_booking='Y' and sq_cons='N' and sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '**********************************

            If _conf_value = "1" Then
                get_sequel += " and  sq_trans_id = 'I' "
                'Else
                'dinonaktifkan agar update sales person by sq
                'get_sequel += " and sq_sales_person=" & SetInteger(fobject.so_sales_person.editvalue)
            End If

        End If

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

        '*********************************************************************
        If fobject.name = "FSalesQuotationConsigmentAloc" Then
            Dim x As Integer
            'fobject.so_sq_ref_code.tag = ds.Tables(0).Rows(_row_gv).Item("sq_oid")
            fobject._so_sq_ref_oid = ds.Tables(0).Rows(_row_gv).Item("sq_oid")
            fobject.so_sq_ref_code.text = ds.Tables(0).Rows(_row_gv).Item("sq_code")
            fobject._so_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("sq_ptnr_id_sold")
            fobject.so_ptnr_id_sold.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold")
            fobject.so_sales_person.editvalue = ds.Tables(0).Rows(_row_gv).Item("sq_sales_person")
            fobject.so_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            'fobject.so_shipping_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))
            fobject.so_bantu_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))
            fobject.so_trans_rmks.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_trans_rmks")

            x = SetNumber(ds.Tables(0).Rows(_row_gv).Item("sq_ar_ac_id"))

            fobject.so_ar_ac_id.EditValue = x
            fobject.so_ar_sb_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ar_sb_id")
            fobject.so_ar_cc_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ar_cc_id")
            fobject.so_sales_program.editvalue = ds.Tables(0).Rows(_row_gv).Item("sq_sales_program")

            fobject.so_booking.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_booking")
            'fobject.so_alocated.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_alocated")
            'fobject.so_cons.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_cons")


            fobject.so_ptsfr_loc_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_id")
            fobject.so_ptsfr_loc_git.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_git")
            fobject.so_ptsfr_loc_to_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_to_id")

            'fobject.so_tax_class.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_tax_class")
            'fobject.so_bk_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_bk_id")
            'fobject.so_ppn_type.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ppn_type")

            fobject.so_cu_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_cu_id")
            fobject.so_exc_rate.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_exc_rate")
            fobject.so_type.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_type")

            'fobject.so_credit_term.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_credit_term")
            fobject.so_pay_type.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_pay_type")
            fobject.so_pay_method.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_pay_method")

            Dim dt_bantu As New DataTable
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_pi_mstr(fobject.so_en_id.EditValue, fobject.so_type.EditValue, fobject.so_cu_id.EditValue, fobject.so_date.DateTime))
            fobject.so_pi_id.Properties.DataSource = dt_bantu
            fobject.so_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
            fobject.so_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
            fobject.so_pi_id.ItemIndex = 0

            x = SetNumber(ds.Tables(0).Rows(_row_gv).Item("sq_pi_id"))

            fobject.so_pi_id.editvalue = x

            'fobject.so_need_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("sq_need_date")
            'fobject.so_due_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("sq_due_date")

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
                            & "  sqd_qty - coalesce(sqd_qty_so,0) as sqd_qty, " _
                            & "  sqd_qty_allocated, " _
                            & "  sqd_qty_picked, " _
                            & "  sqd_qty_shipment, " _
                            & "  sqd_qty_pending_inv, " _
                            & "  sqd_qty_invoice, " _
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
                            & "  sqd_pod_oid " _
                            & "FROM  " _
                            & "  public.sqd_det " _
                            & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                            & "  inner join en_mstr on en_id = sqd_en_id " _
                            & "  inner join si_mstr on si_id = sqd_si_id " _
                            & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                            & "  left outer join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                            & "  left outer join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                            & "  left outer join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                            & "  left outer join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                            & "  where (sqd_qty - coalesce(sqd_qty_shipment,0)) > 0 " _
                            & "  and sqd_sq_oid = '" + ds.Tables(0).Rows(_row_gv).Item("sq_oid") + "'"
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
                _dtrow("sod_oid") = Guid.NewGuid.ToString
                _dtrow("sod_en_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_en_id")
                _dtrow("en_desc") = ds_bantu.Tables(0).Rows(i).Item("en_desc")
                _dtrow("sod_si_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("sod_is_additional_charge") = ds_bantu.Tables(0).Rows(i).Item("sqd_is_additional_charge")
                _dtrow("sod_pt_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_pt_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("sod_rmks") = ds_bantu.Tables(0).Rows(i).Item("sqd_rmks")
                _dtrow("sod_qty") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty")
                _dtrow("sod_qty_open") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty")
                _dtrow("sod_qty_shipment") = 0
                _dtrow("sod_um") = ds_bantu.Tables(0).Rows(i).Item("sqd_um")
                _dtrow("um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("sod_loc_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("sod_cost") = ds_bantu.Tables(0).Rows(i).Item("sqd_cost")
                _dtrow("sod_price") = ds_bantu.Tables(0).Rows(i).Item("sqd_price")
                _dtrow("sod_disc") = ds_bantu.Tables(0).Rows(i).Item("sqd_disc")
                _dtrow("sod_sqd_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_oid")

                If SetString(ds_bantu.Tables(0).Rows(i).Item("sqd_sales_ac_id")) = "" Then

                    Dim ds_bantu2 As New DataSet
                    Dim pt_pl_id As String = ""
                    pt_pl_id = GetRowInfo("select pt_pl_id from pt_mstr where pt_id= " + ds_bantu.Tables(0).Rows(i).Item("sqd_pt_id").ToString)(0).ToString
                    Try
                        Using objcb As New master_new.CustomCommand
                            With objcb
                                .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                    & "From pla_mstr  " _
                                    & "inner join ac_mstr on ac_id = pla_ac_id " _
                                    & "inner join sb_mstr on sb_id = pla_sb_id " _
                                    & "inner join cc_mstr on cc_id = pla_cc_id " _
                                    & "where pla_pl_id = " + pt_pl_id _
                                    & " and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"

                                .InitializeCommand()
                                .FillDataSet(ds_bantu2, "prodline")

                                If ds_bantu2.Tables(0).Rows.Count = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        pt_pl_id) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                    Exit Sub
                                ElseIf ds_bantu2.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    pt_pl_id) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                    Exit Sub
                                ElseIf ds_bantu2.Tables(0).Rows(0).Item(0) = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        pt_pl_id) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                    Exit Sub
                                End If

                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try


                    _dtrow("sod_sales_ac_id") = ds_bantu2.Tables(0).Rows(0).Item("pla_ac_id")
                    _dtrow("ac_code_sales") = ds_bantu2.Tables(0).Rows(0).Item("ac_code")
                    _dtrow("ac_name_sales") = ds_bantu2.Tables(0).Rows(0).Item("ac_name")

                    _dtrow("sod_disc_ac_id") = ds_bantu2.Tables(0).Rows(1).Item("pla_ac_id")
                    _dtrow("ac_code_disc") = ds_bantu2.Tables(0).Rows(1).Item("ac_code")
                    _dtrow("ac_name_disc") = ds_bantu2.Tables(0).Rows(1).Item("ac_name")


                    _dtrow("sod_sales_sb_id") = ds_bantu2.Tables(0).Rows(0).Item("pla_sb_id")
                    _dtrow("sb_desc") = ds_bantu2.Tables(0).Rows(0).Item("sb_desc")
                    _dtrow("sod_sales_cc_id") = ds_bantu2.Tables(0).Rows(0).Item("pla_cc_id")
                    _dtrow("cc_desc") = ds_bantu2.Tables(0).Rows(0).Item("cc_desc")

                    Dim ssql As String
                    ssql = "SELECT  distinct " _
                          & "  en_id, " _
                          & "  en_desc, " _
                          & "  si_desc, " _
                          & "  pt_id, " _
                          & "  pt_code, " _
                          & "  pt_desc1, " _
                          & "  pt_desc2, " _
                          & "  pt_cost, " _
                          & "  invct_cost, " _
                          & "  pt_price, " _
                          & "  pt_type, " _
                          & "  pt_um, " _
                          & "  pt_pl_id, " _
                          & "  pt_ls, " _
                          & "  pt_loc_id, " _
                          & "  loc_desc, " _
                          & "  pt_taxable, " _
                          & "  pt_tax_inc, " _
                          & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                          & "  tax_class_mstr.code_name as tax_class_name, " _
                          & "  pt_ppn_type, " _
                          & "  um_mstr.code_name as um_name " _
                          & "FROM  " _
                          & "  public.pt_mstr" _
                          & " inner join en_mstr on en_id = pt_en_id " _
                          & " inner join loc_mstr on loc_id = pt_loc_id " _
                          & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                          & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                          & " inner join invct_table on invct_pt_id = pt_id " _
                          & " inner join si_mstr on si_id = invct_si_id " _
                          & " where pt_id =" & SetInteger(ds_bantu.Tables(0).Rows(i).Item("sqd_pt_id").ToString) & " "

                    Dim dt_temp As New DataTable
                    dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                    If dt_temp.Rows.Count > 0 Then
                        _dtrow("sod_taxable") = dt_temp.Rows(0).Item("pt_taxable")
                        _dtrow("sod_tax_inc") = dt_temp.Rows(0).Item("pt_tax_inc")
                        _dtrow("sod_tax_class") = dt_temp.Rows(0).Item("pt_tax_class")
                        _dtrow("sod_tax_class_name") = dt_temp.Rows(0).Item("tax_class_name")
                        _dtrow("sod_ppn_type") = dt_temp.Rows(0).Item("pt_ppn_type")
                    End If


                Else
                    _dtrow("sod_sales_ac_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_ac_id")
                    _dtrow("ac_code_sales") = ds_bantu.Tables(0).Rows(i).Item("ac_code_sales")
                    _dtrow("ac_name_sales") = ds_bantu.Tables(0).Rows(i).Item("ac_name_sales")

                    _dtrow("sod_disc_ac_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_disc_ac_id")
                    _dtrow("ac_code_disc") = ds_bantu.Tables(0).Rows(i).Item("ac_code_disc")
                    _dtrow("ac_name_disc") = ds_bantu.Tables(0).Rows(i).Item("ac_name_disc")


                    _dtrow("sod_sales_sb_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_sb_id")
                    _dtrow("sb_desc") = ds_bantu.Tables(0).Rows(i).Item("sb_desc")
                    _dtrow("sod_sales_cc_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_cc_id")
                    _dtrow("cc_desc") = ds_bantu.Tables(0).Rows(i).Item("cc_desc")

                    _dtrow("sod_taxable") = ds_bantu.Tables(0).Rows(i).Item("sqd_taxable")
                    _dtrow("sod_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_inc")
                    _dtrow("sod_tax_class") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_class")
                    _dtrow("sod_tax_class_name") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_class_name")
                    _dtrow("sod_ppn_type") = ds_bantu.Tables(0).Rows(i).Item("sqd_ppn_type")
                End If

                _dtrow("sod_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sqd_um_conv")
                _dtrow("sod_qty_real") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_real")

                '_dtrow("sod_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_inc")
                _dtrow("sod_dp") = ds_bantu.Tables(0).Rows(i).Item("sqd_dp")
                _dtrow("sod_payment") = ds_bantu.Tables(0).Rows(i).Item("sqd_payment")
                _dtrow("sod_sales_unit") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_unit")
                _dtrow("sod_pod_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_pod_oid")
                _dtrow("sod_sqd_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_oid")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()


            If _conf_value = "1" Then
                Try
                    Using objcb As New master_new.CustomCommand
                        With objcb
                            .SQL = "SELECT  " _
                                & "  ptnratt_oid, " _
                                & "  ptnratt_kjb_code, " _
                                & "  ptnratt_bekerja_pada, " _
                                & "  ptnratt_jabatan_bagian, " _
                                & "  ptnratt_kantor_alamat_1, " _
                                & "  ptnratt_kantor_alamat_2, " _
                                & "  ptnratt_kantor_lantai, " _
                                & "  ptnratt_kantor_telp, " _
                                & "  ptnratt_ktp, " _
                                & "  ptnratt_email, " _
                                & "  ptnratt_rumah_alamat_1, " _
                                & "  ptnratt_rumah_alamat_2, " _
                                & "  ptnratt_rumah_kode_pos, " _
                                & "  ptnratt_rumah_telp, " _
                                & "  ptnratt_rumah_hp, " _
                                & "  ptnratt_status_alamat_kirim, " _
                                & "  ptnratt_status_alamat_tagih, " _
                                & "  ptnratt_suami_nama, " _
                                & "  ptnratt_suami_bekerja, " _
                                & "  ptnratt_suami_jabatan, " _
                                & "  ptnratt_suami_kantor_alamat_1, " _
                                & "  ptnratt_suami_kantor_alamat_2, " _
                                & "  ptnratt_suami_telp, " _
                                & "  ptnratt_suami_hp, " _
                                & "  ptnratt_anak_nama_1, " _
                                & "  ptnratt_anak_tgl_lahir_1, " _
                                & "  ptnratt_anak_sekolah_1, " _
                                & "  ptnratt_anak_nama_2, " _
                                & "  ptnratt_anak_tgl_lahir_2, " _
                                & "  ptnratt_anak_sekolah_2, " _
                                & "  ptnratt_anak_nama_3, " _
                                & "  ptnratt_anak_tgl_lahir_3, " _
                                & "  ptnratt_anak_sekolah_3, " _
                                & "  ptnratt_keluarga_dekat_nama, " _
                                & "  ptnratt_keluarga_dekat_alamat_1, " _
                                & "  ptnratt_keluarga_dekat_alamat_2, " _
                                & "  ptnratt_keluarga_dekat_telp, " _
                                & "  ptnratt_keluarga_dekat_hp, " _
                                & "  ptnratt_status_tempat_tinggal, " _
                                & "  ptnratt_jenis_kartu_kredit, " _
                                & "  ptnratt_no_kartu_kredit, " _
                                & "  ptnratt_berlaku_sd, " _
                                & "  ptnratt_dt, " _
                                & "  ptnratt_bank, " _
                                & "  ptnratt_status_rumah_id, " _
                                & "  ptnratt_lama_tinggal_id, " _
                                & "  ptnratt_masa_kerja_id, " _
                                & "  ptnratt_income_id, " _
                                & "  0 as ptnratt_kepribadian_id, " _
                                & "  0 as ptnratt_jaminan_id, " _
                                & "  ptnratt_tanggungan_id, " _
                                & "  sr.code_name as status_rumah, " _
                                & "  lt.code_name as lama_tinggal, " _
                                & "  ms.code_name as masa_kerja, " _
                                & "  ic.code_name as income, " _
                                & "  tg.code_name as tanggungan, " _
                                & "  kp.code_name as kepribadian, " _
                                & "  ja.code_name as jaminan, " _
                                & "  ((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_status_rumah_id)) + " _
                                & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_lama_tinggal_id))+ " _
                                & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_masa_kerja_id))+ " _
                                & "  coalesce((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_kepribadian_id)),0)+ " _
                                & "  coalesce((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_jaminan_id)),0)+ " _
                                & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_income_id))- " _
                                & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_tanggungan_id)))as total_point " _
                                & "FROM  " _
                                & "  public.ptnr_mstr " _
                                & "  INNER JOIN public.en_mstr ON (ptnr_en_id = en_id)" _
                                & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                                & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                                & "  LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_oid " _
                                & "  LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                                & "  LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                                & "  LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                                & "  LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                                & "  LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                                & "  LEFT OUTER JOIN public.code_mstr kp on tg.code_id = ptnratt_kepribadian_id " _
                                & "  LEFT OUTER JOIN public.code_mstr ja on tg.code_id = ptnratt_jaminan_id " _
                                & " where ptnratt_ptnr_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("ptnr_oid")) + ""
                            .InitializeCommand()
                            .FillDataSet(ds_attr, "ptnratt_det")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                fobject.soa_kjb_code.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_kjb_code"))
                fobject.soa_bekerja_pada.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_bekerja_pada"))
                fobject.soa_jabatan_bagian.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_jabatan_bagian"))
                fobject.soa_kantor_alamat_1.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_kantor_alamat_1"))
                fobject.soa_kantor_alamat_2.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_kantor_alamat_2"))
                fobject.soa_kantor_lantai.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_kantor_lantai"))
                fobject.soa_kantor_telp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_kantor_telp"))
                fobject.soa_ktp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_ktp"))
                fobject.soa_email.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_email"))
                fobject.soa_rumah_alamat_1.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_rumah_alamat_1"))
                fobject.soa_rumah_alamat_2.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_rumah_alamat_2"))
                fobject.soa_rumah_kode_pos.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_rumah_kode_pos"))
                fobject.soa_rumah_telp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_rumah_telp"))
                fobject.soa_rumah_hp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_rumah_hp"))
                fobject.soa_status_alamat_kirim.EditValue = SetBitYNB(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_status_alamat_kirim"))
                fobject.soa_status_alamat_tagih.EditValue = SetBitYNB(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_status_alamat_tagih"))
                fobject.soa_suami_nama.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_suami_nama"))
                fobject.soa_suami_bekerja.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_suami_bekerja"))
                fobject.soa_suami_jabatan.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_suami_jabatan"))
                fobject.soa_suami_kantor_alamat_1.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_suami_kantor_alamat_1"))
                fobject.soa_suami_kantor_alamat_2.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_suami_kantor_alamat_2"))
                fobject.soa_suami_telp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_suami_telp"))
                fobject.soa_suami_hp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_suami_hp"))
                fobject.soa_anak_nama_1.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_nama_1"))
                fobject.soa_anak_tgl_lahir_1.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_tgl_lahir_1")
                fobject.soa_anak_sekolah_1.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_sekolah_1"))
                fobject.soa_anak_nama_2.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_nama_2"))
                fobject.soa_anak_tgl_lahir_2.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_tgl_lahir_2")
                fobject.soa_anak_sekolah_2.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_sekolah_2"))
                fobject.soa_anak_nama_3.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_nama_3"))
                fobject.soa_anak_tgl_lahir_3.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_tgl_lahir_3")
                fobject.soa_anak_sekolah_3.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_anak_sekolah_3"))
                fobject.soa_keluarga_dekat_nama.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_keluarga_dekat_nama"))
                fobject.soa_keluarga_dekat_alamat_1.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_keluarga_dekat_alamat_1"))
                fobject.soa_keluarga_dekat_alamat_2.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_keluarga_dekat_alamat_2"))
                fobject.soa_keluarga_dekat_telp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_keluarga_dekat_telp"))
                fobject.soa_keluarga_dekat_hp.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_keluarga_dekat_hp"))
                fobject.soa_status_tempat_tinggal.EditValue = SetBitYNB(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_status_tempat_tinggal"))
                fobject.soa_jenis_kartu_kredit.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_jenis_kartu_kredit"))
                fobject.soa_no_kartu_kredit.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_no_kartu_kredit"))
                fobject.soa_berlaku_sd.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_berlaku_sd")
                fobject.soa_bank.Text = SetString(ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_bank"))
                'fobject.soa_status_rumah_id.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_status_rumah_id")
                'fobject.soa_status_rumah_id.text = SetSetring(ds_attr.Tables("ptnratt_det").Rows(0).Item("status_rumah"))
                'fobject.soa_lama_tinggal_id.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_lama_tinggal_id")
                'fobject.soa_lama_tinggal_id.text = SetSetring(ds_attr.Tables("ptnratt_det").Rows(0).Item("lama_tinggal"))
                'fobject.soa_masa_kerja_id.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_masa_kerja_id")
                'fobject.soa_masa_kerja_id.text = SetSetring(ds_attr.Tables("ptnratt_det").Rows(0).Item("masa_kerja"))
                'fobject.soa_income_id.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_income_id")
                'fobject.soa_income_id.text = SetSetring(ds_attr.Tables("ptnratt_det").Rows(0).Item("income"))
                'fobject.soa_kepribadian_id.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_kepribadian_id")
                'fobject.soa_kepribadian_id.text = SetSetring(ds_attr.Tables("ptnratt_det").Rows(0).Item("kepribadian"))
                'fobject.soa_tanggungan_id.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_tanggungan_id")
                'fobject.soa_tanggungan_id.text = SetSetring(ds_attr.Tables("ptnratt_det").Rows(0).Item("tanggungan"))
                Try
                    'fobject.soa_jaminan_id.editvalue = ds_attr.Tables("ptnratt_det").Rows(0).Item("ptnratt_jaminan_id")
                    'fobject.soa_jaminan_id.text = SetSetring(ds_attr.Tables("ptnratt_det").Rows(0).Item("jaminan"))
                Catch ex As Exception

                End Try

                'fobject.sv_total_point.text = SetInteger(ds_attr.Tables("ptnratt_det").Rows(0).Item("total_point"))
                'fobject.soa_status_rumah_id.ClosePopup()
                'fobject.soa_lama_tinggal_id.ClosePopup()
                'fobject.soa_masa_kerja_id.ClosePopup()
                'fobject.soa_income_id.ClosePopup()
                'fobject.soa_kepribadian_id.ClosePopup()
                'fobject.soa_tanggungan_id.ClosePopup()
                'fobject.soa_jaminan_id.ClosePopup()
            End If

            '*******************************************************************************************
        End If

    End Sub
End Class
