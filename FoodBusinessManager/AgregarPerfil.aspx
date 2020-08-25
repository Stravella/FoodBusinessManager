<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AgregarPerfil.aspx.vb" Inherits="FoodBusinessManager.Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="lblAgregarPerfil" runat="server" Text="Agregar perfil"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblNombrePerfil" runat="server" Text="Nombre" CssClass="inputGroupTitle"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombrePerfil" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:TreeView ID="TreeViewPermisos" runat="server" ImageSet="Arrows" ShowCheckBoxes="All">
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="11pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                </asp:TreeView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="btnAgregarPerfill" runat="server" Text="Agregar Perfil" CssClass="btn btn-block btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>

</asp:Content>
