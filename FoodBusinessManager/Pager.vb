Public Class Pager

    Public Sub New(RegistrosPorPagina As Integer, NroPagina As Integer)
        Me.RegistroPorPagina = RegistrosPorPagina
        Me.PaginaActual = NroPagina
        Me.CantidadRegistros = 0
    End Sub

    Private _CatntidadRegistros As Integer

    Public Property CantidadRegistros() As String
        Get
            Return _CatntidadRegistros
        End Get
        Set(ByVal value As String)
            _CatntidadRegistros = value
        End Set
    End Property

    Private _PaginaActual As Integer
    Public Property PaginaActual() As Integer
        Get
            Return _PaginaActual
        End Get
        Set(ByVal value As Integer)
            _PaginaActual = value
        End Set
    End Property

    Private _RegistrosPorPagina As Integer
    Public Property RegistroPorPagina() As Integer
        Get
            Return _RegistrosPorPagina
        End Get
        Set(ByVal value As Integer)
            _RegistrosPorPagina = value
        End Set
    End Property

    Private _CantidadPaginas As Integer
    Public Property CantidadPaginas() As Integer
        Get
            Return _CantidadPaginas
        End Get
        Set(ByVal value As Integer)
            _CantidadPaginas = value
        End Set
    End Property

    Private _PaginaSiguiente As Boolean
    Public Property SiguientePagina() As Boolean
        Get
            Return _PaginaSiguiente
        End Get
        Set(ByVal value As Boolean)
            _PaginaSiguiente = value
        End Set
    End Property

    Private _PaginaAnterior As Boolean
    Public Property PaginaAnterior() As Boolean
        Get
            Return _PaginaAnterior
        End Get
        Set(ByVal value As Boolean)
            _PaginaAnterior = value
        End Set
    End Property

End Class
