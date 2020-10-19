Imports BLL
Imports Entidades
Imports System.Web.HttpContext

Public Class ModificarDatosUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim cliente As ClienteDTO = Current.Session("Cliente")
            CargarDatos(cliente)
        End If
    End Sub

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Current.Session("accion") = "Modificar" Then
            If txtContraseña.Text = txtValidarContraseña.Text Then
                Dim cliente As ClienteDTO = Current.Session("entidadModal")
                ClienteBLL.ObtenerInstancia.Modificar(cliente)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                            .FechaHora = Now(),
                            .usuario = usuarioLogeado,
                            .tipoSuceso = New SucesoBitacoraDTO With {.id = 8}, 'Suceso: Modificacion usuario
                            .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                            .observaciones = "Se modifico el usuario :" & cliente.usuario.username
                    }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
                If Current.Session("ModificaContraseña") = True Then
                    GestorMailBLL.ObtenerInstancia.EnviarMail(cliente.usuario.mail, "Food Business Manager : Cambio de contraseña", "Se ha registrado su cambio de contraseña", Server.MapPath("\EmailTemplates\Template_mail.html"))
                End If
            Else
                MostrarModal("Error", "Las contraseñas no coinciden",, True)
            End If
        End If
        'Acá tengo que hidear el modal
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Response.Redirect("/ModificarDatosUsuario.aspx")
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

    Public Sub CargarDatos(cliente As ClienteDTO)
        txtMail.Text = cliente.usuario.mail
        txtNombre.Text = cliente.usuario.nombre
        txtApellido.Text = cliente.usuario.apellido
        txtUsuario.Text = cliente.usuario.username
        txtContraseña.Text = cliente.usuario.password
        txtValidarContraseña.Text = cliente.usuario.password
        txtDNI.Text = cliente.usuario.dni
        txtCUIT.Text = cliente.CUIT
        txtRazonSocial.Text = cliente.RazonSocial
        txtDireccion.Text = cliente.domicilio
        txtLocalidad.Text = cliente.localidad
        txtTelefono.Text = cliente.telefono
        txtCP.Text = cliente.CP

        ddlProvincias.DataSource = ProvinciaBLL.ObtenerInstancia.Listar
        ddlProvincias.DataBind()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim clienteLogeado As ClienteDTO = Current.Session("Cliente")
            If clienteLogeado.usuario.password = CriptografiaBLL.ObtenerInstancia.Cifrar(txtContraseña.Text) Then
                Current.Session("ModificaContraseña") = False
            Else
                Current.Session("ModificaContraseña") = True
            End If
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
                            .provincia = ddlProvincias.SelectedItem.Text,
                            .aceptaNewsletter = False,
                            .telefono = txtTelefono.Text
                    }
            Current.Session("entidadModal") = cliente
            Current.Session("accion") = "Modificar"
            MostrarModal("Modificacion de usuario", "¿Está seguro que desea sus modificar sus datos " & usuario.username & "?",, True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error, intente nuevemente o contacte a su administrador",, True)
        End Try
    End Sub
End Class