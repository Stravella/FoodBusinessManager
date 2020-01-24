Imports Entidades
Imports BLL
Imports System.Web.HttpContext

Public Class LogIn
    Inherits System.Web.UI.Page
    Private usuarioLogeado As New UsuarioDTO

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim usuario As New UsuarioDTO
        Try
            'Validar los formatos de lo ingresado
            usuario.username = txtUser.Text
            usuario.password = txtPassword.Text

            'Verifico que exista el usuario
            If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) = True Then

                usuarioLogeado = UsuarioBLL.ObtenerInstancia.LogIn(usuario)

                'esto no funciona, da NullReferenceException
                If usuarioLogeado.username Is Nothing Then 'El usuario existe pero la contraseña no corresponde
                    dvMensaje.Visible = True
                    lblRespuesta.Text = "La contraseña es incorrecta"
                Else
                    If usuarioLogeado.bloqueado = True Then
                        dvMensaje.Visible = True
                        Response.Redirect("NuevaContraseña.aspx", False)
                    Else
                        'Camino feliz
                        DigitoVerificadorBLL.ObtenerInstancia.VerificarIntegridad()
                        'Grabo Bitacora - Suceso Login = 1
                        Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 1},
                                                .usuario = usuarioLogeado,
                                                .ValorAnterior = "",
                                                .NuevoValor = "",
                                                .observaciones = "",
                                                .DVH = ""}
                        BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)

                        Current.Session("usuario") = usuarioLogeado

                        Response.Redirect("Default.aspx", False)
                    End If
                End If
                'Si el usuario no existe
            Else
                dvMensaje.Visible = True
                lblRespuesta.Text = "El usuario no existe"
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class