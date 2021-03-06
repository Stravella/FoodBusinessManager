﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="Pago.aspx.vb" Inherits="FoodBusinessManager.Pago" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col">
                <center>
                    <h4>Pago</h4>
                </center>
            </div>
        </div>
        <hr />
        <%-- Carrito solo lectura --%>
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                    <div class="features-icons-icon d-flex">
                                        <i class="fas fa-shopping-cart m-auto text-primary fa-3x"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <asp:GridView CssClass="table table-hover table-bordered table-info " ID="gv_Carrito" runat="server" AutoGenerateColumns="False" EmptyDataText="No hay nada en su carrito" HorizontalAlign="Center" AllowPaging="True" PageSize="5" RowStyle-Height="40px" ShowFooter="true">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Imagen" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img id="imagen" runat="server" width="75" height="75" src='<%#Eval("Servicio.Imagen.Img64")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="servicio.nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="servicio.precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="importeTotal" HeaderText="Total" DataFormatString="{0:C}" />
                                    </Columns>
                                    <RowStyle Height="40px"></RowStyle>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                            </div>
                            <div class="col-2">
                            </div>
                            <div class="col-4">
                                <asp:Label ID="lblPrecioTotal" CssClass="form-control" runat="server" Text="Precio Total : "></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <%-- Datos Domicilio --%>
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                    <div class="features-icons-icon d-flex">
                                        <i class="fas fa-house-user m-auto text-primary fa-3x"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-6">
                                <label>
                                    Nombre
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-6">
                                <label>
                                    Apellido
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    CUIT
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCUIT" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    Direccion
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-4">
                                <label>
                                    Provincia
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtProvincia" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-4">
                                <label>
                                    Codigo postal
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCP" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-4">
                                <label>
                                    Localidad
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtLocalidad" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <%-- Formas de Pago --%>
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                    <div class="features-icons-icon d-flex">
                                        <i class="fas fa-credit-card m-auto text-primary fa-3x"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <%-- Modulo tarjeta --%>
                        <div class="row">
                            <div class="col">
                                <label>
                                    Nombre
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombreApe" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ErrorMessage="Este campo es requerido" ControlToValidate="txtNombreApe"
                                        ForeColor="Red" ValidationGroup="Tarjeta">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNombreApe"
                                        ValidationExpression="^[a-zA-Z\s]{10,30}$" ForeColor="Red"
                                        ErrorMessage="Ingrese el nombre cómo figura en su tarjeta." ValidationGroup="Tarjeta">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>
                                    Numero Tarjeta
                                    <img id="imgTC" src="IconosSVG/credit-card-solid.svg" width="40" height="20" />
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNumeroTarjeta" CssClass="form-control" runat="server" onkeyup="ValidarTC(this)" placeholder="Ingrese los 16 dígitos sin guiónes ni espacios"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="Este campo es requerido" ControlToValidate="txtNumeroTarjeta"
                                        ForeColor="Red" ValidationGroup="Tarjeta">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="El formato es inválido" ValidationExpression="\d{16}"
                                        ControlToValidate="txtNumeroTarjeta" ForeColor="Red" ValidationGroup="Tarjeta">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <label>
                                    Fecha Vencimiento
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtFechaVencimiento" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ErrorMessage="Este campo es requerido" ControlToValidate="txtFechaVencimiento"
                                        ForeColor="Red" ValidationGroup="Tarjeta"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtFechaVencimiento"
                                        ValidationExpression="((0[1-9])|(1[012]))/\d{2}" ForeColor="Red"
                                        ErrorMessage="Ingrese los 4 dígitos en formato MM/YY." ValidationGroup="Tarjeta">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-6">
                                <label>
                                    Codigo Seguridad
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCodigoSeguridad" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ErrorMessage="Este campo es requerido" ControlToValidate="txtCodigoSeguridad"
                                        ForeColor="Red" ValidationGroup="Tarjeta">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCodigoSeguridad"
                                        ValidationExpression="^[0-9]{3}$" ErrorMessage="Ingrese solo los 3 dígitos."
                                        ForeColor="Red" ValidationGroup="Tarjeta">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-4">
                                <asp:Label ID="lblRespuestaTarjeta" CssClass="form-control" runat="server" Visible="false" Enabled="false"></asp:Label>
                            </div>
                            <div class="col-4">
                            </div>
                            <div class="col-4">
                                <asp:Label ID="lblMontoTarjeta" CssClass="form-control" runat="server" Visible="false" Enabled="false"></asp:Label>
                            </div>
                        </div>
                        <%-- Modulo nota crédito --%>
                        <hr />
                        <div class="row">
                            <div class="col">
                                <h5>Notas de crédito </h5>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col">
                                <asp:GridView ID="grdNotasCredito" runat="server" CssClass="table table-hover table-bordered table-info" AutoGenerateColumns="false" HorizontalAlign="Center" AllowPaging="false" RowStyle-Height="40px">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="id_factura" HeaderText="Nro. Factura" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                        <asp:BoundField DataField="importe" HeaderText="Importe" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="btnValidarTarjeta" CssClass="btn btn-warning" runat="server" Text="Validar Medio de Pago" />
                            </div>
                            <div class="col-4">
                            </div>
                            <div class="col-4">
                                <asp:Label ID="lblMontoNota" CssClass="form-control" runat="server" Visible="false" Enabled="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-6">
                <asp:Button ID="btnCancelar" CssClass="btn btn-block btn-danger" runat="server" Text="Cancelar" />
            </div>
            <div class="col-6">
                <asp:Button ID="btnComprar" CssClass="btn btn-block btn-success" runat="server" Text="Comprar" />
            </div>
        </div>
        <br />
    </div>


    <script>
        function ValidarTC(nroTC) {
            var imagen = document.getElementById("imgTC");
            if (nroTC.value.length > 2) {
                var re = new RegExp("^4");
                var nro = nroTC.value;

                if (nro.match(re) != null) {
                    imagen.src = "IconosSVG/cc-visa-brands.svg";
                    //$("#TCValida").value = true;
                    return true;
                }
                re = new RegExp("^(34|37)");
                if (nro.match(re) != null) {
                    imagen.src = "IconosSVG/cc-amex-brands.svg";

                    return true;
                }
                re = new RegExp("^5[1-5]");
                if (nro.match(re) != null) {
                    imagen.src = "IconosSVG/cc-mastercard-brands.svg";

                    return true;
                }
                imagen.src = "IconosSVG/credit-card-solid.svg";

            }
            else {
                imagen.src = "IconosSVG/credit-card-solid.svg";

            }
        }
    </script>
</asp:Content>
