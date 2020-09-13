Public Class Modal
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Event ClickPrincipal(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ClickSecundario(ByVal sender As Object, ByVal e As EventArgs)

    Public Property Titulo() As String

        Set(ByVal value As String)
            tituloModal.InnerText = value
        End Set
        Get
            Return tituloModal.InnerText
        End Get
    End Property

    Public Property CuerpoMensaje() As String

        Set(ByVal value As String)
            BodyModal.InnerText = value
        End Set
        Get
            Return BodyModal.InnerText
        End Get
    End Property

    Public Property LabelBtnPrincipal() As String

        Set(ByVal value As String)
            btnPrincipal.Text = value
        End Set
        Get
            Return btnPrincipal.Text
        End Get
    End Property
    Public Property LabelBtnSecundario() As String

        Set(ByVal value As String)
            btnSecundario.Text = value
        End Set
        Get
            Return btnSecundario.Text
        End Get
    End Property

    Public Sub Mostrar(ByVal cuerpo As String, Optional titulo As String = Nothing, Optional mostrarCancelar As Boolean = False)
        titulo = IIf(titulo Is Nothing, "FBM", titulo)
        CuerpoMensaje = cuerpo
        btnSecundario.Visible = mostrarCancelar
        Page.ClientScript.RegisterStartupScript(Me.GetType, "openModal", "window.onload = function() { $('#ucMensajeModal').modal('show'); }", True)
    End Sub
    Public Sub MostrarScript(ByVal cuerpo As String, ByVal updtPanel As UpdatePanel, Optional titulo As String = Nothing, Optional mostrarCancelar As Boolean = False)
        titulo = IIf(titulo Is Nothing, "FBM", titulo)
        CuerpoMensaje = cuerpo
        btnSecundario.Visible = mostrarCancelar
        ScriptManager.RegisterStartupScript(updtPanel, updtPanel.GetType(), "show", "$(function () { $('#ucMensajeModal').modal('show'); });", True)
    End Sub

    Private Sub btnPrincipal_Click(sender As Object, e As EventArgs) Handles btnPrincipal.Click
        RaiseEvent ClickPrincipal(Me, e)
    End Sub

    Private Sub btnSecundario_Click(sender As Object, e As EventArgs) Handles btnSecundario.Click
        RaiseEvent ClickSecundario(Me, e)
    End Sub

End Class