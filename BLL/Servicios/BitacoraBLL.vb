Imports Entidades
Imports DAL
Imports System.Reflection
Imports Newtonsoft.Json



Public Class BitacoraBLL
    'En esta clase uso una libreria JSON.net para serializar y deserializar en JSON. 

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

    Public Sub AgregarError(unaBitacora As BitacoraDTO, unaBitacoraError As BitacoraErroresDTO)
        unaBitacoraError.id = unaBitacora.id
        unaBitacoraError.id_bitacora_error = BitacoraDAL.ObtenerInstancia.GetNextErrorID
        BitacoraDAL.ObtenerInstancia.Agregar(unaBitacoraError)
    End Sub

    Public Function ListarTodos(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing) As List(Of BitacoraDTO)
        Return BitacoraDAL.ObtenerInstancia.ListarTodos(tipoSuceso, Usuario, fechaDesde, fechaHasta)
    End Function

    Public Function ListarErrores(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing) As List(Of BitacoraErroresDTO)
        Try
            Return BitacoraDAL.ObtenerInstancia.ListarErrores(tipoSuceso, Usuario, fechaDesde, fechaHasta)
        Catch ex As Exception

        End Try
    End Function


    Public Function ListarSucesoBitacora() As List(Of SucesoBitacoraDTO)
        Return SucesoBitacoraDAL.ObtenerInstancia.Listar
    End Function

    Public Function ObtenerSucesoBitacora(unTipoSuceso As SucesoBitacoraDTO) As SucesoBitacoraDTO
        Return SucesoBitacoraDAL.ObtenerInstancia.ObtenerPorId(unTipoSuceso.id)
    End Function

    Public Function ObtenerCantidadRegistros(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing) As Integer
        Return BitacoraDAL.ObtenerInstancia.ObtenerCantidadRegistros(tipoSuceso, Usuario, fechaDesde, fechaHasta)
    End Function

    Public Function ObtenerUltimaBitacora() As BitacoraDTO
        Try
            Return BitacoraDAL.ObtenerInstancia.ObtenerUltimaBitacora
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CompararObjetos(ByVal Objeto As Object, ByVal ObjetoAComparar As Object) As String
        'El String siempre va a devolver las diferencias entre el Objeto y el Objeto a Comparar
        Try
            Dim Diferencias As String
            Dim propiedades() As PropertyInfo = Objeto.GetType.GetProperties
            Dim propiedad As PropertyInfo
            Dim valor1 As Object
            Dim valor2 As Object
            For Each propiedad In propiedades
                valor1 = propiedad.GetValue(Objeto, Nothing)
                valor2 = propiedad.GetValue(ObjetoAComparar, Nothing)
                If valor1 <> valor2 Then
                    Diferencias += propiedad.Name.ToString & ": " & valor1.ToString & "; "
                End If
            Next
            Return Diferencias
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
