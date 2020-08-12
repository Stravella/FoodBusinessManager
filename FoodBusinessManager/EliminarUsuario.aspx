<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="EliminarUsuario.aspx.vb" Inherits="FoodBusinessManager.EliminarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h1>
                    <asp:Label ID="lblEliminarUsuario" runat="server" Text="Eliminar usuario"> </asp:Label>
                </h1>
            </div>
        </div>
        <br />
    </div>

    <div class="panel panel-info fondo-panel">
        <div class="form-group">
            <div class="form-group label-form">
                <asp:Label ID="lblUsuarios" runat="server" Text="Usuarios"></asp:Label>
            </div>
            <div class="col-md-12">
                <div class="dropdown">
                    <asp:ListBox ID="lstUsuarios" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="form-group">
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-block btn-danger" />
            </div>
        </div>
    </div>

    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAceptar" CssClass="btn btn-success" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" CssClass="btn btn-danger" runat="server" Text="Cancelar" data-dismiss="modal" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
