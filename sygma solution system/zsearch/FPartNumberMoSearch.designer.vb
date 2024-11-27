<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPartNumberMoSearch
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
        Me.sb_retrieve = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.te_search = New DevExpress.XtraEditors.TextEdit
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.sb_get = New DevExpress.XtraEditors.SimpleButton
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_data.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_search.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.sb_get)
        Me.scc_master.Panel1.Controls.Add(Me.sb_retrieve)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.te_search)
        Me.scc_master.Size = New System.Drawing.Size(601, 453)
        Me.scc_master.SplitterPosition = 28
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(601, 419)
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(599, 418)
        '
        'sb_retrieve
        '
        Me.sb_retrieve.Location = New System.Drawing.Point(285, 3)
        Me.sb_retrieve.Name = "sb_retrieve"
        Me.sb_retrieve.Size = New System.Drawing.Size(105, 23)
        Me.sb_retrieve.TabIndex = 12
        Me.sb_retrieve.Text = "Retrieve"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 8)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 11
        Me.LabelControl1.Text = "Part Number"
        '
        'te_search
        '
        Me.te_search.EditValue = ""
        Me.te_search.Location = New System.Drawing.Point(77, 4)
        Me.te_search.Name = "te_search"
        Me.te_search.Size = New System.Drawing.Size(201, 20)
        Me.te_search.TabIndex = 10
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(589, 408)
        Me.gc_master.TabIndex = 1
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        Me.gv_master.OptionsView.ColumnAutoWidth = False
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "# (e)"
        Me.GridColumn1.FieldName = "ceklist"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 45
        '
        'sb_get
        '
        Me.sb_get.Location = New System.Drawing.Point(395, 3)
        Me.sb_get.Name = "sb_get"
        Me.sb_get.Size = New System.Drawing.Size(105, 23)
        Me.sb_get.TabIndex = 14
        Me.sb_get.Text = "Select Data"
        '
        'FPartNumberMoSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(601, 453)
        Me.Name = "FPartNumberMoSearch"
        Me.Text = "Part Number Search"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_data.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_search.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sb_retrieve As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents te_search As DevExpress.XtraEditors.TextEdit
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sb_get As DevExpress.XtraEditors.SimpleButton

End Class
