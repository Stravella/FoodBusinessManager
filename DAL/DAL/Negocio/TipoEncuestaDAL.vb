Imports Entidades
Imports System.Data
Imports System.Data.SqlClient

Public Class TipoEncuestaDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As TipoEncuestaDAL
    Public Shared Function ObtenerInstancia() As TipoEncuestaDAL
        If _instancia Is Nothing Then
            _instancia = New TipoEncuestaDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal tipoEncuesta As TipoEncuestaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", tipoEncuesta.id))
                params.Add(.CrearParametro("@tipo", tipoEncuesta.tipo))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function Listar() As List(Of TipoEncuestaDTO)
        Dim lsTipos As New List(Of TipoEncuestaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("tipo_encuesta_Listar").Rows
            Dim tipoEncuesta As New TipoEncuestaDTO With {
                                              .id = row("id"),
                                              .tipo = row("tipo")
            }
            lsTipos.Add(tipoEncuesta)
        Next
        Return lsTipos
    End Function

    Public Function Obtener(id As Integer) As TipoEncuestaDTO
        Try
            Dim resultado As New TipoEncuestaDTO
            Dim lsTipos As List(Of TipoEncuestaDTO) = Listar()
            For Each tipoEncuesta In lsTipos
                If tipoEncuesta.id = id Then
                    resultado = tipoEncuesta
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
