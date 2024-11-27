Imports master_new.ModFunction

Public Class FInventoryReceiptsSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryReceiptsSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Receipt Number", "invrcp_type2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Receipt Date", "invrcp_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        get_sequel = "SELECT  " _
                        & "  invrcp_type2, " _
                        & "  invrcp_date " _
                        & "FROM  " _
                        & "  public.invrcp_mstr " _
                        & "  where invrcp_en_id = " & _en_id.ToString _
                        & "  and invrcp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and invrcp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  order by invrcp_type2 "
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
        If fobject.name = "FInventoryReceiptsPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invrcp_type2")
        ElseIf fobject.name = "FInventoryReceiptsSerialPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("invrcp_type2")
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
    End Sub
End Class
