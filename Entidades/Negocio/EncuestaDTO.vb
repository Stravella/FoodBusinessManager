Public Class EncuestaDTO

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

    Private _preguntas As New List(Of EncuestaPreguntaDTO)
    Public Property preguntas() As List(Of EncuestaPreguntaDTO)
        Get
            Return _preguntas
        End Get
        Set(ByVal value As List(Of EncuestaPreguntaDTO))
            _preguntas = value
        End Set
    End Property

    Private _tipo As TipoEncuestaDTO
    Public Property tipo() As TipoEncuestaDTO
        Get
            Return _tipo
        End Get
        Set(ByVal value As TipoEncuestaDTO)
            _tipo = value
        End Set
    End Property

End Class
