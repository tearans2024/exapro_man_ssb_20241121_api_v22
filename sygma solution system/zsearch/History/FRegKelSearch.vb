Imports master_new.ModFunction

Public Class FRegKelSearch
    Public _prop_codec, _kec_code, _kel_code As String
    Public _row, _en_id As Integer

    Private Sub FRegKelSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "kel_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "kel_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.reg_kel_mstr.kel_id, " _
                & "  public.reg_kel_mstr.kel_code, " _
                & "  public.reg_kel_mstr.kel_name, " _
                & "  public.reg_kel_mstr.kel_desc, " _
                & "  public.reg_kel_mstr.kel_kec_code, " _
                & "  public.reg_kec_mstr.kec_name " _
                & "FROM " _
                & "  public.reg_kel_mstr " _
                & "  INNER JOIN public.reg_kec_mstr ON (public.reg_kel_mstr.kel_kec_code = public.reg_kec_mstr.kec_code)" _
                & "  where " _
                & "  kec_name = '" + _kel_code.ToString + "'"

        'If Len(TxtSearch.EditValue) > 0 Then
        '    ssql += " a.prop_active ~~* '%" & TxtSearch.EditValue & "%' "
        'End If


        'If Len(filter_tambahan) > 0 Then
        '    ssql += filter_tambahan
        'End If

        'ssql += " order by a.prop_code"
        'ssql += " Limit '500' "
        'dt = GetTableData(ssql)
        'gc_master.DataSource = dt
        'gv_master.BestFitColumns()
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try

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

        If fobject.name = "FPartnerAll" Then
            fobject.gv_edit_address.SetRowCellValue(_row, "ptnra_line_5", ds.Tables(0).Rows(_row_gv).Item("kel_id"))
            fobject.gv_edit_address.SetRowCellValue(_row, "ptnra_line_5", ds.Tables(0).Rows(_row_gv).Item("kel_name"))
            fobject.gv_edit_address.BestFitColumns()
        End If
    End Sub
End Class
