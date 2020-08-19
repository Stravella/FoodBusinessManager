<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Restore.aspx.vb" Inherits="FoodBusinessManager.Restore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid">
        <br />
        <div class="row">
            <div class="col-sm-121">
                <h1>
                    <asp:Label ID="lblRestore" runat="server" Text="Restore" Font-Bold="true"></asp:Label>
                </h1>
            </div>
        </div>
        <br />
        <div class="panel panel-info fondo-panel">
            <div class="form-control">
                <asp:TextBox ID="btnRestore" CssClass="btn btn-warning" Text="Restore" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
