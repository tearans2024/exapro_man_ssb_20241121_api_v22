﻿Imports MyPGDll.ModFunction

Public Class FPurchaseOrderPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction

    Private Sub FPurchaseOrderPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FPurchaseOrderSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FPurchaseOrderSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = le_entity.EditValue
        _type = 2
        _table = "po_mstr"
        _initial = "po"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  po_oid, " _
            & "  po_dom_id, " _
            & "  po_en_id, " _
            & "  po_upd_date, " _
            & "  po_upd_by, " _
            & "  po_add_date, " _
            & "  po_add_by, " _
            & "  po_code, " _
            & "  po_ptnr_id, " _
            & "  po_cmaddr_id, " _
            & "  po_date, " _
            & "  po_need_date, " _
            & "  po_due_date, " _
            & "  po_rmks, " _
            & "  po_sb_id, " _
            & "  po_cc_id, " _
            & "  po_si_id, " _
            & "  po_pjc_id, " _
            & "  po_close_date, " _
            & "  po_total, " _
            & "  po_tran_id, " _
            & "  po_trans_id, " _
            & "  po_credit_term, " _
            & "  po_taxable, " _
            & "  po_tax_inc, " _
            & "  po_tax_class, " _
            & "  po_cu_id, " _
            & "  po_exc_rate, " _
            & "  po_trans_rmks, " _
            & "  po_total_ppn, " _
            & "  po_freight, " _
            & "  po_total_pph, " _
            & "  po_status_cash, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  ptnra_line, ptnrac_contact_name, " _
            & "  ptnra_phone_1, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3, " _
            & "  tax_class_mstr.code_name as tax_class_name, " _
            & "  cu_name, " _
            & " pod_oid," _
            & "  pod_pt_id, " _
            & "  pod_qty, " _
            & "  pod_cost, " _
            & "  pod_disc, " _
            & "  pod_rmks,pod_seq, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
            & "  cc_desc, " _
            & "  um_master.code_code as um_name, " _
            & "  creditterm_mstr.code_name as creditterms_name,  " _
            & "  cmt_ref_oid, cmt_comment, cmt_type , " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  public.po_mstr " _
            & "  inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "  inner join ptnrac_cntc on addrc_ptnra_oid  = ptnra_oid " _
            & "  inner join cmaddr_mstr on cmaddr_id = po_cmaddr_id " _
            & "  inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = po_tax_class " _
            & "  inner join cu_mstr on cu_id = po_cu_id " _
            & "  inner join cc_mstr on cc_id = po_cc_id " _
            & "  inner join pod_det on pod_po_oid = po_oid " _
            & "  inner join pt_mstr on pt_id = pod_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = pod_um  " _
            & "  inner join code_mstr as creditterm_mstr on po_mstr.po_credit_term = creditterm_mstr.code_id" _
            & "  inner join code_mstr addr_type on addr_type.code_id = ptnra_addr_type  " _
            & "  left outer join cmt_mstr on pod_oid = cmt_ref_oid  " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = po_oid  " _
            & "  where po_code >= '" + be_first.Text + "'" _
            & "  and po_code <= '" + be_to.Text + "'" _
                       & "  and addr_type.code_name ~~* 'ship to' " _
            & "  order by po_code, pod_seq, cmt_seq "

        '& "  and ptnra_line = 1 and ptnrac_seq = 1 " _
        '& "  order by po_code "

        ' & "  and ptnrac_seq = 1 " _


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        If CeNoCost.EditValue = True Then
            frm._report = "XRPurchaseOrderPrintOutNoCost"
        Else
            frm._report = "XRPurchaseOrderPrintOut"
        End If

        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()


    End Sub

    Private Sub BtEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtEmail.Click
        Try
            Dim filename, par_code, email_to As String
            email_to = setstring(func_coll.get_conf_file("email_logistik"))

            par_code = be_first.Text
            filename = My.Application.Info.DirectoryPath & "\export\" + par_code + Now.ToString("_yyyyMMdd_HHmmss") + ".png"

            If preview_export(filename, par_code) = True Then
                If email_to <> "" Then

                    If mf.sent_email(email_to, "", "Purchase order " & be_first.Text & " to " & be_to.Text, "Purchase order " & be_first.Text & " to " & be_to.Text, master_new.ClsVar.email_user_name, filename) = True Then
                        Box("Email success")
                    Else
                        Box("Email Failed")
                    End If

                    Try
                        IO.File.Delete(filename)
                    Catch ex As Exception
                        Box(ex.Message)
                    End Try

                Else
                    Box("Email Address Not Available For User Logistic. Please Contact Your Admin Program ")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function preview_export(ByVal par_filename As String, ByVal par_code As String) As Boolean
        Try
            Dim _sql As String

            _sql = "SELECT  " _
            & "  po_oid, " _
            & "  po_dom_id, " _
            & "  po_en_id, " _
            & "  po_upd_date, " _
            & "  po_upd_by, " _
            & "  po_add_date, " _
            & "  po_add_by, " _
            & "  po_code, " _
            & "  po_ptnr_id, " _
            & "  po_cmaddr_id, " _
            & "  po_date, " _
            & "  po_need_date, " _
            & "  po_due_date, " _
            & "  po_rmks, " _
            & "  po_sb_id, " _
            & "  po_cc_id, " _
            & "  po_si_id, " _
            & "  po_pjc_id, " _
            & "  po_close_date, " _
            & "  po_total, " _
            & "  po_tran_id, " _
            & "  po_trans_id, " _
            & "  po_credit_term, " _
            & "  po_taxable, " _
            & "  po_tax_inc, " _
            & "  po_tax_class, " _
            & "  po_cu_id, " _
            & "  po_exc_rate, " _
            & "  po_trans_rmks, " _
            & "  po_total_ppn, " _
            & "  po_freight, " _
            & "  po_total_pph, " _
            & "  po_status_cash, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  ptnra_line, ptnrac_contact_name, " _
            & "  ptnra_phone_1, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3, " _
            & "  tax_class_mstr.code_name as tax_class_name, " _
            & "  cu_name, " _
            & " pod_oid," _
            & "  pod_pt_id, " _
            & "  pod_qty, " _
            & "  pod_cost, " _
            & "  pod_disc, " _
            & "  pod_rmks,pod_seq, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
            & "  cc_desc, " _
            & "  um_master.code_code as um_name, " _
            & "  creditterm_mstr.code_name as creditterms_name,  " _
            & "  cmt_ref_oid, cmt_comment, cmt_type , " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  public.po_mstr " _
            & "  inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "  inner join ptnrac_cntc on addrc_ptnra_oid  = ptnra_oid " _
            & "  inner join cmaddr_mstr on cmaddr_id = po_cmaddr_id " _
            & "  inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = po_tax_class " _
            & "  inner join cu_mstr on cu_id = po_cu_id " _
            & "  inner join cc_mstr on cc_id = po_cc_id " _
            & "  inner join pod_det on pod_po_oid = po_oid " _
            & "  inner join pt_mstr on pt_id = pod_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = pod_um  " _
            & "  inner join code_mstr as creditterm_mstr on po_mstr.po_credit_term = creditterm_mstr.code_id" _
            & "  inner join code_mstr addr_type on addr_type.code_id = ptnra_addr_type  " _
            & "  left outer join cmt_mstr on pod_oid = cmt_ref_oid  " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = po_oid  " _
            & "  where po_code >= '" + be_first.Text + "'" _
            & "  and po_code <= '" + be_to.Text + "'" _
                       & "  and addr_type.code_name ~~* 'ship to' " _
            & "  order by po_code, pod_seq, cmt_seq "


            Dim rpt As New XRPurchaseOrderPrintOutNoCost
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    MsgBox("Maaf data kosong. Gagal email silahkan perbaiki data kemudian klik remainder email")
                    'Exit Function
                    Return False
                    Exit Function
                End If

                .DataSource = ds
                .DataMember = "Table"

                If IO.Directory.Exists(My.Application.Info.DirectoryPath & "\export") = False Then
                    IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\export")
                End If

                '.ShowPreview()

                'Dim _option As New DevExpress.XtraPrinting.ImageExportOptions

                '_option.Resolution = 72
                '_option.ExportMode = DevExpress.XtraPrinting.ImageExportMode.SingleFile

                Dim imageOptions As DevExpress.XtraPrinting.ImageExportOptions = rpt.ExportOptions.Image

                ' Set Image-specific export options.
                imageOptions.Resolution = 72
                imageOptions.ExportMode = DevExpress.XtraPrinting.ImageExportMode.SingleFile
                imageOptions.Format = System.Drawing.Imaging.ImageFormat.Png


                .ExportToImage(par_filename, imageOptions)

            End With
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
End Class
