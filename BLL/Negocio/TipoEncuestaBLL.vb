
Imports DAL
Imports Entidades

Public Class TipoEncuestaBLL
#Region "Singleton"
    Private Shared _instancia As TipoEncuestaBLL
    Public Shared Function ObtenerInstancia() As TipoEncuestaBLL
        If _instancia Is Nothing Then
            _instancia = New TipoEncuestaBLL
        End If
        Return _instancia
    End Function
#End Region



    Public Function Listar() As List(Of TipoEncuestaDTO)
        Try
            Return TipoEncuestaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As TipoEncuestaDTO
        Try
            Return TipoEncuestaDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
