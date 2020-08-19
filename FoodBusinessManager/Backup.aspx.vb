Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class Backup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class