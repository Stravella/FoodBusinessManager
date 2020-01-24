Imports System.Globalization
Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class Idiomas
    Inherits System.Web.UI.Page
    Dim creando As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCulturas()
            InicializarGrilla()
        End If
    End Sub

    Private Sub CargarCulturas()
        Dim culturas = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures)
        For i As Integer = 1 To culturas.Length - 1
            Dim item As New ListItem(culturas(i).EnglishName, culturas(i).Name)
            Me.lstCulturas.Items.Add(item)
        Next
    End Sub

    Public Sub InicializarGrilla()
        'Dim IdiomaActual As Entidades.IdiomaDTO
        Dim idiomaActual As New IdiomaDTO With {.id_idioma = "es-AR"}
        idiomaActual = IdiomaBLL.ObtenerInstancia.Obtener(idiomaActual)
        Me.grillaTraduccion.DataSource = idiomaActual.ListaEtiquetas
        Me.grillaTraduccion.DataBind()
    End Sub

    Private Sub lstCulturas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCulturas.SelectedIndexChanged
        If IsPostBack Then
            Dim idiomaSeleccionado As New IdiomaDTO With {.id_idioma = lstCulturas.SelectedItem.Value}
            If IdiomaBLL.ObtenerInstancia.VerificarExistencia(idiomaSeleccionado) = True Then
                'Sí existe el idioma, cargo la grilla con las traducciones
                CargarGrilla(idiomaSeleccionado)
                Session.Add("CreandoIdioma", False)
                lbl_Respuesta.Text = "Usted está modificando un idioma existente"
                btn_ModificarIdioma.Text = "Modificar idioma"
            Else
                'Sí no existe cargo la grilla en es-AR
                Dim idiomaArgentino As New IdiomaDTO With {.id_idioma = "es-AR"}
                CargarGrilla(idiomaArgentino)
                Session.Add("CreandoIdioma", True)
                lbl_Respuesta.Text = "Usted está creando un nuevo idioma"
                btn_ModificarIdioma.Text = "Crear idioma"
            End If
        End If
    End Sub


    Public Sub CargarGrilla(unIdioma As IdiomaDTO)
        Dim idiomaActual As IdiomaDTO = IdiomaBLL.ObtenerInstancia.Obtener(unIdioma)
        Me.grillaTraduccion.DataSource = idiomaActual.ListaEtiquetas
        Me.grillaTraduccion.DataBind()
    End Sub

    Protected Sub btnModificarIdioma_Click(sender As Object, e As EventArgs) Handles btn_ModificarIdioma.Click
        'Obtengo la lista de etiquetas
        Dim lsTraducciones As New List(Of IdiomaEtiquetaDTO)
        For Each row As GridViewRow In grillaTraduccion.Rows
            Dim txtBox As TextBox = row.Cells(2).FindControl("txtTraduccion")
            Dim etiqueta As New IdiomaEtiquetaDTO With {.id_etiqueta = row.Cells(0).Text,
                                                    .etiqueta = row.Cells(1).Text,
                                                    .traduccion = txtBox.Text}
            lsTraducciones.Add(etiqueta)
        Next
        'Verifico si están completas -> Lo estoy haciendo con un required
        'Acciono si es una modificacion o una creacion
        If Session("CreandoIdioma") = True Then
            Dim idiomaNuevo As New IdiomaDTO With {.id_idioma = lstCulturas.SelectedValue,
                                                   .nombre = lstCulturas.SelectedItem.Text,
                                                   .ListaEtiquetas = lsTraducciones}
            IdiomaBLL.ObtenerInstancia.CrearIdioma(idiomaNuevo)
        Else 'Modificando
            Dim idiomaSeleccionado As New IdiomaDTO With {.id_idioma = lstCulturas.SelectedItem.Value}
            idiomaSeleccionado = IdiomaBLL.ObtenerInstancia.Obtener(idiomaSeleccionado)
            idiomaSeleccionado.ListaEtiquetas = lsTraducciones
            For Each traduccion As IdiomaEtiquetaDTO In idiomaSeleccionado.ListaEtiquetas
                IdiomaBLL.ObtenerInstancia.ModificarTraduccion(idiomaSeleccionado, traduccion)
            Next
        End If
        Session.Remove("CreandoIdioma")
    End Sub

End Class