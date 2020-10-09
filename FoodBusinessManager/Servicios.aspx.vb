Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class Servicios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim lsComparar As New List(Of ServicioDTO)
            CargarRepeater()
        End If
    End Sub

    Public Sub CargarRepeater()
        Dim servicios As List(Of ServicioDTO) = ServicioBLL.ObtenerInstancia.Listar
        repeaterServicios.DataSource = servicios
        repeaterServicios.DataBind()
    End Sub

    Private Sub repeaterServicios_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repeaterServicios.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim repeaterHijo As Repeater = TryCast(e.Item.FindControl("repeaterCaracteristicas"), Repeater)
            Dim servicio As ServicioDTO = TryCast(e.Item.DataItem, ServicioDTO)
            repeaterHijo.DataSource = servicio.caracteristicas
            repeaterHijo.DataBind()
        End If
    End Sub

    Protected Sub repeaterServicios_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles repeaterServicios.ItemCommand
        Dim id As Int16 = Integer.Parse(e.CommandArgument)
        Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(id)
        If e.CommandName = "chk" Then

        End If
        If e.CommandName = "detalle" Then
            Dim url As String = "VistaServicios.aspx?Serv=" & servicio.nombre
            Response.Redirect(url)
        End If
    End Sub


    Protected Sub Check(source As Object, e As EventArgs)
        Dim checkbox As CheckBox = TryCast(source, CheckBox)
        Dim id As Integer = checkbox.Attributes("CommandName")
        Dim servicio As ServicioDTO = ServicioBLL.ObtenerInstancia.Obtener(id)
        Dim lsComparar As New List(Of ServicioDTO)

        If Current.Session("lsComparar") IsNot Nothing Then
            lsComparar = Current.Session("lsComparar")
        End If

        If checkbox.Checked = True Then 'Agrego a la lista
            lsComparar.Add(servicio)
            Current.Session("lsComparar") = lsComparar
        Else 'Quito de la lista
            Dim nuevaLista As New List(Of ServicioDTO)
            For Each serv As ServicioDTO In lsComparar
                If serv.id <> servicio.id Then
                    nuevaLista.Add(serv)
                End If
            Next
            lsComparar = nuevaLista
            Current.Session("lsComparar") = lsComparar
        End If

        Select Case lsComparar.Count
            Case > 1
                btnComparar.Enabled = True
            Case Else
                btnComparar.Enabled = False
        End Select

    End Sub

    Private Sub btnComparar_Click(sender As Object, e As EventArgs) Handles btnComparar.Click
        Dim lsComparar As New List(Of ServicioDTO)

        If Current.Session("lsComparar") IsNot Nothing Then
            lsComparar = Current.Session("lsComparar")
        End If

        repeaterServicios.DataSource = lsComparar
        repeaterServicios.DataBind()

        btnComparar.Visible = False
        btnCancelar.Visible = True
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        CargarRepeater()
        Current.Session("lsComparar") = Nothing
        btnComparar.Visible = True
        btnComparar.Enabled = False
        btnCancelar.Visible = False
    End Sub
End Class