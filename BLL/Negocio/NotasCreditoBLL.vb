Imports Entidades
Imports DAL
Imports System.IO

Public Class NotasCreditoBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As NotasCreditoBLL
    Public Shared Function ObtenerInstancia() As NotasCreditoBLL
        If _instancia Is Nothing Then
            _instancia = New NotasCreditoBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(nota As NotaCreditoDTO)
        Try
            nota.id = NotaCreditoDAL.ObtenerInstancia.GetNextID
            NotaCreditoDAL.ObtenerInstancia.Agregar(nota)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function ListarRedimiblesPorCliente(id_cliente As Integer) As List(Of NotaCreditoDTO)
        Try
            Return NotaCreditoDAL.ObtenerInstancia.ListarRedimiblesPorCliente(id_cliente)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPorIdCliente(id_cliente As Integer) As List(Of NotaCreditoDTO)
        Try
            Return NotaCreditoDAL.ObtenerInstancia.ListarPorCliente(id_cliente)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As NotaCreditoDTO
        Try
            Return NotaCreditoDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ArmarFacturaMail(ByVal oNC As NotaCreditoDTO, urlPlantilla As String) As String
        Dim content As String = String.Empty
        Dim reader As StreamReader = New StreamReader(urlPlantilla)
        content = reader.ReadToEnd

        '  Dim lineaItemProd As String = "<tr class=""itemP""><td>#_ItemProducto    x  #_Q</td><td>#_Importe</td></tr>"
        'Dim factura As FacturaDTO = FacturaBLL.ObtenerInstancia.Obtener(oNC.id_factura)

        content = content.Replace("_TFactura", "Nota de  Crédito").Replace("_NroFact", oNC.id).Replace("_FechaGeneracion", oNC.fecha).Replace("_RazonSocial1", oNC.cliente.RazonSocial)
        content = content.Replace("_CUIT", oNC.cliente.CUIT).Replace("_Domicilio", oNC.cliente.domicilio)
        content = content.Replace("_Ciudad", oNC.cliente.provincia).Replace("_CP", oNC.cliente.CP)
        content = content.Replace("_Mail", oNC.cliente.usuario.mail).Replace("_Telefono", oNC.cliente.telefono).Replace("_Total", oNC.importe).Replace("Producto", "Concepto")
        'remueve el bloque medios de pago
        Dim larg As Integer = content.IndexOf("<!--BorrarMdp-->") - content.IndexOf("<!--BloqueMdP-->")
        content = content.Remove(content.IndexOf("<!--BloqueMdP-->"), larg)

        'ingreso el concepto como una única línea de items.
        Dim lineaItemProd As String = "<tr class=""itemP""><td>_ItemProducto    x  _Q</td><td>$ _Importe</td></tr>"

        lineaItemProd = lineaItemProd.Replace("_ItemProducto", oNC.concepto).Replace("_Q", "1").Replace("_Importe", oNC.importe.ToString)
        content = content.Insert(content.IndexOf("<!--_lineasTroducto-->") + 23, lineaItemProd)
        content = content.Replace("itemP", "itemP last")

        Return content

    End Function


End Class
