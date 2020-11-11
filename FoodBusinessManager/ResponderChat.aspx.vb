Imports System.Web.HttpContext
Imports Entidades
Imports BLL

Public Class ResponderChat
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
                CargarChats()
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
        'Acá tengo que hidear el modal
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Response.Redirect("/ResponderChat.aspx")
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
        Else
            Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
            btnCancelar.Visible = False
        End If
        tituloModal.Text = titulo
        bodyModal.Text = body

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#myModal').modal();", True)
        panelMensaje.Update()
    End Sub

#End Region

    Public Sub CargarChats()
        gv_Chat.DataSource = ChatBLL.ObtenerInstancia.Listar()
        gv_Chat.DataBind()
    End Sub

    Public Sub CargarRespuestas(chat As ChatSesionDTO)
        gv_Mensajes.DataSource = chat.mensajes
        gv_Mensajes.DataBind()
        gv_Mensajes.Visible = True
    End Sub

    Protected Sub gv_Chat_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Chat.RowCommand
        Dim chat As New ChatSesionDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        chat = ChatBLL.ObtenerInstancia.Obtener(id)
        If e.CommandName = "Responder" Then
            If chat.fechaFin = "1/1/1900 12:00:00 AM" Then
                btnCancelar.Visible = True
                btnResponder.Visible = True
                Session("chat") = chat
                CargarRespuestas(chat)
            Else
                MostrarModal("Chat finalizado", "Este chat ya fue finalizado por el cliente",,)
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        gv_Mensajes.DataSource = Nothing
        gv_Mensajes.Visible = False
        btnCancelar.Visible = False
        btnResponder.Visible = False
        Session("chat") = Nothing
    End Sub

    Private Sub btnResponder_Click(sender As Object, e As EventArgs) Handles btnResponder.Click
        Try
            Dim chat As New ChatSesionDTO
            Dim cliente As New ClienteDTO
            cliente = Session("cliente")
            chat = Session("chat")
            chat.usuarioAtendio = cliente.usuario
            ChatBLL.ObtenerInstancia.Modificar(chat)
            Dim mensaje As New ChatMensajeDTO With {
                .mensaje = txtMensaje.Text,
                .fecha = DateTime.Now,
                .username = cliente.usuario.username
            }
            ChatBLL.ObtenerInstancia.CrearMensaje(mensaje, chat)
            Dim bitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = cliente.usuario,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 41}, 'Suceso: respuesta chat
                .criticidad = New CriticidadDTO With {.id = 1}, 'Criticidad: Baja
                .observaciones = "Se respondio un mensaje, la respuesta fue:" & mensaje.mensaje
        }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            MostrarModal("Respuesta a mensaje", "Se respondio el mensaje correctamente",,)

        Catch ex As Exception
            Dim cliente As New ClienteDTO
            cliente = Session("cliente")
            Dim bitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = cliente.usuario,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 41}, 'Suceso: respuesta chat
                .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                .observaciones = "Ocurrio un error al contestar un chat" & ex.Message
        }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            MostrarModal("Error", "Lo siento! Ocurrio un error, intente nuevamente o contacte a su administrador",,)
        End Try
    End Sub



End Class