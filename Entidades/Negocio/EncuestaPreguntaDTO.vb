Public Class EncuestaPreguntaDTO
    Private _Id As Integer
    Public Property ID() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _pregunta As String
    Public Property pregunta() As String
        Get
            Return _pregunta
        End Get
        Set(ByVal value As String)
            _pregunta = value
        End Set
    End Property

    Private _fechaVenc As Date
    Public Property FechaVenc() As Date
        Get
            Return _fechaVenc
        End Get
        Set(ByVal value As Date)
            _fechaVenc = value
        End Set
    End Property

    ''' <summary>
    ''' Son las respuestas que son enunciado.
    ''' </summary>
    ''' <remarks></remarks>
    Private _respuestas As New List(Of RespuestaEncuestaDTO)
    Public Property Respuestas() As List(Of RespuestaEncuestaDTO)
        Get
            Return _respuestas
        End Get
        Set(ByVal value As List(Of RespuestaEncuestaDTO))
            _respuestas = value
        End Set
    End Property

    ''' <summary>
    ''' Son las respuestas que son resultado, las que ligió el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    Private _resultados As New List(Of RespuestaEncuestaDTO)
    Public Property Resultados() As List(Of RespuestaEncuestaDTO)
        Get
            Return _resultados
        End Get
        Set(ByVal value As List(Of RespuestaEncuestaDTO))
            _resultados = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de enunciados / valor para presentar en reportes
    ''' </summary>
    ''' <remarks></remarks>
    Private _reporte As New List(Of ResultadosDTO)
    Public Property Reporte() As List(Of ResultadosDTO)
        Get
            Return _reporte
        End Get
        Set(ByVal value As List(Of ResultadosDTO))
            _reporte = value
        End Set
    End Property

    ''' <summary>
    ''' para reportes obtiene la cantidad de veces que una respuesta fue respondida
    ''' </summary>
    ''' <remarks></remarks>
    Private Q_Respondidas As Integer
    Public Property QRespondidas() As Integer
        Get
            Return Q_Respondidas
        End Get
        Set(ByVal value As Integer)
            Q_Respondidas = value
        End Set
    End Property

    Private _estado As EstadoPreguntaEncuestaDTO
    Public Property Estado() As EstadoPreguntaEncuestaDTO
        Get
            Return _estado
        End Get
        Set(ByVal value As EstadoPreguntaEncuestaDTO)
            _estado = value
        End Set
    End Property

End Class
