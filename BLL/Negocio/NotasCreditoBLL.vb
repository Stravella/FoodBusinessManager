Imports Entidades
Imports DAL

Public Class NotasCreditoBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As NotasCreditoBLL
    Public Shared Function ObtenerInstancia() As NotasCreditoBLL
        If _instancia Is Nothing Then
            _instancia = New NotasCreditoBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(nota As NotaCreditoDTO)
        Try
            nota.id = NotaCreditoDAL.ObtenerInstancia.GetNextID
            NotaCreditoDAL.ObtenerInstancia.Agregar(nota)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function ListarRedimiblesPorCliente(id_cliente As Integer) As List(Of NotaCreditoDTO)
        Try
            Return NotaCreditoDAL.ObtenerInstancia.ListarRedimiblesPorCliente(id_cliente)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPorIdCliente(id_cliente As Integer) As List(Of NotaCreditoDTO)
        Try
            Return NotaCreditoDAL.ObtenerInstancia.ListarPorCliente(id_cliente)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Obtener(id As Integer) As NotaCreditoDTO
        Try
            Return NotaCreditoDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
