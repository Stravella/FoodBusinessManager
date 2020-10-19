Imports DAL
Imports Entidades

Public Class FacturaBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As FacturaBLL
    Public Shared Function ObtenerInstancia() As FacturaBLL
        If _instancia Is Nothing Then
            _instancia = New FacturaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(factura As FacturaDTO)
        Try
            factura.id = FacturaDAL.ObtenerInstancia.GetNextID()
            FacturaDAL.ObtenerInstancia.Agregar(factura)
            If factura.notasCredito IsNot Nothing Then
                For Each notaCredito In factura.notasCredito
                    FacturaDAL.ObtenerInstancia.CrearFacturaNota(factura.id, notaCredito.id)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
