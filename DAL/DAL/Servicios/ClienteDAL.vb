Imports Entidades
Imports System.Data.SqlClient
Imports System.Data

Public Class ClienteDAL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ClienteDAL
    Public Shared Function ObtenerInstancia() As ClienteDAL
        If _instancia Is Nothing Then
            _instancia = New ClienteDAL
        End If
        Return _instancia
    End Function
#End Region

    Public Function CrearParametros(ByVal cliente As ClienteDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_cliente", cliente.id))
                params.Add(.CrearParametro("@id_usuario", cliente.usuario.id))
                params.Add(.CrearParametro("@CUIT", cliente.CUIT))
                params.Add(.CrearParametro("@domicilio", cliente.domicilio))
                params.Add(.CrearParametro("@CP", cliente.domicilio))
                params.Add(.CrearParametro("@Localidad", cliente.localidad))
                params.Add(.CrearParametro("@Provincia", cliente.provincia))
                params.Add(.CrearParametro("@telefono", cliente.telefono))
                params.Add(.CrearParametro("@novedades", cliente.aceptaNewsletter))
                params.Add(.CrearParametro("@estado", cliente.estado.descripcion))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_cliente", "Clientes")
    End Function

    Public Sub AgregarCliente(ByVal cliente As ClienteDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Clientes_Crear", CrearParametros(cliente))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ModificarCliente(ByVal cliente As ClienteDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Clientes_Modificar", CrearParametros(cliente))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EliminarCliente(ByVal cliente As ClienteDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id_cliente", cliente.id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Clientes_Eliminar", CrearParametros(cliente))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListarClientes() As List(Of ClienteDTO)
        Dim lsClientes As New List(Of ClienteDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Clientes_Listar").Rows
            Dim oCliente As New ClienteDTO With {.id = row("id_cliente"),
                                              .usuario = UsuarioDAL.ObtenerInstancia.ObtenerPorId(row("id_usuario")),
                                              .CUIT = row("CUIT"),
                                              .domicilio = row("domicilio"),
                                              .CP = row("CP"),
                                              .localidad = row("Localidad"),
                                              .provincia = row("Provincia"),
                                              .aceptaNewsletter = row("novedades"),
                                              .estado = row("estado"),
                                              .telefono = row("telefono")
            }
            lsClientes.Add(oCliente)
        Next
        Return lsClientes
    End Function

    Public Function Obtener(ByVal cliente As ClienteDTO) As ClienteDTO
        Try
            Dim oCliente As New ClienteDTO
            For Each clie As ClienteDTO In ListarClientes()
                If clie.id = oCliente.id Then
                    oCliente = clie
                End If
            Next
            Return oCliente
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
