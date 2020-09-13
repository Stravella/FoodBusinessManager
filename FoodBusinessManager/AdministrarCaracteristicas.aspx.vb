Imports System.Web.HttpContext
Imports Entidades
Imports BLL

Public Class AdministrarCaracteristicas
    Inherits System.Web.UI.Page
    Dim caracteristicas As List(Of CaracteristicaDTO)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCaracteristicas()
        End If
    End Sub

#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        If Current.Session("accion") = "Eliminar" Then
            Dim caracteristica As CaracteristicaDTO = Current.Session("entidadModal")
            CaracteristicaBLL.ObtenerInstancia.Eliminar(caracteristica)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 12}, 'Suceso: Eliminacion caracteristica
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se elimino la caracteristica :" & caracteristica.id
                }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        End If
        If Current.Session("accion") = "Modificar" Then
            Dim caracteristica As CaracteristicaDTO = Current.Session("entidadModal")
            CaracteristicaBLL.ObtenerInstancia.Modificar(caracteristica)
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
            Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 12}, 'Suceso: Modificacion caracteristica
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se modifico la caracteristica :" & caracteristica.id
                }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
        End If
        CargarCaracteristicas()
        'Acá tengo que hidear el modal
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
    End Sub

    Public Sub MostrarModal(titulo As String, body As String, Optional grd As GridView = Nothing, Optional cancelar As Boolean = False)
        Dim panelMensaje As UpdatePanel = Master.FindControl("Modal")
        Dim tituloModal As Label = panelMensaje.FindControl("lblModalTitle")
        Dim bodyModal As Label = panelMensaje.FindControl("lblModalBody")
        If grd IsNot Nothing Then
            Dim grillaModal As GridView = panelMensaje.FindControl("grilla")
            grillaModal.AutoGenerateColumns = True
            grillaModal.Visible = True
            grillaModal = grd
            grillaModal.DataBind()
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



    Public Sub CargarCaracteristicas()
        caracteristicas = CaracteristicaBLL.ObtenerInstancia.Listar
        gv_Caracteristicas.DataSource = caracteristicas
        gv_Caracteristicas.DataBind()
        txtCaracteristica.Text = ""
        txtID.Text = ""
    End Sub


    Private Sub gv_Caracteristicas_Databound(sender As Object, e As EventArgs) Handles gv_Caracteristicas.DataBound
        Try
            If Not IsNothing(gv_Caracteristicas.DataSource) Then
                If gv_Caracteristicas.DataSource > 0 Then
                    Dim ddlCantidadPaginas As DropDownList = CType(gv_Caracteristicas.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
                    Dim ddlTamañoPaginas As DropDownList = CType(gv_Caracteristicas.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
                    Dim txtTotalPaginas As Label = CType(gv_Caracteristicas.BottomPagerRow.Cells(0).FindControl("lblTotalPaginas"), Label)

                    ddlTamañoPaginas.ClearSelection()
                    ddlTamañoPaginas.Items.FindByValue(gv_Caracteristicas.PageSize).Selected = True

                    txtTotalPaginas.Text = gv_Caracteristicas.PageCount
                    For cnt As Integer = 0 To gv_Caracteristicas.PageCount - 1
                        Dim curr As Integer = cnt + 1
                        Dim item As New ListItem(curr.ToString())
                        If cnt = gv_Caracteristicas.PageIndex Then
                            item.Selected = True
                        End If
                        ddlCantidadPaginas.Items.Add(item)
                    Next cnt

                    gv_Caracteristicas.BottomPagerRow.Visible = True
                    gv_Caracteristicas.BottomPagerRow.CssClass = "table-bottom-dark"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub ddlCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Caracteristicas.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
            gv_Caracteristicas.SetPageIndex(ddl.SelectedIndex)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Caracteristicas.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
            gv_Caracteristicas.PageSize = ddl.SelectedValue
            CargarCaracteristicas()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gv_Caracteristicas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            CargarCaracteristicas()
            gv_Caracteristicas.PageIndex = e.NewPageIndex
            gv_Caracteristicas.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            If txtID.ID <> "" Then
                Dim caracteristica As New CaracteristicaDTO With {.caracteristica = txtCaracteristica.Text}
                CaracteristicaBLL.ObtenerInstancia.Agregar(caracteristica)
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Usuario")
                Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = usuarioLogeado,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 13}, 'Suceso: Creacion caracteristica
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se elimino la caracteristica :" & caracteristica.id
                }
                CargarCaracteristicas()
            Else
                'Todo mostrar modal
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim caracteristica As New CaracteristicaDTO With {.caracteristica = txtCaracteristica.Text, .id = txtID.Text}
            Current.Session("entidadModal") = caracteristica
            Current.Session("accion") = "Modificar"
            Dim serviciosAsociados As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.ListarPorCaracteristica(caracteristica)
            'Si tiene servcios asociados los muestro, si no, no.
            If serviciosAsociados.Count = 0 Then
                MostrarModal("¿Está seguro que desea modificar la caracteristica " & caracteristica.id & "?", "No posee servicios asociados",, True)
            Else
                Dim grd As New GridView With {.AutoGenerateColumns = True, .DataSource = serviciosAsociados}
                MostrarModal("¿Está seguro que desea modificar la caracteristica " & caracteristica.id & "?", "Posee los siguiente servicios asociados", grd, True)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtID.Text = ""
        txtCaracteristica.Text = ""
        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnCancelar.Visible = False
    End Sub


    Protected Sub gv_Caracteristicas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim caracteristica As New CaracteristicaDTO
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        caracteristicas = CaracteristicaBLL.ObtenerInstancia.Listar
        caracteristica = caracteristicas.Find(Function(x) x.id = id)
        If e.CommandName = "Editar" Then
            txtID.Text = caracteristica.id
            txtCaracteristica.Text = caracteristica.caracteristica
            btnAgregar.Visible = False
            btnModificar.Visible = True
            btnCancelar.Visible = True
        ElseIf e.CommandName = "Borrar" Then
            'Busco los servicios asociados
            Dim serviciosAsociados As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.ListarPorCaracteristica(caracteristica)
            Current.Session("entidadModal") = caracteristica
            Current.Session("accion") = "Eliminar"
            'Si tiene servcios asociados los muestro, si no, no.
            If serviciosAsociados.Count = 0 Then
                MostrarModal("¿Está seguro que desea borrar la caracteristica " & caracteristica.id & "?", "No posee servicios asociados",, True)
            Else
                Dim grd As New GridView
                grd.DataSource = serviciosAsociados
                MostrarModal("¿Está seguro que desea borrar la caracteristica " & caracteristica.id & "?", "Posee los siguiente servicios asociados", grd, True)
            End If
        End If
    End Sub

End Class