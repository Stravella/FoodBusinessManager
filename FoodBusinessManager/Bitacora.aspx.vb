﻿Imports Entidades
Imports BLL
Imports System.Web.HttpContext
Imports System.Globalization

Public Class Bitacora2
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
                        cargarUsuarios()
                        cargarSucesos()
                        cargarCriticidad()
                    Else
                        Response.Redirect("/Home.aspx")
                    End If
                End If
            End If
        End If
    End Sub

#Region "modal"
    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Response.Redirect("/Bitacora.aspx")
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
        Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
        If cancelar = True Then
            btnCancelar.Visible = True
        Else
            btnCancelar.Visible = False
        End If
        tituloModal.Text = titulo
        bodyModal.Text = body

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#myModal').modal();", True)
        panelMensaje.Update()
    End Sub

#End Region


#Region "Carga DropDowns"
    Protected Sub cargarUsuarios()
        Dim listaUsuarios As New List(Of UsuarioDTO)
        listaUsuarios = UsuarioBLL.ObtenerInstancia.ListarUsuarios
        lstUsuarios.Items.Add(New ListItem("Todos", "0"))
        For Each user As UsuarioDTO In listaUsuarios
            Dim item As New ListItem
            item.Text = user.username
            item.Value = user.id
            lstUsuarios.Items.Add(item)
            lstUsuarios.SelectedIndex = 0
        Next
    End Sub

    Protected Sub cargarSucesos()
        Dim listaSuceso As New List(Of SucesoBitacoraDTO)
        listaSuceso = BitacoraBLL.ObtenerInstancia.ListarSucesoBitacora
        lstTipoSuceso.Items.Add(New ListItem("Todos", "0"))
        For Each suceso As SucesoBitacoraDTO In listaSuceso
            Dim item As New ListItem
            item.Text = suceso.descripcion
            item.Value = suceso.id
            lstTipoSuceso.Items.Add(item)
            lstTipoSuceso.SelectedIndex = -1
        Next
    End Sub

    Protected Sub cargarCriticidad()
        Dim listaCriticidad As New List(Of CriticidadDTO)
        listaCriticidad = CriticidadBLL.ObtenerInstancia.Listar
        lstCriticidad.Items.Add(New ListItem("Todos", "0"))
        For Each crit As CriticidadDTO In listaCriticidad
            Dim item As New ListItem
            item.Text = crit.criticidad
            item.Value = crit.id
            lstCriticidad.Items.Add(item)
            lstTipoSuceso.SelectedIndex = -1
        Next
    End Sub

#End Region

    Private Sub CargarBitacoras()
        Dim usuarioSeleccionado As New UsuarioDTO
        If lstUsuarios.SelectedIndex = 0 Then
            usuarioSeleccionado = Nothing
        Else
            usuarioSeleccionado = UsuarioBLL.ObtenerInstancia.ObtenerPorId(lstUsuarios.SelectedValue)
        End If

        Dim tipoSucesoSeleccionado As New SucesoBitacoraDTO
        If lstTipoSuceso.SelectedIndex = 0 Then
            tipoSucesoSeleccionado = Nothing
        Else
            tipoSucesoSeleccionado = BitacoraBLL.ObtenerInstancia.ObtenerSucesoBitacora(New SucesoBitacoraDTO With {.id = lstTipoSuceso.SelectedValue})
        End If

        Dim criticidadSeleccionada As New CriticidadDTO
        If lstCriticidad.SelectedIndex = 0 Then
            criticidadSeleccionada = Nothing
        Else
            criticidadSeleccionada = CriticidadBLL.ObtenerInstancia.ObtenerPorId(lstCriticidad.SelectedValue)
        End If

        Dim fechaDesde As Date = Date.Parse("01/01/2019")
        Dim fechaHasta As Date = Date.Parse("01/01/2022")
        If Not (txtDesde.Text = "") Then
            fechaDesde = Date.Parse(txtDesde.Text)
        End If
        If Not (txtHasta.Text = "") Then
            fechaHasta = Date.Parse(txtHasta.Text)
        End If

        Dim ListaBitacora As New List(Of BitacoraDTO)
        ListaBitacora = BitacoraBLL.ObtenerInstancia.Listar(tipoSucesoSeleccionado, usuarioSeleccionado, fechaDesde, fechaHasta, criticidadSeleccionada)

        If ListaBitacora.Count > 0 Then
            Me.gv_Bitacora.DataSource = ListaBitacora
            Me.gv_Bitacora.DataBind()
        Else
            MostrarModal("Lo siento", "La busqueda no devolvio resultados",,)
        End If
    End Sub

    Private Sub gv_Bitacora_DataBound(sender As Object, e As EventArgs) Handles gv_Bitacora.DataBound
        Try
            If Not IsNothing(gv_Bitacora.DataSource) AndAlso gv_Bitacora.Rows.Count > 0 Then
                Dim ddlCantidadPaginas As DropDownList = CType(gv_Bitacora.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
                Dim ddlTamañoPaginas As DropDownList = CType(gv_Bitacora.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
                Dim txtTotalPaginas As Label = CType(gv_Bitacora.BottomPagerRow.Cells(0).FindControl("lblTotalPaginas"), Label)

                ddlTamañoPaginas.ClearSelection()
                ddlTamañoPaginas.Items.FindByValue(gv_Bitacora.PageSize).Selected = True

                txtTotalPaginas.Text = gv_Bitacora.PageCount
                For cnt As Integer = 0 To gv_Bitacora.PageCount - 1
                    Dim curr As Integer = cnt + 1
                    Dim item As New ListItem(curr.ToString())
                    If cnt = gv_Bitacora.PageIndex Then
                        item.Selected = True
                    End If
                    ddlCantidadPaginas.Items.Add(item)
                Next cnt

                gv_Bitacora.BottomPagerRow.Visible = True
                gv_Bitacora.BottomPagerRow.CssClass = "table-bottom-dark"
            End If
        Catch ex As Exception
            MostrarModal("Lo siento", "Ocurrio un error inesperado! Por favor contacte a su administrador",,)
        End Try
    End Sub


    Protected Sub ddlCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Bitacora.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
            gv_Bitacora.SetPageIndex(ddl.SelectedIndex)
        Catch ex As Exception
            MostrarModal("Lo siento", "Ocurrio un error inesperado! Por favor contacte a su administrador",,)
        End Try
    End Sub

    Protected Sub ddlTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Bitacora.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
            gv_Bitacora.PageSize = ddl.SelectedValue
            CargarBitacoras()
        Catch ex As Exception
            MostrarModal("Lo siento", "Ocurrio un error inesperado! Por favor contacte a su administrador",,)
        End Try
    End Sub

    Protected Sub gv_Bitacora_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            CargarBitacoras()
            gv_Bitacora.PageIndex = e.NewPageIndex
            gv_Bitacora.DataBind()
        Catch ex As Exception
            MostrarModal("Lo siento", "Ocurrio un error inesperado! Por favor contacte a su administrador",,)
        End Try
    End Sub

    Private Sub BtnFiltrar_Click(sender As Object, e As EventArgs) Handles BtnFiltrar.Click
        CargarBitacoras()
    End Sub

    Private Sub Bitacora2_Init(sender As Object, e As EventArgs) Handles Me.Init
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub
End Class