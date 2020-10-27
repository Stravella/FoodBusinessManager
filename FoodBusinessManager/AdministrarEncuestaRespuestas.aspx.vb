Imports Entidades
Imports System.Web.HttpContext
Imports BLL
Public Class AdministrarRespuestaOpinion
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
                Dim respuesta As RespuestaEncuestaDTO = Current.Session("entidadModal")
                RespuestaEncuestaBLL.ObtenerInstancia.Eliminar(respuesta.id)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                                .FechaHora = Now(),
                                                .usuario = usuarioLogeado,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 33}, 'Suceso: Eliminacion respuesta
                                                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                                                .observaciones = "Se elimino la respuesta : " & respuesta.respuesta
                                       }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Modificar" Then
                Dim respuesta As RespuestaEncuestaDTO = Current.Session("entidadModal")
                RespuestaEncuestaBLL.ObtenerInstancia.Modificar(respuesta)
                'Actualizo el orden y el id de Catalogo                
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                        .FechaHora = Now(),
                                        .usuario = usuarioLogeado,
                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 34}, 'Suceso: Modificacion opinion
                                        .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Alta
                                        .observaciones = "Se modifico la respuesta : " & respuesta.respuesta
                                }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
            Response.Redirect("/AdministrarEncuestaRespuestas.aspx")
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
            CargarRespuestas()
        End If
    End Sub

    Public Sub CargarRespuestas()
        Dim lsRespuestas As New List(Of RespuestaEncuestaDTO)
        lsRespuestas = RespuestaEncuestaBLL.ObtenerInstancia.Listar
        gvRespuestas.DataSource = lsRespuestas
        gvRespuestas.DataBind()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim respuesta As New RespuestaEncuestaDTO With {
                .respuesta = txtRespuesta.Text,
                .cantidad = 0
            }
            RespuestaEncuestaBLL.ObtenerInstancia.Agregar(respuesta)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                    .FechaHora = Now(),
                    .usuario = usuarioLogeado,
                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 32}, 'Suceso: Creacion respuesta encuesta
                    .criticidad = New CriticidadDTO With {.id = 1}, 'Criticidad: Baja
                    .observaciones = "Se creo la respuesta : " & respuesta.respuesta
            }
            Response.Redirect("/AdministrarEncuestaRespuestas.aspx")
        Catch ex As Exception
            MostrarModal("Advertencia", "La fecha debe ser una fecha vigente",, True)
        End Try
    End Sub

    Private Sub gvOpiniones_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRespuestas.RowCommand
        Dim respuesta As New RespuestaEncuestaDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        respuesta = RespuestaEncuestaBLL.ObtenerInstancia.Obtener(id)
        If e.CommandName = "Editar" Then
            txtID.Text = respuesta.id
            txtRespuesta.Text = respuesta.respuesta
            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
        ElseIf e.CommandName = "Borrar" Then
            'Busco los servicios asociados           
            Current.Session("entidadModal") = respuesta
            Current.Session("accion") = "Eliminar"
            MostrarModal("¿Está seguro que desea borrar la respuesta " & respuesta.id & " ?", respuesta.respuesta,, True)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtID.Text = ""
        txtRespuesta.Text = ""
        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnCancelar.Visible = False
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim respuesta As New RespuestaEncuestaDTO With
                {.id = txtID.Text,
                .respuesta = txtRespuesta.Text,
                .cantidad = 0
                }
            Current.Session("entidadModal") = respuesta
            Current.Session("accion") = "Modificar"
            MostrarModal("¿Está seguro que desea modificar la respuesta " & respuesta.id & "?", respuesta.respuesta,, True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

End Class