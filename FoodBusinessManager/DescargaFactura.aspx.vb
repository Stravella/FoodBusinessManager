Imports Entidades
Imports BLL

Public Class DescargaFactura
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Request.UrlReferrer IsNot Nothing AndAlso (Request.UrlReferrer.LocalPath = "/PostCompra.aspx" Or Request.UrlReferrer.LocalPath = "/MisMovimientos.aspx" _
                                                        Or Request.UrlReferrer.LocalPath = "/Mihistorial.aspx") Then
                Dim oGestorPdf As New GestorPDF
                Dim idFc As Integer = Request("Cr")
                Dim idNc As Integer = Request("Nc")

                Dim compra As CompraDTO = CompraBLL.ObtenerInstancia.Obtener(idFc)

                If Not IsNothing(idFc) AndAlso IsNumeric(idFc) AndAlso idFc <> 0 Then
                    oGestorPdf.ArmarPDF(Response, Server.MapPath("TemplateFactura.html"), compra)
                End If
                If Not IsNothing(idNc) AndAlso IsNumeric(idNc) AndAlso idNc <> 0 Then
                    Dim oNC As NotaCreditoDTO = NotasCreditoBLL.ObtenerInstancia.Obtener(idNc)
                    oGestorPdf.ArmarPDF(Response, Server.MapPath("TemplateFactura.html"), oNC)
                End If

            Else
                    Response.Redirect("Inicio.aspx")
            End If
        End If
    End Sub




End Class