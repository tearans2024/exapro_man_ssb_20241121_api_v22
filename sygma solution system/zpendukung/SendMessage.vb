' --------------------------------------------------------------------------
' Copyrights
' 
' Portions created by or assigned to Cursive Systems, Inc. are
' Copyright (c) 2002-2008 Cursive Systems, Inc.  All Rights Reserved.  Contact
' information for Cursive Systems, Inc. is available at
' http://www.cursive.net/.
' 
' License
' 
' Jabber-Net can be used under either JOSL or the GPL.
' See LICENSE.txt for details.
'  --------------------------------------------------------------------------
Imports master_new.ModFunction

Public Class SendMessage
    Inherits System.Windows.Forms.Form
    Public _user As String
    Public WithEvents txtBody As System.Windows.Forms.RichTextBox

#Region " Windows Form Designer generated code "

    Private m_jc As jabber.client.JabberClient

    Public Sub New(ByRef jc As jabber.client.JabberClient)
        MyBase.New()

        m_jc = jc

        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New(ByRef jc As jabber.client.JabberClient, ByVal toJid As String)
        Me.New(jc)

        txtTo.Text = toJid
    End Sub


    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SendMessage))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSend = New System.Windows.Forms.Button
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.txtTo = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.txtBody = New System.Windows.Forms.RichTextBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnSend)
        Me.Panel1.Controls.Add(Me.txtMessage)
        Me.Panel1.Controls.Add(Me.txtTo)
        Me.Panel1.Controls.Add(Me.label2)
        Me.Panel1.Controls.Add(Me.label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 231)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(529, 108)
        Me.Panel1.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(473, 41)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(48, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Location = New System.Drawing.Point(473, 9)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(48, 23)
        Me.btnSend.TabIndex = 1
        Me.btnSend.Text = "Send"
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(82, 42)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessage.Size = New System.Drawing.Size(383, 54)
        Me.txtMessage.TabIndex = 0
        '
        'txtTo
        '
        Me.txtTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTo.Location = New System.Drawing.Point(82, 10)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(383, 20)
        Me.txtTo.TabIndex = 3
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(8, 41)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(68, 23)
        Me.label2.TabIndex = 8
        Me.label2.Text = "Message:"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(48, 23)
        Me.label1.TabIndex = 6
        Me.label1.Text = "To:"
        '
        'txtBody
        '
        Me.txtBody.BackColor = System.Drawing.Color.White
        Me.txtBody.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtBody.HideSelection = False
        Me.txtBody.Location = New System.Drawing.Point(0, 0)
        Me.txtBody.Name = "txtBody"
        Me.txtBody.ReadOnly = True
        Me.txtBody.Size = New System.Drawing.Size(529, 231)
        Me.txtBody.TabIndex = 2
        Me.txtBody.Text = ""
        '
        'SendMessage
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(529, 339)
        Me.Controls.Add(Me.txtBody)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SendMessage"
        Me.Text = "SendMessage"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try


            Dim msg As New jabber.protocol.client.Message(m_jc.Document)

            msg.To = New jabber.JID(txtTo.Text)

            If txtMessage.Text <> "" Then
                msg.Body = txtMessage.Text
                m_jc.Write(msg)
                'txtBody.AppendText(Now.ToString("HH:mm:ss") & " " & "Me : " & txtMessage.Text & vbNewLine)
                AppendRtfText(Now.ToString("HH:mm:ss") & " " & "Me : " & txtMessage.Text, Color.Blue)
                txtMessage.Text = ""
                'AppendRtfText(Now.ToString("HH:mm:ss") & " " & "Me : " & txtMessage.Text & vbNewLine, Color.Black)
                'AppendRtfText(Now.ToString("HH:mm:ss") & " " & "Me : " & txtMessage.Text & vbNewLine, Color.Red)
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Public Sub AppendRtfText(ByVal par_str As String, ByVal par_color As Color)
        txtBody.SelectionColor = par_color
        txtBody.AppendText(par_str & vbNewLine)
        txtBody.SelectionStart = txtBody.Text.Length
        'txtBody.AppendMaybeScroll(vbCrLf)
        'Dim TextRange As New TextRange(txtChat.Document.ContentEnd, txtChat.Document.ContentEnd)

        '   range.Text = Text;<br />    range.ApplyPropertyValue(TextElement.ForegroundProperty, Color);<br />}

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub SendMessage_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Dim fwFlash As New FlashWindow
            fwFlash.FlashWindow(Me, FlashWindow.enuFlashOptions.FLASHW_STOP, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SendMessage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = _user
            txtMessage.Focus()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub txtMessage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMessage.KeyPress
        If Control.ModifierKeys = Keys.Shift AndAlso e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = False
        Else
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                e.Handled = True
                btnSend_Click(Nothing, Nothing)
            End If
        End If

    End Sub
End Class
