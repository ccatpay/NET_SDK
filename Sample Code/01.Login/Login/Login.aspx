<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SampleCode.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="text-align:center;">
        <div style="background-color: rgba(104,238,104,0.6); border-radius:5px; text-align:center; padding: 25px;">
            <p style="text-align:center;">
                <label id="lbLoginAccount" style="margin:0px 5px;font-weight:600;">登入帳號</label>
                <asp:TextBox ID="tbLoginAccount" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbLoginPassword" style="margin:0px 5px;font-weight:600;">登入密碼</label>
                <asp:TextBox ID="tbLoginPassword" runat="server" style="margin:0px 5px;" type="password"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <asp:Button ID="btSubmit" runat="server" Text="登入" OnClick="btSubmit_Click"/>
            </p>
        </div>
        <p></p>
        <p style="text-align:center;">
            <span id="spSuccessMessage" runat="server" style="color:forestgreen; font-weight:800;"></span>
            <span id="spErrorMessage" runat="server" style="color:red; font-weight:800;"></span>
        </p>
    </form>
</body>
</html>

<style>
    html, body {
    height: 100%;
    }
    
    html {
        display: table;
        margin: auto;
    }
    
    body {
        display: table-cell;
        vertical-align: middle;
    }
</style>