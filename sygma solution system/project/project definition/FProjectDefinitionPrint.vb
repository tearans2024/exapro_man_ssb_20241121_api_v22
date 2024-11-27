Public Class FProjectDefinitionPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FSalesOrderReturnPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Try
            Dim frm As New FProjectSearch
            frm.set_win(Me)
            frm._en_id = le_entity.EditValue
            frm._obj = be_first
            frm.ShowDialog()
            frm.type_form = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Try
            Dim frm As New FProjectSearch
            frm.set_win(Me)
            frm._en_id = le_entity.EditValue
            frm._obj = be_to
            frm.ShowDialog()
            frm.type_form = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  prj_oid, " _
            & "  prj_dom_id, " _
            & "  prj_en_id,en_mstr.en_desc, " _
            & "  prj_add_by, " _
            & "  prj_add_date, " _
            & "  prj_upd_by, " _
            & "  prj_upd_date, " _
            & "  prj_dt, " _
            & "  prj_code, " _
            & "  prj_ptnr_id_sold,sold.ptnr_name as sold_ptnr_name, " _
            & "  prj_ptnr_id_bill,bill.ptnr_name as bill_ptnr_name, " _
            & "  prj_sales_person_id,sal.ptnr_name as sall_ptnr_name, " _
            & "  prj_pjt_code_id,prj_type.code_name as project_type, " _
            & "  prj_ord_date, " _
            & "  prj_start_date, " _
            & "  prj_end_date, " _
            & "  prj_si_id,si_mstr.si_code, " _
            & "  prj_cu_id,cu_code, " _
            & "  prj_exc_rate, " _
            & "  prj_credit_term,ct.code_name as credit_term, " _
            & "  prj_taxable, " _
            & "  prj_tax_inc, " _
            & "  prj_tax_class,tclass.code_name as tax_class, " _
            & "  prj_total, " _
            & "  prj_total_ppn, " _
            & "  prj_total_pph, " _
            & "  (prj_total + prj_total_ppn + prj_total_pph) as prj_total_after_tax," _
            & "  prj_exc_rate * prj_total as prj_total_ext, " _
            & "  prj_exc_rate * prj_total_ppn as prj_total_ppn_ext, " _
            & "  prj_exc_rate * prj_total_pph as prj_total_pph_ext, " _
            & "  prj_exc_rate * (prj_total + prj_total_ppn + prj_total_pph) as prj_total_after_tax_ext, " _
            & "  prj_tran_id, tran_name, " _
            & "  prj_trans_id, " _
            & "  prj_pocust_oid, " _
            & "  prj_ar_ac_id,ac_code,ac_name, " _
            & "  prj_ar_sb_id,sb_code,sb_desc, " _
            & "  prj_ar_cc_id,cc_code,cc_desc, " _
            & "  prjd_oid, " _
            & "  prjd_dom_id, " _
            & "  prjd_en_id, en_mstr_detail.en_desc as en_desc_detail, " _
            & "  prjd_add_by, " _
            & "  prjd_add_date, " _
            & "  prjd_upd_by, " _
            & "  prjd_upd_date, " _
            & "  prjd_dt, " _
            & "  prjd_prj_oid, " _
            & "  prjd_seq, " _
            & "  prjd_si_id,si_mstr_detail.si_desc as si_desc_detail, " _
            & "  prjd_pt_id,pt_code, " _
            & "  prjd_pt_desc1, " _
            & "  prjd_pt_desc2, " _
            & "  prjd_loc_id,loc_code,loc_desc, " _
            & "  prjd_qty, " _
            & "  prjd_qty_full, " _
            & "  prjd_um,um.code_name as um_name, " _
            & "  prjd_price as prjd_cost, " _
            & "  prjd_price, " _
            & "  prjd_disc, " _
            & "  prjd_um_conv, " _
            & "  prjd_qty_real, " _
            & "  ((prjd_qty * prjd_price) - (prjd_qty * prjd_price * prjd_disc)) as prjd_qty_cost, " _
            & "  prjd_taxable, " _
            & "  prjd_tax_inc, " _
            & "  prjd_tax_class,tclass_detail.code_name as tax_class_detail, " _
            & "  prjd_trans_id, " _
            & "  prjd_qty_pao, " _
            & "  prjd_qty - prjd_qty_pao as qty_open, " _
            & "  prjd_qty_mo, " _
            & "  coalesce(prjd_qty_do,0) as prjd_qty_do, " _
            & "  prjd_type,type.code_code as code_code,ptnra_line_1,ptnra_line_2,ptnra_line_3,cmaddr_name,coalesce(cmaddr_line_1,'') || ' ' || coalesce(cmaddr_line_1,'') as cmaddr_line " _
            & "FROM  " _
            & "  public.prj_mstr  " _
            & "  inner join en_mstr on en_id = prj_en_id " _
            & "  inner join prjd_det on prjd_prj_oid = prj_oid " _
            & "  inner join ptnr_mstr sold on sold.ptnr_id = prj_ptnr_id_sold " _
             & "  left OUTER join ptnra_addr on ptnra_ptnr_oid =   sold.ptnr_oid " _
              & "  left OUTER join cmaddr_mstr on cmaddr_en_id =   en_mstr.en_id " _
            & "  inner join ptnr_mstr bill on bill.ptnr_id = prj_ptnr_id_bill " _
            & "  inner join ptnr_mstr sal on sal.ptnr_id = prj_sales_person_id " _
            & "  inner join code_mstr prj_type on prj_type.code_id = prj_pjt_code_id " _
            & "  inner join si_mstr on si_id = prj_si_id " _
            & "  inner join cu_mstr on cu_id = prj_cu_id " _
            & "  inner join code_mstr ct on ct.code_id = prj_credit_term " _
            & "  inner join code_mstr tclass on tclass.code_id = prj_tax_class " _
            & "  inner join ac_mstr on ac_id = prj_ar_ac_id " _
            & "  inner join sb_mstr on sb_id = prj_ar_sb_id " _
            & "  inner join cc_mstr on cc_id = prj_ar_cc_id " _
            & "  inner join tran_mstr on tran_id = prj_tran_id " _
            & "  inner join en_mstr en_mstr_detail on en_mstr_detail.en_id = prjd_en_id " _
            & "  inner join si_mstr si_mstr_detail on si_mstr_detail.si_id = prjd_si_id " _
            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
            & "  inner join code_mstr um on um.code_id =  prjd_um " _
            & "  inner join code_mstr tclass_detail on tclass_detail.code_id = prjd_tax_class " _
            & "  inner join code_mstr type on type.code_id = prjd_type " _
            & " where prj_code >= " + master_new.ModFunction.SetSetring(be_first.Text) _
            & " and prj_code <= " + master_new.ModFunction.SetSetring(be_to.Text)

        '& "  ((prjd_qty * prjd_price) - (prjd_qty * prjd_price * prjd_disc)) as prjd_qty_cost, " _ ini salah penamaan dari kang har, tapi karena sudah di set di report nya ke _cost maka biarkan saja
        Dim rpt As New XRprojectDefinition
        Try
            With rpt
                Try
                    Using objcb As New master_new.CustomCommand
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_bantu
                .DataMember = "data"
                .ShowPreview()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
