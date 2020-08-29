Imports System.Web.HttpContext
Imports Entidades
Imports BLL


Public Class Permisos
    Inherits System.Web.UI.Page
    Dim existe As Boolean
    Dim ls As List(Of PermisoComponente)
    Dim nodos As TreeNodeCollection

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
            TreeViewPermisos.Attributes.Add("onclick", "forcePostBack()")
            TreeViewPermisos.Nodes.Clear()
            TreeHelper.ObtenerInstancia.ArmarNodos(ls, Nothing, TreeViewPermisos)
        End If
    End Sub


    Protected Sub ValidarSiExiste(unNodo As TreeNode)
        For Each NodoHijo As TreeNode In unNodo.ChildNodes
            ValidarSiExiste(NodoHijo)
        Next
        If unNodo.Text = txtNombrePerfil.Text Then
            existe = True
        End If
    End Sub

    Public Function ValidarNombre(unArbol As TreeView) As Boolean
        For Each Nodo As TreeNode In unArbol.Nodes
            ValidarSiExiste(Nodo)
        Next
        Return existe
    End Function


    Protected Sub btnAgregarPerfill_Click1(sender As Object, e As EventArgs) Handles btnAgregarPerfill.Click
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else

            End If

            Dim Perfil As New PerfilCompuesto With {.nombre = txtNombrePerfil.Text,
                                                    .se_puede_borrar = True,
                                                    .url_acceso = ""}
            Perfil = TreeHelper.ObtenerInstancia.RecorrerArbol(Perfil, Nothing, TreeViewPermisos)
            If Perfil.Hijos.Count > 0 Then
                PermisoBLL.ObtenerInstancia.Crear(Perfil)
                'Grabar en bitacora 
                MostrarMensaje("Permiso creado!", TipoAlerta.Success)
            Else
                MostrarMensaje("No se seleccionaron permisos", TipoAlerta.Danger)
            End If
        Catch ex As Exception
            MostrarMensaje("No se pudo crear el permiso, intente nuevamente y en caso de persistir, contacte al administrador.", TipoAlerta.Danger)
        End Try
    End Sub



    Private Sub TreeViewPermisos_TreeNodeCheckChanged(sender As Object, e As TreeNodeEventArgs) Handles TreeViewPermisos.TreeNodeCheckChanged
        Try
            TreeHelper.ObtenerInstancia.CheckearNodosHijo(e.Node)
        Catch ex As Exception
            MostrarMensaje("No se pudo chequear el nodo, intente nuevamente y en caso de persistir, contacte al administrador.", TipoAlerta.Danger)
        End Try
    End Sub

    Protected Sub txtNombrePerfil_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class