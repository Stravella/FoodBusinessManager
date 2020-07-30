<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="EliminarPerfil.aspx.vb" Inherits="FoodBusinessManager.EliminarPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container-fluid">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h1>
                    <asp:Label ID="lblEliminarPerfil" runat="server" Text="Eliminar perfil" Font-Bold="true"></asp:Label>
                </h1>
            </div>
        </div>
        <br />
        <div class="form-group label-form">
            <asp:Label ID="lblPerfil" runat="server" Text="Perfil :" CssClass="control-label col-sm-4"></asp:Label>
        </div>
        <div class="col-md-12">
            <div class="dropdown">
                <asp:DropDownList ID="lstPerfil" runat="server" CssClass="form-control" AutoPostBack="true" DataValueField="ID_Permiso" DataTextField="Nombre"></asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="panel-group">
            <div class="panel panel-warning">
                <div class="panel-heading text-center titulo-panel">
                    <asp:Label ID="lblListadoPermisos" runat="server" Text="Permisos Actuales"></asp:Label>
                </div>
                <div class="panel-body fondo-panel">
                    <asp:TreeView ID="TreeViewPermisoActual" runat="server" ImageSet="Arrows" ShowCheckBoxes="All">
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="row">
                <div class="table table-hover table-bordered table-success">
                    <asp:GridView ID="gv_Perfiles" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" AllowPaging="true" PageSize="10" RowStyle-Height="40px">
                        <Columns>
                            <asp:BoundField DataField="username" HeaderText="Usuarios con el perfil seleccionado" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <br />
        <div class="form-control">
            <asp:Button ID="btnEliminarPerfil" runat="server" Text="Eliminar Perfil" CssClass="form-control btn btn-danger" />
        </div>
    </div>
</asp:Content>
