Imports Entidades
Imports BLL
Public Class OlvidoContraseña1
    Inherits System.Web.UI.Page

#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Alert, Warning, Info, Success
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnEnviarMail_Click(sender As Object, e As EventArgs) Handles btnEnviarMail.Click
        Try
            Dim usuario As New UsuarioDTO With {.mail = txtMail.Text}
            'If UsuarioBLL.ObtenerInstancia.ChequearExistenciaMail(usuario) = True Then 'TODO: Se comentar para probar isn la validacion del mail
            usuario = UsuarioBLL.ObtenerInstancia.ObtenerPorMail(usuario)
                UsuarioBLL.ObtenerInstancia.RecuperarUsuario(usuario)
                MostrarMensaje("Se envió un mail a su casilla", "Success")
            'Else
            'MostrarMensaje("El mail no existe", "Danger")
            'End If
        Catch ex As Exception
            MostrarMensaje("Lo siento! Ocurrio un error", "Danger")
        End Try
    End Sub
End Class