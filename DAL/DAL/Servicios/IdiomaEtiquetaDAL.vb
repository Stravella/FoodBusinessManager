Imports Entidades
Imports System.Data.SqlClient

Public Class IdiomaEtiquetaDAL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As IdiomaEtiquetaDAL
    Public Shared Function ObtenerInstancia() As IdiomaEtiquetaDAL
        If _instancia Is Nothing Then
            _instancia = New IdiomaEtiquetaDAL
        End If
        Return _instancia
    End Function
#End Region


    '#Region "Etiquetas"
    '    'Obtener por id
    '    Public Function ObtenerEtiqueta(ByVal id_etiqueta As Integer) As String
    '        Dim ls As List(Of EtiquetaDTO) = Me.ListarEtiquetas()
    '        Dim oEtiqueta As EtiquetaDTO = ls.Find(Function(x) x.id_etiqueta = id_etiqueta)
    '        Return oEtiqueta.nombre
    '    End Function

    '    'Listar
    '    Public Function ListarEtiquetas() As List(Of EtiquetaDTO)
    '        Dim lsEtiquetas As New List(Of EtiquetaDTO)
    '        For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Idioma_Etiquetas_Listar").Rows
    '            Dim oEtiqueta As New EtiquetaDTO With {.id_etiqueta = row("id_etiqueta"),
    '                                                    .nombre = row("nombre")}
    '            lsEtiquetas.Add(oEtiqueta)
    '        Next
    '        Return lsEtiquetas
    '    End Function

    '#End Region

#Region "Idioma_etiquetas"
    'La tupla idioma_etiqueta son mis traducciones

    Public Function CrearParametros(ByVal Idioma As IdiomaDTO, ByVal IdiomaEtiqueta As IdiomaEtiquetaDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_idioma", Idioma.id_idioma))
                params.Add(.CrearParametro("@id_etiqueta", IdiomaEtiqueta.id_etiqueta))
                params.Add(.CrearParametro("@traduccion", IdiomaEtiqueta.traduccion))
            End With
        Catch ex As Exception

        End Try
        Return params
    End Function

    Public Sub CrearTraduccion(idioma As IdiomaDTO, idiomaEtiqueta As IdiomaEtiquetaDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Idioma_Etiquetas_Crear", Me.CrearParametros(idioma, idiomaEtiqueta))
        Catch ex As Exception

        End Try
    End Sub

    Public Function ObtenerTraducciones(Idioma As IdiomaDTO) As List(Of IdiomaEtiquetaDTO)
        Try
            Dim lsEtiquetas As New List(Of IdiomaEtiquetaDTO)
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id_idioma", Idioma.id_idioma))
            End With
            For Each row As DataRow In AccesoDAL.ObtenerInstancia.LeerBD("Idioma_Etiquetas_ListarPorIdioma", params).Rows
                Dim oEtiqueta As New IdiomaEtiquetaDTO With {.etiqueta = row("etiqueta"),
                                              .traduccion = row("traduccion")
                                              }
                lsEtiquetas.Add(oEtiqueta)
            Next
            Return lsEtiquetas
        Catch ex As Exception

        End Try
    End Function

    Public Sub Modificar(Idioma As IdiomaDTO, IdiomaEtiqueta As IdiomaEtiquetaDTO)
        AccesoDAL.ObtenerInstancia.EjecutarSP("Idioma_Etiquetas_Modificar", CrearParametros(Idioma, IdiomaEtiqueta))
    End Sub



#End Region


End Class
