<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSide.Master" AutoEventWireup="true" CodeBehind="Admin_ShopAndMenuList.aspx.cs" Inherits="LunchBoxOrder.Admin_ShopAndMenuList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="Create" runat="server" CssClass="btn btn-info" Text="新增店家"/>
    <asp:Button ID="CreateMenu" runat="server" CssClass="btn btn-primary" Text="新增菜單"/>
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
            <asp:Repeater ID="ShopMenuRepeater" runat="server" >
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("RowSid") %></th>
                        <td><%# Eval("FoodName") %></td>
                        <td><%# Eval("Price") %></td>
                        <td>
                            <asp:Button ID="btnModify" runat="server" Text="修改"/>
                            <asp:Button ID="btnDelete" runat="server" Text="刪除"/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </tbody>
    </table>
</asp:Content>
