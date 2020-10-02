<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="AdministrarUsuarios.aspx.vb" Inherits="FoodBusinessManager.AdministrarUsuarios1" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-users m-auto text-primary fa-3x"></i>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                     <h4>
                                         <asp:Label ID="AdministrarUsuarios" runat="server" Text="Administrar usuarios"></asp:Label>                                       
                                     </h4>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <center>
                                     <hr />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3">
                                <label>ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtID" runat="server" placeholder="Id" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-9">
                                <label>
                                    <asp:Label ID="lblMail" runat="server" Text="Mail"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtMail" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqMail" runat="server" ErrorMessage="*" ControlToValidate="txtMail" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatMail" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtMail" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqNombre" runat="server" ErrorMessage="*" ControlToValidate="txtNombre" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantNombre" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtNombre" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqApellido" runat="server" ErrorMessage="*" ControlToValidate="txtApellido" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantApellido" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtApellido" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqUsuario" runat="server" ErrorMessage="*" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantUsuario" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtUsuario" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" TextMode="Password" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqContraseña" runat="server" ErrorMessage="*" ControlToValidate="txtContraseña" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="cantContraseña" runat="server" ErrorMessage="Formato incorrecto" Display="Dynamic" ValidationExpression="^([\S\s]{0,100})$" ControlToValidate="txtContraseña" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblDNI" runat="server" Text="DNI"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDNI" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqDNI" runat="server" ErrorMessage="*" ControlToValidate="txtDNI" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatDNI" runat="server" ValidationExpression="[0-9]{8}" ErrorMessage="Formato invalido" ControlToValidate="txtDNI" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblCUIT" runat="server" Text="CUIT"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCUIT" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCUIT" runat="server" ErrorMessage="*" ControlToValidate="txtCUIT" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatCUIT" runat="server" ValidationExpression="\b(20|23|24|27|30|33|34)(\D)?[0-9]{8}(\D)?[0-9]" ErrorMessage="Formato invalido" ControlToValidate="txtCUIT" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    <asp:Label ID="lblRazonSocial" runat="server" Text="Razon social"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtRazonSocial" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtRazonSocial" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^([\S\s]{0,100})$" ErrorMessage="Formato invalido" ControlToValidate="txtRazonSocial" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblDireccion" runat="server" Text="Direccion"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqDireccion" runat="server" ErrorMessage="*" ControlToValidate="txtDireccion" ForeColor="Red" ValidationGroup="Usuario"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqTelefono" runat="server" ErrorMessage="*" ControlToValidate="txtTelefono" ForeColor="Red" ValidationGroup="Usuario"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatTelefono" runat="server" ErrorMessage="Formato inválido" ValidationExpression="^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$" ControlToValidate="txtTelefono" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>
                                    <asp:Label ID="lblCP" runat="server" Text="Código postal"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCP" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCP" runat="server" ErrorMessage="Ingrese su código postal" ControlToValidate="txtCP" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="formatCP" runat="server" ErrorMessage="Formato inválido" ValidationExpression="\b[a-zA-Z0-9]{7}\b|\b[a-zA-Z0-9]{10}\b+" ControlToValidate="txtCP" ForeColor="Red" ValidationGroup="Usuario"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>
                                    <asp:Label ID="lblLocalidad" runat="server" Text="Localidad"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtLocalidad" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqLocalidad" runat="server" ErrorMessage="*" ControlToValidate="txtLocalidad" ForeColor="Red" ValidationGroup="Usuario"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>
                                    <asp:Label ID="lblProvincia" runat="server" Text="Provincia"></asp:Label>
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlProvincias" DataValueField="id" DataTextField="provincia" CssClass="form-control" runat="server" ></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--CTA--%>
                <div class="row">
                    <div class="col">
                        <asp:Button ID="btnAgregar" class="btn btn-lg btn-block btn-success" runat="server" Text="Agregar" ValidationGroup="Usuario" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnModificar" class="btn btn-lg btn-block btn-warning" runat="server" Text="Modificar" Visible="false"  ValidationGroup="Usuario"/>                       
                    </div>
                    <div class="col-6">
                        <asp:Button ID="btnCancelar" class="btn btn-lg btn-block btn-danger" runat="server" Text="Cancelar" Visible="false" />                       
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Lista Usuarios</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView CssClass="table table-hover table-bordered table-success " ID="gv_Usuarios" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" RowStyle-Height="40px" OnRowCommand="gv_Usuarios_RowCommand">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="username" HeaderText="Usuario" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                                        <asp:BoundField DataField="mail" HeaderText="Email" />
                                        <asp:BoundField DataField="bloqueado" HeaderText="Bloqueado" />
                                        <asp:BoundField DataField="perfil.nombre" HeaderText="Perfil" />
                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEditar" ImageUrl="~/IconosSVG/edit-solid.svg" Text="Editar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Borrar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEliminar" ImageUrl="~/IconosSVG/trash-solid.svg" Text="Eliminar" runat="server" CommandName="Borrar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
