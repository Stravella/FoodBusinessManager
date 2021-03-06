﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Bitacora.aspx.vb" Inherits="FoodBusinessManager.Bitacora2" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="Estilos/Principal.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <br />
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="TituloBitacora" runat="server" Text="Bitácora" Font-Bold="true"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="LblTipoSuceso" runat="server" Text="Tipo suceso"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="lstTipoSuceso" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblCriticidad" runat="server" Text="Criticidad"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="lstCriticidad" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="LblUsuarios" runat="server" Text="Usuarios"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="lstUsuarios" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="LblDesde" runat="server" Text="Fecha desde:" CssClass="control-label labelform"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDesde" CssClass="form-control" placeholder="Date" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="LblHasta" runat="server" Text="Fecha hasta:" CssClass="control-label labelform"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtHasta" CssClass="form-control" placeholder="Date" TextMode="Date" runat="server"></asp:TextBox>
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
                                    <asp:Button ID="BtnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-block btn-success" />
                                </div>
                            </div>
                        </div>

                        <br />

                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:GridView CssClass="table table-hover table-bordered table-success " ID="gv_Bitacora" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Bitacora_PageIndexChanging" RowStyle-Height="40px" EmpyDataText="Su consulta no devolvió resultados">
                                        <HeaderStyle CssClass="thead-dark" />
                                        <PagerTemplate>
                                            <div class="col-md-4 text-left">
                                                <asp:Label ID="lblmostrarpag" runat="server" Text="Mostrar Pagina"></asp:Label>
                                                <asp:DropDownList ID="ddlCantidadPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlCantidadPaginas_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblde" runat="server" Text="de"></asp:Label>
                                                <asp:Label ID="lblTotalPaginas" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-md-offset-4">
                                                <asp:Label ID="lblMostrar" runat="server" Text="Mostrar"></asp:Label>
                                                <asp:DropDownList ID="ddlTamañoPaginas" runat="server" AutoPostBack="true" CssClass="margenPaginacion" OnSelectedIndexChanged="ddlTamañoPaginas_SelectedPageSizeChanged">
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblRegistrosPag" runat="server" Text="Registros por Pagina"></asp:Label>
                                            </div>
                                        </PagerTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="ID" />
                                            <asp:BoundField DataField="FechaHora" HeaderText="Fecha/Hora" />
                                            <asp:BoundField DataField="UsuarioVal" HeaderText="Usuario" />
                                            <asp:BoundField DataField="tipoSucesoVal" HeaderText="Tipo Suceso" />
                                            <asp:BoundField DataField="criticidad.criticidad" HeaderText="Criticidad" />
                                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />

</asp:Content>
