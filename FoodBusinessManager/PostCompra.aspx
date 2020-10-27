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
                                Por favor complete la siguiente encuesta para que podamos mejorar el serivicio
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="card-deck">
                                    <asp:Repeater ID="rptPreguntas" runat="server">
                                        <ItemTemplate>
                                            <div class="card border mb-3">
                                                <div class="card-body">
                                                    <h4 class="card-title"><%# Eval("Opinion") %></h4>
                                                    <div class=" form-group form-check pl-0 pt-0 card-text">
                                                        <asp:RadioButtonList ID="rdlRespuestas" runat="server"
                                                            RepeatDirection="Vertical">
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnEncuesta" Text="Enviar" runat="server" CssClass="btn btn-block btn-success" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
