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

    Public Function CrearParametros(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As DateTime = Nothing, Optional ByVal fechaHasta As DateTime = Nothing, Optional ByVal nroPagina As Integer = Nothing, Optional ByVal rowsPagina As Integer = Nothing) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_usuario", Usuario.id))
                params.Add(.CrearParametro("@id_tipo_suceso", tipoSuceso.id))
                params.Add(.CrearParametro("@fechaInicial", fechaDesde))
                params.Add(.CrearParametro("@fechaFinal", fechaHasta))
                params.Add(.CrearParametro("@nroPagina", nroPagina))
                params.Add(.CrearParametro("@rowsPagina", rowsPagina))
            End With
        Catch ex As Exception

        End Try
        Return params
    End Function

    Public Function ListarTodos(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing, Optional ByVal nroPagina As Integer = Nothing, Optional ByVal rowsPagina As Integer = Nothing) As DataTable
        Try

            Dim query As String

            query = "SELECT * FROM Bitacora"
            query += " WHERE fecha_Hora between isnull(CONVERT(date,@fechaInicial,103), '2019-01-01') and isnull(CONVERT(date,@fechaFinal,103), '2021-01-01')"
            query += " AND id_tipo_Suceso = isnull(@id_tipo_suceso, id_tipo_suceso)"
            query += " AND id_usuario = isnull(@id_usuario, id_usuario)"
            query += " ORDER BY fecha_Hora"
            query += " OFFSET ( isnull(@nroPagina, 0) * isnull(@rowsPagina, 10) ) ROWS"
            query += " FETCH NEXT isnull(@rowsPagina, 10) ROWS ONLY"

            Dim params As List(Of SqlParameter) = CrearParametros(tipoSuceso, Usuario, fechaDesde, fechaHasta, nroPagina, rowsPagina)

            Dim dt As DataTable = AccesoDAL.ObtenerInstancia.LeerBDconParams(query, params)

            Return dt
        Catch ex As Exception

        End Try
    End Function


    Public Function ObtenerCantidadRegistros(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing) As Integer
        Try
            Dim query As String

            query = "SELECT count(id_Bitacora) FROM Bitacora"
            query += " WHERE fecha_Hora between isnull(CONVERT(date,@fechaInicial,103), '2019-01-01') and isnull(CONVERT(date,@fechaFinal,103), '2021-01-01')"
            query += " AND id_tipo_Suceso = isnull(@id_tipo_suceso, id_tipo_suceso)"
            query += " AND id_usuario = isnull(@id_usuario, id_usuario)"

            Dim params As List(Of SqlParameter) = CrearParametros(tipoSuceso, Usuario, fechaDesde, fechaHasta)

            Dim dt As DataTable = AccesoDAL.ObtenerInstancia.LeerBDconParams(query, params)

            dt = AccesoDAL.ObtenerInstancia.LeerBDconParams(query, params)

            Return dt(0)(0)
        Catch ex As Exception

        End Try
    End Function


    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_bitacora", "Bitacora")
    End Function
End Class
