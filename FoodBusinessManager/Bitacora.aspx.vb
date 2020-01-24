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

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim usuarioSeleccionado As UsuarioDTO = UsuarioBLL.ObtenerInstancia.ObtenerUsuario(New UsuarioDTO With {.username = lstUsuarios.SelectedValue})
        Dim tipoSucesoSeleccionado As SucesoBitacoraDTO = BitacoraBLL.ObtenerInstancia.ObtenerSucesoBitacora(New SucesoBitacoraDTO With {.id = lstTipoSuceso.SelectedValue})
        Dim fechaDesde As DateTime = Calendar1.SelectedDate
        Dim fechaHasta As DateTime = Calendar2.SelectedDate

        Dim ListaBitacora As New List(Of BitacoraDTO)
        ListaBitacora = BitacoraBLL.ObtenerInstancia.ListarTodos(tipoSucesoSeleccionado, usuarioSeleccionado, fechaDesde, fechaHasta, 1, 10)

    End Sub

End Class