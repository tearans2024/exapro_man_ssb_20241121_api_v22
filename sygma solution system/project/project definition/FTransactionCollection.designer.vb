<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTransactionCollection
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
        Me.tranc_active = New DevExpress.XtraEditors.CheckEdit
        Me.tranc_desc = New DevExpress.XtraEditors.TextEdit
        Me.tranc_table = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.tranc_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tranc_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tranc_table.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.xtp_edit.Size = New System.Drawing.Size(553, 412)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 352)
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
        Me.lci_master.Controls.Add(Me.tranc_active)
        Me.lci_master.Controls.Add(Me.tranc_desc)
        Me.lci_master.Controls.Add(Me.tranc_table)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 352)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'tranc_active
        '
        Me.tranc_active.Location = New System.Drawing.Point(12, 60)
        Me.tranc_active.Name = "tranc_active"
        Me.tranc_active.Properties.Caption = "Is Active"
        Me.tranc_active.Size = New System.Drawing.Size(519, 19)
        Me.tranc_active.StyleController = Me.lci_master
        Me.tranc_active.TabIndex = 7
        '
        'tranc_desc
        '
        Me.tranc_desc.Location = New System.Drawing.Point(72, 36)
        Me.tranc_desc.Name = "tranc_desc"
        Me.tranc_desc.Size = New System.Drawing.Size(459, 20)
        Me.tranc_desc.StyleController = Me.lci_master
        Me.tranc_desc.TabIndex = 6
        '
        'tranc_table
        '
        Me.tranc_table.Location = New System.Drawing.Point(72, 12)
        Me.tranc_table.Name = "tranc_table"
        Me.tranc_table.Size = New System.Drawing.Size(459, 20)
        Me.tranc_table.StyleController = Me.lci_master
        Me.tranc_table.TabIndex = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 352)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.tranc_table
        Me.LayoutControlItem2.CustomizationFormText = "Code"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem2.Text = "Trans Table"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.tranc_desc
        Me.LayoutControlItem3.CustomizationFormText = "Description"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Description"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.tranc_active
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(523, 23)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 71)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 261)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FTransactionCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FTransactionCollection"
        Me.Text = "Transaction Type"
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
        CType(Me.tranc_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tranc_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tranc_table.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents tranc_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents tranc_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tranc_table As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController

End Class
