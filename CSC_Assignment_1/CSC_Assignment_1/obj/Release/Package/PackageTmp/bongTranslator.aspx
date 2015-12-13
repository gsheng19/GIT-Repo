<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bongTranslator.aspx.cs" Inherits="CSC_Assignment_1.bongTranslator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>My Translator</h2>
        <span>Enter your text in English:</span>

    </div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:button runat="server" OnClick="Button1_Click" Text="Translate"></asp:button> <br />
        <span>Here is your translation:</span> <br />
        <asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
