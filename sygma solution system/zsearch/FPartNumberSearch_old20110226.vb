Imports master_new.ModFunction

Public Class FPartNumberSearch
    Public _row, _en_id, _si_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Dim func_data As New function_data

    Private Sub FPartNumberSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  pt_cost, " _
                    & "  pt_price, " _
                    & "  pt_type, " _
                    & "  pt_um, " _
                    & "  pt_pl_id, " _
                    & "  pt_ls, " _
                    & "  pt_loc_id, " _
                    & "  loc_desc, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.pt_mstr" _
                    & " inner join en_mstr on en_id = pt_en_id " _
                    & " inner join loc_mstr on loc_id = pt_loc_id " _
                    & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                    & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and en_active ~~* 'Y'" _
                    & " and pt_en_id in (0," + _en_id.ToString + ")"

        If fobject.name = "" Then
            get_sequel = get_sequel + ""

        End If

        If fobject.name = "FPurchaseOrder" Then
        ElseIf fobject.name = "FPaymentOrder" Then
        ElseIf fobject.name = "FRequisition" Then
        ElseIf fobject.name = "FInventoryRequest" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FPriceList" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryReceipts" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryIssues" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FTransferIssues" Then
            get_sequel = get_sequel + "and pt_type ~~* 'I'"
        ElseIf fobject.name = "FAssetTransferIssue" Then
            get_sequel = get_sequel + "and pt_type ~~* 'A'"
        ElseIf fobject.name = "FSalesOrder" Then
        End If

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    
    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FPurchaseOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            If ds.Tables(0).Rows(_row_gv).Item("pt_type").ToString.ToUpper = "E" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "M")
            Else
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "")
            End If

            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))

            'fobject.gv_edit.SetRowCellValue(_row, "pod_taxable", ds.Tables(0).Rows(_row_gv).Item("pt_taxable"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_tax_inc", ds.Tables(0).Rows(_row_gv).Item("pt_tax_inc"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class", ds.Tables(0).Rows(_row_gv).Item("pt_tax_class"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_tax_class_name", ds.Tables(0).Rows(_row_gv).Item("tax_class_name"))

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPaymentOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            If ds.Tables(0).Rows(_row_gv).Item("pt_type").ToString.ToUpper = "E" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "M")
            Else
                fobject.gv_edit.SetRowCellValue(_row, "pod_memo", "")
            End If

            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FInventoryRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pbd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FRequisition" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPriceList" Then
            fobject.gv_edit_item.SetRowCellValue(_row, "pid_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit_item.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit_item.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit_item.BestFitColumns()
        ElseIf fobject.name = "FInventoryReceipts" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssues" Then
            'dt_bantu = (func_coll.get_prodline_account(ds.Tables(0).Rows(_row_gv).Item("pt_pl_id"), "WO_COP-"))

            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("invct_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            'fobject.gv_edit.SetRowCellValue(_row, "riud_ac_id", dt_bantu.Rows(0).Item("pla_ac_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "ac_code", dt_bantu.Rows(0).Item("ac_code"))
            'fobject.gv_edit.SetRowCellValue(_row, "ac_name", dt_bantu.Rows(0).Item("ac_name"))
            'fobject.gv_edit.SetRowCellValue(_row, "riud_sb_id", dt_bantu.Rows(0).Item("pla_sb_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "sb_desc", "-")
            'fobject.gv_edit.SetRowCellValue(_row, "riud_cc_id", dt_bantu.Rows(0).Item("pla_cc_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "cc_desc", "-")

            If _tran_oid.ToString <> "" Then
                fobject.gv_edit.SetRowCellValue(_row, "riud_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
                fobject.gv_edit.SetRowCellValue(_row, "riud_pbd_oid", ds.Tables(0).Rows(_row_gv).Item("pbd_oid"))
            End If

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryBeginingBalance" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_loc_id", ds.Tables(0).Rows(_row_gv).Item("pt_loc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "riud_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FTransferIssues" Then
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "ptsfrd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrder" Or fobject.name = "FSalesOrderProject" Then
            Dim ds_bantu As New DataSet

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
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
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.gv_edit.SetRowCellValue(_row, "sod_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            'fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))

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

            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FCreateInvoice" Then
            fobject.gv_edit.SetRowCellValue(_row, "arinvd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FAssetTransferIssue" Then

            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um", ds.Tables(0).Rows(_row_gv).Item("um_name"))

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSoPj" Then
            Dim ds_bantu As New DataSet

            fobject.gv_edit.SetRowCellValue(_row, "sopjd_pt_id", ds.Tables(0).Rows(_row_gv).Item("pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_um", ds.Tables(0).Rows(_row_gv).Item("pt_um"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("um_name"))
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
