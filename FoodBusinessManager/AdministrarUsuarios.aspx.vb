﻿Imports System.Web.HttpContext
Imports BLL
Imports Entidades
Public Class AdministrarUsuarios1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not IsPostBack() Then
                If Not IsPostBack() Then
                    If Session("cliente") IsNot Nothing Then
                        Dim cliente As ClienteDTO = DirectCast(Session("cliente"), ClienteDTO)
                        Dim puedeUsar As Boolean = False
                        For Each permiso In cliente.usuario.perfil.Hijos
                            If permiso.PuedeUsar(Request.Url.AbsolutePath) = True Then
                                puedeUsar = True
                            End If
                        Next
                        If puedeUsar = False Then
                            Response.Redirect("/Home.aspx")
                        End If
                        CargarUsuarios()
                        CargarProvincias()
                        CargarPerfiles()
                    Else
                        Response.Redirect("/Home.aspx")
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub CargarUsuarios()
        Dim lsUsuarios As List(Of UsuarioDTO) = UsuarioBLL.ObtenerInstancia.ListarUsuarios
        gv_Usuarios.DataSource = lsUsuarios
        gv_Usuarios.DataBind()
    End Sub

    Public Sub CargarProvincias()
        ddlProvincias.DataSource = ProvinciaBLL.ObtenerInstancia.Listar
        ddlProvincias.DataBind()
    End Sub

    Public Sub CargarPerfiles()
        ddlPerfil.DataSource = PermisoBLL.ObtenerInstancia.ListarPerfiles
        ddlPerfil.DataBind()
    End Sub

    Public Sub SeleccionarPerfil(nombrePerfil As String)
        For Each item As ListItem In ddlPerfil.Items
            If item.Text = nombrePerfil Then
                ddlPerfil.SelectedValue = item.Value
            End If
        Next
    End Sub

    Public Sub SeleccionarProvincia(provincia As String)
        For Each item As ListItem In ddlProvincias.Items
            If item.Text = provincia Then
                ddlProvincias.SelectedValue = item.Value
            End If
        Next
    End Sub


#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Current.Session("accion") = "Eliminar" Then
            Dim cliente As ClienteDTO = Current.Session("entidadModal")
            ClienteBLL.ObtenerInstancia.Eliminar(cliente)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 9}, 'Suceso: Eliminacion usuario
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se elimino el usuario :" & cliente.usuario.username
                }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        End If
        If Current.Session("accion") = "Modificar" Then
            Dim cliente As ClienteDTO = Current.Session("entidadModal")
            ClienteBLL.ObtenerInstancia.Modificar(cliente)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Suceso: Modificacion caracteristica
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se modifico el usuario :" & cliente.usuario.username
                }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        End If
        CargarUsuarios()
        'Acá tengo que hidear el modal
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Response.Redirect("/AdministrarUsuarios.aspx")
    End Sub

    Public Sub MostrarModal(titulo As String, body As String, Optional grd As GridView = Nothing, Optional cancelar As Boolean = False)
        Dim panelMensaje As UpdatePanel = Master.FindControl("Modal")
        Dim tituloModal As Label = panelMensaje.FindControl("lblModalTitle")
        Dim bodyModal As Label = panelMensaje.FindControl("lblModalBody")
        If grd IsNot Nothing Then
            Dim grillaModal As GridView = panelMensaje.FindControl("grilla")
            grillaModal.AutoGenerateColumns = True
            grillaModal.Visible = True
            grillaModal = grd
            grillaModal.DataBind()
        End If
        If cancelar = True Then
            Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
            btnCancelar.Visible = True
        End If
        tituloModal.Text = titulo
        bodyModal.Text = body

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#myModal').modal();", True)
        panelMensaje.Update()
    End Sub

#End Region


    Protected Sub gv_Usuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim usuario As New UsuarioDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        usuario = UsuarioBLL.ObtenerInstancia.ObtenerPorId(id)
        Dim cliente As ClienteDTO = ClienteBLL.ObtenerInstancia.ObtenerPorUsuario(usuario)
        If e.CommandName = "Editar" Then
            txtID.Text = usuario.id
            txtMail.Text = usuario.mail
            txtUsuario.Text = usuario.username
            txtContraseña.Text = usuario.password
            txtNombre.Text = usuario.nombre
            txtApellido.Text = usuario.apellido
            txtCP.Text = cliente.CP
            txtCUIT.Text = cliente.CUIT
            txtDNI.Text = usuario.dni
            txtDireccion.Text = cliente.domicilio
            txtLocalidad.Text = cliente.localidad
            txtTelefono.Text = cliente.telefono
            txtRazonSocial.Text = cliente.RazonSocial
            If usuario.bloqueado = True Then
                chkBloqueado.Checked = True
            Else
                chkBloqueado.Checked = False
            End If

            SeleccionarProvincia(cliente.provincia)
            SeleccionarPerfil(usuario.perfil.nombre)

            Current.Session("perfilAnterior") = usuario.perfil.id_permiso

            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
        ElseIf e.CommandName = "Borrar" Then
            Current.Session("entidadModal") = cliente
            Current.Session("accion") = "Eliminar"
            MostrarModal("Borrado de usuario", "¿Está seguro que desea borrar el usuario " & usuario.username & "?",, True)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtID.Text = ""
        txtMail.Text = ""
        txtUsuario.Text = ""
        txtContraseña.Text = ""
        txtNombre.Text = ""
        txtApellido.Text = ""
        txtCP.Text = ""
        txtCUIT.Text = ""
        txtDNI.Text = ""
        txtDireccion.Text = ""
        txtLocalidad.Text = ""
        txtTelefono.Text = ""
        txtRazonSocial.Text = ""
        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnCancelar.Visible = False
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim unicoAdmin As Boolean = False
            Dim usuario As New UsuarioDTO With {
                            .id = txtID.Text,
                            .nombre = txtNombre.Text,
                            .apellido = txtApellido.Text,
                            .username = txtUsuario.Text,
                            .dni = txtDNI.Text,
                            .mail = txtMail.Text,
                            .fechaCreacion = Now(),
                            .password = CriptografiaBLL.ObtenerInstancia.Cifrar(txtContraseña.Text),
                            .intentos = 0,
                            .bloqueado = chkBloqueado.Checked,
                            .perfil = New PerfilCompuesto With {.id_permiso = ddlPerfil.SelectedValue}
                        }
            Dim cliente As New ClienteDTO With {
                            .usuario = usuario,
                            .CUIT = txtCUIT.Text,
                            .RazonSocial = txtRazonSocial.Text,
                            .domicilio = txtDireccion.Text,
                            .localidad = txtLocalidad.Text,
                            .CP = txtCP.Text,
                            .estado = New EstadoClienteDTO With {.id = 1, .descripcion = "Activo"},
                            .provincia = ddlProvincias.SelectedItem.Text,
                            .aceptaNewsletter = False,
                            .telefono = txtTelefono.Text
                    }

            If Current.Session("perfilAnterior") = 18 AndAlso cliente.usuario.perfil.id_permiso <> 18 Then
                Dim ls As List(Of UsuarioDTO) = UsuarioBLL.ObtenerInstancia.ListarPorPerfil(New PerfilCompuesto With {.id_permiso = 18})
                If ls.Count <= 1 Then
                    unicoAdmin = True
                End If
            End If
            If unicoAdmin = False Then
                Current.Session("perfilAnterior") = Nothing
                Current.Session("entidadModal") = cliente
                Current.Session("accion") = "Modificar"
                MostrarModal("Modificacion de usuario", "¿Está seguro que desea modificar el usuario " & usuario.username & "?",, True)
            Else
                MostrarModal("Advertencia", "Lo siento! Usted es el unico usuario con perfil Administrador. Para modificar su perfil, debe existir otro usuario con el perfil de admin",, True)
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error, intente nuevemente o contacte a su administrador",, True)
        End Try
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim usuario As New UsuarioDTO With {
                .nombre = txtNombre.Text,
                .apellido = txtApellido.Text,
                .username = txtUsuario.Text,
                .dni = txtDNI.Text,
                .mail = txtMail.Text,
                .fechaCreacion = Now(),
                .password = CriptografiaBLL.ObtenerInstancia.Cifrar(txtContraseña.Text),
                .intentos = 0,
                .bloqueado = chkBloqueado.Checked,
                .perfil = New PerfilCompuesto With {.id_permiso = ddlPerfil.SelectedValue}
            }
            Dim cliente As New ClienteDTO
            cliente.usuario = usuario
            cliente.CUIT = txtCUIT.Text
            cliente.RazonSocial = txtRazonSocial.Text
            cliente.domicilio = txtDireccion.Text
            cliente.localidad = txtLocalidad.Text
            cliente.CP = txtCP.Text
            cliente.estado = New EstadoClienteDTO With {.id = 1, .descripcion = "Activo"}
            cliente.provincia = ddlProvincias.SelectedItem.Text
            cliente.aceptaNewsletter = False
            cliente.telefono = txtTelefono.Text

            ClienteBLL.ObtenerInstancia.Agregar(cliente)
            Dim bitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = cliente.usuario,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 10}, 'Suceso: creacion cliente
                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                .observaciones = "Se creo el cliente :" & cliente.usuario.nombre & cliente.usuario.apellido & "de usuario: " & cliente.usuario.username
            }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            MostrarModal("Creación de usuario", "Se creó el usuario " & usuario.username & " exitosamente",, True)
        Catch ex As Exception
            Dim cliente As ClienteDTO = Current.Session("Cliente")
            Dim bitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = cliente.usuario,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 10}, 'Suceso: creacion cliente
                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                .observaciones = "Ocurrio un error al crear el cliente :" & cliente.usuario.nombre & cliente.usuario.apellido & "de usuario: " & cliente.usuario.username
            }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            MostrarModal("Error", "Lo siento! Ocurrio un error, intente nuevemente o contacte a su administrador",, True)
        End Try
    End Sub

End Class