<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRLapBySalesVSConsumen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRLapBySalesVSConsumen))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField7 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField6 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLblPeriode = New DevExpress.XtraReports.UI.XRLabel
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.xrpb_logo = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.DS_salesVSconsumen1 = New sygma_solution_system.DS_salesVSconsumen
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine
        CType(Me.DS_salesVSconsumen1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.XrPivotGridField2, Me.XrPivotGridField3, Me.XrPivotGridField7, Me.XrPivotGridField4, Me.XrPivotGridField1, Me.XrPivotGridField5, Me.XrPivotGridField6})
        Me.XrPivotGrid1.Location = New System.Drawing.Point(0, 0)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.Size = New System.Drawing.Size(725, 83)
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField2.AreaIndex = 0
        Me.XrPivotGridField2.Caption = "Entity"
        Me.XrPivotGridField2.FieldName = "en_desc"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        Me.XrPivotGridField2.UnboundFieldName = "XrPivotGridField2"
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField3.AreaIndex = 1
        Me.XrPivotGridField3.Caption = "Sales"
        Me.XrPivotGridField3.FieldName = "ptnr_name"
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        Me.XrPivotGridField3.UnboundFieldName = "XrPivotGridField3"
        Me.XrPivotGridField3.Width = 110
        '
        'XrPivotGridField7
        '
        Me.XrPivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField7.AreaIndex = 2
        Me.XrPivotGridField7.Caption = "Costumer"
        Me.XrPivotGridField7.FieldName = "consumen"
        Me.XrPivotGridField7.Name = "XrPivotGridField7"
        Me.XrPivotGridField7.Width = 135
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField4.AreaIndex = 0
        Me.XrPivotGridField4.Caption = "Qty"
        Me.XrPivotGridField4.CellFormat.FormatString = "n"
        Me.XrPivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.FieldName = "soshipd_qty"
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        Me.XrPivotGridField4.Width = 75
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField1.AreaIndex = 1
        Me.XrPivotGridField1.Caption = "Brutto"
        Me.XrPivotGridField1.CellFormat.FormatString = "n"
        Me.XrPivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField1.FieldName = "sod_price_ori_aft_disc_aft_tax_ext"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        '
        'XrPivotGridField5
        '
        Me.XrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField5.AreaIndex = 2
        Me.XrPivotGridField5.Caption = "Netto"
        Me.XrPivotGridField5.CellFormat.FormatString = "n"
        Me.XrPivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField5.FieldName = "sod_price_ori_aft_disc_aft_tax"
        Me.XrPivotGridField5.Name = "XrPivotGridField5"
        '
        'XrPivotGridField6
        '
        Me.XrPivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField6.AreaIndex = 3
        Me.XrPivotGridField6.Caption = "Diskon"
        Me.XrPivotGridField6.CellFormat.FormatString = "n2"
        Me.XrPivotGridField6.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField6.FieldName = "sod_disc"
        Me.XrPivotGridField6.Name = "XrPivotGridField6"
        Me.XrPivotGridField6.Width = 95
        '
        'PageHeader
        '
        Me.PageHeader.Height = 0
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(167, 42)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(558, 25)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "Sales Reports By Sales Person & Customer"
        '
        'XrLblPeriode
        '
        Me.XrLblPeriode.Location = New System.Drawing.Point(167, 67)
        Me.XrLblPeriode.Name = "XrLblPeriode"
        Me.XrLblPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLblPeriode.Size = New System.Drawing.Size(558, 25)
        Me.XrLblPeriode.Text = " "
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2, Me.XrPageInfo1})
        Me.PageFooter.Height = 25
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Format = "{0:dddd, dd MMMM yyyy HH:mm:ss}"
        Me.XrPageInfo2.Location = New System.Drawing.Point(100, 0)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.Size = New System.Drawing.Size(275, 25)
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Location = New System.Drawing.Point(0, 0)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.Size = New System.Drawing.Size(100, 25)
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=Syspro_lokal;uid=postgres"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("soshipd_qty", "soshipd_qty"), New System.Data.Common.DataColumnMapping("soship_date", "soship_date"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("sod_price_ori_aft_disc_aft_tax", "sod_price_ori_aft_disc_aft_tax"), New System.Data.Common.DataColumnMapping("sod_price_ori_aft_disc_aft_tax_ext", "sod_price_ori_aft_disc_aft_tax_ext"), New System.Data.Common.DataColumnMapping("sod_disc", "sod_disc"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("consumen", "consumen")})})
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLblPeriode, Me.xrpb_logo, Me.XrLabel2})
        Me.ReportHeader.Height = 92
        Me.ReportHeader.Name = "ReportHeader"
        '
        'xrpb_logo
        '
        Me.xrpb_logo.Image = CType(resources.GetObject("xrpb_logo.Image"), System.Drawing.Image)
        Me.xrpb_logo.Location = New System.Drawing.Point(0, 0)
        Me.xrpb_logo.Name = "xrpb_logo"
        Me.xrpb_logo.Size = New System.Drawing.Size(167, 92)
        Me.xrpb_logo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.Location = New System.Drawing.Point(167, 0)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(258, 25)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "PT. Sygma Examedia Arkanleema"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPivotGrid1})
        Me.GroupHeader1.Height = 83
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'DS_salesVSconsumen1
        '
        Me.DS_salesVSconsumen1.DataSetName = "DS_salesVSconsumen"
        Me.DS_salesVSconsumen1.EnforceConstraints = False
        Me.DS_salesVSconsumen1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrLine1, Me.XrLine2, Me.XrLine3})
        Me.GroupFooter1.Height = 92
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLabel3
        '
        Me.XrLabel3.Location = New System.Drawing.Point(0, 67)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(25, 25)
        Me.XrLabel3.Text = "By"
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(42, 75)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(184, 8)
        '
        'XrLine2
        '
        Me.XrLine2.Location = New System.Drawing.Point(283, 75)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(184, 8)
        '
        'XrLine3
        '
        Me.XrLine3.Location = New System.Drawing.Point(533, 75)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.Size = New System.Drawing.Size(184, 8)
        '
        'XRLapBySalesVSConsumen
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader, Me.GroupHeader1, Me.GroupFooter1})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        Me.DataSource = Me.DS_salesVSconsumen1
        Me.Margins = New System.Drawing.Printing.Margins(40, 40, 100, 100)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Version = "9.2"
        CType(Me.DS_salesVSconsumen1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Public WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrLblPeriode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField6 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField7 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents DS_salesVSconsumen1 As sygma_solution_system.DS_salesVSconsumen
    Friend WithEvents xrpb_logo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
End Class
