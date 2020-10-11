Imports Entidades
Imports System.Data
Imports System.Data.SqlClient
Public Class CategoriaDAL


#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As CategoriaDAL
    Public Shared Function ObtenerInstancia() As CategoriaDAL
        If _instancia Is Nothing Then
            _instancia = New CategoriaDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal categoria As CategoriaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", categoria.id))
                params.Add(.CrearParametro("@nombre", categoria.nombre))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Categorias")
    End Function

    Public Sub Agregar(ByVal categoria As CategoriaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Categorias_Crear", CrearParametros(categoria))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal categoria As CategoriaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Categorias_Modificar", CrearParametros(categoria))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal categoria As CategoriaDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", categoria.id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Categorias_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of CategoriaDTO)
        Dim lsCategoria As New List(Of CategoriaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Categorias_Listar").Rows
            Dim categoria As New CategoriaDTO With {.id = row("id"),
                                              .nombre = row("nombre"),
                                              .subscriptores = SubscripcionesDAL.ObtenerInstancia.ListarPorCategoria(row("id"))
            }
            lsCategoria.Add(categoria)
        Next
        Return lsCategoria
    End Function

    Public Function Obtener(id As Integer) As CategoriaDTO
        Try
            Dim resultado As New CategoriaDTO
            Dim categorias As List(Of CategoriaDTO) = Listar()
            For Each categoria In categorias
                If categoria.id = id Then
                    resultado = categoria
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
