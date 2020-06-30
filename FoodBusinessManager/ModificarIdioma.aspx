<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ModificarIdioma.aspx.vb" Inherits="FoodBusinessManager.ModificarIdioma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <link href="../../Estilos/Principal.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <div align="center">
            
            <br />
            <div>
                <h1>
                    <asp:Label ID="lbl_ModificarIdiomaTitulo" runat="server" Text="Modificar Idioma" CssClass="labels"></asp:Label>
                </h1>
            </div>
            <br />

            <div>
                <asp:DropDownList ID="lstCulturasCreadas" runat="server" AutoPostBack="true" CssClass="dropdown">
                </asp:DropDownList>
            </div>

            &nbsp;
             <br />
            <p></p>
            <br />

            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView CssClass="table table-hover table-bordered table-responsive table-success " ID="gv_Etiquetas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Etiquetas_PageIndexChanging" RowStyle-Height="40px">
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
                                    <asp:TextBox id="txtTraduccion" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
            <p></p>

            <br />
            <br />
            <div>
                <asp:Button ID="btn_modificarIdioma" runat="server" Text="Modificar Idioma" CssClass="mybutton" />
            </div>
            <br />
            <br />

        </div>
    </form>

</asp:Content>
