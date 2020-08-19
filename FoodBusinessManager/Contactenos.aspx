<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Contactenos.aspx.vb" Inherits="FoodBusinessManager.Contactenos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h1>
        <asp:Label ID="lblTitContactenos" runat="server" Text="Contactenos"></asp:Label>
    </h1>
    <div class="container">
        <p>
            <asp:Label ID="lblAffiniti" runat="server" Text="Affiniti Solutions" Font-Bold="true"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblTelContacto" runat="server" Text="Telefono: 11-3256-3402"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblMailContacto" runat="server" Text="Mail: consultas@affiniti.com.ar"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblDireccionContacto" runat="server" Text="Buenos Aires, Capital Federal"></asp:Label>
        </p>
    </div>
</asp:Content>

