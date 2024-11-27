<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRAccountSub
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRAccountSub))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.Detail.Height = 23
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("cogsca_seq", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.Location = New System.Drawing.Point(0, 0)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(483, 23)
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell8, Me.XrTableCell19})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ac_code", "")})
        Me.XrTableCell1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.Text = "XrTableCell1"
        Me.XrTableCell1.Weight = 0.67675102131810716
        '
        'XrTableCell8
        '
        Me.XrTableCell8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ac_name", "")})
        Me.XrTableCell8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.StylePriority.UseFont = False
        Me.XrTableCell8.Text = "XrTableCell8"
        Me.XrTableCell8.Weight = 1.5311959248456639
        '
        'XrTableCell19
        '
        Me.XrTableCell19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsca_amount", "{0:n}")})
        Me.XrTableCell19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.StylePriority.UseFont = False
        Me.XrTableCell19.StylePriority.UseTextAlignment = False
        Me.XrTableCell19.Text = "XrTableCell19"
        Me.XrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell19.Weight = 0.79205305383622948
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2, Me.XrLabel25, Me.XrLine1, Me.XrLine2})
        Me.ReportHeader.Height = 79
        Me.ReportHeader.Name = "ReportHeader"
        Me.ReportHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        '
        'XrTable2
        '
        Me.XrTable2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrTable2.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.XrTable2.Location = New System.Drawing.Point(0, 52)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(483, 23)
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell7, Me.XrTableCell14})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.CanGrow = False
        Me.XrTableCell4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.Text = "Account Code"
        Me.XrTableCell4.Weight = 0.55503766901408169
        '
        'XrTableCell7
        '
        Me.XrTableCell7.CanGrow = False
        Me.XrTableCell7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.StylePriority.UseFont = False
        Me.XrTableCell7.Text = "Account Name"
        Me.XrTableCell7.Weight = 1.2534047016454837
        '
        'XrTableCell14
        '
        Me.XrTableCell14.CanGrow = False
        Me.XrTableCell14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.StylePriority.UseFont = False
        Me.XrTableCell14.StylePriority.UseTextAlignment = False
        Me.XrTableCell14.Text = "Amount"
        Me.XrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell14.Weight = 0.647469452987728
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel25.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.Size = New System.Drawing.Size(123, 21)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = "Account Detail"
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(0, 46)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(483, 6)
        '
        'XrLine2
        '
        Me.XrLine2.Location = New System.Drawing.Point(0, 75)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(483, 4)
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = resources.GetString("OdbcConnection1.ConnectionString")
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("cogsca_cogsc_oid", "cogsca_cogsc_oid"), New System.Data.Common.DataColumnMapping("cogsca_seq", "cogsca_seq"), New System.Data.Common.DataColumnMapping("cogsca_ac_id", "cogsca_ac_id"), New System.Data.Common.DataColumnMapping("ac_code", "ac_code"), New System.Data.Common.DataColumnMapping("ac_name", "ac_name"), New System.Data.Common.DataColumnMapping("cogsca_amount", "cogsca_amount"), New System.Data.Common.DataColumnMapping("cogsca_oid", "cogsca_oid"), New System.Data.Common.DataColumnMapping("cogsca_type", "cogsca_type")})})
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine4})
        Me.ReportFooter.Height = 6
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLine4
        '
        Me.XrLine4.Location = New System.Drawing.Point(0, 0)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.Size = New System.Drawing.Size(483, 6)
        '
        'XRAccountSub
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.ReportHeader, Me.ReportFooter})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        Me.DrawGrid = False
        Me.GridSize = New System.Drawing.Size(2, 2)
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.PageHeight = 583
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.Version = "9.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    'Friend WithEvents DsCogsAccount1 As syspro_ver2.DsCogsAccount
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
End Class
