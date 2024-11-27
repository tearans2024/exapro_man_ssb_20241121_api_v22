Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Public Class FLocationSiteImport
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

    Private Sub sc_le_loc_en_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loc_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", loc_en_id.EditValue))

        With loc_si_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("si_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("is_mstr", loc_en_id.EditValue))

        With sc_le_loc_is
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("is_code").ToString
            .Properties.ValueMember = dt_bantu.Columns("is_id").ToString
            .ItemIndex = 0
        End With

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_customer(loc_en_id.EditValue))
        loc_ptnr_id.Properties.DataSource = dt_bantu
        loc_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        loc_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        loc_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(loc_en_id.EditValue, "emp_region"))
        loc_reg_id.Properties.DataSource = dt_bantu
        loc_reg_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        loc_reg_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        loc_reg_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Warehouse", "wh_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "loc_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Category", "loc_cat", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Internal Code", "loc_internal_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project", "project", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Status", "is_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address", "loc_address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Regional", "regional", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Area", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Line 1", "loc_address_line1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Line 2", "loc_address_line2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Line 3", "loc_address_line3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Telp", "loc_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Fax", "loc_fax", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Contact Person", "loc_contact_person", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Contact Person Phone", "loc_telp_contact_person", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "loc_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "loc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "loc_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "loc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "loc_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  loc_oid, " _
                    & "  loc_dom_id, " _
                    & "  loc_en_id, " _
                    & "  en_code,en_desc, " _
                    & "  loc_add_by, " _
                    & "  loc_add_date, " _
                    & "  loc_upd_by, " _
                    & "  loc_upd_date, " _
                    & "  loc_id, " _
                    & "  loc_wh_id, " _
                    & "  wh_desc, " _
                    & "  loc_si_id,ptnr_name, " _
                    & "  si_desc, " _
                    & "  loc_code, " _
                    & "  loc_desc, " _
                    & "  loc_type, " _
                    & "  loc_cat, " _
                    & "  loc_is_id, " _
                    & "  is_desc, " _
                    & "  loc_active, " _
                    & "  loc_pjc_id, " _
                    & "  pjc_desc, " _
                    & "  loc_eu_site, " _
                    & "  loc_address, " _
                    & "  loc_reg_id,reg.code_name as regional, " _
                    & "  loc_area_id,area_name, " _
                    & "  loc_dt,loc_ptnr_id,loc_internal_code " _
                    & "FROM  " _
                    & "  public.loc_mstr" _
                    & " inner join en_mstr on en_id = loc_en_id " _
                    & " inner join si_mstr on si_id = loc_si_id " _
                    & " inner join is_mstr on is_id = loc_is_id " _
                    & " inner join pjc_mstr on pjc_id = loc_pjc_id " _
                    & " inner join wh_mstr on wh_id = loc_wh_id " _
                    & " left outer join code_mstr reg on reg.code_id = loc_reg_id " _
                    & " left outer join area_mstr on area_id = loc_area_id " _
                    & " left outer join ptnr_mstr on ptnr_id = loc_ptnr_id " _
                    & " where loc_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & " and loc_eu_site = 'Y' "

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        loc_en_id.ItemIndex = 0
        loc_en_id.Focus()
        loc_code.Text = ""
        loc_desc.Text = ""
        loc_active.EditValue = False
        loc_ptnr_id.ItemIndex = 0
        loc_address.Text = ""
        loc_reg_id.ItemIndex = 0
        loc_area_id.ItemIndex = 0
    End Sub

    Public Function get_internal_code(ByVal par_type As String, ByVal par_entity As String, ByVal par_table As String, ByVal par_colom As String, ByVal par_date As Date) As String
        get_internal_code = ""

        Dim tahun, bulan, no_urut_format As String

        Dim tanggal As Date
        tanggal = par_date 'get_tanggal_sistem()
        tahun = tanggal.Year.ToString.Substring(2, 2)
        bulan = tanggal.Month.ToString
        no_urut_format = ""

        If Len(bulan) = 1 Then
            bulan = "0" + bulan
        End If

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select coalesce(max(cast(substring(" + par_colom + ",9,7) as integer)),0) + 1 as no_urut " + _
                           " from " + par_table + _
                           " where substring(" + par_colom + ",3,2) = '" + par_entity + "'" + _
                           " and substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
                           " and length(" + par_colom + ") = 15 " + _
                           " limit 1"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    no_urut_format = ds_bantu.Tables(0).Rows(0).Item("no_urut")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If Len(no_urut_format) = 1 Then
            no_urut_format = "000000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 2 Then
            no_urut_format = "00000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 3 Then
            no_urut_format = "0000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 4 Then
            no_urut_format = "000" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 5 Then
            no_urut_format = "00" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 6 Then
            no_urut_format = "0" + no_urut_format.ToString
        ElseIf Len(no_urut_format) = 7 Then
            no_urut_format = no_urut_format.ToString
        End If

        get_internal_code = par_type + par_entity + tahun + bulan + no_urut_format
        Return get_internal_code
    End Function

    Public Overrides Function insert() As Boolean
        Dim _loc_oid As Guid
        _loc_oid = Guid.NewGuid
        Dim _internal_code As String
        _internal_code = get_internal_code("SI", loc_en_id.EditValue, "loc_mstr", "loc_internal_code", Today())
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
                                            & "  public.loc_mstr " _
                                            & "( " _
                                            & "  loc_oid, " _
                                            & "  loc_dom_id, " _
                                            & "  loc_en_id, " _
                                            & "  loc_add_by, " _
                                            & "  loc_add_date, " _
                                            & "  loc_id, " _
                                            & "  loc_si_id, " _
                                            & "  loc_code, " _
                                            & "  loc_desc, " _
                                            & "  loc_is_id, " _
                                            & "  loc_pjc_id, " _
                                            & "  loc_active, " _
                                            & "  loc_eu_site, " _
                                            & "  loc_ptnr_id, " _
                                            & "  loc_address, " _
                                            & "  loc_reg_id, " _
                                            & "  loc_area_id, " _
                                            & "  loc_dt, " _
                                            & "  loc_internal_code " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_loc_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(loc_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(func_coll.GetID("loc_mstr", loc_en_id.GetColumnValue("en_code"), "loc_id", "loc_en_id", loc_en_id.EditValue.ToString)) & ",  " _
                                            & SetInteger(loc_si_id.EditValue) & ",  " _
                                            & SetSetring(loc_code.Text) & ",  " _
                                            & SetSetring(loc_desc.Text) & ",  " _
                                            & SetSetring(sc_le_loc_is.EditValue) & ",  " _
                                            & SetBitYN(loc_active.EditValue) & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetInteger(loc_ptnr_id.EditValue) & ",  " _
                                            & SetSetring(loc_address.Text) & ",  " _
                                            & SetInteger(loc_reg_id.EditValue) & ",  " _
                                            & SetInteger(loc_area_id.EditValue) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetSetring(_internal_code) _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(_loc_oid.ToString, "loc_oid")
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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            loc_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _loc_oid_mstr = .Item("loc_oid")
                loc_en_id.EditValue = .Item("loc_en_id")
                loc_code.Text = SetString(.Item("loc_code"))
                loc_desc.Text = SetString(.Item("loc_desc"))
                loc_si_id.EditValue = .Item("loc_si_id")
                sc_le_loc_is.EditValue = .Item("loc_is_id")
                loc_reg_id.EditValue = .Item("loc_reg_id")
                loc_area_id.EditValue = .Item("loc_area_id")
                loc_address.Text = SetString(.Item("loc_address"))
                loc_active.EditValue = IIf(.Item("loc_active") = "Y", True, False)

                If IsDBNull(.Item("loc_ptnr_id")) = True Then
                    loc_ptnr_id.ItemIndex = 0
                Else
                    loc_ptnr_id.EditValue = .Item("loc_ptnr_id")
                End If
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
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
                                            & "  public.loc_mstr   " _
                                            & "SET  " _
                                            & "  loc_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  loc_en_id = " & SetInteger(loc_en_id.EditValue) & ",  " _
                                            & "  loc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  loc_upd_date = current_timestamp,  " _
                                            & "  loc_si_id = " & SetInteger(loc_si_id.EditValue) & ",  " _
                                            & "  loc_code = " & SetSetring(loc_code.Text) & ",  " _
                                            & "  loc_desc = " & SetSetring(loc_desc.Text) & ",  " _
                                            & "  loc_is_id = " & SetInteger(sc_le_loc_is.EditValue) & ",  " _
                                            & "  loc_active = " & SetBitYN(loc_active.EditValue) & ",  " _
                                            & "  loc_ptnr_id = " & SetInteger(loc_ptnr_id.EditValue) & ",  " _
                                            & "  loc_reg_id = " & SetInteger(loc_reg_id.EditValue) & ",  " _
                                            & "  loc_area_id = " & SetInteger(loc_area_id.EditValue) & ",  " _
                                            & "  loc_address = " & SetSetring(loc_address.Text) & ",  " _
                                            & "  loc_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  loc_oid = " & SetSetring(_loc_oid_mstr) & " "
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(_loc_oid_mstr, "loc_oid")
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

    Public Overrides Function delete_data() As Boolean
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
                            .Command.CommandText = "delete from loc_mstr where loc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("loc_oid") + "'"
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

    Private Sub sb_migrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_migrate.Click
        Dim i, _id_si, _id, _id_loc As Integer
        Dim _code As String = "_x_"
        Dim ds_si As DataSet

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
        gv_master.Columns("loc_code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        _id_loc = func_coll.GetID("loc_mstr", import_en_id.GetColumnValue("en_code"), "loc_id", "loc_en_id", import_en_id.EditValue.ToString)

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
                            ds_si = New DataSet

                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  si_mstr  " _
                                             & "  WHERE si_desc ~~* " & SetSetring(gv_master.GetRowCellValue(i, "si_desc")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_si, "si")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_si.Tables("si").Rows.Count > 0 Then
                                _id_si = ds_si.Tables("si").Rows(0).Item("si_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Site Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MsgBox(_id)
                                Exit Sub
                            End If

                            'Project
                            Dim ds_pjc As New DataSet
                            Dim _loc_pjc_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  pjc_mstr  " _
                                             & "  WHERE pjc_desc ~~* " & SetSetring(gv_master.GetRowCellValue(i, "project")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_pjc, "pj_id")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_pjc.Tables("pj_id").Rows.Count > 0 Then
                                _loc_pjc_id = ds_pjc.Tables("pj_id").Rows(0).Item("pjc_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Project Name Doesn't Exist : " + gv_master.GetRowCellValue(i, "project"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Inventory Status
                            Dim ds_po_is As New DataSet
                            Dim _loc_is_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  is_mstr  " _
                                             & "  WHERE is_desc ~~* " & SetSetring(gv_master.GetRowCellValue(i, "is_desc")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_po_is, "po_is")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_po_is.Tables("po_is").Rows.Count > 0 Then
                                _loc_is_id = ds_po_is.Tables("po_is").Rows(0).Item("is_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Inventory Status Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Regional
                            Dim ds_reg As New DataSet
                            Dim _reg_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  code_mstr  " _
                                             & "  WHERE code_name ~~* " & SetSetring(gv_master.GetRowCellValue(i, "regional")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_reg, "reg")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_reg.Tables("reg").Rows.Count > 0 Then
                                _reg_id = ds_reg.Tables("reg").Rows(0).Item("code_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Regional Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Area
                            Dim ds_area As New DataSet
                            Dim _area_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  area_mstr  " _
                                             & "  WHERE area_name ~~* " & SetSetring(gv_master.GetRowCellValue(i, "area_name")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_area, "area")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_area.Tables("area").Rows.Count > 0 Then
                                _area_id = ds_area.Tables("area").Rows(0).Item("area_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Area Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Warehouse
                            Dim ds_whouse As New DataSet
                            Dim _wh_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  wh_mstr  " _
                                             & "  WHERE wh_desc ~~* " & SetSetring(gv_master.GetRowCellValue(i, "wh_desc")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_whouse, "whouse")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_whouse.Tables("whouse").Rows.Count > 0 Then
                                _wh_id = ds_whouse.Tables("whouse").Rows(0).Item("wh_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Warehouse Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If


                            'Type
                            Dim ds_type As New DataSet
                            Dim _type_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  code_mstr  " _
                                             & "  WHERE code_name ~~* " & SetSetring(gv_master.GetRowCellValue(i, "loc_type")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_type, "type")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_type.Tables("type").Rows.Count > 0 Then
                                _type_id = ds_type.Tables("type").Rows(0).Item("code_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Location Type Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Categori
                            Dim ds_cat As New DataSet
                            Dim _cat_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  code_mstr  " _
                                             & "  WHERE code_name ~~* " & SetSetring(gv_master.GetRowCellValue(i, "loc_cat")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_cat, "cat")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_cat.Tables("cat").Rows.Count > 0 Then
                                _cat_id = ds_cat.Tables("cat").Rows(0).Item("code_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Location Category Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If


                            'Partner
                            Dim ds_ptnr As New DataSet
                            Dim _ptnr_id As Integer
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  ptnr_mstr  " _
                                             & "  WHERE ptnr_name ~~* " & SetSetring(gv_master.GetRowCellValue(i, "ptnr_name")) & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_ptnr, "ptnr")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_ptnr.Tables("ptnr").Rows.Count > 0 Then
                                _ptnr_id = ds_ptnr.Tables("ptnr").Rows(0).Item("ptnr_id")
                            Else
                                sqlTran.Rollback()
                                MessageBox.Show("Partner Doesn't Exist At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            

                            'Loc Code
                            If Trim(gv_master.GetRowCellValue(i, "loc_code")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Code Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Loc Description
                            If Trim(gv_master.GetRowCellValue(i, "loc_desc")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Descrption Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Warehouse
                            'If Trim(gv_master.GetRowCellValue(i, "warehouse")) = "" Then
                            '    sqlTran.Rollback()
                            '    MessageBox.Show("Warehouse Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            '    'MsgBox(i)
                            '    'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                            '    Exit Sub
                            'End If

                            'Loc Type
                            If Trim(gv_master.GetRowCellValue(i, "loc_type")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Location Type Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Loc Category
                            If Trim(gv_master.GetRowCellValue(i, "loc_cat")) = "" Then
                                sqlTran.Rollback()
                                MessageBox.Show("Location Category Can't Empty At Row " + (i + 1).ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(i)
                                'MessageBox.Show(gv_master.GetRowCellValue(i, "cost_methode").ToString())
                                Exit Sub
                            End If

                            'Taufik 12/03/2012
                            'Cek location yang double
                            Dim ds_locsite As New DataSet
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .SQL = "SELECT * " _
                                             & "  FROM " _
                                             & "  loc_mstr  " _
                                             & "  WHERE loc_desc ~~* " & SetSetring(gv_master.GetRowCellValue(i, "loc_desc")) & " " _
                                             & "  and loc_ptnr_id = " & _ptnr_id & ""
                                        .InitializeCommand()
                                        .FillDataSet(ds_locsite, "locsite")
                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                            End Try

                            If ds_locsite.Tables("locsite").Rows.Count < 1 Then
                                Dim _internal_code As String
                                _internal_code = get_internal_code("SI", loc_en_id.EditValue, "loc_mstr", "loc_internal_code", Today())

                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.loc_mstr " _
                                                & "( " _
                                                & "  loc_oid, " _
                                                & "  loc_dom_id, " _
                                                & "  loc_en_id, " _
                                                & "  loc_add_by, " _
                                                & "  loc_add_date, " _
                                                & "  loc_id, " _
                                                & "  loc_si_id, " _
                                                & "  loc_code, " _
                                                & "  loc_desc, " _
                                                & "  loc_pjc_id, " _
                                                & "  loc_is_id, " _
                                                & "  loc_active, " _
                                                & "  loc_eu_site, " _
                                                & "  loc_ptnr_id, " _
                                                & "  loc_address, " _
                                                & "  loc_reg_id, " _
                                                & "  loc_area_id, " _
                                                & "  loc_wh_id, " _
                                                & "  loc_type, " _
                                                & "  loc_cat, " _
                                                & "  loc_dt, " _
                                                & "  loc_internal_code, " _
                                                & "  loc_address_line1, " _
                                                & "  loc_address_line2, " _
                                                & "  loc_address_line3, " _
                                                & "  loc_telp, " _
                                                & "  loc_fax, " _
                                                & "  loc_contact_person, " _
                                                & "  loc_telp_contact_person " _
                                                & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(import_en_id.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetInteger(_id_loc) & ",  " _
                                                    & SetInteger(_id_si) & ",  " _
                                                    & SetSetringDB(gv_master.GetRowCellValue(i, "loc_code")) & ",  " _
                                                    & SetSetringDB(gv_master.GetRowCellValue(i, "loc_desc")) & ",  " _
                                                    & SetInteger(_loc_pjc_id) & ",  " _
                                                    & SetInteger(_loc_is_id) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_active")) & ",  " _
                                                    & SetSetring("Y") & ",  " _
                                                    & SetInteger(_ptnr_id) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_address")) & ",  " _
                                                    & SetInteger(_reg_id) & ",  " _
                                                    & SetInteger(_area_id) & ",  " _
                                                    & SetInteger(_wh_id) & ",  " _
                                                    & SetInteger(_type_id) & ",  " _
                                                    & SetInteger(_cat_id) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetSetring(_internal_code) & ", " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_address_line1")) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_address_line2")) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_address_line3")) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_telp")) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_fax")) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_contact_person")) & ",  " _
                                                    & SetSetring(gv_master.GetRowCellValue(i, "loc_telp_contact_person")) & " " _
                                                & ");"

                                _id_loc = _id_loc + 1
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
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
        gv_master.Columns("loc_code").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
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
