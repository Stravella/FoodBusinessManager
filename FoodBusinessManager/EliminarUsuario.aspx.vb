Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class EliminarUsuario
    Inherits System.Web.UI.Page
    Dim usuarios As List(Of UsuarioDTO)


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
        If Not IsPostBack() Then
            CargarUsuarios()
        End If
    End Sub

    Protected Sub CargarUsuarios()
        Try
            usuarios = UsuarioBLL.ObtenerInstancia.ListarUsuarios()
            Session("Usuarios") = usuarios
            lstUsuarios.DataSource = usuarios
            lstUsuarios.DataTextField = "username"
            lstUsuarios.DataValueField = "id"
            lstUsuarios.DataBind()
            lstUsuarios.SelectedIndex = 0
        Catch ex As Exception
            MostrarMensaje("Error al cargar usuarios", "Danger")
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            lblModalTitle.Text = "Confirme la operacion"
            lblModalBody.Text = "¿Está seguro que desea borrar el usuario?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            upModal.Update()
        Catch ex As Exception
            MostrarMensaje("Ocurrio algo con el modal!", "Danger")
        End Try
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs)
        Try
            Dim IdiomaActual As New IdiomaDTO
            If IsNothing(Current.Session("Cliente")) Then
                IdiomaActual.nombre = "Español"
            Else
                IdiomaActual.nombre = Application(TryCast(Current.Session("Cliente"), UsuarioDTO).idioma.nombre)
            End If
            usuarios = Session("Usuarios")
            Dim usuarioSeleccionado As UsuarioDTO = usuarios(lstUsuarios.SelectedIndex)
            UsuarioBLL.ObtenerInstancia.EliminarUsuario(usuarioSeleccionado)
            Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 9}, 'Suceso 9: Eliminar usuario
                                        .usuario = Current.Session("Cliente"),
                                        .ValorAnterior = usuarioSeleccionado.ToString,
                                        .NuevoValor = "",
                                        .observaciones = "Se elimino el usuario " & usuarioSeleccionado.username
                                        }
            BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('hide');", True)
            MostrarMensaje("Se ha eliminado el usuario", TipoAlerta.Success)
            Response.Redirect("EliminarUsuario.aspx")
        Catch ex As Exception
            Dim usuarioLogeado As UsuarioDTO = Current.Session("cliente")
            Dim usuarioSeleccionado As UsuarioDTO = usuarios(lstUsuarios.SelectedIndex)
            Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 9}, 'Suceso 9: Eliminar usuario
                                        .usuario = Current.Session("Cliente"),
                                        .ValorAnterior = usuarioSeleccionado.ToString,
                                        .NuevoValor = "",
                                        .observaciones = "Se elimino el usuario " & usuarioSeleccionado.username
                                        }
            Dim registroError As New BitacoraErroresDTO With {
                .excepcion = ex.Message,
                .stackTrace = ex.StackTrace}
            BitacoraBLL.ObtenerInstancia.AgregarError(registroBitacora, registroError)
            MostrarMensaje("Lo siento! Ocurrió un error inesperado", TipoAlerta.Danger)
        End Try
    End Sub
End Class