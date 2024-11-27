<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRMaterialSub
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRMaterialSub))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell13 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
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
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("cogscr_seq", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTable1.Location = New System.Drawing.Point(0, 0)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(998, 23)
        Me.XrTable1.StylePriority.UseFont = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell8, Me.XrTableCell2, Me.XrTableCell10, Me.XrTableCell3, Me.XrTableCell12, Me.XrTableCell13, Me.XrTableCell15, Me.XrTableCell17, Me.XrTableCell19})
        Me.XrTableRow1.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.StylePriority.UseFont = False
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pt_code", "")})
        Me.XrTableCell1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.Text = "XrTableCell1"
        Me.XrTableCell1.Weight = 0.26344935861026869
        '
        'XrTableCell8
        '
        Me.XrTableCell8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pt_desc1", "")})
        Me.XrTableCell8.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.StylePriority.UseFont = False
        Me.XrTableCell8.Text = "XrTableCell8"
        Me.XrTableCell8.Weight = 0.53607214428857708
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pt_desc2", "")})
        Me.XrTableCell2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.Weight = 0.3878532466001523
        '
        'XrTableCell10
        '
        Me.XrTableCell10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogscr_qty", "{0:n}")})
        Me.XrTableCell10.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.StylePriority.UseFont = False
        Me.XrTableCell10.StylePriority.UseTextAlignment = False
        Me.XrTableCell10.Text = "XrTableCell10"
        Me.XrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell10.Weight = 0.16511900806961516
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.um_desc", "")})
        Me.XrTableCell3.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell3.Weight = 0.19630437345278784
        '
        'XrTableCell12
        '
        Me.XrTableCell12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pl_desc", "")})
        Me.XrTableCell12.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.StylePriority.UseFont = False
        Me.XrTableCell12.Text = "XrTableCell12"
        Me.XrTableCell12.Weight = 0.34534041344721511
        '
        'XrTableCell13
        '
        Me.XrTableCell13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ac_code", "")})
        Me.XrTableCell13.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell13.Name = "XrTableCell13"
        Me.XrTableCell13.StylePriority.UseFont = False
        Me.XrTableCell13.Text = "XrTableCell13"
        Me.XrTableCell13.Weight = 0.15914315261539119
        '
        'XrTableCell15
        '
        Me.XrTableCell15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ac_name", "")})
        Me.XrTableCell15.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.StylePriority.UseFont = False
        Me.XrTableCell15.Text = "XrTableCell15"
        Me.XrTableCell15.Weight = 0.43728700716941921
        '
        'XrTableCell17
        '
        Me.XrTableCell17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogscr_cost", "{0:n}")})
        Me.XrTableCell17.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.StylePriority.UseFont = False
        Me.XrTableCell17.StylePriority.UseTextAlignment = False
        Me.XrTableCell17.Text = "XrTableCell17"
        Me.XrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell17.Weight = 0.26673969596947911
        '
        'XrTableCell19
        '
        Me.XrTableCell19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogscr_cost_ext", "{0:n}")})
        Me.XrTableCell19.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.StylePriority.UseFont = False
        Me.XrTableCell19.StylePriority.UseTextAlignment = False
        Me.XrTableCell19.Text = "XrTableCell19"
        Me.XrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell19.Weight = 0.24269159977709434
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine4})
        Me.PageFooter.Height = 6
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLine4
        '
        Me.XrLine4.Location = New System.Drawing.Point(0, 0)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.Size = New System.Drawing.Size(996, 6)
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2, Me.XrLabel25, Me.XrLine1, Me.XrLine2})
        Me.ReportHeader.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportHeader.Height = 79
        Me.ReportHeader.Name = "ReportHeader"
        Me.ReportHeader.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        Me.ReportHeader.StylePriority.UseFont = False
        '
        'XrTable2
        '
        Me.XrTable2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrTable2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTable2.Location = New System.Drawing.Point(0, 50)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(998, 23)
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell7, Me.XrTableCell5, Me.XrTableCell9, Me.XrTableCell6, Me.XrTableCell11, Me.XrTableCell14, Me.XrTableCell16, Me.XrTableCell18, Me.XrTableCell20})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.CanGrow = False
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "Part Number"
        Me.XrTableCell4.Weight = 0.21669352228832625
        '
        'XrTableCell7
        '
        Me.XrTableCell7.CanGrow = False
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Text = "Description1"
        Me.XrTableCell7.Weight = 0.4387592820912361
        '
        'XrTableCell5
        '
        Me.XrTableCell5.CanGrow = False
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Text = "Description2"
        Me.XrTableCell5.Weight = 0.31723907235988785
        '
        'XrTableCell9
        '
        Me.XrTableCell9.CanGrow = False
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.StylePriority.UseTextAlignment = False
        Me.XrTableCell9.Text = "Qty"
        Me.XrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell9.Weight = 0.13476370829170276
        '
        'XrTableCell6
        '
        Me.XrTableCell6.CanGrow = False
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        Me.XrTableCell6.Text = "UM"
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell6.Weight = 0.15883163237945894
        '
        'XrTableCell11
        '
        Me.XrTableCell11.CanGrow = False
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.Text = "Product Line"
        Me.XrTableCell11.Weight = 0.28188886104381083
        '
        'XrTableCell14
        '
        Me.XrTableCell14.CanGrow = False
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.Text = "Account"
        Me.XrTableCell14.Weight = 0.13149868532409525
        '
        'XrTableCell16
        '
        Me.XrTableCell16.CanGrow = False
        Me.XrTableCell16.Name = "XrTableCell16"
        Me.XrTableCell16.Text = "Account Name"
        Me.XrTableCell16.Weight = 0.35612769455621651
        '
        'XrTableCell18
        '
        Me.XrTableCell18.CanGrow = False
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.StylePriority.UseTextAlignment = False
        Me.XrTableCell18.Text = "Cost"
        Me.XrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell18.Weight = 0.2198980166187936
        '
        'XrTableCell20
        '
        Me.XrTableCell20.CanGrow = False
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.StylePriority.UseTextAlignment = False
        Me.XrTableCell20.Text = "Ext Cost"
        Me.XrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell20.Weight = 0.2002113486937652
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel25.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.Size = New System.Drawing.Size(123, 21)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = "Material Detail"
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(0, 44)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(996, 6)
        '
        'XrLine2
        '
        Me.XrLine2.Location = New System.Drawing.Point(0, 73)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(996, 6)
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
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1"), New System.Data.Common.DataColumnMapping("pt_desc2", "pt_desc2"), New System.Data.Common.DataColumnMapping("code_code", "code_code"), New System.Data.Common.DataColumnMapping("cogscr_oid", "cogscr_oid"), New System.Data.Common.DataColumnMapping("cogscr_cogsc_oid", "cogscr_cogsc_oid"), New System.Data.Common.DataColumnMapping("cogscr_seq", "cogscr_seq"), New System.Data.Common.DataColumnMapping("cogscr_pt_id", "cogscr_pt_id"), New System.Data.Common.DataColumnMapping("cogscr_qty", "cogscr_qty"), New System.Data.Common.DataColumnMapping("cogscr_cost", "cogscr_cost"), New System.Data.Common.DataColumnMapping("um_desc", "um_desc"), New System.Data.Common.DataColumnMapping("pl_desc", "pl_desc"), New System.Data.Common.DataColumnMapping("cogscr_cost_ext", "cogscr_cost_ext"), New System.Data.Common.DataColumnMapping("ac_code", "ac_code"), New System.Data.Common.DataColumnMapping("ac_name", "ac_name")})})
        '
        'XRMaterialSub
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageFooter, Me.ReportHeader})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        Me.DrawGrid = False
        Me.GridSize = New System.Drawing.Size(2, 2)
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.Version = "9.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    'Friend WithEvents DsCogsRaw1 As syspro_ver2.DsCogsRaw
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
End Class
