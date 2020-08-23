<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="SeleccionarIdioma.aspx.vb" Inherits="FoodBusinessManager.SeleccionarIdioma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">

    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <h2>
                                    <asp:Label ID="lbl_SeleccionarIdioma" runat="server" Text="Seleccionar Idioma"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblSeleccioneIdioma" runat="server" Text="Selección de idioma:"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="lstIdiomas" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="btn_seleccionarIdioma" runat="server" Text="Seleccionar Idioma" CssClass="btn btn-block btn-warning" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
