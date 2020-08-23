<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Contactenos.aspx.vb" Inherits="FoodBusinessManager.Contactenos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="row">
                        <div class="col">
                            <img class="card-img-top rounded mx-auto d-block" src="Imagenes/AffinitiLogo.png" style="width: 200px; height: 200px" alt="Card image" />
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <p>
                                        <asp:Label ID="lblAffiniti" runat="server" Text="Affiniti Solutions" Font-Bold="true"></asp:Label>
                                    </p>
                                    <p>
                                        <asp:Label ID="lblTelContacto" runat="server" Text="Telefono: 11-3256-3402"></asp:Label>
                                    </p>
                                    <p>
                                        <asp:Label ID="lblMailContacto" runat="server" Text="Mail: consultas@affiniti.com.ar"></asp:Label>
                                    </p>
                                    <p>
                                        <asp:Label ID="lblDireccionContacto" runat="server" Text="Buenos Aires, Capital Federal"></asp:Label>
                                    </p>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>

</asp:Content>

