Imports Entidades
Imports BLL
Imports System.Web.HttpContext
Public Class AgregarUsuario
    Inherits System.Web.UI.Page
    Dim idiomas As List(Of IdiomaDTO)
    Dim perfiles As List(Of PermisoComponente)

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
        panelMensaje.Attributes.Add("role", "alert")
        panelMensaje.Visible = True
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCulturasCreadas()
            CargarListaPerfiles()
        End If
    End Sub

    Private Sub CargarCulturasCreadas()
        Try
            idiomas = IdiomaBLL.ObtenerInstancia.Listar()
            lstIdioma.DataTextField = "nombre"
            lstIdioma.DataValueField = "id_idioma"
            lstIdioma.DataSource = idiomas
            lstIdioma.DataBind()
        Catch ex As Exception
            MostrarMensaje("Error al cargar la lista de idiomas", TipoAlerta.Danger)
        End Try
    End Sub

    Protected Sub CargarListaPerfiles()
        perfiles = BLL.PermisoBLL.ObtenerInstancia.ListarPerfiles
        Session("Perfiles") = perfiles
        lstPerfil.DataSource = perfiles
        lstPerfil.DataTextField = "nombre"
        lstPerfil.DataValueField = "id_permiso"
        lstPerfil.DataBind()
        lstPerfil.SelectedIndex = 0
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            If chkTyC.Checked = True Then
                Dim IdiomaActual As New IdiomaDTO
                If IsNothing(Current.Session("Cliente")) Then
                    IdiomaActual.nombre = "Español"
                Else
                    IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
                End If
                Dim usuario As New UsuarioDTO With {.nombre = txtNombre.Text,
                     .apellido = txtApellido.Text,
                     .username = txtUsuario.Text,
                     .password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(txtContraseña.Text),
                     .mail = txtMail.Text,
                     .bloqueado = False,
                     .fechaCreacion = Now(),
                     .SALT = DigitoVerificadorBLL.ObtenerInstancia.ObtenerSALT(),
                     .idioma = New IdiomaDTO With {.id_idioma = lstIdioma.SelectedValue},
                     .perfil = New PerfilCompuesto With {.id_permiso = lstPerfil.SelectedValue}
                }
                If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) = False Then
                    UsuarioBLL.ObtenerInstancia.AgregarUsuario(usuario)
                    Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 7}, 'Suceso 7: Creacion de usuario
                                                        .usuario = Current.Session("Cliente"),
                                                        .ValorAnterior = "",
                                                        .NuevoValor = usuario.username,
                                                        .observaciones = "Se creo el usuario " & usuario.username
                                                        }
                    BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
                    MostrarMensaje("Se ha creado el usuario", TipoAlerta.Success)
                Else
                    MostrarMensaje("El usuario ya existe", TipoAlerta.Danger)
                End If
            Else
                MostrarMensaje("Debe aceptar los Términos y Condiciones", TipoAlerta.Info)
            End If
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = Current.Session("cliente"),
                .ValorAnterior = "",
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 7}, 'Suceso 7: Creacion de usuario
                .observaciones = "Error creando usuario "
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", TipoAlerta.Danger)
        End Try
    End Sub

End Class