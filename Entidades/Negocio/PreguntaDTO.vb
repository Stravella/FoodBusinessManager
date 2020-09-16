Public Class PreguntaDTO
    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
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

    Private _fechaVencimiento As Date
    Public Property fechaVencimiento() As Date
        Get
            Return _fechaVencimiento
        End Get
        Set(ByVal value As Date)
            _fechaVencimiento = value
        End Set
    End Property

    Private _id_servicio As Integer
    Public Property id_servicio() As Integer
        Get
            Return _id_servicio
        End Get
        Set(ByVal value As Integer)
            _id_servicio = value
        End Set
    End Property

    Private _respuesats As List(Of RespuestaDTO)
    Public Property respuestas() As List(Of RespuestaDTO)
        Get
            Return _respuesats
        End Get
        Set(ByVal value As List(Of RespuestaDTO))
            _respuesats = value
        End Set
    End Property

    Private _usuario As UsuarioDTO
    Public Property usuario() As UsuarioDTO
        Get
            Return _usuario
        End Get
        Set(ByVal value As UsuarioDTO)
            _usuario = value
        End Set
    End Property

End Class
