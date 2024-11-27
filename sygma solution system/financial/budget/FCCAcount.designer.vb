<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FCCAcount
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
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.cca_ac_id = New DevExpress.XtraEditors.LookUpEdit
        Me.cca_cc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.cca_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.cca_remarks = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.Lci_entity = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_type = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_cu_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.cca_ac_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cca_cc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cca_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cca_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(553, 412)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 433)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 412)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 352)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 402)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master, Me.GridView2})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_master
        Me.GridView2.Name = "GridView2"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.cca_ac_id)
        Me.lci_master.Controls.Add(Me.cca_cc_id)
        Me.lci_master.Controls.Add(Me.cca_en_id)
        Me.lci_master.Controls.Add(Me.cca_remarks)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 352)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'cca_ac_id
        '
        Me.cca_ac_id.Location = New System.Drawing.Point(74, 60)
        Me.cca_ac_id.Name = "cca_ac_id"
        Me.cca_ac_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cca_ac_id.Size = New System.Drawing.Size(457, 20)
        Me.cca_ac_id.StyleController = Me.lci_master
        Me.cca_ac_id.TabIndex = 11
        '
        'cca_cc_id
        '
        Me.cca_cc_id.Location = New System.Drawing.Point(74, 36)
        Me.cca_cc_id.Name = "cca_cc_id"
        Me.cca_cc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cca_cc_id.Size = New System.Drawing.Size(457, 20)
        Me.cca_cc_id.StyleController = Me.lci_master
        Me.cca_cc_id.TabIndex = 10
        '
        'cca_en_id
        '
        Me.cca_en_id.Location = New System.Drawing.Point(74, 12)
        Me.cca_en_id.Name = "cca_en_id"
        Me.cca_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cca_en_id.Size = New System.Drawing.Size(457, 20)
        Me.cca_en_id.StyleController = Me.lci_master
        Me.cca_en_id.TabIndex = 9
        '
        'cca_remarks
        '
        Me.cca_remarks.Location = New System.Drawing.Point(74, 84)
        Me.cca_remarks.Name = "cca_remarks"
        Me.cca_remarks.Size = New System.Drawing.Size(457, 20)
        Me.cca_remarks.StyleController = Me.lci_master
        Me.cca_remarks.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.Lci_entity, Me.LayoutControlItem1, Me.lci_type, Me.lci_cu_desc, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 352)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'Lci_entity
        '
        Me.Lci_entity.Control = Me.cca_en_id
        Me.Lci_entity.CustomizationFormText = "Entity"
        Me.Lci_entity.Location = New System.Drawing.Point(0, 0)
        Me.Lci_entity.Name = "Lci_entity"
        Me.Lci_entity.Size = New System.Drawing.Size(523, 24)
        Me.Lci_entity.Text = "Entity"
        Me.Lci_entity.TextSize = New System.Drawing.Size(58, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.cca_cc_id
        Me.LayoutControlItem1.CustomizationFormText = "Warehouse"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Cost Centre"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(58, 13)
        '
        'lci_type
        '
        Me.lci_type.Control = Me.cca_ac_id
        Me.lci_type.CustomizationFormText = "Type"
        Me.lci_type.Location = New System.Drawing.Point(0, 48)
        Me.lci_type.Name = "lci_type"
        Me.lci_type.Size = New System.Drawing.Size(523, 24)
        Me.lci_type.Text = "Account"
        Me.lci_type.TextSize = New System.Drawing.Size(58, 13)
        '
        'lci_cu_desc
        '
        Me.lci_cu_desc.Control = Me.cca_remarks
        Me.lci_cu_desc.CustomizationFormText = "Description"
        Me.lci_cu_desc.Location = New System.Drawing.Point(0, 72)
        Me.lci_cu_desc.Name = "lci_cu_desc"
        Me.lci_cu_desc.Size = New System.Drawing.Size(523, 24)
        Me.lci_cu_desc.Text = "Remarks"
        Me.lci_cu_desc.TextSize = New System.Drawing.Size(58, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 96)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 236)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FCCAcount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FCCAcount"
        Me.Text = "Account Cost Center"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.cca_ac_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cca_cc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cca_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cca_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents cca_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_cu_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cca_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Lci_entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cca_ac_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cca_cc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_type As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
