Public Class FCogsSimPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FPurchaseOrderPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FCogsSearch
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FCogsSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Try
            Dim _sql As String

            _sql = "SELECT  " _
                & "  a.cogsc_code,cogsc_oid, " _
                & "  a.cogsc_date, " _
                & "  e.en_desc, " _
                & "  a.cogsc_remarks, " _
                & "  f.cs_name, " _
                & "  a.cogsc_mtl_total, " _
                & "  a.cogsc_srv_total, " _
                & "  a.cogscr_total, " _
                & "  b.cogsci_seq, " _
                & "  b.cogsci_pt_id, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  c.pt_desc2, " _
                & "  d.code_name as unit_measure, " _
                & "  b.cogsci_qty " _
                & "FROM " _
                & "  public.cogsc_calc a " _
                & "  INNER JOIN public.cogsci_item b ON (a.cogsc_oid = b.cogsci_cogsc_oid) " _
                & "  INNER JOIN public.pt_mstr c ON (b.cogsci_pt_id = c.pt_id) " _
                & "  INNER JOIN public.code_mstr d ON (c.pt_um = d.code_id) " _
                & "  INNER JOIN public.en_mstr e ON (a.cogsc_en_id = e.en_id) " _
                & "  INNER JOIN public.cs_mstr f ON (a.cogsc_cs_id = f.cs_id) " _
                & "WHERE " _
                & "cogsc_code between '" + be_first.Text + "' and '" + be_to.Text + "'"


            Dim rpt As New XRCogsSimulation

            With rpt
                Dim ds As New DataSet

                ds = master_new.PGSqlConn.ReportDataset(_sql)

                If ds.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", _
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds
                .DataMember = "Table"
                .ShowPreview()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

End Class
