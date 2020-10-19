Imports Entidades
Imports DAL

Public Class TarjetaBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As TarjetaBLL
    Public Shared Function ObtenerInstancia() As TarjetaBLL
        If _instancia Is Nothing Then
            _instancia = New TarjetaBLL
        End If
        Return _instancia
    End Function
#End Region

    Function Obtener(tarjeta As TarjetaDTO) As TarjetaDTO
        Try
            Return TarjetaDAL.ObtenerInstancia.Obtener(tarjeta)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
