Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FImportWOERP
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FImportWOERP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_erp, "Code", "wo_nbr", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Wo Type", "wo_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Part Number", "wo_part", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Qty Order", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_erp, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_erp, "Remark", "wo_rmks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        If Trim(wo_number.Text) = "" Then
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

                            .SQL = "select wo_nbr, wo_type, wo_part, wo_qty_ord, wo_ord_date, wo_rel_date, wo_due_date, wo_rmks " _
                               & " from pub.wo_mstr	 " _
                               & " where wo_nbr = '" + Trim(wo_number.Text) + "'"

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
        If MessageBox.Show("Migrate Data To ERPIna..?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables("erp").Rows.Count = 0 Then
            Exit Sub
        End If

        Dim _row, _pt_id As Integer

        _row = BindingContext(ds.Tables(0)).Position
        _pt_id = func_coll.get_query_integer("select pt_id as col1 from pt_mstr where pt_code ~~* " + SetSetring(ds.Tables(0).Rows(0).Item("wo_part")))

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select wo_code from wo_mstr " + _
                           " where wo_code = " + SetSetring(ds.Tables(0).Rows(_row).Item("wo_nbr"))
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wo")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If ds_bantu.Tables("wo").Rows.Count < 1 Then
            Dim _wo_oid As Guid
            _wo_oid = Guid.NewGuid

            'insert data WO
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
                                                & "  public.wo_mstr " _
                                                & "( " _
                                                & "  wo_oid, " _
                                                & "  wo_dom_id, " _
                                                & "  wo_en_id, " _
                                                & "  wo_si_id, " _
                                                & "  wo_id, " _
                                                & "  wo_code, " _
                                                & "  wo_pt_id, " _
                                                & "  wo_qty_ord, " _
                                                & "  wo_qty_comp, " _
                                                & "  wo_qty_rjc, " _
                                                & "  wo_ord_date, " _
                                                & "  wo_due_date, " _
                                                & "  wo_yield_pct, " _
                                                & "  wo_pjc_id, " _
                                                & "  wo_ro_id, " _
                                                & "  wo_status, " _
                                                & "  wo_remarks,wo_type,wo_ref_rework, " _
                                                & "  wo_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_wo_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetIntegerDB(1) & ",  " _
                                                & SetIntegerDB(1) & ",  " _
                                                & SetInteger(func_coll.GetID("wo_mstr", "10", "wo_id", "wo_en_id", 1)) & ",  " _
                                                & SetSetring(ds.Tables(0).Rows(0).Item("wo_nbr")) & ",  " _
                                                & SetIntegerDB(_pt_id) & ",  " _
                                                & SetDbl(ds.Tables(0).Rows(0).Item("wo_qty_ord")) & ",  " _
                                                & "0,  " _
                                                & "0,  " _
                                                & SetDate(ds.Tables(0).Rows(0).Item("wo_ord_date")) & ",  " _
                                                & SetDate(ds.Tables(0).Rows(0).Item("wo_due_date")) & ",  " _
                                                & SetIntegerDB(0) & ",  " _
                                                & SetIntegerDB(0) & ",  " _
                                                & SetIntegerDB(101) & ",  " _
                                                & "'F' ,  " _
                                                & SetSetring(ds.Tables(0).Rows(0).Item("wo_rmks")) & ",  " _
                                                & SetSetring(ds.Tables(0).Rows(0).Item("wo_type")) & ",  " _
                                                & "null, " _
                                                & "current_timestamp " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()
                            MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ds.Tables(0).Clear()
                            wo_number.Text = ""
                            wo_number.Focus()
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
            MessageBox.Show("Work Order Already Available...!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class
