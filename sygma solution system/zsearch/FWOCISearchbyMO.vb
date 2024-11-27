Imports master_new.ModFunction

Public Class FWOCISearchbyMO
    Public _en_id As Integer
    Dim ds_show As DataSet
    Dim _row_gv As Integer
    Public _si_id As Integer
    Dim func_coll As New function_collection

    Public _row As Integer
    Public _obj As Object

    Private Sub FWOCISearchbyMO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub format_grid()

        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "WO No.", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "WO Issued No.", "woci_code", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Partnumber Project", "pt_code_prj", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Partnumber Description Project", "pt_desc1_prj", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Partnumber", "pt_code", DevExpress.Utils.HorzAlignment.Default)
            add_column(gv_master, "Partnumber Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Partnumber Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Bom Description", "bom_desc", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Routing Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Issued Date", "woci_date", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Route", "woci_date", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Qty ", "wor_qty_comp", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Qty Reject", "wor_qty_reject", DevExpress.Utils.HorzAlignment.Default)
            'add_column(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String

        If fobject.name = "FInventoryReceiptsByWOCI" Then
            get_sequel = "SELECT " _
                        & "  wo_oid, " _
                        & "  wo_id, " _
                        & "  woci_oid, " _
                        & "  woci_dom_id, " _
                        & "  woci_en_id, " _
                        & "  woci_add_by, " _
                        & "  woci_add_date, " _
                        & "  woci_upd_by, " _
                        & "  woci_upd_date, " _
                        & "  woci_code, " _
                        & "  woci_date, " _
                        & "  woci_wo_id, " _
                        & "  woci_si_id, " _
                        & "  woci_remarks, " _
                        & "  woci_dt, " _
                        & "  woci_wc_id, " _
                        & "  wo_code, " _
                        & "  wo_type, " _
                        & "  wo_pt_id, " _
                        & "  wo_ord_date, " _
                        & "  wo_due_date, " _
                        & "  wo_bom_id, " _
                        & "  si_desc, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  coalesce(pt_desc2, " _
                        & "    '') as pt_desc2, " _
                        & "  pt_type, " _
                        & "  pt_ls, " _
                        & "  pt_cost, " _
                        & "  en_desc, " _
                        & "  coalesce(sct_total, " _
                        & "    0) as standard_cost, " _
                        & "  wo_dt, " _
                        & "  wo_status, " _
                        & "  pt_um, " _
                        & "  code_name as unit_measure, " _
                        & "  wo_pjc_id, " _
                        & "  pjc_code " _
                        & "FROM public.woci_mstr " _
                        & "  INNER JOIN public.wo_mstr ON (woci_wo_id = wo_id) " _
                        & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (pt_mstr.pt_um = public.code_mstr.code_id) " _
                        & "  INNER JOIN public.en_mstr ON (public.woci_mstr.woci_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.si_mstr ON (public.woci_mstr.woci_si_id = public.si_mstr.si_id) " _
                        & "  INNER JOIN public.pjc_mstr ON (public.wo_mstr.wo_pjc_oid = public.pjc_mstr.pjc_oid) " _
                        & "  left OUTER join sct_mstr on ( sct_pt_id=pt_id and sct_en_id=en_id and sct_cs_id = (SELECT r.cs_id FROM public.cs_mstr r WHERE r.cs_type = 'G')) " _
                        & " where woci_en_id = " & _en_id.ToString _
                        & " AND woci_date between " & SetDate(pr_txttglawal.DateTime) & " and " _
                        & SetDate(pr_txttglakhir.DateTime)
        End If

        get_sequel = get_sequel & " order by woci_code"

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub


    Private Sub fill_data()

        Dim ds_bantu As New DataSet
        Dim i As Integer

        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FInventoryReceiptsByWOCI" Then
           
            fobject._riu_ref_woci_oid = ds.Tables(0).Rows(_row_gv).Item("woci_oid")
            fobject.riu_ref_woci_code.text = ds.Tables(0).Rows(_row_gv).Item("woci_code")

            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  public.wocid_det.wocid_oid, " _
                            & "  public.wocid_det.wocid_woci_oid, " _
                            & "  public.wocid_det.wocid_wod_oid, " _
                            & "  public.woci_mstr.woci_code, " _
                            & "  public.wocid_det.wocid_dt, " _
                            & "  public.wocid_det.wocid_si_id, " _
                            & "  public.wocid_det.wocid_seq, " _
                            & "  public.wod_det.wod_pt_bom_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pt_mstr.pt_um, " _
                            & "  code_name as unit_measure, " _
                            & "  public.pt_mstr.pt_ls, " _
                            & "  public.pt_mstr.pt_pl_id, " _
                            & "  public.wocid_det.wocid_loc_id, " _
                            & "  public.loc_mstr.loc_code, " _
                            & "  public.loc_mstr.loc_desc, " _
                            & "  public.wocid_det.wocid_qty, " _
                            & "  public.wod_det.wod_qty_issued, " _
                            & "  public.wocid_det.wocid_cost AS standard_cost, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.code_mstr.code_name " _
                            & "FROM " _
                            & "  public.wocid_det " _
                            & "  INNER JOIN public.wod_det ON (public.wocid_det.wocid_wod_oid = public.wod_det.wod_oid) " _
                            & "  INNER JOIN public.woci_mstr ON (public.wocid_det.wocid_woci_oid = public.woci_mstr.woci_oid) " _
                            & "  INNER JOIN public.pt_mstr ON (public.wod_det.wod_pt_bom_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.loc_mstr ON (public.wocid_det.wocid_loc_id = public.loc_mstr.loc_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.wocid_det.wocid_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pt_mstr.pt_um = public.code_mstr.code_id) " _
                            & "  LEFT OUTER JOIN public.pl_mstr ON (public.wod_det.wod_pt_bom_id = public.pl_mstr.pl_id) " _
                            & " where woci_en_id = " & _en_id.ToString _
                            & " AND woci_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("woci_oid")) & "  "

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "wocid_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


            fobject.ds_edit.tables(0).clear()

            Dim _dtrow As DataRow
            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                _dtrow = fobject.ds_edit.Tables(0).NewRow
                _dtrow("riud_oid") = Guid.NewGuid.ToString
                _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("wod_pt_bom_id")
                _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
                _dtrow("pt_ls") = ds_bantu.Tables(0).Rows(i).Item("pt_ls")
                _dtrow("riud_qty") = ds_bantu.Tables(0).Rows(i).Item("wod_qty_issued")
                '_dtrow("qty_open") = ds_bantu.Tables(0).Rows(i).Item("wocid_qty")
                _dtrow("riud_um_conv") = 1
                _dtrow("riud_um") = ds_bantu.Tables(0).Rows(i).Item("pt_um")
                _dtrow("riud_um_name") = ds_bantu.Tables(0).Rows(i).Item("unit_measure")
                _dtrow("riud_cost") = ds_bantu.Tables(0).Rows(i).Item("standard_cost")
                '_dtrow("riud_woci_oid") = ds_bantu.Tables(0).Rows(i).Item("wocid_woci_oid")
                _dtrow("riud_qty_real") = ds_bantu.Tables(0).Rows(i).Item("wod_qty_issued")
                _dtrow("riud_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("riud_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("riud_si_id") = ds_bantu.Tables(0).Rows(i).Item("wocid_si_id")
                _dtrow("si_desc") = ds_bantu.Tables(0).Rows(i).Item("si_desc")
                _dtrow("riud_loc_id") = ds_bantu.Tables(0).Rows(i).Item("wocid_loc_id")
                _dtrow("loc_desc") = ds_bantu.Tables(0).Rows(i).Item("loc_desc")

                Dim dt_bantu As New DataTable


                If limit_account(_en_id) = True Then

                    'Dim _filter As String

                    '_filter = " and ac_id in (SELECT  " _
                    '  & "  enacc_ac_id " _
                    '  & "FROM  " _
                    '  & "  public.enacc_mstr  " _
                    '  & "Where enacc_en_id=" & SetInteger(_en_id) & " and enacc_code='inv_issue_account') "


                    'dt_bantu = (func_data.load_ac_mstr(_filter))

                    'If dt_bantu.Rows.Count = 0 Then
                    '    Box("Inventory Issue account setting is empty")
                    '    Exit Sub
                    'End If

                    '_dtrow("riud_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                    '_dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                    '_dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                Else
                    dt_bantu = (func_coll.get_prodline_account(ds_bantu.Tables(0).Rows(i).Item("pt_pl_id"), "WO_COP-"))
                    If dt_bantu Is Nothing Then
                        Box("WO_COP- for " & ds_bantu.Tables(0).Rows(i).Item("pt_code") & " " & ds_bantu.Tables(0).Rows(i).Item("pt_desc1") & " is empty")
                        Exit Sub
                    End If

                    _dtrow("riud_ac_id") = dt_bantu.Rows(0).Item("pla_ac_id")
                    _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                    _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")


                End If
                'End If

                'sys 20110922
                'If ds_bantu.Tables(0).Rows(i).Item("pbd_um") = ds_bantu.Tables(0).Rows(i).Item("pt_um") Then
                '    _dtrow("riud_um_conv") = 1
                '    _dtrow("riud_qty_real") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '    _dtrow("riud_cost") = ds_bantu.Tables(0).Rows(i).Item("invct_cost")
                'Else
                'Dim _um_conv As Double = 1
                'Try
                '    Using objcek As New master_new.CustomCommand
                '        With objcek
                '            '.Connection.Open()
                '            '.Command = .Connection.CreateCommand
                '            '.Command.CommandType = CommandType.Text
                '            .Command.CommandText = "select um_conv from um_mstr " + _
                '                   " where um_pt_id = " + ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id").ToString + _
                '                   " and um_pt_um = " + ds_bantu.Tables(0).Rows(i).Item("pbd_um").ToString + _
                '                   " and um_pt_um_alt = (select pt_um from pt_mstr where pt_id = " + ds_bantu.Tables(0).Rows(i).Item("pbd_pt_id").ToString + ") "
                '            .InitializeCommand()
                '            .DataReader = .ExecuteReader
                '            While .DataReader.Read
                '                _um_conv = .DataReader("um_conv")
                '            End While
                '        End With
                '    End Using
                'Catch ex As Exception
                '    MessageBox.Show(ex.Message)
                '    Exit Sub
                'End Try

                '_dtrow("riud_um_conv") = _um_conv
                '_dtrow("riud_qty_real") = _um_conv * ds_bantu.Tables(0).Rows(i).Item("qty_open")
                '_dtrow("riud_cost") = _um_conv * ds_bantu.Tables(0).Rows(i).Item("invct_cost")



                'End If
                '--

                fobject.ds_edit.Tables(0).Rows.Add(_dtrow)
            Next
            fobject.ds_edit.tables(0).acceptchanges()
            fobject.gv_edit.bestfitcolumns()

            'fobject.ptsfr_pb_oid.enabled = false
            'fobject.ptsfr_so_oid.enabled = false
            'fobject.ptsfr_so_oid.text = ""
            fobject.gc_edit.embeddednavigator.buttons.append.visible = False
            fobject.gc_edit.embeddednavigator.buttons.remove.visible = True


        ElseIf fobject.name = "FWODisAssembly" Then
            fobject.wo_asmb_code.tag = ds.Tables(0).Rows(_row_gv).Item("wo_oid")
            fobject.wo_asmb_code.text = ds.Tables(0).Rows(_row_gv).Item("wo_code")
            fobject.wo_be_asmb_pt.tag = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id")
            fobject.wo_be_asmb_pt.text = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.wo_asmb_qty.text = ds.Tables(0).Rows(_row_gv).Item("wo_qty")
            fobject._ps_oid = ds.Tables(0).Rows(_row_gv).Item("ps_oid")
            'fobject.wo_be_asmb_ps.tag = ds.Tables(0).Rows(_row_gv).Item("wo_ps_id")
            fobject.wo_be_asmb_ps.text = ds.Tables(0).Rows(_row_gv).Item("ps_desc")

            'ElseIf fobject.name = "FInventoryIssuesByWO" Then
            '    'fobject._oid_master = ds.Tables(0).Rows(_row_gv).Item("wo_oid")
            '    fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id")
            '    fobject.wo_code.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_code")
            '    fobject._pt_code = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            '    fobject.part_code.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            '    fobject.pt_desc1.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            '    fobject.wo_qty_ord.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_ord")
            '    fobject.wo_qty_comp.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_comp")
            '    fobject.wo_qty_remaining.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_remaining")
            '    fobject.wo_due_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_due_date")
            '    fobject.wo_status.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_status")
            '    fobject.wo_rel_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_rel_date")

        ElseIf fobject.name = FWORelease.Name Then
            fobject._oid_master = ds.Tables(0).Rows(_row_gv).Item("wo_oid")
            fobject._pt_id = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id")
            fobject.wo_code.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_code")
            fobject._pt_code = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            fobject.part_code.text = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            fobject.pt_desc1.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.wo_qty_ord.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_ord")
            fobject.wo_qty_comp.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_comp")
            fobject.wo_qty_remaining.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_qty_remaining")
            fobject.wo_due_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_due_date")
            fobject.wo_status.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_status")
            fobject.wo_rel_date.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_rel_date")


        ElseIf fobject.name = "FWOReceipt" Then
            fobject.__qty_outstanding = ds.Tables(0).Rows(_row_gv).Item("wo_qty_remaining")
            'fobject.wor_loc_id.EditValue = ds.Tables(0).Rows(_row_gv).Item("loc_id")
            fobject.pt_code.tag = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id")
            fobject.wor_wo_id.text = ds.Tables(0).Rows(_row_gv).Item("wo_code")
            fobject.wor_wo_id.tag = ds.Tables(0).Rows(_row_gv).Item("wo_id")
            fobject.pt_ls.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_ls")
            fobject._pt_um = ds.Tables(0).Rows(_row_gv).Item("pt_um")
            If SetString(ds.Tables(0).Rows(_row_gv).Item("pt_ls")).ToUpper = "S" Then
                fobject.gc_serial.EmbeddedNavigator.Buttons.Append.visible = True
            Else
                fobject.gc_serial.EmbeddedNavigator.Buttons.Append.visible = False
            End If
            fobject.pt_code.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_code")
            fobject.pt_desc1.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.pt_desc2.editvalue = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
            fobject.unit_measure.editvalue = ds.Tables(0).Rows(_row_gv).Item("unit_measure")
            fobject.wor_cost.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_cost")
            fobject.wor_qty_reject.editvalue = ds.Tables(0).Rows(_row_gv).Item("wodr_qty_reject")
            fobject.wor_qty_comp.editvalue = ds.Tables(0).Rows(_row_gv).Item("wodr_qty_complete")

            'fobject._pjc_id = ds.Tables(0).Rows(_row_gv).Item("wo_pjc_id")
            'fobject._cost = ds.Tables(0).Rows(_row_gv).Item("standard_cost")

        ElseIf fobject.name = FInventoryRequestWO.Name Then
            fobject.pb_wo_oid.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid")
            fobject.pb_wo_oid.editvalue = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code")
            'End If

        ElseIf fobject.name = "FWorkOrderIssue" Then
            send_data()

        ElseIf fobject.name = "FTransferIssuesWIP" Then
            fobject.wimtr_wo_oid.text = ds.Tables(0).Rows(_row_gv).Item("wo_code")
            fobject.wimtr_wo_oid.tag = ds.Tables(0).Rows(_row_gv).Item("wo_oid")

        ElseIf fobject.name = "FWIPIssues" Then
            fobject.wimis_wo_oid.text = ds.Tables(0).Rows(_row_gv).Item("wo_code")
            fobject.wimis_wo_oid.tag = ds.Tables(0).Rows(_row_gv).Item("wo_oid")

        ElseIf fobject.name = "FQualityControl" Then
            fobject._ref_oid = ds.Tables(0).Rows(_row_gv).Item("wo_oid")
            fobject.ref_code.text = ds.Tables(0).Rows(_row_gv).Item("wo_code")

            Dim _dtrow As DataRow
            Dim _qcd_oid As String = Guid.NewGuid.ToString

            fobject.ds_edit.Tables("insert_edit").clear()
            If ds.Tables(0).Rows(_row_gv).Item("qty_open") > 0 Then
                _dtrow = fobject.ds_edit.Tables("insert_edit").NewRow
                _dtrow("qcd_oid") = _qcd_oid
                _dtrow("qcd_ref_oid") = ds.Tables(0).Rows(_row_gv).Item("wor_oid")
                _dtrow("qcd_pjc_id") = ds.Tables(0).Rows(_row_gv).Item("wo_pjc_id")
                _dtrow("pjc_code") = ds.Tables(0).Rows(_row_gv).Item("pjc_code")
                _dtrow("qcd_pt_id") = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id")
                _dtrow("pt_code") = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                _dtrow("pt_desc1") = ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
                _dtrow("pt_desc2") = ds.Tables(0).Rows(_row_gv).Item("pt_desc2")
                _dtrow("pt_type") = ds.Tables(0).Rows(_row_gv).Item("pt_type")
                _dtrow("pt_ls") = ds.Tables(0).Rows(_row_gv).Item("pt_ls")
                _dtrow("pt_cost") = ds.Tables(0).Rows(_row_gv).Item("pt_cost") 'harusnya ke nilai standard / current cost 20111223
                _dtrow("qcd_cost") = ds.Tables(0).Rows(_row_gv).Item("standard_cost")
                _dtrow("qcd_qty") = ds.Tables(0).Rows(_row_gv).Item("qty_open")
                _dtrow("qcd_um") = ds.Tables(0).Rows(_row_gv).Item("pt_um")
                _dtrow("um_name") = ds.Tables(0).Rows(_row_gv).Item("unit_measure")
                _dtrow("qcd_qty_pass") = 0
                _dtrow("qcd_qty_reject") = 0
                _dtrow("qcd_bef_si_id") = ds.Tables(0).Rows(_row_gv).Item("wor_si_id")
                _dtrow("si_desc") = ds.Tables(0).Rows(_row_gv).Item("si_desc")
                _dtrow("qcd_bef_loc_id") = ds.Tables(0).Rows(_row_gv).Item("wor_loc_id")
                _dtrow("loc_desc") = ds.Tables(0).Rows(_row_gv).Item("loc_desc")
                fobject.ds_edit.Tables("insert_edit").Rows.Add(_dtrow)

                fobject.ds_edit.Tables("insert_edit").AcceptChanges()
                fobject.gv_edit.BestFitColumns()

                If (UCase(ds.Tables(0).Rows(_row_gv).Item("pt_ls").ToString) = "L") Or (UCase(ds.Tables(0).Rows(_row_gv).Item("pt_ls").ToString) = "S") Then
                    ds_show = New DataSet
                    Try
                        Using objcb As New master_new.CustomCommand
                            With objcb
                                .SQL = "SELECT  " _
                                    & "  wors_oid, " _
                                    & "  wors_wor_oid, " _
                                    & "  wors_qty, " _
                                    & "  wors_um, " _
                                    & "  wors_si_id, " _
                                    & "  wors_lot_serial, " _
                                    & "  wors_loc_id, " _
                                    & "  wors_dt " _
                                    & "FROM  " _
                                    & "  public.wors_serial " _
                                    & " where wors_wor_oid = '" & ds.Tables(0).Rows(_row_gv).Item("wor_oid").ToString & "'"

                                .InitializeCommand()
                                .FillDataSet(ds_show, "wors_serial")

                                If ds_show.Tables("wors_serial").Rows.Count > 0 Then
                                    fobject.ds_serial.Tables("serial").Clear()
                                    For x As Integer = 0 To ds_show.Tables("wors_serial").Rows.Count - 1
                                        _dtrow = fobject.ds_serial.Tables("serial").NewRow
                                        _dtrow("qcds_qcd_oid") = _qcd_oid
                                        _dtrow("is_reject") = False
                                        _dtrow("detail_ref_oid") = ds.Tables(0).Rows(_row_gv).Item("wor_oid")
                                        _dtrow("_pt_id") = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id")
                                        _dtrow("_pt_code") = ds.Tables(0).Rows(_row_gv).Item("pt_code")
                                        _dtrow("_pt_type") = ds.Tables(0).Rows(_row_gv).Item("pt_type")
                                        _dtrow("qcd_cost") = ds.Tables(0).Rows(_row_gv).Item("standard_cost")
                                        _dtrow("qcds_ref_oid") = ds_show.Tables("wors_serial").Rows(x).Item("wors_oid")
                                        _dtrow("qcds_qty") = ds_show.Tables("wors_serial").Rows(x).Item("wors_qty")
                                        _dtrow("qcds_um") = ds_show.Tables("wors_serial").Rows(x).Item("wors_um")
                                        _dtrow("qcds_lot_serial") = ds_show.Tables("wors_serial").Rows(x).Item("wors_lot_serial")
                                        _dtrow("qcds_bef_loc_id") = ds_show.Tables("wors_serial").Rows(x).Item("wors_loc_id")
                                        _dtrow("qcds_bef_si_id") = ds_show.Tables("wors_serial").Rows(x).Item("wors_si_id")
                                        _dtrow("qcd_pjc_id") = ds.Tables(0).Rows(_row_gv).Item("wo_pjc_id")
                                        fobject.ds_serial.Tables("serial").Rows.Add(_dtrow)
                                    Next
                                    fobject.ds_serial.Tables("serial").AcceptChanges()
                                    fobject.gv_serial.BestFitColumns()

                                End If
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End If
            End If
        ElseIf fobject.name = FPurchaseOrder.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_wo_oid", ds.Tables(0).Rows(_row_gv).Item("wo_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "wo_code", ds.Tables(0).Rows(_row_gv).Item("wo_code"))
            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = FWorkOrder.Name Then
            fobject.wo_ref_rework.tag = ds.Tables(0).Rows(_row_gv).Item("wo_oid")
            fobject.wo_ref_rework.text = ds.Tables(0).Rows(_row_gv).Item("wo_code")

            fobject.wo_so_oid.tag = ds.Tables(0).Rows(_row_gv).Item("wo_so_oid")
            fobject.wo_so_oid.text = ds.Tables(0).Rows(_row_gv).Item("so_code")
            fobject.wo_pt_id_prj.tag = ds.Tables(0).Rows(_row_gv).Item("wo_pt_id_prj")
            fobject.wo_pt_id_prj.text = ds.Tables(0).Rows(_row_gv).Item("pt_code_prj") & ", " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1_prj")
            fobject.wo_pt_id.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_code") & ", " & ds.Tables(0).Rows(_row_gv).Item("pt_desc1")
            fobject.wo_pt_id.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_pt_id")
            'fobject.part_desc.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pt_desc1")

        ElseIf fobject.name = "FWorkOrderbyMOPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code")
            '_obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code")

        ElseIf fobject.name = FRequisition.Name Then
            fobject.req_wo_oid.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid")
            fobject.req_wo_oid.editvalue = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code")

            Dim sSQL As String

            sSQL = "SELECT  " _
                & "  a.wod_oid, " _
                & "  a.wod_pt_bom_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  a.wod_op, " _
                & "  c.op_name,pt_um,pt_type, " _
                & "  a.wod_qty, " _
                & "  a.wod_indirect, " _
                & "  a.wod_start_date, " _
                & "  a.wod_end_date, " _
                & "  a.wod_yield_pct, " _
                & "  a.wod_seq, " _
                & "  a.wod_qty_yield, " _
                & "  d.code_name AS um_desc " _
                & "FROM " _
                & "  public.wod_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
                & "  INNER JOIN public.op_mstr c ON (a.wod_op = c.op_code) " _
                & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
                & "WHERE " _
                & "  a.wod_wo_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'"

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            Dim dr_list As DataRow

            fobject.ds_edit.Clear()
            For Each dr As DataRow In dt.Rows
                dr_list = fobject.ds_edit.Tables(0).NewRow

                dr_list("reqd_oid") = Guid.NewGuid.ToString
                dr_list("reqd_en_id") = fobject.req_en_id.editvalue
                dr_list("en_desc") = fobject.req_en_id.GetColumnValue("en_desc")
                dr_list("reqd_ptnr_id") = fobject.req_ptnr_id.EditValue
                dr_list("ptnr_name") = fobject.req_ptnr_id.GetColumnValue("ptnr_name")
                dr_list("reqd_si_id") = fobject.req_si_id.EditValue
                dr_list("si_desc") = fobject.req_si_id.GetColumnValue("si_desc")
                dr_list("reqd_qty_real") = 0
                dr_list("reqd_qty_cost") = 0
                dr_list("reqd_pt_id") = dr("wod_pt_bom_id")
                dr_list("pt_code") = dr("pt_code")
                dr_list("pt_type") = dr("pt_type")
                dr_list("reqd_um") = dr("pt_um")
                dr_list("code_name") = dr("um_desc")
                dr_list("reqd_pt_desc1") = dr("pt_desc1")
                dr_list("reqd_pt_desc2") = dr("pt_desc2")
                dr_list("reqd_rmks") = ""
                dr_list("reqd_end_user") = ""
                dr_list("reqd_qty") = dr("wod_qty")
                dr_list("reqd_qty_real") = dr("wod_qty")
                dr_list("reqd_cost") = SetNumber(master_new.PGSqlConn.GetRowInfo("select f_get_cost(" & _en_id & "," & dr("wod_pt_bom_id") & ",'G') as cost ")(0))
                dr_list("reqd_disc") = 0
                dr_list("reqd_qty_cost") = dr("wod_qty") * SetNumber(master_new.PGSqlConn.GetRowInfo("select f_get_cost(" & _en_id & "," & dr("wod_pt_bom_id") & ",'G') as cost ")(0))
                dr_list("reqd_need_date") = master_new.PGSqlConn.CekTanggal
                dr_list("reqd_due_date") = master_new.PGSqlConn.CekTanggal
                dr_list("reqd_um_conv") = 1
                dr_list("reqd_wod_oid") = dr("wod_oid")
                fobject.ds_edit.Tables(0).Rows.Add(dr_list)

            Next

            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gc_edit.DataSource = fobject.ds_edit.Tables(0)
            fobject.gv_edit.BestFitColumns()


        ElseIf fobject.name = FInventoryRequestWO.Name Then
            fobject.pb_wo_oid.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid")
            fobject.pb_wo_oid.editvalue = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code")

            Dim sSQL As String

            'sSQL = "SELECT  " _
            '    & "  a.wod_oid, " _
            '    & "  a.wod_pt_bom_id, " _
            '    & "  b.pt_code, " _
            '    & "  b.pt_desc1, " _
            '    & "  b.pt_desc2, " _
            '    & "  a.wod_op, " _
            '    & "  c.op_name,pt_um,pt_type, " _
            '    & "  a.wod_qty, " _
            '    & "  a.wod_indirect, " _
            '    & "  a.wod_start_date, " _
            '    & "  a.wod_end_date, " _
            '    & "  a.wod_yield_pct, " _
            '    & "  a.wod_seq, " _
            '    & "  a.wod_qty_yield, " _
            '    & "  d.code_name AS um_desc " _
            '    & "FROM " _
            '    & "  public.wod_det a " _
            '    & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
            '    & "  INNER JOIN public.op_mstr c ON (a.wod_op = c.op_code) " _
            '    & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
            '    & "WHERE " _
            '    & "  a.wod_wo_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'"

            sSQL = "SELECT  " _
                  & "  a.wod_oid, " _
                  & "  a.wod_pt_bom_id, " _
                  & "  b.pt_code, " _
                  & "  b.pt_desc1,pt_um, " _
                  & "  b.pt_desc2, " _
                  & "  a.wod_op,pt_type,pt_ls,pt_cost, " _
                  & "  c.op_name,wod_qty- coalesce(wod_qty_issued,0) as wod_qty_open, " _
                  & "  a.wod_qty,wod_qty_req, coalesce(wod_qty_issued,0) as wod_qty_issued,wod_comp,wod_cost, " _
                  & "  a.wod_indirect, " _
                  & "  a.wod_insheet_pct, " _
                  & "  a.wod_seq, " _
                  & "  a.wod_qty_insheet, " _
                  & "  d.code_name AS um_desc " _
                  & "FROM " _
                  & "  public.wod_det a " _
                  & "  inner join wo_mstr on wo_mstr.wo_oid = a.wod_wo_oid " _
                  & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
                  & "  left outer JOIN public.op_mstr c ON (a.wod_op = c.op_code) " _
                  & "  left outer JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
                  & "WHERE " _
                  & " wo_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'"


            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSQL)

            Dim dr_list As DataRow

            fobject.ds_edit.Clear()
            For Each dr As DataRow In dt.Rows
                dr_list = fobject.ds_edit.Tables(0).NewRow

                dr_list("pbd_en_id") = fobject.pb_en_id.editvalue
                dr_list("en_desc") = fobject.pb_en_id.GetColumnValue("en_desc")

                'dr_list("pbd_si_id") = fobject.req_si_id.EditValue
                'dr_list("si_desc") = fobject.req_si_id.GetColumnValue("si_desc")

                dr_list("pbd_pt_id") = dr("wod_pt_bom_id")
                dr_list("pt_code") = dr("pt_code")
                dr_list("pt_desc1") = dr("pt_desc1")
                dr_list("pt_desc2") = dr("pt_desc2")

                dr_list("pbd_qty") = 0.0

                dr_list("pbd_end_user") = fobject.pb_end_user.EditValue
                dr_list("pbd_um") = dr("pt_um")
                dr_list("code_name") = dr("um_desc")
                dr_list("pbd_due_date") = master_new.PGSqlConn.CekTanggal().Date


                fobject.ds_edit.Tables(0).Rows.Add(dr_list)

            Next

            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gc_edit.DataSource = fobject.ds_edit.Tables(0)
            fobject.gv_edit.BestFitColumns()


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
            Using objcb As New master_new.CustomCommand
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
                    & "  wo_insheet_pct, " _
                    & "  wo_ro_id, " _
                    & "  wo_status, " _
                    & "  wo_remarks, " _
                    & "  si_id, " _
                    & "  si_desc, " _
                    & "  pt_id, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  ro_id, " _
                    & "  ro_desc, " _
                    & "  en_id, " _
                    & "  en_desc, " _
                    & "  pjc_code, " _
                    & "  wo_dt " _
                    & "FROM  " _
                    & "  public.wo_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.wo_mstr.wo_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.wo_mstr.wo_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id) " _
                    & "  LEFT OUTER JOIN public.pjc_mstr ON pjc_oid = wo_pjc_oid " _
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
                            fobject.pt_code.EditValue = .Item("pt_code")
                            fobject.pt_desc1.EditValue = .Item("pt_desc1")
                            fobject.pt_desc2.EditValue = .Item("pt_desc2")
                            fobject.wo_due_date.EditValue = .Item("wo_due_date")
                            fobject.wo_ord_date.EditValue = .Item("wo_ord_date")
                            fobject.wo_qty_ord.EditValue = .Item("wo_qty_ord")
                            fobject.wo_insheet_pct.EditValue = .Item("wo_insheet_pct")
                            fobject.wo_ro_id.EditValue = .Item("ro_desc")
                            fobject.wo_remarks.EditValue = .Item("wo_remarks")
                            fobject.wo_pjc_oid.EditValue = .Item("pjc_code")
                        End With

                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_show = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.SQL = "SELECT  " _
                    '& "  wod_oid, " _
                    '& "  wod_wo_oid, " _
                    '& "  wod_use_bom, " _
                    '& "  wod_pt_bom_id, " _
                    '& "  wod_comp, " _
                    '& "  wod_op, " _
                    '& "  wod_qty_per, " _
                    '& "  wod_qty_req, " _
                    '& "  wod_qty_alloc, " _
                    '& "  wod_qty_picked, " _
                    '& "  coalesce(wod_qty_issued,0) as wod_qty_issued, " _
                    '& "  wod_cost, " _
                    '& "  pt_code, " _
                    '& "  pt_desc1, " _
                    '& "  pt_desc2, " _
                    '& "  code_name, " _
                    '& "  pt_type, " _
                    '& "  pt_ls, " _
                    '& "  pt_cost,pt_loc_id,loc_desc, " _
                    '& "  wod_dt " _
                    '& "FROM  " _
                    '& "  public.wod_det " _
                    '& "  inner join wo_mstr on wo_mstr.wo_oid = wod_det.wod_wo_oid " _
                    '& "  inner join pt_mstr on pt_mstr.pt_id = wod_det.wod_pt_bom_id " _
                    '& "  inner join loc_mstr on pt_mstr.pt_loc_id = loc_mstr.loc_id " _
                    '& "  LEFT OUTER join code_mstr on pt_mstr.pt_um = code_mstr.code_id " _
                    '& " where wo_code = '" & code.ToString & "' Order By pt_code,pt_desc1,code_name"


                    .SQL = "SELECT  " _
                      & "  a.wod_oid, " _
                      & "  a.wod_pt_bom_id, " _
                      & "  b.pt_code, " _
                      & "  b.pt_desc1, " _
                      & "  b.pt_desc2, " _
                      & "  a.wod_op,pt_type,pt_ls,pt_cost, " _
                      & "  c.op_name,wod_qty- coalesce(wod_qty_issued,0) as wod_qty_open, " _
                      & "  a.wod_qty,wod_qty_req, coalesce(wod_qty_issued,0) as wod_qty_issued,wod_comp,wod_cost, " _
                      & "  a.wod_indirect, " _
                      & "  a.wod_insheet_pct, " _
                      & "  a.wod_seq, " _
                      & "  a.wod_qty_insheet, " _
                      & "  d.code_name AS um_desc " _
                      & "FROM " _
                      & "  public.wod_det a " _
                      & "  inner join wo_mstr on wo_mstr.wo_oid = a.wod_wo_oid " _
                      & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
                      & "  left outer JOIN public.op_mstr c ON (a.wod_op = c.op_code) " _
                      & "  left outer JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
                      & "WHERE " _
                      & " wo_code = '" & code.ToString & "'"

                    .InitializeCommand()
                    .FillDataSet(ds_show, "wod_det")

                    'z = 0
                    'For y = 0 To ds_show.Tables("wod_det").Rows.Count - 1
                    '    If SetNumber(ds_show.Tables("wod_det").Rows(y).Item("wod_qty_issued")) >= SetNumber(ds_show.Tables("wod_det").Rows(y).Item("wod_qty_req")) Then
                    '        z = z + 1
                    '    End If
                    'Next

                    'msg = MsgBoxResult.Yes

                    'If z = ds_show.Tables("wod_det").Rows.Count Then
                    '    msg = MsgBox("All part already issued, Do You want to continue?", MsgBoxStyle.YesNo, "Information")
                    'End If

                    'If msg = MsgBoxResult.Yes Then

                    Dim _dtrow As DataRow
                    For i = 0 To ds_show.Tables("wod_det").Rows.Count - 1
                        _dtrow = fobject.ds_edit.Tables("wocid_det").NewRow

                        _dtrow("wocid_wod_oid") = ds_show.Tables("wod_det").Rows(i).Item("wod_oid")
                        _dtrow("wod_pt_bom_id") = ds_show.Tables("wod_det").Rows(i).Item("wod_pt_bom_id")
                        _dtrow("pt_id_wod") = ds_show.Tables("wod_det").Rows(i).Item("wod_pt_bom_id")
                        _dtrow("pt_code_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_code")
                        _dtrow("pt_desc1_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_desc1")
                        _dtrow("pt_desc2_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_desc2")
                        _dtrow("code_name_wod") = ds_show.Tables("wod_det").Rows(i).Item("um_desc")
                        _dtrow("pt_type_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_type")
                        _dtrow("pt_ls_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_ls")
                        _dtrow("pt_cost_wod") = ds_show.Tables("wod_det").Rows(i).Item("pt_cost")
                        ' _dtrow("wocid_op") = ds_show.Tables("wod_det").Rows(i).Item("wod_op")
                        _dtrow("wod_qty_req") = ds_show.Tables("wod_det").Rows(i).Item("wod_qty_req")
                        _dtrow("wod_comp") = ds_show.Tables("wod_det").Rows(i).Item("wod_comp")
                        _dtrow("wocid_qty") = 0.0 'ds_show.Tables("wod_det").Rows(i).Item("wod_qty_open")
                        _dtrow("wocid_cost") = ds_show.Tables("wod_det").Rows(i).Item("wod_cost")
                        ' _dtrow("wocid_qty") = ds_show.Tables("wod_det").Rows(i).Item("wod_qty_open")
                        _dtrow("wocid_qty_open") = ds_show.Tables("wod_det").Rows(i).Item("wod_qty_open")

                        _dtrow("wocid_si_id") = _si_id
                        _dtrow("si_desc") = fobject.wo_si_id.EditValue
                        _dtrow("wocid_loc_id") = DBNull.Value 'ds_show.Tables("wod_det").Rows(i).Item("pt_loc_id")
                        _dtrow("loc_desc") = DBNull.Value ' ds_show.Tables("wod_det").Rows(0).Item("loc_desc")
                        _dtrow("wocid_wc_id") = DBNull.Value ' fobject.woci_wc_id.editvalue
                        _dtrow("wc_desc") = DBNull.Value ' fobject.woci_wc_id.text
                        fobject.ds_edit.Tables("wocid_det").Rows.Add(_dtrow)

                    Next

                    fobject.ds_edit.Tables("wocid_det").AcceptChanges()
                    fobject.gv_edit.BestFitColumns()
                    'End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
