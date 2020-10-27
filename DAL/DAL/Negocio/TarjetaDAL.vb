Imports Entidades
Imports System.Data
Imports System.Data.SqlClient
Public Class TarjetaDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As TarjetaDAL
    Public Shared Function ObtenerInstancia() As TarjetaDAL
        If _instancia Is Nothing Then
            _instancia = New TarjetaDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function Listar() As List(Of TarjetaDTO)
        Dim lsEstado As New List(Of TarjetaDTO)
        For Each Row In AccesoDAL.ObtenerInstancia.LeerBD("Tarjetas_Listar").Rows
            Dim oEstado As New TarjetaDTO With {
                                            .id = Row("id"),
                                            .nro = Row("nro"),
                                            .nombre = Row("nombre"),
                                            .marca = Row("marca"),
                                            .codigo_seguridad = Row("codigo_seguridad"),
                                            .vencimiento = Row("vencimiento"),
                                            .estado = EstadoTarjetaDAL.ObtenerInstancia.Obtener(Row("id_estado_tarjeta"))
            }
            lsEstado.Add(oEstado)
        Next
        Return lsEstado
    End Function

    Public Function Obtener(tarjeta As TarjetaDTO) As TarjetaDTO
        Try
            Dim ls As New List(Of TarjetaDTO)
            ls = Me.Listar()
            Dim oTarjeta As TarjetaDTO = Nothing
            For Each tarj As TarjetaDTO In ls
                If tarj.nro = tarjeta.nro Then
                    oTarjeta = tarj
                End If
            Next
            Return oTarjeta
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorId(id As Integer) As TarjetaDTO
        Try
            Dim ls As New List(Of TarjetaDTO)
            ls = Me.Listar()
            Dim oTarjeta As TarjetaDTO = Nothing
            For Each tarj As TarjetaDTO In ls
                If tarj.id = id Then
                    oTarjeta = tarj
                End If
            Next
            Return oTarjeta
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
