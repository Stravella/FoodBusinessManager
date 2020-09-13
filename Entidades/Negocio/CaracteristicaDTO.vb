Public Class CaracteristicaDTO
    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _caracteristica As String
    Public Property caracteristica() As String
        Get
            Return _caracteristica
        End Get
        Set(ByVal value As String)
            _caracteristica = value
        End Set
    End Property
End Class
