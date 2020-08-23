<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="EliminarUsuario.aspx.vb" Inherits="FoodBusinessManager.EliminarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <br />
        <div class="row">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="lblEliminarUsuario" runat="server" Text="Eliminar usuario"> </asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblSeleccionUsuario" runat="server" Text="Seleccion usuario"></asp:Label>
                                    </label>
                                    <asp:DropDownList ID="lstUsuarios" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-block btn-danger" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
  
        <!-- Modal-->
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

    </div>

</asp:Content>
