<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Servicios.aspx.vb" Inherits="FoodBusinessManager.Servicios1" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
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
                                            <h6>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("descripcion") %>'></asp:Label>
                                            </h6>
                                        </div>
                                    </div>
                                    <asp:Repeater ID="repeaterCaracteristicas" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col">
                                                    <li>
                                                        <asp:Label ID="lblCaracteristica" runat="server" Text='<%#Eval("caracteristica") %>'></asp:Label>
                                                    </li>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
