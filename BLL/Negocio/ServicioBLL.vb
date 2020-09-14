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

End Class
