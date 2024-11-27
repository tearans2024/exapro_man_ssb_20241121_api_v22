Imports master_new.ModFunction

Public Class rptGLReport
    Public Posting_Option As Boolean
    Public periode As String
    Dim _count As Integer = 0
    Dim _debet, _credit As Double
    Dim _glt_code As String = ""
    Dim _sum As Double = 0
    Private Sub rptProfitLoss_BeforePrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.BeforePrint
        Try
            If Posting_Option = True Then
                LblStatusPosting.Text = ""
            Else
                LblStatusPosting.Text = "Unposting Data"
            End If

            LblPeriode.Text = periode

            _debet = 0
            _credit = 0
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint

        _sum = _sum + GetCurrentColumnValue("value")

        _count = _count + 1

        XrLabel8.Text = MaskDec(_sum)
    End Sub

    Private Sub GroupFooter2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter2.BeforePrint

        If _count > 1 Then
            'GroupFooter2.Visible = True
            XrLabel24.Visible = True
            XrLabel25.Visible = True
            XrLabel26.Visible = True
        Else
            XrLabel24.Visible = False
            XrLabel25.Visible = False
            XrLabel26.Visible = False
            'GroupFooter2.Visible = False
        End If

    End Sub

    Private Sub GroupHeader2_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader2.BeforePrint
        _count = 0
    End Sub

    Private Sub GroupHeader1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
        _sum = 0
    End Sub
End Class