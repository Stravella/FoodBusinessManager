<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Home.aspx.vb" Inherits="FoodBusinessManager.Home" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <!-- Masthead -->
        <header class="masthead text-white text-center">
            <div class="overlay"></div>
            <div class="container">
                <div class="row">
                    <div class="col-xl-9 mx-auto">
                        <h1 class="mb-5">Hacé foco en lo importante</h1>
                    </div>
                </div>
            </div>
        </header>
        <!-- Icons Grid -->
        <section class="features-icons bg-light text-center">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                            <div class="features-icons-icon d-flex">
                                <i class="fas fa-cogs m-auto text-primary"></i>
                            </div>
                            <h3>Mejor producción</h3>
                            <p class="lead mb-0">Automatice y mejore sus procesos</p>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                            <div class="features-icons-icon d-flex">
                                <i class="fas fa-brain m-auto text-primary"></i>
                            </div>
                            <h3>Más creación</h3>
                            <p class="lead mb-0">Gane más tiempo para explotar su creatividad, del orden nos ocupamos nosotros..</p>
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="features-icons-item mx-auto mb-0 mb-lg-3">
                            <div class="features-icons-icon d-flex">
                                <i class="fas fa-check m-auto text-primary"></i>
                            </div>
                            <h3>Sin comandas</h3>
                            <p class="lead mb-0">Olvidese de las comandas en papel. Digitalizamos todos sus procesos.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- Image Showcases -->
        <section class="showcase">
            <div class="container-fluid p-0">
                <div class="row no-gutters">
                    <div class="col-lg-6 order-lg-2 text-white showcase-img" style="background-image: url('Imagenes/Cocina_restaurant.jpg');"></div>
                    <div class="col-lg-6 order-lg-1 my-auto showcase-text">
                        <h2>Mejora continua</h2>
                        <p class="lead mb-0">Orientado a la mejora continua de los procesos. Facilite su día a día y permitase enfocarse en mejorar la experiencia de sus clientes.</p>
                    </div>
                </div>
                <div class="row no-gutters">
                    <div class="col-lg-6 text-white showcase-img" style="background-image: url('Imagenes/Comandas.jpg');"></div>
                    <div class="col-lg-6 my-auto showcase-text">
                        <h2>Tecnología sostenible</h2>
                        <p class="lead mb-0">Eliminando la comanda en papel, ahorre dinero en la compra de insumos y reduzca el desperdicio de papel.</p>
                    </div>
                </div>
                <div class="row no-gutters">
                    <div class="col-lg-6 order-lg-2 text-white showcase-img" style="background-image: url('Imagenes/Plato_colorido.jpg');"></div>
                    <div class="col-lg-6 order-lg-1 my-auto showcase-text">
                        <h2>Fácil de usar</h2>
                        <p class="lead mb-0">Luego de una configuración inicial de cocina y carta, ya puede comenzar a vender sus productos de forma ágil e inteligente.</p>
                    </div>
                </div>
            </div>
        </section>

        <!-- Encuesta -->
        <section id="encuestas">
            <div class="container">
                <div class="row" style="place-content: center">
                    <div class="col-4" id="panelEncuesta" runat="server">
                        <div class="card-body">
                            <div class="card card-body">
                                <h3 id="Pregunta" runat="server"></h3>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="idPregunta" runat="server" />
                                        <asp:RadioButtonList runat="server" ID="rbPreguntas" AutoPostBack="false">
                                        </asp:RadioButtonList>
                                        <asp:Chart ID="chReportes" CssClass="chart" Visible="False" BackColor="LightGray" Width="280px" runat="server">
                                            <Series>
                                                <asp:Series Name="Series1"></asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" AlignmentOrientation="Horizontal" Area3DStyle-Enable3D="true"
                                                    Area3DStyle-WallWidth="2" Area3DStyle-Rotation="20"
                                                    Area3DStyle-LightStyle="Simplistic" Area3DStyle-Inclination="40"
                                                    BorderColor="White" ShadowColor="#CCCCCC">
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                        <br />
                                        <asp:Button ID="btnVotar" Text="Votar" CssClass="btn btn-success" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>


    </div>
</asp:Content>
