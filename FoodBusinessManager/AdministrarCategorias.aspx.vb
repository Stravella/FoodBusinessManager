Imports Entidades
Imports System.Web.HttpContext
Imports BLL

Public Class AdministrarCategorias
    Inherits System.Web.UI.Page

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If Current.Session("accion") = "Eliminar" Then
                Dim categoria As CategoriaDTO = Current.Session("entidadModal")
                CategoriaBLL.ObtenerInstancia.Eliminar(categoria)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                        .FechaHora = Now(),
                                        .usuario = usuarioLogeado,
                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 22}, 'Suceso: Eliminacion categoria
                                        .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: media
                                        .observaciones = "Se elimino la categoria :" & categoria.nombre
                               }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Modificar" Then
                Dim categoria As CategoriaDTO = Current.Session("entidadModal")
                CategoriaBLL.ObtenerInstancia.Modificar(categoria)
                'Actualizo el orden y el id de Catalogo                
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                .FechaHora = Now(),
                                .usuario = usuarioLogeado,
                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 23}, 'Suceso: Modificacion categoria
                                .criticidad = New CriticidadDTO With {.id = 2}, 'Criticidad: Alta
                                .observaciones = "Se modifico la categoria : " & categoria.id
                        }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
            Response.Redirect("/AdministrarCategorias.aspx")
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
            If Not IsPostBack() Then
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
                Else
                    Response.Redirect("/Home.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub CargarCategorias()
        Dim categorias As New List(Of CategoriaDTO)
        categorias = CategoriaBLL.ObtenerInstancia.Listar()
        gv_Categorias.DataSource = categorias
        gv_Categorias.DataBind()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim categoria As New CategoriaDTO With {.nombre = txtNombre.Text}
            CategoriaBLL.ObtenerInstancia.Crear(categoria)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                    .FechaHora = Now(),
                    .usuario = usuarioLogeado,
                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 20}, 'Suceso: Creacion Categoria
                    .criticidad = New CriticidadDTO With {.id = 1}, 'Criticidad: Baja
                    .observaciones = "Se creo la categoria : " & categoria.nombre
            }
            Response.Redirect("/AdministrarCategorias.aspx")
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub gv_Categorias_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Categorias.RowCommand
        Dim categoria As New CategoriaDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        categoria = CategoriaBLL.ObtenerInstancia.Obtener(id)
        If e.CommandName = "Editar" Then
            txtID.Text = categoria.id
            txtNombre.Text = categoria.nombre
            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
        ElseIf e.CommandName = "Borrar" Then
            'Busco si tiene newsletter
            Dim newsletters As New List(Of NewsletterDTO)
            newsletters = NewsletterBLL.ObtenerInstancia.ListarPorCategoria(categoria.id)
            If newsletters.Count = 0 Then
                Current.Session("entidadModal") = categoria
                Current.Session("accion") = "Eliminar"
                MostrarModal("Borrado categoria : " & categoria.id, "¿Está seguro que desea borrar la categoria " & categoria.nombre & "?",, True)
            Else 'No puedo borrar is tiene categorias
                MostrarModal("Error", "La categoria tiene newsletters asociados. Si desea borrar la categoria, debe eliminar los newsletters primero",, True)
            End If
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim categoria As New CategoriaDTO With {.nombre = txtNombre.Text, .id = txtID.Text}
            Current.Session("entidadModal") = categoria
            Current.Session("accion") = "Modificar"
            MostrarModal("Modificacion categoria : " & categoria.id, "¿Está seguro que desea modifica la categoria :" & categoria.nombre & "?",, True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtID.Text = ""
        txtNombre.Text = ""
        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnCancelar.Visible = False
    End Sub


End Class