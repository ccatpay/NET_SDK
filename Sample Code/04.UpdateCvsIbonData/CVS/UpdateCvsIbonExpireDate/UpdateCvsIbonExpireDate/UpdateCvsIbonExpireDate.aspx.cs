using CCatPay_Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SampleCode
{
    public partial class UpdateCvsIbonExpireDate : Page
    { 
        private readonly UtilityProcess _utilityProcess;
        private readonly SecurityProcess _securityProcess;

        public UpdateCvsIbonExpireDate()
        {
            _utilityProcess = new UtilityProcess();
            _securityProcess = new SecurityProcess();
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
                using (UpdateCvsIbon CvsIbon = new UpdateCvsIbon())
                {
                    #region Default Data
                    DateTime dtNow = DateTime.UtcNow.AddHours(8);
                    DateTime dtExpire = DateTime.TryParse(tbExpireDate.Text, out dtExpire)
                                      ? dtExpire : dtNow.AddDays(3);

                    decimal orderAmount = Decimal.TryParse(tbOrderAmount.Text, out orderAmount)
                                          ? orderAmount : 0;

                    // 隨機數(10碼)
                    string nonce = dtNow.Hour.ToString() + dtNow.Minute.ToString() + dtNow.Second.ToString()
                                  + _securityProcess.CreateRandomNumber(4);

                    string md5Str = string.Format("{0}:{1}:{2}", tbCustomerOrderNo.Text, string.Format("{0:F1}", orderAmount) , nonce);
                    #endregion

                    /* Ibon */
                    UpdateIbonModel ibon = new UpdateIbonModel()
                    {
                        /* 呼叫 API */
                        ServiceMethod = HttpMethod.HttpPOST
                        , ApiUrl = "http://test.4128888card.com.tw/app/api/Collect"             // 呼叫 API

                        /* 登入 參數 */
                        , UserName = tbLoginAccount.Text
                        , ApiPassword = tbLoginPassword.Text

                        /* Ibon 參數 */
                        , Command = "CvsIbonUpdateDate"                                         // 交易代碼 (CVS Ibon變更金額 固定帶入 CvsIbonUpdateDate)
                        , CustomerId = tbCustomerId.Text                                        // 契客代碼
                        , CustomerOrderNo = tbCustomerOrderNo.Text                              // 契客訂單編號 (必須是已建立的繳款單)
                        , OrderAmount = orderAmount                                             // 代繳金額
                        , ExpireDate = dtExpire                                                 // 繳費到期日
                        , IbonShopId = tbIbonShopId.Text                                        // 廠商代碼 (CCAT、BCAT)
                        , IbonCode = tbIbonCode.Text                                            // ibon 代碼
                        , Nonce = nonce                                                         // 隨機數(10碼)
                        , CheckSum = _securityProcess.GetMd5Hash(md5Str , false)                // 檢核驗證碼比對
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
                        ReturnToken token = GetToken(ibon.UserName, ibon.ApiPassword, ref enErrors);

                        if (String.IsNullOrWhiteSpace(token.Error))
                        {
                            // 成功登入帳號後，須回傳 Token 才可新增訂單
                            ibon.AccessToken = token.AccessToken;        // Token
                            ibon.TokenType = token.TokenType;            // Token 類型

                            ReturnCvsOrder returnOrder = CvsIbon.UpdateExpireDate(ibon);

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
                    spSuccessMessage.InnerHtml = "變更日期成功";
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