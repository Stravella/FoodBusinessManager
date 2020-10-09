Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class NewsletterDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As NewsletterDAL
    Public Shared Function ObtenerInstancia() As NewsletterDAL
        If _instancia Is Nothing Then
            _instancia = New NewsletterDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal newsletter As NewsletterDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", newsletter.ID))
                params.Add(.CrearParametro("@titulo", newsletter.Titulo))
                params.Add(.CrearParametro("@cuerpo", newsletter.Cuerpo))
                params.Add(.CrearParametro("@id_imagen", newsletter.Imagen.ID))
                params.Add(.CrearParametro("@id_estado", newsletter.Estado.ID))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Newsletter")
    End Function

    Public Sub Agregar(ByVal newsletter As NewsletterDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Newsletter_crear", CrearParametros(newsletter))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal newsletter As NewsletterDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Newsletter_modificar", CrearParametros(newsletter))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(ByVal id As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Newsletter_eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of NewsletterDTO)
        Dim lsNewsletter As New List(Of NewsletterDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Newsletter_Listar").Rows
            Dim newsletter As New NewsletterDTO With {.ID = row("id"),
                                              .Titulo = row("titulo"),
                                              .Cuerpo = row("cuerpo"),
                                              .Imagen = ImagenDAL.ObtenerInstancia.Obtener(row("id_imagen")),
                                              .Estado = EstadoDAL.ObtenerInstancia.Obtener(row("id_estado"))
            }
            lsNewsletter.Add(newsletter)
        Next
        Return lsNewsletter
    End Function

    Public Function Obtener(id As Integer) As NewsletterDTO
        Try
            Dim resultado As New NewsletterDTO
            Dim newsletters As List(Of NewsletterDTO) = Listar()
            For Each newsletter In newsletters
                If newsletter.ID = id Then
                    resultado = newsletter
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
