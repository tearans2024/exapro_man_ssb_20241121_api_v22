Imports master_new.ModFunction

Public Class FPTBOMSrch
    Public _row, _pil As Integer
    Public _en_id As String

    Private Sub FBOMSrch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 600
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "codeptbom", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "descptbom", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String

        If fobject.name = "FPSRawMat" Then
            get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                       & " pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                       & "THEN 'N' " _
                       & "ELSE 'N' " _
                       & "END AS ptsign " _
                       & "FROM " _
                       & "  public.pt_mstr  " _
                       & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_pm_code='M' and pt_en_id in (" & _en_id & ")" _
                       & "UNION " _
                       & "SELECT en_mstr.en_desc, bom_oid, bom_en_id, bom_id, " _
                       & " bom_code, bom_desc, CASE WHEN bom_desc = '.,.,' " _
                       & "THEN 'Y' " _
                       & "ELSE 'Y' " _
                       & "END AS bomsign " _
                       & "FROM " _
                       & "  public.bom_mstr INNER JOIN en_mstr on bom_en_id=en_id where bom_en_id in (" & _en_id & ")" _
                       & "  AND bom_id NOT in (SELECT ps_pt_bom_id from ps_mstr)"
        ElseIf fobject.name = FProductStructure.Name Then
            If _pil = 3 Then
                get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                        & "       pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                        & "THEN 'N' " _
                        & "ELSE 'N' " _
                        & "END AS ptsign " _
                        & "FROM " _
                        & "  public.pt_mstr  " _
                        & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and pt_en_id in (0," & _en_id & ")" _
                        & "UNION " _
                        & "SELECT en_mstr.en_desc, bom_oid, bom_en_id, bom_id, " _
                        & " bom_code, bom_desc, CASE WHEN bom_desc = '.,.,' " _
                        & "THEN 'Y' " _
                        & "ELSE 'Y' " _
                        & "END AS bomsign " _
                        & "FROM " _
                        & "  public.bom_mstr INNER JOIN en_mstr on bom_en_id=en_id where bom_en_id in (0," & _en_id & ")"
            Else
                get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                       & " pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                       & "THEN 'N' " _
                       & "ELSE 'N' " _
                       & "END AS ptsign " _
                       & "FROM " _
                       & "  public.pt_mstr  " _
                       & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_pm_code='M' and pt_en_id in (" & _en_id & ")" _
                       & "UNION " _
                       & "SELECT en_mstr.en_desc, bom_oid, bom_en_id, bom_id, " _
                       & " bom_code, bom_desc, CASE WHEN bom_desc = '.,.,' " _
                       & "THEN 'Y' " _
                       & "ELSE 'Y' " _
                       & "END AS bomsign " _
                       & "FROM " _
                       & "  public.bom_mstr INNER JOIN en_mstr on bom_en_id=en_id where bom_en_id in (" & _en_id & ")" _
                       & "  AND bom_id NOT in (SELECT ps_pt_bom_id from ps_mstr)"
            End If
        ElseIf fobject.name = FWhereInUsedReport.Name Then
            get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                       & "       pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                       & "THEN 'N' " _
                       & "ELSE 'N' " _
                       & "END AS ptsign " _
                       & "FROM " _
                       & "  public.pt_mstr  " _
                       & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and pt_en_id in (" & _en_id & ")"

        Else
            get_sequel = "SELECT en_mstr.en_desc, pt_oid as oidptbom, pt_en_id as enptbom, pt_id as idptbom,  " _
                        & "       pt_code as codeptbom, pt_desc1 as descptbom, CASE WHEN pt_desc2 = '.,.,' " _
                        & "THEN 'N' " _
                        & "ELSE 'N' " _
                        & "END AS ptsign " _
                        & "FROM " _
                        & "  public.pt_mstr  " _
                        & "  INNER JOIN en_mstr on pt_en_id=en_id where pt_type='I' and pt_en_id in (0," & _en_id & ")" _
                        & "UNION " _
                        & "SELECT en_mstr.en_desc, bom_oid, bom_en_id, bom_id, " _
                        & " bom_code, bom_desc, CASE WHEN bom_desc = '.,.,' " _
                        & "THEN 'Y' " _
                        & "ELSE 'Y' " _
                        & "END AS bomsign " _
                        & "FROM " _
                        & "  public.bom_mstr INNER JOIN en_mstr on bom_en_id=en_id where bom_en_id in (0," & _en_id & ")"
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

        If fobject.name = "FProductStructure" Then
            If _pil < 3 Then
                If _pil = 1 Then
                    fobject.be_first.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")
                ElseIf _pil = 2 Then
                    fobject.be_to.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")
                End If
            Else
                fobject.gv_edit.SetRowCellValue(_row, "psd_pt_bom_id", ds.Tables(0).Rows(_row_gv).Item("idptbom"))
                'fobject.gv_edit.SetFocusedRowCellValue("psd_pt_bom_id", ds.Tables(0).Rows(_row_gv).Item("bom_id"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("codeptbom"))
                'fobject.gv_edit.SetFocusedRowCellValue("ptbomcopy", ds.Tables(0).Rows(_row_gv).Item("bom_desc"))
                fobject.gv_edit.SetRowCellValue(_row, "psd_use_bom", ds.Tables(0).Rows(_row_gv).Item("ptsign"))
                fobject.gv_edit.SetRowCellValue(_row, "pt_desc", ds.Tables(0).Rows(_row_gv).Item("descptbom"))
                fobject.gv_edit.BestFitColumns()
            End If
           
        ElseIf fobject.name = "FPSRawMat" Then
            If _pil = 1 Then
                fobject.be_from.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            ElseIf _pil = 2 Then
                fobject.be_to.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")
            End If
        ElseIf fobject.name = "FWhereInUsedReport" Then

            fobject.be_first.EditValue = ds.Tables(0).Rows(_row_gv).Item("idptbom")

        End If

    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
