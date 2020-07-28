<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="BitacoraErrores.aspx.vb" Inherits="FoodBusinessManager.BitacoraErrores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div class="container-fluid">
            <br />
            <div class="row">
                <div class="col sm-12">
                    <h1>
                        <asp:Label ID="lblTituloBitacoraError" runat="server" Text="Bitácora de error" Font-Bold="true"></asp:Label>
                    </h1>
                </div>
            </div>


            <div class="form-control">
                <div class="form-group">
                    <asp:Label ID="LblTipoSuceso" runat="server" Text="Tipo suceso"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="lstTipoSuceso" runat="server" CssClass="form-control" AutoPostBack="true" Width="100"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblUsuarios" runat="server" Text="Usuarios"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="lstUsuarios" runat="server" CssClass="form-control" AutoPostBack="true" Width="100"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="LblDesde" runat="server" Text="Fecha desde:" CssClass="control-label labelform"></asp:Label>
                        <div class="input-group">
                            <asp:Calendar ID="CalendarDesde" runat="server" SelectedDate="1990-01-01" Style="align-content"></asp:Calendar>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="LblHasta" runat="server" Text="Fecha desde:" CssClass="control-label labelform"></asp:Label>
                        <div class="input-group">
                            <asp:Calendar ID="CalendarHasta" runat="server" SelectedDate="1990-01-01" Style="align-content"></asp:Calendar>
                        </div>
                    </div>
                </div>
            </div>

            <br />

            <div class="form-group">
                <p>
                    <asp:Button ID="BtnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-block btn-warning" />
                </p>
            </div>

            <br />

            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView CssClass="table table-hover table-bordered table-success " ID="gv_BitacoraError" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" OnPageIndexChanging="gv_Bitacora_PageIndexChanging" RowStyle-Height="40px">
                        <headerstyle cssclass="thead-dark" />
                        <pagertemplate>
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
                            </pagertemplate>
                        <columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="FechaHora" HeaderText="Fecha/Hora" />
                                <asp:BoundField DataField="UsuarioVal" HeaderText="Usuario" />
                                <asp:BoundField DataField="tipoSucesoVal" HeaderText="Tipo Suceso" />
                                <asp:BoundField DataField="ValorAnterior" HeaderText="Valor Anterior" />
                                <asp:BoundField DataField="NuevoValor" HeaderText="Valor Posterior" />
                                <asp:BoundField DataField="id_bitacora_error" HeaderText="ID Error" />
                                <asp:BoundField DataField="Excepcion" HeaderText="Excepción" />
                                <asp:BoundField DataField="StackTrace" HeaderText="Stack Trace" />
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                            </columns>
                    </asp:GridView>
                </div>
            </div>

    </div>

</asp:Content>
