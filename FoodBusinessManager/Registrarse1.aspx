<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Registrarse1.aspx.vb" Inherits="FoodBusinessManager.Registrarse1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='https://www.google.com/recaptcha/api.js'></script>
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
                                    <asp:RequiredFieldValidator ID="reqNombre" runat="server" ErrorMessage="*" ControlToValidate="txtNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantNombre" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtNombre" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqApellido" runat="server" ErrorMessage="*" ControlToValidate="txtApellido" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="reqMail" runat="server" ErrorMessage="*" ControlToValidate="txtMail" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatMail" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtMail"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqUsuario" runat="server" ErrorMessage="*" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantUsuario" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqContraseña" runat="server" ErrorMessage="*" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblValidarContraseña" runat="server" Text="Confirme su contraseña"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtValidarContraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqValidarContraseña" runat="server" ErrorMessage="*" ControlToValidate="txtValidarContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="compValidarContraseña" runat="server" ControlToValidate="txtValidarContraseña" CssClass="ValidationError" ControlToCompare="txtContraseña" ErrorMessage="Las contraseñas no coinciden" ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="cantValidarContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblDNI" runat="server" Text="DNI"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDNI" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqDNI" runat="server" ErrorMessage="*" ControlToValidate="txtDNI" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatDNI" runat="server" ValidationExpression="[0-9]{8}" ErrorMessage="Formato invalido" ControlToValidate="txtDNI" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblCUIT" runat="server" Text="CUIT"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCUIT" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCUIT" runat="server" ErrorMessage="*" ControlToValidate="txtCUIT" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatCUIT" runat="server" ValidationExpression="\b(20|23|24|27|30|33|34)(\D)?[0-9]{8}(\D)?[0-9]" ErrorMessage="Formato invalido" ControlToValidate="txtCUIT" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblRazonSocial" runat="server" Text="Razon social"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtRazonSocial" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtRazonSocial" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^([\S\s]{0,100})$" ErrorMessage="Formato invalido" ControlToValidate="txtRazonSocial" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblDireccion" runat="server" Text="Direccion"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqDireccion" runat="server" ErrorMessage="*" ControlToValidate="txtDireccion" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqTelefono" runat="server" ErrorMessage="*" ControlToValidate="txtTelefono" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatTelefono" runat="server" ErrorMessage="Formato inválido" ValidationExpression="^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$" ControlToValidate="txtTelefono" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>
                                    <asp:Label ID="lblCP" runat="server" Text="Código postal"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCP" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCP" runat="server" ErrorMessage="Ingrese su código postal" ControlToValidate="txtCP" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatCP" runat="server" ErrorMessage="Formato inválido" ValidationExpression="\b[a-zA-Z0-9]{7}\b|\b[a-zA-Z0-9]{10}\b+" ControlToValidate="txtCP" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>
                                    <asp:Label ID="lblLocalidad" runat="server" Text="Localidad"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtLocalidad" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqLocalidad" runat="server" ErrorMessage="*" ControlToValidate="txtLocalidad" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="formatLocalidad" runat="server" ErrorMessage="Formato inválido" ValidationExpression="^[a-z][a-z\s]*$" ControlToValidate="txtLocalidad" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>
                                    <asp:Label ID="lblProvincia" runat="server" Text="Provincia"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlProvincias" DataValueField="id" DataTextField="provincia" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--Captcha--%>
                <div class="row">
                    <div class="col">
                        <div class="g-recaptcha" data-sitekey="6LcvwMQZAAAAAAOvIPVyx69d19GTttlCndXofLCU">
                        </div>
                    </div>
                </div>
                <%--TyC--%>
                <div class="row">
                    <div class="col">
                        <div class="form-check">
                            <asp:CheckBox ID="chkTyC" Text="Acepto términos y condiciones" runat="server" />
                            <asp:HyperLink ID="linkTyC" runat="server" Text="Ver detalle" href="/TerminosYCondiciones.aspx"></asp:HyperLink>
                        </div>
                    </div>
                </div>
                <%--Novedades--%>
                <div class="row">
                    <div class="col">
                        <div class="form-check">
                            <asp:CheckBox ID="chkPoliticas" Text=" Acepto politicas de seguridad" runat="server" />
                            <asp:HyperLink ID="linkPoliticas" runat="server" Text="Ver detalle" href="/PoliticasSeguridad.aspx"></asp:HyperLink>
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

</asp:Content>
