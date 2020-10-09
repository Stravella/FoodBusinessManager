Imports Entidades
Imports System.Data
Imports System.Data.SqlClient
Public Class EstadoDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As EstadoDAL
    Public Shared Function ObtenerInstancia() As EstadoDAL
        If _instancia Is Nothing Then
            _instancia = New EstadoDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function Listar() As List(Of EstadoDTO)
        Dim lsEstado As New List(Of EstadoDTO)
        For Each Row In AccesoDAL.ObtenerInstancia.LeerBD("Estados_listar").Rows
            Dim oEstado As New EstadoDTO With {
                                            .ID = Row("id"),
                                            .Nombre = Row("nombre")
            }
            lsEstado.Add(oEstado)
        Next
        Return lsEstado
    End Function

    Public Function Obtener(id As Integer) As EstadoDTO
        Try
            Dim ls As New List(Of EstadoDTO)
            ls = Me.Listar()
            Dim oEstado As EstadoDTO = Nothing
            For Each estado As EstadoDTO In ls
                If estado.ID = id Then
                    oEstado = estado
                End If
            Next
            Return oEstado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
