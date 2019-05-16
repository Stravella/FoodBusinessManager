Public Class IdiomaDTO
    Private _id_idioma As String
    Private _nombre As String
    Private _DVH As String
    Private _ListaEtiquetas As List(Of IdiomaEtiquetaDTO)

    Public Property id_idioma() As String
        Get
            Return _id_idioma
        End Get
        Set(ByVal Value As String)
            _id_idioma = Value
        End Set
    End Property

    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal Value As String)
            _nombre = Value
        End Set
    End Property

    Public Property DVH() As String
        Get
            Return _DVH
        End Get
        Set(ByVal value As String)
            _DVH = value
        End Set
    End Property

    Public Property ListaEtiquetas() As List(Of IdiomaEtiquetaDTO)
        Get
            Return _ListaEtiquetas
        End Get
        Set(ByVal Value As List(Of IdiomaEtiquetaDTO))
            _ListaEtiquetas = Value
        End Set
    End Property
End Class
