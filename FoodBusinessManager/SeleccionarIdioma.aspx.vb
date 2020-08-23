Imports System.Globalization
Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class SeleccionarIdioma
    Inherits System.Web.UI.Page
    Dim usuarioLogeado As UsuarioDTO
#Region "Mensajes"
    Public Enum TipoAlerta
        Success
        Info
        Warning
        Danger
    End Enum

    Public Sub MostrarMensaje(mensaje As String, tipo As TipoAlerta)
        Dim panelMensaje As Panel = Master.FindControl("Mensaje")
        Dim labelMensaje As Label = panelMensaje.FindControl("labelMensaje")

        labelMensaje.Text = mensaje
        panelMensaje.CssClass = String.Format("alert alert-{0} alert-dismissable", tipo.ToString.ToLower())
        panelMensaje.Attributes.Add("role", "alert")
        panelMensaje.Visible = True
    End Sub
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            usuarioLogeado = Current.Session("Cliente")
            CargarIdiomas()
        End If
    End Sub

    Public Sub CargarIdiomas()
        Try
            'Cargar los idiomas en el drop down
            lstIdiomas.DataSource = IdiomaBLL.ObtenerInstancia.Listar()
            lstIdiomas.DataTextField = "nombre"
            lstIdiomas.DataValueField = "id_idioma"
            lstIdiomas.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_seleccionarIdioma_Click(sender As Object, e As EventArgs) Handles btn_seleccionarIdioma.Click
        Try

            Dim idiomaSeleccionado As New IdiomaDTO With {.id_idioma = lstIdiomas.SelectedValue}
            usuarioLogeado = Current.Session("cliente")
            If Not IsNothing(usuarioLogeado) Then
                'Modifico el idioma en el usuario
                usuarioLogeado.idioma = idiomaSeleccionado
                UsuarioBLL.ObtenerInstancia.ModificarUsuario(usuarioLogeado)
            Else
                Current.Session("Idioma") = IdiomaBLL.ObtenerInstancia.Obtener(idiomaSeleccionado)
            End If
            MostrarMensaje("Se m odifico el idioma", TipoAlerta.Success)
            Response.Redirect("SeleccionarIdioma.aspx")
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 3}, 'Suceso: Seleccion de idioma
                .ValorAnterior = usuarioLogeado.idioma.nombre,
                .NuevoValor = lstIdiomas.SelectedValue,
                .observaciones = "Error traduciendo pagina "
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            MostrarMensaje("Ocurrio un error al cambiar el idioma", TipoAlerta.Danger)
        End Try
    End Sub
End Class