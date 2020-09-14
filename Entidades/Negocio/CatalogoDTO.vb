Public Class CatalogoDTO

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

    Private _servicios As List(Of ServicioDTO)
    Public Property servicios() As List(Of ServicioDTO)
        Get
            Return _servicios
        End Get
        Set(ByVal value As List(Of ServicioDTO))
            _servicios = value
        End Set
    End Property

End Class
