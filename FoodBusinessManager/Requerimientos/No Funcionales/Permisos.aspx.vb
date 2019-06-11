Imports Entidades
Imports BLL

Public Class Permisos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarTree()
        End If
    End Sub

    'LLego solo hasta L2 
    Protected Sub CargarTree()
        Dim ls As List(Of PermisoComponente)
        ls = PermisoBLL.ObtenerInstancia.Listar

        'For Each permiso As PermisoComponente In ls
        '    TreeView.Nodes.Add(CargarNode(permiso))
        'Next

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

    'Protected Function CargarNode(permiso As PerfilCompuesto) As TreeNode
    '    Try
    '        Dim nodo As New TreeNode
    '        nodo.Value = permiso.nombre
    '        If permiso.tieneHijos = True Then
    '            For Each hijo As PermisoComponente In permiso.Hijos
    '                nodo.ChildNodes.Add(CargarNode(hijo))
    '            Next
    '            Return nodo
    '        Else
    '            Return nodo
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Function


End Class