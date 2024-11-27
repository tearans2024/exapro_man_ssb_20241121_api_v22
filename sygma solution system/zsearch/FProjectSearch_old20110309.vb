Imports master_new.ModFunction

Public Class FProjectSearch
    Public _row, _cu_id, _cc_id, _en_id, _ptnr_id As Integer

    Private Sub FProjectSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        format_grid()
        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        If fobject.name = "FProjectRelated" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Project Name", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        ElseIf fobject.Name = "FProjectActivityOrder" Then
            add_column(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Project Name", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FProjectRelated" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & "order by prj_code asc "
        ElseIf fobject.name = "FProjectActivityOrder" Then
            get_sequel = "SELECT  " _
                        & "  prj_oid, " _
                        & "  prj_dom_id, " _
                        & "  prj_en_id, " _
                        & "  prj_code, " _
                        & "  prj_ptnr_id_sold, " _
                        & "  prj_ptnr_id_bill, " _
                        & "  prj_sales_person_id, " _
                        & "  prj_pjt_code_id, " _
                        & "  prj_ord_date, " _
                        & "  prj_si_id, " _
                        & "  prj_cu_id, " _
                        & "  prj_exc_rate, " _
                        & "  prj_credit_term, " _
                        & "  prj_tran_id, " _
                        & "  prj_trans_id, " _
                        & "  prj_pocust_oid, " _
                        & "  prj_ar_ac_id, " _
                        & "  prj_ar_sb_id, " _
                        & "  prj_ar_cc_id " _
                        & "FROM  " _
                        & "  public.prj_mstr " _
                        & " where prj_en_id = " + SetInteger(_en_id) _
                        & " and prj_ptnr_id_sold = " + SetInteger(_ptnr_id) _
                        & " and prj_trans_id = 'I' " _
                        & "order by prj_code asc "
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
        Dim ds_bantu As New DataSet
        Dim i As Integer

        If fobject.name = "FProjectRelated" Then
            fobject.gv_edit.SetRowCellValue(_row, "prjr_prj_oid", ds.Tables(0).Rows(_row_gv).Item("prj_oid"))
            fobject.prjr_prj_oid.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FProjectActivityOrder" Then
            fobject.pao_project_code.text = ds.Tables(0).Rows(_row_gv).Item("prj_code")
            fobject._prj_oid = ds.Tables(0).Rows(_row_gv).Item("prj_oid")

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
                            & "  where prjd_prj_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("prj_oid").ToString()) + "" _
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
            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("paod_oid") = Guid.NewGuid.ToString
                _dtrow("paod_prjd_oid") = ds_bantu.Tables(0).Rows(i).Item("prjd_oid")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("prjd_pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc1")
                _dtrow("prjd_pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("prjd_pt_desc2")
                _dtrow("prj_code") = ds_bantu.Tables(0).Rows(i).Item("prj_code")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("prjd_qty_sisa")
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

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
