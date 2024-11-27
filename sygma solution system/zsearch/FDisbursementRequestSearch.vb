Imports master_new.ModFunction

Public Class FDisbursementRequestSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _ptnr_id As Integer
    Public _cu_id As Integer
    Public _type As String
    Public _obj As Object
    Public _pdpr_id As Integer
    Public cash_oid_value As String
    Dim func_data As New function_data
    Dim func_coll As New function_collection

    Dim _conf_value As String
    Dim _now As DateTime

    Private Sub FPengajuanBiayaSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ant 26 jan 2012
        If fobject.name = "FDisbursementVoucher" Then
            sb_fill.Visible = True
            gv_master.Columns(0).Visible = True
        End If
        '----------------------------

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_disbursement_request")
        'help_load_data(True)
        'gv_master.Focus()
        
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Disbursement Request Number", "pby_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Disbursement Request Date", "pby_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "pby_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pby_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Employee", "xemp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Update", "pby_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pby_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FDisbursementVoucher" Then
            'ant 26 jan 2012
            sb_fill.Visible = True
            gv_master.Columns.Item("is_checked").Visible = True
            get_sequel = "SELECT  " _
                    & "  false as is_checked, " _
                    & "  pby_oid, " _
                    & "  pby_dom_id, " _
                    & "  pby_en_id,en_desc, " _
                    & "  pby_add_by, " _
                    & "  pby_add_date, " _
                    & "  pby_upd_by, " _
                    & "  pby_upd_date, " _
                    & "  pby_code, " _
                    & "  pby_date, " _
                    & "  pby_type, " _
                    & "  pby_cc_id, cc_desc, " _
                    & "  pby_sb_id, sb_desc, " _
                    & "  pby_remarks, " _
                    & "  pby_tran_id, " _
                    & "  pby_trans_id, " _
                    & "  pby_dt, " _
                    & "  pby_ac_id,ac_code, ac_name, " _
                    & "  pby_bk_id,bk_name, " _
                    & "  pby_cu_id, cu_code, " _
                    & "  pby_exc_rate, " _
                    & "  pby_peruntukan_id,code_name as peruntukan, pby_xemp_id, xemp_name " _
                    & "FROM  " _
                    & "  public.pby_mstr " _
                    & " inner join en_mstr on en_id = pby_en_id  " _
                    & " inner join code_mstr on code_id = pby_peruntukan_id " _
                    & " inner join cc_mstr on cc_id = pby_cc_id " _
                    & " inner join sb_mstr on sb_id = pby_sb_id " _
                    & " inner join ac_mstr on ac_id = pby_ac_id " _
                    & " inner join bk_mstr on bk_id = pby_bk_id " _
                    & " inner join cu_mstr on cu_id = pby_cu_id " _
                    & " left outer join xemp_mstr on xemp_id = pby_xemp_id "
            '-------------------------
            If _pdpr_id <> Nothing Then
                get_sequel = get_sequel + " inner join pdprd_det on pdprd_ref_oid = pby_oid " _
                                        & " inner join pdpr_periode on pdpr_oid = pdprd_pdpr_oid "
            End If

            get_sequel = get_sequel + " where pby_date >= " + SetDate(pr_txttglawal.DateTime) _
                                    & " and pby_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                    & " and pby_type = " + SetSetring(_type) _
                                    & " and pby_en_id = " + _en_id.ToString() _
                                    & " and pby_cu_id = " + _cu_id.ToString() _
                                    & " and pby_trans_id not in ('C','X') "

            If _pdpr_id <> Nothing Then
                get_sequel = get_sequel + " and pdpr_id =  " + _pdpr_id.ToString
            End If

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and pby_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by pby_code asc "
        ElseIf fobject.name = "FDisbursementRequest" Then
            get_sequel = "SELECT  " _
                    & "  pby_oid, " _
                    & "  pby_dom_id, " _
                    & "  pby_en_id,en_desc, " _
                    & "  pby_add_by, " _
                    & "  pby_add_date, " _
                    & "  pby_upd_by, " _
                    & "  pby_upd_date, " _
                    & "  pby_code, " _
                    & "  pby_date, " _
                    & "  pby_type, " _
                    & "  pby_cc_id, cc_desc, " _
                    & "  pby_sb_id, sb_desc, " _
                    & "  pby_remarks, " _
                    & "  pby_tran_id, " _
                    & "  pby_trans_id, " _
                    & "  pby_dt, " _
                    & "  pby_ac_id,ac_code, ac_name, " _
                    & "  pby_bk_id,bk_name, " _
                    & "  pby_cu_id, cu_code, " _
                    & "  pby_exc_rate, " _
                    & "  pby_peruntukan_id,code_name as peruntukan, pby_xemp_id, xemp_name " _
                    & "FROM  " _
                    & "  public.pby_mstr " _
                    & " inner join en_mstr on en_id = pby_en_id  " _
                    & " inner join code_mstr on code_id = pby_peruntukan_id " _
                    & " inner join cc_mstr on cc_id = pby_cc_id " _
                    & " inner join sb_mstr on sb_id = pby_sb_id " _
                    & " inner join ac_mstr on ac_id = pby_ac_id " _
                    & " inner join bk_mstr on bk_id = pby_bk_id " _
                    & " inner join cu_mstr on cu_id = pby_cu_id " _
                    & " left outer join xemp_mstr on xemp_id = pby_xemp_id " _
                    & " where pby_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and pby_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and pby_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            get_sequel = get_sequel + " order by pby_code asc "
        ElseIf fobject.name = "FDisbursementRealization" Then
            get_sequel = "SELECT  " _
                    & "  pby_oid, " _
                    & "  pby_dom_id, " _
                    & "  pby_en_id,en_desc, " _
                    & "  pby_add_by, " _
                    & "  pby_add_date, " _
                    & "  pby_upd_by, " _
                    & "  pby_upd_date, " _
                    & "  pby_code, " _
                    & "  pby_date, " _
                    & "  pby_type, " _
                    & "  pby_cc_id, cc_desc, " _
                    & "  pby_sb_id, sb_desc, " _
                    & "  pby_remarks, " _
                    & "  pby_tran_id, " _
                    & "  pby_trans_id, " _
                    & "  pby_dt, " _
                    & "  pby_ac_id,ac_code, ac_name, " _
                    & "  pby_bk_id,bk_name, " _
                    & "  pby_cu_id, cu_code, " _
                    & "  pby_exc_rate, " _
                    & "  pby_peruntukan_id,code_name as peruntukan, pby_xemp_id, xemp_name " _
                    & "FROM  " _
                    & "  public.pby_mstr " _
                    & " inner join en_mstr on en_id = pby_en_id  " _
                    & " inner join code_mstr on code_id = pby_peruntukan_id " _
                    & " inner join cc_mstr on cc_id = pby_cc_id " _
                    & " inner join sb_mstr on sb_id = pby_sb_id " _
                    & " inner join ac_mstr on ac_id = pby_ac_id " _
                    & " inner join bk_mstr on bk_id = pby_bk_id " _
                    & " inner join cu_mstr on cu_id = pby_cu_id " _
                    & " left outer join xemp_mstr on xemp_id = pby_xemp_id " _
                    & " where pby_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and pby_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & "  and pby_type = 'K' " _
                    & "  and pby_en_id = " + _en_id.ToString() _
                    & "  and pby_cu_id = " + _cu_id.ToString() _
                    & "  and pby_trans_id not in ('C','X') "

            If _conf_value = "1" Then
                get_sequel = get_sequel + " and pby_trans_id ~~* 'I' "
            End If

            get_sequel = get_sequel + " order by pby_code asc "
        ElseIf fobject.name = "FDisbursementRealizationKasbon" Then
            Try


                ' Taufik / 11 Maret 2011

                get_sequel = "SELECT  " _
                        & "  pby_oid, " _
                        & "  pby_dom_id, " _
                        & "  pby_en_id,en_desc, " _
                        & "  pby_add_by, " _
                        & "  pby_add_date, " _
                        & "  pby_upd_by, " _
                        & "  pby_upd_date, " _
                        & "  pby_code, " _
                        & "  pby_date, " _
                        & "  pby_type, " _
                        & "  pby_cc_id, cc_desc, " _
                        & "  pby_sb_id, sb_desc, " _
                        & "  pby_remarks, " _
                        & "  pby_tran_id, " _
                        & "  pby_trans_id, " _
                        & "  pby_dt, " _
                        & "  pby_ac_id,ac_code, ac_name, " _
                        & "  pby_bk_id,bk_name, " _
                        & "  pby_cu_id, cu_code, " _
                        & "  pby_exc_rate, " _
                        & "  pby_peruntukan_id,code_name as peruntukan, pby_xemp_id, xemp_name, cashdp_cash_oid " _
                        & "FROM  " _
                        & "  public.pby_mstr " _
                        & " inner join en_mstr on en_id = pby_en_id  " _
                        & " inner join code_mstr on code_id = pby_peruntukan_id " _
                        & " inner join cc_mstr on cc_id = pby_cc_id " _
                        & " inner join sb_mstr on sb_id = pby_sb_id " _
                        & " inner join ac_mstr on ac_id = pby_ac_id " _
                        & " inner join bk_mstr on bk_id = pby_bk_id " _
                        & " inner join cu_mstr on cu_id = pby_cu_id " _
                        & " inner join cashdp_pd on cashdp_pby_oid = pby_oid " _
                        & " left outer join xemp_mstr on xemp_id = pby_xemp_id " _
                        & " where pby_date >= " + SetDate(pr_txttglawal.DateTime) _
                        & " and pby_date <= " + SetDate(pr_txttglakhir.DateTime) _
                        & "  and pby_type = 'K' " _
                        & "  and pby_en_id = " + _en_id.ToString() _
                        & "  and pby_cu_id = " + _cu_id.ToString() _
                        & "  and cashdp_cash_oid = " + SetSetring(cash_oid_value) _
                        & "  and pby_trans_id not in ('C','X') "

                If _conf_value = "1" Then
                    get_sequel = get_sequel + " and pby_trans_id ~~* 'I' "
                End If

                get_sequel = get_sequel + " order by pby_code asc "
            Catch ex As Exception
                MsgBox(Err.Description)
            End Try
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

        Dim ds_bantu As New DataSet
        'Dim i As Integer

        If fobject.name = "FDisbursementVoucher" Then
            fobject.gv_edit_pd.SetRowCellValue(_row, "cashdp_pby_oid", ds.Tables(0).Rows(_row_gv).Item("pby_oid"))
            fobject.gv_edit_pd.SetRowCellValue(_row, "pby_code", ds.Tables(0).Rows(_row_gv).Item("pby_code"))
            fobject.gv_edit_pd.SetRowCellValue(_row, "pby_ac_id", ds.Tables(0).Rows(_row_gv).Item("pby_ac_id"))
            fobject.gv_edit_pd.SetRowCellValue(_row, "pby_ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_pd.BestFitColumns()
        ElseIf fobject.name = "FDisbursementRealization" Then
            fobject.gv_edit_pd.SetRowCellValue(_row, "cashdp_pby_oid", ds.Tables(0).Rows(_row_gv).Item("pby_oid"))
            fobject.gv_edit_pd.SetRowCellValue(_row, "pby_code", ds.Tables(0).Rows(_row_gv).Item("pby_code"))
            fobject.gv_edit_pd.SetRowCellValue(_row, "pby_ac_id", ds.Tables(0).Rows(_row_gv).Item("pby_ac_id"))
            fobject.gv_edit_pd.SetRowCellValue(_row, "pby_ac_name", ds.Tables(0).Rows(_row_gv).Item("ac_name"))
            fobject.gv_edit_pd.BestFitColumns()
        ElseIf fobject.name = "FDisbursementRequest" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pby_code")
            fobject._pby_oid_copy = ds.Tables(0).Rows(_row_gv).Item("pby_oid")
        End If
    End Sub

    'ant 26 jan 2012
    Private Sub sb_fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_fill.Click
        Try
            Dim _row_pos As Integer
            Dim jml As Integer = 0
            ds.Tables(0).AcceptChanges()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("is_checked") = True Then
                    If jml = 0 Then
                        fobject.gv_edit_pd.SetRowCellValue(_row, "cashdp_pby_oid", ds.Tables(0).Rows(i).Item("pby_oid"))
                        fobject.gv_edit_pd.SetRowCellValue(_row, "pby_code", ds.Tables(0).Rows(i).Item("pby_code"))
                        fobject.gv_edit_pd.SetRowCellValue(_row, "pby_ac_id", ds.Tables(0).Rows(i).Item("pby_ac_id"))
                        fobject.gv_edit_pd.SetRowCellValue(_row, "pby_ac_name", ds.Tables(0).Rows(i).Item("ac_name"))
                        jml = jml + 1
                    Else
                        fobject.gv_edit_pd.AddNewRow()
                        _row_pos = fobject.gv_edit_pd.FocusedRowHandle()
                        fobject.gv_edit_pd.SetRowCellValue(_row_pos, "cashdp_pby_oid", ds.Tables(0).Rows(i).Item("pby_oid"))
                        fobject.gv_edit_pd.SetRowCellValue(_row_pos, "pby_code", ds.Tables(0).Rows(i).Item("pby_code"))
                        fobject.gv_edit_pd.SetRowCellValue(_row_pos, "pby_ac_id", ds.Tables(0).Rows(i).Item("pby_ac_id"))
                        fobject.gv_edit_pd.SetRowCellValue(_row_pos, "pby_ac_name", ds.Tables(0).Rows(i).Item("ac_name"))
                    End If
                End If
            Next

            fobject.gv_edit_pd.BestFitColumns()
            Me.Close()
        Catch
        End Try
    End Sub
End Class
