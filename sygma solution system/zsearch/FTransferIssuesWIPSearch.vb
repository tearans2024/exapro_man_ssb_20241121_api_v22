Imports master_new.ModFunction

Public Class FTransferIssuesWIPSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FTransferIssuesWIPSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Transfer Issue Number", "wimtr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Transfer Issue Date", "wimtr_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        get_sequel = "SELECT  " _
                        & "  wimtr_oid, " _
                        & "  wimtr_dom_id, " _
                        & "  wimtr_en_id, " _
                        & "  wimtr_add_by, " _
                        & "  wimtr_add_date, " _
                        & "  wimtr_upd_by, " _
                        & "  wimtr_upd_date, " _
                        & "  wimtr_code, " _
                        & "  wimtr_date, " _
                        & "  wimtr_si_id, " _
                        & "  wimtr_wc_id, " _
                        & "  wimtr_wc_to_id, " _
                        & "  wimtr_remarks, " _
                        & "  wimtr_trans_id, " _
                        & "  wimtr_dt, " _
                        & "  wimtr_wo_oid, " _
                        & "  wimtr_ptnr_id " _
                        & "FROM  " _
                        & "  public.wimtr_mstr " _
                        & "  where wimtr_en_id = " & _en_id.ToString _
                        & "  and wimtr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and wimtr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  order by wimtr_code "
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
        If fobject.name = "FTransferIssuesWIPPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wimtr_code")
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
    End Sub
End Class
