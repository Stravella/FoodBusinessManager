Public Class ClienteDTO

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _usuario As UsuarioDTO
    Public Property usuario() As UsuarioDTO
        Get
            Return _usuario
        End Get
        Set(ByVal value As UsuarioDTO)
            _usuario = value
        End Set
    End Property

    Private _CUIT As Integer
    Public Property CUIT() As Integer
        Get
            Return _CUIT
        End Get
        Set(ByVal value As Integer)
            _CUIT = value
        End Set
    End Property

    Private _domicilio As String
    Public Property domicilio() As String
        Get
            Return _domicilio
        End Get
        Set(ByVal value As String)
            _domicilio = value
        End Set
    End Property

    Private _CP As String
    Public Property CP() As String
        Get
            Return _CP
        End Get
        Set(ByVal value As String)
            _CP = value
        End Set
    End Property

    Private _localidad As String
    Public Property localidad() As String
        Get
            Return _localidad
        End Get
        Set(ByVal value As String)
            _localidad = value
        End Set
    End Property

    Private _provincia As String
    Public Property provincia() As String
        Get
            Return _provincia
        End Get
        Set(ByVal value As String)
            _provincia = value
        End Set
    End Property

    Private _telefono As Integer
    Public Property telefono() As Integer
        Get
            Return _telefono
        End Get
        Set(ByVal value As Integer)
            _telefono = value
        End Set
    End Property

    Private _aceptaNewsletter As Boolean
    Public Property aceptaNewsletter() As Boolean
        Get
            Return _aceptaNewsletter
        End Get
        Set(ByVal value As Boolean)
            _aceptaNewsletter = value
        End Set
    End Property

    Private _estado As EstadoClienteDTO
    Public Property estado() As EstadoClienteDTO
        Get
            Return _estado
        End Get
        Set(ByVal value As EstadoClienteDTO)
            _estado = value
        End Set
    End Property

End Class
