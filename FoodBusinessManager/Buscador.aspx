<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Buscador.aspx.vb" Inherits="FoodBusinessManager.Buscador" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="form-group">
            <div class="card-body">
                <div class="card card-body">
                    <div class="mr-0">
                        <div style="text-align: center">
                            <h3>Resultados de tu búsqueda </h3>
                            <hr />
                        </div>
                        <hr />
                        <br />
                        <asp:Repeater runat="server" ID="rp_busqueda">
                            <ItemTemplate>
                                <div class="card-body">
                                    <ul>
                                        <a class="card-title" href='<%# Eval("url") %>'><%# Eval("Menu") %></a>
                                    </ul>
                                </div>
                                <hr />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
