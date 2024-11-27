Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FSiteCostImport
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim sSQL As String
    Dim ds_import As DataSet

    Private Sub FInventoryReportDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Site", "Site", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "Entity", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Code", "Part Number Code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Desc1", "Part Number Desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Desc2", "Part Number Desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Set", "Cost Set", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Element Code", "Element Code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "Amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Private Sub be_import_excel_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_import_excel.ButtonClick
        Dim opendialog As New OpenFileDialog

        If opendialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            be_import_excel.Text = opendialog.FileName
            load_data_many(True)
        End If
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        If Trim(be_import_excel.Text) = "" Then
            Exit Sub
        End If

        ds_import = New DataSet
        ds_import = func_data.import_from_excel(be_import_excel.Text)

        If ds_import Is Nothing Then
            MsgBox("Data Cant Retrieve From Excel, Please Check Your File..", MsgBoxStyle.Critical, "Import Error")
            Exit Sub
        End If

        gc_master.DataSource = ds_import.Tables(0)
        gv_master.BestFitColumns()
    End Sub

    Public Function ImportFromExcel(ByVal PrmPathExcelFile As String) As DataSet
        ImportFromExcel = Nothing
        Dim MyConnection As System.Data.OleDb.OleDbConnection = Nothing

        Try
            Dim DtSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [sheet1$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "import_xls")

            DtSet = New System.Data.DataSet

            MyCommand.Fill(DtSet)
            MyConnection.Close()

            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
        End Try
    End Function

    Private Sub sb_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_import.Click
        If MessageBox.Show("Import Data...?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim sSQLs As New ArrayList

        For Each dr As DataRow In ds_import.Tables(0).Rows

            sSQL = "UPDATE  " _
                 & "  public.sctd_det   " _
                 & "SET  " _
                 & "  sctd_tl_amount = " & SetDec(dr("Amount")) & ",  " _
                 & "  sctd_amount = coalesce(sctd_ll_amount,0) + " & SetDec(dr("Amount")) & "  " _
                 & "WHERE  " _
                 & "  sctd_sct_oid = (select sct_oid from sct_mstr where  sct_pt_id=" _
                 & " (select pt_id from pt_mstr where pt_code='" & dr("Part Number Code") _
                 & "')  and sct_cs_id=(Select cs_id from cs_mstr where cs_name='" & dr("Cost Set") _
                 & "') and sct_en_id=(select en_id from en_mstr where en_desc='" & dr("Entity") & "')) and  " _
                 & "  sctd_csd_oid = (SELECT csd_oid FROM public.csd_det where csd_csc_id " _
                 & "=(select csc_id from public.csc_category where csc_code='" & dr("Element Code") _
                 & "') and  csd_cs_oid =(Select cs_oid from cs_mstr where cs_name='" & dr("Cost Set") & "')) "

            sSQLs.Add(sSQL)

            If dr("Element Code").ToString.Trim.ToUpper = "CMTL" Then

                sSQL = "update public.sct_mstr set sct_mtl_tl=" & SetDec(dr("Amount")) & ", " _
                  & "sct_total=coalesce(sct_lbr_tl,0)+coalesce(sct_bdn_tl,0)+coalesce(sct_ovh_tl,0)+coalesce(sct_sub_tl,0)+" _
                  & "coalesce(sct_mtl_ll,0)+coalesce(sct_lbr_ll,0)+coalesce(sct_bdn_ll,0)+coalesce(sct_ovh_ll,0)+" _
                  & "coalesce(sct_sub_ll,0)+" & SetDec(dr("Amount")) & " " _
                  & "where sct_pt_id=(select pt_id from pt_mstr where pt_code='" & dr("Part Number Code") & "') and " _
                  & "sct_cs_id=(Select cs_id from cs_mstr where cs_name='" & dr("Cost Set") & "') and " _
                  & "sct_en_id=(select en_id from en_mstr where en_desc='" & dr("Entity") & "')"

                sSQLs.Add(sSQL)

            ElseIf dr("Element Code").ToString.Trim.ToUpper = "COVH" Then

                sSQL = "update public.sct_mstr set sct_ovh_tl=" & SetDec(dr("Amount")) & ", " _
                  & "sct_total=coalesce(sct_lbr_tl,0)+coalesce(sct_bdn_tl,0)+coalesce(sct_mtl_tl,0)+coalesce(sct_sub_tl,0)+" _
                  & "coalesce(sct_mtl_ll,0)+coalesce(sct_lbr_ll,0)+coalesce(sct_bdn_ll,0)+coalesce(sct_ovh_ll,0)+" _
                  & "coalesce(sct_sub_ll,0)+" & SetDec(dr("Amount")) & " " _
                  & "where sct_pt_id=(select pt_id from pt_mstr where pt_code='" & dr("Part Number Code") & "') and " _
                  & "sct_cs_id=(Select cs_id from cs_mstr where cs_name='" & dr("Cost Set") & "') and " _
                  & "sct_en_id=(select en_id from en_mstr where en_desc='" & dr("Entity") & "')"

                sSQLs.Add(sSQL)
            End If

            master_new.PGSqlConn.DbRunTran(sSQLs)
            sSQLs.Clear()
        Next
        Box("Import Success")
    End Sub
End Class
