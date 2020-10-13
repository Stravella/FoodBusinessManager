Imports DAL
Imports Entidades
Public Class NewsletterBLL


#Region "Singleton"
    Private Shared _instancia As NewsletterBLL
    Public Shared Function ObtenerInstancia() As NewsletterBLL
        If _instancia Is Nothing Then
            _instancia = New NewsletterBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(newsletter As NewsletterDTO)
        Try
            newsletter.Imagen = ImagenBLL.ObtenerInstancia.Agregar(newsletter.Imagen)
            newsletter.ID = NewsletterDAL.ObtenerInstancia.GetNextID
            NewsletterDAL.ObtenerInstancia.Agregar(newsletter)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(newsletter As NewsletterDTO)
        Try
            ImagenDAL.ObtenerInstancia.Modificar(newsletter.Imagen)
            NewsletterDAL.ObtenerInstancia.Modificar(newsletter)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(newsletter As NewsletterDTO)
        Try
            NewsletterDAL.ObtenerInstancia.Eliminar(newsletter.ID)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of NewsletterDTO)
        Try
            Return NewsletterDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As NewsletterDTO
        Try
            Return NewsletterDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPorCategoria(id_categoria) As List(Of NewsletterDTO)
        Try
            Return NewsletterDAL.ObtenerInstancia.ListarPorCategoria(id_categoria)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
