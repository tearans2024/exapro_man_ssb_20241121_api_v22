﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FProjectMaintenanceReport
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
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_view1 = New DevExpress.XtraTab.XtraTabPage
        Me.gc_view1 = New DevExpress.XtraGrid.GridControl
        Me.gv_view1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_view2 = New DevExpress.XtraTab.XtraTabPage
        Me.gc_view2 = New DevExpress.XtraGrid.GridControl
        Me.gv_view2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_view1.SuspendLayout()
        CType(Me.gc_view1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_view1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_view2.SuspendLayout()
        CType(Me.gc_view2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_view2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 32
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_view1
        Me.xtc_master.Size = New System.Drawing.Size(555, 395)
        Me.xtc_master.TabIndex = 3
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_view1, Me.xtp_view2})
        '
        'xtp_view1
        '
        Me.xtp_view1.Controls.Add(Me.gc_view1)
        Me.xtp_view1.Name = "xtp_view1"
        Me.xtp_view1.Size = New System.Drawing.Size(553, 374)
        Me.xtp_view1.Text = "Relation With Detail"
        '
        'gc_view1
        '
        Me.gc_view1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_view1.Location = New System.Drawing.Point(0, 0)
        Me.gc_view1.MainView = Me.gv_view1
        Me.gc_view1.Name = "gc_view1"
        Me.gc_view1.Size = New System.Drawing.Size(553, 374)
        Me.gc_view1.TabIndex = 1
        Me.gc_view1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_view1})
        '
        'gv_view1
        '
        Me.gv_view1.GridControl = Me.gc_view1
        Me.gv_view1.Name = "gv_view1"
        '
        'xtp_view2
        '
        Me.xtp_view2.Controls.Add(Me.gc_view2)
        Me.xtp_view2.Name = "xtp_view2"
        Me.xtp_view2.Size = New System.Drawing.Size(553, 374)
        Me.xtp_view2.Text = "Relation With Detail Customer"
        '
        'gc_view2
        '
        Me.gc_view2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_view2.Location = New System.Drawing.Point(0, 0)
        Me.gc_view2.MainView = Me.gv_view2
        Me.gc_view2.Name = "gc_view2"
        Me.gc_view2.Size = New System.Drawing.Size(553, 374)
        Me.gc_view2.TabIndex = 1
        Me.gc_view2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_view2})
        '
        'gv_view2
        '
        Me.gv_view2.GridControl = Me.gc_view2
        Me.gv_view2.Name = "gv_view2"
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(235, 5)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 19
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(184, 8)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 18
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(75, 5)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 17
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(8, 8)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 16
        Me.LabelControl1.Text = "Project Date"
        '
        'FProjectMaintenanceReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FProjectMaintenanceReport"
        Me.Text = "Project Definition Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_view1.ResumeLayout(False)
        CType(Me.gc_view1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_view1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_view2.ResumeLayout(False)
        CType(Me.gc_view2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_view2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_view1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtp_view2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_view2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_view2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gc_view1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_view1 As DevExpress.XtraGrid.Views.Grid.GridView

End Class
