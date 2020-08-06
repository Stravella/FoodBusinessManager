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
                                            .intentos = 0,
                                            .bloqueado = 1,
                                            .fechaCreacion = Now(),
                                            .password = UsuarioBLL.ObtenerInstancia.GenerarToken(),
                                            .SALT = DigitoVerificadorBLL.ObtenerInstancia.ObtenerSALT(),
                                            .perfil = New PerfilCompuesto With {.id_permiso = 18}, 'TODO: ¿Con qué perfil lo inicio? Esta en Admin por defeault
                                            .idioma = New IdiomaDTO With {.id_idioma = "es-AR"}
                                            }
        'Chequeo que el usuario no exista
        If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) = True Then
            dvMensaje.Visible = True
            lblRespuesta.Text = "El nombre de usuario ya está siendo utilizado"
        Else
            'Guardo la password sin encriptar para enviar por mail
            Dim passwordDesencriptada As String = usuario.password
            usuario.password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(usuario.password & usuario.SALT)
            'TODO: Activar esta validacion para cuando terminen las pruebas
            'Chequeo que el mail no exista
            'If UsuarioBLL.ObtenerInstancia.ChequearExistenciaMail(usuario) = True Then
            '    dvMensaje.Visible = True
            '    lblRespuesta.Text = "El mail ya está siendo utilizado"
            'Else
            usuario = UsuarioBLL.ObtenerInstancia.AgregarUsuario(usuario)
            'Seteo la pwd vieja para enviar por mail
            usuario.password = passwordDesencriptada
            GestorMailBLL.ObtenerInstancia.EnviarMail(usuario, False)
            'TODO: Mostrar un modal 
            Response.Redirect("LogIn.aspx")
            'End If
        End If

    End Sub
End Class