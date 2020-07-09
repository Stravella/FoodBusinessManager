Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class ModificarPermiso
    Inherits System.Web.UI.Page
    Dim ls As List(Of PermisoComponente)
    Dim listaPerfiles As List(Of PermisoComponente)
#Region "Mensajes"
    Protected Sub MostrarMensaje(Mensaje As String, Tipo As String)
        'Tipos: Danger,Success,Warning,Info
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Mensaje & "','" & Tipo & "');", True)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ls = PermisoBLL.ObtenerInstancia.Listar
            Dim nodo As New TreeNode
            TreeViewNuevosPermisos.Attributes.Add("onclick", "forcePostBack()")
            TreeViewNuevosPermisos.Nodes.Clear()
            TreeHelper.ObtenerInstancia.ArmarNodos(ls, Nothing, TreeViewNuevosPermisos)
            CargarListaPerfiles()
        End If
    End Sub

    Protected Sub CargarListaPerfiles()
        listaPerfiles = BLL.PermisoBLL.ObtenerInstancia.ListarPerfiles()
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
                IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If
            Dim perfiles As List(Of PermisoComponente) = TryCast(Session("Perfiles"), List(Of PermisoComponente))
            TreeHelper.ObtenerInstancia.CargarPefil(TreeViewPermisoActual, perfiles(lstPerfil.SelectedIndex))
            TreeViewPermisoActual.ExpandAll()
            For Each nodo As TreeNode In TreeViewPermisoActual.Nodes
                TreeHelper.ObtenerInstancia.CheckearTodos(nodo)
            Next
            TreeViewNuevosPermisos.CollapseAll()
            Dim lsUsuarios As New List(Of UsuarioDTO)
            lsUsuarios = BLL.UsuarioBLL.ObtenerInstancia.ListarPorPerfil(perfiles(lstPerfil.SelectedIndex))
            Session("UsuariosPerfilSeleccionado") = lsUsuarios
            If lsUsuarios.Count = 0 Then
                MostrarMensaje("No hay usuarios con el perfil seleccionado", "Info")
            Else
                gv_Perfiles.DataSource = lsUsuarios
                gv_Perfiles.DataBind()
                'TODO: CAMBIAR NOMBRE DEL HEADER DE LA GRILLA
            End If
        Catch ex As Exception
            'TODO: LOGEAR ERROR
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", "Danger")
        End Try
    End Sub


    'TODO: Volver a probar esto más adelante: cuando tenga el ABM Usuarios
    Private Sub btnModificarPerfil_Click(sender As Object, e As EventArgs) Handles btnModificarPerfil.Click
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
                IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If
            Dim perfiles As List(Of PermisoComponente) = TryCast(Session("Perfiles"), List(Of PermisoComponente))
            Dim PerfilViejo As PerfilCompuesto = perfiles(lstPerfil.SelectedIndex)
            Dim PerfilNuevo As New PerfilCompuesto With {.nombre = PerfilViejo.nombre}
            PerfilNuevo = TreeHelper.ObtenerInstancia.RecorrerArbol(PerfilNuevo, Nothing, TreeViewPermisoActual)
            'Creo el nuevo
            PerfilNuevo.nombre = PerfilViejo.nombre
            If PerfilNuevo.Hijos.Count > 0 Then
                PermisoBLL.ObtenerInstancia.Crear(PerfilNuevo)
            Else
                MostrarMensaje("Debe seleccionar algún perfil del listado", "Warning")
            End If
            'Actualizo los usuarios
            Dim lsUsuarios As List(Of UsuarioDTO) = Session("UsuariosPerfilSeleccionado")
            For Each Usuario As UsuarioDTO In lsUsuarios
                Usuario.perfil = PerfilNuevo
                UsuarioBLL.ObtenerInstancia.ModificarUsuario(Usuario)
            Next
            'Borro el viejo
            PermisoBLL.ObtenerInstancia.BorrarPerfil(PerfilViejo)
            MostrarMensaje("Se ha modificado el perfil", "Success")
        Catch ex As Exception
            'TODO: LOGEAR ERROR
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", "Danger")
        End Try
    End Sub
End Class