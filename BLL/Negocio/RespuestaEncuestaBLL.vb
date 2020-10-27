Imports Entidades
Imports DAL
Public Class RespuestaEncuestaBLL


#Region "Singleton"
    Private Shared _instancia As RespuestaEncuestaBLL
    Public Shared Function ObtenerInstancia() As RespuestaEncuestaBLL
        If _instancia Is Nothing Then
            _instancia = New RespuestaEncuestaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(respuesta As RespuestaEncuestaDTO)
        Try
            respuesta.id = RespuestaEncuestaDAL.ObtenerInstancia.GetNextID
            RespuestaEncuestaDAL.ObtenerInstancia.Agregar(respuesta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(respuesta As RespuestaEncuestaDTO)
        Try
            RespuestaEncuestaDAL.ObtenerInstancia.Modificar(respuesta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(id As Integer)
        Try
            RespuestaEncuestaDAL.ObtenerInstancia.Eliminar(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of RespuestaEncuestaDTO)
        Try
            Return RespuestaEncuestaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As RespuestaEncuestaDTO
        Try
            Return RespuestaEncuestaDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function




End Class
