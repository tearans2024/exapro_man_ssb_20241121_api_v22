<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptKwitansiReguler
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptKwitansiReguler))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.xrpb_logo = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XRTerbilang = New DevExpress.XtraReports.UI.XRLabel
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.cf_address = New DevExpress.XtraReports.UI.CalculatedField
        Me.Ds_kw_reg1 = New sygma_solution_system.ds_kw_reg
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ds_kw_reg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel8})
        Me.Detail.Height = 25
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.arpayd_ar_ref", "")})
        Me.XrLabel8.Location = New System.Drawing.Point(158, 0)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(208, 25)
        Me.XrLabel8.Text = "XrLabel8"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.xrpb_logo, Me.XrLabel1, Me.XrLabel3, Me.XrLabel5, Me.XrTable1, Me.XrLabel4, Me.XrLabel6, Me.XRTerbilang})
        Me.PageHeader.Height = 234
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.Location = New System.Drawing.Point(8, 100)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(233, 17)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "Sudah Diterima Dari Bapak/Ibu  : "
        '
        'xrpb_logo
        '
        Me.xrpb_logo.Image = CType(resources.GetObject("xrpb_logo.Image"), System.Drawing.Image)
        Me.xrpb_logo.Location = New System.Drawing.Point(8, 0)
        Me.xrpb_logo.Name = "xrpb_logo"
        Me.xrpb_logo.Size = New System.Drawing.Size(167, 92)
        Me.xrpb_logo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(417, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(167, 25)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "KWITANSI"
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.Location = New System.Drawing.Point(8, 158)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(125, 17)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "Uang Sejumlah  : "
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.Location = New System.Drawing.Point(8, 217)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(150, 17)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = "Untuk Pembayaran :"
        '
        'XrTable1
        '
        Me.XrTable1.Location = New System.Drawing.Point(417, 33)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1, Me.XrTableRow2})
        Me.XrTable1.Size = New System.Drawing.Size(307, 33)
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell3})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "No. Kwitansi"
        Me.XrTableCell1.Weight = 0.865928338762215
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Text = ":"
        Me.XrTableCell2.Weight = 0.13931596091205217
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.arpay_code", "")})
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.Weight = 1.6547557003257327
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell6})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "Date"
        Me.XrTableCell4.Weight = 0.865928338762215
        '
        'XrTableCell5
        '
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Text = ":"
        Me.XrTableCell5.Weight = 0.14198697068403915
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.arpay_eff_date", "")})
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Text = "XrTableCell6"
        Me.XrTableCell6.Weight = 1.6520846905537456
        '
        'XrLabel4
        '
        Me.XrLabel4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ptnr_name", "")})
        Me.XrLabel4.Location = New System.Drawing.Point(250, 100)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(242, 17)
        Me.XrLabel4.Text = "XrLabel4"
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_address", "")})
        Me.XrLabel6.Location = New System.Drawing.Point(250, 117)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(433, 33)
        Me.XrLabel6.Text = "XrLabel6"
        '
        'XRTerbilang
        '
        Me.XRTerbilang.Location = New System.Drawing.Point(150, 158)
        Me.XRTerbilang.Name = "XRTerbilang"
        Me.XRTerbilang.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XRTerbilang.Size = New System.Drawing.Size(533, 42)
        Me.XRTerbilang.Text = "XRTerbilang"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel10, Me.XrPageInfo1, Me.XrLabel12, Me.XrLabel7})
        Me.PageFooter.Height = 125
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel10
        '
        Me.XrLabel10.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel10.Location = New System.Drawing.Point(8, 17)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.Size = New System.Drawing.Size(83, 17)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "Jumlah Rp."
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "{0:dd-MMMM-yyyy}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(525, 25)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(108, 17)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel12
        '
        Me.XrLabel12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel12.Location = New System.Drawing.Point(467, 25)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(58, 17)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.Text = "Bandung,"
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.arpay_total_amount", "{0:n2}")})
        Me.XrLabel7.Location = New System.Drawing.Point(108, 17)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(167, 25)
        Me.XrLabel7.Text = "XrLabel7"
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=sygma"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("arpay_code", "arpay_code"), New System.Data.Common.DataColumnMapping("arpay_date", "arpay_date"), New System.Data.Common.DataColumnMapping("arpay_eff_date", "arpay_eff_date"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("ptnra_line_1", "ptnra_line_1"), New System.Data.Common.DataColumnMapping("ptnra_line_2", "ptnra_line_2"), New System.Data.Common.DataColumnMapping("ptnra_line_3", "ptnra_line_3"), New System.Data.Common.DataColumnMapping("arpay_tbilang", "arpay_tbilang"), New System.Data.Common.DataColumnMapping("arpay_total_amount", "arpay_total_amount"), New System.Data.Common.DataColumnMapping("arpayd_ar_ref", "arpayd_ar_ref")})})
        '
        'cf_address
        '
        Me.cf_address.DataMember = "Table"
        Me.cf_address.DataSource = Me.Ds_kw_reg1
        Me.cf_address.Expression = "trim([ptnra_line_1] + ' ' + [ptnra_line_2] + ' ' + [ptnra_line_3])"
        Me.cf_address.Name = "cf_address"
        '
        'Ds_kw_reg1
        '
        Me.Ds_kw_reg1.DataSetName = "ds_kw_reg"
        Me.Ds_kw_reg1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'rptKwitansiReguler
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageFooter, Me.PageHeader})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.cf_address})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        Me.DataSource = Me.Ds_kw_reg1
        Me.DrawGrid = False
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.PageHeight = 583
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.Version = "9.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ds_kw_reg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrpb_logo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    'Friend WithEvents Ds_kw_reg1 As sygma_solution_system.ds_kw_reg
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents cf_address As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XRTerbilang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Ds_kw_reg1 As sygma_solution_system.ds_kw_reg
End Class
