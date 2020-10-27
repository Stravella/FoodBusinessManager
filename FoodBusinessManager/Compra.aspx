<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Compra.aspx.vb" Inherits="FoodBusinessManager.Compra" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div class="container">
        <br />
        <div class="row">
            <div class="col">
                <center>
                    <h4>Compra</h4>
                </center>
            </div>
        </div>

        <hr />
        <div class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col">
                        <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                            <div class="features-icons-icon d-flex">
                                <i class="fas fa-shopping-cart m-auto text-primary fa-3x"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col">
                        <asp:GridView CssClass="table table-hover table-bordered table-info " ID="gv_Carrito" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay nada en su carrito" HorizontalAlign="Center" AllowPaging="True" PageSize="5" RowStyle-Height="40px" ShowFooter="true">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:TemplateField HeaderText="Imagen" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <img id="imagen" runat="server" width="75" height="75" src='<%#Eval("Servicio.Imagen.Img64")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="servicio.nombre" HeaderText="Nombre" />
                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtCantidad" Columns="5" TextMode="Number" Text='<%# Eval("cantidad") %>'></asp:TextBox>
                                        <asp:LinkButton runat="server" ID="btnRemover" Text="Remover" CommandName="Remover" CommandArgument='<%# Eval("Servicio.id") %>' Style="font-size: 12px;"></asp:LinkButton><br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="txtCantidad" runat="server"
                                            ErrorMessage="Solo se aceptan numeros"
                                            ValidationExpression="\d+"
                                            ForeColor="Red">
                                        </asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="servicio.precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="importeTotal" HeaderText="Total" DataFormatString="{0:C}" />
                            </Columns>
                            <RowStyle Height="40px"></RowStyle>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnActualizarCarrito" CssClass="btn btn-info" runat="server" Text="Actualizar carrito" />
                    </div>
                    <div class="col-2">
                    </div>
                    <div class="col-4">
                        <asp:Label ID="lblPrecioTotal" CssClass="form-control" runat="server" Text="Precio Total : "></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-6">
                <asp:Button ID="btnCancelar" CssClass="btn btn-block btn-danger" runat="server" Text="Cancelar" />
            </div>
            <div class="col-6">
                <asp:Button ID="btnComprar" CssClass="btn btn-block btn-success" runat="server" Text="Continuar" />
            </div>
        </div>
        <br />
    </div>

</asp:Content>
