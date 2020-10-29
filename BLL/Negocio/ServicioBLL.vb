Imports DAL
Imports Entidades
Public Class ServicioBLL

#Region "Singleton"
    Private Shared _instancia As ServicioBLL
    Public Shared Function ObtenerInstancia() As ServicioBLL
        If _instancia Is Nothing Then
            _instancia = New ServicioBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function Listar() As List(Of ServicioDTO)
        Try
            Return ServicioDAL.ObtenerInstancia.Listar()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As ServicioDTO
        Try
            Dim lista As List(Of ServicioDTO) = Listar()
            Dim servicio As ServicioDTO = lista.Find(Function(x) x.id = id)
            Return servicio
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorNombre(nombre As String) As ServicioDTO
        Try
            Dim lista As List(Of ServicioDTO) = Listar()
            Dim servicio As ServicioDTO = lista.Find(Function(x) x.nombre = nombre)
            Return servicio
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub Agregar(servicio As ServicioDTO)
        Try
            servicio.imagen = ImagenBLL.ObtenerInstancia.Agregar(servicio.imagen)
            servicio.id = ServicioDAL.ObtenerInstancia.GetNextID
            ServicioDAL.ObtenerInstancia.Agregar(servicio)
            For Each caracteristica As CaracteristicaDTO In servicio.caracteristicas
                ServicioCaracteristicasDAL.ObtenerInstancia.Agregar(servicio, caracteristica)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(servicio As ServicioDTO)
        Try
            ImagenDAL.ObtenerInstancia.Modificar(servicio.imagen)
            ServicioDAL.ObtenerInstancia.Modificar(servicio)
            ServicioCaracteristicasDAL.ObtenerInstancia.EliminarPorServicio(servicio)
            For Each caracteristica As CaracteristicaDTO In servicio.caracteristicas
                ServicioCaracteristicasDAL.ObtenerInstancia.Agregar(servicio, caracteristica)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(id As Integer)
        Try
            ServicioDAL.ObtenerInstancia.EliminarEncuestaServicio(id)
            ServicioCaracteristicasDAL.ObtenerInstancia.EliminarPorServicio(New ServicioDTO With {.id = id})
            ServicioDAL.ObtenerInstancia.Eliminar(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListarPorCaracteristica(caracteristica As CaracteristicaDTO) As List(Of ServicioDTO)
        Try
            Dim ls As List(Of ServicioDTO) = Listar()
            Dim serviciosAsociados As New List(Of ServicioDTO)
            For Each servicio As ServicioDTO In ls
                For Each caract In servicio.caracteristicas
                    If caract.id = caracteristica.id Then
                        serviciosAsociados.Add(servicio)
                    End If
                Next
            Next
            Return serviciosAsociados
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPorIdCatalogo(id As Integer) As List(Of ServicioDTO)
        Try
            Dim ls As List(Of ServicioDTO) = Listar()
            Dim serviciosAsociados As New List(Of ServicioDTO)
            For Each servicio As ServicioDTO In ls
                If servicio.id_catalogo = id Then
                    serviciosAsociados.Add(servicio)
                End If
            Next
            Return serviciosAsociados
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Filtrar(Optional nombre As String = Nothing, Optional precioMin As Decimal = Nothing, Optional precioMax As Decimal = Nothing, Optional caracteristica As CaracteristicaDTO = Nothing) As List(Of ServicioDTO)
        Try
            Return ServicioDAL.ObtenerInstancia.Filtrar(nombre, precioMin, precioMax, caracteristica)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub AgregarEncuestas(id_servicio As Integer, id_encuesta As Integer)
        Try
            ServicioDAL.ObtenerInstancia.AgregarEncuestaServicio(id_encuesta, id_servicio)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListarPorIdEncuesta(id As Integer) As List(Of ServicioDTO)
        Try
            Return ServicioDAL.ObtenerInstancia.ListarPorEncuesta(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
