<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="AplicacionWeb.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <asp:Label ID="Label1" runat="server" Text="Nombre" Width="150px"></asp:Label>
    <asp:TextBox ID="TxTNombre" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxTNombre" ErrorMessage="Campo Vacio"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Cedula" Width="150px"></asp:Label>
    <asp:TextBox ID="TxTCedula" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxTCedula" ErrorMessage="Campo Vacio"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Email" Width="150px"></asp:Label>
    <asp:TextBox ID="TxTEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxTEmail" ErrorMessage="Campo Vacio"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Telefono" Width="150px"></asp:Label>
    <asp:TextBox ID="TxTTelefono" runat="server" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label ID="Label5" runat="server" Text="Calificacion" Width="150px"></asp:Label>
    <asp:TextBox ID="TxTCalificacion" runat="server" TextMode="Number"></asp:TextBox>
    <br />
    <asp:Label ID="Label6" runat="server" Text="Pasword" Width="150px"></asp:Label>
    <asp:TextBox ID="TxTPasword" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxTPasword" ErrorMessage="Campo Vacio"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="BtNAgregar" runat="server" Text="Agregar" OnClick="BtNAgregar_Click" style="height: 26px" />
        <br />
        <asp:Label ID="LbLMensaje" runat="server"></asp:Label>
    <br />
        </div>
</asp:Content>
