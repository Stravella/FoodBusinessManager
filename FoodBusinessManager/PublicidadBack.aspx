<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="PublicidadBack.aspx.vb" Inherits="FoodBusinessManager.PublicidadBack" %>

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
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <h4>Publicidad</h4>
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                                <div class="features-icons-icon d-flex">
                                                    <i class="fas fa-ad m-auto text-primary fa-3x"></i>
                                                </div>
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>URL</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtURL" runat="server" MaxLength="150" ValidationGroup="Publicidad"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtURL" runat="server" ErrorMessage="* Campo requerido" BackColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Imagen URL</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtImagen" runat="server" MaxLength="150" ValidationGroup="Publicidad"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtImagen" runat="server" ErrorMessage="* Campo requerido" BackColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Texto</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtTexto" runat="server" MaxLength="150" ValidationGroup="Publicidad"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtTexto" runat="server" ErrorMessage="* Campo requerido" BackColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col">
                                <asp:Button CssClass="btn btn-block btn-success" ID="btnGuardar" runat="server" Text="Aceptar" ValidationGroup="Publicidad" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col">
                                <asp:Button CssClass="btn btn-block btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" />
                            </div>
                        </div>
                        <br />
                        <hr />
                        <br />
                        <div class="row">
                            <div class="col">
                                <asp:GridView ID="gv_Publicidad" CssClass="table table-hover table-bordered table-success" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" RowStyle-Height="40px">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <Columns>
                                        <asp:BoundField DataField="url" HeaderText="URL" />
                                        <asp:BoundField DataField="imagenUrl" HeaderText="Imagen" />
                                        <asp:BoundField DataField="texto" HeaderText="Texto Alternativo" />
                                        <asp:TemplateField HeaderText="Borrar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEliminar" ImageUrl="~/IconosSVG/trash-solid.svg" Text="Eliminar" runat="server" CommandName="Delete" Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
