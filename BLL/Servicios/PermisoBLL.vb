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
            Throw ex
        End Try
    End Function


    Public Function Obtener(permiso As PermisoComponente) As PermisoComponente
        Try
            Return PermisoDAL.ObtenerInstancia.Obtener(permiso.id_permiso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorNombre(permiso As PermisoComponente) As PermisoComponente
        Try
            Return PermisoDAL.ObtenerInstancia.ObtenerPorNombre(permiso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Crear(permiso As PermisoComponente)
        Try
            PermisoDAL.ObtenerInstancia.Agregar(permiso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ModificarPerfil(perfilViejo As PerfilCompuesto, perfilNuevo As PerfilCompuesto)
        Try
            'Borro los viejos
            PermisoDAL.ObtenerInstancia.EliminarPermisos(perfilViejo.id_permiso)
            'Asocio los nuevos
            PermisoDAL.ObtenerInstancia.ModificarPerfil(perfilNuevo)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function BorrarPerfil(Perfil As PerfilCompuesto)
        Try
            PermisoDAL.ObtenerInstancia.Eliminar(Perfil)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPerfiles() As List(Of PermisoComponente)
        Try
            Return PermisoDAL.ObtenerInstancia.ListarPerfiles
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPerfilesEditables() As List(Of PermisoComponente)
        Try
            Dim ls As New List(Of PermisoComponente)
            For Each perfil As PermisoComponente In Listar()
                If perfil.se_puede_borrar = True Then
                    ls.Add(perfil)
                End If
            Next
            Return ls
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
