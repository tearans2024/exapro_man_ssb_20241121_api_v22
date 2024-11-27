Imports master_new.ModFunction

Public Class FMktOrderSearch

    Public _row, _en_id As Integer
    Public _colname As String = ""
    'Public _pt_id As Integer
    Public _obj As Object


    Private Sub FMktOrderSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = FWorkOrder.Name Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Project Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Sold to", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Date Project", "pjc_date", DevExpress.Utils.HorzAlignment.Center)
            add_column(gv_master, "Project Total", "prj_total", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.Numeric, "n4")

            add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "PT Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "QTY", "prjd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")

        Else
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Description", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Date Project", "pjc_date", DevExpress.Utils.HorzAlignment.Center)

        End If


        'ant 17 maret 2011
        'sys 20110401 gak jadi pake pod_pt_id
        'If fobject.name = "FPurchaseOrder" Then
        '    add_column(gv_master, "Is Bussiness Plan", "pjc_is_bp", DevExpress.Utils.HorzAlignment.Default)
        '    add_column(gv_master, "Desc 1", "bpd_desc1", DevExpress.Utils.HorzAlignment.Default)
        '    add_column(gv_master, "Desc 2", "bpd_desc2", DevExpress.Utils.HorzAlignment.Default)
        '    add_column(gv_master, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Default)
        'End If
        ''------------------------
    End Sub

    Public Overrides Function get_sequel() As String
        'ant 17 maret 2011
        'sys 20110401 
        If fobject.name = "FPurchaseOrder" Then
            get_sequel = "SELECT  " _
                & "  en_code,en_desc, " _
                & "  pjc_id, " _
                & "  pjc_code, " _
                & "  pjc_date, " _
                & "  pjc_desc " _
                & "FROM  " _
                & "  public.pjc_mstr" _
                & " inner join public.en_mstr on en_id = pjc_en_id" _
                & " where (pjc_code ~~* '%" + Trim(te_search.Text) + "%')" _
                & " and pjc_active ~~* 'Y'" _
                & " and pjc_en_id in (0," + _en_id.ToString + ")" _
                & " order by pjc_code"
            '& " and pjc_id <> 0 " _ semuanya dibuka sekarang 20110920

        ElseIf fobject.name = "FPurchaseOrder_NonBudget" Then
            get_sequel = "SELECT  " _
                & "  en_code,en_desc, " _
                & "  pjc_id, " _
                & "  pjc_code, " _
                & "  pjc_date, " _
                & "  pjc_desc " _
                & "FROM  " _
                & "  public.pjc_mstr" _
                & " inner join public.en_mstr on en_id = pjc_en_id" _
                & " where (pjc_code ~~* '%" + Trim(te_search.Text) + "%')" _
                & " and pjc_active ~~* 'Y'" _
                & " and pjc_en_id in (0," + _en_id.ToString + ")" _
                & " order by pjc_code"

        ElseIf fobject.name = FWorkOrder.Name Then
            get_sequel = "SELECT  " _
                & "  c.pjc_id, " _
                & "  c.pjc_code, " _
                & "  b.ptnr_name, " _
                & "  c.pjc_date, " _
                & "  a.prj_total, " _
                & "  e.pt_code, " _
                & "  e.pt_desc1,d.prjd_pt_id, " _
                & "  d.prjd_qty, " _
                & "  f.en_desc " _
                & "FROM " _
                & "  public.prj_mstr a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.prj_ptnr_id_sold = b.ptnr_id) " _
                & "  INNER JOIN public.pjc_mstr c ON (a.prj_code = c.pjc_code) " _
                & "  INNER JOIN public.prjd_det d ON (a.prj_oid = d.prjd_prj_oid) " _
                & "  INNER JOIN public.pt_mstr e ON (d.prjd_pt_id = e.pt_id) " _
                & "  INNER JOIN public.en_mstr f ON (a.prj_en_id = f.en_id) " _
                & " where (pjc_code ~~* '%" + Trim(te_search.Text) + "%')" _
                & " and pjc_active ~~* 'Y'" _
                & " and pjc_en_id in (0," + _en_id.ToString + ")" _
                & " order by pjc_code"

        ElseIf fobject.name = FWorkOrderbyMO.Name Then
            get_sequel = "SELECT DISTINCT " _
                & "  c.pjc_id, " _
                & "  c.pjc_oid, " _
                & "  c.pjc_code, " _
                & "  b.ptnr_name, " _
                & "  c.pjc_date, " _
                & "  a.prj_total, " _
                & "  f.en_desc " _
                & "FROM " _
                & "  public.prj_mstr a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.prj_ptnr_id_sold = b.ptnr_id) " _
                & "  INNER JOIN public.pjc_mstr c ON (a.prj_code = c.pjc_code) " _
                & "  INNER JOIN public.prjd_det d ON (a.prj_oid = d.prjd_prj_oid) " _
                & "  INNER JOIN public.en_mstr f ON (a.prj_en_id = f.en_id) " _
                & " where (pjc_code ~~* '%" + Trim(te_search.Text) + "%')" _
                & " and pjc_active ~~* 'Y'" _
                & " and pjc_en_id in (0," + _en_id.ToString + ")" _
                & " order by pjc_code"
        Else
            get_sequel = "SELECT  " _
                & "  en_code,en_desc, " _
                & "  pjc_id, " _
                & "  pjc_code, " _
                & "  pjc_date, " _
                & "  pjc_desc " _
                & "FROM  " _
                & "  public.pjc_mstr" _
                & " inner join public.en_mstr on en_id = pjc_en_id" _
                & " where (pjc_code ~~* '%" + Trim(te_search.Text) + "%')" _
                & " and pjc_active ~~* 'Y'" _
                & " and pjc_en_id in (0," + _en_id.ToString + ")" _
                & " order by pjc_code"
        End If

        'End If
        'Me.Width = 750
        'Me.Height = 360
        Return get_sequel
        '----------------------------------
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

        If fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrder_NonBudget" Or fobject.name = "FPurchaseOrderExpense" Then
            If _colname = "pjc_code" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pjc_code", ds.Tables(0).Rows(_row_gv).Item("pjc_code"))
                fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            ElseIf _colname = "pjc_ref_desc" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_pjc_ref_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pjc_ref_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            End If

            'ant 17 maret 2011
            'sys 20110401 
            'fobject.gv_edit.SetRowCellValue(_row, "pod_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            'fobject.gv_edit.SetRowCellValue(_row, "pjc_is_bp", ds.Tables(0).Rows(_row_gv).Item("pjc_is_bp"))
            'fobject.gv_edit.SetRowCellValue(_row, "bpd_qty_open", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            'fobject.gv_edit.SetRowCellValue(_row, "bpd_oid", ds.Tables(0).Rows(_row_gv).Item("bpd_oid"))
            fobject.gv_edit.BestFitColumns()
            '---------------------------------
        ElseIf fobject.name = "FPaymentOrder" Then
            If _colname = "pjc_desc" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            ElseIf _colname = "pjc_ref_desc" Then
                fobject.gv_edit.SetRowCellValue(_row, "pod_pjc_ref_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pjc_ref_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            End If
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FVoucher" Or fobject.name = "FVoucher_20110907" Then
            fobject.gv_edit_dist.SetRowCellValue(_row, "apd_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit_dist.BestFitColumns()
        ElseIf fobject.name = "FPrepaymentVoucher" Then
            fobject.gv_edit_dist.SetRowCellValue(_row, "apd_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit_dist.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit_dist.BestFitColumns()
        ElseIf fobject.name = "FDisbursementVerifikasi" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbyd_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FDisbursementRequest" Then
            If _colname = "pjc_desc" Then
                fobject.gv_edit.SetRowCellValue(_row, "pbyd_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
                fobject.gv_edit.BestFitColumns()
            Else
                fobject.gv_edit.SetRowCellValue(_row, "pbyd_pjc_ref_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pjc_ref_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
                fobject.gv_edit.BestFitColumns()
            End If

        ElseIf fobject.name = "FDisbursementRealization" Then
            If _colname = "pjc_desc" Then
                fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit_cash.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
                fobject.gv_edit_cash.BestFitColumns()
            Else
                fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_pjc_ref_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
                fobject.gv_edit_cash.SetRowCellValue(_row, "pjc_ref_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
                fobject.gv_edit_cash.BestFitColumns()
            End If

        ElseIf fobject.name = "FCreateInvoice" Then
            fobject.gv_edit.SetRowCellValue(_row, "arinvd_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_desc", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit.BestFitColumns()
            'har 20110618

        ElseIf fobject.name = FWorkOrder.Name Then
            fobject.wo_pjc_id.text = ds.Tables(0).Rows(_row_gv).Item("pjc_code")
            fobject.wo_pjc_id.tag = ds.Tables(0).Rows(_row_gv).Item("pjc_id")
            fobject.pjc_desc.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.wo_pt_id.tag = ds.Tables(0).Rows(_row_gv).Item("prjd_pt_id")
            fobject.wo_pt_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code") & " " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.wo_qty.editvalue = ds.Tables(0).Rows(_row_gv).Item("prjd_qty")

        ElseIf fobject.name = FWorkOrderbyMO.Name Then
            fobject.wo_pjc_id.text = ds.Tables(0).Rows(_row_gv).Item("pjc_code")
            fobject.wo_pjc_id.tag = ds.Tables(0).Rows(_row_gv).Item("pjc_oid")
            'fobject.pjc_desc.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            'fobject.wo_pt_id.tag = ds.Tables(0).Rows(_row_gv).Item("prjd_pt_id")
            'fobject.wo_pt_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code") & " " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            'fobject.wo_qty.editvalue = ds.Tables(0).Rows(_row_gv).Item("prjd_qty")

        ElseIf fobject.name = "FInventoryAdjustmentPlus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_code", ds.Tables(0).Rows(_row_gv).Item("pjc_code"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryAdjustmentMinus" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_code", ds.Tables(0).Rows(_row_gv).Item("pjc_code"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryReceipts" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_code", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FInventoryIssues" Then
            fobject.gv_edit.SetRowCellValue(_row, "riud_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_code", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FQualityControl" Then
            fobject.gv_edit.SetRowCellValue(_row, "qcd_pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_code", ds.Tables(0).Rows(_row_gv).Item("pjc_desc"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
