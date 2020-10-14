Imports System.Web.HttpContext
Public Class PublicidadBack
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            cargarXML()
        End If
    End Sub


#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
        Response.Redirect("/PublicidadBack.aspx")
    End Sub

    Public Sub MostrarModal(titulo As String, body As String, Optional grd As GridView = Nothing, Optional cancelar As Boolean = False)
        Dim panelMensaje As UpdatePanel = Master.FindControl("Modal")
        Dim tituloModal As Label = panelMensaje.FindControl("lblModalTitle")
        Dim bodyModal As Label = panelMensaje.FindControl("lblModalBody")
        If grd IsNot Nothing Then
            Dim grillaModal As GridView = panelMensaje.FindControl("grilla")
            grillaModal = grd
            grillaModal.Visible = True
        End If
        If cancelar = True Then
            Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
            btnCancelar.Visible = True
        End If
        tituloModal.Text = titulo
        bodyModal.Text = body

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#myModal').modal();", True)
        panelMensaje.Update()
    End Sub

#End Region


    Sub cargarXML()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("id")
            dt.Columns.Add("url")
            dt.Columns.Add("imagenUrl")
            dt.Columns.Add("texto")

            Dim xmlDoc As XDocument = XDocument.Load(Server.MapPath("\Publicidad\Publicidad.xml"))
            Dim Consulta = From publicidad In xmlDoc.Descendants("Ad")
                           Select New With {
                        .url = publicidad.Element("NavigateUrl").Value,
                        .imagenUrl = publicidad.Element("ImageUrl").Value,
                        .texto = publicidad.Element("AlternateText").Value
                     }

            Dim id As Integer = 0
            For Each a In Consulta

                Dim drow As DataRow = dt.NewRow
                drow(0) = id
                drow(1) = a.url
                drow(2) = a.imagenUrl
                drow(3) = a.texto

                dt.Rows.Add(drow)
                id += 1
            Next
            Current.Session("dtXML") = dt
            gv_Publicidad.DataSource = dt
            gv_Publicidad.DataBind()
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

    Function guardarXML() As Boolean
        Try
            Dim xmlDoc As XDocument = XDocument.Load(Server.MapPath("\Publicidad\Publicidad.xml"))
            Dim xmlElement As XElement = xmlDoc.Element("Advertisements")
            xmlElement.RemoveNodes()
            Dim dt As DataTable = Session("dtXML")
            For Each a As DataRow In dt.Rows
                xmlDoc.Element("Advertisements").Add(New XElement("Ad", New XElement("NavigateUrl", a(1)),
                                     New XElement("ImageUrl", a(2)),
                                      New XElement("height", "391"),
                                      New XElement("width", "120"),
                                      New XElement("Keyword", "Computers"),
                                                             New XElement("Impressions", "2"),
                                     New XElement("AlternateText", a(3))))
            Next
            xmlDoc.Save(Server.MapPath("\Publicidad\Publicidad.xml"))
            cargarXML()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        txtURL.Text = ""
        txtImagen.Text = ""
        txtTexto.Text = ""
    End Sub



    Private Sub gv_Publicidad_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gv_Publicidad.RowDeleting
        Dim dt As DataTable = Session("dtXML")
        dt.Rows(e.RowIndex).Delete()
        Current.Session("dtXML") = dt
        guardarXML()
        Response.Redirect("/PublicidadBack.aspx")
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim dt As DataTable = Session("dtXML")
            Dim drow As DataRow = dt.NewRow
            drow(1) = txtURL.Text
            drow(2) = txtImagen.Text
            drow(3) = txtTexto.Text
            dt.Rows.Add(drow)
            Current.Session("dtXML") = dt
            guardarXML()
            txtURL.Text = ""
            txtImagen.Text = ""
            txtTexto.Text = ""
            Response.Redirect("/PublicidadBack.aspx")
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try
    End Sub

End Class