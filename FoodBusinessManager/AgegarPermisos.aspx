<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AgegarPermisos.aspx.vb" Inherits="FoodBusinessManager.Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Estilos/Principal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" runat="server">
        <br />
        <div>
            <div class="panel-heading text-center">
                Administrar Permisos
            </div>
            <p></p>
            <div>
                <asp:Label ID="lblModificarPerfil" runat="server" Text="Modificar Perfiles"></asp:Label>
                <asp:ListBox ID="lstPerfiles" runat="server" Height="29px"></asp:ListBox>
            </div>
            <p></p>
            <div class="panel-content">
                
                <div class="input-group">
                    <asp:Label ID="inputGroupTitle" runat="server" Text="Nombre Perfil" CssClass="inputGroupTitle"></asp:Label>
                    <asp:TextBox ID="txtNombrePerfil" runat="server" CssClass="inputGroupTextBox"></asp:TextBox>
                </div>
                <p></p>
                <div>
                    <asp:TreeView ID="TreeViewPermisos" runat="server" ShowCheckBoxes="All"></asp:TreeView>
                </div>

            </div>
            <p></p>
            <div>
                <asp:Button ID="btnAgregarPerfill" runat="server" Text="Agregar Perfil" CssClass="btnAgregar" />
            </div>
            <p></p>
            <div>
                <asp:Label ID="lblRespuesta" runat="server" ></asp:Label>
            </div>
        </div>
    </form>
</asp:Content>
