<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRollUpRoutingCost
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
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.bt_Reset = New DevExpress.XtraEditors.SimpleButton
        Me.cs_id = New DevExpress.XtraEditors.LookUpEdit
        Me.bt_generate = New DevExpress.XtraEditors.SimpleButton
        Me.si_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pt_desc1_to = New DevExpress.XtraEditors.TextEdit
        Me.pt_desc2_to = New DevExpress.XtraEditors.TextEdit
        Me.pt_desc2_from = New DevExpress.XtraEditors.TextEdit
        Me.pt_desc1_from = New DevExpress.XtraEditors.TextEdit
        Me.pt_id_to = New DevExpress.XtraEditors.ButtonEdit
        Me.pt_id_from = New DevExpress.XtraEditors.ButtonEdit
        Me.en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem12 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.cs_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pt_desc1_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pt_desc2_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pt_desc2_from.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pt_desc1_from.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pt_id_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pt_id_from.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.bt_Reset)
        Me.lci_master.Controls.Add(Me.cs_id)
        Me.lci_master.Controls.Add(Me.bt_generate)
        Me.lci_master.Controls.Add(Me.si_id)
        Me.lci_master.Controls.Add(Me.pt_desc1_to)
        Me.lci_master.Controls.Add(Me.pt_desc2_to)
        Me.lci_master.Controls.Add(Me.pt_desc2_from)
        Me.lci_master.Controls.Add(Me.pt_desc1_from)
        Me.lci_master.Controls.Add(Me.pt_id_to)
        Me.lci_master.Controls.Add(Me.pt_id_from)
        Me.lci_master.Controls.Add(Me.en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem11})
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup4
        Me.lci_master.Size = New System.Drawing.Size(555, 433)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 3
        Me.lci_master.Text = "LayoutControl1"
        '
        'bt_Reset
        '
        Me.bt_Reset.Location = New System.Drawing.Point(280, 164)
        Me.bt_Reset.Name = "bt_Reset"
        Me.bt_Reset.Size = New System.Drawing.Size(251, 22)
        Me.bt_Reset.StyleController = Me.lci_master
        Me.bt_Reset.TabIndex = 21
        Me.bt_Reset.Text = "Reset"
        '
        'cs_id
        '
        Me.cs_id.Location = New System.Drawing.Point(371, 68)
        Me.cs_id.Name = "cs_id"
        Me.cs_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cs_id.Size = New System.Drawing.Size(160, 20)
        Me.cs_id.StyleController = Me.lci_master
        Me.cs_id.TabIndex = 20
        '
        'bt_generate
        '
        Me.bt_generate.Location = New System.Drawing.Point(24, 164)
        Me.bt_generate.Name = "bt_generate"
        Me.bt_generate.Size = New System.Drawing.Size(252, 22)
        Me.bt_generate.StyleController = Me.lci_master
        Me.bt_generate.TabIndex = 19
        Me.bt_generate.Text = "Rollup"
        '
        'si_id
        '
        Me.si_id.Location = New System.Drawing.Point(115, 68)
        Me.si_id.Name = "si_id"
        Me.si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.si_id.Size = New System.Drawing.Size(161, 20)
        Me.si_id.StyleController = Me.lci_master
        Me.si_id.TabIndex = 18
        '
        'pt_desc1_to
        '
        Me.pt_desc1_to.Location = New System.Drawing.Point(371, 116)
        Me.pt_desc1_to.Name = "pt_desc1_to"
        Me.pt_desc1_to.Size = New System.Drawing.Size(160, 20)
        Me.pt_desc1_to.StyleController = Me.lci_master
        Me.pt_desc1_to.TabIndex = 17
        '
        'pt_desc2_to
        '
        Me.pt_desc2_to.Location = New System.Drawing.Point(371, 140)
        Me.pt_desc2_to.Name = "pt_desc2_to"
        Me.pt_desc2_to.Size = New System.Drawing.Size(160, 20)
        Me.pt_desc2_to.StyleController = Me.lci_master
        Me.pt_desc2_to.TabIndex = 16
        '
        'pt_desc2_from
        '
        Me.pt_desc2_from.Location = New System.Drawing.Point(115, 140)
        Me.pt_desc2_from.Name = "pt_desc2_from"
        Me.pt_desc2_from.Size = New System.Drawing.Size(161, 20)
        Me.pt_desc2_from.StyleController = Me.lci_master
        Me.pt_desc2_from.TabIndex = 15
        '
        'pt_desc1_from
        '
        Me.pt_desc1_from.Location = New System.Drawing.Point(115, 116)
        Me.pt_desc1_from.Name = "pt_desc1_from"
        Me.pt_desc1_from.Size = New System.Drawing.Size(161, 20)
        Me.pt_desc1_from.StyleController = Me.lci_master
        Me.pt_desc1_from.TabIndex = 14
        '
        'pt_id_to
        '
        Me.pt_id_to.Location = New System.Drawing.Point(371, 92)
        Me.pt_id_to.Name = "pt_id_to"
        Me.pt_id_to.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pt_id_to.Properties.ReadOnly = True
        Me.pt_id_to.Size = New System.Drawing.Size(160, 20)
        Me.pt_id_to.StyleController = Me.lci_master
        Me.pt_id_to.TabIndex = 12
        '
        'pt_id_from
        '
        Me.pt_id_from.Location = New System.Drawing.Point(115, 92)
        Me.pt_id_from.Name = "pt_id_from"
        Me.pt_id_from.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pt_id_from.Properties.ReadOnly = True
        Me.pt_id_from.Size = New System.Drawing.Size(161, 20)
        Me.pt_id_from.StyleController = Me.lci_master
        Me.pt_id_from.TabIndex = 11
        '
        'en_id
        '
        Me.en_id.Location = New System.Drawing.Point(115, 44)
        Me.en_id.Name = "en_id"
        Me.en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.en_id.Size = New System.Drawing.Size(161, 20)
        Me.en_id.StyleController = Me.lci_master
        Me.en_id.TabIndex = 7
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Root"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6, Me.EmptySpaceItem2})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(555, 433)
        Me.LayoutControlGroup4.Text = "Root"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "Position"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem2, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem10, Me.EmptySpaceItem12, Me.LayoutControlItem3, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.EmptySpaceItem1})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(535, 190)
        Me.LayoutControlGroup6.Text = "Position"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.pt_id_from
        Me.LayoutControlItem1.CustomizationFormText = "Part Number From"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem1.Text = "Part Number From"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.pt_desc2_to
        Me.LayoutControlItem6.CustomizationFormText = "PT Desc2"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(256, 96)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem6.Text = "PT Desc2"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.pt_desc1_to
        Me.LayoutControlItem7.CustomizationFormText = "PT Desc1"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(256, 72)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem7.Text = "PT Desc1"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.pt_id_to
        Me.LayoutControlItem2.CustomizationFormText = "Partnumber To"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(256, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem2.Text = "Part Number To"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.pt_desc1_from
        Me.LayoutControlItem4.CustomizationFormText = "PT Desc1"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem4.Text = "PT Desc1"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.pt_desc2_from
        Me.LayoutControlItem5.CustomizationFormText = "PT Desc2"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem5.Text = "PT Desc2"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.en_id
        Me.LayoutControlItem10.CustomizationFormText = "Entity"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem10.Text = "Entity"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(87, 13)
        '
        'EmptySpaceItem12
        '
        Me.EmptySpaceItem12.CustomizationFormText = "EmptySpaceItem12"
        Me.EmptySpaceItem12.Location = New System.Drawing.Point(256, 0)
        Me.EmptySpaceItem12.Name = "EmptySpaceItem12"
        Me.EmptySpaceItem12.Size = New System.Drawing.Size(255, 24)
        Me.EmptySpaceItem12.Text = "EmptySpaceItem12"
        Me.EmptySpaceItem12.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.si_id
        Me.LayoutControlItem3.CustomizationFormText = "Site"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem3.Text = "Site"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.bt_generate
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(256, 26)
        Me.LayoutControlItem8.Text = "LayoutControlItem8"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextToControlDistance = 0
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.cs_id
        Me.LayoutControlItem9.CustomizationFormText = "Cost Set"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(256, 24)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem9.Text = "Cost Set"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.bt_Reset
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(256, 120)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(255, 26)
        Me.LayoutControlItem11.Text = "LayoutControlItem11"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextToControlDistance = 0
        Me.LayoutControlItem11.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 190)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(535, 223)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(256, 120)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(255, 26)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FRollUpRoutingCost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Controls.Add(Me.lci_master)
        Me.Name = "FRollUpRoutingCost"
        Me.Text = "Routing Cost Rollup"
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.Controls.SetChildIndex(Me.lci_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.cs_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pt_desc1_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pt_desc2_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pt_desc2_from.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pt_desc1_from.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pt_id_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pt_id_from.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Public WithEvents en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem12 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents pt_id_to As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents pt_id_from As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents pt_desc1_to As DevExpress.XtraEditors.TextEdit
    Public WithEvents pt_desc2_to As DevExpress.XtraEditors.TextEdit
    Public WithEvents pt_desc2_from As DevExpress.XtraEditors.TextEdit
    Public WithEvents pt_desc1_from As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bt_generate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cs_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents bt_Reset As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
