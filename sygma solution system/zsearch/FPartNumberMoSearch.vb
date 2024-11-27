Imports master_new.ModFunction

Public Class FPartNumberMoSearch
    Public _row, _en_id, _si_id, _ptnr_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Dim func_data As New function_data

    Private Sub FPartNumberMoSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PAO Code", "pao_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "MO Code", "mo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty", "mod_qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Area", "area_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  false as ceklist, " _
                    & "  mod_oid, " _
                    & "  mod_dom_id, " _
                    & "  mod_en_id,en_desc, " _
                    & "  mod_mo_oid,mo_code, " _
                    & "  mod_paod_oid,paod_prjd_oid,pao_code, " _
                    & "  prj_code,prjd_pt_id,pt_code,pt_ls,pt_type,pt_cost,prjd_pt_desc1,prjd_pt_desc2,prjd_loc_id, " _
                    & "  loc_desc,loc_area_id,area_name, " _
                    & "  ptnr_name, " _
                    & "  mod_qty, " _
                    & "  mod_um,um.code_name as unit_measure, " _
                    & "  pjc_id, " _
                    & "  mod_qty_do, " _
                    & "  mod_qty - coalesce(mod_qty_do,0) as mod_qty_open " _
                    & "FROM  " _
                    & "  public.mod_det " _
                    & "  inner join mo_mstr on mo_oid = mod_mo_oid " _
                    & "  inner join en_mstr on en_id = mod_en_id " _
                    & "  inner join paod_det on paod_oid = mod_paod_oid " _
                    & "  inner join pao_mstr on pao_oid = paod_pao_oid " _
                    & "  inner join prjd_det on prjd_oid = paod_prjd_oid " _
                    & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                    & "  inner join pjc_mstr on pjc_code = prj_code " _
                    & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                    & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                    & "  inner join code_mstr um on um.code_id =  paod_um " _
                    & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                    & "  inner join area_mstr on area_id = loc_area_id " _
                    & "  where mod_qty - coalesce(mod_qty_do,0) > 0 " _
                    & "  and mo_trans_id <> 'X' " _
                    & "  and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & "  and mod_en_id in (0," + _en_id.ToString + ")"
        Return get_sequel
    End Function

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FDeliveryOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqds_oid", Guid.NewGuid.ToString)
            fobject.gv_edit.SetRowCellValue(_row, "prjd_oid", ds.Tables(0).Rows(_row_gv).Item("paod_prjd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "paod_oid", ds.Tables(0).Rows(_row_gv).Item("mod_paod_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_mod_oid", ds.Tables(0).Rows(_row_gv).Item("mod_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(_row_gv).Item("prjd_pt_id"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("prjd_pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("prjd_pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_qty_open", ds.Tables(0).Rows(_row_gv).Item("mod_qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_qty", ds.Tables(0).Rows(_row_gv).Item("mod_qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_um", ds.Tables(0).Rows(_row_gv).Item("mod_um"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_um_name", ds.Tables(0).Rows(_row_gv).Item("unit_measure"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(_row_gv).Item("pt_ls"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(_row_gv).Item("pt_type"))
            fobject.gv_edit.SetRowCellValue(_row, "reqds_cost", ds.Tables(0).Rows(_row_gv).Item("pt_cost"))
            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "area_name", ds.Tables(0).Rows(_row_gv).Item("area_name"))
            fobject.gv_edit.SetRowCellValue(_row, "pjc_id", ds.Tables(0).Rows(_row_gv).Item("pjc_id"))
            fobject.gv_edit.SetRowCellValue(_row, "prj_code", ds.Tables(0).Rows(_row_gv).Item("prj_code"))
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

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub sb_get_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_get.Click
        Try
            Dim _row_pos As Integer
            Dim jml As Integer = 0
            ds.Tables(0).AcceptChanges()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("ceklist") = True Then
                    If fobject.name = "FDeliveryOrder" Then
                        If jml = 0 Then
                            fobject.gv_edit.SetRowCellValue(_row, "reqds_oid", Guid.NewGuid.ToString)
                            fobject.gv_edit.SetRowCellValue(_row, "prjd_oid", ds.Tables(0).Rows(i).Item("paod_prjd_oid"))
                            fobject.gv_edit.SetRowCellValue(_row, "paod_oid", ds.Tables(0).Rows(i).Item("mod_paod_oid"))
                            fobject.gv_edit.SetRowCellValue(_row, "reqds_mod_oid", ds.Tables(0).Rows(i).Item("mod_oid"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_id", ds.Tables(0).Rows(i).Item("prjd_pt_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(i).Item("pt_code"))
                            fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc1", ds.Tables(0).Rows(i).Item("prjd_pt_desc1"))
                            fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc2", ds.Tables(0).Rows(i).Item("prjd_pt_desc2"))
                            fobject.gv_edit.SetRowCellValue(_row, "reqds_qty_open", ds.Tables(0).Rows(i).Item("mod_qty_open"))
                            fobject.gv_edit.SetRowCellValue(_row, "reqds_qty", ds.Tables(0).Rows(i).Item("mod_qty_open"))
                            fobject.gv_edit.SetRowCellValue(_row, "reqds_um", ds.Tables(0).Rows(i).Item("mod_um"))
                            fobject.gv_edit.SetRowCellValue(_row, "reqds_um_name", ds.Tables(0).Rows(i).Item("unit_measure"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_ls", ds.Tables(0).Rows(i).Item("pt_ls"))
                            fobject.gv_edit.SetRowCellValue(_row, "pt_type", ds.Tables(0).Rows(i).Item("pt_type"))
                            fobject.gv_edit.SetRowCellValue(_row, "reqds_cost", ds.Tables(0).Rows(i).Item("pt_cost"))
                            fobject.gv_edit.SetRowCellValue(_row, "loc_desc", ds.Tables(0).Rows(i).Item("loc_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "area_name", ds.Tables(0).Rows(i).Item("area_name"))
                            fobject.gv_edit.SetRowCellValue(_row, "pjc_id", ds.Tables(0).Rows(i).Item("pjc_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "prj_code", ds.Tables(0).Rows(i).Item("prj_code"))
                            
                            fobject.gv_edit.BestFitColumns()
                            jml = jml + 1
                        Else
                            fobject.gv_edit.AddNewRow()
                            _row_pos = fobject.gv_edit.FocusedRowHandle()

                            fobject.gv_edit.SetRowCellValue(_row_pos, "reqds_oid", Guid.NewGuid.ToString)
                            fobject.gv_edit.SetRowCellValue(_row_pos, "prjd_oid", ds.Tables(0).Rows(i).Item("paod_prjd_oid"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "paod_oid", ds.Tables(0).Rows(i).Item("mod_paod_oid"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "reqds_mod_oid", ds.Tables(0).Rows(i).Item("mod_oid"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_id", ds.Tables(0).Rows(i).Item("prjd_pt_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_code", ds.Tables(0).Rows(i).Item("pt_code"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "prjd_pt_desc1", ds.Tables(0).Rows(i).Item("prjd_pt_desc1"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "prjd_pt_desc2", ds.Tables(0).Rows(i).Item("prjd_pt_desc2"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "reqds_qty_open", ds.Tables(0).Rows(i).Item("mod_qty_open"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "reqds_qty", ds.Tables(0).Rows(i).Item("mod_qty_open"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "reqds_um", ds.Tables(0).Rows(i).Item("mod_um"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "reqds_um_name", ds.Tables(0).Rows(i).Item("unit_measure"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_ls", ds.Tables(0).Rows(i).Item("pt_ls"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pt_type", ds.Tables(0).Rows(i).Item("pt_type"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "reqds_cost", ds.Tables(0).Rows(i).Item("pt_cost"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "loc_desc", ds.Tables(0).Rows(i).Item("loc_desc"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "area_name", ds.Tables(0).Rows(i).Item("area_name"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "pjc_id", ds.Tables(0).Rows(i).Item("pjc_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "prj_code", ds.Tables(0).Rows(i).Item("prj_code"))

                            fobject.gv_edit.BestFitColumns()
                        End If

                        fobject.gv_edit.BestFitColumns()
                    End If
                End If
            Next
        Catch
        End Try

        Me.Close()
    End Sub

End Class
