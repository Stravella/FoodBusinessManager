Imports System.Web.HttpContext
Imports BLL
Imports Entidades
Public Class NewsletterBack
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            'If Current.Session("accion") = "Eliminar" Then
            '    Dim catalogo As CatalogoDTO = Current.Session("entidadModal")
            '    CatalogoBLL.ObtenerInstancia.Eliminar(catalogo.id)
            '    Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            '    Dim bitacora As New BitacoraDTO With {
            '                        .FechaHora = Now(),
            '                        .usuario = usuarioLogeado,
            '                        .tipoSuceso = New SucesoBitacoraDTO With {.ID = 18}, 'Suceso: Eliminacion catalogo
            '                        .criticidad = New CriticidadDTO With {.ID = 3}, 'Criticidad: Alta
            '                        .observaciones = "Se elimino el catalogo :" & catalogo.id
            '                }
            '    BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            'End If
            'If Current.Session("accion") = "Modificar" Then
            '    Dim catalogo As CatalogoDTO = Current.Session("entidadModal")
            '    CatalogoBLL.ObtenerInstancia.Modificar(catalogo)
            '    'Actualizo el orden y el id de Catalogo
            '    For Each servicio As ServicioDTO In catalogo.servicios
            '        ServicioBLL.ObtenerInstancia.Modificar(servicio)
            '    Next
            '    Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            '    Dim bitacora As New BitacoraDTO With {
            '                .FechaHora = Now(),
            '                .usuario = usuarioLogeado,
            '                .tipoSuceso = New SucesoBitacoraDTO With {.ID = 19}, 'Suceso: Modificacion catalogo
            '                .criticidad = New CriticidadDTO With {.ID = 3}, 'Criticidad: Alta
            '                .observaciones = "Se modifico el catalogo :" & catalogo.id
            '        }
            '    BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            'End If
            'CargarCatalogos()
            'CargarServicios()
            'Acá tengo que hidear el modal
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)

        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try

    End Sub

    Public Sub MostrarModal(titulo As String, body As String, Optional grd As GridView = Nothing, Optional cancelar As Boolean = False)
        Dim panelMensaje As UpdatePanel = Master.FindControl("Modal")
        Dim tituloModal As Label = panelMensaje.FindControl("lblModalTitle")
        Dim bodyModal As Label = panelMensaje.FindControl("lblModalBody")
        If grd IsNot Nothing Then
            Dim grillaModal As GridView = panelMensaje.FindControl("grilla")
            grillaModal = grd
            grillaModal.Visible = True
        End If
        If cancelar = True Then
            Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
            btnCancelar.Visible = True
        End If
        tituloModal.Text = titulo
        bodyModal.Text = body

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#myModal').modal();", True)
        panelMensaje.Update()
    End Sub

#End Region

    Protected Sub CargarNewsletter()
        Dim ls As New List(Of NewsletterDTO)
        ls = NewsletterBLL.ObtenerInstancia.Listar

    End Sub




End Class