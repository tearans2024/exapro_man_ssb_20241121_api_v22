Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Public Class FWorkCenterImport
    Dim _loc_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_import As DataSet

    Private Sub FLocationSiteImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        form_first_load()
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
        add_column_copy(gv_master, "Entity", "entity", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center Code", "wc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center Machine", "wc_machine", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Department", "wc_dept", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Mach/Op", "wc_mch_op", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Setup Crew", "wc_setup_crew", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Run Crew", "wc_run_crew", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Mach Burden Rate", "wc_mch_bdn", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Setup Rate", "wc_setup_rate", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Labor Rate", "wc_labor_rate", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "wc_active", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "wc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wc_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "wc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wc_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  wc_oid, " _
                    & "  wc_dom_id, " _
                    & "  wc_en_id, " _
                    & "  en_desc as entity, " _
                    & "  wc_add_by, " _
                    & "  wc_add_date, " _
                    & "  wc_upd_by, " _
                    & "  wc_upd_date, " _
                    & "  wc_id, " _
                    & "  wc_code, " _
                    & "  wc_machine, " _
                    & "  wc_desc, " _
                    & "  wc_dpt_id, " _
                    & "  dpt_desc as wc_dept, " _
                    & "  wc_queue, " _
                    & "  wc_wait, " _
                    & "  wc_mch_op, " _
                    & "  wc_setup_men as wc_setup_crew, " _
                    & "  wc_men_mch as wc_run_crew, " _
                    & "  wc_mch_wkctr, " _
                    & "  wc_mch_bdn_rate as wc_mch_bdn, " _
                    & "  wc_setup_rate, " _
                    & "  wc_lbr_rate as wc_labor_rate, " _
                    & "  wc_bdn_rate, " _
                    & "  wc_bdn_pct, " _
                    & "  wc_active, " _
                    & "  wc_dt " _
                    & "FROM  " _
                    & "  public.wc_mstr " _
                    & "  inner join en_mstr on en_id=wc_en_id " _
                    & "  inner join dpt_mstr on dpt_id=wc_dpt_id " _
                    & " where wc_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim i, _id, _id_wc As Integer
        Dim _code As String = "_x_"


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

        gv_master.ClearSorting()
        gv_master.ClearColumnsFilter()
        gv_master.ClearGrouping()
        gv_master.Columns("wc_code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        _id_wc = func_coll.GetID("wc_mstr", import_en_id.GetColumnValue("en_code"), "wc_id", "wc_en_id", import_en_id.EditValue.ToString)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To gv_master.RowCount - 1

                            guid_value = Guid.NewGuid
                            'Departement
                            Dim ds_dept As New DataSet
                            Dim _dpt_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  dpt_mstr  " _
                                             & "  WHERE dpt_code ~~* " & SetSetring(gv_master.GetRowCellValue(i, "wc_dept")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_dept, "dept_id")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_dept.Tables("dept_id").Rows.Count > 0 Then
                                _dpt_id = ds_dept.Tables("dept_id").Rows(0).Item("dpt_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Departemen Name Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Code
                            If Trim(gv_master.GetRowCellValue(i, "wc_code")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Code Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Description
                            If Trim(gv_master.GetRowCellValue(i, "wc_desc")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Descrption Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Machine
                            If Trim(gv_master.GetRowCellValue(i, "wc_machine")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Machine Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Departement
                            If Trim(gv_master.GetRowCellValue(i, "wc_dept")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Departement Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Machine Operator
                            If Trim(gv_master.GetRowCellValue(i, "wc_mch_op")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Machine/Operator Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If


                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wc_mstr " _
                                                & "( " _
                                                & "  wc_oid, " _
                                                & "  wc_dom_id, " _
                                                & "  wc_en_id, " _
                                                & "  wc_add_by, " _
                                                & "  wc_add_date, " _
                                                & "  wc_id, " _
                                                & "  wc_code, " _
                                                & "  wc_machine, " _
                                                & "  wc_desc, " _
                                                & "  wc_dpt_id, " _
                                                & "  wc_mch_op, " _
                                                & "  wc_setup_men, " _
                                                & "  wc_men_mch, " _
                                                & "  wc_mch_bdn_rate, " _
                                                & "  wc_setup_rate, " _
                                                & "  wc_lbr_rate, " _
                                                & "  wc_active, " _
                                                & "  wc_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(import_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(_id_wc) & ",  " _
                                                & SetSetringDB(gv_master.GetRowCellValue(i, "wc_code")) & ",  " _
                                                & SetSetringDB(gv_master.GetRowCellValue(i, "wc_machine")) & ",  " _
                                                & SetSetringDB(gv_master.GetRowCellValue(i, "wc_desc")) & ",  " _
                                                & SetInteger(_dpt_id) & ",  " _
                                                & SetSetringDB(gv_master.GetRowCellValue(i, "wc_mch_op")) & ",  " _
                                                & SetSetringDB(gv_master.GetRowCellValue(i, "wc_setup_crew")) & ",  " _
                                                & SetSetringDB(gv_master.GetRowCellValue(i, "wc_run_crew")) & ",  " _
                                                & SetDbl(gv_master.GetRowCellValue(i, "wc_mch_bdn")) & ",  " _
                                                & SetDbl(gv_master.GetRowCellValue(i, "wc_setup_rate")) & ",  " _
                                                & SetDbl(gv_master.GetRowCellValue(i, "wc_labor_rate")) & ",  " _
                                                & SetSetring("Y") & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"



                            _id_wc = _id_wc + 1
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
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

        gc_location.DataSource = ds_import.Tables(0)
        gv_master.Columns("wc_code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        gv_master.BestFitColumns()
    End Sub

    Private Sub be_import_xls_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_import_xls.ButtonClick
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
