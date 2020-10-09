Public Class NewsletterDTO
    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _titulo As String
    Public Property Titulo() As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Private _cuerpo As String
    Public Property Cuerpo() As String
        Get
            Return _cuerpo
        End Get
        Set(ByVal value As String)
            _cuerpo = value
        End Set
    End Property


    Private _imagen As ImagenDTO
    Public Property Imagen() As ImagenDTO
        Get
            Return _imagen
        End Get
        Set(ByVal value As ImagenDTO)
            _imagen = value
        End Set
    End Property

    Private _estado As EstadoDTO
    Public Property Estado() As EstadoDTO
        Get
            Return _estado
        End Get
        Set(ByVal value As EstadoDTO)
            _estado = value
        End Set
    End Property

End Class
