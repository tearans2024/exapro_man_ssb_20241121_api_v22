<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FImportPurchaseOrderERP
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.te_po_number = New DevExpress.XtraEditors.TextEdit
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_erp = New DevExpress.XtraTab.XtraTabPage
        Me.gc_erp = New DevExpress.XtraGrid.GridControl
        Me.gv_erp = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.xtp_sys = New DevExpress.XtraTab.XtraTabPage
        Me.gc_sys = New DevExpress.XtraGrid.GridControl
        Me.gv_sys = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.sb_migrate = New DevExpress.XtraEditors.SimpleButton
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_po_number.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_erp.SuspendLayout()
        CType(Me.gc_erp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_erp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_sys.SuspendLayout()
        CType(Me.gc_sys, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_sys, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.sb_migrate)
        Me.scc_master.Panel1.Controls.Add(Me.te_po_number)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 29
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 7)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(115, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Purchase Order Number"
        '
        'te_po_number
        '
        Me.te_po_number.Location = New System.Drawing.Point(140, 4)
        Me.te_po_number.Name = "te_po_number"
        Me.te_po_number.Size = New System.Drawing.Size(124, 20)
        Me.te_po_number.TabIndex = 1
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_erp
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(555, 398)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_erp, Me.xtp_sys})
        '
        'xtp_erp
        '
        Me.xtp_erp.Controls.Add(Me.gc_erp)
        Me.xtp_erp.Name = "xtp_erp"
        Me.xtp_erp.Size = New System.Drawing.Size(553, 397)
        Me.xtp_erp.Text = "ERP"
        '
        'gc_erp
        '
        Me.gc_erp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_erp.Location = New System.Drawing.Point(0, 0)
        Me.gc_erp.MainView = Me.gv_erp
        Me.gc_erp.Name = "gc_erp"
        Me.gc_erp.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gc_erp.Size = New System.Drawing.Size(553, 397)
        Me.gc_erp.TabIndex = 0
        Me.gc_erp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_erp})
        '
        'gv_erp
        '
        Me.gv_erp.GridControl = Me.gc_erp
        Me.gv_erp.Name = "gv_erp"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'xtp_sys
        '
        Me.xtp_sys.Controls.Add(Me.gc_sys)
        Me.xtp_sys.Name = "xtp_sys"
        Me.xtp_sys.Size = New System.Drawing.Size(553, 397)
        Me.xtp_sys.Text = "Syspro"
        '
        'gc_sys
        '
        Me.gc_sys.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_sys.Location = New System.Drawing.Point(0, 0)
        Me.gc_sys.MainView = Me.gv_sys
        Me.gc_sys.Name = "gc_sys"
        Me.gc_sys.Size = New System.Drawing.Size(553, 397)
        Me.gc_sys.TabIndex = 0
        Me.gc_sys.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_sys})
        '
        'gv_sys
        '
        Me.gv_sys.GridControl = Me.gc_sys
        Me.gv_sys.Name = "gv_sys"
        '
        'sb_migrate
        '
        Me.sb_migrate.Location = New System.Drawing.Point(289, 3)
        Me.sb_migrate.Name = "sb_migrate"
        Me.sb_migrate.Size = New System.Drawing.Size(109, 23)
        Me.sb_migrate.TabIndex = 2
        Me.sb_migrate.Text = "Migrate To Syspro"
        '
        'FImportPurchaseOrderERP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FImportPurchaseOrderERP"
        Me.Text = "Import Purchase Order From ERP"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_po_number.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_erp.ResumeLayout(False)
        CType(Me.gc_erp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_erp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_sys.ResumeLayout(False)
        CType(Me.gc_sys, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_sys, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents te_po_number As DevExpress.XtraEditors.TextEdit
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_erp As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_erp As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_erp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents xtp_sys As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_sys As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_sys As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sb_migrate As DevExpress.XtraEditors.SimpleButton

End Class
