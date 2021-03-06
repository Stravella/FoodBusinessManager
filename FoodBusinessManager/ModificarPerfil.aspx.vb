﻿Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class ModificarPermiso
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
                ls = PermisoBLL.ObtenerInstancia.Listar
                Dim nodo As New TreeNode
                TreeViewNuevosPermisos.Attributes.Add("onclick", "forcePostBack()")
                TreeViewNuevosPermisos.Nodes.Clear()
                TreeHelper.ObtenerInstancia.ArmarNodos(ls, Nothing, TreeViewNuevosPermisos)
                CargarListaPerfiles()
            Else
                Response.Redirect("/Home.aspx")
            End If
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
                'IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
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
                MostrarMensaje("No hay usuarios con el perfil seleccionado", TipoAlerta.Info)
            Else
                gv_Perfiles.DataSource = lsUsuarios
                gv_Perfiles.DataBind()
                'TODO: CAMBIAR NOMBRE DEL HEADER DE LA GRILLA
            End If
        Catch ex As Exception
            'TODO: LOGEAR ERROR
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", TipoAlerta.Danger)
        End Try
    End Sub


    'TODO: Volver a probar esto más adelante: cuando tenga el ABM Usuarios
    Private Sub btnModificarPerfil_Click(sender As Object, e As EventArgs) Handles btnModificarPerfil.Click
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
                'IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If
            Dim perfiles As List(Of PermisoComponente) = TryCast(Session("Perfiles"), List(Of PermisoComponente))
            Dim PerfilViejo As PerfilCompuesto = perfiles(lstPerfil.SelectedIndex)
            Dim PerfilNuevo As New PerfilCompuesto With {.id_permiso = PerfilViejo.id_permiso, .nombre = PerfilViejo.nombre, .se_puede_borrar = 1}
            PerfilNuevo = TreeHelper.ObtenerInstancia.RecorrerArbol(PerfilNuevo, Nothing, TreeViewNuevosPermisos)
            'Modifico el perfil
            If PerfilNuevo.Hijos.Count > 0 Then
                PermisoBLL.ObtenerInstancia.ModificarPerfil(PerfilViejo, PerfilNuevo)
                Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 5}, 'Suceso 5: Modificacion de perfil
                                                .usuario = Current.Session("Cliente"),
                .observaciones = "Se modificaron los permisos del perfil "
                                                }
                BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
                MostrarMensaje("Se ha modificado el perfil", TipoAlerta.Success)
            Else
                MostrarMensaje("Debe seleccionar algún permiso del listado", TipoAlerta.Warning)
            End If
        Catch ex As Exception

            MostrarMensaje("Lo siento! Ocurrió un error inesperado", TipoAlerta.Danger)
        End Try
    End Sub

    Private Sub TreeViewNuevosPermisos_TreeNodeCheckChanged(sender As Object, e As TreeNodeEventArgs) Handles TreeViewNuevosPermisos.TreeNodeCheckChanged
        Try
            TreeHelper.ObtenerInstancia.CheckearNodosHijo(e.Node)
        Catch ex As Exception
            MostrarMensaje("No se pudo chequear el nodo, intente nuevamente y en caso de persistir, contacte al administrador.", TipoAlerta.Info)
        End Try
    End Sub
End Class