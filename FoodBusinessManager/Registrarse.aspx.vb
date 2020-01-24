Imports Entidades
Imports BLL

Public Class Registrarse
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegisterConfirm_Click(sender As Object, e As EventArgs) Handles btnRegisterConfirm.Click
        Dim usuario As New UsuarioDTO With {.nombre = txtNombre.Text,
                                            .apellido = txtApellido.Text,
                                            .username = txtUsuario.Text,
                                            .mail = txtMail.Text,
                                            .perfil = New PerfilCompuesto With {.id_permiso = 1},
                                            .idioma = New IdiomaDTO With {.id_idioma = "es-AR"}
                                            }
        'Chequeo que el usuario no exista
        If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) = True Then
            dvMensaje.Visible = True
            lblRespuesta.Text = "El nombre de usuario ya está siendo utilizado"
        Else
            'Chequeo que el mail no exista
            'If UsuarioBLL.ObtenerInstancia.ChequearExistenciaMail(usuario) = True Then
            '    dvMensaje.Visible = True
            '    lblRespuesta.Text = "El mail ya está siendo utilizado"
            'Else
            usuario = UsuarioBLL.ObtenerInstancia.AgregarUsuario(usuario)
            GestorMailBLL.ObtenerInstancia.EnviarMail(usuario, False)
            'TODO: Mostrar un modal 
            Response.Redirect("LogIn.aspx")
            'End If
        End If

    End Sub
End Class