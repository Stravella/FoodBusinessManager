﻿Imports BLL
Imports Entidades
Imports System.Web.HttpContext
Public Class Maestra
    Inherits System.Web.UI.MasterPage
    Private usuarioLogeado As New UsuarioDTO
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            CargarNumberCarrito()
            If IsNothing(Current.Session("Cliente")) Then
                'Panel de logeo
                panelLoginform.Visible = True
                panelLogout.Visible = False
            Else
                Dim cliente As ClienteDTO = Current.Session("Cliente")
                'Perfil y traducciones
                CargarPerfil(cliente.usuario)
                'Panel de usuario logueado
                panelLoginform.Visible = False
                panelLogout.Visible = True
                linkUsuario.Text = "Bienvenido: " & cliente.usuario.username
            End If
        End If
    End Sub

    Public Sub CargarNumberCarrito()
        If Current.Session("Carrito") IsNot Nothing Then
            lblCartCount.Text = Current.Session("CantidadItemsCarrito")
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

#Region "ModalEncuesta"

    Public ReadOnly Property btnEncuestaAceptar() As Button
        Get
            Return enviar
        End Get
    End Property

    Public Event AceptarModalEncuesta As CommandEventHandler

    'Protected Sub Moods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Moods.SelectedIndexChanged
    '    If Moods.SelectedIndex <> 0 Then
    '        RaiseEvent MoodChanged(Me, New CommandEventArgs(Moods.SelectedItem.Text, Moods.SelectedValue))
    '    End If
    'End Sub


    Protected Sub btnEncuestaAceptar_Click(sender As Object, e As EventArgs) Handles enviar.Click
        RaiseEvent AceptarModalEncuesta(Me, New CommandEventArgs("Aceptado", enviar))
    End Sub

    Public Sub btnEncuestaCancelar_Click(sender As Object, e As EventArgs) Handles cancelar.Click

    End Sub

    Private Sub repeaterPreguntas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repeaterPreguntas.ItemDataBound
        Dim rdioRtas = DirectCast(e.Item.FindControl("rdlRespuestas"), RadioButtonList)
        If rdioRtas IsNot Nothing Then
            rdioRtas.DataSource = Nothing
            Dim oPreg As EncuestaPreguntaDTO = DirectCast(e.Item.DataItem, EncuestaPreguntaDTO)

            rdioRtas.DataSource = oPreg.Respuestas
            rdioRtas.DataValueField = "ID"
            rdioRtas.DataTextField = "Respuesta"
            rdioRtas.DataBind()
        End If
    End Sub

#End Region

#Region "Menu"

    Public Sub ArmarMenuLateral()
        Try
            Me.MenuLateral.Items.Add(New MenuItem("Administración del Sistema", "AdminSist"))
            Me.MenuLateral.Items.Item(0).ChildItems.Add(New MenuItem("Backup y Restore", "Backup", Nothing, "/backup.aspx"))
            Me.MenuLateral.Items.Item(0).ChildItems.Add(New MenuItem("Visualizar Bitacora Auditoria", "BitacoraAuditoria", Nothing, "/Bitacora.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Administración Usuarios", "AdminUsu"))
            Me.MenuLateral.Items.Item(1).ChildItems.Add(New MenuItem("Administrar Usuarios", "AdminUsuarios", Nothing, "/AdministrarUsuarios.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Administración Perfiles", "AdminPer"))
            Me.MenuLateral.Items.Item(2).ChildItems.Add(New MenuItem("Crear Perfil", "CrearPerfil", Nothing, "/AgregarPerfil.aspx"))
            Me.MenuLateral.Items.Item(2).ChildItems.Add(New MenuItem("Modificar Perfil", "ModificarPerfil", Nothing, "/ModificarPerfil.aspx"))
            Me.MenuLateral.Items.Item(2).ChildItems.Add(New MenuItem("Eliminar Perfil", "EliminarPerfil", Nothing, "/EliminarPerfil.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Administración Catalogo", "AdminCata"))
            Me.MenuLateral.Items.Item(3).ChildItems.Add(New MenuItem("Catalogos", "AdministrarCatalogos", Nothing, "/AdministrarCatalogo.aspx"))
            Me.MenuLateral.Items.Item(3).ChildItems.Add(New MenuItem("Servicios", "AdministrarServicios", Nothing, "/AdministrarServicios.aspx"))
            Me.MenuLateral.Items.Item(3).ChildItems.Add(New MenuItem("Caracteristicas", "AdministrarCaracteristicas", Nothing, "/AdministrarCaracteristicas.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Administración Mensajes", "AdminMensajes"))
            Me.MenuLateral.Items.Item(4).ChildItems.Add(New MenuItem("Respuestas", "AdministrarRespuestas", Nothing, "/Respuestas.aspx"))
            Me.MenuLateral.Items.Item(4).ChildItems.Add(New MenuItem("Chat", "ResponderChat", Nothing, "/ResponderChat.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Newsletter y Publicidad", "AdminNewsletter"))
            Me.MenuLateral.Items.Item(5).ChildItems.Add(New MenuItem("Categorias", "AdministrarCategorias", Nothing, "/AdministrarCategorias.aspx"))
            Me.MenuLateral.Items.Item(5).ChildItems.Add(New MenuItem("Newsletter", "NewsletterBack", Nothing, "/NewsletterBack.aspx"))
            Me.MenuLateral.Items.Item(5).ChildItems.Add(New MenuItem("Publicidad", "PublicidadBack", Nothing, "/PublicidadBack.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Movimientos", "AdminMovimientos"))
            Me.MenuLateral.Items.Item(6).ChildItems.Add(New MenuItem("Mis Movimientos", "MisMovimientos", Nothing, "/MisMovimientos.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Encuestas y Opiniones", "AdminEncuestas"))
            Me.MenuLateral.Items.Item(7).ChildItems.Add(New MenuItem("Administrar Encuestas", "AdministrarEncuestas", Nothing, "/AdministrarEncuestas.aspx"))
            Me.MenuLateral.Items.Item(7).ChildItems.Add(New MenuItem("Administrar Preguntas", "AdministrarPreguntas", Nothing, "/AdministrarEncuestaPreguntas.aspx"))
            Me.MenuLateral.Items.Item(7).ChildItems.Add(New MenuItem("Administrar Respuesta", "AdministrarRespuestas", Nothing, "/AdministrarEncuestaRespuestas.aspx"))
            Me.MenuLateral.Items.Add(New MenuItem("Reportes", "AdminReportes"))
            Me.MenuLateral.Items.Item(8).ChildItems.Add(New MenuItem("Reportes", "AdministrarReportes", Nothing, "/Reportes.aspx"))
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

#Region "Panel logout"

    Private Sub linkModificarDatos_Click(sender As Object, e As EventArgs) Handles linkModificarDatos.Click

        Response.Redirect("/ModificarDatosUsuario.aspx")
    End Sub

    Private Sub linkLogOut_Click(sender As Object, e As EventArgs) Handles linkLogOut.Click
        Current.Session("Cliente") = Nothing
        Response.Redirect("/Home.aspx")
    End Sub



#End Region


#Region "Busqueda"
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            If txtBuscar.Text IsNot "" Then
                Current.Session("buscar") = txtBuscar.Text
                Response.Redirect("/Buscador.aspx", False)
            Else
                MostrarModal("Advertencia", "Ingrese un valor a buscar",, False)
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, False)
        End Try
    End Sub

#End Region

#Region "Modal"

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, False)
        End Try
    End Sub

    Public Sub MostrarModal(titulo As String, body As String, Optional grd As GridView = Nothing, Optional cancelar As Boolean = False)
        lblModalTitle.Text = titulo
        lblModalBody.Text = body

        If cancelar = True Then
            btnCancelar.Visible = True
        End If
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "myModal", "$('#myModal').modal();", True)
    End Sub


#End Region

#Region "Chat"
    Private Sub linkChat_Click(sender As Object, e As EventArgs) Handles linkChat.Click
        Dim panelMensaje As UpdatePanel = Me.FindControl("panelChat")
        Dim tituloModal As Label = panelMensaje.FindControl("lblTitulo")
        grdChatMensajes.DataSource = Nothing

        Dim cliente As ClienteDTO = DirectCast(Session("Cliente"), ClienteDTO)
        Dim chat As New ChatSesionDTO
        chat = ChatBLL.ObtenerInstancia.ObtenerChatActivo(cliente)
        If chat.id = 0 Then 'Nuevo chat
            chat.fechaInicio = DateTime.Now
            chat.cliente = cliente
            tituloModal.Text = "Nuevo chat - " & DateTime.Now
            grdChatMensajes.DataSource = chat.mensajes
            grdChatMensajes.DataBind()
            btnFinalizarChat.Visible = False
        Else
            tituloModal.Text = "Chat iniciado el - " & chat.fechaInicio
            grdChatMensajes.DataSource = chat.mensajes
            grdChatMensajes.DataBind()
            btnFinalizarChat.Visible = True
        End If
        Session("chat") = chat

        panelMensaje.Update()
        CerrarModalChat()
    End Sub

    Private Sub btnFinalizarChat_Click(sender As Object, e As EventArgs) Handles btnFinalizarChat.Click
        Dim chat As ChatSesionDTO = DirectCast(Session("chat"), ChatSesionDTO)
        If chat IsNot Nothing Then
            chat.fechaFin = DateTime.Now
            If chat.usuarioAtendio Is Nothing Then
                Dim cliente As ClienteDTO = DirectCast(Session("Cliente"), ClienteDTO)
                chat.usuarioAtendio = cliente.usuario
            End If
            ChatBLL.ObtenerInstancia.Modificar(chat)

            Session("chat") = Nothing
            CerrarModalChat()
        End If
    End Sub

    Private Sub btnEnviarChat_Click(sender As Object, e As EventArgs) Handles btnEnviarChat.Click
        Dim cliente As ClienteDTO = DirectCast(Session("Cliente"), ClienteDTO)
        Dim chat As ChatSesionDTO = DirectCast(Session("chat"), ChatSesionDTO)
        Dim mensaje As New ChatMensajeDTO With {
            .fecha = DateTime.Now,
            .mensaje = txtMensaje.Text,
            .username = cliente.usuario.username
        }
        If chat.id = "0" Then 'Nuevo chat
            chat.mensajes.Add(mensaje)
            ChatBLL.ObtenerInstancia.Crear(chat)
        Else
            chat.mensajes.Add(mensaje)
            ChatBLL.ObtenerInstancia.CrearMensaje(mensaje, chat)
        End If
        grdChatMensajes.DataSource = Nothing
        grdChatMensajes.DataSource = chat.mensajes
        grdChatMensajes.DataBind()

        panelChat.Update()
        Session("chat") = Nothing
        CerrarModalChat()
    End Sub

    Private Sub CerrarModalChat()
        txtMensaje.Text = ""

        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChat", "$('#modalChat').modal();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "modalChat", "$('#modalChat').modal();", True)
    End Sub

    Private Sub btnCerrarModalChat_Click(sender As Object, e As EventArgs) Handles btnCerrarModalChat.Click
        txtMensaje.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "modalChat", "$('#modalChat').modal();", True)
    End Sub

#End Region


End Class