Public Class ChatSesionDTO

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _fechaInicio As Date
    Public Property fechaInicio() As Date
        Get
            Return _fechaInicio
        End Get
        Set(ByVal value As Date)
            _fechaInicio = value
        End Set
    End Property

    Private _fechaFin As Date
    Public Property fechaFin() As Date
        Get
            Return _fechaFin
        End Get
        Set(ByVal value As Date)
            _fechaFin = value
        End Set
    End Property

    Private _cliente As ClienteDTO
    Public Property cliente() As ClienteDTO
        Get
            Return _cliente
        End Get
        Set(ByVal value As ClienteDTO)
            _cliente = value
        End Set
    End Property

    Private _usuarioAtendio As UsuarioDTO
    Public Property usuarioAtendio() As UsuarioDTO
        Get
            Return _usuarioAtendio
        End Get
        Set(ByVal value As UsuarioDTO)
            _usuarioAtendio = value
        End Set
    End Property

    Private _mensajes As List(Of ChatMensajeDTO)
    Public Property mensajes() As List(Of ChatMensajeDTO)
        Get
            Return _mensajes
        End Get
        Set(ByVal value As List(Of ChatMensajeDTO))
            _mensajes = value
        End Set
    End Property

End Class
