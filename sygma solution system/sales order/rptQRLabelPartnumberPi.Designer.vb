<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptQRLabelPartnumberPi
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
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.DataTable1TableAdapter = New sygma_solution_system.pi_qrTableAdapters.DataTable1TableAdapter
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.Ds_qr_pi1 = New sygma_solution_system.ds_qr_pi
        CType(Me.Ds_qr_pi1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPictureBox1, Me.XrLabel5, Me.XrLabel1, Me.XrLabel3, Me.XrLabel2})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 381
        Me.Detail.KeepTogether = True
        Me.Detail.MultiColumn.ColumnCount = 3
        Me.Detail.MultiColumn.ColumnSpacing = 2.0!
        Me.Detail.MultiColumn.Direction = DevExpress.XtraReports.UI.ColumnDirection.AcrossThenDown
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Dpi = 254.0!
        Me.XrPictureBox1.Location = New System.Drawing.Point(21, 21)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.Size = New System.Drawing.Size(254, 212)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel5
        '
        Me.XrLabel5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.pt_code", "")})
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Arial Narrow", 8.0!)
        Me.XrLabel5.Location = New System.Drawing.Point(21, 254)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(318, 42)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "XrLabel5"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.pt_desc1", "")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Arial Narrow", 8.0!)
        Me.XrLabel1.Location = New System.Drawing.Point(296, 42)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(423, 64)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.invqr_price1", "")})
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Arial Narrow", 8.0!)
        Me.XrLabel3.Location = New System.Drawing.Point(296, 106)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(360, 63)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "XrLabel3"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.invqr_price2", "")})
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Arial Narrow", 8.0!)
        Me.XrLabel2.Location = New System.Drawing.Point(296, 190)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(360, 42)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 474
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 0
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'DataTable1TableAdapter
        '
        Me.DataTable1TableAdapter.ClearBeforeFill = True
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Dpi = 254.0!
        Me.GroupHeader1.Height = 429
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'Ds_qr_pi1
        '
        Me.Ds_qr_pi1.DataSetName = "ds_qr_pi"
        Me.Ds_qr_pi1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'rptQRLabelPartnumberPi
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.GroupHeader1})
        Me.DataAdapter = Me.DataTable1TableAdapter
        Me.DataMember = "DataTable1"
        Me.DataSource = Me.Ds_qr_pi1
        Me.Dpi = 254.0!
        Me.DrawGrid = False
        Me.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.Ds_qr_pi1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    'Friend WithEvents DsTagPrice1 As EasyAccounting.DsTagPrice
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents DataTable1TableAdapter As sygma_solution_system.pi_qrTableAdapters.DataTable1TableAdapter
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Ds_qr_pi1 As sygma_solution_system.ds_qr_pi
End Class
