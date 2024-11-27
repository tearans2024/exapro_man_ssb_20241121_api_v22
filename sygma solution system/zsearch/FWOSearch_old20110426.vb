Imports master_new.ModFunction

Public Class FWOSearch
    Public _en_id As Integer
    Dim ds_show As DataSet
    Dim _row_gv As Integer

    Private Sub FWOSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Part Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Bom Description", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Routing Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT wo_oid, " _
                    & "  wo_code, " _
                    & "  wo_type, " _
                    & "  wo_pt_id, " _
                    & "  wo_ord_date, " _
                    & "  wo_due_date, " _
                    & "  wo_bom_id, " _
                    & "  wo_ro_id, " _
                    & "  si_desc,pt_code, " _
                    & "  pt_desc1, " _
                    & "  wo_qty_ord,wo_qty_comp,wo_qty_ord - coalesce(wo_qty_comp,0) as wo_qty_remaining,wo_rel_date, " _
                    & "  ro_desc, " _
                    & "  en_desc, " _
                    & "  wo_dt,wo_status " _
                    & "FROM  " _
                    & "  public.wo_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.wo_mstr.wo_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.wo_mstr.wo_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id) " _
                    & " where wo_en_id = " & _en_id.ToString & " " _
                    & " AND wo_ord_date between " & SetDate(pr_txttglawal.DateTime) & " and " & SetDate(pr_txttglakhir.DateTime)

        If fobject.name = "FWorkOrderRelease" Or fobject.name = FWORelease.Name Then
            get_sequel = get_sequel & " order by wo_code"
        ElseIf fobject.name = "FWorkOrderIssue" Then
            get_sequel = get_sequel & " AND wo_status = 'R' order by wo_code"
        End If
                    

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    
    Private Sub fill_data()

        _row_gv = BindingContext(ds.Tables(0)).Position
        If fobject.name = "FWorkOrderRelease" Then
            fobject.wo_code.text = ds.Tables(0).Rows(_row_gv).Item("wo_code")
        ElseIf fobject.name = FWORelease.Name Then
            fobject._oid_master = ds.Tables(0).Rows(_row_gv).Item("wo_oid")
            fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id")
            fobject.wo_code.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_code")
            fobject._pt_code = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            fobject.pt_desc1.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.wo_qty_ord.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_ord")
            fobject.wo_qty_comp.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_comp")
            fobject.wo_qty_remaining.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_remaining")
            fobject.wo_due_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_due_date")
            fobject.wo_status.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_status")

            fobject.wo_rel_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_rel_date")

        ElseIf fobject.name = "FWorkOrderIssue" Then
            send_data()
        End If

    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Public Sub send_data()

        Dim code As String
        Dim y, z As Integer
        Dim msg As MsgBoxResult

        code = ds.Tables(0).Rows(_row_gv).Item("wo_code")

        ds_show = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  wo_oid, " _
                    & "  wo_dom_id, " _
                    & "  wo_en_id, " _
                    & "  wo_si_id, " _
                    & "  wo_id, " _
                    & "  wo_code, " _
                    & "  wo_type, " _
                    & "  wo_pt_id, " _
                    & "  wo_qty_ord, " _
                    & "  wo_qty_comp, " _
                    & "  wo_qty_rjc, " _
                    & "  wo_ord_date, " _
                    & "  current_timestamp as wo_rel_date, " _
                    & "  wo_due_date, " _
                    & "  wo_yield_pct, " _
                    & "  wo_bom_id, " _
                    & "  wo_ro_id, " _
                    & "  wo_status, " _
                    & "  wo_remarks, " _
                    & "  si_id, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  bom_id, " _
                    & "  bom_desc, " _
                    & "  ro_id, " _
                    & "  ro_desc, " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  wo_dt " _
                    & "FROM  " _
                    & "  public.wo_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.wo_mstr.wo_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.wo_mstr.wo_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.bom_mstr ON (public.wo_mstr.wo_bom_id = public.bom_mstr.bom_id) " _
                    & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id) " _
                    & " where wo_code = '" & code.ToString & "'"

                    .InitializeCommand()
                    .FillDataSet(ds_show, "wo_mstr")

                    If ds_show.Tables("wo_mstr").Rows.Count = 0 Then
                        MsgBox("No Work Order Available To Release.., Please Check WO Number..", MsgBoxStyle.Exclamation, "Warning..!")
                    ElseIf ds_show.Tables("wo_mstr").Rows.Count > 1 Then
                        MsgBox("Work Orders Are More Than One.., Please Check WO Number..", MsgBoxStyle.Exclamation, "Warning..!")
                    Else


                        With ds_show.Tables("wo_mstr").Rows(0)
                            fobject._wo_oid_mstr = .Item("wo_oid")
                            fobject._wo_id = .Item("wo_id")
                            fobject._wocid_en_id = .Item("wo_en_id")
                            fobject.wo_code.EditValue = .Item("wo_code")
                            fobject.wo_en_id.EditValue = .Item("en_desc")
                            fobject.wo_si_id.EditValue = .Item("si_desc")
                            fobject.part_desc.EditValue = .Item("pt_desc1")
                            fobject.wo_due_date.EditValue = .Item("wo_due_date")
                            fobject.wo_ord_date.EditValue = .Item("wo_ord_date")
                            fobject.wo_qty_ord.Text = .Item("wo_qty_ord")
                            fobject.wo_yield_pct.EditValue = .Item("wo_yield_pct")
                            fobject.wo_bom_id.EditValue = .Item("bom_desc")
                            fobject.wo_ro_id.EditValue = .Item("ro_desc")
                            fobject.wo_remarks.EditValue = .Item("wo_remarks")
                        End With

                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_show = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  wod_oid, " _
                    & "  wod_wo_oid, " _
                    & "  wod_use_bom, " _
                    & "  wod_pt_bom_id, " _
                    & "  wod_comp, " _
                    & "  wod_op, " _
                    & "  wod_qty_per, " _
                    & "  wod_qty_req, " _
                    & "  wod_qty_alloc, " _
                    & "  wod_qty_picked, " _
                    & "  coalesce(wod_qty_issued,0) as wod_qty_issued, " _
                    & "  wod_cost, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  code_name, " _
                    & "  pt_type, " _
                    & "  pt_ls, " _
                    & "  pt_cost, " _
                    & "  wod_dt " _
                    & "FROM  " _
                    & "  public.wod_det " _
                    & "  inner join wo_mstr on wo_mstr.wo_oid = wod_det.wod_wo_oid " _
                    & "  inner join pt_mstr on pt_mstr.pt_id = wod_det.wod_pt_bom_id " _
                    & "  LEFT OUTER join code_mstr on pt_mstr.pt_um = code_mstr.code_id " _
                    & " where wo_code = '" & code.ToString & "'"

                    .InitializeCommand()
                    .FillDataSet(ds_show, "wod_det")

                    z = 0
                    For y = 0 To ds_show.Tables("wod_det").Rows.Count - 1
                        If ds_show.Tables("wod_det").Rows(y).Item("wod_qty_issued") >= ds_show.Tables("wod_det").Rows(y).Item("wod_qty_req") Then
                            z = z + 1
                        End If
                    Next

                    msg = MsgBoxResult.Yes

                    If z = ds_show.Tables("wod_det").Rows.Count Then
                        msg = MsgBox("All part already issued, Do You want to continue?", MsgBoxStyle.YesNo, "Information")
                    End If

                    If msg = MsgBoxResult.Yes Then
                        Dim _dtrow As DataRow
                        For i = 0 To ds_show.Tables("wod_det").Rows.Count - 1
                            _dtrow = fobject.ds_edit.Tables("wocid_det").NewRow

                            _dtrow("wocid_wod_oid") = ds_show.Tables("wod_det").Rows(i).Item("wod_oid")
                            _dtrow("wod_pt_bom_id") = ds_show.Tables("wod_det").Rows(i).Item("wod_pt_bom_id")
                            _dtrow("pt_id_wod") = ds_show.Tables("wod_det").Rows(i).Item("wod_pt_bom_id")
                            _dtrow("pt_code_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_code")
                            _dtrow("pt_desc1_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_desc1")
                            _dtrow("code_name_wod") = ds_show.Tables("wod_det").Rows(i).Item("code_name")
                            _dtrow("pt_type_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_type")
                            _dtrow("pt_ls_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_ls")
                            _dtrow("pt_cost_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_cost")
                            _dtrow("wocid_op") = ds_show.Tables("wod_det").Rows(i).Item("wod_op")
                            _dtrow("wod_qty_req") = ds_show.Tables("wod_det").Rows(i).Item("wod_qty_req")
                            _dtrow("wod_comp") = ds_show.Tables("wod_det").Rows(i).Item("wod_comp")
                            _dtrow("wod_qty_issued") = ds_show.Tables("wod_det").Rows(i).Item("wod_qty_issued")
                            _dtrow("wod_cost") = ds_show.Tables("wod_det").Rows(i).Item("wod_cost")

                            fobject.ds_edit.Tables("wocid_det").Rows.Add(_dtrow)
                        Next

                        fobject.ds_edit.Tables("wocid_det").AcceptChanges()
                        fobject.gv_edit.BestFitColumns()
                    End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
