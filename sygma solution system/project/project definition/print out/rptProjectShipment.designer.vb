<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptProjectShipment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptProjectShipment))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.xlabel_post1 = New DevExpress.XtraReports.UI.XRLabel
        Me.lbl_authorized_2 = New DevExpress.XtraReports.UI.XRLabel
        Me.lbl_authorized_1 = New DevExpress.XtraReports.UI.XRLabel
        Me.xlabel_sign2 = New DevExpress.XtraReports.UI.XRLabel
        Me.xline_sign2 = New DevExpress.XtraReports.UI.XRLine
        Me.xlabel_sign1 = New DevExpress.XtraReports.UI.XRLabel
        Me.xl_by = New DevExpress.XtraReports.UI.XRLabel
        Me.xline_sign1 = New DevExpress.XtraReports.UI.XRLine
        Me.xlabel_post2 = New DevExpress.XtraReports.UI.XRLabel
        Me.xlabel_post3 = New DevExpress.XtraReports.UI.XRLabel
        Me.xline_sign3 = New DevExpress.XtraReports.UI.XRLine
        Me.xlabel_sign3 = New DevExpress.XtraReports.UI.XRLabel
        Me.lbl_authorized_3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel
        Me.cf_pt_description = New DevExpress.XtraReports.UI.CalculatedField
        Me.Ds_project_shipment1 = New syspro_ver2.ds_project_shipment
        Me.cf_det_pt_desc = New DevExpress.XtraReports.UI.CalculatedField
        Me.qty_open = New DevExpress.XtraReports.UI.CalculatedField
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ds_project_shipment1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.Detail.Height = 21
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.PrintOnEmptyDataSource = False
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("wod_pt_code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrTable1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTable1.Location = New System.Drawing.Point(12, 0)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(749, 20)
        Me.XrTable1.StylePriority.UseBorders = False
        Me.XrTable1.StylePriority.UseFont = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell3, Me.XrTableCell8, Me.XrTableCell5, Me.XrTableCell6, Me.XrTableCell9})
        Me.XrTableRow1.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.StylePriority.UseFont = False
        Me.XrTableRow1.Weight = 0.33999999999999997
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n0}"
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell1.Summary = XrSummary1
        Me.XrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell1.Weight = 0.066254928151128267
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.pt_code", "")})
        Me.XrTableCell3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell3.Weight = 0.28132348886871916
        '
        'XrTableCell8
        '
        Me.XrTableCell8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.pt_desc", "")})
        Me.XrTableCell8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.StylePriority.UseFont = False
        Me.XrTableCell8.StylePriority.UseTextAlignment = False
        Me.XrTableCell8.Text = "XrTableCell8"
        Me.XrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell8.Weight = 1.0121433011579803
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.soshipds_qty", "{0:n}")})
        Me.XrTableCell5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell5.StylePriority.UseFont = False
        Me.XrTableCell5.StylePriority.UsePadding = False
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.Text = "XrTableCell5"
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell5.Weight = 0.13328238699055803
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.unit", "")})
        Me.XrTableCell6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.StylePriority.UseFont = False
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        Me.XrTableCell6.Text = "XrTableCell6"
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell6.Weight = 0.070934632029200279
        '
        'XrTableCell9
        '
        Me.XrTableCell9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.loc_desc", "")})
        Me.XrTableCell9.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.StylePriority.UseFont = False
        Me.XrTableCell9.StylePriority.UseTextAlignment = False
        Me.XrTableCell9.Text = "XrTableCell9"
        Me.XrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell9.Weight = 0.31993842211143386
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine2, Me.xlabel_post1, Me.lbl_authorized_2, Me.lbl_authorized_1, Me.xlabel_sign2, Me.xline_sign2, Me.xlabel_sign1, Me.xl_by, Me.xline_sign1, Me.xlabel_post2, Me.xlabel_post3, Me.xline_sign3, Me.xlabel_sign3, Me.lbl_authorized_3, Me.XrLabel7, Me.XrLabel8, Me.XrLine3, Me.XrLabel9})
        Me.GroupFooter1.Height = 164
        Me.GroupFooter1.Name = "GroupFooter1"
        Me.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
        Me.GroupFooter1.PrintAtBottom = True
        '
        'XrLine2
        '
        Me.XrLine2.Location = New System.Drawing.Point(12, 8)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(749, 9)
        '
        'xlabel_post1
        '
        Me.xlabel_post1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.post1", "")})
        Me.xlabel_post1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_post1.Location = New System.Drawing.Point(12, 119)
        Me.xlabel_post1.Name = "xlabel_post1"
        Me.xlabel_post1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_post1.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_post1.StylePriority.UseFont = False
        Me.xlabel_post1.StylePriority.UseTextAlignment = False
        Me.xlabel_post1.Text = "xlabel_post1"
        Me.xlabel_post1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lbl_authorized_2
        '
        Me.lbl_authorized_2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_authorized_2.Location = New System.Drawing.Point(204, 29)
        Me.lbl_authorized_2.Name = "lbl_authorized_2"
        Me.lbl_authorized_2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbl_authorized_2.Size = New System.Drawing.Size(133, 17)
        Me.lbl_authorized_2.StylePriority.UseFont = False
        Me.lbl_authorized_2.StylePriority.UseTextAlignment = False
        Me.lbl_authorized_2.Text = "Authorized Signature"
        Me.lbl_authorized_2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lbl_authorized_1
        '
        Me.lbl_authorized_1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_authorized_1.Location = New System.Drawing.Point(21, 29)
        Me.lbl_authorized_1.Name = "lbl_authorized_1"
        Me.lbl_authorized_1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbl_authorized_1.Size = New System.Drawing.Size(133, 17)
        Me.lbl_authorized_1.StylePriority.UseFont = False
        Me.lbl_authorized_1.StylePriority.UseTextAlignment = False
        Me.lbl_authorized_1.Text = "Authorized Signature"
        Me.lbl_authorized_1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xlabel_sign2
        '
        Me.xlabel_sign2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.sign2", "")})
        Me.xlabel_sign2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_sign2.Location = New System.Drawing.Point(196, 96)
        Me.xlabel_sign2.Name = "xlabel_sign2"
        Me.xlabel_sign2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_sign2.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_sign2.StylePriority.UseFont = False
        Me.xlabel_sign2.StylePriority.UseTextAlignment = False
        Me.xlabel_sign2.Text = "[sign2]"
        Me.xlabel_sign2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xline_sign2
        '
        Me.xline_sign2.Location = New System.Drawing.Point(196, 108)
        Me.xline_sign2.Name = "xline_sign2"
        Me.xline_sign2.Size = New System.Drawing.Size(142, 9)
        '
        'xlabel_sign1
        '
        Me.xlabel_sign1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.sign1", "")})
        Me.xlabel_sign1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_sign1.Location = New System.Drawing.Point(12, 96)
        Me.xlabel_sign1.Name = "xlabel_sign1"
        Me.xlabel_sign1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_sign1.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_sign1.StylePriority.UseFont = False
        Me.xlabel_sign1.StylePriority.UseTextAlignment = False
        Me.xlabel_sign1.Text = "xlabel_sign1"
        Me.xlabel_sign1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xl_by
        '
        Me.xl_by.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xl_by.Location = New System.Drawing.Point(8, 96)
        Me.xl_by.Name = "xl_by"
        Me.xl_by.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xl_by.Size = New System.Drawing.Size(42, 17)
        Me.xl_by.StylePriority.UseFont = False
        Me.xl_by.Text = "By :"
        '
        'xline_sign1
        '
        Me.xline_sign1.Location = New System.Drawing.Point(12, 108)
        Me.xline_sign1.Name = "xline_sign1"
        Me.xline_sign1.Size = New System.Drawing.Size(142, 9)
        '
        'xlabel_post2
        '
        Me.xlabel_post2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.post2", "")})
        Me.xlabel_post2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_post2.Location = New System.Drawing.Point(196, 119)
        Me.xlabel_post2.Name = "xlabel_post2"
        Me.xlabel_post2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_post2.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_post2.StylePriority.UseFont = False
        Me.xlabel_post2.StylePriority.UseTextAlignment = False
        Me.xlabel_post2.Text = "xlabel_post2"
        Me.xlabel_post2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xlabel_post3
        '
        Me.xlabel_post3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.post3", "")})
        Me.xlabel_post3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_post3.Location = New System.Drawing.Point(376, 119)
        Me.xlabel_post3.Name = "xlabel_post3"
        Me.xlabel_post3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_post3.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_post3.StylePriority.UseFont = False
        Me.xlabel_post3.StylePriority.UseTextAlignment = False
        Me.xlabel_post3.Text = "xlabel_post3"
        Me.xlabel_post3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xline_sign3
        '
        Me.xline_sign3.Location = New System.Drawing.Point(376, 108)
        Me.xline_sign3.Name = "xline_sign3"
        Me.xline_sign3.Size = New System.Drawing.Size(142, 9)
        '
        'xlabel_sign3
        '
        Me.xlabel_sign3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.sign3", "")})
        Me.xlabel_sign3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_sign3.Location = New System.Drawing.Point(376, 96)
        Me.xlabel_sign3.Name = "xlabel_sign3"
        Me.xlabel_sign3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_sign3.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_sign3.StylePriority.UseFont = False
        Me.xlabel_sign3.StylePriority.UseTextAlignment = False
        Me.xlabel_sign3.Text = "xlabel_sign3"
        Me.xlabel_sign3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lbl_authorized_3
        '
        Me.lbl_authorized_3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_authorized_3.Location = New System.Drawing.Point(384, 29)
        Me.lbl_authorized_3.Name = "lbl_authorized_3"
        Me.lbl_authorized_3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbl_authorized_3.Size = New System.Drawing.Size(133, 17)
        Me.lbl_authorized_3.StylePriority.UseFont = False
        Me.lbl_authorized_3.StylePriority.UseTextAlignment = False
        Me.lbl_authorized_3.Text = "Authorized Signature"
        Me.lbl_authorized_3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel7.Location = New System.Drawing.Point(600, 29)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(133, 17)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "RECIPIENT,"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel8.Location = New System.Drawing.Point(592, 96)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(142, 17)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine3
        '
        Me.XrLine3.Location = New System.Drawing.Point(592, 108)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.Size = New System.Drawing.Size(142, 9)
        '
        'XrLabel9
        '
        Me.XrLabel9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.XrLabel9.Location = New System.Drawing.Point(592, 117)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.Size = New System.Drawing.Size(142, 17)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'cf_pt_description
        '
        Me.cf_pt_description.DataMember = "DataTable1"
        Me.cf_pt_description.DataSource = Me.Ds_project_shipment1
        Me.cf_pt_description.Expression = "trim([pt_desc1] + ' ' + [pt_desc2])"
        Me.cf_pt_description.Name = "cf_pt_description"
        '
        'Ds_project_shipment1
        '
        Me.Ds_project_shipment1.DataSetName = "ds_project_shipment"
        Me.Ds_project_shipment1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cf_det_pt_desc
        '
        Me.cf_det_pt_desc.DataMember = "DataTable1"
        Me.cf_det_pt_desc.DataSource = Me.Ds_project_shipment1
        Me.cf_det_pt_desc.Expression = "trim([ptbomdesc] + ' ' + [ptbomdesc2])"
        Me.cf_det_pt_desc.Name = "cf_det_pt_desc"
        '
        'qty_open
        '
        Me.qty_open.DataMember = "DataTable1"
        Me.qty_open.DataSource = Me.Ds_project_shipment1
        Me.qty_open.Expression = "[wod_qty_req] - [wod_qty_issued]"
        Me.qty_open.FieldType = DevExpress.XtraReports.UI.FieldType.[Double]
        Me.qty_open.Name = "qty_open"
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel25.Location = New System.Drawing.Point(617, 59)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = ":"
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel22.Location = New System.Drawing.Point(498, 59)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = "Project Number"
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.prj_code", "")})
        Me.XrLabel18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel18.Location = New System.Drawing.Point(627, 59)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "XrLabel18"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel27
        '
        Me.XrLabel27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel27.Location = New System.Drawing.Point(498, 93)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel27.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel27.StylePriority.UseFont = False
        Me.XrLabel27.Text = "Page"
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.Location = New System.Drawing.Point(617, 93)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = ":"
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo2.Location = New System.Drawing.Point(627, 93)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.RunningBand = Me.GroupHeader1
        Me.XrPageInfo2.Size = New System.Drawing.Size(116, 17)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Image = CType(resources.GetObject("XrPictureBox1.Image"), System.Drawing.Image)
        Me.XrPictureBox1.Location = New System.Drawing.Point(8, 0)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.Size = New System.Drawing.Size(133, 117)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        Me.XrPictureBox1.Visible = False
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.soship_code", "")})
        Me.XrLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(627, 25)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.soship_date", "{0:dd/MM/yyyy}")})
        Me.XrLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.Location = New System.Drawing.Point(627, 42)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "{0:dd/MM/yyyy}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(627, 76)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(116, 17)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.Location = New System.Drawing.Point(498, 25)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "Shipment Number"
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.Location = New System.Drawing.Point(498, 42)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "Shipment Date"
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.Location = New System.Drawing.Point(498, 76)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = "Print Date"
        '
        'XrLabel19
        '
        Me.XrLabel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel19.Location = New System.Drawing.Point(542, 0)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.Size = New System.Drawing.Size(201, 25)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "PROJECT SHIPMENT"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel20.Location = New System.Drawing.Point(617, 25)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.Text = ":"
        '
        'XrLabel21
        '
        Me.XrLabel21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel21.Location = New System.Drawing.Point(617, 42)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.Text = ":"
        '
        'XrLabel23
        '
        Me.XrLabel23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel23.Location = New System.Drawing.Point(617, 76)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.Text = ":"
        '
        'XrLabel24
        '
        Me.XrLabel24.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel24.Location = New System.Drawing.Point(12, 128)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.Text = "Customer :"
        '
        'XrLabel10
        '
        Me.XrLabel10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.ptnr_name", "")})
        Me.XrLabel10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel10.Location = New System.Drawing.Point(12, 149)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.Size = New System.Drawing.Size(471, 18)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "XrLabel10"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel10, Me.XrLabel24, Me.XrLabel23, Me.XrLabel21, Me.XrLabel20, Me.XrLabel19, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrPageInfo1, Me.XrLabel2, Me.XrLabel1, Me.XrPictureBox1, Me.XrPageInfo2, Me.XrLabel6, Me.XrLabel27, Me.XrLabel18, Me.XrLabel22, Me.XrLabel25, Me.XrLine1, Me.XrTable2})
        Me.GroupHeader1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("soship_code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.Height = 214
        Me.GroupHeader1.KeepTogether = True
        Me.GroupHeader1.Name = "GroupHeader1"
        Me.GroupHeader1.StylePriority.UseFont = False
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(12, 180)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(749, 9)
        '
        'XrTable2
        '
        Me.XrTable2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTable2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTable2.Location = New System.Drawing.Point(12, 190)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(749, 21)
        Me.XrTable2.StylePriority.UseBorders = False
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell11, Me.XrTableCell12, Me.XrTableCell7, Me.XrTableCell15, Me.XrTableCell2, Me.XrTableCell4})
        Me.XrTableRow2.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.StylePriority.UseFont = False
        Me.XrTableRow2.Weight = 0.33999999999999997
        '
        'XrTableCell11
        '
        Me.XrTableCell11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.StylePriority.UseFont = False
        Me.XrTableCell11.StylePriority.UseTextAlignment = False
        Me.XrTableCell11.Text = "No."
        Me.XrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell11.Weight = 0.066254928151128267
        '
        'XrTableCell12
        '
        Me.XrTableCell12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.StylePriority.UseFont = False
        Me.XrTableCell12.StylePriority.UseTextAlignment = False
        Me.XrTableCell12.Text = "Part Number"
        Me.XrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell12.Weight = 0.28132348886871916
        '
        'XrTableCell7
        '
        Me.XrTableCell7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.StylePriority.UseFont = False
        Me.XrTableCell7.StylePriority.UseTextAlignment = False
        Me.XrTableCell7.Text = "Description"
        Me.XrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell7.Weight = 1.0121433011579803
        '
        'XrTableCell15
        '
        Me.XrTableCell15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell15.StylePriority.UseFont = False
        Me.XrTableCell15.StylePriority.UsePadding = False
        Me.XrTableCell15.StylePriority.UseTextAlignment = False
        Me.XrTableCell15.Text = "Qty"
        Me.XrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell15.Weight = 0.1325876758630625
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = "UM"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell2.Weight = 0.071281987592947987
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.StylePriority.UseTextAlignment = False
        Me.XrTableCell4.Text = "Location"
        Me.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell4.Weight = 0.32028577767518157
        '
        'rptProjectShipment
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.GroupHeader1, Me.GroupFooter1})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.cf_pt_description, Me.cf_det_pt_desc, Me.qty_open})
        Me.DataSource = Me.Ds_project_shipment1
        Me.DrawGrid = False
        Me.GridSize = New System.Drawing.Size(2, 2)
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(30, 30, 50, 60)
        Me.PageHeight = 583
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.Version = "9.1"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ds_project_shipment1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents cf_pt_description As DevExpress.XtraReports.UI.CalculatedField
    'Friend WithEvents DataTable1TableAdapter As sygma_solution_system.ds_woTableAdapters.DataTable1TableAdapter
    'Friend WithEvents Ds_wo1 As sygma_solution_system.ds_wo
    Friend WithEvents cf_det_pt_desc As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents qty_open As DevExpress.XtraReports.UI.CalculatedField
    'Friend WithEvents Ds_wo_receipt_print1 As syspro_ver2.ds_wo_receipt_print
    'Friend WithEvents DataTable1TableAdapter As syspro_ver2.ds_wo_receipt_printTableAdapters.DataTable1TableAdapter
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    'Friend WithEvents Ds_project_shipment1 As syspro_ver2.ds_project_shipment
    'Friend WithEvents DataTable1TableAdapter As syspro_ver2.ds_project_shipmentTableAdapters.DataTable1TableAdapter
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xlabel_post1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbl_authorized_2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbl_authorized_1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xlabel_sign2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xline_sign2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xlabel_sign1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xl_by As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xline_sign1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xlabel_post2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xlabel_post3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xline_sign3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xlabel_sign3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbl_authorized_3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents Ds_project_shipment1 As syspro_ver2.ds_project_shipment
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    'Friend WithEvents DsWOPickList1 As syspro_ver2.dsWOPickList
End Class
