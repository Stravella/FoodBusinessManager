Imports Entidades
Imports System.Data.SqlClient
Imports System.Data

Public Class CaracteristicasDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As CaracteristicasDAL
    Public Shared Function ObtenerInstancia() As CaracteristicasDAL
        If _instancia Is Nothing Then
            _instancia = New CaracteristicasDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal caracteristica As CaracteristicaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", caracteristica.id))
                params.Add(.CrearParametro("@caracteristica", caracteristica.caracteristica))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Caracteristicas")
    End Function

    Public Sub Agregar(ByVal caracteristica As CaracteristicaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Caracteristicas_Crear", CrearParametros(caracteristica))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal caracteristica As CaracteristicaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Caracteristicas_Modificar", CrearParametros(caracteristica))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal caracteristica As CaracteristicaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", caracteristica.id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Caracteristicas_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of CaracteristicaDTO)
        Dim caracteristicas As New List(Of CaracteristicaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Caracteristicas_Listar").Rows
            Dim caracteristica As New CaracteristicaDTO With {.id = row("id"),
                                              .caracteristica = row("caracteristica")
            }
            caracteristicas.Add(caracteristica)
        Next
        Return caracteristicas
    End Function

    Public Function Obtener(id As Integer) As CaracteristicaDTO
        Dim resultado As New CaracteristicaDTO
        Dim caracteristicas As List(Of CaracteristicaDTO) = Listar()
        For Each caracteristica In caracteristicas
            If caracteristica.id = id Then
                resultado = caracteristica
                Exit For
            End If
        Next
        Return resultado
    End Function


End Class
