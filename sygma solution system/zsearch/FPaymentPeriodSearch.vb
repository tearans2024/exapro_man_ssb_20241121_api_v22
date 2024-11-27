Imports master_new.ModFunction

Public Class FRoyaltiPerSearch
    Public _row, _en_id As Integer

    Private Sub FRoyaltiPerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Periode Name", "pdpr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "From", "pdpr_awal", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "To", "pdpr_akhir", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Status Generate", "pdpr_generate", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  en_code, " _
                    & "  pdpr_oid, " _
                    & "  pdpr_id, " _
                    & "  pdpr_name, " _
                    & "  pdpr_awal, " _
                    & "  pdpr_akhir, " _
                    & "  pdpr_generate " _
                    & " FROM  " _
                    & "  public.pdpr_periode " _
                    & " inner join public.en_mstr on en_id = pdpr_en_id " _
                    & " where (pdpr_name ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and pdpr_en_id in (0," + _en_id.ToString + ")" _
                    & " order by pdpr_name"
        
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

        If fobject.name = "FPaymentRecap" Then

            fobject.pdpr_name.text = ds.Tables(0).Rows(_row_gv).Item("pdpr_name")
            fobject._pdpr_oid = ds.Tables(0).Rows(_row_gv).Item("pdpr_oid")
            fobject._pdpr_awal = ds.Tables(0).Rows(_row_gv).Item("pdpr_awal")
            fobject._pdpr_akhir = ds.Tables(0).Rows(_row_gv).Item("pdpr_akhir")
            fobject.te_awal.text = ds.Tables(0).Rows(_row_gv).Item("pdpr_awal")
            fobject.te_akhir.text = ds.Tables(0).Rows(_row_gv).Item("pdpr_akhir")
            If ds.Tables(0).Rows(_row_gv).Item("pdpr_generate") = "Y" Then
                fobject.sb_generate.enabled = False
            Else
                fobject.sb_generate.enabled = True
            End If
            fobject.gc_ap_edit.datasource = ""
            fobject.gc_disb_edit.datasource = ""
            fobject.gc_ap_edit.Refresh()

        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
