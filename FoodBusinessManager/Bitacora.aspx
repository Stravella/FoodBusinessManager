<%@ Page Title="" Language="VB" AutoEventWireup="True" MasterPageFile="~/MasterPage.Master" CodeBehind="Bitacora.aspx.vb" Inherits="FoodBusinessManager.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link href="Estilos/Principal.css" rel="stylesheet" />

    <script runat="server">
        Sub BtnBuscar_OnClick(Source As Object, e As EventArgs)
            Span1.InnerHtml = Me.Buscar_click(20, 1)
            Span2.InnerHtml = Me.Paginar(20, 1)
        End Sub
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" runat="server">

        <div class="justify-content-center">
            <div align="center">
                <table>
                <tr>
                    <td>
                        <asp:Label ID="LblUsuarios" runat="server" Text="Usuarios"></asp:Label>
                    </td>
                    <td>
                       <asp:DropDownList ID="lstUsuarios" runat="server" Width="100"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            </div>
            <p></p>
            
            <div align="center">
                <table>
                <tr>
                    <td>
                        <asp:Label ID="LblTipoSuceso" runat="server" Text="Tipo Suceso"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="lstTipoSuceso" runat="server" Width="100"></asp:DropDownList>
                    </td>
                </tr>
            </table>
                
            </div>
            <p></p>
            <div style="text-align: center">
                <table align="center" width="1000">
                    <tr>
                        <td>
                            <asp:Label ID="LblDesde" runat="server" Text="Desde" style="align-content" CssClass="align-self-md-center"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LblHasta" runat="server" Text="Hasta" style="align-content"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Calendar ID="CalendarDesde" runat="server" SelectedDate="1990-01-01" style="align-content"></asp:Calendar>
                            </div>
                        </td>
                        <td>
                            <div>
                                <asp:Calendar ID="CalendarHasta" runat="server" SelectedDate="2020-01-24" style="align-content"></asp:Calendar>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <p></p>
            <div align="center">
                <p>
                    <button id="BtnBuscar"
                        onserverclick="BtnBuscar_OnClick"
                        class="btn"
                        runat="server">
                        Buscar
                    </button>
                </p>
            </div>

            <p></p>
            <%-- Acá meto la tabla --%>
            <div id="OutputDiv" align="center">

                <span id="Span1" runat="server" />

            </div>
            <%--Acá meto el paginado--%>
            <div id="OutputPagination" align="center">

                <span id ="Span2" runat="server"/>

            </div>

        </div>

    </form>
</asp:Content>
