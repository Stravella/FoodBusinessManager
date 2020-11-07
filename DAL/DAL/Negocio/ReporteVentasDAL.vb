Imports Entidades
Imports System.Data.SqlClient

Public Class ReporteVentasDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ReporteVentasDAL
    Public Shared Function ObtenerInstancia() As ReporteVentasDAL
        If _instancia Is Nothing Then
            _instancia = New ReporteVentasDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function ReporteAnual(año As Integer) As List(Of ReporteVentasDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia()
            params.Add((.CrearParametro("@año", año)))
        End With
        Dim ls As New List(Of ReporteVentasDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Ventas_año", params).Rows
            Dim reporte As New ReporteVentasDTO With {.nombre = row("nombre"),
                                              .importe = row("importe")
            }
            ls.Add(reporte)
        Next
        Return ls
    End Function

    Public Function ReporteAñoDesdeHasta(año_desde As Integer, año_hasta As Integer) As List(Of ReporteVentasDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia()
            params.Add((.CrearParametro("@año_desde", año_desde)))
            params.Add((.CrearParametro("@año_hasta", año_hasta)))
        End With
        Dim ls As New List(Of ReporteVentasDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Ventas_años", params).Rows
            Dim reporte As New ReporteVentasDTO With {.nombre = row("nombre"),
                                              .importe = row("importe")
            }
            ls.Add(reporte)
        Next
        Return ls
    End Function

    Public Function ReporteMensual(mes As Integer) As List(Of ReporteVentasDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia()
            params.Add((.CrearParametro("@mes", mes)))
        End With
        Dim ls As New List(Of ReporteVentasDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Ventas_mes", params).Rows
            Dim reporte As New ReporteVentasDTO With {.nombre = row("nombre"),
                                              .importe = row("importe")
            }
            ls.Add(reporte)
        Next
        Return ls
    End Function

    Public Function ReporteSemanal(año As Integer) As List(Of ReporteVentasDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia()
            params.Add((.CrearParametro("@año", año)))
        End With
        Dim ls As New List(Of ReporteVentasDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Ventas_semanal", params).Rows
            Dim reporte As New ReporteVentasDTO With {.nombre = row("nombre"),
                                              .importe = row("importe")
            }
            ls.Add(reporte)
        Next
        Return ls
    End Function

End Class
