Imports master_new.ModFunction

Public Class FPtProjectDetailSearch
    Public _row, _en_id, _si_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Public _prj_oid As String = ""
    Public _column_name As String = ""
    Dim func_data As New function_data

    Private Sub FPtProjectDetailSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If _column_name = "pt_code" Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Location", "loc_desc_prjd", DevExpress.Utils.HorzAlignment.Default)
        ElseIf _column_name = "cp_code" Then
            add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Code", "cp_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Description2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Location", "loc_desc_prjc", DevExpress.Utils.HorzAlignment.Default)
        End If
        
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If _column_name = "pt_code" Then
            get_sequel = "SELECT  " _
                   & "  prjd_oid, " _
                   & "  prjd_dom_id, " _
                   & "  prjd_en_id,en_desc,  " _
                   & "  prjd_prj_oid, " _
                   & "  prjd_pt_id,pt_code, " _
                   & "  prjd_pt_desc1, " _
                   & "  prjd_pt_desc2, " _
                   & "  prjd_loc_id, " _
                   & "  prjd_qty, " _
                   & "  prjd_qty_full, " _
                   & "  prjd_um, " _
                   & "  prjd_cost, " _
                   & "  prjd_price, " _
                   & "  prjd_disc, " _
                   & "  prjd_um_conv, " _
                   & "  prjd_qty_real, " _
                   & "  prjd_taxable, " _
                   & "  prjd_tax_inc, " _
                   & "  prjd_tax_class, " _
                   & "  prjd_trans_id, " _
                   & "  prjd_qty_pao, " _
                   & "  prjd_qty_mo, " _
                   & "  prjd_loc_id, " _
                   & "  loc_desc " _
                   & "FROM  " _
                   & "  public.prjd_det " _
                   & "  inner join en_mstr on en_id = prjd_en_id " _
                   & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                   & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                   & " where (pt_code ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or prjd_pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                   & " and prjd_prj_oid = " & SetSetring(_prj_oid.ToString()) _
                   & " and en_active ~~* 'Y'" _
                   & " and prjd_en_id in (0," + _en_id.ToString + ")"
        ElseIf _column_name = "cp_code" Then
            get_sequel = "SELECT  " _
                    & "  prjc_oid, " _
                    & "  prjc_dom_id, " _
                    & "  prjc_en_id,en_desc, " _
                    & "  prjc_add_by, " _
                    & "  prjc_add_date, " _
                    & "  prjc_upd_by, " _
                    & "  prjc_upd_date, " _
                    & "  prjc_dt, " _
                    & "  prjc_prj_oid, " _
                    & "  prjc_seq, " _
                    & "  prjc_si_id, " _
                    & "  prjc_cp_id,cp_code, " _
                    & "  prjc_pt_desc1, " _
                    & "  prjc_pt_desc2, " _
                    & "  prjc_loc_id, " _
                    & "  prjc_qty, " _
                    & "  prjc_qty_full, " _
                    & "  prjc_um, " _
                    & "  prjc_cost, " _
                    & "  prjc_price, " _
                    & "  prjc_disc, " _
                    & "  prjc_um_conv, " _
                    & "  prjc_qty_real, " _
                    & "  prjc_taxable, " _
                    & "  prjc_tax_inc, " _
                    & "  prjc_tax_class, " _
                    & "  prjc_trans_id, " _
                    & "  prjc_qty_inv, " _
                    & "  prjc_loc_id, " _
                    & "  loc_desc " _
                    & "FROM  " _
                    & "  public.prjc_cust " _
                    & "  inner join en_mstr on en_id = prjc_en_id " _
                    & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                    & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                    & " where (cp_code ~~* '%" + Trim(te_search.Text) + "%' or prjc_pt_desc1 ~~* '%" + Trim(te_search.Text) + "%' or prjc_pt_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and prjc_prj_oid = " & SetSetring(_prj_oid.ToString()) _
                    & " and en_active ~~* 'Y'" _
                    & " and prjc_en_id in (0," + _en_id.ToString + ")"
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

        If fobject.name = "FProjectRelated" Then
            If _column_name = "pt_code" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjr_prjd_oid", ds.Tables(0).Rows(_row_gv).Item("prjd_oid"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("prjd_pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("prjd_pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc_prjd", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "prjd_price", ds.Tables(0).Rows(_row_gv).Item("prjd_price"))
            ElseIf _column_name = "cp_code" Then
                fobject.gv_edit.SetRowCellValue(_row, "prjr_prjc_oid", ds.Tables(0).Rows(_row_gv).Item("prjc_oid"))
                fobject.gv_edit.SetRowCellValue(_row, "cp_code", ds.Tables(0).Rows(_row_gv).Item("cp_code"))
                fobject.gv_edit.SetRowCellValue(_row, "prjc_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("prjc_pt_desc1"))
                fobject.gv_edit.SetRowCellValue(_row, "prjc_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("prjc_pt_desc2"))
                fobject.gv_edit.SetRowCellValue(_row, "loc_desc_prjc", ds.Tables(0).Rows(_row_gv).Item("loc_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "prjc_price", ds.Tables(0).Rows(_row_gv).Item("prjc_price"))
            End If
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
