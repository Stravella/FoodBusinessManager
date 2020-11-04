Imports BLL
Imports Entidades
Imports System.Web.HttpContext
Imports System.Web.UI.DataVisualization.Charting

Public Class Reportes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MostrarValoracion()
    End Sub


    Private Sub MostrarValoracion()

        chValoracion.Titles.Clear()
        chValoracion.ChartAreas.Clear()
        chValoracion.Series.Clear()

        Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar

        'Dim Reportes As DataVisualization.Charting.Chart = chValoracion

        'Dim Serie1 = Reportes.Series("Series1")
        'Serie1.Points.Clear()
        'For Each item In servicios
        '    Serie1.Points.AddXY(item.nombre, item.valoracion)
        'Next
        'Reportes.Series("Series1").ChartType = DataVisualization.Charting.SeriesChartType.Bar
        'Dim ChartArea = Reportes.ChartAreas("ChartArea1")
        'ChartArea.AxisX.Title = "Servicio"
        'ChartArea.AxisY.Title = "Valoracion"

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

    Private Sub ddlReportes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportes.SelectedIndexChanged
        If ddlReportes.SelectedIndex > -1 Then
            If ddlReportes.SelectedValue = 0 Then 'no muestro

            End If
            If ddlReportes.SelectedValue = 4 Then 'Valoracion producto
                panelValoracion.Visible = True
            End If

        End If
    End Sub
End Class