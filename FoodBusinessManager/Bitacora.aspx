<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Bitacora.aspx.vb" Inherits="FoodBusinessManager.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" runat="server">
    
        <div>
            <asp:DropDownList ID="lstUsuarios" runat="server">
            </asp:DropDownList>
        </div>
        <div>
            <asp:DropDownList ID="lstTipoSuceso" runat="server">
            </asp:DropDownList>
        </div>
        <div>
            <asp:Calendar ID="Calendar1" runat="server" SelectedDate="1990-01-01"></asp:Calendar>
            <br />
        </div>
        <div>
            <asp:Calendar ID="Calendar2" runat="server" SelectedDate="2020-01-24"></asp:Calendar>
            <br />
        </div>
        <div>
            <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" />
        </div>
        <div>
            <asp:Table ID="TablaBitacora" runat="server" Height="349px" Width="759px">
            </asp:Table>
            <br />
        </div>

        <div>
            <asp:Label ID="PagAnterior" runat="server" Text="Anterior"></asp:Label>
            
            <asp:Label ID="PagSiguiente" runat="server" Text="Siguiente"></asp:Label>
        </div>

    </form>
</asp:Content>
