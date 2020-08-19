<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Registrarse1.aspx.vb" Inherits="FoodBusinessManager.Registrarse1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-8 mx-auto">

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img  width="100px" src="Imagenes/UserImage.png"/>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                     <h4>
                                         <asp:Label ID="lblRegistrarse" runat="server" Text="Registrarse"></asp:Label>                                       
                                     </h4>
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
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqNombre" runat="server" ErrorMessage="Ingrese su nombre" ControlToValidate="txtNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantNombre" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtNombre" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqApellido" runat="server" ErrorMessage="Ingrese su Apellido" ControlToValidate="txtApellido" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantApellido" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtApellido" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblMail" runat="server" Text="Mail"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtMail" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqMail" runat="server" ErrorMessage="Ingrese su mail" ControlToValidate="txtMail" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatMail" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtMail"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqUsuario" runat="server" ErrorMessage="Ingrese su usuario" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantUsuario" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqContraseña" runat="server" ErrorMessage="Ingrese su contraseña" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <%--Captcha--%>
<%--                        <div class="row">
                            <div class="col">
                                <div class="g-recaptcha" data-sitekey="6LdtOcAZAAAAABu0iB0KxhAR35_6zBp5zDaQ7Wjc">
                                </div>
                                <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                                <asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="Se requiere validar el captcha" ControlToValidate="txtCaptcha"
                                    runat="server" ForeColor="Red" Display="Dynamic" />
                            </div>
                        </div>--%>
                        <%--TyC--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="chkTyC" Text=" Acepto términos y condiciones" runat="server" />
                                    <asp:HyperLink ID="linkTyC" runat="server" Text="Ver detalle" href="/TerminosYCondiciones.aspx"></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <%--Novedades--%>
                        <div class="row">
                            <div class="col">
                                <div class="form-check">
                                    <asp:CheckBox ID="chkNovedades" Text=" Acepto recibir novedades y newsletter" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="btnRegistrarse" CssClass="btn btn-success btn-block" runat="server" Text="Registrarse" CausesValidation="true" />
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
