Imports BLL
Imports Entidades
Imports System.Web.HttpContext
Public Class Maestra
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Current.Session("Cliente")) Then
            Dim usuarioLogeado As UsuarioDTO = Current.Session("Cliente")
            CargarPerfil(usuarioLogeado)
            TraducirPagina(usuarioLogeado.idioma)
        End If
    End Sub

#Region "Menu"
    Public Sub ArmarMenu()
        Try
            Me.menu.Items.Add(New MenuItem("Administración del Sistema", "AdminSist"))
            Me.menu.Items.Item(0).ChildItems.Add(New MenuItem("Copia de Seguridad", "Backup", Nothing, "/Backup.aspx"))
            Me.menu.Items.Item(0).ChildItems.Add(New MenuItem("Restauración de Datos", "Restore", Nothing, "/Restore.aspx"))
            Me.menu.Items.Item(0).ChildItems.Add(New MenuItem("Visualizar Bitacora Auditoria", "BitacoraAuditoria", Nothing, "/Bitacora.aspx"))
            Me.menu.Items.Item(0).ChildItems.Add(New MenuItem("Visualizar Bitacora Errores", "BitacoraErrores", Nothing, "/BitacoraErrores.aspx"))
            Me.menu.Items.Add(New MenuItem("Administración Usuarios", "AdminUsu"))
            Me.menu.Items.Item(1).ChildItems.Add(New MenuItem("Agregar Usuario", "AgregarUsuario", Nothing, "/AgregarUsuario.aspx"))
            Me.menu.Items.Item(1).ChildItems.Add(New MenuItem("Modificar Usuario", "ModificarUsuario", Nothing, "/ModificarUsuario.aspx"))
            Me.menu.Items.Item(1).ChildItems.Add(New MenuItem("Eliminar Usuario", "EliminarUsuario", Nothing, "/EliminarUsuario.aspx"))
            Me.menu.Items.Add(New MenuItem("Administración Perfiles", "AdminPer"))
            Me.menu.Items.Item(2).ChildItems.Add(New MenuItem("Crear Perfil", "CrearPerfil", Nothing, "/AgregarPerfil.aspx"))
            Me.menu.Items.Item(2).ChildItems.Add(New MenuItem("Modificar Perfil", "ModificarPerfil", Nothing, "/ModificarPerfil.aspx"))
            Me.menu.Items.Item(2).ChildItems.Add(New MenuItem("Eliminar Perfil", "EliminarPerfil", Nothing, "/EliminarPerfil.aspx"))
            Me.menu.Items.Add(New MenuItem("Administración Idiomas", "AdminIdi"))
            Me.menu.Items.Item(3).ChildItems.Add(New MenuItem("Crear Idioma", "AgregarIdioma", Nothing, "/AgregarIdioma.aspx"))
            Me.menu.Items.Item(3).ChildItems.Add(New MenuItem("Modificar Idioma", "ModificarIdioma", Nothing, "/ModificarIdioma.aspx"))
            Me.menu.Items.Item(3).ChildItems.Add(New MenuItem("Seleccionar Idioma", "SeleccionarIdioma", Nothing, "/SeleccionarIdioma.aspx"))
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Traducciones"
    Private Sub Traducir(ByVal menuItem As MenuItem, ByVal idioma As IdiomaDTO)
        Try
            Dim etiquetas As List(Of IdiomaEtiquetaDTO) = idioma.ListaEtiquetas
            Dim etiquetaAEncontrar As IdiomaEtiquetaDTO = etiquetas.Find(Function(p) p.etiqueta = menuItem.Value)
            If Not IsNothing(etiquetaAEncontrar) Then
                menuItem.Text = etiquetaAEncontrar.traduccion
            End If
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 4}, 'Suceso: Error de sistema
                .observaciones = "Error : " & menuItem.Text & menuItem.Value
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
        End Try
    End Sub

    Private Sub Traducir(ByVal button As Button, ByVal idioma As IdiomaDTO)
        Try
            Dim etiquetas As List(Of IdiomaEtiquetaDTO) = idioma.ListaEtiquetas
            Dim etiquetaAEncontrar As IdiomaEtiquetaDTO = etiquetas.Find(Function(p) p.etiqueta = button.ID)
            If Not IsNothing(etiquetaAEncontrar) Then
                button.Text = etiquetaAEncontrar.traduccion
            End If
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 4}, 'Suceso: Error de sistema
                .observaciones = "Error : " & button.Text & button.ID
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            'TODO: Mostrar mensaje error
        End Try
    End Sub


    Private Sub Traducir(ByVal label As Label, ByVal idioma As IdiomaDTO)
        Try
            Dim etiquetas As List(Of IdiomaEtiquetaDTO) = idioma.ListaEtiquetas
            Dim etiquetaAEncontrar As IdiomaEtiquetaDTO = etiquetas.Find(Function(p) p.etiqueta = label.ID)
            If Not IsNothing(etiquetaAEncontrar) Then
                label.Text = etiquetaAEncontrar.traduccion
            End If
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 4}, 'Suceso: Error de sistema
                .observaciones = "Error : " & label.Text & label.ID
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            'TODO: Mostrar mensaje error
        End Try
    End Sub

    Private Sub TraducirMenu(ByVal idioma As IdiomaDTO)
        Try
            Dim menu As Menu = Me.FindControl("Menu")
            If menu.Items.Count > 0 Then
                TraducirSubMenu(menu.Items, idioma)
            End If
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 4}, 'Suceso: Error de sistema
                .observaciones = "Error traduciendo menu "
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            'TODO: Mostrar mensaje error
        End Try
    End Sub

    Private Sub TraducirSubMenu(ByVal items As MenuItemCollection, ByVal idioma As IdiomaDTO)
        Try
            For Each item As MenuItem In items
                Me.Traducir(item, idioma)
                If item.ChildItems.Count > 0 Then
                    TraducirSubMenu(item.ChildItems, idioma)
                End If
            Next
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 4}, 'Suceso: Error de sistema
                .observaciones = "Error traduciendo sub menu "
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            'TODO: Mostrar mensaje error
        End Try
    End Sub

    Private Sub TraducirControl(ByVal listaControles As ControlCollection, ByVal idioma As IdiomaDTO)
        Try
            For Each control As Control In listaControles
                If TypeOf control Is Button Then
                    Traducir(DirectCast(control, Button), idioma)
                ElseIf TypeOf control Is Label Then
                    Traducir(DirectCast(control, Label), idioma)
                ElseIf TypeOf control Is GridView Then
                    Dim controlGridView As GridView = DirectCast(control, GridView)
                    For Each label In controlGridView.BottomPagerRow.Cells(0).Controls
                        Traducir(DirectCast(label, Label), idioma)
                    Next
                End If
            Next
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 4}, 'Suceso: Error de sistema
                .observaciones = "Error traduciendo controles "
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            'TODO: Mostrar mensaje error
        End Try
    End Sub


    Protected Sub TraducirPagina(ByRef idioma As IdiomaDTO)
        Try
            Dim pagina As String = Right(Request.Path, Len(Request.Path) - 1)
            Me.TraducirMenu(idioma)
            Dim contenido As New ContentPlaceHolder
            TraducirControl(contenido.Controls, idioma)
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim registroBitacora As New BitacoraDTO With {
                .FechaHora = Now(),
                .usuario = usuarioLogeado,
                .tipoSuceso = New SucesoBitacoraDTO With {.id = 4}, 'Suceso: Error de sistema
                .observaciones = "Error traduciendo pagina "
            }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            'TODO: Mostrar mensaje error
        End Try
    End Sub

#End Region

#Region "Perfiles y permisos"
    Private Sub CargarPerfil(usuario As UsuarioDTO)
        Try
            Me.menu.Items.Clear()
            ArmarMenu()
            'Armo la lista de paginas a remover del menu
            Dim listaaRemover As New List(Of MenuItem)
            For Each pagina As MenuItem In menu.Items
                If pagina.ChildItems.Count > 0 Then
                    RecorrerMenu(pagina, usuario, listaaRemover)
                Else
                    If usuario.perfil.PuedeUsar(pagina.NavigateUrl) = False Then
                        listaaRemover.Add(pagina)
                    End If
                End If
            Next
            'Remuevo las paginas del menu
            For Each item As MenuItem In listaaRemover
                menu.Items.Remove(item)
                For Each subnivel As MenuItem In menu.Items
                    subnivel.ChildItems.Remove(item)
                Next
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RecorrerMenu(pagina As MenuItem, Usuario As UsuarioDTO, listaaRemover As List(Of MenuItem))
        Dim flag As Integer = 0
        For Each subpagina As MenuItem In pagina.ChildItems
            If subpagina.ChildItems.Count > 0 Then
                RecorrerMenu(subpagina, Usuario, listaaRemover)
            Else
                If Usuario.perfil.PuedeUsar(subpagina.NavigateUrl) = False Then
                    listaaRemover.Add(subpagina)
                    flag += 1
                End If
            End If
        Next
        'Si no puedo usar ningun item, no muestro el menu
        If flag = pagina.ChildItems.Count Then
            listaaRemover.Add(pagina)
        End If
    End Sub

#End Region



End Class