<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignarEvaluador.aspx.cs" Inherits="AplicacionWeb.AsignarEvaluador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Lista Emprendimientos"></asp:Label>
    
        <br />
    
        <asp:Button ID="BtNListarEmprendimiento" runat="server" OnClick="BtNListarEmprendimiento_Click" Text="Listar" />
    
    </div>
        <p>
            <asp:ListBox ID="LsBEmprendimiento" runat="server" Width="610px"></asp:ListBox>
        </p>
            <asp:Label ID="Label2" runat="server" Text="Lista Evaluadores"></asp:Label>
            <br />
            <asp:Button ID="BtNListarEvaluadores" runat="server" OnClick="BtNListarEvaluadores_Click" Text="Listar" />
        <br />
        <br />
        <asp:ListBox ID="LsBEvaluadores" runat="server" Width="610px"></asp:ListBox>
            <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Ingrese Id del Emprendimiento:"></asp:Label>
        <br />
        <asp:TextBox ID="TxtIdEmprendimiento" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Ingrese Cedula del Evaluador:"></asp:Label>
        <br />
        <asp:TextBox ID="TxtCedulaEvaluador" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Ingrese Email del Evaluador:"></asp:Label>
        <br />
        <asp:TextBox ID="TxtEmailEvaluador" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BtnEvaluadorEmprendimeinto" runat="server" OnClick="BtnEvaluadorEmprendimeinto_Click" Text="Asignar Evaluador a Emprendimiento" />
        <br />
        <br />
        <asp:Label ID="LbLMensaje" runat="server"></asp:Label>
    </form>
</body>
</html>
