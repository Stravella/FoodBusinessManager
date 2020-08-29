
Imports System.Data.SqlClient
Imports Entidades

Public Class CriticidadDAL
    Private Shared _instancia As CriticidadDAL
    Public Shared Function ObtenerInstancia() As CriticidadDAL
        If _instancia Is Nothing Then
            _instancia = New CriticidadDAL
        End If
        Return _instancia
    End Function

    Public Function Listar() As List(Of CriticidadDTO)
        Dim ls As New List(Of CriticidadDTO)
        For Each Row In AccesoDAL.ObtenerInstancia.LeerBD("Criticidad_Listar").Rows
            Dim o As New CriticidadDTO With {
                                            .id = Row("id_criticidad"),
                                            .criticidad = Row("criticidad")
            }
            ls.Add(o)
        Next
        Return ls
    End Function

    Public Function ObtenerPorId(id As Integer) As CriticidadDTO
        Dim resultado As CriticidadDTO
        Try
            For Each crit As CriticidadDTO In Listar()
                If crit.id = id Then
                    resultado = crit
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
