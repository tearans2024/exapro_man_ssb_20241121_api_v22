Imports master_new.ModFunction

Public Class FGroupPartnerSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim dt_bantu As DataTable

    Private Sub FGroupPartnerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        form_first_load()
        load_cb()
        help_load_data(True)

        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

        scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        'gv_master.Columns("status").Visible = False

    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        par_entity.Properties.DataSource = dt_bantu
        par_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        par_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        par_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "ptnrg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Limit Credit", "ptnr_limit_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        Dim _en_id_coll As String = func_data.entity_parent(_en_id)


        If fobject.name = FPartnerAllBaru.Name Then

            get_sequel = "SELECT  " _
            & "  a.ptnrg_oid, " _
            & "  a.ptnrg_en_id, " _
            & "  a.ptnrg_id, " _
            & "  a.ptnrg_code, " _
            & "  a.ptnrg_name, " _
            & "  a.ptnrg_desc, " _
            & "  a.ptnr_limit_credit " _
            & "FROM " _
            & " public.ptnrg_grp a " _
            & "WHERE " _
            & " a.ptnrg_active = 'Y'" _
            & "ORDER BY " _
            & " a.ptnrg_code"


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

        If fobject.name = "FPartnerAllBaru" Then
            'fobject._sc_pp_ptnr_ptnrg_id = ds.Tables(0).Rows(_row_gv).Item("ptnrg_id")
            fobject.sc_pp_ptnr_ptnrg_id.text = ds.Tables(0).Rows(_row_gv).Item("ptnrg_name")
            fobject.ptnr_limit_credit.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_limit_credit")
            'fobject.so_bantu_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

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
