Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports System.Net

Public Class Reg


    Public Function EncryptText(ByVal strText As String) As String
        Return Encrypt(strText, "22222222")
    End Function

    'Decrypt the text 
    Public Function DecryptText(ByVal strText As String) As String
        Return Decrypt(strText, "22222222")
    End Function

    Private Function Encrypt(ByVal strText As String, ByVal strEncrKey _
             As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(strEncrKey, 8))

            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), _
                   CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return System.Convert.ToBase64String(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function Decrypt(ByVal strText As String, ByVal sDecrKey _
               As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Strings.Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = System.Convert.FromBase64String(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, _
                 IV), CryptoStreamMode.Write)

            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

            Return encoding.GetString(ms.ToArray())

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Function cek_hw() As String
        'Dim _key_date As String
        Dim _value As String
        Try
            'fp.UseCpuID = 1
            'fp.UseBiosID = 1
            'fp.UseBaseID = 1 'mobo
            'fp.UseDiskID = 0 'hdd
            'fp.UseVideoID = 1 'vga
            'fp.UseMacID = 0 'ethernet

            _value = CpuID()


            _value = _value.ToUpper & BaseID().ToUpper & BiosID.ToUpper
            'TxtLisensi.Text = _value


            _value = Pack(_value)
            Return _value
        Catch ex As Exception
            Return ""
        End Try

    End Function
    Private m_ReturnLength As Long = 6
    Private m_TotalLength As Long = 6
    Private Function Pack(ByVal Text As String) As String


        'Packs the string to m_ReturnLength digits
        'If m_ReturnLength=-1 : Return complete string

        Dim RetVal As String
        Dim X As Long
        Dim Y As Long
        Dim N As Char

        For Each N In Text
            Y += 1
            X += (Asc(N) * Y)
        Next N

        If m_ReturnLength > 0 Then
            RetVal = X.ToString.PadRight(m_ReturnLength, "0")
        Else
            RetVal = X.ToString
        End If

        If m_ReturnLength = 0 Then
            m_TotalLength = RetVal.Length
            Return RetVal
        Else
            m_TotalLength = RetVal.Length
            Return RetVal.Substring(0, m_ReturnLength)
        End If


    End Function


    Private Function BiosID() As String
        'RaiseEvent StartingWith("BiosID")

        'BIOS Identifier
        Try
            Dim _value As String
            _value = Identifier("Win32_BIOS", "Manufacturer") _
              & Identifier("Win32_BIOS", "SMBIOSBIOSVersion") _
              & Identifier("Win32_BIOS", "IdentificationCode") _
              & Identifier("Win32_BIOS", "SerialNumber") _
              & Identifier("Win32_BIOS", "ReleaseDate") _
              & Identifier("Win32_BIOS", "Version")

            Return _value
        Catch ex As Exception
            Return "XX"
        End Try

    End Function

    Private Function BaseID() As String
        ' RaiseEvent StartingWith("BaseID")

        'Motherboard ID

        Dim _value As String
        Try

            _value = Identifier("Win32_BaseBoard", "Model") _
              & Identifier("Win32_BaseBoard", "Manufacturer") _
              & Identifier("Win32_BaseBoard", "Name") _
              & Identifier("Win32_BaseBoard", "SerialNumber")
            ' Return _value.Substring(0, 2) & Microsoft.VisualBasic.Right(_value, 2)
            Return _value
        Catch ex As Exception
            Return "XX"
        End Try
        '  RaiseEvent DoneWith("BaseID")
    End Function
    Public Function CpuID() As String
        'RaiseEvent StartingWith("CpuID")

        'Uses first CPU identifier available in order of preference
        'Don't get all identifiers as very time consuming
        Try
            Dim RetVal As String = Identifier("Win32_Processor", "UniqueId")

            If RetVal = "" Then   'If no UniqueId, use ProcessorID
                RetVal = Identifier("Win32_Processor", "ProcessorId")

                If RetVal = "" Then   'If no ProcessorID, use Name
                    RetVal = Identifier("Win32_Processor", "Name")

                    If RetVal = "" Then   'If no Name, use Manufacturer
                        RetVal = Identifier("Win32_Processor", "Manufacturer")
                    End If


                End If
            End If

            'Add clock speed for extra security
            'RetVal += Identifier("Win32_Processor", "MaxClockSpeed")

            'Return RetVal.Substring(0, 2) & Microsoft.VisualBasic.Right(RetVal, 2)
            Return RetVal
        Catch ex As Exception
            Return "XX"
        End Try


        '        RaiseEvent DoneWith("CpuID")
    End Function

    Private Function Identifier(ByVal wmiClass As String, ByVal wmiProperty As String) As String
        'Return a hardware identifier

        Dim Result As String = ""
        Dim mc As New System.Management.ManagementClass(wmiClass)
        Dim moc As System.Management.ManagementObjectCollection = mc.GetInstances
        Dim mo As System.Management.ManagementObject

        For Each mo In moc
            'Only get the first one
            If Result = "" Then
                Try
                    Result = mo(wmiProperty).ToString
                    Exit For
                Catch ex As Exception
                    'Ignore error
                End Try
            End If
        Next mo

        Return Result
    End Function

    Function cek_registrasi() As Boolean
        Try
            cek_registrasi = True
            If IO.File.Exists(My.Application.Info.DirectoryPath & "\id.reg") And IO.File.Exists(My.Application.Info.DirectoryPath & "\serial.reg") Then
                If Pack(EncryptText(TxtID.Text & GetFileContents(My.Application.Info.DirectoryPath & "\serial.reg"))) = GetFileContents(My.Application.Info.DirectoryPath & "\id.reg") Then
                    cek_registrasi = True
                Else
                    cek_registrasi = False
                End If
            Else
                cek_registrasi = False
            End If

            Return cek_registrasi
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetFileContents(ByVal FullPath As String, _
              Optional ByRef ErrInfo As String = "") As String

        Dim strContents As String
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
            'GetFileContents = strContents
        Catch Ex As Exception
            Return ""
            ErrInfo = Ex.Message
        End Try
    End Function
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LblRegister.Visible = False

            Dim _time_awal As Date
            Dim _time_akhir As Date
            _time_awal = Now
            TxtID.Text = cek_hw()
            _time_akhir = Now
            Dim _eksekusi As Double = DateDiff(DateInterval.Second, _time_awal, _time_akhir)
            LblTimeLoad.Text = _eksekusi

            If cek_registrasi() = True Then
                LblRegister.Visible = False
                FPass.Show()
                'Info.Show()
                'Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
                'With myprocess
                '    .StartInfo.FileName = My.Application.Info.DirectoryPath & "\data.dump"
                '    .StartInfo.RedirectStandardOutput = False
                '    .StartInfo.UseShellExecute = False
                '    '.StartInfo.Arguments = "a " & ControlChars.Quote & file_tujuan & ControlChars.Quote _
                '    '    & " " & ControlChars.Quote & folder_asal & " " & opsi & ControlChars.Quote   'a c:\hasil\test.7z C:\temp\*.* -r

                '    .StartInfo.CreateNoWindow = True
                '    .StartInfo.UseShellExecute = False
                '    .StartInfo.WindowStyle = ProcessWindowStyle.Hidden

                '    .Start()
                '    '.WaitForExit()
                '    '.Close()
                '    Me.Close()
                'End With
                Me.Close()
            Else
                'LblRegister.Visible = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub BtBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtBatal.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        TxtLisensi.Text = Pack(EncryptText(TxtID.Text & TxtSerial.Text))
    End Sub

    Private Sub BtRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtRegister.Click
        Try
            If TxtLisensi.Text = Pack(EncryptText(TxtID.Text.ToUpper & TxtSerial.Text.ToUpper)) Then
                If SaveTextToFile(TxtLisensi.Text.ToUpper, My.Application.Info.DirectoryPath & "\id.reg") = False Or SaveTextToFile(TxtSerial.Text.ToUpper, My.Application.Info.DirectoryPath & "\serial.reg") = False Then
                    MsgBox("Penyimpanan file registrasi gagal", MsgBoxStyle.Information, "Registrasi")
                    Exit Sub
                Else
                    MsgBox("Registrasi berhasil.", MsgBoxStyle.Information, "Registrasi")
                    FPass.Show()
                    'Info.Show()
                    Me.Close()
                    'Dim myprocess As System.Diagnostics.Process = New System.Diagnostics.Process()
                    'With myprocess
                    '    .StartInfo.FileName = My.Application.Info.DirectoryPath & "\data.dump"
                    '    .StartInfo.RedirectStandardOutput = False
                    '    .StartInfo.UseShellExecute = False
                    '    '.StartInfo.Arguments = "a " & ControlChars.Quote & file_tujuan & ControlChars.Quote _
                    '    '    & " " & ControlChars.Quote & folder_asal & " " & opsi & ControlChars.Quote   'a c:\hasil\test.7z C:\temp\*.* -r

                    '    .StartInfo.CreateNoWindow = True
                    '    .StartInfo.UseShellExecute = False
                    '    .StartInfo.WindowStyle = ProcessWindowStyle.Hidden

                    '    .Start()
                    '    '.WaitForExit()
                    '    '.Close()
                    '    Me.Close()
                    'End With
                End If
            Else
                MsgBox("Maaf, registrasi belum berhasil. Silahkan ulangi lagi  ", MsgBoxStyle.Information, "Registrasi")
            End If
        Catch ex As Exception
            'Pesan(Err)
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Shared Function SaveTextToFile(ByVal strData As String, _
    ByVal FullPath As String, _
      Optional ByVal ErrInfo As String = "") As Boolean

        'Dim Contents As String
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Try
            objReader = New StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message

        End Try
        Return bAns
    End Function

    Private Sub Btcopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btcopy.Click
        Try
            Clipboard.SetText(TxtID.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPaste.Click
        Try
            TxtLisensi.Text = Clipboard.GetText
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtAmbilLisensi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAmbilLisensi.Click
        Try
            Dim _hasil As String = ""
            Dim _html As String
            ' Dim _url_awal As String = GetFileContents(appbase() & "\filekonfigurasi\conf_system.txt")
            Dim url As String = "http://vpnsygma.ddns.net:84/keygen_exapro/keygen_new.php?hardid=$hardid&cdcode=$cdcode" 'konfigurasi(_url_awal, "exc_rate_reff") & exr_start_date.DateTime.ToString("yyy-MM-dd")
            ' Creates an HttpWebRequest for the specified URL.
            url = url.Replace("$hardid", TxtID.Text.ToUpper).Replace("$cdcode", TxtSerial.Text.ToUpper)

            Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            ' Sends the request and waits for a response.
            Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)

            ' Calls the method GetResponseStream to return the stream associated with the response.
            Dim receiveStream As Stream = myHttpWebResponse.GetResponseStream()
            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")

            ' Pipes the response stream to a higher level stream reader with the required encoding format.
            Dim readStream As New StreamReader(receiveStream, encode)

            Dim EOF As Boolean = False
            _html = ""

            Do While EOF = False

                Dim contents As String = readStream.ReadLine
                'If InStr(contents, "textbar") <> 0 Then
                _html = contents
                ' End If
                If readStream.EndOfStream = True Then
                    EOF = True
                End If
            Loop

            ' Releases the resources of the Stream.
            readStream.Close()
            ' Releases the resources of the response.
            myHttpWebResponse.Close()

            _hasil = _html

            TxtLisensi.Text = _hasil

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
