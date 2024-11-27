<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLocation
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
        Me.loc_git = New DevExpress.XtraEditors.CheckEdit
        Me.sc_ce_loc_active = New DevExpress.XtraEditors.CheckEdit
        Me.sc_le_loc_si = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_loc_is = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_loc_cat = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_loc_type = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_loc_wh = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_loc_en = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_te_loc_code = New DevExpress.XtraEditors.TextEdit
        Me.sc_te_loc_desc = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_cu_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_cu_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.Lci_entity = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_type = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_category = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_inventory = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_site = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.loc_git.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_ce_loc_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_loc_si.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_loc_is.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_loc_cat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_loc_type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_loc_wh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_loc_en.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_loc_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_loc_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_category, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_inventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_site, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 433)
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
        Me.lci_master.Controls.Add(Me.loc_git)
        Me.lci_master.Controls.Add(Me.sc_ce_loc_active)
        Me.lci_master.Controls.Add(Me.sc_le_loc_si)
        Me.lci_master.Controls.Add(Me.sc_le_loc_is)
        Me.lci_master.Controls.Add(Me.sc_le_loc_cat)
        Me.lci_master.Controls.Add(Me.sc_le_loc_type)
        Me.lci_master.Controls.Add(Me.sc_le_loc_wh)
        Me.lci_master.Controls.Add(Me.sc_le_loc_en)
        Me.lci_master.Controls.Add(Me.sc_te_loc_code)
        Me.lci_master.Controls.Add(Me.sc_te_loc_desc)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 285)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'loc_git
        '
        Me.loc_git.Location = New System.Drawing.Point(12, 227)
        Me.loc_git.Name = "loc_git"
        Me.loc_git.Properties.Caption = "Is Good In Transit"
        Me.loc_git.Size = New System.Drawing.Size(519, 19)
        Me.loc_git.StyleController = Me.lci_master
        Me.loc_git.TabIndex = 15
        '
        'sc_ce_loc_active
        '
        Me.sc_ce_loc_active.Location = New System.Drawing.Point(12, 204)
        Me.sc_ce_loc_active.Name = "sc_ce_loc_active"
        Me.sc_ce_loc_active.Properties.Caption = "Is Active"
        Me.sc_ce_loc_active.Size = New System.Drawing.Size(519, 19)
        Me.sc_ce_loc_active.StyleController = Me.lci_master
        Me.sc_ce_loc_active.TabIndex = 6
        '
        'sc_le_loc_si
        '
        Me.sc_le_loc_si.Location = New System.Drawing.Point(98, 156)
        Me.sc_le_loc_si.Name = "sc_le_loc_si"
        Me.sc_le_loc_si.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_loc_si.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_loc_si.StyleController = Me.lci_master
        Me.sc_le_loc_si.TabIndex = 14
        '
        'sc_le_loc_is
        '
        Me.sc_le_loc_is.Location = New System.Drawing.Point(98, 180)
        Me.sc_le_loc_is.Name = "sc_le_loc_is"
        Me.sc_le_loc_is.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_loc_is.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_loc_is.StyleController = Me.lci_master
        Me.sc_le_loc_is.TabIndex = 13
        '
        'sc_le_loc_cat
        '
        Me.sc_le_loc_cat.Location = New System.Drawing.Point(98, 132)
        Me.sc_le_loc_cat.Name = "sc_le_loc_cat"
        Me.sc_le_loc_cat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_loc_cat.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_loc_cat.StyleController = Me.lci_master
        Me.sc_le_loc_cat.TabIndex = 12
        '
        'sc_le_loc_type
        '
        Me.sc_le_loc_type.Location = New System.Drawing.Point(98, 108)
        Me.sc_le_loc_type.Name = "sc_le_loc_type"
        Me.sc_le_loc_type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_loc_type.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_loc_type.StyleController = Me.lci_master
        Me.sc_le_loc_type.TabIndex = 11
        '
        'sc_le_loc_wh
        '
        Me.sc_le_loc_wh.Location = New System.Drawing.Point(98, 84)
        Me.sc_le_loc_wh.Name = "sc_le_loc_wh"
        Me.sc_le_loc_wh.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_loc_wh.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_loc_wh.StyleController = Me.lci_master
        Me.sc_le_loc_wh.TabIndex = 10
        '
        'sc_le_loc_en
        '
        Me.sc_le_loc_en.Location = New System.Drawing.Point(98, 12)
        Me.sc_le_loc_en.Name = "sc_le_loc_en"
        Me.sc_le_loc_en.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_loc_en.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_loc_en.StyleController = Me.lci_master
        Me.sc_le_loc_en.TabIndex = 9
        '
        'sc_te_loc_code
        '
        Me.sc_te_loc_code.Location = New System.Drawing.Point(98, 36)
        Me.sc_te_loc_code.Name = "sc_te_loc_code"
        Me.sc_te_loc_code.Size = New System.Drawing.Size(433, 20)
        Me.sc_te_loc_code.StyleController = Me.lci_master
        Me.sc_te_loc_code.TabIndex = 4
        '
        'sc_te_loc_desc
        '
        Me.sc_te_loc_desc.Location = New System.Drawing.Point(98, 60)
        Me.sc_te_loc_desc.Name = "sc_te_loc_desc"
        Me.sc_te_loc_desc.Size = New System.Drawing.Size(433, 20)
        Me.sc_te_loc_desc.StyleController = Me.lci_master
        Me.sc_te_loc_desc.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_cu_code, Me.lci_cu_desc, Me.Lci_entity, Me.LayoutControlItem1, Me.lci_type, Me.lci_category, Me.lci_inventory, Me.lci_site, Me.LayoutControlItem5, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 285)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_cu_code
        '
        Me.lci_cu_code.Control = Me.sc_te_loc_code
        Me.lci_cu_code.CustomizationFormText = "Currency Code"
        Me.lci_cu_code.Location = New System.Drawing.Point(0, 24)
        Me.lci_cu_code.Name = "lci_cu_code"
        Me.lci_cu_code.Size = New System.Drawing.Size(523, 24)
        Me.lci_cu_code.Text = "Code"
        Me.lci_cu_code.TextSize = New System.Drawing.Size(82, 13)
        '
        'lci_cu_desc
        '
        Me.lci_cu_desc.Control = Me.sc_te_loc_desc
        Me.lci_cu_desc.CustomizationFormText = "Description"
        Me.lci_cu_desc.Location = New System.Drawing.Point(0, 48)
        Me.lci_cu_desc.Name = "lci_cu_desc"
        Me.lci_cu_desc.Size = New System.Drawing.Size(523, 24)
        Me.lci_cu_desc.Text = "Description"
        Me.lci_cu_desc.TextSize = New System.Drawing.Size(82, 13)
        '
        'Lci_entity
        '
        Me.Lci_entity.Control = Me.sc_le_loc_en
        Me.Lci_entity.CustomizationFormText = "Entity"
        Me.Lci_entity.Location = New System.Drawing.Point(0, 0)
        Me.Lci_entity.Name = "Lci_entity"
        Me.Lci_entity.Size = New System.Drawing.Size(523, 24)
        Me.Lci_entity.Text = "Entity"
        Me.Lci_entity.TextSize = New System.Drawing.Size(82, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.sc_le_loc_wh
        Me.LayoutControlItem1.CustomizationFormText = "Warehouse"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Warehouse"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(82, 13)
        '
        'lci_type
        '
        Me.lci_type.Control = Me.sc_le_loc_type
        Me.lci_type.CustomizationFormText = "Type"
        Me.lci_type.Location = New System.Drawing.Point(0, 96)
        Me.lci_type.Name = "lci_type"
        Me.lci_type.Size = New System.Drawing.Size(523, 24)
        Me.lci_type.Text = "Type"
        Me.lci_type.TextSize = New System.Drawing.Size(82, 13)
        '
        'lci_category
        '
        Me.lci_category.Control = Me.sc_le_loc_cat
        Me.lci_category.CustomizationFormText = "Category"
        Me.lci_category.Location = New System.Drawing.Point(0, 120)
        Me.lci_category.Name = "lci_category"
        Me.lci_category.Size = New System.Drawing.Size(523, 24)
        Me.lci_category.Text = "Category"
        Me.lci_category.TextSize = New System.Drawing.Size(82, 13)
        '
        'lci_inventory
        '
        Me.lci_inventory.Control = Me.sc_le_loc_is
        Me.lci_inventory.CustomizationFormText = "Inventory Status"
        Me.lci_inventory.Location = New System.Drawing.Point(0, 168)
        Me.lci_inventory.Name = "lci_inventory"
        Me.lci_inventory.Size = New System.Drawing.Size(523, 24)
        Me.lci_inventory.Text = "Inventory Status"
        Me.lci_inventory.TextSize = New System.Drawing.Size(82, 13)
        '
        'lci_site
        '
        Me.lci_site.Control = Me.sc_le_loc_si
        Me.lci_site.CustomizationFormText = "Site"
        Me.lci_site.Location = New System.Drawing.Point(0, 144)
        Me.lci_site.Name = "lci_site"
        Me.lci_site.Size = New System.Drawing.Size(523, 24)
        Me.lci_site.Text = "Site"
        Me.lci_site.TextSize = New System.Drawing.Size(82, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.sc_ce_loc_active
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 192)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(523, 23)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.loc_git
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 215)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 50)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FLocation"
        Me.Text = "Location"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.loc_git.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_ce_loc_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_loc_si.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_loc_is.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_loc_cat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_loc_type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_loc_wh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_loc_en.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_loc_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_loc_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_category, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_inventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_site, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents sc_ce_loc_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_te_loc_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_cu_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents sc_te_loc_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_cu_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents sc_le_loc_en As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Lci_entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents sc_le_loc_si As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_le_loc_is As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_le_loc_cat As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_le_loc_type As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_le_loc_wh As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_type As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_category As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_inventory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_site As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents loc_git As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem

End Class
