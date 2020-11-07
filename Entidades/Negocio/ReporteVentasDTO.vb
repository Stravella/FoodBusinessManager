Public Class ReporteVentasDTO

    Private _nombre As String
    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _importe As Integer
    Public Property importe() As Integer
        Get
            Return _importe
        End Get
        Set(ByVal value As Integer)
            _importe = value
        End Set
    End Property



End Class
