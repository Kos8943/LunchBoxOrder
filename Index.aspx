<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="LunchBoxOrder.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Bootstrap/js/bootstrap.js"></script>
    <link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="JQuery/jquery-3.6.0.min.js"></script>
    <style>
        .GroupBorder {
            border: 1px solid #000;
            color: #000;
        }

            .GroupBorder:hover {
                text-decoration: none;
                color: #000;
            }

            .GroupBorder img {
                width: 200px;
                height: 200px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container my-5 px-5 py-3 GroupBorder">
            <div class="row">
                <div class="col-12">
                    <asp:TextBox ID="TxtSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" />
                    <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
                    <asp:Button ID="CreatGroup" runat="server" Text="揪團" OnClick="CreatGroup_Click" />
                </div>

                <asp:Repeater ID="GroupRepeater" runat="server" OnItemCommand="GroupRepeater_ItemCommand">
                    <ItemTemplate>
                        <a href="CheckOrder.aspx?Sid=<%# Eval("Sid") %>" class="col-12 GroupBorder my-3">
                            <div class="col-12 row">
                                <img src="Imgs/<%# Eval("GroupImgName") %>" />
                                <div class="">
                                    <h2><%# Eval("GroupName") %></h2>
                                    <p>店名:<%# Eval("ShopName") %></p>
                                    <p>已加入</p>
                                </div>
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>



            </div>
        </div>
    </form>
</body>
</html>
