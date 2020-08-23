Imports System.Globalization
Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class Idiomas
    Inherits System.Web.UI.Page

#Region "Mensajes"
    Public Enum TipoAlerta
        Success
        Info
        Warning
        Danger
    End Enum

    Public Sub MostrarMensaje(mensaje As String, tipo As TipoAlerta)
        Dim panelMensaje As Panel = Master.FindControl("Mensaje")
        Dim labelMensaje As Label = panelMensaje.FindControl("labelMensaje")

        labelMensaje.Text = mensaje
        panelMensaje.CssClass = String.Format("alert alert-{0} alert-dismissable", tipo.ToString.ToLower())
        panelMensaje.Attributes.Add("role", "alert")
        panelMensaje.Visible = True
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCulturas()
            CargarPalabras()
            Current.Session("Traducciones") = Nothing
        End If
    End Sub

    Private Sub CargarCulturas()
        Dim culturas = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures)
        For i As Integer = 1 To culturas.Length - 1
            Dim item As New ListItem(culturas(i).DisplayName, culturas(i).Name)
            Me.lstCulturas.Items.Add(item)
        Next
    End Sub

    Private Sub CargarPalabras()
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
                IdiomaActual.ListaEtiquetas = IdiomaBLL.ObtenerInstancia.ObtenerTraducciones(IdiomaActual)
            Else
                Dim usuarioLogeado As UsuarioDTO = Current.Session("Cliente")
                IdiomaActual = usuarioLogeado.idioma
            End If

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


    Public Sub CargarIdioma(ByRef Idioma As IdiomaDTO)
        'TODO : Tengo que iterar la tabla y cargar el id_etiqueta y la nueva traduccion. 
        Try
            For Each rw As GridViewRow In gv_Etiquetas.Rows
                Dim traduccion As TextBox = CType(rw.Cells(0).FindControl("txtTraduccion"), TextBox)
                Dim etiquetaTraducida As New IdiomaEtiquetaDTO With {
                    .id_etiqueta = rw.Cells(0).Text,
                    .etiqueta = rw.Cells(1).Text,
                    .traduccion = traduccion.Text
                }
                Idioma.ListaEtiquetas.Add(etiquetaTraducida)
            Next
        Catch ex As Exception
            MostrarMensaje("Ocurrio un error al cargar el idioma", TipoAlerta.Danger)
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
            MostrarMensaje("Ocurrio un error al guardar las traducciones", TipoAlerta.Danger)
        End Try
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

    Private Sub lstCulturas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCulturas.SelectedIndexChanged
        If IsPostBack Then
            Dim idiomaSeleccionado As New IdiomaDTO With {.id_idioma = lstCulturas.SelectedItem.Value}
            If IdiomaBLL.ObtenerInstancia.VerificarExistencia(idiomaSeleccionado) = True Then
                CargarGrilla(idiomaSeleccionado)
                Session.Add("CreandoIdioma", False)

            Else
                'Sí no existe cargo la grilla en es-AR
                Dim idiomaArgentino As New IdiomaDTO With {.id_idioma = "es-AR"}
                CargarGrilla(idiomaArgentino)
                Session.Add("CreandoIdioma", True)

            End If
        End If
    End Sub

    Public Sub CargarGrilla(unIdioma As IdiomaDTO)
        Dim idiomaActual As IdiomaDTO = IdiomaBLL.ObtenerInstancia.Obtener(unIdioma)
        gv_Etiquetas.DataSource = idiomaActual.ListaEtiquetas
        gv_Etiquetas.DataBind()
    End Sub

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

    Protected Sub btn_crearIdioma_Click(sender As Object, e As EventArgs) Handles btn_crearIdioma.Click
        Try
            Dim IdiomaActual As IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual = Application("Español")
            Else
                IdiomaActual = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If

            If Page.IsValid = True Then
                Dim Idioma As New IdiomaDTO
                Idioma.id_idioma = lstCulturas.SelectedItem.Value
                Idioma.nombre = lstCulturas.SelectedItem.Text
                Idioma.ListaEtiquetas = New List(Of IdiomaEtiquetaDTO)
                Idioma.DVH = "PENDIENTE IMPLEMENTAR"
                CargarIdioma(Idioma)
                If IdiomaBLL.ObtenerInstancia.VerificarExistencia(Idioma) Then
                    MostrarMensaje("El idioma ya existe", "Warning")
                Else
                    If IdiomaBLL.ObtenerInstancia.CrearIdioma(Idioma) Then
                        Dim clienteLogeado As UsuarioDTO = Current.Session("cliente")
                        'TODO: Guardar en Bitacora que se creo el idioma correctamente
                        MostrarMensaje("Se creo el idioma", TipoAlerta.Success)
                    Else
                        MostrarMensaje("Ocurrio un error al guardar la acción, contacte al administrador", TipoAlerta.Success)
                    End If
                End If
            Else

            End If


        Catch ex As Exception
            MostrarMensaje("Ocurrio un error al crear el idioma", TipoAlerta.Danger)
        End Try
    End Sub
End Class