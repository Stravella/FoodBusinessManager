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
            usuario.intentos = 0
            'Se crea en bloqueado para que genere su nueva contraseña
            usuario.bloqueado = 1
            usuario.fechaCreacion = DateTime.Now
            'Guardo la password sin encriptar para enviarla por mail
            usuario.password = Me.GenerarToken
            Dim passwordDesencriptada As String = usuario.password
            'Genero el SALT
            usuario.SALT = DigitoVerificadorBLL.ObtenerInstancia.ObtenerSALT
            usuario.password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(usuario.password & usuario.SALT)
            usuario.DVH = DigitoVerificadorBLL.ObtenerInstancia.CalcularDVH(usuario)
            UsuarioDAL.ObtenerInstancia.AgregarUsuario(usuario)
            'seteo la vieja password al usuario para enviarla por mail
            usuario.password = passwordDesencriptada
            'TODO: chequear que esto funcione
            DigitoVerificadorBLL.ObtenerInstancia.ActualizarDVV("usuarios")
            Return usuario
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Sub ModificarUsuario(usuario As UsuarioDTO)
        UsuarioDAL.ObtenerInstancia.ModificarUsuario(usuario)
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
            'Password se mantiene encriptada
            If oUsuario.password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(usuario.password & oUsuario.SALT) Then
                Return oUsuario
            Else
                If oUsuario.bloqueado = False Then
                    If oUsuario.intentos = 3 Then
                        oUsuario.bloqueado = True
                    Else
                        oUsuario.intentos = +1
                    End If
                    ModificarUsuario(oUsuario)
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception

        End Try
    End Function

    'Bloqueo el usuario y le guardo la contraseña
    Public Function RecuperarUsuario(usuario As UsuarioDTO) As UsuarioDTO
        Try
            Dim usuarioObtenido = ObtenerUsuario(usuario)
            usuarioObtenido.bloqueado = 1
            usuarioObtenido.password = GenerarToken()
            GestorMailBLL.ObtenerInstancia.EnviarMail(usuarioObtenido, True)
            usuarioObtenido.password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(usuarioObtenido.password + usuarioObtenido.SALT)
            'Encriptar la nueva contraseña (si la encripto el token tiene que devolverse antes)
            usuarioObtenido.DVH = DigitoVerificadorBLL.ObtenerInstancia.CalcularDVH(usuario)
            ModificarUsuario(usuarioObtenido)
            Return usuarioObtenido
        Catch ex As Exception

        End Try
    End Function

    'Desbloqueo el usuario y guardo la nueva contraseña
    Public Function CambiarContraseña(usuario As UsuarioDTO) As UsuarioDTO
        Try
            Dim usuarioObtenido = ObtenerUsuario(usuario)
            usuarioObtenido.password = DigitoVerificadorBLL.ObtenerInstancia.Encriptar(usuario.password & usuarioObtenido.SALT)
            usuarioObtenido.bloqueado = 0
            usuarioObtenido.DVH = DigitoVerificadorBLL.ObtenerInstancia.CalcularDVH(usuarioObtenido)
            ModificarUsuario(usuarioObtenido)
            DigitoVerificadorBLL.ObtenerInstancia.ActualizarDVV("usuarios")
            Return usuarioObtenido
        Catch ex As Exception

        End Try
    End Function

    '¿Valido si expira en 24hs?
    Public Function GenerarToken() As String
        Try
            Dim time() As Byte = BitConverter.GetBytes(DateTime.UtcNow.ToBinary())
            Dim key() As Byte = Guid.NewGuid().ToByteArray()
            Dim token As String = Convert.ToBase64String(time.Concat(key).ToArray())
            Return token
        Catch ex As Exception

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

    Public Function ValidarUserMail(usuario As UsuarioDTO) As Boolean
        Try
            Dim lsUsuarios As List(Of UsuarioDTO) = UsuarioDAL.ObtenerInstancia.ListarUsuarios
            For Each oUsuario As UsuarioDTO In lsUsuarios
                If oUsuario.username = usuario.username AndAlso oUsuario.mail = usuario.mail Then
                    Return True
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Function

End Class ' UsuarioBLL


