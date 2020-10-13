<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Servicios.aspx.vb" Inherits="FoodBusinessManager.Servicios" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col">
                <center>
                    <h4>Catálogo</h4>
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <center>
                            <h4>Filtros</h4>
                        </center>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>Nombre</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <center>
                                            <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Caracteristica</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:DropDownList ID="lstCaracteristicas" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <label>Precio minimo:</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtPrecioMin" runat="server" ValidationGroup="Filtro"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPrecioMin" runat="server" ErrorMessage="El valor solo puede ser numerico" ForeColor="Red" ValidationExpression="^[0-9]\d*(,\d+)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <label>Precio máximo:</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtPrecioMax" runat="server" ValidationGroup="Filtro"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPrecioMax" runat="server" ForeColor="Red" ErrorMessage="El valor solo puede ser numerico" ValidationExpression="^[0-9]\d*(,\d+)?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnFiltrar" CssClass="btn btn-block btn-secondary" runat="server" Text="Filtrar" ValidationGroup="Filtro" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <br />
        <div class="row">
            <asp:Repeater ID="repeaterServicios" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card">
                            <div class="card-header">
                                <div class="row">
                                    <div class="col">
                                        <h4>
                                            <center>
                                                    <asp:Label ID="lblServicio" runat="server" Text='<%#Eval("nombre") %>'></asp:Label>
                                                </center>
                                        </h4>
                                        <center>
                                                <asp:Image ID="imgServicio" runat="server" height="150px" width="150px" ImageUrl='<%#Eval("Imagen.Img64") %>'></asp:Image>
                                            </center>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">
                                            <center>
                                                <h6>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                            </h6>
                                            </center>
                                        </div>
                                    </div>
                                    <asp:Repeater ID="repeaterCaracteristicas" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col">
                                                    <center>
                                                        <li>
                                                        <asp:Label ID="lblCaracteristica" runat="server" Text='<%#Eval("caracteristica") %>'></asp:Label>
                                                    </li>
                                                    </center>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="card-footer">
                                    <div class="row">
                                        <div class="col">
                                            <center>
                                                <asp:Label ID="lblPrecio" CssClass="text-success" runat="server" Font-Bold="true" > $<%#Eval("precio") %> </asp:Label>
                                            </center>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col">
                                            <center>
                                                <asp:CheckBox id="chkComparar" text=" Comparar" OnCheckedChanged="Check" CommandName='<%# Eval("id") %>' AutoPostBack="true" runat="server"></asp:CheckBox>
                                            </center>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col">
                                            <asp:Button ID="btnDetalle" CssClass="btn btn-block btn-info" CommandName="detalle" CommandArgument='<%# Eval("id") %>' runat="server" Text="Ver detalle" />
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col">
                                            <asp:Button ID="btnComprar" CssClass="btn btn-block btn-success" Visible="true" runat="server" Text="Comprar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <hr />
        <div class="row">
            <div class="col">
                <asp:Button ID="btnComparar" CssClass="btn btn-block btn-warning" Enabled="false" runat="server" Text="Comparar" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col">
                <asp:Button ID="btnCancelar" CssClass="btn btn-block btn-danger" Enabled="true" Visible="false" runat="server" Text="Cancelar" />
            </div>
        </div>
        <br />
    </div>
</asp:Content>
