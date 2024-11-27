<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptDashboardKeuanganSubBS
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
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand
        Me.DsBalanceSheet1 = New sygma_solution_system.DsBalanceSheet
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.GroupFooter2 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrTable3 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.GroupFooter3 = New DevExpress.XtraReports.UI.GroupFooterBand
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsBalanceSheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.XrTable2.Size = New System.Drawing.Size(847, 42)
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
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.bs_caption", "")})
        Me.XrTableCell4.Dpi = 254.0!
        Me.XrTableCell4.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.Weight = 1.6309012875536479
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.z_nilai", "")})
        Me.XrTableCell6.Dpi = 254.0!
        Me.XrTableCell6.ForeColor = System.Drawing.Color.White
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.StylePriority.UseForeColor = False
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n0}"
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
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.GroupHeader1.Dpi = 254.0!
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("bs_number", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.Height = 42
        Me.GroupHeader1.Level = 1
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.Height = 0
        Me.ReportFooter.Name = "ReportFooter"
        '
        'DsBalanceSheet1
        '
        Me.DsBalanceSheet1.DataSetName = "DsBalanceSheet"
        Me.DsBalanceSheet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.GroupHeader3.Dpi = 254.0!
        Me.GroupHeader3.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("bsd_number", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader3.Height = 42
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'XrTable1
        '
        Me.XrTable1.Dpi = 254.0!
        Me.XrTable1.Location = New System.Drawing.Point(21, 0)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(826, 42)
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
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.bsd_caption", "")})
        Me.XrTableCell1.Dpi = 254.0!
        Me.XrTableCell1.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.Text = "XrTableCell4"
        Me.XrTableCell1.Weight = 1.6309012875536479
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.z_nilai", "")})
        Me.XrTableCell2.Dpi = 254.0!
        Me.XrTableCell2.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:n2}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell2.Summary = XrSummary2
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell2.Weight = 0.75107296137339052
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Dpi = 254.0!
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("bs_group", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader2.Height = 0
        Me.GroupHeader2.Level = 2
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Dpi = 254.0!
        Me.GroupFooter1.Height = 0
        Me.GroupFooter1.Level = 1
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'GroupFooter2
        '
        Me.GroupFooter2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable3})
        Me.GroupFooter2.Dpi = 254.0!
        Me.GroupFooter2.Height = 42
        Me.GroupFooter2.Level = 2
        Me.GroupFooter2.Name = "GroupFooter2"
        '
        'XrTable3
        '
        Me.XrTable3.Dpi = 254.0!
        Me.XrTable3.Location = New System.Drawing.Point(0, 0)
        Me.XrTable3.Name = "XrTable3"
        Me.XrTable3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow3})
        Me.XrTable3.Size = New System.Drawing.Size(847, 42)
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell3, Me.XrTableCell5})
        Me.XrTableRow3.Dpi = 254.0!
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.bs_remarks", "")})
        Me.XrTableCell3.Dpi = 254.0!
        Me.XrTableCell3.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.Text = "XrTableCell4"
        Me.XrTableCell3.Weight = 1.6309012875536479
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.z_nilai", "")})
        Me.XrTableCell5.Dpi = 254.0!
        Me.XrTableCell5.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseFont = False
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        XrSummary3.FormatString = "{0:n2}"
        XrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell5.Summary = XrSummary3
        Me.XrTableCell5.Text = "XrTableCell5"
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell5.Weight = 0.75107296137339052
        '
        'GroupFooter3
        '
        Me.GroupFooter3.Dpi = 254.0!
        Me.GroupFooter3.Height = 0
        Me.GroupFooter3.Name = "GroupFooter3"
        '
        'rptDashboardKeuanganSubBS
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.ReportFooter, Me.Detail, Me.ReportHeader, Me.GroupHeader1, Me.GroupHeader3, Me.GroupHeader2, Me.GroupFooter1, Me.GroupFooter2, Me.GroupFooter3})
        Me.DataMember = "Table"
        Me.DataSource = Me.DsBalanceSheet1
        Me.Dpi = 254.0!
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsBalanceSheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents DsBalanceSheet1 As sygma_solution_system.DsBalanceSheet
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents GroupFooter2 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents GroupFooter3 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrTable3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
End Class
