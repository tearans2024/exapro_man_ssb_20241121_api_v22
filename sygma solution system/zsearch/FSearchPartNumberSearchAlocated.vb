Imports master_new.ModFunction

Public Class FSearchPartNumberSearchAlocated
    Public _row, _en_id, _pi_id, _pi_area_id, _si_id, _loc_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _so_booking As String
    Public _so_alocated As String
    Public _tran_oid As String = ""
    Public _ppn_type As String = ""
    Dim func_data As New function_data
    Public _filter As String
    Public grid_call As String = ""
    Public _so_cash As Boolean = False
    Public _qty_receive As Double
    Public _pt_id As Integer

    Private Sub FSearchPartNumberSearchAlocated_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If _tran_oid.ToString <> "" Then
            add_column(gv_master, "Entity", "pi_en_id", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column_copy(gv_master, "Approval Status (Initial, Tax, Accounting)", "pt_approval_status", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "PPN Type", "pt_ppn_type", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Qty Open", "invc_qty_available", DevExpress.Utils.HorzAlignment.Default)
        Else
            add_column(gv_master, "invc_oid", False)
            'add_column(gv_master, "invc_oid", "invc_oid", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Pricelist", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Sales Area", "area_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            'add_column_copy(gv_master, "Approval Status (Initial, Tax, Accounting)", "pt_approval_status", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Tax Include", "pt_tax_inc", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "PPN Type", "pt_ppn_type", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Qty", "invc_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
            add_column(gv_master, "Qty", "invc_qty_available", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
            add_column(gv_master, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Cost", "invct_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")

        End If
    End Sub

    Public Overrides Function get_sequel() As String
        'Dim _en_id_coll As String = func_data.entity_parent(_en_id)
        get_sequel = ""

        If fobject.name = "FSalesQuotation" Or fobject.name = FSalesOrderAlocated.Name Then
            If grid_call = "header" Then
                get_sequel = "SELECT DISTINCT  " _
                    & "  public.en_mstr.en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.pt_mstr.pt_en_id, " _
                    & "  public.pi_mstr.pi_en_id, " _
                    & "  public.pi_mstr.pi_code, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.invc_mstr.invc_oid, " _
                    & "  public.pid_det.pid_pt_id, " _
                    & "  public.pidd_det.pidd_area_id, " _
                    & "  public.area_mstr.area_name, " _
                    & "  public.pidd_det.pidd_price, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.invc_mstr.invc_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pt_mstr.pt_type, " _
                    & "  public.pt_mstr.pt_loc_id, " _
                    & "  public.invc_mstr.invc_loc_id, " _
                    & "  invct_table.invct_cost, " _
                    & "  public.loc_mstr.loc_code, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.loc_mstr.loc_type, " _
                    & "  public.pt_mstr.pt_ls, " _
                    & "  public.invc_mstr.invc_qty, " _
                    & "  public.invc_mstr.invc_qty_alloc, " _
                    & "  coalesce(public.invc_mstr.invc_qty_booked, 0) AS invc_qty_booked, " _
                    & "  coalesce(public.invc_mstr.invc_qty, 0) - coalesce(public.invc_mstr.invc_qty_booked, 0) - coalesce(public.invc_mstr.invc_qty_alloc, 0) AS invc_qty_available, " _
                    & "  public.pt_mstr.pt_um, " _
                    & "  public.pt_mstr.pt_pl_id, " _
                    & "  public.pt_mstr.pt_taxable, " _
                    & "  public.pt_mstr.pt_tax_inc, " _
                    & "  public.pt_mstr.pt_tax_class, " _
                    & "  tax_class_mstr.code_name AS tax_class_name, " _
                    & "  coalesce(pt_approval_status, 'A') AS pt_approval_status, " _
                    & "  public.pt_mstr.pt_ppn_type, " _
                    & "  public.pt_mstr.pt_additional, " _
                    & "  um_mstr.code_name AS um_name " _
                    & "FROM " _
                    & "  public.pt_mstr " _
                    & "  INNER JOIN public.pid_det ON (public.pt_mstr.pt_id = public.pid_det.pid_pt_id) " _
                    & "  INNER JOIN public.pi_mstr ON (public.pid_det.pid_pi_oid = public.pi_mstr.pi_oid) " _
                    & "  INNER JOIN public.invc_mstr ON (public.pid_det.pid_pt_id = public.invc_mstr.invc_pt_id) " _
                    & "  INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr um_mstr ON (public.pt_mstr.pt_um = um_mstr.code_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.invc_mstr.invc_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.pt_mstr.pt_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pidd_det ON (public.pid_det.pid_oid = public.pidd_det.pidd_pid_oid) " _
                    & "  INNER JOIN public.area_mstr ON (public.pidd_det.pidd_area_id = public.area_mstr.area_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr tax_class_mstr ON (public.pt_mstr.pt_tax_class = tax_class_mstr.code_id) " _
                    & "  left outer join invct_table on invct_pt_id = pt_id " _
                    & " where en_active ~~* 'Y'" _
                    & " and invc_en_id = " + _en_id.ToString _
                    & " and invc_loc_id = " + _loc_id.ToString _
                    & " and pi_id = " + _pi_id.ToString _
                    & " and pidd_area_id = " + _pi_area_id.ToString _
                    & " and pt_type= 'I' " _
                    & " and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_syslog_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and invc_qty is not Null"

            Else
                get_sequel = "SELECT DISTINCT  " _
                    & "  public.en_mstr.en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.pt_mstr.pt_en_id, " _
                    & "  public.pi_mstr.pi_en_id, " _
                    & "  public.pi_mstr.pi_code, " _
                    & "  public.pi_mstr.pi_desc, " _
                    & "  public.invc_mstr.invc_oid, " _
                    & "  public.pid_det.pid_pt_id, " _
                    & "  public.pidd_det.pidd_area_id, " _
                    & "  public.area_mstr.area_name, " _
                    & "  public.pidd_det.pidd_price, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.invc_mstr.invc_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pt_mstr.pt_type, " _
                    & "  public.pt_mstr.pt_loc_id, " _
                    & "  public.invc_mstr.invc_loc_id, " _
                    & "  invct_table.invct_cost, " _
                    & "  public.loc_mstr.loc_code, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.loc_mstr.loc_type, " _
                    & "  public.pt_mstr.pt_ls, " _
                    & "  public.invc_mstr.invc_qty, " _
                    & "  public.invc_mstr.invc_qty_alloc, " _
                    & "  public.invc_mstr.invc_qty_available, " _
                    & "  coalesce(public.invc_mstr.invc_qty_booked, 0) AS invc_qty_booked, " _
                    & "  coalesce(public.invc_mstr.invc_qty, 0) - coalesce(public.invc_mstr.invc_qty_booked, 0) - coalesce(public.invc_mstr.invc_qty_alloc, 0) AS invc_qty_open, " _
                    & "  public.pt_mstr.pt_um, " _
                    & "  public.pt_mstr.pt_pl_id, " _
                    & "  public.pt_mstr.pt_taxable, " _
                    & "  public.pt_mstr.pt_tax_inc, " _
                    & "  public.pt_mstr.pt_tax_class, " _
                    & "  tax_class_mstr.code_name AS tax_class_name, " _
                    & "  coalesce(pt_approval_status, 'A') AS pt_approval_status, " _
                    & "  public.pt_mstr.pt_ppn_type, " _
                    & "  public.pt_mstr.pt_additional, " _
                    & "  um_mstr.code_name AS um_name " _
                    & "FROM " _
                    & "  public.pt_mstr " _
                    & "  INNER JOIN public.pid_det ON (public.pt_mstr.pt_id = public.pid_det.pid_pt_id) " _
                    & "  INNER JOIN public.pi_mstr ON (public.pid_det.pid_pi_oid = public.pi_mstr.pi_oid) " _
                    & "  INNER JOIN public.invc_mstr ON (public.pid_det.pid_pt_id = public.invc_mstr.invc_pt_id) " _
                    & "  INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr um_mstr ON (public.pt_mstr.pt_um = um_mstr.code_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.invc_mstr.invc_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.pt_mstr.pt_si_id = public.si_mstr.si_id) " _
                    & "  LEFT OUTER JOIN public.pidd_det ON (public.pid_det.pid_oid = public.pidd_det.pidd_pid_oid) " _
                    & "  INNER JOIN public.area_mstr ON (public.pidd_det.pidd_area_id = public.area_mstr.area_id) " _
                    & "  LEFT OUTER JOIN public.code_mstr tax_class_mstr ON (public.pt_mstr.pt_tax_class = tax_class_mstr.code_id) " _
                    & "  left outer join invct_table on invct_pt_id = pt_id " _
                    & " where en_active ~~* 'Y'" _
                    & " and invc_en_id = " + _en_id.ToString _
                    & " and pi_id = " + _pi_id.ToString _
                    & " and pidd_area_id = " + _pi_area_id.ToString _
                    & " and pt_type= 'I' " _
                    & " and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_syslog_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and invc_qty is not Null"

                'If ce_loc.EditValue = True Then
                '    get_sequel += " and invc_loc_id = '" & bt_loc_code.EditValue & "'"
                'Else
                '    get_sequel += " and invc_loc_id = " + _loc_id.ToString _
                '    '& " and invc_loc_id = " + _loc_id.ToString _
                'End If

            End If
        End If

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Public Overrides Sub fill_data()
        'Dim ds_bantu As New DataSet
        Try


            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position
            Dim dt_bantu As New DataTable()
            Dim func_coll As New function_collection

            If ds.Tables(0).Rows(_row_gv).Item("pt_approval_status").ToString.ToUpper = "I" Then
                Box("This product has not been approved by tax department")
                Exit Sub
            ElseIf ds.Tables(0).Rows(_row_gv).Item("pt_approval_status").ToString.ToUpper = "T" Then
                Box("This product has not been approved by accounting department")
                Exit Sub
            End If

            '***********************************************************************************************************
            If fobject.name = FSalesOrderAlocated.Name Or fobject.name = FSalesQuotationConsigmentAloc.Name Or fobject.name = FSalesQuotationConsigmentAlocated.Name Then

                If grid_call = "header" Then

                    fobject.sq_pt_id.tag = ds.Tables(0).Rows(_row_gv).Item("invc_pt_id")
                    fobject.sq_pt_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                    fobject.pt_desc1.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                    fobject.pt_desc2.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")

                    'ambil harga dari pricelist
                    Dim dt_price As New DataTable
                    Dim sSQL As String
                    sSQL = "SELECT  " _
                            & "  pi_id, " _
                            & "  pidd_oid, " _
                            & "  pidd_pid_oid, " _
                            & "  pidd_payment_type, " _
                            & "  pidd_price, " _
                            & "  pidd_disc, " _
                            & "  pidd_dp, " _
                            & "  pidd_interval, " _
                            & "  coalesce(pidd_payment,0) as pidd_payment, " _
                            & "  pidd_min_qty, " _
                            & "  pidd_sales_unit, " _
                            & "  pid_pt_id " _
                            & "FROM  " _
                            & "  public.pidd_det " _
                            & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                            & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                            & "  where pi_id = " & fobject.sq_pi_id.EditValue _
                            & "  and pidd_payment_type = " & fobject.sq_pay_type.EditValue _
                            & "  and pid_pt_id = " & ds.Tables(0).Rows(_row_gv).Item("pt_id") _
                            & "  order by pidd_min_qty desc "

                    dt_price = master_new.PGSqlConn.GetTableData(sSQL)

                    For Each dr As DataRow In dt_price.Rows
                        fobject.sq_price.editvalue = SetNumber(dr("pidd_price"))
                    Next

                    Dim dt_temp As New DataTable
                    Dim _row As Integer = 0


                    Dim dt_pt As New DataTable
                    ' Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")

                    'If _file = "" Then
                    '    Exit Sub
                    'End If

                    ' file_excel.EditValue = _file
                    'ds = master_new.excelconn.ImportExcel(_file)

                    sSQL = "SELECT  " _
                        & "  a.packd_oid, " _
                        & "  a.packd_pack_code, " _
                        & "  a.packd_pt_id, " _
                        & "  a.packd_dt " _
                        & "FROM " _
                        & "  public.packd_detail a " _
                        & "  INNER JOIN public.pack_mstr b ON (a.packd_pack_code = b.pack_code) " _
                        & "WHERE " _
                        & "  b.pack_pt_id =" & SetInteger(ds.Tables(0).Rows(_row_gv).Item("pt_id"))

                    dt_pt = master_new.PGSqlConn.GetTableData(sSQL)

                    fobject.ds_edit.Tables(0).Rows.Clear()
                    fobject.ds_edit.AcceptChanges()

                    fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    For Each dr As DataRow In dt_pt.Rows
                        sSQL = "SELECT DISTINCT  " _
                            & "  public.en_mstr.en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.pt_mstr.pt_en_id, " _
                            & "  public.pi_mstr.pi_en_id, " _
                            & "  public.pi_mstr.pi_code, " _
                            & "  public.pi_mstr.pi_desc, " _
                            & "  public.invc_mstr.invc_oid, " _
                            & "  public.pid_det.pid_pt_id, " _
                            & "  public.pt_mstr.pt_id, " _
                            & "  public.invc_mstr.invc_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pt_mstr.pt_type, " _
                            & "  public.pt_mstr.pt_loc_id, " _
                            & "  public.invc_mstr.invc_loc_id, " _
                            & "  invct_table.invct_cost, " _
                            & "  public.loc_mstr.loc_code, " _
                            & "  public.loc_mstr.loc_desc, " _
                            & "  public.loc_mstr.loc_type, " _
                            & "  public.pt_mstr.pt_ls, " _
                            & "  public.invc_mstr.invc_qty, " _
                            & "  public.invc_mstr.invc_qty_alloc, " _
                            & "  coalesce(public.invc_mstr.invc_qty_booked, 0) AS invc_qty_booked, " _
                            & "  coalesce(public.invc_mstr.invc_qty, 0) - coalesce(public.invc_mstr.invc_qty_booked, 0) - coalesce(public.invc_mstr.invc_qty_alloc, 0) AS invc_qty_open, " _
                            & "  public.pt_mstr.pt_um, " _
                            & "  public.pt_mstr.pt_pl_id, " _
                            & "  public.pt_mstr.pt_taxable, " _
                            & "  public.pt_mstr.pt_tax_inc, " _
                            & "  public.pt_mstr.pt_tax_class, " _
                            & "  tax_class_mstr.code_name AS tax_class_name, " _
                            & "  coalesce(pt_approval_status, 'A') AS pt_approval_status, " _
                            & "  public.pt_mstr.pt_ppn_type, " _
                            & "  public.pt_mstr.pt_additional, " _
                            & "  um_mstr.code_name AS um_name " _
                            & "FROM " _
                            & "  public.pt_mstr " _
                            & "  INNER JOIN public.pid_det ON (public.pt_mstr.pt_id = public.pid_det.pid_pt_id) " _
                            & "  INNER JOIN public.pi_mstr ON (public.pid_det.pid_pi_oid = public.pi_mstr.pi_oid) " _
                            & "  INNER JOIN public.invc_mstr ON (public.pid_det.pid_pt_id = public.invc_mstr.invc_pt_id) " _
                            & "  INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id) " _
                            & "  LEFT OUTER JOIN public.code_mstr um_mstr ON (public.pt_mstr.pt_um = um_mstr.code_id) " _
                            & "  INNER JOIN public.en_mstr ON (public.invc_mstr.invc_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.pt_mstr.pt_si_id = public.si_mstr.si_id) " _
                            & "  LEFT OUTER JOIN public.code_mstr tax_class_mstr ON (public.pt_mstr.pt_tax_class = tax_class_mstr.code_id) " _
                            & "  left outer join invct_table on invct_pt_id = pt_id " _
                            & " where pt_id =" & SetInteger(dr("packd_pt_id")) & " "

                        dt_temp = master_new.PGSqlConn.GetTableData(sSQL)


                        For Each dr_temp As DataRow In dt_temp.Rows

                            Dim ds_bantu As New DataSet

                            Try
                                Using objcb As New master_new.CustomCommand
                                    With objcb
                                        .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                            & "From pla_mstr  " _
                                                            & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                            & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                            & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                            & "where pla_pl_id = " + dr_temp("pt_pl_id").ToString _
                                                            & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                                        .InitializeCommand()
                                        .FillDataSet(ds_bantu, "prodline")

                                        If ds_bantu.Tables(0).Rows.Count = 0 Then
                                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                                dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                            Exit Sub
                                        ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                            dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                            Exit Sub
                                        ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                            Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                                dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                            Exit Sub
                                        End If

                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            fobject.gv_edit.AddNewRow()
                            fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", dr_temp("invc_oid"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_qty", dr_temp("invc_qty"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_pt_id", dr_temp("invc_pt_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", dr_temp("invc_oid"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_loc_id", dr_temp("invc_loc_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_um", dr_temp("pt_um"))
                            fobject.gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_cost", dr_temp("invct_cost"))
                            'fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", dr_temp("invc_oid"))

                            'If _so_type <> "D" Then
                            '    fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                            'End If

                            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", ds_bantu.Tables(0).Rows(0).Item("invc_oid"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_invc_qty", ds_bantu.Tables(0).Rows(0).Item("invc_qty"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                            fobject.gv_edit.SetRowCellValue(_row, "sod_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                            fobject.gv_edit.SetRowCellValue(_row, "sod_taxable", dr_temp("pt_taxable"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_tax_inc", dr_temp("pt_tax_inc"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_tax_class", dr_temp("pt_tax_class"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_tax_class_name", dr_temp("tax_class_name"))

                            fobject.gv_edit.SetRowCellValue(_row, "sod_ppn_type", dr_temp("pt_ppn_type"))
                            fobject.gv_edit.SetRowCellValue(_row, "sod_qty", 0)
                            fobject.gv_edit.SetRowCellValue(_row, "sod_disc", 0)

                            'dt_bantu = New DataTable
                            ' dt_bantu = (load_price_list(so_pi_id.EditValue, _sod_pt_id, fobject.so_pay_type.EditValue, e.Value))
                            Dim dt_bantu_new As New DataTable()

                            Using ds_bantu_new As New DataSet()

                                Try
                                    Using objcb As New master_new.CustomCommand
                                        With objcb
                                            .SQL = "SELECT  " _
                                                & "  pi_id, " _
                                                & "  pidd_oid, " _
                                                & "  pidd_pid_oid, " _
                                                & "  pidd_payment_type, " _
                                                & "  pidd_price, " _
                                                & "  pidd_disc, " _
                                                & "  pidd_dp, " _
                                                & "  pidd_interval, " _
                                                & "  coalesce(pidd_payment,0) as pidd_payment, " _
                                                & "  pidd_min_qty, " _
                                                & "  pidd_sales_unit, " _
                                                & "  pid_pt_id " _
                                                & "FROM  " _
                                                & "  public.pidd_det " _
                                                & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                                                & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                                                & "  where pi_id = " & fobject.sq_pi_id.EditValue _
                                                & "  and pidd_payment_type = " & fobject.sq_pay_type.EditValue _
                                                & "  and pidd_area_id = " & fobject.sq_pi_area_id.EditValue _
                                                & "  and pid_pt_id = " & dr_temp("pt_id") _
                                                & "  order by pidd_min_qty desc "

                                            .InitializeCommand()
                                            .FillDataSet(ds_bantu_new, "pi_mstr")
                                            dt_bantu_new = ds_bantu_new.Tables(0)
                                        End With
                                    End Using
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message)
                                End Try
                            End Using

                            If dt_bantu_new.Rows.Count > 0 Then
                                fobject.gv_edit.SetRowCellValue(_row, "sod_price", dt_bantu_new.Rows(0).Item("pidd_price"))
                                fobject.gv_edit.SetRowCellValue(_row, "sod_disc", dt_bantu_new.Rows(0).Item("pidd_disc"))

                                fobject.gv_edit.SetRowCellValue(_row, "sod_payment", dt_bantu_new.Rows(0).Item("pidd_payment"))
                                fobject.gv_edit.SetRowCellValue(_row, "sod_dp", dt_bantu_new.Rows(0).Item("pidd_dp"))
                                fobject.gv_edit.SetRowCellValue(_row, "sod_sales_unit", dt_bantu_new.Rows(0).Item("pidd_sales_unit"))
                            Else
                                fobject.gv_edit.SetRowCellValue(_row, "sod_price", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sod_disc", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sod_payment", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sod_dp", 0)
                                fobject.gv_edit.SetRowCellValue(_row, "sod_sales_unit", 0)
                            End If


                            fobject.gc_edit.EmbeddedNavigator.Buttons.DoClick(fobject.gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                            _row = _row + 1
                            System.Windows.Forms.Application.DoEvents()

                        Next
                    Next
                    fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
                    fobject.gv_edit.BestFitColumns()
                Else
                    Dim ds_bantu As New DataSet

                    Try
                        Using objcb As New master_new.CustomCommand
                            With objcb
                                .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                    & "From pla_mstr  " _
                                                    & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                    & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                    & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                    & "where pla_pl_id = " + ds.Tables(0).Rows(_row_gv).Item("pt_pl_id").ToString _
                                                    & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                                .InitializeCommand()
                                .FillDataSet(ds_bantu, "prodline")

                                If ds_bantu.Tables(0).Rows.Count = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        ds.Tables(0).Rows(_row_gv).Item("pt_pl_id")) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                    Exit Sub
                                End If

                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                    'penempatan invc_oid di detail edit)
                    fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", ds.Tables(0).Rows(_row_gv).Item("invc_oid"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_invc_qty", ds.Tables(0).Rows(_row_gv).Item("invc_qty"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_ppn_type", ds.Tables(0).Rows(_row_gv).Item("pt_ppn_type"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_taxable", ds.Tables(0).Rows(_row_gv).Item("pt_taxable"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_tax_inc", ds.Tables(0).Rows(_row_gv).Item("pt_tax_inc"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_tax_class", ds.Tables(0).Rows(_row_gv).Item("pt_tax_class"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("tax_class_name"))

                    fobject.gv_edit.SetRowCellValue(_row, "sod_pt_id", ds.Tables(0).Rows(_row_gv).Item("invc_pt_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
                    fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_loc_id", ds.Tables(0).Rows(_row_gv).Item("invc_loc_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "_sod_invc_oid", ds.Tables(0).Rows(_row_gv).Item("invc_oid"))
                    fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
                    fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_invc_oid", ds.Tables(0).Rows(_row_gv).Item("invc_oid"))


                    fobject.gv_edit.SetRowCellValue(_row, "sod_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                    fobject.gv_edit.SetRowCellValue(_row, "sod_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                    fobject.gv_edit.SetRowCellValue(_row, "sod_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                    fobject.gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                    Dim dt_bantu_new As New DataTable()

                    Using ds_bantu_new As New DataSet()

                        Try
                            Using objcb As New master_new.CustomCommand
                                With objcb
                                    .SQL = "SELECT  " _
                                        & "  pi_id, " _
                                        & "  pidd_oid, " _
                                        & "  pidd_pid_oid, " _
                                        & "  pidd_payment_type, " _
                                        & "  pidd_price, " _
                                        & "  pidd_disc, " _
                                        & "  pidd_dp, " _
                                        & "  pidd_interval, " _
                                        & "  coalesce(pidd_payment,0) as pidd_payment, " _
                                        & "  pidd_min_qty, " _
                                        & "  pidd_sales_unit, " _
                                        & "  pid_pt_id " _
                                        & "FROM  " _
                                        & "  public.pidd_det " _
                                        & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                                        & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                                        & "  where pi_id = " + _pi_id.ToString _
                                        & "  and pidd_payment_type = " & fobject.so_pay_type.EditValue _
                                        & " and pidd_area_id = " + _pi_area_id.ToString _
                                        & "  and pid_pt_id = " & ds.Tables(0).Rows(_row_gv).Item("pt_id") _
                                        & "  order by pidd_min_qty desc "

                                    .InitializeCommand()
                                    .FillDataSet(ds_bantu_new, "pi_mstr")
                                    dt_bantu_new = ds_bantu_new.Tables(0)
                                End With
                            End Using
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End Using

                    If dt_bantu_new.Rows.Count > 0 Then
                        fobject.gv_edit.SetRowCellValue(_row, "sod_price", dt_bantu_new.Rows(0).Item("pidd_price"))
                        fobject.gv_edit.SetRowCellValue(_row, "sod_disc", dt_bantu_new.Rows(0).Item("pidd_disc"))

                        fobject.gv_edit.SetRowCellValue(_row, "sod_payment", dt_bantu_new.Rows(0).Item("pidd_payment"))
                        fobject.gv_edit.SetRowCellValue(_row, "sod_dp", dt_bantu_new.Rows(0).Item("pidd_dp"))
                        fobject.gv_edit.SetRowCellValue(_row, "sod_sales_unit", dt_bantu_new.Rows(0).Item("pidd_sales_unit"))
                    Else
                        fobject.gv_edit.SetRowCellValue(_row, "sod_price", 0)
                        fobject.gv_edit.SetRowCellValue(_row, "sod_disc", 0)
                        fobject.gv_edit.SetRowCellValue(_row, "sod_payment", 0)
                        fobject.gv_edit.SetRowCellValue(_row, "sod_dp", 0)
                        fobject.gv_edit.SetRowCellValue(_row, "sod_sales_unit", 0)
                    End If

                    fobject.gv_edit.BestFitColumns()

                End If

                'Dim _dtrow As DataRow
                'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                '    _dtrow("sod_invc_oid") = ds_bantu.Tables(0).Rows(i).Item("invc_oid")
                'Next
                '*********************************************************************************************************
                '=====================
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()

        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

End Class
