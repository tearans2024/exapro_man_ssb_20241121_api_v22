<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XR_WO_QRCode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XR_WO_QRCode))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel35 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel36 = New DevExpress.XtraReports.UI.XRLabel
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.PgSqlDataAdapter1 = New CoreLab.PostgreSql.PgSqlDataAdapter
        Me.pgSqlSelectCommand1 = New CoreLab.PostgreSql.PgSqlCommand
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Height = 17
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel35
        '
        Me.XrLabel35.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "wo_mstr.pt_desc11", "")})
        Me.XrLabel35.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel35.Location = New System.Drawing.Point(17, 208)
        Me.XrLabel35.Name = "XrLabel35"
        Me.XrLabel35.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel35.Size = New System.Drawing.Size(133, 17)
        Me.XrLabel35.StylePriority.UseFont = False
        Me.XrLabel35.Text = "XrLabel35"
        '
        'XrLabel36
        '
        Me.XrLabel36.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "wo_mstr.pt_code1", "")})
        Me.XrLabel36.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel36.Location = New System.Drawing.Point(25, 192)
        Me.XrLabel36.Name = "XrLabel36"
        Me.XrLabel36.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel36.Size = New System.Drawing.Size(100, 17)
        Me.XrLabel36.StylePriority.UseFont = False
        Me.XrLabel36.Text = "XrLabel36"
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
        Me.PageFooter.Height = 8
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Height = 20
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'PgSqlDataAdapter1
        '
        Me.PgSqlDataAdapter1.SelectCommand = Me.pgSqlSelectCommand1
        Me.PgSqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "wo_mstr", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("wo_oid", "wo_oid"), New System.Data.Common.DataColumnMapping("wo_dom_id", "wo_dom_id"), New System.Data.Common.DataColumnMapping("wo_en_id", "wo_en_id"), New System.Data.Common.DataColumnMapping("wo_si_id", "wo_si_id"), New System.Data.Common.DataColumnMapping("wo_id", "wo_id"), New System.Data.Common.DataColumnMapping("wo_code", "wo_code"), New System.Data.Common.DataColumnMapping("wo_type", "wo_type"), New System.Data.Common.DataColumnMapping("wo_pt_id", "wo_pt_id"), New System.Data.Common.DataColumnMapping("wo_qty_ord", "wo_qty_ord"), New System.Data.Common.DataColumnMapping("wo_qty_comp", "wo_qty_comp"), New System.Data.Common.DataColumnMapping("wo_qty_rjc", "wo_qty_rjc"), New System.Data.Common.DataColumnMapping("wo_ord_date", "wo_ord_date"), New System.Data.Common.DataColumnMapping("wo_rel_date", "wo_rel_date"), New System.Data.Common.DataColumnMapping("wo_due_date", "wo_due_date"), New System.Data.Common.DataColumnMapping("wo_insheet_pct", "wo_insheet_pct"), New System.Data.Common.DataColumnMapping("wo_ro_id", "wo_ro_id"), New System.Data.Common.DataColumnMapping("wo_status", "wo_status"), New System.Data.Common.DataColumnMapping("wo_remarks", "wo_remarks"), New System.Data.Common.DataColumnMapping("wo_pjc_id", "wo_pjc_id"), New System.Data.Common.DataColumnMapping("wo_dt", "wo_dt"), New System.Data.Common.DataColumnMapping("wo_ref_rework", "wo_ref_rework"), New System.Data.Common.DataColumnMapping("wo_ref_code", "wo_ref_code"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("si_desc", "si_desc"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1"), New System.Data.Common.DataColumnMapping("pt_desc2", "pt_desc2"), New System.Data.Common.DataColumnMapping("pjc_code", "pjc_code"), New System.Data.Common.DataColumnMapping("pjc_desc", "pjc_desc"), New System.Data.Common.DataColumnMapping("ro_code", "ro_code"), New System.Data.Common.DataColumnMapping("ro_desc", "ro_desc"), New System.Data.Common.DataColumnMapping("wod_op", "wod_op"), New System.Data.Common.DataColumnMapping("wod_qty_per", "wod_qty_per"), New System.Data.Common.DataColumnMapping("wod_qty_req", "wod_qty_req"), New System.Data.Common.DataColumnMapping("wod_qty_alloc", "wod_qty_alloc"), New System.Data.Common.DataColumnMapping("wod_qty_picked", "wod_qty_picked"), New System.Data.Common.DataColumnMapping("wod_qty_issued", "wod_qty_issued"), New System.Data.Common.DataColumnMapping("wod_cost", "wod_cost"), New System.Data.Common.DataColumnMapping("wod_dt", "wod_dt"), New System.Data.Common.DataColumnMapping("wod_indirect", "wod_indirect"), New System.Data.Common.DataColumnMapping("wod_start_date", "wod_start_date"), New System.Data.Common.DataColumnMapping("wod_end_date", "wod_end_date"), New System.Data.Common.DataColumnMapping("wod_qty", "wod_qty"), New System.Data.Common.DataColumnMapping("wod_yield_pct", "wod_yield_pct"), New System.Data.Common.DataColumnMapping("wod_seq", "wod_seq"), New System.Data.Common.DataColumnMapping("wod_qty_yield", "wod_qty_yield"), New System.Data.Common.DataColumnMapping("pt_code1", "pt_code1"), New System.Data.Common.DataColumnMapping("pt_desc11", "pt_desc11"), New System.Data.Common.DataColumnMapping("wod_qty_remaining", "wod_qty_remaining"), New System.Data.Common.DataColumnMapping("op_name", "op_name"), New System.Data.Common.DataColumnMapping("op_desc", "op_desc"), New System.Data.Common.DataColumnMapping("ro_total", "ro_total"), New System.Data.Common.DataColumnMapping("ro_yield", "ro_yield"), New System.Data.Common.DataColumnMapping("um_desc", "um_desc")})})
        '
        'pgSqlSelectCommand1
        '
        Me.pgSqlSelectCommand1.CommandText = resources.GetString("pgSqlSelectCommand1.CommandText")
        Me.pgSqlSelectCommand1.Name = "pgSqlSelectCommand1"
        Me.pgSqlSelectCommand1.Owner = Me
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Location = New System.Drawing.Point(8, 33)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.Size = New System.Drawing.Size(142, 117)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel19
        '
        Me.XrLabel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel19.Location = New System.Drawing.Point(8, 0)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.Size = New System.Drawing.Size(150, 25)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "WORK ORDER"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "{0:dd/MM/yyyy}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(25, 175)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(117, 17)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wo_code", "")})
        Me.XrLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(17, 158)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(117, 17)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrPageInfo1, Me.XrLabel19, Me.XrPictureBox1, Me.XrLabel36, Me.XrLabel35})
        Me.GroupHeader1.Height = 238
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XR_WO_QRCode
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.GroupHeader1, Me.PageHeader, Me.PageFooter, Me.GroupFooter1})
        Me.DataAdapter = Me.PgSqlDataAdapter1
        Me.DataMember = "wo_mstr"
        Me.Version = "9.2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents PgSqlDataAdapter1 As CoreLab.PostgreSql.PgSqlDataAdapter
    Friend WithEvents pgSqlSelectCommand1 As CoreLab.PostgreSql.PgSqlCommand
    'Private WithEvents XR_sub_routing1 As syspro_ver2.XR_sub_routing
    Friend WithEvents XrLabel35 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel36 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    'Friend WithEvents DSworkOrder1 As syspro_ver2.DSworkOrder
    'Private WithEvents XR_sub_routing2 As syspro_ver2.XR_sub_routing
End Class
