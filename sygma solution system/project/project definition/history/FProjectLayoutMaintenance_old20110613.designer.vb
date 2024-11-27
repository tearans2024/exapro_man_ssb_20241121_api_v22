<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FProjectLayoutMaintenance
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
        Dim StyleFormatCondition1 As DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition = New DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition
        Dim StyleFormatCondition2 As DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition = New DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition
        Me.prjl_is_root = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.is_allow = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.DataLayoutControl1 = New DevExpress.XtraDataLayout.DataLayoutControl
        Me.prjl_user_value = New DevExpress.XtraEditors.TextEdit
        Me.prjl_desc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.menu_idSpinEdit = New DevExpress.XtraEditors.SpinEdit
        Me.ItemFormenu_id = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator
        Me.tl_layout = New DevExpress.XtraTreeList.TreeList
        Me.TreeListColumn4 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn5 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.prjl_user_value1 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn3 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn6 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.prjl_isnull = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.prjl_data_type = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.te_prjg_name = New DevExpress.XtraEditors.TextEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.be_prjg_code = New DevExpress.XtraEditors.ButtonEdit
        Me.xtp_project = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DataLayoutControl1.SuspendLayout()
        CType(Me.prjl_user_value.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_desc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.menu_idSpinEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemFormenu_id, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tl_layout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.te_prjg_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_prjg_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_project.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.tl_layout)
        Me.xtp_data.Size = New System.Drawing.Size(726, 390)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.GroupControl1)
        Me.scc_master.Panel1.ShowCaption = True
        Me.scc_master.Size = New System.Drawing.Size(728, 468)
        Me.scc_master.SplitterPosition = 51
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(728, 411)
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_project})
        Me.xtc_master.Controls.SetChildIndex(Me.xtp_project, 0)
        Me.xtc_master.Controls.SetChildIndex(Me.xtp_edit, 0)
        Me.xtc_master.Controls.SetChildIndex(Me.xtp_data, 0)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(726, 390)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DataLayoutControl1)
        Me.Panel1.Size = New System.Drawing.Size(716, 330)
        '
        'prjl_is_root
        '
        Me.prjl_is_root.Caption = "prjl_is_root"
        Me.prjl_is_root.FieldName = "prjl_is_root"
        Me.prjl_is_root.Name = "prjl_is_root"
        Me.prjl_is_root.OptionsColumn.ReadOnly = True
        '
        'is_allow
        '
        Me.is_allow.Caption = "is_allow"
        Me.is_allow.FieldName = "is_allow"
        Me.is_allow.Name = "is_allow"
        Me.is_allow.OptionsColumn.ReadOnly = True
        '
        'DataLayoutControl1
        '
        Me.DataLayoutControl1.Controls.Add(Me.prjl_user_value)
        Me.DataLayoutControl1.Controls.Add(Me.prjl_desc_id)
        Me.DataLayoutControl1.Controls.Add(Me.menu_idSpinEdit)
        Me.DataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataLayoutControl1.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.ItemFormenu_id})
        Me.DataLayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.DataLayoutControl1.Name = "DataLayoutControl1"
        Me.DataLayoutControl1.Root = Me.LayoutControlGroup1
        Me.DataLayoutControl1.Size = New System.Drawing.Size(716, 330)
        Me.DataLayoutControl1.TabIndex = 0
        Me.DataLayoutControl1.Text = "DataLayoutControl1"
        '
        'prjl_user_value
        '
        Me.prjl_user_value.EnterMoveNextControl = True
        Me.prjl_user_value.Location = New System.Drawing.Point(83, 46)
        Me.prjl_user_value.Name = "prjl_user_value"
        Me.prjl_user_value.Properties.DisplayFormat.FormatString = "n2"
        Me.prjl_user_value.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.prjl_user_value.Properties.EditFormat.FormatString = "n2"
        Me.prjl_user_value.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.prjl_user_value.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.prjl_user_value.Size = New System.Drawing.Size(621, 20)
        Me.prjl_user_value.StyleController = Me.DataLayoutControl1
        Me.prjl_user_value.TabIndex = 14
        '
        'prjl_desc_id
        '
        Me.prjl_desc_id.EnterMoveNextControl = True
        Me.prjl_desc_id.Location = New System.Drawing.Point(83, 22)
        Me.prjl_desc_id.Name = "prjl_desc_id"
        Me.prjl_desc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prjl_desc_id.Properties.NullText = ""
        Me.prjl_desc_id.Size = New System.Drawing.Size(621, 20)
        Me.prjl_desc_id.StyleController = Me.DataLayoutControl1
        Me.prjl_desc_id.TabIndex = 13
        '
        'menu_idSpinEdit
        '
        Me.menu_idSpinEdit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.menu_idSpinEdit.Location = New System.Drawing.Point(94, 12)
        Me.menu_idSpinEdit.Name = "menu_idSpinEdit"
        Me.menu_idSpinEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.menu_idSpinEdit.Size = New System.Drawing.Size(649, 20)
        Me.menu_idSpinEdit.StyleController = Me.DataLayoutControl1
        Me.menu_idSpinEdit.TabIndex = 4
        '
        'ItemFormenu_id
        '
        Me.ItemFormenu_id.Control = Me.menu_idSpinEdit
        Me.ItemFormenu_id.CustomizationFormText = "menu_id"
        Me.ItemFormenu_id.Location = New System.Drawing.Point(0, 0)
        Me.ItemFormenu_id.Name = "ItemFormenu_id"
        Me.ItemFormenu_id.Size = New System.Drawing.Size(735, 24)
        Me.ItemFormenu_id.Text = "menu_id"
        Me.ItemFormenu_id.TextSize = New System.Drawing.Size(50, 20)
        Me.ItemFormenu_id.TextToControlDistance = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.EmptySpaceItem1, Me.SimpleSeparator1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(716, 330)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AllowDrawBackground = False
        Me.LayoutControlGroup2.CustomizationFormText = "autoGeneratedGroup0"
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem2, Me.LayoutControlItem2, Me.LayoutControlItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "autoGeneratedGroup0"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(696, 58)
        Me.LayoutControlGroup2.Text = "autoGeneratedGroup0"
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem2.MaxSize = New System.Drawing.Size(0, 10)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(10, 10)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(696, 10)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.prjl_desc_id
        Me.LayoutControlItem2.CustomizationFormText = "Description ID"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 10)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(696, 24)
        Me.LayoutControlItem2.Text = "Description ID"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.prjl_user_value
        Me.LayoutControlItem1.CustomizationFormText = "Value"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 34)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(696, 24)
        Me.LayoutControlItem1.Text = "Value"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(67, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 60)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(696, 250)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'SimpleSeparator1
        '
        Me.SimpleSeparator1.CustomizationFormText = "SimpleSeparator1"
        Me.SimpleSeparator1.Location = New System.Drawing.Point(0, 58)
        Me.SimpleSeparator1.Name = "SimpleSeparator1"
        Me.SimpleSeparator1.Size = New System.Drawing.Size(696, 2)
        Me.SimpleSeparator1.Text = "SimpleSeparator1"
        '
        'tl_layout
        '
        Me.tl_layout.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn4, Me.TreeListColumn5, Me.TreeListColumn1, Me.TreeListColumn2, Me.prjl_user_value1, Me.TreeListColumn3, Me.TreeListColumn6, Me.prjl_is_root, Me.prjl_isnull, Me.prjl_data_type, Me.is_allow})
        Me.tl_layout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tl_layout.DragExpandDelay = 800
        StyleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        StyleFormatCondition1.Appearance.Options.UseBackColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Column = Me.prjl_is_root
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal
        StyleFormatCondition1.Value1 = "Y"
        StyleFormatCondition2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        StyleFormatCondition2.Appearance.Options.UseBackColor = True
        StyleFormatCondition2.ApplyToRow = True
        StyleFormatCondition2.Column = Me.is_allow
        StyleFormatCondition2.Value1 = "Y"
        Me.tl_layout.FormatConditions.AddRange(New DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition() {StyleFormatCondition1, StyleFormatCondition2})
        Me.tl_layout.KeyFieldName = "prjl_id"
        Me.tl_layout.Location = New System.Drawing.Point(5, 5)
        Me.tl_layout.Name = "tl_layout"
        Me.tl_layout.OptionsBehavior.AllowIncrementalSearch = True
        Me.tl_layout.OptionsBehavior.DragNodes = True
        Me.tl_layout.OptionsBehavior.EnableFiltering = True
        Me.tl_layout.OptionsBehavior.PopulateServiceColumns = True
        Me.tl_layout.OptionsSelection.MultiSelect = True
        Me.tl_layout.OptionsView.AutoWidth = False
        Me.tl_layout.OptionsView.ShowPreview = True
        Me.tl_layout.ParentFieldName = "prjl_parent_id"
        Me.tl_layout.Size = New System.Drawing.Size(716, 380)
        Me.tl_layout.TabIndex = 1
        '
        'TreeListColumn4
        '
        Me.TreeListColumn4.Caption = "prjl_id"
        Me.TreeListColumn4.FieldName = "prjl_id"
        Me.TreeListColumn4.Name = "TreeListColumn4"
        Me.TreeListColumn4.OptionsColumn.ReadOnly = True
        '
        'TreeListColumn5
        '
        Me.TreeListColumn5.Caption = "prjl_parent_id"
        Me.TreeListColumn5.FieldName = "prjl_parent_id"
        Me.TreeListColumn5.Name = "TreeListColumn5"
        Me.TreeListColumn5.OptionsColumn.ReadOnly = True
        Me.TreeListColumn5.Width = 124
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TreeListColumn1.AppearanceCell.Options.UseBackColor = True
        Me.TreeListColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.TreeListColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TreeListColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.TreeListColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TreeListColumn1.Caption = "Seq"
        Me.TreeListColumn1.FieldName = "prjl_seq"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.ReadOnly = True
        Me.TreeListColumn1.Width = 50
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Description ID"
        Me.TreeListColumn2.FieldName = "desc_id"
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.ReadOnly = True
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 0
        Me.TreeListColumn2.Width = 247
        '
        'prjl_user_value1
        '
        Me.prjl_user_value1.Caption = "Value"
        Me.prjl_user_value1.FieldName = "prjl_user_value"
        Me.prjl_user_value1.Name = "prjl_user_value1"
        Me.prjl_user_value1.OptionsColumn.ReadOnly = True
        Me.prjl_user_value1.Visible = True
        Me.prjl_user_value1.VisibleIndex = 1
        Me.prjl_user_value1.Width = 205
        '
        'TreeListColumn3
        '
        Me.TreeListColumn3.Caption = "Update By"
        Me.TreeListColumn3.FieldName = "prjl_upd_by"
        Me.TreeListColumn3.Name = "TreeListColumn3"
        Me.TreeListColumn3.Visible = True
        Me.TreeListColumn3.VisibleIndex = 2
        Me.TreeListColumn3.Width = 108
        '
        'TreeListColumn6
        '
        Me.TreeListColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.TreeListColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TreeListColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.TreeListColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TreeListColumn6.Caption = "Update Date"
        Me.TreeListColumn6.FieldName = "prjl_upd_date"
        Me.TreeListColumn6.Name = "TreeListColumn6"
        Me.TreeListColumn6.Visible = True
        Me.TreeListColumn6.VisibleIndex = 3
        Me.TreeListColumn6.Width = 125
        '
        'prjl_isnull
        '
        Me.prjl_isnull.Caption = "prjl_isnull"
        Me.prjl_isnull.FieldName = "prjl_isnull"
        Me.prjl_isnull.Name = "prjl_isnull"
        Me.prjl_isnull.OptionsColumn.ReadOnly = True
        '
        'prjl_data_type
        '
        Me.prjl_data_type.Caption = "prjl_data_type"
        Me.prjl_data_type.FieldName = "prjl_data_type"
        Me.prjl_data_type.Name = "prjl_data_type"
        Me.prjl_data_type.OptionsColumn.ReadOnly = True
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.te_prjg_name)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.be_prjg_code)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(728, 51)
        Me.GroupControl1.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(254, 29)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Description :"
        '
        'te_prjg_name
        '
        Me.te_prjg_name.Location = New System.Drawing.Point(324, 26)
        Me.te_prjg_name.Name = "te_prjg_name"
        Me.te_prjg_name.Size = New System.Drawing.Size(160, 20)
        Me.te_prjg_name.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(18, 29)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Group Code :"
        '
        'be_prjg_code
        '
        Me.be_prjg_code.Location = New System.Drawing.Point(85, 26)
        Me.be_prjg_code.Name = "be_prjg_code"
        Me.be_prjg_code.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_prjg_code.Size = New System.Drawing.Size(146, 20)
        Me.be_prjg_code.TabIndex = 3
        '
        'xtp_project
        '
        Me.xtp_project.Controls.Add(Me.gc_detail)
        Me.xtp_project.Name = "xtp_project"
        Me.xtp_project.Size = New System.Drawing.Size(553, 345)
        Me.xtp_project.Text = "Detail Project"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(553, 345)
        Me.gc_detail.TabIndex = 1
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_detail.OptionsView.ShowGroupPanel = False
        '
        'FProjectLayoutMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(728, 468)
        Me.Name = "FProjectLayoutMaintenance"
        Me.Text = "Layout Maintenance"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DataLayoutControl1.ResumeLayout(False)
        CType(Me.prjl_user_value.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_desc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.menu_idSpinEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemFormenu_id, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimpleSeparator1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tl_layout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.te_prjg_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_prjg_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_project.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataLayoutControl1 As DevExpress.XtraDataLayout.DataLayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents menu_idSpinEdit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents ItemFormenu_id As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator
    Friend WithEvents tl_layout As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents prjl_desc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prjl_user_value1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Public WithEvents te_prjg_name As DevExpress.XtraEditors.TextEdit
    Public WithEvents be_prjg_code As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents TreeListColumn4 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn5 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_is_root As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_isnull As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_data_type As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_user_value As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents is_allow As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtp_project As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TreeListColumn3 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn6 As DevExpress.XtraTreeList.Columns.TreeListColumn

End Class
