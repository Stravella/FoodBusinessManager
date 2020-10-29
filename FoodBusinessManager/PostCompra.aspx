<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="PostCompra.aspx.vb" Inherits="FoodBusinessManager.PostCompra" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>
                                        ¡Compra exitosa!
                                    </h4>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                ¡Su compra fue realizada con éxito! Le enviamos la factura a su mail. Podrá descargarla desde "Mis movimientos".
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>
                                        Encuesta
                                    </h4>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <h5>Por favor complete la siguiente encuesta para que podamos mejorar el servicio</h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:UpdatePanel ID="panelRepeater" runat="server">
                                    <ContentTemplate>
                                        <asp:Repeater ID="repeaterEncuesta" runat="server">
                                            <ItemTemplate>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-9">                                                      
                                                        <asp:Label ID="lblNombreEncuesta" runat="server" Text='<%# Eval("nombre") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-3">
                                                        <asp:Button ID="btnResponder" CssClass="btn btn-block btn-warning" CommandName="responder" CommandArgument='<%# Eval("id") %>' OnClick="btnResponder_Click" runat="server" Text="Responder" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
