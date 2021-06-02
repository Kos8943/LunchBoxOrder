<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LunchBoxOrder.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="JQuery/jquery-3.6.0.min.js"></script>
    <style>
        #ErrorMsg{
            color:red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <lebel for="Account">帳號</lebel>
            <asp:TextBox ID="Account" runat="server" AutoCompleteType="None"></asp:TextBox>
            <br />
            <lebel for="Password">密碼</lebel>
            <asp:TextBox ID="Password" runat="server" AutoCompleteType="None"></asp:TextBox>
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click"/>
            <asp:Button ID="btnGoIndex" runat="server" Text="返回首頁" OnClick="btnGoIndex_Click"/>
            <br />
            <asp:Label ID="ErrorMsg" runat="server" Text="" Visible="false"></asp:Label>

        </div>
    </form>
</body>
</html>
