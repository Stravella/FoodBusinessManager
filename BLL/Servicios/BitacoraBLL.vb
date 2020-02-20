Imports Entidades
Imports DAL



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

    Public Function ListarTodos(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing, Optional ByVal nroPagina As Integer = Nothing, Optional ByVal rowsPagina As Integer = Nothing) As DataTable
        Return BitacoraDAL.ObtenerInstancia.ListarTodos(tipoSuceso, Usuario, fechaDesde, fechaHasta, rowsPagina, nroPagina)
    End Function

    Public Function ListarSucesoBitacora() As List(Of SucesoBitacoraDTO)
        Return SucesoBitacoraDAL.ObtenerInstancia.Listar
    End Function

    Public Function ObtenerSucesoBitacora(unTipoSuceso As SucesoBitacoraDTO) As SucesoBitacoraDTO
        Return SucesoBitacoraDAL.ObtenerInstancia.Obtener(unTipoSuceso)
    End Function

End Class
