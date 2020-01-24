<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SeleccionarIdioma.aspx.vb" Inherits="FoodBusinessManager.SeleccionarIdioma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../Estilos/Principal.css" rel="stylesheet" />
    <form id="form1" runat="server">
        <asp:Label ID="lbl_IdiomaActual" CssClass="labels" runat="server" Text="Su idioma actual es : "></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="drop_ListaIdiomas" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btn_AplicarIdioma" CssClass="myButton" runat="server" Text="Button" />
    </form>
</asp:Content>
