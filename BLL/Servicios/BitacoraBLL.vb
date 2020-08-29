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


End Class