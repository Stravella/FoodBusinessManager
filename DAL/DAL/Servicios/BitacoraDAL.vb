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

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_bitacora", "Bitacora")
    End Function


    Public Function CrearParametros(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As DateTime = Nothing, Optional ByVal fechaHasta As DateTime = Nothing, Optional ByVal nroPagina As Integer = Nothing, Optional ByVal rowsPagina As Integer = Nothing, Optional ByVal criticidad As CriticidadDTO = Nothing) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_usuario", Usuario.id))
                params.Add(.CrearParametro("@id_tipo_suceso", tipoSuceso.id))
                params.Add(.CrearParametro("@fechaInicial", fechaDesde))
                params.Add(.CrearParametro("@fechaFinal", fechaHasta))
                params.Add(.CrearParametro("@nroPagina", nroPagina))
                params.Add(.CrearParametro("@rowsPagina", rowsPagina))
                params.Add(.CrearParametro("@criticidad", criticidad.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Sub Agregar(Elemento As BitacoraDTO)
        Dim params As New List(Of SqlParameter)
        With AccesoDAL.ObtenerInstancia()
            params.Add(.CrearParametro("@ID", Elemento.id))
            params.Add(.CrearParametro("@fecha_hora", Elemento.FechaHora))
            params.Add(.CrearParametro("@id_usuario", Elemento.usuario.id))
            params.Add(.CrearParametro("@id_tipo_suceso", Elemento.tipoSuceso.id))
            params.Add(.CrearParametro("@obs", Elemento.observaciones))
            params.Add(.CrearParametro("@criticidad", Elemento.criticidad.id))
            .EjecutarSP("Bitacora_Crear", params)
        End With
    End Sub

    Public Function Listar(Optional ByVal tipoSuceso As Entidades.SucesoBitacoraDTO = Nothing, Optional ByVal Usuario As Entidades.UsuarioDTO = Nothing, Optional ByVal fechaDesde As Date = Nothing, Optional ByVal fechaHasta As Date = Nothing, Optional ByVal criticidad As CriticidadDTO = Nothing) As List(Of BitacoraDTO)
        Try
            Dim params As List(Of SqlParameter) = CrearParametros(tipoSuceso, Usuario, fechaDesde, fechaHasta, criticidad.id)
            Dim ls As New List(Of BitacoraDTO)
            For Each Row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Bitacora_Listar", params).Rows
                Dim oBitacora As New BitacoraDTO With {.id = Row("id_Bitacora"),
                                                       .FechaHora = Row("fecha_Hora"),
                                                       .usuario = UsuarioDAL.ObtenerInstancia.ObtenerPorId(Row("id_usuario")),
                                                       .tipoSuceso = SucesoBitacoraDAL.ObtenerInstancia.ObtenerPorId(Row("id_tipo_suceso")),
                                                       .observaciones = Row("observaciones"),
                                                       .criticidad = CriticidadDAL.ObtenerInstancia.ObtenerPorId(Row("id_criticidad"))
                                                       }
                ls.Add(oBitacora)
            Next
            Return ls
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
