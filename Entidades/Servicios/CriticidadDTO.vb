Public Class CriticidadDTO
    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _criticidad As String
    Public Property criticidad() As String
        Get
            Return _criticidad
        End Get
        Set(ByVal value As String)
            _criticidad = value
        End Set
    End Property
End Class
