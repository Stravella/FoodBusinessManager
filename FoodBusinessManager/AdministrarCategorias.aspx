<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AdministrarCategorias.aspx.vb" Inherits="FoodBusinessManager.AdministrarCategorias" %>

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
                                     <h4>Categorias</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-list m-auto text-primary fa-3x"></i>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-3">
                                <label>ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-9">
                                <label>Nombre</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Falta completar este campo" ForeColor="Red" ControlToValidate="txtNombre" ValidationGroup="categorias"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnAgregar" class="btn btn-lg btn-block btn-success" runat="server" Text="Agregar" ValidationGroup="categorias"/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Button ID="btnModificar" class="btn btn-lg btn-block btn-warning" runat="server" Text="Modificar" Visible="false" ValidationGroup="categorias" />
                            </div>
                            <div class="col-6">
                                <asp:Button ID="btnCancelar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Cancelar" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="gv_Categorias" runat="server" CssClass="table table-hover table-bordered table-info" AutoGenerateColumns="false" HorizontalAlign="Center" RowStyle-Height="40px">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEditar" ImageUrl="~/IconosSVG/edit-solid.svg" Text="Editar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Borrar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEliminar" ImageUrl="~/IconosSVG/trash-solid.svg" Text="Eliminar" runat="server" CommandName="Borrar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
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
