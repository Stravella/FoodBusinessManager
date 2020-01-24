Imports Entidades
Imports System.Data
Imports System.Data.SqlClient

Public Class SucesoBitacoraDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As SucesoBitacoraDAL
    Public Shared Function ObtenerInstancia() As SucesoBitacoraDAL
        If _instancia Is Nothing Then
            _instancia = New SucesoBitacoraDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function Listar() As List(Of SucesoBitacoraDTO)
        Dim lsSucesoBitacora As New List(Of SucesoBitacoraDTO)
        For Each Row In AccesoDAL.ObtenerInstancia.LeerBD("TipoSuceso_Listar").Rows
            Dim oSuceso As New SucesoBitacoraDTO With {
                                            .id = Row("id_tipo_suceso"),
                                            .descripcion = Row("descripcion")
            }
            lsSucesoBitacora.Add(oSuceso)
        Next
        Return lsSucesoBitacora
    End Function


    Public Function Obtener(unSucesoBitacora As SucesoBitacoraDTO) As SucesoBitacoraDTO
        Try
            Dim ls As New List(Of SucesoBitacoraDTO)
            ls = Me.Listar()
            Dim oSuceso As SucesoBitacoraDTO = Nothing
            For Each Suceso As SucesoBitacoraDTO In ls
                If Suceso.id = unSucesoBitacora.id Then
                    oSuceso = Suceso
                End If
            Next
            Return oSuceso
        Catch ex As Exception

        End Try
    End Function

End Class
