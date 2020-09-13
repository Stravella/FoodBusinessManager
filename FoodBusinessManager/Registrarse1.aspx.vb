Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Web.HttpContext
Imports BLL
Imports Entidades
Public Class Registrarse1
    Inherits System.Web.UI.Page

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
        If Not IsPostBack() Then
            CargarProvincias()
        End If
    End Sub

    Private Sub CargarProvincias()
        Try
            Dim ls As List(Of ProvinciaDTO) = ProvinciaBLL.ObtenerInstancia.Listar
            ddlProvincias.DataSource = ls
            ddlProvincias.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRegistrarse_Click(sender As Object, e As EventArgs) Handles btnRegistrarse.Click
        Try
            If chkTyC.Checked = True Then
                If Me.IsReCaptchaValid() Then
                    Dim usuario As New UsuarioDTO With {
                            .nombre = txtNombre.Text,
                            .apellido = txtApellido.Text,
                            .username = txtUsuario.Text,
                            .dni = txtDNI.Text,
                            .mail = txtMail.Text,
                            .fechaCreacion = Now(),
                            .password = CriptografiaBLL.ObtenerInstancia.Cifrar(txtContraseña.Text),
                            .intentos = 0,
                            .bloqueado = 1,
                            .perfil = New PerfilCompuesto With {.id_permiso = 18}
                        }
                    Dim cliente As New ClienteDTO With {
                            .usuario = usuario,
                            .CUIT = txtCUIT.Text,
                            .RazonSocial = txtRazonSocial.Text,
                            .domicilio = txtDireccion.Text,
                            .localidad = txtLocalidad.Text,
                            .CP = txtCP.Text,
                            .estado = New EstadoClienteDTO With {.id = 1, .descripcion = "Activo"},
                            .provincia = ddlProvincias.SelectedValue,
                            .aceptaNewsletter = False,
                            .telefono = txtTelefono.Text
                    }
                    If UsuarioBLL.ObtenerInstancia.ChequearExistenciaMail(usuario) Then
                        MostrarMensaje("El mail ya se encuentra registrado!", TipoAlerta.Danger)
                    Else
                        If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) Then
                            MostrarMensaje("El usuario ya se encuentra registrado!", TipoAlerta.Danger)
                        Else
                            ClienteBLL.ObtenerInstancia.Agregar(cliente)
                            Dim ActiveURL = "https://" & Request.Url.Host & ":" & Request.Url.Port & "/" & "ConfirmarContraseña.aspx?clave=" + Server.UrlEncode(CriptografiaBLL.ObtenerInstancia.EncriptarSimetrico(usuario.mail))
                            GestorMailBLL.ObtenerInstancia.EnviarMail(usuario.mail, "Bienvenido a Food BusinessManager.", "Hola " + cliente.RazonSocial + ", ya sos usuario. <br /><br />" + vbCrLf + "Ingresá al siguiente link para activarlo: " + "<a href=" + ActiveURL + ">link</a>" + "<br /><br /> Si no te funciona el link, copia y pega esta dirección: " + ActiveURL, Server.MapPath("\EmailTemplates\Template_mail.html"))

                            Dim bitacora As New BitacoraDTO With {
                                .FechaHora = Now(),
                                .usuario = cliente.usuario,
                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 10}, 'Suceso: creacion cliente
                                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                                .observaciones = "Se creo el cliente :" & cliente.usuario.nombre & cliente.usuario.apellido & "de usuario: " & cliente.usuario.username
                            }
                            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
                            MostrarMensaje("Se envio un correo a su casilla de mail", TipoAlerta.Success)
                        End If
                    End If
                Else
                    MostrarMensaje("El captcha no es válido", TipoAlerta.Danger)
                End If
            Else
                MostrarMensaje("Debe aceptar los terminos condiciones", TipoAlerta.Info)
            End If
        Catch ex As Exception
            Dim bitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 10}, 'Suceso: creacion cliente
                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                .observaciones = ex.Message
            }
            '.usuario = cliente.usuario,
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            MostrarMensaje("Lo siento! Ocurrio un error, contacte a su administrador", TipoAlerta.Danger)

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