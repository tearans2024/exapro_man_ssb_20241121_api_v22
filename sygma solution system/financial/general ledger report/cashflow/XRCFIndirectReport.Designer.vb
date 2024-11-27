<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRCFIndirectReport
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.xpg_data = New DevExpress.XtraReports.UI.XRPivotGrid
        'Me.Ds_cashflow_indirect1 = New syspro_ver2.ds_cashflow_indirect
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField6 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        'Me.Cfid_reportTableAdapter = New syspro_ver2.ds_cashflow_indirectTableAdapters.cfid_reportTableAdapter
        'CType(Me.Ds_cashflow_indirect1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xpg_data})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 333
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'xpg_data
        '
        Me.xpg_data.DataMember = "cfid_report"
        'Me.xpg_data.DataSource = Me.Ds_cashflow_indirect1
        Me.xpg_data.Dpi = 254.0!
        Me.xpg_data.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.XrPivotGridField3, Me.XrPivotGridField2, Me.XrPivotGridField4, Me.XrPivotGridField6, Me.XrPivotGridField5})
        Me.xpg_data.Location = New System.Drawing.Point(21, 0)
        Me.xpg_data.Name = "xpg_data"
        Me.xpg_data.OptionsView.ShowColumnHeaders = False
        Me.xpg_data.OptionsView.ShowDataHeaders = False
        Me.xpg_data.OptionsView.ShowRowGrandTotals = False
        Me.xpg_data.Size = New System.Drawing.Size(2434, 333)
        '
        'Ds_cashflow_indirect1
        '
        'Me.Ds_cashflow_indirect1.DataSetName = "ds_cashflow_indirect"
        'Me.Ds_cashflow_indirect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField3.AreaIndex = 0
        Me.XrPivotGridField3.Caption = "Header"
        Me.XrPivotGridField3.FieldName = "cfidrule_header"
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField2.AreaIndex = 1
        Me.XrPivotGridField2.Caption = "Sub Header"
        Me.XrPivotGridField2.FieldName = "cfidrule_subheader"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        Me.XrPivotGridField2.Options.ShowGrandTotal = False
        Me.XrPivotGridField2.Options.ShowTotals = False
        Me.XrPivotGridField2.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.XrPivotGridField4.AreaIndex = 0
        Me.XrPivotGridField4.Caption = "Month"
        Me.XrPivotGridField4.FieldName = "_month"
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        '
        'XrPivotGridField6
        '
        Me.XrPivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField6.AreaIndex = 2
        Me.XrPivotGridField6.Caption = "Cashflow Account"
        Me.XrPivotGridField6.FieldName = "ac_name"
        Me.XrPivotGridField6.Name = "XrPivotGridField6"
        '
        'XrPivotGridField5
        '
        Me.XrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField5.AreaIndex = 0
        Me.XrPivotGridField5.Caption = "Amount"
        Me.XrPivotGridField5.FieldName = "cfid_amount"
        Me.XrPivotGridField5.Name = "XrPivotGridField5"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel20, Me.XrLabel13})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 156
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel20
        '
        Me.XrLabel20.Dpi = 254.0!
        Me.XrLabel20.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel20.Location = New System.Drawing.Point(614, 64)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(1250, 53)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "CASHFLOW REPORT"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel13
        '
        Me.XrLabel13.Dpi = 254.0!
        Me.XrLabel13.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel13.Location = New System.Drawing.Point(614, 0)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel13.Size = New System.Drawing.Size(1250, 53)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "PT. SYGMA EXAMEDIA ARKANLEEMA"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 0
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'Cfid_reportTableAdapter
        '
        'Me.Cfid_reportTableAdapter.ClearBeforeFill = True
        '
        'XRCFIndirectReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter})
        'Me.DataAdapter = Me.Cfid_reportTableAdapter
        Me.DataMember = "cfid_report"
        'Me.DataSource = Me.Ds_cashflow_indirect1
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(254, 254, 254, 254)
        Me.PageHeight = 2101
        Me.PageWidth = 2969
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        'CType(Me.Ds_cashflow_indirect1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    'Friend WithEvents Ds_cashflow_indirect1 As syspro_ver2.ds_cashflow_indirect
    'Friend WithEvents Cfid_reportTableAdapter As syspro_ver2.ds_cashflow_indirectTableAdapters.cfid_reportTableAdapter
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Public WithEvents xpg_data As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents XrPivotGridField6 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
End Class
