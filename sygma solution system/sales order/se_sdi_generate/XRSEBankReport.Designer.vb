<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSEBankReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRSEBankReport))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell24 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell13 = New DevExpress.XtraReports.UI.XRTableCell
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell23 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabelPeriode = New DevExpress.XtraReports.UI.XRLabel
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.LblTglBayar = New DevExpress.XtraReports.UI.XRLabel
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.DsseBank1 = New sygma_solution_system.DSSEBank
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsseBank1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 64
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("segend_entity", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending), New DevExpress.XtraReports.UI.GroupField("segend_nama", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable2
        '
        Me.XrTable2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTable2.Dpi = 254.0!
        Me.XrTable2.Location = New System.Drawing.Point(0, 0)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(2752, 64)
        Me.XrTable2.StylePriority.UseBorders = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell24, Me.XrTableCell7, Me.XrTableCell22, Me.XrTableCell5, Me.XrTableCell20, Me.XrTableCell10, Me.XrTableCell17, Me.XrTableCell6, Me.XrTableCell13})
        Me.XrTableRow2.Dpi = 254.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segend_oid", "{0:n0}")})
        Me.XrTableCell4.Dpi = 254.0!
        Me.XrTableCell4.Name = "XrTableCell4"
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrTableCell4.Summary = XrSummary1
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.Weight = 0.096125461254612549
        '
        'XrTableCell24
        '
        Me.XrTableCell24.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segend_nama", "")})
        Me.XrTableCell24.Dpi = 254.0!
        Me.XrTableCell24.Name = "XrTableCell24"
        Me.XrTableCell24.Text = "XrTableCell24"
        Me.XrTableCell24.Weight = 0.2959530271174805
        '
        'XrTableCell7
        '
        Me.XrTableCell7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.bank", "")})
        Me.XrTableCell7.Dpi = 254.0!
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Text = "XrTableCell7"
        Me.XrTableCell7.Weight = 0.41678779069767441
        '
        'XrTableCell22
        '
        Me.XrTableCell22.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.norek", "")})
        Me.XrTableCell22.Dpi = 254.0!
        Me.XrTableCell22.Name = "XrTableCell22"
        Me.XrTableCell22.Text = "XrTableCell22"
        Me.XrTableCell22.Weight = 0.34702034883720934
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segend_komisi", "{0:n0}")})
        Me.XrTableCell5.Dpi = 254.0!
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.Text = "XrTableCell5"
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell5.Weight = 0.20748546511627908
        '
        'XrTableCell20
        '
        Me.XrTableCell20.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segend_add_income", "{0:n0}")})
        Me.XrTableCell20.Dpi = 254.0!
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.StylePriority.UseTextAlignment = False
        Me.XrTableCell20.Text = "XrTableCell20"
        Me.XrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell20.Weight = 0.32303779069767447
        '
        'XrTableCell10
        '
        Me.XrTableCell10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segend_coaching_income", "{0:n0}")})
        Me.XrTableCell10.Dpi = 254.0!
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.StylePriority.UseTextAlignment = False
        Me.XrTableCell10.Text = "XrTableCell10"
        Me.XrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell10.Weight = 0.25436046511627897
        '
        'XrTableCell17
        '
        Me.XrTableCell17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segend_total_income", "{0:n0}")})
        Me.XrTableCell17.Dpi = 254.0!
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.StylePriority.UseTextAlignment = False
        Me.XrTableCell17.Text = "XrTableCell17"
        Me.XrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell17.Weight = 0.27725290697674421
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segend_entity", "")})
        Me.XrTableCell6.Dpi = 254.0!
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Text = "XrTableCell6"
        Me.XrTableCell6.Weight = 0.34702034883720922
        '
        'XrTableCell13
        '
        Me.XrTableCell13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.segen_remarks", "")})
        Me.XrTableCell13.Dpi = 254.0!
        Me.XrTableCell13.Name = "XrTableCell13"
        Me.XrTableCell13.Text = "XrTableCell13"
        Me.XrTableCell13.Weight = 0.43495639534883712
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 95
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrTable1.Dpi = 254.0!
        Me.XrTable1.Location = New System.Drawing.Point(0, 0)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(2752, 95)
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell23, Me.XrTableCell8, Me.XrTableCell21, Me.XrTableCell2, Me.XrTableCell19, Me.XrTableCell9, Me.XrTableCell18, Me.XrTableCell3, Me.XrTableCell14})
        Me.XrTableRow1.Dpi = 254.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.CanGrow = False
        Me.XrTableCell1.Dpi = 254.0!
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "NO"
        Me.XrTableCell1.Weight = 0.0960686600221484
        '
        'XrTableCell23
        '
        Me.XrTableCell23.CanGrow = False
        Me.XrTableCell23.Dpi = 254.0!
        Me.XrTableCell23.Name = "XrTableCell23"
        Me.XrTableCell23.Text = "NAMA"
        Me.XrTableCell23.Weight = 0.2960098283499446
        '
        'XrTableCell8
        '
        Me.XrTableCell8.CanGrow = False
        Me.XrTableCell8.Dpi = 254.0!
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.Text = "BANK"
        Me.XrTableCell8.Weight = 0.41569767441860461
        '
        'XrTableCell21
        '
        Me.XrTableCell21.CanGrow = False
        Me.XrTableCell21.Dpi = 254.0!
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.Text = "NO REK"
        Me.XrTableCell21.Weight = 0.34702034883720928
        '
        'XrTableCell2
        '
        Me.XrTableCell2.CanGrow = False
        Me.XrTableCell2.Dpi = 254.0!
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = "KOMISI"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell2.Weight = 0.20748546511627908
        '
        'XrTableCell19
        '
        Me.XrTableCell19.CanGrow = False
        Me.XrTableCell19.Dpi = 254.0!
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.StylePriority.UseTextAlignment = False
        Me.XrTableCell19.Text = "PERFORMANCE INCOME"
        Me.XrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell19.Weight = 0.32303779069767447
        '
        'XrTableCell9
        '
        Me.XrTableCell9.CanGrow = False
        Me.XrTableCell9.Dpi = 254.0!
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.StylePriority.UseTextAlignment = False
        Me.XrTableCell9.Text = "COACHING INCOME"
        Me.XrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell9.Weight = 0.25436046511627897
        '
        'XrTableCell18
        '
        Me.XrTableCell18.CanGrow = False
        Me.XrTableCell18.Dpi = 254.0!
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.StylePriority.UseTextAlignment = False
        Me.XrTableCell18.Text = "TOTAL"
        Me.XrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell18.Weight = 0.27725290697674421
        '
        'XrTableCell3
        '
        Me.XrTableCell3.CanGrow = False
        Me.XrTableCell3.Dpi = 254.0!
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Text = "CABANG"
        Me.XrTableCell3.Weight = 0.34702034883720922
        '
        'XrTableCell14
        '
        Me.XrTableCell14.CanGrow = False
        Me.XrTableCell14.Dpi = 254.0!
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.Text = "KETERANGAN"
        Me.XrTableCell14.Weight = 0.43604651162790686
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.Location = New System.Drawing.Point(466, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(1778, 64)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "PEMBAYARAN KOMISI SALES EXECUTIVE SYGMA DAYA INSANI"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabelPeriode
        '
        Me.XrLabelPeriode.Dpi = 254.0!
        Me.XrLabelPeriode.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabelPeriode.Location = New System.Drawing.Point(466, 64)
        Me.XrLabelPeriode.Name = "XrLabelPeriode"
        Me.XrLabelPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabelPeriode.Size = New System.Drawing.Size(1778, 64)
        Me.XrLabelPeriode.StylePriority.UseFont = False
        Me.XrLabelPeriode.StylePriority.UseTextAlignment = False
        Me.XrLabelPeriode.Text = "PERIODE"
        Me.XrLabelPeriode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 77
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabelPeriode, Me.LblTglBayar})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 228
        Me.ReportHeader.Name = "ReportHeader"
        '
        'LblTglBayar
        '
        Me.LblTglBayar.Dpi = 254.0!
        Me.LblTglBayar.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LblTglBayar.Location = New System.Drawing.Point(466, 130)
        Me.LblTglBayar.Name = "LblTglBayar"
        Me.LblTglBayar.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LblTglBayar.Size = New System.Drawing.Size(1778, 64)
        Me.LblTglBayar.StylePriority.UseFont = False
        Me.LblTglBayar.StylePriority.UseTextAlignment = False
        Me.LblTglBayar.Text = "PERIODE"
        Me.LblTglBayar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=sdi"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("segend_oid", "segend_oid"), New System.Data.Common.DataColumnMapping("segend_segen_oid", "segend_segen_oid"), New System.Data.Common.DataColumnMapping("segend_ptnr_id", "segend_ptnr_id"), New System.Data.Common.DataColumnMapping("segend_level", "segend_level"), New System.Data.Common.DataColumnMapping("segend_nama", "segend_nama"), New System.Data.Common.DataColumnMapping("segend_active_sales", "segend_active_sales"), New System.Data.Common.DataColumnMapping("segend_su_sales", "segend_su_sales"), New System.Data.Common.DataColumnMapping("segend_pengali_komisi", "segend_pengali_komisi"), New System.Data.Common.DataColumnMapping("segend_komisi", "segend_komisi"), New System.Data.Common.DataColumnMapping("segend_su_group", "segend_su_group"), New System.Data.Common.DataColumnMapping("segend_percent_add_income", "segend_percent_add_income"), New System.Data.Common.DataColumnMapping("segend_pengali_add_income", "segend_pengali_add_income"), New System.Data.Common.DataColumnMapping("segend_add_income", "segend_add_income"), New System.Data.Common.DataColumnMapping("segend_percent_coaching_income", "segend_percent_coaching_income"), New System.Data.Common.DataColumnMapping("segend_pengali_coaching_income", "segend_pengali_coaching_income"), New System.Data.Common.DataColumnMapping("segend_coaching_income", "segend_coaching_income"), New System.Data.Common.DataColumnMapping("segend_total_income", "segend_total_income"), New System.Data.Common.DataColumnMapping("segend_payment_date", "segend_payment_date"), New System.Data.Common.DataColumnMapping("segend_entity", "segend_entity"), New System.Data.Common.DataColumnMapping("segend_lvl_id", "segend_lvl_id"), New System.Data.Common.DataColumnMapping("segend_parent", "segend_parent"), New System.Data.Common.DataColumnMapping("segend_komisi_telah_dibayar", "segend_komisi_telah_dibayar"), New System.Data.Common.DataColumnMapping("segend_komisi_bulanan", "segend_komisi_bulanan")})})
        '
        'DsseBank1
        '
        Me.DsseBank1.DataSetName = "DSSEBank"
        Me.DsseBank1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'XRSEBankReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        Me.DataSource = Me.DsseBank1
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.PageHeight = 2101
        Me.PageWidth = 2969
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsseBank1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrLabelPeriode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents DsseBank1 As sygma_solution_system.DSSEBank
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell24 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell23 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Public WithEvents LblTglBayar As DevExpress.XtraReports.UI.XRLabel
End Class
