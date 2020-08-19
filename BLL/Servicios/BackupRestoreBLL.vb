Imports Entidades
Imports DAL
Public Class BackupRestoreBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As IdiomaDAL
    Public Shared Function ObtenerInstancia() As IdiomaDAL
        If _instancia Is Nothing Then
            _instancia = New IdiomaDAL
        End If
        Return _instancia
    End Function
#End Region



End Class
