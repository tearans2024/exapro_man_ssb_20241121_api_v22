<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSalesOrderPlazaGenerate
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
        Me.StatusError = New DevExpress.XtraEditors.LabelControl
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.de_end = New DevExpress.XtraEditors.DateEdit
        Me.de_start = New DevExpress.XtraEditors.DateEdit
        Me.BtGenerate = New DevExpress.XtraEditors.SimpleButton
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_end.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_start.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_start.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel2.Controls.Add(Me.lci_master)
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.StatusError)
        Me.lci_master.Controls.Add(Me.le_entity)
        Me.lci_master.Controls.Add(Me.LabelControl2)
        Me.lci_master.Controls.Add(Me.LabelControl1)
        Me.lci_master.Controls.Add(Me.de_end)
        Me.lci_master.Controls.Add(Me.de_start)
        Me.lci_master.Controls.Add(Me.BtGenerate)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup4
        Me.lci_master.Size = New System.Drawing.Size(555, 433)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 6
        Me.lci_master.Text = "LayoutControl1"
        '
        'StatusError
        '
        Me.StatusError.Location = New System.Drawing.Point(12, 147)
        Me.StatusError.Name = "StatusError"
        Me.StatusError.Size = New System.Drawing.Size(4, 13)
        Me.StatusError.StyleController = Me.lci_master
        Me.StatusError.TabIndex = 16
        Me.StatusError.Text = "-"
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(78, 44)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity.Size = New System.Drawing.Size(197, 20)
        Me.le_entity.StyleController = Me.lci_master
        Me.le_entity.TabIndex = 15
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(57, 130)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl2.StyleController = Me.lci_master
        Me.LabelControl2.TabIndex = 14
        Me.LabelControl2.Text = "-"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 130)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl1.StyleController = Me.lci_master
        Me.LabelControl1.TabIndex = 13
        Me.LabelControl1.Text = "Status : "
        '
        'de_end
        '
        Me.de_end.EditValue = Nothing
        Me.de_end.Location = New System.Drawing.Point(333, 68)
        Me.de_end.Name = "de_end"
        Me.de_end.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.de_end.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.de_end.Size = New System.Drawing.Size(198, 20)
        Me.de_end.StyleController = Me.lci_master
        Me.de_end.TabIndex = 12
        '
        'de_start
        '
        Me.de_start.EditValue = Nothing
        Me.de_start.Location = New System.Drawing.Point(78, 68)
        Me.de_start.Name = "de_start"
        Me.de_start.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.de_start.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.de_start.Size = New System.Drawing.Size(197, 20)
        Me.de_start.StyleController = Me.lci_master
        Me.de_start.TabIndex = 11
        '
        'BtGenerate
        '
        Me.BtGenerate.Location = New System.Drawing.Point(12, 104)
        Me.BtGenerate.Name = "BtGenerate"
        Me.BtGenerate.Size = New System.Drawing.Size(531, 22)
        Me.BtGenerate.StyleController = Me.lci_master
        Me.BtGenerate.TabIndex = 10
        Me.BtGenerate.Text = "Generate"
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Root"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6, Me.LayoutControlItem3, Me.EmptySpaceItem1, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.EmptySpaceItem3, Me.LayoutControlItem7})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(555, 433)
        Me.LayoutControlGroup4.Text = "Root"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "Position"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem6, Me.EmptySpaceItem2})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(535, 92)
        Me.LayoutControlGroup6.Text = "Data"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.de_start
        Me.LayoutControlItem1.CustomizationFormText = "Start Date"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem1.Text = "Start Date"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.de_end
        Me.LayoutControlItem2.CustomizationFormText = "End Date"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(255, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem2.Text = "End Date"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(50, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.le_entity
        Me.LayoutControlItem6.CustomizationFormText = "Entity"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem6.Text = "Entity"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(50, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(255, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(256, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.BtGenerate
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 92)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(535, 26)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 152)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(535, 26)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.LabelControl1
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 118)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(45, 17)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.LabelControl2
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(45, 118)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(490, 17)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 178)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(535, 235)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.StatusError
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 135)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(535, 17)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'FSalesOrderPlazaGenerate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FSalesOrderPlazaGenerate"
        Me.Text = "Generate Sales Order Plaza"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_end.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_start.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_start.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BtGenerate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents de_end As DevExpress.XtraEditors.DateEdit
    Friend WithEvents de_start As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents StatusError As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem

End Class
