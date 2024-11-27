Imports master_new.ModFunction

Public Class FPartNumberProjectSearch
    Public _row, _en_id, _si_id, _ptnr_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Dim func_data As New function_data

    Private Sub FPartNumberProjectSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PAO Code", "pao_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  paod_oid, " _
                    & "  paod_en_id,en_desc, " _
                    & "  paod_prjd_oid, " _
                    & "  prj_code,pao_code,pao_ptnr_id,pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
                    & "  paod_qty, " _
                    & "  paod_um,um.code_name as unit_measure, " _
                    & "  paod_qty_mo, " _
                    & "  paod_qty - coalesce(paod_qty_mo,0) as qty_open, " _
                    & "  paod_eta_target, " _
                    & "  paod_eta_confirm, " _
                    & "  paod_etd_target, " _
                    & "  paod_is_confirm, " _
                    & "  paod_is_reconfirm,ptnr_name " _
                    & "FROM  " _
                    & "  public.paod_det " _
                    & "  inner join en_mstr on en_id = paod_en_id " _
                    & "  inner join pao_mstr on pao_oid = paod_pao_oid " _
                    & "  inner join prjd_det on prjd_oid = paod_prjd_oid " _
                    & "  inner join ptnr_mstr on ptnr_id = pao_ptnr_id " _
                    & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                    & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                    & "  inner join code_mstr um on um.code_id =  paod_um " _
                    & "  where pao_trans_id = 'I' " _
                    & "  and paod_qty - coalesce(paod_qty_mo,0) > 0 " _
                    & "  and (pt_code ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & "  and paod_en_id in (0," + _en_id.ToString + ")"
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

        If fobject.name = "FMO" Then
            fobject.gv_edit.SetRowCellValue(_row, "prjd_oid", ds.Tables(0).Rows(_row_gv).Item("paod_prjd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "mod_paod_oid", ds.Tables(0).Rows(_row_gv).Item("paod_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
            fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("prjd_pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("prjd_pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "qty_open", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "mod_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "mod_um", ds.Tables(0).Rows(_row_gv).Item("paod_um"))
            fobject.gv_edit.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("unit_measure"))
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
