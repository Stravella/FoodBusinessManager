Imports BLL
Imports Entidades
Imports System.Web.HttpContext

Public Class NewsletterSubscribir
    Inherits System.Web.UI.Page

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
            Response.Redirect("/NewsletterSubscribir.aspx")
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            CargarCategorias()
        End If
    End Sub

    Protected Sub CargarCategorias()
        Dim categorias As New List(Of CategoriaDTO)
        categorias = CategoriaBLL.ObtenerInstancia.Listar()
        lstCategorias.DataSource = categorias
        lstCategorias.DataBind()
    End Sub

    Private Sub btnSubscribrse_Click(sender As Object, e As EventArgs) Handles btnSubscribrse.Click
        Try
            Dim subscriptor As New SubscriptorDTO With {.mail = txtSubscripcion.Text}
            SubscriptorBLL.ObtenerInstancia.Agregar(subscriptor)
            Dim listaIntereses As New List(Of CategoriaDTO)
            For Each item As ListItem In lstCategorias.Items
                If item.Selected = True Then
                    Dim categoria As New CategoriaDTO With {
                        .id = item.Value,
                        .nombre = item.Text
                    }
                    SubscriptorBLL.ObtenerInstancia.Subscribir(subscriptor.id, categoria.id)
                End If
            Next
            MostrarModal("Felicitaciones!", "Se ha subscripto correctamente al newsletter!",, True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

End Class