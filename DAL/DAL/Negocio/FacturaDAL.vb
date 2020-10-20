Imports Entidades
Imports System.Data.SqlClient
Imports System.Data

Public Class FacturaDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As FacturaDAL
    Public Shared Function ObtenerInstancia() As FacturaDAL
        If _instancia Is Nothing Then
            _instancia = New FacturaDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal factura As FacturaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", factura.id))
                params.Add(.CrearParametro("@id_cliente", factura.cliente.id))
                params.Add(.CrearParametro("@total", Convert.ToDecimal(factura.total)))
                If factura.tarjeta Is Nothing Then

                Else
                    params.Add(.CrearParametro("@id_tarjeta", factura.tarjeta.id))
                End If
                params.Add(.CrearParametro("@importe_tarjeta", Convert.ToDecimal(factura.importeTarjeta)))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Factura")
    End Function

    Public Sub Agregar(ByVal factura As FacturaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Factura_crear", CrearParametros(factura))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function Listar() As List(Of FacturaDTO)
        Dim lsFactura As New List(Of FacturaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Factura_Listar").Rows
            Dim factura As New FacturaDTO With {.id = row("id"),
                                              .cliente = ClienteDAL.ObtenerInstancia.Obtener(New ClienteDTO With {.id = row("id_cliente")}),
                                              .total = row("total"),
                                              .importeTarjeta = row("importe_tarjeta"),
                                              .notasCredito = NotaCreditoDAL.ObtenerInstancia.ListarRedimidasPorIdFactura(row("id"))
            }
            If row("id_tarjeta") Is DBNull.Value Then
                factura.tarjeta = Nothing
            Else
                factura.tarjeta = TarjetaDAL.ObtenerInstancia.ObtenerPorId(row("id_tarjeta"))
            End If
            lsFactura.Add(factura)
        Next
        Return lsFactura
    End Function

    Public Function Obtener(id As Integer) As FacturaDTO
        Try
            Dim resultado As New FacturaDTO
            Dim facturas As List(Of FacturaDTO) = Listar()
            For Each fact In facturas
                If fact.id = id Then
                    resultado = fact
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Factura Nota Credito"

    Public Sub CrearFacturaNota(id_factura As Integer, id_nota As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_factura", id_factura))
                params.Add(.CrearParametro("@id_nota_credito", id_nota))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Factura_notas_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


End Class
