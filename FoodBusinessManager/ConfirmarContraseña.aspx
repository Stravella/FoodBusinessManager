<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="ConfirmarContraseña.aspx.vb" Inherits="FoodBusinessManager.ConfirmarContraseña" %>

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
                                    <asp:Label ID="titConfirmarContraseña" runat="server" Text="Confirmar contraseña"></asp:Label></h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblConfirmarContraseña" runat="server" Text="Re ingrese su contraseña"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtConfirmarContraseña" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqContraseña" runat="server" ErrorMessage="*" ControlToValidate="txtConfirmarContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtConfirmarContraseña" ForeColor="Red"></asp:RegularExpressionValidator>
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
                                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-block btn-success" CausesValidation="true" />
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
