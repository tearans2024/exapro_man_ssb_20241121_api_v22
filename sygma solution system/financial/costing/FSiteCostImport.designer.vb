<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSiteCostImport
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
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_header = New DevExpress.XtraTab.XtraTabPage
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        Me.be_import_excel = New DevExpress.XtraEditors.ButtonEdit
        Me.sb_import = New DevExpress.XtraEditors.SimpleButton
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_header.SuspendLayout()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_import_excel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.sb_import)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl4)
        Me.scc_master.Panel1.Controls.Add(Me.be_import_excel)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(777, 433)
        Me.scc_master.SplitterPosition = 32
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.SelectedTabPage = Me.xtp_header
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(777, 395)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_header})
        '
        'xtp_header
        '
        Me.xtp_header.Controls.Add(Me.gc_master)
        Me.xtp_header.Name = "xtp_header"
        Me.xtp_header.Size = New System.Drawing.Size(770, 388)
        Me.xtp_header.Text = "Header"
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(0, 0)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(770, 388)
        Me.gc_master.TabIndex = 2
        Me.gc_master.UseEmbeddedNavigator = True
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        Me.gv_master.OptionsView.ShowAutoFilterRow = True
        Me.gv_master.OptionsView.ShowFooter = True
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(16, 9)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(85, 13)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Import from Excel"
        '
        'be_import_excel
        '
        Me.be_import_excel.Location = New System.Drawing.Point(117, 6)
        Me.be_import_excel.Name = "be_import_excel"
        Me.be_import_excel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_import_excel.Size = New System.Drawing.Size(371, 20)
        Me.be_import_excel.TabIndex = 2
        '
        'sb_import
        '
        Me.sb_import.Location = New System.Drawing.Point(504, 4)
        Me.sb_import.Name = "sb_import"
        Me.sb_import.Size = New System.Drawing.Size(75, 23)
        Me.sb_import.TabIndex = 4
        Me.sb_import.Text = "Import"
        '
        'FSiteCostImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(777, 433)
        Me.Name = "FSiteCostImport"
        Me.Text = "Site Cost Import"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_header.ResumeLayout(False)
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_import_excel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_header As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Public WithEvents be_import_excel As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sb_import As DevExpress.XtraEditors.SimpleButton

End Class
