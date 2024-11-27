Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FPartNumberImport

    Dim _pt_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_import As DataSet

    Private Sub FPartNumberImport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("unitmeasure", pt_en_id.EditValue))

        With pt_um
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("its_mstr", pt_en_id.EditValue))

        With pt_its_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("its_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("its_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(pt_en_id.EditValue))

        With pt_loc_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("loc_type_mstr", pt_en_id.EditValue))

        With pt_loc_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("is_mstr", pt_en_id.EditValue))

        With pt_po_is
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("is_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("is_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("group_mstr", pt_en_id.EditValue))

        With pt_group
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Private Sub sc_le_pt_en_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pt_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        import_en_id.Properties.DataSource = dt_bantu
        import_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        import_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        import_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        With pt_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pl_mstr", ""))
        With pt_pl_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("pl_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("pl_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_type", ""))
        With pt_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_cost_method", ""))
        With pt_cost_method
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_ls", ""))
        With pt_ls
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pt_pm_code", ""))
        With pt_pm_code
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pt_class("A','B','C','D','E"))
        With pt_class
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_code").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_prod_line, "Site", "site", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Code", "code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Description1", "desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Description2", "desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Prod. Line", "prod_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Inv. Status", "inventory_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Type", "type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Class", "class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Cost Method", "cost_methode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Location", "location", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Loc. Type", "location_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Inv. Status On PO Receive", "po_receipts_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Group", "group", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Taxable", "taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "ProcureMethod", "procure_methode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Lot/Serial/Non", "lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Safety Stock", "safety_stock", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Reorder Point", "reorder_point", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Minimum Order", "min_order", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Maximum Order", "max_order", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Cost", "cost", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Price", "price", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_prod_line, "Phantom", "phantom", DevExpress.Utils.HorzAlignment.Default)
    End Sub


    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        If Trim(be_import_xls.Text) = "" Then
            Exit Sub
        End If

        ds_import = New DataSet
        ds_import = func_data.import_from_excel(be_import_xls.Text)

        If ds_import Is Nothing Then
            MsgBox("Data Cant Retrieve From Excel, Please Check Your File..", MsgBoxStyle.Critical, "Import Error")
            Exit Sub
        End If

        gc_prod_line.DataSource = ds_import.Tables(0)
        gv_prod_line.Columns("code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        gv_prod_line.BestFitColumns()
    End Sub

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim i, _id_pl, _id, _id_pt As Integer
        Dim _code As String = "_x_"
        Dim ds_pl As DataSet
        Dim sSQL, _oid_mstr As String
        If ds_import.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds_import.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        If MessageBox.Show("Import Data To Syspro..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim guid_value As Guid

        gv_prod_line.ClearSorting()
        gv_prod_line.ClearColumnsFilter()
        gv_prod_line.ClearGrouping()
        gv_prod_line.Columns("code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        _id_pt = func_coll.GetID("pt_mstr", import_en_id.GetColumnValue("en_code"), "pt_id", "pt_en_id", import_en_id.EditValue.ToString)

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

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To gv_prod_line.RowCount - 1
                            guid_value = Guid.NewGuid

                            'site
                            Dim ds_site As New DataSet
                            Dim _si_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT si_id " _
                                             & "  FROM " _
                                             & "  si_mstr  " _
                                             & "  WHERE si_code ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "site")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_site, "site")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_site.Tables("site").Rows.Count > 0 Then
                                _si_id = ds_site.Tables("site").Rows(0).Item("si_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Site Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            ds_pl = New DataSet

                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  pl_mstr  " _
                                             & "  WHERE pl_desc ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "prod_line")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_pl, "pl")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_pl.Tables("pl").Rows.Count > 0 Then
                                _id_pl = ds_pl.Tables("pl").Rows(0).Item("pl_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Product Line Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MsgBox(_id)
                                Exit Sub
                            End If

                            'Lot/Serial
                            If UCase(gv_prod_line.GetRowCellValue(i, "lot_serial")) <> "L" And UCase(gv_prod_line.GetRowCellValue(i, "lot_serial")) <> "S" And UCase(gv_prod_line.GetRowCellValue(i, "lot_serial")) <> "N" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Lot / Serial / Non Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_prod_line.GetRowCellValue(i, "lot_serial").ToString())
                                Exit Sub
                            End If

                            'Unit Measure
                            Dim ds_um As New DataSet
                            Dim _um_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  code_mstr  " _
                                             & "  where code_field ~~* 'unitmeasure'  " _
                                             & "  and code_name ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "unit_measure")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_um, "um")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_um.Tables("um").Rows.Count > 0 Then
                                _um_id = ds_um.Tables("um").Rows(0).Item("code_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Unit Measure Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MsgBox(_um_id)
                                Exit Sub
                            End If

                            'Type
                            If UCase(gv_prod_line.GetRowCellValue(i, "type")) <> "I" And UCase(gv_prod_line.GetRowCellValue(i, "type")) <> "A" And UCase(gv_prod_line.GetRowCellValue(i, "type")) <> "E" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Type Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_prod_line.GetRowCellValue(i, "type").ToString())
                                Exit Sub
                            End If

                            'P/M
                            If UCase(gv_prod_line.GetRowCellValue(i, "procure_methode")) <> "M" And UCase(gv_prod_line.GetRowCellValue(i, "procure_methode")) <> "P" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Purchase / Manufacture Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_prod_line.GetRowCellValue(i, "procure_methode").ToString())
                                Exit Sub
                            End If

                            'Location
                            Dim ds_loc As New DataSet
                            Dim _loc_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  loc_mstr  " _
                                             & "  WHERE loc_desc ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "location")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_loc, "loc")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_loc.Tables("loc").Rows.Count > 0 Then
                                _loc_id = ds_loc.Tables("loc").Rows(0).Item("loc_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Location Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Location type
                            Dim ds_loc_type As New DataSet
                            Dim _id_loc_type As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT code_id " _
                                              & "  FROM " _
                                              & "  code_mstr  " _
                                              & "  where code_field ~~* 'loc_type_mstr'  " _
                                              & "  and code_name ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "location_type")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_loc_type, "loc_type")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_loc_type.Tables("loc_type").Rows.Count > 0 Then
                                _id_loc_type = ds_loc_type.Tables("loc_type").Rows(0).Item("code_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Location Type Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Item Status
                            Dim ds_its As New DataSet
                            Dim _its_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  its_mstr  " _
                                             & "  WHERE its_desc ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "inventory_status")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_its, "its")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_its.Tables("its").Rows.Count > 0 Then
                                _its_id = ds_its.Tables("its").Rows(0).Item("its_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Inventory Status Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MsgBox(_um_id)
                                Exit Sub
                            End If

                            'PO is
                            Dim ds_po_is As New DataSet
                            Dim _po_is_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  is_mstr  " _
                                             & "  WHERE is_code ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "po_receipts_status")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_po_is, "po_is")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_po_is.Tables("po_is").Rows.Count > 0 Then
                                _po_is_id = ds_po_is.Tables("po_is").Rows(0).Item("is_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("PO Receive Status Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MsgBox(_po_is_id)
                                Exit Sub
                            End If

                            'pt group
                            Dim ds_pt_group As New DataSet
                            Dim _pt_group_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  code_mstr  " _
                                             & "  where code_field ~~* 'group_mstr'  " _
                                             & "  and code_name ~~* " & SetSetring(gv_prod_line.GetRowCellValue(i, "group")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_pt_group, "group")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_pt_group.Tables("group").Rows.Count > 0 Then
                                _pt_group_id = ds_pt_group.Tables("group").Rows(0).Item("code_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Group Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MsgBox(_pt_group_id)
                                Exit Sub
                            End If

                            'Class
                            If UCase(gv_prod_line.GetRowCellValue(i, "class")) <> "A" And UCase(gv_prod_line.GetRowCellValue(i, "class")) <> "B" And UCase(gv_prod_line.GetRowCellValue(i, "class")) <> "C" And UCase(gv_prod_line.GetRowCellValue(i, "class")) <> "D" And UCase(gv_prod_line.GetRowCellValue(i, "class")) <> "E" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Class Status Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_prod_line.GetRowCellValue(i, "class").ToString())
                                Exit Sub
                            End If

                            'Cost Methode
                            If UCase(gv_prod_line.GetRowCellValue(i, "cost_methode")) <> "A" And UCase(gv_prod_line.GetRowCellValue(i, "cost_methode")) <> "F" And UCase(gv_prod_line.GetRowCellValue(i, "cost_methode")) <> "L" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Cost Methode Status Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_prod_line.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Phantom
                            If UCase(gv_prod_line.GetRowCellValue(i, "phantom")) <> "Y" And UCase(gv_prod_line.GetRowCellValue(i, "phantom")) <> "N" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Phantom Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pt_mstr " _
                                                & "( " _
                                                & "  pt_oid, " _
                                                & "  pt_dom_id, " _
                                                & "  pt_en_id, " _
                                                & "  pt_add_by, " _
                                                & "  pt_add_date, " _
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
                                                & "  pt_phantom, " _
                                                & "  pt_si_id, " _
                                                & "  pt_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(import_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(_id_pt) & ",  " _
                                                & SetSetringDB(gv_prod_line.GetRowCellValue(i, "code")) & ",  " _
                                                & SetSetringDB(gv_prod_line.GetRowCellValue(i, "desc1")) & ",  " _
                                                & SetSetringDB(gv_prod_line.GetRowCellValue(i, "desc2")) & ",  " _
                                                & SetInteger(_id_pl) & ",  " _
                                                & SetInteger(_um_id) & ",  " _
                                                & SetInteger(_its_id) & ",  " _
                                                & SetSetring(gv_prod_line.GetRowCellValue(i, "type")) & ",  " _
                                                & SetSetring(gv_prod_line.GetRowCellValue(i, "cost_methode")) & ",  " _
                                                & SetInteger(_loc_id) & ",  " _
                                                & SetInteger(_id_loc_type) & ",  " _
                                                & SetInteger(_po_is_id) & ",  " _
                                                & SetInteger(_pt_group_id) & ",  " _
                                                & SetSetring(gv_prod_line.GetRowCellValue(i, "taxable")) & ",  " _
                                                & SetSetring(gv_prod_line.GetRowCellValue(i, "procure_methode")) & ",  " _
                                                & SetSetring(gv_prod_line.GetRowCellValue(i, "lot_serial")) & ",  " _
                                                & SetDbl(gv_prod_line.GetRowCellValue(i, "safety_stock")) & ",  " _
                                                & SetDbl(gv_prod_line.GetRowCellValue(i, "reorder_point")) & ",  " _
                                                & SetDbl(gv_prod_line.GetRowCellValue(i, "min_order")) & ",  " _
                                                & SetDbl(gv_prod_line.GetRowCellValue(i, "max_order")) & ",  " _
                                                & SetDbl(gv_prod_line.GetRowCellValue(i, "cost")) & ",  " _
                                                & SetDbl(gv_prod_line.GetRowCellValue(i, "price")) & ",  " _
                                                & SetSetring(gv_prod_line.GetRowCellValue(i, "class")) & ",  " _
                                                & SetSetring(gv_prod_line.GetRowCellValue(i, "phantom")) & ",  " _
                                                & SetInteger(_si_id) & ",  " _
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
                                    & SetInteger(_si_id) & ",  " _
                                    & SetInteger(_id_pt) & ",  " _
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

                            _id_pt = _id_pt + 1
                        Next

                        sqlTran.Commit()
                        'sqlTran.Rollback()
                        MsgBox("Import success....")
                        be_import_xls.Text = ""
                        ds_import.Tables(0).Clear()
                        import_en_id.Enabled = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        MsgBox(i)
                        MsgBox(_id)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub be_import_xls_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_import_xls.ButtonClick
        Dim opendialog As New OpenFileDialog
        If import_en_id.EditValue = 0 Then
            MessageBox.Show("Please Define Entity First...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If opendialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            be_import_xls.Text = opendialog.FileName
            load_data_many(True)
            import_en_id.Enabled = False
        End If
    End Sub

    
End Class


