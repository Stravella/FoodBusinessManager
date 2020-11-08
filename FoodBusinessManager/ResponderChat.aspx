<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="ResponderChat.aspx.vb" Inherits="FoodBusinessManager.ResponderChat" %>

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
                                <center>
                                     <h4>Chats</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-comment-dots m-auto text-primary fa-3x"></i>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>Mensaje</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtMensaje" runat="server" ValidationGroup="categorias" MaxLength="200" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Complete este campo" ControlToValidate="txtMensaje"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnResponder" class="btn btn-lg btn-block btn-success" runat="server" Text="responder" ValidationGroup="categorias" Visible="false" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnCancelar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Cancelar" Visible="false"/>
                            </div>
                        </div>
                    </div>
                <hr />
                <%--Grilla mensajes--%>
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="gv_Mensajes" runat="server" CssClass="table table-hover table-bordered table-info" AutoGenerateColumns="false" HorizontalAlign="Center" RowStyle-Height="40px">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="fecha" HeaderText="fecha" />
                                <asp:BoundField DataField="username" HeaderText="Usuario" />
                                <asp:BoundField DataField="mensaje" HeaderText="mensaje" />                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <%--Grilla chat--%>
               <div class="row">
                    <div class="col">
                        <asp:GridView ID="gv_Chat" runat="server" CssClass="table table-hover table-bordered table-success" AutoGenerateColumns="false" HorizontalAlign="Center" RowStyle-Height="40px">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="cliente.razon_social" HeaderText="Cliente" />
                                <asp:BoundField DataField="usuarioAtendio.username" HeaderText="Usuario que lo atendió" />
                                <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha inicio" />
                                <asp:BoundField DataField="fecha_fin" HeaderText="Fecha fin" />
                                <asp:TemplateField HeaderText="Responder" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgResponder" ImageUrl="~/IconosSVG/reply-solid.svg" Text="Responder" runat="server" CommandName="Responder" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div> 
        </div>
        <br />
    </div>
</asp:Content>
