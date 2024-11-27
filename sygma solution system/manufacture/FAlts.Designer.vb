<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FAlts
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
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.alts_active = New DevExpress.XtraEditors.CheckEdit
        Me.alts_ref = New DevExpress.XtraEditors.TextEdit
        Me.alts_bom_id = New DevExpress.XtraEditors.LookUpEdit
        Me.alts_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.alts_pt_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.alts_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.alts_ref.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.alts_bom_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.alts_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.alts_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 285)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 402)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.alts_active)
        Me.lci_master.Controls.Add(Me.alts_ref)
        Me.lci_master.Controls.Add(Me.alts_bom_id)
        Me.lci_master.Controls.Add(Me.alts_en_id)
        Me.lci_master.Controls.Add(Me.alts_pt_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 285)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'alts_active
        '
        Me.alts_active.Location = New System.Drawing.Point(12, 108)
        Me.alts_active.Name = "alts_active"
        Me.alts_active.Properties.Caption = "Is Active"
        Me.alts_active.Size = New System.Drawing.Size(519, 19)
        Me.alts_active.StyleController = Me.lci_master
        Me.alts_active.TabIndex = 8
        '
        'alts_ref
        '
        Me.alts_ref.Location = New System.Drawing.Point(68, 84)
        Me.alts_ref.Name = "alts_ref"
        Me.alts_ref.Size = New System.Drawing.Size(463, 20)
        Me.alts_ref.StyleController = Me.lci_master
        Me.alts_ref.TabIndex = 7
        '
        'alts_bom_id
        '
        Me.alts_bom_id.Location = New System.Drawing.Point(68, 60)
        Me.alts_bom_id.Name = "alts_bom_id"
        Me.alts_bom_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.alts_bom_id.Size = New System.Drawing.Size(463, 20)
        Me.alts_bom_id.StyleController = Me.lci_master
        Me.alts_bom_id.TabIndex = 6
        '
        'alts_en_id
        '
        Me.alts_en_id.Location = New System.Drawing.Point(68, 12)
        Me.alts_en_id.Name = "alts_en_id"
        Me.alts_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.alts_en_id.Size = New System.Drawing.Size(201, 20)
        Me.alts_en_id.StyleController = Me.lci_master
        Me.alts_en_id.TabIndex = 5
        '
        'alts_pt_id
        '
        Me.alts_pt_id.Location = New System.Drawing.Point(68, 36)
        Me.alts_pt_id.Name = "alts_pt_id"
        Me.alts_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.alts_pt_id.Size = New System.Drawing.Size(463, 20)
        Me.alts_pt_id.StyleController = Me.lci_master
        Me.alts_pt_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 285)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.alts_pt_id
        Me.LayoutControlItem1.CustomizationFormText = "PT ID"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Part Desc."
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(52, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.alts_en_id
        Me.LayoutControlItem2.CustomizationFormText = "Entity ID"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem2.Text = "Entity"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(52, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.alts_bom_id
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "BOM Desc."
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(52, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.alts_ref
        Me.LayoutControlItem4.CustomizationFormText = "Reference"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem4.Text = "Reference"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(52, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.alts_active
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(523, 169)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(261, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(262, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FAlts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FAlts"
        Me.Text = "Alts"
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.alts_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.alts_ref.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.alts_bom_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.alts_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.alts_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents alts_bom_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents alts_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents alts_pt_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents alts_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents alts_ref As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
