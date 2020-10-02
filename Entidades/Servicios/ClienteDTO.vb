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

    Private _CUIT As String
    Public Property CUIT() As String
        Get
            Return _CUIT
        End Get
        Set(ByVal value As String)
            _CUIT = value
        End Set
    End Property

    Private _razonSocial As String
    Public Property RazonSocial() As String
        Get
            Return _razonSocial
        End Get
        Set(ByVal value As String)
            _razonSocial = value
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

    Private _telefono As String
    Public Property telefono() As String
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
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
