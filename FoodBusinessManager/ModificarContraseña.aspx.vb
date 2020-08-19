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
            Dim usuario As UsuarioDTO = Current.Session("Cliente")
            If txtNuevaContraseña.Text = txtConfirmeContraseña.Text Then
                usuario.password = txtNuevaContraseña.Text
                UsuarioBLL.ObtenerInstancia.CambiarContraseña(usuario)
                UsuarioBLL.ObtenerInstancia.ModificarUsuario(usuario)
                Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Tipo suceso: Modificacion usuario
                                                .usuario = usuario,
                                                .ValorAnterior = txtContraseña.Text,
                                                .NuevoValor = txtNuevaContraseña.Text,
                                                .observaciones = "Se modifico la contraseña"
                                                }
                BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
                MostrarMensaje("Se modifico la contraseña", "Success")
                Response.Redirect("/Login1.aspx")
            Else
                MostrarMensaje("Las contraseñas no coinciden", "Warning")
            End If
        Catch ex As Exception
            Dim usuario As UsuarioDTO = Current.Session("Cliente")
            Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Tipo suceso: Modificacion usuario
                                                .usuario = usuario,
                                                .ValorAnterior = txtContraseña.Text,
                                                .NuevoValor = txtNuevaContraseña.Text,
                                                .observaciones = "Se modifico la contraseña"
                                                }
            Dim BitacoraError As New BitacoraErroresDTO With {.excepcion = ex.Message,
                        .stackTrace = ex.StackTrace
                        }
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, BitacoraError)
            MostrarMensaje("Lo siento! Ocurrio un error", "Warning")
        End Try
    End Sub
End Class