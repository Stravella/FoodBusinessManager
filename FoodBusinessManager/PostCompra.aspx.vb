Imports BLL
Imports Entidades

Public Class PostCompra
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarOpiniones()
    End Sub

    Public Sub CargarOpiniones()
        Dim opiniones As List(Of EncuestaPreguntaDTO) = EncuestaPreguntaBLL.ObtenerInstancia.Listar
        rptPreguntas.DataSource = opiniones
    End Sub




End Class