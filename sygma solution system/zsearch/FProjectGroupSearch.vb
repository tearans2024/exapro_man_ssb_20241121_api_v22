Imports master_new.ModFunction

Public Class FProjectGroupSearch
    Public _row, _cu_id, _cc_id, _en_id, _ptnr_id As Integer

    Private Sub FProjectGroupSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        de_1.EditValue = Today()
        de_2.EditValue = Today()

        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'If fobject.name = "FProjectLayoutMaintenance" Then
        add_column(gv_master, "Code", "prjg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "prjg_name", DevExpress.Utils.HorzAlignment.Default)
        'ElseIf fobject.name = "FLayoutManualInsert" Then
        '    add_column(gv_master, "Code", "prjg_code", DevExpress.Utils.HorzAlignment.Default)
        '    add_column(gv_master, "Name", "prjg_name", DevExpress.Utils.HorzAlignment.Default)
        'End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FProjectLayoutMaintenance" Then
            get_sequel = "SELECT  " _
                        & "  prjg_oid, " _
                        & "  prjg_dom_id, " _
                        & "  prjg_en_id, " _
                        & "  prjg_add_by, " _
                        & "  prjg_add_date, " _
                        & "  prjg_upd_by, " _
                        & "  prjg_upd_date, " _
                        & "  prjg_dt, " _
                        & "  prjg_code, " _
                        & "  prjg_prj_oid, " _
                        & "  prjg_remarks, " _
                        & "  prjg_date, " _
                        & "  coalesce(prjg_name,'') as prjg_name, " _
                        & "  prjg_tran_id " _
                        & "FROM  " _
                        & "  public.prjg_group " _
                        & " where (prjg_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prjg_date <= " & SetDate(de_2.DateTime.Date) & ") " _
                        & "order by prjg_code asc "
        ElseIf fobject.name = "FLayoutManualInsert" Then
            get_sequel = "SELECT  " _
                        & "  prjg_oid, " _
                        & "  prjg_dom_id, " _
                        & "  prjg_en_id, " _
                        & "  prjg_add_by, " _
                        & "  prjg_add_date, " _
                        & "  prjg_upd_by, " _
                        & "  prjg_upd_date, " _
                        & "  prjg_dt, " _
                        & "  prjg_code, " _
                        & "  prjg_prj_oid, " _
                        & "  prjg_remarks, " _
                        & "  prjg_date, " _
                        & "  coalesce(prjg_name,'') as prjg_name, " _
                        & "  prjg_tran_id " _
                        & "FROM  " _
                        & "  public.prjg_group " _
                        & " where (prjg_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and prjg_date <= " & SetDate(de_2.DateTime.Date) & ") " _
                        & "order by prjg_code asc "
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
        'Dim i As Integer

        If fobject.name = "FProjectLayoutMaintenance" Then
            fobject.be_prjg_code.text = ds.Tables(0).Rows(_row_gv).Item("prjg_code")
            fobject.te_prjg_name.text = ds.Tables(0).Rows(_row_gv).Item("prjg_name")
            fobject._prjg_oid = ds.Tables(0).Rows(_row_gv).Item("prjg_oid")
            fobject.find()
        ElseIf fobject.name = "FLayoutManualInsert" Then
            fobject.be_prjg_code.text = ds.Tables(0).Rows(_row_gv).Item("prjg_code")
            fobject.te_prjg_name.text = ds.Tables(0).Rows(_row_gv).Item("prjg_name")
            fobject._prjg_oid = ds.Tables(0).Rows(_row_gv).Item("prjg_oid")
            fobject._en_id = ds.Tables(0).Rows(_row_gv).Item("prjg_en_id")
            fobject._tran_id = ds.Tables(0).Rows(_row_gv).Item("prjg_tran_id")
            fobject.load_custom()
            fobject.find()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
