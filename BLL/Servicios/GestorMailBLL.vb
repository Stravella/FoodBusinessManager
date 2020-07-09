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

    Public Sub EnviarMail(usuario As UsuarioDTO, esRecupero As Boolean)
        Try
            'Busco la configuracion.  
            Dim emailSender As String = ConfigurationManager.AppSettings("emailsender").ToString()
            Dim emailSenderHost As String = ConfigurationManager.AppSettings("smtp").ToString()
            Dim emailSenderPort As String = Convert.ToInt16(ConfigurationManager.AppSettings("portnumber"))
            Dim emailIsSSL As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("IsSSL"))
            Dim Str As StreamReader
            Dim _mailmsg As New MailMessage
            Dim subject As String
            'Estos templates hay que actualizarlos segun donde se encuentre el repo.
            If esRecupero = True Then
                Str = New StreamReader("C:\Users\Seba\Source\Repos\FoodBusinessManager\FoodBusinessManager\EmailTemplates\RecoverPassword.html")
                subject = "Recupero contraseña - Food Business Manager"
            Else
                Str = New StreamReader("C:\Users\Seba\Source\Repos\FoodBusinessManager\FoodBusinessManager\EmailTemplates\SignUp.html")
                subject = "Bienvenido a Food Business Manager!"
            End If
            'Busco el Body    
            Dim MailText As String = Str.ReadToEnd()
            Str.Close()
            'Reemplazo Nombre y contraseña
            MailText = MailText.Replace("[newusername]", usuario.nombre.Trim + " " + usuario.apellido.Trim)
            MailText = MailText.Replace("[newtoken]", usuario.password.Trim)

            'Configuro el mail
            _mailmsg.IsBodyHtml = True
            _mailmsg.From = New MailAddress(emailSender)
            _mailmsg.To.Add(usuario.mail.ToString)
            _mailmsg.Subject = subject
            _mailmsg.Body = MailText

            Dim _smtp As New SmtpClient
            _smtp.Host = emailSenderHost
            _smtp.Port = emailSenderPort
            _smtp.EnableSsl = emailIsSSL
            Dim emailSenderPassword As String = ConfigurationManager.AppSettings("password").ToString()
            Dim _network As NetworkCredential = New NetworkCredential(emailSender, emailSenderPassword)
            _smtp.Credentials = _network
            'Envio el mail
            _smtp.Send(_mailmsg)
        Catch ex As Exception

        End Try
    End Sub

End Class
