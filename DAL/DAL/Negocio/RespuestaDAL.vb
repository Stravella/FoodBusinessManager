Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class RespuestaDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As RespuestaDAL
    Public Shared Function ObtenerInstancia() As RespuestaDAL
        If _instancia Is Nothing Then
            _instancia = New RespuestaDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal respuesta As RespuestaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", respuesta.id))
                params.Add(.CrearParametro("@respuesta", respuesta.respuesta))
                params.Add(.CrearParametro("@id_pregunta", respuesta.id_pregunta))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Respuestas")
    End Function

    Public Sub Agregar(ByVal respuesta As RespuestaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Respuestas_Crear", CrearParametros(respuesta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal respuesta As RespuestaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Respuestas_Modificar", CrearParametros(respuesta))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal respuesta As RespuestaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", respuesta.id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Respuestas_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of RespuestaDTO)
        Dim lsRespuesta As New List(Of RespuestaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Respuestas_Listar").Rows
            Dim respuesta As New RespuestaDTO With {.id = row("id"),
                                              .respuesta = row("respuesta"),
                                              .id_pregunta = row("id_pregunta")
            }
            lsRespuesta.Add(respuesta)
        Next
        Return lsRespuesta
    End Function

    Public Function ListarPorIdPregunta(id As Integer) As List(Of RespuestaDTO)
        Dim ls As List(Of RespuestaDTO) = Listar()
        Dim lsResultado As New List(Of RespuestaDTO)
        For Each respuesta In ls
            If respuesta.id_pregunta = id Then
                lsResultado.Add(respuesta)
            End If
        Next
        Return lsResultado
    End Function

    Public Function Obtener(id As Integer) As RespuestaDTO
        Try
            Dim resultado As New RespuestaDTO
            Dim respuestas As List(Of RespuestaDTO) = Listar()
            For Each respuesta In respuestas
                If respuesta.id = id Then
                    resultado = respuesta
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
