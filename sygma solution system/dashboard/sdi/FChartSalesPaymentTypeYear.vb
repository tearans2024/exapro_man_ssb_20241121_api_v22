﻿Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports System.Drawing
Imports DevExpress.XtraCharts

Public Class FChartSalesPaymentTypeYear
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _now = func_coll.get_now

        pr_txtyear.DateTime = _now
        'UpdateControls()
        'InitExplodeModeComboBox()
        chart_type.EditValue = "Bar Type"
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        Dim ssql As String
        Try
            Dim _en_id_all As String
            _en_id_all = get_en_id_child(1)

            ssql = ""
            Dim _kode As String = ""
            Dim _filter As String = ""
            For i As Integer = 1 To 12
                _kode = pr_txtyear.Text & Format(i, "00")
                _filter = Format(i, "00")
                ssql = ssql + "select  " _
                            & "ngroup,   " _
                            & "nvalue, " _
                            & "payment_type  " _
                            & "from   " _
                            & "  (select  " _
                            & "  cast('" & _kode & "' as varchar)  as ngroup, " _
                            & "  payment_type ,  " _
                            & "  count(payment_type) as nvalue   " _
                            & "  from (SELECT   x.code_name as payment_type , en_desc          " _
                            & "  FROM soshipd_det inner join soship_mstr on soship_oid = soshipd_soship_oid       " _
                            & "  inner join sod_det on sod_oid = soshipd_sod_oid       " _
                            & "  inner join so_mstr on so_oid = sod_so_oid       " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id       " _
                            & "  inner join en_mstr on en_id = soship_en_id       " _
                            & "  inner join code_mstr x on x.code_id = so_pay_type       " _
                            & "  inner join  (SELECT taxr_tax_class, taxr_rate as rate_ppn       " _
                            & "  FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type       " _
                            & "  where  code_name ~~* 'ppn') taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class       " _
                            & "  inner join  (SELECT taxr_tax_class, taxr_rate as rate_pph       " _
                            & "  FROM taxr_mstr inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type       " _
                            & "  where  code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = sod_tax_class       " _
                            & "  inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person       " _
                            & "  inner join ptnr_mstr bill_mstr on bill_mstr.ptnr_id = so_ptnr_id_bill       " _
                            & "  inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id       " _
                            & "  where to_char(soship_date,'yyyyMM')= '" & _kode & "' " _
                            & "  and so_en_id in (" & _en_id_all & ")) as temp    " _
                            & "  group by  " _
                            & "  payment_type) as temp UNION "



            Next

            ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 6)

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)

            ChartControl1.SeriesDataMember = "payment_type"
            ChartControl1.SeriesTemplate.ArgumentDataMember = "ngroup"
            ChartControl1.SeriesTemplate.ValueScaleType = ScaleType.Numerical
            ChartControl1.SeriesTemplate.ValueDataMembers.AddRange(New String() {"nvalue"})
            ChartControl1.SeriesTemplate.SeriesPointsSorting = SortingMode.Ascending
            ChartControl1.DataSource = dt

            For Each Series As Series In ChartControl1.Series
                If chart_type.EditValue.ToString.ToLower = "bar type" Then
                    Series.ChangeView(ViewType.Bar)
                Else
                    Series.ChangeView(ViewType.Line)
                End If
            Next
            pgc_master.DataSource = dt

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub load_data_many(ByVal par As Boolean)
        Dim ssql As String
        Try
            Dim _en_id_all As String


            _en_id_all = get_en_id_child(1)


            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)
            ChartControl1.Series(0).DataSource = dt

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub checkEditShowLabels_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkEditShowLabels.CheckedChanged
        For Each series As Series In ChartControl1.Series
            series.Label.Visible = Me.checkEditShowLabels.Checked
        Next series

    End Sub

    Public Overrides Function export_data() As Boolean
        Try
            Dim _file As String
            _file = AskSaveAsFile("Excel Files | *.xls")
            If _file.Length > 0 Then
                ChartControl1.ExportToXls(_file)
            End If
            Box("Sukses")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Function

    Private Sub chart_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chart_type.EditValueChanged
        Try

            For Each Series As Series In ChartControl1.Series
                If chart_type.EditValue.ToString.ToLower = "bar type" Then
                    Series.ChangeView(ViewType.Bar)
                Else
                    Series.ChangeView(ViewType.Line)
                End If

            Next
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
