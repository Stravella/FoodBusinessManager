Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class DesubscribirNewsletter
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Desubscribir()
        End If
    End Sub

    Protected Sub Desubscribir()
        Try
            Dim id As String = CriptografiaBLL.ObtenerInstancia.Desencriptar(Request.QueryString("idSubscriptor"))
            Dim subscriptor As SubscriptorDTO = SubscriptorBLL.ObtenerInstancia.Obtener(id)
            SubscriptorBLL.ObtenerInstancia.Desubscribir(subscriptor)
            SubscriptorBLL.ObtenerInstancia.Eliminar(subscriptor)
        Catch ex As Exception
        End Try
    End Sub

End Class