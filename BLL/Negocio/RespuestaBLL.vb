
Imports DAL
Imports Entidades

Public Class RespuestaBLL
#Region "Singleton"
    Private Shared _instancia As RespuestaBLL
    Public Shared Function ObtenerInstancia() As RespuestaBLL
        If _instancia Is Nothing Then
            _instancia = New RespuestaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(respuesta As RespuestaDTO)
        Try
            respuesta.id = RespuestaDAL.ObtenerInstancia.GetNextID
            RespuestaDAL.ObtenerInstancia.Agregar(respuesta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of RespuestaDTO)
        Try
            Return RespuestaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPorIdPregunta(id As Integer) As List(Of RespuestaDTO)
        Try
            Dim respuestas As List(Of RespuestaDTO) = Listar()
            Dim resultado As New List(Of RespuestaDTO)
            For Each respuesta In respuestas
                If respuesta.id_pregunta = id Then
                    resultado.Add(respuesta)
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
