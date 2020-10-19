Imports Entidades
Imports System.Data.SqlClient
Imports System.Data


Public Class CompraDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As CompraDAL
    Public Shared Function ObtenerInstancia() As CompraDAL
        If _instancia Is Nothing Then
            _instancia = New CompraDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal compra As CompraDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", compra.id))
                params.Add(.CrearParametro("@id_cliente", compra.cliente.id))
                params.Add(.CrearParametro("@fecha", compra.fecha))
                params.Add(.CrearParametro("@total", Convert.ToDecimal(compra.total)))
                params.Add(.CrearParametro("@id_factura", compra.factura.id))
                params.Add(.CrearParametro("@id_estado_compra", compra.estado.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Compra")
    End Function

    Public Sub Agregar(ByVal compra As CompraDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Compra_crear", CrearParametros(compra))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal compra As CompraDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Compra_modificar", CrearParametros(compra))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of CompraDTO)
        Dim lsCompra As New List(Of CompraDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Compra_Listar").Rows
            Dim compra As New CompraDTO With {.id = row("id"),
                                              .cliente = ClienteDAL.ObtenerInstancia.Obtener(New ClienteDTO With {.id = row("id_cliente")}),
                                              .fecha = row("fecha"),
                                              .estado = ObtenerEstadoCompra(row("id_estado_compra")),
                                              .total = row("total"),
                                              .factura = FacturaDAL.ObtenerInstancia.Obtener(row("id_factura")),
                                              .carrito = ListarCarritoPorIdCompra(row("id"))
            }
            lsCompra.Add(compra)
        Next
        Return lsCompra
    End Function

    Public Function ListarPorIdUsuario(id As Integer) As List(Of CompraDTO)
        Try
            Dim lsCompra As New List(Of CompraDTO)
            lsCompra = Listar()
            Dim lsResultado As New List(Of CompraDTO)
            For Each compra In lsCompra
                If compra.cliente.id = id Then
                    lsResultado.Add(compra)
                End If
            Next
            Return lsResultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As CompraDTO
        Try
            Dim resultado As New CompraDTO
            Dim compras As List(Of CompraDTO) = Listar()
            For Each compra In compras
                If compra.id = id Then
                    resultado = compra
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#Region "Estados_compra"
    Public Function ListarEstados() As List(Of EstadoCompraDTO)
        Try
            Dim lsEstados As New List(Of EstadoCompraDTO)
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Estado_compra_listar").Rows
                Dim estado As New EstadoCompraDTO With {.id = row("id"),
                                                  .estado = row("estado")
                }
                lsEstados.Add(estado)
            Next
            Return lsEstados
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerEstadoCompra(id As Integer) As EstadoCompraDTO
        Try
            Dim lsEstados As List(Of EstadoCompraDTO) = ListarEstados()
            Dim resultado As New EstadoCompraDTO
            For Each oEstado As EstadoCompraDTO In lsEstados
                If oEstado.id = id Then
                    resultado = oEstado
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ArmadoCarrito"
    Public Sub AgregarCompraServicio(ByVal compra As CompraDTO, servicio As ServicioDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_compra", compra.id))
                params.Add(.CrearParametro("@id_servicio", servicio.id))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Compra_servicio_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListarCarritoPorIdCompra(id As Integer) As List(Of ServicioCarritoDTO)
        Dim lsCarrito As New List(Of ServicioCarritoDTO)
        Dim params As New List(Of SqlParameter)
        Dim existeCarrito As Boolean = False
        With AccesoDAL.ObtenerInstancia()
            params.Add(.CrearParametro("@id_compra", id))
        End With
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Compra_servicio_listar_por_compra", params).Rows
            existeCarrito = False
            If lsCarrito.Count > 0 Then 'Si el carrito tiene resultados lo itero, y lo agrego a la cantidad si existe, y si no lo creo y lo agrego
                For Each serv As ServicioCarritoDTO In lsCarrito
                    If serv.servicio.id = row("id_servicio") Then
                        serv.cantidad = serv.cantidad + 1
                        serv.importeTotal = serv.cantidad * serv.servicio.precio
                        existeCarrito = True
                    End If
                Next
                If existeCarrito = False Then
                    Dim servCarrito As New ServicioCarritoDTO With {
                        .servicio = ServicioDAL.ObtenerInstancia.Obtener(row("id_servicio")),
                        .cantidad = 1
                        }
                    servCarrito.importeTotal = 1 * servCarrito.servicio.precio
                    lsCarrito.Add(servCarrito)
                End If
            Else 'Si no tiene elementos lo agrego
                Dim servCarrito As New ServicioCarritoDTO With {
                .servicio = ServicioDAL.ObtenerInstancia.Obtener(row("id_servicio")),
                .cantidad = 1
                }
                servCarrito.importeTotal = 1 * servCarrito.servicio.precio
                lsCarrito.Add(servCarrito)
            End If
        Next
        Return lsCarrito
    End Function

#End Region

End Class
