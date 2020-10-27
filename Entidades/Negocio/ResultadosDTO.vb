Public Class ResultadosDTO
    Private _respuesta As String
    Public Property Respuesta() As String
            Get
                Return _respuesta
            End Get
            Set(ByVal value As String)
                _respuesta = value
            End Set
        End Property

        Private _cantidad As Integer
        Public Property Cantidad() As Integer
            Get
                Return _cantidad
            End Get
            Set(ByVal value As Integer)
                _cantidad = value
            End Set
        End Property

End Class
