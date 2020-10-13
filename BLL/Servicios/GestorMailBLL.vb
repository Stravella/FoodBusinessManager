Imports Entidades
Imports System.Net
Imports System.Net.Mail
Imports System.Configuration
Imports System.Web.Configuration
Imports System.IO

Public Class GestorMailBLL

#Region "Singleton"
    Private Shared _instancia As GestorMailBLL
    Public Shared Function ObtenerInstancia() As GestorMailBLL
        If _instancia Is Nothing Then
            _instancia = New GestorMailBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function EnviarMail(ByVal pDestino As String, pAsunto As String, pCuerpo As String, ByVal pPath As String) As Boolean
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("Food.Business.Manager@gmail.com", "fbm123456")

            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"
            e_mail = New MailMessage()
            e_mail.From = New MailAddress("Food.Business.Manager@gmail.com", "Food Business Manager")
            e_mail.To.Add(pDestino)
            e_mail.Subject = pAsunto
            e_mail.IsBodyHtml = True

            Dim sr = New StreamReader(pPath)
            Dim Plantilla As String = sr.ReadToEnd
            sr.Dispose()

            e_mail.Body = Plantilla.Replace("Cuerpodelmail", pCuerpo)
            Smtp_Server.Send(e_mail)

        Catch ex As Exception

        End Try

        Return True
    End Function

End Class
