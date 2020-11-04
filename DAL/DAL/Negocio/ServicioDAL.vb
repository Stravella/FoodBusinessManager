Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class ServicioDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ServicioDAL
    Public Shared Function ObtenerInstancia() As ServicioDAL
        If _instancia Is Nothing Then
            _instancia = New ServicioDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal servicio As ServicioDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", servicio.id))
                params.Add(.CrearParametro("@nombre", servicio.nombre))
                params.Add(.CrearParametro("@descripcion", servicio.descripcion))
                params.Add(.CrearParametro("@precio", servicio.precio))
                params.Add(.CrearParametro("id_imagen", servicio.imagen.ID))
                params.Add(.CrearParametro("id_catalogo", servicio.id_catalogo))
                params.Add(.CrearParametro("orden_catalogo", servicio.orden_catalogo))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Servicio")
    End Function

    Public Sub Agregar(ByVal servicio As ServicioDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Servicios_Crear", CrearParametros(servicio))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal servicio As ServicioDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Servicios_Modificar", CrearParametros(servicio))
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
            AccesoDAL.ObtenerInstancia.EjecutarSP("Servicios_Eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of ServicioDTO)
        Dim servicios As New List(Of ServicioDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Servicios_Listar").Rows
            Dim servicio As New ServicioDTO With {.id = row("id"),
                                              .nombre = row("nombre"),
                                              .descripcion = row("descripcion"),
                                              .precio = row("precio"),
                                              .imagen = ImagenDAL.ObtenerInstancia.Obtener(row("id_imagen")),
                                              .id_catalogo = row("id_catalogo"),
                                              .orden_catalogo = row("orden_catalogo"),
                                              .encuestas = EncuestaDAL.ObtenerInstancia.ListarEncuestasPorIdServicio(row("id")),
                                              .valoracion = ObtenerValoracion(row("id"))
             }
            servicio.caracteristicas = ServicioCaracteristicasDAL.ObtenerInstancia.ListarPorServicio(servicio)
            servicios.Add(servicio)
        Next
        Return servicios
    End Function

    Public Function Obtener(id As Integer) As ServicioDTO
        Try
            Dim resultado As New ServicioDTO
            Dim servicios As List(Of ServicioDTO) = Listar()
            For Each servicio In servicios
                If servicio.id = id Then
                    resultado = servicio
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Filtrar(Optional nombre As String = Nothing, Optional precioMin As Decimal = Nothing, Optional precioMax As Decimal = Nothing, Optional caracteristica As CaracteristicaDTO = Nothing) As List(Of ServicioDTO)
        Try
            Dim servicios As New List(Of ServicioDTO)
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                If nombre <> "" Then
                    params.Add(.CrearParametro("@nombre", nombre))
                Else
                    params.Add(New SqlParameter With {.ParameterName = "@nombre", .Value = DBNull.Value})
                End If
                If precioMin > 0 Then
                    params.Add(.CrearParametro("@precio_min", precioMin))
                Else
                    params.Add(New SqlParameter With {.ParameterName = "@precio_min", .Value = DBNull.Value})
                End If
                If precioMax > 0 Then
                    params.Add(.CrearParametro("@precio_max", precioMax))
                Else
                    params.Add(New SqlParameter With {.ParameterName = "@precio_max", .Value = DBNull.Value})
                End If
            End With
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Servicio_filtar", params).Rows
                Dim servicio As New ServicioDTO With {.id = row("id"),
                                              .nombre = row("nombre"),
                                              .descripcion = row("descripcion"),
                                              .precio = row("precio"),
                                              .imagen = ImagenDAL.ObtenerInstancia.Obtener(row("id_imagen")),
                                              .id_catalogo = row("id_catalogo"),
                                              .orden_catalogo = row("orden_catalogo")
             }
                servicio.caracteristicas = ServicioCaracteristicasDAL.ObtenerInstancia.ListarPorServicio(servicio)
                servicios.Add(servicio)
            Next
            Dim resultado As New List(Of ServicioDTO)
            If caracteristica Is Nothing Then
                resultado = servicios
            Else
                For Each servicio In servicios
                    For Each oCaracteristica In servicio.caracteristicas
                        If oCaracteristica.id = caracteristica.id Then
                            resultado.Add(servicio)
                        End If
                    Next
                Next
            End If
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function



#Region "Servicio_encuestas"
    'Borro el servicio, borro la relacion con las encuestas
    Public Sub EliminarEncuestaServicio(ByVal id_servicio As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_servicio", id_servicio)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_servicio_eliminarPorServicio", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub AgregarEncuestaServicio(ByVal id_encuesta As Integer, id_servicio As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_encuesta", id_encuesta)))
                params.Add((.CrearParametro("@id_servicio", id_servicio)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Encuesta_servicio_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Public Function ListarPorEncuesta(id_encuesta As Integer) As List(Of ServicioDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_encuesta", id_encuesta)))
            End With
            Dim servicios As New List(Of ServicioDTO)
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Encuesta_servicio_obtenerPorEncuesta", params).Rows
                Dim servicio As New ServicioDTO With {.id = row("id"),
                                              .nombre = row("nombre"),
                                              .descripcion = row("descripcion"),
                                              .precio = row("precio"),
                                              .imagen = ImagenDAL.ObtenerInstancia.Obtener(row("id_imagen")),
                                              .id_catalogo = row("id_catalogo"),
                                              .orden_catalogo = row("orden_catalogo"),
                                              .encuestas = EncuestaDAL.ObtenerInstancia.ListarEncuestasPorIdServicio(row("id"))
             }
                servicio.caracteristicas = ServicioCaracteristicasDAL.ObtenerInstancia.ListarPorServicio(servicio)
                servicios.Add(servicio)
            Next
            Return servicios
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerValoracion(id_servicio As Integer) As Double
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_servicio", id_servicio)))
            End With
            Return Math.Round(AccesoDAL.ObtenerInstancia.LeerBD("Servicio_promedio", params).Rows(0)(0), 1)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region





End Class
