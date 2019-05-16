Imports Entidades

Public Class PermisoHoja
    Inherits PermisoComponente

    Public Overrides Function PuedeUsar(unaUrl As String) As Boolean
        If Me.url_acceso = unaUrl Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function agregarHijo(hijo As PermisoComponente) As Boolean
        Return False
    End Function

    Public Overrides Function tieneHijos() As Boolean
        Return False
    End Function

    Public Overrides Function esValido(nombrePermiso As String) As Boolean
        Return Me.nombre.Equals(nombrePermiso)
    End Function
End Class
