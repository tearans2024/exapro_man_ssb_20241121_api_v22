Imports master_new.ModFunction

Public Class FCogsSearch
    Public _row, _en_id As Integer
    Public _obj As Object

    Private Sub FWCSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 600
        Me.Height = 400
        de_1.EditValue = Today()
        de_2.EditValue = Today()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "cogsc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date", "cogsc_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remark", "cogsc_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "Cost Set", "cs_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Material", "cogsc_mtl_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Service", "cogsc_srv_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Total", "cogscr_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        'If fobject.name = FProjectBudgetMaintenance.Name Then
        get_sequel = "SELECT  " _
                & "  cogsc_oid,b.en_desc, " _
                & "  a.cogsc_code, " _
                & "  a.cogsc_date, " _
                & "  a.cogsc_remarks, " _
                & "  c.cs_name, " _
                & "  a.cogsc_mtl_total, " _
                & "  a.cogsc_srv_total, " _
                & "  a.cogscr_total " _
                & "FROM " _
                & "  public.cogsc_calc a " _
                & "  INNER JOIN public.en_mstr b ON (a.cogsc_en_id = b.en_id) " _
                & "  INNER JOIN public.cs_mstr c ON (a.cogsc_cs_id = c.cs_id) " _
                & "WHERE " _
                & "  a.cogsc_date BETWEEN " & SetDate(de_1.DateTime) & " and " & SetDate(de_2.DateTime) & "   " _
                & "  " _
                & " ORDER BY " _
                & "  a.cogsc_code"

        'End If

        Return get_sequel
    End Function

    'Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
    '    fill_data()
    '    Me.Close()
    'End Sub

    'Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '        fill_data()
    '        Me.Close()
    '    End If
    'End Sub

    'Private Sub fill_data()
    '    Dim _row_gv As Integer
    '    _row_gv = BindingContext(ds.Tables(0)).Position

    '    If fobject.name = FProjectBudgetMaintenance.Name Then
    '        fobject.BECogs.text = ds.Tables(0).Rows(_row_gv).Item("cogsc_code")
    '        fobject.BECogs.tag = ds.Tables(0).Rows(_row_gv).Item("cogsc_oid")
    '    ElseIf fobject.name = FCogsSimPrint.Name Then
    '        _obj.text = ds.Tables(0).Rows(_row_gv).Item("cogsc_code")
    '    End If

    'End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
