Imports Entidades
Imports BLL

Public Class NuevaContraseña
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtPassword.Text = txtConfirmPassword.Text Then
            Dim oUsuario As New UsuarioDTO With {.username = txtUsuario.Text,
                                                 .password = txtPassword.Text}
            If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(oUsuario) = True Then
                oUsuario = UsuarioBLL.ObtenerInstancia.CambiarContraseña(oUsuario)
                lblRespuesta.Text = "Se modifico la contraseña con exito"
            Else
                lblRespuesta.Text = "El usuario es incorrecto"
            End If
        Else
            lblRespuesta.Text = "Las contraseñas no coinciden"
        End If
    End Sub
End Class