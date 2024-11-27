Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FProjectImport

    Dim _pt_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_import As DataSet

    Private Sub FPartNumberImport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        add_column_copy(gv_project, "Code", "code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_project, "Description", "desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_project, "Date Project", "project_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_project, "Is Budget", "is_project", DevExpress.Utils.HorzAlignment.Default)
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
        gv_project.Columns("code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        gv_project.BestFitColumns()
    End Sub

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim i, _id, _id_pjc As Integer
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

        gv_project.ClearSorting()
        gv_project.ClearColumnsFilter()
        gv_project.ClearGrouping()
        gv_project.Columns("code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        _id_pjc = func_coll.GetID("pjc_mstr", import_en_id.GetColumnValue("en_code"), "pjc_id", "pjc_en_id", import_en_id.EditValue.ToString)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To gv_project.RowCount - 1
                            guid_value = Guid.NewGuid

                            If Trim(gv_project.GetRowCellValue(i, "code").ToString) = "" Then
                                MessageBox.Show("Project Code Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                            If Trim(gv_project.GetRowCellValue(i, "desc").ToString) = "" Then
                                MessageBox.Show("Project Description Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If

                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.pjc_mstr " _
                                            & "( " _
                                            & "  pjc_oid, " _
                                            & "  pjc_dom_id, " _
                                            & "  pjc_en_id, " _
                                            & "  pjc_add_by, " _
                                            & "  pjc_add_date, " _
                                            & "  pjc_id, " _
                                            & "  pjc_code, " _
                                            & "  pjc_date, " _
                                            & "  pjc_desc, " _
                                            & "  pjc_is_budget, " _
                                            & "  pjc_active, " _
                                            & "  pjc_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(import_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(_id_pjc) & ",  " _
                                            & SetSetringDB(gv_project.GetRowCellValue(i, "code")) & ",  " _
                                            & SetDate(gv_project.GetRowCellValue(i, "project_date")) & ",  " _
                                            & SetSetringDB(gv_project.GetRowCellValue(i, "desc")) & ",  " _
                                            & SetSetring(gv_project.GetRowCellValue(i, "is_project")) & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ")"




                            _id_pjc = _id_pjc + 1
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


