Imports System.Security.Cryptography
Imports System.Text
Imports System.Configuration
Imports DAL
Imports System.IO
Imports System.ComponentModel

Public Class CriptografiaBLL

    Private Shared _instancia As CriptografiaBLL
    Public Shared Function ObtenerInstancia() As CriptografiaBLL
        If _instancia Is Nothing Then
            _instancia = New CriptografiaBLL
        End If
        Return _instancia
    End Function


    Public Function Cifrar(ByVal value As String) As String
        Dim str As String = ""
        Try
            Dim Ue As UnicodeEncoding = New UnicodeEncoding()
            Dim ByteSourceText As Byte() = Ue.GetBytes(value)
            Dim Md5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
            Dim ByteHash As Byte() = Md5.ComputeHash(ByteSourceText)
            Md5.Clear()
            str = Convert.ToBase64String(ByteHash)
        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
        Return str
    End Function

    Public Function EncriptToURL(ByVal value As String) As String
        Dim hash As String = Me.Cifrar(value)
        Return hash.Replace("+", "_")

    End Function

    Public Function CompararHash(ByVal hash1 As String, ByVal hash2 As String) As Boolean
        Return hash1.Equals(hash2)
    End Function

    Public Function EncriptarSimetrico(ByVal paramTexto As String) As String
        Dim CyphMode As CipherMode = CipherMode.ECB
        Dim Key As String = ConfigurationManager.AppSettings("ClavePrivada")
        Try
            Dim Des As New TripleDESCryptoServiceProvider
            Dim InputbyteArray() As Byte = Encoding.Default.GetBytes(paramTexto)
            Dim hashMD5 As New MD5CryptoServiceProvider
            Des.Key = hashMD5.ComputeHash(Encoding.Default.GetBytes(Key))
            Des.Mode = CyphMode
            Dim ms As MemoryStream = New MemoryStream
            Dim cs As CryptoStream = New CryptoStream(ms, Des.CreateEncryptor(), CryptoStreamMode.Write)
            cs.Write(InputbyteArray, 0, InputbyteArray.Length)
            cs.FlushFinalBlock()
            Dim ret As StringBuilder = New StringBuilder
            Dim b() As Byte = ms.ToArray
            ms.Close()
            Dim I As Integer
            For I = 0 To UBound(b)
                ret.AppendFormat("{0:X2}", b(I))
            Next
            Return ret.ToString
        Catch ex As System.Security.Cryptography.CryptographicException
            Throw New Exception
        End Try
    End Function


    Public Function Desencriptar(ByVal paramTexto As String) As String
        Dim CyphMode As CipherMode = CipherMode.ECB
        Dim Key As String = ConfigurationManager.AppSettings("ClavePrivada")
        Try
            If paramTexto = String.Empty Then
                Return ""
            Else
                Dim Des As New TripleDESCryptoServiceProvider
                Dim InputbyteArray(CType(paramTexto.Length / 2 - 1, Integer)) As Byte
                Dim hashMD5 As New MD5CryptoServiceProvider
                Des.Key = hashMD5.ComputeHash(Encoding.Default.GetBytes(Key))
                Des.Mode = CyphMode
                Dim X As Integer
                For X = 0 To InputbyteArray.Length - 1
                    Dim IJ As Int32 = (Convert.ToInt32(paramTexto.Substring(X * 2, 2), 16))
                    Dim BT As New ByteConverter
                    InputbyteArray(X) = New Byte
                    InputbyteArray(X) = CType(BT.ConvertTo(IJ, GetType(Byte)), Byte)
                Next
                Dim ms As MemoryStream = New MemoryStream
                Dim cs As CryptoStream = New CryptoStream(ms, Des.CreateDecryptor(), CryptoStreamMode.Write)
                cs.Write(InputbyteArray, 0, InputbyteArray.Length)
                cs.FlushFinalBlock()
                Dim ret As StringBuilder = New StringBuilder
                Dim B() As Byte = ms.ToArray
                ms.Close()
                Dim I As Integer
                For I = 0 To UBound(B)
                    ret.Append(Chr(B(I)))
                Next
                Return ret.ToString
            End If
        Catch ex As System.Security.Cryptography.CryptographicException
            Throw New Exception
        End Try
    End Function


End Class
