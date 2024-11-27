Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FPartnerGroup

    Dim _ptnrg_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FPartnerGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()

        
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        ptnrg_en_id.Properties.DataSource = dt_bantu
        ptnrg_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ptnrg_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ptnrg_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ptnrg_en_id.EditValue, "creditterms_mstr"))
        ptnrg_credit_term.Properties.DataSource = dt_bantu
        ptnrg_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ptnrg_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ptnrg_credit_term.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ptnrg_en_id.EditValue, "payment_methode"))
        ptnrg_payment_methode.Properties.DataSource = dt_bantu
        ptnrg_payment_methode.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ptnrg_payment_methode.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ptnrg_payment_methode.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        ar_ac_id.Properties.DataSource = dt_bantu
        ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ar_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        ap_ac_id.Properties.DataSource = dt_bantu
        ap_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ap_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ap_ac_id.ItemIndex = 0


        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_creditterms_mstr(ptnrg_en_id.EditValue))
        'ptnrg_credit_term.Properties.DataSource = dt_bantu
        'ptnrg_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'ptnrg_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'ptnrg_credit_term.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ptnrg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ptnrg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Account Code", "ac_code_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Account Name", "ac_name_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Code", "ac_code_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Name", "ac_name_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Account", "ptnrg_ap_acc_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Methode", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Limit Credit", "ptnrg_limit_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Is Active", "ptnrg_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptnrg_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnrg_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnrg_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnrg_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ptnrg_oid, " _
                    & "  ptnrg_dom_id, " _
                    & "  ptnrg_en_id, " _
                    & "  en_code, " _
                    & "  ptnrg_add_by, " _
                    & "  ptnrg_add_date, " _
                    & "  ptnrg_upd_by, " _
                    & "  ptnrg_upd_date, " _
                    & "  ptnrg_id, " _
                    & "  ptnrg_code, " _
                    & "  ptnrg_name, " _
                    & "  ptnrg_desc, " _
                    & "  ptnrg_desc, " _
                    & "  ptnrg_credit_term, " _
                    & "  ptnrg_ar_ac_id, " _
                    & "  ptnrg_ap_ac_id, " _
                    & "  credit_term.code_name as credit_term_name, " _
                    & "  ptnrg_payment_methode, " _
                    & "  pay_method.code_name as pay_method_name, " _
                    & "  ptnrg_limit_credit, " _
                    & "  ptnrg_active, " _
                    & "  ptnrg_dt, " _
                    & "  ac_mstr_ar.ac_code as ac_code_ar, " _
                    & "  ac_mstr_ar.ac_name as ac_name_ar, " _
                    & "  ac_mstr_ap.ac_code as ac_code_ap, " _
                    & "  ac_mstr_ap.ac_name as ac_name_ap " _
                    & " FROM  " _
                    & " public.ptnrg_grp" _
                    & " inner join public.en_mstr on en_id = ptnrg_en_id" _
                    & " inner join public.ac_mstr on ac_id = ptnrg_id" _
                    & " inner join code_mstr credit_term on credit_term.code_id = ptnrg_credit_term " _
                    & " inner join code_mstr pay_method on pay_method.code_id = ptnrg_payment_methode " _
                    & " INNER JOIN public.ac_mstr ac_mstr_ap ON ptnrg_ap_ac_id = ac_mstr_ap.ac_id " _
                    & " INNER JOIN public.ac_mstr ac_mstr_ar ON ptnrg_ar_ac_id = ac_mstr_ar.ac_id " _
                    & " where ptnrg_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ptnrg_en_id.Focus()
        ptnrg_en_id.ItemIndex = 0
        ptnrg_code.Text = ""
        ptnrg_name.Text = ""
        ptnrg_desc.Text = ""
        ptnrg_payment_methode.Text = ""
        ptnrg_limit_credit.Text = ""
        ptnrg_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnrg_oid As Guid
        _ptnrg_oid = Guid.NewGuid
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
                                            & "  public.ptnrg_grp " _
                                            & "( " _
                                            & "  ptnrg_oid, " _
                                            & "  ptnrg_dom_id, " _
                                            & "  ptnrg_en_id, " _
                                            & "  ptnrg_add_by, " _
                                            & "  ptnrg_add_date, " _
                                            & "  ptnrg_id, " _
                                            & "  ptnrg_code, " _
                                            & "  ptnrg_name, " _
                                            & "  ptnrg_desc, " _
                                            & "  ptnrg_ar_ac_id, " _
                                            & "  ptnrg_ap_ac_id, " _
                                            & "  ptnrg_credit_term, " _
                                            & "  ptnrg_payment_methode, " _
                                            & "  ptnrg_limit_credit, " _
                                            & "  ptnrg_active, " _
                                            & "  ptnrg_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnrg_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ptnrg_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("ptnrg_grp", ptnrg_en_id.GetColumnValue("en_code"), "ptnrg_id", "ptnrg_en_id", ptnrg_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(ptnrg_code.Text) & ",  " _
                                            & SetSetring(ptnrg_name.Text) & ",  " _
                                            & SetSetring(ptnrg_desc.Text) & ",  " _
                                            & SetInteger(ptnrg_credit_term.EditValue) & ",  " _
                                            & SetInteger(ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(ap_ac_id.EditValue) & ",  " _
                                            & SetInteger(ptnrg_payment_methode.EditValue) & ",  " _
                                            & SetDbl(ptnrg_limit_credit.EditValue) & ",  " _
                                            & SetBitYN(ptnrg_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ");"
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
                        set_row(Trim(ptnrg_code.Text), "ptnrg_code")
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

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnrg_oid = .Item("ptnrg_oid")
                ptnrg_en_id.EditValue = .Item("ptnrg_en_id")
                ptnrg_code.Text = .Item("ptnrg_code")
                ptnrg_name.Text = .Item("ptnrg_name")
                ptnrg_desc.Text = .Item("ptnrg_desc")
                ar_ac_id.EditValue = .Item("ptnrg_ar_ac_id")
                ap_ac_id.EditValue = .Item("ptnrg_ap_ac_id")
                ptnrg_credit_term.EditValue = .Item("ptnrg_credit_term")
                ptnrg_payment_methode.EditValue = .Item("ptnrg_payment_methode")
                ptnrg_limit_credit.EditValue = .Item("ptnrg_limit_credit")
                ptnrg_active.EditValue = IIf(.Item("ptnrg_active") = "Y", True, False)
            End With
            ptnrg_en_id.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
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
                                            & "  public.ptnrg_grp   " _
                                            & "SET  " _
                                            & "  ptnrg_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  ptnrg_en_id = " & SetInteger(ptnrg_en_id.EditValue) & ",  " _
                                            & "  ptnrg_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ptnrg_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ptnrg_code = " & SetSetring(ptnrg_code.Text) & ",  " _
                                            & "  ptnrg_name = " & SetSetring(ptnrg_name.Text) & ",  " _
                                            & "  ptnrg_desc = " & SetSetring(ptnrg_desc.Text) & ",  " _
                                            & "  ptnrg_ar_ac_id = " & SetInteger(ar_ac_id.EditValue) & ",  " _
                                            & "  ptnrg_ap_ac_id = " & SetInteger(ap_ac_id.EditValue) & ",  " _
                                            & "  ptnrg_credit_term = " & SetInteger(ptnrg_credit_term.EditValue) & ",  " _
                                            & "  ptnrg_payment_methode = " & SetInteger(ptnrg_payment_methode.EditValue) & ",  " _
                                            & "  ptnrg_limit_credit = " & SetDbl(ptnrg_limit_credit.EditValue) & ",  " _
                                            & "  ptnrg_active = " & SetBitYN(ptnrg_active.EditValue) & ",  " _
                                            & "  ptnrg_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ptnrg_oid = " & SetSetring(_ptnrg_oid.ToString) & " "

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
                        set_row(Trim(ptnrg_code.Text), "ptnrg_code")
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
                            .Command.CommandText = "delete from ptnrg_grp where ptnrg_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnrg_oid") + "'"
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
End Class
