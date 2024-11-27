Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FImportPurchaseOrderERP
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FImportPurchaseOrderERP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_erp, "PO Number", "po_nbr", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Vendor", "po_vend", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Supplier", "ad_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Currency", "po_curr", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Order Date", "po_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Due Date", "po_due_date", DevExpress.Utils.HorzAlignment.Center)

        'add_column_copy(gv_erp, "Site", "po_site", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_erp, "Receive Number", "prh_receiver", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Receive Date", "prh_rcp_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_erp, "Packing Slip Number", "prh_ps_nbr", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Type", "prh_rcp_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Exchange Rate", "prh_ex_rate2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Site", "prh_site", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Part Number", "pod_part", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Prod Line", "pt_prod_line", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Qty Receipt", "prh_rcvd", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_erp, "Purchase Cost", "pod_pur_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_erp, "Tax Class", "pod_taxc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        If Trim(te_po_number.Text) = "" Then
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

                            '.SQL = "select prh_nbr, prh_vend, ad_name, po_ord_date, po_due_date, po_site, " _
                            '    & " prh_receiver, prh_rcp_date, prh_ps_nbr, prh_rcp_type, prh_curr, " _
                            '    & " prh_ex_rate2, prh_site,  " _
                            '    & " prh_line, prh_rcvd, prh_um, " _
                            '    & " prh_part, prh_domain, prh_curr_amt, prh_pur_cost, prh_pur_std, prh_type, prh_taxc " _
                            '    & " from pub.prh_hist	 " _
                            '    & " left outer join pub.ad_mstr on ad_addr = prh_vend " _
                            '    & " left outer join pub.po_mstr on po_nbr = prh_nbr " _
                            '    & " where prh_nbr = '" + Trim(te_po_number.Text) + "'"

                            .SQL = "select po_nbr, po_vend, ad_name, po_ord_date, po_due_date, po_site, po_curr " _
                               & " from pub.po_mstr	 " _
                               & " left outer join pub.ad_mstr on ad_addr = po_vend " _
                               & " where po_nbr = '" + Trim(te_po_number.Text) + "'"

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

        Dim _row, _ptnr_id As Integer
        _row = BindingContext(ds.Tables(0)).Position

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select * from po_mstr " + _
                           " where po_code = " + SetSetring(ds.Tables(0).Rows(_row).Item("po_nbr"))
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "po")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If ds_bantu.Tables("po").Rows.Count < 1 Then
            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select ptnr_id from ptnr_mstr where ptnr_name ~~* '" + ds.Tables("erp").Rows(_row).Item("ad_name") + "'" _
                                                 & " and ptnr_en_id in (select user_en_id from tconfuserentity " _
                                                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                 & " and ptnr_active = 'Y' "
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
            _po_oid = Guid.NewGuid
            Dim _po_cu_id As Integer
            _po_cu_id = get_cu_id(ds.Tables(0).Rows(0).Item("po_curr"))

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
                                                & "  po_type, " _
                                                & "  po_bk_id, " _
                                                & "  po_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_po_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(1) & ", " _
                                                & SetSetring(master_new.ClsVar.sNama) & ", " _
                                                & " current_timestamp " & ", " _
                                                & SetSetring(ds.Tables(0).Rows(_row).Item("po_nbr")) & ",  " _
                                                & SetInteger(_ptnr_id) & ", " _
                                                & SetInteger(1) & ", " _
                                                & "current_timestamp" & ",  " _
                                                & SetDate(ds.Tables(0).Rows(_row).Item("po_ord_date")) & ",  " _
                                                & SetDate(ds.Tables(0).Rows(_row).Item("po_due_date")) & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetInteger(0) & ", " _
                                                & SetInteger(0) & ", " _
                                                & SetInteger(1) & ", " _
                                                & SetInteger(0) & ", " _
                                                & SetDbl(0) & ", " _
                                                & SetInteger(5) & ", " _
                                                & SetInteger(1084) & ", " _
                                                & SetSetring("N") & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & SetInteger(18) & ", " _
                                                & SetInteger(_po_cu_id) & ", " _
                                                & SetDbl(1) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetSetring("R") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("Y") & ",  " _
                                                & SetSetring("P") & ",  " _
                                                & SetInteger(0) & ", " _
                                                & "current_timestamp" & "  " _
                                                & ")"

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()
                            MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ds.Tables(0).Clear()
                            te_po_number.Text = ""
                            te_po_number.Focus()
                        Catch ex As PgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show("Migrate Data Failed...!!!, Please Try Again..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        Else
            MessageBox.Show("Purchase Order Already Available...!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

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
