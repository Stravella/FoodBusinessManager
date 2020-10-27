Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class Free
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarServicio()
            If Current.Session("usuario") Is Nothing Then
                panelPreguntas.Enabled = False
            Else
                panelPreguntas.Enabled = True
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
        Try
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
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

    Public Sub CargarServicio()
        Dim nombreServicio As String = Request.QueryString("Serv")
        Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.ObtenerPorNombre(nombreServicio)
        lblServicio.Text = servicio.nombre
        imgServicio.ImageUrl = servicio.imagen.Img64
        lblDescripcion.Text = servicio.descripcion
        lblPrecio.Text = "$ " & servicio.precio
        repeaterCaracteristicas.DataSource = servicio.caracteristicas
        repeaterCaracteristicas.DataBind()
        Dim preguntas As List(Of PreguntaDTO) = PreguntaBLL.ObtenerInstancia.ListarPorIdServicio(servicio.id)
        repeaterPreguntas.DataSource = preguntas
        repeaterPreguntas.DataBind()
    End Sub

    Private Sub repeaterPreguntas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repeaterPreguntas.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim repeaterHijo As Repeater = TryCast(e.Item.FindControl("repeaterRespuestas"), Repeater)
            Dim pregunta As PreguntaDTO = TryCast(e.Item.DataItem, PreguntaDTO)
            repeaterHijo.DataSource = pregunta.respuestas
            repeaterHijo.DataBind()
        End If
    End Sub

    Private Sub btnPregunta_Click(sender As Object, e As EventArgs) Handles btnPregunta.Click
        Try
            If panelPreguntas.Enabled = False Then
                MostrarModal("Pregunta", "Para dejar una pregunta debe encontrarse logueado!",, True)
            Else
                Dim nombreServicio As String = Request.QueryString("Serv")
                Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.ObtenerPorNombre(nombreServicio)
                Dim pregunta As New PreguntaDTO With {
                    .pregunta = txtPregunta.Text,
                    .id_servicio = servicio.id,
                    .fechaVencimiento = DateAdd(DateInterval.Year, 1, Now),
                    .usuario = Current.Session("usuario")
                }
                PreguntaBLL.ObtenerInstancia.Agregar(pregunta)
                MostrarModal("Pregunta", "Gracias por realizar una pregunta! Le contestaremos a la brevedad",, True)
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub btnComprar_Click(sender As Object, e As EventArgs) Handles btnComprar.Click
        Dim nombreServicio As String = Request.QueryString("Serv")
        Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.ObtenerPorNombre(nombreServicio)

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
            Response.Redirect(Request.Url.PathAndQuery)


        End If

    End Sub
End Class