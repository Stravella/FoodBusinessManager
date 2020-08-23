<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="FAQS.aspx.vb" Inherits="FoodBusinessManager.FAQS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="lblFAQS" runat="server" Text="Preguntas frecuentes"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <h4>
                                    <asp:Label ID="lblTitAdquirirServicio" runat="server" Text="¿Como puedo adquirir el servicio?" Font-Bold="true"></asp:Label>
                                </h4>
                                <p>
                                    <asp:Label ID="lblAdquirirServicio" CssClass="text-secondary" runat="server" Text="Puede adquirir el servicio desde la opcion de compra desde la página"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <h4>
                                    <asp:Label ID="lblTitAdquirirPais" runat="server" Text="¿Puedo adquirir el servicio desde cualquier punto del país?" Font-Bold="true"></asp:Label>
                                </h4>
                                <p>
                                    <asp:Label ID="lblAdquirirPais" CssClass="text-secondary" runat="server" Text="Sí. En caso de elegir la instalación presencial, contactese con alguno de nuestros representates para conocer las zonas de instalación"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <h4>
                                    <asp:Label ID="lblTitCuantoTiempoInstalacion" runat="server" Text="¿Cuanto tiempo después de adquirido el producto se realiza la instalación del mismo?" Font-Bold="true"></asp:Label>
                                </h4>
                                <p>
                                    <asp:Label ID="lblCuantoTiempoInstalacion" CssClass="text-secondary" runat="server" Text="La instalación se realiza en el lapso de 24hs via Acceso Remoto. En caso de necesitar una instalación física, el tiempo aproximado es de 2 a 5 días hábiles "></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <h4>
                                    <asp:Label ID="lblTitBajaServicio" runat="server" Text="¿Como puedo dar la baja del servicio?" Font-Bold="true"></asp:Label>
                                </h4>
                                <p>
                                    <asp:Label ID="lblBajaServicio" CssClass="text-secondary" runat="server" Text="Contactandose con alguno de nuestro representantes puede iniciar el proceso de baja del servicio "></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <h4>
                                    <asp:Label ID="lblTitRequisitosInstalacion" runat="server" Text="¿Cuales son los requisitos para instalar el servicio?" Font-Bold="true"></asp:Label>
                                </h4>
                                <p>
                                    <asp:Label ID="lblRequisitosInstalacion" CssClass="text-secondary" runat="server" Text="Los requisitos mínimos para la instalacion son : Procesador  Intel® Core™ 2 Duo E6600 o mejor, Memoria 4gb de ram o mejor, Almacenamiento 1gb de espacio o mejor, Conexión a internet"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
