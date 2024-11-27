Imports master_new.ModFunction
Imports CoreLab.PostgreSql

Public Class FSalesOrderRelatedSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Public _sopj_oid_par As String
    Public _seq_par As Integer

    Private Sub FSalesOrderRelatedSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Number", "sopj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Related Number", "sotran_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Related Date", "sotran_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Qty", "qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If  fobject.name = "FSalesOrderProjectTransaction" Then
            get_sequel = "select  " _
                    & " en_desc, " _
                    & " sopj_code, " _
                    & " sopj_date, " _
                    & " sotran_oid, " _
                    & " sotran_code, " _
                    & " sotran_date, " _
                    & " sopjd_pt_id, " _
                    & " pt_code, " _
                    & " pt_desc1, " _
                    & " pt_desc2, " _
                    & " sotrand_qty, " _
                    & " sotrand_qty - coalesce(sotrand_qty_full,0) as qty_open, " _
                    & " sotrand_oid, " _
                    & " sotrand_sopjd_oid, " _
                    & " sopjd_oid, " _
                    & " sotrand_oid " _
                    & " from sotran_mstr " _
                    & " inner join en_mstr on en_id = sotran_en_id " _
                    & " inner join sotrand_det on sotrand_sotran_oid = sotran_oid " _
                    & " inner join sopjd_det on sopjd_oid = sotrand_sopjd_oid " _
                    & " inner join sopj_mstr on sopj_oid = sopjd_sopj_oid " _
                    & " inner join pt_mstr on pt_id = sopjd_pt_id " _
                    & " where sotran_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sotran_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and sotran_en_id = " + _en_id.ToString _
                    & " and coalesce(sotran_type,'') = ''" _
                    & " and coalesce(sotran_trans_id,'')  = ''" _
                    & " and sotran_seq = " + (_seq_par - 1).ToString _
                    & " and sotran_sopj_oid = " + SetSetring(_sopj_oid_par) _
                    & " and sotrand_qty - coalesce(sotrand_qty_full,0) > 0"
        ElseIf fobject.name = "FSalesOrderProjectTransactionDP" Then
            get_sequel = "select  " _
                    & " en_desc, " _
                    & " sopj_code, " _
                    & " sopj_date, " _
                    & " sotran_oid, " _
                    & " sotran_code, " _
                    & " sotran_date, " _
                    & " sopjd_pt_id, " _
                    & " pt_code, " _
                    & " pt_desc1, " _
                    & " pt_desc2, " _
                    & " sotrand_qty, " _
                    & " sotrand_qty - coalesce(sotrand_qty_full,0) as qty_open, " _
                    & " sotrand_oid, " _
                    & " sotrand_sopjd_oid, " _
                    & " sopjd_oid, " _
                    & " sotrand_oid " _
                    & " from sotran_mstr " _
                    & " inner join en_mstr on en_id = sotran_en_id " _
                    & " inner join sotrand_det on sotrand_sotran_oid = sotran_oid " _
                    & " inner join sopjd_det on sopjd_oid = sotrand_sopjd_oid " _
                    & " inner join sopj_mstr on sopj_oid = sopjd_sopj_oid " _
                    & " inner join pt_mstr on pt_id = sopjd_pt_id " _
                    & " where sotran_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sotran_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and coalesce(sotran_type,'') = 'DP'" _
                    & " and sotran_en_id = " + _en_id.ToString _
                    & " and coalesce(sotran_trans_id,'')  = ''" _
                    & " and sotran_seq = " + (_seq_par - 1).ToString _
                    & " and sotran_sopj_oid = " + SetSetring(_sopj_oid_par) _
                    & " and sotrand_qty - coalesce(sotrand_qty_full,0) > 0"
        End If
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FSalesOrderProjectTransaction" Then
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_oid", ds.Tables(0).Rows(_row_gv).Item("sopjd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "sotrand_related_oid", ds.Tables(0).Rows(_row_gv).Item("sotrand_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "sotran_code_related", ds.Tables(0).Rows(_row_gv).Item("sotran_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))

            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "qty_open", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "sotrand_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))

            fobject.gv_edit.BestFitColumns()
        ElseIf fobject.name = "FSalesOrderProjectTransactionDP" Then
            fobject.gv_edit.SetRowCellValue(_row, "sopjd_oid", ds.Tables(0).Rows(_row_gv).Item("sopjd_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "sotrand_related_oid", ds.Tables(0).Rows(_row_gv).Item("sotrand_oid"))
            fobject.gv_edit.SetRowCellValue(_row, "sotran_code_related", ds.Tables(0).Rows(_row_gv).Item("sotran_code"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_code", ds.Tables(0).Rows(_row_gv).Item("pt_code"))

            fobject.gv_edit.SetRowCellValue(_row, "pt_desc1", ds.Tables(0).Rows(_row_gv).Item("pt_desc1"))
            fobject.gv_edit.SetRowCellValue(_row, "pt_desc2", ds.Tables(0).Rows(_row_gv).Item("pt_desc2"))
            fobject.gv_edit.SetRowCellValue(_row, "qty_open", ds.Tables(0).Rows(_row_gv).Item("qty_open"))
            fobject.gv_edit.SetRowCellValue(_row, "sotrand_qty", ds.Tables(0).Rows(_row_gv).Item("qty_open"))

            fobject.gv_edit.BestFitColumns()
        End If
    End Sub
End Class
