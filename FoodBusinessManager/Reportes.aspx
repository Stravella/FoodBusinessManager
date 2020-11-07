<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Reportes.aspx.vb" Inherits="FoodBusinessManager.Reportes" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <%--ENCABEZADO--%>
        <div class="row mt-2">
            <div class="col form-inline">
                <p class="text-muted h4 custom-control-inline mr-0">Seleccione para visualizar un reporte:</p>
                <asp:DropDownList ID="ddlReportes" runat="server" CssClass="ml-3 form-control" AutoPostBack="True">
                    <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Encuestas" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Valoracion servicio" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Ventas" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-12">
                <hr />
            </div>
        </div>
        <%--Valoracion de servicios--%>
        <asp:Panel ID="panelValoracion" runat="server" Visible="false">
            <div class="col-12 justify-content-center">
                <div class="card prodZoom">
                    <div class=" card-header h4  text-center">Valoracion productos</div>
                    <div class="card-body pb-2 text-center">
                        <asp:Chart ID="chValoracion" runat="server">
                            <Series>
                                <asp:Series Name="Series1"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </div>
        </asp:Panel>
        <%--Graficos encuestas--%>
        <asp:Panel ID="panelEncuestas" Visible="false" runat="server">
            <div class="col">
                <div class="card">
                    <div class=" card-header h4  text-center">Seleccione una encuesta:</div>
                    <asp:DropDownList ID="ddlEncuestas" CssClass="form-control small" runat="server" AutoPostBack="true"></asp:DropDownList>
                    <asp:Repeater ID="rptPreguntas" runat="server">
                        <ItemTemplate>
                            <div class="card-body pb-2 text-center">
                                <asp:Chart ID="chPregunta" runat="server">
                                    <Series>
                                        <asp:Series Name="Series1"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>

        <%--panel ventas--%>
        <asp:Panel ID="panelVentas" Visible="false" runat="server">
            <div class="col-12">
                <div class="card">
                    <div class=" card-header h4  text-center">Reporte ventas</div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col justify-content-center">
                                <p class="h5 text-muted">Reporte anual</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-4">
                                <p>Año desde : </p>
                                <asp:DropDownList ID="ddlAñoDesde" runat="server" CssClass="ml-3 form-control" AutoPostBack="False"></asp:DropDownList>
                            </div>
                            <div class="col-4">
                                <p>Año hasta : </p>
                                <asp:DropDownList ID="ddlAñoHasta" runat="server" CssClass="ml-3 form-control" AutoPostBack="False"></asp:DropDownList>
                            </div>
                            <div class="col-4">
                                <asp:Button ID="btnFiltrarAnual" CssClass="btn btn-success vertical-bottom" runat="server" Text="Filtrar" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col justify-content-center">
                                <p class="h5 text-muted">Reporte mensual, semanal, diario</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>Año : </p>
                                <asp:DropDownList ID="ddlAño" runat="server" CssClass="ml-3 form-control" AutoPostBack="False"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="btnFiltrarMensual" CssClass="btn btn-success" runat="server" Text="Reporte mensual" />
                            </div>
                            <div class="col-4">
                                <asp:Button ID="btnFiltrarSemanal" CssClass="btn btn-success" runat="server" Text="Reporte semanal" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>Mes : </p>
                                <asp:DropDownList ID="ddlMes" runat="server" CssClass="ml-3 form-control" AutoPostBack="False"></asp:DropDownList>
                                <asp:Button ID="btnFiltrarDiario" CssClass="btn btn-success" runat="server" Text="Reporte diario" />
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col align-content-center">
                                <asp:GridView CssClass="table table-hover table-bordered table-info " ID="gv_Ventas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" RowStyle-Height="40px" EmptyDataText="No hubo resultados">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <Columns>
                                        <asp:BoundField DataField="nombre" HeaderText="Filtro" />
                                        <asp:BoundField DataField="importe" HeaderText="Importe" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col align-content-center">
                                <asp:Chart ID="chVentas" runat="server">
                                    <Series>
                                        <asp:Series Name="Series1"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>


    </div>
</asp:Content>
