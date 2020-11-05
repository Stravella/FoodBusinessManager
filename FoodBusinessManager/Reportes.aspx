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
        </asp:Panel>



        <%-- <div id="rowCSAT" class="row mt-2" runat="server" visible="false">
            <div class="col-12">
                <h2 id="hPreguntas" class="text-muted" runat="server"></h2>
                <hr />
            </div>--%>
        <%--GRAFICOS DE CSAT Y ENCUESTAS PRODUCTOO--%>
        <%-- <div class="col mt-2">
                <div class="card-deck">
                    <asp:Repeater ID="rptCSAT" runat="server">
                        <ItemTemplate>
                            <div class="col-6 mt-2">
                                <div class="card prodZoom" id="divResultadosEncuesta" runat="server" visible="true">
                                    <div class="card-body pb-2">
                                        <div class="card-title h4  text-center"><%# Eval("Pregunta")%></div>
                                        <input id="inpNroPreg" type="hidden" runat="server" value='<%# Eval("ID")%>' />

                                        <asp:Chart ID="chPregunta" runat="server" Width="400" Height="350">
                                            <series>
                                                <asp:Series Name="Series1" ChartType="Column" Color="#960080ff"></asp:Series>
                                            </series>
                                            <chartareas>
                                                <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                                </asp:ChartArea>
                                            </chartareas>
                                        </asp:Chart>
                                        <p>Cantidad de preguntas respondidas: <%# Eval("QRespondidas")%> </p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>--%>

        <%--grilla Gráficos Ventas--%>
        <%--        <asp:Panel ID="pnlVentas" runat="server" Visible="false">
            <div class="row mt-2 justify-content-center">
                <div class="col-12">
                    <h2 class="text-muted">Reporte de ganancias</h2>
                    <hr />
                </div>--%>

        <%--VENTAS POR AÑO--%>
        <%--             <div class="col-8 mt-2 justify-content-center">
                    <div class="card prodZoom">
                        <div class=" card-header h4  text-center">Ventas por Año</div>
                        <div class="card-body pb-2 text-center">
                            <asp:Chart ID="chAno" runat="server" Width="500" Height="300" CssClass="bg-light">

                                <series>
                                    <asp:Series Name="Series1" ChartType="Column" Color="#d1690e" LabelFormat="{0:c}" LegendText="Ventas anuales"></asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                            <p class="card-text">Cantidad de ventas en el periodo: <span id="lblQvtasAno" runat="server"></span></p>
                        </div>
                    </div>
                </div>--%>

        <%--VENTAS POR MES--%>
        <%--                <div class="col-10 mt-5 justify-content-center">
                    <div class="card prodZoom">
                        <div class=" card-header h4  text-center">Ventas por Mes</div>
                        <div class="card-body pb-2 text-center">
                            <asp:Chart ID="chMes" runat="server" Width="600" Height="350" CssClass="bg-light">
                                <series>
                                    <asp:Series Name="Series1" ChartType="Column" LabelFormat="{0:c}"  Color="#2e8456" LegendText="Ventas mensuales"></asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>--%>

        <%--VENTAS POR DIA--%>
        <%--                <div class="col-10 mt-5 justify-content-center">
                    <div class="card prodZoom">
                        <div class=" card-header h4  text-center">Ventas por Día</div>
                        <div class="card-body pb-2 text-center">
                            <asp:Chart ID="chDia" runat="server" Width="800" Height="400" CssClass="bg-light">
                                <series>
                                    <asp:Series Name="Series1" ChartType="Spline" LabelFormat="{0:c}" Color="#2d94d8" LegendText="Ventas diarias"></asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="true">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>--%>

        <%-- <p class="small mt-5">*Se excluyen facturas canceladas por notas de crédito. </p>--%>
        <%--            </div>
        </asp:Panel>--%>
    </div>
</asp:Content>
