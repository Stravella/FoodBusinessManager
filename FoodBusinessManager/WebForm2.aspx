<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="WebForm2.aspx.vb" Inherits="FoodBusinessManager.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:CheckBox ID="CheckBox1" runat="server" />
        <asp:Button ID="Button2" runat="server" Text="Button" />
    </div>
</asp:Content>
