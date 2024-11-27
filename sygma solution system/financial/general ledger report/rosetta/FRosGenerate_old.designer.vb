<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRosGenerate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRosGenerate))
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField7 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.sb_generate = New DevExpress.XtraEditors.SimpleButton
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.te_update_by = New DevExpress.XtraEditors.TextEdit
        Me.te_update_date = New DevExpress.XtraEditors.TextEdit
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        Me.LblStatus = New DevExpress.XtraEditors.LabelControl
        Me.le_periode = New DevExpress.XtraEditors.LookUpEdit
        Me.sb_gen_continue = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_update_by.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_update_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_periode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.pgc_master)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.le_periode)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl5)
        Me.scc_master.Panel1.Controls.Add(Me.LblStatus)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl4)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl3)
        Me.scc_master.Panel1.Controls.Add(Me.te_update_date)
        Me.scc_master.Panel1.Controls.Add(Me.te_update_by)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.le_entity)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.sb_gen_continue)
        Me.scc_master.Panel1.Controls.Add(Me.sb_generate)
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_master.Size = New System.Drawing.Size(872, 491)
        Me.scc_master.SplitterPosition = 44
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=syspro"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("ap_type_name", "ap_type_name"), New System.Data.Common.DataColumnMapping("ap_amount", "ap_amount"), New System.Data.Common.DataColumnMapping("ap_pay_amount", "ap_pay_amount"), New System.Data.Common.DataColumnMapping("ap_outstanding", "ap_outstanding")})})
        '
        'pgc_master
        '
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField7, Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField5, Me.PivotGridField3, Me.PivotGridField4})
        Me.pgc_master.Location = New System.Drawing.Point(5, 5)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.Size = New System.Drawing.Size(543, 335)
        Me.pgc_master.TabIndex = 0
        '
        'PivotGridField7
        '
        Me.PivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField7.AreaIndex = 0
        Me.PivotGridField7.Caption = "Entity"
        Me.PivotGridField7.FieldName = "en_desc"
        Me.PivotGridField7.Name = "PivotGridField7"
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField1.AreaIndex = 1
        Me.PivotGridField1.Caption = "Transaction Line"
        Me.PivotGridField1.FieldName = "line_name"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 2
        Me.PivotGridField2.Caption = "Kode Arus Kas"
        Me.PivotGridField2.FieldName = "cashflow_name"
        Me.PivotGridField2.Name = "PivotGridField2"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "Amount"
        Me.PivotGridField5.CellFormat.FormatString = "n"
        Me.PivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.FieldName = "rstbal_amount"
        Me.PivotGridField5.Name = "PivotGridField5"
        Me.PivotGridField5.ValueFormat.FormatString = "n"
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "Group"
        Me.PivotGridField3.FieldName = "group_name"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.UnboundFieldName = "PivotGridField3"
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField4.AreaIndex = 1
        Me.PivotGridField4.Caption = "Account Name"
        Me.PivotGridField4.FieldName = "account_name"
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.UnboundFieldName = "PivotGridField4"
        '
        'sb_generate
        '
        Me.sb_generate.Location = New System.Drawing.Point(77, 110)
        Me.sb_generate.Name = "sb_generate"
        Me.sb_generate.Size = New System.Drawing.Size(129, 23)
        Me.sb_generate.TabIndex = 16
        Me.sb_generate.Text = "Generate Reset"
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(555, 15)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Size = New System.Drawing.Size(193, 20)
        Me.le_entity.TabIndex = 18
        Me.le_entity.Visible = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(486, 18)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl1.TabIndex = 17
        Me.LabelControl1.Text = "Entity"
        Me.LabelControl1.Visible = False
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(8, 18)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl2.TabIndex = 20
        Me.LabelControl2.Text = "GL Periode"
        '
        'te_update_by
        '
        Me.te_update_by.Location = New System.Drawing.Point(77, 40)
        Me.te_update_by.Name = "te_update_by"
        Me.te_update_by.Size = New System.Drawing.Size(100, 20)
        Me.te_update_by.TabIndex = 21
        '
        'te_update_date
        '
        Me.te_update_date.Location = New System.Drawing.Point(77, 66)
        Me.te_update_date.Name = "te_update_date"
        Me.te_update_date.Size = New System.Drawing.Size(100, 20)
        Me.te_update_date.TabIndex = 22
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(8, 69)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl3.TabIndex = 23
        Me.LabelControl3.Text = "Last Update"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(8, 43)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(56, 13)
        Me.LabelControl4.TabIndex = 24
        Me.LabelControl4.Text = "Updated By"
        '
        'LblStatus
        '
        Me.LblStatus.Location = New System.Drawing.Point(298, 17)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(4, 13)
        Me.LblStatus.TabIndex = 25
        Me.LblStatus.Text = "-"
        '
        'le_periode
        '
        Me.le_periode.Location = New System.Drawing.Point(77, 15)
        Me.le_periode.Name = "le_periode"
        Me.le_periode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_periode.Properties.DisplayFormat.FormatString = "d"
        Me.le_periode.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_periode.Properties.EditFormat.FormatString = "d"
        Me.le_periode.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_periode.Size = New System.Drawing.Size(151, 20)
        Me.le_periode.TabIndex = 26
        '
        'sb_gen_continue
        '
        Me.sb_gen_continue.Location = New System.Drawing.Point(77, 139)
        Me.sb_gen_continue.Name = "sb_gen_continue"
        Me.sb_gen_continue.Size = New System.Drawing.Size(129, 23)
        Me.sb_gen_continue.TabIndex = 16
        Me.sb_gen_continue.Text = "Generate Continue"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(298, 43)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl5.TabIndex = 25
        Me.LabelControl5.Text = "-"
        '
        'FRosGenerate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(872, 491)
        Me.Name = "FRosGenerate"
        Me.Text = "Rosetta Generate"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_update_by.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_update_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_periode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField7 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents sb_generate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents te_update_date As DevExpress.XtraEditors.TextEdit
    Friend WithEvents te_update_by As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents le_periode As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sb_gen_continue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl

End Class
