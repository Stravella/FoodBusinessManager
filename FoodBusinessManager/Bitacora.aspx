﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Bitacora.aspx.vb" Inherits="FoodBusinessManager.Bitacora2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="Estilos/Principal.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" runat="server">
        <div class="panel-heading text-center">
            <h1>
                <asp:Label ID="TituloBitacora" runat="server" Text="Bitacora" Font-Size="Large" Font-Bold="true"></asp:Label>
            </h1>
        </div>

        <div class="form-inline">
            <div class="col-md-3">
                <asp:Label ID="LblUsuarios" runat="server" Text="Usuarios"></asp:Label>
                <div class="input-group">
                    <asp:DropDownList ID="lstUsuarios" runat="server" CssClass="form-control" AutoPostBack="true" Width="100"></asp:DropDownList>
                    <span class="input-group-addon" id="basic-addon10"><span class="glyphicon glyphicon-user" aria-hidden="true"></span></span>
                </div>
            </div>
            <div class="col-md-2">
                <asp:Label ID="LblTipoSuceso" runat="server" Text="Tipo Suceso"></asp:Label>
                <div class="input-group">
                    <asp:DropDownList ID="lstTipoSuceso" runat="server" CssClass="form-control" AutoPostBack="true" Width="100"></asp:DropDownList>
                    <span class="input-group-addon" id="basic-addon11"><span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span></span>
                </div>
            </div>
            <div class="col-md-2 col-md-offset-2">
                <asp:Label ID="LblDesde" runat="server" Text="Desde:" CssClass="control-label labelform"></asp:Label>
                <div class="input-group">
                    <asp:Calendar ID="CalendarDesde" runat="server" SelectedDate="1990-01-01" Style="align-content"></asp:Calendar>
                </div>
            </div>
            <div class="col-md-2">
                <asp:Label ID="LblHasta" runat="server" Text="Hasta:" CssClass="control-label labelform"></asp:Label>
                <div class="input-group">
                    <asp:Calendar ID="CalendarHasta" runat="server" SelectedDate="2020-01-24" Style="align-content"></asp:Calendar>
                </div>
            </div>
        </div>

        <p></p>
        <div class="col-md-2 col-md-offset-5">
            <p>
                <asp:Button ID="BtnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-block btn-warning"/>
            </p>
        </div>
        <p>
        </p>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView CssClass="table table-hover table-bordered table-responsive table-success " ID="gv_Bitacora" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Bitacora_PageIndexChanging" RowStyle-Height="40px">
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
                        <asp:BoundField DataField="ValorAnterior" HeaderText="Valor Anterior" />
                        <asp:BoundField DataField="NuevoValor" HeaderText="Valor Posterior" />
                        <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
