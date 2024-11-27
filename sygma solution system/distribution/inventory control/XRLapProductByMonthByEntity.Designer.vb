<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRLapProductByMonthByEntity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRLapProductByMonthByEntity))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
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
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid
        Me.DS_entity_pt1 = New sygma_solution_system.DS_entity_pt
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField8 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField6 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField7 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.xrpb_logo = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine
        CType(Me.DS_entity_pt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
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
        Me.XrLabel1.Size = New System.Drawing.Size(667, 25)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "Sales Reports By Entity & Product"
        '
        'XrLblPeriode
        '
        Me.XrLblPeriode.Location = New System.Drawing.Point(167, 67)
        Me.XrLblPeriode.Name = "XrLblPeriode"
        Me.XrLblPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLblPeriode.Size = New System.Drawing.Size(667, 25)
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
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("soshipd_qty", "soshipd_qty"), New System.Data.Common.DataColumnMapping("soship_date", "soship_date"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("sod_price_ori_aft_disc_aft_tax", "sod_price_ori_aft_disc_aft_tax"), New System.Data.Common.DataColumnMapping("sod_price_ori_aft_disc_aft_tax_ext", "sod_price_ori_aft_disc_aft_tax_ext"), New System.Data.Common.DataColumnMapping("sod_disc", "sod_disc"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("ptnr_name1", "ptnr_name1"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1")})})
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLblPeriode, Me.xrpb_logo, Me.XrLabel2})
        Me.ReportHeader.Height = 92
        Me.ReportHeader.Name = "ReportHeader"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPivotGrid1})
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.DataMember = "Table"
        Me.XrPivotGrid1.DataSource = Me.DS_entity_pt1
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.XrPivotGridField2, Me.XrPivotGridField1, Me.XrPivotGridField8, Me.XrPivotGridField3, Me.XrPivotGridField4, Me.XrPivotGridField5, Me.XrPivotGridField6, Me.XrPivotGridField7})
        Me.XrPivotGrid1.Location = New System.Drawing.Point(0, 0)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.Size = New System.Drawing.Size(1083, 100)
        '
        'DS_entity_pt1
        '
        Me.DS_entity_pt1.DataSetName = "DS_entity_pt"
        Me.DS_entity_pt1.EnforceConstraints = False
        Me.DS_entity_pt1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField2.AreaIndex = 0
        Me.XrPivotGridField2.Caption = "Entity"
        Me.XrPivotGridField2.FieldName = "en_desc"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        Me.XrPivotGridField2.UnboundFieldName = "XrPivotGridField2"
        Me.XrPivotGridField2.Width = 110
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField1.AreaIndex = 1
        Me.XrPivotGridField1.Caption = "Part Number"
        Me.XrPivotGridField1.FieldName = "pt_code"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        '
        'XrPivotGridField8
        '
        Me.XrPivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField8.AreaIndex = 2
        Me.XrPivotGridField8.Caption = "Product"
        Me.XrPivotGridField8.FieldName = "pt_desc1"
        Me.XrPivotGridField8.Name = "XrPivotGridField8"
        Me.XrPivotGridField8.Width = 180
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.XrPivotGridField3.AreaIndex = 0
        Me.XrPivotGridField3.Caption = "Month"
        Me.XrPivotGridField3.FieldName = "soship_date"
        Me.XrPivotGridField3.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        Me.XrPivotGridField3.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
        Me.XrPivotGridField3.UnboundFieldName = "XrPivotGridField3"
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField4.AreaIndex = 0
        Me.XrPivotGridField4.Caption = "Qty"
        Me.XrPivotGridField4.CellFormat.FormatString = "n"
        Me.XrPivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.FieldName = "soshipd_qty"
        Me.XrPivotGridField4.GrandTotalCellFormat.FormatString = "n"
        Me.XrPivotGridField4.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        Me.XrPivotGridField4.Width = 60
        '
        'XrPivotGridField5
        '
        Me.XrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField5.AreaIndex = 1
        Me.XrPivotGridField5.Caption = "Brutto"
        Me.XrPivotGridField5.CellFormat.FormatString = "n"
        Me.XrPivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField5.FieldName = "sod_price_ori_aft_disc_aft_tax"
        Me.XrPivotGridField5.GrandTotalCellFormat.FormatString = "n"
        Me.XrPivotGridField5.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField5.Name = "XrPivotGridField5"
        '
        'XrPivotGridField6
        '
        Me.XrPivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField6.AreaIndex = 2
        Me.XrPivotGridField6.Caption = "Netto"
        Me.XrPivotGridField6.CellFormat.FormatString = "n"
        Me.XrPivotGridField6.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField6.FieldName = "sod_price_ori_aft_disc_aft_tax_ext"
        Me.XrPivotGridField6.GrandTotalCellFormat.FormatString = "n"
        Me.XrPivotGridField6.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField6.Name = "XrPivotGridField6"
        '
        'XrPivotGridField7
        '
        Me.XrPivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField7.AreaIndex = 3
        Me.XrPivotGridField7.Caption = "Discount"
        Me.XrPivotGridField7.CellFormat.FormatString = "n2"
        Me.XrPivotGridField7.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.FieldName = "sod_disc"
        Me.XrPivotGridField7.GrandTotalCellFormat.FormatString = "n2"
        Me.XrPivotGridField7.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.Name = "XrPivotGridField7"
        Me.XrPivotGridField7.Width = 95
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
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrLine1, Me.XrLine2, Me.XrLine3})
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLabel3
        '
        Me.XrLabel3.Location = New System.Drawing.Point(100, 67)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(25, 25)
        Me.XrLabel3.Text = "By"
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(100, 83)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(184, 8)
        '
        'XrLine2
        '
        Me.XrLine2.Location = New System.Drawing.Point(342, 83)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(184, 8)
        '
        'XrLine3
        '
        Me.XrLine3.Location = New System.Drawing.Point(583, 83)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.Size = New System.Drawing.Size(184, 8)
        '
        'XRProductByMonthByEntity2
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader, Me.GroupHeader1, Me.GroupFooter1})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        Me.DataSource = Me.DS_entity_pt1
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(44, 45, 100, 100)
        Me.PageHeight = 827
        Me.PageWidth = 1169
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Version = "9.2"
        CType(Me.DS_entity_pt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrLblPeriode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Public WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField6 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField7 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents DS_entity_pt1 As sygma_solution_system.DS_entity_pt
    Friend WithEvents XrPivotGridField8 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents xrpb_logo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
End Class
