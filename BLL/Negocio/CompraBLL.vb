Imports DAL
Imports Entidades
Public Class CompraBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As CompraBLL
    Public Shared Function ObtenerInstancia() As CompraBLL
        If _instancia Is Nothing Then
            _instancia = New CompraBLL
        End If
        Return _instancia
    End Function
#End Region
    Public Sub Agregar(compra As CompraDTO)
        Try
            compra.id = CompraDAL.ObtenerInstancia.GetNextID()
            CompraDAL.ObtenerInstancia.Agregar(compra)
            For Each serv As ServicioCarritoDTO In compra.carrito
                CompraDAL.ObtenerInstancia.AgregarCompraServicio(compra, serv.servicio)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(compra As CompraDTO)
        Try
            CompraDAL.ObtenerInstancia.Modificar(compra)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Obtener(id As Integer) As CompraDTO
        Try
            Return CompraDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ListarPorIdUsuario(id As Integer)
        Try
            Return CompraDAL.ObtenerInstancia.ListarPorIdUsuario(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
