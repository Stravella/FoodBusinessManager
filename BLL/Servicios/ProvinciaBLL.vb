Imports Entidades
Imports DAL
Public Class ProvinciaBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ProvinciaBLL
    Public Shared Function ObtenerInstancia() As ProvinciaBLL
        If _instancia Is Nothing Then
            _instancia = New ProvinciaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of ProvinciaDTO)
        Try
            Return ProvinciasDAL.ObtenerInstancia.ListarProvincias
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
