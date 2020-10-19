Public Class ServicioCarritoDTO
    Private _servicio As ServicioDTO
    Public Property servicio() As ServicioDTO
        Get
            Return _servicio
        End Get
        Set(ByVal value As ServicioDTO)
            _servicio = value
        End Set
    End Property

    Private _cantidad As Integer
    Public Property cantidad() As Integer
        Get
            Return _cantidad
        End Get
        Set(ByVal value As Integer)
            _cantidad = value
        End Set
    End Property

    Private _importeTotal As Double
    Public Property importeTotal() As Double
        Get
            Return _importeTotal
        End Get
        Set(ByVal value As Double)
            _importeTotal = value
        End Set
    End Property

End Class
