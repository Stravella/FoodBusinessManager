<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Maestra.Master" CodeBehind="NewsletterBack.aspx.vb" Inherits="FoodBusinessManager.NewsletterBack" %>

<%@ MasterType VirtualPath="~/Maestra.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <div class="col">
                                    <center>
                                        <h4>Newsletter</h4>
                                    </center>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <div class="features-icons-item mx-auto mb-5 mb-lg-0 mb-lg-3">
                                        <div class="features-icons-icon d-flex">
                                            <i class="fas fa-newspaper m-auto text-primary fa-3x"></i>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label>ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <label>Titulo</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtTitulo" runat="server" ValidationGroup="newsletter"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtTitulo" ValidationGroup="Newsletter"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Cuerpo</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="txtCuerpo" TextMode="MultiLine" MaxLength="350" runat="server" ValidationGroup="newsletter"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCuerpo" ValidationGroup="Newsletter"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Imagen</label>
                                <asp:FileUpload class="form-control" ID="FileUpload1" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>
                                    Estado
                                </label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlEstado" DataValueField="id" DataTextField="nombre" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <asp:Button ID="btnAgregar" class="btn btn-block btn-success" runat="server" Text="Agregar" ValidationGroup="Newsletter" />
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col">
                        <asp:GridView ID="gv_Newsletter" CssClass="table table-hover table-bordered table-success" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="5" RowStyle-Height="40px" >
                            <HeaderStyle CssClass="thead-dark" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="titulo" HeaderText="Titulo" />
                                <asp:TemplateField HeaderText="Enviar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEnviar" ImageUrl="~/IconosSVG/play-circle-regular.svg" Text="Enviar" ToolTip="Enviar" runat="server" CommandName="Enviar" CommandArgument='<%# Eval("id") %>' Height="20px" Width="20px" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
    <br />
</asp:Content>
