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
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PAO Code", "pao_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "MO Code", "mo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  mod_oid, " _
                    & "  mod_dom_id, " _
                    & "  mod_en_id,en_desc, " _
                    & "  mod_mo_oid,mo_code, " _
                    & "  mod_paod_oid,paod_prjd_oid,pao_code, " _
                    & "  prj_code,prjd_pt_id,pt_code,pt_ls,pt_type,pt_cost,prjd_pt_desc1,prjd_pt_desc2, " _
                    & "  mod_ptnr_id,ptnr_name, " _
                    & "  mod_qty, " _
                    & "  mod_um,um.code_name as unit_measure, " _
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
                    & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                    & "  inner join ptnr_mstr on ptnr_id = mod_ptnr_id " _
                    & "  inner join code_mstr um on um.code_id =  paod_um " _
                    & "  where mod_qty - coalesce(mod_qty_do,0) > 0 " _
                    & "  and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & "  and mod_en_id in (0," + _en_id.ToString + ")"
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

        If fobject.name = "FDeliveryOrder" Then
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
