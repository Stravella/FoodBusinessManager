Imports BLL
Imports Entidades
Imports System.Web.HttpContext
Public Class BitacoraErrores
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargarUsuarios()
            cargarSucesos()
        End If
    End Sub

#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Alert, Warning, Info, Success
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
    End Sub
#End Region

    Protected Sub cargarUsuarios()
        Dim listaUsuarios As New List(Of UsuarioDTO)
        listaUsuarios = UsuarioBLL.ObtenerInstancia.ListarUsuarios
        For Each user As UsuarioDTO In listaUsuarios
            Dim item As New ListItem
            item.Text = user.username
            item.Value = user.username
            lstUsuarios.Items.Add(item)
        Next
    End Sub

    Protected Sub cargarSucesos()
        Dim listaSuceso As New List(Of SucesoBitacoraDTO)
        listaSuceso = BitacoraBLL.ObtenerInstancia.ListarSucesoBitacora
        For Each suceso As SucesoBitacoraDTO In listaSuceso
            Dim item As New ListItem
            item.Text = suceso.descripcion
            item.Value = suceso.id
            lstTipoSuceso.Items.Add(item)
        Next
    End Sub

    Private Sub CargarBitacoras()
        Dim usuarioSeleccionado As UsuarioDTO = UsuarioBLL.ObtenerInstancia.ObtenerUsuario(New UsuarioDTO With {.username = lstUsuarios.SelectedValue})
        Dim tipoSucesoSeleccionado As SucesoBitacoraDTO = BitacoraBLL.ObtenerInstancia.ObtenerSucesoBitacora(New SucesoBitacoraDTO With {.id = lstTipoSuceso.SelectedValue})
        Dim fechaDesde As DateTime = CalendarDesde.SelectedDate
        Dim fechaHasta As DateTime = CalendarHasta.SelectedDate

        Dim ListaBitacora As New List(Of BitacoraErroresDTO)
        ListaBitacora = BitacoraBLL.ObtenerInstancia.ListarErrores(tipoSucesoSeleccionado, usuarioSeleccionado, fechaDesde, fechaHasta)

        If IsNothing(ListaBitacora) Then
            Me.gv_BitacoraError.DataSource = ListaBitacora
            Me.gv_BitacoraError.DataBind()
        Else
            Me.gv_BitacoraError.DataSource = ListaBitacora
            Me.gv_BitacoraError.DataBind()
        End If
    End Sub


    Private Sub gv_Bitacora_DataBound(sender As Object, e As EventArgs) Handles gv_BitacoraError.DataBound
        Try
            If Not IsNothing(gv_BitacoraError.DataSource) Then
                Dim ddlCantidadPaginas As DropDownList = CType(gv_BitacoraError.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
                Dim ddlTamañoPaginas As DropDownList = CType(gv_BitacoraError.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
                Dim txtTotalPaginas As Label = CType(gv_BitacoraError.BottomPagerRow.Cells(0).FindControl("lblTotalPaginas"), Label)

                ddlTamañoPaginas.ClearSelection()
                ddlTamañoPaginas.Items.FindByValue(gv_BitacoraError.PageSize).Selected = True

                txtTotalPaginas.Text = gv_BitacoraError.PageCount
                For cnt As Integer = 0 To gv_BitacoraError.PageCount - 1
                    Dim curr As Integer = cnt + 1
                    Dim item As New ListItem(curr.ToString())
                    If cnt = gv_BitacoraError.PageIndex Then
                        item.Selected = True
                    End If
                    ddlCantidadPaginas.Items.Add(item)
                Next cnt

                Dim IdiomaActual As IdiomaDTO
                If IsNothing(Current.Session("Cliente")) Then
                    IdiomaActual = Application("Español")
                Else
                    IdiomaActual = Application(TryCast(Current.Session("Cliente"), Entidades.IdiomaDTO).nombre)
                End If
                'Con esto cambio el nombre de los rows de acuerdo al idioma del usuario
                'TODO: Implementar cuando tenga multidioma
                With gv_BitacoraError.HeaderRow

                End With
                gv_BitacoraError.BottomPagerRow.Visible = True
                gv_BitacoraError.BottomPagerRow.CssClass = "table-bottom-dark"
            End If
        Catch ex As Exception
            'TODO: Guardar Bitacora Error y mostrar mensaje error
        End Try
    End Sub


    Protected Sub ddlCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_BitacoraError.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
            gv_BitacoraError.SetPageIndex(ddl.SelectedIndex)
        Catch ex As Exception
            'TODO: Implementar logeo de errores y mensaje error
        End Try
    End Sub

    Protected Sub ddlTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_BitacoraError.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
            gv_BitacoraError.PageSize = ddl.SelectedValue
            CargarBitacoras()
        Catch ex As Exception
            'TODO: Implementar logeo de errores y mensaje error
        End Try
    End Sub

    Protected Sub gv_Bitacora_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            CargarBitacoras()
            gv_BitacoraError.PageIndex = e.NewPageIndex
            gv_BitacoraError.DataBind()
        Catch ex As Exception
            'TODO: Implementar logeo de errores y mensaje error
        End Try
    End Sub

    Private Sub BtnFiltrar_Click(sender As Object, e As EventArgs) Handles BtnFiltrar.Click
        CargarBitacoras()
    End Sub

End Class