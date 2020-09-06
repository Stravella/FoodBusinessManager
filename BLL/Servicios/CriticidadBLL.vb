Imports Entidades
Imports DAL
Imports System.Reflection
Public Class CriticidadBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As CriticidadBLL
    Public Shared Function ObtenerInstancia() As CriticidadBLL
        If _instancia Is Nothing Then
            _instancia = New CriticidadBLL
        End If
        Return _instancia
    End Function
#End Region


    Public Function Listar() As List(Of CriticidadDTO)
        Try
            Return CriticidadDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorId(id As Integer) As CriticidadDTO
        Try
            Dim ls As List(Of CriticidadDTO) = Listar()
            Dim resultado As CriticidadDTO
            For Each obj As CriticidadDTO In ls
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
