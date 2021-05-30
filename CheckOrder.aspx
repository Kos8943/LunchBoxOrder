﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOrder.aspx.cs" Inherits="LunchBoxOrder.CheckOrder" %>

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
        }

        .GroupImg {
            height: 300px;
            width: 300px;
        }

        .MenuImg {
            height: 150px;
            width: 150px;
        }

        .MenuBorder {
            border: 1px solid #000;
        }

        .OrderImg {
            height: 150px;
            width: 150px;
        }

        .OrderBorder {
            border: 1px solid #000;
        }

        .btnDeleteOrder {
            border: none;
            background: rgba(0,0,0,0);
            font-size: 20px;
            height: 25px;
            width: 25px;
        }

        .OrderListArea {
            height: 150px;
            width: 150px;
            overflow: auto;
            overflow-x: hidden;
            padding-top: 10px;
        }

        .ConfirmAreaBorder {
            border: 1px solid #000;
            position: relative;
        }

        #btnConfirm {
            width: 100px;
            height: 35px;
            position: absolute;
            top: 75%;
            left: 88.5%;
        }

        #btnTurnBack {
            width: 100px;
            height: 35px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container my-5 GroupBorder">

            <%-- ----表頭區域---- --%>
            <div class="row m-2">
                <img src="Imgs/funamori.png" class="GroupImg" runat="server" id="GroupImg" />
                <div>
                    <h1 runat="server" id="GroupName">團一</h1>
                    <label>狀態:</label>
                    <asp:DropDownList ID="GroupStatusDropList" runat="server">
                        <asp:ListItem Value="0" Text="未結團"></asp:ListItem>
                        <asp:ListItem Value="1" Text="結團"></asp:ListItem>
                        <asp:ListItem Value="2" Text="已到"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnChangeStatus" runat="server" Text="修改狀態" />
                </div>
            </div>

            <div class="row m-2">
                <div class="col-3">
                    <asp:Literal ID="ShopName" runat="server"></asp:Literal>
                </div>
                <div class="col-3">
                    <asp:Literal ID="GroupLeader" runat="server"></asp:Literal>
                </div>
            </div>
            <%-- ----表頭區域---- --%>


            <%-- ----菜單區域---- --%>
            <div class="row m-2">
                <asp:Repeater ID="MenuRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col-4 p-4">
                            <div class="row MenuBorder">
                                <img src="Imgs/funamori.png" class="MenuImg" />
                                <div class="p-3">
                                    <p><%# Eval("FoodName") %></p>
                                    <span>NT$ <%# Eval("Price") %></span>
                                    <div>
                                        <span>數量:</span>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <%-- ----菜單區域---- --%>


            <%-- ----已訂便當區域---- --%>
            <div class="row m-2">

                <asp:Repeater ID="OrderRepeater" runat="server" OnItemDataBound="OrderRepeater_ItemDataBound" OnItemCommand="OrderRepeater_ItemCommand">
                    <ItemTemplate>
                        <div class="col-4 p-4">
                            <div class="row OrderBorder">
                                <asp:Button ID="DeleteOrder" CssClass="btnDeleteOrder" runat="server" Text="X" CommandName="Delete" CommandArgument='<%# Eval("AccountSid") %>'/>
                                <img src="Imgs/ochaduke.png" class="OrderImg" />
                                <div class="col-5 OrderListArea">
                                    <asp:Repeater ID="OrderDetailRepeater" runat="server">
                                        <ItemTemplate>
                                            <p><%# Eval("FoodName") %> * <%# Eval("Qty") %></p>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <%-- ----已訂便當區域---- --%>


            <%-- ----確認區域---- --%>
            <div class="row m-3 p-3 ConfirmAreaBorder">
                <img src="Imgs/perimeni.png" class="OrderImg" />
                <div class="row ml-5">
                    <div class="col-12">
                        <span>三杯</span>
                        <span>X6</span>
                    </div>
                    <div class="col-12">
                        <span>三杯</span>
                        <span>X6</span>
                    </div>
                    <div class="col-12">
                        <span>三杯</span>
                        <span>X6</span>
                    </div>
                    <div class="col-12">
                        <span>三杯</span>
                        <span>X6</span>
                    </div>
                </div>
                <asp:Button ID="btnConfirm" runat="server" Text="OK" />
            </div>
            <div class="m-3 pr-4 d-flex flex-row-reverse">
                <asp:Button ID="btnTurnBack" runat="server" Text="返回" />
            </div>


            <%-- ----確認區域---- --%>
        </div>
    </form>
</body>
</html>
