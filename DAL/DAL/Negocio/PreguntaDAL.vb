Imports Entidades
Imports System.Data.SqlClient
Imports System.Data

Public Class PreguntaDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As PreguntaDAL
    Public Shared Function ObtenerInstancia() As PreguntaDAL
        If _instancia Is Nothing Then
            _instancia = New PreguntaDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal pregunta As PreguntaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", pregunta.id))
                params.Add(.CrearParametro("@pregunta", pregunta.pregunta))
                params.Add(.CrearParametro("@fechaVencimiento", pregunta.fechaVencimiento))
                params.Add(.CrearParametro("@id_servicio", pregunta.id_servicio))
                params.Add(.CrearParametro("@id_usuario", pregunta.usuario.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Preguntas")
    End Function

    Public Sub Agregar(pregunta As PreguntaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Preguntas_Crear", CrearParametros(pregunta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(pregunta As PreguntaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Preguntas_Modificar", CrearParametros(pregunta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(pregunta As PreguntaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", pregunta.id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Preguntas_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of PreguntaDTO)
        Dim lsPreguntas As New List(Of PreguntaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Preguntas_Listar").Rows
            Dim pregunta As New PreguntaDTO With {.id = row("id"),
                                              .pregunta = row("pregunta"),
                                              .fechaVencimiento = row("fechaVencimiento"),
                                              .respuestas = RespuestaDAL.ObtenerInstancia.ListarPorIdPregunta(row("id")),
                                              .id_servicio = row("id_servicio"),
                                              .usuario = UsuarioDAL.ObtenerInstancia.ObtenerPorId(row("id_usuario"))
            }
            lsPreguntas.Add(pregunta)
        Next
        Return lsPreguntas
    End Function

    Public Function Obtener(id As Integer) As PreguntaDTO
        Try
            Dim resultado As New PreguntaDTO
            Dim preguntas As List(Of PreguntaDTO) = Listar()
            For Each pregunta In preguntas
                If pregunta.id = id Then
                    resultado = pregunta
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Preguntas_encuesta"

    Public Function ListarPorIdEncuesta(id As Integer) As List(Of PreguntaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia
                params.Add(.CrearParametro("@id_encuesta", id))
            End With
            Dim lsPreguntas As New List(Of PreguntaDTO)
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_pregunta_encuesta_listarPorIdEncuesta", params).Rows
                Dim pregunta As New PreguntaDTO With {.id = row("id"),
                                                  .pregunta = row("pregunta"),
                                                  .fechaVencimiento = row("fechaVencimiento"),
                                                  .respuestas = RespuestaDAL.ObtenerInstancia.ListarPorIdPregunta(row("id")),
                                                  .id_servicio = row("id_servicio"),
                                                  .usuario = UsuarioDAL.ObtenerInstancia.ObtenerPorId(row("id_usuario"))
                }
                lsPreguntas.Add(pregunta)
            Next
            Return lsPreguntas
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

End Class
