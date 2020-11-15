Imports BLL
Imports Entidades
Imports System.Web.HttpContext

Public Class Compra
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
            If ValidarCliente() = True Then
                CargarCarrito()
            Else
                MostrarModal("Se requiere logueo", "Lo siento! Para continuar operando debe estar logueado",, True)
                Response.Redirect("/Home.aspx")
            End If
        End If
    End Sub


    Public Sub CargarCarrito()
        Try
            If Current.Session("Carrito") Is Nothing Then
                MostrarModal("No hay elementos", "Lo siento! Su carrito no contiene elementos",, True)
            Else
                Dim total As Double = 0
                Dim carrito As List(Of ServicioCarritoDTO) = DirectCast(Current.Session("Carrito"), List(Of ServicioCarritoDTO))
                'Cargo el total
                For Each serv In carrito
                    total = total + serv.importeTotal
                Next
                gv_Carrito.DataSource = carrito
                gv_Carrito.DataBind()
                lblPrecioTotal.Text = "El monto total es: $" & total
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al cargar su carrito",, True)
        End Try
    End Sub


    Public Function ValidarCliente() As Boolean
        Dim cliente As New ClienteDTO
        cliente = Current.Session("Cliente")
        If cliente Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub gv_Carrito_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Carrito.RowCommand
        Dim carrito As List(Of ServicioCarritoDTO) = DirectCast(Current.Session("Carrito"), List(Of ServicioCarritoDTO))
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        Dim servicioCarrito As New ServicioCarritoDTO

        If e.CommandName = "Remover" Then
            For Each serv As ServicioCarritoDTO In carrito
                If serv.servicio.id = id Then
                    servicioCarrito = serv
                End If
            Next
            carrito.Remove(servicioCarrito)
            Current.Session("Carrito") = carrito
            CargarCarrito()
        End If
    End Sub

    Private Sub btnActualizarCarrito_Click(sender As Object, e As EventArgs) Handles btnActualizarCarrito.Click
        Try
            Dim carrito As List(Of ServicioCarritoDTO) = DirectCast(Current.Session("Carrito"), List(Of ServicioCarritoDTO))
            For i As Integer = 0 To gv_Carrito.Rows.Count - 1
                Dim nombre As String = gv_Carrito.Rows(i).Cells(1).Text
                Dim cantidad As String = DirectCast(gv_Carrito.Rows(i).FindControl("txtCantidad"), TextBox).Text
                For Each serv As ServicioCarritoDTO In carrito
                    If serv.servicio.nombre = nombre Then
                        If serv.servicio.nombre = "Free" And cantidad > 1 Then
                            MostrarModal("Solo puede probar la version free por 30 dias", "Lo siento! Ya tiene la prueba de 30 dias agregada al carrito",, True)
                        Else
                            serv.cantidad = Integer.Parse(cantidad)
                            serv.importeTotal = cantidad * serv.servicio.precio
                        End If
                    End If
                Next
            Next


            Dim cantidadItemsCarrito As Integer
            For Each serv As ServicioCarritoDTO In carrito
                cantidadItemsCarrito = cantidadItemsCarrito + serv.cantidad
            Next

            Current.Session("CantidadItemsCarrito") = cantidadItemsCarrito

            Current.Session("Carrito") = carrito
            Response.Redirect("/Compra.aspx")
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al cargar su carrito",, True)
        End Try




    End Sub

    Private Sub btnComprar_Click(sender As Object, e As EventArgs) Handles btnComprar.Click
        Response.Redirect("/Pago.aspx")
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("/Home.aspx")
    End Sub

End Class