<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="ModificarUsuario.aspx.vb" Inherits="FoodBusinessManager.ModificarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h1>
                    <asp:Label ID="lblModificarUsuario" runat="server" Text="Modificar usuario"> </asp:Label>
                </h1>
            </div>
        </div>
        <br />

        <div class="panel panel-info fondo-panel">
            <div class="form-group label-form">
                <asp:Label ID="lblUsuarios" runat="server" Text="Usuarios"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="dropdown">
                    <asp:ListBox ID="lstUsuarios" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox>
                </div>
            </div>
        </div>
        <br />

        <div class="fondo panel-info fondo-panel">
            <div class="form-group">
                <div class="form-group label-form">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                </div>
                <div class="col-md-12">
                    <div class="input-group">
                        <asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido1" runat="server" ErrorMessage="*" ControlToValidate="txtNombre" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="form-group label-form">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                </div>
                <div class="col-md-12">
                    <div class="input-group">
                        <asp:TextBox ID="txtApellido" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido2" runat="server" ErrorMessage="*" ControlToValidate="txtApellido" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="form-group label-form">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                </div>
                <div class="col-md-12">
                    <div class="input-group">
                        <asp:TextBox ID="txtUsuario" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido3" runat="server" ErrorMessage="*" ControlToValidate="txtUsuario" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="form-group label-form">
                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
                </div>
                <div class="col-md-12">
                    <div class="input-group">
                        <asp:TextBox ID="txtContraseña" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="valRequerido4" runat="server" ErrorMessage="*" ControlToValidate="txtContraseña" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="form-group label-form">
                    <asp:Label ID="lblMail" runat="server" Text="EMail"></asp:Label>
                </div>
                <div class="col-md-12">
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

            <div class="form-group label-form">
                <asp:Label ID="lblPerfil" runat="server" Text="Perfil"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="dropdown">
                    <asp:ListBox ID="lstPerfil" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox>
                </div>
            </div>

            <div class="form-group label-form">
                <asp:Label ID="lblIdioma" runat="server" Text="Idioma"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="dropdown">
                    <asp:ListBox ID="lstIdioma" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox>
                </div>
            </div>
            <br />
            <div class="form-check">
                <asp:CheckBox ID="chkBloqueado" CssClass="form-check-input" Text="Bloqueado" runat="server"/>             
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="form-group">
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-block btn-warning" />
            </div>
        </div>
    </div>
</asp:Content>
