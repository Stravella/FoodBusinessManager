Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class ProvinciasDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ProvinciasDAL
    Public Shared Function ObtenerInstancia() As ProvinciasDAL
        If _instancia Is Nothing Then
            _instancia = New ProvinciasDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal provincia As ProvinciaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_provincia", provincia.id))
                params.Add(.CrearParametro("@Provincia", provincia.provincia))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function ListarProvincias() As List(Of ProvinciaDTO)
        Dim lsProvincias As New List(Of ProvinciaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Provincias_Listar").Rows
            Dim oProvincia As New ProvinciaDTO With {.id = row("id_provincia"),
                                              .provincia = row("Provincia")
            }
            lsProvincias.Add(oProvincia)
        Next
        Return lsProvincias
    End Function


End Class
