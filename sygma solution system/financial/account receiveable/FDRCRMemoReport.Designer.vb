<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDRCRMemoReport
    Inherits master_new.MasterInfTwo

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_1 = New DevExpress.XtraTab.XtraTabPage
        Me.gc_master_header = New DevExpress.XtraGrid.GridControl
        Me.gv_master_header = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.DsaragingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Ds_ar_aging = New sygma_solution_system.ds_ar_aging
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.DataTable1TableAdapter = New sygma_solution_system.ds_soTableAdapters.DataTable1TableAdapter
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_1.SuspendLayout()
        CType(Me.gc_master_header, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master_header, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(1013, 433)
        Me.scc_master.SplitterPosition = 29
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_1
        Me.xtc_master.Size = New System.Drawing.Size(1013, 398)
        Me.xtc_master.TabIndex = 3
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_1, Me.XtraTabPage1})
        '
        'xtp_1
        '
        Me.xtp_1.Controls.Add(Me.gc_master_header)
        Me.xtp_1.Name = "xtp_1"
        Me.xtp_1.Size = New System.Drawing.Size(1011, 377)
        Me.xtp_1.Text = "Data Header"
        '
        'gc_master_header
        '
        Me.gc_master_header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master_header.Location = New System.Drawing.Point(0, 0)
        Me.gc_master_header.MainView = Me.gv_master_header
        Me.gc_master_header.Name = "gc_master_header"
        Me.gc_master_header.Size = New System.Drawing.Size(1011, 377)
        Me.gc_master_header.TabIndex = 62
        Me.gc_master_header.UseEmbeddedNavigator = True
        Me.gc_master_header.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master_header})
        '
        'gv_master_header
        '
        Me.gv_master_header.GridControl = Me.gc_master_header
        Me.gv_master_header.Name = "gv_master_header"
        Me.gv_master_header.OptionsView.ColumnAutoWidth = False
        Me.gv_master_header.OptionsView.ShowAutoFilterRow = True
        Me.gv_master_header.OptionsView.ShowFooter = True
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.gc_detail)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1011, 377)
        Me.XtraTabPage1.Text = "Data Detail"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(1011, 377)
        Me.gc_detail.TabIndex = 63
        Me.gc_detail.UseEmbeddedNavigator = True
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_detail.OptionsView.ShowAutoFilterRow = True
        Me.gv_detail.OptionsView.ShowFooter = True
        '
        'DsaragingBindingSource
        '
        Me.DsaragingBindingSource.DataSource = Me.Ds_ar_aging
        Me.DsaragingBindingSource.Position = 0
        '
        'Ds_ar_aging
        '
        Me.Ds_ar_aging.DataSetName = "ds_ar_aging"
        Me.Ds_ar_aging.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(247, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 33
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(193, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 32
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(61, 3)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 31
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(6, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 30
        Me.LabelControl1.Text = "First Date"
        '
        'DataTable1TableAdapter
        '
        Me.DataTable1TableAdapter.ClearBeforeFill = True
        '
        'FDRCRMemoReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1013, 433)
        Me.Name = "FDRCRMemoReport"
        Me.Text = "Debit Credit Memo Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_1.ResumeLayout(False)
        CType(Me.gc_master_header, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master_header, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DsaragingBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Ds_ar_aging As sygma_solution_system.ds_ar_aging
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DataTable1TableAdapter As sygma_solution_system.ds_soTableAdapters.DataTable1TableAdapter
    Friend WithEvents gc_master_header As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master_header As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView

End Class
