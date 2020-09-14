Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class Catalogo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            CargarCatalogos()
            CargarServicios()
        End If
    End Sub

    Public Sub CargarCatalogos()
        Dim catalogos As List(Of CatalogoDTO) = CatalogoBLL.ObtenerInstancia.Listar
        gv_Catalogos.DataSource = catalogos
        gv_Catalogos.DataBind()
    End Sub

    Public Function CargarServicios()
        Try
            Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar
            grdServicios.DataSource = servicios
            grdServicios.DataBind()
        Catch ex As Exception

        End Try
    End Function

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If Current.Session("accion") = "Eliminar" Then
                Dim catalogo As CatalogoDTO = Current.Session("entidadModal")
                CatalogoBLL.ObtenerInstancia.Eliminar(catalogo.id)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                    .FechaHora = Now(),
                                    .usuario = usuarioLogeado,
                                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 18}, 'Suceso: Eliminacion catalogo
                                    .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                                    .observaciones = "Se elimino el catalogo :" & catalogo.id
                            }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Modificar" Then
                Dim catalogo As CatalogoDTO = Current.Session("entidadModal")
                CatalogoBLL.ObtenerInstancia.Modificar(catalogo)
                'Actualizo el orden y el id de Catalogo
                For Each servicio As ServicioDTO In catalogo.servicios
                    ServicioBLL.ObtenerInstancia.Modificar(servicio)
                Next
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                            .FechaHora = Now(),
                            .usuario = usuarioLogeado,
                            .tipoSuceso = New SucesoBitacoraDTO With {.id = 19}, 'Suceso: Modificacion catalogo
                            .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                            .observaciones = "Se modifico el catalogo :" & catalogo.id
                    }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        End If
        CargarCatalogos()
        CargarServicios()
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


#Region "Populado Gridview"
    Private Sub gv_Bitacora_DataBound(sender As Object, e As EventArgs) Handles gv_Catalogos.DataBound
        Try
            If Not IsNothing(gv_Catalogos.DataSource) Then
                If gv_Catalogos.DataSource > 0 Then
                    Dim ddlCantidadPaginas As DropDownList = CType(gv_Catalogos.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
                    Dim ddlTamañoPaginas As DropDownList = CType(gv_Catalogos.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
                    Dim txtTotalPaginas As Label = CType(gv_Catalogos.BottomPagerRow.Cells(0).FindControl("lblTotalPaginas"), Label)

                    ddlTamañoPaginas.ClearSelection()
                    ddlTamañoPaginas.Items.FindByValue(gv_Catalogos.PageSize).Selected = True

                    txtTotalPaginas.Text = gv_Catalogos.PageCount
                    For cnt As Integer = 0 To gv_Catalogos.PageCount - 1
                        Dim curr As Integer = cnt + 1
                        Dim item As New ListItem(curr.ToString())
                        If cnt = gv_Catalogos.PageIndex Then
                            item.Selected = True
                        End If
                        ddlCantidadPaginas.Items.Add(item)
                    Next cnt

                    gv_Catalogos.BottomPagerRow.Visible = True
                    gv_Catalogos.BottomPagerRow.CssClass = "table-bottom-dark"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub ddlCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Catalogos.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
            gv_Catalogos.SetPageIndex(ddl.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Catalogos.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
            gv_Catalogos.PageSize = ddl.SelectedValue
            CargarCatalogos()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gv_Servicios_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            CargarCatalogos()
            gv_Catalogos.PageIndex = e.NewPageIndex
            gv_Catalogos.DataBind()
        Catch ex As Exception

        End Try
    End Sub
#End Region





    Private Sub gv_Catalogos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Catalogos.RowCommand
        Dim catalogos As List(Of CatalogoDTO) = CatalogoBLL.ObtenerInstancia.Listar
        Dim catalogo As New CatalogoDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        catalogo = catalogos.Find(Function(x) x.id = id)

        If e.CommandName = "Editar" Then
            txtID.Text = catalogo.id
            txtNombre.Text = catalogo.nombre
            txtDescripcion.Text = catalogo.descripcion
            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
            CargarServicios()
            For Each servicio As ServicioDTO In catalogo.servicios
                For Each gvrow As GridViewRow In grdServicios.Rows
                    If Convert.ToInt16(gvrow.Cells(1).Text) = servicio.id Then
                        Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                        checkbox.Checked = True
                    End If
                Next
            Next
            Current.Session("catalogoModificando") = catalogo
        End If
        If e.CommandName = "Borrar" Then
            Current.Session("entidadModal") = catalogo
            Current.Session("accion") = "Eliminar"
            MostrarModal("Eliminacion de catalogo", "¿Está seguro que desea eliminar el catalogo" & catalogo.id & "?",, True)
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim catalogo As New CatalogoDTO With {
            .nombre = txtNombre.Text,
            .descripcion = txtDescripcion.Text
        }
        Dim lsServicios As New List(Of ServicioDTO)
        For Each gvrow As GridViewRow In grdServicios.Rows
            Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
            If checkbox.Checked = True Then
                Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(gvrow.Cells(1).Text)
                lsServicios.Add(servicio)
            End If
        Next
        catalogo.servicios = lsServicios
        CatalogoBLL.ObtenerInstancia.Agregar(catalogo)
        For Each servicio In catalogo.servicios
            servicio.id = catalogo.id
            'Agregar el orden
            ServicioBLL.ObtenerInstancia.Modificar(servicio)
        Next
        CargarCatalogos()
        CargarServicios()
        Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
        Dim bitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 17}, 'Suceso: Creacion Catalogo
                .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                .observaciones = "Se creo el catalogo :" & catalogo.nombre
        }
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtID.Text = ""
        txtNombre.Text = ""
        txtDescripcion.Text = ""
        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnCancelar.Visible = False
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim catalogo As CatalogoDTO = Current.Session("catalogoModificando")
            catalogo.nombre = txtNombre.Text
            catalogo.descripcion = txtDescripcion.Text
            Dim lsServicios As New List(Of ServicioDTO)
            For Each gvrow As GridViewRow In grdServicios.Rows
                Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                If checkbox.Checked = True Then
                    Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(gvrow.Cells(1).Text)
                    servicio.id_catalogo = catalogo.id
                    servicio.orden_catalogo = gvrow.Cells(6).Text
                    lsServicios.Add(servicio)
                End If
            Next
            catalogo.servicios = lsServicios
            Current.Session("entidadModal") = catalogo
            Current.Session("accion") = "Modificar"
            MostrarModal("Modificacion de catalogo", "¿Está seguro que desea modificar el catalogo" & catalogo.id & "?",, True)
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub grdServicios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdServicios.RowCommand
        Dim id As Integer = Integer.Parse(e.CommandArgument)
        For Each gvrow As GridViewRow In grdServicios.Rows
            If Convert.ToInt16(gvrow.Cells(1).Text) = id Then
                Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                If checkbox.Checked = True Then
                    'Tengo que habilitar para edicion unicamente el campo que me interesa.
                    Dim lbl As Label = gvrow.FindControl("lblOrden")
                    Dim txt As TextBox = gvrow.FindControl("txtOrden")
                    lbl.Visible = False
                    txt.Visible = True
                Else
                    MostrarModal("Error", "Lo siento! No puede modificar el orden de servicios que no se encuentran en este catálogo.",, True)
                End If
            End If
        Next
        'veo que este chekeado, y modifico el orden
    End Sub


End Class