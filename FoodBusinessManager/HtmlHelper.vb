Public Class HtmlHelper
    Public Shared Function BuildTable(data As Integer) As String
        Dim stringbuilder As New StringBuilder
        stringbuilder.Append("<table>")
        For index = 1 To data
            stringbuilder.Append("<tr><td>").Append(index).Append("</td<td>").Append(data).Append("</td></tr>")
        Next
        stringbuilder.Append("</table>")
        Return stringbuilder.ToString()
    End Function
End Class
