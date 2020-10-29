Imports BLL
Imports Entidades


Public Class Home
    Inherits System.Web.UI.Page

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarEncuesta()
        End If
    End Sub

    Private Sub CargarEncuesta()
        Try
            Dim listaOpinion As New List(Of EncuestaDTO)

            listaOpinion = EncuestaBLL.ObtenerInstancia.ListarOpiniones

            Dim random As New Random
            Dim r = random.Next(listaOpinion.Count)

            Dim oOpinion = New EncuestaDTO
            oOpinion = listaOpinion.Item(r)
            'filtro las opiniones por fecha
            Dim listaPreguntas As New List(Of EncuestaPreguntaDTO)
            For Each Pregunta As EncuestaPreguntaDTO In oOpinion.preguntas
                If Pregunta.FechaVenc > Now.ToShortDateString Then
                    listaPreguntas.Add(Pregunta)
                End If
            Next

            Dim rPregunta = random.Next(listaPreguntas.Count)
            Dim oPregunta = New EncuestaPreguntaDTO
            oPregunta = listaPreguntas.Item(rPregunta)

            Pregunta.InnerText = oPregunta.pregunta
            idPregunta.Value = oPregunta.ID

            rbPreguntas.DataSource = Nothing
            rbPreguntas.DataSource = oPregunta.Respuestas
            rbPreguntas.DataTextField = "Respuesta"
            rbPreguntas.DataValueField = "Id"
            rbPreguntas.DataBind()

            Dim Reportes As DataVisualization.Charting.Chart = chReportes
            Reportes.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False
            Reportes.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = False

        Catch ex As Exception
            panelEncuesta.Visible = False
        End Try
    End Sub

    'Votar 
    Protected Sub btnVotar_Click(sender As Object, e As EventArgs) Handles btnVotar.Click
        Try
            Dim idPreg As HiddenField = idPregunta
            RespuestaEncuestaBLL.ObtenerInstancia.Responder(idPreg.Value, rbPreguntas.SelectedValue)
            VerRestultadoEncuesta()
        Catch ex As Exception

        End Try
    End Sub



    'Ver resultado en el grafico

    Private Sub VerRestultadoEncuesta()
        Try
            Dim idPreg As HiddenField = idPregunta
            Dim listRespuestas As List(Of RespuestaEncuestaDTO) = RespuestaEncuestaBLL.ObtenerInstancia.ListarPorIdPregunta(idPreg.Value)
            Dim Reportes As DataVisualization.Charting.Chart = chReportes

            Dim Serie1 = Reportes.Series("Series1")
            Serie1.Points.Clear()
            For Each item In listRespuestas
                Serie1.Points.AddXY(item.Respuesta, item.Cantidad)
            Next
            Reportes.Series("Series1").ChartType = DataVisualization.Charting.SeriesChartType.Pie
            Dim ChartArea = Reportes.ChartAreas("ChartArea1")
            ChartArea.AxisX.Title = idPreg.Value
            ChartArea.AxisY.Title = "Cantidad de respuestas"

            chReportes.Visible = True
            rbPreguntas.Visible = False
            btnVotar.Visible = False
            UpdatePanel1.Update()
        Catch ex As Exception

        End Try
    End Sub


End Class