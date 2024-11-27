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

'Imports MyDll.DBMysql
'Imports MyDll.ModFunctionMy


Imports master_new.ModFunction
Imports System.Data

Imports System.Diagnostics
Imports System.Xml

Imports jabber
Imports jabber.protocol
Imports jabber.protocol.client
Imports jabber.protocol.iq



Public Class MainForm
    Inherits System.Windows.Forms.Form
    'Dim dt, dt2 As New DataTable
    Friend WithEvents TstLog As muzzle.BottomScrollRichText
    Friend WithEvents tpLog As System.Windows.Forms.TabPage
    Public _window_pesan As Object

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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

    Private m_err As Boolean = False

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents sb As System.Windows.Forms.StatusBar
    Friend WithEvents pnlCon As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlPresence As System.Windows.Forms.StatusBarPanel
    Friend WithEvents jc As jabber.client.JabberClient
    Friend WithEvents rm As jabber.client.RosterManager
    Friend WithEvents pm As jabber.client.PresenceManager
    Friend WithEvents ilPresence As System.Windows.Forms.ImageList
    Friend WithEvents mnuPresence As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents TabLog As System.Windows.Forms.TabControl
    Friend WithEvents tpRoster As System.Windows.Forms.TabPage
    Friend WithEvents tpDebug As System.Windows.Forms.TabPage

    Friend WithEvents mnuAvailable As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAway As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOffline As System.Windows.Forms.MenuItem
    Friend WithEvents roster As muzzle.RosterTree
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SendMessaheToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpMessage As System.Windows.Forms.TabPage
    Friend WithEvents PesanText As muzzle.BottomScrollRichText
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents debug As muzzle.BottomScrollRichText

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.sb = New System.Windows.Forms.StatusBar
        Me.pnlCon = New System.Windows.Forms.StatusBarPanel
        Me.pnlPresence = New System.Windows.Forms.StatusBarPanel
        Me.jc = New jabber.client.JabberClient(Me.components)
        Me.rm = New jabber.client.RosterManager(Me.components)
        Me.pm = New jabber.client.PresenceManager(Me.components)
        Me.ilPresence = New System.Windows.Forms.ImageList(Me.components)
        Me.mnuPresence = New System.Windows.Forms.ContextMenu
        Me.mnuAvailable = New System.Windows.Forms.MenuItem
        Me.mnuAway = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mnuOffline = New System.Windows.Forms.MenuItem
        Me.TabLog = New System.Windows.Forms.TabControl
        Me.tpRoster = New System.Windows.Forms.TabPage
        Me.roster = New muzzle.RosterTree
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendMessaheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tpDebug = New System.Windows.Forms.TabPage
        Me.debug = New muzzle.BottomScrollRichText
        Me.tpMessage = New System.Windows.Forms.TabPage
        Me.PesanText = New muzzle.BottomScrollRichText
        Me.tpLog = New System.Windows.Forms.TabPage
        Me.TstLog = New muzzle.BottomScrollRichText
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pnlCon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlPresence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabLog.SuspendLayout()
        Me.tpRoster.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.tpDebug.SuspendLayout()
        Me.tpMessage.SuspendLayout()
        Me.tpLog.SuspendLayout()
        Me.SuspendLayout()
        '
        'sb
        '
        Me.sb.Location = New System.Drawing.Point(0, 488)
        Me.sb.Name = "sb"
        Me.sb.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.pnlCon, Me.pnlPresence})
        Me.sb.ShowPanels = True
        Me.sb.Size = New System.Drawing.Size(632, 22)
        Me.sb.TabIndex = 0
        '
        'pnlCon
        '
        Me.pnlCon.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.pnlCon.Name = "pnlCon"
        Me.pnlCon.Text = "Click on ""Offline"", and select a presence to log in."
        Me.pnlCon.Width = 568
        '
        'pnlPresence
        '
        Me.pnlPresence.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.pnlPresence.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.pnlPresence.Name = "pnlPresence"
        Me.pnlPresence.Text = "Offline"
        Me.pnlPresence.Width = 47
        '
        'jc
        '
        Me.jc.AutoReconnect = 3.0!
        Me.jc.AutoStartCompression = True
        Me.jc.AutoStartTLS = True
        Me.jc.InvokeControl = Me
        Me.jc.KeepAlive = 30.0!
        Me.jc.LocalCertificate = Nothing
        Me.jc.Password = Nothing
        Me.jc.User = Nothing
        '
        'rm
        '
        Me.rm.AutoAllow = jabber.client.AutoSubscriptionHanding.AllowAll
        Me.rm.Stream = Me.jc
        '
        'pm
        '
        Me.pm.CapsManager = Nothing
        Me.pm.Stream = Me.jc
        '
        'ilPresence
        '
        Me.ilPresence.ImageStream = CType(resources.GetObject("ilPresence.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilPresence.TransparentColor = System.Drawing.Color.Transparent
        Me.ilPresence.Images.SetKeyName(0, "")
        Me.ilPresence.Images.SetKeyName(1, "")
        '
        'mnuPresence
        '
        Me.mnuPresence.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAvailable, Me.mnuAway, Me.MenuItem3, Me.mnuOffline})
        '
        'mnuAvailable
        '
        Me.mnuAvailable.Index = 0
        Me.mnuAvailable.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.mnuAvailable.Text = "Available"
        '
        'mnuAway
        '
        Me.mnuAway.Index = 1
        Me.mnuAway.Shortcut = System.Windows.Forms.Shortcut.CtrlA
        Me.mnuAway.Text = "Away"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "-"
        '
        'mnuOffline
        '
        Me.mnuOffline.Index = 3
        Me.mnuOffline.Shortcut = System.Windows.Forms.Shortcut.F9
        Me.mnuOffline.Text = "Offline"
        '
        'TabLog
        '
        Me.TabLog.Controls.Add(Me.tpRoster)
        Me.TabLog.Controls.Add(Me.tpDebug)
        Me.TabLog.Controls.Add(Me.tpMessage)
        Me.TabLog.Controls.Add(Me.tpLog)
        Me.TabLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabLog.Location = New System.Drawing.Point(0, 0)
        Me.TabLog.Name = "TabLog"
        Me.TabLog.SelectedIndex = 0
        Me.TabLog.Size = New System.Drawing.Size(632, 488)
        Me.TabLog.TabIndex = 1
        '
        'tpRoster
        '
        Me.tpRoster.Controls.Add(Me.roster)
        Me.tpRoster.Location = New System.Drawing.Point(4, 22)
        Me.tpRoster.Name = "tpRoster"
        Me.tpRoster.Size = New System.Drawing.Size(624, 462)
        Me.tpRoster.TabIndex = 0
        Me.tpRoster.Text = "Roster"
        Me.tpRoster.UseVisualStyleBackColor = True
        '
        'roster
        '
        Me.roster.AllowDrop = True
        Me.roster.Client = Me.jc
        Me.roster.ContextMenuStrip = Me.ContextMenuStrip1
        Me.roster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.roster.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.roster.ImageIndex = 1
        Me.roster.Location = New System.Drawing.Point(0, 0)
        Me.roster.Name = "roster"
        Me.roster.PresenceManager = Me.pm
        Me.roster.RosterManager = Me.rm
        Me.roster.SelectedImageIndex = 0
        Me.roster.ShowLines = False
        Me.roster.ShowRootLines = False
        Me.roster.Size = New System.Drawing.Size(624, 462)
        Me.roster.Sorted = True
        Me.roster.StatusColor = System.Drawing.Color.Teal
        Me.roster.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendMessaheToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(150, 26)
        '
        'SendMessaheToolStripMenuItem
        '
        Me.SendMessaheToolStripMenuItem.Name = "SendMessaheToolStripMenuItem"
        Me.SendMessaheToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.SendMessaheToolStripMenuItem.Text = "Send Message"
        '
        'tpDebug
        '
        Me.tpDebug.Controls.Add(Me.debug)
        Me.tpDebug.Location = New System.Drawing.Point(4, 22)
        Me.tpDebug.Name = "tpDebug"
        Me.tpDebug.Size = New System.Drawing.Size(624, 462)
        Me.tpDebug.TabIndex = 1
        Me.tpDebug.Text = "Debug"
        Me.tpDebug.UseVisualStyleBackColor = True
        '
        'debug
        '
        Me.debug.Dock = System.Windows.Forms.DockStyle.Fill
        Me.debug.Location = New System.Drawing.Point(0, 0)
        Me.debug.Name = "debug"
        Me.debug.Size = New System.Drawing.Size(624, 462)
        Me.debug.TabIndex = 0
        Me.debug.Text = ""
        '
        'tpMessage
        '
        Me.tpMessage.Controls.Add(Me.PesanText)
        Me.tpMessage.Location = New System.Drawing.Point(4, 22)
        Me.tpMessage.Name = "tpMessage"
        Me.tpMessage.Size = New System.Drawing.Size(624, 462)
        Me.tpMessage.TabIndex = 2
        Me.tpMessage.Text = "Message"
        Me.tpMessage.UseVisualStyleBackColor = True
        '
        'PesanText
        '
        Me.PesanText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PesanText.Location = New System.Drawing.Point(0, 0)
        Me.PesanText.Name = "PesanText"
        Me.PesanText.Size = New System.Drawing.Size(624, 462)
        Me.PesanText.TabIndex = 1
        Me.PesanText.Text = ""
        '
        'tpLog
        '
        Me.tpLog.Controls.Add(Me.TstLog)
        Me.tpLog.Location = New System.Drawing.Point(4, 22)
        Me.tpLog.Name = "tpLog"
        Me.tpLog.Padding = New System.Windows.Forms.Padding(3)
        Me.tpLog.Size = New System.Drawing.Size(624, 462)
        Me.tpLog.TabIndex = 3
        Me.tpLog.Text = "Log"
        Me.tpLog.UseVisualStyleBackColor = True
        '
        'TstLog
        '
        Me.TstLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TstLog.Location = New System.Drawing.Point(3, 3)
        Me.TstLog.Name = "TstLog"
        Me.TstLog.Size = New System.Drawing.Size(618, 456)
        Me.TstLog.TabIndex = 2
        Me.TstLog.Text = ""
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(632, 510)
        Me.Controls.Add(Me.TabLog)
        Me.Controls.Add(Me.sb)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.Text = "SYGMA CHAT"
        CType(Me.pnlCon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlPresence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabLog.ResumeLayout(False)
        Me.tpRoster.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.tpDebug.ResumeLayout(False)
        Me.tpMessage.ResumeLayout(False)
        Me.tpLog.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Connect()

        'Dim log As New muzzle.ClientLogin(jc)
        'log.ReadFromFile("login.xml")
        'If log.ShowDialog() = Windows.Forms.DialogResult.OK Then
        'log.WriteToFile("login.xml")

        'If update_hosts() = False Then
        '    Exit Sub
        'End If

        jc.User = master_new.PGSqlConn.GetRowInfo("select coalesce(userpidgin,'') as user_pidgin from tconfuser where userid=" & master_new.ClsVar.sUserID)(0).ToString  '"sygma"
        jc.Password = "123"
        jc.PlaintextAuth = False
        jc.NetworkHost = master_new.PGSqlConn.GetRowInfo("select coalesce(xmpp_ip,'') as xmpp_ip from tconfsetting")(0).ToString  '"sygma"
        jc.Server = master_new.PGSqlConn.GetRowInfo("select coalesce(xmpp_name,'') as xmpp_name from tconfsetting")(0).ToString

        jc.Port = 5222
        jc.SSL = False
        jc.Connection = connection.ConnectionType.Socket


        If jc.User <> "" Then
            jc.Connect()
        End If

        'End If
    End Sub
    Private Function update_hosts() As Boolean
        Try

            Dim windowsPath As String = System.Environment.GetEnvironmentVariable("windir")
            Using sw As New System.IO.StreamWriter(windowsPath & "\system32\drivers\etc\hosts", True)
                'sw.WriteLine(konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "server_xmpp_ip") & " " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "server_xmpp_name"))
                'sw.Close()
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub sb_PanelClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.StatusBarPanelClickEventArgs) Handles sb.PanelClick
        If Not e.StatusBarPanel Is pnlPresence Then Return

        mnuPresence.Show(sb, New Point(e.X, e.Y))
    End Sub

    Private Sub mnuOffline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOffline.Click
        If jc.IsAuthenticated Then
            jc.Close()
        End If
    End Sub

    Private Sub mnuAway_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAway.Click
        If jc.IsAuthenticated Then
            jc.Presence(PresenceType.available, "Away", "away", 0)
            pnlPresence.Text = "Away"
        Else
            Connect()
        End If
    End Sub

    Private Sub mnuAvailable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAvailable.Click
        If jc.IsAuthenticated Then
            jc.Presence(PresenceType.available, "Available", Nothing, 0)
            pnlPresence.Text = "Available"
        Else
            Connect()
        End If
    End Sub

    Private Sub jc_OnConnect(ByVal sender As Object, ByVal stream As jabber.connection.StanzaStream) Handles jc.OnConnect
        m_err = False
        Exit Sub
        ' debug.AppendMaybeScroll("Connected to: " & stream.ToString() & vbCrLf)
    End Sub

    Private Sub jc_OnReadText(ByVal sender As Object, ByVal txt As String) Handles jc.OnReadText
        Try
            debug.SelectionColor = Color.Red
            debug.AppendText("RECV: ")
            debug.SelectionColor = Color.Black
            debug.AppendText(txt)
            debug.AppendMaybeScroll(vbCrLf)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub jc_OnWriteText(ByVal sender As Object, ByVal txt As String) Handles jc.OnWriteText
        ' keepalive
        If txt = " " Then
            Return
        End If

        debug.SelectionColor = Color.Blue
        debug.AppendText("SEND: ")
        debug.SelectionColor = Color.Black
        debug.AppendText(txt)
        debug.AppendMaybeScroll(vbCrLf)
    End Sub

    Private Sub jc_OnAuthenticate(ByVal sender As Object) Handles jc.OnAuthenticate
        pnlPresence.Text = "Available"
        pnlCon.Text = "Connected"
    End Sub

    Private Sub jc_OnDisconnect(ByVal sender As Object) Handles jc.OnDisconnect
        pnlPresence.Text = "Offline"

        If Not m_err Then
            pnlCon.Text = "Disconnected"
        End If

    End Sub

    Private Sub jc_OnError(ByVal sender As Object, ByVal ex As System.Exception) Handles jc.OnError

        pnlCon.Text = "Error!"
        debug.SelectionColor = Color.Green
        debug.AppendText("ERROR: ")
        debug.SelectionColor = Color.Black
        debug.AppendText(ex.ToString())
        debug.AppendMaybeScroll(vbCrLf)

    End Sub

    Private Sub jc_OnAuthError(ByVal sender As Object, ByVal iq As XmlElement) Handles jc.OnAuthError
        If (MessageBox.Show(Me, "Create new account?", _
            "Authentication error", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK) Then
            jc.Register(New JID(jc.User, jc.Server, Nothing))
        Else
            jc.Close()
            Connect()
        End If
    End Sub

    Private Function jc_OnRegisterInfo(ByVal sender As System.Object, ByVal register As jabber.protocol.iq.Register) As System.Boolean Handles jc.OnRegisterInfo
        Dim data As jabber.protocol.x.Data = register.Form
        If (data Is Nothing) Then Return True

        Dim form As muzzle.XDataForm = New muzzle.XDataForm(data)

        If (form.ShowDialog() <> Windows.Forms.DialogResult.OK) Then Return False
        form.FillInResponse(data)

        Return True
    End Function

    Private Sub jc_OnRegistered(ByVal sender As Object, ByVal iq As jabber.protocol.client.IQ) Handles jc.OnRegistered
        If (iq.Type = jabber.protocol.client.IQType.result) Then
            jc.Login()
        Else
            pnlCon.Text = "Registration error"
        End If
    End Sub

    Private Sub jc_OnMessage(ByVal sender As Object, ByVal msg As jabber.protocol.client.Message) Handles jc.OnMessage
        Try

            If Not msg.Body Is Nothing Then
                'PesanText.AppendText(msg.From.Bare & " : " & msg.Body & vbNewLine)
                If find_form(msg.From.Bare, msg.Body.ToString) = False Then
                    Exit Sub
                End If
            End If


        Catch ex As Exception
            TstLog.AppendText(ex.Message)
        End Try


    End Sub
    Public Function find_form(ByVal par_user As String, ByVal par_message As String) As Boolean
        Try
            Dim frm As Form

            For Each frm In My.Application.OpenForms

                If frm.Text = par_user Then

                    Dim new_frm As SendMessage = DirectCast(frm, SendMessage)

                    'new_frm.txtBody.AppendText(Now.ToString("HH:mm:ss") & " " & par_user & " : " & par_message & vbNewLine)
                    new_frm.AppendRtfText(Now.ToString("HH:mm:ss") & " " & par_user & " : " & par_message, Color.Red)
                    'new_frm.BringToFront()

                    Dim _popup As New frmPopup

                    _popup._user = par_user
                    _popup._msg = Now.ToString("HH:mm:ss") & " " & par_user & " : " & par_message
                    _popup.Show()

                    Dim fwFlash As New FlashWindow

                    fwFlash.FlashWindow(new_frm, FlashWindow.enuFlashOptions.FLASHW_ALL, 9999)

                    Return True
                    Exit Function
                End If
            Next

            Dim sm As New SendMessage(jc, par_user)
            sm._user = par_user
            'sm.txtBody.AppendText(Now.ToString("HH:mm:ss") & " " & par_user & " : " & par_message & vbNewLine)
            sm.AppendRtfText(Now.ToString("HH:mm:ss") & " " & par_user & " : " & par_message, Color.Red)
            sm.Show()
            sm.BringToFront()

            'Dim fwFlash As New FlashWindow
            'fwFlash.FlashWindow(sm, FlashWindow.enuFlashOptions.FLASHW_ALL, 9999)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function find_form(ByVal par_user As String) As Boolean
        Try
            Dim frm As Form

            For Each frm In My.Application.OpenForms

                If frm.Text = par_user Then
                    'do something
                    'Dim new_frm As SendMessage = DirectCast(frm, SendMessage)

                    'new_frm.txtBody.AppendText(par_message & vbNewLine)
                    frm.BringToFront()
                    Return True
                    Exit Function
                End If
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub jc_OnIQ(ByVal sender As Object, ByVal iq As jabber.protocol.client.IQ) Handles jc.OnIQ
        If iq.Type <> jabber.protocol.client.IQType.get Then Return

        Dim query As XmlElement = iq.Query
        If (query Is Nothing) Then
            Return
        End If

        If TypeOf query Is Version Then
            iq = iq.GetResponse(jc.Document)
            Dim ver As jabber.protocol.iq.Version = DirectCast(iq.Query, jabber.protocol.iq.Version)
            ver.OS = Environment.OSVersion.ToString()
            ver.EntityName = Application.ProductName
            ver.Ver = Application.ProductVersion
            jc.Write(iq)
        End If
    End Sub

    Private Sub rm_OnRosterEnd(ByVal sender As Object) Handles rm.OnRosterEnd
        'roster.ExpandAll()
    End Sub

    Private Sub roster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles roster.DoubleClick
        Try
            Dim n As muzzle.RosterTree.ItemNode = DirectCast(roster.SelectedNode, muzzle.RosterTree.ItemNode)
            If find_form(n.JID.ToString) = False Then
                Dim sm As New SendMessage(jc, n.JID.ToString())
                sm._user = n.JID.ToString
                sm.Show()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub jc_OnStreamError(ByVal sender As Object, ByVal rp As System.Xml.XmlElement) Handles jc.OnStreamError
        m_err = True
        pnlCon.Text = "Stream error: " + rp.InnerText
    End Sub

    Private Sub jc_OnStreamInit(ByVal sender As Object, ByVal stream As jabber.protocol.ElementStream) Handles jc.OnStreamInit
        stream.AddFactory(New FooFactory)
    End Sub


    Private Sub SendMessaheToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMessaheToolStripMenuItem.Click
        Dim n As muzzle.RosterTree.ItemNode = DirectCast(roster.SelectedNode, muzzle.RosterTree.ItemNode)
        'Dim sm As New SendMessage(jc, n.JID.ToString())
        'sm._user = n.JID.ToString
        'sm.Show()
        If find_form(n.JID.ToString) = False Then
            Dim sm As New SendMessage(jc, n.JID.ToString())
            sm._user = n.JID.ToString
            sm.Show()
        End If
    End Sub

    Private Sub SendToAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Dim sSql As String
        'Dim Tujuan As String = ""
        Try
            'Box(DbConString2)
            'sSql = "SELECT  A.groupName,  A.username,  A.administrator " _
            '& "FROM  jivegroupuser A " _
            '& "Where A.groupName='Sygma_Pst_IT'"

            'dt = MyDll.DBMysql.GetTableData(sSql)
            'For Each row As DataRow In dt.Rows
            '    If Tujuan <> "" Then
            '        Tujuan = Tujuan & row("username") & "@serv" & ";"
            '    Else
            '        Tujuan = row("username") & "@serv" & ";"
            '    End If

            'Next
            'Dim sm As New SendMessage(jc, Tujuan)
            'sm.Show()

        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub

    Private Sub ShowMessageFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim sm As New SendMessage(jc, "")
        sm.Show()
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'mnuOffline_Click(sender, e)
        Try
            If jc.IsAuthenticated Then
                jc.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            mnuAvailable_Click(sender, e)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If Now.Second = 30 Then
                If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt"), "xmpp_on") = "1" Then
                    mnuAvailable_Click(sender, e)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class

'-------------------------- Add packet type -----------------------
' don't forget to call AddFactory() in OnStreamInit!

' Convenience class, used for creating outbound IQ's with this type
Public Class FooIQ
    Inherits jabber.protocol.client.IQ

    Public Sub New(ByVal doc As XmlDocument)
        MyBase.New(doc)
        doc.AppendChild(New Foo(doc))
    End Sub
End Class

' The type of the first child of IQ.  Example packet:
'
' <iq>
'   <query xmlns='urn:foo'>
'     <bar>A value</bar>
'   </query>
' </iq>
Public Class Foo
    Inherits jabber.protocol.Element

    ' the namespace
    Public Const NS As String = "urn:foo"

    Public Sub New(ByVal doc As XmlDocument)
        MyBase.New("query", NS, doc)
    End Sub

    Public Sub New(ByVal prefix As String, ByVal qname As XmlQualifiedName, ByVal doc As XmlDocument)
        MyBase.New(prefix, qname, doc)
    End Sub

    ' this property gets and sets a child element called "bar".
    Public Property Bar() As String
        Get
            Return MyBase.GetElem("bar")
        End Get
        Set(ByVal Value As String)
            MyBase.SetElem("bar", Value)
        End Set
    End Property
End Class


' The factory class.  This ends up adding a mapping from urn:foo|foo to the constructor for the Foo class,
' under the covers.  The namespace|elementname of an inbound element will be looked up in the map to
' figure out how to create the correct type.
Public Class FooFactory
    Implements jabber.protocol.IPacketTypes

    Private Shared ReadOnly s_qnames As jabber.protocol.QnameType() = _
        {New jabber.protocol.QnameType("query", Foo.NS, GetType(Foo))}

    Public ReadOnly Property Types() As jabber.protocol.QnameType() Implements jabber.protocol.IPacketTypes.Types
        Get
            Return s_qnames
        End Get
    End Property
End Class
