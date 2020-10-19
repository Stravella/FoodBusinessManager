Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class NotaCreditoDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As NotaCreditoDAL
    Public Shared Function ObtenerInstancia() As NotaCreditoDAL
        If _instancia Is Nothing Then
            _instancia = New NotaCreditoDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal notaCredito As NotaCreditoDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", notaCredito.id))
                params.Add(.CrearParametro("@fecha", notaCredito.fecha))
                params.Add(.CrearParametro("@concepto", notaCredito.concepto))
                params.Add(.CrearParametro("@importe", Convert.ToDecimal(notaCredito.importe)))
                params.Add(.CrearParametro("@id_estado_nota", notaCredito.estado.id))
                params.Add(.CrearParametro("@id_factura_originante", notaCredito.id_factura))
                params.Add(.CrearParametro("@id_cliente", notaCredito.cliente.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Nota_credito")
    End Function

    Public Sub Agregar(ByVal notaCredito As NotaCreditoDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Notas_credito_crear", CrearParametros(notaCredito))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal notaCredito As NotaCreditoDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Notas_credito_modificar", CrearParametros(notaCredito))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of NotaCreditoDTO)
        Dim lsNotas As New List(Of NotaCreditoDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Notas_credito_listar").Rows
            Dim notaCredito As New NotaCreditoDTO With {.id = row("id"),
                                              .id_factura = row("id_factura_originante"),
                                              .concepto = row("concepto"),
                                              .fecha = row("fecha"),
                                              .importe = row("importe"),
                                              .estado = ObtenerEstadoNota(row("id_estado_nota")),
                                              .cliente = ClienteDAL.ObtenerInstancia.Obtener(New ClienteDTO With {.id = row("id_cliente")})
            }
            lsNotas.Add(notaCredito)
        Next
        Return lsNotas
    End Function

    Public Function ListarRedimidasPorIdFactura(id As Integer) As List(Of NotaCreditoDTO)
        Try
            Dim lsResultado As New List(Of NotaCreditoDTO)
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_factura", id))
            End With
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Factura_notas_listar_por_factura", params).Rows
                Dim notaCredito As New NotaCreditoDTO With {.id = row("id"),
                                              .id_factura = row("id_factura_originante"),
                                              .concepto = row("concepto"),
                                              .fecha = row("fecha"),
                                              .importe = row("importe"),
                                              .estado = ObtenerEstadoNota(row("id_estado_nota")),
                                              .cliente = ClienteDAL.ObtenerInstancia.Obtener(New ClienteDTO With {.id = row("id_cliente")})
            }
                lsResultado.Add(notaCredito)
            Next
            Return lsResultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPorCliente(id_cliente As Integer) As List(Of NotaCreditoDTO)
        Try
            Dim lsNotas As New List(Of NotaCreditoDTO)
            lsNotas = Listar()
            Dim lsResultado As New List(Of NotaCreditoDTO)
            For Each nota In lsNotas
                If nota.cliente.id = id_cliente Then
                    lsResultado.Add(nota)
                End If
            Next
            Return lsResultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarRedimiblesPorCliente(id_cliente As Integer) As List(Of NotaCreditoDTO)
        Try
            Dim lsNotas As New List(Of NotaCreditoDTO)
            lsNotas = Listar()
            Dim lsResultado As New List(Of NotaCreditoDTO)
            For Each nota In lsNotas
                If nota.cliente.id = id_cliente AndAlso nota.estado.id = 2 Then
                    lsResultado.Add(nota)
                End If
            Next
            Return lsResultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As NotaCreditoDTO
        Try
            Dim resultado As New NotaCreditoDTO
            Dim lsNotas As List(Of NotaCreditoDTO) = Listar()
            For Each nota In lsNotas
                If nota.id = id Then
                    resultado = nota
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Estado_nota"

    Public Function ListarEstadoNota() As List(Of EstadoNotaDTO)
        Dim lsEstadoNota As New List(Of EstadoNotaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Estado_nota_listar").Rows
            Dim estadoNota As New EstadoNotaDTO With {
                    .id = row("id"),
                    .estado = row("estado_nota")}
            lsEstadoNota.Add(estadoNota)
        Next
        Return lsEstadoNota
    End Function

    Public Function ObtenerEstadoNota(id As Integer) As EstadoNotaDTO
        Try
            Dim lsEstado As List(Of EstadoNotaDTO) = ListarEstadoNota()
            Dim resultado As New EstadoNotaDTO
            For Each oEstado In lsEstado
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


End Class
