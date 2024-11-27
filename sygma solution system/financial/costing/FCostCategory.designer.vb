<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FCostCategory
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
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.DataLayoutControl2 = New DevExpress.XtraDataLayout.DataLayoutControl
        Me.csc_ac_id = New DevExpress.XtraEditors.LookUpEdit
        Me.csc_code = New DevExpress.XtraEditors.ComboBoxEdit
        Me.csc_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.csc_name = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem
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
        CType(Me.DataLayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DataLayoutControl2.SuspendLayout()
        CType(Me.csc_ac_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.csc_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.csc_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.csc_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Appearance.PageClient.BackColor = System.Drawing.SystemColors.Window
        Me.xtp_data.Appearance.PageClient.Options.UseBackColor = True
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DataLayoutControl2)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 402)
        Me.gc_master.TabIndex = 1
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'DataLayoutControl2
        '
        Me.DataLayoutControl2.Controls.Add(Me.csc_ac_id)
        Me.DataLayoutControl2.Controls.Add(Me.csc_code)
        Me.DataLayoutControl2.Controls.Add(Me.csc_en_id)
        Me.DataLayoutControl2.Controls.Add(Me.csc_name)
        Me.DataLayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataLayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.DataLayoutControl2.Name = "DataLayoutControl2"
        Me.DataLayoutControl2.Root = Me.LayoutControlGroup2
        Me.DataLayoutControl2.Size = New System.Drawing.Size(543, 285)
        Me.DataLayoutControl2.TabIndex = 2
        Me.DataLayoutControl2.Text = "DataLayoutControl2"
        '
        'csc_ac_id
        '
        Me.csc_ac_id.Location = New System.Drawing.Point(91, 84)
        Me.csc_ac_id.Name = "csc_ac_id"
        Me.csc_ac_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.csc_ac_id.Size = New System.Drawing.Size(186, 20)
        Me.csc_ac_id.StyleController = Me.DataLayoutControl2
        Me.csc_ac_id.TabIndex = 19
        '
        'csc_code
        '
        Me.csc_code.Location = New System.Drawing.Point(91, 36)
        Me.csc_code.Name = "csc_code"
        Me.csc_code.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.csc_code.Properties.Items.AddRange(New Object() {"CMTL", "CBDN", "COVH", "CLBR", "CSBC"})
        Me.csc_code.Size = New System.Drawing.Size(186, 20)
        Me.csc_code.StyleController = Me.DataLayoutControl2
        Me.csc_code.TabIndex = 18
        '
        'csc_en_id
        '
        Me.csc_en_id.Location = New System.Drawing.Point(91, 12)
        Me.csc_en_id.Name = "csc_en_id"
        Me.csc_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.csc_en_id.Size = New System.Drawing.Size(186, 20)
        Me.csc_en_id.StyleController = Me.DataLayoutControl2
        Me.csc_en_id.TabIndex = 17
        '
        'csc_name
        '
        Me.csc_name.Location = New System.Drawing.Point(91, 60)
        Me.csc_name.Name = "csc_name"
        Me.csc_name.Size = New System.Drawing.Size(186, 20)
        Me.csc_name.StyleController = Me.DataLayoutControl2
        Me.csc_name.TabIndex = 6
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(543, 285)
        Me.LayoutControlGroup2.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Text = "Root"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AllowDrawBackground = False
        Me.LayoutControlGroup1.CustomizationFormText = "autoGeneratedGroup0"
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.EmptySpaceItem3, Me.EmptySpaceItem4, Me.LayoutControlItem4, Me.EmptySpaceItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "autoGeneratedGroup0"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(523, 265)
        Me.LayoutControlGroup1.Text = "autoGeneratedGroup0"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.csc_name
        Me.LayoutControlItem1.CustomizationFormText = "Area Name"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(269, 24)
        Me.LayoutControlItem1.Text = "Category Name"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.csc_en_id
        Me.LayoutControlItem3.CustomizationFormText = "Entity"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(269, 24)
        Me.LayoutControlItem3.Text = "Entity"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(75, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.csc_code
        Me.LayoutControlItem2.CustomizationFormText = "Category Code"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(269, 24)
        Me.LayoutControlItem2.Text = "Category Code"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(75, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 96)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 169)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(269, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(254, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(269, 24)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(254, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(269, 48)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(254, 24)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.csc_ac_id
        Me.LayoutControlItem4.CustomizationFormText = "Account "
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(269, 24)
        Me.LayoutControlItem4.Text = "Account "
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(75, 13)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(269, 72)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(254, 24)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'FCostCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FCostCategory"
        Me.Text = "Cost Category"
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
        CType(Me.DataLayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DataLayoutControl2.ResumeLayout(False)
        CType(Me.csc_ac_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.csc_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.csc_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.csc_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DataLayoutControl2 As DevExpress.XtraDataLayout.DataLayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents csc_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents csc_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents csc_code As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents csc_ac_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem

End Class
