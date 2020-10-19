Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class Servicios
    Inherits System.Web.UI.Page


#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim lsComparar As New List(Of ServicioDTO)
            CargarRepeater()
            CargarCaracteristicas()
        End If
    End Sub

    Protected Sub CargarCaracteristicas()
        Dim listaCaracteristicas As New List(Of CaracteristicaDTO)
        listaCaracteristicas = CaracteristicaBLL.ObtenerInstancia.Listar
        lstCaracteristicas.Items.Add(New ListItem("Todas", "0"))
        For Each caracteristica As CaracteristicaDTO In listaCaracteristicas
            Dim item As New ListItem
            item.Text = caracteristica.caracteristica
            item.Value = caracteristica.id
            lstCaracteristicas.Items.Add(item)
        Next
        lstCaracteristicas.SelectedIndex = 0
    End Sub

    Public Sub CargarRepeater()
        Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar
        repeaterServicios.DataSource = servicios
        repeaterServicios.DataBind()
    End Sub

    Private Sub repeaterServicios_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repeaterServicios.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim repeaterHijo As Repeater = TryCast(e.Item.FindControl("repeaterCaracteristicas"), Repeater)
            Dim servicio As ServicioDTO = TryCast(e.Item.DataItem, ServicioDTO)
            repeaterHijo.DataSource = servicio.caracteristicas
            repeaterHijo.DataBind()
        End If
    End Sub

    Protected Sub repeaterServicios_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles repeaterServicios.ItemCommand
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(id)
        If e.CommandName = "chk" Then

        End If
        If e.CommandName = "detalle" Then
            Dim url As String = "VistaServicios.aspx?Serv=" & servicio.nombre
            Response.Redirect(url)
        End If
        If e.CommandName = "comprar" Then
            Dim carrito As List(Of ServicioCarritoDTO)
            If Current.Session("Carrito") Is Nothing Then
                carrito = New List(Of ServicioCarritoDTO)
                Dim servicioCarrito As New ServicioCarritoDTO With {.servicio = servicio, .cantidad = 1, .importeTotal = servicio.precio * .cantidad}
                carrito.Add(servicioCarrito)
                Current.Session("Carrito") = carrito
            Else
                carrito = DirectCast(Current.Session("Carrito"), List(Of ServicioCarritoDTO))
                Dim existe As Boolean = False

                For Each serv As ServicioCarritoDTO In carrito
                    If serv.servicio.id = servicio.id Then
                        existe = True
                    End If
                Next

                If existe = False Then
                    carrito.Add(New ServicioCarritoDTO With {.servicio = servicio, .cantidad = 0})
                End If

                For Each serv As ServicioCarritoDTO In carrito
                    If serv.servicio.id = servicio.id Then
                        'Servicio Free puede existir 1 solo
                        If serv.servicio.nombre = "Free" And serv.cantidad > 0 Then
                            MostrarModal("Solo puede probar la version free por 30 dias", "Lo siento! Ya tiene la prueba de 30 dias agregada al carrito",, True)
                            Exit For
                        Else
                            serv.cantidad = serv.cantidad + 1
                            serv.importeTotal = serv.servicio.precio * serv.cantidad
                        End If
                    End If
                Next
                Current.Session("Carrito") = carrito
            End If
        End If
    End Sub


    Protected Sub Check(source As Object, e As EventArgs)
        Dim checkbox As CheckBox = TryCast(source, CheckBox)
        Dim id As Integer = checkbox.Attributes("CommandName")
        Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(id)
        Dim lsComparar As New List(Of ServicioDTO)

        If Current.Session("lsComparar") IsNot Nothing Then
            lsComparar = Current.Session("lsComparar")
        End If

        If checkbox.Checked = True Then 'Agrego a la lista
            lsComparar.Add(servicio)
            Current.Session("lsComparar") = lsComparar
        Else 'Quito de la lista
            Dim nuevaLista As New List(Of ServicioDTO)
            For Each serv As ServicioDTO In lsComparar
                If serv.id <> servicio.id Then
                    nuevaLista.Add(serv)
                End If
            Next
            lsComparar = nuevaLista
            Current.Session("lsComparar") = lsComparar
        End If

        Select Case lsComparar.Count
            Case > 1
                btnComparar.Enabled = True
            Case Else
                btnComparar.Enabled = False
        End Select

    End Sub

    Private Sub btnComparar_Click(sender As Object, e As EventArgs) Handles btnComparar.Click
        Dim lsComparar As New List(Of ServicioDTO)

        If Current.Session("lsComparar") IsNot Nothing Then
            lsComparar = Current.Session("lsComparar")
        End If

        repeaterServicios.DataSource = lsComparar
        repeaterServicios.DataBind()

        btnComparar.Visible = False
        btnCancelar.Visible = True
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        CargarRepeater()
        Current.Session("lsComparar") = Nothing
        btnComparar.Visible = True
        btnComparar.Enabled = False
        btnCancelar.Visible = False
    End Sub

    Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
        Try
            Dim filtroNombre As String
            If txtNombre.Text = "" Then
                filtroNombre = ""
            Else
                filtroNombre = txtNombre.Text
            End If
            Dim precioMin As Double
            If txtPrecioMin.Text = "" Then
                precioMin = 0
            Else
                precioMin = Convert.ToDouble(txtPrecioMin.Text)
            End If
            Dim precioMax As Double
            If txtPrecioMax.Text = "" Then
                precioMax = 0
            Else
                precioMax = Convert.ToDouble(txtPrecioMax.Text)
            End If
            Dim caracteristica As New CaracteristicaDTO
            If lstCaracteristicas.SelectedValue = 0 Then
                caracteristica = Nothing
            Else
                caracteristica = CaracteristicaBLL.ObtenerInstancia.Obtener(lstCaracteristicas.SelectedValue)
            End If
            Dim servicios As New List(Of ServicioDTO)
            servicios = ServicioBLL.ObtenerInstancia.Filtrar(filtroNombre, precioMin, precioMax, caracteristica)

            If servicios.Count = 0 Then
                MostrarModal("No hubo resultados", "Lo siento! La busqueda no devolvio resultados",, True)
                CargarRepeater()
            Else
                repeaterServicios.DataSource = servicios
                repeaterServicios.DataBind()
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error, contacte a su administrador",, True)
        End Try
    End Sub


End Class