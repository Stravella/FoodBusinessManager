Imports Entidades
Imports BLL
Imports System.Web.HttpContext
Public Class ModificarUsuario
    Inherits System.Web.UI.Page
    Dim idiomas As List(Of IdiomaDTO)
    Dim perfiles As List(Of PermisoComponente)
    Dim usuarios As List(Of UsuarioDTO)

#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Danger,Success,Warning,Info
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            CargarCulturasCreadas()
            CargarListaPerfiles()
            CargarUsuarios()
        End If
    End Sub

    Private Sub CargarCulturasCreadas()
        Try
            idiomas = IdiomaBLL.ObtenerInstancia.Listar()
            Session("Idiomas") = idiomas
            lstIdioma.DataTextField = "nombre"
            lstIdioma.DataValueField = "id_idioma"
            lstIdioma.DataSource = idiomas
            lstIdioma.DataBind()
        Catch ex As Exception
            MostrarMensaje("Error al cargar la lista de idiomas", "Danger")
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

    Protected Sub CargarUsuarios()
        Try
            usuarios = UsuarioBLL.ObtenerInstancia.ListarUsuarios()
            Session("Usuarios") = usuarios
            lstUsuarios.DataSource = usuarios
            lstUsuarios.DataTextField = "username"
            lstUsuarios.DataValueField = "id"
            lstUsuarios.DataBind()
            lstUsuarios.SelectedIndex = 0
        Catch ex As Exception
            MostrarMensaje("Error al cargar usuarios", "Danger")
        End Try
    End Sub

    Private Sub lstUsuarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUsuarios.SelectedIndexChanged
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
                IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If
            usuarios = Session("Usuarios")
            Dim usuarioSeleccionado As UsuarioDTO = usuarios(lstUsuarios.SelectedIndex)
            If Not usuarioSeleccionado Is Nothing Then
                txtApellido.Text = usuarioSeleccionado.apellido
                txtContraseña.Text = usuarioSeleccionado.password
                txtMail.Text = usuarioSeleccionado.mail
                txtNombre.Text = usuarioSeleccionado.nombre
                txtUsuario.Text = usuarioSeleccionado.username
                txtUsuario.Enabled = False
                If usuarioSeleccionado.bloqueado = True Then
                    chkBloqueado.Checked = True
                Else
                    chkBloqueado.Checked = False
                End If
                lstIdioma.SelectedValue = usuarioSeleccionado.idioma.id_idioma
                lstPerfil.SelectedValue = usuarioSeleccionado.perfil.id_permiso
            End If
        Catch ex As Exception
            MostrarMensaje("Error al seleccionar usuario", "Danger")
        End Try
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
                IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If
            usuarios = Session("Usuarios")
            idiomas = Session("Idiomas")
            perfiles = Session("Perfiles")
            Dim usuarioSeleccionado As UsuarioDTO = usuarios(lstUsuarios.SelectedIndex)
            Dim usuarioModificado As New UsuarioDTO With {.id = lstUsuarios.SelectedValue,
                .username = usuarioSeleccionado.username, 'El nombre de usuario no lo puede modificar
                .password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(txtContraseña.Text),
                .apellido = txtApellido.Text,
                .nombre = txtNombre.Text,
                .mail = txtMail.Text,
                .fechaCreacion = usuarioSeleccionado.fechaCreacion,
                .intentos = usuarioSeleccionado.intentos,
                .idioma = idiomas(lstIdioma.SelectedIndex),
                .perfil = perfiles(lstPerfil.SelectedIndex),
                .SALT = usuarioSeleccionado.SALT
                }
            If chkBloqueado.Checked = True Then
                usuarioModificado.bloqueado = True
            Else
                usuarioModificado.bloqueado = False
                usuarioModificado.intentos = 0
            End If
            UsuarioBLL.ObtenerInstancia.ModificarUsuario(usuarioModificado)
            Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Suceso 8: Modificacion de usuario
                                                    .usuario = Current.Session("Cliente"),
                                                    .ValorAnterior = usuarioSeleccionado.ToString,
                                                    .NuevoValor = usuarioModificado.ToString,
                                                    .observaciones = "Se modifico el usuario " & usuarioModificado.username
                                                    }
            BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
            MostrarMensaje("Se ha creado el usuario", "Success")
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim usuarioSeleccionado As UsuarioDTO = usuarios(lstUsuarios.SelectedIndex)
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = Current.Session("cliente"),
                .ValorAnterior = "",
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Suceso 8: Modificacion de usuario
                .observaciones = "Error modificando usuario " & usuarioSeleccionado.username
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", "Danger")
        End Try
    End Sub


End Class