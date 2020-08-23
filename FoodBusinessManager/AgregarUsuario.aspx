<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AgregarUsuario.aspx.vb" Inherits="FoodBusinessManager.AgregarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="lblAgregarUsuario" runat="server" Text="Agregar usuario"> </asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="valRequerido1" runat="server" ErrorMessage="*" ControlToValidate="txtNombre" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtApellido" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="valRequerido2" runat="server" ErrorMessage="*" ControlToValidate="txtApellido" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsuario" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="valRequerido3" runat="server" ErrorMessage="*" ControlToValidate="txtUsuario" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtContraseña" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="valRequerido4" runat="server" ErrorMessage="*" ControlToValidate="txtContraseña" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblMail" runat="server" Text="EMail"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtMail" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="valRequerido5" runat="server" ErrorMessage="*" ControlToValidate="txtMail" EnableClientScript="false" Display="Dynamic" CssClass="validador-requerido"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="valFormatoMail" runat="server" ErrorMessage="Email invalido" ControlToValidate="txtMail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" Display="Dynamic" CssClass="validador-requerido"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblPerfil" runat="server" Text="Perfil"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="lstPerfil" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblIdioma" runat="server" Text="Idioma"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="lstIdioma" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="chkTyC" Text=" Acepto términos y condiciones" runat="server" />
                                    <asp:HyperLink ID="linkTyC" runat="server" Text="Ver detalle" href="/TerminosYCondiciones.aspx"></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="chkNovedades" Text=" Acepto recibir novedades y newsletter" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-block btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
