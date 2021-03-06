﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AdministrarEncuestas.aspx.vb" Inherits="FoodBusinessManager.AdministrarEncuestas" %>

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
                                     <h4>Encuestas/Opiniones</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-check-square m-auto text-primary fa-3x"></i>
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
                                <label>Nombre</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Falta completar este campo" ForeColor="Red" ControlToValidate="txtNombre" ValidationGroup="Encuesta"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    Tipo
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlTipo" CssClass="form-control" runat="server" DataValueField="id" DataTextField="tipo" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView ID="gvServicios" runat="server" CssClass="table table-hover table-bordered table-warning" AutoGenerateColumns="false" EmptyDataText="No hay servicios" HorizontalAlign="Center" AllowPaging="false" RowStyle-Height="40px" Visible="false">
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
                                        <asp:BoundField DataField="nombre" HeaderText="Pregunta" />                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <hr />
                        <%-- PReguntas --%>
                        <asp:GridView ID="gvPreguntas" runat="server" CssClass="table table-hover table-bordered table-success" AutoGenerateColumns="false" EmptyDataText="No hay preguntas disponibles" HorizontalAlign="Center" AllowPaging="false" RowStyle-Height="40px">
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
                                <asp:BoundField DataField="pregunta" HeaderText="Pregunta" />
                                <asp:BoundField DataField="FechaVenc" HeaderText="Estado" />
                                <asp:BoundField DataField="estado.estado" HeaderText="Estado" />
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
                        <asp:GridView ID="gvEncuesta" runat="server" CssClass="table table-hover table-bordered table-info" AutoGenerateColumns="false" HorizontalAlign="Center" EmptyDataText="No hay encuestas creadas" RowStyle-Height="40px">
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="tipo.tipo" HeaderText="Tipo" />
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
