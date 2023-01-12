<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CocsOrderQueryByOrderNo.aspx.cs" Inherits="SampleCode.CocsOrderQueryByOrderNo" %>

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
            <p id="OrderNoField" style="text-align:center;">
                <label id="lbCustomerOrderNo" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>契客訂單號碼
                </label>
                <asp:TextBox ID="tbCustomerOrderNo" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
             <p id="DateField_1" style="text-align:center;display:none;">
                <label id="lbOrderStartDate" style="margin:0px 5px;font-weight:600;">
                    <span style="color:red; font-weight:800;">*</span>訂單日期起日
                </label>
                <asp:TextBox ID="tbOrderStartDate" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p id="DateField_2" style="text-align:center;display:none;">
                <label id="lbOrderEndDate" style="margin: 0px 5px; font-weight: 600;">
                    <span style="color:red; font-weight:800;">*</span>訂單日期迄日
                </label>
                <asp:TextBox ID="tbOrderEndDate" runat="server" style="margin:0px 5px;"></asp:TextBox>
            </p>
            <p style="text-align:center;">
                <asp:Button ID="btSubmit" runat="server" Text="查詢訂單" OnClick="btSubmit_Click"/>
            </p>
        </div>
         <p style="text-align:center;">
            <span id="spSuccessMessage" runat="server" style="color:forestgreen; font-weight:800;"></span>
            <span id="spErrorMessage" runat="server" style="color:red; font-weight:800;"></span>
        </p>
        <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="false" PageSize="10"
             AllowPaging="true" OnPageIndexChanging="gvOrder_PageIndexChanging" style="text-align:center;">
            <Columns>
                <asp:BoundField HeaderText="契客訂單號碼" DataField="CustomerOrderNo" />
                <asp:BoundField HeaderText="代繳金額" DataField="OrderAmount" />
                <asp:BoundField HeaderText="繳費到期日" DataField="ExpireDate" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField HeaderText="ibon 碼" DataField="IbonCode" />
                <asp:BoundField HeaderText="安源 ibon 廠商代碼" DataField="IbonShopId" />
                <asp:BoundField HeaderText="代繳帳號(ATM轉帳帳號)" DataField="VirtualAccount" />
                <asp:BoundField HeaderText="超商條碼 1" DataField="STBarcode1" />
                <asp:BoundField HeaderText="超商條碼 2" DataField="STBarcode2" />
                <asp:BoundField HeaderText="超商條碼 3" DataField="STBarcode3" />
                <asp:BoundField HeaderText="帳單金額" DataField="BillAmount" />
                <asp:BoundField HeaderText="超商手續費" DataField="CSFee" />
                <asp:BoundField HeaderText="代繳資訊建立時間" DataField="CreateTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"/>
                <asp:BoundField HeaderText="繳款日期" DataField="PayDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"/>
                <asp:BoundField HeaderText="（預計）撥款金額" DataField="GrantAmount" />
                <asp:BoundField HeaderText="撥款日期" DataField="GrantDate" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField HeaderText="收單行" DataField="CocsAcquirerType" />
                <asp:BoundField HeaderText="短網址" DataField="ShortUrl" />
                <asp:BoundField HeaderText="ibon繳款門市店號" DataField="StoreId" />
                <asp:TemplateField HeaderText="是否列印紙本發票" >
                    <ItemTemplate><%# Eval("PrintInvoice").Equals("1") ? "是" : "否" %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="載具類別">
                    <ItemTemplate>
                        <div>
                            <%# (!String.IsNullOrWhiteSpace(Eval("VehicleType").ToString())) 
                                ? (Enum.GetName(typeof(CCatPay_Net.VehicleType),Convert.ToInt32(Eval("VehicleType")))).Equals("Member") 
                                  ? "會員載具"
                                  : (Enum.GetName(typeof(CCatPay_Net.VehicleType),Convert.ToInt32(Eval("VehicleType")))).Equals("Mobile") 
                                    ? "手機條碼" 
                                    : (Enum.GetName(typeof(CCatPay_Net.VehicleType),Convert.ToInt32(Eval("VehicleType")))).Equals("Natural") 
                                      ? "自然人憑證" : string.Empty
                                : string.Empty %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="載具條碼" DataField="VehicleBarcode" />
                <asp:TemplateField HeaderText="是否捐贈發票" >
                    <ItemTemplate><%# Eval("DonateInvoice").Equals("1") ? "是" : "否" %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="愛心碼" DataField="LoveCode" />
                <asp:BoundField HeaderText="發票號碼" DataField="InvoiceNo" />
                <asp:BoundField HeaderText="發票日期" DataField="InvoiceDate" />
            </Columns>
        </asp:GridView>
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