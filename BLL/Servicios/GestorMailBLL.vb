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

            Dim ruta As String = "C:\Users\Seba\Source\Repos\FoodBusinessManager\FoodBusinessManager\Imagenes\FBM_Logo_Mail.png"
            Dim img As LinkedResource = New LinkedResource(ruta, MediaTypeNames.Image.Jpeg)
            img.ContentId = "Pic1"

            e_mail.From = New MailAddress("Food.Business.Manager@gmail.com", "Food Business Manager")
            e_mail.To.Add(pDestino)
            e_mail.Subject = pAsunto
            e_mail.IsBodyHtml = True

            Dim sr = New StreamReader(pPath)
            Dim Plantilla As String = sr.ReadToEnd
            sr.Dispose()


            e_mail.Body = Plantilla.Replace("{Cuerpodelmail}", pCuerpo)

            Dim vistaAlternativa As AlternateView = AlternateView.CreateAlternateViewFromString(e_mail.Body, Nothing, MediaTypeNames.Text.Html)
            vistaAlternativa.LinkedResources.Add(img)

            e_mail.AlternateViews.Add(vistaAlternativa)

            Smtp_Server.Send(e_mail)

        Catch ex As Exception

        End Try

        Return True
    End Function

    Public Function EnviarMail(ByVal pDestino As String, pAsunto As String, pImagen As String, pCuerpo As String, ByVal pPath As String) As Boolean
        Try
            Dim Smtp_Server As New SmtpClient

            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("Food.Business.Manager@gmail.com", "fbm123456")

            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"
            e_mail = New MailMessage()

            'Imagen del header
            Dim ruta As String = "C:\Users\Seba\Source\Repos\FoodBusinessManager\FoodBusinessManager\Imagenes\FBM_Logo_Mail.png"
            Dim img As LinkedResource = New LinkedResource(ruta, MediaTypeNames.Image.Jpeg)
            img.ContentId = "Pic1"

            'Imagen del body
            Dim rutaImgBody As String = pImagen
            Dim img2 As LinkedResource = New LinkedResource(rutaImgBody, MediaTypeNames.Image.Jpeg)
            img.ContentId = "Pic2"

            e_mail.From = New MailAddress("Food.Business.Manager@gmail.com", "Food Business Manager")
            e_mail.To.Add(pDestino)
            e_mail.Subject = pAsunto
            e_mail.IsBodyHtml = True

            Dim sr = New StreamReader(pPath)
            Dim Plantilla As String = sr.ReadToEnd
            sr.Dispose()


            e_mail.Body = Plantilla.Replace("{Cuerpodelmail}", pCuerpo)

            Dim vistaAlternativa As AlternateView = AlternateView.CreateAlternateViewFromString(e_mail.Body, Nothing, MediaTypeNames.Text.Html)
            vistaAlternativa.LinkedResources.Add(img)
            vistaAlternativa.LinkedResources.Add(img2)

            e_mail.AlternateViews.Add(vistaAlternativa)

            Smtp_Server.Send(e_mail)

        Catch ex As Exception

        End Try

        Return True
    End Function

    'Private _ServerMapPathTemplate As String
    'Public Property ServerMapPathTemplate() As String
    '    Get
    '        Return _ServerMapPathTemplate
    '    End Get
    '    Set(ByVal value As String)
    '        _ServerMapPathTemplate = value
    '    End Set
    'End Property

    Public Function EnviarCorreo(pDestino As String, pAsunto As String, pCuerpo As String, linkDestino As String, pPath As String, Optional esfactura As Boolean = False, Optional ByVal adjuntos As Byte() = Nothing) As Boolean
        '
        ' el path template me lo traigo del formulario con: Server.MapPath("MailTemplate.html")
        Dim result As Boolean = False
        Dim _from As String = "Food.Business.Manager@gmail.com"
        Dim footer As String = linkDestino.Substring(0, linkDestino.LastIndexOf("/")) & "/DesubscribirNewsletter.aspx"
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.DeliveryMethod = SmtpDeliveryMethod.Network
            Smtp_Server.Credentials = New Net.NetworkCredential("Food.Business.Manager@gmail.com", "fbm123456")

            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"
            e_mail = New MailMessage()
            e_mail.From = New MailAddress(_from)
            e_mail.To.Add(pDestino)
            e_mail.Subject = pAsunto
            e_mail.IsBodyHtml = True
            If adjuntos IsNot Nothing Then
                e_mail.Attachments.Add(New Attachment(New MemoryStream(adjuntos), "FBM.pdf"))
            End If

            Dim sr = New StreamReader(pPath)
            Dim Plantilla As String = sr.ReadToEnd
            sr.Dispose()

            If esfactura Then
                e_mail.Body = pCuerpo
            Else
                e_mail.Body = Plantilla.Replace("Titulo", pAsunto).Replace("Cuerpo", pCuerpo).Replace("LINKDESTINO", linkDestino).Replace("LINKFOOTER", footer)
            End If

            Smtp_Server.Send(e_mail)
            result = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return result

    End Function


End Class
