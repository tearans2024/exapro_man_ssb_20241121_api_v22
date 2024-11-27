Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDepartment

    Dim _dpt_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
   
    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

    End Sub

    Public Overrides Sub load_cb()

        init_le(dpt_en_id, "en_mstr")

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "dpt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "dpt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Labor Capacity", "dpt_lbr_cap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "dpt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "dpt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "dpt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "dpt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Active", "dpt_active", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  a.dpt_oid, " _
                    & "  a.dpt_dom_id, " _
                    & "  a.dpt_en_id, " _
                    & "  a.dpt_add_by, " _
                    & "  a.dpt_add_date, " _
                    & "  a.dpt_upd_by, " _
                    & "  a.dpt_upd_date, " _
                    & "  a.dpt_id, " _
                    & "  a.dpt_code, " _
                    & "  a.dpt_desc, " _
                    & "  a.dpt_lbr_cap, " _
                    & "  a.dpt_active, " _
                    & "  a.dpt_dt, " _
                    & "  b.en_desc, " _
                    & "  b.en_code, b.en_id " _
                    & "FROM " _
                    & "  public.dpt_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.dpt_en_id = b.en_id) " _
                    & " Where a.dpt_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " ORDER BY dpt_code "

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        dpt_en_id.Focus()
        dpt_en_id.ItemIndex = 0
        sc_ce_dpt_active.EditValue = True
        dpt_lbr_cap.Text = "0"
        dpt_desc.Text = ""

    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        If required(dpt_en_id, "Entity") = False Then
            Return False
            Exit Function
        End If
        If required(dpt_desc, "Description") = False Then
            Return False
            Exit Function
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _dpt_oid As Guid
        _dpt_oid = Guid.NewGuid

        Dim ssqls As New ArrayList
        Dim _dpt_code As String = GetNewNumberYM("dpt_mstr", "dpt_code", 5, "DPT" & dpt_en_id.GetColumnValue("en_code") _
                                       & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        Dim _dpt_id As Integer = GetNewIDSvrCode(dpt_en_id.GetColumnValue("en_code"), "dpt_mstr", "dpt_id")

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
                                            & "  public.dpt_mstr " _
                                            & "( " _
                                            & "  dpt_oid, " _
                                            & "  dpt_dom_id, " _
                                            & "  dpt_en_id, " _
                                            & "  dpt_add_by, " _
                                            & "  dpt_add_date, " _
                                            & "  dpt_id, " _
                                            & "  dpt_code, " _
                                            & "  dpt_desc, " _
                                            & "  dpt_lbr_cap, " _
                                            & "  dpt_active, " _
                                            & "  dpt_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_dpt_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(dpt_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetInteger(_dpt_id) & ",  " _
                                            & SetSetring(_dpt_code) & ",  " _
                                            & SetSetring(dpt_desc.Text) & ",  " _
                                            & SetInteger(dpt_lbr_cap.Text) & ",  " _
                                            & SetBitYN(sc_ce_dpt_active.EditValue) & ",  " _
                                            & "  " & SetDate(master_new.PGSqlConn.CekTanggal) & ") "


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        after_success()
                        set_row(Trim(_dpt_oid.ToString), "dpt_oid")
                        insert = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            dpt_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _dpt_oid = .Item("dpt_oid")
                dpt_desc.Text = SetString(.Item("dpt_desc"))
                dpt_lbr_cap.Text = .Item("dpt_lbr_cap")
                dpt_en_id.EditValue = .Item("dpt_en_id")
                sc_ce_dpt_active.EditValue = SetBitYNB(.Item("dpt_active"))
            End With

            edit_data = True
        End If
    End Function
    Public Overrides Function edit()
        edit = True

        Dim ssqls As New ArrayList

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
                                            & "  public.dpt_mstr   " _
                                            & "SET  " _
                                            & "  dpt_en_id = " & SetSetring(dpt_en_id.EditValue) & ",  " _
                                            & "  dpt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  dpt_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  dpt_desc = " & SetSetring(dpt_desc.Text) & ",  " _
                                            & "  dpt_lbr_cap = " & SetInteger(dpt_lbr_cap.Text) & ",  " _
                                            & "  dpt_active = " & SetBitYN(sc_ce_dpt_active.EditValue) & ",  " _
                                            & "  dpt_dt = " & SetDate(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "WHERE  " _
                                            & "  dpt_oid = '" + _dpt_oid + "' "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        after_success()
                        edit = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
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
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from dpt_mstr where dpt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dpt_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If MyPGDll.PGSqlConn.status_sync = True Then
                                For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function
End Class




