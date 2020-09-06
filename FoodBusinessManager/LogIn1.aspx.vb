Imports Entidades
Imports BLL
Imports System.Web.HttpContext
Public Class LogIn1
    Inherits System.Web.UI.Page
    Private usuarioLogeado As New UsuarioDTO

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

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim usuario As New UsuarioDTO
            usuario.username = txtUsuario.Text
            usuario.password = txtContraseña.Text
            If BLL.UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) Then
                usuarioLogeado = UsuarioBLL.ObtenerInstancia.LogIn(usuario)
                If usuarioLogeado Is Nothing Then 'El usuario existe pero no corresponde la contraseña
                    MostrarMensaje("<strong> Error </strong> La contraseña es incorrecta", TipoAlerta.Danger)
                Else
                    If usuarioLogeado.bloqueado = True Then 'La contraseña responde, pero el usuario está bloqueado -> Solicito cambio contraseña
                        MostrarMensaje("<strong> Error </strong> El usuario se encuentra bloqueado! Debe recuperar su contraseña", TipoAlerta.Danger)
                    Else
                        Current.Session("Cliente") = ClienteBLL.ObtenerInstancia.ObtenerPorUsuario(usuarioLogeado)
                        'Grabo Bitacora - Suceso Login = 1
                        Dim bitacora As New BitacoraDTO With {
                                .FechaHora = Now(),
                                .usuario = usuarioLogeado,
                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 1}, 'Suceso: creacion cliente
                                .criticidad = New CriticidadDTO With {.id = 1}, 'Criticidad: media
                                .observaciones = "Se logeo el usuario :" & usuarioLogeado.username
                            }
                        BitacoraBLL.ObtenerInstancia.Agregar(bitacora)

                        Response.Redirect("Default1.aspx", False)
                    End If
                End If
            Else
                MostrarMensaje("<strong> Error </strong> El usuario no existe", TipoAlerta.Danger)
            End If
        Catch ex As Exception
            Dim bitacora As New BitacoraDTO With {
            .FechaHora = Now(),
            .usuario = usuarioLogeado,
            .tipoSuceso = New SucesoBitacoraDTO With {.id = 1}, 'Suceso: Logeo
            .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
            .observaciones = "Ocurrio un error al logear el usuario :" & usuarioLogeado.username & ". Error: " & ex.Message
             }
            MostrarMensaje("<strong> Lo siento! </strong> Ocurrio un error inesperado.", TipoAlerta.Danger)
        End Try
    End Sub

    Private Sub btnRegistrarse_Click(sender As Object, e As EventArgs) Handles btnRegistrarse.Click
        Response.Redirect("/Registrarse1.aspx")
    End Sub
End Class