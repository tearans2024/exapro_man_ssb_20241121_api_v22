Imports master_new.ModFunction
Public Class rptKwitansiReguler

    Private Sub rptKwitansiReguler_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            XRTerbilang.Text = Terbilang(SetNumber(GetCurrentColumnValue("arpay_total_amount"))) & " Rupiah"
            xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class