Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FGenerateSiteCost
    Public func_data As New function_data
    Dim dt_bantu As DataTable
    Dim sSQL As String
    Private Sub FGenerateSiteCost_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        en_id.Properties.DataSource = dt_bantu
        en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        en_id.ItemIndex = 0
    End Sub

    Private Sub en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles en_id.EditValueChanged
        init_le(si_id, "site", en_id.EditValue)
    End Sub

    Private Sub pt_id_from_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id_from.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._en_id = en_id.EditValue
            frm._obj = pt_id_from
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub pt_id_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id_to.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._en_id = en_id.EditValue
            frm._obj = pt_id_to
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub generate_site_cost()
        Dim _oid_mstr As String
        Try
            sSQL = "select pt_id,pt_code  " _
                & "from pt_mstr  " _
                & "where  " _
                & "lower(pt_type)='i' and pt_code between '" & pt_id_from.Text & "' and '" & pt_id_to.Text & "' " _
                & " and pt_id not in (select distinct sct_pt_id from sct_mstr) " _
                & "order by pt_code"

            Dim dt_pt As New DataTable
            dt_pt = GetTableData(sSQL)


            sSQL = "SELECT  " _
                & "  a.cs_id, " _
                & "  a.cs_name, " _
                & "  a.cs_is_default " _
                & "FROM " _
                & "  public.cs_mstr a " _
                & "WHERE " _
                & "  a.cs_is_default = 'Y'"

            Dim dt_cs As New DataTable
            dt_cs = GetTableData(sSQL)

            'sSQL = "SELECT  " _
            '    & "  a.cs_id, " _
            '    & "  a.cs_name, " _
            '    & "  a.cs_is_default, " _
            '    & "  b.csd_oid, " _
            '    & "  b.csd_seq " _
            '    & "FROM " _
            '    & "  public.cs_mstr a " _
            '    & "  INNER JOIN public.csd_det b ON (a.cs_oid = b.csd_cs_oid) " _
            '    & "WHERE " _
            '    & "  a.cs_is_default = 'Y' " _
            '    & "ORDER BY csd_seq "

            sSQL = "SELECT  " _
               & "  a.cs_id, " _
               & "  a.cs_name, " _
               & "  a.cs_is_default, " _
               & "  b.csd_oid, " _
               & "  b.csd_seq, " _
               & "  d.csc_ac_id " _
               & "FROM " _
               & "  public.cs_mstr a " _
               & "  INNER JOIN public.csd_det b ON (a.cs_oid = b.csd_cs_oid) " _
               & "  INNER JOIN public.csc_category d ON (d.csc_id = b.csd_csc_id) " _
               & "  INNER JOIN public.ac_mstr c ON (d.csc_ac_id = c.ac_id) " _
               & "WHERE " _
               & "  a.cs_is_default = 'Y' " _
               & "ORDER BY csd_seq "

            Dim dt_csd As New DataTable
            dt_csd = GetTableData(sSQL)

            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For Each dr_pt As DataRow In dt_pt.Rows
                            For Each dr_cs As DataRow In dt_cs.Rows
                                _oid_mstr = Guid.NewGuid.ToString

                                sSQL = "INSERT INTO  " _
                                    & "  public.sct_mstr " _
                                    & "( " _
                                    & "  sct_oid, " _
                                    & "  sct_dom_id, " _
                                    & "  sct_en_id, " _
                                    & "  sct_add_by, " _
                                    & "  sct_add_date, " _
                                    & "  sct_dt, " _
                                    & "  sct_si_id, " _
                                    & "  sct_pt_id, " _
                                    & "  sct_cs_id, " _
                                    & "  sct_total, " _
                                    & "  sct_mtl_tl, " _
                                    & "  sct_lbr_tl, " _
                                    & "  sct_bdn_tl, " _
                                    & "  sct_ovh_tl, " _
                                    & "  sct_sub_tl, " _
                                    & "  sct_mtl_ll, " _
                                    & "  sct_lbr_ll, " _
                                    & "  sct_bdn_ll, " _
                                    & "  sct_ovh_ll, " _
                                    & "  sct_sub_ll " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(_oid_mstr) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(en_id.EditValue) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & SetInteger(si_id.EditValue) & ",  " _
                                    & SetInteger(dr_pt("pt_id")) & ",  " _
                                    & SetInteger(dr_cs("cs_id")) & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & ",  " _
                                    & 0 & "  " _
                                    & ")"



                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = sSQL

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()



                                For Each dr_csd As DataRow In dt_csd.Rows
                                    If dr_csd("cs_id") = dr_cs("cs_id") Then
                                        sSQL = "INSERT INTO  " _
                                            & "  public.sctd_det " _
                                            & "( " _
                                            & "  sctd_oid, " _
                                            & "  sctd_add_by, " _
                                            & "  sctd_add_date, " _
                                            & "  sctd_dt, " _
                                            & "  sctd_sct_oid, " _
                                            & "  sctd_csd_oid, " _
                                            & "  sctd_ac_id, " _
                                            & "  sctd_tl_amount, " _
                                            & "  sctd_ll_amount, " _
                                            & "  sctd_amount " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_oid_mstr) & ",  " _
                                            & SetSetring(dr_csd("csd_oid")) & ",  " _
                                            & SetInteger(dr_csd("csc_ac_id")) & ",  " _
                                            & 0 & ",  " _
                                            & 0 & ",  " _
                                            & 0 & "  " _
                                            & ")"


                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = sSQL

                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                Next
                            Next
                        Next

                        sqlTran.Commit()
                        Box("Generate success")
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub bt_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_generate.Click
        If MessageBox.Show("Generate Site Cost...?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        generate_site_cost()
    End Sub
End Class
