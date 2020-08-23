<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AboutUs.aspx.vb" Inherits="FoodBusinessManager.AboutUs" %>

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
                                    <asp:Label ID="lblTituloAboutUs" runat="server" Text="¿Quienes sómos?"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblAboutUs1" CssClass="text-justify" runat="server" Text="Somos AFFINITI Solutions, una empresa joven de servicios de IT especializada en el desarrollo de software. Contamos con un equipo con conocimiento en la industria y el campo del desarrollo de software."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblAboutUs2" CssClass="text-justify" runat="server" Text="Nos enfocamos en la calidad, nuestros altos estándares transmiten a nuestros clientes por medio de nuestros productos, la seguridad que necesitan para elegirnos."></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <p>
                                    <asp:Label ID="lblAboutUs3" CssClass="text-justify" runat="server" Text="Desarrollamos software enfocado en la experiencia de usuario: Nuestro grupo de analistas entienden que las necesidades de los usuarios cambian, y son ellos los que marcan el sentido del producto."></asp:Label>
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
