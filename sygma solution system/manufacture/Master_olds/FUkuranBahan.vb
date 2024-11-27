﻿Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FUkuranBahan

    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssql As String
    Dim dt_edit As New DataTable

    Private Sub FSite_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Kode", "kode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Laminasi", "laminasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Foil", "foil", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Casemaker", "casemaker", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Spot", "spot", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_edit, "Kode", "kode", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Laminasi", "laminasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Foil", "foil", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Casemaker", "casemaker", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Spot", "spot", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.kode, " _
                    & "  a.laminasi, " _
                    & "  a.foil, " _
                    & "  a.casemaker, " _
                    & "  a.spot " _
                    & "FROM " _
                    & "  public.cetak_bahan a " _
                    & "ORDER BY " _
                    & "  a.kode"




        Return get_sequel
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            ssql = "SELECT  " _
                    & "  a.kode, " _
                    & "  a.laminasi, " _
                    & "  a.foil, " _
                    & "  a.casemaker, " _
                    & "  a.spot " _
                    & "FROM " _
                    & "  public.cetak_bahan a " _
                    & "ORDER BY " _
                    & "  a.kode"

            dt_edit = master_new.PGSqlConn.GetTableData(ssql)

            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        dt_edit.AcceptChanges()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE from cetak_bahan  "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.cetak_bahan " _
                                        & "( " _
                                        & "  kode, " _
                                        & "  laminasi,foil,casemaker, " _
                                        & "  spot" _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(dr("kode")) & ",  " _
                                        & SetDec(dr("laminasi")) & ",  " _
                                        & SetDec(dr("foil")) & ",  " _
                                        & SetDec(dr("casemaker")) & ",  " _
                                        & SetDec(dr("spot")) & "  " _
                                        & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Daftar Ukuran Bahan ")
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If
                        sqlTran.Commit()

                        after_success()
                        ' set_row(Trim(op_code.Text), "op_code")
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
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Private Sub BtImportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImportExcel.Click
        Try

            Dim filex As String = ""
            filex = AskOpenFile("Xls Files | *.xls")

            If filex = "" Then
                Exit Sub
            End If

            gc_edit.DataSource = Nothing

            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(filex)

            dt_edit.Rows.Clear()

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim _row_new As DataRow
                _row_new = dt_edit.NewRow

                _row_new("kode") = dr("Kode")
                _row_new("harga_jns_kertas") = dr("Jenis")
                _row_new("harga_ukuran_plano") = dr("Ukuran Plano")
                _row_new("harga_panjang") = dr("Panjang")
                _row_new("harga_lebar") = dr("Lebar")
                _row_new("harga_kg") = dr("Harga Kg")
                _row_new("harga_rim") = dr("Harga Rim")
                _row_new("harga_gramasi") = dr("Harga Gramasi")

                dt_edit.Rows.Add(_row_new)
            Next
            dt_edit.AcceptChanges()


            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
