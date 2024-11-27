Imports master_new.ModFunction

Public Class FPartNumberCustomerSearch
    Public _row, _en_id, _si_id As Integer
    Public _obj As Object
    Public _so_type As String
    Public _tran_oid As String = ""
    Public _prj_oid As String = ""
    Public _ptnr_id As Integer
    Dim func_data As New function_data
    Public grid_call As String = ""

    Private Sub FPartNumberCustomerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "cp_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "cp_desc2", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  cp_en_id, " _
                    & "  en_desc, " _
                    & "  cp_id, " _
                    & "  cp_code, " _
                    & "  cp_desc1, " _
                    & "  cp_desc2, " _
                    & "  cp_um, " _
                    & "  cp_ptnr_id, " _
                    & "  um_mstr.code_name as um_name " _
                    & "FROM  " _
                    & "  public.cp_mstr" _
                    & " inner join en_mstr on en_id = cp_en_id " _
                    & " inner join code_mstr um_mstr on cp_um = um_mstr.code_id " _
                    & " where (cp_code ~~* '%" + Trim(te_search.Text) + "%' or cp_desc1 ~~* '%" + Trim(te_search.Text) + "%' or cp_desc2 ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and cp_en_id in (0," + _en_id.ToString + ")" _
                    & " and cp_ptnr_id = " & SetInteger(_ptnr_id) _
                    & " order by cp_code asc "
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

        If fobject.name = "FProjectMaintenance" Then
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_cp_id", ds.Tables(0).Rows(_row_gv).Item("cp_id"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "cp_code", ds.Tables(0).Rows(_row_gv).Item("cp_code"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc1", ds.Tables(0).Rows(_row_gv).Item("cp_desc1"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_pt_desc2", ds.Tables(0).Rows(_row_gv).Item("cp_desc2"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "prjc_um", ds.Tables(0).Rows(_row_gv).Item("cp_um"))
            fobject.gv_edit_cust.SetRowCellValue(_row, "unit_measure", ds.Tables(0).Rows(_row_gv).Item("um_name"))
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
