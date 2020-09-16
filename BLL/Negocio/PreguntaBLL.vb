Imports DAL
Imports Entidades
Public Class PreguntaBLL
#Region "Singleton"
    Private Shared _instancia As PreguntaBLL
    Public Shared Function ObtenerInstancia() As PreguntaBLL
        If _instancia Is Nothing Then
            _instancia = New PreguntaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(pregunta As PreguntaDTO)
        Try
            pregunta.id = PreguntaDAL.ObtenerInstancia.GetNextID
            PreguntaDAL.ObtenerInstancia.Agregar(pregunta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Modificar(pregunta As PreguntaDTO)
        Try
            PreguntaDAL.ObtenerInstancia.Modificar(pregunta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar(pregunta As PreguntaDTO)
        Try
            PreguntaDAL.ObtenerInstancia.Eliminar(pregunta)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Listar() As List(Of PreguntaDTO)
        Try
            Return PreguntaDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPorIdServicio(id As Integer) As List(Of PreguntaDTO)
        Try
            Dim ls As List(Of PreguntaDTO) = Listar()
            Dim resultado As New List(Of PreguntaDTO)
            For Each pregunta In ls
                If pregunta.id_servicio = id Then
                    resultado.Add(pregunta)
                End If
            Next
            Return resultado
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Obtener(id As Integer) As PreguntaDTO
        Try
            Return PreguntaDAL.ObtenerInstancia.Obtener(id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
