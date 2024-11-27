Imports master_new.ModFunction

Public Class FPSPeriodeSearch
    Public _row, _en_id As Integer
    Public grid_call As String = ""

    Private Sub FPSPeriodeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Periode", "periode_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Start Date", "periode_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "End Date", "periode_end_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.psperiode_mstr.periode_oid, " _
                & "  public.psperiode_mstr.periode_id, " _
                & "  public.psperiode_mstr.periode_code, " _
                & "  public.psperiode_mstr.periode_start_date, " _
                & "  public.psperiode_mstr.periode_end_date, " _
                & "  public.psperiode_mstr.periode_active " _
                & "FROM " _
                & "  public.psperiode_mstr " _
                & "  LEFT OUTER JOIN public.dbr_mstr ON (public.psperiode_mstr.periode_id = public.dbr_mstr.dbr_periode_id) " _
                & "WHERE " _
                & "  public.psperiode_mstr.periode_active = 'Y'"
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
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
        Dim i As Integer

        If fobject.name = "FDPointRewardReports" Then
            Dim x As Integer
            fobject._be_dbr_periode_id = ds.Tables(0).Rows(_row_gv).Item("periode_id")
            fobject.be_dbr_periode.Text = ds.Tables(0).Rows(_row_gv).Item("periode_code")
            fobject.dbr_start_date.DateTime = ds.Tables(0).Rows(_row_gv).Item("periode_start_date")
            fobject.dbr_end_date.DateTime = ds.Tables(0).Rows(_row_gv).Item("periode_end_date")
            'fobject._dbr_city_id = ds.Tables(0).Rows(_row_gv).Item("dbg_city_id")
            'fobject.dbr_city.text = ds.Tables(0).Rows(_row_gv).Item("code_name")
        End If
    End Sub
End Class
