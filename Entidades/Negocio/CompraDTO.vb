Public Class CompraDTO

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _fecha As Date
    Public Property fecha() As Date
        Get
            Return _fecha
        End Get
        Set(ByVal value As Date)
            _fecha = value
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

    Private _carrito As List(Of ServicioCarritoDTO)
    Public Property carrito() As List(Of ServicioCarritoDTO)
        Get
            Return _carrito
        End Get
        Set(ByVal value As List(Of ServicioCarritoDTO))
            _carrito = value
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

    Private _factura As FacturaDTO
    Public Property factura() As FacturaDTO
        Get
            Return _factura
        End Get
        Set(ByVal value As FacturaDTO)
            _factura = value
        End Set
    End Property

    Private _estado As EstadoCompraDTO
    Public Property estado() As EstadoCompraDTO
        Get
            Return _estado
        End Get
        Set(ByVal value As EstadoCompraDTO)
            _estado = value
        End Set
    End Property


End Class
