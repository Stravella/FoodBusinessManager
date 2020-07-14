<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AgegarPermisos.aspx.vb" Inherits="FoodBusinessManager.Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Estilos/Principal.css" rel="stylesheet" />
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
        <form id="form1" runat="server">
            <br />
            <div>
                <div class="row">
                    <div class="col-sm-121">
                        <h1>Agregar Perfil
                        </h1>
                    </div>
                </div>
                <br />

                <div class="panel panel-info">
                    <div class="panel-body fondo-panel">
                        <div class="form-group">
                            <div class="form-group label-form">
                                <asp:Label ID="lblNombrePerfil" runat="server" Text="Nombre" CssClass="inputGroupTitle"></asp:Label>
                            </div>
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtNombrePerfil" runat="server" CssClass="form control" AutoPostBack="True"></asp:TextBox>
                            </div>
                        </div>
                        <br />


                        <div>
                            <asp:TreeView ID="TreeViewPermisos" runat="server" ImageSet="Arrows" ShowCheckBoxes="All">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                            </asp:TreeView>
                        </div>
                    </div>
                </div>
                <br />
                <div class="col-md-4 col-md-offset-4">
                    <asp:Button ID="btnAgregarPerfill" runat="server" Text="Agregar Perfil" CssClass="form-control btn btn-warning" />
                </div>
            </div>
            <p></p>
    </div>
    </form>
    </div>
</asp:Content>
