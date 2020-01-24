Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session.Add("test", {"1", "2", "3"})
        'TODO: Armar una clase para cada tipo de mensaje (acordarse de borrarlo)
        'Session.Add("test2", New Entidades.IdiomaDTO With {.id_idioma = "es-AR"})
        Me.table.Rows = 3
        table.style = "width:100%"
    End Sub

End Class