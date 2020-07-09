<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ModificarPermiso.aspx.vb" Inherits="FoodBusinessManager.ModificarPermiso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <script type="text/javascript">

        function forcePostBack() {
            var yourObject = window.event.srcElement;
            if (yourObject.tagName == "INPUT" && yourObject.type == "checkbox") {
                __doPostBack("", "");
            }
        }
    </script>

    <div class="container-fluid">
        <form id="form1" runat="server" class="form-horizontal">
            <br />
            <div class="row">
                <div class="col-sm-121">
                    <h1>
                        <asp:Label ID="lblTituloModificarPerfil" runat="server" Text="Modificar Perfil" Font-Bold="true"></asp:Label>
                    </h1>
                </div>

            </div>
            <div class="form-group label-form">
                <asp:Label ID="lblPerfil" runat="server" Text="Perfil :" CssClass="control-label col-sm-4"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="dropdown">
                    <asp:DropDownList ID="lstPerfil" runat="server" CssClass="form-control" AutoPostBack="true" DataValueField="ID_Permiso" DataTextField="Nombre"></asp:DropDownList>
                </div>
            </div>

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
            <div class="panel-group">
                <div class="panel panel-success">
                    <div class="panel-heading text-center titulo-panel">
                        <asp:Label ID="Label1" runat="server" Text="Nuevos Permisos"></asp:Label>
                    </div>
                    <div class="panel-body fondo-panel">
                        <asp:TreeView ID="TreeViewNuevosPermisos" runat="server" ImageSet="Arrows" ShowCheckBoxes="All">
                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                            <ParentNodeStyle Font-Bold="False" />
                            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                        </asp:TreeView>
                    </div>
                </div>
            </div>
            <br />
            <%-- Acá va la tabla  --%>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">
                        <asp:GridView ID="gv_Perfiles" runat="server" CssClass="grid" AutoGenerateColumns="false" HorizontalAlign="Center" AllowPaging="true" PageSize="10" RowStyle-Height="40px">
                            <Columns>
                                <asp:BoundField DataField="username" HeaderText="Usuarios con el perfil seleccionado" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />

            <div class="form-control">
                <asp:Button ID="btnModificarPerfil" runat="server" Text="Modificar Perfil" CssClass="form-control btn btn-warning" />
            </div>

        </form>
    </div>

</asp:Content>
