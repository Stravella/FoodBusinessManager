Imports System.Web.HttpContext
Imports Entidades
Imports BLL

Public Class MisCompras
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCompras()
            CargarNotas()
        End If
    End Sub


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
        AddHandler Master.AceptarModalEncuesta, AddressOf modalEncuestaAceptar_click
    End Sub

    Public Sub CargarCompras()
        Try
            Dim cliente As ClienteDTO = DirectCast(Current.Session("Cliente"), ClienteDTO)
            Dim lsCompras As New List(Of CompraDTO)
            lsCompras = CompraBLL.ObtenerInstancia.ListarPorIdUsuario(cliente.id)
            gv_Compras.DataSource = lsCompras
            gv_Compras.DataBind()
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al cargar sus compras",, True)
        End Try
    End Sub

    Public Sub CargarNotas()
        Try
            Dim cliente As ClienteDTO = DirectCast(Current.Session("Cliente"), ClienteDTO)
            Dim lsNotas As New List(Of NotaCreditoDTO)
            lsNotas = NotasCreditoBLL.ObtenerInstancia.ListarPorIdCliente(cliente.id)
            gv_Notas.DataSource = lsNotas
            gv_Notas.DataBind()
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error al cargar sus notas de crédito",, True)
        End Try
    End Sub

#Region "modal"
    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Current.Session("Accion") = "CancelarCompra" Then
            Dim cliente As ClienteDTO = DirectCast(Current.Session("Cliente"), ClienteDTO)
            Dim notaCredito As NotaCreditoDTO = DirectCast(Current.Session("NotaCredito"), NotaCreditoDTO)
            Dim compra As CompraDTO = DirectCast(Current.Session("Compra"), CompraDTO)
            compra.estado = New EstadoCompraDTO With {.id = 4}
            CompraBLL.ObtenerInstancia.Modificar(compra)
            NotasCreditoBLL.ObtenerInstancia.Agregar(notaCredito)
            Dim bitacora As New BitacoraDTO With {
                                    .FechaHora = Now(),
                                    .usuario = cliente.usuario,
                                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 28}, 'Suceso: Cancelacion Compra
                                    .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                                    .observaciones = "Se realizo la cancelacion de la compra :" & compra.id & "Se genero la nota de credito : " & notaCredito.id
                           }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        Else
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        End If
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Response.Redirect("/MisMovimientos.aspx")
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

#Region "Modal Encuestas"

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalEncuestaAceptar_click(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim panel As UpdatePanel = Master.FindControl("PanelModal")
        Dim repeater As Repeater = panel.FindControl("repeaterPreguntas")
        For Each pregItem As RepeaterItem In repeater.Items
            'Dim itemIndex As Integer = pregItem.ItemIndex
            Dim lblIdPregunta = DirectCast(pregItem.FindControl("lblIdPregunta"), Label)
            Dim idPregunta As Integer = lblIdPregunta.Text
            Dim oPreg As EncuestaPreguntaDTO = EncuestaPreguntaBLL.ObtenerInstancia.Obtener(idPregunta)
            Dim rdlst = DirectCast(pregItem.FindControl("rdlRespuestas"), System.Web.UI.WebControls.RadioButtonList)

            Dim idEncuesta As Integer = Session("idEncuesta")
            Dim rtaElegida = (From rtas As ListItem In rdlst.Items
                              Where rtas.Selected
                              Select New RespuestaEncuestaDTO With {.id = rtas.Value, .respuesta = rtas.Text}).ToList(0)


            RespuestaEncuestaBLL.ObtenerInstancia.Responder(idEncuesta, oPreg.ID, rtaElegida.id)
            Dim compra As CompraDTO = DirectCast(Current.Session("Compra"), CompraDTO)
            Dim servicio As ServicioDTO = DirectCast(Current.Session("Servicio"), ServicioDTO)

            EncuestaBLL.ObtenerInstancia.ResponderEncuesta(compra.id, servicio.id)

        Next
        Session("idEncuesta") = Nothing
        Current.Session("Servicio") = Nothing
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

#Region "gv_Compras"
    Private Sub gv_Compras_DataBound(sender As Object, e As EventArgs) Handles gv_Compras.DataBound
        Try
            If Not IsNothing(gv_Compras.DataSource) AndAlso gv_Compras.Rows.Count > 0 Then
                Dim ddlCantidadPaginas As DropDownList = CType(gv_Compras.BottomPagerRow.Cells(0).FindControl("ddlComprasCantidadPaginas"), DropDownList)
                Dim ddlTamañoPaginas As DropDownList = CType(gv_Compras.BottomPagerRow.Cells(0).FindControl("ddlComprasTamañoPaginas"), DropDownList)
                    Dim txtTotalPaginas As Label = CType(gv_Compras.BottomPagerRow.Cells(0).FindControl("lblComprasTotalPaginas"), Label)

                    ddlTamañoPaginas.ClearSelection()
                    ddlTamañoPaginas.Items.FindByValue(gv_Compras.PageSize).Selected = True

                    txtTotalPaginas.Text = gv_Compras.PageCount
                    For cnt As Integer = 0 To gv_Compras.PageCount - 1
                        Dim curr As Integer = cnt + 1
                        Dim item As New ListItem(curr.ToString())
                        If cnt = gv_Compras.PageIndex Then
                            item.Selected = True
                        End If
                        ddlCantidadPaginas.Items.Add(item)
                    Next cnt

                    gv_Compras.BottomPagerRow.Visible = True
                    gv_Compras.BottomPagerRow.CssClass = "table-bottom-dark"

            End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub ddlComprasCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Compras.BottomPagerRow.Cells(0).FindControl("ddlComprasCantidadPaginas"), DropDownList)
            gv_Compras.SetPageIndex(ddl.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlComprasTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Compras.BottomPagerRow.Cells(0).FindControl("ddlComprasTamañoPaginas"), DropDownList)
            gv_Compras.PageSize = ddl.SelectedValue
            CargarCompras()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gv_Compras_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            CargarCompras()
            gv_Compras.PageIndex = e.NewPageIndex
            gv_Compras.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv_Compras_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Compras.RowCommand
        Dim compra As New CompraDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        compra = CompraBLL.ObtenerInstancia.Obtener(id)

        If e.CommandName = "Cancelar" Then
            Select Case compra.estado.id
                Case 4
                    MostrarModal("Error", "La compra ya se encuentra cancelada",, True)
                Case 5
                    MostrarModal("Error", "La compra no se puede redimir",, True)
                Case 2
                    'TODO: Implementar cancelacion de compra
                    MostrarModal("Solicitud de cancelacion", "La compra se encuentra en proceso. Se elevo una solicitud para su cancelacion",, True)
                Case Else
                    Dim notaCredito As New NotaCreditoDTO With {
                        .estado = New EstadoNotaDTO With {.id = 2}, 'Pendiente de redimir
                        .id_factura = compra.factura.id,
                        .fecha = Now,
                        .importe = compra.total,
                        .concepto = "Nota de credito por cancelacion de compra : " & compra.id & " por un total de $" & compra.total & " realizada el día " & compra.fecha & ". La factura correspondiente es : " & compra.factura.id,
                        .cliente = compra.cliente
                        }
                    Current.Session("Accion") = "CancelarCompra"
                    Current.Session("NotaCredito") = notaCredito
                    Current.Session("Compra") = compra
                    MostrarModal("Confirmar cancelacion de compra", "¿Desea cancelar la compra? Se generara una nota de credito por $" & compra.total,, True)
            End Select
        ElseIf e.CommandName = "Descargar" Then
            Response.Write("<script>window.open ('/DescargaFactura.aspx?Cr=" & e.CommandArgument & "','_blank');</script>")
        ElseIf e.CommandName = "Valorar" Then
            gvServicios.DataSource = Nothing

            gvServicios.DataSource = compra.carrito
            gvServicios.DataBind()


            'Dim lsServicios As New List(Of ServicioDTO)
            'For Each itemCarrito As ServicioCarritoDTO In compra.carrito
            '    lsServicios.Add(itemCarrito.servicio)
            'Next
            Current.Session("Compra") = compra
            'gvServicios.DataSource = lsServicios
            'gvServicios.DataBind()
            gvServicios.Visible = True
        End If
    End Sub




#End Region


#Region "gv_Notas"
    Private Sub gv_Notas_DataBound(sender As Object, e As EventArgs) Handles gv_Notas.DataBound
        Try
            If Not IsNothing(gv_Notas.DataSource) AndAlso gv_Notas.Rows.Count > 0 Then
                Dim ddlNotasCantidadPaginas As DropDownList = CType(gv_Notas.BottomPagerRow.Cells(0).FindControl("ddlNotasCantidadPaginas"), DropDownList)
                Dim ddlNotasTamañoPaginas As DropDownList = CType(gv_Notas.BottomPagerRow.Cells(0).FindControl("ddlNotasTamañoPaginas"), DropDownList)
                Dim txtNotasTotalPaginas As Label = CType(gv_Notas.BottomPagerRow.Cells(0).FindControl("lblNotasTotalPaginas"), Label)

                ddlNotasTamañoPaginas.ClearSelection()
                ddlNotasTamañoPaginas.Items.FindByValue(gv_Notas.PageSize).Selected = True

                txtNotasTotalPaginas.Text = gv_Notas.PageCount
                For cnt As Integer = 0 To gv_Notas.PageCount - 1
                    Dim curr As Integer = cnt + 1
                    Dim item As New ListItem(curr.ToString())
                    If cnt = gv_Notas.PageIndex Then
                        item.Selected = True
                    End If
                    ddlNotasCantidadPaginas.Items.Add(item)
                Next cnt

                gv_Notas.BottomPagerRow.Visible = True
                gv_Notas.BottomPagerRow.CssClass = "table-bottom-dark"
            End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub ddlNotasCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddlNotas As DropDownList = CType(gv_Notas.BottomPagerRow.Cells(0).FindControl("ddlNotasCantidadPaginas"), DropDownList)
            gv_Notas.SetPageIndex(ddlNotas.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlNotasTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddlNotas As DropDownList = CType(gv_Notas.BottomPagerRow.Cells(0).FindControl("ddlNotasTamañoPaginas"), DropDownList)
            gv_Notas.PageSize = ddlNotas.SelectedValue
            CargarNotas()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gv_Notas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            CargarNotas()
            gv_Notas.PageIndex = e.NewPageIndex
            gv_Notas.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv_Notas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Notas.RowCommand
        Dim nota As New NotaCreditoDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        nota = NotasCreditoBLL.ObtenerInstancia.Obtener(id)

        If e.CommandName = "Descargar" Then
            Response.Write("<script>window.open ('/DescargaFactura.aspx?Nc=" & e.CommandArgument & "','_blank');</script>")
        End If
    End Sub

#End Region

#Region "gvServicios"

    Private Sub gvServicios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvServicios.RowCommand
        Dim servicio As New ServicioDTO

        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        servicio = ServicioBLL.ObtenerInstancia.Obtener(id)

        Dim compra As CompraDTO = DirectCast(Current.Session("Compra"), CompraDTO)

        Dim encuesta As New EncuestaDTO
        If servicio.encuestas.Count > 0 Then
            encuesta = servicio.encuestas.Item(0)
        End If

        If EncuestaBLL.ObtenerInstancia.RespondioEncuesta(compra.id, servicio.id) = False Then
            Current.Session("Servicio") = servicio
            Session("idEncuesta") = encuesta.id
            MostrarEncuesta(encuesta.nombre, encuesta.preguntas)
        Else
            MostrarModal("Ya contestó esta encuesta", "La encuesta ya se encuentra contestada.",,)
        End If

    End Sub

#End Region

End Class