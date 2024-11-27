<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FWorkCenterImport
    Inherits master_new.MasterWITwo

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
        Me.gc_location = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.import_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.be_import_xls = New DevExpress.XtraEditors.ButtonEdit
        Me.sb_migrate = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_location, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.import_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_import_xls.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_location)
        Me.xtp_data.Size = New System.Drawing.Size(711, 363)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.import_en_id)
        Me.scc_master.Panel1.Controls.Add(Me.be_import_xls)
        Me.scc_master.Panel1.Controls.Add(Me.sb_migrate)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(713, 433)
        Me.scc_master.SplitterPosition = 43
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(713, 384)
        '
        'xtp_edit
        '
        Me.xtp_edit.PageVisible = False
        Me.xtp_edit.Size = New System.Drawing.Size(711, 363)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(701, 303)
        '
        'gc_location
        '
        Me.gc_location.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_location.Location = New System.Drawing.Point(5, 5)
        Me.gc_location.MainView = Me.gv_master
        Me.gc_location.Name = "gc_location"
        Me.gc_location.Size = New System.Drawing.Size(701, 353)
        Me.gc_location.TabIndex = 1
        Me.gc_location.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master, Me.GridView2})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_location
        Me.gv_master.Name = "gv_master"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_location
        Me.GridView2.Name = "GridView2"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(9, 15)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl2.TabIndex = 72
        Me.LabelControl2.Text = "Entity :"
        '
        'import_en_id
        '
        Me.import_en_id.Location = New System.Drawing.Point(47, 12)
        Me.import_en_id.Name = "import_en_id"
        Me.import_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.import_en_id.Size = New System.Drawing.Size(120, 20)
        Me.import_en_id.TabIndex = 71
        '
        'be_import_xls
        '
        Me.be_import_xls.Location = New System.Drawing.Point(266, 12)
        Me.be_import_xls.Name = "be_import_xls"
        Me.be_import_xls.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_import_xls.Size = New System.Drawing.Size(305, 20)
        Me.be_import_xls.TabIndex = 70
        '
        'sb_migrate
        '
        Me.sb_migrate.Location = New System.Drawing.Point(591, 11)
        Me.sb_migrate.Name = "sb_migrate"
        Me.sb_migrate.Size = New System.Drawing.Size(109, 23)
        Me.sb_migrate.TabIndex = 69
        Me.sb_migrate.Text = "Import"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(214, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl1.TabIndex = 68
        Me.LabelControl1.Text = "File Path :"
        '
        'lci_master
        '
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(701, 303)
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.SimpleSeparator1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(701, 303)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'SimpleSeparator1
        '
        Me.SimpleSeparator1.CustomizationFormText = "SimpleSeparator1"
        Me.SimpleSeparator1.Location = New System.Drawing.Point(0, 0)
        Me.SimpleSeparator1.Name = "SimpleSeparator1"
        Me.SimpleSeparator1.Size = New System.Drawing.Size(681, 283)
        Me.SimpleSeparator1.Text = "SimpleSeparator1"
        '
        'FWorkCenterImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(713, 433)
        Me.Name = "FWorkCenterImport"
        Me.Text = "Work Center Import"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_location, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.import_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_import_xls.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_location As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents import_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents be_import_xls As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents sb_migrate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator

End Class
