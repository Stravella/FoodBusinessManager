Imports Entidades
Imports BLL
Imports SelectPdf
Imports System.IO
Imports System.Web

Public Class GestorPDF


    Public Sub ArmarPDF(ByVal rsp As HttpResponse, urlPlantilla As String, docMostrabale As Object)
        ' read parameters from the webpage
        Dim pdf_page_size As String = "A4"
        Dim pageSize As PdfPageSize = DirectCast([Enum].Parse(GetType(PdfPageSize), pdf_page_size, True), PdfPageSize)

        Dim pdf_orientation As String = "Portrait"
        Dim pdfOrientation As PdfPageOrientation = DirectCast([Enum].Parse(GetType(PdfPageOrientation), pdf_orientation, True), PdfPageOrientation)

        Dim webPageWidth As Integer = 1024

        Dim webPageHeight As Integer = 0

        ' instantiate a html to pdf converter object
        Dim converter As New HtmlToPdf()

        ' set converter options
        converter.Options.PdfPageSize = pageSize
        converter.Options.MarginTop = 15
        converter.Options.PdfPageOrientation = pdfOrientation
        converter.Options.WebPageWidth = webPageWidth
        converter.Options.WebPageHeight = webPageHeight

        Dim doc As New PdfDocument
        Dim tipoDoc As Type = docMostrabale.GetType

        If docMostrabale.GetType Is GetType(CompraDTO) Then
            Dim cuerpo As String = FacturaBLL.ObtenerInstancia.ArmarFacturaMail(urlPlantilla, docMostrabale)
            doc = converter.ConvertHtmlString(cuerpo)
        ElseIf docMostrabale.GetType Is GetType(NotaCreditoDTO) Then
            Dim cuerpo As String = NotasCreditoBLL.ObtenerInstancia.ArmarFacturaMail(docMostrabale, urlPlantilla)
            doc = converter.ConvertHtmlString(cuerpo)
        End If
        converter.Options.MaxPageLoadTime = 120
        Try
            doc.Save(rsp, False, "FBM.pdf")

            'en false te descarga el archivo!!!!
        Catch ex As Exception
        Finally
            doc.Close()
        End Try
    End Sub

    Public Function ArmardPDFADjunto(ByVal rsp As HttpResponse, urlPlantilla As String, docMostrabale As Object) As Byte()
        ' read parameters from the webpage
        Dim pdf_page_size As String = "A4"
        Dim pageSize As PdfPageSize = DirectCast([Enum].Parse(GetType(PdfPageSize), pdf_page_size, True), PdfPageSize)

        Dim pdf_orientation As String = "Portrait"
        Dim pdfOrientation As PdfPageOrientation = DirectCast([Enum].Parse(GetType(PdfPageOrientation), pdf_orientation, True), PdfPageOrientation)

        Dim webPageWidth As Integer = 1024

        Dim webPageHeight As Integer = 0

        ' instantiate a html to pdf converter object
        Dim converter As New HtmlToPdf()

        ' set converter options
        converter.Options.PdfPageSize = pageSize
        converter.Options.MarginTop = 15
        converter.Options.PdfPageOrientation = pdfOrientation
        converter.Options.WebPageWidth = webPageWidth
        converter.Options.WebPageHeight = webPageHeight

        Dim doc As New PdfDocument
        Dim tipoDoc As Type = docMostrabale.GetType

        If docMostrabale.GetType Is GetType(CompraDTO) Then
            Dim cuerpo As String = FacturaBLL.ObtenerInstancia.ArmarFacturaMail(urlPlantilla, docMostrabale)
            doc = converter.ConvertHtmlString(cuerpo)
        ElseIf docMostrabale.GetType Is GetType(NotaCreditoDTO) Then
            Dim cuerpo As String = NotasCreditoBLL.ObtenerInstancia.ArmarFacturaMail(docMostrabale, urlPlantilla)
            doc = converter.ConvertHtmlString(cuerpo)
        End If
        converter.Options.MaxPageLoadTime = 120
        Try
            Dim memoryStream As New MemoryStream()
            doc.Save(memoryStream)
            Dim bytes As Byte() = memoryStream.ToArray()
            memoryStream.Close()
            doc.Close()

            Return bytes
        Catch ex As Exception
        Finally
            doc.Close()
        End Try
    End Function


End Class
