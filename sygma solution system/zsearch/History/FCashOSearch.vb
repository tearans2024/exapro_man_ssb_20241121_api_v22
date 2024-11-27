Imports master_new.ModFunction

Public Class FCashOSearch
    Public _row, _en_id, _ptnr_id As Integer
    Public _obj As Object

    Private Sub FCashOSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Cash Out Number", "casho_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date", "casho_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "Amount", "casho_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
            & "  casho_code,casho_oid, " _
            & "  casho_date, " _
            & "  casho_amount " _
            & "FROM " _
            & "  public.casho_out " _
            & "WHERE " _
            & "  casho_amount > 0" _
            & "ORDER BY " _
            & "  casho_code"

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

        If fobject.name = FARPaymentDetail.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("casho_code")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("casho_oid")
            fobject.arpay_cashi_amount.editvalue = ds.Tables(0).Rows(_row_gv).Item("casho_amount")


        End If
    End Sub


    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

End Class
