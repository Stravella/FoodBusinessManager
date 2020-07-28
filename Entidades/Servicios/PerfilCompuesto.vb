Imports Entidades

Public Class PerfilCompuesto
    Inherits PermisoComponente

    Private _hijos As New List(Of PermisoComponente)
    Public ReadOnly Property Hijos() As List(Of PermisoComponente)
        Get
            Return _hijos
        End Get
    End Property


    Public Overrides Function PuedeUsar(unaUrl As String) As Boolean
        Return _hijos.Any(Function(h) h.PuedeUsar(unaUrl))
    End Function

    Public Overrides Function agregarHijo(hijo As PermisoComponente) As Boolean
        If Not Hijos.Contains(hijo) Then
            Hijos.Add(hijo)
        End If
    End Function

    Public Overrides Function tieneHijos() As Boolean
        Return True
    End Function

    Public Overrides Function esValido(nombrePermiso As String) As Boolean
        Dim tieneUnValido As Boolean = False
        If nombrePermiso = Me.nombre Then
            Return True
        End If
        For Each p In Me._hijos
            If p.nombre = nombrePermiso Then
                Return True
            Else
                tieneUnValido = p.esValido(nombrePermiso)
            End If
            If tieneUnValido = True Then
                Exit For
            Else

            End If
        Next
        Return tieneUnValido
    End Function
End Class
