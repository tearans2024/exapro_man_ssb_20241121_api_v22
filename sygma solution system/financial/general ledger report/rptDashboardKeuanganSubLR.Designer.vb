<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptDashboardKeuanganSubLR
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
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.DsProfitLoss1 = New sygma_solution_system.DSProfitLoss
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsProfitLoss1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable2
        '
        Me.XrTable2.Dpi = 254.0!
        Me.XrTable2.Location = New System.Drawing.Point(0, 0)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(740, 43)
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell6})
        Me.XrTableRow2.Dpi = 254.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pls_item", "")})
        Me.XrTableCell4.Dpi = 254.0!
        Me.XrTableCell4.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.Weight = 1.6309012875536479
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.v_nilai", "")})
        Me.XrTableCell6.Dpi = 254.0!
        Me.XrTableCell6.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.StylePriority.UseFont = False
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n2}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell6.Summary = XrSummary1
        Me.XrTableCell6.Text = "XrTableCell6"
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell6.Weight = 0.75107296137339052
        '
        'ReportHeader
        '
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 0
        Me.ReportHeader.Name = "ReportHeader"
        '
        'DsProfitLoss1
        '
        Me.DsProfitLoss1.DataSetName = "DSProfitLoss"
        Me.DsProfitLoss1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.GroupHeader1.Dpi = 254.0!
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("pls_number", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.Height = 43
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Dpi = 254.0!
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("pl_number", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader2.Height = 0
        Me.GroupHeader2.Level = 1
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel12})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.Height = 48
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.v_nilai", "")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel1.Location = New System.Drawing.Point(466, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(275, 48)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:n2}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrLabel1.Summary = XrSummary2
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrLabel12.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(466, 48)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.Text = "Laba / Rugi Bersih"
        '
        'rptDashboardKeuanganSubLR
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.ReportFooter, Me.Detail, Me.ReportHeader, Me.GroupHeader1, Me.GroupHeader2})
        Me.DataMember = "Table"
        Me.DataSource = Me.DsProfitLoss1
        Me.Dpi = 254.0!
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsProfitLoss1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents DsProfitLoss1 As sygma_solution_system.DSProfitLoss
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
End Class
