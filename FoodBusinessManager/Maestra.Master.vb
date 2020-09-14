Imports BLL
Imports Entidades
Imports System.Web.HttpContext
Public Class Maestra
    Inherits System.Web.UI.MasterPage
    Private usuarioLogeado As New UsuarioDTO
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
        If Not IsNothing(Current.Session("Cliente")) Then
            'Perfil y traducciones
            Dim cliente As ClienteDTO = Current.Session("Cliente")
            CargarPerfil(cliente.usuario)
            'Manejo de navbar
            panelLoginform.Visible = False
            panelLogout.Visible = True
        Else
            panelLoginform.Visible = True
            panelLogout.Visible = False
        End If

    End Sub

#Region "Mensajes"
    Public Enum TipoAlerta
        Success
        Info
        Warning
        Danger
    End Enum

    Public Sub MostrarMensaje(mensaje As String, tipo As TipoAlerta)
        Dim panelMensaje As Panel = Me.FindControl("Mensaje")
        Dim labelMensaje As Label = panelMensaje.FindControl("labelMensaje")

        labelMensaje.Text = mensaje
        panelMensaje.CssClass = String.Format("alert alert-{0} alert-dismissable", tipo.ToString.ToLower())
        panelMensaje.Style.Add("z-index", "1000")
        panelMensaje.Attributes.Add("role", "alert")
        panelMensaje.Visible = True
    End Sub

#End Region

#Region "Modal"

    Public ReadOnly Property btnModalAceptar() As Button
        Get
            Return btnAceptar
        End Get
    End Property

    Public Event AceptarModal As CommandEventHandler

    'Protected Sub Moods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Moods.SelectedIndexChanged
    '    If Moods.SelectedIndex <> 0 Then
    '        RaiseEvent MoodChanged(Me, New CommandEventArgs(Moods.SelectedItem.Text, Moods.SelectedValue))
    '    End If
    'End Sub


    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        RaiseEvent AceptarModal(Me, New CommandEventArgs("Aceptado", btnAceptar))
    End Sub

    Public Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

    End Sub

#End Region

#Region "Menu"

    Public Sub ArmarMenuLateral()
        Try
            Me.MenuLateral.Items.Add(New MenuItem("Administración del Sistema", "AdminSist"))
            Me.MenuLateral.Items.Item(0).ChildItems.Add(New MenuItem("Copia de Seguridad", "Backup", Nothing, "/Backup.aspx"))
            Me.MenuLateral.Items.Item(0).ChildItems.Add(New MenuItem("Restauración de Datos", "Restore", Nothing, "/Restore.aspx"))
            Me.MenuLateral.Items.Item(0).ChildItems.Add(New MenuItem("Visualizar Bitacora Auditoria", "BitacoraAuditoria", Nothing, "/Bitacora.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Administración Usuarios", "AdminUsu"))
            Me.MenuLateral.Items.Item(1).ChildItems.Add(New MenuItem("Agregar Usuario", "AgregarUsuario", Nothing, "/AgregarUsuario.aspx"))
            Me.MenuLateral.Items.Item(1).ChildItems.Add(New MenuItem("Modificar Usuario", "ModificarUsuario", Nothing, "/ModificarUsuario.aspx"))
            Me.MenuLateral.Items.Item(1).ChildItems.Add(New MenuItem("Eliminar Usuario", "EliminarUsuario", Nothing, "/EliminarUsuario.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Administración Perfiles", "AdminPer"))
            Me.MenuLateral.Items.Item(2).ChildItems.Add(New MenuItem("Crear Perfil", "CrearPerfil", Nothing, "/AgregarPerfil.aspx"))
            Me.MenuLateral.Items.Item(2).ChildItems.Add(New MenuItem("Modificar Perfil", "ModificarPerfil", Nothing, "/ModificarPerfil.aspx"))
            Me.MenuLateral.Items.Item(2).ChildItems.Add(New MenuItem("Eliminar Perfil", "EliminarPerfil", Nothing, "/EliminarPerfil.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Administración Catalogo", "AdminCata"))
            Me.MenuLateral.Items.Item(3).ChildItems.Add(New MenuItem("Catalogos", "AdministrarCatalogos", Nothing, "/AdministrarCatalogo.aspx"))
            Me.MenuLateral.Items.Item(3).ChildItems.Add(New MenuItem("Servicios", "AdministrarServicios", Nothing, "/AdministrarServicios.aspx"))
            Me.MenuLateral.Items.Item(3).ChildItems.Add(New MenuItem("Caracteristicas", "AdministrarCaracteristicas", Nothing, "/AdministrarCaracteristicas.aspx"))

        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Perfiles y permisos"
    Private Sub CargarPerfil(usuario As UsuarioDTO)
        Try
            Me.MenuLateral.Items.Clear()
            ArmarMenuLateral()
            'Armo la lista de paginas a remover del menu
            Dim listaaRemover As New List(Of MenuItem)
            For Each pagina As MenuItem In MenuLateral.Items
                If pagina.ChildItems.Count > 0 Then
                    RecorrerMenu(pagina, usuario, listaaRemover)
                Else
                    If usuario.perfil.PuedeUsar(pagina.NavigateUrl) = False Then
                        listaaRemover.Add(pagina)
                    End If
                End If
            Next
            'Remuevo las paginas del menu
            For Each item As MenuItem In listaaRemover
                MenuLateral.Items.Remove(item)
                For Each subnivel As MenuItem In MenuLateral.Items
                    subnivel.ChildItems.Remove(item)
                Next
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RecorrerMenu(pagina As MenuItem, Usuario As UsuarioDTO, listaaRemover As List(Of MenuItem))
        Dim flag As Integer = 0
        For Each subpagina As MenuItem In pagina.ChildItems
            If subpagina.ChildItems.Count > 0 Then
                RecorrerMenu(subpagina, Usuario, listaaRemover)
            Else
                If Usuario.perfil.PuedeUsar(subpagina.NavigateUrl) = False Then
                    listaaRemover.Add(subpagina)
                    flag += 1
                End If
            End If
        Next
        'Si no puedo usar ningun item, no muestro el menu
        If flag = pagina.ChildItems.Count Then
            listaaRemover.Add(pagina)
        End If
    End Sub



    Private Sub linkLogOut_Click(sender As Object, e As EventArgs) Handles linkLogOut.Click
        Current.Session("Cliente") = Nothing
        Response.Redirect("/Home.aspx")
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim usuario As New UsuarioDTO
            usuario.username = txtUsuario.Text
            usuario.password = txtContraseña.Text
            If BLL.UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) Then
                usuarioLogeado = UsuarioBLL.ObtenerInstancia.LogIn(usuario)
                If usuarioLogeado Is Nothing Then 'El usuario existe pero no corresponde la contraseña
                    'Modal.Mostrar("La contraseña es incorrecta", "Error!")
                Else
                    If usuarioLogeado.bloqueado = True Then 'La contraseña responde, pero el usuario está bloqueado -> Solicito cambio contraseña
                        'Modal.Mostrar("El usuario se encuentra bloqueado! Debe recuperar su contraseña", "Error!")
                    Else
                        Current.Session("Cliente") = ClienteBLL.ObtenerInstancia.ObtenerPorUsuario(usuarioLogeado)
                        Current.Session("Usuario") = usuarioLogeado
                        'Grabo Bitacora - Suceso Login = 1
                        Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 1}, 'Suceso: creacion cliente
                        .criticidad = New CriticidadDTO With {.id = 1}, 'Criticidad: media
                        .observaciones = "Se logeo el usuario :" & usuarioLogeado.username
                            }
                        BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
                        Response.Redirect("/Home.aspx", False)
                    End If
                End If
            Else
                'Modal.Mostrar("Error. El usuario no existe", "Error")
            End If
        Catch ex As Exception
            Dim bitacora As New BitacoraDTO With {
            .FechaHora = Now(),
            .usuario = usuarioLogeado,
            .tipoSuceso = New SucesoBitacoraDTO With {.id = 1}, 'Suceso: Logeo
            .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
            .observaciones = "Ocurrio un error al logear el usuario :" & usuarioLogeado.username & ". Error: " & ex.Message.ToString}
            'Modal.Mostrar("Lo siento! Ocurrio un error inesperado.", "Error")
        End Try
    End Sub


#End Region




End Class