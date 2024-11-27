<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPartnerGroup
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
        Me.ap_ac_id = New DevExpress.XtraEditors.LookUpEdit
        Me.ar_ac_id = New DevExpress.XtraEditors.LookUpEdit
        Me.ptnrg_limit_credit = New DevExpress.XtraEditors.TextEdit
        Me.ptnrg_credit_term = New DevExpress.XtraEditors.LookUpEdit
        Me.ptnrg_payment_methode = New DevExpress.XtraEditors.LookUpEdit
        Me.ptnrg_name = New DevExpress.XtraEditors.TextEdit
        Me.ptnrg_active = New DevExpress.XtraEditors.CheckEdit
        Me.ptnrg_desc = New DevExpress.XtraEditors.TextEdit
        Me.ptnrg_code = New DevExpress.XtraEditors.TextEdit
        Me.ptnrg_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.ARAccount = New DevExpress.XtraLayout.LayoutControlItem
        Me.PaymentAccount = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
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
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.ap_ac_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ar_ac_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_limit_credit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_credit_term.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_payment_methode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrg_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ARAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PaymentAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 335)
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
        Me.lci_master.Controls.Add(Me.ap_ac_id)
        Me.lci_master.Controls.Add(Me.ar_ac_id)
        Me.lci_master.Controls.Add(Me.ptnrg_limit_credit)
        Me.lci_master.Controls.Add(Me.ptnrg_credit_term)
        Me.lci_master.Controls.Add(Me.ptnrg_payment_methode)
        Me.lci_master.Controls.Add(Me.ptnrg_name)
        Me.lci_master.Controls.Add(Me.ptnrg_active)
        Me.lci_master.Controls.Add(Me.ptnrg_desc)
        Me.lci_master.Controls.Add(Me.ptnrg_code)
        Me.lci_master.Controls.Add(Me.ptnrg_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 300)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'ap_ac_id
        '
        Me.ap_ac_id.Location = New System.Drawing.Point(103, 132)
        Me.ap_ac_id.Name = "ap_ac_id"
        Me.ap_ac_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ap_ac_id.Size = New System.Drawing.Size(428, 20)
        Me.ap_ac_id.StyleController = Me.lci_master
        Me.ap_ac_id.TabIndex = 19
        '
        'ar_ac_id
        '
        Me.ar_ac_id.Location = New System.Drawing.Point(103, 108)
        Me.ar_ac_id.Name = "ar_ac_id"
        Me.ar_ac_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ar_ac_id.Size = New System.Drawing.Size(428, 20)
        Me.ar_ac_id.StyleController = Me.lci_master
        Me.ar_ac_id.TabIndex = 18
        '
        'ptnrg_limit_credit
        '
        Me.ptnrg_limit_credit.Location = New System.Drawing.Point(103, 204)
        Me.ptnrg_limit_credit.Name = "ptnrg_limit_credit"
        Me.ptnrg_limit_credit.Size = New System.Drawing.Size(428, 20)
        Me.ptnrg_limit_credit.StyleController = Me.lci_master
        Me.ptnrg_limit_credit.TabIndex = 17
        '
        'ptnrg_credit_term
        '
        Me.ptnrg_credit_term.Location = New System.Drawing.Point(103, 156)
        Me.ptnrg_credit_term.Name = "ptnrg_credit_term"
        Me.ptnrg_credit_term.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ptnrg_credit_term.Size = New System.Drawing.Size(428, 20)
        Me.ptnrg_credit_term.StyleController = Me.lci_master
        Me.ptnrg_credit_term.TabIndex = 16
        '
        'ptnrg_payment_methode
        '
        Me.ptnrg_payment_methode.Location = New System.Drawing.Point(103, 180)
        Me.ptnrg_payment_methode.Name = "ptnrg_payment_methode"
        Me.ptnrg_payment_methode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ptnrg_payment_methode.Size = New System.Drawing.Size(428, 20)
        Me.ptnrg_payment_methode.StyleController = Me.lci_master
        Me.ptnrg_payment_methode.TabIndex = 14
        '
        'ptnrg_name
        '
        Me.ptnrg_name.Location = New System.Drawing.Point(103, 60)
        Me.ptnrg_name.Name = "ptnrg_name"
        Me.ptnrg_name.Size = New System.Drawing.Size(428, 20)
        Me.ptnrg_name.StyleController = Me.lci_master
        Me.ptnrg_name.TabIndex = 8
        '
        'ptnrg_active
        '
        Me.ptnrg_active.Location = New System.Drawing.Point(12, 228)
        Me.ptnrg_active.Name = "ptnrg_active"
        Me.ptnrg_active.Properties.Caption = "Is Active"
        Me.ptnrg_active.Size = New System.Drawing.Size(519, 19)
        Me.ptnrg_active.StyleController = Me.lci_master
        Me.ptnrg_active.TabIndex = 7
        '
        'ptnrg_desc
        '
        Me.ptnrg_desc.Location = New System.Drawing.Point(103, 84)
        Me.ptnrg_desc.Name = "ptnrg_desc"
        Me.ptnrg_desc.Size = New System.Drawing.Size(428, 20)
        Me.ptnrg_desc.StyleController = Me.lci_master
        Me.ptnrg_desc.TabIndex = 6
        '
        'ptnrg_code
        '
        Me.ptnrg_code.Location = New System.Drawing.Point(103, 36)
        Me.ptnrg_code.Name = "ptnrg_code"
        Me.ptnrg_code.Size = New System.Drawing.Size(428, 20)
        Me.ptnrg_code.StyleController = Me.lci_master
        Me.ptnrg_code.TabIndex = 5
        '
        'ptnrg_en_id
        '
        Me.ptnrg_en_id.Location = New System.Drawing.Point(103, 12)
        Me.ptnrg_en_id.Name = "ptnrg_en_id"
        Me.ptnrg_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ptnrg_en_id.Size = New System.Drawing.Size(428, 20)
        Me.ptnrg_en_id.StyleController = Me.lci_master
        Me.ptnrg_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.LayoutControlItem5, Me.LayoutControlItem7, Me.LayoutControlItem3, Me.LayoutControlItem6, Me.LayoutControlItem9, Me.ARAccount, Me.PaymentAccount})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 300)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.ptnrg_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.ptnrg_code
        Me.LayoutControlItem2.CustomizationFormText = "Code"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem2.Text = "Code"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.ptnrg_active
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 216)
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
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 239)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 41)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.ptnrg_name
        Me.LayoutControlItem5.CustomizationFormText = "Name"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem5.Text = "Name"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.ptnrg_payment_methode
        Me.LayoutControlItem7.CustomizationFormText = "Payment Methode"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 168)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem7.Text = "Payment Methode"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.ptnrg_desc
        Me.LayoutControlItem3.CustomizationFormText = "Description"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Description"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.ptnrg_credit_term
        Me.LayoutControlItem6.CustomizationFormText = "Credit Term"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem6.Text = "Credit Term"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(87, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.ptnrg_limit_credit
        Me.LayoutControlItem9.CustomizationFormText = "Credit Limit"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 192)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem9.Text = "Credit Limit"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(87, 13)
        '
        'ARAccount
        '
        Me.ARAccount.Control = Me.ar_ac_id
        Me.ARAccount.CustomizationFormText = "Sales Account"
        Me.ARAccount.Location = New System.Drawing.Point(0, 96)
        Me.ARAccount.Name = "ARAccount"
        Me.ARAccount.Size = New System.Drawing.Size(523, 24)
        Me.ARAccount.Text = "Sales Account"
        Me.ARAccount.TextSize = New System.Drawing.Size(87, 13)
        '
        'PaymentAccount
        '
        Me.PaymentAccount.Control = Me.ap_ac_id
        Me.PaymentAccount.CustomizationFormText = "Payment Account"
        Me.PaymentAccount.Location = New System.Drawing.Point(0, 120)
        Me.PaymentAccount.Name = "PaymentAccount"
        Me.PaymentAccount.Size = New System.Drawing.Size(523, 24)
        Me.PaymentAccount.Text = "Payment Account"
        Me.PaymentAccount.TextSize = New System.Drawing.Size(87, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FPartnerGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FPartnerGroup"
        Me.Text = "Partner Group"
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
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.ap_ac_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ar_ac_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_limit_credit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_credit_term.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_payment_methode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrg_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ARAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PaymentAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents ptnrg_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ptnrg_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrg_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrg_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents ptnrg_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents ptnrg_payment_methode As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ptnrg_credit_term As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ptnrg_limit_credit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ap_ac_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents ar_ac_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents ARAccount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PaymentAccount As DevExpress.XtraLayout.LayoutControlItem

End Class
