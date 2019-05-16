Imports Entidades
Imports DAL

Public Class PermisoBLL


#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As PermisoBLL
    Public Shared Function ObtenerInstancia() As PermisoBLL
        If _instancia Is Nothing Then
            _instancia = New PermisoBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of PermisoComponente)
        Try
            Return PermisoDAL.ObtenerInstancia.Listar
        Catch ex As Exception

        End Try
    End Function


    Public Function Obtener(permiso As PermisoComponente) As PermisoComponente
        Try
            Return PermisoDAL.ObtenerInstancia.Obtener(permiso.id_permiso)
        Catch ex As Exception

        End Try
    End Function

    Public Function PuedeUsar() As Boolean

    End Function

End Class
