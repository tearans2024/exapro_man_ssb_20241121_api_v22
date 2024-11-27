Public Class XRCogsSimulation

    Private Sub GroupFooter1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint
        Dim sSQL As String
        Try

            sSQL = "SELECT  " _
                & "  a.cogscr_seq, " _
                & "  a.cogscr_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  d.pt_desc2, " _
                & "  e.code_name, " _
                & "  b.ro_code, " _
                & "  b.ro_desc, " _
                & "  b.ro_total, " _
                & "  c.rod_op, " _
                & "  f.wc_desc, " _
                & "  c.rod_desc, " _
                & "  c.rod_run, " _
                & "  c.rod_setup, " _
                & "  c.rod_lbr_amount + " _
                & "  c.rod_bdn_amount + " _
                & "  c.rod_sbc_amount as amount, " _
                & "  a.cogscr_qty " _
                & "FROM " _
                & "  public.rod_det c " _
                & "  INNER JOIN public.ro_mstr b ON (c.rod_ro_oid = b.ro_oid) " _
                & "  INNER JOIN public.cogscr_route a ON (b.ro_id = a.cogscr_ro_id) " _
                & "  INNER JOIN public.pt_mstr d ON (a.cogscr_pt_id = d.pt_id) " _
                & "  INNER JOIN public.code_mstr e ON (d.pt_um = e.code_id) " _
                & "  INNER JOIN public.wc_mstr f ON (c.rod_wc_id = f.wc_id) " _
                & "WHERE " _
                & "  a.cogscr_cogsc_oid = '" & GetCurrentColumnValue("cogsc_oid").ToString & "'"


            With XrSubreport1.ReportSource
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(sSQL)

                .DataSource = ds
                .DataMember = "Table"

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try

            sSQL = "SELECT  " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  d.code_code, " _
                & "  a.cogscr_oid, " _
                & "  a.cogscr_cogsc_oid, " _
                & "  a.cogscr_seq, " _
                & "  a.cogscr_pt_id, " _
                & "  a.cogscr_qty, " _
                & "  a.cogscr_cost,d.code_name as um_desc, pl_desc, " _
                & "  a.cogscr_cost * a.cogscr_qty as cogscr_cost_ext,f.ac_code,f.ac_name " _
                & "FROM " _
                & "  public.pt_mstr b " _
                & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
                & "  INNER JOIN public.cogscr_raw a ON (b.pt_id = a.cogscr_pt_id) " _
                & "  INNER JOIN public.cogsc_calc c ON (a.cogscr_cogsc_oid = c.cogsc_oid) " _
                & "  INNER JOIN public.pl_mstr e ON (b.pt_pl_id = e.pl_id) " _
                & "  LEFT OUTER JOIN public.ac_mstr f ON (a.cogscr_ac_id = f.ac_id)   " _
                & "  WHERE  " _
                & "  cogscr_cogsc_oid='" & GetCurrentColumnValue("cogsc_oid").ToString & "'"

            With XrSubreport2.ReportSource
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(sSQL)

                .DataSource = ds
                .DataMember = "Table"

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Try

            sSQL = "SELECT  " _
                & "  a.cogsca_cogsc_oid, " _
                & "  a.cogsca_seq, " _
                & "  a.cogsca_ac_id, " _
                & "  b.ac_code, " _
                & "  b.ac_name, " _
                & "  a.cogsca_amount, " _
                & "  a.cogsca_oid,cogsca_type " _
                & "FROM " _
                & "  public.cogsca_acct a " _
                & "  INNER JOIN public.ac_mstr b ON (a.cogsca_ac_id = b.ac_id) " _
                & "  INNER JOIN public.cogsc_calc c ON (a.cogsca_cogsc_oid = c.cogsc_oid)  " _
                & "WHERE  " _
                & "  cogsca_cogsc_oid='" & GetCurrentColumnValue("cogsc_oid").ToString & "'"

            'sSQL = "SELECT  " _
            '    & "  b.pt_code, " _
            '    & "  b.pt_desc1, " _
            '    & "  b.pt_desc2, " _
            '    & "  d.code_code, " _
            '    & "  a.cogscr_oid, " _
            '    & "  a.cogscr_cogsc_oid, " _
            '    & "  a.cogscr_seq, " _
            '    & "  a.cogscr_pt_id, " _
            '    & "  a.cogscr_qty, " _
            '    & "  a.cogscr_cost,d.code_name as um_desc, pl_desc, " _
            '    & "  a.cogscr_cost * a.cogscr_qty as cogscr_cost_ext,f.ac_code,f.ac_name " _
            '    & "FROM " _
            '    & "  public.pt_mstr b " _
            '    & "  INNER JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
            '    & "  INNER JOIN public.cogscr_raw a ON (b.pt_id = a.cogscr_pt_id) " _
            '    & "  INNER JOIN public.cogsc_calc c ON (a.cogscr_cogsc_oid = c.cogsc_oid) " _
            '    & "  INNER JOIN public.pl_mstr e ON (b.pt_pl_id = e.pl_id) " _
            '    & "  LEFT OUTER JOIN public.ac_mstr f ON (a.cogscr_ac_id = f.ac_id)   " _
            '    & "  WHERE  " _
            '    & "  cogscr_cogsc_oid='" & GetCurrentColumnValue("cogsc_oid").ToString & "'"

            With XrSubreport3.ReportSource
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(sSQL)

                .DataSource = ds
                .DataMember = "Table"

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class