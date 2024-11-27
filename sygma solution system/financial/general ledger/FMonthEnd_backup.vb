﻿Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FMonthEnd
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssql As String
    Private Sub FMonthEnd_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        ssql = "select count(*) as jml from conf_file where conf_name='rec_gl_bal' and conf_value='1'"
        Try
            If GetRowInfo(ssql)(0) = 0 Then
                btRecountGLBal.Visible = False
                CeCalcOpen.Visible = False
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
        
    End Sub

    Public Overrides Sub load_cb()
       
        init_le(le_entity, "en_mstr")
        init_le(le_glperiode, "gcal_mstr")
    End Sub

    Private Function before_close() As Boolean
        before_close = True

        Dim _jml As Integer
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(glt_posted) as jml from glt_det where glt_posted ~~* 'N' " + _
                                           " and glt_en_id = " + le_entity.EditValue.ToString + _
                                           " and glt_date <= '" + le_glperiode.GetColumnValue("gcal_end_date") + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _jml = .DataReader("jml")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _jml > 0 Then
            MessageBox.Show("Unposted Transaction Still Exist.." + Chr(13) + "Please Transaction Post To All Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_close
    End Function

    Private Sub sb_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MessageBox.Show("Close Transaction For This Periode...", "Question?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        'pengecekan jurnal yang belum posting
        If before_close() = False Then
            'Exit Sub
            If DevExpress.XtraEditors.XtraMessageBox.Show("Do you want to continue?", "Confirmation ...", _
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
        End If

        Cursor = Cursors.WaitCursor
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Dim ssqls As New ArrayList

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  glbal_oid, " _
                        & "  glbal_dom_id, " _
                        & "  glbal_en_id, " _
                        & "  glbal_add_by, " _
                        & "  glbal_add_date, " _
                        & "  glbal_upd_by, " _
                        & "  glbal_upd_date, " _
                        & "  glbal_gcal_oid, " _
                        & "  glbal_ac_id, " _
                        & "  glbal_sb_id, " _
                        & "  glbal_cc_id, " _
                        & "  glbal_cu_id, " _
                        & "  glbal_balance_open, " _
                        & "  coalesce(glbal_balance_unposted,0) as glbal_balance_unposted , " _
                        & "  coalesce(glbal_balance_posted,0) as glbal_balance_posted, " _
                        & "  glbal_dt " _
                        & "FROM  " _
                        & "  public.glbal_balance" _
                        & " where glbal_gcal_oid = '" + le_glperiode.EditValue.ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Mencari gl calendar berikutnya ********************************
        Dim _next_date As Date
        Dim _gcal_oid As String = ""
        _next_date = le_glperiode.GetColumnValue("gcal_end_date")
        _next_date = _next_date.AddDays(1)

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_oid from gcal_mstr " + _
                                           " where gcal_start_date <='" + _next_date + "'" + _
                                           " and gcal_end_date >='" + _next_date + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows Then
                        While .DataReader.Read
                            _gcal_oid = .DataReader("gcal_oid").ToString
                        End While
                    Else
                        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _next_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        '***************************************************************

        Dim _glbal_oid As String = ""

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
                                            & "  public.gcald_det   " _
                                            & "SET  " _
                                            & "  gcald_ap = 'Y', " _
                                            & "  gcald_ar = 'Y', " _
                                            & "  gcald_fa = 'Y', " _
                                            & "  gcald_ic = 'Y', " _
                                            & "  gcald_so = 'Y', " _
                                            & "  gcald_gl = 'Y', " _
                                            & "  gcald_year = 'N', " _
                                            & "  gcal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE gcald_gcal_oid = '" & le_glperiode.EditValue.ToString & "' " _
                                            & " and gcald_en_id = " & le_entity.EditValue.ToString

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            Try
                                Using objcb As New master_new.WDABasepgsql("", "")
                                    With objcb
                                        .Connection.Open()
                                        .Command = .Connection.CreateCommand
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "select glbal_oid from glbal_balance" + _
                                                               " where glbal_en_id = " + le_entity.EditValue.ToString + _
                                                               " and glbal_ac_id = " + ds_bantu.Tables(0).Rows(i).Item("glbal_ac_id").ToString + _
                                                               " and glbal_sb_id = " + ds_bantu.Tables(0).Rows(i).Item("glbal_sb_id").ToString + _
                                                               " and glbal_cc_id = " + ds_bantu.Tables(0).Rows(i).Item("glbal_cc_id").ToString + _
                                                               " and glbal_gcal_oid = '" + _gcal_oid.ToString + "'"
                                        .InitializeCommand()
                                        .DataReader = .Command.ExecuteReader
                                        If .DataReader.HasRows Then
                                            While .DataReader.Read
                                                _glbal_oid = .DataReader("glbal_oid").ToString
                                            End While
                                        Else
                                            _glbal_oid = ""
                                        End If

                                    End With
                                End Using
                            Catch ex As Exception
                                MessageBox.Show(ex.Message)
                                sqlTran.Rollback()
                                Exit Sub
                            End Try

                            If _glbal_oid = "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.glbal_balance " _
                                                    & "( " _
                                                    & "  glbal_oid, " _
                                                    & "  glbal_dom_id, " _
                                                    & "  glbal_en_id, " _
                                                    & "  glbal_add_by, " _
                                                    & "  glbal_add_date, " _
                                                    & "  glbal_gcal_oid, " _
                                                    & "  glbal_ac_id, " _
                                                    & "  glbal_sb_id, " _
                                                    & "  glbal_cc_id, " _
                                                    & "  glbal_cu_id, " _
                                                    & "  glbal_balance_open, " _
                                                    & "  glbal_balance_unposted, " _
                                                    & "  glbal_balance_posted, " _
                                                    & "  glbal_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_gcal_oid.ToString) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_ac_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_sb_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_cc_id")) & ",  " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("glbal_cu_id")) & ",  " _
                                                    & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glbal_balance_open") + ds_bantu.Tables(0).Rows(i).Item("glbal_balance_posted")) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & " public.glbal_balance " _
                                                    & "SET  " _
                                                    & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                    & "  glbal_balance_open = " & SetDbl(ds_bantu.Tables(0).Rows(i).Item("glbal_balance_open") + ds_bantu.Tables(0).Rows(i).Item("glbal_balance_posted")) & ", " _
                                                    & "  glbal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  glbal_oid = " & SetSetring(_glbal_oid) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        If MyPGDll.PGSqlConn.status_sync = True Then
                            For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        MessageBox.Show("Transaction This Periode Have Been Closed..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Cursor = Cursors.Arrow
    End Sub

    Private Sub BtPraClosing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPraClosing.Click
        Try

            Dim ssqls As New ArrayList

            'step 1
            If le_entity.EditValue = 0 Then
                ssql = "select en_id,en_desc from en_mstr where en_id<>0 and en_active='Y' order by en_id "
            Else
                ssql = "select en_id,en_desc from en_mstr where en_id=" & le_entity.EditValue & " order by en_id "
                If ce_all_child.EditValue = True Then
                    ssql = "select en_id,en_code,en_desc from en_mstr where (en_parent=" & le_entity.EditValue _
                        & " or en_id=" & le_entity.EditValue & ") and en_active='Y' order by en_code"
                End If
            End If


            Dim dt As New DataTable
            dt = GetTableData(ssql)

            'pemindahan dr biaya operasional ke kepala 5
            For Each dr As DataRow In dt.Rows
                If move_biaya_operasional(ssqls, dr("en_id"), 1) = False Then
                    Exit Sub
                End If
            Next


            If ask("Anda yakin akan memproses step 1 (pemindahan kepala 6 ke 51)?", "Konfirmasi...") Then
                'SaveArraytoFile(ssqls)
                'DbRunTran(ssqls)

                If MyPGDll.PGSqlConn.status_sync = True Then
                    'DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "")
                    If DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "") = False Then
                        'Return False
                        Exit Sub
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        'Return False
                        Exit Sub
                    End If
                    ssqls.Clear()
                End If
                Box("Proses Step 1 sucess")

            Else
                Exit Sub
            End If


            'pemindahan dr kepala 51 ke 1012001 (wip)
            For Each dr As DataRow In dt.Rows
                If move_biaya_operasional(ssqls, dr("en_id"), 2) = False Then
                    Exit Sub
                End If
            Next

            If ask("Anda yakin akan memproses step 2 (pemindahan kepala 51 ke WIP)?", "Konfirmasi...") Then
                If MyPGDll.PGSqlConn.status_sync = True Then
                    'DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "")
                    If DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "") = False Then
                        'Return False
                        Exit Sub
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        'Return False
                        Exit Sub
                    End If
                    ssqls.Clear()
                End If
                Box("Proses Step 2 sucess")
            Else
                Exit Sub
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Function move_biaya_operasional(ByVal _ssqls As ArrayList, ByVal _en As String, ByVal _step As Integer) As Boolean
        Try
            Dim _level As Integer = 2
            Dim _dom As Integer = 1
            Dim _glt_code As String

            ssql = "SELECT  " _
            & "  a.end_oid, " _
            & "  a.end_ac_id_from, " _
            & "  b.ac_code AS ac_code_from, " _
            & "  b.ac_name AS ac_name_from, " _
            & "  b.ac_code_hirarki AS ac_code_hirarki_from, " _
            & "  a.end_ac_id_to, " _
            & "  c.ac_code AS ac_code_to, " _
            & "  c.ac_name AS ac_name_to, " _
            & "  c.ac_code_hirarki AS ac_code_hirarki_to, " _
            & "  a.end_step, " _
            & "  a.end_seq, " _
            & "  a.end_remark, " _
            & "  b.ac_sign AS ac_sign_from, " _
            & "  b.ac_cu_id AS ac_cu_id_from, " _
            & "  c.ac_cu_id AS ac_cu_id_to, " _
            & "  c.ac_sign AS ac_sign_to " _
            & "FROM " _
            & "  public.tconfsettingendmoth a " _
            & "  INNER JOIN public.ac_mstr b ON (a.end_ac_id_from = b.ac_id) " _
            & "  INNER JOIN public.ac_mstr c ON (a.end_ac_id_to = c.ac_id) " _
            & "ORDER BY " _
            & "  a.end_step, " _
            & "  a.end_seq"

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            Dim _en_code As String
            _en_code = GetRowInfo("select en_code from en_mstr where en_id=" & _en)(0)

            _glt_code = func_coll.get_transaction_number("JP", _en_code, "glt_det", "glt_code")

            For Each dr As DataRow In dt.Rows
                If dr("end_step") = _step Then
                    'pemindahan dari 6 ke 51
                    ssql = "SELECT  " _
                        & "  a.ac_id, " _
                        & "  a.ac_code, " _
                        & "  a.ac_name, " _
                        & "  a.ac_sign, " _
                        & "  a.ac_level, " _
                        & "  a.ac_code_hirarki,a.ac_cu_id, " _
                        & "  a.ac_is_sumlevel,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                        & ", " & _en & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                        & "FROM " _
                        & "  public.ac_mstr a " _
                        & "  where substring(a.ac_code_hirarki,1," & Len(dr("ac_code_hirarki_from")) & ") ='" _
                        & dr("ac_code_hirarki_from") & "' and a.ac_is_sumlevel='N' and a.ac_active='Y'"

                    Dim dt_step_1 As New DataTable
                    dt_step_1 = GetTableData(ssql)


                    Dim i As Integer = 0
                    For Each dr_step_1 As DataRow In dt_step_1.Rows
                        'lakukan jurnal
                        If dr_step_1("ac_value") <> 0 Then

                            'insert debet (akun tujuan)
                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(_en) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JP") & ",  " _
                             & SetInteger(dr("ac_cu_id_to")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i) & ",  " _
                             & SetIntegerDB(dr("end_ac_id_to")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment" & " " & dr("ac_name_to") & " " & GetIDByName("en_mstr", "en_desc", "en_id", _en)) & ",  " _
                             & SetDbl(dr_step_1("ac_value")) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            _ssqls.Add(ssql)

                            'update gl bal
                            If update_gl_bal(_ssqls, le_glperiode.EditValue, _en, dr("end_ac_id_to"), "D", dr_step_1("ac_value"), _
                                             dr("ac_cu_id_to"), func_data.get_exchange_rate(dr("ac_cu_id_to"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then
                                Return False
                                Exit Function
                            End If

                            'insert kredit
                            ssql = "INSERT INTO  " _
                              & "  public.glt_det " _
                              & "( " _
                              & "  glt_oid, " _
                              & "  glt_dom_id, " _
                              & "  glt_en_id, " _
                              & "  glt_add_by, " _
                              & "  glt_add_date, " _
                              & "  glt_code, " _
                              & "  glt_date, " _
                              & "  glt_type, " _
                              & "  glt_cu_id, " _
                              & "  glt_exc_rate, " _
                              & "  glt_seq, " _
                              & "  glt_ac_id, " _
                              & "  glt_sb_id, " _
                              & "  glt_cc_id, " _
                              & "  glt_desc, " _
                              & "  glt_debit, " _
                              & "  glt_credit, " _
                              & "  glt_ref_trans_code, " _
                              & "  glt_daybook, " _
                              & "  glt_posted, " _
                              & "  glt_dt " _
                              & ")  " _
                              & "VALUES ( " _
                              & SetSetring(Guid.NewGuid.ToString) & ",  " _
                              & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                              & SetInteger(_en) & ",  " _
                              & SetSetring(master_new.ClsVar.sNama) & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                              & SetSetring(_glt_code) & ",  " _
                              & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                              & SetSetring("JP") & ",  " _
                              & SetInteger(dr_step_1("ac_cu_id")) & ",  " _
                              & SetDbl(1) & ",  " _
                              & SetInteger(i + 1) & ",  " _
                              & SetIntegerDB(dr_step_1("ac_id")) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetSetringDB("End Month Adjusment" & " " & dr_step_1("ac_name") & " " & GetIDByName("en_mstr", "en_desc", "en_id", _en)) & ",  " _
                              & SetDbl(0) & ",  " _
                              & SetDbl(dr_step_1("ac_value")) & ",  " _
                              & SetSetring("") & ",  " _
                              & SetSetring("MONTH END") & ",  " _
                              & SetSetring("Y") & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                              & ")"

                            _ssqls.Add(ssql)

                            'update glbal
                            If update_gl_bal(_ssqls, le_glperiode.EditValue, _en, dr_step_1("ac_id"), "C", dr_step_1("ac_value"), _
                                             dr_step_1("ac_cu_id"), func_data.get_exchange_rate(dr_step_1("ac_cu_id"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then
                                Return False
                                Exit Function
                            End If

                            i += 2
                        End If

                        ' _glt_code = Microsoft.VisualBasic.Left(_glt_code, Len(_glt_code) - 5) & Format(CInt(Microsoft.VisualBasic.Right(_glt_code, 5)) + 1, "00000")
                    Next
                End If
            Next
            Return True

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function
    Function update_gl_bal(ByVal _ssqls As ArrayList, ByVal _gcal_oid As String, ByVal _en_id As String, _
                           ByVal _ac_id As String, ByVal _par_sign As String, ByVal _glt_value As Double, _
                           ByVal _cu_id As String, ByVal _exc_rate As Double) As Boolean
        Try
            ssql = "select ac_sign, ac_cu_id from ac_mstr where ac_id = " + _ac_id
            Dim _ac_cu_id As Integer = GetRowInfo(ssql)(1)


            If _par_sign <> GetRowInfo(ssql)(0) Then
                _glt_value = _glt_value * -1
            End If


            If _cu_id = master_new.ClsVar.ibase_cur_id Then
                If _ac_cu_id <> _cu_id Then
                    _glt_value = _glt_value / func_data.get_exchange_rate(_ac_cu_id)
                End If
            Else
                If _ac_cu_id <> _cu_id Then
                    _glt_value = _glt_value * _exc_rate
                End If
            End If

            ssql = "select glbal_oid from glbal_balance" + _
                   " where glbal_en_id = " + _en_id + _
                   " and glbal_ac_id = " + _ac_id + _
                   " and glbal_gcal_oid = '" + _gcal_oid + "'"

            Dim dt_glbal As New DataTable
            dt_glbal = GetTableData(ssql)

            If dt_glbal.Rows.Count = 0 Then
                'Box("Opening Balance for accout " & GetIDByName("ac_mstr", "ac_name", "ac_id", _ac_id) & " is empty")
                ssql = "INSERT INTO  " _
                    & "  public.glbal_balance " _
                    & "( " _
                    & "  glbal_oid, " _
                    & "  glbal_dom_id, " _
                    & "  glbal_en_id, " _
                    & "  glbal_add_by, " _
                    & "  glbal_add_date, " _
                    & "  glbal_gcal_oid, " _
                    & "  glbal_ac_id, " _
                    & "  glbal_sb_id, " _
                    & "  glbal_cc_id, " _
                    & "  glbal_cu_id, " _
                    & "  glbal_balance_open, " _
                    & "  glbal_balance_unposted, " _
                    & "  glbal_balance_posted, " _
                    & "  glbal_dt " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    & SetInteger(_en_id) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                    & SetSetring(_gcal_oid) & ",  " _
                    & SetInteger(_ac_id) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(0) & ",  " _
                    & SetInteger(_cu_id) & ",  " _
                    & SetDbl(0) & ",  " _
                    & SetDbl(0) & ",  " _
                    & SetDbl(0) & ",  " _
                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                    & ")"

                DbRun(ssql)
                ' _ssqls.Add(ssql)
                'Return False
            Else
                ssql = "UPDATE  " _
                   & "  public.glbal_balance   " _
                   & "SET  " _
                   & "  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                   & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "," _
                   & "  glbal_balance_posted = coalesce(glbal_balance_posted,0) + " & SetDbl(_glt_value) & ",  " _
                   & "  glbal_dt =   " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                   & "  " _
                   & "WHERE  " _
                   & "  glbal_oid = " & SetSetring(dt_glbal.Rows(0).Item(0)) & " "

                _ssqls.Add(ssql)
            End If


            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function
    Function move_lababerjalan(ByVal _ssqls As ArrayList, ByVal _en_id As String) As Boolean

        ssql = " SELECT  " _
               & "  a.glbal_balance_posted, " _
               & "  b.ac_sign, " _
               & "  b.ac_type, " _
               & "  a.glbal_ac_id " _
               & "FROM " _
               & "  public.ac_mstr b " _
               & "  INNER JOIN public.glbal_balance a ON (b.ac_id = a.glbal_ac_id) " _
               & "WHERE " _
               & "  a.glbal_gcal_oid = '" & le_glperiode.EditValue & "' AND  " _
               & "  coalesce(a.glbal_balance_posted, 0) <> 0 AND  " _
               & "  a.glbal_en_id = " & _en_id & " AND  b.ac_type IN ('R','E')"

        Dim dt_profit As New DataTable
        dt_profit = GetTableData(ssql)

        For Each dr_profit As DataRow In dt_profit.Rows

        Next

    End Function

    Private Sub btRecountGLBal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRecountGLBal.Click
        Try
            Dim ssqls As New ArrayList
            Dim level, dom, en As Integer

            dom = 0
            en = 0

            ssqls.Clear()
            ssql = "update glbal_balance set glbal_balance_unposted=0,glbal_balance_posted=0 where glbal_gcal_oid='" & le_glperiode.EditValue & "' "
            ssqls.Add(ssql)

            If le_entity.EditValue > 0 Then
                level = 2
                en = CInt(le_entity.EditValue)
            Else
                level = 2
                dom = 1
            End If

            If le_entity.EditValue = 0 Then
                ssql = "select en_id,en_desc from en_mstr where en_id<>0 order by en_id "
            Else
                ssql = "select en_id,en_desc from en_mstr where en_id=" & en & " order by en_id "
            End If

            Dim dt_en As New DataTable
            dt_en = GetTableData(ssql)
            Dim i As Integer = 0
            For Each dr_en As DataRow In dt_en.Rows
                If CeCalcOpen.EditValue = True Then
                    ssql = "select ac_id, f_get_glbal(ac_id," & level & "," & dom & "," & _
                          dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','N') as gl_unposted, f_get_glbal(ac_id," & level & "," & dom & "," & _
                          dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','Y') as gl_posted,f_get_glbal(ac_id," & level & "," & dom & "," & _
                          dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','O') as gl_open, ac_code_hirarki, ac_name, ac_sign,ac_is_sumlevel,ac_cu_id from ac_mstr " _
                          & "where ac_is_sumlevel='N' and ac_id<>0 order by ac_id"
                Else
                    ssql = "select ac_id, f_get_glbal(ac_id," & level & "," & dom & "," & _
                          dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','N') as gl_unposted, f_get_glbal(ac_id," & level & "," & dom & "," & _
                          dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','Y') as gl_posted,f_get_glbal(ac_id," & level & "," & dom & "," & _
                          dr_en("en_id") & ",'" & le_glperiode.EditValue.ToString & "','C') as gl_open,ac_code_hirarki, ac_name, ac_sign,ac_is_sumlevel,ac_cu_id from ac_mstr " _
                          & "where ac_is_sumlevel='N' and ac_id<>0 order by ac_id"
                End If

                Dim dt As New DataTable
                dt = GetTableData(ssql)
                i += 1
                btRecountGLBal.Text = "Recount GL Total = " & System.Math.Round(i / dt_en.Rows.Count * 100, 0) & " %"
                System.Windows.Forms.Application.DoEvents()

                For Each dr_ac As DataRow In dt.Rows

                    ssql = "select * from glbal_balance where glbal_en_id=" & dr_en("en_id") _
                        & " and glbal_gcal_oid=" & SetSetring(le_glperiode.EditValue.ToString) _
                        & " and glbal_ac_id=" & dr_ac("ac_id")

                    If CekRowSelect(ssql) > 0 Then
                        ssql = "update glbal_balance set glbal_balance_unposted=" & SetDec(dr_ac("gl_unposted")) _
                        & " , glbal_balance_posted=" & SetDec(dr_ac("gl_posted")) & " where glbal_en_id=" & dr_en("en_id") _
                        & " and glbal_gcal_oid=" & SetSetring(le_glperiode.EditValue.ToString) _
                        & " and glbal_ac_id=" & dr_ac("ac_id")

                    Else

                        ssql = "INSERT INTO  " _
                            & "  public.glbal_balance " _
                            & "( " _
                            & "  glbal_oid, " _
                            & "  glbal_dom_id, " _
                            & "  glbal_en_id, " _
                            & "  glbal_gcal_oid, " _
                            & "  glbal_ac_id, " _
                            & "  glbal_sb_id, " _
                            & "  glbal_cc_id, " _
                            & "  glbal_cu_id, " _
                            & "  glbal_balance_open, " _
                            & "  glbal_balance_unposted, " _
                            & "  glbal_balance_posted " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetInteger("1") & ",  " _
                            & SetInteger(dr_en("en_id")) & ",  " _
                            & SetSetring(le_glperiode.EditValue.ToString) & ",  " _
                            & SetInteger(dr_ac("ac_id")) & ",  " _
                            & SetInteger("0") & ",  " _
                            & SetInteger("0") & ",  " _
                            & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                            & SetDec(dr_ac("gl_open")) & ",  " _
                            & SetDec(dr_ac("gl_unposted")) & ",  " _
                            & SetDec(dr_ac("gl_posted")) & "  " _
                            & ")"
                    End If

                    ssqls.Add(ssql)
                Next

                'Dim xx As New ArrayList
                'xx = ssqls
                If DbRunTran(ssqls) = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Next
            btRecountGLBal.Text = "Recount GL Total = Execute "
            System.Windows.Forms.Application.DoEvents()
            'DbRunTran(ssqls)
            Box("Sukses")
            btRecountGLBal.Text = "Recount GL Total"
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged
        Try
            If (le_entity.EditValue = 1 Or le_entity.EditValue = 0) Then
                If ce_all_child.EditValue = True Then
                    ce_all_child.EditValue = False
                    ce_all_child.Enabled = False
                Else
                    ce_all_child.Enabled = False
                End If
            Else
                ce_all_child.Enabled = True
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sb_close_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_close.Click
        'proses pemindahan dari semua pendapatan dan beban ke laba berjalan
        'closing juga supaya transaksi tidak bisa diedit
        'proses pemindahan saldo akhir ke opening bulan berikutnya atau jika belum ada maka akan membuat gcal baru
        Dim _level As Integer = 2
        Dim _dom As Integer = 1
        Dim _glt_code As String


        ssql = "SELECT  " _
                & "  a.ac_id, " _
                & "  a.ac_code, " _
                & "  a.ac_name, " _
                & "  a.ac_type, " _
                & "  a.ac_is_sumlevel, " _
                & "  a.ac_sign, " _
                & "  a.ac_cu_id, " _
                & "  a.ac_level, " _
                & "  b.code " _
                & "FROM " _
                & "  public.ac_mstr a INNER JOIN " _
                & "  public.tconfsettingacc b  on (a.ac_id=CAST(b.setting as integer)) " _
                & "  where b.code='acc_profit_running'"

        Dim dr_laba_berjalan As DataRow
        dr_laba_berjalan = GetRowInfo(ssql)


        ssql = "SELECT  " _
                & "  a.ac_id, " _
                & "  a.ac_code, " _
                & "  a.ac_name, " _
                & "  a.ac_type, " _
                & "  a.ac_is_sumlevel, " _
                & "  a.ac_sign, " _
                & "  a.ac_cu_id, " _
                & "  a.ac_level, " _
                & "  b.code " _
                & "FROM " _
                & "  public.ac_mstr a INNER JOIN " _
                & "  public.tconfsettingacc b  on (a.ac_id=CAST(b.setting as integer)) " _
                & "  where b.code='acc_profit_hold'"

        Dim dr_laba_ditahan As DataRow
        dr_laba_ditahan = GetRowInfo(ssql)


        Try
            Dim ssqls As New ArrayList

            If le_entity.EditValue = 0 Then
                ssql = "select en_id,en_code,en_desc from en_mstr where en_id<>0 and en_active='Y' order by en_id "
            Else
                ssql = "select en_id,en_code,en_desc from en_mstr where en_id=" & le_entity.EditValue & " order by en_id "
                If ce_all_child.EditValue = True Then
                    ssql = "select en_id,en_code,en_desc from en_mstr where (en_parent=" & le_entity.EditValue _
                        & " or en_id=" & le_entity.EditValue & ") and en_active='Y' order by en_code"
                End If
            End If


            Dim dt As New DataTable
            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                ssql = "SELECT  " _
                    & "  a.ac_id, " _
                    & "  a.ac_code, " _
                    & "  a.ac_name, " _
                    & "  a.ac_type, " _
                    & "  a.ac_is_sumlevel, " _
                    & "  a.ac_sign, " _
                    & "  a.ac_cu_id, " _
                    & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                        & ", " & dr("en_id") & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                    & "FROM " _
                    & "  public.ac_mstr a " _
                    & "WHERE " _
                    & "  a.ac_type in ('E','R') and ac_active='Y' and ac_is_sumlevel='N' order by ac_type,ac_id"

                Dim dt_ac As New DataTable
                dt_ac = GetTableData(ssql)


                _glt_code = func_coll.get_transaction_number("JP", dr("en_code"), "glt_det", "glt_code")
                'pemindahan dari beban dan pendapatan ke laba berjalan
                Dim i As Integer = 0
                For Each dr_ac As DataRow In dt_ac.Rows
                    If dr_ac("ac_value") <> 0 Then
                        If dr_ac("ac_type") = "E" Then
                            'beban dulu yang disesuaikan
                            'insert debet (akun tujuan (laba berjalan))
                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(dr("en_id")) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JP") & ",  " _
                             & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i) & ",  " _
                             & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment 2") & ",  " _
                             & SetDbl(dr_ac("ac_value")) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            'Exit Sub
                            ssqls.Add(ssql)

                            'update gl bal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, dr("en_id"), dr_laba_berjalan("ac_id"), "D", dr_ac("ac_value"), _
                                             dr_laba_berjalan("ac_cu_id"), func_data.get_exchange_rate(dr_laba_berjalan("ac_cu_id"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then

                                Exit Sub
                            End If

                            'insert kredit
                            ssql = "INSERT INTO  " _
                              & "  public.glt_det " _
                              & "( " _
                              & "  glt_oid, " _
                              & "  glt_dom_id, " _
                              & "  glt_en_id, " _
                              & "  glt_add_by, " _
                              & "  glt_add_date, " _
                              & "  glt_code, " _
                              & "  glt_date, " _
                              & "  glt_type, " _
                              & "  glt_cu_id, " _
                              & "  glt_exc_rate, " _
                              & "  glt_seq, " _
                              & "  glt_ac_id, " _
                              & "  glt_sb_id, " _
                              & "  glt_cc_id, " _
                              & "  glt_desc, " _
                              & "  glt_debit, " _
                              & "  glt_credit, " _
                              & "  glt_ref_trans_code, " _
                              & "  glt_daybook, " _
                              & "  glt_posted, " _
                              & "  glt_dt " _
                              & ")  " _
                              & "VALUES ( " _
                              & SetSetring(Guid.NewGuid.ToString) & ",  " _
                              & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                              & SetInteger(dr("en_id")) & ",  " _
                              & SetSetring(master_new.ClsVar.sNama) & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                              & SetSetring(_glt_code) & ",  " _
                              & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                              & SetSetring("JP") & ",  " _
                              & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                              & SetDbl(1) & ",  " _
                              & SetInteger(i + 1) & ",  " _
                              & SetIntegerDB(dr_ac("ac_id")) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetIntegerDB(0) & ",  " _
                              & SetSetringDB("End Month Adjusment 2") & ",  " _
                              & SetDbl(0) & ",  " _
                              & SetDbl(dr_ac("ac_value")) & ",  " _
                              & SetSetring("") & ",  " _
                              & SetSetring("MONTH END") & ",  " _
                              & SetSetring("Y") & ",  " _
                              & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                              & ")"

                            ssqls.Add(ssql)

                            'update glbal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, dr("en_id"), dr_ac("ac_id"), "C", dr_ac("ac_value"), _
                                             dr_ac("ac_cu_id"), func_data.get_exchange_rate(dr_ac("ac_cu_id"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then
                                Exit Sub
                            End If
                        Else

                            'insert debet (akun tujuan (laba berjalan))
                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(dr("en_id")) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JP") & ",  " _
                             & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i + 1) & ",  " _
                             & SetIntegerDB(dr_ac("ac_id")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment 2") & ",  " _
                             & SetDbl(dr_ac("ac_value")) & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            ssqls.Add(ssql)

                            'update glbal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, dr("en_id"), dr_ac("ac_id"), "D", dr_ac("ac_value"), _
                                             dr_ac("ac_cu_id"), func_data.get_exchange_rate(dr_ac("ac_cu_id"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then
                                Exit Sub
                            End If


                            'insert kredit

                            ssql = "INSERT INTO  " _
                             & "  public.glt_det " _
                             & "( " _
                             & "  glt_oid, " _
                             & "  glt_dom_id, " _
                             & "  glt_en_id, " _
                             & "  glt_add_by, " _
                             & "  glt_add_date, " _
                             & "  glt_code, " _
                             & "  glt_date, " _
                             & "  glt_type, " _
                             & "  glt_cu_id, " _
                             & "  glt_exc_rate, " _
                             & "  glt_seq, " _
                             & "  glt_ac_id, " _
                             & "  glt_sb_id, " _
                             & "  glt_cc_id, " _
                             & "  glt_desc, " _
                             & "  glt_debit, " _
                             & "  glt_credit, " _
                             & "  glt_ref_trans_code, " _
                             & "  glt_daybook, " _
                             & "  glt_posted, " _
                             & "  glt_dt " _
                             & ")  " _
                             & "VALUES ( " _
                             & SetSetring(Guid.NewGuid.ToString) & ",  " _
                             & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                             & SetInteger(dr("en_id")) & ",  " _
                             & SetSetring(master_new.ClsVar.sNama) & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                             & SetSetring(_glt_code) & ",  " _
                             & SetDate(le_glperiode.GetColumnValue("gcal_end_date")) & ",  " _
                             & SetSetring("JP") & ",  " _
                             & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                             & SetDbl(1) & ",  " _
                             & SetInteger(i) & ",  " _
                             & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetIntegerDB(0) & ",  " _
                             & SetSetringDB("End Month Adjusment 2") & ",  " _
                             & SetDbl(0) & ",  " _
                             & SetDbl(dr_ac("ac_value")) & ",  " _
                             & SetSetring("") & ",  " _
                             & SetSetring("MONTH END") & ",  " _
                             & SetSetring("Y") & ",  " _
                             & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                             & ")"

                            ssqls.Add(ssql)

                            'update gl bal
                            If update_gl_bal(ssqls, le_glperiode.EditValue, dr("en_id"), dr_laba_berjalan("ac_id"), "C", dr_ac("ac_value"), _
                                             dr_laba_berjalan("ac_cu_id"), func_data.get_exchange_rate(dr_laba_berjalan("ac_cu_id"), _
                                             le_glperiode.GetColumnValue("gcal_end_date"))) = False Then

                                Exit Sub
                            End If
                        End If
                        i += 2
                    End If
                Next
            Next


            'pemindahan open + posted ke opening bulan berikutnya
            If ask("Anda yakin akan memproses laba berjalan?", "Konfirmasi...") Then
                'SaveArraytoFile(ssqls)

                If MyPGDll.PGSqlConn.status_sync = True Then
                    'DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "")
                    If DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "") = False Then
                        'Return False
                        Exit Sub
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        'Return False
                        Exit Sub
                    End If
                    ssqls.Clear()
                End If
                Box("Proses Step 1 sucess")

            Else
                Exit Sub
            End If

            'pemindahan open + posted ke opening bulan berikutnya
            'looping entity
            For Each dr As DataRow In dt.Rows
                ssql = "SELECT  " _
                   & "  a.ac_id, " _
                   & "  a.ac_code, " _
                   & "  a.ac_name, " _
                   & "  a.ac_type, " _
                   & "  a.ac_is_sumlevel, " _
                   & "  a.ac_sign, " _
                   & "  a.ac_cu_id, " _
                   & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                       & ", " & dr("en_id") & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                   & "FROM " _
                   & "  public.ac_mstr a " _
                   & "WHERE " _
                   & "  a.ac_type not in ('E','R') and ac_active='Y' and ac_is_sumlevel='N' and ac_id<>0 order by ac_type,ac_id"

                Dim dt_ac As New DataTable
                dt_ac = GetTableData(ssql)

                For Each dr_ac As DataRow In dt_ac.Rows
                    ssql = "select glbal_oid from glbal_balance where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                            & " and glbal_en_id=" & dr("en_id") & " and glbal_gcal_oid='" & le_glperiode.EditValue _
                            & "' and glbal_ac_id=" & dr_ac("ac_id")

                    If CekRowSelect(ssql) > 0 Then
                        ssql = "update glbal_balance set glbal_balance_open=coalesce(" & SetDec(dr_ac("ac_value")) _
                            & ",0),  glbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & "  glbal_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) _
                            & "where glbal_dom_id=" & master_new.ClsVar.sdom_id _
                            & " and glbal_en_id=" & dr("en_id") & " and glbal_gcal_oid='" & le_glperiode.EditValue _
                            & "' and glbal_ac_id=" & dr_ac("ac_id")

                        ssqls.Add(ssql)

                    Else
                        ssql = "INSERT INTO  " _
                                & "  public.glbal_balance " _
                                & "( " _
                                & "  glbal_oid, " _
                                & "  glbal_dom_id, " _
                                & "  glbal_en_id, " _
                                & "  glbal_add_by, " _
                                & "  glbal_add_date, " _
                                & "  glbal_gcal_oid, " _
                                & "  glbal_ac_id, " _
                                & "  glbal_sb_id, " _
                                & "  glbal_cc_id, " _
                                & "  glbal_cu_id, " _
                                & "  glbal_balance_open, " _
                                & "  glbal_balance_unposted, " _
                                & "  glbal_balance_posted, " _
                                & "  glbal_dt " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(dr("en_id")) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                & SetSetring(le_glperiode.EditValue) & ",  " _
                                & SetInteger(dr_ac("ac_id")) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(dr_ac("ac_cu_id")) & ",  " _
                                & SetDbl(dr_ac("ac_value")) & ",  " _
                                & SetDbl(0) & ",  " _
                                & SetDbl(0) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                & ")"

                        ssqls.Add(ssql)
                    End If
                Next
            Next


            'If ask("Anda yakin akan memproses step 2 (pemindahan kepala 51 ke WIP)?", "Konfirmasi...") Then
            If MyPGDll.PGSqlConn.status_sync = True Then
                'DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "")
                If DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    'Return False
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            Box("Proses Closing tahap pembuatan Opening Balance Sukses")
            'Else
            'Exit Sub
            'End If

            'perubahan status di gcal_det
            For Each dr As DataRow In dt.Rows
                ssql = "UPDATE  " _
                        & "  public.gcald_det   " _
                        & "SET  " _
                        & "  gcald_ap = 'Y', " _
                        & "  gcald_ar = 'Y', " _
                        & "  gcald_fa = 'Y', " _
                        & "  gcald_ic = 'Y', " _
                        & "  gcald_so = 'Y', " _
                        & "  gcald_gl = 'Y', " _
                        & "  gcald_year = 'N', " _
                        & "  gcal_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                        & "  " _
                        & "WHERE gcald_gcal_oid = '" & le_glperiode.EditValue.ToString & "' " _
                        & " and gcald_en_id = " & dr("en_id")

                ssqls.Add(ssql)
            Next


            'Dim tanggal_awal As Date
            'tanggal_awal = DateAdd(DateInterval.Day, 1, CDate(le_glperiode.GetColumnValue("gcal_end_date")))

            Dim dr_gcal As DataRow = get_next_gcal(le_glperiode.EditValue)

            'If get_next_gcal(le_glperiode.EditValue, dt_gcal) = False Then
            '    Box("Next GL Calender not found")
            '    Exit Sub
            'End If

            'pembuatan jurnal otomatis uktuk awal bulan
            For Each dr As DataRow In dt.Rows
                ssql = "SELECT  " _
                   & "  a.ac_id, " _
                   & "  a.ac_code, " _
                   & "  a.ac_name, " _
                   & "  a.ac_type, " _
                   & "  a.ac_is_sumlevel, " _
                   & "  a.ac_sign, " _
                   & "  a.ac_cu_id, " _
                   & "  a.ac_level,f_get_balance_sheet( ac_id, " & _level & ", " & _dom _
                       & ", " & dr("en_id") & ", cast('" & le_glperiode.EditValue & "' as uuid), 'Y') as ac_value " _
                   & "FROM " _
                   & "  public.ac_mstr a " _
                   & "WHERE " _
                   & "  ac_id=" & dr_laba_berjalan(0) & ""

                Dim dt_ac As New DataTable
                dt_ac = GetTableData(ssql)


                _glt_code = func_coll.get_transaction_number("JP", dr("en_code"), "glt_det", "glt_code")

                Dim i As Integer = 0
                For Each dr_ac In dt_ac.Rows
                    '(akun tujuan (laba berjalan))

                    ssql = "INSERT INTO  " _
                     & "  public.glt_det " _
                     & "( " _
                     & "  glt_oid, " _
                     & "  glt_dom_id, " _
                     & "  glt_en_id, " _
                     & "  glt_add_by, " _
                     & "  glt_add_date, " _
                     & "  glt_code, " _
                     & "  glt_date, " _
                     & "  glt_type, " _
                     & "  glt_cu_id, " _
                     & "  glt_exc_rate, " _
                     & "  glt_seq, " _
                     & "  glt_ac_id, " _
                     & "  glt_sb_id, " _
                     & "  glt_cc_id, " _
                     & "  glt_desc, " _
                     & "  glt_debit, " _
                     & "  glt_credit, " _
                     & "  glt_ref_trans_code, " _
                     & "  glt_daybook, " _
                     & "  glt_posted, " _
                     & "  glt_dt " _
                     & ")  " _
                     & "VALUES ( " _
                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                     & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                     & SetInteger(dr("en_id")) & ",  " _
                     & SetSetring(master_new.ClsVar.sNama) & ",  " _
                     & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                     & SetSetring(_glt_code) & ",  " _
                     & SetDate(dr_gcal("gcal_start_date")) & ",  " _
                     & SetSetring("JP") & ",  " _
                     & SetInteger(dr_laba_berjalan("ac_cu_id")) & ",  " _
                     & SetDbl(1) & ",  " _
                     & SetInteger(i) & ",  " _
                     & SetIntegerDB(dr_laba_berjalan("ac_id")) & ",  " _
                     & SetIntegerDB(0) & ",  " _
                     & SetIntegerDB(0) & ",  " _
                     & SetSetringDB("End Month Adjusment 2") & ",  " _
                     & SetDbl(dr_ac("ac_value")) & ",  " _
                     & SetDbl(0) & ",  " _
                     & SetSetring("") & ",  " _
                     & SetSetring("MONTH END") & ",  " _
                     & SetSetring("Y") & ",  " _
                     & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                     & ")"

                    ssqls.Add(ssql)

                    'update gl bal
                    If update_gl_bal(ssqls, dr_gcal("gcal_oid"), dr("en_id"), dr_laba_berjalan("ac_id"), "D", dr_ac("ac_value"), _
                                     dr_laba_berjalan("ac_cu_id"), func_data.get_exchange_rate(dr_laba_berjalan("ac_cu_id"), _
                                     dr_gcal("gcal_start_date"))) = False Then

                        Exit Sub
                    End If

                    'insert kredit
                    ssql = "INSERT INTO  " _
                      & "  public.glt_det " _
                      & "( " _
                      & "  glt_oid, " _
                      & "  glt_dom_id, " _
                      & "  glt_en_id, " _
                      & "  glt_add_by, " _
                      & "  glt_add_date, " _
                      & "  glt_code, " _
                      & "  glt_date, " _
                      & "  glt_type, " _
                      & "  glt_cu_id, " _
                      & "  glt_exc_rate, " _
                      & "  glt_seq, " _
                      & "  glt_ac_id, " _
                      & "  glt_sb_id, " _
                      & "  glt_cc_id, " _
                      & "  glt_desc, " _
                      & "  glt_debit, " _
                      & "  glt_credit, " _
                      & "  glt_ref_trans_code, " _
                      & "  glt_daybook, " _
                      & "  glt_posted, " _
                      & "  glt_dt " _
                      & ")  " _
                      & "VALUES ( " _
                      & SetSetring(Guid.NewGuid.ToString) & ",  " _
                      & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                      & SetInteger(dr("en_id")) & ",  " _
                      & SetSetring(master_new.ClsVar.sNama) & ",  " _
                      & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                      & SetSetring(_glt_code) & ",  " _
                      & SetDate(dr_gcal("gcal_start_date")) & ",  " _
                      & SetSetring("JP") & ",  " _
                      & SetInteger(dr_laba_ditahan("ac_cu_id")) & ",  " _
                      & SetDbl(1) & ",  " _
                      & SetInteger(i + 1) & ",  " _
                      & SetIntegerDB(dr_laba_ditahan("ac_id")) & ",  " _
                      & SetIntegerDB(0) & ",  " _
                      & SetIntegerDB(0) & ",  " _
                      & SetSetringDB("End Month Adjusment 2") & ",  " _
                      & SetDbl(0) & ",  " _
                      & SetDbl(dr_ac("ac_value")) & ",  " _
                      & SetSetring("") & ",  " _
                      & SetSetring("MONTH END") & ",  " _
                      & SetSetring("Y") & ",  " _
                      & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                      & ")"

                    ssqls.Add(ssql)

                    'update glbal
                    If update_gl_bal(ssqls, dr_gcal("gcal_oid"), dr("en_id"), dr_ac("ac_id"), "C", dr_ac("ac_value"), _
                                     dr_ac("ac_cu_id"), func_data.get_exchange_rate(dr_ac("ac_cu_id"), _
                                     dr_gcal("gcal_start_date"))) = False Then
                        Exit Sub
                    End If

                Next
            Next


            If MyPGDll.PGSqlConn.status_sync = True Then
                'DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "")
                If DbRunTran(ssqls, "", MyPGDll.ClassSync.FinsertSQL2Array(ssqls), "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Exit Sub
                End If
                ssqls.Clear()
            End If
            Box("Proses Closing tahap pembuatan Jurnal Laba Berjalan ke Laba Ditahan Sukses")
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub
    Public Function get_next_gcal(ByVal _gcal_uid As String) As DataRow
        Try
            ssql = "select gcal_oid,gcal_start_date,gcal_end_date from gcal_mstr where gcal_start_date > (select gcal_end_date from gcal_mstr where gcal_oid='" & _gcal_uid & "') order by gcal_start_date limit 1"

            Dim dr As DataRow
            dr = GetRowInfo(ssql)

            Return dr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
