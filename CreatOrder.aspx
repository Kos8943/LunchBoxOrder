<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatOrder.aspx.cs" Inherits="LunchBoxOrder.CreatOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="JQuery/jquery-3.6.0.min.js"></script>
    <style>
        #FoodImg{
            width:150px;
            height:150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="">
                <label for="GroupName">團名</label>
                <asp:TextBox ID="GroupName" runat="server"></asp:TextBox>
                <br />
                <label for="ShopName">店名</label>
                <asp:DropDownList ID="ShopName" runat="server"></asp:DropDownList>
                <br />
                <img src="Imgs/funamori.png" runat="server" id="FoodImg"/>
                <asp:DropDownList ID="FoodDropList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FoodImgs_SelectedIndexChanged">
                    <asp:ListItem Value="funamori.png" Text="生魚片"></asp:ListItem>
                    <asp:ListItem Value="ochaduke.png" Text="鰻魚蓋飯"></asp:ListItem>
                    <asp:ListItem Value="perimeni.png" Text="餃子"></asp:ListItem>
                    <asp:ListItem Value="pizza.png" Text="披薩"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Button ID="CreateGroup" runat="server" Text="建立" OnClick="CreateGroup_Click"/>
                <asp:Button ID="Cancel" runat="server" Text="取消" OnClick="Cancel_Click"/>
                <br />
                <asp:Label ID="ErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
