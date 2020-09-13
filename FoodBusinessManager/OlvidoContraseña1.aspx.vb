Imports Entidades
Imports BLL
Public Class OlvidoContraseña1
    Inherits System.Web.UI.Page

#Region "Mensajes"
    Public Enum TipoAlerta
        Success
        Info
        Warning
        Danger
    End Enum

    Public Sub MostrarMensaje(mensaje As String, tipo As TipoAlerta)
        Dim panelMensaje As Panel = Master.FindControl("Mensaje")
        Dim labelMensaje As Label = panelMensaje.FindControl("labelMensaje")

        labelMensaje.Text = mensaje
        panelMensaje.CssClass = String.Format("alert alert-{0} alert-dismissable", tipo.ToString.ToLower())
        panelMensaje.Style.Add("z-index", "1000")
        panelMensaje.Attributes.Add("role", "alert")
        panelMensaje.Visible = True
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnEnviarMail_Click(sender As Object, e As EventArgs) Handles btnEnviarMail.Click
        Try
            Dim usuario As New UsuarioDTO With {.mail = txtMail.Text}
            usuario = UsuarioBLL.ObtenerInstancia.ObtenerPorMail(usuario.mail)

            Dim ActiveURL = "https://" & Request.Url.Host & ":" & Request.Url.Port & "/" & "ModificarContraseña.aspx?clave=" + Server.UrlEncode(CriptografiaBLL.ObtenerInstancia.EncriptarSimetrico(usuario.mail))
            GestorMailBLL.ObtenerInstancia.EnviarMail(usuario.mail, "Cambio de contraseña en Food BusinessManager.", "Hola " + usuario.username + ", le enviamos los pasos para cambiar su contraseña. <br /><br />" + vbCrLf + "Ingresá al siguiente link para modificarla: " + "<a href=" + ActiveURL + ">link</a>" + "<br /><br /> Si no te funciona el link, copia y pega esta dirección: " + ActiveURL, Server.MapPath("\EmailTemplates\Template_mail.html"))
            MostrarMensaje("Se envió un mail a su casilla", TipoAlerta.Success)
            Response.Redirect("/Home.aspx")
        Catch ex As Exception
            MostrarMensaje("Lo siento! Ocurrio un error", TipoAlerta.Danger)
        End Try
    End Sub
End Class