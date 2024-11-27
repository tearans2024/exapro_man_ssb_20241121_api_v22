﻿Imports master_new.ModFunction

Public Class FProjectReturnReasonSearch

    Public _row, _en_id As Integer

    Private Sub FProjectReturnReasonSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 400
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select code_oid, code_dom_id, code_en_id, en_desc, code_add_by, code_add_date, " + _
                     " code_upd_by, code_upd_date, code_id, code_seq, code_field, " + _
                     " code_code, code_name, code_desc, code_default, " + _
                     " code_parent, code_usr_1, code_usr_2, code_usr_3, " + _
                     " code_usr_4, code_usr_5, code_active, code_dt from code_mstr " + _
                     " inner join en_mstr on code_en_id = en_id " + _
                     " where (code_code ~~* '%" + Trim(te_search.Text) + "%' or code_name ~~* '%" + Trim(te_search.Text) + "%' or code_desc ~~* '%" + Trim(te_search.Text) + "%')" + _
                     " and code_active ~~* 'Y'" + _
                     " and code_en_id in (0," + _en_id.ToString + ")" + _
                     " and code_field ~~* 'project_return_reason'"
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

        If fobject.name = "FProjectReturn" Then
            fobject.gv_edit.SetRowCellValue(_row, "soshipd_rea_code_id", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "reason_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
