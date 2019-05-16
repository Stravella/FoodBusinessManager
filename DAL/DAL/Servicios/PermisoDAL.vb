Imports Entidades
Imports System.Data.SqlClient

Public Class PermisoDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As PermisoDAL
    Public Shared Function ObtenerInstancia() As PermisoDAL
        If _instancia Is Nothing Then
            _instancia = New PermisoDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_perfil", "perfil")
    End Function

    Public Function CrearParametros(ByVal permiso As PermisoComponente) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_perfil", permiso.id_permiso))
                params.Add(.CrearParametro("@nombre", permiso.nombre))
                params.Add(.CrearParametro("@es_substractiva", permiso.substraccion))
                params.Add(.CrearParametro("@es_permiso", CInt(0)))
                params.Add(.CrearParametro("@url", permiso.url_acceso))
            End With
        Catch ex As Exception

        End Try
        Return params
    End Function

    Public Function CrearParametros(ByVal perfil As PermisoComponente, permiso As PermisoComponente) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_perfil", perfil.id_permiso))
                params.Add(.CrearParametro("@id_permiso", permiso.id_permiso))
            End With
        Catch ex As Exception

        End Try
        Return params
    End Function

    Public Sub Agregar(permiso As PerfilCompuesto)
        Try
            'Creo el perfil y las relaciones
            If permiso.tieneHijos = True Then
                AccesoDAL.ObtenerInstancia.EjecutarSP("Perfil_Crear", CrearParametros(permiso))
                For Each hijo As PermisoComponente In permiso.Hijos
                    AccesoDAL.ObtenerInstancia.EjecutarSP("PerfilPermiso_Crear", CrearParametros(permiso, hijo))
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Modificar(permiso As PermisoComponente)
        Try
            If permiso.tieneHijos = True Then
                AccesoDAL.ObtenerInstancia.EjecutarSP("Perfil_Eliminar", CrearParametros(permiso))
                Agregar(permiso)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Eliminar(permiso As PerfilCompuesto)
        Try
            'Elimina el perfil y todas las relaciones
            If permiso.tieneHijos = True Then
                AccesoDAL.ObtenerInstancia.EjecutarSP("Perfil_Eliminar", CrearParametros(permiso))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function Obtener(id As Integer) As PermisoComponente
        Try
            Dim oPermiso As PermisoComponente = ConvertirPermiso(AccesoDAL.ObtenerInstancia.LeerBD("Perfil_Obtener").Rows(0))
            Return oPermiso
        Catch

        End Try
    End Function


    Public Function Listar() As List(Of PermisoComponente)
        Dim ls As New List(Of PermisoComponente)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Perfil_Listar").Rows
            'Esto es recursivo, deberia chequear de no agregar a los que ya estén?
            ls.Add(ConvertirPermiso(row))
        Next
        Return ls
    End Function

    Public Function ConvertirPermiso(row As DataRow) As PermisoComponente
        Dim oPermiso As PermisoComponente
        If row("es_permiso") = True Then
            oPermiso = New PermisoHoja
        Else
            oPermiso = New PerfilCompuesto
        End If
        oPermiso.id_permiso = row("id_perfil")
        oPermiso.nombre = row("nombre")
        oPermiso.url_acceso = row("url")
        oPermiso.substraccion = row("es_substractiva")
        If oPermiso.tieneHijos = True Then
            Dim lsHijos As List(Of PermisoComponente)
            Dim Params As New List(Of SqlParameter)
            Dim Param As New SqlParameter("@id_perfil", oPermiso.id_permiso)
            Params.Add(Param)
            For Each RowHijo As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Perfil_ListarHijos", Params).Rows
                oPermiso.agregarHijo(ConvertirPermiso(RowHijo))
            Next
        End If
        Return oPermiso
    End Function


End Class
