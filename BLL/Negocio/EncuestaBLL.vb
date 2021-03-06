﻿
Imports DAL
Imports Entidades

Public Class EncuestaBLL
#Region "Singleton"
    Private Shared _instancia As EncuestaBLL
    Public Shared Function ObtenerInstancia() As EncuestaBLL
        If _instancia Is Nothing Then
            _instancia = New EncuestaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(encuesta As EncuestaDTO)
        Try
            encuesta.id = EncuestaDAL.ObtenerInstancia.GetNextID
            EncuestaDAL.ObtenerInstancia.Agregar(encuesta)
            For Each pregunta In encuesta.preguntas
                EncuestaDAL.ObtenerInstancia.AgregarPregunta(encuesta.id, pregunta.ID)
                For Each respuesta As RespuestaEncuestaDTO In pregunta.Respuestas
                    EncuestaPreguntaDAL.ObtenerInstancia.AgregarEncuestaPreguntaRespuesta(encuesta.id, pregunta.ID, respuesta.id)
                Next
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Eliminar(id As Integer)
        Try
            RespuestaEncuestaDAL.ObtenerInstancia.EliminarRespuestasUsuarios(id)
            EncuestaDAL.ObtenerInstancia.EliminarEncuestaPreguntas(id)
            Dim encuesta As EncuestaDTO = Obtener(id)
            For Each pregunta In encuesta.preguntas
                EncuestaPreguntaBLL.ObtenerInstancia.Eliminar(pregunta)
            Next
            EncuestaDAL.ObtenerInstancia.Eliminar(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(encuesta As EncuestaDTO)
        Try
            EncuestaDAL.ObtenerInstancia.Modificar(encuesta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of EncuestaDTO)
        Try
            Return EncuestaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarEncuestas() As List(Of EncuestaDTO)
        Try
            Return EncuestaDAL.ObtenerInstancia.ListarEncuestas
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListarOpiniones() As List(Of EncuestaDTO)
        Try
            Return EncuestaDAL.ObtenerInstancia.ListarOpiniones
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As EncuestaDTO
        Try
            Dim encuesta As EncuestaDTO = EncuestaDAL.ObtenerInstancia.Obtener(id)
            For Each pregunta In encuesta.preguntas
                pregunta.Respuestas = RespuestaEncuestaDAL.ObtenerInstancia.ListarPorEncuestaPregunta(encuesta.id, pregunta.ID)
            Next
            Return encuesta
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub EliminarRelacionServicios(id As Integer)
        Try
            EncuestaDAL.ObtenerInstancia.EliminarEncuestaServicio(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Encuesta-valoracion-servicio"

    Public Sub ResponderEncuesta(id_compra As Integer, id_servicio As Integer)
        Try
            EncuestaDAL.ObtenerInstancia.ResponderEncuesta(id_compra, id_servicio)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function RespondioEncuesta(id_compra As Integer, id_servicio As Integer) As Boolean
        Try
            Return EncuestaDAL.ObtenerInstancia.RespondioEncuesta(id_compra, id_servicio)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region




End Class
