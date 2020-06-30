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

    <form id="form1" runat="server">
        <br />
        <div>
            <h1>
                <div class="panel-heading text-center">
                    Agregar Perfil
                </div>
            </h1>
            <p></p>
            <div class="panel-content">

                <div class="input-group">
                    <asp:Label ID="inputGroupTitle" runat="server" Text="Nombre" CssClass="inputGroupTitle"></asp:Label>
                    <asp:TextBox ID="txtNombrePerfil" runat="server" CssClass="inputGroupTextBox" AutoPostBack="True"></asp:TextBox>
                </div>
                <p></p>

                <div>
                    <asp:TreeView ID="TreeViewPermisos" runat="server" ImageSet="Arrows" ShowCheckBoxes="All">
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                    </asp:TreeView>

                </div>

            </div>
            <p></p>
            <div>
                <asp:Button ID="btnAgregarPerfill" runat="server" Text="Agregar Perfil" CssClass="btnAgregar" />
            </div>
            <p></p>
        </div>
    </form>
</asp:Content>
