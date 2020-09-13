Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class ImagenDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ImagenDAL
    Public Shared Function ObtenerInstancia() As ImagenDAL
        If _instancia Is Nothing Then
            _instancia = New ImagenDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal imagen As ImagenDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", imagen.ID))
                params.Add(.CrearParametro("@imagen", imagen.Img64))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Imagenes")
    End Function

    Public Sub Agregar(ByVal imagen As ImagenDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Imagenes_Crear", CrearParametros(imagen))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal imagen As ImagenDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Imagenes_Modificar", CrearParametros(imagen))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal imagen As ImagenDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", imagen.ID)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Imagenes_Eliminar", CrearParametros(imagen))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of ImagenDTO)
        Dim lsImagen As New List(Of ImagenDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Imagenes_Listar").Rows
            Dim imagen As New ImagenDTO With {.ID = row("id"),
                                              .Img64 = row("imagen")
            }
            lsImagen.Add(imagen)
        Next
        Return lsImagen
    End Function

    Public Function Obtener(id As Integer) As ImagenDTO
        Try
            Dim resultado As New ImagenDTO
            Dim imagenes As List(Of ImagenDTO) = Listar()
            For Each imagen In imagenes
                If imagen.ID = id Then
                    resultado = imagen
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
