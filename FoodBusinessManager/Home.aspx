<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Home.aspx.vb" Inherits="FoodBusinessManager.Home" %>

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
                        <p class="lead mb-0"> Eliminando la comanda en papel, ahorre dinero en la compra de insumos y reduzca el desperdicio de papel.</p>
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

    </div>
</asp:Content>
