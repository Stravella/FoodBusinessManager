Public Class NotaCreditoDTO

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

    Private _id_factura As Integer
    Public Property id_factura() As Integer
        Get
            Return _id_factura
        End Get
        Set(ByVal value As Integer)
            _id_factura = value
        End Set
    End Property

    Private _concepto As String
    Public Property concepto() As String
        Get
            Return _concepto
        End Get
        Set(ByVal value As String)
            _concepto = value
        End Set
    End Property

    Private _importe As Double
    Public Property importe() As Double
        Get
            Return _importe
        End Get
        Set(ByVal value As Double)
            _importe = value
        End Set
    End Property

    Private _estado As EstadoNotaDTO
    Public Property estado() As EstadoNotaDTO
        Get
            Return _estado
        End Get
        Set(ByVal value As EstadoNotaDTO)
            _estado = value
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
End Class
