Imports Entidades
Imports BLL

Public Class Bitacora
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargarUsuarios()
            cargarSucesos()
        End If
    End Sub

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

    Protected Function Buscar_click() As String
        If IsPostBack Then
            Dim usuarioSeleccionado As UsuarioDTO = UsuarioBLL.ObtenerInstancia.ObtenerUsuario(New UsuarioDTO With {.username = lstUsuarios.SelectedValue})
            Dim tipoSucesoSeleccionado As SucesoBitacoraDTO = BitacoraBLL.ObtenerInstancia.ObtenerSucesoBitacora(New SucesoBitacoraDTO With {.id = lstTipoSuceso.SelectedValue})
            Dim fechaDesde As DateTime = CalendarDesde.SelectedDate
            Dim fechaHasta As DateTime = CalendarHasta.SelectedDate

            Dim ListaBitacora As New DataTable
            ListaBitacora = BitacoraBLL.ObtenerInstancia.ListarTodos(tipoSucesoSeleccionado, usuarioSeleccionado, fechaDesde, fechaHasta, 10, 1)

            Return HtmlHelper.BuildTable(ListaBitacora)
        End If
    End Function
End Class