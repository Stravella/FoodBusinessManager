Imports Entidades
Imports System.Data
Imports System.Data.SqlClient

Public Class DigitoVerificadorDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As DigitoVerificadorDAL
    Public Shared Function ObtenerInstancia() As DigitoVerificadorDAL
        If _instancia Is Nothing Then
            _instancia = New DigitoVerificadorDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function ListarTodos() As DataTable
        Try
            Dim DT As DataTable = AccesoDAL.ObtenerInstancia.LeerBD("select * from DVV")
        Catch ex As Exception

        End Try
    End Function

    Public Function Obtener(unaTabla As String) As String
        Try
            For Each row As DataRow In ListarTodos().Rows
                If row("tabla") = unaTabla Then
                    Return row.Item("dvv").ToString
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Function

    Public Sub Agregar(unaTabla As String, dvv As String)
        Try
            AccesoDAL.ObtenerInstancia.EscribirBD("INSERT INTO DVV(tabla, dvv) VALUES(" & unaTabla & ", " & dvv & ")")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Modificar(unaTabla As String, dvv As String)
        Try
            AccesoDAL.ObtenerInstancia.EscribirBD("UPDATE DVV SET dvv= " & dvv & " where tabla=" & unaTabla & ")")
        Catch ex As Exception

        End Try
    End Sub

    Public Function ObtenerTodoDVH(unaTabla As String) As DataTable
        Try
            Dim DT As DataTable = AccesoDAL.ObtenerInstancia.LeerBD("SELECT dvh FROM" + unaTabla)
            Return DT
        Catch ex As Exception

        End Try
    End Function

    Public Function TieneRegistros(unaTabla As String) As Integer
        Try
            Dim dt As DataTable = AccesoDAL.ObtenerInstancia.LeerBD("SELECT COUNT(dvv) FROM" + unaTabla)
            Return Convert.ToInt32(dt.Rows(0))
        Catch ex As Exception

        End Try
    End Function

    Public Function ObtenerTabla(unaTabla As String) As DataTable
        Try
            Dim dt As DataTable = AccesoDAL.ObtenerInstancia.LeerBD("SELECT * FROM" + unaTabla)
            Return dt
        Catch ex As Exception

        End Try
    End Function

End Class
