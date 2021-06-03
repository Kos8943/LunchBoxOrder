<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSide.Master" AutoEventWireup="true" CodeBehind="Admin_CreateUserAccount.aspx.cs" Inherits="LunchBoxOrder.Admin_CreateUserAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .redMsg{
            color:red;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form>
        <div class="container">
            <div class="form-group">
                <label for="ContentPlaceHolder1_txtAccount">*帳號</label>
                <span class="redMsg"><asp:Literal ID="ltAccount" runat="server" Visible="false"></asp:Literal></span>
                <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control" placeholder="Account"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="ContentPlaceHolder1_txtPassword">*密碼</label>
                <span class="redMsg"><asp:Literal ID="ltPassword" runat="server" Visible="false"></asp:Literal></span>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ContentPlaceHolder1_txtPassword">*名稱</label>
                <span class="redMsg"><asp:Literal ID="ltUserName" runat="server" Visible="false"></asp:Literal></span>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
            </div>
            <div class="form-group form-check">
                <asp:CheckBox ID="CheckBoxIsAdmin" CssClass="form-check-input" runat="server" />
                <label class="form-check-label" for="ContentPlaceHolder1_CheckBoxIsAdmin">管理者帳號</label>
            </div>

            <div class="form-group">
                <label for="UserImgFile">*上傳照片</label>
                <span class="redMsg"><asp:Literal ID="ltUploadImg" runat="server" Visible="false"></asp:Literal></span>
                <asp:FileUpload ID="UserImgFile" CssClass="form-control-file" runat="server" />
                <%--<input type="file" class="form-control-file" id="UserImgFile">--%>
            </div>
            <%--<button type="submit" class="btn btn-primary">Submit</button>--%>
            <asp:Button ID="btnSubmit" runat="server" Text="建立" OnClick="btnSubmit_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click"/>
            <br />
            <span class="redMsg"><asp:Literal ID="ltMsg" runat="server" Visible="false"></asp:Literal></span>
        </div>

    </form>
</asp:Content>
