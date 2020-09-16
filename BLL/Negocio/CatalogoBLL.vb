
Imports DAL
Imports Entidades
Public Class CatalogoBLL
#Region "Singleton"
    Private Shared _instancia As CatalogoBLL
    Public Shared Function ObtenerInstancia() As CatalogoBLL
        If _instancia Is Nothing Then
            _instancia = New CatalogoBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(catalogo As CatalogoDTO)
        Try
            CatalogoDAL.ObtenerInstancia.Agregar(catalogo)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Eliminar(id As Integer)
        Try
            CatalogoDAL.ObtenerInstancia.Eliminar(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(catalogo As CatalogoDTO)
        Try
            CatalogoDAL.ObtenerInstancia.Modificar(catalogo)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of CatalogoDTO)
        Try
            Return CatalogoDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
