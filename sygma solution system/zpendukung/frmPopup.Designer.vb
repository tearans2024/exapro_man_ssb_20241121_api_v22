<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPopup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPopup))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TxtMsg = New DevExpress.XtraEditors.LabelControl
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'TxtMsg
        '
        Me.TxtMsg.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMsg.Appearance.ForeColor = System.Drawing.Color.DarkBlue
        Me.TxtMsg.Appearance.Options.UseFont = True
        Me.TxtMsg.Appearance.Options.UseForeColor = True
        Me.TxtMsg.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.TxtMsg.Location = New System.Drawing.Point(16, 9)
        Me.TxtMsg.Name = "TxtMsg"
        Me.TxtMsg.Size = New System.Drawing.Size(256, 16)
        Me.TxtMsg.TabIndex = 0
        Me.TxtMsg.Text = " "
        '
        'frmPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 53)
        Me.Controls.Add(Me.TxtMsg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPopup"
        Me.Text = " "
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TxtMsg As DevExpress.XtraEditors.LabelControl
End Class
