<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPartNumberSKUGenerator
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
        Me.psplan_real_cost = New DevExpress.XtraEditors.TextEdit
        Me.psplan_ttl_cost = New DevExpress.XtraEditors.TextEdit
        Me.psplan_rev = New DevExpress.XtraEditors.TextEdit
        Me.psplan_days = New DevExpress.XtraEditors.TextEdit
        Me.psplan_prod_dt = New DevExpress.XtraEditors.DateEdit
        Me.psplan_prod_duedt = New DevExpress.XtraEditors.DateEdit
        Me.psplan_wo_dt = New DevExpress.XtraEditors.DateEdit
        Me.psplan_wo = New DevExpress.XtraEditors.TextEdit
        Me.psplan_ptnr_id = New DevExpress.XtraEditors.ButtonEdit
        Me.psplan_copy = New DevExpress.XtraEditors.ButtonEdit
        Me.psplan_ratio = New DevExpress.XtraEditors.TextEdit
        Me.psplan_remarks = New DevExpress.XtraEditors.TextEdit
        Me.psplan_tran_id = New DevExpress.XtraEditors.LookUpEdit
        Me.gc_edit = New DevExpress.XtraGrid.GridControl
        Me.gv_edit = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.psplan_active = New DevExpress.XtraEditors.CheckEdit
        Me.psplan_desc = New DevExpress.XtraEditors.TextEdit
        Me.psplan_code = New DevExpress.XtraEditors.TextEdit
        Me.psplan_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_work_flow = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_wf = New DevExpress.XtraGrid.GridControl
        Me.gv_wf = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_email = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_email = New DevExpress.XtraGrid.GridControl
        Me.gv_email = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_smart_approve = New DevExpress.XtraTab.XtraTabPage
        Me.scc_smart_approve = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_smart = New DevExpress.XtraGrid.GridControl
        Me.gv_smart = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.col_select = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.be_first = New DevExpress.XtraEditors.ButtonEdit
        Me.be_to = New DevExpress.XtraEditors.ButtonEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.ce_all_data = New DevExpress.XtraEditors.CheckEdit
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.psplan_real_cost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_ttl_cost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_rev.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_days.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_prod_dt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_prod_dt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_prod_duedt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_prod_duedt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_wo_dt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_wo_dt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_wo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_ptnr_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_copy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_ratio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_tran_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.psplan_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_detail.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_work_flow.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_email.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_smart_approve.SuspendLayout()
        CType(Me.scc_smart_approve, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_smart_approve.SuspendLayout()
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_all_data.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Appearance.PageClient.BackColor = System.Drawing.SystemColors.Window
        Me.xtp_data.Appearance.PageClient.Options.UseBackColor = True
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(912, 423)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.ce_all_data)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.be_first)
        Me.scc_master.Panel1.Controls.Add(Me.be_to)
        Me.scc_master.Size = New System.Drawing.Size(914, 481)
        Me.scc_master.SplitterPosition = 31
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(912, 423)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(902, 378)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(914, 444)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(902, 413)
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
        Me.lci_master.Controls.Add(Me.psplan_real_cost)
        Me.lci_master.Controls.Add(Me.psplan_ttl_cost)
        Me.lci_master.Controls.Add(Me.psplan_rev)
        Me.lci_master.Controls.Add(Me.psplan_days)
        Me.lci_master.Controls.Add(Me.psplan_prod_dt)
        Me.lci_master.Controls.Add(Me.psplan_prod_duedt)
        Me.lci_master.Controls.Add(Me.psplan_wo_dt)
        Me.lci_master.Controls.Add(Me.psplan_wo)
        Me.lci_master.Controls.Add(Me.psplan_ptnr_id)
        Me.lci_master.Controls.Add(Me.psplan_copy)
        Me.lci_master.Controls.Add(Me.psplan_ratio)
        Me.lci_master.Controls.Add(Me.psplan_remarks)
        Me.lci_master.Controls.Add(Me.psplan_tran_id)
        Me.lci_master.Controls.Add(Me.gc_edit)
        Me.lci_master.Controls.Add(Me.psplan_active)
        Me.lci_master.Controls.Add(Me.psplan_desc)
        Me.lci_master.Controls.Add(Me.psplan_code)
        Me.lci_master.Controls.Add(Me.psplan_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(902, 378)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'psplan_real_cost
        '
        Me.psplan_real_cost.Location = New System.Drawing.Point(552, 188)
        Me.psplan_real_cost.Name = "psplan_real_cost"
        Me.psplan_real_cost.Size = New System.Drawing.Size(326, 20)
        Me.psplan_real_cost.StyleController = Me.lci_master
        Me.psplan_real_cost.TabIndex = 27
        '
        'psplan_ttl_cost
        '
        Me.psplan_ttl_cost.Location = New System.Drawing.Point(122, 188)
        Me.psplan_ttl_cost.Name = "psplan_ttl_cost"
        Me.psplan_ttl_cost.Size = New System.Drawing.Size(328, 20)
        Me.psplan_ttl_cost.StyleController = Me.lci_master
        Me.psplan_ttl_cost.TabIndex = 26
        '
        'psplan_rev
        '
        Me.psplan_rev.Location = New System.Drawing.Point(550, 164)
        Me.psplan_rev.Name = "psplan_rev"
        Me.psplan_rev.Size = New System.Drawing.Size(328, 20)
        Me.psplan_rev.StyleController = Me.lci_master
        Me.psplan_rev.TabIndex = 25
        '
        'psplan_days
        '
        Me.psplan_days.Location = New System.Drawing.Point(122, 164)
        Me.psplan_days.Name = "psplan_days"
        Me.psplan_days.Size = New System.Drawing.Size(326, 20)
        Me.psplan_days.StyleController = Me.lci_master
        Me.psplan_days.TabIndex = 24
        '
        'psplan_prod_dt
        '
        Me.psplan_prod_dt.EditValue = Nothing
        Me.psplan_prod_dt.Location = New System.Drawing.Point(122, 140)
        Me.psplan_prod_dt.Name = "psplan_prod_dt"
        Me.psplan_prod_dt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.psplan_prod_dt.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.psplan_prod_dt.Size = New System.Drawing.Size(328, 20)
        Me.psplan_prod_dt.StyleController = Me.lci_master
        Me.psplan_prod_dt.TabIndex = 23
        '
        'psplan_prod_duedt
        '
        Me.psplan_prod_duedt.EditValue = Nothing
        Me.psplan_prod_duedt.Location = New System.Drawing.Point(552, 140)
        Me.psplan_prod_duedt.Name = "psplan_prod_duedt"
        Me.psplan_prod_duedt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.psplan_prod_duedt.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.psplan_prod_duedt.Size = New System.Drawing.Size(326, 20)
        Me.psplan_prod_duedt.StyleController = Me.lci_master
        Me.psplan_prod_duedt.TabIndex = 22
        '
        'psplan_wo_dt
        '
        Me.psplan_wo_dt.EditValue = Nothing
        Me.psplan_wo_dt.Location = New System.Drawing.Point(552, 116)
        Me.psplan_wo_dt.Name = "psplan_wo_dt"
        Me.psplan_wo_dt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.psplan_wo_dt.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.psplan_wo_dt.Size = New System.Drawing.Size(326, 20)
        Me.psplan_wo_dt.StyleController = Me.lci_master
        Me.psplan_wo_dt.TabIndex = 21
        '
        'psplan_wo
        '
        Me.psplan_wo.Location = New System.Drawing.Point(122, 116)
        Me.psplan_wo.Name = "psplan_wo"
        Me.psplan_wo.Size = New System.Drawing.Size(328, 20)
        Me.psplan_wo.StyleController = Me.lci_master
        Me.psplan_wo.TabIndex = 20
        '
        'psplan_ptnr_id
        '
        Me.psplan_ptnr_id.Location = New System.Drawing.Point(122, 92)
        Me.psplan_ptnr_id.Name = "psplan_ptnr_id"
        Me.psplan_ptnr_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.psplan_ptnr_id.Size = New System.Drawing.Size(328, 20)
        Me.psplan_ptnr_id.StyleController = Me.lci_master
        Me.psplan_ptnr_id.TabIndex = 18
        '
        'psplan_copy
        '
        Me.psplan_copy.Location = New System.Drawing.Point(552, 44)
        Me.psplan_copy.Name = "psplan_copy"
        Me.psplan_copy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.psplan_copy.Size = New System.Drawing.Size(326, 20)
        Me.psplan_copy.StyleController = Me.lci_master
        Me.psplan_copy.TabIndex = 17
        '
        'psplan_ratio
        '
        Me.psplan_ratio.Location = New System.Drawing.Point(122, 236)
        Me.psplan_ratio.Name = "psplan_ratio"
        Me.psplan_ratio.Properties.Mask.EditMask = "n2"
        Me.psplan_ratio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.psplan_ratio.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.psplan_ratio.Size = New System.Drawing.Size(756, 20)
        Me.psplan_ratio.StyleController = Me.lci_master
        Me.psplan_ratio.TabIndex = 16
        '
        'psplan_remarks
        '
        Me.psplan_remarks.Location = New System.Drawing.Point(122, 212)
        Me.psplan_remarks.Name = "psplan_remarks"
        Me.psplan_remarks.Size = New System.Drawing.Size(756, 20)
        Me.psplan_remarks.StyleController = Me.lci_master
        Me.psplan_remarks.TabIndex = 14
        '
        'psplan_tran_id
        '
        Me.psplan_tran_id.Location = New System.Drawing.Point(552, 260)
        Me.psplan_tran_id.Name = "psplan_tran_id"
        Me.psplan_tran_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.psplan_tran_id.Size = New System.Drawing.Size(326, 20)
        Me.psplan_tran_id.StyleController = Me.lci_master
        Me.psplan_tran_id.TabIndex = 13
        '
        'gc_edit
        '
        Me.gc_edit.Location = New System.Drawing.Point(24, 328)
        Me.gc_edit.MainView = Me.gv_edit
        Me.gc_edit.Name = "gc_edit"
        Me.gc_edit.Size = New System.Drawing.Size(854, 26)
        Me.gc_edit.TabIndex = 10
        Me.gc_edit.UseEmbeddedNavigator = True
        Me.gc_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit})
        '
        'gv_edit
        '
        Me.gv_edit.GridControl = Me.gc_edit
        Me.gv_edit.Name = "gv_edit"
        Me.gv_edit.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit.OptionsView.ColumnAutoWidth = False
        Me.gv_edit.OptionsView.ShowFooter = True
        Me.gv_edit.OptionsView.ShowGroupPanel = False
        '
        'psplan_active
        '
        Me.psplan_active.Location = New System.Drawing.Point(24, 260)
        Me.psplan_active.Name = "psplan_active"
        Me.psplan_active.Properties.Caption = "Is Active"
        Me.psplan_active.Size = New System.Drawing.Size(426, 19)
        Me.psplan_active.StyleController = Me.lci_master
        Me.psplan_active.TabIndex = 9
        '
        'psplan_desc
        '
        Me.psplan_desc.Location = New System.Drawing.Point(552, 92)
        Me.psplan_desc.Name = "psplan_desc"
        Me.psplan_desc.Properties.MaxLength = 45
        Me.psplan_desc.Size = New System.Drawing.Size(326, 20)
        Me.psplan_desc.StyleController = Me.lci_master
        Me.psplan_desc.TabIndex = 7
        '
        'psplan_code
        '
        Me.psplan_code.Location = New System.Drawing.Point(122, 68)
        Me.psplan_code.Name = "psplan_code"
        Me.psplan_code.Size = New System.Drawing.Size(328, 20)
        Me.psplan_code.StyleController = Me.lci_master
        Me.psplan_code.TabIndex = 5
        '
        'psplan_en_id
        '
        Me.psplan_en_id.Location = New System.Drawing.Point(122, 44)
        Me.psplan_en_id.Name = "psplan_en_id"
        Me.psplan_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.psplan_en_id.Size = New System.Drawing.Size(328, 20)
        Me.psplan_en_id.StyleController = Me.lci_master
        Me.psplan_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(902, 378)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Header"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6, Me.LayoutControlItem1, Me.LayoutControlItem9, Me.LayoutControlItem13, Me.LayoutControlItem5, Me.LayoutControlItem8, Me.LayoutControlItem11, Me.LayoutControlItem17, Me.LayoutControlItem18, Me.LayoutControlItem10, Me.LayoutControlItem2, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.LayoutControlItem12, Me.LayoutControlItem14, Me.LayoutControlItem3, Me.LayoutControlItem15, Me.LayoutControlItem16})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(882, 284)
        Me.LayoutControlGroup2.Text = "Header"
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.psplan_active
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 216)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.psplan_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.psplan_tran_id
        Me.LayoutControlItem9.CustomizationFormText = "Transaction"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(430, 216)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(428, 24)
        Me.LayoutControlItem9.Text = "Routing Approval"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.psplan_copy
        Me.LayoutControlItem13.CustomizationFormText = "Copy PS"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(430, 0)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(428, 24)
        Me.LayoutControlItem13.Text = "Copy PS"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.psplan_ptnr_id
        Me.LayoutControlItem5.CustomizationFormText = "Partner Name"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem5.Text = "Partner Name"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.psplan_wo
        Me.LayoutControlItem8.CustomizationFormText = "WO Number"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem8.Text = "WO Number"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.psplan_wo_dt
        Me.LayoutControlItem11.CustomizationFormText = "WO Release"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(430, 72)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(428, 24)
        Me.LayoutControlItem11.Text = "WO Release"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.psplan_ttl_cost
        Me.LayoutControlItem17.CustomizationFormText = "Total Cost Planning"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem17.Text = "Total Cost Planning"
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.psplan_real_cost
        Me.LayoutControlItem18.CustomizationFormText = "Cost Realization"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(430, 144)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(428, 24)
        Me.LayoutControlItem18.Text = "Cost Realization"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.psplan_remarks
        Me.LayoutControlItem10.CustomizationFormText = "Remark"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 168)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(858, 24)
        Me.LayoutControlItem10.Text = "Remark"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.psplan_code
        Me.LayoutControlItem2.CustomizationFormText = "Prod. Struc. Code"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem2.Text = "PS Code"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.psplan_desc
        Me.LayoutControlItem4.CustomizationFormText = "Description"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(430, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(428, 24)
        Me.LayoutControlItem4.Text = "Description"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(94, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(430, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(428, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.psplan_ratio
        Me.LayoutControlItem12.CustomizationFormText = "Ratio"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 192)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(858, 24)
        Me.LayoutControlItem12.Text = "Ratio"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(94, 13)
        Me.LayoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.psplan_prod_dt
        Me.LayoutControlItem14.CustomizationFormText = "Production Planning"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem14.Text = "Production Planning"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.psplan_prod_duedt
        Me.LayoutControlItem3.CustomizationFormText = "Due Date"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(430, 96)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(428, 24)
        Me.LayoutControlItem3.Text = "Due Date"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.psplan_days
        Me.LayoutControlItem15.CustomizationFormText = "Days Control"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(428, 24)
        Me.LayoutControlItem15.Text = "Days Control"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.psplan_rev
        Me.LayoutControlItem16.CustomizationFormText = "Revision"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(428, 120)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(430, 24)
        Me.LayoutControlItem16.Text = "Revision"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(94, 13)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "Detail"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 284)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(882, 74)
        Me.LayoutControlGroup3.Text = "Detail"
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.gc_edit
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(858, 30)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.dp_detail})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("fa9e0fd9-3bb6-47da-8548-01e56cd81008")
        Me.dp_detail.Location = New System.Drawing.Point(0, 481)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(914, 188)
        Me.dp_detail.Size = New System.Drawing.Size(914, 188)
        Me.dp_detail.Text = "Data Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(908, 160)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_detail
        Me.xtc_detail.Size = New System.Drawing.Size(908, 160)
        Me.xtc_detail.TabIndex = 2
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail, Me.xtp_work_flow, Me.xtp_email, Me.xtp_smart_approve})
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.scc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(906, 139)
        Me.xtp_detail.Text = "Detail"
        '
        'scc_detail
        '
        Me.scc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail.Name = "scc_detail"
        Me.scc_detail.Panel1.Controls.Add(Me.gc_detail)
        Me.scc_detail.Panel1.Text = "Panel1"
        Me.scc_detail.Panel2.Text = "Panel2"
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(906, 139)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(906, 139)
        Me.gc_detail.TabIndex = 0
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsFilter.AllowFilterEditor = False
        Me.gv_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_detail.OptionsView.ShowGroupPanel = False
        '
        'xtp_work_flow
        '
        Me.xtp_work_flow.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_work_flow.Name = "xtp_work_flow"
        Me.xtp_work_flow.Size = New System.Drawing.Size(906, 139)
        Me.xtp_work_flow.Text = "Work Flow"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gc_wf)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl1.Size = New System.Drawing.Size(906, 139)
        Me.SplitContainerControl1.SplitterPosition = 467
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'gc_wf
        '
        Me.gc_wf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_wf.Location = New System.Drawing.Point(0, 0)
        Me.gc_wf.MainView = Me.gv_wf
        Me.gc_wf.Name = "gc_wf"
        Me.gc_wf.Size = New System.Drawing.Size(906, 139)
        Me.gc_wf.TabIndex = 0
        Me.gc_wf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_wf, Me.GridView3})
        '
        'gv_wf
        '
        Me.gv_wf.GridControl = Me.gc_wf
        Me.gv_wf.Name = "gv_wf"
        Me.gv_wf.OptionsView.ColumnAutoWidth = False
        Me.gv_wf.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.gc_wf
        Me.GridView3.Name = "GridView3"
        '
        'xtp_email
        '
        Me.xtp_email.Controls.Add(Me.SplitContainerControl2)
        Me.xtp_email.Name = "xtp_email"
        Me.xtp_email.PageVisible = False
        Me.xtp_email.Size = New System.Drawing.Size(906, 139)
        Me.xtp_email.Text = "For email"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.gc_email)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl2.Size = New System.Drawing.Size(906, 139)
        Me.SplitContainerControl2.SplitterPosition = 467
        Me.SplitContainerControl2.TabIndex = 2
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'gc_email
        '
        Me.gc_email.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_email.Location = New System.Drawing.Point(0, 0)
        Me.gc_email.MainView = Me.gv_email
        Me.gc_email.Name = "gc_email"
        Me.gc_email.Size = New System.Drawing.Size(906, 139)
        Me.gc_email.TabIndex = 0
        Me.gc_email.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_email, Me.GridView2})
        '
        'gv_email
        '
        Me.gv_email.GridControl = Me.gc_email
        Me.gv_email.Name = "gv_email"
        Me.gv_email.OptionsView.ColumnAutoWidth = False
        Me.gv_email.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_email
        Me.GridView2.Name = "GridView2"
        '
        'xtp_smart_approve
        '
        Me.xtp_smart_approve.Controls.Add(Me.scc_smart_approve)
        Me.xtp_smart_approve.Name = "xtp_smart_approve"
        Me.xtp_smart_approve.PageVisible = False
        Me.xtp_smart_approve.Size = New System.Drawing.Size(906, 139)
        Me.xtp_smart_approve.Text = "Smart Approve"
        '
        'scc_smart_approve
        '
        Me.scc_smart_approve.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_smart_approve.Location = New System.Drawing.Point(0, 0)
        Me.scc_smart_approve.Name = "scc_smart_approve"
        Me.scc_smart_approve.Panel1.Controls.Add(Me.gc_smart)
        Me.scc_smart_approve.Panel1.Text = "Panel1"
        Me.scc_smart_approve.Panel2.Text = "Panel2"
        Me.scc_smart_approve.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_smart_approve.Size = New System.Drawing.Size(906, 139)
        Me.scc_smart_approve.SplitterPosition = 608
        Me.scc_smart_approve.TabIndex = 2
        Me.scc_smart_approve.Text = "SplitContainerControl1"
        '
        'gc_smart
        '
        Me.gc_smart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_smart.Location = New System.Drawing.Point(0, 0)
        Me.gc_smart.MainView = Me.gv_smart
        Me.gc_smart.Name = "gc_smart"
        Me.gc_smart.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.gc_smart.Size = New System.Drawing.Size(906, 139)
        Me.gc_smart.TabIndex = 2
        Me.gc_smart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_smart, Me.GridView1})
        '
        'gv_smart
        '
        Me.gv_smart.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_select})
        Me.gv_smart.GridControl = Me.gc_smart
        Me.gv_smart.Name = "gv_smart"
        Me.gv_smart.OptionsView.ColumnAutoWidth = False
        Me.gv_smart.OptionsView.ShowGroupPanel = False
        '
        'col_select
        '
        Me.col_select.Caption = "#"
        Me.col_select.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.col_select.FieldName = "status"
        Me.col_select.Name = "col_select"
        Me.col_select.Visible = True
        Me.col_select.VisibleIndex = 0
        Me.col_select.Width = 64
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gc_smart
        Me.GridView1.Name = "GridView1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(275, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Label2"
        '
        'be_first
        '
        Me.be_first.Location = New System.Drawing.Point(262, 6)
        Me.be_first.Name = "be_first"
        Me.be_first.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_first.Size = New System.Drawing.Size(131, 20)
        Me.be_first.TabIndex = 7
        '
        'be_to
        '
        Me.be_to.Location = New System.Drawing.Point(461, 6)
        Me.be_to.Name = "be_to"
        Me.be_to.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_to.Size = New System.Drawing.Size(138, 20)
        Me.be_to.TabIndex = 8
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(191, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 9
        Me.LabelControl1.Text = "Part Number"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(433, 9)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(10, 13)
        Me.LabelControl2.TabIndex = 9
        Me.LabelControl2.Text = "to"
        '
        'ce_all_data
        '
        Me.ce_all_data.EditValue = True
        Me.ce_all_data.Location = New System.Drawing.Point(21, 7)
        Me.ce_all_data.Name = "ce_all_data"
        Me.ce_all_data.Properties.Caption = "All Data"
        Me.ce_all_data.Size = New System.Drawing.Size(84, 19)
        Me.ce_all_data.TabIndex = 10
        '
        'FPartNumberSKUGenerator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(914, 669)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FPartNumberSKUGenerator"
        Me.Text = "Product Structure"
        Me.Controls.SetChildIndex(Me.dp_detail, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.psplan_real_cost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_ttl_cost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_rev.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_days.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_prod_dt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_prod_dt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_prod_duedt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_prod_duedt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_wo_dt.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_wo_dt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_wo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_ptnr_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_copy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_ratio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_tran_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.psplan_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_detail.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_work_flow.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_email.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_smart_approve.ResumeLayout(False)
        CType(Me.scc_smart_approve, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_smart_approve.ResumeLayout(False)
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_all_data.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents psplan_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents psplan_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents psplan_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents psplan_en_id As DevExpress.XtraEditors.LookUpEdit
    Public WithEvents gc_edit As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Public WithEvents be_first As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents be_to As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ce_all_data As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_work_flow As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_wf As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_wf As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_email As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_email As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_email As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_smart_approve As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_smart_approve As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_smart As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_smart As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents col_select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents psplan_tran_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents psplan_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents psplan_ratio As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents psplan_copy As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents psplan_prod_duedt As DevExpress.XtraEditors.DateEdit
    Friend WithEvents psplan_wo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents psplan_days As DevExpress.XtraEditors.TextEdit
    Friend WithEvents psplan_prod_dt As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents psplan_real_cost As DevExpress.XtraEditors.TextEdit
    Friend WithEvents psplan_ttl_cost As DevExpress.XtraEditors.TextEdit
    Friend WithEvents psplan_rev As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents psplan_wo_dt As DevExpress.XtraEditors.DateEdit
    Public WithEvents psplan_ptnr_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
