﻿Imports Entidades
Imports System.Data.SqlClient
Imports System.Data

Public Class RespuestaEncuestaDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As RespuestaEncuestaDAL
    Public Shared Function ObtenerInstancia() As RespuestaEncuestaDAL
        If _instancia Is Nothing Then
            _instancia = New RespuestaEncuestaDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal respuesta As RespuestaEncuestaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", respuesta.id))
                params.Add(.CrearParametro("@respuesta", respuesta.respuesta))
                params.Add(.CrearParametro("@cantidad", respuesta.cantidad))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function


    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Encuesta_respuestas")
    End Function

    Public Sub Agregar(ByVal respuesta As RespuestaEncuestaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_respuestas_crear", CrearParametros(respuesta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal respuesta As RespuestaEncuestaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_respuestas_modificar", CrearParametros(respuesta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal id As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", id))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_respuestas_eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of RespuestaEncuestaDTO)
        Dim lsRespuestas As New List(Of RespuestaEncuestaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_respuestas_listar").Rows
            Dim respuesta As New RespuestaEncuestaDTO With {.id = row("id"),
                                              .respuesta = row("respuesta"),
                                              .cantidad = row("cantidad")
            }
            lsRespuestas.Add(respuesta)
        Next
        Return lsRespuestas
    End Function

    Public Function Obtener(id As Integer) As RespuestaEncuestaDTO
        Try
            Dim resultado As New RespuestaEncuestaDTO
            Dim lsEncuestaRespuestas As List(Of RespuestaEncuestaDTO) = Listar()
            For Each respuestasEncuesta In lsEncuestaRespuestas
                If respuestasEncuesta.id = id Then
                    resultado = respuestasEncuesta
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Pregunta_respuestas"
    Public Function ListarPorIDPregunta(id As Integer) As List(Of RespuestaEncuestaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia
                params.Add(.CrearParametro("@id_pregunta", id))
            End With
            Dim lsRespuestas As New List(Of RespuestaEncuestaDTO)
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_preguntas_respuestas_listarPorPregunta", params).Rows
                Dim respuesta As New RespuestaEncuestaDTO With {.id = row("id"),
                                                  .respuesta = row("respuesta"),
                                                  .cantidad = row("cantidad")
                }
                lsRespuestas.Add(respuesta)
            Next
            Return lsRespuestas
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region



End Class
