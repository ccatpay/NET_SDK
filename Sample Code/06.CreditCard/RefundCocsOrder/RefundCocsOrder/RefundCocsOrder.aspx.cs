using CCatPay_Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SampleCode
{
    public partial class RefundCocsOrder : Page
    {
        private readonly InitializeProcess _initializeProcess;
        private readonly UtilityProcess _utilityProcess;

        public RefundCocsOrder()
        {
            _initializeProcess = new InitializeProcess();
            _utilityProcess = new UtilityProcess();
        }

        /// <summary>
        /// COCS 刷卡取消交易
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
                    ddlAcquirerType.Items.AddRange(_initializeProcess.SetAcquirerTypeDropDownList("display", "name", "COCS"));
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
                using (CreditCard creditCard = new CreditCard())
                {
                    #region Default Data
                    DateTime dtNow = DateTime.UtcNow.AddHours(8);

                    string acquirerType = (!String.IsNullOrWhiteSpace(ddlAcquirerType.SelectedValue))
                                          ? ddlAcquirerType.SelectedValue.ToString()
                                          : string.Empty;

                    decimal orderAmount = Decimal.TryParse(tbOrderAmount.Text, out orderAmount)
                                          ? orderAmount : 1024;

                    decimal refundAmount = Decimal.TryParse(tbRefundAmount.Text, out refundAmount)
                                          ? refundAmount : 0;

                    DateTime dtSendTime = DateTime.TryParse(tbSendTime.Text, out dtSendTime)
                                        ? dtSendTime : dtNow;
                    #endregion

                    /* SmsShortName */
                    CocsOrderModel cancel = new CocsOrderModel()
                    {
                        /* 呼叫 API */
                        ServiceMethod = HttpMethod.HttpPOST
                        , ApiUrl = "http://test.4128888card.com.tw/app/api/Collect"     // 呼叫 API

                        /* 登入 參數 */
                        , UserName = tbLoginAccount.Text
                        , ApiPassword = tbLoginPassword.Text

                        /* Credit Card 參數 */
                        , Command = "CocsOrderRefund"                                   // 交易代碼 (COCS 刷卡取消交易 固定帶入 CocsOrderRefund)
                        , CustomerId = tbCustomerId.Text                                // 契客代碼
                        , CustomerOrderNo = tbCustomerOrderNo.Text                      // 契客訂單號碼
                        , AcquirerType = acquirerType                                   // 指定收單銀行 esun: 玉山銀行、chinatrust: 中國信託銀行
                        , OrderAmount = orderAmount                                     // 訂單/交易金額
                        , RefundAmount = refundAmount                                   // 取消交易金額
                        , SendTime = dtSendTime                                         // 傳送時間
                    };

                    if (String.IsNullOrWhiteSpace(tbLoginAccount.Text))
                    {
                        enErrors.Add("請輸入登入帳號");
                    }

                    if (String.IsNullOrWhiteSpace(tbLoginPassword.Text))
                    {
                        enErrors.Add("請輸入登入密碼");
                    }

                    if (orderAmount < refundAmount)
                    {
                        enErrors.Add("取消交易金額必須小於等於交易金額");
                    }

                    if (enErrors.Count <= 0)
                    {
                        // 帳號登入
                        ReturnToken token = GetToken(cancel.UserName, cancel.ApiPassword, ref enErrors);

                        if (String.IsNullOrWhiteSpace(token.Error))
                        {
                            cancel.AccessToken = token.AccessToken;        // Token
                            cancel.TokenType = token.TokenType;            // Token 類型

                            ReturnOrder rtn = creditCard.CocsOrderRefund(cancel);

                            if (rtn.Status.Contains("ERROR"))
                            {
                                enErrors = _utilityProcess.SplitErrorMsg(rtn.Status, rtn.Message, enErrors);
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
                    spSuccessMessage.InnerHtml = "簡訊名稱變更完成";
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
    }
}