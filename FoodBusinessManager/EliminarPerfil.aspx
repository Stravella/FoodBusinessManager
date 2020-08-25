<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="EliminarPerfil.aspx.vb" Inherits="FoodBusinessManager.EliminarPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="lblEliminarPerfil" runat="server" Text="Eliminar perfil" Font-Bold="true"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblPerfil" runat="server" Text="Perfil :" ></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="lstPerfil" runat="server" CssClass="form-control" AutoPostBack="true" DataValueField="ID_Permiso" DataTextField="Nombre"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblListadoPermisos" runat="server" Text="Permisos Actuales"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TreeView ID="TreeViewPermisoActual" runat="server" ImageSet="Arrows" ShowCheckBoxes="All" Enabled="false">
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                        <NodeStyle Font-Names="Tahoma" Font-Size="11pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                        <ParentNodeStyle Font-Bold="False" />
                                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col">
                                <div class="table table-hover table-bordered table-success">
                                    <asp:GridView ID="gv_Perfiles" runat="server" AutoGenerateColumns="false" HorizontalAlign="Center" AllowPaging="true" PageSize="10" RowStyle-Height="40px">
                                        <Columns>
                                            <asp:BoundField DataField="username" HeaderText="Usuarios con el perfil seleccionado" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
                                    <asp:Button ID="btnEliminarPerfil" runat="server" Text="Eliminar Perfil" CssClass="btn btn-block btn-danger" />
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
