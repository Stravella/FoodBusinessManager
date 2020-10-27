Imports Entidades
Imports System.Web.HttpContext
Imports BLL

Public Class AdministrarEncuestaPreguntas
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
                Dim pregunta As EncuestaPreguntaDTO = Current.Session("entidadModal")
                EncuestaPreguntaBLL.ObtenerInstancia.Eliminar(pregunta)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                            .FechaHora = Now(),
                                            .usuario = usuarioLogeado,
                                            .tipoSuceso = New SucesoBitacoraDTO With {.id = 30}, 'Suceso: Eliminacion opinion
                                            .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                                            .observaciones = "Se elimino la pregunta : " & pregunta.pregunta
                                   }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Modificar" Then
                Dim pregunta As EncuestaPreguntaDTO = Current.Session("entidadModal")
                EncuestaPreguntaBLL.ObtenerInstancia.Modificar(pregunta)
                'Actualizo el orden y el id de Catalogo                
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                    .FechaHora = Now(),
                                    .usuario = usuarioLogeado,
                                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 31}, 'Suceso: Modificacion opinion
                                    .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Alta
                                    .observaciones = "Se modifico la pregunta : " & pregunta.ID
                            }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
            Response.Redirect("/AdministrarEncuestaPreguntas.aspx")
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
            CargarOpiniones()
            CargarEstados()
            CargarRespuestas()
        End If
    End Sub

    Public Sub CargarOpiniones()
        Dim opiniones As New List(Of EncuestaPreguntaDTO)
        opiniones = EncuestaPreguntaBLL.ObtenerInstancia.Listar()
        gvPreguntas.DataSource = opiniones
        gvPreguntas.DataBind()
    End Sub

    Public Sub CargarEstados()
        Dim estados As New List(Of EstadoPreguntaEncuestaDTO)
        estados = EstadoOpinionBLL.ObtenerInstancia.Listar
        ddlEstado.DataSource = estados
        ddlEstado.DataBind()
        ddlEstado.SelectedIndex = 0
    End Sub

    Public Sub CargarRespuestas()
        Dim respuestas As New List(Of RespuestaEncuestaDTO)
        respuestas = RespuestaEncuestaBLL.ObtenerInstancia.Listar()
        gvRespuestas.DataSource = respuestas
        gvRespuestas.DataBind()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            If txtVencimiento.Text >= Now() Then
                Dim pregunta As New EncuestaPreguntaDTO With {
                .pregunta = txtOpinion.Text,
                .Estado = New EstadoPreguntaEncuestaDTO With {.id = 1}, 'Activa
                .FechaVenc = txtVencimiento.Text
            }
                For Each gvrow As GridViewRow In gvRespuestas.Rows
                    Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                    If checkbox.Checked = True Then
                        Dim respuesta As New RespuestaEncuestaDTO With
                            {.id = Convert.ToInt16(gvrow.Cells(1).Text),
                             .respuesta = gvrow.Cells(2).Text
                            }
                        pregunta.Respuestas.Add(respuesta)
                    End If
                Next
                EncuestaPreguntaBLL.ObtenerInstancia.Agregar(pregunta)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 29}, 'Suceso: Creacion Opinion
                        .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Baja
                        .observaciones = "Se creo la pregunta : " & pregunta.pregunta
                }
                Response.Redirect("/AdministrarEncuestaPreguntas.aspx")
            Else
                MostrarModal("Advertencia", "La fecha debe ser una fecha vigente",, True)
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub gvOpiniones_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPreguntas.RowCommand
        Dim pregunta As New EncuestaPreguntaDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        pregunta = EncuestaPreguntaBLL.ObtenerInstancia.Obtener(id)
        For Each resp In pregunta.Respuestas
            For Each gvrow As GridViewRow In gvRespuestas.Rows
                If gvrow.Cells(1).Text = resp.id Then
                    Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                    checkbox.Checked = True
                End If
            Next
        Next
        If e.CommandName = "Editar" Then
            txtID.Text = pregunta.ID
            txtOpinion.Text = pregunta.pregunta
            txtVencimiento.Text = pregunta.FechaVenc
            ddlEstado.SelectedValue = pregunta.Estado.id
            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
        ElseIf e.CommandName = "Borrar" Then
            'Busco los servicios asociados           
            Current.Session("entidadModal") = pregunta
            Current.Session("accion") = "Eliminar"
            MostrarModal("¿Está seguro que desea borrar la pregunta " & pregunta.ID & " ?", pregunta.pregunta,, True)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtID.Text = ""
        txtOpinion.Text = ""
        txtVencimiento.Text = ""
        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnCancelar.Visible = False
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim pregunta As New EncuestaPreguntaDTO With
                {.ID = txtID.Text,
                .pregunta = txtOpinion.Text,
                .FechaVenc = txtVencimiento.Text,
                .Estado = EstadoOpinionBLL.ObtenerInstancia.Obtener(ddlEstado.SelectedValue)
                }
            Current.Session("entidadModal") = pregunta
            Current.Session("accion") = "Modificar"
            MostrarModal("¿Está seguro que desea modificar la pregunta " & pregunta.ID & "?", pregunta.pregunta,, True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

End Class