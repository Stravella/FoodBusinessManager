Imports Entidades
Imports System.Data.SqlClient
Imports System.Data


Public Class ServicioCaracteristicasDAL

#Region "Singleton"
    Public Sub New()

        End Sub
    Private Shared _instancia As ServicioCaracteristicasDAL
    Public Shared Function ObtenerInstancia() As ServicioCaracteristicasDAL
        If _instancia Is Nothing Then
            _instancia = New ServicioCaracteristicasDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(servicio As ServicioDTO, caracteristica As CaracteristicaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_servicio", servicio.id))
                params.Add(.CrearParametro("@id_caracteristica", caracteristica.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Sub Agregar(servicio As ServicioDTO, caracteristica As CaracteristicaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Servicio_caracteristica_crear", CrearParametros(servicio, caracteristica))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(servicio As ServicioDTO, caracteristica As CaracteristicaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Servicio_caracteristica_eliminar", CrearParametros(servicio, caracteristica))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EliminarPorServicio(servicio As ServicioDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_servicio", servicio.id))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Servicio_caracteristica_eliminar_por_servicio", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListarPorServicio(servicio As ServicioDTO) As List(Of CaracteristicaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_servicio", servicio.id))
            End With
            Dim caracteristicas As New List(Of CaracteristicaDTO)
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Servicio_caracteristica_Listar", params).Rows
                Dim caracteristica As CaracteristicaDTO = CaracteristicasDAL.ObtenerInstancia.Obtener(row("id_caracteristica"))
                caracteristicas.Add(caracteristica)
            Next
            Return caracteristicas
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
