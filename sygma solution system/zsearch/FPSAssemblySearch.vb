Imports master_new.ModFunction
Imports DevExpress.XtraEditors.Controls
Imports master_new.PGSqlConn


Public Class FPSAssemblySearch
    Public _row As Integer
    Public _en_id, _si_id As Integer
    Public _ptnr_id, _sq_ptnr_id_sold As Integer
    Public _cu_id As Integer
    Public _obj, _sq_pi_id, _sq_pidd_area_id As Object
    Public _sq_code, _ppn_type, _sq_trans_rmks As String
    Public _sq_ptnr_id_sold_mstr
    Public _interval As Integer
    Public _loc_id As Integer
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Dim _conf_value As String
    Dim ds_attr As New DataSet
    Public _date As Date

    Private Sub FPSAssemblySearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
        _conf_value = func_coll.get_conf_file("wf_sales_quotation")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Code", "ps_par", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partnumber Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partnumber Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT   " _
        & "   en_desc,  " _
        & "   ps_oid,  " _
        & "   ps_dom_id,  " _
        & "   ps_en_id,  " _
        & "   ps_add_by,  " _
        & "   ps_add_date,  " _
        & "   ps_upd_by,  " _
        & "   ps_upd_date,  " _
        & "   ps_par,  " _
        & "   ps_id,  " _
        & "   ps_desc,  " _
        & "   ps_use_bom, " _
        & "   ps_pt_bom_id,  " _
        & "   ps_active, " _
        & "   pt_desc1, " _
        & "   pt_desc2 " _
        & " FROM  " _
        & "   public.ps_mstr  " _
        & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) " _
        & "  INNER JOIN pt_mstr on pt_id = ps_pt_bom_id"

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

        If fobject.name = "FSalesQuotationConsigmentAlocated" Or fobject.name = "FSalesOrderAlocated" Then
            Dim x As Integer
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_desc")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_id")
            
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT DISTINCT  " _
                                & "  public.psd_det.psd_seq, " _
                                & "  public.psd_det.psd_en_id, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.psd_det.psd_pt_bom_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.psd_det.psd_qty, " _
                                & "  public.pt_mstr.pt_um, " _
                                & "  um_mstr.code_name as um_name, " _
                                & "  public.loc_mstr.loc_id, " _
                                & "  public.loc_mstr.loc_desc, " _
                                & "  public.invct_table.invct_cost, " _
                                & "  public.pt_mstr.pt_si_id, " _
                                & "  public.si_mstr.si_desc " _
                                & "FROM " _
                                & "  public.psd_det " _
                                & "  INNER JOIN public.ps_mstr ON (public.psd_det.psd_ps_oid = public.ps_mstr.ps_oid) " _
                                & "  INNER JOIN public.pt_mstr ON (public.psd_det.psd_pt_bom_id = public.pt_mstr.pt_id) " _
                                & "  INNER JOIN public.en_mstr ON (public.psd_det.psd_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.invct_table ON (public.pt_mstr.pt_id = public.invct_table.invct_pt_id) " _
                                & "  INNER JOIN public.loc_mstr ON (public.pt_mstr.pt_loc_id = public.loc_mstr.loc_id)" _
                                & "  inner join code_mstr um_mstr on um_mstr.code_id = pt_um " _
                                & "  INNER JOIN public.invc_mstr ON (public.psd_det.psd_pt_bom_id = public.invc_mstr.invc_pt_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.invct_table.invct_si_id = public.si_mstr.si_id) " _
                                & "  where ps_id = " & ds.Tables(0).Rows(_row_gv).Item("ps_id")
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "psd_det")

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("sqd_oid") = Guid.NewGuid.ToString
                _dtrow("sqd_en_id") = ds_bantu.Tables(0).Rows(i).Item("psd_en_id")
                _dtrow("en_desc") = ds_bantu.Tables(0).Rows(i).Item("en_desc")
                _dtrow("sqd_si_id") = ds_bantu.Tables(0).Rows(i).Item("pt_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("sqd_pt_id") = ds_bantu.Tables(0).Rows(i).Item("psd_pt_bom_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                '_dtrow("pt_type") = ds_bantu.Tables(0).Rows(i).Item("pt_type")
                '_dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                '_dtrow("sod_rmks") = ds_bantu.Tables(0).Rows(i).Item("sqd_rmks")

                'If ds.Tables(0).Rows(_row_gv).Item("sq_booking") = "Y" Then
                '    _dtrow("sqd_qty_booking") = ds_bantu.Tables(0).Rows(i).Item("psd_qty")

                'Else
                '    _dtrow("sod_qty_open") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_allocated")
                'End If

                '    '_dtrow("sod_qty_open") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_booking")
                _dtrow("sqd_qty") = ds_bantu.Tables(0).Rows(i).Item("psd_qty")
                _dtrow("sqd_qty_real") = ds_bantu.Tables(0).Rows(i).Item("psd_qty")
                '_dtrow("sqd_qty_allocated") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_allocated")
                '_dtrow("sqd_qty_booking") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_booking")
                '_dtrow("sqd_qty_shipment") = 0
                _dtrow("sqd_um") = ds_bantu.Tables(0).Rows(i).Item("pt_um")
                _dtrow("um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
                _dtrow("sqd_loc_id") = ds_bantu.Tables(0).Rows(i).Item("loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")
                _dtrow("sqd_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")

                '_dtrow("sod_sqd_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_oid")
                '_dtrow("sqd_invc_oid") = ds_bantu.Tables(0).Rows(i).Item("invc_oid")

                'If SetString(ds_bantu.Tables(0).Rows(i).Item("sqd_sales_ac_id")) = "" Then

                Dim ds_bantu2 As New DataSet
                Dim pt_pl_id As String = ""
                pt_pl_id = GetRowInfo("select pt_pl_id from pt_mstr where pt_id= " + ds_bantu.Tables(0).Rows(i).Item("psd_pt_bom_id").ToString)(0).ToString
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

                _dtrow("sqd_sales_ac_id") = ds_bantu2.Tables(0).Rows(0).Item("pla_ac_id")
                _dtrow("ac_code_sales") = ds_bantu2.Tables(0).Rows(0).Item("ac_code")
                _dtrow("ac_name_sales") = ds_bantu2.Tables(0).Rows(0).Item("ac_name")

                _dtrow("sqd_disc_ac_id") = ds_bantu2.Tables(0).Rows(1).Item("pla_ac_id")
                _dtrow("ac_code_disc") = ds_bantu2.Tables(0).Rows(1).Item("ac_code")
                _dtrow("ac_name_disc") = ds_bantu2.Tables(0).Rows(1).Item("ac_name")

                _dtrow("sqd_sales_sb_id") = ds_bantu2.Tables(0).Rows(0).Item("pla_sb_id")
                _dtrow("sb_desc") = ds_bantu2.Tables(0).Rows(0).Item("sb_desc")
                _dtrow("sqd_sales_cc_id") = ds_bantu2.Tables(0).Rows(0).Item("pla_cc_id")
                _dtrow("cc_desc") = ds_bantu2.Tables(0).Rows(0).Item("cc_desc")

                Dim ds_bantu3 As New DataSet
                'Dim pt_pl_id As String = ""
                'pt_pl_id = GetRowInfo("select pt_pl_id from pt_mstr where pt_id= " + ds_bantu.Tables(0).Rows(i).Item("psd_pt_bom_id").ToString)(0).ToString
                Try
                    Using objcb As New master_new.CustomCommand
                        With objcb
                            .SQL = "SELECT DISTINCT  " _
                                    & "  en_mstr.en_id, " _
                                    & "  en_mstr.en_desc, " _
                                    & "  si_mstr.si_desc, " _
                                    & "  public.pt_mstr.pt_id, " _
                                    & "  public.pt_mstr.pt_code, " _
                                    & "  public.pt_mstr.pt_desc1, " _
                                    & "  public.pt_mstr.pt_desc2, " _
                                    & "  public.pt_mstr.pt_cost, " _
                                    & "  invct_table.invct_cost, " _
                                    & "  public.pt_mstr.pt_price, " _
                                    & "  public.pt_mstr.pt_type, " _
                                    & "  public.pt_mstr.pt_um, " _
                                    & "  public.pt_mstr.pt_pl_id, " _
                                    & "  public.pt_mstr.pt_ls, " _
                                    & "  public.pt_mstr.pt_loc_id, " _
                                    & "  loc_mstr.loc_desc, " _
                                    & "  public.pt_mstr.pt_taxable, " _
                                    & "  public.pt_mstr.pt_tax_inc, " _
                                    & "  public.pt_mstr.pt_tax_class, " _
                                    & "  coalesce(pt_approval_status, 'A') AS pt_approval_status, " _
                                    & "  tax_class_mstr.code_name AS tax_class_name, " _
                                    & "  um_mstr.code_name AS um_name, " _
                                    & "  public.pt_mstr.pt_ppn_type, " _
                                    & "  public.pid_det.pid_pt_id, " _
                                    & "  public.pidd_det.pidd_payment_type, " _
                                    & "  public.pidd_det.pidd_area_id, " _
                                    & "  public.pidd_det.pidd_price, " _
                                    & "  public.pidd_det.pidd_disc, " _
                                    & "  public.pidd_det.pidd_dp, " _
                                    & "  public.pidd_det.pidd_payment, " _
                                    & "  public.pidd_det.pidd_sales_unit, " _
                                    & "  public.pidd_det.pidd_commision, " _
                                    & "  public.invc_mstr.invc_oid " _
                                    & "FROM " _
                                    & "  public.pt_mstr " _
                                    & "  INNER JOIN en_mstr ON (public.pt_mstr.pt_en_id = en_mstr.en_id) " _
                                    & "  INNER JOIN loc_mstr ON (public.pt_mstr.pt_loc_id = loc_mstr.loc_id) " _
                                    & "  INNER JOIN code_mstr um_mstr ON (public.pt_mstr.pt_um = um_mstr.code_id) " _
                                    & "  LEFT OUTER JOIN code_mstr tax_class_mstr ON (tax_class_mstr.code_id = public.pt_mstr.pt_tax_class) " _
                                    & "  INNER JOIN invct_table ON (public.pt_mstr.pt_id = invct_table.invct_pt_id) " _
                                    & "  INNER JOIN invc_mstr ON (public.pt_mstr.pt_id = invc_mstr.invc_pt_id) " _
                                    & "  INNER JOIN si_mstr ON (invct_table.invct_si_id = si_mstr.si_id) " _
                                    & "  INNER JOIN public.pid_det ON (public.pt_mstr.pt_id = public.pid_det.pid_pt_id) " _
                                    & "  INNER JOIN public.pidd_det ON (public.pid_det.pid_oid = public.pidd_det.pidd_pid_oid) " _
                                    & "  INNER JOIN public.pi_mstr ON (public.pid_det.pid_pi_oid = public.pi_mstr.pi_oid) " _
                                    & " where pt_id =" & SetInteger(ds_bantu.Tables(0).Rows(i).Item("psd_pt_bom_id").ToString) & " " _
                                    & " and loc_id =" & SetInteger(ds_bantu.Tables(0).Rows(i).Item("loc_id").ToString) & " " _
                                    & " and pi_id = " + _sq_pi_id.ToString _
                                    & " and pidd_area_id = " + _sq_pidd_area_id.ToString

                            .InitializeCommand()
                            .FillDataSet(ds_bantu3, "[pricelist")

                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                _dtrow("sqd_price") = ds_bantu3.Tables(0).Rows(0).Item("pidd_price")
                _dtrow("sqd_disc") = ds_bantu3.Tables(0).Rows(0).Item("pidd_disc")
                _dtrow("sqd_dp") = ds_bantu3.Tables(0).Rows(0).Item("pidd_dp")
                _dtrow("sqd_payment") = ds_bantu3.Tables(0).Rows(0).Item("pidd_payment")
                _dtrow("sqd_sales_unit") = ds_bantu3.Tables(0).Rows(0).Item("pidd_sales_unit")
                '_dtrow("sqd_commision_total") = ds_bantu3.Tables(0).Rows(0).Item("pidd_commision")
                '_dtrow("sqd_commision") = ds_bantu3.Tables(0).Rows(0).Item("pidd_commision")
                _dtrow("sqd_invc_oid") = ds_bantu3.Tables(0).Rows(0).Item("invc_oid")
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
                      & " where pt_id =" & SetInteger(ds_bantu.Tables(0).Rows(i).Item("psd_pt_bom_id").ToString) & " "

                Dim dt_temp As New DataTable
                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    _dtrow("sqd_taxable") = dt_temp.Rows(0).Item("pt_taxable")
                    _dtrow("sqd_tax_inc") = dt_temp.Rows(0).Item("pt_tax_inc")
                    _dtrow("sqd_tax_class") = dt_temp.Rows(0).Item("pt_tax_class")
                    _dtrow("sqd_tax_class_name") = dt_temp.Rows(0).Item("tax_class_name")
                    _dtrow("sqd_ppn_type") = dt_temp.Rows(0).Item("pt_ppn_type")
                End If



                'Else
                '    _dtrow("sod_sales_ac_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_ac_id")
                '    _dtrow("ac_code_sales") = ds_bantu.Tables(0).Rows(i).Item("ac_code_sales")
                '    _dtrow("ac_name_sales") = ds_bantu.Tables(0).Rows(i).Item("ac_name_sales")

                '    _dtrow("sod_disc_ac_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_disc_ac_id")
                '    _dtrow("ac_code_disc") = ds_bantu.Tables(0).Rows(i).Item("ac_code_disc")
                '    _dtrow("ac_name_disc") = ds_bantu.Tables(0).Rows(i).Item("ac_name_disc")


                '    _dtrow("sod_sales_sb_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_sb_id")
                '    _dtrow("sb_desc") = ds_bantu.Tables(0).Rows(i).Item("sb_desc")
                '    _dtrow("sod_sales_cc_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_cc_id")
                '    _dtrow("cc_desc") = ds_bantu.Tables(0).Rows(i).Item("cc_desc")

                '    _dtrow("sod_taxable") = ds_bantu.Tables(0).Rows(i).Item("sqd_taxable")
                '    _dtrow("sod_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_inc")
                '    _dtrow("sod_tax_class") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_class")
                '    _dtrow("sod_tax_class_name") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_class_name")
                '    _dtrow("sod_ppn_type") = ds_bantu.Tables(0).Rows(i).Item("sqd_ppn_type")
                'End If

                '    _dtrow("sod_um_conv") = ds_bantu.Tables(0).Rows(i).Item("sqd_um_conv")
                '    _dtrow("sod_qty_real") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_real")

                '    '_dtrow("sod_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sqd_tax_inc")

                '_dtrow("sqd_disc") = ds_bantu.Tables(0).Rows(i).Item("sqd_dp")
                '_dtrow("sqd_dp") = ds_bantu.Tables(0).Rows(i).Item("sqd_dp")
                '_dtrow("sqd_payment") = ds_bantu.Tables(0).Rows(i).Item("sqd_payment")
                '_dtrow("sqd_sales_unit") = ds_bantu.Tables(0).Rows(i).Item("sqd_sales_unit")
                '_dtrow("sqd_pod_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_pod_oid")
                '_dtrow("sqd_sqd_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_oid")
                '    _dtrow("sod_invc_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_invc_oid")
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()


            'ElseIf fobject.name = FTransferIssues.Name Then
            '    _obj.text = ds.Tables(0).Rows(_row_gv).Item("sq_code")
            '    _obj.tag = ds.Tables(0).Rows(_row_gv).Item("sq_oid")

            '    fobject.ptsfr_remarks.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold"))
            '    'fobject.ptsfr_booking.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_booking")

            '    If ds.Tables(0).Rows(_row_gv).Item("sq_booking") = "Y" Then
            '        fobject.ptsfr_booking.Checked = True
            '    End If

            '    If ds.Tables(0).Rows(_row_gv).Item("sq_cons") = "Y" Then
            '        fobject.ptsfr_cons.Checked = True
            '    End If

            '    'fobject.ptsfr_loc_id.editvalue = SetString(ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_to_id"))
            '    'fobject.ptsfr_loc_to_id.editvalue = SetString(ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_to_id"))

            '    'fobject.ptsfr_loc_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_id")
            '    'fobject.ptsfr_loc_git.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_git")
            '    'fobject.ptsfr_loc_to_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_id")

            '    If ds.Tables(0).Rows(_row_gv).Item("sq_cons") = "Y" Then
            '        'fobject.ptsfr_loc_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_id")
            '        'fobject.ptsfr_loc_git.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_git")
            '        'fobject.ptsfr_loc_to_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("sq_ptsfr_loc_to_id")
            '    End If

            '    Try
            '        Using objcb As New master_new.CustomCommand
            '            With objcb
            '                .SQL = "SELECT  " _
            '                    & "  sqd_oid, " _
            '                    & "  sqd_dom_id, " _
            '                    & "  sqd_en_id, " _
            '                    & "  sqd_add_by, " _
            '                    & "  sqd_add_date, " _
            '                    & "  sqd_upd_by, " _
            '                    & "  sqd_upd_date, " _
            '                    & "  sqd_sq_oid, " _
            '                    & "  sqd_seq, " _
            '                    & "  sqd_is_additional_charge, " _
            '                    & "  sqd_si_id, " _
            '                    & "  sqd_pt_id, " _
            '                    & "  sqd_rmks, " _
            '                    & "  sqd_qty, " _
            '                    & "  sqd_qty - coalesce(sqd_qty_transfer,0) as sqd_qty_open, " _
            '                    & "  sqd_um, " _
            '                    & "  sqd_cost, " _
            '                    & "  sqd_price, " _
            '                    & "  sqd_disc, " _
            '                    & "  sqd_sales_ac_id, " _
            '                    & "  sqd_sales_sb_id, " _
            '                    & "  sqd_sales_cc_id, " _
            '                    & "  sqd_disc_ac_id, " _
            '                    & "  sqd_um_conv, " _
            '                    & "  sqd_qty_real, " _
            '                    & "  sqd_taxable, " _
            '                    & "  sqd_tax_inc, " _
            '                    & "  sqd_tax_class, " _
            '                    & "  sqd_ppn_type, " _
            '                    & "  sqd_status, " _
            '                    & "  sqd_dt, " _
            '                    & "  sqd_payment, " _
            '                    & "  sqd_dp, " _
            '                    & "  sqd_sales_unit, " _
            '                    & "  sqd_loc_id, " _
            '                    & "  sqd_serial, " _
            '                    & "  en_desc, " _
            '                    & "  si_desc, " _
            '                    & "  pt_code, " _
            '                    & "  pt_desc1, " _
            '                    & "  pt_desc2, " _
            '                    & "  pt_type, " _
            '                    & "  pt_ls, " _
            '                    & "  um_mstr.code_name as um_name, " _
            '                    & "  ac_mstr_sales.ac_code as ac_code_sales, " _
            '                    & "  ac_mstr_sales.ac_name as ac_name_sales, " _
            '                    & "  sb_desc, " _
            '                    & "  cc_desc, " _
            '                    & "  ac_mstr_disc.ac_code as ac_code_disc, " _
            '                    & "  ac_mstr_disc.ac_name as ac_name_disc, " _
            '                    & "  tax_class.code_name as sqd_tax_class_name, " _
            '                    & "  loc_desc, " _
            '                    & "  sqd_pod_oid, " _
            '                    & "  sqd_invc_oid " _
            '                    & "FROM  " _
            '                    & "  public.sqd_det " _
            '                    & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
            '                    & "  inner join en_mstr on en_id = sqd_en_id " _
            '                    & "  inner join si_mstr on si_id = sqd_si_id " _
            '                    & "  inner join pt_mstr on pt_id = sqd_pt_id " _
            '                    & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
            '                    & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
            '                    & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
            '                    & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
            '                    & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
            '                    & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
            '                    & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
            '                    & "  where (sqd_qty - coalesce(sqd_qty_transfer,0)) > 0 " _
            '                    & "  and sqd_sq_oid = '" + ds.Tables(0).Rows(_row_gv).Item("sq_oid") + "'"
            '                .InitializeCommand()
            '                .FillDataSet(ds_bantu, "sqd_det")
            '            End With
            '        End Using
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try

            '    fobject.ds_edit.tables(0).clear()


            '    Dim _dtrow As DataRow
            '    For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            '        _dtrow = fobject.ds_edit.Tables(0).NewRow
            '        _dtrow("ptsfrd_oid") = Guid.NewGuid.ToString
            '        _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_pt_id")
            '        _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            '        _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
            '        _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
            '        _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
            '        _dtrow("ptsfrd_qty_open") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_open")
            '        _dtrow("ptsfrd_qty") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_open")
            '        _dtrow("ptsfrd_um") = ds_bantu.Tables(0).Rows(i).Item("sqd_um")
            '        _dtrow("ptsfrd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
            '        _dtrow("ptsfrd_cost") = ds_bantu.Tables(0).Rows(i).Item("sqd_cost")
            '        _dtrow("ptsfrd_pbd_oid") = ""
            '        _dtrow("ptsfrd_sqd_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_oid")
            '        _dtrow("ptsfrd_invc_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_invc_oid")
            '        fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            '    Next
            '    fobject.ds_edit.Tables(0).AcceptChanges()
            '    fobject.gv_edit.BestFitColumns()


            '    Dim ssql As String
            '    ssql = "select loc_id,loc_code, loc_desc, code_name from loc_mstr" _
            '            & " inner join code_mstr on code_id = loc_type " _
            '            & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
            '            & " and loc_en_id in (0," + _en_id.ToString & ") and loc_active ~~* 'y' and loc_ptnr_id=" _
            '            & ds.Tables(0).Rows(_row_gv).Item("sq_ptnr_id_sold") & " order by loc_desc"

            '    Dim dt2 As New DataTable
            '    dt2 = master_new.PGSqlConn.GetTableData(ssql)

            '    With fobject.ptsfr_loc_to_id
            '        If .Properties.Columns.VisibleCount = 0 Then
            '            .Properties.Columns.Add(New LookUpColumnInfo("loc_id", "ID", 20))
            '            .Properties.Columns.Add(New LookUpColumnInfo("loc_code", "Code", 20))
            '            .Properties.Columns.Add(New LookUpColumnInfo("loc_desc", "Description", 20))
            '        End If
            '        .Properties.DataSource = dt2
            '        .Properties.DisplayMember = dt2.Columns("loc_desc").ToString
            '        .Properties.ValueMember = dt2.Columns("loc_id").ToString
            '        If dt2.Rows.Count > 0 Then
            '            .EditValue = dt2.Rows(0).Item("loc_id")
            '        End If

            '        .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            '        .Properties.BestFit()
            '        .Properties.DropDownRows = 30
            '        .Properties.PopupWidth = 300
            '    End With

            'ElseIf fobject.name = FTransferIssuesReturn.Name Then

            '    _obj.text = ds.Tables(0).Rows(_row_gv).Item("sq_code")
            '    _obj.tag = ds.Tables(0).Rows(_row_gv).Item("sq_oid")
            '    'fobject.ptsfr_remarks.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name_sold"))

            '    Try
            '        Using objcb As New master_new.CustomCommand
            '            With objcb
            '                .SQL = "SELECT  " _
            '                    & "  sqd_oid, " _
            '                    & "  sqd_dom_id, " _
            '                    & "  sqd_en_id, " _
            '                    & "  sqd_add_by, " _
            '                    & "  sqd_add_date, " _
            '                    & "  sqd_upd_by, " _
            '                    & "  sqd_upd_date, " _
            '                    & "  sqd_sq_oid, " _
            '                    & "  sqd_seq, " _
            '                    & "  sqd_is_additional_charge, " _
            '                    & "  sqd_si_id, " _
            '                    & "  sqd_pt_id, " _
            '                    & "  sqd_rmks, " _
            '                    & "  sqd_qty, " _
            '                    & "  coalesce(sqd_qty_transfer,0)   as sqd_qty_open, " _
            '                    & "  sqd_um, " _
            '                    & "  sqd_cost, " _
            '                    & "  sqd_price, " _
            '                    & "  sqd_disc, " _
            '                    & "  sqd_sales_ac_id, " _
            '                    & "  sqd_sales_sb_id, " _
            '                    & "  sqd_sales_cc_id, " _
            '                    & "  sqd_disc_ac_id, " _
            '                    & "  sqd_um_conv, " _
            '                    & "  sqd_qty_real, " _
            '                    & "  sqd_taxable, " _
            '                    & "  sqd_tax_inc, " _
            '                    & "  sqd_tax_class, " _
            '                    & "  sqd_ppn_type, " _
            '                    & "  sqd_status, " _
            '                    & "  sqd_dt, " _
            '                    & "  sqd_payment, " _
            '                    & "  sqd_dp, " _
            '                    & "  sqd_sales_unit, " _
            '                    & "  sqd_loc_id, " _
            '                    & "  sqd_serial, " _
            '                    & "  en_desc, " _
            '                    & "  si_desc, " _
            '                    & "  pt_code, " _
            '                    & "  pt_desc1, " _
            '                    & "  pt_desc2, " _
            '                    & "  pt_type, " _
            '                    & "  pt_ls, " _
            '                    & "  um_mstr.code_name as um_name, " _
            '                    & "  ac_mstr_sales.ac_code as ac_code_sales, " _
            '                    & "  ac_mstr_sales.ac_name as ac_name_sales, " _
            '                    & "  sb_desc, " _
            '                    & "  cc_desc, " _
            '                    & "  ac_mstr_disc.ac_code as ac_code_disc, " _
            '                    & "  ac_mstr_disc.ac_name as ac_name_disc, " _
            '                    & "  tax_class.code_name as sqd_tax_class_name, " _
            '                    & "  loc_desc, " _
            '                    & "  sqd_pod_oid " _
            '                    & "FROM  " _
            '                    & "  public.sqd_det " _
            '                    & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
            '                    & "  inner join en_mstr on en_id = sqd_en_id " _
            '                    & "  inner join si_mstr on si_id = sqd_si_id " _
            '                    & "  inner join pt_mstr on pt_id = sqd_pt_id " _
            '                    & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
            '                    & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
            '                    & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
            '                    & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
            '                    & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
            '                    & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
            '                    & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
            '                    & "  where (coalesce(sqd_qty_transfer,0) ) > 0 " _
            '                    & "  and sqd_sq_oid = '" + ds.Tables(0).Rows(_row_gv).Item("sq_oid") + "'"
            '                .InitializeCommand()
            '                .FillDataSet(ds_bantu, "sqd_det")
            '            End With
            '        End Using
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try

            '    fobject.ds_edit.tables(0).clear()


            '    Dim _dtrow As DataRow
            '    For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            '        _dtrow = fobject.ds_edit.Tables(0).NewRow
            '        _dtrow("ptsfrd_oid") = Guid.NewGuid.ToString
            '        _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_pt_id")
            '        _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            '        _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
            '        _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
            '        _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
            '        _dtrow("ptsfrd_qty_open") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_open")
            '        _dtrow("ptsfrd_qty") = ds_bantu.Tables(0).Rows(i).Item("sqd_qty_open")
            '        _dtrow("ptsfrd_um") = ds_bantu.Tables(0).Rows(i).Item("sqd_um")
            '        _dtrow("ptsfrd_um_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")
            '        _dtrow("ptsfrd_cost") = ds_bantu.Tables(0).Rows(i).Item("sqd_cost")
            '        _dtrow("ptsfrd_pbd_oid") = ""
            '        _dtrow("ptsfrd_sqd_oid") = ds_bantu.Tables(0).Rows(i).Item("sqd_oid")
            '        fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            '    Next
            '    fobject.ds_edit.Tables(0).AcceptChanges()
            '    fobject.gv_edit.BestFitColumns()


            '    Dim ssql As String
            '    ssql = "select loc_id,loc_code, loc_desc, code_name from loc_mstr" _
            '            & " inner join code_mstr on code_id = loc_type " _
            '            & " where loc_dom_id = " & master_new.ClsVar.sdom_id _
            '            & " and loc_en_id in (0," + _en_id.ToString & ") and loc_active ~~* 'y' and loc_ptnr_id=" _
            '            & ds.Tables(0).Rows(_row_gv).Item("sq_ptnr_id_sold") & " order by loc_desc"

            '    Dim dt2 As New DataTable
            '    dt2 = master_new.PGSqlConn.GetTableData(ssql)

            '    With fobject.ptsfr_loc_id
            '        If .Properties.Columns.VisibleCount = 0 Then
            '            .Properties.Columns.Add(New LookUpColumnInfo("loc_id", "ID", 20))
            '            .Properties.Columns.Add(New LookUpColumnInfo("loc_code", "Code", 20))
            '            .Properties.Columns.Add(New LookUpColumnInfo("loc_desc", "Description", 20))
            '        End If
            '        .Properties.DataSource = dt2
            '        .Properties.DisplayMember = dt2.Columns("loc_desc").ToString
            '        .Properties.ValueMember = dt2.Columns("loc_id").ToString
            '        If dt2.Rows.Count > 0 Then
            '            .EditValue = dt2.Rows(0).Item("loc_id")
            '        End If

            '        .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            '        .Properties.BestFit()
            '        .Properties.DropDownRows = 30
            '        .Properties.PopupWidth = 300
            '    End With
            'ElseIf fobject.name = FCashoutPrint.Name Then
            '    _obj.text = ds.Tables(0).Rows(_row_gv).Item("sq_code")
            'ElseIf fobject.name = FInventoryRequest.Name Then

            '    _obj.text = ds.Tables(0).Rows(_row_gv).Item("sq_code")
            '    _obj.tag = ds.Tables(0).Rows(_row_gv).Item("sq_oid")

            '    Try
            '        Using objcb As New master_new.CustomCommand
            '            With objcb
            '                .SQL = "SELECT  " _
            '                    & "  sqd_oid, " _
            '                    & "  sqd_dom_id, " _
            '                    & "  sqd_en_id, " _
            '                    & "  sqd_add_by, " _
            '                    & "  sqd_add_date, " _
            '                    & "  sqd_upd_by, " _
            '                    & "  sqd_upd_date, " _
            '                    & "  sqd_sq_oid, " _
            '                    & "  sqd_seq, " _
            '                    & "  sqd_is_additional_charge, " _
            '                    & "  sqd_si_id, " _
            '                    & "  sqd_pt_id, " _
            '                    & "  sqd_rmks, " _
            '                    & "  sqd_qty, " _
            '                    & "  coalesce(sqd_qty_transfer,0)   as sqd_qty_open, " _
            '                    & "  sqd_um, " _
            '                    & "  sqd_cost, " _
            '                    & "  sqd_price, " _
            '                    & "  sqd_disc, " _
            '                    & "  sqd_sales_ac_id, " _
            '                    & "  sqd_sales_sb_id, " _
            '                    & "  sqd_sales_cc_id, " _
            '                    & "  sqd_disc_ac_id, " _
            '                    & "  sqd_um_conv, " _
            '                    & "  sqd_qty_real, " _
            '                    & "  sqd_taxable, " _
            '                    & "  sqd_tax_inc, " _
            '                    & "  sqd_tax_class, " _
            '                    & "  sqd_ppn_type, " _
            '                    & "  sqd_status, " _
            '                    & "  sqd_dt, " _
            '                    & "  sqd_payment, " _
            '                    & "  sqd_dp, " _
            '                    & "  sqd_sales_unit, " _
            '                    & "  sqd_loc_id, " _
            '                    & "  sqd_serial, " _
            '                    & "  en_desc, " _
            '                    & "  si_desc, " _
            '                    & "  pt_code, " _
            '                    & "  pt_desc1, " _
            '                    & "  pt_desc2, " _
            '                    & "  pt_type, " _
            '                    & "  pt_ls, " _
            '                    & "  um_mstr.code_name as um_name, " _
            '                    & "  ac_mstr_sales.ac_code as ac_code_sales, " _
            '                    & "  ac_mstr_sales.ac_name as ac_name_sales, " _
            '                    & "  sb_desc, " _
            '                    & "  cc_desc, " _
            '                    & "  ac_mstr_disc.ac_code as ac_code_disc, " _
            '                    & "  ac_mstr_disc.ac_name as ac_name_disc, " _
            '                    & "  tax_class.code_name as sqd_tax_class_name, " _
            '                    & "  loc_desc, " _
            '                    & "  sqd_pod_oid " _
            '                    & "FROM  " _
            '                    & "  public.sqd_det " _
            '                    & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
            '                    & "  inner join en_mstr on en_id = sqd_en_id " _
            '                    & "  inner join si_mstr on si_id = sqd_si_id " _
            '                    & "  inner join pt_mstr on pt_id = sqd_pt_id " _
            '                    & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
            '                    & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
            '                    & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
            '                    & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
            '                    & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
            '                    & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
            '                    & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
            '                    & "  where  sqd_sq_oid = '" + ds.Tables(0).Rows(_row_gv).Item("sq_oid") + "'"
            '                .InitializeCommand()
            '                .FillDataSet(ds_bantu, "sqd_det")
            '            End With
            '        End Using
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message)
            '    End Try

            '    fobject.ds_edit.tables(0).clear()


            '    Dim _dtrow As DataRow
            '    For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            '        _dtrow = fobject.ds_edit.Tables(0).NewRow
            '        _dtrow("pbd_pt_id") = ds_bantu.Tables(0).Rows(i).Item("sqd_pt_id")
            '        _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            '        _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
            '        _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
            '        _dtrow("pbd_qty") = 0
            '        _dtrow("pbd_um") = ds_bantu.Tables(0).Rows(i).Item("sqd_um")
            '        _dtrow("code_name") = ds_bantu.Tables(0).Rows(i).Item("um_name")

            '        _dtrow("pbd_en_id") = fobject.pb_en_id.editvalue
            '        _dtrow("en_desc") = fobject.pb_en_id.GetColumnValue("en_desc")
            '        _dtrow("pbd_end_user") = Trim(fobject.pb_end_user.Text)
            '        _dtrow("pbd_due_date") = fobject.pb_due_date.DateTime

            '        _dtrow("pbd_si_id") = ds.Tables(0).Rows(_row_gv).Item("sq_si_id")
            '        _dtrow("si_desc") = ds.Tables(0).Rows(_row_gv).Item("si_desc")

            '        fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            '    Next
            '    fobject.ds_edit.Tables(0).AcceptChanges()
            '    fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class