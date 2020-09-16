Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class Servicios1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
End Class