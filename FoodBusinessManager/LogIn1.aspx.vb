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
                usuarioLogeado = BLL.UsuarioBLL.ObtenerInstancia.LogIn(usuario)
                If usuarioLogeado Is Nothing Then 'El usuario existe pero no corresponde la contraseña
                    MostrarMensaje("<strong> Error </strong> La contraseña es incorrecta", TipoAlerta.Danger)
                Else
                    If usuarioLogeado.bloqueado = True Then 'La contraseña responde, pero el usuario está bloqueado -> Solicito cambio contraseña
                        Current.Session("Cliente") = usuarioLogeado
                        Response.Redirect("/ModificarContraseña.aspx")
                    Else
                        Current.Session("Cliente") = usuarioLogeado
                        'Grabo Bitacora - Suceso Login = 1
                        Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 1},
                                                .usuario = usuarioLogeado,
                                                .observaciones = ""
                                                }
                        BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)

                        Response.Redirect("Default1.aspx", False)
                    End If
                End If
            Else
                MostrarMensaje("<strong> Error </strong> El usuario no existe", TipoAlerta.Danger)
            End If
        Catch ex As Exception
            'Grabo Bitacora - Suceso Login = 1

        End Try
    End Sub

    Private Sub btnRegistrarse_Click(sender As Object, e As EventArgs) Handles btnRegistrarse.Click
        Response.Redirect("/Registrarse1.aspx")
    End Sub
End Class