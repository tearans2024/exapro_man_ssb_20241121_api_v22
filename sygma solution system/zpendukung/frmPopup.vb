Public Class frmPopup
    Public _msg As String = ""
    Dim _second As Integer = 0
    Public _user As String
    Private Sub Form2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Text = _user

            If _msg.Length > 67 Then
                TxtMsg.Text = _msg.Substring(1, 67) & " ..."
            Else
                TxtMsg.Text = _msg
            End If

            Dim working_area As Rectangle = _
        SystemInformation.WorkingArea
            Dim x As Integer = _
                working_area.Left + _
                working_area.Width - _
                Me.Width
            Dim y As Integer = _
                working_area.Top + _
                working_area.Height - _
                Me.Height
            Me.Location = New Point(x, y)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If _second >= 30 Then
                Me.Opacity -= 0.05
                'when opacity is zero the form is invisible and we dispose it
                If Me.Opacity = 0 Then Me.Dispose()
            End If
            _second += 1
        Catch ex As Exception
        End Try
    End Sub


End Class