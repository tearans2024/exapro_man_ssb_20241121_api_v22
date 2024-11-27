'Imports Npgsql
Imports System

Imports System.Text


Imports System.IO
Imports master_new.ModFunction
Imports CoreLab.PostgreSql

Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports Microsoft.VisualBasic
Imports System.Text.Encoding
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Globalization
Imports System.IO.Compression
Imports System.Data.OleDb

Imports master_new.ClsVar



Public Class PGSqlConn
    '===Ini dll untuk koneksi ke postgresql
    Public Shared Function DbConString() As String

        Dim constring As String = master_new.WDABasepgsql.DbConString

        DbConString = constring

    End Function
    Public Shared Function DbConString(ByVal server As String) As String

        If server.ToUpper = "SYNC" Then
            Dim constring As String = "Server= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserversync.txt"), "server") & " ;" _
                    & "Database=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\pgserversync.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\pgserversync.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\pgserversync.txt"), "user") & ";Password=bangkar;Pooling=false;"
            DbConString = constring

        ElseIf server.ToUpper = "SVR1" Then

            Dim constring As String = "Server= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver_1.txt"), "server") & " ;" & _
                      "Database=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_1.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_1.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_1.txt"), "user") & ";Password=bangkar;Pooling=false;"

            DbConString = constring

        ElseIf server.ToUpper = "SVR2" Then

            Dim constring As String = "Server= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver_2.txt"), "server") & " ;" & _
                      "Database=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_2.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_2.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_2.txt"), "user") & ";Password=bangkar;Pooling=false;"

            DbConString = constring

        ElseIf server.Length > 0 Then
            If server.Contains(";") Then
                Dim i As Integer
                Dim svr, port As String
                i = 0
                i = server.IndexOf(";")

                svr = server.Substring(0, i)
                port = server.Substring(i + 1, Len(server) - i - 1)

                Dim constring As String = "Server= " & svr & " ;" & _
                   "Database=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "db") & ";Port=" & port & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "user") & ";Password=bangkar;Pooling=false;Connection Lifetime=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\conf_system.txt"), "limit_connection_timeout") & ";"

                DbConString = constring
            Else
                Dim constring As String = "Server= " & server & " ;" & _
                   "Database=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "user") & ";Password=bangkar;Pooling=false;"
                DbConString = constring
            End If

        Else
            DbConString = DbConString()
        End If

    End Function

    Public Shared Function CekStyeTanggal() As String
        Try
            'Dim sSQL As String

            'sSQL = "SHOW DATESTYLE"
            'Dim _error As New ArrayList
            'CekStyeTanggal = GetRowInfo(sSQL)(0).ToString
            'CekStyeTanggal = GetRowInfoJSON(sSQL, _error)(0).ToString
            Return "ISO, DMY"
        Catch ex As Exception
            Pesan(Err)
            CekStyeTanggal = ""
        End Try

    End Function

    Public Shared Function DbRun(ByVal sSql As String) As Boolean
        'Dim DbConn As PgSqlConnection
        'Dim CmdSql As PgSqlCommand

        'DbConn = New PgSqlConnection(DbConString)
        'CmdSql = New PgSqlCommand(sSql, DbConn)

        Try
            'With CmdSql
            '    '.Connection.Open()
            '    .ExecuteNonQuery()
            '    .Connection.Close()
            '    .Dispose()
            'End With
            'Return True

            Dim ssqls As New ArrayList
            Dim par_error As New ArrayList
            ssqls.Add(sSql)
            If dml(ssqls, par_error) Then
                'after_success()
                'set_row(Trim(_en_oid.ToString), "en_oid")
                'insert = True
                Return True
            Else
                Box(par_error.Item(0).ToString)
                Return False
            End If
        Catch ex As Exception
            Throw ex 'New Exception("Data Entry Error")
            'MessageBox.Show(sSql & " >> desc :" & Err.Description & " >> err.number :" & Err.Number)
            Return False
        Finally
            'DbConn.Close()
            'DbConn.Dispose()
        End Try

    End Function

    Public Shared Sub DbRunParameter(ByVal sSql As String, ByVal sSQLs As ArrayList)
        Dim DbConn As PgSqlConnection
        Dim CmdSql As PgSqlCommand
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim Prmtr, Isi As String

        DbConn = New PgSqlConnection(DbConString)
        CmdSql = New PgSqlCommand(sSql, DbConn)
        Prmtr = ""
        Isi = ""

        Try
            With CmdSql
                For Each Parameter As String In sSQLs
                    'MsgBox(Parameter)
                    i = Parameter
                    a = i.Split(";")
                    For j = 0 To a.GetUpperBound(0)
                        If j = 0 Then
                            If Len(a(j)) > 0 Then
                                Prmtr = a(j)
                            End If
                        End If

                        If j = 1 Then
                            If Len(a(j)) > 0 Then
                                Isi = a(j)
                            End If
                        End If
                    Next
                    'MsgBox(Prmtr & " >> " & Isi & " <<")
                    '.Parameters.
                    '.Parameters.AddWithValue(Prmtr, Isi)
                    Dim Param = New PgSqlParameter(Prmtr, DbType.String)
                    Param.Value = Isi
                    .Parameters.Add(Param)
                Next
                '.Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
                .Dispose()
            End With
        Catch ex As Exception
            Throw ex 'New Exception("Data Entry Error")
            'MessageBox.Show(sSql & " >> desc :" & Err.Description & " >> err.number :" & Err.Number)
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Sub

    Public Shared Function DbRunTran(ByVal sSQLs As ArrayList, Optional ByVal debug As Boolean = False) As Boolean
        'Dim DbConn As New PgSqlConnection(DbConString)
        'Dim SqlTrans As PgSqlTransaction
        'Dim sql As String = ""

        'DbConn.Open()
        'SqlTrans = DbConn.BeginTransaction()

        Try
            'For Each sSQL As String In sSQLs
            '    Dim CmdSql As New PgSqlCommand(sSQL, DbConn)
            '    sql = sSQL
            '    If debug = True Then
            '        Box(sql)
            '    End If

            '    With CmdSql
            '        .Transaction = SqlTrans
            '        .ExecuteNonQuery()
            '        'System.Windows.Forms.Application.DoEvents()
            '    End With

            'Next
            'SqlTrans.Commit()
            'Return True

            Dim par_error As New ArrayList
            If dml(sSQLs, par_error) Then
                'after_success()
                'set_row(Trim(_en_oid.ToString), "en_oid")
                'insert = True
                Return True
            Else
                Box(par_error.Item(0).ToString)
                Return False
            End If

        Catch ex As Exception
            'SqlTrans.Rollback()
            'MsgBox(sql, MsgBoxStyle.Information, "Query error...")
            Throw ex 'New Exception("Data Entry Failed")
            Return False
        Finally
            'DbConn.Close()
            'DbConn.Dispose()
        End Try

    End Function

    Public Shared Function DbRunTran(ByVal sSQLs As ArrayList, ByVal server As String) As Boolean
        'Dim DbConn As New PgSqlConnection(DbConString(server))
        'Dim SqlTrans As PgSqlTransaction

        'DbConn.Open()
        'SqlTrans = DbConn.BeginTransaction()

        Try

            'For Each sSQL As String In sSQLs
            '    Dim CmdSql As New PgSqlCommand(sSQL, DbConn)

            '    With CmdSql
            '        .Transaction = SqlTrans
            '        .ExecuteNonQuery()
            '        'System.Windows.Forms.Application.DoEvents()
            '    End With
            'Next
            'SqlTrans.Commit()
            'Return True

            Dim par_error As New ArrayList
            If dml(sSQLs, par_error) Then
                'after_success()
                'set_row(Trim(_en_oid.ToString), "en_oid")
                'insert = True
                Return True
            Else
                Box(par_error.Item(0).ToString)
                Return False
            End If

        Catch ex As Exception

            'SqlTrans.Rollback()

            'Throw ex 'New Exception("Data Entry Failed")
            MsgBox(ex.Message)
            Return False
        Finally
            'DbConn.Close()
            'DbConn.Dispose()
        End Try

    End Function

    Public Shared Function DbRunTran(ByVal sSQLs1 As ArrayList, ByVal server1 As String, _
                                ByVal sSQLs2 As ArrayList, ByVal server2 As String) As Boolean

        'Dim DbConn1 As New PgSqlConnection(DbConString(server1))
        'Dim SqlTrans1 As PgSqlTransaction

        'DbConn1.Open()
        'SqlTrans1 = DbConn1.BeginTransaction()

        'Dim DbConn2 As New PgSqlConnection(DbConString(server2))
        'Dim SqlTrans2 As PgSqlTransaction

        'DbConn2.Open()
        'SqlTrans2 = DbConn2.BeginTransaction()

        Try

            'For Each sSQL1 As String In sSQLs1
            '    Dim CmdSql1 As New PgSqlCommand(sSQL1, DbConn1)
            '    With CmdSql1
            '        .Transaction = SqlTrans1
            '        .ExecuteNonQuery()
            '    End With
            'Next

            'For Each sSQL2 As String In sSQLs2
            '    Dim CmdSql2 As New PgSqlCommand(sSQL2, DbConn2)

            '    With CmdSql2
            '        .Transaction = SqlTrans2
            '        .ExecuteNonQuery()
            '    End With
            'Next

            'SqlTrans1.Commit()
            'SqlTrans2.Commit()
            Dim par_error As New ArrayList
            sSQLs1.AddRange(sSQLs2)
            If dml(sSQLs1, par_error) Then
                'after_success()
                'set_row(Trim(_en_oid.ToString), "en_oid")
                'insert = True
                Return True
            Else
                Box(par_error.Item(0).ToString)
                Return False
            End If

            ' Return True
        Catch ex As Exception

            'SqlTrans1.Rollback()
            'SqlTrans2.Rollback()

            MsgBox(ex.Message)
            Return False
        Finally
            'DbConn1.Close()
            'DbConn1.Dispose()
            'DbConn2.Close()
            'DbConn2.Dispose()
        End Try
    End Function

    Public Shared Function DbRunTran(ByVal sSQLs1 As ArrayList, ByVal server1 As String, _
                                ByVal sSQLs2 As ArrayList, ByVal server2 As String, _
                                ByVal sSQLs3 As ArrayList, ByVal server3 As String) As Boolean

        Dim DbConn1 As New PgSqlConnection(DbConString(server1))
        Dim SqlTrans1 As PgSqlTransaction


        DbConn1.Open()
        SqlTrans1 = DbConn1.BeginTransaction()

        Dim DbConn2 As New PgSqlConnection(DbConString(server2))
        Dim SqlTrans2 As PgSqlTransaction


        DbConn2.Open()
        SqlTrans2 = DbConn2.BeginTransaction()

        Dim DbConn3 As New PgSqlConnection(DbConString(server3))
        Dim SqlTrans3 As PgSqlTransaction


        DbConn3.Open()
        SqlTrans3 = DbConn3.BeginTransaction()

        Try

            For Each sSQL1 As String In sSQLs1
                Dim CmdSql1 As New PgSqlCommand(sSQL1, DbConn1)

                With CmdSql1
                    .Transaction = SqlTrans1
                    .ExecuteNonQuery()
                End With
            Next

            For Each sSQL2 As String In sSQLs2
                Dim CmdSql2 As New PgSqlCommand(sSQL2, DbConn2)

                With CmdSql2
                    .Transaction = SqlTrans2
                    .ExecuteNonQuery()
                End With
            Next


            For Each sSQL3 As String In sSQLs3
                Dim CmdSql3 As New PgSqlCommand(sSQL3, DbConn3)

                With CmdSql3
                    .Transaction = SqlTrans3
                    .ExecuteNonQuery()
                End With
            Next

            SqlTrans1.Commit()
            SqlTrans2.Commit()
            SqlTrans3.Commit()
            Return True
        Catch ex As Exception

            SqlTrans1.Rollback()
            SqlTrans2.Rollback()
            SqlTrans3.Rollback()

            Throw ex 'New Exception("Data Entry Failed")
            Return False
        Finally
            DbConn1.Close()
            DbConn1.Dispose()

            DbConn2.Close()
            DbConn2.Dispose()

            DbConn3.Close()
            DbConn3.Dispose()
        End Try

    End Function
    Public Shared Sub DbRunTranParameter(ByVal sSQLs As ArrayList)
        'contoh penggunaan procedure eksekusi query menggunakan parameter 
        'dengan fasilitas rollback jika terjadi kegagalan
        'penggunaan parameter ini berfungsi untuk melewatkan karakter2 tertentu seperti petik satu
        'misal nama Nu'man. jika kata Nu'man di insert menggunakan sql biasa akan error

        'dim sSql as string
        'dim sSqls as new ArrayList

        'sSql="Insert Into Tabel1 (Item1,Item2,Item3) Values " _
        '& "(Parameter1,Parameter2,Parameter3)~Parameter1;Value1#Parameter2;Value2#Parameter3;Value4"
        'sSqls.Add(sSql)

        'sSql="Insert Into Tabel2 (Item1,Item2,Item3) Values " _
        '& "(Parameter1,Parameter2,Parameter3)~Parameter1;Value1#Parameter2;Value2#Parameter3;Value4"
        'sSqls.Add(sSql)

        'DbRunTranParameter(sSqls)

        'silahkan deklarasikan dbconstring dengan koneksi string yang dipakai
        'jika anda menggunakan mode System.Data.SqlClient  maka rubahlah oleDb menjadi sql
        'contoh Dim SqlTrans As OleDbTransaction menjadi Dim SqlTrans As SqlTransaction dst, dst


        Dim DbConn As New PgSqlConnection(DbConString)
        Dim SqlTrans As PgSqlTransaction
        Dim i, h As String
        Dim a(), b(), c() As String
        Dim j, k, l As Integer
        Dim Prmtr, Isi As String
        Dim Sql, Parameter As String

        Parameter = ""
        Sql = ""
        Prmtr = ""
        Isi = ""

        DbConn.Open()
        SqlTrans = DbConn.BeginTransaction()

        Try
            For Each sSQL As String In sSQLs
                'Pisahkan antara Query dgn parameter
                i = sSQL
                a = i.Split("~")
                For j = 0 To a.GetUpperBound(0)

                    If j = 0 Then
                        If Len(a(j)) > 0 Then
                            Sql = a(j)
                        End If
                    End If

                    If j = 1 Then
                        If Len(a(j)) > 0 Then
                            Parameter = a(j)
                        End If
                    End If
                Next

                Dim CmdSql As New PgSqlCommand(Sql, DbConn)
                With CmdSql
                    'Cek apakah ada parameternya
                    If i.Contains("~") <> False Then
                        'Pisahkan parameter satu dgn parameter lainya
                        b = Parameter.Split("#")
                        For k = 0 To b.GetUpperBound(0)
                            h = b(k)
                            'Pisahkan antara parameter dan value dr parameter
                            c = h.Split(";")
                            For l = 0 To c.GetUpperBound(0)
                                If l = 0 Then
                                    If Len(c(l)) > 0 Then
                                        Prmtr = c(l)
                                    End If
                                End If

                                If l = 1 Then
                                    If Len(c(l)) > 0 Then
                                        Isi = c(l)
                                    End If
                                End If
                            Next
                            'parameter disini
                            '.Parameters.AddWithValue(Prmtr, Isi)
                            Dim Param = New PgSqlParameter(Prmtr, DbType.String)
                            Param.Value = Isi
                            .Parameters.Add(Param)
                        Next
                    End If

                    .Transaction = SqlTrans
                    .ExecuteNonQuery()
                End With
            Next
            SqlTrans.Commit()
        Catch ex As Exception
            SqlTrans.Rollback()
            Throw ex 'New Exception("Data Entry Failed")
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Sub

    Public Shared Function GetRowInfo(ByVal sSql As String) As DataRow
        Dim _error As New ArrayList
        Dim Dt As New DataTable

        Dt = get_sql(sSql, _error)

        If Dt Is Nothing Then
            Return Nothing
            Exit Function
        End If
        If Dt.Rows.Count = 0 Then
            Return Nothing
        ElseIf Dt.Rows.Count > 1 Then
            'Throw New Exception("Multiple rows effected")
            Return Dt.Rows(0)
        Else
            Return Dt.Rows(0)
        End If
    End Function



    Public Shared Function GetRowInfoJSON(ByVal ssql As String, ByVal par_error As ArrayList) As DataRow
        Dim _error As New ArrayList
        Dim Dt As New DataTable

        Dt = get_sql(ssql, _error)

        If _error.Count > 0 Then
            Return Nothing
        End If

        If Dt.Rows.Count = 0 Then
            Return Nothing
        ElseIf Dt.Rows.Count > 1 Then
            'Throw New Exception("Multiple rows effected")
            Return Dt.Rows(0)
        Else
            Return Dt.Rows(0)
        End If

    End Function

    Public Shared Function GetDataColumn(ByVal sSql As String) As Object
        Try
            Dim sDa As New PgSqlDataAdapter(sSql, DbConString)
            Dim Dt As New DataTable

            sDa.SelectCommand.CommandTimeout = 240
            sDa.Fill(Dt)
            sDa.Dispose()
            If Dt.Rows.Count > 0 Then
                Return Dt.Rows(0).Item(0)
            Else
                Return DBNull.Value
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try

    End Function
    Public Shared Sub GetDataset(ByVal ssql As String, ByVal ds As DataSet, ByVal par_table_name As String)
        Try
            Dim par_error As New ArrayList
            Dim dt As New DataTable

            dt = GetTableDataJson(ssql, par_error)
            If Not dt Is Nothing Then
                dt.TableName = par_table_name
            End If


            If par_error.Count > 0 Then
                MsgBox(par_error.Item(0).ToString)
            End If

            Try

                ds.Tables.Remove(par_table_name)
            Catch ex As Exception

            End Try

            If Not dt Is Nothing Then
                ds.Tables.Add(dt)


            End If
        Catch ex As Exception
            Box(ex.Message)
        End Try
    End Sub

    Public Shared Sub GetDatasetMysql(ByVal ssql As String, ByVal ds As DataSet, ByVal par_table_name As String)
        Try
            Dim par_error As New ArrayList
            Dim dt As New DataTable

            dt = GetTableDataJsonMysql(ssql, par_error)
            If Not dt Is Nothing Then
                dt.TableName = par_table_name
            End If


            If par_error.Count > 0 Then
                MsgBox(par_error.Item(0).ToString)
            End If

            Try

                ds.Tables.Remove(par_table_name)
            Catch ex As Exception

            End Try

            If Not dt Is Nothing Then
                ds.Tables.Add(dt)


            End If
        Catch ex As Exception
            Box(ex.Message)
        End Try
    End Sub
    Public Shared Function GetTableData(ByVal sSql As String) As DataTable
        Dim _error As New ArrayList
        Try
            'Dim sDa As New PgSqlDataAdapter(sSql, DbConString)
            Dim Dt As New DataTable

            'sDa.SelectCommand.CommandTimeout = 480
            'sDa.Fill(Dt)
            'Return Dt
            'Dt.Dispose()
            'sDa.Dispose()

            Dt = get_sql(sSql, _error)

            If _error.Count > 0 Then
                Box(_error(0).ToString)
            End If

            Return Dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try

    End Function

    Public Shared Function GetTableDataMysql(ByVal sSql As String) As DataTable
        Dim _error As New ArrayList
        Try

            Dim Dt As New DataTable

            Dt = get_sql_mysql(sSql, _error)

            If _error.Count > 0 Then
                Box(_error(0).ToString)
            End If

            Return Dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try

    End Function

    Public Shared Function GetTableData(ByVal sSql As String, ByVal par_none As String, ByVal par_constring As String) As DataTable

        Dim sDa As New PgSqlDataAdapter(sSql, par_constring)
        Dim Dt As New DataTable

        sDa.SelectCommand.CommandTimeout = 240
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()
    End Function

    Public Shared Function GetTableData(ByVal sSql As String, ByVal server As String) As DataTable

        'Dim DbConn As PgSqlConnection
        'DbConn = New PgSqlConnection(DbConString(server))
        'Dim cmd As PgSqlCommand
        'Dim adapter As New PgSqlDataAdapter
        Dim _error As New ArrayList
        Try
            'open the command objects connection
            'Dim Dt As New DataTable

            'DbConn.Open()

            'cmd = New PgSqlCommand
            'cmd.Connection = DbConn
            'cmd.CommandType = CommandType.Text
            'cmd.CommandText = sSql

            'adapter.SelectCommand = cmd
            'adapter.SelectCommand.CommandTimeout = 240
            'adapter.Fill(Dt)
            Return get_sql(sSql, _error)
            'Return Dt

            'DbConn.Close()
            'adapter.Dispose()
            'Catch ex As PgSqlException
            '    'Throw ex
            '    Throw ex
            '    Return Nothing
        Catch ex As Exception
            ' Throw New Exception(ex.Message)
            Throw ex
            Return Nothing
        Finally
            'DbConn.Close()
            'adapter.Dispose()
        End Try
    End Function

    Public Shared Function GetTableDataJson(ByVal sSql As String, ByVal par_error As ArrayList, Optional ByVal par_option As String = "") As DataTable
        Return get_sql(sSql, par_error, par_option)
    End Function
    Public Shared Function GetTableDataJsonMysql(ByVal sSql As String, ByVal par_error As ArrayList, Optional ByVal par_option As String = "") As DataTable
        Return get_sql_mysql(sSql, par_error, par_option)
    End Function
    Public Shared Function GetTableDataJsonDuckdb(ByVal sSql As String, ByVal par_error As ArrayList, Optional ByVal par_option As String = "") As DataTable
        Return get_sql_duckdb(sSql, par_error, par_option)
    End Function

    Public Shared Function GetTableDataHelperMysql(ByVal ssql) As DataTable
        Dim dt_temp As New DataTable
        Try
            Dim _error As New ArrayList


            dt_temp = GetTableDataJsonMysql(ssql, _error)

            If _error.Count > 0 Then
                Box(_error(0).ToString)
            End If

            Return dt_temp

        Catch ex As Exception
            Box(ex.Message)
            Return dt_temp
        End Try
    End Function

    Public Shared Function GetTableDataHelper(ByVal ssql) As DataTable
        Dim dt_temp As New DataTable
        Try
            Dim _error As New ArrayList


            dt_temp = GetTableDataJson(ssql, _error)

            If _error.Count > 0 Then
                Box(_error(0).ToString)
            End If

            Return dt_temp

        Catch ex As Exception
            Box(ex.Message)
            Return dt_temp
        End Try
    End Function

    Public Shared Function GetTableDataReader(ByVal sSql As String) As PgSqlDataReader
        Dim DbConn As PgSqlConnection
        DbConn = New PgSqlConnection(DbConString)

        DbConn.Open()

        Dim cmd As PgSqlCommand
        Dim rs As PgSqlDataReader

        cmd = New PgSqlCommand
        cmd.Connection = DbConn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sSql
        rs = cmd.ExecuteReader(CommandBehavior.SingleResult)

        Return rs
        'Label1.Text = rs.GetString(0) & vbNewLine & rs.GetString(1)

        rs.Close()
        cmd.Dispose()
        DbConn.Close()
        DbConn.Dispose()
    End Function

    Public Shared Function postDataJson(ByVal par_json As String, ByVal actionData As String, ByVal par_url As String, ByVal par_error As ArrayList) As String
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            webClient.Headers("content-encoding") = "gzip"
            webClient.Headers("accept-encoding") = "gzip"

            reqString = Encoding.Default.GetBytes(par_json)
            resByte = webClient.UploadData(par_url, actionData, reqString)

            resString = DecompressString(resByte) 'Encoding.Default.GetString(resByte)

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            'Console.WriteLine(ex.Message)
            par_error.Add(ex.Message)
            Return ""
        End Try

    End Function

    Public Shared Function getData(ByVal par_url As String) As String

        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            resByte = webClient.DownloadData(par_url)
            resString = Encoding.Default.GetString(resByte)

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

    Public Shared Function GetJSONToDataTableUsingNewtonSoftDll(ByVal JSONData As String) As DataTable
        'Dim json As JObject = JsonConvert.DeserializeObject(Of JObject)(JSONData, New JsonSerializerSettings With {.Culture = CultureInfo.GetCultureInfo("en-US")})

        Dim dt As DataTable = CType(JsonConvert.DeserializeObject(JSONData, (GetType(DataTable)), New JsonSerializerSettings With {.Culture = CultureInfo.GetCultureInfo("en-US")}), DataTable)

        'Dim dt As DataTable = CType(JsonConvert.DeserializeObject(JSONData, (GetType(DataTable)), New JsonSerializerSettings With {.Culture = CultureInfo.GetCultureInfo("id-ID")}), DataTable)
        Return dt
    End Function
    Public Shared Sub save_as_text(ByVal par_array As ArrayList)
        Dim _str As String = ""
        Try
            For Each item As String In par_array
                _str = _str & item & vbNewLine
            Next

            Dim _file As String = ""
            _file = AskSaveAsFile("TXT Files | *.txt")
            If _file = "" Then
                Box("Please select file name to export")
                Exit Sub
            End If

            SaveTextToFile(_str, _file)

            Box("Export finish")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Shared Function dml(ByVal par_array As ArrayList, ByVal par_error As ArrayList) As Boolean
        Try
            Dim dict As New Dictionary(Of String, Dictionary(Of String, String))
            Dim listOfDictionaries = New List(Of Dictionary(Of String, String))
            Dim listOfDictionariesSsql = New List(Of Dictionary(Of String, String))

            'setting alamat api
            Dim dictMasterSsqls As New Dictionary(Of String, Object)
            dictMasterSsqls.Add("type", "multi_dml")
            dictMasterSsqls.Add("user", master_new.ClsVar.sNama)
            dictMasterSsqls.Add("token", "93020291-b256-4572-a82e-d3acef8baa05")
            dictMasterSsqls.Add("dbname", _dbname)
            dictMasterSsqls.Add("server", _server)

            For Each Str As String In par_array
                Dim dictSQLDet As New Dictionary(Of String, String)
                dictSQLDet.Add("qry", Str)
                listOfDictionariesSsql.Add(dictSQLDet)
            Next

            Dim dictSQLDet2 As New Dictionary(Of String, String)
            dictSQLDet2.Add("qry", "~")
            listOfDictionariesSsql.Add(dictSQLDet2)

            dictMasterSsqls.Add("query", listOfDictionariesSsql)

            Dim json_str As String = JsonConvert.SerializeObject(dictMasterSsqls)


            Dim _response As String = postDataJson(json_str, "post", _url_api, par_error)

            If _response <> "" Then
                Dim result As JObject = JObject.Parse(_response.ToString)
                Dim _status As Boolean

                _status = result.GetValue("status")
                If _status = False Then
                    par_error.Add(result.GetValue("message").ToString)
                    Return False
                    Exit Function
                End If

            Else
                par_error.Add("No response")
                Return False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            par_error.Add(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function dml_Mysql(ByVal par_array As ArrayList, ByVal par_error As ArrayList) As Boolean
        Try
            Dim dict As New Dictionary(Of String, Dictionary(Of String, String))
            Dim listOfDictionaries = New List(Of Dictionary(Of String, String))
            Dim listOfDictionariesSsql = New List(Of Dictionary(Of String, String))

            Dim dictMasterSsqls As New Dictionary(Of String, Object)
            dictMasterSsqls.Add("type", "multi_dml_mysql")
            dictMasterSsqls.Add("user", master_new.ClsVar.sNama)
            dictMasterSsqls.Add("token", "93020291-b256-4572-a82e-d3acef8baa05")

            For Each Str As String In par_array
                Dim dictSQLDet As New Dictionary(Of String, String)
                dictSQLDet.Add("qry", Str)
                listOfDictionariesSsql.Add(dictSQLDet)
            Next

            Dim dictSQLDet2 As New Dictionary(Of String, String)
            dictSQLDet2.Add("qry", "~")
            listOfDictionariesSsql.Add(dictSQLDet2)

            dictMasterSsqls.Add("query", listOfDictionariesSsql)

            Dim json_str As String = JsonConvert.SerializeObject(dictMasterSsqls)


            Dim _response As String = postDataJson(json_str, "post", _url_api, par_error)

            If _response <> "" Then
                Dim result As JObject = JObject.Parse(_response.ToString)
                Dim _status As Boolean

                _status = result.GetValue("status")
                If _status = False Then
                    par_error.Add(result.GetValue("message").ToString)
                    Return False
                    Exit Function
                End If

            Else
                par_error.Add("No response")
                Return False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            par_error.Add(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function dml_Duckdb(ByVal par_array As ArrayList, ByVal par_error As ArrayList) As Boolean
        Try
            Dim dict As New Dictionary(Of String, Dictionary(Of String, String))
            Dim listOfDictionaries = New List(Of Dictionary(Of String, String))
            Dim listOfDictionariesSsql = New List(Of Dictionary(Of String, String))

            Dim dictMasterSsqls As New Dictionary(Of String, Object)
            dictMasterSsqls.Add("type", "multi_dml_duckdb")
            dictMasterSsqls.Add("user", master_new.ClsVar.sNama)
            dictMasterSsqls.Add("token", "93020291-b256-4572-a82e-d3acef8baa05")

            For Each Str As String In par_array
                Dim dictSQLDet As New Dictionary(Of String, String)
                dictSQLDet.Add("qry", Str)
                listOfDictionariesSsql.Add(dictSQLDet)
            Next

            Dim dictSQLDet2 As New Dictionary(Of String, String)
            dictSQLDet2.Add("qry", "~")
            listOfDictionariesSsql.Add(dictSQLDet2)

            dictMasterSsqls.Add("query", listOfDictionariesSsql)

            Dim json_str As String = JsonConvert.SerializeObject(dictMasterSsqls)


            Dim _response As String = postDataJson(json_str, "post", _url_api, par_error)

            If _response <> "" Then
                Dim result As JObject = JObject.Parse(_response.ToString)
                Dim _status As Boolean

                _status = result.GetValue("status")
                If _status = False Then
                    par_error.Add(result.GetValue("message").ToString)
                    Return False
                    Exit Function
                End If

            Else
                par_error.Add("No response")
                Return False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            par_error.Add(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function getvalue(ByVal par_sql As String, ByVal par_tipe As String) As Object
        Try
            Dim temp_data As Object
            If par_tipe = "string" Then
                temp_data = ""
            ElseIf par_tipe = "integer" Then
                temp_data = 0
            ElseIf par_tipe = "numeric" Then
                temp_data = 0.0
            Else
                temp_data = DBNull.Value
            End If


            Dim _error As New ArrayList

            Dim dt_temp As New DataTable
            dt_temp = master_new.PGSqlConn.GetTableDataJson(par_sql, _error)


            If _error.Count > 0 Then
                Box(_error(0).ToString)
            End If

            For Each dr As DataRow In dt_temp.Rows
                temp_data = dr(0)
            Next

            Return temp_data
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function


    Public Shared Function postData(ByVal dictData As Dictionary(Of String, Object), ByVal actionData As String, ByVal par_url As String) As String
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim reqByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            webClient.Headers("content-encoding") = "gzip"
            webClient.Headers("accept-encoding") = "gzip"
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
            resByte = webClient.UploadData(par_url, actionData, reqString)

            'MakeReport(dictData("query").ToString + vbNewLine + vbNewLine + ">> size : " + resByte.Lsength.ToString("N0") + " bytes" + vbNewLine)
            'Dim decompressedContent = New System.IO.Compression.GZipStream(resByte, Compression.CompressionMode.Decompress)
            'Dim response As HttpResponseMessage = webClient.UploadData(par_url, actionData, reqString)
            resString = DecompressString(resByte)  '

            webClient.Dispose()
            Return resString
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

    Shared Function CompressStringToBytes(ByVal input As String) As Byte()
        Using outputStream As New MemoryStream()
            Using gzipStream As New Compression.GZipStream(outputStream, CompressionMode.Compress)
                Using writer As New StreamWriter(gzipStream, Encoding.UTF8)
                    writer.Write(input)
                End Using
            End Using
            Return outputStream.ToArray()
        End Using
    End Function

    Shared Function DecompressString(ByVal compressedBytes As Byte()) As String
        Using inputStream As New MemoryStream(compressedBytes)
            Using gzipStream As New Compression.GZipStream(inputStream, CompressionMode.Decompress)
                Using reader As New StreamReader(gzipStream, Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Using
    End Function


    Public Shared Function getvalue_Mysql(ByVal par_sql As String, ByVal par_tipe As String) As Object
        Try
            Dim temp_data As Object
            If par_tipe = "string" Then
                temp_data = ""
            ElseIf par_tipe = "integer" Then
                temp_data = 0
            ElseIf par_tipe = "numeric" Then
                temp_data = 0.0
            Else
                temp_data = DBNull.Value
            End If


            Dim _error As New ArrayList

            Dim dt_temp As New DataTable
            dt_temp = master_new.PGSqlConn.GetTableDataJsonMysql(par_sql, _error)


            If _error.Count > 0 Then
                Box(_error(0).ToString)
            End If

            For Each dr As DataRow In dt_temp.Rows
                temp_data = dr(0)
            Next

            Return temp_data
        Catch ex As Exception
            Return DBNull.Value
        End Try
    End Function

    Public Shared Function get_sql(ByVal par_sql As String, ByVal par_error As ArrayList, Optional ByVal par_option As String = "") As DataTable
        Dim dt_hasil As New DataTable
        Try

            Dim dictData As New Dictionary(Of String, Object)
            dictData.Add("type", "select_null")
            dictData.Add("query", par_sql)
            dictData.Add("option", par_option)
            dictData.Add("user", master_new.ClsVar.sNama)
            dictData.Add("token", "93020291-b256-4572-a82e-d3acef8baa05")
            dictData.Add("dbname", _dbname)
            dictData.Add("server", _server)

            Dim _response As String = postData(dictData, "post", _url_api)

            If _response <> "" Then
                Dim result As JObject = JObject.Parse(_response.ToString)
                Dim _status As Boolean

                _status = result.GetValue("status")
                If _status = True Then

                    Dim data As String = result.GetValue("data").ToString()
                    If data <> "" And data <> "[]" Then


                        dt_hasil = GetJSONToDataTableUsingNewtonSoftDll(data)

                        If result.GetValue("message").ToString = "data_null" Then
                            dt_hasil.Rows.Clear()
                        End If
                    Else
                        'par_error.Add("No data")
                        dt_hasil = Nothing
                    End If
                Else
                    par_error.Add(result.GetValue("message").ToString & vbNewLine & par_sql)
                    dt_hasil = Nothing
                End If

            Else
                par_error.Add("No response" & vbNewLine & par_sql)
                dt_hasil = Nothing

            End If


            Return dt_hasil
        Catch ex As Exception
            '_MakeReport(ex.Message)
            par_error.Add(ex.Message & vbNewLine & par_sql)
            dt_hasil = Nothing
            Return dt_hasil
        End Try
    End Function

    Public Shared Function get_sql_mysql(ByVal par_sql As String, ByVal par_error As ArrayList, Optional ByVal par_option As String = "") As DataTable
        Dim dt_hasil As New DataTable
        Try

            Dim dictData As New Dictionary(Of String, Object)
            dictData.Add("type", "select_null_mysql")
            dictData.Add("query", par_sql)
            dictData.Add("option", par_option)
            dictData.Add("user", master_new.ClsVar.sNama)
            dictData.Add("token", "93020291-b256-4572-a82e-d3acef8baa05")

            Dim _response As String = postData(dictData, "post", _url_api)

            If _response <> "" Then
                Dim result As JObject = JObject.Parse(_response.ToString)
                Dim _status As Boolean

                _status = result.GetValue("status")
                If _status = True Then

                    Dim data As String = result.GetValue("data").ToString()
                    If data <> "" And data <> "[]" Then


                        dt_hasil = GetJSONToDataTableUsingNewtonSoftDll(data)

                        If result.GetValue("message").ToString = "data_null" Then
                            dt_hasil.Rows.Clear()
                        End If
                    Else
                        'par_error.Add("No data")
                        dt_hasil = Nothing
                    End If
                Else
                    par_error.Add(result.GetValue("message").ToString & vbNewLine & par_sql)
                    dt_hasil = Nothing
                End If

            Else
                par_error.Add("No response" & vbNewLine & par_sql)
                dt_hasil = Nothing

            End If


            Return dt_hasil
        Catch ex As Exception
            '_MakeReport(ex.Message)
            par_error.Add(ex.Message & vbNewLine & par_sql)
            dt_hasil = Nothing
            Return dt_hasil
        End Try
    End Function

    Public Shared Function get_sql_duckdb(ByVal par_sql As String, ByVal par_error As ArrayList, Optional ByVal par_option As String = "") As DataTable
        Dim dt_hasil As New DataTable
        Try
            Dim p_sql = par_sql.Replace("`", "").Replace("/", "-")

            Dim dictData As New Dictionary(Of String, Object)
            dictData.Add("type", "select_null_duckdb")
            dictData.Add("query", p_sql)
            dictData.Add("option", par_option)
            dictData.Add("user", master_new.ClsVar.sNama)
            dictData.Add("token", "93020291-b256-4572-a82e-d3acef8baa05")

            Dim _response As String = postData(dictData, "post", _url_api)

            If _response <> "" Then
                Dim result As JObject = JObject.Parse(_response.ToString)
                Dim _status As Boolean

                _status = result.GetValue("status")
                If _status = True Then

                    Dim data As String = result.GetValue("data").ToString()
                    If data <> "" And data <> "[]" Then


                        dt_hasil = GetJSONToDataTableUsingNewtonSoftDll(data)

                        If result.GetValue("message").ToString = "data_null" Then
                            dt_hasil.Rows.Clear()
                        End If
                    Else
                        'par_error.Add("No data")
                        dt_hasil = Nothing
                    End If
                Else
                    par_error.Add(result.GetValue("message").ToString & vbNewLine & par_sql)
                    dt_hasil = Nothing
                End If

            Else
                par_error.Add("No response" & vbNewLine & par_sql)
                dt_hasil = Nothing

            End If


            Return dt_hasil
        Catch ex As Exception
            '_MakeReport(ex.Message)
            par_error.Add(ex.Message & vbNewLine & par_sql)
            dt_hasil = Nothing
            Return dt_hasil
        End Try
    End Function

    Public Shared Function get_sql_ds(ByVal par_sql As String, ByVal par_table As String, ByVal par_error As ArrayList) As DataSet
        Dim ds_hasil As New DataSet
        Dim dt_temp As New DataTable
        Try



            Dim dictData As New Dictionary(Of String, Object)
            dictData.Add("type", "select")
            dictData.Add("query", par_sql)
            dictData.Add("user", master_new.ClsVar.sNama)
            dictData.Add("token", "93020291-b256-4572-a82e-d3acef8baa05")
            dictData.Add("dbname", _dbname)
            dictData.Add("server", _server)


            Dim _response As String = postData(dictData, "post", _url_api)

            If _response <> "" Then
                Dim result As JObject = JObject.Parse(_response.ToString)
                Dim _status As Boolean

                _status = result.GetValue("status")
                If _status = True Then

                    Dim data As String = result.GetValue("data").ToString()
                    If data <> "" Then

                        dt_temp = GetJSONToDataTableUsingNewtonSoftDll(data)
                        If par_table <> "" Then
                            dt_temp.TableName = par_table
                        Else
                            'par_error.Add("No data")
                            ds_hasil = Nothing
                        End If

                    End If
                Else
                    par_error.Add(result.GetValue("message").ToString)
                    ds_hasil = Nothing
                End If

            Else
                par_error.Add("No response")
                ds_hasil = Nothing
            End If



            ds_hasil.Tables.Add(dt_temp)


            Return ds_hasil
        Catch ex As Exception
            '_MakeReport(ex.Message)
            par_error.Add(ex.Message)
            ds_hasil = Nothing
            Return ds_hasil
        End Try
    End Function


    '=================fungsi tambahan ====================
    Public Shared Function GetNewID(ByVal Tbl As String, ByVal Fld As String) As Integer
        Dim NewRow As DataRow
        Dim sSQL As String

        GetNewID = -1
        sSQL = "SELECT COUNT(" & Fld & ") AS Co, Max(" & Fld & ") + 1 AS ID FROM " & Tbl
        NewRow = GetRowInfo(sSQL)

        If CType(NewRow("Co"), Integer) = 0 Then
            GetNewID = 1
        Else
            GetNewID = CType(NewRow("ID"), Integer)
        End If

    End Function
    Public Shared Function GetNewID(ByVal Tbl As String, ByVal Fld As String, ByVal fldKey As String, ByVal filter As String) As Integer
        Dim NewRow As DataRow
        Dim sSQL As String

        GetNewID = -1
        sSQL = "SELECT COUNT(" & Fld & ") AS Co, Max(" & Fld & ") + 1 AS ID FROM " & Tbl _
                & " where " & fldKey & "='" & filter & "'"

        NewRow = GetRowInfo(sSQL)

        If CType(NewRow("Co"), Integer) = 0 Then
            GetNewID = 1
        Else
            GetNewID = CType(NewRow("ID"), Integer)
        End If

    End Function

    Public Shared Function GetNewNumber(ByVal Tbl As String, ByVal Fld As String, ByVal digit As String) As Integer
        Dim NewRow As DataRow
        Dim sSQL As String

        GetNewNumber = -1
        sSQL = "SELECT COUNT (" & Fld & ") AS Co, to_number(MAX(RIGHT(" & Fld & "," & digit & ")),'999999999999999') + 1 AS ID FROM " & Tbl
        NewRow = GetRowInfo(sSQL)

        If CType(NewRow("Co"), Integer) = 0 Then
            GetNewNumber = 1
        Else
            GetNewNumber = CType(NewRow("ID"), Integer)
        End If

    End Function
    Public Shared Function GetNewNumberYM(ByVal Tbl As String, ByVal Fld As String, _
               ByVal digit As Integer, ByVal Filter As String, _
               Optional ByVal ModeFull As Boolean = True) As String

        Dim _error As New ArrayList
        Dim NewRow As DataRow
        Dim sSQL As String
        Dim FormatHasil As String = ""

        For i As Integer = 1 To digit
            FormatHasil = FormatHasil & "0"
        Next

        GetNewNumberYM = -1
        sSQL = "SELECT coalesce(cast(RIGHT(MAX(" & Fld _
        & ")," & digit & ") as integer),0) + 1 AS ID FROM " & Tbl _
        & " Where left(" & Fld & "," & Len(Filter) & ")='" & Filter & "'"
        'NewRow = GetRowInfo(sSQL)
        NewRow = GetRowInfoJSON(sSQL, _error)
        If ModeFull = True Then
            GetNewNumberYM = Filter & CType(NewRow("ID"), Integer).ToString(FormatHasil)
        Else
            GetNewNumberYM = CType(NewRow("ID"), Integer)
        End If
    End Function
    Public Shared Function GetNewNumberYMChild(ByVal Tbl As String, ByVal Fld As String, _
       ByVal FldKunci As String, ByVal digit As Integer, ByVal Filter As String, _
       Optional ByVal ModeFull As Boolean = True) As String

        Dim NewRow As DataRow
        Dim sSQL As String
        Dim FormatHasil As String = ""

        For i As Integer = 1 To digit
            FormatHasil = FormatHasil & "0"
        Next

        GetNewNumberYMChild = -1
        sSQL = "SELECT COUNT (" & Fld & ") AS Co, MAX(RIGHT(" & Fld & "," & digit & ")) + 1 AS ID FROM " & Tbl _
        & " Where (" & FldKunci & ")='" & Filter & "'"

        NewRow = GetRowInfo(sSQL)

        If ModeFull = True Then
            If CType(NewRow("Co"), Integer) = 0 Then
                GetNewNumberYMChild = Filter & CType(1, Integer).ToString(FormatHasil)
            Else
                GetNewNumberYMChild = Filter & CType(NewRow("ID"), Integer).ToString(FormatHasil)
            End If
        Else
            If CType(NewRow("Co"), Integer) = 0 Then
                GetNewNumberYMChild = CType(1, Integer)
            Else
                GetNewNumberYMChild = CType(NewRow("ID"), Integer)
            End If
        End If


    End Function


    Public Shared Function GetIDByName(ByVal Tbl As String, ByVal fld As String, ByVal fldName As String, ByVal fldVal As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow

        GetIDByName = "0"
        sSQL = "SELECT " & fld & " FROM " & Tbl & " WHERE " & fldName & " = '" & fldVal & "'"
        Dim _error As New ArrayList

        NewRow = GetRowInfoJSON(sSQL, _error)

        If NewRow Is Nothing Then
            'ini handle eror
            GetIDByName = ""
        Else
            GetIDByName = CType(SetString(NewRow(fld)), String)
        End If

    End Function

    Public Shared Function GetIDByName2(ByVal Tbl As String, ByVal fld As String, ByVal fldName As String, ByVal fldVal As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow

        GetIDByName2 = "0"
        sSQL = "SELECT " & fld & " FROM " & Tbl & " WHERE " & fldName & " = " & fldVal

        Dim _error As New ArrayList

        NewRow = GetRowInfoJSON(sSQL, _error)

        If NewRow Is Nothing Then
            'ini handle eror
            GetIDByName2 = ""
        Else
            GetIDByName2 = CType(SetString(NewRow(fld)), String)
        End If

    End Function

    Public Shared Function GetIDByName2Prmtr(ByVal Tbl As String, ByVal fld As String, ByVal fldName1 As String, ByVal fldVal1 As String, ByVal fldName2 As String, ByVal fldVal2 As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow

        GetIDByName2Prmtr = "0"
        sSQL = "SELECT " & fld & " FROM " & Tbl & " WHERE " & fldName1 & " = '" & fldVal1 & "' and " & fldName2 & " = " & fldVal2 & ""

        NewRow = GetRowInfo(sSQL)
        If NewRow Is Nothing Then
            GetIDByName2Prmtr = ""
        Else
            GetIDByName2Prmtr = CType(SetString(NewRow(fld)), String)
        End If


    End Function

    Public Shared Function GetNameByID(ByVal Tbl As String, ByVal fld As String, ByVal fldName As String, ByVal fldVal As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow
        Dim _error As New ArrayList

        GetNameByID = ""
        sSQL = "SELECT " & fldName & " FROM " & Tbl & " WHERE " & fld & " = " & fldVal
        NewRow = GetRowInfoJSON(sSQL, _error)

        If NewRow Is Nothing Then
            'ini handle eror
            GetNameByID = ""
        Else
            GetNameByID = CType(SetString(NewRow(fldName)), String)
        End If
    End Function

    Public Shared Function CekTahun() As String
        Try
            Dim sSQL As String
            Dim NewRow As DataRow

            'GetIDByName = "0"
            sSQL = "SELECT LOCALTIME as Thn"
            NewRow = GetRowInfo(sSQL)

            CekTahun = NewRow("Thn")
        Catch ex As Exception
            CekTahun = ""
        End Try

    End Function

    Public Shared Function CekTanggal() As Date
        Try
            Dim sSQL As String
            Dim NewRow As DataRow

            'GetIDByName = "0"
            sSQL = "SELECT LOCALTIMESTAMP as Tgl"

            'NewRow = GetRowInfo(sSQL)
            Dim _error As New ArrayList

            NewRow = GetRowInfoJSON(sSQL, _error)

            CekTanggal = NewRow("Tgl")
        Catch ex As Exception
            CekTanggal = Nothing
        End Try
    End Function

    Public Shared Function CekJam() As String
        Try
            Dim sSQL As String
            Dim NewRow As DataRow

            'GetIDByName = "0"
            sSQL = "SELECT LOCALTIME as Jam"
            NewRow = GetRowInfo(sSQL)

            CekJam = NewRow("Jam")
        Catch ex As Exception
            CekJam = ""
        End Try

    End Function

    Public Shared Sub InsertCombo(ByVal sql As String, ByVal Cbo As Windows.Forms.ComboBox)
        Try

            Dim dt As New DataTable
            dt = GetTableData(sql)

            Cbo.Items.Clear()
            For Each row As DataRow In dt.Rows
                Cbo.Items.Add(row(0))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Shared Sub InsertCombo(ByVal sql As String, ByVal Cbo As DevExpress.XtraEditors.ComboBoxEdit)
        Try

            Dim dt As New DataTable
            dt = GetTableData(sql)

            Cbo.Properties.Items.Clear()
            For Each row As DataRow In dt.Rows
                Cbo.Properties.Items.Add(row(0))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Function GetTableDataPaging(ByVal Sql As String, ByVal Mulai As Integer, _
   ByVal JmlPerPage As Integer, ByVal OrderBy As String) As DataTable

        Dim sSql As String

        sSql = Sql & " Order By " & OrderBy & " limit " & JmlPerPage & " OFFSET " & Mulai
        'MsgBox(sSql)
        Dim sDa As New PgSqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()

    End Function

    Public Shared Function CekRowSelect(ByVal Sql As String) As Integer
        Dim NewSql As String
        NewSql = "Select count (*) from (" & Sql & ") a"

        'Dim sDa As New PgSqlDataAdapter(NewSql, DbConString)
        Dim Dt As New DataTable
        'sDa.Fill(Dt)
        Dim _error As New ArrayList
        Dt = GetTableDataJson(NewSql, _error)

        Return CType(Dt.Rows(0).Item(0), Integer)

        Dt.Dispose()
        'sDa.Dispose()

    End Function

    Public Shared Function GetTableDataTop(ByVal Sql As String, ByVal Jml As Integer, ByVal OrderBy As String) As DataTable
        Dim sSql As String

        sSql = "Select  * From ( " & Sql & " ) a Order By " & OrderBy & " limit " & Jml

        Dim sDa As New PgSqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()
    End Function

    Public Shared Function ReportDataset(ByVal sSql As String) As DataSet
        Try
            ' Create a connection.
            'Dim Connection As New PgSqlConnection(DbConString())

            '' Create a data adapter and a dataset.
            'Dim Adapter As New PgSqlDataAdapter(sSql, Connection)
            Dim DataSet1 As New DataSet()

            '' Specify the data adapter and the data source for the report.
            '' Note that you must fill the datasource with data because it is not bound directly to the specified data adapter.
            ''report.DataAdapter = Adapter
            'Adapter.SelectCommand.CommandTimeout = 240
            'Adapter.Fill(DataSet1, "Table")

            GetDataset(sSql, DataSet1, "Table")

            Return DataSet1

        Catch ex As Exception
            Pesan(Err)
            Return Nothing
        End Try
    End Function
    Public Shared Function sSQLsSync(ByVal par_ssqls As ArrayList, ByVal par_error As ArrayList) As Boolean
        Try

            For Each Sql As String In FinsertSQL2Array(par_ssqls)
                par_ssqls.Add(Sql)
            Next

            Return True
        Catch ex As Exception
            par_error.Add(ex.Message)
            Return False
        End Try
    End Function
    Public Shared Function FinsertSQL2Array(ByVal List As ArrayList) As ArrayList
        Dim sSqls As New ArrayList
        Dim sSql As String
        Dim i As Integer = 1
        Try
            For Each Sql As String In List

                sSql = "INSERT INTO  " _
                    & "  public.t_sql_out " _
                    & "( " _
                    & "  sql_uid, " _
                    & "  seq, " _
                    & "  sql_command, " _
                    & "  mili_second, " _
                    & "  waktu " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & i & ",  " _
                    & SetSetring(Sql) & ",  " _
                    & "cast(to_char(now(),'MS') as integer)" & ",  " _
                    & "now()" & "  " _
                    & ")"

                sSqls.Add(sSql)
                sSql = ""

                i = i + 1
            Next
            Return sSqls
        Catch ex As Exception
            Pesan(Err)
            Return sSqls
        End Try


    End Function

    Public Shared Function status_sync() As Boolean
        Try
            'Dim ssql As String = "select conf_value from conf_file  where conf_name ='sync_status'"

            'Dim dr As DataRow
            'dr = GetRowInfo(ssql)

            'If dr Is Nothing Then
            '    'Box("Sorry, configuration " & par_type & " doesn't exist")
            '    ' get_conf_file
            '    Return True
            'ElseIf dr(0) Is System.DBNull.Value Then
            '    Return True

            'ElseIf dr(0).ToString = "0" Then
            '    Return False

            'ElseIf dr(0).ToString = "1" Then
            '    Return True
            'Else
            '    Return True
            'End If

            Dim ssql As String = "select conf_value from conf_file  where conf_name ='sync_status'"

            'Dim dr As DataRow
            'dr = GetRowInfo(ssql)

            Dim _error As New ArrayList
            Dim dt_temp As New DataTable

            dt_temp = get_sql(ssql, _error)
            If _error.Count > 0 Then
                Box(_error(0).ToString)
                Return False
            Else
                For Each dr As DataRow In dt_temp.Rows
                    If dr(0).ToString = "0" Then
                        Return False
                    ElseIf dr(0).ToString = "1" Then
                        Return True
                    Else
                        Return True
                    End If
                Next
            End If


        Catch ex As Exception
            Return True
        End Try
    End Function

End Class

