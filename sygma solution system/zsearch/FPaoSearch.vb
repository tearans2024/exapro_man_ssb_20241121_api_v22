Imports master_new.ModFunction

Public Class FPaoSearch
    Public _obj As Object
    Public _row, _cu_id, _cc_id, _en_id, _ptnr_id As Integer

    Private Sub FPaoSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        de_1.EditValue = Today()
        de_2.EditValue = Today()

        Me.Width = 800
        Me.Height = 360
        gv_master.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "PAO Code", "pao_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "PAO Date", "pao_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FMO" Then
            get_sequel = "SELECT  " _
                        & "  pao_oid, " _
                        & "  pao_dom_id, " _
                        & "  pao_en_id, " _
                        & "  pao_add_by, " _
                        & "  pao_add_date, " _
                        & "  pao_upd_by, " _
                        & "  pao_upd_date, " _
                        & "  pao_dt, " _
                        & "  pao_trans_id, " _
                        & "  pao_tran_id, " _
                        & "  pao_code, " _
                        & "  pao_date, " _
                        & "  pao_ptnr_id,ptnr_name, " _
                        & "  pao_remarks " _
                        & "FROM  " _
                        & "  public.pao_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = pao_ptnr_id " _
                        & " where pao_en_id = " + SetInteger(_en_id) _
                        & " and pao_trans_id = 'I' " _
                        & " and (pao_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and pao_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by pao_code asc "
        ElseIf fobject.name = "FPAODeleteLine" Then
            get_sequel = "SELECT  " _
                        & "  pao_oid, " _
                        & "  pao_dom_id, " _
                        & "  pao_en_id, " _
                        & "  pao_add_by, " _
                        & "  pao_add_date, " _
                        & "  pao_upd_by, " _
                        & "  pao_upd_date, " _
                        & "  pao_dt, " _
                        & "  pao_trans_id, " _
                        & "  pao_tran_id, " _
                        & "  pao_code, " _
                        & "  pao_date, " _
                        & "  pao_ptnr_id,ptnr_name, " _
                        & "  pao_remarks " _
                        & "FROM  " _
                        & "  public.pao_mstr " _
                        & "  inner join ptnr_mstr on ptnr_id = pao_ptnr_id " _
                        & " where pao_en_id = " + SetInteger(_en_id) _
                        & " and pao_trans_id in ('I','W') " _
                        & " and (pao_date >= " & SetDate(de_1.DateTime.Date) _
                        & " and pao_date <= " & SetDate(de_2.DateTime.Date) & ")" _
                        & "order by pao_code asc "
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

        If fobject.name = "FMO" Then
            fobject.mo_pao_code.text = ds.Tables(0).Rows(_row_gv).Item("pao_code")
            fobject._pao_oid = ds.Tables(0).Rows(_row_gv).Item("pao_oid")

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  paod_oid, " _
                            & "  paod_dom_id, " _
                            & "  paod_en_id, " _
                            & "  paod_add_by, " _
                            & "  paod_add_date, " _
                            & "  paod_upd_by, " _
                            & "  paod_upd_date, " _
                            & "  paod_dt, " _
                            & "  paod_pao_oid,pao_ptnr_id,ptnr_name, " _
                            & "  paod_prjd_oid,pt_code,prjd_pt_desc1,prjd_pt_desc2,prjd_qty, " _
                            & "  prj_code,loc_desc, " _
                            & "  0 as qty_open, " _
                            & "  paod_qty, " _
                            & "  paod_um,um.code_name as unit_measure, " _
                            & "  paod_qty_mo, " _
                            & "  paod_qty - coalesce(paod_qty_mo,0) as mod_qty_sisa, " _
                            & "  paod_eta_target, " _
                            & "  paod_eta_confirm, " _
                            & "  paod_etd_target,paod_is_confirm " _
                            & "FROM  " _
                            & "  public.paod_det " _
                            & "  inner join pao_mstr on pao_oid = paod_pao_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = pao_ptnr_id " _
                            & "  inner join prjd_det on prjd_oid = paod_prjd_oid " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = paod_um " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  where paod_pao_oid = " + SetSetring(ds.Tables(0).Rows(_row_gv).Item("pao_oid").ToString()) + "" _
                            & "  and paod_qty - coalesce(paod_qty_mo,0) > 0 " _
                            & "  order by pt_code asc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "paod_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            fobject.ds_edit.tables(0).clear()
            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("mod_oid") = Guid.NewGuid.ToString
                _dtrow("mod_paod_oid") = ds_bantu.Tables(0).Rows(i).Item("paod_oid")
                _dtrow("mod_ptnr_id") = ds_bantu.Tables(0).Rows(i).Item("pao_ptnr_id")
                _dtrow("ptnr_name") = ds_bantu.Tables(0).Rows(i).Item("ptnr_name")
                _dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("mod_qty_sisa")
                _dtrow("mod_qty") = ds_bantu.Tables(0).Rows(i).Item("mod_qty_sisa")
                _dtrow("mod_um") = ds_bantu.Tables(0).Rows(i).Item("paod_um")
                _dtrow("unit_measure") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                _dtrow("mod_eta") = Today()

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FPAODeleteLine" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pao_code")
            fobject._pao_oid = ds.Tables(0).Rows(_row_gv).Item("pao_oid")
            fobject.load_data_many(True)
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

End Class
