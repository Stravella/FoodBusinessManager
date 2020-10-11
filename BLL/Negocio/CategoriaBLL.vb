Imports Entidades
Imports DAL
Public Class CategoriaBLL

#Region "Singleton"
    Private Shared _instancia As CategoriaBLL
    Public Shared Function ObtenerInstancia() As CategoriaBLL
        If _instancia Is Nothing Then
            _instancia = New CategoriaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Crear(categoria As CategoriaDTO)
        Try
            categoria.id = CategoriaDAL.ObtenerInstancia.GetNextID
            CategoriaDAL.ObtenerInstancia.Agregar(categoria)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(categoria As CategoriaDTO)
        Try
            CategoriaDAL.ObtenerInstancia.Modificar(categoria)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(categoria As CategoriaDTO)
        Try
            CategoriaDAL.ObtenerInstancia.Eliminar(categoria)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function Listar() As List(Of CategoriaDTO)
        Try
            Return CategoriaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As CategoriaDTO
        Try
            Return CategoriaDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
