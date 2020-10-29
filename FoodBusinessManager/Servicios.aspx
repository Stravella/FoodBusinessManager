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
                            <div class="col-12">
                                <label>Nombre</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
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
                        <div class="card h-auto">
                            <div class="card-header text-center h-50">
                                <div class="row">
                                    <div class="col">
                                        <h4><%#Eval("nombre") %></h4>
                                        <center><asp:Image ID="imgServicio" runat="server" height="150px" width="150px" ImageUrl='<%#Eval("Imagen.Img64") %>'></asp:Image></center>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body h-100">
                                <div class="row">
                                    <div class="col">
                                        <h6><%#Eval("descripcion") %></h6>
                                    </div>
                                </div>
                                <asp:Repeater ID="repeaterCaracteristicas" runat="server">
                                    <ItemTemplate>
                                        <li><%#Eval("caracteristica") %></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="card-footer h-50">
                                <p class="text-muted">$<%#Eval("precio") %></p>
                                <asp:CheckBox ID="chkComparar" Text=" Comparar" OnCheckedChanged="Check" CommandName='<%# Eval("id") %>' AutoPostBack="true" runat="server"></asp:CheckBox>
                                <asp:Button ID="btnDetalle" CssClass="btn btn-block btn-info" CommandName="detalle" CommandArgument='<%# Eval("id") %>' runat="server" Text="Ver detalle" />
                                <asp:Button ID="btnComprar" CssClass="btn btn-block btn-success" CommandName="comprar" CommandArgument='<%# Eval("id") %>' Visible="true" runat="server" Text="Agregar al carrito" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <%-- Probe con card-decks. Me alinea el height de la card, no me pega el footer al fondo ni me centra las cards --%>
        <%--<div class="card-deck">
                <asp:Repeater ID="repeaterServicios" runat="server">
                    <ItemTemplate>
                        <div class="card text-center h-100 d-flex flex-column justify-content-between">
                            <center><asp:Image ID="imgServicio" CssClass="card-img-top" runat="server" Height="200px" Width="200px" ImageUrl='<%#Eval("Imagen.Img64") %>'></asp:Image></center>
                            <div class="card-body h-100 ">
                                <h5 class="card-title">
                                    <asp:Label ID="lblServicio" runat="server" Text='<%#Eval("nombre") %>'></asp:Label>
                                </h5>
                                <asp:Repeater ID="repeaterCaracteristicas" runat="server">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col">
                                                <li>
                                                    <%#Eval("caracteristica") %>
                                                </li>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="card-footer align-bottom">
                                    <p class="text-muted">$<%#Eval("precio") %></p>
                                    <asp:CheckBox ID="chkComparar" Text=" Comparar" OnCheckedChanged="Check" CommandName='<%# Eval("id") %>' AutoPostBack="true" runat="server"></asp:CheckBox>
                                    <asp:Button ID="btnDetalle" CssClass="btn btn-block btn-info" CommandName="detalle" CommandArgument='<%# Eval("id") %>' runat="server" Text="Ver detalle" />
                                    <asp:Button ID="btnComprar" CssClass="btn btn-block btn-success" CommandName="comprar" CommandArgument='<%# Eval("id") %>' Visible="true" runat="server" Text="Agregar al carrito" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>--%>
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
