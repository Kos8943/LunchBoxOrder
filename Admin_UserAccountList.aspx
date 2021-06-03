<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSide.Master" AutoEventWireup="true" CodeBehind="Admin_UserAccountList.aspx.cs" Inherits="LunchBoxOrder.Admin_UserAccountList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="Create" runat="server" CssClass="btn btn-info" Text="新增帳號" OnClick="Create_Click"/>
    <table class="table table-hover">
        <thead>
            <tr>
                <th scope="col">#</th>
                <th scope="col">帳號</th>
                <th scope="col">名稱</th>
                <th scope="col"></th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="UserAccountRepeater" runat="server" OnItemCommand="UserAccountRepeater_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("RowSid") %></th>
                        <td><%# Eval("Account") %></td>
                        <td><%# Eval("UserName") %></td>
                        <td>
                            <asp:Button ID="btnModify" runat="server" Text="修改" CommandArgument='<%# Eval("Sid") %>' CommandName="Modify"/>
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CommandArgument='<%# Eval("Sid") %>' CommandName="Delete"/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </tbody>
    </table>
</asp:Content>
