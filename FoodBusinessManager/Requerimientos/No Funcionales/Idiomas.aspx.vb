Imports System.Globalization
Imports System.Web.HttpContext

Public Class Idiomas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarCulturas()
        CargarGrilla()
    End Sub


    Private Sub CargarCulturas()
        Dim culturas = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures)
        For i As Integer = 1 To culturas.Length - 1
            Dim item As New ListItem(culturas(i).Name, culturas(i).NativeName)
            Me.lstCulturas.Items.Add(item)
        Next
    End Sub


    Public Sub CargarGrilla()
        Dim IdiomaActual As Entidades.IdiomaDTO
        If IsNothing(Current.Session("Cliente")) Then
            IdiomaActual = Application("Español")
        Else
            IdiomaActual = Application(TryCast(Current.Session("Cliente"), Entidades.UsuarioDTO).idioma.nombre)
        End If

        If IsNothing(IdiomaActual.ListaEtiquetas) Then
            Me.lblRespuesta.Visible = True
            Me.grillaTraducciones.DataSource = IdiomaActual.ListaEtiquetas
            Me.grillaTraducciones.DataBind()
        Else
            Me.lblRespuesta.Visible = False
            Me.grillaTraducciones.DataSource = IdiomaActual.ListaEtiquetas
            Me.grillaTraducciones.DataBind()
        End If

        'IdiomaActual = Application(TryCast(Current.Session("Cliente"), Entidades.UsuarioDTO).idioma.nombre)

        'grillaTraducciones.DataSource = IdiomaActual.ListaEtiquetas
        'grillaTraducciones.DataBind()

    End Sub

End Class