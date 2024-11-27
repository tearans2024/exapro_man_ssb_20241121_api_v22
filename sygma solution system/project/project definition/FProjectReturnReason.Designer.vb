<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FProjectReturnReason
    Inherits syspro_ver2.FCodeMstr

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
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_code_usr_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lci_master
        '
        Me.lci_master.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_code_usr_1})
        '
        'lci_code_usr_1
        '
        Me.lci_code_usr_1.Location = New System.Drawing.Point(261, 215)
        Me.lci_code_usr_1.Size = New System.Drawing.Size(262, 117)
        Me.lci_code_usr_1.TextToControlDistance = 5
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 433)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 142)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 190)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FProjectReturnReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FProjectReturnReason"
        Me.Text = "Project Return Reason"
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_code_usr_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
