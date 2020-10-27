Imports Entidades
Imports System.Data.SqlClient

Public Class EncuestaPreguntaDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As EncuestaPreguntaDAL
    Public Shared Function ObtenerInstancia() As EncuestaPreguntaDAL
        If _instancia Is Nothing Then
            _instancia = New EncuestaPreguntaDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal pregunta As EncuestaPreguntaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", pregunta.ID))
                params.Add(.CrearParametro("@pregunta", pregunta.pregunta))
                params.Add(.CrearParametro("@fecha_vencimiento", pregunta.FechaVenc))
                params.Add(.CrearParametro("@id_estado", pregunta.Estado.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function


    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Encuesta_preguntas")
    End Function

    Public Sub Agregar(ByVal pregunta As EncuestaPreguntaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_preguntas_crear", CrearParametros(pregunta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal pregunta As EncuestaPreguntaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_preguntas_modificar", CrearParametros(pregunta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub Eliminar(ByVal pregunta As EncuestaPreguntaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", pregunta.ID)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_preguntas_eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of EncuestaPreguntaDTO)
        Dim lsEncuestaPregunta As New List(Of EncuestaPreguntaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_preguntas_listar").Rows
            Dim encuestaPregunta As New EncuestaPreguntaDTO With {.ID = row("id"),
                                              .pregunta = row("pregunta"),
                                              .FechaVenc = row("fecha_vencimiento"),
                                              .Estado = EstadoPreguntaDAL.ObtenerInstancia.Obtener(row("id_estado")),
                                              .Respuestas = RespuestaEncuestaDAL.ObtenerInstancia.ListarPorIDPregunta(row("id"))
            }
            lsEncuestaPregunta.Add(encuestaPregunta)
        Next
        Return lsEncuestaPregunta
    End Function

    Public Function Obtener(id As Integer) As EncuestaPreguntaDTO
        Try
            Dim resultado As New EncuestaPreguntaDTO
            Dim lsEncuestaPregunta As List(Of EncuestaPreguntaDTO) = Listar()
            For Each encuestaPregunta In lsEncuestaPregunta
                If encuestaPregunta.ID = id Then
                    resultado = encuestaPregunta
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#Region "Encuesta_Preguntas"

    Public Function ListarPorIdEncuesta(id As Integer) As List(Of EncuestaPreguntaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia
                params.Add(.CrearParametro("id_encuesta", id))
            End With
            Dim lsEncuestaPregunta As New List(Of EncuestaPreguntaDTO)
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_pregunta_encuesta_listarPorIdEncuesta", params).Rows
                Dim encuestaPregunta As New EncuestaPreguntaDTO With {.ID = row("id"),
                                                  .pregunta = row("pregunta"),
                                                  .FechaVenc = row("fecha_vencimiento"),
                                                  .Estado = EstadoPreguntaDAL.ObtenerInstancia.Obtener(row("id_estado"))
                }
                lsEncuestaPregunta.Add(encuestaPregunta)
            Next
            Return lsEncuestaPregunta
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Sub EliminarPorIdEncuesta(ByVal id As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_encuesta", id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_encuesta_preguntas_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


#Region "Pregunta_respuestas"

    Public Sub AgregarRespuesta(id_pregunta As Integer, id_respuesta As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_pregunta", id_pregunta)))
                params.Add((.CrearParametro("@id_respuesta", id_respuesta)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_pregunta_respuestas_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EliminarRespuesta(id_pregunta As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_pregunta", id_pregunta)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_pregunta_respuestas_eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


End Class
