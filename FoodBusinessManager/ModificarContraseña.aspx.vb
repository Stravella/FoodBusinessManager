Imports System.Web.HttpContext
Imports Entidades
Imports BLL

Public Class ModificarContraseña
    Inherits System.Web.UI.Page

#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Alert, Warning, Info, Success
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnCambiarContraseña_Click(sender As Object, e As EventArgs) Handles btnCambiarContraseña.Click
        Try
            Dim mailURL As String = Request.QueryString("clave")
            mailURL = CriptografiaBLL.ObtenerInstancia.Desencriptar(mailURL)
            Dim usuario As UsuarioDTO = UsuarioBLL.ObtenerInstancia.ObtenerPorMail(mailURL)

            If txtNuevaContraseña.Text = txtConfirmeContraseña.Text Then
                usuario.password = CriptografiaBLL.ObtenerInstancia.Cifrar(txtNuevaContraseña.Text)
                usuario.intentos = 0
                usuario.bloqueado = 0
                UsuarioBLL.ObtenerInstancia.ModificarUsuario(usuario)
                Dim bitacora As New BitacoraDTO With {
                                .FechaHora = Now(),
                                .usuario = usuario,
                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Suceso: modificacion usuario
                                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                                .observaciones = "Se modifico la contraseña del usuario :" & usuario.username
                            }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
                Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Tipo suceso: Modificacion usuario
                                                .usuario = usuario,
                                                .observaciones = "Se modifico la contraseña"
                                                }
                BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
                MostrarMensaje("Se modifico la contraseña", "Success")
                Response.Redirect("/Default1.aspx")
            Else
                MostrarMensaje("Las contraseñas no coinciden", "Warning")
            End If
        Catch ex As Exception
            Dim bitacora As New BitacoraDTO With {
                                .FechaHora = Now(),
                                .usuario = New UsuarioDTO With {.username = ""},
                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Suceso: modificacion usuario
                                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                                .observaciones = "Ocurrio un error :" & ex.Message
                            }
            MostrarMensaje("Lo siento! Ocurrio un error", "Warning")
        End Try
    End Sub
End Class