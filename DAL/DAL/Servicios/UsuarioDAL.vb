Imports Entidades
Imports System.Data.SqlClient
Imports System.Data

Public Class UsuarioDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As UsuarioDAL
    Public Shared Function ObtenerInstancia() As UsuarioDAL
        If _instancia Is Nothing Then
            _instancia = New UsuarioDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal usuario As UsuarioDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@Nombre", usuario.nombre))
                params.Add(.CrearParametro("@Apellido", usuario.apellido))
                params.Add(.CrearParametro("@username", usuario.username))
                params.Add(.CrearParametro("@password", usuario.password))
                params.Add(.CrearParametro("@fecha_Creacion", usuario.fechaCreacion))
                params.Add(.CrearParametro("@intentos", usuario.intentos))
                params.Add(.CrearParametro("@bloqueado", usuario.bloqueado))
                params.Add(.CrearParametro("@id_idioma", usuario.idioma.id_idioma))
                params.Add(.CrearParametro("@id_perfil", usuario.perfil.id_permiso))
                params.Add(.CrearParametro("@mail", usuario.mail))
                params.Add(.CrearParametro("@id_usuario", usuario.id))
                params.Add(.CrearParametro("SALT", usuario.SALT))
            End With
        Catch ex As Exception

        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_usuario", "usuarios")
    End Function


    Public Function ObtenerUsuario(ByVal usuario As UsuarioDTO) As UsuarioDTO
        Try
            Dim oUsuario As UsuarioDTO
            'listo todos los usuarios y selecciono por username.
            Dim ls As List(Of UsuarioDTO) = Me.ListarUsuarios()
            'Dim oUsuario As UsuarioDTO = ls.Find(Function(x) x.username = usuario.username)
            For Each iUsuario As UsuarioDTO In ls
                If iUsuario.username = usuario.username Then
                    oUsuario = iUsuario
                End If
            Next
            'Encontrado el usuario, comparo password
            Return oUsuario
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorId(ByVal id As Integer) As UsuarioDTO
        Try
            Dim oUsuario As UsuarioDTO
            'listo todos los usuarios y selecciono por id.
            Dim ls As List(Of UsuarioDTO) = Me.ListarUsuarios()
            For Each iUsuario As UsuarioDTO In ls
                If iUsuario.id = id Then
                    oUsuario = iUsuario
                End If
            Next
            Return oUsuario
        Catch ex As Exception

        End Try
    End Function


    Public Function ListarUsuarios() As List(Of UsuarioDTO)
        Dim lsUsuarios As New List(Of UsuarioDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("ListarUsuarios").Rows
            Dim oUsuario As New UsuarioDTO With {.nombre = row("nombre"),
                                              .apellido = row("apellido"),
                                              .username = row("username"),
                                              .password = row("password"),
                                              .fechaCreacion = row("fecha_Creacion"),
                                              .bloqueado = row("bloqueado"),
                                              .intentos = row("intentos"),
                                              .mail = row("mail"),
                                              .id = row("id_usuario"),
                                              .SALT = row("SALT"),
                                              .idioma = IdiomaDAL.ObtenerInstancia.ObtenerIdioma(New IdiomaDTO With {.id_idioma = row("id_idioma")}),
                                              .perfil = PermisoDAL.ObtenerInstancia.Obtener(row("id_perfil"))
            }
            lsUsuarios.Add(oUsuario)
        Next
        Return lsUsuarios
    End Function

    Public Function ListarPorPefil(ByVal unPerfil As PermisoComponente) As List(Of UsuarioDTO)
        Try
            Dim lsUsuarios As New List(Of UsuarioDTO)
            Dim lsUsuariosPerfil As New List(Of UsuarioDTO)
            lsUsuarios = ListarUsuarios()
            For Each Usuario As UsuarioDTO In lsUsuarios
                If Usuario.perfil.id_permiso = unPerfil.id_permiso Then
                    lsUsuariosPerfil.Add(Usuario)
                End If
            Next
            Return lsUsuariosPerfil
        Catch ex As Exception

        End Try
    End Function

    'Agregar Usuario
    Public Sub AgregarUsuario(ByVal usuario As UsuarioDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Usuarios_Crear", CrearParametros(usuario))
        Catch ex As Exception

        End Try
    End Sub

    'Modificar Usuario
    Public Sub ModificarUsuario(ByVal usuario As UsuarioDTO)
        AccesoDAL.ObtenerInstancia.EjecutarSP("Usuarios_Modificar", CrearParametros(usuario))
    End Sub

End Class
