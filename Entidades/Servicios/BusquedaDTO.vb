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

    Private _icono As String
    Public Property Icono() As String
        Get
            Return _icono
        End Get
        Set(ByVal value As String)
            _icono = value
        End Set
    End Property
End Class
