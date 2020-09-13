Imports DAL
Imports Entidades
Public Class CaracteristicaBLL
#Region "Singleton"
    Private Shared _instancia As CaracteristicaBLL
    Public Shared Function ObtenerInstancia() As CaracteristicaBLL
        If _instancia Is Nothing Then
            _instancia = New CaracteristicaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of CaracteristicaDTO)
        Try
            Return CaracteristicasDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Agregar(caracteristica As CaracteristicaDTO)
        Try
            caracteristica.id = CaracteristicasDAL.ObtenerInstancia.GetNextID
            CaracteristicasDAL.ObtenerInstancia.Agregar(caracteristica)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(caracteristica As CaracteristicaDTO)
        Try
            CaracteristicasDAL.ObtenerInstancia.Modificar(caracteristica)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(caracteristica As CaracteristicaDTO)
        Try
            CaracteristicasDAL.ObtenerInstancia.Eliminar(caracteristica)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
