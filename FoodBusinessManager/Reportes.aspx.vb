Imports BLL
Imports Entidades
Imports System.Web.HttpContext
Imports System.Web.UI.DataVisualization.Charting

Public Class Reportes
    Inherits System.Web.UI.Page


#Region "modal"
    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Response.Redirect("/Reportes.aspx")
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
        Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
        If cancelar = True Then
            btnCancelar.Visible = True
        Else
            btnCancelar.Visible = False
        End If
        tituloModal.Text = titulo
        bodyModal.Text = body

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#myModal').modal();", True)
        panelMensaje.Update()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarEncuestas()
            CargarFiltroVentas()
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

    Private Sub CargarFiltroVentas()
        'Cargar Filtros Año -> Cuento de 2018 a 2020
        Dim item As New ListItem With {.Value = 0, .Text = "Seleccione :"}
        ddlAño.Items.Add(item)
        ddlAñoDesde.Items.Add(item)
        ddlAñoHasta.Items.Add(item)

        For i As Integer = 2018 To 2020
            item = New ListItem
            item.Text = i.ToString
            item.Value = i
            ddlAño.Items.Add(item)
            ddlAñoDesde.Items.Add(item)
            ddlAñoHasta.Items.Add(item)
        Next
        'Cargar Filtros meses    
        item = New ListItem With {.Value = 0, .Text = "Seleccione :"}
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 1
        item.Text = "Enero"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 2
        item.Text = "Febrero"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 3
        item.Text = "Marzo"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 4
        item.Text = "Abril"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 5
        item.Text = "Mayo"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 6
        item.Text = "Junio"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 7
        item.Text = "Julio"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 8
        item.Text = "Agosto"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 9
        item.Text = "Septiembre"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 10
        item.Text = "Octubre"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 11
        item.Text = "Noviembre"
        ddlMes.Items.Add(item)
        item = New ListItem
        item.Value = 12
        item.Text = "Diciembre"
        ddlMes.Items.Add(item)

        'Selecciono por default el primero
        ddlAño.SelectedIndex = 0
        ddlAñoDesde.SelectedIndex = 0
        ddlAñoHasta.SelectedIndex = 0
        ddlMes.SelectedIndex = 0
        'ddlAño.Items.FindByValue(0).Selected = True
        'ddlAñoDesde.Items.FindByValue(0).Selected = True
        'ddlAñoHasta.Items.FindByValue(0).Selected = True
        'ddlMes.Items.FindByValue(0).Selected = True
    End Sub

    Private Sub ddlReportes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportes.SelectedIndexChanged
        If ddlReportes.SelectedIndex > -1 Then
            If ddlReportes.SelectedValue = 0 Then 'no muestro
                panelEncuestas.Visible = False
                panelValoracion.Visible = False
                panelVentas.Visible = False
            End If
            If ddlReportes.SelectedValue = 1 Then 'Encuestas
                panelEncuestas.Visible = True
                panelValoracion.Visible = False
                panelVentas.Visible = False
            End If
            If ddlReportes.SelectedValue = 2 Then 'Valoracion producto
                MostrarValoracion()
                panelValoracion.Visible = True
                panelEncuestas.Visible = False
                panelVentas.Visible = False
            End If
            If ddlReportes.SelectedValue = 3 Then 'Ventas
                panelValoracion.Visible = False
                panelEncuestas.Visible = False
                panelVentas.Visible = True
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

#Region "Ventas"
    Private Sub btnFiltrarAnual_Click(sender As Object, e As EventArgs) Handles btnFiltrarAnual.Click
        If ddlAñoDesde.SelectedValue = 0 Or ddlAñoHasta.SelectedValue = 0 Then
            MostrarModal("Advertencia", "Por favor verifique los filtros de años elegidos.",,)
        Else
            Dim reportes As New List(Of ReporteVentasDTO)
            reportes = ReporteVentasBLL.ObtenerInstancia.ReporteAñoDesdeHasta(ddlAñoDesde.SelectedValue, ddlAñoHasta.SelectedValue)
            gv_Ventas.DataSource = reportes
            gv_Ventas.DataBind()

            'agrego el titulo
            chVentas.Titles.Add("Reporte anual")

            'creo el area y habilito el gráfico en 3D
            Dim area As New ChartArea
            'area.Area3DStyle.Enable3D = True
            area.AxisX.Title = "Mes"
            area.AxisY.Title = "Importe"
            chVentas.ChartAreas.Add(area)

            'agrego la serie
            Dim serie As New Series("Reportes")
            'digo q tipo de grafico quiero 
            serie.ChartType = SeriesChartType.Column
            For Each item In reportes
                serie.Points.AddXY(item.nombre, item.importe)
            Next

            chVentas.Series.Add(serie)

        End If
    End Sub

    Private Sub btnFiltrarMensual_Click(sender As Object, e As EventArgs) Handles btnFiltrarMensual.Click
        If ddlAño.SelectedIndex = 0 Then
            MostrarModal("Advertencia", "Por favor verifique haber seleccionado el año que desea visualizar",,)
        Else
            Dim reportes As New List(Of ReporteVentasDTO)
            reportes = ReporteVentasBLL.ObtenerInstancia.ReporteAnual(ddlAño.SelectedValue)
            gv_Ventas.DataSource = reportes
            gv_Ventas.DataBind()

            'agrego el titulo
            chVentas.Titles.Add("Reporte mensual")

            'creo el area y habilito el gráfico en 3D
            Dim area As New ChartArea
            'area.Area3DStyle.Enable3D = True
            area.AxisX.Title = "Numero mes"
            area.AxisY.Title = "Importe"
            chVentas.ChartAreas.Add(area)

            'agrego la serie
            Dim serie As New Series("Reportes")
            'digo q tipo de grafico quiero 
            serie.ChartType = SeriesChartType.Column
            For Each item In reportes
                serie.Points.AddXY(item.nombre, item.importe)
            Next

            chVentas.Series.Add(serie)
        End If
    End Sub

    Private Sub btnFiltrarSemanal_Click(sender As Object, e As EventArgs) Handles btnFiltrarSemanal.Click
        If ddlAño.SelectedIndex = 0 Then
            MostrarModal("Advertencia", "Por favor verifique haber seleccionado el año que desea visualizar",,)
        Else
            Dim reportes As New List(Of ReporteVentasDTO)
            reportes = ReporteVentasBLL.ObtenerInstancia.ReporteSemanal(ddlAño.SelectedValue)
            gv_Ventas.DataSource = reportes
            gv_Ventas.DataBind()

            'agrego el titulo
            chVentas.Titles.Add("Reporte semanal")

            'creo el area y habilito el gráfico en 3D
            Dim area As New ChartArea
            'area.Area3DStyle.Enable3D = True
            area.AxisX.Title = "Numero semana"
            area.AxisY.Title = "Importe"
            chVentas.ChartAreas.Add(area)

            'agrego la serie
            Dim serie As New Series("Reportes")
            'digo q tipo de grafico quiero 
            serie.ChartType = SeriesChartType.Column
            For Each item In reportes
                serie.Points.AddXY(item.nombre, item.importe)
            Next

            chVentas.Series.Add(serie)
        End If
    End Sub

    Private Sub btnFiltrarDiario_Click(sender As Object, e As EventArgs) Handles btnFiltrarDiario.Click
        If ddlMes.SelectedIndex = 0 Then
            MostrarModal("Advertencia", "Por favor verifique haber seleccionado el mes que desea visualizar",,)
        Else
            Dim reportes As New List(Of ReporteVentasDTO)
            reportes = ReporteVentasBLL.ObtenerInstancia.ReporteMensual(ddlMes.SelectedValue)
            gv_Ventas.DataSource = reportes
            gv_Ventas.DataBind()

            'agrego el titulo
            chVentas.Titles.Add("Reporte diario")

            'creo el area y habilito el gráfico en 3D
            Dim area As New ChartArea
            'area.Area3DStyle.Enable3D = True
            area.AxisX.Title = "Numero dia"
            area.AxisY.Title = "Importe"
            chVentas.ChartAreas.Add(area)

            'agrego la serie
            Dim serie As New Series("Reportes")
            'digo q tipo de grafico quiero 
            serie.ChartType = SeriesChartType.Column
            For Each item In reportes
                serie.Points.AddXY(item.nombre, item.importe)
            Next

            chVentas.Series.Add(serie)
        End If
    End Sub

#End Region

End Class