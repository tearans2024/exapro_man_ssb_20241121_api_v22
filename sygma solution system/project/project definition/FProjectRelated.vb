Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FProjectRelated
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _po_oid As String
    Public ds_edit, ds_edit_cust As DataSet
    Dim _now As DateTime
    Dim _conf_budget, _conf_value As String
    Public _prjr_oid_master, _prjr_name_master, _prj_oid As String
    Dim _prj_code_pre_edit As String
    Dim _prj_date_pre_edit As Date

#Region "SettingAwal"
    Private Sub FProjectRelated_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        prjr_en_id.Properties.DataSource = dt_bantu
        prjr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        prjr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        prjr_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Related Date", "prjr_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Related Name", "prjr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "prjr_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "prjr_oid", False)
        add_column(gv_detail, "prjr_en_id", False)
        add_column(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Related Date", "prjr_date", DevExpress.Utils.HorzAlignment.Center)
        
        add_column(gv_detail, "prjr_prj_oid", False)
        add_column(gv_detail, "prjr_prjd_oid", False)
        add_column(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "prjr_prjc_oid", False)
        add_column_copy(gv_detail, "Customer Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "prjr_oid", False)
        add_column(gv_edit, "prjr_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Related Date", "prjr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit, "prjr_prj_oid", False)
        add_column(gv_edit, "prjr_prjd_oid", False)
        add_column_browse(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_loc_id", True)
        add_column_edit(gv_edit, "Location", "loc_desc_prjd", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_price", True)
        add_column(gv_edit, "prjr_prjc_oid", False)
        add_column_browse(gv_edit, "Customer Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjc_loc_id", True)
        add_column_edit(gv_edit, "Location", "loc_desc_prjc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjc_price", True)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & " prjr_en_id,prjr_prj_oid,prjr_date,prjr_name,prjr_remarks, " _
                    & " prj_code " _
                    & "FROM  " _
                    & " public.prjr_relate " _
                    & " inner join prj_mstr on prj_oid = prjr_prj_oid " _
                    & " where prjr_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and prjr_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and prjr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & " group by " _
                    & " prjr_en_id,prjr_prj_oid,prjr_date,prjr_name,prjr_remarks, " _
                    & " prj_code "
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
                & "  prjr_oid, " _
                & "  prjr_dom_id, " _
                & "  prjr_en_id,en_desc, " _
                & "  prjr_add_by, " _
                & "  prjr_add_date, " _
                & "  prjr_upd_by, " _
                & "  prjr_upd_date, " _
                & "  prjr_dt, " _
                & "  prjr_date, " _
                & "  prjr_name, " _
                & "  prjr_remarks, " _
                & "  prjr_prj_oid,prj_code, " _
                & "  prjr_prjd_oid,prjd_pt_id,pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
                & "  prjr_prjc_oid,prjc_cp_id,cp_code,prjc_pt_desc1,prjc_pt_desc2 " _
                & "FROM  " _
                & "  public.prjr_relate " _
                & "  inner join en_mstr on en_id = prjr_en_id " _
                & "  inner join prj_mstr on prj_oid = prjr_prj_oid " _
                & "  inner join prjd_det on prjd_oid = prjr_prjd_oid " _
                & "  inner join pt_mstr dt on dt.pt_id = prjd_pt_id " _
                & "  inner join prjc_cust on prjc_oid = prjr_prjc_oid  " _
                & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                & " where prjr_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and prjr_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and prjr_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("prjr_prj_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjr_prj_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjr_prj_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        prjr_en_id.Focus()
        prjr_en_id.ItemIndex = 0
        prjr_date.EditValue = Today()
        prjr_prj_oid.Text = ""
        prjr_name.Text = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
            'tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  prjr_oid, " _
                        & "  prjr_dom_id, " _
                        & "  prjr_en_id,en_desc, " _
                        & "  prjr_add_by, " _
                        & "  prjr_add_date, " _
                        & "  prjr_upd_by, " _
                        & "  prjr_upd_date, " _
                        & "  prjr_dt, " _
                        & "  prjr_date, " _
                        & "  prjr_name, " _
                        & "  prjr_remarks, " _
                        & "  prjr_prj_oid,prj_code, " _
                        & "  prjd_loc_id, " _
                        & "  loc_prjd.loc_desc as loc_desc_prjd, " _
                        & "  prjc_loc_id, " _
                        & "  loc_prjc.loc_desc as loc_desc_prjc, " _
                        & "  prjr_prjd_oid,prjd_pt_id,pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
                        & "  prjr_prjc_oid,prjc_cp_id,cp_code,prjc_pt_desc1,prjc_pt_desc2, " _
                        & "  prjd_price, " _
                        & "  prjc_price " _
                        & "FROM  " _
                        & "  public.prjr_relate " _
                        & "  inner join en_mstr on en_id = prjr_en_id " _
                        & "  inner join prj_mstr on prj_oid = prjr_prj_oid " _
                        & "  inner join prjd_det on prjd_oid = prjr_prjd_oid " _
                        & "  inner join loc_mstr loc_prjd on loc_prjd.loc_id = prjd_loc_id  " _
                        & "  inner join pt_mstr dt on dt.pt_id = prjd_pt_id " _
                        & "  inner join prjc_cust on prjc_oid = prjr_prjc_oid  " _
                        & "  inner join loc_mstr loc_prjc on loc_prjc.loc_id = prjc_loc_id  " _
                        & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                        & "  where prjr_name = 'GTR-GR$' "
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function delete_data() As Boolean
        row = BindingContext(ds.Tables(0)).Position
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position
            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
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
                            .Command.CommandText = "delete from prjr_relate where prjr_prj_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjr_prj_oid") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        Return delete_data
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position
            With ds.Tables(0).Rows(row)
                prjr_en_id.EditValue = .Item("prjr_en_id")
                prjr_date.EditValue = .Item("prjr_date")
                prjr_name.Text = SetString(.Item("prjr_name"))
                prjr_remarks.Text = SetString(.Item("prjr_remarks"))
                prjr_prj_oid.Text = .Item("prj_code")
                _prj_oid = .Item("prjr_prj_oid")
            End With
            prjr_en_id.Focus()
            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjr_oid, " _
                            & "  prjr_dom_id, " _
                            & "  prjr_en_id,en_desc, " _
                            & "  prjr_add_by, " _
                            & "  prjr_add_date, " _
                            & "  prjr_upd_by, " _
                            & "  prjr_upd_date, " _
                            & "  prjr_dt, " _
                            & "  prjr_date, " _
                            & "  prjr_name, " _
                            & "  prjr_remarks, " _
                            & "  prjr_prj_oid,prj_code, " _
                            & "  prjr_prjd_oid,prjd_pt_id,pt_code,prjd_pt_desc1,prjd_pt_desc2, " _
                            & "  prjr_prjc_oid,prjc_cp_id,cp_code,prjc_pt_desc1,prjc_pt_desc2 " _
                            & "FROM  " _
                            & "  public.prjr_relate " _
                            & "  inner join en_mstr on en_id = prjr_en_id " _
                            & "  inner join prj_mstr on prj_oid = prjr_prj_oid " _
                            & "  inner join prjd_det on prjd_oid = prjr_prjd_oid " _
                            & "  inner join pt_mstr dt on dt.pt_id = prjd_pt_id " _
                            & "  inner join prjc_cust on prjc_oid = prjr_prjc_oid  " _
                            & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                            & "  where prjr_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjr_prj_oid").ToString()) _
                            & "  order by prjd_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Dim ssqls As New ArrayList
        Dim _prjr_oid As Guid
        _prjr_oid = Guid.NewGuid

        Dim ds_bantu As New DataSet
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "DELETE FROM prjr_relate   " _
                        '                    & " WHERE " _
                        '                    & "  prjr_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjr_prj_oid").ToString()) & "  " _
                        '                    & ";"
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For Each _dr As DataRow In ds_edit.Tables(0).Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.prjr_relate   " _
                                                & "SET  " _
                                                & "  prjr_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                                & "  prjr_en_id = " & SetInteger(_dr("prjr_en_id")) & ",  " _
                                                & "  prjr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  prjr_upd_date = current_timestamp, " _
                                                & "  prjr_dt = current_timestamp, " _
                                                & "  prjr_date = " & SetDate(_dr("prjr_date")) & ",  " _
                                                & "  prjr_name = " & SetSetring(prjr_name.Text) & ",  " _
                                                & "  prjr_remarks = " & SetSetring(prjr_remarks.Text) & ",  " _
                                                & "  prjr_prj_oid = " & SetSetring(_prj_oid.ToString()) & ",  " _
                                                & "  prjr_prjd_oid = " & SetSetring(_dr("prjr_prjd_oid")) & ",  " _
                                                & "  prjr_prjc_oid = " & SetSetring(_dr("prjr_prjc_oid")) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  prjr_oid = " & SetSetring(_dr("prjr_oid").ToString()) & "  " _
                                                & ";"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        'set_row(_prj_oid_master.ToString(), "prj_oid")
                        edit = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Function GetDataPrjd(ByVal par_prj_oid As String) As DataSet
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select prjd_loc_id,prjd_qty,prjd_cost,prjd_price " _
                         & " from prjd_det " _
                         & " where prjd_prj_oid = " + SetSetring(par_prj_oid.ToString())
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "prjd")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return ds_bantu
    End Function

    Public Function GetDataPrjc(ByVal par_prj_oid As String) As DataSet
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select prjc_loc_id,prjc_qty,prjc_cost,prjc_price " _
                         & " from prjc_cust " _
                         & " where prjc_prj_oid = " + SetSetring(par_prj_oid.ToString())
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "prjc")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return ds_bantu
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        Dim i As Integer

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("prjr_prjd_oid")) = True Then
                MessageBox.Show("Part Number Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("prjr_prjc_oid")) = True Then
                MessageBox.Show("Customer Part Number Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next

        ''rev by hendrik 2011-02-27
        'Dim _ds_prjd As New DataSet
        'Dim _ds_prjc As New DataSet

        '_ds_prjd = GetDataPrjd(_prj_oid.ToString())
        '_ds_prjc = GetDataPrjc(_prj_oid.ToString())

        ''Cek location
        'For Each _dr_prjd As DataRow In _ds_prjd.Tables(0).Rows
        '    For Each _dr_prjc As DataRow In _ds_prjc.Tables(0).Rows
        '        MessageBox.Show(_dr_prjd("prjd_loc_id").ToString, "")
        '        MessageBox.Show(_dr_prjc("prjc_loc_id").ToString, "")
        '        If _dr_prjd("prjd_loc_id") <> _dr_prjc("prjc_loc_id") Then
        '            MessageBox.Show("Terdapat location yg berbeda..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Return False
        '        End If
        '    Next
        'Next

        'cek cost
        'Dim _total_prjd_cost, _total_prjc_cost As Double
        'Dim _prjd_price, _prjc_price As Double
        'For Each _dr_prjd As DataRow In _ds_prjd.Tables(0).Rows
        '    _total_prjd_cost = _total_prjd_cost + (_dr_prjd("prjd_qty") * _dr_prjd("prjd_cost"))
        '    _prjd_price = _prjd_price + _dr_prjd("prjd_price")
        'Next
        'For Each _dr_prjc As DataRow In _ds_prjc.Tables(0).Rows
        '    _total_prjc_cost = _total_prjc_cost + (_dr_prjc("prjc_qty") * _dr_prjc("prjc_cost"))
        '    _prjc_price = _prjc_price + _dr_prjc("prjc_price")
        'Next
        'If _total_prjd_cost <> _total_prjc_cost Then
        '    MessageBox.Show("Total Cost pada project tidak sama..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If
        'If _prjd_price <> _prjc_price Then
        '    MessageBox.Show("Total Price pada project tidak sama..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For Each _dr As DataRow In ds_edit.Tables(0).Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.prjr_relate " _
                                                & "( " _
                                                & "  prjr_oid, " _
                                                & "  prjr_dom_id, " _
                                                & "  prjr_en_id, " _
                                                & "  prjr_add_by, " _
                                                & "  prjr_add_date, " _
                                                & "  prjr_dt, " _
                                                & "  prjr_date, " _
                                                & "  prjr_name, " _
                                                & "  prjr_remarks, " _
                                                & "  prjr_prj_oid, " _
                                                & "  prjr_prjd_oid, " _
                                                & "  prjr_prjc_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(prjr_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ", " _
                                                & " current_timestamp " & ", " _
                                                & " current_timestamp " & ", " _
                                                & SetDate(prjr_date.DateTime.Date) & ",  " _
                                                & SetSetring(prjr_name.Text) & ",  " _
                                                & SetSetring(prjr_remarks.Text) & ",  " _
                                                & SetSetring(_prj_oid.ToString()) & ",  " _
                                                & SetSetring(_dr("prjr_prjd_oid")) & ",  " _
                                                & SetSetring(_dr("prjr_prjc_oid")) & "  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        after_success()
                        'set_row(_prj_code.ToString, "prj_code")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

#End Region

#Region "gv_edit"

    'Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
    '    Dim _rcvd_um_conv As Double = 1
    '    Dim _rcvd_qty As Double = 1
    '    Dim _rcvd_qty_real As Double

    '    If e.Column.Name = "rcvd_qty" Then
    '        Try
    '            _rcvd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "rcvd_um_conv"))
    '        Catch ex As Exception
    '        End Try

    '        _rcvd_qty_real = e.Value * _rcvd_um_conv
    '        gv_edit.SetRowCellValue(e.RowHandle, "rcvd_qty_real", _rcvd_qty_real)
    '    ElseIf e.Column.Name = "rcvd_um_conv" Then
    '        Try
    '            _rcvd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "rcvd_qty")))
    '        Catch ex As Exception
    '        End Try

    '        _rcvd_qty_real = e.Value * _rcvd_qty
    '        gv_edit.SetRowCellValue(e.RowHandle, "rcvd_qty_real", _rcvd_qty_real)
    '    End If
    'End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _prjr_en_id As Integer = gv_edit.GetRowCellValue(_row, "prjr_en_id")

        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            Dim frm As New FPtProjectDetailSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = prjr_en_id.EditValue
            frm._column_name = _col
            frm._prj_oid = _prj_oid
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "cp_code" Then
            Dim frm As New FPtProjectDetailSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = prjr_en_id.EditValue
            frm._column_name = _col
            frm._prj_oid = _prj_oid
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

#End Region

    Public Function get_tanggal_sistem() As Date
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select current_date as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_tanggal_sistem = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_tanggal_sistem
    End Function

    Private Sub gv_edit_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "prjr_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "prjr_en_id", prjr_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", prjr_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "prjr_date", prjr_date.DateTime.Date)
            .SetRowCellValue(e.RowHandle, "prjr_name", "")
            .SetRowCellValue(e.RowHandle, "prjr_remarks", "")
            .SetRowCellValue(e.RowHandle, "prjr_prj_oid", _prj_oid)
            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        'Dim _prjd_qty, _prjd_qty_real, _prjd_um_conv, _prjd_qty_cost, _prjd_cost, _prjd_disc, _prjd_qty_receive As Double
        '_prjd_um_conv = 1
        '_prjd_qty = 1
        '_prjd_qty_cost = 0
        '_prjd_disc = 0

        'If e.Column.Name = "prjd_qty" Then
        '    Try
        '        _prjd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "prjd_um_conv"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _prjd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "prjd_cost"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _prjd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "prjd_disc"))
        '    Catch ex As Exception
        '    End Try

        '    _prjd_qty_real = e.Value * _prjd_um_conv
        '    _prjd_qty_cost = (e.Value * _prjd_cost) - (e.Value * _prjd_cost * _prjd_disc)

        '    gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_real", _prjd_qty_real)
        '    gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_cost", _prjd_qty_cost)
        'ElseIf e.Column.Name = "prjd_cost" Then
        '    Try
        '        _prjd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_qty")))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _prjd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_disc")))
        '    Catch ex As Exception
        '    End Try

        '    _prjd_qty_cost = (e.Value * _prjd_qty) - (e.Value * _prjd_qty * _prjd_disc)
        '    gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_cost", _prjd_qty_cost)
        'ElseIf e.Column.Name = "prjd_disc" Then
        '    Try
        '        _prjd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_qty")))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _prjd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_cost")))
        '    Catch ex As Exception
        '    End Try

        '    _prjd_qty_cost = (_prjd_cost * _prjd_qty) - (_prjd_cost * _prjd_qty * e.Value)
        '    gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_cost", _prjd_qty_cost)
        'ElseIf e.Column.Name = "prjd_um_conv" Then
        '    Try
        '        _prjd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_qty")))
        '    Catch ex As Exception
        '    End Try

        '    _prjd_qty_real = e.Value * _prjd_qty

        '    gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_real", _prjd_qty_real)
        'ElseIf e.Column.Name = "prjd_taxable" Then
        '    If gv_edit.GetRowCellValue(e.RowHandle, "prjd_taxable").ToString.ToUpper = "N" Then
        '        gv_edit.SetRowCellValue(e.RowHandle, "prjd_tax_inc", "N")
        '        gv_edit.SetRowCellValue(e.RowHandle, "prjd_tax_class", func_coll.get_id_tax_class("NON-TAX"))
        '        gv_edit.SetRowCellValue(e.RowHandle, "tax_class", "NON-TAX")
        '    End If
        'ElseIf e.Column.Name = "prjd_tax_inc" Then
        '    If gv_edit.GetRowCellValue(e.RowHandle, "prjd_tax_inc").ToString.ToUpper = "Y" And gv_edit.GetRowCellValue(e.RowHandle, "prjd_taxable").ToString.ToUpper = "N" Then
        '        gv_edit.SetRowCellValue(e.RowHandle, "prjd_tax_inc", "N")
        '    End If
        'End If
    End Sub

    Private Sub prjr_prj_oid_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles prjr_prj_oid.ButtonClick
        Dim frm As New FProjectSearch
        frm.set_win(Me)
        frm.type_form = True
        frm._en_id = prjr_en_id.EditValue
        frm.ShowDialog()
    End Sub

End Class
