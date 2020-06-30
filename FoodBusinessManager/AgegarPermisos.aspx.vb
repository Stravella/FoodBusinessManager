Imports System.Web.HttpContext
Imports Entidades
Imports BLL


Public Class Permisos
    Inherits System.Web.UI.Page
    Dim existe As Boolean
    Dim ls As List(Of PermisoComponente)
    Dim nodos As TreeNodeCollection

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
                IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If

            Dim Perfil As New PerfilCompuesto With {.nombre = txtNombrePerfil.Text}
            Perfil = TreeHelper.ObtenerInstancia.RecorrerArbol(Perfil, Nothing, TreeViewPermisos)
            If Perfil.Hijos.Count > 0 Then
                PermisoBLL.ObtenerInstancia.Crear(Perfil)
                'Grabar en bitacora 
                MostrarMensaje("Permiso creado!", "Success")
            Else
                MostrarMensaje("No se seleccionaron permisos", "Danger")
            End If
        Catch ex As Exception
            MostrarMensaje("No se pudo crear el permiso, intente nuevamente y en caso de persistir, contacte al administrador.", "Danger")
        End Try
    End Sub



    Private Sub TreeViewPermisos_TreeNodeCheckChanged(sender As Object, e As TreeNodeEventArgs) Handles TreeViewPermisos.TreeNodeCheckChanged
        Try
            TreeHelper.ObtenerInstancia.CheckearNodosHijo(e.Node)
        Catch ex As Exception
            MostrarMensaje("No se pudo chequear el nodo, intente nuevamente y en caso de persistir, contacte al administrador.", "Info")
        End Try
    End Sub

End Class