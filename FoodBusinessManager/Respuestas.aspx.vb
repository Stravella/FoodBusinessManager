Imports BLL
Imports Entidades
Imports System.Web.HttpContext

Public Class Respuestas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
                CargarServicios()
            Else
                Response.Redirect("/Home.aspx")
            End If
        End If
    End Sub

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        'If Current.Session("accion") = "Eliminar" Then
        '    Dim caracteristica As CaracteristicaDTO = Current.Session("entidadModal")
        '    CaracteristicaBLL.ObtenerInstancia.Eliminar(caracteristica)
        '    Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
        '    Dim bitacora As New BitacoraDTO With {
        '                .FechaHora = Now(),
        '                .usuario = usuarioLogeado,
        '                .tipoSuceso = New SucesoBitacoraDTO With {.ID = 12}, 'Suceso: Eliminacion caracteristica
        '                .criticidad = New CriticidadDTO With {.ID = 3}, 'Criticidad: Alta
        '                .observaciones = "Se elimino la caracteristica :" & caracteristica.id
        '        }
        '    BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        'End If
        'If Current.Session("accion") = "Modificar" Then
        '    Dim caracteristica As CaracteristicaDTO = Current.Session("entidadModal")
        '    CaracteristicaBLL.ObtenerInstancia.Modificar(caracteristica)
        '    Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
        '    Dim bitacora As New BitacoraDTO With {
        '                .FechaHora = Now(),
        '                .usuario = usuarioLogeado,
        '                .tipoSuceso = New SucesoBitacoraDTO With {.ID = 12}, 'Suceso: Modificacion caracteristica
        '                .criticidad = New CriticidadDTO With {.ID = 3}, 'Criticidad: Alta
        '                .observaciones = "Se modifico la caracteristica :" & caracteristica.id
        '        }
        '    BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        'End If
        'CargarCaracteristicas()
        'Acá tengo que hidear el modal
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
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

    Public Sub CargarServicios()
        Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar()
        For Each servicio In servicios
            Dim item As New ListItem
            item.Text = servicio.nombre
            item.Value = servicio.id
            ddlServicio.Items.Add(item)
        Next
        CargarPreguntas(1)
    End Sub

    Private Sub ddlServicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServicio.SelectedIndexChanged
        CargarPreguntas(ddlServicio.SelectedValue)
    End Sub


    Public Sub CargarPreguntas(id As Integer)
        ddlPregunta.Items.Clear()
        Dim preguntas As List(Of PreguntaDTO) = PreguntaBLL.ObtenerInstancia.ListarPorIdServicio(id)
        For Each pregunta In preguntas
            Dim item As New ListItem
            item.Text = pregunta.pregunta
            item.Value = pregunta.id
            ddlPregunta.Items.Add(item)
        Next
        If preguntas.Count = 0 Then
            Dim item As New ListItem
            item.Text = "No hay preguntas"
            item.Value = 0
            ddlPregunta.Items.Add(item)
        End If
    End Sub


    Private Sub ddlPregunta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPregunta.SelectedIndexChanged
        CargarRespuestas(ddlPregunta.SelectedValue)
    End Sub


    Public Sub CargarRespuestas(id As Integer)
        Dim respuestas As List(Of RespuestaDTO) = RespuestaBLL.ObtenerInstancia.ListarPorIdPregunta(id)
        grdRespuestas.DataSource = respuestas
        grdRespuestas.DataBind()
    End Sub

    Private Sub btnResponder_Click(sender As Object, e As EventArgs) Handles btnResponder.Click
        Try
            Dim respuesta As New RespuestaDTO With {.respuesta = txtRespuesta.Text, .id_pregunta = ddlPregunta.SelectedValue}
            RespuestaBLL.ObtenerInstancia.Agregar(respuesta)
            Response.Redirect("/Respuestas.aspx")
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    'Responder


End Class