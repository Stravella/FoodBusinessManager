Imports System.IO
Imports System.Net
Imports System.Web.HttpContext
Imports BLL
Imports Entidades
Public Class ConfirmarContraseña
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

    Protected Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Dim mailURL As String = Request.QueryString("clave")
        mailURL = CriptografiaBLL.ObtenerInstancia.Desencriptar(mailURL)

        Dim usuario As UsuarioDTO = UsuarioBLL.ObtenerInstancia.ObtenerPorMail(mailURL)

        If usuario.password = CriptografiaBLL.ObtenerInstancia.Cifrar(txtConfirmarContraseña.Text) Then
            usuario.intentos = 0
            usuario.bloqueado = False
            UsuarioBLL.ObtenerInstancia.ModificarUsuario(usuario)
            Dim bitacora As New BitacoraDTO With {
            .FechaHora = Now(),
            .usuario = usuario,
            .tipoSuceso = New SucesoBitacoraDTO With {.id = 1}, 'Suceso: logeo cliente
            .criticidad = New CriticidadDTO With {.id = 1}, 'Criticidad: baja
            .observaciones = "Se confirmo la contraseña dle usuario:" & usuario.username & "y logeo por primera vez"
        }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            Current.Session("Cliente") = ClienteBLL.ObtenerInstancia.ObtenerPorUsuario(usuario)
            'TODO: esto redirige a HOME
            Response.Redirect("Home.aspx")
        Else
            MostrarMensaje("La contraseña ingresada no coincide!", TipoAlerta.Danger)
        End If

    End Sub


End Class