Imports master_new.ModFunction

Public Class FFakturPajakSearch
    Public _row As Integer
    Public _en_id, _ptnr_id As Integer
    Public _obj As Object
    Public func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FFakturPajakSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Faktur Pajak Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Faktur Pajak Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Faktur Pajak Sign", "fp_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Invoice Number", "arinv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Invoice Date", "arinv_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FFakturPajakPrint" Then
            get_sequel = "SELECT  " _
                    & "  fp_oid, " _
                    & "  fp_dom_id, " _
                    & "  fp_en_id, " _
                    & "  fp_code, " _
                    & "  fp_date, " _
                    & "  fp_sign,   " _
                    & "  fp_status, " _
                    & "  fp_ppn_type, " _
                    & "  fp_ptnr_id, " _
                    & "  fp_tax_inc, " _
                    & "  en_desc, " _
                    & "  arinv_code, " _
                    & "  arinv_date, " _
                    & "  ptnr_name " _
                    & "FROM  " _
                    & "  public.fp_mstr " _
                    & "  inner join arinv_mstr on arinv_oid = fp_arinv_oid " _
                    & "  inner join ptnr_mstr on ptnr_id = fp_ptnr_id " _
                    & "  inner join en_mstr on en_id = fp_en_id" _
                    & "  where fp_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and fp_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and fp_en_id = " + _en_id.ToString
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

        Dim ds_bantu As New DataSet
        If fobject.name = "FFakturPajakPrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("fp_code")
        End If
    End Sub
End Class
