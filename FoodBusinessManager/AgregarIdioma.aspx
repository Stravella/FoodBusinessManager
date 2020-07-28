<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AgregarIdioma.aspx.vb" Inherits="FoodBusinessManager.Idiomas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <link href="../../Estilos/Principal.css" rel="stylesheet" />
    <div class="container-fluid">

        <br />
        <div class="row">
            <div class="col-sm-12">
                <h1>
                    <asp:Label ID="lbl_AgregarIdiomaTitulo" runat="server" Text="Agregar Idioma"> </asp:Label>
                </h1>
            </div>
        </div>
        <br />
        <div class="panel panel-info fondo-panel">
            <div class="form-group">
                <div class="form-group label-form">
                    <asp:Label ID="lblCultura" runat="server" Text="Cultura"></asp:Label>
                </div>
                <div class="col-md-12">
                    <div class="dropdown">
                        <asp:DropDownList ID="lstCulturas" CssClass="form-control" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <br />

            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView CssClass="table table-hover table-bordered table-success " ID="gv_Etiquetas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Etiquetas_PageIndexChanging" RowStyle-Height="40px">
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
                            <asp:BoundField DataField="id_etiqueta" HeaderText="ID" />
                            <asp:BoundField DataField="traduccion" HeaderText="Etiqueta" />
                            <asp:TemplateField HeaderText="Traduccion">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTraduccion" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />

            <br />
            <div class="form-control">
                <asp:Button ID="btn_crearIdioma" runat="server" Text="Agregar Idioma" CssClass="form-control btn btn-warning" />
            </div>
        </div>
        <br />

    </div>
</asp:Content>
