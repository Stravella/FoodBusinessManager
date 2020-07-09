Imports Entidades

'Helper para recorrer arboles y armar nodos
Public Class TreeHelper

#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As TreeHelper
    Public Shared Function ObtenerInstancia() As TreeHelper
        If _instancia Is Nothing Then
            _instancia = New TreeHelper
        End If
        Return _instancia
    End Function
#End Region

    Public Sub CargarPefil(ByRef arbol As TreeView, Optional Perfil As PerfilCompuesto = Nothing)
        Try
            If arbol.Nodes.Count = 0 Then
                If IsNothing(Perfil) Then
                    Dim ls = BLL.PermisoBLL.ObtenerInstancia.Listar()
                    ArmarNodos(ls, Nothing, arbol)
                    arbol.CollapseAll()
                Else
                    ArmarNodos(Perfil.Hijos, Nothing, arbol)
                    arbol.CollapseAll()
                End If
            Else
                If IsNothing(Perfil) Then
                    arbol.Nodes.Clear()
                    CargarPefil(arbol)
                Else
                    arbol.Nodes.Clear()
                    CargarPefil(arbol, Perfil)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ArmarNodos(permisos As List(Of PermisoComponente), nodos As TreeNode, arbol As TreeView)
        If IsNothing(nodos) Then
            For Each permiso As PermisoComponente In permisos
                Dim nodoPadre As New TreeNode With {.Value = permiso.id_permiso, .Text = permiso.nombre}
                arbol.Nodes.Add(nodoPadre)
                If permiso.tieneHijos = True Then
                    Dim perfilCompuesto As PerfilCompuesto = DirectCast(permiso, PerfilCompuesto)
                    ArmarNodos(perfilCompuesto.Hijos, nodoPadre, arbol)
                End If
            Next
        Else
            For Each permiso2 As PermisoComponente In permisos
                Dim nodoHijo As New TreeNode With {.Value = permiso2.id_permiso, .Text = permiso2.nombre}
                nodos.ChildNodes.Add(nodoHijo)
                If permiso2.tieneHijos = True Then
                    Dim perfilCompuesto As PerfilCompuesto = DirectCast(permiso2, PerfilCompuesto)
                    ArmarNodos(perfilCompuesto.Hijos, nodoHijo, arbol)
                End If
            Next
        End If
    End Sub

    Public Sub CheckearNodosHijo(n As TreeNode)
        Try
            QuitarCheckPadre(n)
            For Each subNodo As TreeNode In n.ChildNodes
                subNodo.Checked = n.Checked
                CheckearNodosHijo(subNodo)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub QuitarCheckPadre(n As TreeNode)
        Try
            If n.Checked = False AndAlso n.Parent IsNot Nothing Then
                n.Parent.Checked = False
                QuitarCheckPadre(n.Parent)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Me voy por overflow acá
    Public Function RecorrerArbol(perfil As PermisoComponente, nodos As TreeNode, arbol As TreeView) As PermisoComponente
        If IsNothing(nodos) Then
            For Each n As TreeNode In arbol.Nodes
                If n.Checked = True Then
                    If n.ChildNodes.Count > 0 Then
                        Dim p As New PerfilCompuesto With {.id_permiso = n.Value, .nombre = n.Text}
                        RecorrerArbol(p, n, arbol)
                        If Not perfil.esValido(p.nombre) Then
                            perfil.agregarHijo(p)
                        Else
                            p.Hijos.Clear()
                        End If
                    Else
                        Dim P As New PermisoHoja With {.id_permiso = n.Value, .nombre = n.Text}
                        If Not P.esValido(perfil.nombre) Then
                            perfil.agregarHijo(P)
                        End If
                    End If
                Else
                    RecorrerArbol(perfil, n, arbol)
                End If
            Next
        Else
            For Each n2 As TreeNode In nodos.ChildNodes
                If n2.Checked = True Then
                    If n2.ChildNodes.Count <> 0 Then
                        Dim p As New PerfilCompuesto With {.id_permiso = n2.Value, .nombre = n2.Text}
                        If Not p.esValido(perfil.nombre) Then
                            RecorrerArbol(p, n2, arbol)
                            perfil.agregarHijo(p)
                        Else
                            RecorrerArbol(p, n2, arbol)
                            p.Hijos.Clear()
                        End If
                    Else
                        Dim p As New PermisoHoja With {.id_permiso = n2.Value, .nombre = n2.Text}
                        If Not p.esValido(perfil.nombre) Then
                            perfil.agregarHijo(p)
                        End If
                    End If
                Else
                    If n2.ChildNodes.Count <> 0 Then 'deberia ser 0 en los hijos si nodos
                        RecorrerArbol(perfil, n2, arbol)
                    End If
                End If
            Next
        End If
        Return perfil
    End Function


    Public Sub CheckearTodos(nodos As TreeNode)
        Try
            For Each nodo As TreeNode In nodos.ChildNodes
                nodo.Checked = True
                If nodo.ChildNodes.Count > 0 Then
                    CheckearTodos(nodo)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub



End Class
