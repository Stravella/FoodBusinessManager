Public MustInherit Class PermisoComponente

    Private _id_permiso As Integer
    Private _nombre As String
    Private _url_acceso As String
    Private _substraccion As Boolean

    Public Property url_acceso() As String
        Get
            Return _url_acceso
        End Get
        Set(ByVal Value As String)
            _url_acceso = Value
        End Set
    End Property

    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal Value As String)
            _nombre = Value
        End Set
    End Property

    Public Property id_permiso() As Integer
        Get
            Return _id_permiso
        End Get
        Set(ByVal Value As Integer)
            _id_permiso = Value
        End Set
    End Property

    Public Property substraccion() As Boolean
        Get
            Return _substraccion
        End Get
        Set(ByVal value As Boolean)
            _substraccion = value
        End Set
    End Property

    Public MustOverride Function PuedeUsar(ByVal unaUrl As String) As Boolean
    Public MustOverride Function agregarHijo(ByVal hijo As PermisoComponente) As Boolean
    Public MustOverride Function tieneHijos() As Boolean
    Public MustOverride Function esValido(nombrePermiso As String) As Boolean

    Public Overrides Function ToString() As String
        Return Me.nombre
    End Function

End Class
