<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="NewsletterSubscribir.aspx.vb" Inherits="FoodBusinessManager.NewsletterSubscribir" %>

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
                                     <h4>Newsletter</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-newspaper m-auto text-primary fa-3x"></i>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>Ingrese su correo</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtSubscripcion" runat="server" ValidationGroup="subscripcion"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqMail" runat="server" ErrorMessage="*" ControlToValidate="txtSubscripcion" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatMail" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtSubscripcion"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Escoja a que categorias subscribirse :</label>
                                <asp:CheckBoxList ID="lstCategorias" runat="server" DataTextField="Nombre" DataValueField="id"></asp:CheckBoxList>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnSubscribrse" class="btn btn-lg btn-block btn-info" runat="server" Text="Subscribirse" ValidationGroup="subscripcion" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
