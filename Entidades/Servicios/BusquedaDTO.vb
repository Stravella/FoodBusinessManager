Public Class BusquedaDTO
    Private _id As Integer
    Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _menu As String
    Public Property Menu() As String
        Get
            Return _menu
        End Get
        Set(ByVal value As String)
            _menu = value
        End Set
    End Property

    Private _url As String
    Public Property URL() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Private _esPublico As Boolean
    Public Property esPublico() As Boolean
        Get
            Return _esPublico
        End Get
        Set(ByVal value As Boolean)
            _esPublico = value
        End Set
    End Property


End Class
