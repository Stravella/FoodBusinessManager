Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class Pago
    Inherits System.Web.UI.Page

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Current.Session("Accion") = "Compra" Then
            Dim cliente As ClienteDTO = DirectCast(Current.Session("Cliente"), ClienteDTO)
            Dim carrito As List(Of ServicioCarritoDTO) = DirectCast(Current.Session("Carrito"), List(Of ServicioCarritoDTO))
            Dim total As Double = 0
            For Each serv In carrito
                total = total + serv.importeTotal
            Next

            Dim factura As New FacturaDTO With {
                        .cliente = cliente,
                        .total = total
                    }
            Dim nuevaNota As New NotaCreditoDTO


            If Current.Session("NotasCredito") IsNot Nothing Then
                Dim lsNotas As List(Of NotaCreditoDTO) = DirectCast(Current.Session("NotasCredito"), List(Of NotaCreditoDTO))
                Dim diferenciaNotasImporte As Double = Current.Session("DiferenciaNotas")
                If Math.Abs(diferenciaNotasImporte) > 0 Then

                    'Creo una nota por la diferencia
                    nuevaNota.estado = New EstadoNotaDTO With {.id = 2} 'pendiente
                    nuevaNota.importe = Math.Abs(diferenciaNotasImporte)
                    nuevaNota.fecha = Now
                    nuevaNota.concepto = "Nota de credito correspondiente a la fecha : " & Now
                    nuevaNota.id_factura = factura.id
                    nuevaNota.cliente = factura.cliente

                    factura.notasCredito = lsNotas
                    factura.tarjeta = Nothing
                    factura.importeTarjeta = 0
                Else
                    If Current.Session("RequiereTarjeta") = True Then
                        Dim tarjeta As TarjetaDTO = DirectCast(Current.Session("Tarjeta"), TarjetaDTO)
                        factura.tarjeta = tarjeta
                        factura.importeTarjeta = Math.Abs(diferenciaNotasImporte)
                    Else 'La nota de credito es justa y no necesito tarjeta
                        factura.tarjeta = Nothing
                        factura.notasCredito = lsNotas
                        factura.importeTarjeta = 0
                    End If
                End If
            Else
                Dim tarjeta As TarjetaDTO = DirectCast(Current.Session("Tarjeta"), TarjetaDTO)
                factura.notasCredito = Nothing
                factura.tarjeta = tarjeta
                factura.importeTarjeta = total
            End If
            FacturaBLL.ObtenerInstancia.Agregar(factura)
            If nuevaNota.importe > 0 Then ' Aquí veo si tengo que agregar una nota               
                NotasCreditoBLL.ObtenerInstancia.Agregar(nuevaNota)
            End If

            Dim compra As New CompraDTO With {
                        .carrito = carrito,
                        .cliente = cliente,
                        .fecha = Now,
                        .total = total,
                        .estado = New EstadoCompraDTO With {.id = 1}, 'Estado: Pendiente
                        .factura = factura
                    }
            CompraBLL.ObtenerInstancia.Agregar(compra)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                                    .FechaHora = Now(),
                                    .usuario = usuarioLogeado,
                                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 27}, 'Suceso: Compra
                                    .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                                    .observaciones = "Se realizo la compra :" & compra.id
                           }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            'TODO: Redirect a encuesta
        End If
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
            CargarCarrito()
            CargarCliente()
            CargarNotaCredito()
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

    Public Sub CargarCliente()
        Try
            If Current.Session("Cliente") Is Nothing Then
                MostrarModal("Error", "Lo siento! Para continuar debe encontrarse logueado",, True)
                Response.Redirect("/Home.aspx")
            Else
                Dim cliente As ClienteDTO = DirectCast(Current.Session("Cliente"), ClienteDTO)
                txtNombre.Text = cliente.usuario.nombre
                txtApellido.Text = cliente.usuario.apellido
                txtCUIT.Text = cliente.CUIT
                txtDireccion.Text = cliente.domicilio
                txtProvincia.Text = cliente.provincia
                txtCP.Text = cliente.CP
                'Cargo el nombre en la tarjeta
                txtNombreApe.Text = cliente.usuario.apellido & " " & cliente.usuario.nombre
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al cargar su usuario",, True)
        End Try
    End Sub

    Sub CargarNotaCredito()
        Try
            If Current.Session("Cliente") Is Nothing Then
                MostrarModal("Error", "Lo siento! Para continuar debe encontrarse logueado",, True)
                Response.Redirect("/Home.aspx")
            Else
                Dim cliente As ClienteDTO = DirectCast(Current.Session("Cliente"), ClienteDTO)
                Dim lsNotas As New List(Of NotaCreditoDTO)
                lsNotas = NotasCreditoBLL.ObtenerInstancia.ListarRedimiblesPorCliente(cliente.id)
                grdNotasCredito.DataSource = lsNotas
                grdNotasCredito.DataBind()
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al cargar sus notas de crédito",, True)
        End Try
    End Sub

    Private Sub btnValidarTarjeta_Click(sender As Object, e As EventArgs) Handles btnValidarTarjeta.Click
        Try
            Dim tarj As New TarjetaDTO With {
                   .id = 0,
                   .nro = txtNumeroTarjeta.Text,
                   .codigo_seguridad = txtCodigoSeguridad.Text,
                   .vencimiento = txtFechaVencimiento.Text
            }
            tarj = TarjetaBLL.ObtenerInstancia.Obtener(tarj)
            lblRespuestaTarjeta.Visible = True
            If tarj.id = 0 Then
                lblRespuestaTarjeta.CssClass = "danger"
                lblRespuestaTarjeta.Text = "La tarjeta no existe"
                Current.Session("TarjetaValida") = False
            Else 'La tarjeta existe
                Select Case tarj.estado.estado
                    Case "Valida"
                        lblRespuestaTarjeta.CssClass = "success"
                        lblRespuestaTarjeta.Text = "La tarjeta es válida"
                        Current.Session("TarjetaValida") = True
                    Case "Sin fondos"
                        lblRespuestaTarjeta.CssClass = "danger"
                        lblRespuestaTarjeta.Text = "La tarjeta no tiene fondos"
                        Current.Session("TarjetaValida") = False
                    Case "Inactiva"
                        lblRespuestaTarjeta.CssClass = "danger"
                        lblRespuestaTarjeta.Text = "La tarjeta esta inactiva"
                        Current.Session("TarjetaValida") = False
                End Select
                Select Case tarj.marca
                    Case "Visa Crédito"
                        lblRespuestaTarjeta.CssClass = "fas fa-cc-visa"
                    Case "Mastercard"
                        lblRespuestaTarjeta.CssClass = "fas fa-cc-mastercard"
                    Case "American Express"
                        lblRespuestaTarjeta.CssClass = "fas fa-cc-amex"
                End Select
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al validar su tarjeta",, True)
        End Try
    End Sub

    Private Sub btnComprar_Click(sender As Object, e As EventArgs) Handles btnComprar.Click
        Try
            If Current.Session("TarjetaValida") Is Nothing Then
                MostrarModal("Error", "Para continuar, debe validar el medio de pago primero",, True)
            Else
                If Current.Session("TarjetaValida") = True Then
                    Dim cliente As ClienteDTO = DirectCast(Current.Session("Cliente"), ClienteDTO)
                    Dim carrito As List(Of ServicioCarritoDTO) = DirectCast(Current.Session("Carrito"), List(Of ServicioCarritoDTO))
                    Dim total As Double = 0
                    For Each serv In carrito
                        total = total + serv.importeTotal
                    Next
                    'Guardo la accion para el modal
                    Current.Session("Accion") = "Compra"


                    'Notas de crédito
                    Dim totalNotas As Double = 0
                    Dim lsNotas As New List(Of NotaCreditoDTO)
                    For Each gvrow As GridViewRow In grdNotasCredito.Rows
                        Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                        If checkbox.Checked = True Then
                            Dim nota As New NotaCreditoDTO
                            nota = NotasCreditoBLL.ObtenerInstancia.Obtener(Convert.ToInt16(gvrow.Cells(1).Text))
                            lsNotas.Add(nota)
                        End If
                    Next
                    If lsNotas.Count > 0 Then 'Hay notas de credito
                        Dim servicio As New Diferencia
                        For Each nota In lsNotas
                            totalNotas = totalNotas + nota.importe
                        Next
                        Dim diferenciaNotasImporte As Double = servicio.Diferencia(totalNotas, total)
                        Current.Session("NotasCredito") = lsNotas
                        Current.Session("DiferenciaNotas") = diferenciaNotasImporte
                        Select Case diferenciaNotasImporte
                            Case > 0 'Necesito generar una nueva nota
                                Current.Session("RequiereTarjeta") = False
                                MostrarModal("Confirmar compra", "Se abonara un total de: $" & total & " con la/s notas de credito seleccionadas. Se generara una nueva nota de credito por la diferencia de : $" & Math.Abs(diferenciaNotasImporte),, True)
                            Case < 0 'Necesito pagar la diferencia
                                Dim tarj As New TarjetaDTO With {
                                    .id = 0,
                                    .nro = txtNumeroTarjeta.Text,
                                    .codigo_seguridad = txtCodigoSeguridad.Text,
                                    .vencimiento = txtFechaVencimiento.Text
                                }
                                tarj = TarjetaBLL.ObtenerInstancia.Obtener(tarj)
                                Current.Session("RequiereTarjeta") = True
                                Current.Session("Tarjeta") = tarj
                                MostrarModal("Confirmar compra", "Se abonara un total de: $" & total & ", de los cuales $" & totalNotas & " se abonara con la/s notas de credito seleccionadas, y el $" & Math.Abs(diferenciaNotasImporte) & " restante con la tarjeta nro. " & tarj.nro,, True)
                            Case = 0 'Es justo el importe, no necesito tarjeta ni nota
                                Current.Session("RequiereTarjeta") = False
                                MostrarModal("Confirmar compra", "Se abonara $" & total & " con la/s notas de credito seleccionadas",, True)
                        End Select
                    Else 'No hay notas de credito
                        Dim tarj As New TarjetaDTO With {
                                .id = 0,
                                .nro = txtNumeroTarjeta.Text,
                                .codigo_seguridad = txtCodigoSeguridad.Text,
                                .vencimiento = txtFechaVencimiento.Text
                            }
                        tarj = TarjetaBLL.ObtenerInstancia.Obtener(tarj)
                        Current.Session("RequiereTarjeta") = True
                        Current.Session("Tarjeta") = tarj
                        MostrarModal("Confirmar compra", "Se abonara $" & total & " con la tarjeta nro. " & tarj.nro,, True)
                    End If

                Else
                    MostrarModal("Error", "El medio de pago seleccionado es invalido",, True)
                End If
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al realizar la compra",, True)
        End Try
    End Sub
End Class