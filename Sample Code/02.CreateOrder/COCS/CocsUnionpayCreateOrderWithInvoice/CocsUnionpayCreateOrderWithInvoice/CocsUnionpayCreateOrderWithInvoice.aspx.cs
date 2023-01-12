using CCatPay_Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SampleCode
{
    public partial class CocsUnionpayCreateOrderWithInvoice : Page
    {
        private readonly InitializeProcess _initializeProcess;
        private readonly UtilityProcess _utilityProcess;

        public CocsUnionpayCreateOrderWithInvoice()
        {
            _initializeProcess = new InitializeProcess();
            _utilityProcess = new UtilityProcess();
        }

        /// <summary>
        /// COCS代收代付 (有電子發票)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    /* 調整下拉式選單的 Text、Value 值為何：
                       value: HashCode、name: string、display: DisplayName */
                    ddlVehicleType.Items.AddRange(_initializeProcess.SetVehicleTypeDropDownList("display", "value"));
                }
            }
            catch (Exception ex)
            {
                spErrorMessage.InnerHtml = ex.Message;
                spSuccessMessage.InnerHtml = string.Empty;
            }
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            List<string> enErrors = new List<string>();

            try
            {
                using (AllCreateOrder WithInvoice = new AllCreateOrder())
                {
                    #region SetData
                    DateTime dtNow = DateTime.UtcNow.AddHours(8);

                    string orderNo = (!String.IsNullOrWhiteSpace(tbCustomerOrderNo.Text))
                                     ? tbCustomerOrderNo.Text
                                     : "COCSTEST" + dtNow.ToString("yyyyMMddHHmmssfff");

                    decimal orderAmount = Decimal.TryParse(tbOrderAmount.Text, out orderAmount)
                                          ? orderAmount : 1024;

                    DateTime dtSendTime = DateTime.TryParse(tbSendTime.Text, out dtSendTime)
                                        ? dtSendTime : dtNow;

                    string payerPostCode = tbPayerPostCode.Text;

                    string payerAddress = tbPayerAddress.Text;

                    string vehicleType = (!String.IsNullOrWhiteSpace(ddlVehicleType.SelectedValue))
                                        ? ddlVehicleType.SelectedValue.ToString()
                                        : string.Empty;
                    #endregion

                    /* COCS Order */
                    CocsOrderModel order = new CocsOrderModel()
                    {
                        /* 呼叫 API，必填 */
                          ServiceMethod = HttpMethod.HttpPOST
                        , ApiUrl = "http://test.4128888card.com.tw/app/api/Collect" // 呼叫 API

                        /* 登入 參數，必填 */
                        , UserName = tbLoginAccount.Text                            // 登入帳號
                        , ApiPassword = tbLoginPassword.Text                        // 登入密碼

                        /* 訂單參數，必填 */
                        , Command = "CocsUnionpayAppend"                            // 交易代碼 (COCS 新增刷卡訂單-玉山銀聯卡 固定帶入 CocsUnionpayAppend)
                        , CustomerId = tbCustomerId.Text                            // 契客代碼
                        , CustomerOrderNo = orderNo                                 // 契客訂單號碼
                        , OrderAmount = orderAmount                                 // 代繳金額
                        , OrderDetail = tbOrderDetail.Text                          // 繳款單明細
                        , SendTime = dtSendTime                                     // 傳送時間

                        /* 非必填 */
                        , SuccessUrl = tbSuccessUrl.Text                            // 訂單授權成功指定回傳 URL
                        , ApnUrl = tbApnUrl.Text                                    // APN指定傳送網址
                    };

                    CocsInvoiceModel invoice = new CocsInvoiceModel() 
                    {
                          /* 發票參數，必填 */
                          B2C = (chkB2C.Checked) ? "1" : "0"                        // 此訂單是否開立電子發票 0:不開立 (預設) 1:開立
                        , ProductName = tbProductName.Text                          // 商品名稱 
                        , PrintInvoice = (chkPrintInvoice.Checked) ? "1" : "0"      // 是否列印紙本發票 0:不列印 1:列印
                        , DonateInvoice = (chkDonateInvoice.Checked) ? "1" : "0"    // 是否捐贈發票 0:不捐贈 1:捐贈
                        , PayerName = tbPayerName.Text                              // 繳款人姓名
                        , PayerPostCode = payerPostCode                             // 繳款人郵遞區號
                        , PayerAddress = payerAddress                               // 繳款人地址
                        , PayerMobile = tbPayerMobile.Text                          // 繳款人手機
                        , PayerEmail = tbPayerEmail.Text                            // 繳款人 Email

                        /* 非必填 */
                        , VehicleType = vehicleType                                 // 載具類別 1:會員載具 2:手機條碼 3:自然人憑證
                        , VehicleBarcode = tbVehicleBarcode.Text                    // 載具條碼
                        , LoveCode = tbLoveCode.Text                                // 愛心碼 (選捐贈。愛心碼 如不輸入，預設為創世基金會919)
                        , BuyerBillNo = tbBuyerBillNo.Text                          // 買方統一編號
                        , BuyerInvoiceTitle = tbBuyerInvoiceTitle.Text              // 發票抬頭
                    };

                    if (String.IsNullOrWhiteSpace(tbLoginAccount.Text))
                    {
                        enErrors.Add("請輸入登入帳號");
                    }

                    if (String.IsNullOrWhiteSpace(tbLoginPassword.Text))
                    {
                        enErrors.Add("請輸入登入密碼");
                    }

                    if (enErrors.Count <= 0)
                    {
                        // 帳號登入
                        ReturnToken token = GetToken(order.UserName, order.ApiPassword, ref enErrors);

                        if (String.IsNullOrWhiteSpace(token.Error))
                        {
                            order.AccessToken = token.AccessToken;        // Token
                            order.TokenType = token.TokenType;            // Token 類型

                            ReturnCocsOrder returnOrder = WithInvoice.CreateOrder<ReturnCocsOrder, CocsOrderModel, CocsInvoiceModel>(order, invoice);

                            if (returnOrder.Status.Contains("ERROR"))
                            {
                                enErrors = _utilityProcess.SplitErrorMsg(returnOrder.Status, returnOrder.Message, enErrors);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {
                if (enErrors.Count() > 0)
                {
                    string szErrorMessage = String.Join("<br/>", enErrors);
                    spErrorMessage.InnerHtml = szErrorMessage;
                    spSuccessMessage.InnerHtml = string.Empty;
                }
                else
                {
                    spErrorMessage.InnerHtml = string.Empty;
                    spSuccessMessage.InnerHtml = "訂單建立成功";
                }
            }
        }

        private ReturnToken GetToken(string userName, string password, ref List<string> enErrors)
        {
            ReturnToken token = new ReturnToken();

            try
            {
                using (Token loginToken = new Token())
                {
                    /* Token 參數 */
                    TokenModel model = new TokenModel()
                    {
                        ServiceMethod = HttpMethod.HttpPOST
                        , UserName = userName
                        , ApiPassword = password
                        , ApiUrl = "http://test.4128888card.com.tw/app/token"
                    };

                    /* 取得 Token */
                    token = loginToken.GetToken(model);

                    enErrors = _utilityProcess.SplitErrorMsg(token.Error, token.ErrorDescription, enErrors);
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }

            return token;
        }

        protected void chkB2C_CheckedChanged(object sender, EventArgs e)
        {
            if (chkB2C.Checked)
            {
                btSubmit.Attributes.Add("style", "display:none");
                InvoiceField.Attributes.Add("style", "background-color: rgba(128,255,255,0.6); border-radius:5px; text-align:center; padding: 5px; margin:5px; float: left;");
            }
            else
            {
                btSubmit.Attributes.Remove("style");
                InvoiceField.Attributes.Remove("style");
                InvoiceField.Attributes.Add("style", "display:none");
            }
        }
    }
}