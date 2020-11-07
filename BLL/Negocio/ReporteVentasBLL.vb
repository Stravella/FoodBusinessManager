Imports DAL
Imports Entidades

Public Class ReporteVentasBLL
#Region "Singleton"
    Private Shared _instancia As ReporteVentasBLL
    Public Shared Function ObtenerInstancia() As ReporteVentasBLL
        If _instancia Is Nothing Then
            _instancia = New ReporteVentasBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Function ReporteAnual(año As Integer) As List(Of ReporteVentasDTO)
        Try
            Return ReporteVentasDAL.ObtenerInstancia.ReporteAnual(año)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ReporteAñoDesdeHasta(año_desde As Integer, año_hasta As Integer) As List(Of ReporteVentasDTO)
        Try
            Return ReporteVentasDAL.ObtenerInstancia.ReporteAñoDesdeHasta(año_desde, año_hasta)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ReporteMensual(mes As Integer) As List(Of ReporteVentasDTO)
        Try
            Return ReporteVentasDAL.ObtenerInstancia.ReporteMensual(mes)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ReporteSemanal(año As Integer) As List(Of ReporteVentasDTO)
        Try
            Return ReporteVentasDAL.ObtenerInstancia.ReporteSemanal(año)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
