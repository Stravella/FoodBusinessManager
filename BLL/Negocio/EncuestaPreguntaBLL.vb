Imports DAL
Imports Entidades

Public Class EncuestaPreguntaBLL
#Region "Singleton"
    Private Shared _instancia As EncuestaPreguntaBLL
    Public Shared Function ObtenerInstancia() As EncuestaPreguntaBLL
        If _instancia Is Nothing Then
            _instancia = New EncuestaPreguntaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of EncuestaPreguntaDTO)
        Try
            Return EncuestaPreguntaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarSinUso() As List(Of EncuestaPreguntaDTO)
        Try
            Return EncuestaPreguntaDAL.ObtenerInstancia.ListarSinUso
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Agregar(pregunta As EncuestaPreguntaDTO)
        Try
            pregunta.ID = EncuestaPreguntaDAL.ObtenerInstancia.GetNextID
            EncuestaPreguntaDAL.ObtenerInstancia.Agregar(pregunta)
            For Each respuesta In pregunta.Respuestas
                RespuestaEncuestaDAL.ObtenerInstancia.AsociarAPregunta(respuesta.id, pregunta.ID)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Obtener(id As Integer) As EncuestaPreguntaDTO
        Try
            Return EncuestaPreguntaDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Eliminar(Pregunta As EncuestaPreguntaDTO)
        Try
            EncuestaPreguntaDAL.ObtenerInstancia.EliminarRespuesta(Pregunta.ID)
            For Each respuesta In Pregunta.Respuestas
                RespuestaEncuestaDAL.ObtenerInstancia.Eliminar(respuesta.id)
            Next
            EncuestaPreguntaDAL.ObtenerInstancia.Eliminar(Pregunta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(Pregunta As EncuestaPreguntaDTO)
        Try
            EncuestaPreguntaDAL.ObtenerInstancia.EliminarRespuesta(Pregunta.ID)
            EncuestaPreguntaDAL.ObtenerInstancia.Modificar(Pregunta)
            For Each respuesta In Pregunta.Respuestas
                'EncuestaPreguntaDAL.ObtenerInstancia.AgregarRespuesta(Pregunta.ID, respuesta.id)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
