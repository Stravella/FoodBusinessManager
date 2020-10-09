<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Respuestas.aspx.vb" Inherits="FoodBusinessManager.Respuestas" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Respuestas</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-reply-all m-auto text-primary fa-3x"></i>
                                        </div>
                                   </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>Servicio</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlServicio" CssClass="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Pregunta</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlPregunta" CssClass="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>
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
                                <center>
                           <h4>Respuestas</h4>
                        </center>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col">
                                <asp:GridView ID="grdRespuestas" runat="server" CssClass="table table-hover table-bordered table-info" AutoGenerateColumns="false" HorizontalAlign="Center" AllowPaging="false" RowStyle-Height="40px">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <Columns>
                                        <asp:BoundField DataField="respuesta" HeaderText="Respuestas" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtRespuesta" runat="server" placeholder="Descripcion" TextMode="MultiLine" Rows="2" ValidationGroup="respuestas"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Este campo es requerido" BackColor="Red" ValidationGroup="respuestas" ControlToValidate="txtRespuesta"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <asp:Button ID="btnResponder" class="btn btn-lg btn-block btn-success" runat="server" Text="Responder" ValidationGroup="respuestas" />
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </div>
    </div>
    <br />
</asp:Content>
