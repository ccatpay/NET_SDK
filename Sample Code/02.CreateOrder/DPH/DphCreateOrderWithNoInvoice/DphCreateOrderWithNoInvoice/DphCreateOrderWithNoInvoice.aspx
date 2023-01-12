<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DphCreateOrderWithNoInvoice.aspx.cs" Inherits="SampleCode.DphCreateOrderWithNoInvoice" %>

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
        <div style="background-color: rgba(128,255,255,0.6); border-radius:5px; text-align:center; padding: 5px; margin:5px; float: left;">
            <p style="text-align:center;">
                <label id="lbCustomerId" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>契客代碼
                </label>
                <asp:TextBox ID="tbCustomerId" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbCustomerOrderNo" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>契客訂單號碼
                </label>
                <asp:TextBox ID="tbCustomerOrderNo" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbOrderAmount" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>代繳金額
                </label>
                <asp:TextBox ID="tbOrderAmount" runat="server" style="margin:0px 5px;" type="number" max="100000" min="1"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbOrderDetail" style="margin:0px 5px;font-weight:600;">繳款單明細</label>
                <asp:TextBox ID="tbOrderDetail" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbPayerName" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>繳款人姓名
                </label>
                <asp:TextBox ID="tbPayerName" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbAcquirerType" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>收單行
                </label>
                <asp:DropDownList ID="ddlAcquirerType" runat="server">
                    <Items>
                       <asp:ListItem Text="請選擇收單行" Value="" />
                   </Items>
                </asp:DropDownList>
            </p>
            <p style="text-align:center;">
                <label id="lbSendTime" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>傳送時間
                </label>
                <asp:TextBox ID="tbSendTime" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbSuccessUrl" style="margin:0px 5px;font-weight:600;">訂單授權成功指定回傳 URL</label>
                <asp:TextBox ID="tbSuccessUrl" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbApnUrl" style="margin:0px 5px;font-weight:600;">APN指定傳送網址</label>
                <asp:TextBox ID="tbApnUrl" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center; display:none;">
                <asp:CheckBox ID="chkB2C" runat="server" OnCheckedChanged="chkB2C_CheckedChanged" AutoPostBack="true"/>
                <label id="lbB2C" style="margin:0px 5px;font-weight:600;">電子發票</label>
            </p>
            <p style="text-align:center;">
                <asp:Button ID="btSubmit" runat="server" Text="建立新訂單" OnClick="btSubmit_Click"/>
            </p>
        </div>

    <div id="InvoiceField" runat="server"
          style="background-color: rgba(128,255,255,0.6); border-radius:5px; text-align:center; padding: 5px; margin:5px; float: left; display:none;">
            <p style="text-align:center;">
                <label id="lbInvoice" style="margin:0px 5px;font-weight:800;font-size:larger;">電子發票</label>
            </p>
            <p style="text-align:center;">
                <label id="lbProductName" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>商品名稱
                </label>
                <asp:TextBox ID="tbProductName" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <asp:CheckBox ID="chkPrintInvoice" runat="server" />
                <label id="lbPrintInvoice" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>是否列印紙本發票
                </label>
            </p>
            <p style="text-align:center;">
                <asp:CheckBox ID="chkDonateInvoice" runat="server" />
                <label id="lbDonateInvoice" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>是否捐贈發票
                </label>
            </p>
            <p style="text-align:center;">
                <label id="lbPayerPostCode" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>繳款人郵遞區號
                </label>
                <asp:TextBox ID="tbPayerPostCode" runat="server" style="margin:0px 5px;" MaxLength="5"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbPayerAddress" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>繳款人地址
                </label>
                <asp:TextBox ID="tbPayerAddress" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbPayerMobile" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>繳款人手機
                </label>
                <asp:TextBox ID="tbPayerMobile" runat="server" style="margin:0px 5px;" MaxLength="10"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbPayerEmail" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>繳款人 Email
                </label>
                <asp:TextBox ID="tbPayerEmail" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>

            <p style="text-align:center;">
                <label id="lbVehicleType" style="margin:0px 5px;font-weight:600;">載具類別</label>
                <asp:DropDownList ID="ddlVehicleType" runat="server">
                    <Items>
                       <asp:ListItem Text="請選擇載具類別" Value="" />
                   </Items>
                </asp:DropDownList>
            </p>
            <p style="text-align:center;">
                <label id="lbVehicleBarcode" style="margin:0px 5px;font-weight:600;">載具條碼</label>
                <asp:TextBox ID="tbVehicleBarcode" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbLoveCode" style="margin:0px 5px;font-weight:600;">愛心碼</label>
                <asp:TextBox ID="tbLoveCode" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbBuyerBillNo" style="margin:0px 5px;font-weight:600;">買方統一編號</label>
                <asp:TextBox ID="tbBuyerBillNo" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <label id="lbBuyerInvoiceTitle" style="margin:0px 5px;font-weight:600;">發票抬頭</label>
                <asp:TextBox ID="tbBuyerInvoiceTitle" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <asp:Button ID="btSubmit2" runat="server" Text="建立新訂單" OnClick="btSubmit_Click"/>
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