using CCatPay_Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SampleCode
{
    public partial class CvsCreateOrderWithNoInvoice : Page
    { 
        private readonly InitializeProcess _initializeProcess;
        private readonly UtilityProcess _utilityProcess;

        public CvsCreateOrderWithNoInvoice()
        {
            _initializeProcess = new InitializeProcess();
            _utilityProcess = new UtilityProcess();
        }

        /// <summary>
        /// CVS代收代付 (無電子發票)
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
                    ddlPaymentType.Items.AddRange(_initializeProcess.SetPaymentTypeDropDownList("display", "value" , "CVS"));
                    ddlPaymentAcquirerType.Items.AddRange(_initializeProcess.SetAcquirerTypeDropDownList("display", "value", "CVS"));
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
                using (AllCreateOrder NoInvoice = new AllCreateOrder())
                {
                    #region SetData
                    DateTime dtNow = DateTime.UtcNow.AddHours(8);

                    string orderNo = (!String.IsNullOrWhiteSpace(tbCustomerOrderNo.Text))
                                     ? tbCustomerOrderNo.Text
                                     : "CVSTEST" + dtNow.ToString("yyyyMMddHHmmssfff");

                    decimal orderAmount = Decimal.TryParse(tbOrderAmount.Text, out orderAmount)
                                          ? orderAmount : 1024;

                    DateTime dtExpire = DateTime.TryParse(tbExpireDate.Text, out dtExpire)
                                      ? dtExpire : dtNow.AddDays(3);

                    string payerPostCode = tbPayerPostCode.Text;

                    string payerAddress = tbPayerAddress.Text;

                    PaymentType? paymentType = null;
                    paymentType = (!String.IsNullOrWhiteSpace(ddlPaymentType.SelectedValue))
                                  ? (PaymentType)Enum.Parse(typeof(PaymentType), ddlPaymentType.SelectedValue.ToString())
                                  : paymentType;


                    string paymentAcquirerType = (!String.IsNullOrWhiteSpace(ddlPaymentAcquirerType.SelectedValue))
                                                 ? ddlPaymentAcquirerType.SelectedValue.ToString()
                                                 : string.Empty;
                    #endregion

                    /* CVS Order */
                    CvsOrderModel order = new CvsOrderModel()
                    {
                        /* 呼叫 API */
                        ServiceMethod = HttpMethod.HttpPOST
                        , ApiUrl = "http://test.4128888card.com.tw/app/api/Collect"            // 呼叫 API

                        /* 登入 參數 */
                        , UserName = tbLoginAccount.Text
                        , ApiPassword = tbLoginPassword.Text

                        /* Order 參數 */
                        , Command = "CvsOrderAppend"                                           // 交易代碼 (CVS代收代付 固定帶入 CvsOrderAppend)
                        , CustomerId = tbCustomerId.Text                                       // 契客代碼
                        , CustomerOrderNo = orderNo                                            // 契客訂單號碼
                        , OrderAmount = orderAmount                                            // 代繳金額
                        , ExpireDate = dtExpire                                                // 繳費到期日
                        , PayerName = tbPayerName.Text                                         // 繳款人姓名
                        , PayerPostCode = payerPostCode                                        // 繳款人郵遞區號
                        , PayerAddress = payerAddress                                          // 繳款人地址
                        , PayerMobile = tbPayerMobile.Text                                     // 繳款人手機
                        , PayerEmail = tbPayerEmail.Text                                       // 繳款人 Email
                        , PaymentType = paymentType                                            // 繳款方式 
                        , PaymentAcquirerType = paymentAcquirerType                            // 收單行
                        , ApnUrl = tbApnUrl.Text                                               // APN指定傳送網址
                        , OrderDetail = tbOrderDetail.Text                                     // 繳款單明細
                    };


                    /* 電子發票，沒有發送電子發票可不用填下面參數 */
                    CvsInvoiceModel invoice = new CvsInvoiceModel();

                    //CvsInvoiceModel invoice = new CvsInvoiceModel()
                    //{
                    //    /* 必填 */
                    //      B2C = (chkB2C.Checked) ? "1" : "0"                                 // 此訂單是否開立電子發票 0:不開立 (預設) 1:開立
                    //    , ProductName = tbProductName.Text                                   // 商品名稱 (b2c為 1 則必填)
                    //    , PrintInvoice = (chkPrintInvoice.Checked) ? "1" : "0"               // 是否列印紙本發票 (b2c為 1 則必填) 0:不列印 1:列印
                    //    , DonateInvoice = (chkDonateInvoice.Checked) ? "1" : "0"             // 是否捐贈發票 (b2c為 1 則必填) 0:不捐贈 1:捐贈

                    //    /* 非必填 */
                    //    , VehicleType = ddlVehicleType.SelectedValue.ToString()              // 載具類別 1:會員載具 2:手機條碼 3:自然人憑證
                    //    , VehicleBarcode = tbVehicleBarcode.Text                             // 載具條碼
                    //    , LoveCode = tbLoveCode.Text                                         // 愛心碼 (選捐贈。愛心碼 如不輸入，預設為創世基金會919)
                    //    , BuyerBillNo = tbBuyerBillNo.Text                                   // 買方統一編號
                    //    , BuyerInvoiceTitle = tbBuyerInvoiceTitle.Text                       // 發票抬頭
                    //    , BuyerInvoiceZip = tbBuyerInvoiceZip.Text                           // 買方統編發票寄送郵遞區號
                    //    , BuyerInvoiceAddr = tbBuyerInvoiceAddr.Text                         // 買方統編發票寄送地址
                    //};

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
                            // 成功登入帳號後，須回傳 Token 才可新增訂單
                            order.AccessToken = token.AccessToken;        // Token
                            order.TokenType = token.TokenType;            // Token 類型

                            ReturnCvsOrder returnOrder = NoInvoice.CreateOrder<ReturnCvsOrder, CvsOrderModel, CvsInvoiceModel>(order, invoice);

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