Public Class HtmlHelper
    Public Shared Function BuildTableProto(data As Integer) As String
        Dim stringbuilder As New StringBuilder
        stringbuilder.Append("<table>")
        For index = 1 To data
            stringbuilder.Append("<tr><td>").Append(index).Append("</td<td>").Append(data).Append("</td></tr>")
        Next
        stringbuilder.Append("</table>")
        Return stringbuilder.ToString()
    End Function

    Public Shared Function BuildTable(dt As DataTable) As String
        Dim stringbuilder As New StringBuilder

        stringbuilder.Append("<table>")
        'Header
        stringbuilder.Append("<tr>")
        For Each column As DataColumn In dt.Columns
            stringbuilder.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" _
                            + (column.ColumnName + "</th>")))
        Next
        stringbuilder.Append("</tr>")
        'Datos
        For Each row As DataRow In dt.Rows
            stringbuilder.Append("<tr>")
            For Each column As DataColumn In dt.Columns
                stringbuilder.Append(("<td style='width:100px;border: 1px solid #ccc'>" _
                                + (row(column.ColumnName).ToString + "</td>")))
            Next
            stringbuilder.Append("</tr>")
        Next
        stringbuilder.Append("</table>")

        Return stringbuilder.ToString()
    End Function

    Public Shared Function Paginate(Pager As Pager) As String
        Dim stringbuilder As New StringBuilder
        stringbuilder.Append("<div class='pagination'>")

        For i = 1 To Pager.CantidadPaginas
            stringbuilder.Append("<a href='#'>" + i + "</a>")
            If i > 9 Then
                stringbuilder.Append("<a href='#'> Siguiente </a>")
                Exit For
            End If
        Next

        stringbuilder.Append("</div>")
        Return stringbuilder.ToString
    End Function

End Class
