<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MyTable.ascx.vb" Inherits="FoodBusinessManager.MyTable" %>
<script runat="server">
' Public Rows As Integer
    </script>

<%For index = 1 To Rows %>
    <br />
    <a href="#"style="<%=style%>" ><%=index %></a>   
<%Next %>