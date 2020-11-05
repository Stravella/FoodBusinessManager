Imports BLL
Imports Entidades
Imports System.Web.HttpContext
Imports System.Web.UI.DataVisualization.Charting

Public Class Reportes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarEncuestas()
        End If
    End Sub


    Private Sub MostrarValoracion()
        chValoracion.Titles.Clear()
        chValoracion.ChartAreas.Clear()
        chValoracion.Series.Clear()

        Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar
        'agrego el titulo
        chValoracion.Titles.Add("Valoracion de productos")

        'creo el area y habilito el gráfico en 3D
        Dim area As New ChartArea
        area.Area3DStyle.Enable3D = True
        area.AxisX.Title = "Servicio"
        area.AxisY.Title = "Valoracion"
        chValoracion.ChartAreas.Add(area)

        'agrego la serie
        Dim serie As New Series("Servicios")
        'digo q tipo de grafico quiero 
        serie.ChartType = SeriesChartType.Bar
        For Each item In servicios
            serie.Points.AddXY(item.nombre, item.valoracion)
        Next

        chValoracion.Series.Add(serie)

    End Sub


    Private Sub CargarEncuestas()
        Dim encuestas As New List(Of EncuestaDTO)
        encuestas = EncuestaBLL.ObtenerInstancia.ListarEncuestas
        ddlEncuestas.Items.Add(New ListItem("Seleccione", "0"))
        For Each encuesta In encuestas
            Dim item As New ListItem
            item.Text = encuesta.nombre
            item.Value = encuesta.id
            ddlEncuestas.Items.Add(item)
        Next
        ddlEncuestas.SelectedIndex = 0
    End Sub



    Private Sub ddlReportes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportes.SelectedIndexChanged
        If ddlReportes.SelectedIndex > -1 Then
            If ddlReportes.SelectedValue = 0 Then 'no muestro

            End If
            If ddlReportes.SelectedValue = 1 Then 'Encuestas
                panelEncuestas.Visible = True
                panelValoracion.Visible = False
            End If
            If ddlReportes.SelectedValue = 2 Then 'Valoracion producto
                MostrarValoracion()
                panelValoracion.Visible = True
                panelEncuestas.Visible = False
            End If
            If ddlReportes.SelectedValue = 3 Then 'Ventas

            End If
        End If
    End Sub

    Private Sub ddlEncuestas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEncuestas.SelectedIndexChanged
        If ddlEncuestas.SelectedValue <> 0 Then
            Dim encuesta As EncuestaDTO = EncuestaBLL.ObtenerInstancia.Obtener(ddlEncuestas.SelectedValue)
            rptPreguntas.DataSource = encuesta.preguntas
            rptPreguntas.DataBind()
        End If
    End Sub

    Private Sub rptPreguntas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptPreguntas.ItemDataBound
        'en el dataBound le asigno el datasource el gráfico
        Dim grafico = DirectCast(e.Item.FindControl("chPregunta"), Chart)

        If e.Item.DataItem IsNot Nothing Then
            Dim pregunta As EncuestaPreguntaDTO = TryCast(e.Item.DataItem, EncuestaPreguntaDTO)

            grafico.DataSource = pregunta.Respuestas

            grafico.Titles.Add(pregunta.pregunta)
            grafico.Series("Series1").XValueMember = "Respuesta"
            grafico.Series("Series1").YValueMembers = "Cantidad"
            grafico.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            grafico.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False

            grafico.DataBind()

        End If

    End Sub
End Class