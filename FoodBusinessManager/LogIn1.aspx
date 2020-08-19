<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="LogIn1.aspx.vb" Inherits="FoodBusinessManager.LogIn1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-6 mx-auto">

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img  width="150px" src="Imagenes/UserImage.png"/>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                     <h3>
                                         <asp:Label ID="lblLoginUsuario" runat="server" Text="Login usuario"></asp:Label>                                       
                                     </h3>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                     <hr />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqUsuario" runat="server" ErrorMessage="Ingrese su usuario" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantUsuario" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                                <label>
                                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqContraseña" runat="server" ErrorMessage="Ingrese su contraseña" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnLogin" CssClass="btn btn-success btn-block" runat="server" Text="Login" CausesValidation="true"/>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnRegistrarse" CssClass="btn btn-info btn-block" runat="server" Text="Registrarse" CausesValidation="false" />
                                </div>
                                <asp:HyperLink ID="linkOlvidoContraseña" runat="server" href="/OlvidoContraseña1.aspx" Text="¿Olvido su contraseña?"></asp:HyperLink>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
