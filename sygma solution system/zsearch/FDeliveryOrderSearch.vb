Imports master_new.ModFunction

Public Class FDeliveryOrderSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FDeliveryOrderSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_purchase_order")
    End Sub

    Public Overrides Sub format_grid()
        'If fobject.name = "FDeliveryOrderPrint" Then
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "DO Number", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "DO Date", "reqs_date", DevExpress.Utils.HorzAlignment.Center)
        'End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        'If fobject.name = "FDeliveryOrderPrint" Then
        get_sequel = "SELECT  " _
                    & "  reqs_oid, " _
                    & "  reqs_dom_id, " _
                    & "  reqs_en_id,en_desc, " _
                    & "  reqs_code, " _
                    & "  reqs_date, " _
                    & "  reqs_en_id_to, " _
                    & "  reqs_loc_id_from, " _
                    & "  reqs_loc_id_git, " _
                    & "  reqs_loc_id_to, " _
                    & "  reqs_trans_id, " _
                    & "  reqs_receive_date, " _
                    & "  reqs_remarks, " _
                    & "  reqs_si_id, " _
                    & "  reqs_si_to_id, " _
                    & "  reqs_tran_id, " _
                    & "  reqs_type, " _
                    & "  reqs_ship_via, " _
                    & "  reqs_driver, " _
                    & "  reqs_type_mobil, " _
                    & "  reqs_no_mobil " _
                    & "FROM  " _
                    & "  public.reqs_mstr " _
                    & "  inner join en_mstr on en_id = reqs_en_id " _
                    & "  where reqs_trans_id = 'I' " _
                    & "  and reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and reqs_date <=  " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and reqs_en_id = " + _en_id.ToString _
                    & "  order by reqs_code asc "
        'End If

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

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
        'Dim _exc_rate As Double
        _row_gv = BindingContext(ds.Tables(0)).Position

        Dim ds_bantu As New DataSet
        'Dim i As Integer

        If fobject.name = "FDeliveryOrderPrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("reqs_code")
        ElseIf fobject.name = "FDeliveryOrderSerialPrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("reqs_code")
        End If
    End Sub
End Class
