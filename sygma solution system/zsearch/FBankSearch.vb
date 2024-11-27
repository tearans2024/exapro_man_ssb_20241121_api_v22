Imports master_new.ModFunction

Public Class FBankSearch
    Public _row, _en_id As Integer
    Public _type As String

    Private Sub FBankSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "bk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_code", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FPaymentPeriod" Then
            get_sequel = "SELECT  " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.bk_mstr.bk_id, " _
                        & "  public.bk_mstr.bk_code, " _
                        & "  public.bk_mstr.bk_name, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.cu_mstr.cu_code " _
                        & "FROM " _
                        & "  public.bk_mstr " _
                        & "  INNER JOIN public.en_mstr ON (public.bk_mstr.bk_en_id = public.en_mstr.en_id)" _
                        & "  INNER JOIN public.cu_mstr ON (public.bk_mstr.bk_cu_id = public.cu_mstr.cu_id) " _
                        & "  INNER JOIN public.ac_mstr ON (public.bk_mstr.bk_ac_id = public.ac_mstr.ac_id)" _
                        & "WHERE  bk_active = 'Y'" _
                        & "  and bk_en_id = " & _en_id
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

        If fobject.name = "FPaymentPeriod" Then
            If _type = "ap_gen" Then
                fobject.gv_ap_edit.SetRowCellValue(_row, "pdprd_bk_id", ds.Tables(0).Rows(_row_gv).Item("bk_id"))
                fobject.gv_ap_edit.SetRowCellValue(_row, "bk_name", ds.Tables(0).Rows(_row_gv).Item("bk_name"))
                fobject.gv_ap_edit.BestFitColumns()
            ElseIf _type = "disb_gen" Then
                fobject.gv_disb_edit.SetRowCellValue(_row, "pdprd_bk_id", ds.Tables(0).Rows(_row_gv).Item("bk_id"))
                fobject.gv_disb_edit.SetRowCellValue(_row, "bk_name", ds.Tables(0).Rows(_row_gv).Item("bk_name"))
                fobject.gv_disb_edit.BestFitColumns()
            End If
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
