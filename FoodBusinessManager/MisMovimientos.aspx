<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="MisMovimientos.aspx.vb" Inherits="FoodBusinessManager.MisCompras" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col">
                <h4>Mis movimientos
                </h4>
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <center>
                                     <h5>
                                        Mis compras
                                    </h5>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <asp:GridView CssClass="table table-hover table-bordered table-info" ID="gv_Compras" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" EmptyDataText="No tiene compras para mostrar" PageSize="5" OnPageIndexChanging="gv_Compras_PageIndexChanging" RowStyle-Height="40px">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <PagerTemplate>
                                        <div class="col-md-4 text-left">
                                            <asp:Label ID="lblComprasmostrarpag" runat="server" Text="Mostrar Pagina"></asp:Label>
                                            <asp:DropDownList ID="ddlComprasCantidadPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlComprasCantidadPaginas_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:Label ID="lblComprasde" runat="server" Text="de"></asp:Label>
                                            <asp:Label ID="lblComprasTotalPaginas" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-4 col-md-offset-4">
                                            <asp:Label ID="lblComprasMostrar" runat="server" Text="Mostrar"></asp:Label>
                                            <asp:DropDownList ID="ddlComprasTamañoPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlComprasTamañoPaginas_SelectedPageSizeChanged">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblComprasRegistrosPag" runat="server" Text="Registros por Pagina"></asp:Label>
                                        </div>
                                    </PagerTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="total" HeaderText="Importe" />
                                        <asp:BoundField DataField="estado.estado" HeaderText="Estado" />
                                        <asp:TemplateField HeaderText="Cancelar Compra" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgCancelar" ImageUrl="~/IconosSVG/window-close-solid.svg" Text="Cancelar" runat="server" CommandName="Cancelar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descargar Factura" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDescargar" ImageUrl="~/IconosSVG/file-download-solid.svg" Text="Descargar" runat="server" CommandName="Descargar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ver detalle" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgValorar" ImageUrl="~/IconosSVG/info-circle-solid.svg" Text="Detalla" runat="server" CommandName="Valorar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <asp:GridView CssClass="table table-hover table-bordered table-warning" ID="gvServicios" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" RowStyle-Height="40px" Visible="false">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:BoundField DataField="servicio.id" HeaderText="ID" />
                                <asp:BoundField DataField="servicio.nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="importeTotal" HeaderText="Importe total" />
                                <asp:TemplateField HeaderText="Valorar servicios" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgValorar" ImageUrl="~/IconosSVG/check-square-regular.svg" Text="Valorar" runat="server" CommandName="Valorar" CommandArgument='<%# Eval("servicio.id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <center>
                                     <h5>
                                        Mis notas de crédito
                                    </h5>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <asp:GridView CssClass="table table-hover table-bordered table-success " ID="gv_Notas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Notas_PageIndexChanging" EmptyDataText="No tiene notas de credito para mostrar" RowStyle-Height="40px">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <PagerTemplate>
                                        <div class="col-md-4 text-left">
                                            <asp:Label ID="lblNotasmostrarpag" runat="server" Text="Mostrar Pagina"></asp:Label>
                                            <asp:DropDownList ID="ddlNotasCantidadPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlNotasCantidadPaginas_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:Label ID="lblNotasde" runat="server" Text="de"></asp:Label>
                                            <asp:Label ID="lblNotasTotalPaginas" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-4 col-md-offset-4">
                                            <asp:Label ID="lblNotasMostrar" runat="server" Text="Mostrar"></asp:Label>
                                            <asp:DropDownList ID="ddlNotasTamañoPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlNotasTamañoPaginas_SelectedPageSizeChanged">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblNotasRegistrosPag" runat="server" Text="Registros por Pagina"></asp:Label>
                                        </div>
                                    </PagerTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                        <asp:BoundField DataField="importe" HeaderText="Importe" />
                                        <asp:BoundField DataField="estado.estado" HeaderText="Estado" />
                                        <asp:TemplateField HeaderText="Descargar nota de crédito" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDescargar" ImageUrl="~/IconosSVG/file-download-solid.svg" Text="Descargar" runat="server" CommandName="Descargar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
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
