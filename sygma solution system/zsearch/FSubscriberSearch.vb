Imports master_new.ModFunction

Public Class FSubscriberSearch

    Public _row As Integer
    Public _en_id As Integer
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FSubscriberSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
       
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Date", "ssc_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Subscriber Number", "ssc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Subscriber Description", "ssc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer's Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address 1", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address 2", "ptnra_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Bandwidth", "ssc_bandwidth", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Is Trial", "ssc_is_trial", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Trial Long", "ssc_trial_long", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FTicketing" Then
            get_sequel = "SELECT  " _
                        & "  ssc_oid, " _
                        & "  ssc_dom_id, " _
                        & "  ssc_en_id, " _
                        & "  en_desc, " _
                        & "  ssc_code, " _
                        & "  ssc_date, " _
                        & "  ssc_desc, " _
                        & "  ssc_ptnr_id, " _
                        & "  ptnr_name, " _
                        & "  ptnr_occp, " _
                        & "  ptnr_comp_name, " _
                        & "  ssc_is_trial, " _
                        & "  ssc_bandwidth_id, " _
                        & "  ptnra_addr.ptnra_line_1 , " _
                        & "  ptnra_addr.ptnra_line_2, " _
                        & "  bandwidth.code_name as ssc_bandwidth, " _
                        & "  ssc_trial_long " _
                        & "FROM  " _
                        & "  public.ssc_mstr" _
                        & "  inner join en_mstr on en_id = ssc_en_id " _
                        & "  inner join ptnr_mstr on ptnr_id = ssc_ptnr_id " _
                        & "  left outer JOIN ptnra_addr ON ptnr_mstr.ptnr_oid = ptnra_addr.ptnra_ptnr_oid " _
                        & "  inner join code_mstr bandwidth on bandwidth.code_id = ssc_bandwidth_id " _
                        & "  inner join wf_mstr wm on wf_ref_oid = ssc_oid " _
                        & "  inner join tconfuser on usernama = ssc_add_by " _
                        & "  where wf_user_id in (" + get_user_approval() + ")" _
                        & "  and wf_iscurrent ~~* 'Y'" _
                        & "  and ssc_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & "  and ssc_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  and ssc_en_id in (select user_en_id from tconfuserentity " _
                                             & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        ElseIf fobject.name = "FInstalationRequest" Then
            get_sequel = "SELECT  " _
                         & "  ssc_oid, " _
                         & "  ssc_dom_id, " _
                         & "  ssc_en_id, " _
                         & "  en_desc, " _
                         & "  ssc_code, " _
                         & "  ssc_date, " _
                         & "  ssc_desc, " _
                         & "  ssc_ptnr_id, " _
                         & "  ptnr_name, " _
                         & "  ptnr_occp, " _
                         & "  ptnr_comp_name, " _
                         & "  ssc_is_trial, " _
                         & "  ssc_bandwidth_id, " _
                         & "  tck_schd_end, " _
                         & "  ptnra_addr.ptnra_line_1 , " _
                         & "  ptnra_addr.ptnra_line_2, " _
                         & "  bandwidth.code_name as ssc_bandwidth, " _
                         & "  ssc_trial_long " _
                         & "FROM  " _
                         & "  public.ssc_mstr" _
                         & "  inner join en_mstr on en_id = ssc_en_id " _
                         & "  inner join ptnr_mstr on ptnr_id = ssc_ptnr_id " _
                         & "  left outer JOIN ptnra_addr ON ptnr_mstr.ptnr_oid = ptnra_addr.ptnra_ptnr_oid " _
                         & "  inner join code_mstr bandwidth on bandwidth.code_id = ssc_bandwidth_id " _
                         & "  inner join wf_mstr wm on wf_ref_oid = ssc_oid " _
                         & "  inner join tconfuser on usernama = ssc_add_by " _
                         & "  inner join tck_mstr on tck_ssc_oid = ssc_oid " _
                         & "  where wf_user_id in (" + get_user_approval() + ")" _
                         & "  and wf_iscurrent ~~* 'Y'" _
                         & "  and ssc_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                         & "  and ssc_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                         & "  and ssc_en_id in (select user_en_id from tconfuserentity " _
                                              & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        End If

        
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

        If fobject.name = "FTicketing" Then
            fobject._ssc_oid = SetString(ds.Tables(0).Rows(_row_gv).Item("ssc_oid"))
            fobject.ssc_code.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ssc_code"))
            fobject.ssc_cust_name.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.ssc_cust_address.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") & " " & ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2"))
            fobject.ssc_bandwidth.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ssc_bandwidth"))
        ElseIf fobject.name = "FInstalationRequest" Then
            fobject._req_ssc_oid = SetString(ds.Tables(0).Rows(_row_gv).Item("ssc_oid"))
            fobject.ssc_code.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ssc_code"))
            fobject.req_due_date.editvalue = SetString(ds.Tables(0).Rows(_row_gv).Item("tck_schd_end"))
        End If
        
    End Sub

    Private Function get_user_approval() As String
        get_user_approval = "'" + master_new.ClsVar.sNama + "',"
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupnama from tconfusergroup ug " + _
                           " inner join tconfgroup g on g.groupid = ug.groupid " + _
                           " where userid = " + master_new.ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wfs_status")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            get_user_approval = get_user_approval + "'" + ds_bantu.Tables(0).Rows(i).Item("groupnama") + "',"
        Next
        get_user_approval = get_user_approval.Substring(0, Len(get_user_approval) - 1)
        Return get_user_approval
    End Function
End Class
