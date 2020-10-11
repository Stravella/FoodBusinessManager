Imports Entidades
Imports System.Data
Imports System.Data.SqlClient
Public Class SubscripcionesDAL


#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As SubscripcionesDAL
    Public Shared Function ObtenerInstancia() As SubscripcionesDAL
        If _instancia Is Nothing Then
            _instancia = New SubscripcionesDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal subscriptor As SubscriptorDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", subscriptor.id))
                params.Add(.CrearParametro("@subscriptor", subscriptor.mail))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Subscriptores")
    End Function

    Public Sub Agregar(ByVal subscriptor As SubscriptorDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Subscriptor_crear", CrearParametros(subscriptor))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub Eliminar(ByVal subscriptor As SubscriptorDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", subscriptor.id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Subscriptor_eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of SubscriptorDTO)
        Dim lsSubscriptores As New List(Of SubscriptorDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Subscriptor_listar").Rows
            Dim subscriptor As New SubscriptorDTO With {.id = row("id"),
                                              .mail = row("subscriptor")
            }
            lsSubscriptores.Add(subscriptor)
        Next
        Return lsSubscriptores
    End Function

    Public Function Obtener(id As Integer) As SubscriptorDTO
        Try
            Dim resultado As New SubscriptorDTO
            Dim subscriptores As List(Of SubscriptorDTO) = Listar()
            For Each subscriptor In subscriptores
                If subscriptor.id = id Then
                    resultado = subscriptor
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ListarPorCategoria(id_categoria As Integer) As List(Of SubscriptorDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_categoria", id_categoria)))
            End With
            Dim lsSubscriptores As New List(Of SubscriptorDTO)
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Subscriptor_categoria_listar_por_categoria", params).Rows
                Dim subscriptor As New SubscriptorDTO With {.id = row("id"),
                                                  .mail = row("subscriptor")
                }
                lsSubscriptores.Add(subscriptor)
            Next
            Return lsSubscriptores
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Desubscribir(ByVal subscriptor As SubscriptorDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_subscriptor", subscriptor.id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Subscriptor_categoria_eliminar_subscripcion", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub subscribir(id_subscriptor As Integer, id_categoria As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_subscriptor", id_subscriptor)))
                params.Add((.CrearParametro("@id_categoria", id_categoria)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Subscriptor_categoria_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
