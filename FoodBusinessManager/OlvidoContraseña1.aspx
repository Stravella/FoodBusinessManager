<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="OlvidoContraseña1.aspx.vb" Inherits="FoodBusinessManager.OlvidoContraseña1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="row">
                        <div class="col">
                            <center>
                                <h3>
                                    <asp:Label ID="lblCambiarContraseña" runat="server" Text="Cambiar contraseña"></asp:Label>
                                </h3>
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
                            <center>
                                <asp:Label ID="lblDescripcionCambioContraseña" runat="server" Text="Vamos a enviarle un mail para que pueda cambiar su contraseña"></asp:Label>
                            </center>                          
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
                            <div class="col">
                                <asp:Button ID="btnEnviarMail" CssClass="btn btn-success btn-block" runat="server" Text="Enviar mail" CausesValidation="true" />
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
