Public Class FRegionalSearch
    Public _row, _en_id As Integer

    Private Sub FRegionalSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Region Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Region Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Region Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FProjectActivityOrder" Then
            get_sequel = "SELECT  " _
                    & "  code_id, " _
                    & "  code_code, " _
                    & "  code_name, " _
                    & "  code_desc " _
                    & "FROM  " _
                    & "  public.code_mstr " _
                    & " where code_en_id = " + _en_id.ToString _
                    & " and code_field = 'emp_region'"
        End If

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

        If fobject.name = "FProjectActivityOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "loc_reg_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "reg_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
