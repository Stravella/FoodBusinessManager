Imports System.Data.SqlClient
Imports Entidades

Public Class BusquedaDAL

    Private Shared _instancia As BusquedaDAL
    Public Shared Function ObtenerInstancia() As BusquedaDAL
        If _instancia Is Nothing Then
            _instancia = New BusquedaDAL
        End If
        Return _instancia
    End Function


    Public Function Listar(palabra As String, backend As Integer) As List(Of BusquedaDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia
            params.Add(.CrearParametro("@palabra_buscar", palabra))
            params.Add(.CrearParametro("@usuario", backend))
        End With
        Dim ls As New List(Of BusquedaDTO)
        For Each Row In AccesoDAL.ObtenerInstancia.LeerBD("Busqueda_frontEnd", params).Rows
            Dim o As New BusquedaDTO With {
                                            .Id = Row("id"),
                                            .URL = Row("url"),
                                            .Menu = Row("descripcion")
            }
            ls.Add(o)
        Next
        Return ls
    End Function

End Class
