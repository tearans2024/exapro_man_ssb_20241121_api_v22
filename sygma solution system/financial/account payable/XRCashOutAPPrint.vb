Imports master_new.ModFunction

Public Class XRCashOutAPPrint

    Private Sub XRCashOutAPPrint_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
    End Sub
End Class