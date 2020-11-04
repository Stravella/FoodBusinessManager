Imports DAL
Imports Entidades

Public Class BDBLL
#Region "Singleton"
    Private Shared _instancia As BDBLL
    Public Shared Function ObtenerInstancia() As BDBLL
        If _instancia Is Nothing Then
            _instancia = New BDBLL
        End If
        Return _instancia
    End Function
#End Region


    Public Function Listar() As List(Of BackupDTO)
        Try
            Return BDDAL.ObtenerInstancia.Listar
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Obtener(id_backup As Integer) As BackupDTO
        Try
            Return BDDAL.ObtenerInstancia.Obtener(id_backup)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorNombre(Nombre As String) As BackupDTO
        Try
            Return BDDAL.ObtenerInstancia.ObtenerPorNombre(Nombre)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Agregar(backup As BackupDTO)
        Try
            backup.ID = BDDAL.ObtenerInstancia.GetNextID
            BDDAL.ObtenerInstancia.Agregar(backup)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Eliminar(id_backup As Integer)
        Try
            BDDAL.ObtenerInstancia.Eliminar(id_backup)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Restaurar(backup As BackupDTO)
        Try
            BDDAL.ObtenerInstancia.RestaurarBackup(backup)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Generar(backup As BackupDTO)
        Try
            BDDAL.ObtenerInstancia.GenerarBackup(backup)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class
