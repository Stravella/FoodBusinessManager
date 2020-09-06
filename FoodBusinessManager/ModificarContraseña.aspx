<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="ModificarContraseña.aspx.vb" Inherits="FoodBusinessManager.ModificarContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="card">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="row">
                        <div class="col">
                            <center>
                                <h3><asp:Label ID="lblModificarContraseña" runat="server" Text="Modificar contraseña"></asp:Label></h3>
                            </center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <label>
                                <asp:Label ID="lblNuevaContraseña" runat="server" Text="Nueva contraseña"></asp:Label>
                            </label>
                            <div class="form-group">
                                <asp:TextBox ID="txtNuevaContraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqNuevaContraseña" runat="server" ErrorMessage="Ingrese su nueva contraseña" ControlToValidate="txtNuevaContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="cantNuevaContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtNuevaContraseña" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <label>
                                <asp:Label ID="lblConfirmeContraseña" runat="server" Text="Confirme contraseña"></asp:Label>
                            </label>
                            <div class="form-group">
                                <asp:TextBox ID="txtConfirmeContraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqConfirmeContraseña" runat="server" ErrorMessage="Confirme su nueva contraseña" ControlToValidate="txtConfirmeContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="valConfirmeContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtConfirmeContraseña" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnCambiarContraseña" CssClass="btn btn-success btn-block" runat="server" Text="Cambiar Contraseña" CausesValidation="true" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
