Imports master_new.ModFunction

Public Class FDBGroupSearch
    Public _row, _en_id As Integer
    Public grid_call As String = ""

    Private Sub FDBGroupSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Name", "dbg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "City", "code_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.dbg_group.dbg_oid, " _
                    & "  public.dbg_group.dbg_code, " _
                    & "  public.dbg_group.dbg_name, " _
                    & "  public.dbg_group.dbg_city_id, " _
                    & "  public.code_mstr.code_name " _
                    & "FROM " _
                    & "  public.dbg_group " _
                    & "  INNER JOIN public.code_mstr ON (public.dbg_group.dbg_city_id = public.code_mstr.code_id)"

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
            fobject._be_dbr_dbg_name_oid = ds.Tables(0).Rows(_row_gv).Item("dbg_oid")
            fobject.be_dbr_dbg_name.Text = ds.Tables(0).Rows(_row_gv).Item("dbg_name")
            fobject._dbr_city_id = ds.Tables(0).Rows(_row_gv).Item("dbg_city_id")
            fobject.dbr_city.text = ds.Tables(0).Rows(_row_gv).Item("code_name")
        End If
    End Sub
End Class
