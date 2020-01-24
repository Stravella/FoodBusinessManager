Imports System.Globalization
Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class SeleccionarIdioma
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Cargar el idioma del usuario
        Dim usuarioLogeado As UsuarioDTO = Session("usuario")
        'mostrar el idioma en el label
        lbl_IdiomaActual.Text = "Su idioma actual es : " & usuarioLogeado.idioma.nombre
        'Cargar los idiomas en el drop down
        drop_ListaIdiomas.DataSource = IdiomaBLL.ObtenerInstancia.Listar()
        drop_ListaIdiomas.DataBind()
    End Sub

    'Modifico el idioma
    Private Sub btn_AplicarIdioma_Click(sender As Object, e As EventArgs) Handles btn_AplicarIdioma.Click

    End Sub

End Class