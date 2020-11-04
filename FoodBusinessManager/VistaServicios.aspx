<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="VistaServicios.aspx.vb" Inherits="FoodBusinessManager.Free" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h4>
                                    <center>
                                        <asp:Label ID="lblServicio" runat="server" Text='<%#Eval("nombre") %>'></asp:Label>
                                    </center>
                                    <center>
                                       <asp:Image ID="imgServicio" runat="server" height="150px" width="150px" ImageUrl='<%#Eval("Imagen.Img64") %>'></asp:Image>
                                    </center>
                                    <br />
                                    <center><asp:Label ID="lblValoracion" CssClass="text-secondary" runat="server" Text='<%#Eval("valoracion") %>'></asp:Label><i class="fas fa-star" style="color:gold"></i></center>
                                    <p class="text-muted text-center small">En base a la valoración de nuestros clientes</p>
                                </h4>
                            </div>
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
                                <hr />
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
                        </div>
                    </div>
                    <div class="card-footer">
                        <center>
                            <asp:Label ID="lblPrecio" CssClass="text-success" runat="server" Font-Bold="true" > $<%#Eval("precio") %> </asp:Label>
                        </center>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col">
                            <center>
                                <asp:Button ID="btnComprar" CssClass="btn btn-block btn-success" Visible="true" runat="server" Text="Agregar al carrito" />
                            </center>                            
                        </div>
                    </div>
                    <br />
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <hr />
                    </div>
                </div>
                <asp:Panel ID="panelPreguntas" runat="server">
                    <div class="row">
                        <div class="col">
                            <asp:Label ID="lblPregunta" runat="server" Text="Dejanos tu pregunta" CssClass="control-label labelform" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-9">
                            <div class="form-group">
                                <asp:TextBox ID="txtPregunta" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Este campo es requerido" BackColor="Red" ValidationGroup="pregunta" ControlToValidate="txtPregunta"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-3">
                            <asp:Button ID="btnPregunta" CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="Preguntar" CausesValidation="true" ValidationGroup="pregunta" />
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <div class="row">
                    <div class="col">
                        <asp:Repeater ID="repeaterPreguntas" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col">
                                        <asp:Label ID="lblPregunta" runat="server" Text='<%#Eval("Pregunta") %>'></asp:Label>
                                    </div>
                                </div>
                                <asp:Repeater ID="repeaterRespuestas" runat="server">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-1">
                                            </div>
                                            <div class="col-11">
                                                <asp:Label ID="lblRespuesta" runat="server" Text='<%#Eval("Respuesta") %>' BackColor="LightGray"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="row">
                                    <div class="col">
                                        <hr />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
