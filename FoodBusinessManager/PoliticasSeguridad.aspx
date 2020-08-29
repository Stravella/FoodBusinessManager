<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="PoliticasSeguridad.aspx.vb" Inherits="FoodBusinessManager.PoliticasSeguridad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="titPoliticas" runat="server" Text="Politicas de seguridad"></asp:Label></h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="titConfidencialidad" runat="server" Text="CONFIDENCIALIDAD" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblConfidencialidad" CssClass="text-justify" runat="server" Text="Comprar en AFFINIT SOLUTIONS es 100% seguro, gracias a que cuenta con los estándares de seguridad más rigurosos utilizados a nivel mundial."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="titCompraSegura" runat="server" Text="COMPRA SEGURA" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblCompraSegura" CssClass="text-justify" runat="server" Text="Utilizamos un sistema de seguridad llamado SSL (Secure Socket Layer), que actualmente es el estándar usado por las compañías más importantes del mundo para realizar transacciones electrónicas seguras, lo que significa que toda tu información personal no podrá ser leída ni capturada por terceros mientras viaja por la red."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="titDeclaracionPrivacidad" runat="server" Text="DECLARACION DE PRIVACIDAD" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblDeclaracionPrivacidad" CssClass="text-justify" runat="server" Text="No traspasaremos, bajo ninguna modalidad y a ninguna empresa que no pertenezca a la empresa AFFINITI SOLUTIONS, datos personales de los clientes registrados en su tienda de Internet, y te asegura que estos serán manejados en forma absolutamente confidencial y conforme lo dispone la legislación vigente."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="titInformacionRequerida" runat="server" Text="INFORMACION REQUERIDA" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblInformacionRequerida" CssClass="text-justify" runat="server" Text="El formulario 'Registrate' de nuestro sitio web, pide a los usuarios información como su nombre, dirección electrónica, dirección física y número telefónico. Esta información es usada para responder las consultas acerca de nuestros productos y servicios, para facturas o para pagar cuentas. También se utiliza para mantener contacto con nuestros clientes. Bajo ninguna circunstancia, esta información es compartida con empresas no relacionadas con AFFINITI SOLUTIONS o con terceros."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="titLinksOtrosSitios" runat="server" Text="LINKS A OTROS SITIOS" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblLinksOtrosSitios" CssClass="text-justify" runat="server" Text="Este sitio puede contener 'links' a otros sitios. AFFINITI SOLUTIONS no es responsable de las prácticas de seguridad o privacidad, o el contenido de esos sitios. Asimismo, no avalamos ningún producto o servicio ofrecido en dichos sitios."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="titDatosPersonales" runat="server" Text="DATOS PERSONALES" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblDatosPersonales" CssClass="text-justify" runat="server" Text="El usuario dispondrá en todo momento de los derechos de información, rectificación y cancelación de los datos personales conforme a la Ley Nº 19.628 sobre protección de datos de carácter personal. Asimismo, si deseas modificar tu información personal, lo puedes hacer utilizando algunos de los siguientes canales de comunicación: Enviando un correo electrónico a venta@affinitisolutions.com.ar."></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
