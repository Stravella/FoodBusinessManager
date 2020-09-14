Imports Entidades
Imports System.Data.SqlClient
Imports System.Data

Public Class CatalogoDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As CatalogoDAL
    Public Shared Function ObtenerInstancia() As CatalogoDAL
        If _instancia Is Nothing Then
            _instancia = New CatalogoDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal catalogo As CatalogoDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_catalogo", catalogo.id))
                params.Add(.CrearParametro("@nombre", catalogo.nombre))
                params.Add(.CrearParametro("@descripcion", catalogo.descripcion))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_catalogo", "Catalogo")
    End Function

    Public Sub Agregar(ByVal catalogo As CatalogoDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Catalogo_Crear", CrearParametros(catalogo))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal catalogo As CatalogoDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Catalogo_Modificar", CrearParametros(catalogo))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal id As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_catalogo", id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Catalogo_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of CatalogoDTO)
        Dim lsCatalogo As New List(Of CatalogoDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Catalogo_Listar").Rows
            Dim catalogo As New CatalogoDTO With {.id = row("id_catalogo"),
                                              .nombre = row("nombre"),
                                              .descripcion = row("descripcion")
            }
            lsCatalogo.Add(catalogo)
        Next
        Return lsCatalogo
    End Function

    Public Function Obtener(id As Integer) As CatalogoDTO
        Try
            Dim resultado As New CatalogoDTO
            Dim catalogos As List(Of CatalogoDTO) = Listar()
            For Each catalogo In catalogos
                If catalogo.id = id Then
                    resultado = catalogo
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
