Imports master_new.ModFunction

Public Class FBusPlanSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value_req, _conf_value_bplan As String
    Dim _now As DateTime

    Private Sub FBusPlanSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value_bplan = func_coll.get_conf_file("wf_bussiness_plan")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Bussiness Plan Number", "bp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Start Date", "bp_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "End Date", "bp_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "bpd_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "bpd_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Cost", "bpd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Remarks", "bpd_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FSalesOrder" Then
            get_sequel = "SELECT  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.bp_mstr.bp_oid, " _
                    & "  public.bp_mstr.bp_upd_date, " _
                    & "  public.bp_mstr.bp_upd_by, " _
                    & "  public.bp_mstr.bp_add_date, " _
                    & "  public.bp_mstr.bp_add_by, " _
                    & "  public.bp_mstr.bp_code, " _
                    & "  public.bp_mstr.bp_date, " _
                    & "  public.bp_mstr.bp_start_date, " _
                    & "  public.bp_mstr.bp_end_date, " _
                    & "  public.bp_mstr.bp_remarks, " _
                    & "  tran_name, " _
                    & "  public.bpd_det.bpd_oid, " _
                    & "  public.bpd_det.bpd_bp_oid, " _
                    & "  public.bpd_det.bpd_seq, " _
                    & "  public.bpd_det.bpd_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_pl_id, " _
                    & "  public.bpd_det.bpd_desc2, " _
                    & "  public.bpd_det.bpd_desc1, " _
                    & "  public.bpd_det.bpd_remarks, " _
                    & "  public.bpd_det.bpd_qty, " _
                    & "  public.bpd_det.bpd_qty_consume, " _
                    & "  (bpd_det.bpd_qty - coalesce(bpd_det.bpd_qty_consume,0)) as qty_open, " _
                    & "  public.bpd_det.bpd_um, " _
                    & "  public.code_mstr.code_name, " _
                    & "  bpd_det.bpd_cost " _
                    & "FROM " _
                    & "  public.bp_mstr " _
                    & "  INNER JOIN public.bpd_det ON (public.bpd_det.bpd_bp_oid = public.bp_mstr.bp_oid) " _
                    & "  INNER JOIN public.en_mstr ON (public.bp_mstr.bp_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.tran_mstr ON (public.bp_mstr.bp_tran_id = public.tran_mstr.tran_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.bpd_det.bpd_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.bpd_det.bpd_um = public.code_mstr.code_id) " _
                    & " where bp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and bp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and bp_en_id = " + _en_id.ToString _
                    & " and (bpd_det.bpd_qty - coalesce(bpd_det.bpd_qty_consume,0)) > 0 " _
                    & " and bp_trans_id ~~* 'I' " _
                    & " order by bp_code  "
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

        
        Dim _bpd_qty_cost, _bpd_cost, _bpd_disc, _qty_open As Double

        If fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_bpd_oid", ds.Tables(0).Rows(_row_gv).Item("bpd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "bp_code", ds.Tables(0).Rows(_row_gv).Item("bp_code"))

            fobject.gv_edit.SetRowCellValue(_row, "sod_pt_id", ds.Tables(0).Rows(_row_gv).Item("bpd_pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("bpd_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("bpd_desc2"))

            fobject.gv_edit.SetRowCellValue(_row, "sod_rmks", ds.Tables(0).Rows(_row_gv).Item("bpd_remarks"))
            fobject.gv_edit.SetRowCellValue(_row, "bpd_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            
            fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("bpd_um"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))

            fobject.gv_edit.SetRowCellValue(_row, "sod_cost", ds.Tables(0).Rows(_row_gv).Item("bpd_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "sod_disc", 0)

            fobject.gv_edit.SetRowCellValue(_row, "sod_um_conv", 1)

            _qty_open = ds.Tables(0).Rows(_row_gv).Item("qty_open")
            _bpd_cost = ds.Tables(0).Rows(_row_gv).Item("bpd_cost")
            _bpd_disc = 0
            _bpd_qty_cost = (_qty_open * _bpd_cost) - (_qty_open * _bpd_cost * _bpd_disc)

            Dim ds_bantu As New DataSet
            ds_bantu = New DataSet
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

            fobject.gv_edit.SetRowCellValue(_row, "sod_qty_cost", _bpd_qty_cost)
            fobject.gv_edit.BestFitColumns()
        End If

    End Sub
End Class
