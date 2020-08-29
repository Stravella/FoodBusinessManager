Public Class ProvinciaDTO
    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
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

End Class
