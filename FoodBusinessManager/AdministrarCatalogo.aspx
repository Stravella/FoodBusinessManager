<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AdministrarCatalogo.aspx.vb" Inherits="FoodBusinessManager.Catalogo" %>

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
                           <h4>Catálogo</h4>
                        </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                            <div class="features-icons-icon d-flex">
                                <i class="fas fa-book-open m-auto text-primary fa-3x"></i>
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
                            <div class="col-md-3">
                                <label>ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtID" runat="server" placeholder="Id" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <label>Nombre</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server" placeholder="Nombre"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Falta completar este campo" ForeColor="Red" ControlToValidate="txtNombre" ValidationGroup="catalogo"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Descripcion</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtDescripcion" runat="server" placeholder="Descripcion" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Falta completar este campo" ForeColor="Red" ControlToValidate="txtDescripcion" ValidationGroup="catalogo"></asp:RequiredFieldValidator>
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
                           <h4>Servicios</h4>
                        </center>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col">
                                <asp:GridView ID="grdServicios" runat="server" CssClass="table table-hover table-bordered table-info" AutoGenerateColumns="false" HorizontalAlign="Center" RowStyle-Height="40px">
                                    <HeaderStyle CssClass="thead-dark" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="precio" HeaderText="precio" />
                                        <asp:TemplateField HeaderText="Imagen" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img id="imagen" runat="server" width="75" height="75" src='<%# eval("Imagen.Img64")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="orden">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrden" runat="server"
                                                    Text='<%# Eval("orden_catalogo") %>'></asp:Label>
                                                <asp:TextBox ID="txtOrden" runat="server"
                                                    Text='<%# Eval("orden_catalogo") %>' Width="175px"
                                                    Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Editar orden" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEditar" ImageUrl="~/IconosSVG/edit-solid.svg" Text="Editar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <asp:Button ID="btnAgregar" class="btn btn-lg btn-block btn-success" runat="server" Text="Agregar" ValidationGroup="catalogo" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Button ID="btnModificar" class="btn btn-lg btn-block btn-warning" runat="server" Text="Modificar" Visible="false" ValidationGroup="catalogo" />
                            </div>
                            <div class="col-6">
                                <asp:Button ID="btnCancelar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Cancelar" Visible="false" />
                            </div>
                        </div>

                    </div>
                </div>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                           <h4>Lista Catálogos</h4>
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
                                <asp:GridView CssClass="table table-hover table-bordered table-success " ID="gv_Catalogos" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Servicios_PageIndexChanging" RowStyle-Height="40px">
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
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
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
            </div>
        </div>
    </div>
</asp:Content>
