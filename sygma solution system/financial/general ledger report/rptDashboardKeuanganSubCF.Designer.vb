<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptDashboardKeuanganSubCF
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel
        Me.Ds_CashFlow1 = New sygma_solution_system.Ds_CashFlow
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ds_CashFlow1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("sub_header", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 0
        Me.ReportHeader.Name = "ReportHeader"
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.GroupHeader2.Dpi = 254.0!
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("sort_number", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader2.Height = 43
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'XrTable1
        '
        Me.XrTable1.Dpi = 254.0!
        Me.XrTable1.Location = New System.Drawing.Point(0, 0)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(910, 42)
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2})
        Me.XrTableRow1.Dpi = 254.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.remark_header", "")})
        Me.XrTableCell1.Dpi = 254.0!
        Me.XrTableCell1.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.Text = "XrTableCell4"
        Me.XrTableCell1.Weight = 1.6309012875536479
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_value", "")})
        Me.XrTableCell2.Dpi = 254.0!
        Me.XrTableCell2.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n2}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell2.Summary = XrSummary1
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell2.Weight = 0.75107296137339052
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel14, Me.XrLabel13, Me.XrLabel12, Me.XrLabel10, Me.XrLabel9})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.Height = 143
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_value", "")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel1.Location = New System.Drawing.Point(614, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(296, 48)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:n2}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrLabel1.Summary = XrSummary2
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel14
        '
        Me.XrLabel14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_value_ending", "{0:n2}")})
        Me.XrLabel14.Dpi = 254.0!
        Me.XrLabel14.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel14.Location = New System.Drawing.Point(614, 85)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel14.Size = New System.Drawing.Size(296, 58)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "XrLabel14"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_value_beginning", "{0:n2}")})
        Me.XrLabel13.Dpi = 254.0!
        Me.XrLabel13.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel13.Location = New System.Drawing.Point(614, 42)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel13.Size = New System.Drawing.Size(296, 53)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "XrLabel13"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel12.Location = New System.Drawing.Point(0, 90)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(614, 48)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.Text = "Kas dan Setara Kas Akhir Periode"
        '
        'XrLabel10
        '
        Me.XrLabel10.Dpi = 254.0!
        Me.XrLabel10.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel10.Location = New System.Drawing.Point(0, 42)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel10.Size = New System.Drawing.Size(614, 48)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "Kas dan Setara Kas Awal Periode"
        '
        'XrLabel9
        '
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel9.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel9.Size = New System.Drawing.Size(614, 48)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "Kenaikan / Penurunan Kas dan Setara Kas"
        '
        'Ds_CashFlow1
        '
        Me.Ds_CashFlow1.DataSetName = "Ds_CashFlow"
        Me.Ds_CashFlow1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'rptDashboardKeuanganSubCF
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.ReportFooter, Me.Detail, Me.ReportHeader, Me.GroupHeader2})
        Me.DataMember = "Table"
        Me.DataSource = Me.Ds_CashFlow1
        Me.Dpi = 254.0!
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ds_CashFlow1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Ds_CashFlow1 As sygma_solution_system.Ds_CashFlow
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
End Class
