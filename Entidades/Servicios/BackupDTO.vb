Public Class BackupDTO

    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _fecha As Date
    Public Property Fecha() As Date
        Get
            Return _fecha
        End Get
        Set(ByVal value As Date)
            _fecha = value
        End Set
    End Property

    Private _tamano As String
    Public Property Tamano() As String
        Get
            Return _tamano
        End Get
        Set(ByVal value As String)
            _tamano = value
        End Set
    End Property

    Private _usuario As UsuarioDTO
    Public Property Usuario() As UsuarioDTO
        Get
            Return _usuario
        End Get
        Set(ByVal value As UsuarioDTO)
            _usuario = value
        End Set
    End Property

    Private _path As String
    Public Property Path() As String
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property

End Class
