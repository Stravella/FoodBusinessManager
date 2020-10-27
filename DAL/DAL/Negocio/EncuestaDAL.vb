Imports Entidades
Imports System.Data
Imports System.Data.SqlClient

Public Class EncuestaDAL
#Region "Singleton"
    Private Shared _instancia As EncuestaDAL
    Public Shared Function ObtenerInstancia() As EncuestaDAL
        If _instancia Is Nothing Then
            _instancia = New EncuestaDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal encuesta As EncuestaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", encuesta.id))
                params.Add(.CrearParametro("@nombre", encuesta.nombre))
                params.Add(.CrearParametro("@id_tipo", encuesta.tipo.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Encuesta")
    End Function


    Public Sub Agregar(ByVal encuesta As EncuestaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_Crear", CrearParametros(encuesta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal encuesta As EncuestaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_Modificar", CrearParametros(encuesta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal id As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", id)))
            End With
            EliminarPreguntasPorEncuesta(id)
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of EncuestaDTO)
        Dim lsEncuestas As New List(Of EncuestaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_listar").Rows
            Dim encuesta As New EncuestaDTO With {.id = row("id"),
                                              .nombre = row("nombre"),
                                              .tipo = TipoEncuestaDAL.ObtenerInstancia.Obtener(row("id_tipo")),
                                              .preguntas = EncuestaPreguntaDAL.ObtenerInstancia.ListarPorIdEncuesta(row("id"))
            }
            lsEncuestas.Add(encuesta)
        Next
        Return lsEncuestas
    End Function

    Public Function Obtener(id As Integer) As EncuestaDTO
        Try
            Dim resultado As New EncuestaDTO
            Dim lsEncuestas As List(Of EncuestaDTO) = Listar()
            For Each encuesta In lsEncuestas
                If encuesta.id = id Then
                    resultado = encuesta
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#Region "Encuesta_preguntas"

    Public Sub EliminarPreguntasPorEncuesta(ByVal id_encuesta As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_encuesta", id_encuesta)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_encuesta_preguntas_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub AgregarPregunta(ByVal id_encuesta As Integer, id_pregunta As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_encuesta", id_encuesta)))
                params.Add((.CrearParametro("@id_pregunta", id_pregunta)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_encuesta_preguntas_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



#End Region


End Class
