<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryProjectSearch
    Inherits master_new.FSearch

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
        Me.pilih = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.te_search = New DevExpress.XtraEditors.TextEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.sb_retrieve = New DevExpress.XtraEditors.SimpleButton
        Me.CE_All = New DevExpress.XtraEditors.CheckEdit
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl
        Me.BtCancel = New DevExpress.XtraEditors.SimpleButton
        Me.BtOK = New DevExpress.XtraEditors.SimpleButton
        Me.sb_fill = New DevExpress.XtraEditors.SimpleButton
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_data.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_search.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CE_All.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.sb_fill)
        Me.scc_master.Panel1.Controls.Add(Me.CE_All)
        Me.scc_master.Panel1.Controls.Add(Me.sb_retrieve)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.te_search)
        Me.scc_master.Size = New System.Drawing.Size(1034, 358)
        Me.scc_master.SplitterPosition = 31
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(1034, 321)
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Controls.Add(Me.PanelControl3)
        Me.xtp_data.Size = New System.Drawing.Size(1032, 320)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gc_master.Size = New System.Drawing.Size(1022, 274)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.pilih})
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        Me.gv_master.OptionsView.ColumnAutoWidth = False
        Me.gv_master.OptionsView.ShowAutoFilterRow = True
        '
        'pilih
        '
        Me.pilih.Caption = "# (e)"
        Me.pilih.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.pilih.FieldName = "pilih"
        Me.pilih.Name = "pilih"
        Me.pilih.Visible = True
        Me.pilih.VisibleIndex = 0
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'te_search
        '
        Me.te_search.EditValue = ""
        Me.te_search.Location = New System.Drawing.Point(182, 4)
        Me.te_search.Name = "te_search"
        Me.te_search.Size = New System.Drawing.Size(195, 20)
        Me.te_search.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(110, 8)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Part Number"
        '
        'sb_retrieve
        '
        Me.sb_retrieve.Location = New System.Drawing.Point(383, 3)
        Me.sb_retrieve.Name = "sb_retrieve"
        Me.sb_retrieve.Size = New System.Drawing.Size(75, 23)
        Me.sb_retrieve.TabIndex = 9
        Me.sb_retrieve.Text = "Retrieve"
        '
        'CE_All
        '
        Me.CE_All.Location = New System.Drawing.Point(6, 7)
        Me.CE_All.Name = "CE_All"
        Me.CE_All.Properties.Caption = "Select All"
        Me.CE_All.Size = New System.Drawing.Size(75, 19)
        Me.CE_All.TabIndex = 10
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.BtCancel)
        Me.PanelControl3.Controls.Add(Me.BtOK)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(5, 279)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1022, 36)
        Me.PanelControl3.TabIndex = 1
        '
        'BtCancel
        '
        Me.BtCancel.Location = New System.Drawing.Point(283, 4)
        Me.BtCancel.Name = "BtCancel"
        Me.BtCancel.Size = New System.Drawing.Size(105, 26)
        Me.BtCancel.TabIndex = 0
        Me.BtCancel.Text = "Cancel"
        '
        'BtOK
        '
        Me.BtOK.Location = New System.Drawing.Point(172, 5)
        Me.BtOK.Name = "BtOK"
        Me.BtOK.Size = New System.Drawing.Size(105, 26)
        Me.BtOK.TabIndex = 0
        Me.BtOK.Text = "OK"
        '
        'sb_fill
        '
        Me.sb_fill.Location = New System.Drawing.Point(469, 3)
        Me.sb_fill.Name = "sb_fill"
        Me.sb_fill.Size = New System.Drawing.Size(75, 23)
        Me.sb_fill.TabIndex = 18
        Me.sb_fill.Text = "Select Data"
        Me.sb_fill.Visible = False
        '
        'FInventoryProjectSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1034, 358)
        Me.Name = "FInventoryProjectSearch"
        Me.Text = "Inventory Project Search"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_data.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_search.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CE_All.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents te_search As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sb_retrieve As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents pilih As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CE_All As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sb_fill As DevExpress.XtraEditors.SimpleButton

End Class
