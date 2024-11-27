Imports master_new.ModFunction

Public Class FMOSearch
    Public _row, _cu_id, _cc_id, _en_id, _ptnr_id As Integer
    Public _obj As Object

    Private Sub FMOSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        de_1.EditValue = Today()
        de_2.EditValue = Today()

        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "MO Code", "mo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "MO Date", "mo_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FMODeleteLine" Then
            get_sequel = "SELECT  " _
                        & "  mo_oid, " _
                        & "  mo_dom_id, " _
                        & "  mo_en_id, " _
                        & "  mo_dt, " _
                        & "  mo_trans_id, " _
                        & "  mo_tran_id, " _
                        & "  mo_code, " _
                        & "  mo_date, " _
                        & "  mo_remarks " _
                        & "FROM  " _
                        & "  public.mo_mstr " _
                        & " where mo_en_id = " + SetInteger(_en_id) _
                        & " and mo_trans_id in ('I','W') " _
                        & " and (mo_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and mo_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by mo_code asc "
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
        Dim ds_bantu As New DataSet

        If fobject.name = "FMODeleteLine" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("mo_code")
            fobject._mo_oid = ds.Tables(0).Rows(_row_gv).Item("mo_oid")
            fobject.load_data_many(True)
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
