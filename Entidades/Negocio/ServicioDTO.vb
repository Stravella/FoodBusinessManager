Public Class ServicioDTO
    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _nombre As String
    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _descripcion As String
    Public Property descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Private _precio As Decimal
    Public Property precio() As Decimal
        Get
            Return _precio
        End Get
        Set(ByVal value As Decimal)
            _precio = value
        End Set
    End Property

    Private _imagen As ImagenDTO
    Public Property imagen() As ImagenDTO
        Get
            Return _imagen
        End Get
        Set(ByVal value As ImagenDTO)
            _imagen = value
        End Set
    End Property

    Private _caracteristicas As List(Of CaracteristicaDTO)
    Public Property caracteristicas() As List(Of CaracteristicaDTO)
        Get
            Return _caracteristicas
        End Get
        Set(ByVal value As List(Of CaracteristicaDTO))
            _caracteristicas = value
        End Set
    End Property


    Private _id_catalogo As Integer
    Public Property id_catalogo() As Integer
        Get
            Return _id_catalogo
        End Get
        Set(ByVal value As Integer)
            _id_catalogo = value
        End Set
    End Property

    Private _orden_catalogo As Integer
    Public Property orden_catalogo() As Integer
        Get
            Return _orden_catalogo
        End Get
        Set(ByVal value As Integer)
            _orden_catalogo = value
        End Set
    End Property

End Class
