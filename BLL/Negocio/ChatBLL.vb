Imports Entidades
Imports DAL

Public Class ChatBLL

#Region "Singleton"
    Private Shared _instancia As ChatBLL
    Public Shared Function ObtenerInstancia() As ChatBLL
        If _instancia Is Nothing Then
            _instancia = New ChatBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Crear(chat As ChatSesionDTO)
        Try
            chat.id = ChatDAL.ObtenerInstancia.GetNextID
            ChatDAL.ObtenerInstancia.Agregar(chat)
            For Each mensaje In chat.mensajes
                chat.id = ChatDAL.ObtenerInstancia.GetNextMensajeID
                ChatDAL.ObtenerInstancia.AgregarMensaje(mensaje, chat)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(chat As ChatSesionDTO)
        Try
            ChatDAL.ObtenerInstancia.Modificar(chat)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function Listar() As List(Of ChatSesionDTO)
        Try
            Return ChatDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(id As Integer) As ChatSesionDTO
        Try
            Return ChatDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub CrearMensaje(mensaje As ChatMensajeDTO, chat As ChatSesionDTO)
        Try
            chat.id = ChatDAL.ObtenerInstancia.GetNextMensajeID
            ChatDAL.ObtenerInstancia.AgregarMensaje(mensaje, chat)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class
