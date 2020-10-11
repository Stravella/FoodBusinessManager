Imports Entidades
Imports DAL

Public Class SubscriptorBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As SubscriptorBLL
    Public Shared Function ObtenerInstancia() As SubscriptorBLL
        If _instancia Is Nothing Then
            _instancia = New SubscriptorBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(subscriptor As SubscriptorDTO)
        Try
            subscriptor.id = SubscripcionesDAL.ObtenerInstancia.GetNextID
            SubscripcionesDAL.ObtenerInstancia.Agregar(subscriptor)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Public Sub Eliminar(subscriptor As SubscriptorDTO)
        Try
            SubscripcionesDAL.ObtenerInstancia.Eliminar(subscriptor)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Subscribir(id_subscriptor As Integer, id_categoria As Integer)
        Try
            SubscripcionesDAL.ObtenerInstancia.subscribir(id_subscriptor, id_categoria)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Desubscribir(subscriptor As SubscriptorDTO)
        Try
            SubscripcionesDAL.ObtenerInstancia.Desubscribir(subscriptor)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




End Class
