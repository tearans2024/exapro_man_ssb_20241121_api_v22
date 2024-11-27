Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FInventoryImport

    Dim _pt_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_import As DataSet
    Dim _now As DateTime

    Private Sub FInventoryImport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
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
        add_column_copy(gv_inventory, "Part Code", "part_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inventory, "Site", "site", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inventory, "Project", "project", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inventory, "Location", "location", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inventory, "QTY", "qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_inventory, "Cost", "cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_inventory, "Serial", "serial", DevExpress.Utils.HorzAlignment.Default)
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

        gc_inventory.DataSource = ds_import.Tables(0)
        'gv_inventory.Columns("part_code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        gv_inventory.BestFitColumns()
    End Sub

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim i As Integer
        Dim _imp_success As Boolean = True

        If ds_import.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds_import.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        If MessageBox.Show("Import Data To Syspro..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _tran_id As Integer
        _tran_id = func_coll.get_id_tran_mstr("cyc-cnt")
        If _tran_id = -1 Then
            MessageBox.Show("Inventory Begining Balance In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If


        Dim guid_value As Guid

        gv_inventory.ClearSorting()
        gv_inventory.ClearColumnsFilter()
        gv_inventory.ClearGrouping()
        gv_inventory.Columns("part_code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        Dim _pt_id, _site_id, _location_id, _pjc_id As String

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        For i = 0 To gv_inventory.RowCount - 1
                            guid_value = Guid.NewGuid

                            'If i = 133 Then
                            '    MsgBox("")
                            'End If

                            _pt_id = ""
                            Try
                                Using objcek As New master_new.WDABasepgsql("", "")
                                    With objcek
                                        .Connection.Open()
                                        .Command = .Connection.CreateCommand
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "SELECT pt_id " _
                                                             & "  FROM " _
                                                             & "  pt_mstr  " _
                                                             & "  WHERE upper(pt_code) = " & SetSetring(UCase(gv_inventory.GetRowCellValue(i, "part_code"))) & ""
                                        .InitializeCommand()
                                        .DataReader = .Command.ExecuteReader
                                        While .DataReader.Read
                                            _pt_id = .DataReader("pt_id")
                                        End While
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                                Exit Sub
                            End Try

                            If _pt_id = "" Then
                                MsgBox("Unknown Part on Row " & (i + 1).ToString, MsgBoxStyle.Critical, "Part Unknown")
                                Exit Sub
                            End If

                            _site_id = ""
                            Try
                                Using objcek As New master_new.WDABasepgsql("", "")
                                    With objcek
                                        .Connection.Open()
                                        .Command = .Connection.CreateCommand
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "SELECT si_id " _
                                                                 & "  FROM " _
                                                                 & "  si_mstr  " _
                                                                 & "  WHERE upper(si_desc) = " & SetSetring(UCase(gv_inventory.GetRowCellValue(i, "site"))) & ""
                                        .InitializeCommand()
                                        .DataReader = .Command.ExecuteReader
                                        While .DataReader.Read
                                            _site_id = .DataReader("si_id")
                                        End While
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                                Exit Sub
                            End Try

                            If _site_id = "" Then
                                MsgBox("Unknown Site on Row " & (i + 1).ToString, MsgBoxStyle.Critical, "Unknown Site")
                                Exit Sub
                            End If

                            _pjc_id = ""
                            Try
                                Using objcek As New master_new.WDABasepgsql("", "")
                                    With objcek
                                        .Connection.Open()
                                        .Command = .Connection.CreateCommand
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "SELECT pjc_id " _
                                                                 & "  FROM " _
                                                                 & "  pjc_mstr  " _
                                                                 & "  WHERE upper(pjc_code) = " & SetSetring(UCase(gv_inventory.GetRowCellValue(i, "project"))) & ""
                                        .InitializeCommand()
                                        .DataReader = .Command.ExecuteReader
                                        While .DataReader.Read
                                            _pjc_id = .DataReader("pjc_id")
                                        End While
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                                Exit Sub
                            End Try

                            If _pjc_id = "" Then
                                MsgBox("Unknown Project on Row " & (i + 1).ToString, MsgBoxStyle.Critical, "Unknown Project")
                                Exit Sub
                            End If

                            _location_id = ""
                            Try
                                Using objcek As New master_new.WDABasepgsql("", "")
                                    With objcek
                                        .Connection.Open()
                                        .Command = .Connection.CreateCommand
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "SELECT loc_id " _
                                                                 & "  FROM " _
                                                                 & "  loc_mstr  " _
                                                                 & "  WHERE upper(loc_desc) = " & SetSetring(UCase(gv_inventory.GetRowCellValue(i, "location"))) & ""
                                        .InitializeCommand()
                                        .DataReader = .Command.ExecuteReader
                                        While .DataReader.Read
                                            _location_id = .DataReader("loc_id")
                                        End While
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                                Exit Sub
                            End Try

                            If _location_id = "" Then
                                MsgBox("Unknown Location on Row " & (i + 1).ToString, MsgBoxStyle.Critical, "Unknown Location")
                                Exit Sub
                            End If

                            update_invc_mstr(objinsert, import_en_id.EditValue, _site_id, _pjc_id, _location_id, _pt_id, IIf(IsDBNull(gv_inventory.GetRowCellValue(i, "serial")) = True, "''", gv_inventory.GetRowCellValue(i, "serial").ToString), gv_inventory.GetRowCellValue(i, "qty"))
                            update_invh_mstr(objinsert, _tran_id, 1, import_en_id.EditValue, "", "", "Cycle Count Initial", "I", _site_id, _pjc_id, _location_id, _pt_id, gv_inventory.GetRowCellValue(i, "qty"), gv_inventory.GetRowCellValue(i, "cost"), gv_inventory.GetRowCellValue(i, "cost"), IIf(IsDBNull(gv_inventory.GetRowCellValue(i, "serial")) = True, "''", gv_inventory.GetRowCellValue(i, "serial").ToString), _now)

                        Next
                        sqlTran.Commit()

                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            MessageBox.Show(ex.Message)
            _imp_success = False
        End Try

        If _imp_success = True Then
            MsgBox("Import success....")
            be_import_xls.Text = ""
            ds_import.Tables(0).Clear()
        End If

        import_en_id.Enabled = True
        

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

    Public Sub update_invc_mstr(ByVal par_obj As Object, ByVal par_en_id As Integer, ByVal par_si_id As Integer, ByVal par_pjc_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, ByVal par_serial As String, ByVal par_qty As Double)

        Dim _invc_oid As String = ""

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select invc_oid from invc_mstr " + _
                                           " where invc_dom_id = " + master_new.ClsVar.sdom_id + _
                                           " and invc_en_id = " + par_en_id.ToString + _
                                           " and invc_si_id = " + par_si_id.ToString + _
                                           " and invc_pjc_id = " + par_si_id.ToString + _
                                           " and invc_loc_id = " + par_loc_id.ToString + _
                                           " and invc_pt_id = " + par_pt_id.ToString + _
                                           " and coalesce(invc_serial,'') = " + IIf(par_serial = "''", "''", SetSetring(par_serial))
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _invc_oid = .DataReader("invc_oid").ToString
                        End While
                    Else
                        _invc_oid = ""
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _invc_oid = "" Then
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invc_mstr " _
                                        & "( " _
                                        & "  invc_oid, " _
                                        & "  invc_dom_id, " _
                                        & "  invc_en_id, " _
                                        & "  invc_si_id, " _
                                        & "  invc_pjc_id, " _
                                        & "  invc_loc_id, " _
                                        & "  invc_pt_id, " _
                                        & "  invc_qty, " _
                                        & "  invc_serial " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(par_en_id) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_pjc_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetSetring(IIf(par_serial = "''", "", par_serial)) & "  " _
                                        & ")"
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

            End With

        Else
            With par_obj
                Try
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "UPDATE  " _
                                        & "  public.invc_mstr   " _
                                        & "SET  " _
                                        & "  invc_qty = " & SetDbl(par_qty) _
                                        & "WHERE  " _
                                        & "  invc_oid = " & SetSetring(_invc_oid) & " "

                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End With

        End If

    End Sub

    Public Sub update_invh_mstr(ByVal par_obj As Object, ByVal par_tran_id As Integer, ByVal par_seq As Integer, ByVal par_en_id As Integer, _
                                      ByVal par_trn_code As String, ByVal par_trn_oid As String, ByVal par_desc As String, ByVal par_opn_type As String, _
                                      ByVal par_si_id As Integer, ByVal par_pjc_id As Integer, ByVal par_loc_id As Integer, ByVal par_pt_id As Integer, _
                                      ByVal par_qty As Double, ByVal par_cost As Double, ByVal par_avg_cost As Double, ByVal par_serial As String, ByVal par_date As Date)
        'Insert History Inventory

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                        & "  public.invh_mstr " _
                                        & "( " _
                                        & "  invh_oid, " _
                                        & "  invh_tran_id, " _
                                        & "  invh_seq, " _
                                        & "  invh_dom_id, " _
                                        & "  invh_en_id, " _
                                        & "  invh_trn_code, " _
                                        & "  invh_trn_oid, " _
                                        & "  invh_date, " _
                                        & "  invh_desc, " _
                                        & "  invh_opn_type, " _
                                        & "  invh_si_id, " _
                                        & "  invh_pjc_id, " _
                                        & "  invh_loc_id, " _
                                        & "  invh_pt_id, " _
                                        & "  invh_qty, " _
                                        & "  invh_cost, " _
                                        & "  invh_avg_cost, " _
                                        & "  invh_serial " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(par_tran_id) & ",  " _
                                        & SetInteger(par_seq) & ",  " _
                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetSetring(par_en_id) & ",  " _
                                        & SetSetring(par_trn_code) & ",  " _
                                        & SetSetring(par_trn_oid) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring(par_desc) & ",  " _
                                        & SetSetring(par_opn_type) & ",  " _
                                        & SetInteger(par_si_id) & ",  " _
                                        & SetInteger(par_pjc_id) & ",  " _
                                        & SetInteger(par_loc_id) & ",  " _
                                        & SetInteger(par_pt_id) & ",  " _
                                        & SetDbl(par_qty) & ",  " _
                                        & SetDbl(par_cost) & ",  " _
                                        & SetDbl(par_avg_cost) & ",  " _
                                        & SetSetring(par_serial) & "  " _
                                        & ")"

                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End With
    End Sub
    
End Class


