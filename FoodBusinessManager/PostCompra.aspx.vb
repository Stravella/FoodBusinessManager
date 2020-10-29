Imports BLL
Imports Entidades
Imports System.Web.HttpContext

Public Class PostCompra
    Inherits System.Web.UI.Page

#Region "Modal Encuestas"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModalEncuesta, AddressOf modalEncuestaAceptar_click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalEncuestaAceptar_click(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim panel As UpdatePanel = Master.FindControl("PanelModal")
        Dim repeater As Repeater = panel.FindControl("repeaterPreguntas")
        For Each pregItem As RepeaterItem In repeater.Items
            'Dim itemIndex As Integer = pregItem.ItemIndex
            Dim lblIdPregunta = DirectCast(pregItem.FindControl("lblIdPregunta"), Label)
            Dim idPregunta As Integer = lblIdPregunta.Text
            Dim oPreg As EncuestaPreguntaDTO = EncuestaPreguntaBLL.ObtenerInstancia.Obtener(idPregunta)
            'Dim oPreg As EncuestaPreguntaDTO = EncuestaPreguntaBLL.ObtenerInstancia.Obtener(itemIndex)
            Dim rdlst = DirectCast(pregItem.FindControl("rdlRespuestas"), System.Web.UI.WebControls.RadioButtonList)

            Dim rtaElegida = (From rtas As ListItem In rdlst.Items
                              Where rtas.Selected
                              Select New RespuestaEncuestaDTO With {.id = rtas.Value, .respuesta = rtas.Text}).ToList(0)


            RespuestaEncuestaBLL.ObtenerInstancia.Responder(oPreg.ID, rtaElegida.id)
        Next

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#modalEncuesta').modal('hide')", True)
    End Sub

    Public Sub MostrarEncuesta(titulo As String, preguntas As List(Of EncuestaPreguntaDTO))
        Dim panel As UpdatePanel = Master.FindControl("PanelModal")
        Dim tituloModal As Label = panel.FindControl("titulo")
        Dim repeater As Repeater = panel.FindControl("repeaterPreguntas")

        tituloModal.Text = titulo

        repeater.DataSource = preguntas
        repeater.DataBind()

        Dim btnCancelar As Button = panel.FindControl("btnCancelar")
        btnCancelar.Visible = True

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#modalEncuesta').modal();", True)
        panel.Update()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarEncuestas()
        End If
    End Sub

    Public Sub CargarEncuestas()
        Dim encuesta As New List(Of EncuestaDTO)
        encuesta = EncuestaBLL.ObtenerInstancia.ListarEncuestas
        repeaterEncuesta.DataSource = encuesta
        repeaterEncuesta.DataBind()
    End Sub

    Private Sub repeaterEncuesta_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles repeaterEncuesta.ItemCommand
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        Dim encuesta As EncuestaDTO = EncuestaBLL.ObtenerInstancia.Obtener(id)
        If e.CommandName = "responder" Then
            MostrarEncuesta(encuesta.nombre, encuesta.preguntas)
        End If
    End Sub

    Protected Sub btnResponder_Click(sender As Object, e As EventArgs)
        If TypeOf sender Is Button Then
            Dim btn As Button = sender
            btn.Text = "Gracias!"
            btn.Enabled = False
        End If
    End Sub
End Class