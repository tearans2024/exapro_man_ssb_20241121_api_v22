<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMonthEnd
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
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.CeCalcOpen = New DevExpress.XtraEditors.CheckEdit
        Me.btRecountGLBal = New DevExpress.XtraEditors.SimpleButton
        Me.BtPraClosing = New DevExpress.XtraEditors.SimpleButton
        Me.ce_all_child = New DevExpress.XtraEditors.CheckEdit
        Me.le_glperiode = New DevExpress.XtraEditors.LookUpEdit
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.sb_close = New DevExpress.XtraEditors.SimpleButton
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.CeCalcOpen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_all_child.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_glperiode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel2.Controls.Add(Me.lci_master)
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.CeCalcOpen)
        Me.lci_master.Controls.Add(Me.btRecountGLBal)
        Me.lci_master.Controls.Add(Me.BtPraClosing)
        Me.lci_master.Controls.Add(Me.ce_all_child)
        Me.lci_master.Controls.Add(Me.le_glperiode)
        Me.lci_master.Controls.Add(Me.le_entity)
        Me.lci_master.Controls.Add(Me.sb_close)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(555, 433)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'CeCalcOpen
        '
        Me.CeCalcOpen.Location = New System.Drawing.Point(279, 118)
        Me.CeCalcOpen.Name = "CeCalcOpen"
        Me.CeCalcOpen.Properties.Caption = "Calculate Opening"
        Me.CeCalcOpen.Size = New System.Drawing.Size(124, 19)
        Me.CeCalcOpen.StyleController = Me.lci_master
        Me.CeCalcOpen.TabIndex = 10
        '
        'btRecountGLBal
        '
        Me.btRecountGLBal.Location = New System.Drawing.Point(24, 118)
        Me.btRecountGLBal.Name = "btRecountGLBal"
        Me.btRecountGLBal.Size = New System.Drawing.Size(251, 22)
        Me.btRecountGLBal.StyleController = Me.lci_master
        Me.btRecountGLBal.TabIndex = 9
        Me.btRecountGLBal.Text = "Recount GL Total"
        '
        'BtPraClosing
        '
        Me.BtPraClosing.Location = New System.Drawing.Point(24, 92)
        Me.BtPraClosing.Name = "BtPraClosing"
        Me.BtPraClosing.Size = New System.Drawing.Size(251, 22)
        Me.BtPraClosing.StyleController = Me.lci_master
        Me.BtPraClosing.TabIndex = 8
        Me.BtPraClosing.Text = "Pra Closing"
        '
        'ce_all_child
        '
        Me.ce_all_child.Location = New System.Drawing.Point(279, 44)
        Me.ce_all_child.Name = "ce_all_child"
        Me.ce_all_child.Properties.Caption = "With All Child"
        Me.ce_all_child.Size = New System.Drawing.Size(124, 19)
        Me.ce_all_child.StyleController = Me.lci_master
        Me.ce_all_child.TabIndex = 7
        '
        'le_glperiode
        '
        Me.le_glperiode.Location = New System.Drawing.Point(79, 68)
        Me.le_glperiode.Name = "le_glperiode"
        Me.le_glperiode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_glperiode.Properties.DisplayFormat.FormatString = "d"
        Me.le_glperiode.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_glperiode.Properties.EditFormat.FormatString = "d"
        Me.le_glperiode.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_glperiode.Properties.PopupWidth = 500
        Me.le_glperiode.Size = New System.Drawing.Size(196, 20)
        Me.le_glperiode.StyleController = Me.lci_master
        Me.le_glperiode.TabIndex = 5
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(79, 44)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity.Size = New System.Drawing.Size(196, 20)
        Me.le_entity.StyleController = Me.lci_master
        Me.le_entity.TabIndex = 4
        '
        'sb_close
        '
        Me.sb_close.Location = New System.Drawing.Point(279, 92)
        Me.sb_close.Name = "sb_close"
        Me.sb_close.Size = New System.Drawing.Size(252, 22)
        Me.sb_close.StyleController = Me.lci_master
        Me.sb_close.TabIndex = 6
        Me.sb_close.Text = "Closing Transaction"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(555, 433)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 144)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(535, 269)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Transaction"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.EmptySpaceItem4, Me.EmptySpaceItem5})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(535, 144)
        Me.LayoutControlGroup2.Text = "Transaction"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.le_entity
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(51, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.le_glperiode
        Me.LayoutControlItem2.CustomizationFormText = "GL Periode"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem2.Text = "GL Periode"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(51, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.sb_close
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(255, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(256, 26)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(255, 24)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(256, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.ce_all_child
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(255, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(128, 24)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.BtPraClosing
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(255, 26)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.btRecountGLBal
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 74)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(255, 26)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.CeCalcOpen
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(255, 74)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(128, 26)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(383, 74)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(128, 26)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(383, 0)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(128, 24)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FMonthEnd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FMonthEnd"
        Me.Text = "Month End Adjustment Transaction"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.CeCalcOpen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_all_child.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_glperiode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents sb_close As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents le_glperiode As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents ce_all_child As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtPraClosing As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btRecountGLBal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CeCalcOpen As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem

End Class
