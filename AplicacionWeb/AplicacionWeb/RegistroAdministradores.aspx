<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroAdministradores.aspx.cs" Inherits="AplicacionWeb.RegistroAdministradores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Email" Width="150px"></asp:Label>
        <asp:TextBox ID="TxTEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxTEmail" ErrorMessage="Campo Vacio"></asp:RequiredFieldValidator>
    
    </div>
        <asp:Label ID="Label2" runat="server" Text="Contraseña" Width="150px"></asp:Label>
        <asp:TextBox ID="TxTPasword" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxTPasword" ErrorMessage="Campo Vacio"></asp:RequiredFieldValidator>
        <p>
            <asp:Button ID="BtNAgregar" runat="server" OnClick="Button1_Click" Text="Agregar" />
        </p>
        <asp:Label ID="LbLMensaje" runat="server"></asp:Label>
    </form>
</body>
</html>
