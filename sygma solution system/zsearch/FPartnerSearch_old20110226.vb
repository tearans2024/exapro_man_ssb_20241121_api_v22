Imports master_new.ModFunction

Public Class FPartnerSearch
    Public _row, _en_id As Integer

    Private Sub FPartnerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 471
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partner Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Is Customer", "ptnr_is_cust", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Is Vendor", "ptnr_is_vend", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Is Member", "ptnr_is_member", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Is Employee", "ptnr_is_emp", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select ptnr_id, ptnr_code, ptnr_name, ptnr_is_cust, ptnr_is_vend , ptnr_is_member , ptnr_is_emp " + _
                     " from ptnr_mstr " + _
                     " inner join en_mstr on ptnr_en_id = en_id " + _
                     " and (ptnr_code ~~* '%" + Trim(te_search.Text) + "%' or ptnr_name ~~* '%" + Trim(te_search.Text) + "%')" + _
                     " where ptnr_active ~~* 'Y'" _
                    & " and ptnr_en_id in (0," + _en_id.ToString + ")" _
                    & " order by ptnr_name"
        Return get_sequel
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

        fobject.gv_edit.SetRowCellValue(_row, "rod_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
        fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
        fobject.gv_edit.BestFitColumns()
       
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
