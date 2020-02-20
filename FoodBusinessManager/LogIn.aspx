<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LogIn.aspx.vb" Inherits="FoodBusinessManager.LogIn" %>

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
        Login</h2>
        
        <label for="txtUser">User</label>
        <br />
        <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Ingrese usuario" required/>
        
        <p>
            <label for="txtPassword">Password</label>
            <br />
            <asp:TextBox ID="txtPassword" type="password" runat="server" CssClass="form-control" placeholder="Ingrese contraseña" required/>
        </p
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Login" Width="90%" Class="btn btn-primary" />

        </div>
        <br />
        <div>
            <p><a href="Registrarse.aspx">Registrarse</a></p>
        </div>
        <div>
            <p><a href="OlvidoContraseña.aspx">¿Olvido la contraseña?</a></p>
        </div> 
        <br />
        <br />
        <div id="dvMensaje" runat="server" visible="false" class="alert-danger">
            <strong><asp:Label ID="lblRespuesta" runat="server"  ></asp:Label></strong>
        </div>
     </div>



   </form>
</body>
</html>
