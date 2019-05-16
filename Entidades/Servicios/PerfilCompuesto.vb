Imports Entidades

Public Class PerfilCompuesto
    Inherits PermisoComponente

    Private _hijos As New List(Of PermisoComponente)
    Public ReadOnly Property Hijos() As List(Of PermisoComponente)
        Get
            Return _hijos
        End Get
    End Property

    Private _substracciones As New List(Of PermisoComponente)
    Public ReadOnly Property substracciones() As List(Of PermisoComponente)
        Get
            Return _substracciones
        End Get
    End Property

    Public Overrides Function PuedeUsar(unaUrl As String) As Boolean
        Return _hijos.Any(Function(h) h.PuedeUsar(unaUrl)) AndAlso
                Not _substracciones.Any(Function(s) s.PuedeUsar(unaUrl))
    End Function

    Public Overrides Function agregarHijo(hijo As PermisoComponente) As Boolean
        If Not Hijos.Contains(hijo) Then
            Hijos.Add(hijo)
        End If
    End Function

    Public Function agregarSubstraccion(substraccion As PermisoComponente) As Boolean
        If Not substracciones.Contains(substraccion) Then
            substracciones.Add(substraccion)
        End If
    End Function

    Public Overrides Function tieneHijos() As Boolean
        Return True
    End Function

    Public Overrides Function esValido(nombrePermiso As String) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
