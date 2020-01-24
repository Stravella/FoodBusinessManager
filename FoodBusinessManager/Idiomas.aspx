<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Idiomas.aspx.vb" Inherits="FoodBusinessManager.Idiomas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../Estilos/Principal.css" rel="stylesheet" />
    <form id="form1" runat="server">
        <div align="center">
            <br />
            <asp:Label ID="lbl_IdiomaTitulo" runat="server" Text="Idioma" CssClass="labels"></asp:Label>
            <br />
            <asp:DropDownList ID="lstCulturas" runat="server" AutoPostBack="true" CssClass="dropdown">
            </asp:DropDownList>
            &nbsp;
             <br />
            <br />
            <asp:GridView ID="grillaTraduccion" runat="server" AutoGenerateColumns="False" Width="80%" ssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows" 
            AllowPaging="True"> 
                <Columns>
                    <asp:BoundField HeaderText="Id Etiqueta" DataField="id_etiqueta" />
                    <asp:BoundField ApplyFormatInEditMode="True" DataField="traduccion" HeaderText="Traduccion" />
                    <asp:TemplateField HeaderText="Nueva traduccion">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTraduccion" runat="server" CssClass="textbox" width="80%" required ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />

            <asp:Label ID="lbl_Respuesta" runat="server" Text="Label" CssClass="labels"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btn_ModificarIdioma" runat="server" Text="Modificar Idioma" CssClass="mybutton"/>
            <br />
            <br />
        </div>
    </form>
</asp:Content>
