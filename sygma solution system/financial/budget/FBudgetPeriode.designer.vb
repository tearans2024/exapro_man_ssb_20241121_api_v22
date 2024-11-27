<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FBudgetPeriode
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
        Me.bdgtp_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.bdgtp_end_date = New DevExpress.XtraEditors.DateEdit
        Me.bdgtp_start_date = New DevExpress.XtraEditors.DateEdit
        Me.bdgtp_code = New DevExpress.XtraEditors.TextEdit
        Me.bdgtp_remarks = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_cu_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_cu_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.bdgtp_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtp_end_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtp_end_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtp_start_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtp_start_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtp_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtp_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(736, 422)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(738, 443)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(738, 443)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(736, 422)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(726, 362)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(726, 412)
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
        Me.lci_master.Controls.Add(Me.bdgtp_en_id)
        Me.lci_master.Controls.Add(Me.bdgtp_end_date)
        Me.lci_master.Controls.Add(Me.bdgtp_start_date)
        Me.lci_master.Controls.Add(Me.bdgtp_code)
        Me.lci_master.Controls.Add(Me.bdgtp_remarks)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(726, 362)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'bdgtp_en_id
        '
        Me.bdgtp_en_id.Location = New System.Drawing.Point(92, 24)
        Me.bdgtp_en_id.Name = "bdgtp_en_id"
        Me.bdgtp_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgtp_en_id.Size = New System.Drawing.Size(270, 20)
        Me.bdgtp_en_id.StyleController = Me.lci_master
        Me.bdgtp_en_id.TabIndex = 10
        '
        'bdgtp_end_date
        '
        Me.bdgtp_end_date.EditValue = Nothing
        Me.bdgtp_end_date.Location = New System.Drawing.Point(434, 96)
        Me.bdgtp_end_date.Name = "bdgtp_end_date"
        Me.bdgtp_end_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgtp_end_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.bdgtp_end_date.Size = New System.Drawing.Size(268, 20)
        Me.bdgtp_end_date.StyleController = Me.lci_master
        Me.bdgtp_end_date.TabIndex = 9
        '
        'bdgtp_start_date
        '
        Me.bdgtp_start_date.EditValue = Nothing
        Me.bdgtp_start_date.Location = New System.Drawing.Point(92, 96)
        Me.bdgtp_start_date.Name = "bdgtp_start_date"
        Me.bdgtp_start_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgtp_start_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.bdgtp_start_date.Size = New System.Drawing.Size(270, 20)
        Me.bdgtp_start_date.StyleController = Me.lci_master
        Me.bdgtp_start_date.TabIndex = 8
        '
        'bdgtp_code
        '
        Me.bdgtp_code.Location = New System.Drawing.Point(92, 48)
        Me.bdgtp_code.Name = "bdgtp_code"
        Me.bdgtp_code.Size = New System.Drawing.Size(270, 20)
        Me.bdgtp_code.StyleController = Me.lci_master
        Me.bdgtp_code.TabIndex = 4
        '
        'bdgtp_remarks
        '
        Me.bdgtp_remarks.Location = New System.Drawing.Point(92, 72)
        Me.bdgtp_remarks.Name = "bdgtp_remarks"
        Me.bdgtp_remarks.Size = New System.Drawing.Size(610, 20)
        Me.bdgtp_remarks.StyleController = Me.lci_master
        Me.bdgtp_remarks.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem3, Me.LayoutControlGroup6})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(726, 362)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 120)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(706, 222)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_cu_desc, Me.lci_cu_code, Me.EmptySpaceItem1, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(706, 120)
        Me.LayoutControlGroup6.Text = "LayoutControlGroup6"
        Me.LayoutControlGroup6.TextVisible = False
        '
        'lci_cu_desc
        '
        Me.lci_cu_desc.Control = Me.bdgtp_remarks
        Me.lci_cu_desc.CustomizationFormText = "Description1"
        Me.lci_cu_desc.Location = New System.Drawing.Point(0, 48)
        Me.lci_cu_desc.Name = "lci_cu_desc"
        Me.lci_cu_desc.Size = New System.Drawing.Size(682, 24)
        Me.lci_cu_desc.Text = "Remarks"
        Me.lci_cu_desc.TextSize = New System.Drawing.Size(64, 13)
        '
        'lci_cu_code
        '
        Me.lci_cu_code.Control = Me.bdgtp_code
        Me.lci_cu_code.CustomizationFormText = "Currency Code"
        Me.lci_cu_code.Location = New System.Drawing.Point(0, 24)
        Me.lci_cu_code.Name = "lci_cu_code"
        Me.lci_cu_code.Size = New System.Drawing.Size(342, 24)
        Me.lci_cu_code.Text = "Periode Code"
        Me.lci_cu_code.TextSize = New System.Drawing.Size(64, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(342, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(340, 48)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.bdgtp_start_date
        Me.LayoutControlItem1.CustomizationFormText = "Start Date"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(342, 24)
        Me.LayoutControlItem1.Text = "Start Date"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(64, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.bdgtp_end_date
        Me.LayoutControlItem2.CustomizationFormText = "End Date"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(342, 72)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(340, 24)
        Me.LayoutControlItem2.Text = "End Date"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(64, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.bdgtp_en_id
        Me.LayoutControlItem3.CustomizationFormText = "Entity"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(342, 24)
        Me.LayoutControlItem3.Text = "Entity"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(64, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FBudgetPeriode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(738, 443)
        Me.Name = "FBudgetPeriode"
        Me.Text = "Periode Budget"
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
        CType(Me.bdgtp_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtp_end_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtp_end_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtp_start_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtp_start_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtp_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtp_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents bdgtp_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_cu_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents bdgtp_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_cu_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents bdgtp_end_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents bdgtp_start_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents bdgtp_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem

End Class
