Imports Entidades
Imports DAL

Public Class UsuarioBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As UsuarioBLL
    Public Shared Function ObtenerInstancia() As UsuarioBLL
        If _instancia Is Nothing Then
            _instancia = New UsuarioBLL
        End If
        Return _instancia
    End Function
#End Region

#Region "CRUD"
    Public Function ObtenerUsuario(usuario As UsuarioDTO) As UsuarioDTO
        Return UsuarioDAL.ObtenerInstancia.ObtenerUsuario(usuario)
    End Function

    Public Function ListarUsuarios() As List(Of UsuarioDTO)
        Return UsuarioDAL.ObtenerInstancia.ListarUsuarios
    End Function

    Public Function AgregarUsuario(usuario As UsuarioDTO) As UsuarioDTO

        Try
            usuario.id = UsuarioDAL.ObtenerInstancia.GetNextID
            UsuarioDAL.ObtenerInstancia.AgregarUsuario(usuario)
            Return usuario
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub ModificarUsuario(usuario As UsuarioDTO)
        Try
            UsuarioDAL.ObtenerInstancia.ModificarUsuario(usuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EliminarUsuario(usuario As UsuarioDTO)
        Try
            UsuarioDAL.ObtenerInstancia.EliminarUsuario(usuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

    Public Function ListarPorPerfil(unPerfil As PerfilCompuesto) As List(Of UsuarioDTO)
        Try
            Return UsuarioDAL.ObtenerInstancia.ListarPorPefil(unPerfil)
        Catch ex As Exception

        End Try
    End Function

    Public Function LogIn(usuario As UsuarioDTO) As UsuarioDTO
        Try
            Dim oUsuario As UsuarioDTO = UsuarioDAL.ObtenerInstancia.ObtenerUsuario(usuario)
            If oUsuario.password = CriptografiaBLL.ObtenerInstancia.Cifrar(usuario.password) Then
                Return oUsuario
            Else
                If oUsuario.intentos = 3 Then
                    oUsuario.bloqueado = True
                Else
                    oUsuario.bloqueado = +1
                End If
                ModificarUsuario(oUsuario)
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ChequearExistenciaUsuario(usuario As UsuarioDTO) As Boolean
        Try
            Dim lsUsuarios As List(Of UsuarioDTO) = UsuarioDAL.ObtenerInstancia.ListarUsuarios
            For Each oUsuario As UsuarioDTO In lsUsuarios
                If oUsuario.username = usuario.username Then
                    Return True
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ChequearExistenciaMail(usuario As UsuarioDTO) As Boolean
        Try
            Dim lsUsuarios As List(Of UsuarioDTO) = UsuarioDAL.ObtenerInstancia.ListarUsuarios
            For Each oUsuario As UsuarioDTO In lsUsuarios
                If oUsuario.mail = usuario.mail Then
                    Return True
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Function

    Public Function ObtenerPorMail(mail As String) As UsuarioDTO
        Try
            Dim usuarioRetorno As New UsuarioDTO
            Dim lsUsuarios As List(Of UsuarioDTO) = UsuarioDAL.ObtenerInstancia.ListarUsuarios
            For Each oUsuario As UsuarioDTO In lsUsuarios
                If oUsuario.mail = mail Then
                    usuarioRetorno = oUsuario
                    Exit For
                End If
            Next
            Return usuarioRetorno
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class ' UsuarioBLL


