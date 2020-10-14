<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Publicidad.aspx.vb" Inherits="FoodBusinessManager.Publicidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <asp:AdRotator AdvertisementFile="Publicidad/Publicidad.xml" runat="server" Width="391px" Height="120px"></asp:AdRotator>
    </div>   
</asp:Content>
