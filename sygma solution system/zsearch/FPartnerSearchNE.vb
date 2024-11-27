Imports master_new.ModFunction

Public Class FPartnerSearchNE
    Public _row As Integer
    Public _en_id, _par_ptnr_id As Integer
    Public _obj As Object

    Dim func_data As New function_data
    Dim dt_bantu As DataTable

    Private Sub FPartnerSearchNE_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        load_cb()
        help_load_data(True)

        'gc_master.ForceInitialize()
        'gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        'gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        'gv_master.Focus()

        'If fobject.name = FPartnerTaxGroup.Name Then
        '    scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both
        '    gv_master.Columns("status").Visible = True
        'Else
        '    scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '    gv_master.Columns("status").Visible = False
        'End If
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'par_entity.Properties.DataSource = dt_bantu
        'par_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'par_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'par_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "dbg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "City", "code_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        Dim _en_id_coll As String = func_data.entity_parent(_en_id)

        If fobject.name = "FDBPointRewards" Then
            'ElseIf fobject.name = "FCashIn" Or fobject.name = FLocation.Name Or fobject.name = FARMerge.Name Or fobject.name = FARMergeByShipment.Name Or fobject.name = FSOShipMerge.Name Then
            get_sequel = "SELECT  " _
                    & "  public.dbg_group.dbg_oid, " _
                    & "  public.dbg_group.dbg_name, " _
                    & "  public.code_mstr.code_name " _
                    & "FROM " _
                    & "  public.dbg_group " _
                    & "  INNER JOIN public.code_mstr ON (public.dbg_group.dbg_city_id = public.code_mstr.code_id)" _
                   & " order by dbg_name"
        End If

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

        If fobject.name = "FDBPointRewards" Then
            fobject._dbr_dbg_oid = ds.Tables(0).Rows(_row_gv).Item("dbg_oid")
            fobject.dbg_name.text = ds.Tables(0).Rows(_row_gv).Item("dbg_name")
            fobject.dbr_city.editvalue = ds.Tables(0).Rows(_row_gv).Item("dbg_city_id")
            'fobject.gv_edit.SetRowCellValue(_row, "dbgd_en_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_en_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "dbgd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
            'fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    'Private Sub sb_fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_fill.Click
    '    Try
    '        Dim _row_pos As Integer
    '        Dim jml As Integer = 0
    '        ds.Tables(0).AcceptChanges()

    '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '            If ds.Tables(0).Rows(i).Item("status") = True Then
    '                jml = jml + 1
    '            End If
    '        Next
    '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '            If ds.Tables(0).Rows(i).Item("status") = True Then
    '                If fobject.name = FPartnerTaxGroup.Name Then
    '                    If jml = 0 Then
    '                        fobject.gv_edit.SetRowCellValue(_row, "tipgd_en_id", ds.Tables(0).Rows(i).Item("ptnr_en_id"))
    '                        fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
    '                        fobject.gv_edit.SetRowCellValue(_row, "tipgd_ptnr_id", ds.Tables(0).Rows(i).Item("ptnr_id"))
    '                        fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))
    '                        jml = jml + 1
    '                    Else
    '                        fobject.gv_edit.AddNewRow()
    '                        _row_pos = fobject.gv_edit.FocusedRowHandle()
    '                        fobject.gv_edit.SetRowCellValue(_row_pos, "tipgd_en_id", ds.Tables(0).Rows(i).Item("ptnr_en_id"))
    '                        fobject.gv_edit.SetRowCellValue(_row_pos, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
    '                        fobject.gv_edit.SetRowCellValue(_row_pos, "tipgd_ptnr_id", ds.Tables(0).Rows(i).Item("ptnr_id"))
    '                        fobject.gv_edit.SetRowCellValue(_row_pos, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))
    '                    End If

    '                    fobject.gv_edit.BestFitColumns()
    '                End If
    '            End If
    '        Next
    '        If jml = 0 Then
    '            MsgBox("Please checklist data first")
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    Me.Close()
    'End Sub
End Class
