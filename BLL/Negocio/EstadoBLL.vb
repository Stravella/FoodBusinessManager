Imports Entidades
Imports DAL

Public Class EstadoBLL

#Region "Singleton"
    Private Shared _instancia As EstadoBLL
    Public Shared Function ObtenerInstancia() As EstadoBLL
        If _instancia Is Nothing Then
            _instancia = New EstadoBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of EstadoDTO)
        Try
            Return EstadoDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
