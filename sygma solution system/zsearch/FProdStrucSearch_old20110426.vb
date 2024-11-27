Imports master_new.ModFunction

Public Class FProdStrucSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _obj As Object
    Dim func_data As New function_data

    Private Sub FProdStrucSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Prod. Structure Number", "ps_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "ps_par", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part/BOM", "ptbomdesc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT   " _
                    & "   en_desc,  " _
                    & "   ps_oid,  " _
                    & "   ps_dom_id,  " _
                    & "   ps_en_id,  " _
                    & "   ps_add_by,  " _
                    & "   ps_add_date,  " _
                    & "   ps_upd_by,  " _
                    & "   ps_upd_date,  " _
                    & "   ps_par,  " _
                    & "   ps_id,  " _
                    & "   ps_desc,  " _
                    & "   ps_use_bom, " _
                    & "   ps_pt_bom_id,  " _
                    & "   ps_active, " _
                    & "   CASE WHEN ps_use_bom = 'Y' " _
                    & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=ps_pt_bom_id) " _
                    & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=ps_pt_bom_id) " _
                    & "   END AS ptbomdesc " _
                    & " FROM  " _
                    & "   public.ps_mstr  " _
                    & "  INNER JOIN en_mstr on (ps_mstr.ps_en_id = en_mstr.en_id) "

        Return get_sequel

    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
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
        If fobject.name = FProdStrucTree.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
        ElseIf fobject.name = FPartnumberSubtitute.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_id")
        ElseIf fobject.name = FWorkOrder.Name Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_par")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ps_pt_bom_id")
            fobject.part_desc.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptbomdesc")
            Dim ssql As String
            ssql = "select pt_ro_id,ro_desc from pt_mstr inner join ro_mstr on (pt_ro_id=ro_id) where pt_id=" & ds.Tables(0).Rows(_row_gv).Item("ps_pt_bom_id")
            Dim _row As DataRow = master_new.PGSqlConn.GetRowInfo(ssql)

            If _row Is Nothing Then
            Else
                fobject.wo_ro_id.text = _row("ro_desc")
                fobject.wo_ro_id.tag = _row("pt_ro_id")
            End If
            
        End If


    End Sub
End Class
