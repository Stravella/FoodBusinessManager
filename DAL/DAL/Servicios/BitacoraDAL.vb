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

    Public Function ListarTodos(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing, Optional ByVal nroPagina As Integer = Nothing, Optional ByVal rowsPagina As Integer = Nothing) As List(Of BitacoraDTO)
        Try

            Dim query As String
            query = "SELECT * FROM Bitacora "
            If Not tipoSuceso Is Nothing Then
                query += "WHERE id_tipo_Suceso =" + tipoSuceso.id
            End If
            If Not Usuario Is Nothing Then
                query += " AND id_usuario =" + Usuario.id
            End If
            query += " AND fecha_Hora >=" + fechaDesde
            query += " AND fecha_Hora <=" + fechaHasta
            query += "ORDER BY fecha_Hora"
            query += " OFFSET (" + nroPagina + " * ISNULL(" + rowsPagina + ",10) ) ROWS"
            query += " FETCH NEXT ISNULL(" + rowsPagina + ",10) ROWS ONLY"

            Dim lsBitacoras As New List(Of BitacoraDTO)
            For Each Row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD(query).Rows
                Dim oBitacora As New BitacoraDTO With {
                    .id = Row("id_bitacora"),
                    .FechaHora = Row("fecha_Hora"),
                    .tipoSuceso = SucesoBitacoraDAL.ObtenerInstancia.Obtener(New SucesoBitacoraDTO With {.id = Row("id_tipo_Suceso")}),
                    .usuario = UsuarioDAL.ObtenerInstancia.ObtenerUsuario(New UsuarioDTO With {.id = Row("id_usuario")}),
                    .ValorAnterior = Row("valorAnterior"),
                    .NuevoValor = Row("valorNuevo"),
                    .observaciones = Row("observaciones"),
                    .DVH = Row("DVH")
                }
            Next
            Return lsBitacoras
        Catch ex As Exception

        End Try
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_bitacora", "Bitacora")
    End Function
End Class
