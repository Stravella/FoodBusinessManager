Imports Entidades
Imports BLL

Public Class OlvidoContraseña

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnRecuperarContraseña_Click(sender As Object, e As EventArgs) Handles btnRecuperarContraseña.Click
        Dim oUsuario As New UsuarioDTO With {.username = txtUsuario.Text,
                                             .mail = txtMail.Text}
        'Valido User
        If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(oUsuario) = True Then
            'Valido Mail
            If UsuarioBLL.ObtenerInstancia.ChequearExistenciaMail(oUsuario) = True Then
                'Valido que el mail corresponda al usuario
                If UsuarioBLL.ObtenerInstancia.ValidarUserMail(oUsuario) = True Then
                    'Genero el token y lo bloqueo
                    oUsuario = UsuarioBLL.ObtenerInstancia.RecuperarUsuario(oUsuario)
                    'GestorMailBLL.ObtenerInstancia.EnviarMail(oUsuario, True)
                    lblRespuesta.Text = "Se envio un correo a su casilla"
                Else
                    dvMensaje.Visible = True
                    lblRespuesta.Text = "El usuario y mail ingresado no corresponden"
                End If
            Else
                dvMensaje.Visible = True
                lblRespuesta.Text = "El mail no existe"
            End If
        Else
            dvMensaje.Visible = True
            lblRespuesta.Text = "El nombre de usuario no existe"
        End If
    End Sub
End Class