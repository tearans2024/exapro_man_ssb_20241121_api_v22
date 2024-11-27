Imports master_new.ModFunction

Public Class FPartnerGroupSearch
    Public _row, _en_id, _ptnrg_id As Integer
    Public _obj As Object
    Dim func_data As New function_data


    Private Sub FPartnerGroupSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Group Code", "ptnrg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "ptnrg_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        Dim _en_id_coll As String = func_data.entity_parent(_en_id)

        get_sequel = "SELECT  " _
     & "  ptnrg_oid, " _
                    & "  ptnrg_dom_id, " _
                    & "  ptnrg_en_id, " _
                    & "  en_code, " _
                    & "  ptnrg_id, " _
                    & "  ptnrg_code, " _
                    & "  ptnrg_name, " _
                    & "  ptnrg_desc, " _
                    & "  ptnrg_desc, " _
                    & "  ptnrg_credit_term, " _
                    & "  credit_term.code_name as credit_term_name, " _
                    & "  ptnrg_payment_methode, " _
                    & "  pay_method.code_name as pay_method_name, " _
                    & "  ptnrg_limit_credit, " _
                    & "  ptnrg_active, " _
                    & "  ptnrg_dt " _
                    & " FROM  " _
                    & " public.ptnrg_grp" _
                    & " inner join public.en_mstr on en_id = ptnrg_en_id" _
                    & " left outer join public.ac_mstr on ac_id = ptnrg_id" _
                    & " inner join code_mstr credit_term on credit_term.code_id = ptnrg_credit_term " _
                    & " inner join code_mstr pay_method on pay_method.code_id = ptnrg_payment_methode " _
            & "WHERE " _
            & "  ptnrg_en_id in (" + _en_id_coll + ")"



        'get_sequel = "SELECT  " _
        '& "  a.ptnrg_oid, " _
        '& " a.ptnrg_en_id, " _
        '& "  a.ptnrg_id, " _
        '& "  a.ptnrg_code, " _
        '& "  a.ptnrg_name, " _
        '& "  a.ptnrg_desc, " _
        '& "  a.ptnrg_limit_credit " _
        '& "FROM " _
        '& "  public.ptnrg_grp a " _
        '& "WHERE " _
        '& " a.ptnrg_active = 'Y'" _
        '& "ORDER BY " _
        '& " a.ptnrg_code"
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

        Dim ds_bantu As New DataSet
        Dim i As Integer

        If fobject.name = FPartnerAll.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnrg_code")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnrg_oid")
            fobject.pa_ptnrg_grp_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("ptnrg_name")
            fobject.ptnr_limit_credit.text = ds.Tables(0).Rows(_row_gv).Item("ptnrg_limit_credit")
            fobject.pa_credit_term.EditValue = ds.Tables(0).Rows(_row_gv).Item("credit_term_name")
            fobject.pa_pay_method.EditValue = ds.Tables(0).Rows(_row_gv).Item("pay_method_name")

        End If
    End Sub


    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub

End Class
