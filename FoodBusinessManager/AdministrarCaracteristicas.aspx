<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AdministrarCaracteristicas.aspx.vb" Inherits="FoodBusinessManager.AdministrarCaracteristicas" %>

 <%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                           <h4>Caracteristicas</h4>
                        </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                            <div class="features-icons-icon d-flex">
                                <i class="fas fa-users-cog m-auto text-primary fa-3x"></i>
                            </div>
                                        </div>
                        </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtID" runat="server" placeholder="Id" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Caracteristica</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtCaracteristica" runat="server" placeholder="Descripcion" TextMode="MultiLine" Rows="2" ValidationGroup="catacteristicas"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCaracteristica"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnAgregar" class="btn btn-lg btn-block btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" ValidationGroup="catacteristicas" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Button ID="btnModificar" class="btn btn-lg btn-block btn-warning" runat="server" Text="Modificar" Visible="false" ValidationGroup="catacteristicas" />
                            </div>
                            <div class="col-6">
                                <asp:Button ID="btnCancelar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Cancelar" Visible="false"/>
                            </div>
                        </div>
                    </div>
                </div>
                <br>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                           <h4>Lista Caracteristicas</h4>
                        </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView CssClass="table table-hover table-bordered table-success " ID="gv_Caracteristicas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Caracteristicas_PageIndexChanging" RowStyle-Height="40px" OnRowCommand="gv_Caracteristicas_RowCommand">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <PagerTemplate>
                                        <div class="col-md-4 text-left">
                                            <asp:Label ID="lblmostrarpag" runat="server" Text="Mostrar Pagina"></asp:Label>
                                            <asp:DropDownList ID="ddlCantidadPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlCantidadPaginas_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:Label ID="lblde" runat="server" Text="de"></asp:Label>
                                            <asp:Label ID="lblTotalPaginas" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-4 col-md-offset-4">
                                            <asp:Label ID="lblMostrar" runat="server" Text="Mostrar"></asp:Label>
                                            <asp:DropDownList ID="ddlTamañoPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlTamañoPaginas_SelectedPageSizeChanged">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblRegistrosPag" runat="server" Text="Registros por Pagina"></asp:Label>
                                        </div>
                                    </PagerTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="caracteristica" HeaderText="Caracteristica" />
                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEditar" ImageUrl="~/IconosSVG/edit-solid.svg" Text="Editar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Borrar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
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
            </div>
        </div>
    </div>
</asp:Content>
