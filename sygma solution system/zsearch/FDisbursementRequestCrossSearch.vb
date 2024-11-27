Imports master_new.ModFunction

Public Class FDisbursementRequestCrossSearch

    Public _en_id As Integer
    Public _button As String
    Public _type As String
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FPengajuanBiayaSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_disbursement_request")
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "pbyd_oid", False)
        add_column(gv_master, "pbyd_pby_oid", False)
        add_column(gv_master, "Disbursement Request Number", "pby_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Disbursement Request Date", "pby_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site ID", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "pbyd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Activity", "activity_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Amount", "pbyd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  pbyd_oid, " _
            & "  pbyd_pby_oid, " _
            & "  pbyd_seq, " _
            & "  pbyd_ac_id, ac_code, ac_name," _
            & "  pbyd_sb_id,sb_desc, " _
            & "  pbyd_cc_id,cc_desc, " _
            & "  pbyd_desc, " _
            & "  pbyd_pjc_id, pjc_desc, " _
            & "  pbyd_amount, " _
            & "  pbyd_amount_pay, " _
            & "  pbyd_amount as pbyd_amount_remaining, " _
            & "  pbyd_activity_code_id, " _
            & "  code_name as activity_name, " _
            & "  pbyd_loc_eu_site_id,loc_desc, " _
            & "  pby_oid,pby_code,pby_date, " _
            & "  pbyd_dt " _
            & "FROM  " _
            & "  public.pbyd_det " _
            & "  inner join pby_mstr on pby_oid = pbyd_pby_oid " _
            & "  inner join cc_mstr on cc_id = pbyd_cc_id " _
            & "  inner join sb_mstr on sb_id = pbyd_sb_id " _
            & "  inner join ac_mstr on ac_id = pbyd_ac_id " _
            & "  left outer join pjc_mstr on pjc_id = pbyd_pjc_id " _
            & "  left outer join loc_mstr on loc_id = pbyd_loc_eu_site_id " _
            & "  left outer join code_mstr on code_id = pbyd_activity_code_id " _
            & "  where pby_date >= " + SetDate(pr_txttglawal.DateTime) _
            & "  and pby_date <= " + SetDate(pr_txttglakhir.DateTime) _
            & "  and pby_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & "  and pby_add_by = " & SetSetring(master_new.ClsVar.sNama.ToString) & " " _
            & "  and pby_type = 'K'" _
            & "  and coalesce(pby_realisasi_amount,0) = 0" _
            & "  and coalesce(pby_amount_pay,0) >= pby_amount "



        If _conf_value = "1" Then
            get_sequel = get_sequel + " and pby_trans_id ~~* 'I' "
        End If

        get_sequel = get_sequel + " order by pby_code asc "
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
        'Dim i As Integer
        If _button = "pbyc_from" Then
            fobject._pbyc_pbyd_oid_from = ds.Tables(0).Rows(_row_gv).Item("pbyd_oid")
            fobject.be_pbyc_pbyd_oid_from.text = ds.Tables(0).Rows(_row_gv).Item("pby_code")
            fobject.pbyc_account_from.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")

            'Tambahan 24 Aug -------------------------(+ coalesce untuk hitung "pbyd_amount_remaining")
            fobject._max_amount = ds.Tables(0).Rows(_row_gv).Item("pbyd_amount_remaining")
            '-------------------------------------------------

        ElseIf _button = "pbyc_to" Then
            fobject._pbyc_pbyd_oid_to = ds.Tables(0).Rows(_row_gv).Item("pbyd_oid")
            fobject.be_pbyc_pbyd_oid_to.text = ds.Tables(0).Rows(_row_gv).Item("pby_code")
            fobject.pbyc_account_to.text = ds.Tables(0).Rows(_row_gv).Item("ac_name")
        End If

    End Sub

End Class
