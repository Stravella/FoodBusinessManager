Imports Entidades
Imports System.Data
Imports System.Data.SqlClient

Public Class EstadoTarjetaDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As EstadoTarjetaDAL
    Public Shared Function ObtenerInstancia() As EstadoTarjetaDAL
        If _instancia Is Nothing Then
            _instancia = New EstadoTarjetaDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of EstadoTarjetaDTO)
        Dim lsEstado As New List(Of EstadoTarjetaDTO)
        For Each Row In AccesoDAL.ObtenerInstancia.LeerBD("Estados_tarjetas_listar").Rows
            Dim oEstado As New EstadoTarjetaDTO With {
                                            .id = Row("id"),
                                            .estado = Row("estado")
            }
            lsEstado.Add(oEstado)
        Next
        Return lsEstado
    End Function

    Public Function Obtener(id As Integer) As EstadoTarjetaDTO
        Try
            Dim ls As New List(Of EstadoTarjetaDTO)
            ls = Me.Listar()
            Dim oEstado As EstadoTarjetaDTO = Nothing
            For Each estado As EstadoTarjetaDTO In ls
                If estado.id = id Then
                    oEstado = estado
                End If
            Next
            Return oEstado
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
