Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class EliminarPerfil
    Inherits System.Web.UI.Page
    Dim ls As List(Of PermisoComponente)
    Dim listaPerfiles As List(Of PermisoComponente)

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
            ls = PermisoBLL.ObtenerInstancia.Listar
            Dim nodo As New TreeNode
            CargarListaPerfiles()
        End If
    End Sub

    Protected Sub CargarListaPerfiles()
        listaPerfiles = BLL.PermisoBLL.ObtenerInstancia.ListarPerfilesEditables
        Session("Perfiles") = listaPerfiles
        lstPerfil.DataSource = listaPerfiles
        lstPerfil.DataBind()
        lstPerfil.SelectedIndex = 0
    End Sub

    Private Sub lstPerfil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstPerfil.SelectedIndexChanged
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
            End If
            Dim perfiles As List(Of PermisoComponente) = TryCast(Session("Perfiles"), List(Of PermisoComponente))
            TreeHelper.ObtenerInstancia.CargarPefil(TreeViewPermisoActual, perfiles(lstPerfil.SelectedIndex))
            TreeViewPermisoActual.ExpandAll()
            For Each nodo As TreeNode In TreeViewPermisoActual.Nodes
                TreeHelper.ObtenerInstancia.CheckearTodos(nodo)
            Next
            Dim listaUsuarios As New List(Of UsuarioDTO)
            listaUsuarios = BLL.UsuarioBLL.ObtenerInstancia.ListarPorPerfil(perfiles(lstPerfil.SelectedIndex))
            Session("UsuariosPerfilSeleccionado") = listaUsuarios
            If listaUsuarios.Count = 0 Then
                MostrarMensaje("No hay usuarios con el perfil seleccionado", TipoAlerta.Info)
            Else
                gv_Perfiles.DataSource = listaUsuarios
                gv_Perfiles.DataBind()
                'TODO: CAMBIAR NOMBRE DEL HEADER DE LA GRILLA
            End If
        Catch ex As Exception
            'TODO: LOGEAR ERROR
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", TipoAlerta.Danger)
        End Try
    End Sub

    Private Sub btnEliminarPerfil_Click(sender As Object, e As EventArgs) Handles btnEliminarPerfil.Click
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
            End If
            listaPerfiles = BLL.PermisoBLL.ObtenerInstancia.ListarPerfilesEditables
            Dim perfilSeleccionado As PerfilCompuesto = listaPerfiles(lstPerfil.SelectedIndex)
            Dim listaUsuarios As New List(Of UsuarioDTO)
            listaUsuarios = BLL.UsuarioBLL.ObtenerInstancia.ListarPorPerfil(perfilSeleccionado)
            If listaUsuarios.Count = 0 Then
                PermisoBLL.ObtenerInstancia.BorrarPerfil(perfilSeleccionado)
                Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 6}, 'Suceso 6: Borrado de perfil
                                                .usuario = Current.Session("Cliente"),
                                                .observaciones = "Se elimino el perfil"
                                                }
                BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
                MostrarMensaje("Se ha modificado el perfil", TipoAlerta.Success)
                Response.Redirect("EliminarPerfil.aspx")
            Else
                MostrarMensaje("No se puede eliminar un perfil que es utilizado por usuarios. Primero re asigne un nuevo perfil a los usuarios", TipoAlerta.Info)
            End If
        Catch ex As Exception

            MostrarMensaje("Lo siento! Ocurrió un error inesperado", TipoAlerta.Danger)
        End Try
    End Sub

End Class