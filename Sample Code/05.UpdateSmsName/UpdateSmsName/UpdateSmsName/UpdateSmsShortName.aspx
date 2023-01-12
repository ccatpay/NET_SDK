﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateSmsShortName.aspx.cs" Inherits="SampleCode.UpdateSmsShortName" %>

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
                <label id="lbCustomerId" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>契客代碼
                </label>
                <asp:TextBox ID="tbCustomerId" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbServiceType" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>金流服務類型
                </label>
                <asp:DropDownList ID="ddlServiceType" runat="server">
                    <Items>
                       <asp:ListItem Text="請選擇金流服務類型" Value="" />
                   </Items>
                </asp:DropDownList>
            </p>
            <p style="text-align:center;">
                <label id="lbSmsShortName" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>調整後簡訊簡稱
                </label>
                <asp:TextBox ID="tbSmsShortName" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <asp:Button ID="btSubmit" runat="server" Text="變更簡訊名稱" OnClick="btSubmit_Click"/>
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