<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Default.aspx.vb" Inherits="FoodBusinessManager._Default" %>

<%@ Register Src="~/MyTable.ascx" TagName="WebControl" TagPrefix="TWebControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server"> 
        <TWebControl:WebControl runat="server" Rows="10" ID="table" style="height:100%" />
</asp:Content>
