Imports Entidades
Imports DAL

Public Class IdiomaBLL

#Region "Singleton"
    Private Shared _instancia As IdiomaBLL
    Public Shared Function ObtenerInstancia() As IdiomaBLL
        If _instancia Is Nothing Then
            _instancia = New IdiomaBLL
        End If
        Return _instancia
    End Function
#End Region

    Public Sub CrearIdioma(idioma As IdiomaDTO)
        Try
            IdiomaDAL.ObtenerInstancia.Agregar(idioma)
            For Each Etiqueta As IdiomaEtiquetaDTO In idioma.ListaEtiquetas
                IdiomaEtiquetaDAL.ObtenerInstancia.CrearTraduccion(idioma, Etiqueta)
            Next
        Catch ex As Exception

        End Try
    End Sub
    'Obtiene el idioma y las traducciones
    Public Function Obtener(idioma As IdiomaDTO) As IdiomaDTO
        Return IdiomaDAL.ObtenerInstancia.ObtenerIdioma(idioma)
    End Function

    Public Function VerificarExistencia(idioma As IdiomaDTO) As Boolean
        Return IdiomaDAL.ObtenerInstancia.VerificarIdioma(idioma)
    End Function

    Public Function Listar() As List(Of IdiomaDTO)
        Try
            Return IdiomaDAL.ObtenerInstancia.ListarIdiomas
        Catch ex As Exception

        End Try
    End Function

#Region "Etiquetas"

    Public Sub CrearTraduccion(Idioma As IdiomaDTO, Etiqueta As IdiomaEtiquetaDTO)
        Try
            IdiomaEtiquetaDAL.ObtenerInstancia.CrearTraduccion(Idioma, Etiqueta)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ModificarTraduccion(Idioma As IdiomaDTO, Etiqueta As IdiomaEtiquetaDTO)
        Try
            IdiomaEtiquetaDAL.ObtenerInstancia.Modificar(Idioma, Etiqueta)
        Catch ex As Exception

        End Try
    End Sub

    Public Function ObtenerTraducciones(Idioma As IdiomaDTO) As List(Of IdiomaEtiquetaDTO)
        Try
            Return IdiomaEtiquetaDAL.ObtenerInstancia.ObtenerTraducciones(Idioma)
        Catch ex As Exception

        End Try
    End Function

#End Region



End Class
