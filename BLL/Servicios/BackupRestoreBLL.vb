Imports Entidades
Imports DAL
Public Class BackupRestoreBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As BackupRestoreBLL
    Public Shared Function ObtenerInstancia() As BackupRestoreBLL
        If _instancia Is Nothing Then
            _instancia = New BackupRestoreBLL
        End If
        Return _instancia
    End Function
#End Region



End Class
