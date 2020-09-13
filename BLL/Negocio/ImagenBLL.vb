Imports DAL
Imports Entidades
Public Class ImagenBLL

#Region "Singleton"
    Private Shared _instancia As ImagenBLL
    Public Shared Function ObtenerInstancia() As ImagenBLL
        If _instancia Is Nothing Then
            _instancia = New ImagenBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Agregar(imagen As ImagenDTO) As ImagenDTO
        Try
            imagen.ID = ImagenDAL.ObtenerInstancia.GetNextID
            ImagenDAL.ObtenerInstancia.Agregar(imagen)
            Return imagen
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
