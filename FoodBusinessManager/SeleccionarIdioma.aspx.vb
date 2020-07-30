Imports System.Globalization
Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class SeleccionarIdioma
    Inherits System.Web.UI.Page
    Dim usuarioLogeado As UsuarioDTO
#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Danger,Success,Warning,Info
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
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
            MostrarMensaje("Se modifico el idioma", "Success")
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
            'TODO: Mostrar mensaje error
        End Try
    End Sub
End Class