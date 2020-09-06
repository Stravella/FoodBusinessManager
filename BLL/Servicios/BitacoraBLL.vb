Imports Entidades
Imports DAL
Imports System.Reflection

Public Class BitacoraBLL

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As BitacoraBLL
    Public Shared Function ObtenerInstancia() As BitacoraBLL
        If _instancia Is Nothing Then
            _instancia = New BitacoraBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub Agregar(unaBitacora As BitacoraDTO)
        Dim NomUsr As String = IIf(unaBitacora.usuario.username Is Nothing, "", unaBitacora.usuario.username)
        unaBitacora.id = BitacoraDAL.ObtenerInstancia.GetNextID
        BitacoraDAL.ObtenerInstancia.Agregar(unaBitacora)
    End Sub


    Public Function ListarSucesoBitacora() As List(Of SucesoBitacoraDTO)
        Return SucesoBitacoraDAL.ObtenerInstancia.Listar
    End Function

    Public Function ObtenerSucesoBitacora(unTipoSuceso As SucesoBitacoraDTO) As SucesoBitacoraDTO
        Return SucesoBitacoraDAL.ObtenerInstancia.ObtenerPorId(unTipoSuceso.id)
    End Function

    Public Function Listar(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing, Optional ByVal criticidad As CriticidadDTO = Nothing) As List(Of BitacoraDTO)
        Try
            Return BitacoraDAL.ObtenerInstancia.Listar(tipoSuceso, Usuario, fechaDesde, fechaHasta, criticidad)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class