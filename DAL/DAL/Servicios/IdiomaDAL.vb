Imports Entidades
Imports System.Data.SqlClient

Public Class IdiomaDAL
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

    Public Function CrearParametros(ByVal idioma As idiomaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", idioma.id_idioma))
                params.Add(.CrearParametro("@nombre", idioma.nombre))
                params.Add(.CrearParametro("@DVH", idioma.DVH))
            End With
        Catch ex As Exception

        End Try
        Return params
    End Function

    'GetNextID

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id_idioma", "idiomas")
    End Function

    'Agregar
    Public Sub Agregar(idioma As IdiomaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Idioma_Crear", CrearParametros(idioma))
        Catch ex As Exception

        End Try
    End Sub

    'Modificar

    'Listar
    Public Function ListarIdiomas() As List(Of IdiomaDTO)
        Dim lsIdiomas As New List(Of IdiomaDTO)
        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Idioma_listar").Rows
            Dim oIdioma As New IdiomaDTO With {.id_idioma = row("id_idioma"),
                                              .nombre = row("nombre")
                                              }

            lsIdiomas.Add(oIdioma)
        Next
        Return lsIdiomas
    End Function
    'Obtener

    Public Function ObtenerIdioma(Idioma As IdiomaDTO) As IdiomaDTO
        Dim ls As List(Of IdiomaDTO) = Me.ListarIdiomas()
        Dim oIdioma As IdiomaDTO = ls.Find(Function(x) x.id_idioma = Idioma.id_idioma)
        oIdioma.ListaEtiquetas = IdiomaEtiquetaDAL.ObtenerInstancia.ObtenerTraducciones(oIdioma)
        Return oIdioma
    End Function





End Class
