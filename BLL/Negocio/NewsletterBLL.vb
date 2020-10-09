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

    Public Function Listar() As List(Of NewsletterDTO)
        Try
            Return NewsletterDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
