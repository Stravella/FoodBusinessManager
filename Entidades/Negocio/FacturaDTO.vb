Public Class FacturaDTO

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _cliente As ClienteDTO
    Public Property cliente() As ClienteDTO
        Get
            Return _cliente
        End Get
        Set(ByVal value As ClienteDTO)
            _cliente = value
        End Set
    End Property

    Private _total As Double
    Public Property total() As Double
        Get
            Return _total
        End Get
        Set(ByVal value As Double)
            _total = value
        End Set
    End Property

    Private _tarjeta As TarjetaDTO
    Public Property tarjeta() As TarjetaDTO
        Get
            Return _tarjeta
        End Get
        Set(ByVal value As TarjetaDTO)
            _tarjeta = value
        End Set
    End Property

    Private _importeTarjeta As Double
    Public Property importeTarjeta() As Double
        Get
            Return _importeTarjeta
        End Get
        Set(ByVal value As Double)
            _importeTarjeta = value
        End Set
    End Property

    Private _notasCredito As List(Of NotaCreditoDTO)
    Public Property notasCredito() As List(Of NotaCreditoDTO)
        Get
            Return _notasCredito
        End Get
        Set(ByVal value As List(Of NotaCreditoDTO))
            _notasCredito = value
        End Set
    End Property

End Class
