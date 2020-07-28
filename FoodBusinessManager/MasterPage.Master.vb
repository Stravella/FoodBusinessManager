Public Class MaterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "Menu"
    Public Sub ArmarMenu()
        Try

            'Me.Menu.Items.Add(New MenuItem("Home", "Home", Nothing, "/Default.aspx"))
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Traducciones"

#End Region

#Region "Perfiles y permisos"

#End Region


End Class