<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NuevaContraseña.aspx.vb" Inherits="FoodBusinessManager.NuevaContraseña" %>

<!DOCTYPE html>
<script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
    media="screen" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style ="max-width: 400px">
            <h2 class="form-signin-heading">
             Confirmacion contraseña</h2>

            <label for="txtUsuario">Usuario</label>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" required></asp:TextBox>
            <br />
            <br />
            <label for="txtPassword">Contraseña</label>
            <asp:TextBox ID="txtPassword" type="password" runat="server" CssClass="form-control" required></asp:TextBox>
            <br />
            <br />
            <label for="txtConfirmPassword">Confirmar contraseña</label>
            <asp:TextBox ID="txtConfirmPassword" type="password" runat="server" CssClass="form-control" required></asp:TextBox>
        </div>
        <p>
            <asp:Button ID="btnSubmit" runat="server" Text="Confirmar" Class="btn btn-secondary" />
        </p>
        <div id="dvMensaje" runat="server" visible="false" class="alert-danger">
            <strong>
               <asp:Label ID="lblRespuesta" runat="server"></asp:Label>
            </strong>
        </div>
    </form>
</body>
</html>
