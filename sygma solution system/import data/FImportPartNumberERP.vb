Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FImportPartNumberERP
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FImportPurchaseOrderERP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        gv_erp.Columns("status").VisibleIndex = 0
    End Sub
    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        import_en_id.Properties.DataSource = dt_bantu
        import_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        import_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        import_en_id.ItemIndex = 0
    End Sub
    Public Overrides Sub format_grid()
        add_column_edit(gv_erp, "Selection", "status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Code", "pt_part", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Prod. Line", "pt_prod_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "UM", "pt_um", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Inv. Status", "pt_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Type", "pt_part_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Cost Method", "pt_cost_method", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Location", "pt_loc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Loc. Type", "loc_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Inv. Status On PO Receive", "pt_ms", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Group", "pt_group", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Taxable", "pt_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "ProcureMethod", "pt_pm_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Lot/Serial/Non", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Safety Stock", "pt_sfty_stk", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Reorder Point", "pt_ord_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Minimum Order", "pt_ord_min", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Maximum Order", "pt_ord_max", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Cost", "pt_cost", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Price", "pt_price", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Phantom", "pt_phantom", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Inspection Lead Time", "pt_insp_lead", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Manufacture Lead Time", "pt_mfg_lead", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Purchase Lead time", "pt_pur_lead", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_erp, "BOM", "bom_desc", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Routing", "pt_routing", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_erp, "Nesting", "pt_is_nesting", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_erp, "User Create", "loc_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_erp, "Date Create", "loc_add_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_erp, "User Update", "loc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Date Update", "pt_mod_date", DevExpress.Utils.HorzAlignment.Center)

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Dim i As Integer
        Dim k As Integer
        Dim ds_erp_kosong As DataSet
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            'Buat Data Set Kosong
            ds = New DataSet
            Try
                Using objload As New master_new.WDABase("", "")
                    With objload
                        .SQL = "select 0 as status,pt_part, pt_desc1, pt_desc2, pt_um, pt_prod_line, pt_group, " _
                              & " pt_part_type, pt_status, pt_phantom, pt_loc, pt_ms, pt_avg_int, " _
                              & " pt_ord_qty, pt_ord_per, pt_vend,pt_lot_ser, " _
                              & " pt_pm_code, pt_mfg_lead, pt_pur_lead, pt_insp_rqd, pt_insp_lead, " _
                              & " pt_cum_lead, pt_ord_min, pt_ord_max, pt_ord_mult, pt_yield_pct, " _
                              & " pt_price, pt_routing, pt_rev,pt_sfty_stk,pt_rop,pt_abc, " _
                              & " pt_size, pt_size_um, pt_taxable, pt_site, pt_length, pt_height, " _
                              & " pt_width,pt_mod_date " _
                              & " from pub.pt_mstr " _
                              & " where pt_status in ('-99') "
                        .InitializeCommand()
                        .FillDataSet(ds, "erp")


                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ' Buat Dataset Part Number Dari ERP
            ds_erp_kosong = New DataSet
            Try
                Using objload As New master_new.WDABase("", "")
                    With objload
                        If pt_modified.Checked = True Then
                            .SQL = "select 0 as status,pt_part, pt_desc1, pt_desc2, pt_um, pt_prod_line, pt_group, " _
                            & " pt_part_type, pt_status, pt_phantom, pt_loc, pt_ms, pt_avg_int, " _
                            & " pt_ord_qty, pt_ord_per, pt_vend,pt_lot_ser, " _
                            & " pt_pm_code, pt_mfg_lead, pt_pur_lead, pt_insp_rqd, pt_insp_lead, " _
                            & " pt_cum_lead, pt_ord_min, pt_ord_max, pt_ord_mult, pt_yield_pct, " _
                            & " pt_price, pt_routing, pt_rev,pt_sfty_stk,pt_rop,pt_abc, " _
                            & " pt_size, pt_size_um, pt_taxable, pt_site, pt_length, pt_height, " _
                            & " pt_width,pt_mod_date " _
                            & " from pub.pt_mstr " _
                            & " where pt_status in ('ACT','act','') " 
                        ElseIf pt_modified.Checked = False Then
                            .SQL = "select 0 as status,pt_part, pt_desc1, pt_desc2, pt_um, pt_prod_line, pt_group, " _
                            & " pt_part_type, pt_status, pt_phantom, pt_loc, pt_ms, pt_avg_int, " _
                            & " pt_ord_qty, pt_ord_per, pt_vend,pt_lot_ser, " _
                            & " pt_pm_code, pt_mfg_lead, pt_pur_lead, pt_insp_rqd, pt_insp_lead, " _
                            & " pt_cum_lead, pt_ord_min, pt_ord_max, pt_ord_mult, pt_yield_pct, " _
                            & " pt_price, pt_routing, pt_rev,pt_sfty_stk,pt_rop,pt_abc, " _
                            & " pt_size, pt_size_um, pt_taxable, pt_site, pt_length, pt_height, " _
                            & " pt_width,pt_mod_date " _
                            & " from pub.pt_mstr	 " _
                            & " where pt_part like '%" + Trim(te_part_number.Text) + "%' " _
                            & " and pt_status in ('ACT','act','') "
                        End If

                        .InitializeCommand()
                        .FillDataSet(ds_erp_kosong, "erp_kosong")

                        ds_erp_kosong.Tables("erp_kosong").AcceptChanges()

                        ' Buat Data Set Part Number Dari Syspro 2
                        Dim ds_ptmstr As DataSet
                        ds_ptmstr = New DataSet
                        Try
                            Using objcb As New master_new.WDABasepgsql("", "")
                                With objcb
                                    .SQL = "SELECT  pt_code,pt_upd_date_erp" _
                                   & " FROM pt_mstr where pt_type='I'" ' = " & SetSetring(ds.Tables("erp").Rows(i).Item("pt_part"))
                                    .FillDataSet(ds_ptmstr, "pt_mstr")
                                End With
                            End Using
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                        Dim _status As Boolean = False
                        For i = 0 To ds_erp_kosong.Tables(0).Rows.Count - 1
                            _status = False
                            For k = 0 To ds_ptmstr.Tables(0).Rows.Count - 1
                                Try

                                    If ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_part").ToString.Trim = ds_ptmstr.Tables(0).Rows(k).Item("pt_code").ToString.Trim Then
                                        If CDate(ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_mod_date")) > CDate(ds_ptmstr.Tables(0).Rows(k).Item("pt_upd_date_erp")) Then
                                            _status = False
                                        Else
                                            _status = True
                                        End If
                                    End If

                                Catch ex As Exception
                                    MessageBox.Show((i + 1).ToString, ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_part"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End Try
                            Next

                            If _status = False Then
                                'Insert Dataset ERP Ke Dataset Kosong
                                Dim _dtrow As DataRow
                                _dtrow = ds.Tables(0).NewRow
                                _dtrow("pt_part") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_part")
                                _dtrow("pt_desc1") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_desc1").ToString()
                                _dtrow("pt_desc2") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_desc2").ToString()
                                _dtrow("pt_um") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_um")
                                _dtrow("pt_prod_line") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_prod_line")
                                _dtrow("pt_group") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_group")
                                _dtrow("pt_part_type") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_part_type")
                                _dtrow("pt_status") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_status")
                                _dtrow("pt_phantom") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_phantom")
                                _dtrow("pt_loc") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_loc")
                                _dtrow("pt_ms") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_ms")
                                _dtrow("pt_avg_int") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_avg_int")
                                _dtrow("pt_ord_qty") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_ord_qty")
                                _dtrow("pt_ord_per") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_ord_per")
                                _dtrow("pt_vend") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_vend")
                                _dtrow("pt_lot_ser") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_lot_ser")
                                _dtrow("pt_pm_code") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_pm_code")
                                _dtrow("pt_mfg_lead") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_mfg_lead")
                                _dtrow("pt_pur_lead") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_pur_lead")
                                _dtrow("pt_insp_rqd") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_insp_rqd")
                                _dtrow("pt_insp_lead") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_insp_lead")
                                _dtrow("pt_cum_lead") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_cum_lead")
                                _dtrow("pt_ord_min") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_ord_min")
                                _dtrow("pt_ord_max") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_ord_max")
                                _dtrow("pt_ord_mult") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_ord_mult")
                                _dtrow("pt_yield_pct") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_yield_pct")
                                _dtrow("pt_price") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_price")
                                _dtrow("pt_routing") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_routing")
                                _dtrow("pt_rev") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_rev")
                                _dtrow("pt_sfty_stk") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_sfty_stk")
                                _dtrow("pt_rop") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_rop")
                                _dtrow("pt_abc") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_abc")
                                _dtrow("pt_size") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_size")
                                _dtrow("pt_size_um") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_size_um")
                                _dtrow("pt_taxable") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_taxable")
                                _dtrow("pt_site") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_site")
                                _dtrow("pt_length") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_length")
                                _dtrow("pt_height") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_height")
                                _dtrow("pt_width") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_width")
                                _dtrow("pt_mod_date") = ds_erp_kosong.Tables("erp_kosong").Rows(i).Item("pt_mod_date")
                                ds.Tables(0).Rows.Add(_dtrow)
                            End If
                        Next

                    End With
                End Using

                Dim ColSelectErp As DataColumn
                ColSelectErp = New DataColumn("status", System.Type.GetType("System.Boolean"))
                ds.Tables("erp").Columns.Add(ColSelectErp)

                Dim j As Integer
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    ds.Tables("erp").Rows(j).Item("status") = True
                Next

                ds.Tables("erp").AcceptChanges()

                gc_erp.DataSource = ds.Tables("erp")
                bestfit_column()
                ConditionsAdjustment()
                load_data_grid_detail()
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
            End Try

        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim k, j As Integer
        j = 0

        For k = 0 To ds.Tables("erp").Rows.Count - 1
            If ds.Tables("erp").Rows(k).Item("status") = True Then
                j += 1
            End If
        Next

        If j = 0 Then
            MessageBox.Show("Please Select Data, First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If import_en_id.EditValue = 0 Then
            MessageBox.Show("Please Define Entity First...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If MessageBox.Show("Migrate Data To Syspro..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables("erp").Rows.Count = 0 Then
            Exit Sub
        End If

        Dim i, _pl_id, _um, _grup As Integer
        Dim sSQL, _oid_mstr As String

        sSQL = "SELECT  " _
                & "  a.cs_id, " _
                & "  a.cs_name, " _
                & "  a.cs_is_default " _
                & "FROM " _
                & "  public.cs_mstr a " _
                & "WHERE " _
                & "  a.cs_is_default = 'Y'"

        Dim dt_cs As New DataTable
        dt_cs = master_new.PGSqlConn.GetTableData(sSQL)

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
        dt_csd = master_new.PGSqlConn.GetTableData(sSQL)

        For i = 0 To ds.Tables("erp").Rows.Count - 1
            '    If ds.Tables("erp").Rows(i).Item("pt_prod_line") <> "MI" And ds.Tables("erp").Rows(_row).Item("pt_prod_line") <> "ML" Then
            '        MessageBox.Show("Error Product Line..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Sub
            '    End If
            'Next
            If ds.Tables("erp").Rows(i).Item("status") = True Then

                Dim ds_bantu As New DataSet
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "select * from pt_mstr " + _
                                   " where pt_code = " + SetSetring(ds.Tables(0).Rows(i).Item("pt_part"))
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "pt")
                            dt_bantu = ds_bantu.Tables(0)
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


                Try
                    Using objcek As New master_new.WDABasepgsql("", "")
                        With objcek
                            .Connection.Open()
                            .Command = .Connection.CreateCommand
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "select pl_id from pl_mstr where pl_code ~~* '" + ds.Tables("erp").Rows(i).Item("pt_prod_line") + "'" _
                                                    & " and pl_active = 'Y' "
                            .InitializeCommand()
                            .DataReader = .Command.ExecuteReader
                            If .DataReader.HasRows = False Then
                                MessageBox.Show("Product Line " & ds.Tables("erp").Rows(i).Item("pt_prod_line") & " Doesn't Exist, Please Input First ...", "Information", MessageBoxButtons.OK)
                                Exit Sub
                            Else
                                While .DataReader.Read
                                    _pl_id = .DataReader("pl_id")
                                End While
                            End If
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try


                Try
                    Using objcek As New master_new.WDABasepgsql("", "")
                        With objcek
                            .Connection.Open()
                            .Command = .Connection.CreateCommand
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "select code_id from code_mstr where code_name ~~* '" + ds.Tables("erp").Rows(i).Item("pt_um") + "'" _
                                                    & " and code_active = 'Y' "
                            .InitializeCommand()
                            .DataReader = .Command.ExecuteReader
                            If .DataReader.HasRows = False Then
                                MessageBox.Show("Unit Measure " & ds.Tables("erp").Rows(i).Item("pt_um") & " Doesn't Exist, Please Input First ...", "Information", MessageBoxButtons.OK)
                                Exit Sub
                            Else
                                While .DataReader.Read
                                    _um = .DataReader("code_id")
                                End While
                            End If
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                Try
                    Using objcek As New master_new.WDABasepgsql("", "")
                        With objcek
                            .Connection.Open()
                            .Command = .Connection.CreateCommand
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "select code_id from code_mstr where code_name ~~* '" + ds.Tables("erp").Rows(i).Item("pt_group") + "'" _
                                                    & " and code_active = 'Y' "
                            .InitializeCommand()
                            .DataReader = .Command.ExecuteReader
                            If .DataReader.HasRows = False Then
                                MessageBox.Show("Part Number Group " & ds.Tables("erp").Rows(i).Item("pt_group") & " Doesn't Exist, Please Input First ...", "Information", MessageBoxButtons.OK)
                                Exit Sub
                            Else
                                While .DataReader.Read
                                    _grup = .DataReader("code_id")
                                End While
                            End If
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try



                If SetSetring(ds.Tables("erp").Rows(i).Item("pt_lot_ser")) <> "S" Then
                    ds.Tables(0).Rows(i).Item("pt_lot_ser") = "N"
                Else
                    ds.Tables(0).Rows(i).Item("pt_lot_ser") = "S"
                End If

                Dim _pt_oid As Guid
                _pt_oid = Guid.NewGuid
                Dim _pt_id As Integer
                _pt_id = func_coll.GetID("pt_mstr", import_en_id.GetColumnValue("en_code"), "pt_id", "pt_en_id", import_en_id.EditValue.ToString)

                'Dim _pod_taxable As String = ""
                'Dim _po_cu_id As Integer
                '_po_cu_id = get_cu_id(ds.Tables(0).Rows(0).Item("po_curr"))

                'insert data Part Number
                If ds_bantu.Tables("pt").Rows.Count < 1 Then

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
                                                        & "  public.pt_mstr " _
                                                        & "( " _
                                                        & "  pt_oid, " _
                                                        & "  pt_dom_id, " _
                                                        & "  pt_en_id, " _
                                                        & "  pt_add_by, " _
                                                        & "  pt_add_date, " _
                                                        & "  pt_upd_by_erp, " _
                                                        & "  pt_upd_date_erp, " _
                                                        & "  pt_id, " _
                                                        & "  pt_code, " _
                                                        & "  pt_desc1, " _
                                                        & "  pt_desc2, " _
                                                        & "  pt_pl_id, " _
                                                        & "  pt_um, " _
                                                        & "  pt_its_id, " _
                                                        & "  pt_type, " _
                                                        & "  pt_cost_method, " _
                                                        & "  pt_loc_id, " _
                                                        & "  pt_loc_type, " _
                                                        & "  pt_po_is, " _
                                                        & "  pt_group, " _
                                                        & "  pt_taxable, " _
                                                        & "  pt_pm_code, " _
                                                        & "  pt_ls, " _
                                                        & "  pt_sfty_stk, " _
                                                        & "  pt_rop, " _
                                                        & "  pt_ord_min, " _
                                                        & "  pt_ord_max, " _
                                                        & "  pt_cost, " _
                                                        & "  pt_price, " _
                                                        & "  pt_class, " _
                                                        & "  pt_inspection_lt, " _
                                                        & "  pt_manufacture_lt, " _
                                                        & "  pt_purchase_lt, " _
                                                        & "  pt_phantom, " _
                                                        & "  pt_is_nesting, " _
                                                        & "  pt_si_id, " _
                                                        & "  pt_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(_pt_oid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(1) & ", " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "current_timestamp" & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & SetDate(ds.Tables(0).Rows(i).Item("pt_mod_date")) & ",  " _
                                                        & SetInteger(_pt_id) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(i).Item("pt_part")) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(i).Item("pt_desc1")) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(i).Item("pt_desc2")) & ",  " _
                                                        & SetSetring(_pl_id) & ",  " _
                                                        & SetSetring(_um) & ",  " _
                                                        & SetInteger(1) & ",  " _
                                                        & SetSetring("I") & ",  " _
                                                        & SetSetring("A") & ",  " _
                                                        & SetInteger(1) & ",  " _
                                                        & SetInteger(12) & ",  " _
                                                        & SetInteger(1) & ",  " _
                                                        & SetInteger(_grup) & ",  " _
                                                        & SetBitYN(ds.Tables(0).Rows(i).Item("pt_taxable")) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(i).Item("pt_pm_code")) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(i).Item("pt_lot_ser")) & ",  " _
                                                        & SetDbl(ds.Tables(0).Rows(i).Item("pt_sfty_stk")) & ",  " _
                                                        & SetDbl(ds.Tables(0).Rows(i).Item("pt_rop")) & ",  " _
                                                        & SetDbl(ds.Tables(0).Rows(i).Item("pt_ord_min")) & ",  " _
                                                        & SetDbl(ds.Tables(0).Rows(i).Item("pt_ord_max")) & ",  " _
                                                        & SetDbl(0) & ",  " _
                                                        & SetDbl(ds.Tables(0).Rows(i).Item("pt_price")) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(i).Item("pt_abc")) & ",  " _
                                                        & SetInteger(ds.Tables(0).Rows(i).Item("pt_insp_lead")) & ",  " _
                                                        & SetInteger(ds.Tables(0).Rows(i).Item("pt_mfg_lead")) & ",  " _
                                                        & SetInteger(ds.Tables(0).Rows(i).Item("pt_pur_lead")) & ",  " _
                                                        & SetBitYN(ds.Tables(0).Rows(i).Item("pt_phantom")) & ",  " _
                                                        & SetBitYN(0) & ",  " _
                                                        & SetInteger(1) & ",  " _
                                                        & "  current_timestamp)"


                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

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
                                            & SetInteger(import_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(1) & ",  " _
                                            & SetInteger(_pt_id) & ",  " _
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

                                    sqlTran.Commit()
                                    'MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    'ds.Tables(0).Clear()
                                    'te_part_number.Text = ""
                                    'te_part_number.Focus()
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
                    '    MessageBox.Show("Part Number Already Available...!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "UPDATE  " _
                                                        & "  public.pt_mstr   " _
                                                        & "SET  " _
                                                        & "  pt_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & "  pt_en_id = " & SetInteger(1) & ",  " _
                                                        & "  pt_upd_by_erp = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & "  pt_upd_date_erp = current_timestamp,  " _
                                                        & "  pt_desc1 = " & SetSetring(ds.Tables(0).Rows(i).Item("pt_desc1")) & ",  " _
                                                        & "  pt_desc2 = " & SetSetring(ds.Tables(0).Rows(i).Item("pt_desc2")) & ",  " _
                                                        & "  pt_pl_id = " & SetSetring(_pl_id) & ",  " _
                                                        & "  pt_um = " & SetSetring(_um) & ",  " _
                                                        & "  pt_its_id = " & SetInteger(1) & ",  " _
                                                        & "  pt_type = " & SetSetring("I") & ",  " _
                                                        & "  pt_cost_method = " & SetSetring("A") & ",  " _
                                                        & "  pt_loc_id = " & SetInteger(1) & ",  " _
                                                        & "  pt_loc_type = " & SetInteger(12) & ",  " _
                                                        & "  pt_po_is = " & SetInteger(1) & ",  " _
                                                        & "  pt_group = " & SetInteger(_grup) & ",  " _
                                                        & "  pt_taxable = " & SetBitYN(ds.Tables(0).Rows(i).Item("pt_taxable")) & ",  " _
                                                        & "  pt_pm_code = " & SetSetring(ds.Tables(0).Rows(i).Item("pt_pm_code")) & ",  " _
                                                        & "  pt_ls = " & SetSetring(ds.Tables(0).Rows(i).Item("pt_lot_ser")) & ",  " _
                                                        & "  pt_sfty_stk = " & SetDbl(ds.Tables(0).Rows(i).Item("pt_sfty_stk")) & ",  " _
                                                        & "  pt_rop = " & SetDbl(ds.Tables(0).Rows(i).Item("pt_rop")) & ",  " _
                                                        & "  pt_ord_min = " & SetDbl(ds.Tables(0).Rows(i).Item("pt_ord_min")) & ",  " _
                                                        & "  pt_ord_max = " & SetDbl(ds.Tables(0).Rows(i).Item("pt_ord_max")) & ",  " _
                                                        & "  pt_cost = " & SetDbl(0) & ",  " _
                                                        & "  pt_price = " & SetDbl(ds.Tables(0).Rows(i).Item("pt_price")) & ",  " _
                                                        & "  pt_class = " & SetSetring(ds.Tables(0).Rows(i).Item("pt_abc")) & ",  " _
                                                        & "  pt_inspection_lt = " & SetInteger(ds.Tables(0).Rows(i).Item("pt_insp_lead")) & ",  " _
                                                        & "  pt_manufacture_lt = " & SetInteger(ds.Tables(0).Rows(i).Item("pt_mfg_lead")) & ",  " _
                                                        & "  pt_purchase_lt = " & SetInteger(ds.Tables(0).Rows(i).Item("pt_pur_lead")) & ",  " _
                                                        & "  pt_phantom = " & SetBitYN(ds.Tables(0).Rows(i).Item("pt_phantom")) & ",  " _
                                                        & "  pt_is_nesting = " & SetBitYN(0) & ",  " _
                                                        & "  pt_dt = current_timestamp  " _
                                                        & "  " _
                                                        & "WHERE  " _
                                                        & "  pt_code = " & SetSetring(ds.Tables(0).Rows(i).Item("pt_part")) & " "

                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    sqlTran.Commit()

                                    'after_success()
                                    'set_row(Trim(pt_code.Text), "pt_code")
                                    'edit = True
                                Catch ex As PgSqlException
                                    sqlTran.Rollback()
                                    MessageBox.Show(ex.Message)
                                End Try
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End If
            End If
        Next i

        MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ds.Tables(0).Clear()
        load_data_grid_detail()
        'te_part_number.Text = ""
        'te_part_number.Focus()

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
