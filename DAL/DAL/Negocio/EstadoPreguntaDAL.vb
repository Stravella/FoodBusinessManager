Imports Entidades
Imports System.Data.SqlClient
Public Class EstadoPreguntaDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As EstadoPreguntaDAL
    Public Shared Function ObtenerInstancia() As EstadoPreguntaDAL
        If _instancia Is Nothing Then
            _instancia = New EstadoPreguntaDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of EstadoPreguntaEncuestaDTO)
        Dim lsEstados As New List(Of EstadoPreguntaEncuestaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_preguntas_estado_listar").Rows
            Dim estado As New EstadoPreguntaEncuestaDTO With {.id = row("id"),
                                              .estado = row("estado")
            }
            lsEstados.Add(estado)
        Next
        Return lsEstados
    End Function

    Public Function Obtener(id As Integer) As EstadoPreguntaEncuestaDTO
        Try
            Dim resultado As New EstadoPreguntaEncuestaDTO
            Dim lsEstados As List(Of EstadoPreguntaEncuestaDTO) = Listar()
            For Each estado In lsEstados
                If estado.id = id Then
                    resultado = estado
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
