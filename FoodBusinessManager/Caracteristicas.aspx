<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Caracteristicas.aspx.vb" Inherits="FoodBusinessManager.Caracteristicas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <!-- Head-->
        <!-- Subtitulo -->
        <section class="features-icons bg-light text-center">
            <div class="container">
                <div class="row">
                    <div class="col">
                        <h3>Características generales</h3>
                        <p class="lead mb-0">Food Business Manager es un software gastronómico que cuenta con una gestión totalmente integrada. Está preparado para adaptarse a cualquier formato gastronómico ya sea un restaurant, parrillas, bares, clubes, heladerías, pizzerías, cafeterías, entre otros. </p>
                    </div>
                </div>
            </div>
        </section>
        <!-- Lista caracterisitcas -->
        <section>
            <div class="container-fluid p-0">
                <div class="row">
                    <div class="col-md-4">
                        <div class="card mb-4 box-shadow">
                            <img class="card-img-top" src="Imagenes/carta-restaurante.jpg" style="height: 200px; width: 100%; display: block;" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title">Administre su carta</h5>
                                <p class="card-text">Cree los platos de una manera simple, indique ingredientes y cantidades de forma precisa</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card mb-4 box-shadow">
                            <img class="card-img-top" src="Imagenes/Pedido.jpg" style="height: 200px; width: 100%; display: block;" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title">Pedidos</h5>
                                <p class="card-text">Cargue los pedidos desde cualquier lugar, cuente con el stock actualizado en todo momento. Indique alergias y/o pedidos especiales</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card mb-4 box-shadow">
                            <img class="card-img-top" src="Imagenes/Cocina_card.jpg" style="height: 200px; width: 100%; display: block;" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title">Procesos</h5>
                                <p class="card-text">La plataforma se encarga de administrar los procesos productivos, indicando que cocinar, cuando y donde de manera virtual. Reduzca considerablemente la carga administrativa del Jefe de cocina, y enfoque su expertise en crear los mejores platos para sus clientes.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="card mb-4 box-shadow">
                            <img class="card-img-top" src="Imagenes/Almacen_restaurante.jfif" style="height: 200px; width: 100%; display: block;" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title">Manejo de stock</h5>
                                <p class="card-text">Olvidese de pensar si se cuenta con los ingredientes para responder a la demanda. Food Business Manager se encarga de mantener actualizado su stock, sabiendo en todo momento la cantidad real de materia prima para su produccion.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card mb-4 box-shadow">
                            <img class="card-img-top" src="Imagenes/Factura_restaurante.jpg" style="height: 200px; width: 100%; display: block;" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title">Facturacion</h5>
                                <p class="card-text">Realice el cobro de cada consumo por mesa de manera agil</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card mb-4 box-shadow">
                            <img class="card-img-top" src="Imagenes/Estadisticas_card.jfif" style="height: 200px; width: 100%; display: block;" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title">Estadisticas</h5>
                                <p class="card-text">La plataforma obtendra estadisticas distintas estadisticas de consumo que le permitiran entender los gustos de sus clientes, recomendar platos segun su stock próximo a vencer y proyectar la cantidad de ingredientes necesarios segun el consumo promedio de los ultimos meses. Mientras mas utilice la plataforma, se generaran mas y mejores estadisticas que le permitiran tomar mejores decisiones</div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
