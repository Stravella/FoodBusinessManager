Imports System.Globalization
Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class ModificarIdioma
    Inherits System.Web.UI.Page
    Dim Idiomas As List(Of IdiomaDTO)

#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Danger,Success,Warning,Info
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCulturasCreadas()
        End If
    End Sub

    Private Sub CargarCulturasCreadas()
        Try
            Idiomas = IdiomaBLL.ObtenerInstancia.Listar()
            lstCulturasCreadas.DataTextField = "nombre"
            lstCulturasCreadas.DataValueField = "id_idioma"
            lstCulturasCreadas.DataSource = Idiomas
            lstCulturasCreadas.DataBind()
        Catch ex As Exception
            MostrarMensaje("Error al cargar la lista de idiomas", "Danger")
        End Try
    End Sub

    Private Sub lstCulturasCreadas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCulturasCreadas.SelectedIndexChanged
        Dim idiomaSeleccionado As New IdiomaDTO With {.id_idioma = lstCulturasCreadas.SelectedItem.Value}
        CargarGrilla(idiomaSeleccionado)
    End Sub

    Public Sub CargarGrilla(unIdioma As IdiomaDTO)
        Dim idiomaSeleccionado As IdiomaDTO = IdiomaBLL.ObtenerInstancia.Obtener(unIdioma)
        gv_Etiquetas.DataSource = idiomaSeleccionado.ListaEtiquetas
        gv_Etiquetas.DataBind()
    End Sub

    Private Sub gv_Etiquetas_DataBound(sender As Object, e As EventArgs) Handles gv_Etiquetas.DataBound
        Try
            Dim ddlCantidadPaginas As DropDownList = CType(gv_Etiquetas.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
            Dim ddlTamañoPaginas As DropDownList = CType(gv_Etiquetas.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
            Dim txtTotalPaginas As Label = CType(gv_Etiquetas.BottomPagerRow.Cells(0).FindControl("lblTotalPaginas"), Label)

            ddlTamañoPaginas.ClearSelection()
            ddlTamañoPaginas.Items.FindByValue(gv_Etiquetas.PageSize).Selected = True

            txtTotalPaginas.Text = gv_Etiquetas.PageCount
            For cnt As Integer = 0 To gv_Etiquetas.PageCount - 1
                Dim curr As Integer = cnt + 1
                Dim item As New ListItem(curr.ToString())
                If cnt = gv_Etiquetas.PageIndex Then
                    item.Selected = True
                End If
                ddlCantidadPaginas.Items.Add(item)
            Next cnt

            If Not IsNothing(Current.Session("Traducciones")) Then
                Dim traduccionesNuevas As Dictionary(Of Integer, String) = Current.Session("Traducciones")
                For Each rw As GridViewRow In gv_Etiquetas.Rows
                    Dim txt As TextBox = CType(rw.Cells(0).FindControl("txtTraduccion"), TextBox)
                    If traduccionesNuevas.ContainsKey(rw.Cells(0).Text) Then
                        txt.Text = traduccionesNuevas(rw.Cells(0).Text)
                    End If
                Next
            End If

            gv_Etiquetas.BottomPagerRow.Visible = True
            gv_Etiquetas.BottomPagerRow.CssClass = "table-bottom-dark"

        Catch ex As Exception
            'TODO: Implementar Logeo de Error y Mensaje
        End Try
    End Sub


    Private Sub CargarPalabras()
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
                IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If

            IdiomaActual.ListaEtiquetas = IdiomaBLL.ObtenerInstancia.ObtenerTraducciones(IdiomaActual)

            If IsNothing(IdiomaActual.ListaEtiquetas) Then
                gv_Etiquetas.DataSource = IdiomaActual.ListaEtiquetas
                gv_Etiquetas.DataBind()
            Else
                gv_Etiquetas.DataSource = IdiomaActual.ListaEtiquetas
                gv_Etiquetas.DataBind()
            End If
        Catch ex As Exception
            'TODO: IMPLEMENTAR MENSAJE DE ERROR
        End Try
    End Sub

    Private Sub GuardarTraducciones()
        Try
            Dim traduccionesNuevas As Dictionary(Of Integer, String)
            If Not IsNothing(Current.Session("Traducciones")) Then
                traduccionesNuevas = Current.Session("Traducciones")
            Else
                traduccionesNuevas = New Dictionary(Of Integer, String)
            End If
            For Each rw As GridViewRow In gv_Etiquetas.Rows
                Dim txt As TextBox = CType(rw.Cells(0).FindControl("txtTraduccion"), TextBox)
                If traduccionesNuevas.ContainsKey(rw.Cells(0).Text) Then
                    traduccionesNuevas(rw.Cells(0).Text) = txt.Text
                Else
                    If txt.Text <> "" Then
                        traduccionesNuevas.Add(rw.Cells(0).Text, txt.Text)
                    End If
                End If
            Next
            Current.Session("Traducciones") = traduccionesNuevas
        Catch ex As Exception
            'TODO: IMPLEMENTAR MENSAJE DE ERROR
        End Try
    End Sub

    Private Sub CargarIdioma(Idioma As IdiomaDTO)
        Try
            For Each rw As GridViewRow In gv_Etiquetas.Rows
                Dim traduccion As TextBox = CType(rw.Cells(0).FindControl("txtTraduccion"), TextBox)
                Dim etiquetaTraducida As New IdiomaEtiquetaDTO With {
                    .id_etiqueta = rw.Cells(0).Text,
                    .etiqueta = rw.Cells(1).Text,
                    .traduccion = traduccion.Text
                }
                'GUARDO UNICAMENTE LOS QUE TIENEN TRADUCCIÓN
                If etiquetaTraducida.traduccion <> "" Then
                    Idioma.ListaEtiquetas.Add(etiquetaTraducida)
                End If
            Next
        Catch ex As Exception
            'TODO: IMPLEMENTAR MENSAJE DE ERROR
        End Try
    End Sub

#Region "Paginado"
    Protected Sub ddlCantidadPaginas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Etiquetas.BottomPagerRow.Cells(0).FindControl("ddlCantidadPaginas"), DropDownList)
            gv_Etiquetas.SetPageIndex(ddl.SelectedIndex)
        Catch ex As Exception
            'TODO: Implementar logeo de errores y mensaje error
        End Try
    End Sub

    Protected Sub ddlTamañoPaginas_SelectedPageSizeChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(gv_Etiquetas.BottomPagerRow.Cells(0).FindControl("ddlTamañoPaginas"), DropDownList)
            gv_Etiquetas.PageSize = ddl.SelectedValue
            GuardarTraducciones()
            CargarPalabras()
        Catch ex As Exception
            'TODO: Implementar logeo de errores y mensaje error
        End Try
    End Sub

    Protected Sub gv_Etiquetas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            GuardarTraducciones()
            CargarPalabras()
            gv_Etiquetas.PageIndex = e.NewPageIndex
            gv_Etiquetas.DataBind()
        Catch ex As Exception
            'TODO: Implementar logeo de errores y mensaje error
        End Try
    End Sub

    Protected Sub btn_modificarIdioma_Click(sender As Object, e As EventArgs) Handles btn_modificarIdioma.Click
        Try
            Dim IdiomaActual As IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual = Application("Español")
            Else
                IdiomaActual = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If

            If Page.IsValid = True Then
                Dim IdiomaModificado As New IdiomaDTO
                IdiomaModificado.id_idioma = lstCulturasCreadas.SelectedItem.Value
                IdiomaModificado.nombre = lstCulturasCreadas.SelectedItem.Text
                IdiomaModificado.ListaEtiquetas = New List(Of IdiomaEtiquetaDTO)
                IdiomaModificado.DVH = "PENDIENTE IMPLEMENTAR"
                CargarIdioma(IdiomaModificado)

                For Each etiquetaTraducida As IdiomaEtiquetaDTO In IdiomaModificado.ListaEtiquetas
                    IdiomaBLL.ObtenerInstancia.ModificarTraduccion(IdiomaModificado, etiquetaTraducida)
                    'LOGEAR

                Next

            Else
            End If
            MostrarMensaje("Se modifico el idioma!", "Success")
        Catch ex As Exception
            MostrarMensaje("Ocurrio un error!", "Danger")
        End Try
    End Sub


#End Region


End Class