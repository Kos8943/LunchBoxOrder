<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSide.Master" AutoEventWireup="true" CodeBehind="Admin_CreateMenu.aspx.cs" Inherits="LunchBoxOrder.Admin_CreateMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group createShopArea">
        <br />
        <label for="<%= ShopDropDownList.ClientID %>">店名</label>
        <asp:Label ID="LabelMsg" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:DropDownList ID="ShopDropDownList" runat="server"></asp:DropDownList>
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
        <span class="redMsg">
            <asp:Literal ID="FoodImgFileLabel" runat="server" Visible="false"></asp:Literal></span>
        <asp:FileUpload ID="FoodImgFile" CssClass="form-control-file" runat="server" />
        <br />
        <asp:Button class="btn btn-primary" ID="btnCreateShop" runat="server" Text="建立" OnClick="btnCreateShop_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click"/>
        <br />
        <asp:Label ID="submitMsg" runat="server" Text="Label" Visible="false"></asp:Label>
    </div>
</asp:Content>
