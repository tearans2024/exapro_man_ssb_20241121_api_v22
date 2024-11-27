<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FProjectImport
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
        Me.components = New System.ComponentModel.Container
        Me.gc_prod_line = New DevExpress.XtraGrid.GridControl
        Me.gv_project = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.import_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.be_import_xls = New DevExpress.XtraEditors.ButtonEdit
        Me.sb_migrate = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_prod_line, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_project, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.import_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_import_xls.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_prod_line)
        Me.xtp_data.Size = New System.Drawing.Size(718, 382)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.import_en_id)
        Me.scc_master.Panel1.Controls.Add(Me.be_import_xls)
        Me.scc_master.Panel1.Controls.Add(Me.sb_migrate)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(720, 443)
        Me.scc_master.SplitterPosition = 34
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(720, 403)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(543, 285)
        '
        'gc_prod_line
        '
        Me.gc_prod_line.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_prod_line.Location = New System.Drawing.Point(5, 5)
        Me.gc_prod_line.MainView = Me.gv_project
        Me.gc_prod_line.Name = "gc_prod_line"
        Me.gc_prod_line.Size = New System.Drawing.Size(708, 372)
        Me.gc_prod_line.TabIndex = 0
        Me.gc_prod_line.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_project, Me.GridView2})
        '
        'gv_project
        '
        Me.gv_project.GridControl = Me.gc_prod_line
        Me.gv_project.Name = "gv_project"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_prod_line
        Me.GridView2.Name = "GridView2"
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(10, 9)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl2.TabIndex = 67
        Me.LabelControl2.Text = "Entity :"
        '
        'import_en_id
        '
        Me.import_en_id.Location = New System.Drawing.Point(48, 6)
        Me.import_en_id.Name = "import_en_id"
        Me.import_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.import_en_id.Size = New System.Drawing.Size(120, 20)
        Me.import_en_id.TabIndex = 66
        '
        'be_import_xls
        '
        Me.be_import_xls.Location = New System.Drawing.Point(267, 6)
        Me.be_import_xls.Name = "be_import_xls"
        Me.be_import_xls.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_import_xls.Size = New System.Drawing.Size(305, 20)
        Me.be_import_xls.TabIndex = 65
        '
        'sb_migrate
        '
        Me.sb_migrate.Location = New System.Drawing.Point(592, 5)
        Me.sb_migrate.Name = "sb_migrate"
        Me.sb_migrate.Size = New System.Drawing.Size(109, 23)
        Me.sb_migrate.TabIndex = 64
        Me.sb_migrate.Text = "Import"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(215, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl1.TabIndex = 63
        Me.LabelControl1.Text = "File Path :"
        '
        'FProjectImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(720, 443)
        Me.Name = "FProjectImport"
        Me.Text = "Project Import"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_prod_line, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_project, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.import_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_import_xls.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_prod_line As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_project As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents import_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents be_import_xls As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents sb_migrate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl

End Class
