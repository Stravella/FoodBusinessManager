Imports DAL
Imports Entidades

Public Class BusquedaBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As BusquedaBLL
    Public Shared Function ObtenerInstancia() As BusquedaBLL
        If _instancia Is Nothing Then
            _instancia = New BusquedaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Buscar(palabra As String, esPublico As Integer) As List(Of BusquedaDTO)
        Try
            Return BusquedaDAL.ObtenerInstancia.Listar(palabra, esPublico)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Buscar(palabra As String) As List(Of BusquedaDTO)
        Try
            Return BusquedaDAL.ObtenerInstancia.Listar(palabra)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
