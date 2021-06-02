<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="LunchBoxOrder.Index" %>

<%@ Register Src="~/ChangePages.ascx" TagPrefix="uc1" TagName="ChangePages" %>


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

        .pageFontSize {
            font-size: 24px;
            margin-bottom: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container my-5 px-5 py-3 GroupBorder">
            <div class="row">
                <div class="col-12">
                    <asp:TextBox ID="TxtSearch" runat="server" Placeholder="團名"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
                    <asp:Button ID="btnLoginOut" runat="server" Text="登出" OnClick="btnLoginOut_Click" Enabled="false"/> 
                    <asp:Button ID="CreatGroup" runat="server" Text="揪團" OnClick="CreatGroup_Click" />
                </div>

                <asp:Repeater ID="GroupRepeater" runat="server" OnItemCommand="GroupRepeater_ItemCommand" OnItemDataBound="GroupRepeater_ItemDataBound">
                    <ItemTemplate>
                        <a href="CheckOrder.aspx?Sid=<%# Eval("Sid") %>" class="col-12 GroupBorder my-3">
                            <div class=" row">
                                <img src="Imgs/<%# Eval("GroupImgName") %>" class="col-3" />
                                <div class="col-3">
                                    <h2><%# Eval("GroupName") %></h2>
                                    <p>店名:<%# Eval("ShopName") %></p>
                                    <%--<p>已加入</p>--%>
                                </div>
                                <div class="col-6 row">
                                    <asp:Repeater ID="MenuRepeater" runat="server">
                                        <ItemTemplate>
                                            <div class="col-3">
                                                <span><%# Eval("FoodName") %></span>
                                            </div>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="d-flex justify-content-center pageFontSize">
            <uc1:ChangePages runat="server" ID="ChangePages" />
        </div>

    </form>
</body>
</html>
