Imports Entidades
Imports System.Web.HttpContext
Imports BLL

Public Class AdministrarEncuestas
    Inherits System.Web.UI.Page

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If Current.Session("accion") = "Eliminar" Then
                Dim encuesta As EncuestaDTO = Current.Session("entidadModal")
                EncuestaBLL.ObtenerInstancia.Eliminar(encuesta.id)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                            .FechaHora = Now(),
                                            .usuario = usuarioLogeado,
                                            .tipoSuceso = New SucesoBitacoraDTO With {.id = 37}, 'Suceso: Eliminacion encuesta
                                            .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                                            .observaciones = "Se elimino la encuesta : " & encuesta.nombre
                                   }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Modificar" Then
                Dim encuesta As EncuestaDTO = Current.Session("entidadModal")
                If ddlTipo.SelectedItem.Text = "Valoracion de servicio" Then
                    Dim lsServicios As New List(Of ServicioDTO)
                    For Each gvrow As GridViewRow In gvServicios.Rows
                        Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                        If checkbox.Checked = True Then
                            Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(Convert.ToInt16(gvrow.Cells(1).Text))
                            lsServicios.Add(servicio)
                        End If
                    Next
                    EncuestaBLL.ObtenerInstancia.EliminarRelacionServicios(encuesta.id)
                    For Each servicio In lsServicios
                        ServicioBLL.ObtenerInstancia.AgregarEncuestas(servicio.id, encuesta.id)
                    Next
                End If
                EncuestaBLL.ObtenerInstancia.Modificar(encuesta)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                    .FechaHora = Now(),
                                    .usuario = usuarioLogeado,
                                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 36}, 'Suceso: Modificacion encuesta
                                    .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Alta
                                    .observaciones = "Se modifico la encuesta : " & encuesta.id
                            }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
            Response.Redirect("/AdministrarEncuestas.aspx")
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Public Sub MostrarModal(titulo As String, body As String, Optional grd As GridView = Nothing, Optional cancelar As Boolean = False)
        Dim panelMensaje As UpdatePanel = Master.FindControl("Modal")
        Dim tituloModal As Label = panelMensaje.FindControl("lblModalTitle")
        Dim bodyModal As Label = panelMensaje.FindControl("lblModalBody")
        If grd IsNot Nothing Then
            Dim grillaModal As GridView = panelMensaje.FindControl("grilla")
            grillaModal = grd
            grillaModal.Visible = True
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
                        CargarEncuestas()
                        CargarTipo()
                        CargarPreguntas()
                        CargarServicios()
                    Else
                        Response.Redirect("/Home.aspx")
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub CargarEncuestas()
        Dim lsEncuesta As New List(Of EncuestaDTO)
        lsEncuesta = EncuestaBLL.ObtenerInstancia.Listar
        gvEncuesta.DataSource = lsEncuesta
        gvEncuesta.DataBind()
    End Sub

    Public Sub CargarTipo()
        Dim lsTipos As New List(Of TipoEncuestaDTO)
        lsTipos = TipoEncuestaBLL.ObtenerInstancia.Listar
        ddlTipo.DataSource = lsTipos
        ddlTipo.DataBind()
        ddlTipo.SelectedIndex = 0
    End Sub

    Public Sub CargarPreguntas()
        Dim preguntasDisponibles As New List(Of EncuestaPreguntaDTO)
        preguntasDisponibles = EncuestaPreguntaBLL.ObtenerInstancia.ListarSinUso()
        gvPreguntas.DataSource = preguntasDisponibles
        gvPreguntas.DataBind()
    End Sub

    Public Sub CargarServicios()
        Dim servicios As New List(Of ServicioDTO)
        servicios = ServicioBLL.ObtenerInstancia.Listar()
        gvServicios.DataSource = servicios
        gvServicios.DataBind()
    End Sub


    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim encuesta As New EncuestaDTO With {
                .nombre = txtNombre.Text,
                .tipo = TipoEncuestaBLL.ObtenerInstancia.Obtener(ddlTipo.SelectedValue)
            }
            For Each gvrow As GridViewRow In gvPreguntas.Rows
                Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                If checkbox.Checked = True Then
                    Dim pregunta As EncuestaPreguntaDTO = EncuestaPreguntaBLL.ObtenerInstancia.Obtener(Convert.ToInt16(gvrow.Cells(1).Text))
                    encuesta.preguntas.Add(pregunta)
                End If
            Next
            Dim lsServicios As New List(Of ServicioDTO)
            If ddlTipo.SelectedItem.Text = "Valoracion de servicio" Then
                For Each gvrow As GridViewRow In gvServicios.Rows

                    Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                    If checkbox.Checked = True Then
                        Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(Convert.ToInt16(gvrow.Cells(1).Text))
                        lsServicios.Add(servicio)
                    End If
                Next
            End If
            EncuestaBLL.ObtenerInstancia.Agregar(encuesta)
            If lsServicios IsNot Nothing Then
                For Each servicio In lsServicios
                    ServicioBLL.ObtenerInstancia.AgregarEncuestas(servicio.id, encuesta.id)
                Next
            End If
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 35}, 'Suceso: Creacion Opinion
                        .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Baja
                        .observaciones = "Se creo la encuesta : " & encuesta.nombre
                }
            Response.Redirect("/AdministrarEncuestas.aspx")
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub gvOpiniones_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvEncuesta.RowCommand
        Dim encuesta As New EncuestaDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        encuesta = EncuestaBLL.ObtenerInstancia.Obtener(id)
        For Each pregunta In encuesta.preguntas
            For Each gvrow As GridViewRow In gvPreguntas.Rows
                If gvrow.Cells(1).Text = pregunta.ID Then
                    Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                    checkbox.Checked = True
                End If
            Next
        Next
        If e.CommandName = "Editar" Then
            txtID.Text = encuesta.id
            txtNombre.Text = encuesta.nombre
            ddlTipo.SelectedValue = encuesta.tipo.id
            If encuesta.tipo.id = 3 Then
                gvServicios.Visible = True
                Dim servicios As New List(Of ServicioDTO)
                servicios = ServicioBLL.ObtenerInstancia.ListarPorIdEncuesta(encuesta.id)
                For Each servicio In servicios
                    For Each gvrow As GridViewRow In gvServicios.Rows
                        Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                        If servicio.id = Convert.ToInt16(gvrow.Cells(1).Text) Then
                            checkbox.Checked = True
                        End If
                    Next
                Next
            End If
            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
        ElseIf e.CommandName = "Borrar" Then
            'Busco los servicios asociados           
            Current.Session("entidadModal") = encuesta
            Current.Session("accion") = "Eliminar"
            MostrarModal("¿Está seguro que desea borrar la encuesta " & encuesta.nombre & " ?", "Se eliminaran todas las preguntas asociadas y las respuestas de los clientes",, True)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtID.Text = ""
        txtNombre.Text = ""
        ddlTipo.SelectedIndex = 0
        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnCancelar.Visible = False
        For Each gvrow As GridViewRow In gvPreguntas.Rows
            Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
            If checkbox.Checked = True Then
                checkbox.Checked = False
            End If
        Next
        For Each gvrow As GridViewRow In gvServicios.Rows
            Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
            If checkbox.Checked = True Then
                checkbox.Checked = False
            End If
        Next
        gvServicios.Visible = False
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim encuesta As New EncuestaDTO With {
                .id = txtID.Text,
                .nombre = txtNombre.Text,
                .tipo = TipoEncuestaBLL.ObtenerInstancia.Obtener(ddlTipo.SelectedValue)
            }
            For Each gvrow As GridViewRow In gvPreguntas.Rows
                Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                If checkbox.Checked = True Then
                    Dim pregunta As New EncuestaPreguntaDTO With
                            {.ID = Convert.ToInt16(gvrow.Cells(1).Text),
                             .pregunta = gvrow.Cells(2).Text
                            }
                    encuesta.preguntas.Add(pregunta)
                End If
            Next
            Current.Session("entidadModal") = encuesta
            Current.Session("accion") = "Modificar"
            MostrarModal("¿Está seguro que desea modificar la encuesta " & encuesta.id & "?", encuesta.nombre,, True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub ddlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipo.SelectedIndexChanged
        If ddlTipo.SelectedItem.Text = "Valoracion de servicio" Then
            gvServicios.Visible = True
        Else
            gvServicios.Visible = False
            For Each gvrow As GridViewRow In gvServicios.Rows
                Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                If checkbox.Checked = True Then
                    checkbox.Checked = False
                End If
            Next
        End If
    End Sub
End Class