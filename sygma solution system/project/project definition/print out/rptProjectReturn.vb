Imports master_new.ModFunction

Public Class rptProjectReturn
    Dim flag2 As String
    Dim wod_num As Integer

    Private Sub rptProjectReturn_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'XrPictureBox1.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

        If Trim(GetCurrentColumnValue("sign1")) = "" Then
            lbl_authorized_1.Visible = False
            xlabel_sign1.Visible = False
            xline_sign1.Visible = False
            xl_by.Visible = False
        Else
            lbl_authorized_1.Visible = True
            xlabel_sign1.Visible = True
            xline_sign1.Visible = True
            xl_by.Visible = True
        End If
        If Trim(GetCurrentColumnValue("post1")) = "" Then
            xlabel_post1.Visible = False
        Else
            xlabel_post1.Visible = True
        End If

        If Trim(GetCurrentColumnValue("sign2")) = "" Then
            lbl_authorized_2.Visible = False
            xlabel_sign2.Visible = False
            xline_sign2.Visible = False
        Else
            lbl_authorized_2.Visible = True
            xlabel_sign2.Visible = True
            xline_sign2.Visible = True
        End If
        If Trim(GetCurrentColumnValue("post2")) = "" Then
            xlabel_post2.Visible = False
        Else
            xlabel_post2.Visible = True
        End If

        If Trim(GetCurrentColumnValue("sign3")) = "" Then
            lbl_authorized_3.Visible = False
            xlabel_sign3.Visible = False
            xline_sign3.Visible = False
        Else
            lbl_authorized_3.Visible = True
            xlabel_sign3.Visible = True
            xline_sign3.Visible = True
        End If
        If Trim(GetCurrentColumnValue("post3")) = "" Then
            xlabel_post3.Visible = False
        Else
            xlabel_post3.Visible = True
        End If
    End Sub

End Class