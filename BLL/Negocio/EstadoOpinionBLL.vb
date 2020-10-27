Imports DAL
Imports Entidades
Public Class EstadoOpinionBLL

#Region "Singleton"
    Private Shared _instancia As EstadoOpinionBLL
    Public Shared Function ObtenerInstancia() As EstadoOpinionBLL
        If _instancia Is Nothing Then
            _instancia = New EstadoOpinionBLL
        End If
        Return _instancia
    End Function
#End Region
    Public Function Listar() As List(Of EstadoPreguntaEncuestaDTO)
        Try
            Return EstadoPreguntaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Obtener(id As Integer) As EstadoPreguntaEncuestaDTO
        Try
            Return EstadoPreguntaDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
