<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OlvidoContraseña.aspx.vb" Inherits="FoodBusinessManager.OlvidoContraseña" %>

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
        <div style="max-width: 400px">
            <h2 class="form-signin-heading">
            Recuperar contraseña</h2>
            <label for="txtUsuario">Usuario</label>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" required></asp:TextBox>
        </div>
            <label for="txtMail">Mail</label>
            <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" required></asp:TextBox>
        <p>
            <asp:Button ID="btnRecuperarContraseña" runat="server" Text="Recuperar Contraseña" Class="btn btn-secondary"/>
        </p>
        <p>
            <div id="dvMensaje" runat="server" visible="false" class="alert-danger">
                <strong><asp:Label ID="lblRespuesta" runat="server"  ></asp:Label></strong>
            </div>
        </p>
    </form>
</body>
</html>
