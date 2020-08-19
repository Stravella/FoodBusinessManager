Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Web.HttpContext
Imports BLL
Imports Entidades
Public Class Registrarse1
    Inherits System.Web.UI.Page

#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Alert, Warning, Info, Success
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnRegistrarse_Click(sender As Object, e As EventArgs) Handles btnRegistrarse.Click
        Try
            If chkTyC.Checked = True Then 'check de TyC
                'If Me.IsReCaptchaValid() Then
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
                If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) = True Then
                    MostrarMensaje("El usuario ya existe", "Danger")
                Else
                    If UsuarioBLL.ObtenerInstancia.ChequearExistenciaMail(usuario) = True Then
                        MostrarMensaje("El mail ya está siendo utilizado", "Danger")
                    Else
                        Dim passwordDesencriptada As String = usuario.password
                        usuario.password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(usuario.password & usuario.SALT) 'Guardo la password desencriptada para enviar por mail
                        usuario = UsuarioBLL.ObtenerInstancia.AgregarUsuario(usuario)
                        usuario.password = passwordDesencriptada 'Seteo la pwd vieja para enviar por mail
                        GestorMailBLL.ObtenerInstancia.EnviarMail(usuario, False)
                        MostrarMensaje("Usuario creado con éxito!", "Success")
                        Response.Redirect("LogIn.aspx")
                    End If
                End If
                'End If
            Else
                MostrarMensaje("Debe aceptar los terminos y condiciones", "Danger")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function IsReCaptchaValid() As Boolean
        Dim result = False
        '        Dim mensaje As String
        Dim captchaResponse = Me.Request("g-recaptcha-response")
        Dim secretKey = ConfigurationManager.AppSettings("reCAPTCHA")
        Dim apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}"
        Dim requestUri = String.Format(apiUrl, secretKey, captchaResponse)
        Dim request = CType(WebRequest.Create(requestUri), HttpWebRequest)
        Using response As WebResponse = request.GetResponse()

            Using stream As StreamReader = New StreamReader(response.GetResponseStream())
                Dim jResponse As JObject = JObject.Parse(stream.ReadToEnd())
                Dim isSuccess = jResponse.Value(Of Boolean)("success")
                result = If((isSuccess), True, False)
            End Using
        End Using

        Return result
    End Function

End Class