<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AdministrarEncuestaPreguntas.aspx.vb" Inherits="FoodBusinessManager.AdministrarEncuestaPreguntas" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <center>
                                     <h4>Preguntas a encuestas/opiniones</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-question m-auto text-primary fa-3x"></i>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-3">
                                <label>ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-9">
                                <label>Pregunta</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtOpinion" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Este campo es requerido" ForeColor="Red" ControlToValidate="txtOpinion" ValidationGroup="Opinion"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label>
                                    Fecha vencimiento
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtVencimiento" CssClass="form-control" placeholder="Date" TextMode="Date" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Este campo es requerido" ForeColor="Red" ControlToValidate="txtVencimiento" ValidationGroup="Opinion"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <label>
                                    Estado
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlEstado" CssClass="form-control" runat="server" DataValueField="id" DataTextField="estado"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <%-- Respuestas --%>
                        <asp:GridView ID="gvRespuestas" runat="server" CssClass="table table-hover table-bordered table-success" AutoGenerateColumns="false" EmptyDataText="No hay respuestas creadas" HorizontalAlign="Center" AllowPaging="false" RowStyle-Height="40px">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="respuesta" HeaderText="Respuesta" />
                            </Columns>
                        </asp:GridView>                       
                        <hr />
                        <div class="row">
                            <div class="col">
                                <asp:Button ID="btnAgregar" class="btn btn-lg btn-block btn-success" runat="server" Text="Agregar" ValidationGroup="Opinion" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Button ID="btnModificar" class="btn btn-lg btn-block btn-warning" runat="server" Text="Modificar" Visible="false" ValidationGroup="Opinion" />
                            </div>
                            <div class="col-6">
                                <asp:Button ID="btnCancelar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Cancelar" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <%-- GRILLA --%>
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="gvPreguntas" runat="server" CssClass="table table-hover table-bordered table-info" AutoGenerateColumns="false" HorizontalAlign="Center" EmptyDataText="No hay preguntas creadas" RowStyle-Height="40px">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="Pregunta" HeaderText="Pregunta" />
                                <asp:BoundField DataField="FechaVenc" HeaderText="Fecha Vencimiento" />
                                <asp:BoundField DataField="Estado.estado" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEditar" ImageUrl="~/IconosSVG/edit-solid.svg" Text="Editar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Borrar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEliminar" ImageUrl="~/IconosSVG/trash-solid.svg" Text="Eliminar" runat="server" CommandName="Borrar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <br />
</asp:Content>
