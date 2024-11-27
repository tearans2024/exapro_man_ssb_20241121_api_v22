﻿
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraNavBar
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports System.Collections.Generic


Public Class FMainMenu
    Private Const WM_GETTEXT As Integer = &HD
    Private Const WM_GETTEXTLENGTH As Integer = &HE

    Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" _
            (ByVal hWnd1 As IntPtr, ByVal hWnd2 As IntPtr, ByVal lpsz1 As String, _
            ByVal lpsz2 As String) As Integer

    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
        (ByVal hWnd As IntPtr, ByVal wMsg As Integer, _
        ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
        (ByVal hWnd As IntPtr, ByVal wMsg As Integer, _
        ByVal wParam As Integer, ByVal lParam As System.Text.StringBuilder) As Integer

    Dim _number_find As Integer = 0
    Public searchString As String
    Public searchIndex As Integer
    Public searchArray As New List(Of TreeListNode)
    Dim _number_warning As Integer = 0
    Dim _version_id As Integer
    Dim dt_menu As New DataTable

    Private Sub init_menu_tree()
        Dim sSQL As String
        Try
            'If func_coll.get_conf_file("syspro_approval_code") = "DUTA" Then
            'ssb _version_id = 3

            'mtf()
            '_version_id = 3
            _version_id = 9
            '_version_id = 9
            'mutif _version_id = 3S
            '_version_id = 1

            'ElseIf func_coll.get_conf_file("syspro_approval_code") = "SDI" Then
            '    _version_id = 10
            'ElseIf func_coll.get_conf_file("syspro_approval_code") = "MEDIAQU" Then
            '    _version_id = 5
            'ElseIf func_coll.get_conf_file("syspro_approval_code") = "SDQ" Then
            '    _version_id = 5
            'Else
            '    _version_id = 4
            'End If


            'saa ver 3
            ''sea ver 3
            'ski ver 3
            'seg ver 3
            'sdi ver 3
            'duta ver 3



            'sSQL = "SELECT  " _
            '  & "a.menuid, " _
            '  & "a.menuname, " _
            '  & "a.menuid_parent,a.menudesc,a.menuseq " _
            '  & "FROM " _
            '  & "  public.tconfmenucollection a " _
            '  & "WHERE menuid in (SELECT menu_id as menuid " _
            '  & "FROM " _
            '  & "  public.get_all_menu_group(" & master_new.ClsVar.sUserID.ToString & ") where menu_id is not null " & _
            '  " UNION  " _
            '  & "SELECT menu_id as menuid " _
            '  & "  " _
            '  & "FROM " _
            '  & "  public.get_all_menu_user(" & master_new.ClsVar.sUserID.ToString & ") where menu_id is not null) "


            sSQL = "SELECT  " _
                & "a.menuid, " _
                & "a.menuname, " _
                & "a.menuid_parent,a.menudesc,a.menuseq " _
                & "FROM " _
                & "  public.tconfmenucollection a " _
                & "WHERE menuid in (SELECT menu_id as menuid " _
                & "FROM " _
                & "  public.get_all_menu_group(" & master_new.ClsVar.sUserID.ToString & "," & _version_id & ") where menu_id is not null " & _
                " UNION  " _
                & "SELECT menu_id as menuid " _
                & "  " _
                & "FROM " _
                & "  public.get_all_menu_user(" & master_new.ClsVar.sUserID.ToString & "," & _version_id & ") where menu_id is not null) "



            'Dim _status_sync As Boolean = master_new.PGSqlConn.status_sync

            dt_menu = GetTableData(sSQL)
   
            TreeList1.DataSource = dt_menu
            TreeList1.BestFitColumns()
            TreeList1.Columns("menuseq").SortOrder = SortOrder.Ascending
            TreeList1.CollapseAll()
            ceExpandCollaps.EditValue = False


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Function GetText(ByVal hWnd As IntPtr) As String
        Dim textLength As Integer = SendMessage(hWnd, WM_GETTEXTLENGTH, 0, 0) + 1
        Dim sb As New System.Text.StringBuilder(textLength)
        If textLength > 0 Then
            Call SendMessage(hWnd, WM_GETTEXT, textLength, sb)
        End If
        Return sb.ToString
    End Function

    Public Overrides Sub reminder_check()
        Dim func_data As New function_data
        Dim _user_approval As String = func_data.get_user_approval()
        Dim ds_bantu As New DataSet
        Dim nik As String = master_new.ClsVar.sNik
        Try
            Using objload As New master_new.CustomCommand
                With objload
                    .SQL = "select wf_ref_desc, wf_ref_code, wf_dt from wf_mstr " + _
                           " where wf_user_id in (" + _user_approval + ")" + _
                           " and wf_iscurrent ~~* 'y'" + _
                           " and wf_wfs_id = '0' " + _
                           " union " + _
                           " select wf_ref_desc, wf_ref_code, wf_dt from wf_mstr " + _
                           " where wf_user_id in (" + _user_approval + ")" + _
                           " and wf_iscurrent ~~* 'y'" + _
                           " and wf_wfs_id = '2' " + _
                           " and wf_date_to <= " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wf")
                End With
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If ds_bantu.Tables("wf").Rows.Count > 0 Then
            Dim frm_r As New FReminderSystem
            frm_r.set_window(Me)
            frm_r.Show()
        End If
    End Sub

    Public Overrides Sub setbbi()

        Dim ssql As String
        Try
            ssql = "select groupid from tconfusergroup where userid = " & master_new.ClsVar.sUserID & ""
            Dim dt As New DataTable

            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                If dr(0) = 1 Then
                    bbi_user.ImageIndex = -1
                    bbi_group.ImageIndex = -1
                    bbi_menu.ImageIndex = -1
                    bbi_add_menu.ImageIndex = -1

                    bbi_user.Enabled = True
                    bbi_group.Enabled = True
                    bbi_menu.Enabled = True
                    bbi_add_menu.Enabled = True
                End If
            Next
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub InitMenu()
        panelContainer1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        init_menu_tree()
        'Try
        '    'set semua item menjadi visible = false
        '    Dim dp As DevExpress.XtraBars.Docking.DockPanel
        '    Dim dpb As DevExpress.XtraBars.Docking.DockPanel
        '    Dim cc As DevExpress.XtraBars.Docking.ControlContainer
        '    Dim nbc As DevExpress.XtraNavBar.NavBarControl

        '    For Each ctrl In Me.Controls
        '        If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
        '            dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
        '            For Each ctrl_dp In dp.Controls
        '                If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.DockPanel Then
        '                    dpb = CType(ctrl_dp, DevExpress.XtraBars.Docking.DockPanel)
        '                    For Each ctrl_cc In dpb.Controls
        '                        If TypeOf ctrl_cc Is DevExpress.XtraBars.Docking.ControlContainer Then
        '                            cc = CType(ctrl_cc, DevExpress.XtraBars.Docking.ControlContainer)
        '                            For Each ctrl_ccd In cc.Controls
        '                                If TypeOf ctrl_ccd Is DevExpress.XtraNavBar.NavBarControl Then
        '                                    nbc = CType(ctrl_ccd, DevExpress.XtraNavBar.NavBarControl)
        '                                    For Each item As DevExpress.XtraNavBar.NavBarItem In nbc.Items
        '                                        item.Visible = False
        '                                    Next item
        '                                End If
        '                            Next
        '                        End If
        '                    Next
        '                End If
        '            Next
        '        End If
        '    Next

        '    Using objcb As New master_new.CustomCommand
        '        With objcb
        '            '.Connection.Open()
        '            '.Command = .Connection.CreateCommand
        '            '.Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select distinct menuname, enablestatus, visiblestatus  " & _
        '                                   " from tconfusergroup ug " & _
        '                                   " inner join tconfmenu m on m.groupid = ug.groupid " & _
        '                                   " inner join tconfmenucollection mc on mc.menuid = m.menuid " & _
        '                                   " where userid = " & master_new.ClsVar.sUserID.ToString & _
        '                                   " UNION  " _
        '                                    & "SELECT  " _
        '                                    & "  b.menuname, " _
        '                                    & "  a.enablestatus, " _
        '                                    & "  a.visiblestatus " _
        '                                    & "FROM " _
        '                                    & "  public.tconfmenuuser a " _
        '                                    & "  INNER JOIN public.tconfmenucollection b ON (a.menuid = b.menuid) " _
        '                                    & "WHERE " _
        '                                    & "  a.userid = " & master_new.ClsVar.sUserID.ToString & _
        '                                    " order by menuname,visiblestatus, enablestatus "

        '            .InitializeCommand()
        '            .DataReader = .ExecuteReader
        '            While .DataReader.Read
        '                If .DataReader("menuname").ToString = "Domain" Then
        '                    nbi_master_data_domain.Visible = .DataReader("visiblestatus")
        '                    nbi_master_data_domain.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_master_data_domain, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ConfFile" Then
        '                    nbi_company_conf.Visible = .DataReader("visiblestatus")
        '                    nbi_company_conf.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_company_conf, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RoutingApproval" Then
        '                    nbi_company_routing_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_company_routing_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_company_routing_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CompanyAddress" Then
        '                    nbi_mst_comp_company_address.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_comp_company_address.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_comp_company_address, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Employee" Then
        '                    nbi_mst_comp_employee.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_comp_employee.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_comp_employee, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Agama" Then
        '                    nbi_master_data_agama.Visible = .DataReader("visiblestatus")
        '                    nbi_master_data_agama.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_master_data_agama, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "OrganizationType" Then
        '                    nbi_mst_org_type.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_org_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_org_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Organization" Then
        '                    nbi_mst_organization.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_organization.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_organization, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "OrganizationStructure" Then
        '                    nbi_mst_organization_structure.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_organization_structure.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_organization_structure, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "OrganizationStructureDetail" Then
        '                    nbi_mst_org_struc_detail.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_org_struc_detail.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_org_struc_detail, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "OrganizationTree" Then
        '                    nbi_mst_organizationtree.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_organizationtree.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_organizationtree, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransactionStatus" Then
        '                    nbi_mst_comp_tran_status.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_comp_tran_status.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_comp_tran_status, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Transaction" Then
        '                    nbi_mst_emp_transaction.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_emp_transaction.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_emp_transaction, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PartNumber" Then
        '                    nbi_mst_is_partnumber.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_partnumber.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_partnumber, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "PartNumberApprovalTax" Then
        '                    nbi_mst_is_approval_tax.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_approval_tax.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_approval_tax, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "PartNumberApprovalAccounting" Then
        '                    nbi_mst_is_approval_accounting.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_approval_accounting.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_approval_accounting, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "PartNumberGroup" Then
        '                    nbi_mst_is_part_number_group.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_part_number_group.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_part_number_group, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryStatus" Then
        '                    nbi_mst_is_inventorystatus.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_inventorystatus.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_inventorystatus, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Site" Then
        '                    nbi_mst_is_site.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_site.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_site, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ItemSiteCost" Then
        '                    nbi_mst_is_item_site_cost.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_item_site_cost.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_item_site_cost, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ItemStatus" Then
        '                    nbi_mst_is_itemstatus.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_itemstatus.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_itemstatus, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Warehouse" Then
        '                    nbi_mst_is_warehouse.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_warehouse.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_warehouse, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WarehouseType" Then
        '                    nbi_mst_is_warehouse_type.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_warehouse_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_warehouse_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WarehouseCategory" Then
        '                    nbi_mst_is_warehouse_category.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_warehouse_category.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_warehouse_category, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Location" Then
        '                    nbi_mst_is_location.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_location.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_location, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "LocationCategory" Then
        '                    nbi_mst_is_location_category.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_location_category.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_location_category, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "LocationType" Then
        '                    nbi_mst_is_location_type.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_location_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_location_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ProductLine" Then
        '                    nbi_mst_is_productline.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_productline.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_productline, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryAccount" Then
        '                    nbi_mst_prodline_location.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_prodline_location.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_prodline_location, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "UnitMeasure" Then
        '                    nbi_mst_is_unitmeasure.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_unitmeasure.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_unitmeasure, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "UMConversion" Then
        '                    nbi_mst_is_unit_conversion.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_is_unit_conversion.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_is_unit_conversion, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Partner" Then
        '                    nbi_mst_at_partner.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_partner.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_partner, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PartnerAddress" Then
        '                    nbi_mst_at_partner_address.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_partner_address.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_partner_address, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PartnerContactPerson" Then
        '                    nbi_mst_at_partner_cp.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_partner_cp.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_partner_cp, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PartnerAll" Then
        '                    nbi_mst_at_partner_all.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_partner_all.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_partner_all, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ContactPersonFunction" Then
        '                    nbi_mst_at_cp_function.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_cp_function.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_cp_function, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PartnerBank" Then
        '                    nbi_mst_at_partner_bank.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_partner_bank.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_partner_bank, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PartnerGroup" Then
        '                    nbi_mst_at_partner_group.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_partner_group.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_partner_group, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "AddressType" Then
        '                    nbi_mst_at_address_type.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_address_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_address_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TaxClass" Then
        '                    nbi_mst_at_taxclass.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_taxclass.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_taxclass, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TaxType" Then
        '                    nbi_mst_at_tax_type.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_tax_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_tax_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TaxRate" Then
        '                    nbi_mst_at_tax_rate.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_at_tax_rate.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_at_tax_rate, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CreditTerms" Then
        '                    nbi_mst_fin_credit_terms.Visible = .DataReader("visiblestatus")
        '                    nbi_mst_fin_credit_terms.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_mst_fin_credit_terms, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Position" Then
        '                    nbi_company_position.Visible = .DataReader("visiblestatus")
        '                    nbi_company_position.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_company_position, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DocumentApproval" Then
        '                    nbi_company_dok_aprv.Visible = .DataReader("visiblestatus")
        '                    nbi_company_dok_aprv.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_company_dok_aprv, .DataReader("enablestatus"))

        '                    '#Distribution
        '                ElseIf .DataReader("menuname").ToString = "Requisition" Then
        '                    nbi_dist_req_mstr.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_mstr.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_mstr, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RequisitionApproval" Then
        '                    nbi_dist_req_mstr_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_mstr_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_mstr_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RequisitionPrint" Then
        '                    nbi_dist_req_mstr_print.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_mstr_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_mstr_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ReqTransferIssue" Then
        '                    nbi_dist_req_transfer_issue.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_transfer_issue.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_transfer_issue, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ReqTransferIssueApproval" Then
        '                    nbi_dist_req_transfer_issue_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_transfer_issue_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_transfer_issue_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ReqTransferIssuePrint" Then
        '                    nbi_dist_req_transfer_issue_print.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_transfer_issue_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_transfer_issue_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ReqTransferReceipt" Then
        '                    nbi_dist_req_transfer_receipt.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_transfer_receipt.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_transfer_receipt, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ReqTransferReceiptPrint" Then
        '                    nbi_dist_req_transfer_receipt_print.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_req_transfer_receipt_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_req_transfer_receipt_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseOrder" Then
        '                    nbi_dist_purchase_order.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_order.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_order, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseOrderPrint" Then
        '                    nbi_dist_purchase_order_print.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_order_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_order_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseOrderPrintNoCost" Then
        '                    nbi_dist_purchase_order_print_no_cost.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_order_print_no_cost.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_order_print_no_cost, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseOrderReport" Then
        '                    nbi_dist_purchase_order_report.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_order_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_order_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseReceipt" Then
        '                    nbi_dist_purchase_receipt.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_receipt.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_receipt, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseReceiptPrint" Then
        '                    nbi_dist_purchase_receipt_print.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_receipt_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_receipt_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseReceiptReport" Then
        '                    nbi_dist_purchase_receipt_report.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_receipt_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_receipt_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseReturn" Then
        '                    nbi_dist_purchase_return.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_return.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_return, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseReturnPrint" Then
        '                    nbi_dist_purchase_return_print.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_purchase_return_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_purchase_return_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ReasonCodeReturn" Then
        '                    nbi_dist_reason_code_return.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_reason_code_return.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_reason_code_return, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseOrderFilm" Then
        '                    nbi_purchase_order_filem.Visible = .DataReader("visiblestatus")
        '                    nbi_purchase_order_filem.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_purchase_order_filem, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PurchaseOrderPrintFilm" Then
        '                    nbi_purchase_order_filem_print.Visible = .DataReader("visiblestatus")
        '                    nbi_purchase_order_filem_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_purchase_order_filem_print, .DataReader("enablestatus"))


        '                ElseIf .DataReader("menuname").ToString = "InventoryBeginingBalance" Then
        '                    nbi_inv_inventory_begining_balance.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_inventory_begining_balance.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_inventory_begining_balance, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryReportDetail" Then
        '                    nbi_inv_inventory_detail.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_inventory_detail.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_inventory_detail, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryReportDetailLog" Then
        '                    nbi_inv_inventory_detail_2.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_inventory_detail_2.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_inventory_detail_2, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryReportDate" Then
        '                    nbi_inv_inventory_by_eff_date.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_inventory_by_eff_date.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_inventory_by_eff_date, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryRequest" Then
        '                    nbi_inv_request.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_request.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_request, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryRequestApproval" Then
        '                    nbi_inv_request_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_request_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_request_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryRequestPrint" Then
        '                    nbi_inv_request_print.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_request_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_request_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryReceipts" Then
        '                    nbi_inv_receipts.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_receipts.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_receipts, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InvReceiptsPrint" Then
        '                    nbi_inv_receipts_print.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_receipts_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_receipts_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryIssues" Then
        '                    nbi_inv_issues.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_issues.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_issues, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InvIssuePrint" Then
        '                    nbi_inv_issues_print.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_issues_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_issues_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransferIssues" Then
        '                    nbi_inv_transfer_issues.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_transfer_issues.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_transfer_issues, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransferIssuesReturn" Then
        '                    nbi_inv_transfer_issues_return.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_transfer_issues_return.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_transfer_issues_return, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransferIssueApproval" Then
        '                    nbi_inv_transfer_issues_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_transfer_issues_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_transfer_issues_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransferIssuesPrint" Then
        '                    nbi_inv_transfer_issues_print_out.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_transfer_issues_print_out.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_transfer_issues_print_out, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransferReceipts" Then
        '                    nbi_inv_transfer_receipts.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_transfer_receipts.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_transfer_receipts, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransferReceiptsPrint" Then
        '                    nbi_inv_transfer_receipts_print_out.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_transfer_receipts_print_out.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_transfer_receipts_print_out, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryAdjustmentPlus" Then
        '                    nbi_inv_adjusment_plus.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_adjusment_plus.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_adjusment_plus, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryAdjustmentMinus" Then
        '                    nbi_inv_adjustment_minus.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_adjustment_minus.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_adjustment_minus, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "InventoryAdjusmentPlusReport" Then
        '                    nbi_inv_adjusment_plus_report.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_adjusment_plus_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_adjusment_plus_report, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "InventoryAdjusmentMinusReport" Then
        '                    nbi_inv_adjustment_minus_report.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_adjustment_minus_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_adjustment_minus_report, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "InventoryReportDetailWIP" Then
        '                    nbi_inv_report_detail_wip.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_report_detail_wip.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_report_detail_wip, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "WorkInProgress" Then
        '                    nbi_manu_wip.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_wip.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_wip, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "TransferIssuesWIP" Then
        '                    nbi_manu_trans_issue_wip.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_trans_issue_wip.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_trans_issue_wip, .DataReader("enablestatus"))


        '                ElseIf .DataReader("menuname").ToString = "InventoryCycleCount" Then
        '                    nbi_inv_cycle_count.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_cycle_count.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_cycle_count, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryHistory" Then
        '                    nbi_dist_inv_history.Visible = .DataReader("visiblestatus")
        '                    nbi_dist_inv_history.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_dist_inv_history, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryIssuesReport" Then
        '                    nbi_inv_isues_report.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_isues_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_isues_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InventoryReceiptsReport" Then
        '                    nbi_inv_receipts_report.Visible = .DataReader("visiblestatus")
        '                    nbi_inv_receipts_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_inv_receipts_report, .DataReader("enablestatus"))

        '                    '#AR
        '                ElseIf .DataReader("menuname").ToString = "ARType" Then
        '                    nbi_fin_ar_type.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DRCRMemo" Then
        '                    nbi_fin_ar_dc_memo.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_dc_memo.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_dc_memo, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DRCRMemoDetail" Then
        '                    nbi_fin_ar_dc_memo_detail.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_dc_memo_detail.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_dc_memo_detail, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DRCRMemoReport" Then
        '                    nbi_fin_ar_dc_memo_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_dc_memo_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_dc_memo_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ARPayment" Then
        '                    nbi_fin_ar_payment.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_payment.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_payment, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ARPaymentDetail" Then
        '                    nbi_fin_ar_payment_detail.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_payment_detail.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_payment_detail, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DRCRMemoPrint" Then
        '                    nbi_fin_ar_dc_memo_print_out.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_dc_memo_print_out.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_dc_memo_print_out, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DRCRMemoKonsiyasiPrint" Then
        '                    nbi_fin_ar_dc_memo_konsiyasi_print_out.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_dc_memo_konsiyasi_print_out.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_dc_memo_konsiyasi_print_out, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ARPaymentReport" Then
        '                    nbi_fin_payment_ar_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_payment_ar_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_payment_ar_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DRCRMemoReportDetail" Then
        '                    nbi_fin_ar_dc_memo_detail_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_dc_memo_detail_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_dc_memo_detail_report, .DataReader("enablestatus"))



        '                ElseIf .DataReader("menuname").ToString = "FakturPajakTransactionCode" Then
        '                    nbi_fin_ar_faktur_pajak_transaction_code.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_faktur_pajak_transaction_code.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_faktur_pajak_transaction_code, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "FakturPajak" Then
        '                    nbi_fin_ar_faktur_pajak.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_faktur_pajak.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_faktur_pajak, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "FakturPajakApproval" Then
        '                    nbi_fin_ar_faktur_pajak_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_faktur_pajak_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_faktur_pajak_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PajakSignUser" Then
        '                    nbi_fin_ar_faktur_pajak_sign.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_faktur_pajak_sign.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_faktur_pajak_sign, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "FakturPajakPrint" Then
        '                    nbi_fin_ar_faktur_pajak_print.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_faktur_pajak_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_faktur_pajak_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SOARFPReport" Then
        '                    nbi_so_ar_fp_report.Visible = .DataReader("visiblestatus")
        '                    nbi_so_ar_fp_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_so_ar_fp_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CashInReport" Then
        '                    nbi_fin_ar_cash_in_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_cash_in_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_cash_in_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DRCRMemoBalanceReport" Then
        '                    nbi_fin_ar_dbcr_balance_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_dbcr_balance_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_dbcr_balance_report, .DataReader("enablestatus"))


        '                    '#AP
        '                ElseIf .DataReader("menuname").ToString = "AccountPayableType" Then
        '                    nbi_fin_ap_type.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Voucher" Then
        '                    nbi_fin_ap_voucher.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_voucher.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_voucher, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "VoucherTax" Then
        '                    nbi_fin_ap_voucher_tax.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_voucher_tax.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_voucher_tax, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PaymentManualChecks" Then
        '                    nbi_fin_ap_payment.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_payment.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_payment, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "VoucherReportByAging" Then
        '                    nbi_fin_ap_voucher_report_by_aging.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_voucher_report_by_aging.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_voucher_report_by_aging, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "VoucherReportByType" Then
        '                    nbi_fin_ap_voucher_report_by_type.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_voucher_report_by_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_voucher_report_by_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "VoucherReportByTop" Then
        '                    nbi_fin_ap_voucher_report_by_top.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_voucher_report_by_top.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_voucher_report_by_top, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "UnvoucheredPOReceipt" Then
        '                    nbi_fin_ap_voucher_report_by_unvouchered.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_voucher_report_by_unvouchered.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_voucher_report_by_unvouchered, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ARReportByAging" Then
        '                    nbi_fin_ar_report_by_aging.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_report_by_aging.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_report_by_aging, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ARReportByAgingSDI" Then
        '                    nbi_fin_ar_report_by_aging_sdi.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_report_by_aging_sdi.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_report_by_aging_sdi, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ARReportByTop" Then
        '                    nbi_fin_ar_report_by_top.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_report_by_top.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_report_by_top, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ARPendingInvoice" Then
        '                    nbi_fin_ar_report_pending_invoice.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ar_report_pending_invoice.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ar_report_pending_invoice, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "VoucherBalanceReport" Then
        '                    nbi_fin_ap_voucher_balance_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_ap_voucher_balance_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_ap_voucher_balance_report, .DataReader("enablestatus"))

        '                    '#GL
        '                ElseIf .DataReader("menuname").ToString = "Entity" Then
        '                    nbi_fin_gl_entity.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_entity.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_entity, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "EntityGroup" Then
        '                    nbi_fin_gl_entity_group.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_entity_group.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_entity_group, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "GLCalendar" Then
        '                    nbi_fin_gl_glcalendar.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_glcalendar.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_glcalendar, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ProjectAccount" Then
        '                    nbi_fin_gl_project.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_project.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_project, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Account" Then
        '                    nbi_fin_gl_account.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_account.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_account, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SubAccount" Then
        '                    nbi_fin_gl_subaccount.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_subaccount.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_subaccount, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CostCenter" Then
        '                    nbi_fin_gl_cost_center.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_cost_center.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_cost_center, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CostCenterUser" Then
        '                    nbi_fin_gl_cost_center_user.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_cost_center_user.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_cost_center_user, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RelationAccToCostCnt" Then
        '                    nbi_fin_gl_cost_center_account.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_cost_center_account.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_cost_center_account, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "OpeningBalance" Then
        '                    nbi_fin_gl_opening_balance.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_opening_balance.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_opening_balance, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "StandardTransaction" Then
        '                    nbi_fin_gl_standard_transaction.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_standard_transaction.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_standard_transaction, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "UnpostedTransaction" Then
        '                    nbi_fin_gl_unposted_transaction.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_unposted_transaction.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_unposted_transaction, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TransactionPost" Then
        '                    nbi_fin_gl_transaction_post.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_transaction_post.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_transaction_post, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "MonthEnd" Then
        '                    nbi_fin_gl_month_end.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_month_end.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_month_end, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "YearEnd" Then
        '                    nbi_fin_gl_year_end.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_year_end.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_year_end, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "AddAccount" Then
        '                    nbi_fin_add_account_glbal.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_add_account_glbal.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_add_account_glbal, .DataReader("enablestatus"))

        '                    '#Multiple Currency
        '                ElseIf .DataReader("menuname").ToString = "Currency" Then
        '                    nbi_fin_mc_currency.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_mc_currency.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_mc_currency, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ExchangeRate" Then
        '                    nbi_fin_mc_exchange_rate.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_mc_exchange_rate.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_mc_exchange_rate, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Bank" Then
        '                    nbi_fin_mc_bank.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_mc_bank.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_mc_bank, .DataReader("enablestatus"))

        '                    '#GL Report
        '                ElseIf .DataReader("menuname").ToString = "GeneralLedgerReport" Then
        '                    nbi_fin_gl_general_ledger_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_general_ledger_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_general_ledger_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ProfitLossReport" Then
        '                    nbi_fin_gl_profit_loss_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_profit_loss_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_profit_loss_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "TrialBalanceReport" Then
        '                    nbi_fin_gl_trial_balance_report.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_trial_balance_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_trial_balance_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "BalanceSheetReport" Then
        '                    nbi_fin_gl_balance_sheet.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_balance_sheet.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_balance_sheet, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CashFlowReport" Then
        '                    nbi_fin_gl_cash_flow.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_gl_cash_flow.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_gl_cash_flow, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "BSSetting" Then
        '                    nbi_fin_bsSetting.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_bsSetting.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_bsSetting, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PLSetting" Then
        '                    nbi_fin_plSetting.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_plSetting.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_plSetting, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CashFlowSetting" Then
        '                    nbi_fin_cfSetting.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_cfSetting.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_cfSetting, .DataReader("enablestatus"))

        '                    '#Budget
        '                ElseIf .DataReader("menuname").ToString = "BdgtPeriode" Then
        '                    nbi_fin_bgt_periode.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_bgt_periode.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_bgt_periode, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Budgeting" Then
        '                    nbi_fin_bgt_generate.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_bgt_generate.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_bgt_generate, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "BudgetDetail" Then
        '                    nbi_fin_bgt_maintenance.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_bgt_maintenance.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_bgt_maintenance, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "BudgetApproval" Then
        '                    nbi_fin_bgt_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_bgt_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_bgt_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CrossBudget" Then
        '                    nbi_fin_bgt_cross.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_bgt_cross.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_bgt_cross, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CrossBudgetApproval" Then
        '                    nbi_fin_bgt_cross_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_fin_bgt_cross_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_fin_bgt_cross_approval, .DataReader("enablestatus"))

        '                    '#cash management
        '                ElseIf .DataReader("menuname").ToString = "Transfer" Then
        '                    nbi_cash_transfer.Visible = .DataReader("visiblestatus")
        '                    nbi_cash_transfer.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_cash_transfer, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CashOut" Then
        '                    nbi_cash_out.Visible = .DataReader("visiblestatus")
        '                    nbi_cash_out.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_cash_out, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "CashIn" Then
        '                    nbi_cash_in.Visible = .DataReader("visiblestatus")
        '                    nbi_cash_in.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_cash_in, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Reconciliation" Then
        '                    nbi_cash_bank_reconciliation.Visible = .DataReader("visiblestatus")
        '                    nbi_cash_bank_reconciliation.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_cash_bank_reconciliation, .DataReader("enablestatus"))

        '                    '#Sales Order
        '                ElseIf .DataReader("menuname").ToString = "SalesProgram" Then
        '                    nbi_sales_order_sales_program.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_sales_program.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_sales_program, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PaymentType" Then
        '                    nbi_sales_order_pay_type.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_pay_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_pay_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PaymentMethode" Then
        '                    nbi_sales_order_payment_methode.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_payment_methode.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_payment_methode, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ReasonCodeSO" Then
        '                    nbi_sales_order_reason_code.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_reason_code.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_reason_code, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Promo" Then
        '                    nbi_so_promo.Visible = .DataReader("visiblestatus")
        '                    nbi_so_promo.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_so_promo, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PriceList" Then
        '                    nbi_sales_order_pricelist.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_pricelist.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_pricelist, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PriceListHeader" Then
        '                    nbi_sales_order_pricelist_header.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_pricelist_header.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_pricelist_header, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PriceListDetail" Then
        '                    nbi_sales_order_pricelist_detail.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_pricelist_detail.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_pricelist_detail, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PriceListCopy" Then
        '                    nbi_sales_order_pricelist_copy.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_pricelist_copy.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_pricelist_copy, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrder" Then
        '                    nbi_sales_order_so.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_so.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_so, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderCreditReport" Then
        '                    nbi_sales_order_so_credit_report.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_so_credit_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_so_credit_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderApproval" Then
        '                    nbi_sales_order_so_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_so_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_so_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderPrint" Then
        '                    nbi_sales_order_so_print.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_so_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_so_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderReport" Then
        '                    nbi_sales_order_so_report.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_so_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_so_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderFakturPenjualanPrint" Then
        '                    nbi_sales_order_faktur_penjualan_print.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_faktur_penjualan_print.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_faktur_penjualan_print, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderShipment" Then
        '                    nbi_sales_order_shipments.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_shipments.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_shipments, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderShipmentPrint" Then
        '                    nbi_sales_order_shipments_print_out.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_shipments_print_out.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_shipments_print_out, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderReturn" Then
        '                    nbi_sales_order_returns.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_returns.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_returns, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderReturnPrint" Then
        '                    nbi_sales_order_returns_print_out.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_returns_print_out.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_returns_print_out, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderManualAllocation" Then
        '                    nbi_sales_order_manual_allocation.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_manual_allocation.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_manual_allocation, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderShipmentReport" Then
        '                    nbi_so_ship_report.Visible = .DataReader("visiblestatus")
        '                    nbi_so_ship_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_so_ship_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesOrderReturnReport" Then
        '                    nbi_so_retur_report.Visible = .DataReader("visiblestatus")
        '                    nbi_so_retur_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_so_retur_report, .DataReader("enablestatus"))



        '                    '# Insentif & Royalti
        '                ElseIf .DataReader("menuname").ToString = "Area" Then
        '                    nbi_sales_order_area.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_area.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_area, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SalesStructure" Then
        '                    nbi_sales_order_sales_struktur.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_sales_struktur.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_sales_struktur, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DSPeriode" Then
        '                    nbi_sales_order_periode_intensif_ds.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_periode_intensif_ds.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_periode_intensif_ds, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "DSRule" Then
        '                    nbi_sales_order_rule_ds.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_rule_ds.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_rule_ds, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "InsentifDS" Then
        '                    nbi_sales_order_intensif_ds.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_intensif_ds.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_intensif_ds, .DataReader("enablestatus"))

        '                ElseIf .DataReader("menuname").ToString = "RSControlFile" Then
        '                    nbi_sales_order_cf_rs.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_cf_rs.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_cf_rs, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RSPeriode" Then
        '                    nbi_sales_order_periode_intensif_rs.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_periode_intensif_rs.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_periode_intensif_rs, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RSRule" Then
        '                    nbi_sales_order_rule_rs.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_rule_rs.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_rule_rs, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RSInsentif" Then
        '                    nbi_sales_order_intensif_rs.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_intensif_rs.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_intensif_rs, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RoyaltiRule" Then
        '                    nbi_sales_order_royalti_rule.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_royalti_rule.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_royalti_rule, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "RoyaltiPeriode" Then
        '                    nbi_sales_order_royalti_periode.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_royalti_periode.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_royalti_periode, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Royalti" Then
        '                    nbi_sales_order_royalti.Visible = .DataReader("visiblestatus")
        '                    nbi_sales_order_royalti.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_sales_order_royalti, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SOARFPReport" Then
        '                    nbi_so_faktur_pajak_report.Visible = .DataReader("visiblestatus")
        '                    nbi_so_faktur_pajak_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_so_faktur_pajak_report, .DataReader("enablestatus"))

        '                    'manufacturing
        '                ElseIf .DataReader("menuname").ToString = "ProductStructure" Then
        '                    nbi_manu_prod_structure.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_prod_structure.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_prod_structure, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ProdStrucTree" Then
        '                    nbi_manu_prod_structure_report.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_prod_structure_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_prod_structure_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WhereInUsedReport" Then
        '                    nbi_manu_where_in_used.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_where_in_used.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_where_in_used, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "PSRawMat" Then
        '                    nbi_manu_materials_summary_report.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_materials_summary_report.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_materials_summary_report, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SimulatedPickList" Then
        '                    nbi_manu_simulated_picklist.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_simulated_picklist.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_simulated_picklist, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ItemSub" Then
        '                    nbi_manu_item_subtitute.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_item_subtitute.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_item_subtitute, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Departemen" Then
        '                    nbi_manu_departement.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_departement.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_departement, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Wc" Then
        '                    nbi_manu_work_center.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_work_center.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_work_center, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ToolCodeMstr" Then
        '                    nbi_manu_tools.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_tools.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_tools, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "Routing" Then
        '                    nbi_manu_routing.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_routing.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_routing, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WorkOrder" Then
        '                    nbi_manu_work_order.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_work_order.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_work_order, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WORelease" Then
        '                    nbi_manu_wo_release.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_wo_release.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_wo_release, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WorkOrderIssue" Then
        '                    nbi_manu_wo_comp_issue.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_wo_comp_issue.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_wo_comp_issue, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WOBillMaintenance" Then
        '                    nbi_manu_wo_bill.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_wo_bill.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_wo_bill, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "WOReceipt" Then
        '                    nbi_manu_wo_receipt.Visible = .DataReader("visiblestatus")
        '                    nbi_manu_wo_receipt.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_manu_wo_receipt, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ProjectMaintenance" Then
        '                    nbi_prj_project.Visible = .DataReader("visiblestatus")
        '                    nbi_prj_project.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_prj_project, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "POCustomer" Then
        '                    nbi_prj_po_customer.Visible = .DataReader("visiblestatus")
        '                    nbi_prj_po_customer.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_prj_po_customer, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "ProjectType" Then
        '                    nbi_prj_project_type.Visible = .DataReader("visiblestatus")
        '                    nbi_prj_project_type.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_prj_project_type, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "SoPjAreaId" Then
        '                    nbi_prj_project_area.Visible = .DataReader("visiblestatus")
        '                    nbi_prj_project_area.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_prj_project_area, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "BillOfQuantity" Then
        '                    nbi_prj_boq.Visible = .DataReader("visiblestatus")
        '                    nbi_prj_boq.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_prj_boq, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "BoQApproval" Then
        '                    nbi_prj_boq_approval.Visible = .DataReader("visiblestatus")
        '                    nbi_prj_boq_approval.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_prj_boq_approval, .DataReader("enablestatus"))
        '                ElseIf .DataReader("menuname").ToString = "BoQtoPR" Then
        '                    nbi_pr_boq.Visible = .DataReader("visiblestatus")
        '                    nbi_pr_boq.Enabled = .DataReader("enablestatus")
        '                    strikeout(nbi_pr_boq, .DataReader("enablestatus"))
        '                End If
        '            End While
        '        End With
        '    End Using

        '    ' group yg tidak ada itemnya di hide dan dock panel yg tidak ada menunya juga di hide
        '    dpb = Nothing
        '    For Each ctrl In Me.Controls
        '        Dim status_dp As Boolean
        '        If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
        '            dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
        '            For Each ctrl_dp In dp.Controls
        '                status_dp = False
        '                If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.DockPanel Then
        '                    dpb = CType(ctrl_dp, DevExpress.XtraBars.Docking.DockPanel)

        '                    For Each ctrl_cc In dpb.Controls
        '                        If TypeOf ctrl_cc Is DevExpress.XtraBars.Docking.ControlContainer Then
        '                            cc = CType(ctrl_cc, DevExpress.XtraBars.Docking.ControlContainer)
        '                            For Each ctrl_ccd In cc.Controls
        '                                If TypeOf ctrl_ccd Is DevExpress.XtraNavBar.NavBarControl Then
        '                                    nbc = CType(ctrl_ccd, DevExpress.XtraNavBar.NavBarControl)

        '                                    For Each nbg As NavBarGroup In nbc.Groups
        '                                        Dim status_nbi As Boolean
        '                                        status_nbi = False
        '                                        For x As Integer = 0 To nbg.ItemLinks.Count - 1
        '                                            If nbg.ItemLinks(x).Visible = True Then
        '                                                status_nbi = True
        '                                                status_dp = True
        '                                            End If
        '                                        Next
        '                                        nbg.Expanded = True
        '                                        If status_nbi = False Then
        '                                            nbg.Visible = False
        '                                        End If

        '                                    Next

        '                                End If
        '                            Next
        '                        End If
        '                    Next

        '                End If
        '                If status_dp = False Then
        '                    dpb.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        '                End If
        '            Next
        '        End If
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Sub strikeout(ByVal par_obj As DevExpress.XtraNavBar.NavBarItem, ByVal par_status As Boolean)
        If par_status = False Then
            par_obj.SmallImageIndex = 0
            par_obj.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Else
            par_obj.SmallImageIndex = 1
            par_obj.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular)
        End If
    End Sub

    Public Overrides Sub sytle_mainmenu(ByVal par_caption As String)
        'nbc_master_data.PaintStyleName = master_new.ClsVar.SMainMenuStyle
        'nbc_master_data.ResetStyles()

        'nbc_distribution.PaintStyleName = master_new.ClsVar.SMainMenuStyle
        'nbc_distribution.ResetStyles()

        'nbc_financial.PaintStyleName = master_new.ClsVar.SMainMenuStyle
        'nbc_financial.ResetStyles()

        'nbc_sales_order.PaintStyleName = master_new.ClsVar.SMainMenuStyle
        'nbc_sales_order.ResetStyles()

        'nbc_financial.PaintStyleName = master_new.ClsVar.SMainMenuStyle
        'nbc_financial.ResetStyles()
    End Sub

    Public Overrides Sub InitDockPanel()
        'status_visible_dock_panel("dp_masterdata", False)
        'status_visible_dock_panel("dp_distribution", False)
        'status_visible_dock_panel("dp_financial", False)
        'status_visible_dock_panel("dp_sales_order", False)
        'status_visible_dock_panel("dp_manufacturing", False)

        'status_visible_dock_panel("dp_erp_masterdata", True)
        'status_visible_dock_panel("dp_erp_distribution", True)
        'status_visible_dock_panel("dp_manufacturing", True)
        'status_visible_dock_panel("dp_financial", True)
        'status_visible_dock_panel("dp_customer_service", True)
        'status_visible_dock_panel("dp_syspro_financial", True)
        'status_visible_dock_panel("dp_syspro_additional", True)
    End Sub

    Public Overrides Sub status_visible_dock_panel(ByVal par_nama As String, ByVal par_status As Boolean)
        'If par_nama = "dp_masterdata" And par_status = True Then
        '    dp_master_data.Close()
        'ElseIf par_nama = "dp_masterdata" And par_status = False Then
        '    dp_master_data.Show()
        'ElseIf par_nama = "dp_distribution" And par_status = True Then
        '    dp_distribution.Close()
        'ElseIf par_nama = "dp_distribution" And par_status = False Then
        '    dp_distribution.Show()
        'ElseIf par_nama = "dp_financial" And par_status = True Then
        '    dp_financial.Close()
        'ElseIf par_nama = "dp_financial" And par_status = False Then
        '    dp_financial.Show()
        'ElseIf par_nama = "dp_sales_order" And par_status = True Then
        '    dp_sales_order.Close()
        'ElseIf par_nama = "dp_sales_order" And par_status = False Then
        '    dp_sales_order.Show()

        'ElseIf par_nama = "dp_manufacturing" And par_status = True Then
        '    dp_manufacturing.Close()
        'ElseIf par_nama = "dp_manufacturing" And par_status = False Then
        '    dp_manufacturing.Show()
        'End If
    End Sub

    Public Overrides Sub load_server()
        Try
            'With frmServer
            '    .Show()
            '    .Hide()
            'End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#Region "Master Data"
    Private Sub nbi_master_data_domain_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_master_data_domain.LinkClicked
        Dim frm As New FDomain
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_master_data_agama_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_master_data_agama.LinkClicked
        Dim frm As New FAgama
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_org_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_org_type.LinkClicked
        Dim frm As New FOrganizationType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_organization_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_organization.LinkClicked
        Dim frm As New FOrganization
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_organization_structure_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_organization_structure.LinkClicked
        Dim frm As New FOrganizationStructure
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_org_struc_detail_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_org_struc_detail.LinkClicked
        Dim frm As New FOrganizationStructureDetail
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_organizationtree_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_organizationtree.LinkClicked
        Dim frm As New FOrganizationTree
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_partner_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_partner.LinkClicked
        Dim frm As New FPartner
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_fin_ap_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs)
        Dim frm As New FAccountPayableType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_inventorystatus_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_inventorystatus.LinkClicked
        Dim frm As New FInventoryStatus
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_itemstatus_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_itemstatus.LinkClicked
        Dim frm As New FItemStatus
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_location_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_location.LinkClicked
        Dim frm As New FLocation
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_location_category_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_location_category.LinkClicked
        Dim frm As New FLocationCategory
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_location_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_location_type.LinkClicked
        Dim frm As New FLocationType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_warehouse_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_warehouse.LinkClicked
        Dim frm As New FWarehouse
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_warehouse_category_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_warehouse_category.LinkClicked
        Dim frm As New FWarehouseCategory
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_warehouse_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_warehouse_type.LinkClicked
        Dim frm As New FWarehouseType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_partnumber_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_partnumber.LinkClicked
        Dim frm As New FPartNumber
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub



    Private Sub nbi_mst_is_approval_tax_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_approval_tax.LinkClicked
        Try
            Dim frm As New FPartNumberApprovalTax
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_mst_is_approval_accounting_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_approval_accounting.LinkClicked
        Try
            Dim frm As New FPartNumberApprovalAccounting
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_mst_is_part_number_group_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_part_number_group.LinkClicked
        Dim frm As New FPartNumberGroupForecast
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_taxclass_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_taxclass.LinkClicked
        Dim frm As New FTaxClass
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_productline_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_productline.LinkClicked
        Dim frm As New FProductLine
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_prodline_location_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_prodline_location.LinkClicked
        Dim frm As New FInventoryAccount
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_unitmeasure_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_unitmeasure.LinkClicked
        Dim frm As New FUnitMeasure
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_site_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_site.LinkClicked
        Dim frm As New FSite
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_is_item_site_cost_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_item_site_cost.LinkClicked
        Dim frm As New FItemSiteCost
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_partner_group_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_partner_group.LinkClicked
        Dim frm As New FPartnerGroup
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_comp_company_address_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_comp_company_address.LinkClicked
        Dim frm As New FCompanyAddress
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_comp_employee_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_comp_employee.LinkClicked
        Dim frm As New FEmployee
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_comp_tran_status_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_comp_tran_status.LinkClicked
        Dim frm As New FTransactionStatus
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_emp_transaction_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_emp_transaction.LinkClicked
        Dim frm As New FTransaction
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_partner_bank_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_partner_bank.LinkClicked
        Dim frm As New FPartnerBank
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_partner_address_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_partner_address.LinkClicked
        Dim frm As New FPartnerAddress
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_partner_cp_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_partner_cp.LinkClicked
        Dim frm As New FPartnerContactPerson
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_partner_all_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_partner_all.LinkClicked
        Dim frm As New FPartnerAll
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_address_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_address_type.LinkClicked
        Dim frm As New FAddressType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_cp_function_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_cp_function.LinkClicked
        Dim frm As New FContactPersonFunction
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_fin_credit_terms_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_fin_credit_terms.LinkClicked
        Dim frm As New FCreditTerms
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
  
    Private Sub nbi_mst_at_tax_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_tax_type.LinkClicked
        Dim frm As New FTaxType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_at_tax_rate_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_at_tax_rate.LinkClicked
        Dim frm As New FTaxRate
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_mst_fin_gl_calendar_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs)
        Dim frm As New FGLCalendar
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_company_position_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_company_position.LinkClicked
        Dim frm As New FPosition
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_company_conf_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_company_conf.LinkClicked
        Dim frm As New FConfFile
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_company_routing_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_company_routing_approval.LinkClicked
        Dim frm As New FRoutingApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_company_dok_aprv_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_company_dok_aprv.LinkClicked
        Dim frm As New FDocumentApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
#End Region

#Region "Distribution"
    Private Sub nbi_dist_req_mstr_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_mstr.LinkClicked
        Dim frm As New FRequisition
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_req_mstr_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_mstr_approval.LinkClicked
        Dim frm As New FRequisitionApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_req_mstr_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_mstr_print.LinkClicked
        Dim frm As New FRequisitionPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_req_transfer_issue_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_transfer_issue.LinkClicked
        Dim frm As New FReqTransferIssue
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_req_transfer_issue_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_transfer_issue_approval.LinkClicked
        Dim frm As New FReqTransferIssueApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_req_transfer_issue_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_transfer_issue_print.LinkClicked
        Dim frm As New FReqTransferIssuePrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_req_transfer_receipt_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_transfer_receipt.LinkClicked
        Dim frm As New FReqTransferReceipt
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_req_transfer_receipt_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_req_transfer_receipt_print.LinkClicked
        Dim frm As New FReqTransferReceiptPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_order_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_order.LinkClicked
        Dim frm As New FPurchaseOrder
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_order_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_order_print.LinkClicked
        Dim frm As New FPurchaseOrderPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_order_print_no_cost_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_order_print_no_cost.LinkClicked
        Dim frm As New FPurchaseOrderPrintNoCost
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_order_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_order_report.LinkClicked
        Dim frm As New FPurchaseOrderReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_receipt_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_receipt.LinkClicked
        Dim frm As New FPurchaseReceipt
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_receipt_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_receipt_print.LinkClicked
        Dim frm As New FPurchaseReceiptPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_receipt_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_receipt_report.LinkClicked
        Dim frm As New FPurchaseReceiptReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_inventory_detail_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_inventory_detail.LinkClicked
        Dim frm As New FInventoryReportDetail
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_inventory_detail_2_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_inventory_detail_2.LinkClicked
        Dim frm As New FInventoryReportDetailLog
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_inventory_by_eff_date_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_inventory_by_eff_date.LinkClicked
        Dim frm As New FInventoryReportDate
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_request_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_request.LinkClicked
        Dim frm As New FInventoryRequest
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_request_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_request_print.LinkClicked
        Dim frm As New FInventoryRequestPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_receipts_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_receipts.LinkClicked
        Dim frm As New FInventoryReceipts
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_receipts_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_receipts_print.LinkClicked
        Dim frm As New FInvReceiptsPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_cycle_count_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_cycle_count.LinkClicked
        Dim frm As New FInventoryCycleCount
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_issues_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_issues.LinkClicked
        Dim frm As New FInventoryIssues
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_issues_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_issues_print.LinkClicked
        Dim frm As New FInvIssuePrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_transfer_issues_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_transfer_issues.LinkClicked
        Dim frm As New FTransferIssues
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub


    Private Sub nbi_inv_transfer_issues_print_out_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_transfer_issues_print_out.LinkClicked
        Dim frm As New FTransferIssuesPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_transfer_receipts_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_transfer_receipts.LinkClicked
        Dim frm As New FTransferReceipts
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_transfer_receipts_print_out_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_transfer_receipts_print_out.LinkClicked
        Dim frm As New FTransferReceiptsPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_inventory_adjustment_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_inventory_begining_balance.LinkClicked
        Dim frm As New FInventoryBeginingBalance
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_return_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_return.LinkClicked
        Dim frm As New FPurchaseReturn
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_purchase_return_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_purchase_return_print.LinkClicked
        Dim frm As New FPurchaseReturnPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_dist_reason_code_return_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_reason_code_return.LinkClicked
        Dim frm As New FReasonCodeReturn
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_adjusment_plus_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_adjusment_plus.LinkClicked
        Dim frm As New FInventoryAdjustmentPlus
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_adjustment_minus_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_adjustment_minus.LinkClicked
        Dim frm As New FInventoryAdjustmentMinus
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_adjusment_plus_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_adjusment_plus_report.LinkClicked
        Dim frm As New FInventoryAdjusmentPlusReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_adjustment_minus_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_adjustment_minus_report.LinkClicked
        Dim frm As New FInventoryAdjusmentMinusReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_transfer_issues_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_transfer_issues_approval.LinkClicked
        Dim frm As New FTransferIssueApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_request_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_request_approval.LinkClicked
        Dim frm As New FInventoryRequestApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_inv_transfer_issues_return_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_transfer_issues_return.LinkClicked
        Try
            Dim frm As New FTransferIssuesReturn
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub nbi_dist_inv_history_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_dist_inv_history.LinkClicked
        Try
            Dim frm As New FInventoryHistory
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
#End Region

#Region "Financial"
    Private Sub nbi_fin_gl_standard_transaction_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_standard_transaction.LinkClicked
        Dim frm As New FStandardTransaction
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_unposted_transaction_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_unposted_transaction.LinkClicked
        Dim frm As New FUnpostedTransaction
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_transaction_post_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_transaction_post.LinkClicked
        Dim frm As New FTransactionPost
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_voucher_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_voucher.LinkClicked
        Dim frm As New FVoucher
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_voucher_tax_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_voucher_tax.LinkClicked
        Dim frm As New FVoucherTax
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_type.LinkClicked
        Dim frm As New FAccountPayableType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_payment_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_payment.LinkClicked
        Dim frm As New FPaymentManualChecks
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_voucher_report_by_aging_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_voucher_report_by_aging.LinkClicked
        Dim frm As New FVoucherReportByAging
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_voucher_report_by_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_voucher_report_by_type.LinkClicked
        Dim frm As New FVoucherReportByType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_voucher_report_by_unvouchered_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_voucher_report_by_unvouchered.LinkClicked
        Dim frm As New FUnvoucheredPOReceipt
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ap_voucher_report_by_top_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_voucher_report_by_top.LinkClicked
        Dim frm As New FVoucherReportByTop
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_mc_exchange_rate_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_mc_exchange_rate.LinkClicked
        Dim frm As New FExchangeRate
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_account_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_account.LinkClicked
        Dim frm As New FAccount
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_cost_center_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_cost_center.LinkClicked
        Dim frm As New FCostCenter
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_cost_center_user_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_cost_center_user.LinkClicked
        Dim frm As New FCostCenterUser
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_cost_center_account_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_cost_center_account.LinkClicked
        Dim frm As New FRelationAccToCostCnt
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_entity_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_entity.LinkClicked
        Dim frm As New FEntity
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_entity_group_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_entity_group.LinkClicked
        Dim frm As New FEntityGroup
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_glcalendar_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_glcalendar.LinkClicked
        Dim frm As New FGLCalendar
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_project_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_project.LinkClicked
        Dim frm As New FProjectAccount
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_subaccount_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_subaccount.LinkClicked
        Dim frm As New FSubAccount
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_mc_bank_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_mc_bank.LinkClicked
        Dim frm As New FBank
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_mc_currency_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_mc_currency.LinkClicked
        Dim frm As New FCurrency
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_general_ledger_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_general_ledger_report.LinkClicked
        Dim frm As New FGeneralLedgerReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_profit_loss_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_profit_loss_report.LinkClicked
        Dim frm As New FProfitLossReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_trial_balance_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_trial_balance_report.LinkClicked
        'Box("Menu under developing")
        Dim frm As New FTrialBalanceReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_balance_sheet_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_balance_sheet.LinkClicked
        Dim frm As New FBalanceSheetReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_cash_flow_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_cash_flow.LinkClicked
        Dim frm As New FCashFlowReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_opening_balance_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_opening_balance.LinkClicked
        Dim frm As New FOpeningBalance
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_month_end_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_month_end.LinkClicked
        Dim frm As New FMonthEnd
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_gl_year_end_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_gl_year_end.LinkClicked
        Dim frm As New FYearEnd
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_dc_memo_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_dc_memo.LinkClicked
        Dim frm As New FDRCRMemo
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_payment_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_payment.LinkClicked
        Dim frm As New FARPayment
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_dc_memo_detail_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_dc_memo_detail.LinkClicked
        Dim frm As New FDRCRMemoDetail
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_payment_detail_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_payment_detail.LinkClicked
        Dim frm As New FARPaymentDetail
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_dc_memo_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_dc_memo_report.LinkClicked
        Dim frm As New FDRCRMemoReportOld
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
    'tambahan tanggal 09/06/2011
    Private Sub nbi_fin_ar_dc_memo_detail_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_dc_memo_detail_report.LinkClicked
        Dim frm As New FDRCRMemoReportDetail
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_type.LinkClicked
        Dim frm As New FARType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_dc_memo_print_out_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_dc_memo_print_out.LinkClicked
        Dim frm As New FDRCRMemoPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_dc_memo_konsiyasi_print_out_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_dc_memo_konsiyasi_print_out.LinkClicked
        Dim frm As New FDRCRMemoKonsiyasiPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_faktur_pajak_transaction_code_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_faktur_pajak_transaction_code.LinkClicked
        Dim frm As New FFakturPajakTransactionCode
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_faktur_pajak_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_faktur_pajak.LinkClicked
        Dim frm As New FFakturPajak
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_faktur_pajak_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_faktur_pajak_approval.LinkClicked
        Dim frm As New FFakturPajakApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_faktur_pajak_sign_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_faktur_pajak_sign.LinkClicked
        Dim frm As New FPajakSignUser
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_faktur_pajak_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_faktur_pajak_print.LinkClicked
        Dim frm As New FFakturPajakPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_so_ar_fp_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_so_ar_fp_report.LinkClicked
        Dim frm As New FSOARFPReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_report_by_aging_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_report_by_aging.LinkClicked
        Dim frm As New FARReportByAging
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_report_by_aging_sdi_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_report_by_aging_sdi.LinkClicked
        Dim frm As New FARReportByAgingSDI
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_report_by_top_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_report_by_top.LinkClicked
        Dim frm As New FARReportByTop
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_report_pending_invoice_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_report_pending_invoice.LinkClicked
        Dim frm As New FARPendingInvoice
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_ar_cash_in_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_cash_in_report.LinkClicked
        Dim frm As New FCashInReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_bgt_periode_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_bgt_periode.LinkClicked
        Dim frm As New FBdgtPeriode
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_bgt_generate_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_bgt_generate.LinkClicked
        Dim frm As New FBudgeting
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_bgt_maintenance_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_bgt_maintenance.LinkClicked
        Dim frm As New FBudgetDetail
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_bgt_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_bgt_approval.LinkClicked
        Dim frm As New FBudgetApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_bgt_cross_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_bgt_cross.LinkClicked
        Dim frm As New FCrossBudget
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_bgt_cross_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_bgt_cross_approval.LinkClicked
        Dim frm As New FCrossBudgetApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
    Private Sub nbi_mst_is_unit_conversion_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_mst_is_unit_conversion.LinkClicked
        Dim frm As New FUMConversion
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_bsSetting_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_bsSetting.LinkClicked
        Dim frm As New FBSSetting_old
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_plSetting_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_plSetting.LinkClicked
        Dim frm As New FPLSetting
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_fin_cfSetting_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_cfSetting.LinkClicked
        Dim frm As New FCashFlowSetting
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub


    Private Sub nbi_fin_ar_dbcr_balance_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ar_dbcr_balance_report.LinkClicked
        Dim frm As New FDRCRMemoBalanceReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub


    Private Sub nbi_fin_ap_voucher_balance_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_ap_voucher_balance_report.LinkClicked
        Dim frm As New FVoucherBalanceReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_cash_transfer_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_cash_transfer.LinkClicked
        Dim frm As New FTransfer
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub


    Private Sub nbi_cash_out_LinkClicked(ByVal sender As Object, ByVal e As NavBarLinkEventArgs) Handles nbi_cash_out.LinkClicked
        Dim frm As New FCashOut
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_cash_in_LinkClicked(ByVal sender As Object, ByVal e As NavBarLinkEventArgs) Handles nbi_cash_in.LinkClicked
        Dim frm As New FCashIn
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub


    Private Sub nbi_cash_bank_reconciliation_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_cash_bank_reconciliation.LinkClicked
        Try
            Dim frm As New FReconciliation
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

#End Region

#Region "Sales Order"
    Private Sub nbi_so_promo_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_so_promo.LinkClicked
        Dim frm As New FPromo
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_pricelist_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_pricelist.LinkClicked
        Dim frm As New FPriceList
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_pricelist_header_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_pricelist_header.LinkClicked
        Dim frm As New FPriceListHeader
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_pricelist_detail_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_pricelist_detail.LinkClicked
        Dim frm As New FPriceListDetail
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_pricelist_copy_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_pricelist_copy.LinkClicked
        Dim frm As New FPriceListCopy
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_sales_program_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_sales_program.LinkClicked
        Dim frm As New FSalesProgram
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_pay_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_pay_type.LinkClicked
        Dim frm As New FPaymentType
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_payment_methode_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_payment_methode.LinkClicked
        Dim frm As New FPaymentMethode
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_so_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_so.LinkClicked
        Dim frm As New FSalesOrder
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_so_credit_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_so_credit_report.LinkClicked
        Dim frm As New FSalesOrderCreditReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_so_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_so_approval.LinkClicked
        Dim frm As New FSalesOrderApproval
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_so_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_so_print.LinkClicked
        Dim frm As New FSalesOrderPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_so_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_so_report.LinkClicked
        Dim frm As New FSalesOrderReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_so_cash_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_faktur_penjualan_print.LinkClicked
        Dim frm As New FSalesOrderFakturPenjualanPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_manual_allocation_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_manual_allocation.LinkClicked
        Dim frm As New FSalesOrderManualAllocation
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_intensif_ds_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_intensif_ds.LinkClicked
        Dim frm As New FInsentifDS
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_periode_intensif_ds_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_periode_intensif_ds.LinkClicked
        Dim frm As New FDSPeriode
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_rule_ds_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_rule_ds.LinkClicked
        Dim frm As New FDSRule
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_shipments_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_shipments.LinkClicked
        Dim frm As New FSalesOrderShipment
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_shipments_print_out_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_shipments_print_out.LinkClicked
        Dim frm As New FSalesOrderShipmentPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_returns_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_returns.LinkClicked
        Dim frm As New FSalesOrderReturn
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_returns_print_out_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_returns_print_out.LinkClicked
        Dim frm As New FSalesOrderReturnPrint
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_reason_code_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_reason_code.LinkClicked
        Dim frm As New FReasonCodeSO
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_area_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_area.LinkClicked
        Dim frm As New FArea
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_sales_struktur_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_sales_struktur.LinkClicked
        Dim frm As New FSalesStructure
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

#Region "Insentif & Royalti"
    Private Sub nbi_sales_order_cf_rs_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_cf_rs.LinkClicked
        Dim frm As New FRSControlFile
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_periode_intensif_rs_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_periode_intensif_rs.LinkClicked
        Dim frm As New FRSPeriode
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_rule_rs_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_rule_rs.LinkClicked
        Dim frm As New FRSRule
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_intensif_rs_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_intensif_rs.LinkClicked
        Dim frm As New FRSInsentif
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_royalti_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_royalti_rule.LinkClicked
        Dim frm As New FRoyaltiRule
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_royalti_periode_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_royalti_periode.LinkClicked
        Dim frm As New FRoyaltiPeriode
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_sales_order_royalti_LinkClicked1(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_sales_order_royalti.LinkClicked
        Dim frm As New FRoyalti
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
#End Region

#End Region

#Region "Manufacturing"
    Private Sub nbi_manu_prod_structure_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_prod_structure.LinkClicked
        Try
            Dim frm As New FProductStructure
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub nbi_manu_prod_structure_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_prod_structure_report.LinkClicked
        Try
            Dim frm As New FProdStrucTree
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_where_in_used_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_where_in_used.LinkClicked
        Try
            Dim frm As New FWhereInUsedReport
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_materials_summary_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_materials_summary_report.LinkClicked
        Try
            Dim frm As New FPSRawMat
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_simulated_picklist_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_simulated_picklist.LinkClicked
        Try
            Dim frm As New FSimulatedPickList
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_item_subtitute_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_item_subtitute.LinkClicked
        Try
            Dim frm As New FItemSub
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_departement_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_departement.LinkClicked
        Try
            Dim frm As New FDepartemen
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_work_center_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_work_center.LinkClicked
        Try
            Dim frm As New FWc
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_tools_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_tools.LinkClicked
        Try
            Dim frm As New FToolCodeMstr
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_man_department_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_man_department.LinkClicked
        
    End Sub

    Private Sub nbi_manu_routing_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_routing.LinkClicked
        Try
            Dim frm As New FRouting
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_man_workcenter_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_man_workcenter.LinkClicked
        Try
            Dim frm As New FWorkCenter
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_work_order_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_work_order.LinkClicked
        Try
            Dim frm As New FWorkOrder
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_wo_release_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_wo_release.LinkClicked
        Try
            Dim frm As New FWORelease
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_wo_comp_issue_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_wo_comp_issue.LinkClicked
        Try
            Dim frm As New FWorkOrderIssue
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_wo_bill_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_wo_bill.LinkClicked
        Try
            Dim frm As New FWOBillMaintenance
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_manu_wo_receipt_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_wo_receipt.LinkClicked
        Try
            Dim frm As New FWOReceipt
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_prj_project_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_prj_project.LinkClicked
        Try
            Dim frm As New FProjectMaintenance
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_prj_po_customer_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_prj_po_customer.LinkClicked
        Try
            Dim frm As New FPOCustomer
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_prj_project_type_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_prj_project_type.LinkClicked
        Try
            Dim frm As New FProjectType
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_prj_project_area_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_prj_project_area.LinkClicked
        Try
            Dim frm As New FSoPjAreaId
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_prj_boq_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_prj_boq.LinkClicked
        Try
            Dim frm As New FBillOfQuantity
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_prj_boq_approval_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_prj_boq_approval.LinkClicked
        Try
            Dim frm As New FBoQApproval
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_pr_boq_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_pr_boq.LinkClicked
        Try
            Dim frm As New FBoQtoPR
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
#End Region

    Private Sub FMainMenu_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            If master_new.ClsVar.CExit <> False Then
                If DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure to close this application?", "Confirm ...", _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                    e.Cancel = True
                    master_new.ClsVar.CExit = True
                    Exit Sub
                Else
                    'If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "xmpp_on") = "1" Then
                    '    Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("PidginPortable")
                    '    For Each p As Process In pProcess
                    '        p.Kill()
                    '    Next
                    'End If
                    'End
                    master_new.ClsVar.CExit = False
                    Global.System.Windows.Forms.Application.Exit()
                End If
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub Load_Form()
        'Dim sSQL As String
        'Dim _user_name As String

        'sSQL = "select userpidgin from tconfuser where userid=" & master_new.ClsVar.sUserID

        '_user_name = SetString(GetRowInfo(sSQL)(0).ToString)

        'If SaveTextToFile(_user_name, appbase() & "\filekonfigurasi\user_active.txt") = False Then
        '    Exit Sub
        'End If

        Dim func_coll As New function_collection
        MyBase.Load_Form()

        iText.Caption = iText.Caption & ", Create Jurnal : " & func_coll.get_create_jurnal_status.ToString & ", version : " & master_new.ModFunction.GetVersion

        'BarStaticItem5.Caption = "jkahakdh ahd kgadgasgd ajdg adgajgdjagdgjagdj ajdjagjdgjagdjgajgd agadgjagsjdgja d jasgda jdgaj dgasd jad jadsgja djga dagd"

        'Dim frm As New FWhatsNew
        'frm.MdiParent = Me
        'frm.Show()

        find_form()
        cek_warning()
        If _number_warning > 0 Then
            BarStaticItem5.Caption = _number_warning & " pending invoice"
            BarStaticItem5.Appearance.ForeColor = Color.Red
            Timer2.Enabled = True
        Else
            BarStaticItem5.Caption = "-"
            BarStaticItem5.Appearance.ForeColor = Color.Black
            Timer2.Enabled = False
        End If
        '    Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
        '    With myprocess
        '        .StartInfo.FileName = My.Application.Info.DirectoryPath & "\SygmaChat.exe"
        '        .Start()
        '    End With
        'End If

        'frm.Hide()

        If dt_menu.Rows.Count = 0 Then
            ''Box("Please update application")
            'Dim frm As New frmUpdate
            'frm.MdiParent = Me
            'frm.Show()

            'Box("Please update application")
            Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()

            With myprocess
                .StartInfo.FileName = GetCurrentPath() & "UpdateProgram.exe"
                If System.IO.File.Exists(.StartInfo.FileName) Then
                    .StartInfo.RedirectStandardOutput = True
                    .StartInfo.UseShellExecute = False
                    .Start()
                Else
                    Box("File " & .StartInfo.FileName & " tidak ada")
                End If

            End With

        End If

    End Sub

    Public Overrides Sub show_user_history()
        Try
            Dim frm As New FUserHistory
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Overrides Sub chat_syspro()
        'Dim frm As New FUserHistory
        'frm.MdiParent = Me
        'frm.set_window(Me)
        'frm.type_form = True
        'frm.Show()
        find_form_manual()
    End Sub

    Public Overrides Sub execute_teamviewer()
        Try


            Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
            With myprocess
                .StartInfo.FileName = My.Application.Info.DirectoryPath & "\zpendukung\TeamViewerQS.exe"
                '.StartInfo.RedirectStandardOutput = False
                '.StartInfo.UseShellExecute = False
                '.StartInfo.CreateNoWindow = True
                '.StartInfo.UseShellExecute = False
                '.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                .Start()
                '.WaitForExit()
                '.Close()
            End With
            Box("Informasikan ID dan Password Teamviewer kepada IT, tunggu beberapa saat sampai muncul ID dan Password")


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub nbi_inv_isues_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_isues_report.LinkClicked
        Try
            Dim frm As New FInventoryIssuesReport
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_inv_receipts_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_receipts_report.LinkClicked
        Try
            Dim frm As New FInventoryReceiptsReport
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_so_ship_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_so_ship_report.LinkClicked
        Try
            Dim frm As New FSalesOrderShipmentReport
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub nbi_so_retur_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_so_retur_report.LinkClicked
        Try
            Dim frm As New FSalesOrderReturnReport
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub nbi_fin_add_account_glbal_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_add_account_glbal.LinkClicked
        Try
            Dim frm As New FAddAcountToGLBal
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Now.Second = 10 And Now.Minute Mod 2 = 0 Then
            find_form()

            'BarStaticItem5.Caption = "ksbfkbsfb   sfkskfdg k  ksfksdfkskf ks dfksfsdgfgskfk  skfksdgfgskdgf skfgsgdfkskgf skgfksgf sfgksgf skgdfksgkfgksdfg s"
            cek_warning()
            If _number_warning > 0 Then
                BarStaticItem5.Caption = _number_warning & " pending invoice"
                BarStaticItem5.Appearance.ForeColor = Color.Red
                Timer2.Enabled = True
            Else
                BarStaticItem5.Caption = "-"
                BarStaticItem5.Appearance.ForeColor = Color.Black
                Timer2.Enabled = False
            End If
        End If

       
    End Sub
    Public Sub cek_warning()
        Dim ssql As String
        Try
            
            ssql = "SELECT distinct " _
                & "  b.so_code, " _
                & "  c.ptnr_name, " _
                & "  b.so_date,en_desc " _
                & "FROM " _
                & "  public.so_mstr b " _
                & "  INNER JOIN public.ptnr_mstr c ON (b.so_ptnr_id_bill = c.ptnr_id) " _
                & "  INNER JOIN public.sod_det a ON (b.so_oid = a.sod_so_oid) " _
                & "  INNER JOIN public.en_mstr e ON (b.so_en_id = e.en_id) " _
                & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                & "WHERE " _
                & " coalesce(sod_qty_shipment,0) > coalesce(sod_qty_invoice,0) " _
                & " and so_date between " & SetDateNTime00(DateAdd(DateInterval.Month, -18, CDate("01/" & Format(master_new.PGSqlConn.CekTanggal, "MM/yyyy")))) & " and " & SetDateNTime00(EndOfMonth(master_new.PGSqlConn.CekTanggal, 0)) & " " _
                & "  and b.so_en_id in (select info_en_id from info_mstr where info_user_nama='" & master_new.ClsVar.sNama & "') and pay_type.code_usr_1 <> '0' " _
                & "  "


            Dim dt As New DataTable
            dt = GetTableData(ssql)

            _number_warning = 0
            If dt.Rows.Count > 0 Then
                _number_warning = dt.Rows.Count
            End If



        Catch ex As Exception

        End Try
    End Sub
    Public Function find_form_manual() As Boolean
        Try
            If Process.GetProcessesByName("PidginPortable").Length > 0 Then
                Return True
                Exit Function
            End If

            If Process.GetProcessesByName("Pidgin").Length > 0 Then
                Return True
                Exit Function
            End If
          
            Dim sSQL As String
            Dim _user_name As String

            sSQL = "select userpidgin from tconfuser where userid=" & master_new.ClsVar.sUserID

            _user_name = SetString(GetRowInfo(sSQL)(0).ToString)
            If _user_name.Trim = "" Then
                Return True
                Exit Function
            End If

            Dim _ip, _server As String
            _ip = GetRowInfo("select xmpp_ip from tconfsetting")(0).ToString
            _server = GetRowInfo("select xmpp_name from tconfsetting")(0).ToString

            SaveTextToFile(GetFileContents(appbase() & "\PidginPortable\Data\settings\.purple\accounts_default.xml").Replace("#user#", _
                          _user_name & "@" & _server & "/SYSPRO").Replace("#ipserver#", _ip), _
                           appbase() & "\PidginPortable\Data\settings\.purple\accounts.xml")

            'PidginPortable\Data\settings\.purple
            Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
            With myprocess
                .StartInfo.FileName = My.Application.Info.DirectoryPath & "\PidginPortable\PidginPortable.exe"
                .Start()
            End With

            Return True
            Exit Function

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function find_form() As Boolean
        Try
            If Process.GetProcessesByName("PidginPortable").Length > 0 Then
                Return True
                Exit Function
            End If

            If Process.GetProcessesByName("Pidgin").Length > 0 Then
                Return True
                Exit Function
            End If

            If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "xmpp_on") = "1" Then
                Dim sSQL As String
                Dim _user_name As String

                sSQL = "select userpidgin from tconfuser where userid=" & master_new.ClsVar.sUserID

                _user_name = SetString(GetRowInfo(sSQL)(0).ToString)
                If _user_name.Trim = "" Then
                    Return True
                    Exit Function
                End If
                Dim _ip, _server As String
                _ip = GetRowInfo("select xmpp_ip from tconfsetting")(0).ToString
                _server = GetRowInfo("select xmpp_name from tconfsetting")(0).ToString

                SaveTextToFile(GetFileContents(appbase() & "\PidginPortable\Data\settings\.purple\accounts_default.xml").Replace("#user#", _
                              _user_name & "@" & _server & "/" & master_new.ModFunction.IPAddresses(System.Net.Dns.GetHostName)).Replace("#ipserver#", _ip), _
                               appbase() & "\PidginPortable\Data\settings\.purple\accounts.xml")


                'PidginPortable\Data\settings\.purple
                Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
                With myprocess
                    .StartInfo.FileName = My.Application.Info.DirectoryPath & "\PidginPortable\PidginPortable.exe"
                    .Start()
                End With

                Return True
                Exit Function
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub nbi_so_faktur_pajak_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_so_faktur_pajak_report.LinkClicked
        Try
            Dim frm As New FSOARFPReport
            frm.MdiParent = Me
            frm.set_window(Me)
            frm.type_form = True
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    'penambahan tanggal 09/06/2011 - ra
    Private Sub nbi_fin_payment_ar_report_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_fin_payment_ar_report.LinkClicked
        Dim frm As New FARPaymentReport
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    '13/06/2011 penambahan menu po filem
    Private Sub nbi_purchase_order_filem_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_purchase_order_filem.LinkClicked
        Dim frm As New FPurchaseOrderFilm
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
    '13/06/2011 penambahan menu po filem
    Private Sub nbi_purchase_order_filem_print_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_purchase_order_filem_print.LinkClicked
        Dim frm As New FPurchaseOrderPrintFilm
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
    'har 20110804
    Private Sub nbi_inv_report_detail_wip_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_inv_report_detail_wip.LinkClicked
        Dim frm As New FInventoryReportDetailWIP
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_manu_wip_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_wip.LinkClicked
        Dim frm As New FWorkInProgress
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub

    Private Sub nbi_manu_trans_issue_wip_LinkClicked(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles nbi_manu_trans_issue_wip.LinkClicked
        Dim frm As New FTransferIssuesWIP
        frm.MdiParent = Me
        frm.set_window(Me)
        frm.type_form = True
        frm.Show()
    End Sub
    Private Sub test() Handles bbi_warning.ItemClick
        Timer2.Enabled = False
        bbi_warning.Enabled = True

        Dim frm As New FReminderWarning

        frm.MdiParent = Me
        frm.Show()
    End Sub
    Private Sub ceExpandCollaps_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ceExpandCollaps.EditValueChanged
        Try
            If ceExpandCollaps.EditValue = True Then
                TreeList1.ExpandAll()
                ceExpandCollaps.Text = "Expand All"
                'init_menu_tree()
            Else
                init_menu_tree()
                TreeList1.CollapseAll()
                ceExpandCollaps.Text = "Expand All"

            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub TreeList1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeList1.DoubleClick
        Try
            If Not TreeList1.FocusedNode.HasChildren Then
                CallForm("F" & TreeList1.FocusedNode("menuname"))
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub CallForm(ByVal par_form As String)
        Dim _assembly As Reflection.Assembly = Me.GetType.Assembly
        Dim _form As Form
        Try
            If par_form = "FMenuAuthorization" Or par_form = "FUserGroup" Or par_form = "FGroup" Or par_form = "FAddMenu" Then
                open_form(par_form)
            Else
                _form = _assembly.CreateInstance("sygma_solution_system" & "." & par_form.ToString())
                _form.MdiParent = Me
                _form.Show()
            End If


           
        Catch ex As Exception
            MessageBox.Show("Form not found", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    'Public Shared Sub open_form(ByVal frm_object As String, ByVal mdi As Form)
    '    Try

    '        'Dim objForm As Form
    '        'Dim sValue As String
    '        'Dim FullTypeName As String
    '        'Dim FormInstanceType As Type

    '        '' Form class name
    '        'sValue = frm_object

    '        '' Assume that form classes' namespace is the same as ProductName
    '        'FullTypeName = Application.ProductName & "." & sValue

    '        '' Now, get the actual type
    '        'FormInstanceType = Type.GetType(FullTypeName, True, True)
    '        '' Create an instance of this form type
    '        'objForm = CType(Activator.CreateInstance(FormInstanceType), Form)

    '        Dim _assembly As Reflection.Assembly = Me.get_
    '        Dim _form As Form = _assembly.CreateInstance(_assembly.GetName.Name & "." & frm_object())



    '        ' Show the form instance
    '        objForm.MdiParent = mdi
    '        objForm.Show()
    '        objForm.BringToFront()
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    Private Sub btSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSearch.Click
        If txtSearchMenu.Text <> "" Then
            searchString = txtSearchMenu.Text
            searchArray.Clear()
            searchIndex = -1

            Dim opS As MyNodeOperation_Search = New MyNodeOperation_Search(searchString)
            TreeList1.NodesIterator.DoLocalOperation(opS, TreeList1.Nodes)
            searchArray = opS.SearchList

            If searchArray.Count > 0 Then
                searchIndex = 0
                TreeList1.FocusedNode = searchArray(searchIndex)
                'staSearch.Text = (searchIndex + 1).ToString & " / " & searchArray.Count.ToString
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("No result found for [" & searchString & "] !", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'staSearch.Text = ""
            End If
        End If

        '_number_find = 0
        'Dim op As New TreeListOperationFindNodeByText(TreeList1.Columns(0), txtSearchMenu.Text)
        'op.par_number = _number_find
        'TreeList1.NodesIterator.DoOperation(op)
        'If Not op.Node Is Nothing Then
        '    TreeList1.FocusedNode = op.Node
        'End If
    End Sub

    Private Sub btFindNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFindNext.Click
        If txtSearchMenu.Text <> "" Then
            If searchArray.Count > 0 Then
                searchIndex += 1
                If searchIndex > (searchArray.Count - 1) Then
                    searchIndex = 0
                End If
                TreeList1.FocusedNode = searchArray(searchIndex)
                'staSearch.Text = (searchIndex + 1).ToString & " / " & searchArray.Count.ToString
            End If
        End If

        '_number_find = _number_find + 1

        'Dim op As New TreeListOperationFindNodeByText(TreeList1.Columns(0), txtSearchMenu.Text)
        'op.par_number = _number_find
        'TreeList1.NodesIterator.DoOperation(op)
        'If Not op.Node Is Nothing Then
        '    TreeList1.FocusedNode = op.Node
        'End If

    End Sub

    Private Sub TreeList1_GetSelectImage(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.GetSelectImageEventArgs) Handles TreeList1.GetSelectImage
        e.NodeImageIndex = e.Node.Level + 1
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If BarStaticItem5.Appearance.ForeColor = Color.Black Then
            bbi_warning.Enabled = True
            BarStaticItem5.Appearance.ForeColor = Color.Red

        Else
            bbi_warning.Enabled = True
            BarStaticItem5.Appearance.ForeColor = Color.Black
        End If
    End Sub
End Class

Class MyNodeOperation_Search

    Inherits TreeListOperation
    Private search_string As String
    Private search_count As Integer
    Private search_lst As New List(Of TreeListNode)


    Public Sub New(ByVal searchStr As String)
        Me.search_string = searchStr.ToUpper.Trim
        Me.search_count = 0
        Me.search_lst.Clear()
    End Sub

    Public Overrides Sub Execute(ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode)
        'If node.Item("partNumber").ToString.Contains(search_string) Or node.Item("description").ToString.Contains(search_string) Then
        If node.Item("menudesc").ToString.ToLower.Contains(search_string.ToLower) Then
            search_count += 1
            search_lst.Add(node)
        End If
    End Sub

    Public ReadOnly Property SearchCount() As Integer
        Get
            Return search_count
        End Get
    End Property

    Public ReadOnly Property SearchList() As List(Of TreeListNode)
        Get
            Return search_lst
        End Get
    End Property

End Class



Public Class CollapseAllChildrenOperation
    Inherits TreeListOperation
    Private processedNode As TreeListNode
    Private isProcessedNodeVisited As Boolean = False

    Public Sub New(ByVal processedNode As TreeListNode)
        MyBase.New()
        Me.processedNode = processedNode
    End Sub

    Public Overrides ReadOnly Property NeedsFullIteration() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Sub Execute(ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode)
        If node.HasAsParent(processedNode) OrElse node Is processedNode Then
            node.Expanded = False
        End If
    End Sub

    Public Overrides Function NeedsVisitChildren(ByVal node As TreeListNode) As Boolean
        If Not node.HasChildren Then
            Return False
        End If
        Return If(node Is processedNode, True, node.HasAsParent(processedNode) OrElse processedNode.HasAsParent(node))
    End Function

    Public Overrides Function CanContinueIteration(ByVal node As TreeListNode) As Boolean
        Return If(isProcessedNodeVisited, node Is processedNode OrElse node.HasAsParent(processedNode), True)
    End Function
End Class
