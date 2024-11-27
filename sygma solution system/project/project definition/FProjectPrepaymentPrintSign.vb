Public Class FProjectPrepaymentPrintSign
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FProjectPrepaymentPrintSign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FProjectShipment_so_Search()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FProjectShipment_so_Search()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        Dim _conf_document_sign As Integer
        Dim _code_id As Integer = 0

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_id from code_mstr where code_field ~~* 'type_document' and code_code ~~* 'PS'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _code_id = .DataReader.Item("code_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        _conf_document_sign = func_coll.get_conf_file("document_sign")

        If _conf_document_sign = 1 Then
            _sql = "SELECT   " _
                    & " soship_code,soship_date,prj_code, " _
                    & " soshipd_soship_oid,  " _
                    & " ptnr_name, " _
                    & " soshipd_seq, soshipd_prepayment as soshipd_progress_pay, " _
                    & " soshipd_qty as soshipds_qty, um.code_name as unit,  " _
                    & " soshipd_qty_real,  " _
                    & " prjd_price,  " _
                    & " prjd_price * soshipd_qty * soshipd_prepayment as soshipd_amount,  " _
                    & " si_desc,  " _
                    & " loc_desc,  " _
                    & " pt_code,(coalesce(prjd_pt_desc1,'') || ' ' || coalesce(prjd_pt_desc2,'')) as pt_desc,  " _
                    & "  (SELECT coalesce(docappd_user_id,' ') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                    & "          WHERE docapp_cc_id = 0 and docappd_seq = 0 and code_id = " + _code_id.ToString + " limit 1) as sign1, " _
                    & "  COALESCE((SELECT coalesce(docappd_user_id,' ') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                    & "          WHERE docapp_cc_id = 0 AND docappd_seq = 1 and code_id = " + _code_id.ToString + " limit 1),'') as sign2, " _
                    & "  COALESCE((SELECT coalesce(docappd_user_id,' ') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                    & "          WHERE docapp_cc_id = 0 AND docappd_seq = 2 and code_id = " + _code_id.ToString + " limit 1),'') as sign3, " _
                    & "  (SELECT coalesce(docappd_position,' ') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                    & "          WHERE docapp_cc_id = 0 AND docappd_seq = 0 and code_id = " + _code_id.ToString + " limit 1) as post1, " _
                    & "  COALESCE((SELECT coalesce(docappd_position,' ') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                    & "          WHERE docapp_cc_id = 0 AND docappd_seq = 1 and code_id = " + _code_id.ToString + " limit 1),'') as post2, " _
                    & "  COALESCE((SELECT coalesce(docappd_position,' ') as docappd_user_id FROM docappd_det inner join docapp_mstr on docapp_oid = docappd_docapp_oid inner join code_mstr on code_id = docapp_doc_id" _
                    & "          WHERE docapp_cc_id = 0 AND docappd_seq = 2 and code_id = " + _code_id.ToString + " limit 1),'') as post3 " _
                    & " FROM   " _
                    & " public.soshipd_det  " _
                    & " inner join soship_mstr on soship_oid = soshipd_soship_oid  " _
                    & " inner join prjd_det on prjd_oid = soshipd_prjd_oid  " _
                    & " inner join prj_mstr on prj_oid = prjd_prj_oid " _
                    & " inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                    & " inner join pt_mstr on pt_id = prjd_pt_id  " _
                    & " inner join si_mstr on si_id = soshipd_si_id  " _
                    & " left outer join loc_mstr on loc_id = soshipd_loc_id  " _
                    & " inner join code_mstr um on um.code_id = prjd_um " _
                    & " where soship_code >= '" + be_first.Text + "'" _
                    & " and soship_code <= '" + be_to.Text + "'" _
                    & "  and coalesce(soship_is_shipment,'N') = 'Y' " _
                    & "  and coalesce(soship_is_prepayment,'N') = 'Y' " _
                    & " ORDER BY soship_code, soshipd_seq "

            Dim rpt As New rptProjectPrepayment
            Try
                With rpt
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
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

        Else
            _sql = "SELECT   " _
                    & " soship_code,soship_date,prj_code, " _
                    & " soshipd_soship_oid,  " _
                    & " ptnr_name, " _
                    & " soshipd_seq, soshipd_prepayment as soshipd_progress_pay, " _
                    & " soshipd_qty as soshipds_qty,um.code_name as unit,  " _
                    & " soshipd_qty_real,  " _
                    & " prjd_price,  " _
                    & " prjd_price * soshipd_qty * soshipd_prepayment as soshipd_amount,  " _
                    & " si_desc,  " _
                    & " loc_desc,  " _
                    & " pt_code,(coalesce(prjd_pt_desc1,'') || ' ' || coalesce(prjd_pt_desc2,'')) as pt_desc,  " _
                    & " FROM   " _
                    & " public.soshipd_det  " _
                    & " inner join soship_mstr on soship_oid = soshipd_soship_oid  " _
                    & " inner join prjd_det on prjd_oid = soshipd_prjd_oid  " _
                    & " inner join prj_mstr on prj_oid = prjd_prj_oid " _
                    & " inner join ptnr_mstr on ptnr_id = prj_ptnr_id_sold " _
                    & " inner join pt_mstr on pt_id = prjd_pt_id  " _
                    & " inner join si_mstr on si_id = soshipd_si_id  " _
                    & " left outer join loc_mstr on loc_id = soshipd_loc_id  " _
                    & " inner join code_mstr um on um.code_id = prjd_um " _
                    & " where soship_code >= '" + be_first.Text + "'" _
                    & " and soship_code <= '" + be_to.Text + "'" _
                    & "  and coalesce(soship_is_shipment,'N') = 'Y' " _
                    & "  and coalesce(soship_is_prepayment,'N') = 'Y' " _
                    & " ORDER BY soship_code, soshipd_seq "
            Dim rpt As New rptProjectShipment
            Try
                With rpt
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
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
        End If

    End Sub

End Class
