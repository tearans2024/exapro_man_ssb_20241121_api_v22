Imports master_new.ModFunction

Public Class FDisbursementVoucherSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FDisbursementVoucherSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_payment_order")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Voucher Number", "cash_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date", "cash_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Type", "cash_type", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remarks", "cash_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FDisbursementVoucherPrint" Then
            get_sequel = "SELECT  " _
                & "  cash_oid, " _
                & "  cash_dom_id, " _
                & "  cash_en_id,en_desc, " _
                & "  cash_add_by, " _
                & "  cash_add_date, " _
                & "  cash_upd_by, " _
                & "  cash_upd_date, " _
                & "  cash_bk_id,bk_name, " _
                & "  cash_code, " _
                & "  cash_date, " _
                & "  cash_type, " _
                & "  cash_cu_id,cu_name, " _
                & "  cash_book_balance, " _
                & "  cash_bank_balance, " _
                & "  cash_exch_rate, " _
                & "  cash_remarks, " _
                & "  cash_dt " _
                & "FROM  " _
                & "  public.cash_mstr " _
                & " inner join en_mstr on en_id = cash_en_id  " _
                & "  inner join bk_mstr on bk_id = cash_bk_id " _
                & "  inner join cu_mstr on cu_id = cash_cu_id " _
                & " where cash_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and cash_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and cash_en_id = " + _en_id.ToString _
                & "  and cash_type <> 'R' " _
                & " order by cash_date asc "

            '& " and (cash_code ~~* '%" + Trim(te_search.Text) + "%' " _
        End If
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
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FDisbursementVoucherPrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("cash_code")
        End If
    End Sub
End Class
