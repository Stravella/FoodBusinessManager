Imports System.Web.HttpContext
Imports BLL
Imports Entidades
Public Class NewsletterBack
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cliente") IsNot Nothing Then
                Dim cliente As ClienteDTO = DirectCast(Session("cliente"), ClienteDTO)
                Dim puedeUsar As Boolean = False
                For Each permiso In cliente.usuario.perfil.Hijos
                    If permiso.PuedeUsar(Request.Url.AbsolutePath) = True Then
                        puedeUsar = True
                    End If
                Next
                If puedeUsar = False Then
                    Response.Redirect("/Home.aspx")
                End If
                CargarCategorias()
                CargarNewsletter()
                CargarEstados()
            Else
                Response.Redirect("/Home.aspx")
            End If
        End If

    End Sub

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If Current.Session("accion") = "Eliminar" Then
                Dim newsletter As NewsletterDTO = Current.Session("entidadModal")
                NewsletterBLL.ObtenerInstancia.Eliminar(newsletter)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                        .FechaHora = Now(),
                                        .usuario = usuarioLogeado,
                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 25}, 'Suceso: Eliminacion newsletter
                                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                                        .observaciones = "Se elimino el newsletter :" & newsletter.ID
                                }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Modificar" Then
                Dim newsletter As NewsletterDTO = Current.Session("entidadModal")
                NewsletterBLL.ObtenerInstancia.Modificar(newsletter)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                        .FechaHora = Now(),
                                        .usuario = usuarioLogeado,
                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 24}, 'Suceso: Modificacion newsletter
                                        .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Alta
                                        .observaciones = "Se modifico el newsletter :" & newsletter.ID
                                }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Enviar" Then
                Dim newsletter As NewsletterDTO = Current.Session("entidadModal")
                For Each subscriptor As SubscriptorDTO In newsletter.Categoria.subscriptores
                    Dim ActiveURL = "https://" & Request.Url.Host & ":" & Request.Url.Port & "/" & "DesubscribirNewsletter.aspx?idSubscriptor=" + Server.UrlEncode(CriptografiaBLL.ObtenerInstancia.EncriptarSimetrico(subscriptor.id))

                    GestorMailBLL.ObtenerInstancia.EnviarNewsletter(subscriptor.mail, "Food Business Manager : " & newsletter.Titulo, newsletter.Cuerpo, ActiveURL, Server.MapPath("\EmailTemplates\TemplateMail.html"), newsletter.Imagen.Img64)

                Next
                newsletter.Estado.ID = 2 'Estado enviado.
                NewsletterBLL.ObtenerInstancia.Modificar(newsletter)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                        .FechaHora = Now(),
                                        .usuario = usuarioLogeado,
                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 26}, 'Suceso: Envio newsletter
                                        .criticidad = New CriticidadDTO With {.id = 1}, 'Criticidad: Baja
                                        .observaciones = "Se envio el newsletter :" & newsletter.ID
                                }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            'Acá tengo que hidear el modal
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
            Response.Redirect("/NewsletterBack.aspx")
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
        gv_Newsletter.DataSource = ls
        gv_Newsletter.DataBind()
    End Sub

    Protected Sub CargarCategorias()
        Dim lsCategorias As New List(Of CategoriaDTO)
        lsCategorias = CategoriaBLL.ObtenerInstancia.Listar
        ddlCategoria.DataSource = lsCategorias
        ddlCategoria.DataBind()
    End Sub

    Protected Sub CargarEstados()
        Dim lsEstados As New List(Of EstadoDTO)
        lsEstados = EstadoBLL.ObtenerInstancia.Listar
        ddlEstado.DataSource = lsEstados
        ddlEstado.DataBind()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim fileName As String = "~/Imagenes/" + FileUpload1.FileName
            FileUpload1.SaveAs(Server.MapPath("~/Imagenes/" + FileUpload1.FileName))
            Dim imagen As New ImagenDTO With {.Img64 = fileName}

            Dim newsletter As New NewsletterDTO With {
                .Titulo = txtTitulo.Text,
                .Imagen = imagen,
                .Cuerpo = txtCuerpo.Text,
                .Categoria = New CategoriaDTO With {.id = ddlCategoria.SelectedValue},
                .Estado = New EstadoDTO With {.ID = 1} 'Por defecto, se crea en estado "Sin enviar".
                }
            NewsletterBLL.ObtenerInstancia.Agregar(newsletter)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                    .FechaHora = Now(),
                    .usuario = usuarioLogeado,
                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 23}, 'Suceso: Creacion Neswletter
                    .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Alta
                    .observaciones = "Se creo el Newsletter :" & newsletter.ID
            }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            Response.Redirect("/NewsletterBack.aspx")
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                    .FechaHora = Now(),
                    .usuario = usuarioLogeado,
                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 23}, 'Suceso: Creacion Neswletter
                    .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Alta
                    .observaciones = "Ocurrio un error creando un newsletter :" & ex.Message
            }
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub gv_Newsletter_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Newsletter.RowCommand
        Dim newsletters As List(Of NewsletterDTO) = NewsletterBLL.ObtenerInstancia.Listar
        Dim newsletter As New NewsletterDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        newsletter = newsletters.Find(Function(x) x.ID = id)
        Select Case e.CommandName
            Case "Editar"
                txtID.Text = newsletter.ID
                txtTitulo.Text = newsletter.Titulo
                txtCuerpo.Text = newsletter.Cuerpo
                For Each item As ListItem In ddlCategoria.Items
                    item.Selected = False
                    If item.Value = newsletter.Categoria.id Then
                        item.Selected = True
                    End If
                Next
                For Each item As ListItem In ddlEstado.Items
                    item.Selected = False
                    If item.Value = newsletter.Estado.ID Then
                        item.Selected = True
                    End If
                Next
                Image1.ImageUrl = newsletter.Imagen.Img64
                Image1.Visible = True
                FileUpload1.Visible = False
                lblFileSubido.Text = newsletter.Imagen.Img64
                lblFileSubido.Visible = True
                btnCambiarImagen.Visible = True
                btnAgregar.Visible = False
                btnModificar.Visible = True
                btnCancelar.Visible = True
                Current.Session("entidadModal") = newsletter
                Current.Session("accion") = "Modificar"
            Case "Borrar"
                Current.Session("entidadModal") = newsletter
                Current.Session("accion") = "Eliminar"
                MostrarModal("Eliminacion de newsletter " & newsletter.ID, "¿Está seguro que desea eliminar el newsletter " & newsletter.Titulo & "?",, True)
            Case "Enviar"
                Current.Session("entidadModal") = newsletter
                Current.Session("accion") = "Enviar"
                MostrarModal("Envio de newsletter " & newsletter.ID, "¿Está seguro que desea enviar el newsletter " & newsletter.Titulo & "?",, True)
        End Select

    End Sub

    Private Sub btnCambiarImagen_Click(sender As Object, e As EventArgs) Handles btnCambiarImagen.Click
        lblFileSubido.Visible = False
        btnCambiarImagen.Visible = False
        FileUpload1.Visible = True
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim newsletter As NewsletterDTO = NewsletterBLL.ObtenerInstancia.Obtener(txtID.Text)
            With newsletter
                .Titulo = txtTitulo.Text
                .Cuerpo = txtCuerpo.Text
                .Categoria = New CategoriaDTO With {.id = ddlCategoria.SelectedValue}
            End With
            If FileUpload1.HasFile = True Then
                Dim fileName As String = "~/Imagenes/" + FileUpload1.FileName
                FileUpload1.SaveAs(Server.MapPath("~/Imagenes/" + FileUpload1.FileName))
                newsletter.Imagen.Img64 = fileName
            Else
                newsletter.Imagen.Img64 = lblFileSubido.Text
            End If
            Current.Session("entidadModal") = newsletter
            Current.Session("accion") = "Modificar"
            MostrarModal("Modificacion de newsletter", "¿Está seguro que desea modificar el newsletter" & newsletter.Titulo & "?",, True)
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                    .FechaHora = Now(),
                    .usuario = usuarioLogeado,
                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 24}, 'Suceso: Creacion Neswletter
                    .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                    .observaciones = "Ocurrio un error modificando un newsletter :" & ex.Message
            }
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("/NewsletterBack.aspx")
    End Sub
End Class