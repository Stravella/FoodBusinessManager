Public Class RespuestaDTO
    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _respuesta As String
    Public Property respuesta() As String
        Get
            Return _respuesta
        End Get
        Set(ByVal value As String)
            _respuesta = value
        End Set
    End Property

    Private _id_pregunta As Integer
    Public Property id_pregunta() As Integer
        Get
            Return _id_pregunta
        End Get
        Set(ByVal value As Integer)
            _id_pregunta = value
        End Set
    End Property

End Class
