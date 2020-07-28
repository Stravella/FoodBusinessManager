<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="SeleccionarIdioma.aspx.vb" Inherits="FoodBusinessManager.SeleccionarIdioma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    <div class="container-fluid">
            <br />
            <div class="row">
                <h1>
                    <asp:Label ID="lbl_SeleccionarIdioma" runat="server" Text="Seleccionar Idioma" CssClass="labels"></asp:Label>
                </h1>
            </div>
            <br />

            <div class="panel panel-info fondo-panel">
                <div class="form-group">
                    <div class="label-form">
                        <asp:Label ID="lblIdioma" runat="server" Text="Idioma"></asp:Label>
                    </div>
                    <div class="dropdown">
                        <asp:DropDownList ID="lstIdiomas" runat="server" AutoPostBack="true" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="form-control">
                    <asp:Button ID="btn_seleccionarIdioma" runat="server" Text="Seleccionar Idioma" CssClass="form-control btn btn-warning" />
                </div>
            </div>


    </div>
</asp:Content>
