<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSide.Master" AutoEventWireup="true" CodeBehind="Admin_ShopAndMenuList.aspx.cs" Inherits="LunchBoxOrder.Admin_ShopAndMenuList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .createShopArea{
            display:none;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:Button type="Button" ID="Create" runat="server" CssClass="btn btn-info" Text="新增店家" />--%>
    <button type="Button" id="Create" class="btn btn-info">新增店家</button>
    <asp:Button ID="CreateMenu" runat="server" CssClass="btn btn-primary" Text="新增菜單" OnClick="CreateMenu_Click"/>

        <div class="form-group createShopArea">
            <br />
            <label for="<%= ShopName.ClientID %>">店名</label>
            <asp:Label ID="LabelMsg" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox class="form-control" ID="ShopName" runat="server" placeholder="店名"></asp:TextBox>
            <br />
            <label for="<%= FoodName.ClientID %>">餐點名稱</label>
            <asp:Label ID="FoodNameLabel" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox class="form-control" ID="FoodName" runat="server" placeholder="餐點名稱"></asp:TextBox>
            <br />
            <label for="<%= Price.ClientID %>">單價</label>
            <asp:Label ID="PriceLabel" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:TextBox class="form-control" ID="Price" runat="server" placeholder="單價"></asp:TextBox>
            <br />
            <label for="<%= FoodImgFile.ClientID %>">*上傳照片</label>
                <span class="redMsg"><asp:Literal ID="FoodImgFileLabel" runat="server" Visible="false"></asp:Literal></span>
                <asp:FileUpload ID="FoodImgFile" CssClass="form-control-file" runat="server" />
            <br />
            <asp:Button class="btn btn-primary" ID="btnCreateShop" runat="server" Text="建立" OnClick="btnCreateShop_Click"/>
            
        </div>

    <br />
    <asp:Label ID="Label1" runat="server" Text="選擇查詢店家"></asp:Label>
    <asp:DropDownList ID="ShopDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ShopDropDownList_SelectedIndexChanged"></asp:DropDownList>
    <table class="table table-hover">
        <thead>
            <tr>
                <th scope="col">#</th>
                <th scope="col">餐點名稱</th>
                <th scope="col">價格</th>
                <th scope="col"></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="ShopMenuRepeater" runat="server" OnItemCommand="ShopMenuRepeater_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("RowSid") %></th>
                        <td><%# Eval("FoodName") %></td>
                        <td><%# Eval("Price") %></td>
                        <td>
                            <asp:Button ID="btnModify" runat="server" Text="修改" CommandArgument='<%# Eval("MenuSid") %>' CommandName="UpDate"/>
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CommandArgument='<%# Eval("MenuSid") %>' CommandName="Delete" OnClientClick="javascript:return confirm('確定刪除?')"/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </tbody>
    </table>
    
    <script>

        //$(document).ready(function () {
        //    $(".createShopArea").hide();
        //});

        $("#Create").click(function () {
            $(".createShopArea").toggle(300);
        });
    </script>
</asp:Content>
