<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLayoutManualInsert
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.te_prjg_name = New DevExpress.XtraEditors.TextEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.be_prjg_code = New DevExpress.XtraEditors.ButtonEdit
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.tl_layout = New DevExpress.XtraTreeList.TreeList
        Me.TreeListColumn4 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn5 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.prjl_isnull = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.prjl_data_type = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.TreeListColumn3 = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.sb_delete = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl
        Me.prjl_parent_id = New DevExpress.XtraEditors.LookUpEdit
        Me.sb_add = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl
        Me.prjl_tranc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prjl_isnull1 = New DevExpress.XtraEditors.CheckEdit
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl
        Me.prjl_data_type1 = New DevExpress.XtraEditors.LookUpEdit
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        Me.prjl_desc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prjl_is_root1 = New DevExpress.XtraEditors.CheckEdit
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.prjl_seq = New DevExpress.XtraEditors.SpinEdit
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.te_prjg_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_prjg_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.tl_layout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_parent_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_tranc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_isnull1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_data_type1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_desc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_is_root1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prjl_seq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_data.Size = New System.Drawing.Size(747, 446)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.GroupControl1)
        Me.scc_master.Panel1.ShowCaption = True
        Me.scc_master.Size = New System.Drawing.Size(749, 524)
        Me.scc_master.SplitterPosition = 51
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(749, 467)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(543, 285)
        '
        'prjl_is_root
        '
        Me.prjl_is_root.Caption = "Root"
        Me.prjl_is_root.FieldName = "prjl_is_root"
        Me.prjl_is_root.Name = "prjl_is_root"
        Me.prjl_is_root.OptionsColumn.AllowEdit = False
        Me.prjl_is_root.OptionsColumn.ReadOnly = True
        Me.prjl_is_root.Visible = True
        Me.prjl_is_root.VisibleIndex = 4
        Me.prjl_is_root.Width = 43
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
        Me.GroupControl1.Size = New System.Drawing.Size(749, 51)
        Me.GroupControl1.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(254, 29)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Group Name :"
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
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(5, 5)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.tl_layout)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.sb_delete)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LabelControl6)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.prjl_parent_id)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.sb_add)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LabelControl7)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.prjl_tranc_id)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.prjl_isnull1)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LabelControl5)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.prjl_data_type1)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LabelControl4)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.prjl_desc_id)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.prjl_is_root1)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LabelControl3)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.prjl_seq)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(737, 436)
        Me.SplitContainerControl1.SplitterPosition = 307
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'tl_layout
        '
        Me.tl_layout.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn4, Me.TreeListColumn5, Me.TreeListColumn1, Me.TreeListColumn2, Me.prjl_is_root, Me.prjl_isnull, Me.prjl_data_type, Me.TreeListColumn3})
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
        StyleFormatCondition2.Value1 = "Y"
        Me.tl_layout.FormatConditions.AddRange(New DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition() {StyleFormatCondition1, StyleFormatCondition2})
        Me.tl_layout.KeyFieldName = "prjl_id"
        Me.tl_layout.Location = New System.Drawing.Point(0, 0)
        Me.tl_layout.Name = "tl_layout"
        Me.tl_layout.OptionsBehavior.DragNodes = True
        Me.tl_layout.OptionsBehavior.EnableFiltering = True
        Me.tl_layout.OptionsBehavior.PopulateServiceColumns = True
        Me.tl_layout.OptionsView.AutoWidth = False
        Me.tl_layout.OptionsView.ShowPreview = True
        Me.tl_layout.ParentFieldName = "prjl_parent_id"
        Me.tl_layout.Size = New System.Drawing.Size(737, 307)
        Me.tl_layout.TabIndex = 2
        '
        'TreeListColumn4
        '
        Me.TreeListColumn4.Caption = "prjl_id"
        Me.TreeListColumn4.FieldName = "prjl_id"
        Me.TreeListColumn4.Name = "TreeListColumn4"
        Me.TreeListColumn4.OptionsColumn.AllowEdit = False
        Me.TreeListColumn4.OptionsColumn.ReadOnly = True
        '
        'TreeListColumn5
        '
        Me.TreeListColumn5.Caption = "prjl_parent_id"
        Me.TreeListColumn5.FieldName = "prjl_parent_id"
        Me.TreeListColumn5.Name = "TreeListColumn5"
        Me.TreeListColumn5.OptionsColumn.AllowEdit = False
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
        Me.TreeListColumn1.OptionsColumn.AllowEdit = False
        Me.TreeListColumn1.OptionsColumn.ReadOnly = True
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 1
        Me.TreeListColumn1.Width = 50
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Description ID"
        Me.TreeListColumn2.FieldName = "desc_id"
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.AllowEdit = False
        Me.TreeListColumn2.OptionsColumn.ReadOnly = True
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 0
        Me.TreeListColumn2.Width = 166
        '
        'prjl_isnull
        '
        Me.prjl_isnull.Caption = "IsNull"
        Me.prjl_isnull.FieldName = "prjl_isnull1"
        Me.prjl_isnull.Name = "prjl_isnull"
        Me.prjl_isnull.OptionsColumn.AllowEdit = False
        Me.prjl_isnull.OptionsColumn.ReadOnly = True
        Me.prjl_isnull.Visible = True
        Me.prjl_isnull.VisibleIndex = 3
        Me.prjl_isnull.Width = 53
        '
        'prjl_data_type
        '
        Me.prjl_data_type.Caption = "Data Type"
        Me.prjl_data_type.FieldName = "prjl_data_type"
        Me.prjl_data_type.Name = "prjl_data_type"
        Me.prjl_data_type.OptionsColumn.AllowEdit = False
        Me.prjl_data_type.OptionsColumn.ReadOnly = True
        Me.prjl_data_type.Visible = True
        Me.prjl_data_type.VisibleIndex = 2
        Me.prjl_data_type.Width = 85
        '
        'TreeListColumn3
        '
        Me.TreeListColumn3.Caption = "Value"
        Me.TreeListColumn3.FieldName = "prjl_user_value"
        Me.TreeListColumn3.Name = "TreeListColumn3"
        Me.TreeListColumn3.OptionsColumn.AllowEdit = False
        Me.TreeListColumn3.OptionsColumn.ReadOnly = True
        Me.TreeListColumn3.Visible = True
        Me.TreeListColumn3.VisibleIndex = 5
        Me.TreeListColumn3.Width = 256
        '
        'sb_delete
        '
        Me.sb_delete.Location = New System.Drawing.Point(553, 100)
        Me.sb_delete.Name = "sb_delete"
        Me.sb_delete.Size = New System.Drawing.Size(85, 23)
        Me.sb_delete.TabIndex = 15
        Me.sb_delete.Text = "Delete"
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(60, 9)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl6.TabIndex = 13
        Me.LabelControl6.Text = "Parent ID :"
        '
        'prjl_parent_id
        '
        Me.prjl_parent_id.EnterMoveNextControl = True
        Me.prjl_parent_id.Location = New System.Drawing.Point(120, 7)
        Me.prjl_parent_id.Name = "prjl_parent_id"
        Me.prjl_parent_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prjl_parent_id.Size = New System.Drawing.Size(247, 20)
        Me.prjl_parent_id.TabIndex = 14
        '
        'sb_add
        '
        Me.sb_add.Location = New System.Drawing.Point(462, 100)
        Me.sb_add.Name = "sb_add"
        Me.sb_add.Size = New System.Drawing.Size(85, 23)
        Me.sb_add.TabIndex = 12
        Me.sb_add.Text = "Add/Update"
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(12, 105)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(101, 13)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = "Transaction Column :"
        '
        'prjl_tranc_id
        '
        Me.prjl_tranc_id.EnterMoveNextControl = True
        Me.prjl_tranc_id.Location = New System.Drawing.Point(120, 103)
        Me.prjl_tranc_id.Name = "prjl_tranc_id"
        Me.prjl_tranc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prjl_tranc_id.Size = New System.Drawing.Size(247, 20)
        Me.prjl_tranc_id.TabIndex = 11
        '
        'prjl_isnull1
        '
        Me.prjl_isnull1.Location = New System.Drawing.Point(374, 81)
        Me.prjl_isnull1.Name = "prjl_isnull1"
        Me.prjl_isnull1.Properties.Caption = "IsNull"
        Me.prjl_isnull1.Size = New System.Drawing.Size(79, 19)
        Me.prjl_isnull1.TabIndex = 7
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(56, 81)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl5.TabIndex = 5
        Me.LabelControl5.Text = "Data Type :"
        '
        'prjl_data_type1
        '
        Me.prjl_data_type1.EnterMoveNextControl = True
        Me.prjl_data_type1.Location = New System.Drawing.Point(120, 79)
        Me.prjl_data_type1.Name = "prjl_data_type1"
        Me.prjl_data_type1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prjl_data_type1.Size = New System.Drawing.Size(247, 20)
        Me.prjl_data_type1.TabIndex = 6
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(39, 58)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(74, 13)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Description ID :"
        '
        'prjl_desc_id
        '
        Me.prjl_desc_id.EnterMoveNextControl = True
        Me.prjl_desc_id.Location = New System.Drawing.Point(120, 55)
        Me.prjl_desc_id.Name = "prjl_desc_id"
        Me.prjl_desc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prjl_desc_id.Properties.NullText = ""
        Me.prjl_desc_id.Size = New System.Drawing.Size(247, 20)
        Me.prjl_desc_id.TabIndex = 4
        '
        'prjl_is_root1
        '
        Me.prjl_is_root1.Location = New System.Drawing.Point(374, 9)
        Me.prjl_is_root1.Name = "prjl_is_root1"
        Me.prjl_is_root1.Properties.Caption = "IsRoot"
        Me.prjl_is_root1.Size = New System.Drawing.Size(79, 19)
        Me.prjl_is_root1.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(84, 34)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Seq. :"
        '
        'prjl_seq
        '
        Me.prjl_seq.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.prjl_seq.EnterMoveNextControl = True
        Me.prjl_seq.Location = New System.Drawing.Point(120, 31)
        Me.prjl_seq.Name = "prjl_seq"
        Me.prjl_seq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.prjl_seq.Size = New System.Drawing.Size(247, 20)
        Me.prjl_seq.TabIndex = 1
        '
        'FLayoutManualInsert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(749, 524)
        Me.Name = "FLayoutManualInsert"
        Me.Text = "Layout Manual Insert"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.te_prjg_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_prjg_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.tl_layout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_parent_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_tranc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_isnull1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_data_type1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_desc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_is_root1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prjl_seq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Public WithEvents te_prjg_name As DevExpress.XtraEditors.TextEdit
    Public WithEvents be_prjg_code As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents tl_layout As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn4 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn5 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_is_root As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_isnull As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_data_type As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents prjl_data_type1 As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents prjl_desc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prjl_is_root1 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents prjl_seq As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents prjl_tranc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prjl_isnull1 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents sb_add As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents prjl_parent_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents TreeListColumn3 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents sb_delete As DevExpress.XtraEditors.SimpleButton

End Class
