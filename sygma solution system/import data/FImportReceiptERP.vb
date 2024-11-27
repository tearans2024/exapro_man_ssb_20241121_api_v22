Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FImportReceiptERP
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FImportReceiptERP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_erp, "PO Number", "prh_nbr", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Supplier", "ad_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Order Date", "po_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Due Date", "po_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_erp, "Site", "po_site", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Receive Number", "prh_receiver", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Receive Date", "prh_rcp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Packing Slip Number", "prh_ps_nbr", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Type", "prh_rcp_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Currency", "prh_curr", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Exchange Rate", "prh_ex_rate2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Site", "prh_site", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Part Number", "pod_part", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Prod Line", "pt_prod_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Qty Receipt", "prh_rcvd", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_erp, "Purchase Cost", "pod_pur_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_erp, "Tax Class", "pod_taxc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        If Trim(te_receipt_number.Text) = "" Then
            Exit Sub
        End If

        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            If xtc_master.SelectedTabPageIndex = 0 Then
                Try
                    ds = New DataSet
                    Using objload As New master_new.WDABase("", "")
                        With objload

                            .SQL = "select prh_nbr, prh_vend, ad_name, po_ord_date, po_due_date, po_site, " _
                                 & " prh_receiver, prh_rcp_date, prh_ps_nbr, prh_rcp_type, prh_curr, " _
                                 & " prh_ex_rate2, prh_site,  " _
                                 & " pod_part, ifnull(pt_desc1,'')as pt_desc1, ifnull(pt_desc2,'') as pt_desc2, pt_prod_line, " _
                                 & " prh_line, prh_rcvd, prh_um, " _
                                 & " pod_qty_ord, pod_pur_cost, pod_disc_pct, pod_need, pod_due_date, pod_taxable, pod_taxc, " _
                                 & " prh_part, pod_loc, prh_domain, prh_curr_amt, prh_pur_cost, prh_pur_std, prh_type, prh_taxc " _
                                 & " from pub.prh_hist	 " _
                                 & " inner join pub.ad_mstr on ad_addr = prh_vend " _
                                 & " inner join pub.po_mstr on po_nbr = prh_nbr " _
                                 & " inner join pub.pod_det on pod_nbr = prh_nbr and pod_line = prh_line " _
                                 & " left outer join pub.pt_mstr on pt_part = pod_part " _
                                 & " where prh_receiver = '" + Trim(te_receipt_number.Text) + "'"

                            .InitializeCommand()
                            .FillDataSet(ds, "erp")
                            gc_erp.DataSource = ds.Tables("erp")

                            bestfit_column()
                            ConditionsAdjustment()
                            load_data_grid_detail()
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        If MessageBox.Show("Migrate Data To Syspro..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables("erp").Rows.Count = 0 Then
            Exit Sub
        End If

        Dim i, _row, _ptnr_id As Integer
        _row = BindingContext(ds.Tables(0)).Position

        For i = 0 To ds.Tables("erp").Rows.Count - 1
            If ds.Tables("erp").Rows(i).Item("pt_prod_line") <> "MI" And ds.Tables("erp").Rows(_row).Item("pt_prod_line") <> "ML" Then
                MessageBox.Show("Error Product Line..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Next
        
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ptnr_id from ptnr_mstr where ptnr_name ~~* '" + ds.Tables("erp").Rows(_row).Item("ad_name") + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows = False Then
                        MessageBox.Show("Data Supplier Doesn't Exist..", "Information", MessageBoxButtons.OK)
                        Exit Sub
                    Else
                        While .DataReader.Read
                            _ptnr_id = .DataReader("ptnr_id")
                        End While
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dim _po_oid As Guid
        Dim _pod_taxable As String = ""
        Dim _pod_tax_class As Integer
        _po_oid = Guid.NewGuid
        Dim _pod_pt_id As Integer
        Dim _rcv_cu_id As Integer
        _rcv_cu_id = get_cu_id(ds.Tables(0).Rows(0).Item("prh_curr"))

        'Untuk Keperluan ke database SYSPR MFGPRO.................
        Dim ds_local As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql_erp("", "")
                With objcb
                    .SQL = "select distinct(prh_receiver) as prh_receiver from prh_hist_local " _
                         + " where prh_receiver = '" + ds.Tables("erp").Rows(0).Item("prh_receiver") + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_local, "prh_hist_local")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If ds_local.Tables(0).Rows.Count = 0 Then
            Try
                Using objinsert As New master_new.WDABasepgsql_erp("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            For i = 0 To ds.Tables("erp").Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.prh_hist_local " _
                                    & "( " _
                                    & "  prh_nbr, " _
                                    & "  prh_vend, " _
                                    & "  ad_name, " _
                                    & "  prh_receiver, " _
                                    & "  prh_ps_nbr, " _
                                    & "  prh_rcp_date, " _
                                    & "  prh_part, " _
                                    & "  pt_description, " _
                                    & "  prh_rcvd, " _
                                    & "  prh_um, " _
                                    & "  pod_loc, " _
                                    & "  prh_domain, " _
                                    & "  prh_curr, " _
                                    & "  prh_ex_rate2, " _
                                    & "  prh_curr_amt, " _
                                    & "  prh_pur_cost, " _
                                    & "  prh_pur_std, " _
                                    & "  prh_type, " _
                                    & "  prh_rcp_type, " _
                                    & "  prh_line, " _
                                    & "  pt_desc1, " _
                                    & "  pt_desc2, " _
                                    & "  prh_taxc " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_nbr")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_vend")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("ad_name")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_receiver")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_ps_nbr")) & ",  " _
                                    & SetDate(ds.Tables("erp").Rows(i).Item("prh_rcp_date")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_part")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("pt_desc1") + " " + ds.Tables("erp").Rows(i).Item("pt_desc2")) & ",  " _
                                    & SetDbl(ds.Tables("erp").Rows(i).Item("prh_rcvd")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_um")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("pod_loc")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_domain")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_curr")) & ",  " _
                                    & SetDbl(ds.Tables("erp").Rows(i).Item("prh_ex_rate2")) & ",  " _
                                    & SetDbl(ds.Tables("erp").Rows(i).Item("prh_curr_amt")) & ",  " _
                                    & SetDbl(ds.Tables("erp").Rows(i).Item("prh_pur_cost")) & ",  " _
                                    & SetDbl(ds.Tables("erp").Rows(i).Item("prh_pur_std")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_type")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_rcp_type")) & ",  " _
                                    & SetInteger(ds.Tables("erp").Rows(i).Item("prh_line")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("pt_desc1")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("pt_desc2")) & ",  " _
                                    & SetSetring(ds.Tables("erp").Rows(i).Item("prh_taxc")) & "  " _
                                    & ")"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                            sqlTran.Commit()
                        Catch ex As PgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub 'harus keluar agar tidak update ke database syspsro
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub 'harus keluar agar tidak update ke database syspsro
            End Try
        End If

        'End OF Untuk Keperluan ke database SYSPR MFGPRO.................

        'insert data PO
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.po_mstr " _
                                            & "( " _
                                            & "  po_oid, " _
                                            & "  po_dom_id, " _
                                            & "  po_en_id, " _
                                            & "  po_add_by, " _
                                            & "  po_add_date, " _
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
                                            & "  po_total, " _
                                            & "  po_tran_id, " _
                                            & "  po_credit_term, " _
                                            & "  po_taxable, " _
                                            & "  po_tax_inc, " _
                                            & "  po_tax_class, " _
                                            & "  po_cu_id, " _
                                            & "  po_exc_rate, " _
                                            & "  po_total_ppn, " _
                                            & "  po_total_pph, " _
                                            & "  po_status_cash, " _
                                            & "  po_do_code, " _
                                            & "  po_erp, " _
                                            & "  po_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_po_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(1) & ", " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " current_timestamp " & ", " _
                                            & SetSetring(ds.Tables(0).Rows(_row).Item("prh_nbr")) & ",  " _
                                            & SetInteger(_ptnr_id) & ", " _
                                            & SetInteger(1) & ", " _
                                            & "current_timestamp" & ",  " _
                                            & SetDate(ds.Tables(0).Rows(_row).Item("po_ord_date")) & ",  " _
                                            & SetDate(ds.Tables(0).Rows(_row).Item("po_due_date")) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & " null " & ",  " _
                                            & " null " & ",  " _
                                            & " null " & ",  " _
                                            & " null " & ",  " _
                                            & SetDbl(0) & ", " _
                                            & SetInteger(5) & ", " _
                                            & SetInteger(1084) & ", " _
                                            & SetSetring("N") & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & SetInteger(18) & ", " _
                                            & SetInteger(_rcv_cu_id) & ", " _
                                            & SetDbl(1) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetSetring("R") & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds.Tables("erp").Rows.Count - 1
                            If ds.Tables("erp").Rows(i).Item("pod_taxable") = True Then
                                _pod_taxable = "Y"
                            End If

                            If ds.Tables("erp").Rows(i).Item("pod_taxc") = "" Then
                                _pod_tax_class = 18
                            Else
                                _pod_tax_class = get_tax_class(ds.Tables("erp").Rows(i).Item("pod_taxc"))
                                If _pod_tax_class = -1 Then
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If
                            End If

                            If ds.Tables("erp").Rows(i).Item("pt_prod_line") = "ML" Then
                                _pod_pt_id = 1019
                            ElseIf ds.Tables("erp").Rows(i).Item("pt_prod_line") = "MI" Then
                                _pod_pt_id = 1041
                            End If
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pod_det " _
                                                & "( " _
                                                & "  pod_oid, " _
                                                & "  pod_dom_id, " _
                                                & "  pod_en_id, " _
                                                & "  pod_add_by, " _
                                                & "  pod_add_date, " _
                                                & "  pod_po_oid, " _
                                                & "  pod_seq, " _
                                                & "  pod_reqd_oid, " _
                                                & "  pod_si_id, " _
                                                & "  pod_pt_id, " _
                                                & "  pod_pt_desc1, " _
                                                & "  pod_pt_desc2, " _
                                                & "  pod_rmks, " _
                                                & "  pod_end_user, " _
                                                & "  pod_qty, " _
                                                & "  pod_um, " _
                                                & "  pod_cost, " _
                                                & "  pod_disc, " _
                                                & "  pod_sb_id, " _
                                                & "  pod_cc_id, " _
                                                & "  pod_pjc_id, " _
                                                & "  pod_need_date, " _
                                                & "  pod_due_date, " _
                                                & "  pod_um_conv, " _
                                                & "  pod_qty_real, " _
                                                & "  pod_taxable, " _
                                                & "  pod_tax_inc, " _
                                                & "  pod_tax_class, " _
                                                & "  pod_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & master_new.ClsVar.sdom_id & ",  " _
                                                & SetInteger(1) & ", " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetSetring(_po_oid.ToString) & ",  " _
                                                & SetInteger(ds.Tables("erp").Rows(i).Item("prh_line")) & ",  " _
                                                & " null " & ",  " _
                                                & SetInteger(1) & ", " _
                                                & SetInteger(_pod_pt_id) & ",  " _
                                                & SetSetringDB(ds.Tables("erp").Rows(i).Item("pod_part")) & ",  " _
                                                & SetSetringDB(ds.Tables("erp").Rows(i).Item("pt_desc1") + " " + ds.Tables("erp").Rows(i).Item("pt_desc2")) & ",  " _
                                                & SetSetringDB("") & ",  " _
                                                & SetSetringDB("") & ",  " _
                                                & SetDbl(ds.Tables("erp").Rows(i).Item("prh_rcvd")) & ",  " _
                                                & SetInteger(10) & ",  " _
                                                & SetDbl(ds.Tables("erp").Rows(i).Item("pod_pur_cost")) & ",  " _
                                                & SetDbl(ds.Tables("erp").Rows(i).Item("pod_disc_pct")) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & " null " & ",  " _
                                                & " null " & ",  " _
                                                & " null " & ",  " _
                                                & " null " & ",  " _
                                                & " null " & ",  " _
                                                & SetSetring(_pod_taxable) & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & SetInteger(_pod_tax_class) & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next


                        sqlTran.Commit()
                        'MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'load_data_many(True)

                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select * from po_mstr " + _
                           " inner join pod_det on pod_po_oid = po_oid " + _
                           " where po_oid = " + SetSetring(_po_oid.ToString)
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "po")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Insert Data Receipt
        Dim _rcv_oid As Guid
        Dim _rcv_is_receive As String
        Dim ssqls As New ArrayList
        Dim _prh_ex_rate2 As Double = ds.Tables(0).Rows(0).Item("prh_ex_rate2")
        Dim _prh_rcp_date As Date = ds.Tables("erp").Rows(0).Item("prh_rcp_date")
        Dim _type As String = ""
        _rcv_oid = Guid.NewGuid

        If ds.Tables("erp").Rows(0).Item("prh_rcp_type") = "" Then
            _rcv_is_receive = "Y"
            _type = "IC-RPO"
        Else
            _rcv_is_receive = "N"
            _type = "IC-PRS"
        End If

        If _rcv_cu_id = -1 Then
            Exit Sub
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.rcv_mstr " _
                                            & "( " _
                                            & "  rcv_oid, " _
                                            & "  rcv_dom_id, " _
                                            & "  rcv_en_id, " _
                                            & "  rcv_add_by, " _
                                            & "  rcv_add_date, " _
                                            & "  rcv_code, " _
                                            & "  rcv_date, " _
                                            & "  rcv_eff_date, " _
                                            & "  rcv_po_oid, " _
                                            & "  rcv_packing_slip, " _
                                            & "  rcv_cu_id, " _
                                            & "  rcv_exc_rate, " _
                                            & "  rcv_is_receive, " _
                                            & "  rcv_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_rcv_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(1) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(Trim(te_receipt_number.Text).ToUpper) & ",  " _
                                            & "current_date" & ",  " _
                                            & SetDate(ds.Tables("erp").Rows(0).Item("prh_rcp_date")) & ",  " _
                                            & SetSetring(_po_oid.ToString) & ",  " _
                                            & SetSetring(ds.Tables(0).Rows(0).Item("prh_ps_nbr")) & ",  " _
                                            & SetInteger(_rcv_cu_id) & ",  " _
                                            & SetDbl(ds.Tables(0).Rows(0).Item("prh_ex_rate2")) & ",  " _
                                            & SetSetring(_rcv_is_receive) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ")"
                        'di atas ada SetDate(ds.Tables("erp").Rows(0).Item("prh_rcp_date")) ini agar tetep mengacu ke eff_date di receipt
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.rcvd_det " _
                                                    & "( " _
                                                    & "  rcvd_oid, " _
                                                    & "  rcvd_rcv_oid, " _
                                                    & "  rcvd_pod_oid, " _
                                                    & "  rcvd_qty, " _
                                                    & "  rcvd_um, " _
                                                    & "  rcvd_packing_qty, " _
                                                    & "  rcvd_um_conv, " _
                                                    & "  rcvd_qty_real, " _
                                                    & "  rcvd_si_id, " _
                                                    & "  rcvd_loc_id, " _
                                                    & "  rcvd_supp_lot, " _
                                                    & "  rcvd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(_rcv_oid.ToString) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(i).Item("pod_oid").ToString) & ",  " _
                                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                    & SetInteger(10) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(1) & ",  " _
                                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                    & SetInteger(1) & ",  " _
                                                   & SetInteger(2) & ",  " _
                                                    & " null " & ",  " _
                                                    & "current_timestamp" & "  " _
                                                    & ")"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        
                        If func_coll.get_conf_file("automatic_jurnal_receipt_return") = "1" Then
                            If insert_glt_det_ic_local(ssqls, objinsert, ds_bantu, _
                                                 1, get_en_code, _
                                                 _rcv_oid.ToString, Trim(te_receipt_number.Text), _
                                                 _prh_rcp_date, _
                                                 _rcv_cu_id, _prh_ex_rate2, _
                                                 "IC", _type) = False Then
                                sqlTran.Rollback()
                                Exit Sub
                            End If
                        End If

                        sqlTran.Commit()
                        MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        'load_data_many(True)   
                        ds.Tables(0).Clear()
                        te_receipt_number.Text = ""
                        te_receipt_number.Focus()
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function insert_glt_det_ic_local(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_exc_rate As Double, _
                                   ByVal par_type As String, ByVal par_daybook As String) As Boolean
        insert_glt_det_ic_local = True
        Dim i, _pl_id As Integer
        Dim _glt_code, _glt_desc As String
        Dim dt_bantu As DataTable
        Dim _cost As Double
        Dim _seq As Integer = 0
        _glt_desc = ""
        _glt_code = func_coll.get_transaction_number("GR", par_en_code, "glt_det", "glt_code", func_coll.get_tanggal_sistem)

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("pod_qty") <> 0 Then
                        dt_bantu = New DataTable
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pod_pt_id"))

                        If par_daybook.ToUpper = "IC-RPO" Then
                            dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))
                            _glt_desc = "PO Receipt"
                        ElseIf par_daybook.ToUpper = "IC-PRS" Then
                            dt_bantu = (func_coll.get_prodline_account(_pl_id, "PRC_PORACC"))
                            _glt_desc = "PO Return"
                        End If

                        _cost = func_coll.get_cost_pod_det(par_ds.Tables(0).Rows(i).Item("pod_oid")) * par_ds.Tables(0).Rows(i).Item("pod_qty")
                        If _cost < 0 Then
                            _cost = _cost * -1
                        End If
                        'Insert Untuk Yang Debet Dulu
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring(_glt_desc) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, par_cu_id, _
                                                         par_exc_rate, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If
                        '********************** finish untuk yang debet

                        _seq = _seq + 1
                        dt_bantu = New DataTable

                        If par_daybook.ToUpper = "IC-RPO" Then
                            dt_bantu = (func_coll.get_prodline_account(_pl_id, "PRC_PORACC"))
                        ElseIf par_daybook.ToUpper = "IC-PRS" Then
                            dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB(_glt_desc) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, par_cu_id, _
                                                         par_exc_rate, _cost, "C") = False Then

                            Return False
                            Exit Function
                        End If

                        _seq = _seq + 1
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next

    End Function

    Private Function get_cu_id(ByVal par_cu_name As String) As Integer
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select cu_id from cu_mstr where cu_name ~~* '" + par_cu_name + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows = False Then
                        MessageBox.Show("Data Currency Doesn't Exist..", "Information", MessageBoxButtons.OK)
                        Return -1
                    Else
                        While .DataReader.Read
                            get_cu_id = .DataReader("cu_id")
                        End While
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return -1
            Exit Function
        End Try
        Return get_cu_id
    End Function

    Private Function get_tax_class(ByVal par_tax_name As String) As Integer
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_id from code_mstr where code_field ~~* 'taxclass_mstr'" + _
                                           " and code_code ~~* '" + par_tax_name + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows = False Then
                        MessageBox.Show("Data Tax Class Doesn't Exist..", "Information", MessageBoxButtons.OK)
                        Return -1
                    Else
                        While .DataReader.Read
                            get_tax_class = .DataReader("code_id")
                        End While
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return -1
            Exit Function
        End Try
        Return get_tax_class
    End Function

    Private Function get_en_code() As String
        get_en_code = ""
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select en_code from en_mstr where en_id = 1"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_en_code = .DataReader("en_code")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_en_code
    End Function
End Class
