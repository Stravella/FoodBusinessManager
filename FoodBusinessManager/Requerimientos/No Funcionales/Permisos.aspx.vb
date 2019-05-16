Imports Entidades
Imports BLL

Public Class Permisos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarTree()
        End If
    End Sub

    Protected Sub CargarTree()
        Dim ls As List(Of PermisoComponente)
        ls = PermisoBLL.ObtenerInstancia.Listar

        For Each permiso As PermisoComponente In ls
            Dim nodoPadre As New TreeNode
            nodoPadre.Value = permiso.nombre
            If permiso.tieneHijos = True Then
                Dim perfil As PerfilCompuesto = permiso
                For Each permisoHijo As PermisoComponente In perfil.Hijos
                    Dim nodoHijo As New TreeNode
                    nodoHijo.Value = permisoHijo.nombre
                    nodoPadre.ChildNodes.Add(nodoHijo)
                Next
            End If
            TreeView.Nodes.Add(nodoPadre)
        Next

    End Sub


End Class