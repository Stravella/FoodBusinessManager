Public Class HtmlHelper

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

    'Public Shared Function Paginate(Pager As Paginador) As String
    '    Dim stringbuilder As New StringBuilder
    '    stringbuilder.Append("<nav aria-label='Page navigation example'>")
    '    stringbuilder.Append("<ul class='pagination justify-content-center'>")
    '    stringbuilder.Append("<li class='page-item disabled'>
    '    <a class='page-link' href='#' tabindex='-1'>" + "Anterior" + "</a>
    '    </li>")

    '    For i = 1 To Pager.paginaFinal
    '        stringbuilder.Append("<li Class='page-item'><a class='page-link' href='#'>" + i.ToString + "</a></li>")
    '        If i > 9 Then
    '            stringbuilder.Append("<li class='page-item'>")
    '            stringbuilder.Append("<a class='page-link' href='#'>" + "Siguiente" + "</a>")
    '            stringbuilder.Append("</li")
    '            Exit For
    '        End If
    '    Next
    '    stringbuilder.Append("</ul>")
    '    stringbuilder.Append("</nav>")
    '    Return stringbuilder.ToString
    'End Function



End Class
