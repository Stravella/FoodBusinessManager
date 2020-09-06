Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class EstadoClienteDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As EstadoClienteDAL
    Public Shared Function ObtenerInstancia() As EstadoClienteDAL
        If _instancia Is Nothing Then
            _instancia = New EstadoClienteDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal estadoCliente As EstadoClienteDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_estado_cliente", estadoCliente.id))
                params.Add(.CrearParametro("@descripcion", estadoCliente.descripcion))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function Listar() As List(Of EstadoClienteDTO)
        Dim lsEstados As New List(Of EstadoClienteDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Estado_Cliente_Listar").Rows
            Dim oEstado As New EstadoClienteDTO With {.id = row("id_estado_cliente"),
                                              .descripcion = row("descripcion")
            }
            lsEstados.Add(oEstado)
        Next
        Return lsEstados
    End Function

    Public Function ObtenerPorId(id As Integer) As EstadoClienteDTO
        Try
            Dim ls As List(Of EstadoClienteDTO) = Listar()
            Dim resultado As New EstadoClienteDTO
            For Each obj As EstadoClienteDTO In ls
                If obj.id = id Then
                    resultado = obj
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
