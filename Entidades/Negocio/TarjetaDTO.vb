Public Class TarjetaDTO
    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _nro As String
    Public Property nro() As String
        Get
            Return _nro
        End Get
        Set(ByVal value As String)
            _nro = value
        End Set
    End Property

    Private _nombre As String
    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _marca As String
    Public Property marca() As String
        Get
            Return _marca
        End Get
        Set(ByVal value As String)
            _marca = value
        End Set
    End Property

    Private _vencimiento As String
    Public Property vencimiento() As String
        Get
            Return _vencimiento
        End Get
        Set(ByVal value As String)
            _vencimiento = value
        End Set
    End Property

    Private _codigo_seguridad As String
    Public Property codigo_seguridad() As String
        Get
            Return _codigo_seguridad
        End Get
        Set(ByVal value As String)
            _codigo_seguridad = value
        End Set
    End Property

    Private _estado As EstadoTarjetaDTO
    Public Property estado() As EstadoTarjetaDTO
        Get
            Return _estado
        End Get
        Set(ByVal value As EstadoTarjetaDTO)
            _estado = value
        End Set
    End Property

End Class
