<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestDphCash.aspx.cs" Inherits="SampleCode.RequestDphCash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="text-align:center;">
        <div style="background-color: rgba(104,238,104,0.6); border-radius:5px; text-align:center; padding: 5px; margin:5px;">
            <p style="text-align:center;">
                <label id="lbLoginAccount" style="margin:0px 5px;font-weight:600;"><span style="color:red; font-weight:800;">*</span>登入帳號</label>
                <asp:TextBox ID="tbLoginAccount" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbLoginPassword" style="margin:0px 5px;font-weight:600;"><span style="color:red; font-weight:800;">*</span>登入密碼</label>
                <asp:TextBox ID="tbLoginPassword" runat="server" style="margin:0px 5px;" type="password"></asp:TextBox>
            </p>
        </div>
        <div style="background-color: rgba(128,255,255,0.6); border-radius:5px; text-align:center; padding: 5px; margin:5px;">
            <p style="text-align:center;">
                <label id="lbCustomerOrderNo" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>契客訂單號碼
                </label>
                <asp:TextBox ID="tbCustomerOrderNo" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbOrderAmount" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>訂單/交易金額
                </label>
                <asp:TextBox ID="tbOrderAmount" runat="server" style="margin:0px 5px;" type="number" max="100000" min="1"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbCrAmount" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>指定請款金額
                </label>
                <asp:TextBox ID="tbCrAmount" runat="server" style="margin:0px 5px;" type="number" max="100000" min="1"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbSendTime" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>傳送時間
                </label>
                <asp:TextBox ID="tbSendTime" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <asp:Button ID="btSubmit" runat="server" Text="指定請款" OnClick="btSubmit_Click"/>
            </p>
        </div>
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

    p {
        display: block;
        margin-block-start: 0.6em;
        margin-block-end: 0.6em;
        margin-inline-start: 0px;
        margin-inline-end: 0px;
    }
</style>