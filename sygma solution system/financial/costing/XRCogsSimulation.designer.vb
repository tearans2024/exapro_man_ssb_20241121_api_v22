<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRCogsSimulation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRCogsSimulation))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrCogsRouteSub1 = New syspro_ver2.XRCogsRouteSub
        Me.XrSubreport2 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrMaterialSub1 = New syspro_ver2.XRMaterialSub
        Me.XrSubreport3 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrAccountSub1 = New syspro_ver2.XRAccountSub
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrCogsRouteSub1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrMaterialSub1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrAccountSub1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.Detail.Height = 25
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable2
        '
        Me.XrTable2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTable2.Location = New System.Drawing.Point(0, 0)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(646, 25)
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell11, Me.XrTableCell5, Me.XrTableCell10, Me.XrTableCell6})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pt_code", "")})
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.Weight = 0.5
        '
        'XrTableCell11
        '
        Me.XrTableCell11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pt_desc1", "")})
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.Text = "XrTableCell11"
        Me.XrTableCell11.Weight = 0.8297213622291022
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pt_desc2", "")})
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Text = "XrTableCell5"
        Me.XrTableCell5.Weight = 0.87151702786377727
        '
        'XrTableCell10
        '
        Me.XrTableCell10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsci_qty", "{0:n}")})
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.StylePriority.UseTextAlignment = False
        Me.XrTableCell10.Text = "XrTableCell10"
        Me.XrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell10.Weight = 0.34674922600619207
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.unit_measure", "")})
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        Me.XrTableCell6.Text = "XrTableCell6"
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell6.Weight = 0.45201238390092879
        '
        'PageHeader
        '
        Me.PageHeader.Height = 0
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Height = 79
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
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
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("cogsc_code", "cogsc_code"), New System.Data.Common.DataColumnMapping("cogsc_date", "cogsc_date"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("cogsc_remarks", "cogsc_remarks"), New System.Data.Common.DataColumnMapping("cs_name", "cs_name"), New System.Data.Common.DataColumnMapping("cogsc_mtl_total", "cogsc_mtl_total"), New System.Data.Common.DataColumnMapping("cogsc_srv_total", "cogsc_srv_total"), New System.Data.Common.DataColumnMapping("cogscr_total", "cogscr_total"), New System.Data.Common.DataColumnMapping("cogsci_seq", "cogsci_seq"), New System.Data.Common.DataColumnMapping("cogsci_pt_id", "cogsci_pt_id"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1"), New System.Data.Common.DataColumnMapping("pt_desc2", "pt_desc2"), New System.Data.Common.DataColumnMapping("unit_measure", "unit_measure"), New System.Data.Common.DataColumnMapping("cogsci_qty", "cogsci_qty")})})
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel2, Me.XrLabel3, Me.XrLabel4, Me.XrLabel5, Me.XrLabel6, Me.XrTable1, Me.XrLabel7, Me.XrLabel8, Me.XrLabel9, Me.XrLabel10, Me.XrLabel11, Me.XrLabel12, Me.XrLabel13, Me.XrLabel14, Me.XrLine1, Me.XrLine2, Me.XrLabel15, Me.XrLabel16, Me.XrLabel17, Me.XrLabel18, Me.XrLabel19, Me.XrLabel20, Me.XrLabel21, Me.XrLabel22, Me.XrLabel23})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("cogsc_code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.Height = 192
        Me.GroupHeader1.Name = "GroupHeader1"
        Me.GroupHeader1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsc_code", "")})
        Me.XrLabel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel1.Location = New System.Drawing.Point(100, 27)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(138, 21)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "XrLabel1"
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsc_date", "{0:dd/MM/yyyy}")})
        Me.XrLabel2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel2.Location = New System.Drawing.Point(100, 52)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(138, 21)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "XrLabel2"
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsc_remarks", "")})
        Me.XrLabel3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel3.Location = New System.Drawing.Point(100, 102)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(250, 21)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "XrLabel3"
        '
        'XrLabel4
        '
        Me.XrLabel4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsc_mtl_total", "{0:n}")})
        Me.XrLabel4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel4.Location = New System.Drawing.Point(542, 27)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(100, 21)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "XrLabel4"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel5
        '
        Me.XrLabel5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsc_srv_total", "{0:n}")})
        Me.XrLabel5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel5.Location = New System.Drawing.Point(542, 52)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(100, 21)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "XrLabel5"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogscr_total", "{0:n}")})
        Me.XrLabel6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel6.Location = New System.Drawing.Point(542, 77)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(100, 21)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrTable1
        '
        Me.XrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrTable1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrTable1.Location = New System.Drawing.Point(0, 167)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(646, 19)
        Me.XrTable1.StylePriority.UseFont = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell12, Me.XrTableCell2, Me.XrTableCell9, Me.XrTableCell3})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.CanGrow = False
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "Part Number"
        Me.XrTableCell1.Weight = 0.5
        '
        'XrTableCell12
        '
        Me.XrTableCell12.CanGrow = False
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.Text = "Description1"
        Me.XrTableCell12.Weight = 0.8297213622291022
        '
        'XrTableCell2
        '
        Me.XrTableCell2.CanGrow = False
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Text = "Description2"
        Me.XrTableCell2.Weight = 0.87151702786377727
        '
        'XrTableCell9
        '
        Me.XrTableCell9.CanGrow = False
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.StylePriority.UseTextAlignment = False
        Me.XrTableCell9.Text = "QTY"
        Me.XrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell9.Weight = 0.34674922600619207
        '
        'XrTableCell3
        '
        Me.XrTableCell3.CanGrow = False
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "Unit Measure"
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell3.Weight = 0.45201238390092879
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cs_name", "")})
        Me.XrLabel7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel7.Location = New System.Drawing.Point(100, 77)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(138, 21)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.Text = "XrLabel7"
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel8.Location = New System.Drawing.Point(0, 27)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(88, 21)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.Text = "Cogs Code"
        '
        'XrLabel9
        '
        Me.XrLabel9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel9.Location = New System.Drawing.Point(0, 52)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.Size = New System.Drawing.Size(88, 21)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "Date"
        '
        'XrLabel10
        '
        Me.XrLabel10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel10.Location = New System.Drawing.Point(0, 77)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.Size = New System.Drawing.Size(88, 21)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "Cost Set"
        '
        'XrLabel11
        '
        Me.XrLabel11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel11.Location = New System.Drawing.Point(0, 102)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.Size = New System.Drawing.Size(88, 21)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.Text = "Remarks"
        '
        'XrLabel12
        '
        Me.XrLabel12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel12.Location = New System.Drawing.Point(442, 77)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(88, 21)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.Text = "Total"
        '
        'XrLabel13
        '
        Me.XrLabel13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel13.Location = New System.Drawing.Point(442, 52)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.Size = New System.Drawing.Size(88, 21)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.Text = "Service Total"
        '
        'XrLabel14
        '
        Me.XrLabel14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel14.Location = New System.Drawing.Point(442, 27)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.Size = New System.Drawing.Size(88, 21)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.Text = "Material Total"
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(0, 188)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(646, 4)
        '
        'XrLine2
        '
        Me.XrLine2.Location = New System.Drawing.Point(0, 158)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(646, 4)
        '
        'XrLabel15
        '
        Me.XrLabel15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel15.Location = New System.Drawing.Point(88, 31)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.Text = ":"
        '
        'XrLabel16
        '
        Me.XrLabel16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel16.Location = New System.Drawing.Point(88, 56)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.Text = ":"
        '
        'XrLabel17
        '
        Me.XrLabel17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel17.Location = New System.Drawing.Point(88, 81)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.Text = ":"
        '
        'XrLabel18
        '
        Me.XrLabel18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel18.Location = New System.Drawing.Point(88, 106)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.Text = ":"
        '
        'XrLabel19
        '
        Me.XrLabel19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel19.Location = New System.Drawing.Point(529, 27)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.Text = ":"
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel20.Location = New System.Drawing.Point(529, 52)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.Text = ":"
        '
        'XrLabel21
        '
        Me.XrLabel21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel21.Location = New System.Drawing.Point(529, 77)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.Text = ":"
        '
        'XrLabel22
        '
        Me.XrLabel22.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cogsc_oid", "")})
        Me.XrLabel22.ForeColor = System.Drawing.Color.White
        Me.XrLabel22.Location = New System.Drawing.Point(600, 113)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.Size = New System.Drawing.Size(100, 23)
        Me.XrLabel22.StylePriority.UseForeColor = False
        Me.XrLabel22.Text = "XrLabel22"
        Me.XrLabel22.Visible = False
        '
        'XrLabel23
        '
        Me.XrLabel23.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel23.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.Size = New System.Drawing.Size(312, 25)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "COST OF GOODS SOLD SIMULATION"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport1, Me.XrLine3, Me.XrSubreport2, Me.XrSubreport3})
        Me.GroupFooter1.Height = 29
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLine3
        '
        Me.XrLine3.Location = New System.Drawing.Point(0, 0)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.Size = New System.Drawing.Size(646, 4)
        '
        'XrSubreport1
        '
        Me.XrSubreport1.Location = New System.Drawing.Point(0, 4)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ReportSource = Me.XrCogsRouteSub1
        Me.XrSubreport1.Size = New System.Drawing.Size(996, 6)
        '
        'XrCogsRouteSub1
        '
        Me.XrCogsRouteSub1.DataMember = "Table"
        Me.XrCogsRouteSub1.DrawGrid = False
        Me.XrCogsRouteSub1.GridSize = New System.Drawing.Size(2, 2)
        Me.XrCogsRouteSub1.Landscape = True
        Me.XrCogsRouteSub1.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.XrCogsRouteSub1.Name = "XrCogsRouteSub1"
        Me.XrCogsRouteSub1.PageColor = System.Drawing.Color.White
        Me.XrCogsRouteSub1.PageHeight = 827
        Me.XrCogsRouteSub1.PageWidth = 1169
        Me.XrCogsRouteSub1.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.XrCogsRouteSub1.Version = "9.2"
        '
        'XrSubreport2
        '
        Me.XrSubreport2.Location = New System.Drawing.Point(0, 12)
        Me.XrSubreport2.Name = "XrSubreport2"
        Me.XrSubreport2.ReportSource = Me.XrMaterialSub1
        Me.XrSubreport2.Size = New System.Drawing.Size(996, 4)
        '
        'XrMaterialSub1
        '
        Me.XrMaterialSub1.DataMember = "Table"
        Me.XrMaterialSub1.DrawGrid = False
        Me.XrMaterialSub1.GridSize = New System.Drawing.Size(2, 2)
        Me.XrMaterialSub1.Landscape = True
        Me.XrMaterialSub1.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.XrMaterialSub1.Name = "XrMaterialSub1"
        Me.XrMaterialSub1.PageColor = System.Drawing.Color.White
        Me.XrMaterialSub1.PageHeight = 850
        Me.XrMaterialSub1.PageWidth = 1100
        Me.XrMaterialSub1.Version = "9.2"
        '
        'XrSubreport3
        '
        Me.XrSubreport3.Location = New System.Drawing.Point(0, 19)
        Me.XrSubreport3.Name = "XrSubreport3"
        Me.XrSubreport3.ReportSource = Me.XrAccountSub1
        Me.XrSubreport3.Size = New System.Drawing.Size(996, 6)
        '
        'XrAccountSub1
        '
        Me.XrAccountSub1.DataMember = "Table"
        Me.XrAccountSub1.DrawGrid = False
        Me.XrAccountSub1.GridSize = New System.Drawing.Size(2, 2)
        Me.XrAccountSub1.Landscape = True
        Me.XrAccountSub1.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.XrAccountSub1.Name = "XrAccountSub1"
        Me.XrAccountSub1.PageColor = System.Drawing.Color.White
        Me.XrAccountSub1.PageHeight = 583
        Me.XrAccountSub1.PageWidth = 827
        Me.XrAccountSub1.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.XrAccountSub1.Version = "9.2"
        '
        'XRCogsSimulation
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupHeader1, Me.Detail, Me.PageHeader, Me.PageFooter, Me.GroupFooter1})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        Me.DrawGrid = False
        Me.GridSize = New System.Drawing.Size(2, 2)
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.Version = "9.2"
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrCogsRouteSub1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrMaterialSub1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrAccountSub1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    'Friend WithEvents DsCogsSim1 As syspro_ver2.DSCogsSim
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Private WithEvents XrCogsRouteSub1 As syspro_ver2.XRCogsRouteSub
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrSubreport2 As DevExpress.XtraReports.UI.XRSubreport
    Private WithEvents XrMaterialSub1 As syspro_ver2.XRMaterialSub
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrSubreport3 As DevExpress.XtraReports.UI.XRSubreport
    Private WithEvents XrAccountSub1 As syspro_ver2.XRAccountSub
End Class
