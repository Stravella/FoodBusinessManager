<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AgregarUsuario.aspx.vb" Inherits="FoodBusinessManager.AgregarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h1>
                    <asp:Label ID="lbl_AgregarUsuario" runat="server" Text="Agregar usuario"> </asp:Label>
                </h1>
            </div>
        </div>
        <br />

        <div class="panel panel-info fondo-panel">
            <div class="form-group">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido1" runat="server" ErrorMessage="*" ControlToValidate="txtNombre" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtApellido" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido2" runat="server" ErrorMessage="*" ControlToValidate="txtApellido" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtUsuario" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido3" runat="server" ErrorMessage="*" ControlToValidate="txtUsuario" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtContraseña" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido4" runat="server" ErrorMessage="*" ControlToValidate="txtContraseña" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblMail" runat="server" Text="EMail"></asp:Label>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtMail" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido5" runat="server" ErrorMessage="*" ControlToValidate="txtMail" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-1">
                    <asp:RegularExpressionValidator ID="valFormatoMail" runat="server" ErrorMessage="Email invalido" ControlToValidate="txtMail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" CssClass="validador-requerido"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPerfil" runat="server" Text="Perfil"></asp:Label>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:ListBox ID="lstPerfil" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblIdioma" runat="server" Text="Idioma"></asp:Label>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:ListBox ID="lstIdioma" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="form-group">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-bloc btn-success" />
                    </div>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
