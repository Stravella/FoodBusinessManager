Imports Entidades
Imports BLL


Public Class Permisos
    Inherits System.Web.UI.Page
    Dim existe As Boolean


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarTree()
            CargarListaPerfiles()
            existe = False
            lblRespuesta.Visible = False
        End If
    End Sub

    'LLego solo hasta L2 
    Protected Sub CargarTree()
        TreeViewPermisos.Nodes.Clear()
        Dim ls As List(Of PermisoComponente)
        ls = PermisoBLL.ObtenerInstancia.Listar

        For Each permiso As PermisoComponente In ls
            Dim nodoPadre As New TreeNode
            nodoPadre.Value = permiso.id_permiso
            nodoPadre.Text = permiso.nombre
            If permiso.tieneHijos = True Then
                Dim perfil As PerfilCompuesto = permiso
                For Each permisoHijo As PermisoComponente In perfil.Hijos
                    Dim nodoHijo As New TreeNode
                    nodoHijo.Value = permisoHijo.id_permiso
                    nodoHijo.Text = permisoHijo.nombre
                    nodoPadre.ChildNodes.Add(nodoHijo)
                Next
            End If
            TreeViewPermisos.Nodes.Add(nodoPadre)
        Next

    End Sub

    Protected Sub CargarListaPerfiles()
        lstPerfiles.ClearSelection()
        Dim listaPerfiles As List(Of PermisoComponente) = PermisoBLL.ObtenerInstancia.ListarPerfiles
        For Each permiso As PermisoComponente In listaPerfiles
            Dim item As New ListItem
            item.Text = permiso.nombre
            item.Value = permiso.id_permiso
            lstPerfiles.Items.Add(item)
        Next
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

        'Validar que exista un nombre en el textbox y que este sea valido
        If txtNombrePerfil.Text Is Nothing Then
            lblRespuesta.Text = "Ingrese un nombre"
            lblRespuesta.Visible = True
        Else
            If ValidarNombre(TreeViewPermisos) = True Then
                lblRespuesta.Text = "El nombre ya existe"
                lblRespuesta.Visible = True
            Else 'El nombre es valido y puedo continuar
                Dim nuevoPerfil As New PerfilCompuesto With {.nombre = txtNombrePerfil.Text}
                nuevoPerfil = CrearPermiso(TreeViewPermisos.Nodes, nuevoPerfil)
                PermisoBLL.ObtenerInstancia.Crear(nuevoPerfil)
                CargarTree()
                lblRespuesta.Text = "Permiso creado exitosamente!"
                lblRespuesta.Visible = True
            End If
        End If

    End Sub

    Protected Function CrearPermiso(Nodos As TreeNodeCollection, unPermiso As PermisoComponente) As PermisoComponente

        For Each nodo As TreeNode In Nodos
            If nodo.Checked Then
                Dim newPermiso As PermisoComponente
                If nodo.ChildNodes.Count > 0 Then
                    newPermiso = New PerfilCompuesto With {.id_permiso = nodo.Value, .nombre = nodo.Text}
                    unPermiso.agregarHijo(newPermiso)
                    CrearPermiso(nodo.ChildNodes, newPermiso)
                Else
                    newPermiso = New PermisoHoja
                    newPermiso.id_permiso = nodo.Value
                    newPermiso.nombre = nodo.Text
                    unPermiso.agregarHijo(newPermiso)
                End If
            Else
                If nodo.ChildNodes.Count > 0 Then
                    CrearPermiso(nodo.ChildNodes, unPermiso)
                End If
            End If
        Next
        Return unPermiso

    End Function

    Protected Sub lstPerfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstPerfiles.SelectedIndexChanged
        TreeViewPermisos.CheckedNodes.Clear()
        Dim PerfilSeleccionado As String = lstPerfiles.SelectedItem.Text
        For Each node As TreeNode In TreeViewPermisos.Nodes
            If node.Text = PerfilSeleccionado Then
                node.Checked = True
                If node.ChildNodes.Count > 0 Then
                    For Each nodo As TreeNode In node.ChildNodes
                        nodo.Checked = True
                    Next
                End If
            End If

        Next

        btnAgregarPerfill.Text = "Modificar Perfil"

    End Sub

End Class