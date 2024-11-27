Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FProjectGroup
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _mo_id, _prjg_code, _mo_oid As String
    Public ds_edit, ds_serial As DataSet
    Dim _now As DateTime
    Dim _conf_budget As String
    Dim _conf_value As String
    Public _ptnr_id As Integer
    Dim mf As New master_new.ModFunction
    Public _pao_oid As String
    Dim _from_edit As Boolean
    Dim _mo_tran_id_pre_edit As Integer
    Dim _prjg_oid_master As String
    Public _prj_oid As String

#Region "SettingAwal"
    Private Sub FProjectGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _from_edit = False
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        prjg_en_id.Properties.DataSource = dt_bantu
        prjg_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        prjg_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        prjg_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_layout_name())
        prjg_tran_id.Properties.DataSource = dt_bantu
        prjg_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        prjg_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        prjg_tran_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "prjg_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Code", "prjg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "prjg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "prjg_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Add", "prjg_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Add", "prjg_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "prjg_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "prjg_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "prjgd_oid", False)
        add_column(gv_detail, "prjgd_prjg_oid", False)
        add_column(gv_detail, "prjgd_prjc_oid", False)
        add_column_copy(gv_detail, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "prjgd_oid", False)
        add_column(gv_edit, "prjgd_prjg_oid", False)
        add_column(gv_edit, "prjgd_prjc_oid", False)
        'add_column_browse(gv_edit, "Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description 1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description 2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_layout, "prjl_prjg_oid", False)
        add_column_copy(gv_layout, "Seq.", "prjl_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_layout, "Project Code", "prjg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_layout, "Description ID", "desc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_layout, "Data Type", "prjl_data_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_layout, "IsNull", "prjl_isnull", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_layout, "IsRoot", "prjl_is_root", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_layout, "Parent ID", "desc_parent_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_layout, "Tran Column", "tranc_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  prjg_oid, " _
                    & "  prjg_dom_id, " _
                    & "  prjg_en_id,en_desc, " _
                    & "  prjg_add_by, " _
                    & "  prjg_add_date, " _
                    & "  prjg_upd_by, " _
                    & "  prjg_upd_date, " _
                    & "  prjg_dt, " _
                    & "  prjg_code, " _
                    & "  prjg_prj_oid,prj_code, " _
                    & "  prjg_remarks, " _
                    & "  prjg_date, " _
                    & "  prjg_name, " _
                    & "  prjg_tran_id,tran_name " _
                    & "FROM  " _
                    & "  public.prjg_group " _
                    & "  inner join en_mstr on en_id = prjg_en_id " _
                    & "  inner join prj_mstr on prj_oid = prjg_prj_oid " _
                    & "  inner join tran_mstr on tran_id = prjg_tran_id " _
                    & " where prjg_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and prjg_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and prjg_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & " order by prjg_date,prjg_code asc "
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
            & "  prjgd_oid, " _
            & "  prjgd_dom_id, " _
            & "  prjgd_en_id, " _
            & "  prjgd_add_by, " _
            & "  prjgd_add_date, " _
            & "  prjgd_upd_by, " _
            & "  prjgd_upd_date, " _
            & "  prjgd_dt, " _
            & "  prjgd_prjg_oid, " _
            & "  prjgd_prjc_oid, " _
            & "  cp_code,prjc_pt_desc1,prjc_pt_desc2, " _
            & "  loc_desc,si_desc,prj_code " _
            & "FROM  " _
            & "  public.prjgd_det " _
            & "  inner join prjg_group on prjg_oid = prjgd_prjg_oid  " _
            & "  inner join prjc_cust on prjc_oid = prjgd_prjc_oid " _
            & "  inner join cp_mstr on cp_id = prjc_cp_id " _
            & "  inner join loc_mstr on loc_id = prjc_loc_id " _
            & "  inner join si_mstr on si_id = prjc_si_id " _
            & "  inner join prj_mstr on prj_oid = prjc_prj_oid " _
            & " where prjg_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and prjg_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by cp_code asc "
        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("layout").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  mstr.prjl_oid, " _
            & "  mstr.prjl_dom_id, " _
            & "  mstr.prjl_en_id, " _
            & "  mstr.prjl_add_by, " _
            & "  mstr.prjl_add_date, " _
            & "  mstr.prjl_upd_by, " _
            & "  mstr.prjl_upd_date, " _
            & "  mstr.prjl_dt, " _
            & "  mstr.prjl_seq, " _
            & "  mstr.prjl_prjg_oid,prjg_code,prjg_name, " _
            & "  mstr.prjl_id, " _
            & "  mstr.prjl_isnull, " _
            & "  mstr.prjl_data_type, " _
            & "  mstr.prjl_parent_id,desc_pr.code_name as desc_parent_id, " _
            & "  mstr.prjl_user_value, " _
            & "  mstr.prjl_is_root, " _
            & "  mstr.prjl_tranc_id,tranc_desc, " _
            & "  mstr.prjl_desc_id,desc_mstr.code_name as desc_id, " _
            & "  mstr.prjl_tran_id,tran_name " _
            & "FROM  " _
            & "public.prjl_layout mstr " _
            & "inner join prjg_group on prjg_oid = mstr.prjl_prjg_oid " _
            & "inner join tran_mstr on tran_id = mstr.prjl_tran_id  " _
            & "inner join tranu_usr on tranu_tran_id = tran_id   " _
            & "inner join code_mstr desc_mstr on desc_mstr.code_id = mstr.prjl_desc_id   " _
            & "inner join tranc_coll on tranc_id = mstr.prjl_tranc_id   " _
            & "inner join lyt_mstr pr on pr.lyt_id = mstr.prjl_parent_id   " _
            & "inner join code_mstr desc_pr on desc_pr.code_id = pr.lyt_desc_id  " _
            & " where prjg_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and prjg_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by mstr.prjl_seq asc "
        load_data_detail(sql, gc_layout, "layout")
        gv_layout.BestFitColumns()

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("prjgd_prjg_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjgd_prjg_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjg_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_layout.Columns("prjl_prjg_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjl_prjg_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjg_oid").ToString & "'")
            gv_layout.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        prjg_en_id.Focus()
        prjg_en_id.ItemIndex = 0
        prjg_date.EditValue = Today()
        prjg_remarks.Text = ""
        prjg_tran_id.ItemIndex = 0
        Try
            tcg_header.SelectedTabPageIndex = 0
            'tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        prjg_en_id.Enabled = True
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        _from_edit = False

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  prjgd_oid, " _
                        & "  prjgd_dom_id, " _
                        & "  prjgd_en_id, " _
                        & "  prjgd_add_by, " _
                        & "  prjgd_add_date, " _
                        & "  prjgd_upd_by, " _
                        & "  prjgd_upd_date, " _
                        & "  prjgd_dt, " _
                        & "  prjgd_prjg_oid, " _
                        & "  prjgd_prjc_oid, " _
                        & "  cp_code,prjc_pt_desc1,prjc_pt_desc2, " _
                        & "  loc_desc,si_desc " _
                        & "FROM  " _
                        & "  public.prjgd_det " _
                        & "  inner join prjg_group on prjg_oid = prjgd_prjg_oid  " _
                        & "  inner join prjc_cust on prjc_oid = prjgd_prjc_oid " _
                        & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                        & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                        & "  inner join si_mstr on si_id = prjc_si_id " _
                        & "  where prjgd_en_id = -5464 "
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
        '_conf_value = func_coll.get_conf_file("wf_mo")

        row = BindingContext(ds.Tables(0)).Position
        'If func_coll.get_status_wf(ds.Tables(0).Rows(row).Item("mo_code")) > 0 Then
        '    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Function
        'End If

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
                Using objinsert As New master_new.CustomCommand
                    With objinsert
.Command.Open()
                        ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            '.Command = .Connection.CreateCommand
                            '.Command.Transaction = sqlTran

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from prjg_group where prjg_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjg_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            .Command.Commit()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            'sqlTran.Rollback()
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

    Private Function cek_layout(ByVal par_prjg_oid As String) As String
        cek_layout = "kosong"
        Try
            Using objkalendar As New master_new.CustomCommand
                With objkalendar
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(prjl_user_value,'-') from prjl_layout  " + _
                                           " where prjl_prjg_oid = '" + par_prjg_oid.ToString() + "'" + _
                                           " and coalesce(prjl_user_value,'-') <> '-' "
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        'cek_layout = .DataReader.Item("prjl_user_value")
                        cek_layout = "isi"
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return cek_layout
    End Function

    Public Overrides Function edit_data() As Boolean
        _from_edit = True

        If cek_layout(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjg_oid")) = "isi" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If

        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position
            With ds.Tables(0).Rows(row)
                prjg_en_id.EditValue = .Item("prjg_en_id")
                prjg_date.EditValue = .Item("prjg_date")
                prjg_name.Text = .Item("prjg_name")
                prjg_prj_oid.Text = .Item("prj_code")
                _prj_oid = .Item("prjg_prj_oid")
                prjg_remarks.Text = SetString(.Item("prjg_remarks"))
                prjg_tran_id.EditValue = .Item("prjg_tran_id")
                _prjg_oid_master = .Item("prjg_oid")
            End With
            prjg_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjgd_oid, " _
                            & "  prjgd_dom_id, " _
                            & "  prjgd_en_id, " _
                            & "  prjgd_add_by, " _
                            & "  prjgd_add_date, " _
                            & "  prjgd_upd_by, " _
                            & "  prjgd_upd_date, " _
                            & "  prjgd_dt, " _
                            & "  prjgd_prjg_oid, " _
                            & "  prjgd_prjc_oid, " _
                            & "  cp_code,prjc_pt_desc1,prjc_pt_desc2, " _
                            & "  loc_desc,si_desc " _
                            & "FROM  " _
                            & "  public.prjgd_det " _
                            & "  inner join prjg_group on prjg_oid = prjgd_prjg_oid  " _
                            & "  inner join prjc_cust on prjc_oid = prjgd_prjc_oid " _
                            & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                            & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                            & "  inner join si_mstr on si_id = prjc_si_id " _
                            & " where prjgd_prjg_oid = '" + ds.Tables(0).Rows(row).Item("prjg_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'ds_get = New DataSet
            'Try
            '    Using objcb As New master_new.CustomCommand
            '        With objcb
            '            .SQL = "SELECT  " _
            '                & "  prjgd_oid, " _
            '                & "  prjgd_dom_id, " _
            '                & "  prjgd_en_id, " _
            '                & "  prjgd_add_by, " _
            '                & "  prjgd_add_date, " _
            '                & "  prjgd_upd_by, " _
            '                & "  prjgd_upd_date, " _
            '                & "  prjgd_dt, " _
            '                & "  prjgd_prjg_oid, " _
            '                & "  prjgd_prjc_oid, " _
            '                & "  cp_code,prjc_pt_desc1,prjc_pt_desc2, " _
            '                & "  loc_desc,si_desc " _
            '                & "FROM  " _
            '                & "  public.prjgd_det " _
            '                & "  inner join prjg_group on prjg_oid = prjgd_prjg_oid  " _
            '                & "  inner join prjc_cust on prjc_oid = prjgd_prjc_oid " _
            '                & "  inner join cp_mstr on cp_id = prjc_cp_id " _
            '                & "  inner join loc_mstr on loc_id = prjc_loc_id " _
            '                & "  inner join si_mstr on si_id = prjc_si_id " _
            '                & " where prjgd_prjg_oid = '" + ds.Tables(0).Rows(row).Item("prjg_oid").ToString + "'"
            '            .InitializeCommand()
            '            .FillDataSet(ds_get, "get")
            '        End With
            '    End Using
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Dim ssqls As New ArrayList
        Dim i As Integer
        Dim ds_bantu As New DataSet
        ds_bantu = func_data.load_layout_mstr(prjg_tran_id.EditValue)

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from prjgd_det where prjgd_prjg_oid = '" + _prjg_oid_master + "'"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from prjl_layout where prjl_prjg_oid = '" + _prjg_oid_master + "'"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.prjg_group   " _
                                            & "SET  " _
                                            & "  prjg_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  prjg_en_id = " & SetInteger(prjg_en_id.EditValue) & ",  " _
                                            & "  prjg_add_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  prjg_add_date = current_timestamp, " _
                                            & "  prjg_dt = current_timestamp, " _
                                            & "  prjg_prj_oid = " & SetSetring(_prj_oid.ToString()) & ",  " _
                                            & "  prjg_remarks = " & SetSetring(prjg_remarks.Text) & ",  " _
                                            & "  prjg_date = " & SetDate(prjg_date.DateTime.Date) & ",  " _
                                            & "  prjg_name = " & SetSetring(prjg_name.Text) & ",  " _
                                            & "  prjg_tran_id = " & SetInteger(prjg_tran_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  prjg_oid = " & SetSetring(_prjg_oid_master.ToString()) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.prjgd_det " _
                                                & "( " _
                                                & "  prjgd_oid, " _
                                                & "  prjgd_dom_id, " _
                                                & "  prjgd_en_id, " _
                                                & "  prjgd_add_by, " _
                                                & "  prjgd_add_date, " _
                                                & "  prjgd_dt, " _
                                                & "  prjgd_prjg_oid, " _
                                                & "  prjgd_prjc_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(prjg_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetSetring(_prjg_oid_master.ToString()) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjgd_prjc_oid").ToString()) & " " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        Dim a As Integer
                        For a = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.prjl_layout " _
                                                    & "( " _
                                                    & "  prjl_oid, " _
                                                    & "  prjl_dom_id, " _
                                                    & "  prjl_en_id, " _
                                                    & "  prjl_add_by, " _
                                                    & "  prjl_add_date, " _
                                                    & "  prjl_dt, " _
                                                    & "  prjl_seq, " _
                                                    & "  prjl_prjg_oid, " _
                                                    & "  prjl_id, " _
                                                    & "  prjl_isnull, " _
                                                    & "  prjl_data_type, " _
                                                    & "  prjl_parent_id, " _
                                                    & "  prjl_user_value, " _
                                                    & "  prjl_is_root, " _
                                                    & "  prjl_tranc_id, " _
                                                    & "  prjl_desc_id, " _
                                                    & "  prjl_tran_id " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(prjg_en_id.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_seq")) & ",  " _
                                                    & SetSetring(_prjg_oid_master.ToString()) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_id")) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("lyt_isnull")) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("lyt_data_type")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_parent_id")) & ",  " _
                                                    & SetSetring("") & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("lyt_is_root")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_tranc_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_desc_id")) & ",  " _
                                                    & SetInteger(prjg_tran_id.EditValue) & "  " _
                                                    & ");"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        .Command.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_prjg_oid_master, "prjg_oid")
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
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

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        Dim _date As Date = func_coll.get_tanggal_sistem
        'Dim i As Integer

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("mod_um")) = True Then
        '        MessageBox.Show("Unit Measure Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next


        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Shared Function GetIDMax() As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(ass_id),0) as max_col from ass_mstr "
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        GetIDMax = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetIDMax
    End Function

    Public Shared Function GetMaxLine(ByVal _par_pt_id As Integer) As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(ass_line),0) as max_col from ass_mstr " + _
                                           "where ass_pt_id = " + _par_pt_id.ToString()
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        GetMaxLine = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetMaxLine
    End Function

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Dim _prjg_oid As Guid
        _prjg_oid = Guid.NewGuid()
        ds_bantu = func_data.load_layout_mstr(prjg_tran_id.EditValue)

        _prjg_code = func_coll.get_transaction_number("PG", prjg_en_id.GetColumnValue("en_code"), "prjg_group", "prjg_code", func_coll.get_tanggal_sistem)
        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.prjg_group " _
                                            & "( " _
                                            & "  prjg_oid, " _
                                            & "  prjg_dom_id, " _
                                            & "  prjg_en_id, " _
                                            & "  prjg_add_by, " _
                                            & "  prjg_add_date, " _
                                            & "  prjg_dt, " _
                                            & "  prjg_code, " _
                                            & "  prjg_prj_oid, " _
                                            & "  prjg_remarks, " _
                                            & "  prjg_date, " _
                                            & "  prjg_name, " _
                                            & "  prjg_tran_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_prjg_oid.ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(prjg_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_prjg_code) & ",  " _
                                            & SetSetring(_prj_oid.ToString()) & ",  " _
                                            & SetSetring(prjg_remarks.Text) & ",  " _
                                            & SetDate(prjg_date.DateTime.Date) & ",  " _
                                            & SetSetring(prjg_name.Text) & ",  " _
                                            & SetInteger(prjg_tran_id.EditValue) & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.prjgd_det " _
                                                & "( " _
                                                & "  prjgd_oid, " _
                                                & "  prjgd_dom_id, " _
                                                & "  prjgd_en_id, " _
                                                & "  prjgd_add_by, " _
                                                & "  prjgd_add_date, " _
                                                & "  prjgd_dt, " _
                                                & "  prjgd_prjg_oid, " _
                                                & "  prjgd_prjc_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(prjg_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetSetring(_prjg_oid.ToString()) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjgd_prjc_oid").ToString()) & " " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                        Next

                        Dim a As Integer
                        For a = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.prjl_layout " _
                                                    & "( " _
                                                    & "  prjl_oid, " _
                                                    & "  prjl_dom_id, " _
                                                    & "  prjl_en_id, " _
                                                    & "  prjl_add_by, " _
                                                    & "  prjl_add_date, " _
                                                    & "  prjl_dt, " _
                                                    & "  prjl_seq, " _
                                                    & "  prjl_prjg_oid, " _
                                                    & "  prjl_id, " _
                                                    & "  prjl_isnull, " _
                                                    & "  prjl_data_type, " _
                                                    & "  prjl_parent_id, " _
                                                    & "  prjl_user_value, " _
                                                    & "  prjl_is_root, " _
                                                    & "  prjl_tranc_id, " _
                                                    & "  prjl_desc_id, " _
                                                    & "  prjl_tran_id " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(prjg_en_id.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_seq")) & ",  " _
                                                    & SetSetring(_prjg_oid.ToString()) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_id")) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("lyt_isnull")) & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("lyt_data_type")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_parent_id")) & ",  " _
                                                    & SetSetring("") & ",  " _
                                                    & SetSetring(ds_bantu.Tables(0).Rows(a).Item("lyt_is_root")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_tranc_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(a).Item("lyt_desc_id")) & ",  " _
                                                    & SetInteger(prjg_tran_id.EditValue) & "  " _
                                                    & ");"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        .Command.Commit()
                        after_success()
                        set_row(_prjg_code.ToString, "prjg_code")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
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

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        'Dim _qty_open, _qty_mo As Double

        'If _from_edit = False Then
        '    If e.Column.Name = "mod_qty" Then
        '        Try
        '            _qty_open = (gv_edit.GetRowCellValue(e.RowHandle, "qty_open"))
        '        Catch ex As Exception
        '        End Try

        '        If e.Value > _qty_open Then
        '            MessageBox.Show("Qty Open Can't Lower Than Qty MO..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            gv_edit.CancelUpdateCurrentRow()
        '            Exit Sub
        '        End If
        '    End If
        'Else
        '    If e.Column.Name = "mod_qty" Then
        '        Try
        '            _qty_open = (gv_edit.GetRowCellValue(e.RowHandle, "qty_open"))
        '            _qty_mo = ds_get.Tables(0).Rows(BindingContext(ds_get.Tables(0)).Position).Item("mod_qty") '(gv_edit.GetRowCellValue(e.RowHandle, "paod_qty"))
        '        Catch ex As Exception
        '        End Try

        '        If e.Value > (_qty_open + _qty_mo) Then
        '            MessageBox.Show("Qty MO Can't Bigger Than Qty PAO..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            gv_edit.CancelUpdateCurrentRow()
        '            Exit Sub
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "cp_code" Then
            Dim frm As New FPartNumberCustomerSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = prjg_en_id.EditValue
            frm._prj_oid = _prj_oid
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

#End Region

    Public Function get_tanggal_sistem() As Date
        Try
            Using objcek As New master_new.CustomCommand
                With objcek
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select current_date as tanggal"
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
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

#Region "Approval"

    Public Overrides Sub approve_line()
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mo_code")
        '_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mo_oid")
        '_colom = "mo_trans_id"
        '_table = "mo_mstr"
        '_criteria = "mo_code"
        '_initial = "mo"
        '_type = "mo"
        '_title = "MO"
        'approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mo_code")
        '_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mo_oid")
        '_colom = "mo_trans_id"
        '_table = "mo_mstr"
        '_criteria = "mo_code"
        '_initial = "mo"
        '_type = "mo"

        'cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)
        '_conf_value = func_coll.get_conf_file("wf_mo")

        'Dim _pao_trans_id As String = ""
        'Dim ssqls As New ArrayList

        'If _conf_value = "1" Then
        '    Try
        '        Using objcek As New master_new.CustomCommand
        '            With objcek
        '                '.Connection.Open()
        '                '.Command = .Connection.CreateCommand
        '                '.Command.CommandType = CommandType.Text
        '                .Command.CommandText = "select mo_trans_id from mo_mstr where mo_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mo_oid").ToString)
        '                .InitializeCommand()
        '                .DataReader = .ExecuteReader
        '                While .DataReader.Read
        '                    _pao_trans_id = .DataReader("mo_trans_id").ToString
        '                End While
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try
        'End If

        'If _pao_trans_id.ToUpper = "D" Then
        '    MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
        '    Exit Sub
        'ElseIf _pao_trans_id.ToUpper = "C" Then
        '    MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
        '    Exit Sub
        'ElseIf _pao_trans_id.ToUpper = "X" Then
        '    MessageBox.Show("Can't Cancel For Cancel Data..", "Conf", MessageBoxButtons.OK)
        '    Exit Sub
        'Else
        '    If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
        '        Exit Sub
        '    End If
        'End If

        'Try
        '    Using objinsert As New master_new.CustomCommand
        '        With objinsert
        '.Command.Open()
        '            ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                '.Command = .Connection.CreateCommand
        '                '.Command.Transaction = sqlTran

        '                For Each _dr As DataRow In ds.Tables("detail").Rows
        '                    If _dr("mod_mo_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mo_oid") Then
        '                        'If _dr("paod_qty_mo") > 0 Then
        '                        '    MessageBox.Show("Can't Cancel For Qty MO more than 0..!", "Conf", MessageBoxButtons.OK)
        '                        '    'sqlTran.Rollback()
        '                        '    Exit Sub
        '                        'End If

        '                        '.Command.CommandType = CommandType.Text
        '                        .Command.CommandText = "update prjd_det set prjd_qty_mo = prjd_qty_mo - " + SetDbl(_dr("mod_qty")) + " " + _
        '                                               " where prjd_oid = " + SetSetring(_dr("prjd_oid").ToString())
        '                        .Command.ExecuteNonQuery()
        '                        '.Command.Parameters.Clear()

        '                        '.Command.CommandType = CommandType.Text
        '                        .Command.CommandText = "update paod_det set paod_qty_mo = paod_qty_mo - " + SetDbl(_dr("mod_qty")) + " " + _
        '                                               " where paod_oid = " + SetSetring(_dr("mod_paod_oid").ToString())
        '                        .Command.ExecuteNonQuery()
        '                        '.Command.Parameters.Clear()
        '                    End If
        '                Next

        '                '.Command.CommandType = CommandType.Text
        '                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
        '                                       " where " + par_criteria + " = '" + par_code + "'"
        '                .Command.ExecuteNonQuery()
        '                '.Command.Parameters.Clear()

        '                '.Command.CommandType = CommandType.Text
        '                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
        '                                       " where wf_ref_code = '" + par_code + "'"
        '                .Command.ExecuteNonQuery()
        '                '.Command.Parameters.Clear()

        '                .Command.Commit()
        '                MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                row = BindingContext(ds.Tables(0)).Position
        '                help_load_data(True)
        '                set_row(Trim(par_oid), par_initial + "_oid")
        '            Catch ex As PgSqlException
        '                'sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Public Overrides Sub reminder_mail()
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _code, _type, _user, _title As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mo_code")
        '_type = "mo"
        '_user = func_coll.get_wf_iscurrent(_code)
        '_title = "MO"

        'If _user = "" Then
        '    MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        'reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        'Dim i As Integer
        'Dim _po_is_budget As String = ""

        'If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '    Exit Sub
        'End If

        'ds.Tables("smart").AcceptChanges()

        'For i = 0 To ds.Tables("smart").Rows.Count - 1
        '    If ds.Tables("smart").Rows(i).Item("status") = True Then

        '        Try
        '            gv_email.Columns("mo_oid").FilterInfo = _
        '            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[mo_oid] = '" & ds.Tables("smart").Rows(i).Item("mo_oid").ToString & "'")
        '            gv_email.BestFitColumns()

        '            'gv_email.Columns("po_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("po_oid"))
        '        Catch ex As Exception
        '        End Try

        '        _trans_id = "W" 'default langsung ke W 
        '        user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("pao_code"), 0)
        '        user_wf_email = mf.get_email_address(user_wf)

        '        Try
        '            Using objinsert As New master_new.CustomCommand
        '                With objinsert
        '.Command.Open()
        '                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '                    Try
        '                        '.Command = .Connection.CreateCommand
        '                        '.Command.Transaction = sqlTran
        '                        '.Command.CommandType = CommandType.Text
        '                        .Command.CommandText = "update mo_mstr set mo_trans_id = '" + _trans_id + "'," + _
        '                                       " mo_upd_by = '" + master_new.ClsVar.sNama + "'," + _
        '                                       " mo_upd_date = current_timestamp " + _
        '                                       " where mo_oid = '" + ds.Tables("smart").Rows(i).Item("mo_oid") + "'"

        '                        .Command.ExecuteNonQuery()
        '                        '.Command.Parameters.Clear()

        '                        '============================================================================
        '                        If get_status_wf(ds.Tables("smart").Rows(i).Item("mo_code")) = 0 Then
        '                            '.Command.CommandType = CommandType.Text
        '                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
        '                                                   " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("mo_code") + "'" + _
        '                                                   " and wf_seq = 0"

        '                            .Command.ExecuteNonQuery()
        '                            '.Command.Parameters.Clear()

        '                            'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
        '                            '.Command.CommandType = CommandType.Text
        '                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
        '                                                   " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("mo_code") + "'"

        '                            .Command.ExecuteNonQuery()
        '                            '.Command.Parameters.Clear()

        '                        ElseIf get_status_wf(ds.Tables("smart").Rows(i).Item("mo_code")) > 0 Then
        '                            '.Command.CommandType = CommandType.Text
        '                            .Command.CommandText = "update wf_mstr set " + _
        '                                                   " wf_iscurrent = 'Y', " + _
        '                                                   " wf_wfs_id = '0', " + _
        '                                                   " wf_desc = '', " + _
        '                                                   " wf_date_to = null, " + _
        '                                                   " wf_aprv_user = '', " + _
        '                                                   " wf_aprv_date = null " + _
        '                                                   " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("mo_code") + "'" + _
        '                                                   " and wf_wfs_id = '4' "
        '                            .Command.ExecuteNonQuery()
        '                            '.Command.Parameters.Clear()
        '                        End If
        '                        '============================================================================

        '                        .Command.Commit()

        '                        format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("mo_code"), "mo")

        '                        filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("mo_code") + ".xls"
        '                        ExportTo(gv_email, New ExportXlsProvider(filename))

        '                        If user_wf_email <> "" Then
        '                            mf.sent_email(user_wf_email, "", mf.title_email("MO", ds.Tables("smart").Rows(i).Item("mo_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
        '                        Else
        '                            MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                        End If

        '                    Catch ex As PgSqlException
        '                        'sqlTran.Rollback()
        '                        MessageBox.Show(ex.Message)
        '                    End Try
        '                End With
        '            End Using
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '        End Try
        '    End If
        'Next

        'help_load_data(True)
        'MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

    Private Sub gv_edit_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            '.SetRowCellValue(e.RowHandle, "mod_ptnr_id", mo_ptnr_id.EditValue)
            '.SetRowCellValue(e.RowHandle, "ptnr_name", mo_ptnr_id.Text)
            '.BestFitColumns()
        End With
    End Sub

    Private Sub prjg_prj_oid_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles prjg_prj_oid.ButtonClick
        Dim frm As New FProjectSearch()
        frm.set_win(Me)
        frm._en_id = prjg_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

End Class
