Imports master_new.ModFunction

Public Class FPartnerSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _conf_value As String
    Dim _now As DateTime
    Public _is_supl, _is_cust As String

    Private Sub FPartnerSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        get_sequel = "SELECT  " _
            & "  ptnr_oid, " _
            & "  ptnr_dom_id, " _
            & "  ptnr_en_id,en_desc, " _
            & "  ptnr_add_by, " _
            & "  ptnr_add_date, " _
            & "  ptnr_upd_by, " _
            & "  ptnr_upd_date, " _
            & "  ptnr_id, " _
            & "  ptnr_code, " _
            & "  ptnr_name, " _
            & "  ptnr_ptnrg_id, " _
            & "  ptnr_url, " _
            & "  ptnr_remarks, " _
            & "  ptnr_parent, " _
            & "  ptnr_is_cust, " _
            & "  ptnr_is_vend, " _
            & "  ptnr_active, " _
            & "  ptnr_dt, " _
            & "  ptnr_ac_ar_id, " _
            & "  ptnr_sb_ar_id, " _
            & "  ptnr_cc_ar_id, " _
            & "  ptnr_ac_ap_id, " _
            & "  ptnr_sb_ap_id, " _
            & "  ptnr_cc_ap_id, " _
            & "  ptnr_cu_id, " _
            & "  ptnr_limit_credit, " _
            & "  ptnr_is_member, " _
            & "  ptnr_prepaid_balance, " _
            & "  ptnr_npwp, " _
            & "  ptnr_is_emp, " _
            & "  ptnr_name_alt, " _
            & "  ptnr_nppkp, " _
            & "  ptnr_credit_term, " _
            & "  ptnr_tax_class, " _
            & "  ptnr_taxable, " _
            & "  ptnr_name_bank " _
            & "FROM  " _
            & "  public.ptnr_mstr " _
            & "  inner join en_mstr on en_id = ptnr_en_id " _
            & "  where ptnr_is_cust = " & SetSetring(_is_cust) _
            & "  and ptnr_is_vend = " & SetSetring(_is_supl) _
            & "  and ptnr_en_id = " & SetInteger(_en_id) _
            & "  and ptnr_active = 'Y'" _
            & "  order by ptnr_name asc "
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
        Dim i As Integer

        If fobject.name = "FProjectActivityOrder" Then
            fobject.pao_ptnr_id.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject._ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  ptnr_name,prj_code,prj_ord_date,prj_cu_id, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class, " _
                            & "  prjd_trans_id, " _
                            & "  prjd_qty_pao, " _
                            & "  prjd_qty - coalesce(prjd_qty_pao,0) as prjd_qty_sisa, " _
                            & "  prjd_qty_mo " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = prjd_um " _
                            & "  where prjd_trans_id = 'I' " _
                            & "  and prj_ptnr_id_sold = " + SetInteger(ds.Tables(0).Rows(_row_gv).Item("ptnr_id")) + "" _
                            & "  and prjd_qty - coalesce(prjd_qty_pao,0) > 0 " _
                            & "  order by prjd_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "prjd_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()
            'If ds_bantu.Tables(0).Rows.Count > 0 Then
            '    If ds_bantu.Tables(0).Rows(0).Item("prj_cu_id") <> master_new.ClsVar.ibase_cur_id Then
            '        _exc_rate = func_data.get_exchange_rate(ds_bantu.Tables(0).Rows(0).Item("prj_cu_id"))
            '        If _exc_rate = 1 Then
            '            fobject.rcv_exc_rate.EditValue = 0
            '        Else
            '            fobject.rcv_exc_rate.EditValue = _exc_rate
            '        End If
            '    Else
            '        fobject.rcv_exc_rate.EditValue = 1
            '    End If
            'End If

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("paod_oid") = Guid.NewGuid.ToString
                _dtrow("paod_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                _dtrow("prj_code") = ds_bantu.Tables(0).Rows(i).Item("prj_code")
                _dtrow("paod_qty") = ds_bantu.Tables(0).Rows(i).Item("prjd_qty_sisa")
                _dtrow("paod_um") = ds_bantu.Tables(0).Rows(i).Item("prjd_um")
                _dtrow("unit_measure") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                _dtrow("paod_eta_target") = Today()
                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()

        End If
    End Sub
End Class
