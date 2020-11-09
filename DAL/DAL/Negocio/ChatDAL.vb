
Imports Entidades
Imports System.Data.SqlClient
Imports System.Data
Public Class ChatDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ChatDAL
    Public Shared Function ObtenerInstancia() As ChatDAL
        If _instancia Is Nothing Then
            _instancia = New ChatDAL
        End If
        Return _instancia
    End Function
#End Region


    Public Function CrearParametros(ByVal chatSesion As ChatSesionDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", chatSesion.id))
                params.Add(.CrearParametro("@fecha_inicio", chatSesion.fechaInicio))
                params.Add(.CrearParametro("@fecha_fin", chatSesion.fechaFin))
                params.Add(.CrearParametro("@id_cliente", chatSesion.cliente.id))
                params.Add(.CrearParametro("@id_usuario_atendio", chatSesion.usuarioAtendio.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Chat_sesion")
    End Function

    'En el alta de la sesion no seteo la fecha de fin
    Public Sub Agregar(ByVal chatSesion As ChatSesionDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", chatSesion.id))
                params.Add(.CrearParametro("@fecha_inicio", chatSesion.fechaInicio))
                params.Add(.CrearParametro("@id_cliente", chatSesion.cliente.id))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Chat_sesion_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(ByVal chatSesion As ChatSesionDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Chat_sesion_modificar", CrearParametros(chatSesion))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of ChatSesionDTO)
        Dim ls As New List(Of ChatSesionDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Chat_sesion_listar").Rows
            Dim chatSesion As New ChatSesionDTO With {.id = row("id"),
                                              .fechaInicio = row("fecha_inicio"),
                                              .fechaFin = row("fecha_fin"),
                                              .cliente = ClienteDAL.ObtenerInstancia.Obtener(New ClienteDTO With {.id = row("id_cliente")}),
                                              .usuarioAtendio = UsuarioDAL.ObtenerInstancia.ObtenerPorId(row("id_usuario_atendio")),
                                              .mensajes = ListarPorSesion(row("id"))
            }
            ls.Add(chatSesion)
        Next
        Return ls
    End Function


    Public Function Obtener(id As Integer) As ChatSesionDTO
        Try
            Dim ls As New List(Of ChatSesionDTO)
            Dim resultado As New ChatSesionDTO
            ls = Listar()
            For Each chat In ls
                If chat.id = id Then
                    resultado = chat
                    Exit For
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerChatActivo(cliente As ClienteDTO) As ChatSesionDTO
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_cliente", cliente.id))
            End With
            Dim chat As New ChatSesionDTO
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Chat_obtenerActivo", params).Rows
                With chat
                    .id = row("id")
                    .fechaInicio = row("fecha_inicio")
                    .fechaFin = row("fecha_fin")
                    .cliente = ClienteDAL.ObtenerInstancia.Obtener(New ClienteDTO With {.id = row("id_cliente")})
                    .usuarioAtendio = UsuarioDAL.ObtenerInstancia.ObtenerPorId(row("id_usuario_atendio"))
                    .mensajes = ListarPorSesion(row("id"))
                End With
                Exit For
            Next
            Return chat
        Catch ex As Exception
            Throw ex
        End Try
    End Function



#Region "Mensajes"

    Public Function GetNextMensajeID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Chat_mensajes")
    End Function
    Public Sub AgregarMensaje(ByVal mensaje As ChatMensajeDTO, sesion As ChatSesionDTO)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", mensaje.id))
                params.Add(.CrearParametro("@username", mensaje.username))
                params.Add(.CrearParametro("@fecha", mensaje.fecha))
                params.Add(.CrearParametro("@mensaje", mensaje.mensaje))
                params.Add(.CrearParametro("@id_sesion", sesion.id))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Chat_mensaje_crear", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ListarPorSesion(id As Integer) As List(Of ChatMensajeDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia()
            params.Add(.CrearParametro("@id_sesion", id))
        End With
        Dim ls As New List(Of ChatMensajeDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Chat_mensaje_listarPorSesion", params).Rows
            Dim mensaje As New ChatMensajeDTO With {.id = row("id"),
                                              .fecha = row("fecha"),
                                              .mensaje = row("mensaje"),
                                              .username = row("username")
            }
            ls.Add(mensaje)
        Next
        Return ls
    End Function

#End Region


End Class
