Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FPriceListHeader
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pi_oid_mstr As String
    Dim _now As DateTime

    Private Sub FPriceListHeader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pi_en_id.Properties.DataSource = dt_bantu
        pi_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pi_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pi_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_so_type())
        pi_so_type.Properties.DataSource = dt_bantu
        pi_so_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        pi_so_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        pi_so_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        pi_cu_id.Properties.DataSource = dt_bantu
        pi_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        pi_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        pi_cu_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()


        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", pi_en_id.EditValue))
        pi_ptnrg_id.Properties.DataSource = dt_bantu
        pi_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        pi_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        pi_ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_promo_mstr(pi_en_id.EditValue))
        pi_promo_id.Properties.DataSource = dt_bantu
        pi_promo_id.Properties.DisplayMember = dt_bantu.Columns("promo_desc").ToString
        pi_promo_id.Properties.ValueMember = dt_bantu.Columns("promo_id").ToString
        pi_promo_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sales_program(pi_en_id.EditValue))
        pi_sales_program.Properties.DataSource = dt_bantu
        pi_sales_program.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        pi_sales_program.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        pi_sales_program.ItemIndex = 0
    End Sub

    Private Sub pi_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pi_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Type", "pi_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Programe", "sales_program_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "pi_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "pi_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "pi_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "pi_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pi_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pi_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pi_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  pi_oid, " _
                    & "  pi_dom_id, " _
                    & "  pi_en_id, " _
                    & "  en_desc, " _
                    & "  pi_add_by, " _
                    & "  pi_add_date, " _
                    & "  pi_upd_by, " _
                    & "  pi_upd_date, " _
                    & "  pi_id, " _
                    & "  pi_code, " _
                    & "  pi_desc, " _
                    & "  pi_so_type, " _
                    & "  pi_promo_id, " _
                    & "  promo_desc, " _
                    & "  pi_cu_id, " _
                    & "  cu_name, " _
                    & "  pi_sales_program, " _
                    & "  code_name as sales_program_name, " _
                    & "  pi_start_date, " _
                    & "  pi_end_date, " _
                    & "  pi_active,pi_ptnrg_id,ptnrg_name, " _
                    & "  pi_dt " _
                    & "FROM  " _
                    & "  public.pi_mstr " _
                    & " inner join en_mstr on en_id = pi_en_id " _
                    & " inner join cu_mstr on cu_id = pi_cu_id " _
                    & " inner join promo_mstr on promo_id = pi_promo_id " _
                    & " inner join code_mstr on code_id = pi_sales_program " _
                    & "  left outer JOIN public.ptnrg_grp ON ptnrg_id = pi_ptnrg_id " _
                    & " where pi_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        pi_en_id.Focus()
        pi_en_id.ItemIndex = 0
        pi_code.Text = ""
        pi_desc.Text = ""
        pi_so_type.ItemIndex = 0
        pi_promo_id.ItemIndex = 0
        pi_cu_id.ItemIndex = 0
        pi_sales_program.ItemIndex = 0
        pi_start_date.DateTime = _now
        pi_end_date.DateTime = _now
        pi_active.EditValue = True
        pi_ptnrg_id.ItemIndex = 0

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pi_oid As Guid = Guid.NewGuid
        Dim i As Integer = 0
        Dim ssqls As New ArrayList

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
                                            & "  public.pi_mstr " _
                                            & "( " _
                                            & "  pi_oid, " _
                                            & "  pi_dom_id, " _
                                            & "  pi_en_id, " _
                                            & "  pi_add_by, " _
                                            & "  pi_add_date, " _
                                            & "  pi_id, " _
                                            & "  pi_code, " _
                                            & "  pi_desc, " _
                                            & "  pi_so_type, " _
                                            & "  pi_promo_id, " _
                                            & "  pi_cu_id, " _
                                            & "  pi_sales_program, " _
                                            & "  pi_start_date, " _
                                            & "  pi_end_date,pi_ptnrg_id, " _
                                            & "  pi_active, " _
                                            & "  pi_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pi_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pi_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("pi_mstr", pi_en_id.GetColumnValue("en_code"), "pi_id", "pi_en_id", pi_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(pi_code.Text) & ",  " _
                                            & SetSetring(pi_desc.Text) & ",  " _
                                            & SetSetring(pi_so_type.EditValue) & ",  " _
                                            & SetInteger(pi_promo_id.EditValue) & ",  " _
                                            & SetInteger(pi_cu_id.EditValue) & ",  " _
                                            & SetInteger(pi_sales_program.EditValue) & ",  " _
                                            & SetDate(pi_start_date.DateTime) & ",  " _
                                            & SetDate(pi_end_date.DateTime) & ",  " _
                                             & SetInteger(pi_ptnrg_id.EditValue) & ",  " _
                                            & SetBitYN(pi_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        .Command.Commit()
                        after_success()
                        set_row(_pi_oid.ToString, "pi_oid")
                        insert = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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

        Dim ssqls As New ArrayList

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
                            .Command.CommandText = "delete from pi_mstr where pi_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pi_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                Next
                            End If

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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pi_oid_mstr = .Item("pi_oid")
                pi_en_id.EditValue = .Item("pi_en_id")
                pi_code.Text = .Item("pi_code")
                pi_desc.Text = SetString(.Item("pi_desc"))
                pi_so_type.EditValue = .Item("pi_so_type")
                pi_promo_id.EditValue = .Item("pi_promo_id")
                pi_cu_id.EditValue = .Item("pi_cu_id")
                pi_sales_program.EditValue = .Item("pi_sales_program")
                pi_start_date.DateTime = .Item("pi_start_date")
                pi_end_date.DateTime = .Item("pi_end_date")
                pi_active.EditValue = SetBitYNB(.Item("pi_active"))
                pi_ptnrg_id.EditValue = .Item("pi_ptnrg_id")
            End With

            pi_en_id.Focus()

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim i As Integer = 0
        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.pi_mstr   " _
                                            & "SET  " _
                                            & "  pi_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pi_en_id = " & SetInteger(pi_en_id.EditValue) & ",  " _
                                            & "  pi_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pi_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & "  pi_code = " & SetSetring(pi_code.Text) & ",  " _
                                            & "  pi_desc = " & SetSetring(pi_desc.Text) & ",  " _
                                            & "  pi_so_type = " & SetSetring(pi_so_type.EditValue) & ",  " _
                                            & "  pi_promo_id = " & SetInteger(pi_promo_id.EditValue) & ",  " _
                                             & "  pi_ptnrg_id = " & SetInteger(pi_ptnrg_id.EditValue) & ",  " _
                                            & "  pi_cu_id = " & SetInteger(pi_cu_id.EditValue) & ",  " _
                                            & "  pi_sales_program = " & SetInteger(pi_sales_program.EditValue) & ",  " _
                                            & "  pi_start_date = " & SetDate(pi_start_date.DateTime) & ",  " _
                                            & "  pi_end_date = " & SetDate(pi_end_date.DateTime) & ",  " _
                                            & "  pi_active = " & SetBitYN(pi_active.EditValue) & ",  " _
                                            & "  pi_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pi_oid = " & SetSetring(_pi_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        .Command.Commit()
                        after_success()
                        set_row(_pi_oid_mstr, "pi_oid")
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

End Class
