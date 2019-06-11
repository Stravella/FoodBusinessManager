<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Idiomas.aspx.vb" Inherits="FoodBusinessManager.Idiomas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:DropDownList ID="lstCulturas" runat="server">
        </asp:DropDownList>
        <br />
        <asp:GridView ID="grillaTraducciones" runat="server">
        </asp:GridView>
        <br />
        <asp:Label ID="lblRespuesta" runat="server" Text="Label"></asp:Label>
        <br />
    </form>
</asp:Content>
