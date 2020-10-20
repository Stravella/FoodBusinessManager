Imports System.IO
Imports DAL
Imports Entidades

Public Class FacturaBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As FacturaBLL
    Public Shared Function ObtenerInstancia() As FacturaBLL
        If _instancia Is Nothing Then
            _instancia = New FacturaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(factura As FacturaDTO)
        Try
            factura.id = FacturaDAL.ObtenerInstancia.GetNextID()
            FacturaDAL.ObtenerInstancia.Agregar(factura)
            If factura.notasCredito IsNot Nothing Then
                For Each notaCredito In factura.notasCredito
                    FacturaDAL.ObtenerInstancia.CrearFacturaNota(factura.id, notaCredito.id)
                    notaCredito.estado.id = 1
                    NotaCreditoDAL.ObtenerInstancia.Modificar(notaCredito)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Obtener(id As Integer) As FacturaDTO
        Try
            Return FacturaDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ArmarFacturaMail(urlPlantilla As String, compra As CompraDTO) As String
        Dim content As String = String.Empty
        Dim reader As StreamReader = New StreamReader(urlPlantilla)
        content = reader.ReadToEnd

        content = content.Replace("_TFactura", "Factura").Replace("_NroFact", compra.factura.id).Replace("_FechaGeneracion", compra.fecha).Replace("_RazonSocial1", compra.cliente.RazonSocial)
        content = content.Replace("_CUIT", compra.cliente.CUIT).Replace("_Domicilio", compra.cliente.domicilio).Replace("_Ciudad", compra.cliente.provincia).Replace("_CP", compra.cliente.CP)
        content = content.Replace("_Mail", compra.cliente.usuario.mail).Replace("_Telefono", compra.cliente.telefono).Replace("_Total", compra.factura.total)

        If compra.factura.importeTarjeta > 0 Then
            content = content.Replace("_FormaDePago", "Tarjeta de credito")
            Dim lineaMDP As String = "<tr class=""details""><td>_MedioDePago</td><td>$ _ImporteMDP</td></tr>"
            lineaMDP = lineaMDP.Replace("_MedioDePago", "Nro: " & compra.factura.tarjeta.nro).Replace("_ImporteMDP", compra.factura.importeTarjeta)
            content = content.Insert(content.IndexOf("<!--_lineasMDP-->") + 18, lineaMDP)
        End If
        If compra.factura.notasCredito.Count > 0 Then
            content = content.Replace("_FormaDePago", "Nota de crédito")
            For Each nota In compra.factura.notasCredito
                Dim lineaMDP As String = "<tr class=""details""><td>_MedioDePago</td><td>$ _ImporteMDP</td></tr>"
                lineaMDP = lineaMDP.Replace("_MedioDePago", nota.concepto).Replace("_ImporteMDP", nota.importe)
                content = content.Insert(content.IndexOf("<!--_lineasMDP-->") + 18, lineaMDP)
            Next
        End If

        For Each servicioCarrito In compra.carrito
            Dim lineaItemProd As String = "<tr class=""itemP""><td>_ItemProducto    x  _Q</td><td>$ _Importe</td></tr>"

            lineaItemProd = lineaItemProd.Replace("_ItemProducto", servicioCarrito.servicio.nombre).Replace("_Q", servicioCarrito.cantidad).Replace("_Importe", servicioCarrito.importeTotal)
            content = content.Insert(content.IndexOf("<!--_lineasTroducto-->") + 23, lineaItemProd)

            If compra.carrito.IndexOf(servicioCarrito) = compra.carrito.Count - 1 Then
                content = content.Replace("itemP", "itemP last")
            End If
        Next
        Return content

    End Function

    'Public Sub EnviarFCporMail(oFactu As FacturaBE, url As String, link As String, urlPlantilla As String, ByVal adjunto As Byte())
    '    If Not IsNothing(oFactu) Then
    '        Dim cuerpoMail As String = Me.ArmarFacturaMail(oFactu, urlPlantilla)
    '        Dim asunto As String = String.Format("Te enviamos tu factura número {0} creada el {1}", oFactu.ID, oFactu.FechaGeneracion.ToShortDateString)
    '        Try

    '            BLLMail.ObtenerInstancia.EnviarCorreo(oFactu.Mail, asunto, cuerpoMail, link, True, adjunto)

    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub


End Class
