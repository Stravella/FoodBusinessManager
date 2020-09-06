Imports Entidades
Imports DAL

Public Class ClienteBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As ClienteBLL
    Public Shared Function ObtenerInstancia() As ClienteBLL
        If _instancia Is Nothing Then
            _instancia = New ClienteBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(cliente As ClienteDTO)
        Try
            UsuarioBLL.ObtenerInstancia.AgregarUsuario(cliente.usuario)
            ClienteDAL.ObtenerInstancia.AgregarCliente(cliente)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(cliente As ClienteDTO)
        Try
            ClienteDAL.ObtenerInstancia.ModificarCliente(cliente)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of ClienteDTO)
        Try
            Return ClienteDAL.ObtenerInstancia.ListarClientes
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Obtener(cliente As ClienteDTO) As ClienteDTO
        Try
            Return ClienteDAL.ObtenerInstancia.Obtener(cliente)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorUsuario(usuario As UsuarioDTO) As ClienteDTO
        Try
            Dim ls As List(Of ClienteDTO) = Listar()
            Dim resultado As New ClienteDTO
            For Each obj As ClienteDTO In ls
                If obj.usuario.id = usuario.id Then
                    resultado = obj
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
