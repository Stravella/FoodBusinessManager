Imports Entidades
Imports BLL
Imports System.Web.HttpContext
Public Class ModificarUsuario
    Inherits System.Web.UI.Page
    Dim idiomas As List(Of IdiomaDTO)
    Dim perfiles As List(Of PermisoComponente)
    Dim usuarios As List(Of UsuarioDTO)

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
        If Not IsPostBack() Then
            CargarListaPerfiles()
            CargarUsuarios()
        End If
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
            MostrarMensaje("Error al cargar usuarios", TipoAlerta.Danger)
        End Try
    End Sub

    Private Sub lstUsuarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUsuarios.SelectedIndexChanged
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else

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

                lstPerfil.SelectedValue = usuarioSeleccionado.perfil.id_permiso
            End If
        Catch ex As Exception
            MostrarMensaje("Error al seleccionar usuario", TipoAlerta.Danger)
        End Try
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
            End If
            usuarios = Session("Usuarios")
            idiomas = Session("Idiomas")
            perfiles = Session("Perfiles")
            Dim usuarioSeleccionado As UsuarioDTO = usuarios(lstUsuarios.SelectedIndex)
            Dim usuarioModificado As New UsuarioDTO With {.id = lstUsuarios.SelectedValue,
                .username = usuarioSeleccionado.username, 'El nombre de usuario no lo puede modificar
                .password = CriptografiaBLL.ObtenerInstancia.Cifrar(txtContraseña.Text),
                .apellido = txtApellido.Text,
                .nombre = txtNombre.Text,
                .mail = txtMail.Text,
                .fechaCreacion = usuarioSeleccionado.fechaCreacion,
                .intentos = usuarioSeleccionado.intentos,
                .perfil = perfiles(lstPerfil.SelectedIndex),
                .dni = usuarioModificado.dni
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
                                                    .observaciones = "Se modifico el usuario " & usuarioModificado.username
                                                    }
            BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
            MostrarMensaje("Se ha creado el usuario", TipoAlerta.Success)
        Catch ex As Exception
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", TipoAlerta.Danger)
        End Try
    End Sub


End Class