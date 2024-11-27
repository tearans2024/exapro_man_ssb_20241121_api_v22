Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FImportProdStructure
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_bef_insert As DataSet
    Dim dt_table As DataTable

    Private Sub FImportProdStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_excel, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Product Structure Code.", "ps_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Parent Part", "ptbomdesc_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Raw Part", "ptbomdesc_raw", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_excel, "Start Date", "psd_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_excel, "End Date", "psd_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_excel, "Qty", "psd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_excel, "Scrap", "psd_scrp_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        
    End Sub

    Private Sub be_import_xls_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_import_xls.ButtonClick
        Dim opendialog As New OpenFileDialog
        Dim ds_import As DataSet

        ds_import = New DataSet
        If opendialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            be_import_xls.Text = opendialog.FileName
            ds_import = New DataSet
            ds_import = ImportFromExcel(be_import_xls.Text)

            ds_bef_insert = New DataSet
            ds_bef_insert = arrange_from_excel(ds_import)
            dt_table = ds_bef_insert.Tables(0)
            'dt_table.DefaultView.Sort = "ro_code ASC"

            gc_excel.DataSource = dt_table
            gv_excel.BestFitColumns()

            migrate()
        End If
    End Sub

    Public Function ImportFromExcel(ByVal PrmPathExcelFile As String) As DataSet
        ImportFromExcel = Nothing
        Dim MyConnection As System.Data.OleDb.OleDbConnection = Nothing

        Try
            Dim DtSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")
            'MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [prod-str$]", MyConnection)
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [sheet1$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "product_structure")

            DtSet = New System.Data.DataSet

            If IsDBNull(DtSet.Tables("product_structure")) Then
                MsgBox("Data Kosong")
                Exit Function
            End If

            MyCommand.Fill(DtSet)
            MyConnection.Close()

            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
        End Try
    End Function

    Public Function arrange_from_excel(ByVal _par_ds_import_data As DataSet) As DataSet
        Dim dr_list As DataRow
        Dim ds_arrange As DataSet = Nothing
        Dim ds_part_raw, ds_part_parent As DataSet
        Dim i As Integer
        Dim _par_ds_import As DataSet

        _par_ds_import = New DataSet

        _par_ds_import = _par_ds_import_data

        If _par_ds_import.Tables("product_structure").Rows.Count > 0 Then

            ds_arrange = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  '' as en_desc, " _
                                & "  public.psd_det.psd_oid, " _
                                & "  public.psd_det.psd_ps_oid, " _
                                & "  public.psd_det.psd_add_by, " _
                                & "  public.psd_det.psd_add_date, " _
                                & "  public.psd_det.psd_upd_by, " _
                                & "  public.psd_det.psd_upd_date, " _
                                & "  public.psd_det.psd_use_bom, " _
                                & "  public.psd_det.psd_pt_bom_id, " _
                                & "  '' as ptbomdesc_raw, " _
                                & "  public.psd_det.psd_comp, " _
                                & "  public.psd_det.psd_ref, " _
                                & "  public.psd_det.psd_desc, " _
                                & "  public.psd_det.psd_start_date, " _
                                & "  public.psd_det.psd_end_date, " _
                                & "  public.psd_det.psd_qty, " _
                                & "  public.psd_det.psd_str_type, " _
                                & "  public.psd_det.psd_scrp_pct, " _
                                & "  public.psd_det.psd_lt_off, " _
                                & "  public.psd_det.psd_op, " _
                                & "  public.psd_det.psd_seq, " _
                                & "  public.psd_det.psd_fcst_pct, " _
                                & "  public.psd_det.psd_group, " _
                                & "  public.psd_det.psd_process, " _
                                & "  public.psd_det.psd_dt, " _
                                & "  public.ps_mstr.ps_oid, " _
                                & "  public.ps_mstr.ps_dom_id, " _
                                & "  public.ps_mstr.ps_en_id, " _
                                & "  public.ps_mstr.ps_add_by, " _
                                & "  public.ps_mstr.ps_add_date, " _
                                & "  public.ps_mstr.ps_upd_by, " _
                                & "  public.ps_mstr.ps_upd_date, " _
                                & "  public.ps_mstr.ps_id, " _
                                & "  public.ps_mstr.ps_par, " _
                                & "  public.ps_mstr.ps_desc, " _
                                & "  public.ps_mstr.ps_use_bom, " _
                                & "  public.ps_mstr.ps_pt_bom_id, " _
                                & "  '' as ptbomdesc_parent, " _
                                & "  public.ps_mstr.ps_active, " _
                                & "  public.ps_mstr.ps_dt " _
                                & "FROM " _
                                & "  public.ps_mstr " _
                                & "  INNER JOIN public.psd_det ON (public.ps_mstr.ps_oid = public.psd_det.psd_ps_oid)" _
                                & " where psd_seq = -312"

                        .InitializeCommand()
                        .FillDataSet(ds_arrange, "arrange_ps")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            i = 0

            For Each _dr As DataRow In _par_ds_import.Tables("product_structure").Rows
                If IsDBNull(_par_ds_import.Tables("product_structure").Rows(i).Item("part parent")) = False Then
                    Try
                        dr_list = ds_arrange.Tables(0).NewRow
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Please Contact Your Administrator")
                        ds_arrange.Clear()
                        Return ds_arrange
                    End Try
                    dr_list("en_desc") = le_entity.Text
                    dr_list("ps_desc") = SetString(_par_ds_import.Tables("product_structure").Rows(i).Item("part parent"))

                    ds_part_parent = New DataSet
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "SELECT  pt_id " _
                               & " FROM pt_mstr " _
                               & "  where lower(pt_code) = lower(" & SetSetring(_par_ds_import.Tables("product_structure").Rows(i).Item("part parent")) & ") " _
                               & "  and pt_en_id = " & le_entity.EditValue
                                .InitializeCommand()
                                .FillDataSet(ds_part_parent, "pt_mstr_parent")
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    'MessageBox.Show(ds_part_parent.Tables(0).Rows.Count)
                    If ds_part_parent.Tables(0).Rows.Count > 0 Then
                        dr_list("ps_pt_bom_id") = SetInteger(ds_part_parent.Tables(0).Rows(0).Item("pt_id"))
                        dr_list("ptbomdesc_parent") = SetString(_par_ds_import.Tables("product_structure").Rows(i).Item("part parent"))
                    Else
                        MsgBox("Unknown Parent Part Name At Line " & i + 1 & "....", MsgBoxStyle.Critical, "Part Name Not Found")
                        'ds_arrange.Clear()
                        'be_import_xls.Text = ""
                        'Return ds_arrange
                    End If

                    ds_part_raw = New DataSet
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "SELECT  pt_id " _
                               & " FROM pt_mstr " _
                               & "  where lower(pt_code) = lower(" & SetSetring(_par_ds_import.Tables("product_structure").Rows(i).Item("raw part")) & ") " _
                               & "  and pt_en_id = " & le_entity.EditValue
                                .InitializeCommand()
                                .FillDataSet(ds_part_raw, "pt_mstr_raw")
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try

                    If ds_part_raw.Tables(0).Rows.Count > 0 Then
                        dr_list("psd_pt_bom_id") = SetInteger(ds_part_raw.Tables(0).Rows(0).Item("pt_id"))
                        dr_list("ptbomdesc_raw") = SetString(_par_ds_import.Tables("product_structure").Rows(i).Item("raw part"))
                    Else
                        MsgBox("Unknown Raw Part Name At Line " & i + 1 & "....", MsgBoxStyle.Critical, "Part Name Not Found")
                        'ds_arrange.Clear()
                        'be_import_xls.Text = ""
                        'Return ds_arrange
                    End If
                    'MessageBox.Show(_par_ds_import.Tables("product_structure").Rows(i).Item("raw part"))
                    dr_list("psd_start_date") = _par_ds_import.Tables("product_structure").Rows(i).Item("start").ToString
                    dr_list("psd_end_date") = _par_ds_import.Tables("product_structure").Rows(i).Item("end").ToString
                    dr_list("psd_qty") = _par_ds_import.Tables("product_structure").Rows(i).Item("qty").ToString
                    dr_list("psd_scrp_pct") = _par_ds_import.Tables("product_structure").Rows(i).Item("scrap pct").ToString
                    ds_arrange.Tables(0).Rows.Add(dr_list)
                    i = i + 1
                End If
            Next

            ds_arrange.Tables(0).AcceptChanges()

        End If

        Return ds_arrange

    End Function

    Private Sub migrate()
        Dim i As Integer
        Dim _ps_oid As Guid
        Dim _ps_id As Integer
        Dim _ps_code As String = ""
        Dim _seq As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        _ps_id = SetInteger(func_coll.GetID("ps_mstr", le_entity.GetColumnValue("en_code"), "ps_id", "ps_en_id", le_entity.EditValue.ToString)) - 1

                        For i = 0 To dt_table.Rows.Count - 1
                            If dt_table.Rows(i).Item("ps_desc").ToString <> _ps_code Then
                                _ps_oid = Guid.NewGuid

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.ps_mstr " _
                                                    & "( " _
                                                    & "  ps_oid, " _
                                                    & "  ps_dom_id, " _
                                                    & "  ps_en_id, " _
                                                    & "  ps_add_by, " _
                                                    & "  ps_add_date, " _
                                                    & "  ps_id, " _
                                                    & "  ps_par, " _
                                                    & "  ps_desc, " _
                                                    & "  ps_use_bom, " _
                                                    & "  ps_pt_bom_id, " _
                                                    & "  ps_active, " _
                                                    & "  ps_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetringDB(_ps_oid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(le_entity.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & " current_timestamp " & ",  " _
                                                    & SetInteger(_ps_id) & ",  " _
                                                    & SetSetring(dt_table.Rows(i).Item("ps_desc")) & ",  " _
                                                    & SetSetring(dt_table.Rows(i).Item("ps_desc")) & ",  " _
                                                    & "'N',  " _
                                                    & SetInteger(dt_table.Rows(i).Item("ps_pt_bom_id")) & ",  " _
                                                    & "'Y',  " _
                                                    & "current_timestamp  " _
                                                    & ");"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                _seq = 0
                                _ps_code = dt_table.Rows(i).Item("ps_desc").ToString
                                _ps_id = _ps_id + 1
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.psd_det " _
                                                & "( " _
                                                & "  psd_oid, " _
                                                & "  psd_ps_oid, " _
                                                & "  psd_add_by, " _
                                                & "  psd_add_date, " _
                                                & "  psd_use_bom, " _
                                                & "  psd_pt_bom_id, " _
                                                & "  psd_start_date, " _
                                                & "  psd_end_date, " _
                                                & "  psd_qty, " _
                                                & "  psd_scrp_pct, " _
                                                & "  psd_seq, " _
                                                & "  psd_group, " _
                                                & "  psd_process, " _
                                                & "  psd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_ps_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & "'N',  " _
                                                & SetInteger(dt_table.Rows(i).Item("psd_pt_bom_id")) & ",  " _
                                                & SetDate(dt_table.Rows(i).Item("psd_start_date")) & ",  " _
                                                & SetDate(dt_table.Rows(i).Item("psd_end_date")) & ",  " _
                                                & SetDbl(dt_table.Rows(i).Item("psd_qty")) & ",  " _
                                                & SetDbl(dt_table.Rows(i).Item("psd_scrp_pct")) & ",  " _
                                                & _seq & ",  " _
                                                & "0,  " _
                                                & "0,  " _
                                                & "current_timestamp  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            _seq = _seq + 1
                        Next

                        sqlTran.Commit()
                        MsgBox("Import Successfull", MsgBoxStyle.Information, "Data Has Been Imported")
                        be_import_xls.Text = ""
                        ds_bef_insert.Clear()
                        gc_excel.DataSource = ds_bef_insert.Tables(0)
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim i As Integer
        Dim _ps_oid As Guid
        Dim _ps_id As Integer
        Dim _ps_code As String = ""
        Dim _seq As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        _ps_id = SetInteger(func_coll.GetID("ps_mstr", le_entity.GetColumnValue("en_code"), "ps_id", "ps_en_id", le_entity.EditValue.ToString)) - 1

                        For i = 0 To dt_table.Rows.Count - 1
                            If dt_table.Rows(i).Item("ps_desc").ToString <> _ps_code Then
                                _ps_oid = Guid.NewGuid

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.ps_mstr " _
                                                    & "( " _
                                                    & "  ps_oid, " _
                                                    & "  ps_dom_id, " _
                                                    & "  ps_en_id, " _
                                                    & "  ps_add_by, " _
                                                    & "  ps_add_date, " _
                                                    & "  ps_id, " _
                                                    & "  ps_par, " _
                                                    & "  ps_desc, " _
                                                    & "  ps_use_bom, " _
                                                    & "  ps_pt_bom_id, " _
                                                    & "  ps_active, " _
                                                    & "  ps_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetringDB(_ps_oid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(le_entity.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & " current_timestamp " & ",  " _
                                                    & SetInteger(_ps_id) & ",  " _
                                                    & SetSetring(dt_table.Rows(i).Item("ps_desc")) & ",  " _
                                                    & SetSetring(dt_table.Rows(i).Item("ps_desc")) & ",  " _
                                                    & "'N',  " _
                                                    & SetInteger(dt_table.Rows(i).Item("ps_pt_bom_id")) & ",  " _
                                                    & "'Y',  " _
                                                    & "current_timestamp  " _
                                                    & ");"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                _seq = 0
                                _ps_code = dt_table.Rows(i).Item("ps_desc").ToString
                                _ps_id = _ps_id + 1
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.psd_det " _
                                                & "( " _
                                                & "  psd_oid, " _
                                                & "  psd_ps_oid, " _
                                                & "  psd_add_by, " _
                                                & "  psd_add_date, " _
                                                & "  psd_use_bom, " _
                                                & "  psd_pt_bom_id, " _
                                                & "  psd_start_date, " _
                                                & "  psd_end_date, " _
                                                & "  psd_qty, " _
                                                & "  psd_scrp_pct, " _
                                                & "  psd_seq, " _
                                                & "  psd_group, " _
                                                & "  psd_process, " _
                                                & "  psd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_ps_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & "'N',  " _
                                                & SetInteger(dt_table.Rows(i).Item("psd_pt_bom_id")) & ",  " _
                                                & SetDate(dt_table.Rows(i).Item("psd_start_date")) & ",  " _
                                                & SetDate(dt_table.Rows(i).Item("psd_end_date")) & ",  " _
                                                & SetDbl(dt_table.Rows(i).Item("psd_qty")) & ",  " _
                                                & SetDbl(dt_table.Rows(i).Item("psd_scrp_pct")) & ",  " _
                                                & _seq & ",  " _
                                                & "0,  " _
                                                & "0,  " _
                                                & "current_timestamp  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            _seq = _seq + 1
                        Next

                        sqlTran.Commit()
                        MsgBox("Import Successfull", MsgBoxStyle.Information, "Data Has Been Imported")
                        be_import_xls.Text = ""
                        ds_bef_insert.Clear()
                        gc_excel.DataSource = ds_bef_insert.Tables(0)
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
