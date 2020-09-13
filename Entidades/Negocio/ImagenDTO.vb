Public Class ImagenDTO
    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _img64 As String
    Public Property Img64() As String
        Get
            Return _img64
        End Get
        Set(ByVal value As String)
            _img64 = value
        End Set
    End Property

End Class
