Imports master_new.ModFunction

Public Class FWOReceiptsSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FWOReceiptsSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "WO. Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "WO. Receipt Code", "wor_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "Date", "wor_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "Effective Date", "wor_date_eff", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")

        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        get_sequel = "SELECT  " _
            & "  a.wor_en_id, " _
            & "  e.en_desc, " _
            & "  a.wor_code, " _
            & "  a.wor_date, " _
            & "  a.wor_date_eff, " _
            & "  a.wor_wo_id, " _
            & "  b.wo_code, " _
            & "  b.wo_remarks, " _
            & "  c.pt_code, " _
            & "  c.pt_id, " _
            & "  c.pt_desc1,c.pt_desc2,c.pt_ls, " _
            & "  wo_pjc_id, " _
            & "  pjc_code " _
            & "FROM " _
            & "  public.wor_mstr a " _
            & "  INNER JOIN public.wo_mstr b ON (a.wor_wo_id = b.wo_id) " _
            & "  INNER JOIN public.pt_mstr c ON (b.wo_pt_id = c.pt_id) " _
            & "  INNER JOIN public.en_mstr e ON (a.wor_en_id = e.en_id) " _
            & "  INNER JOIN public.pjc_mstr ON pjc_id = wo_pjc_id " _
            & " Where wor_date between " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and  " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " AND a.wor_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & " ORDER BY wor_code"

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

        If fobject.name = "FWOReceiptPrintSign" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("wor_code")
        End If
    End Sub
End Class
