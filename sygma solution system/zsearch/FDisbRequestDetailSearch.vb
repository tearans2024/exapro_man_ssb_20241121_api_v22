Imports master_new.ModFunction

Public Class FDisbRequestDetailSearch
    Public _row, _en_id As Integer
    Public _pby_oid As String
    Dim func_data As New function_data

    Private Sub FPtProjectDetailSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "pbyd_oid", False)
        add_column(gv_master, "pbyd_pby_oid", False)
        add_column(gv_master, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "pbyd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remarks", "pbyd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Activity", "activity_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Ref. Project", "pjc_ref_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Open Amount", "amount_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        get_sequel = "SELECT  " _
                            & "  pbyd_oid, " _
                            & "  pbyd_pby_oid, " _
                            & "  pbyd_seq, " _
                            & "  pbyd_ac_id, ac_code, ac_name, " _
                            & "  pbyd_sb_id,sb_desc, " _
                            & "  pbyd_cc_id,cc_desc, " _
                            & "  pbyd_desc, " _
                            & "  pbyd_remarks, " _
                            & "  pbyd_pjc_id, pjc_mstr.pjc_desc, " _
                            & "  pbyd_pjc_ref_id, pjc_ref.pjc_desc as pjc_ref_desc, " _
                            & "  pbyd_amount - (coalesce(pbyd_realisasi_amount,0) + coalesce(pbyd_pengembalian_amount,0)) as amount_open, " _
                            & "  pbyd_activity_code_id, " _
                            & "  code_name as activity_name, " _
                            & "  pbyd_loc_eu_site_id, loc_desc, pbyd_site_relok, " _
                            & "  pbyd_dt " _
                            & "FROM  " _
                            & "  public.pbyd_det " _
                            & " inner join pby_mstr on pby_oid = pbyd_pby_oid " _
                            & " inner join cc_mstr on cc_id = pbyd_cc_id " _
                            & " inner join sb_mstr on sb_id = pbyd_sb_id " _
                            & " inner join ac_mstr on ac_id = pbyd_ac_id " _
                            & " left outer join pjc_mstr on pjc_mstr.pjc_id = pbyd_pjc_id " _
                            & " left outer join pjc_mstr as pjc_ref on pjc_ref.pjc_id = pbyd_pjc_ref_id " _
                            & " left outer join loc_mstr on loc_id = pbyd_loc_eu_site_id " _
                            & " left outer join code_mstr on code_id = pbyd_activity_code_id " _
                            & " where pbyd_pby_oid = '" + _pby_oid + "'" _
                            & " and pby_en_id = " + _en_id.ToString _
                            & " order by ac_code "

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub


    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FDisbursementRealization" Then

            fobject.gv_edit_cash.SetRowCellValue(_row, "cashd_pbyd_oid", ds.Tables(0).Rows(_row_gv).Item("pbyd_oid"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "ref_ac_id", ds.Tables(0).Rows(_row_gv).Item("pbyd_ac_id"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "ref_ac_code", ds.Tables(0).Rows(_row_gv).Item("ac_code"))
            fobject.gv_edit_cash.SetRowCellValue(_row, "ref_ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
        End If

    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
