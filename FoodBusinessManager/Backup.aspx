<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Backup.aspx.vb" Inherits="FoodBusinessManager.Backup" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <%--<asp:ScriptManager runat="server" ID="script12"></asp:ScriptManager>--%>
    <div class="container">
        <br />
        <div class="row ">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <center><h4>Backup</h4></center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-save m-auto text-primary fa-3x"></i>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-7">
                                <p class="text-muted">Elija el archivo de BackUp para subir al servidor</p>
<%--                                <asp:FileUpload ID="FileUpload1" CssClass="btn btn-secondary" runat="server" />--%>
                                <asp:FileUpload ID="FileUpload1" CssClass="btn btn-secondary" runat="server" />
                                <%--<asp:Button ID="btnUpload" CssClass="btn btn-success" runat="server" Text="Subir" />--%>
                                <asp:Button ID="btnUpload" CssClass="btn btn-success" runat="server" Text="Subir" />
                            </div>
                            <div class="col-5">
                                <h5>Genere un nuevo Backup</h5>
                                <p class="text-muted">Se creará un nuevo Backup con el estado actual de la Base de datos.</p>
                                <div class="form-group">
                                    <asp:Button ID="btnCrearBackUP" CssClass="btn btn-success" runat="server" Text="Crear Backup" />
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row ">
                            <div class="col-12">
                                <p class="text-muted">Seleccione el nombre de un archivo para descargar una copia local</p>
                                <asp:GridView ID="grdBackup" runat="server" AllowPaging="True" CssClass="table table-bordered table-hover table-info" AutoGenerateColumns="False"
                                    HeaderStyle-CssClass="thead-dark" RowStyle-CssClass="text-center" SelectedRowStyle-CssClass="table-secondary"
                                    PagerSettings-Visible="true" PageSize="10" Visible="true" DataKeyNames="ID" EmptyDataText="No hay backups creados">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Archivo" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkDescargar" runat="server"
                                                    CausesValidation="False"
                                                    CommandArgument='<%# Eval("Nombre")%>'
                                                    CommandName="Download" Text='<%# Eval("Nombre") %>' CssClass=" text-body">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="true" />
                                        <asp:BoundField DataField="Usuario.username" HeaderText="Usuario Creador" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha Creación" />
                                        <asp:BoundField DataField="Tamano" HeaderText="Tamaño en Bytes" />
                                        <asp:TemplateField HeaderText="Restore" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRestaurar" runat="server" ImageUrl="IconosSVG/hdd-regular.svg"
                                                    CausesValidation="False"
                                                    CommandArgument='<%# Eval("Nombre")%>'
                                                    CommandName="Restore" Text='<%# Eval("Nombre") %>' Height="20px" Width="20px"></asp:ImageButton>
                                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" TargetControlID="btnRestaurar" DisplayModalPopupID="" ConfirmText="Está seguro??" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="IconosSVG/trash-solid.svg"
                                                    CausesValidation="False" CommandName="Delete" CommandArgument='<%# Eval("Nombre") %>' Height="20px" Width="20px"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="table-info text-center" />
                                    <RowStyle CssClass="table-light text-center font-italic" />
                                    <SelectedRowStyle CssClass="table-secondary" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>

</asp:Content>
