Imports DevExpress.XtraPrinting
Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Public Class XRLapProductByMonthByEntity

    Private Sub XRProductByMonthByEntity2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
        Catch ex As Exception

        End Try
    End Sub
End Class