<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reg))
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtID = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.BtAmbilLisensi = New System.Windows.Forms.Button
        Me.BtPaste = New System.Windows.Forms.Button
        Me.Btcopy = New System.Windows.Forms.Button
        Me.LblRegister = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblTimeLoad = New System.Windows.Forms.Label
        Me.BtBatal = New System.Windows.Forms.Button
        Me.BtRegister = New System.Windows.Forms.Button
        Me.TxtLisensi = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtSerial = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ID Komputer"
        '
        'TxtID
        '
        Me.TxtID.Location = New System.Drawing.Point(93, 32)
        Me.TxtID.Name = "TxtID"
        Me.TxtID.ReadOnly = True
        Me.TxtID.Size = New System.Drawing.Size(217, 20)
        Me.TxtID.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.BtAmbilLisensi)
        Me.GroupBox1.Controls.Add(Me.BtPaste)
        Me.GroupBox1.Controls.Add(Me.Btcopy)
        Me.GroupBox1.Controls.Add(Me.LblRegister)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.LblTimeLoad)
        Me.GroupBox1.Controls.Add(Me.BtBatal)
        Me.GroupBox1.Controls.Add(Me.BtRegister)
        Me.GroupBox1.Controls.Add(Me.TxtLisensi)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TxtSerial)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TxtID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 272)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " "
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.MistyRose
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Location = New System.Drawing.Point(15, 195)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(362, 59)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = resources.GetString("Label7.Text")
        '
        'BtAmbilLisensi
        '
        Me.BtAmbilLisensi.Location = New System.Drawing.Point(316, 112)
        Me.BtAmbilLisensi.Name = "BtAmbilLisensi"
        Me.BtAmbilLisensi.Size = New System.Drawing.Size(97, 21)
        Me.BtAmbilLisensi.TabIndex = 4
        Me.BtAmbilLisensi.Text = "Ambil Lisensi"
        Me.BtAmbilLisensi.UseVisualStyleBackColor = True
        '
        'BtPaste
        '
        Me.BtPaste.Location = New System.Drawing.Point(316, 85)
        Me.BtPaste.Name = "BtPaste"
        Me.BtPaste.Size = New System.Drawing.Size(97, 22)
        Me.BtPaste.TabIndex = 4
        Me.BtPaste.Text = "Tempel Lisensi"
        Me.BtPaste.UseVisualStyleBackColor = True
        '
        'Btcopy
        '
        Me.Btcopy.Location = New System.Drawing.Point(316, 31)
        Me.Btcopy.Name = "Btcopy"
        Me.Btcopy.Size = New System.Drawing.Size(97, 21)
        Me.Btcopy.TabIndex = 4
        Me.Btcopy.Text = "Salin ID"
        Me.Btcopy.UseVisualStyleBackColor = True
        '
        'LblRegister
        '
        Me.LblRegister.AutoSize = True
        Me.LblRegister.ForeColor = System.Drawing.Color.Red
        Me.LblRegister.Location = New System.Drawing.Point(15, 159)
        Me.LblRegister.Name = "LblRegister"
        Me.LblRegister.Size = New System.Drawing.Size(46, 13)
        Me.LblRegister.TabIndex = 6
        Me.LblRegister.Text = "Register"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(383, 200)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "  "
        '
        'LblTimeLoad
        '
        Me.LblTimeLoad.AutoSize = True
        Me.LblTimeLoad.ForeColor = System.Drawing.Color.Silver
        Me.LblTimeLoad.Location = New System.Drawing.Point(6, 150)
        Me.LblTimeLoad.Name = "LblTimeLoad"
        Me.LblTimeLoad.Size = New System.Drawing.Size(10, 13)
        Me.LblTimeLoad.TabIndex = 4
        Me.LblTimeLoad.Text = "."
        Me.LblTimeLoad.Visible = False
        '
        'BtBatal
        '
        Me.BtBatal.Location = New System.Drawing.Point(218, 154)
        Me.BtBatal.Name = "BtBatal"
        Me.BtBatal.Size = New System.Drawing.Size(75, 23)
        Me.BtBatal.TabIndex = 3
        Me.BtBatal.Text = "Batal"
        Me.BtBatal.UseVisualStyleBackColor = True
        '
        'BtRegister
        '
        Me.BtRegister.Location = New System.Drawing.Point(117, 154)
        Me.BtRegister.Name = "BtRegister"
        Me.BtRegister.Size = New System.Drawing.Size(75, 23)
        Me.BtRegister.TabIndex = 2
        Me.BtRegister.Text = "Registrasi"
        Me.BtRegister.UseVisualStyleBackColor = True
        '
        'TxtLisensi
        '
        Me.TxtLisensi.Location = New System.Drawing.Point(93, 85)
        Me.TxtLisensi.Multiline = True
        Me.TxtLisensi.Name = "TxtLisensi"
        Me.TxtLisensi.Size = New System.Drawing.Size(217, 54)
        Me.TxtLisensi.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Lisensi"
        '
        'TxtSerial
        '
        Me.TxtSerial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSerial.Location = New System.Drawing.Point(93, 59)
        Me.TxtSerial.Name = "TxtSerial"
        Me.TxtSerial.Size = New System.Drawing.Size(217, 20)
        Me.TxtSerial.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Serial"
        '
        'Reg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 299)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Reg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registrasi"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtID As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtBatal As System.Windows.Forms.Button
    Friend WithEvents BtRegister As System.Windows.Forms.Button
    Friend WithEvents TxtLisensi As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblTimeLoad As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblRegister As System.Windows.Forms.Label
    Friend WithEvents Btcopy As System.Windows.Forms.Button
    Friend WithEvents BtPaste As System.Windows.Forms.Button
    Friend WithEvents TxtSerial As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BtAmbilLisensi As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
