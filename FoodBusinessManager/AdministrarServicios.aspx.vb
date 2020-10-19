Imports System.Web.HttpContext
Imports Entidades
Imports BLL

Public Class AdministrarServicios
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
                Dim servicio As ServicioDTO = Current.Session("entidadModal")
                ServicioBLL.ObtenerInstancia.Eliminar(servicio.id)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                .FechaHora = Now(),
                                .usuario = usuarioLogeado,
                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 15}, 'Suceso: Eliminacion servicio
                                .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                                .observaciones = "Se elimino el servicio :" & servicio.id
                        }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            If Current.Session("accion") = "Modificar" Then
                Dim servicio As ServicioDTO = Current.Session("entidadModal")
                ServicioBLL.ObtenerInstancia.Modificar(servicio)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                                .FechaHora = Now(),
                                .usuario = usuarioLogeado,
                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 16}, 'Suceso: Eliminacion servicio
                                .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                                .observaciones = "Se modifico el servicio :" & servicio.id
                        }
                BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            End If
            CargarCaracteristicas()
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            CargarServicios()
            CargarCaracteristicas()
        End If
    End Sub

    Public Function CargarServicios()
        Try
            Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar
            gv_Servicios.DataSource = servicios
            gv_Servicios.DataBind()
        Catch ex As Exception

        End Try
    End Function


    Public Function CargarCaracteristicas()
        Dim caracteristicas As List(Of CaracteristicaDTO) = CaracteristicaBLL.ObtenerInstancia.Listar
        grdCaracteristicas.DataSource = caracteristicas
        grdCaracteristicas.DataBind()
    End Function

    Private Sub gv_Bitacora_DataBound(sender As Object, e As EventArgs) Handles gv_Servicios.DataBound
        Try
            If Not IsNothing(gv_Servicios.DataSource) Then
                If gv_Servicios.DataSource > 0 Then
                    Dim ddlCantidadPaginas As DropDownList = CType(gv_Servicios.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
                    Dim ddlTamañoPaginas As DropDownList = CType(gv_Servicios.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
                    Dim txtTotalPaginas As Label = CType(gv_Servicios.BottomPagerRow.Cells(0).FindControl("lblTotalPaginas"), Label)

                    ddlTamañoPaginas.ClearSelection()
                    ddlTamañoPaginas.Items.FindByValue(gv_Servicios.PageSize).Selected = True

                    txtTotalPaginas.Text = gv_Servicios.PageCount
                    For cnt As Integer = 0 To gv_Servicios.PageCount - 1
                        Dim curr As Integer = cnt + 1
                        Dim item As New ListItem(curr.ToString())
                        If cnt = gv_Servicios.PageIndex Then
                            item.Selected = True
                        End If
                        ddlCantidadPaginas.Items.Add(item)
                    Next cnt

                    gv_Servicios.BottomPagerRow.Visible = True
                    gv_Servicios.BottomPagerRow.CssClass = "table-bottom-dark"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub ddlCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Servicios.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
            gv_Servicios.SetPageIndex(ddl.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Servicios.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
            gv_Servicios.PageSize = ddl.SelectedValue
            CargarServicios()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gv_Servicios_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            CargarServicios()
            gv_Servicios.PageIndex = e.NewPageIndex
            gv_Servicios.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim fileName As String = "~/Imagenes/" + FileUpload1.FileName
            FileUpload1.SaveAs(Server.MapPath("~/Imagenes/" + FileUpload1.FileName))
            Dim imagen As New ImagenDTO With {.Img64 = fileName}

            Dim lsCaracteristicas As New List(Of CaracteristicaDTO)
            For Each gvrow As GridViewRow In grdCaracteristicas.Rows
                Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                If checkbox.Checked = True Then
                    Dim caracteristica As New CaracteristicaDTO With
                            {.id = Convert.ToInt16(gvrow.Cells(1).Text),
                             .caracteristica = gvrow.Cells(2).Text
                            }
                    lsCaracteristicas.Add(caracteristica)
                End If
            Next
            Dim servicio As New ServicioDTO With {
                .nombre = txtNombre.Text,
                .imagen = imagen,
                .precio = txtPrecio.Text,
                .descripcion = txtDescripcion.Text,
                .caracteristicas = lsCaracteristicas,
                .id_catalogo = 0,
                .orden_catalogo = 0
                }
            ServicioBLL.ObtenerInstancia.Agregar(servicio)
            CargarServicios()
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                    .FechaHora = Now(),
                    .usuario = usuarioLogeado,
                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 14}, 'Suceso: Creacion Servicio
                    .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                    .observaciones = "Se creo el servico :" & servicio.id
            }
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                    .FechaHora = Now(),
                    .usuario = usuarioLogeado,
                    .tipoSuceso = New SucesoBitacoraDTO With {.id = 14}, 'Suceso: Creacion Servicio
                    .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                    .observaciones = "Ocurrio un error creando un servicio :" & ex.Message
            }
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim servicio As ServicioDTO = Current.Session("servicioModificando")
            With servicio
                .id = CType(txtID.Text, Integer)
                .nombre = txtNombre.Text
                .descripcion = txtDescripcion.Text
                .precio = CType(txtPrecio.Text, Decimal)
            End With
            Dim lsCaracteristicas As New List(Of CaracteristicaDTO)
            For Each gvrow As GridViewRow In grdCaracteristicas.Rows
                Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                If checkbox.Checked = True Then
                    Dim caracteristica As New CaracteristicaDTO With
                            {.id = Convert.ToInt16(gvrow.Cells(1).Text),
                             .caracteristica = gvrow.Cells(2).Text
                            }
                    lsCaracteristicas.Add(caracteristica)
                End If
            Next
            servicio.caracteristicas = lsCaracteristicas
            If FileUpload1.HasFile = True Then
                Dim fileName As String = "~/Imagenes/" + FileUpload1.FileName
                FileUpload1.SaveAs(Server.MapPath("~/Imagenes/" + FileUpload1.FileName))
                servicio.imagen.Img64 = fileName
            Else
                servicio.imagen.Img64 = lblFileSubido.Text
            End If
            Current.Session("entidadModal") = servicio
            Current.Session("accion") = "Modificar"
            MostrarModal("Modificacion de servicio", "¿Está seguro que desea modificar el servicio" & servicio.id & "?",, True)
        Catch ex As Exception
            'TODO: Mostra error y agregar bitacora
        End Try
    End Sub



    Private Sub btnCancelar_click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            txtID.Text = ""
            txtDescripcion.Text = ""
            txtNombre.Text = ""
            txtPrecio.Text = ""
            btnAgregar.Visible = True
            btnCancelar.Visible = False
            btnModificar.Visible = False
            CargarServicios()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub gv_Servicios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_Servicios.RowCommand
        Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar
        Dim servicio As New ServicioDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        servicio = servicios.Find(Function(x) x.id = id)

        If e.CommandName = "Editar" Then
            txtID.Text = servicio.id
            txtNombre.Text = servicio.nombre
            txtDescripcion.Text = servicio.nombre
            txtPrecio.Text = servicio.precio
            Image1.ImageUrl = servicio.imagen.Img64
            Image1.Visible = True
            FileUpload1.Visible = False
            lblFileSubido.Text = servicio.imagen.Img64
            lblFileSubido.Visible = True
            btnCambiarImagen.Visible = True
            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
            CargarCaracteristicas()
            For Each caracteristica As CaracteristicaDTO In servicio.caracteristicas
                For Each gvrow As GridViewRow In grdCaracteristicas.Rows
                    If Convert.ToInt16(gvrow.Cells(1).Text) = caracteristica.id Then
                        Dim checkbox As CheckBox = gvrow.FindControl("Checkbox1")
                        checkbox.Checked = True
                    End If
                Next
            Next
            Current.Session("servicioModificando") = servicio
        End If
        If e.CommandName = "Borrar" Then
            Current.Session("entidadModal") = servicio
            Current.Session("accion") = "Eliminar"
            MostrarModal("Eliminacion de servicio", "¿Está seguro que desea eliminar el servicio" & servicio.id & "?",, True)
        End If
    End Sub

    Private Sub btnCambiarImagen_Click(sender As Object, e As EventArgs) Handles btnCambiarImagen.Click
        lblFileSubido.Visible = False
        btnCambiarImagen.Visible = False
        FileUpload1.Visible = True
    End Sub

End Class