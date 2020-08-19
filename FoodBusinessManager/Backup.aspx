<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Backup.aspx.vb" Inherits="FoodBusinessManager.Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid">
        <br />
        <div class="row">
            <div class="col-sm-121">
                <h1>
                    <asp:Label ID="lblBackup" runat="server" Text="Backup" Font-Bold="true"></asp:Label>
                </h1>
            </div>
        </div>
        <div class="panel panel-info fondo-panel">
            <div class="col-5">
                <h5><asp:Label ID="lblGenereBackup" runat="server" Text="Genere un nuevo Backup"></asp:Label></h5>
                <asp:Label ID="lblDescripcionBackup" runat="server" Text="Seleccione para crear un Backup con el estado actual de la base de datos"></asp:Label>
                <div class="form-group">
                    <asp:Button ID="btnBackup" runat="server" Text="Backup" CssClass="btn btn-warning" />
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
