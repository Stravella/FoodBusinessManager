Imports System.Data.SqlClient
Imports Entidades

Public Class BitacoraDAL
    Private Shared _instancia As BitacoraDAL
    Public Shared Function ObtenerInstancia() As BitacoraDAL
        If _instancia Is Nothing Then
            _instancia = New BitacoraDAL
        End If
        Return _instancia
    End Function

    Public Sub Agregar(Elemento As BitacoraDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia()
            params.Add(.CrearParametro("@ID", Elemento.id))
            params.Add(.CrearParametro("@fecha_hora", Elemento.FechaHora))
            params.Add(.CrearParametro("@id_usuario", Elemento.usuario.id))
            params.Add(.CrearParametro("@id_tipo_suceso", Elemento.tipoSuceso.id))
            params.Add(.CrearParametro("@valorAnterior", Elemento.ValorAnterior))
            params.Add(.CrearParametro("@valorNuevo", Elemento.NuevoValor))
            params.Add(.CrearParametro("@obs", Elemento.observaciones))
            params.Add(.CrearParametro("DVH", Elemento.DVH))
            .EjecutarSP("Bitacora_Crear", params)
        End With
    End Sub

    'Public Overloads Function ListarTodos(ByVal Usuario As UsuarioBE, ByVal Desde As DateTime, ByVal Hasta As DateTime) As List(Of BitacoraBE)
    '    UsuariosLst = MapperUsuario.GetInstancia.ListarUsuarios
    '    TipoSucesosLST = MapperTipoSuceso.ObtenerInstancia.ListarTodos
    '    Dim lsReturn As New List(Of BitacoraBE)
    '    Dim params As New List(Of SqlParameter)
    '    Dim dt As DataTable
    '    With AccesoDAL.GetInstancia()
    '        params.Add(.CrearParametro("@Desde", Desde))
    '        params.Add(.CrearParametro("@Hasta", Hasta))
    '        If Usuario IsNot Nothing Then
    '            params.Add(.CrearParametro("@usuario", Usuario.Usuario))
    '            dt = AccesoDAL.GetInstancia.LeerBD("Bitacora_Listar", params)
    '        Else
    '            Dim pars As New List(Of SqlParameter)
    '            pars.Add(params(0))
    '            pars.Add(params(1))
    '            dt = AccesoDAL.GetInstancia.LeerBD("Bitacora_ListarTodosUsers", pars)
    '        End If
    '    End With
    '    For Each r As DataRow In dt.Rows
    '        Dim oUsuario As UsuarioBE = Me.UsuariosLst.Find(Function(x) x.Usuario = r.Item("Usuario"))
    '        Dim oTipoSuceso As TipoSucesoBE = Me.TipoSucesosLST.Find(Function(x) x.ID = r.Item("TipoSuceso"))
    '        Dim Bita As New BitacoraBE With {.Usuario = oUsuario, .FechaHora = r.Item("FechaHora"),
    '                                         .TipoSuceso = oTipoSuceso, .Observaciones = r.Item("Observacion"), .ID = r("TipoSuceso")}
    '        lsReturn.Add(Bita)
    '    Next
    '    Return lsReturn
    'End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_bitacora", "Bitacora")
    End Function
End Class
