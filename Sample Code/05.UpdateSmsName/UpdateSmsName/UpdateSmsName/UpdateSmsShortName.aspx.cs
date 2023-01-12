using CCatPay_Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SampleCode
{
    public partial class UpdateSmsShortName : Page
    {
        private readonly InitializeProcess _initializeProcess;
        private readonly UtilityProcess _utilityProcess;

        public UpdateSmsShortName()
        {
            _initializeProcess = new InitializeProcess();
            _utilityProcess = new UtilityProcess();
        }

        /// <summary>
        /// CVS COCS 變更簡訊名稱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    // 只顯示 CVS COCS 兩種金流服務類型
                    ServiceType[] serviceTypes = new ServiceType[] 
                    { ServiceType.CVS
                    , ServiceType.COCS };

                    ddlServiceType.Items.AddRange(_initializeProcess.SetCustomServiceTypeDropDownList("display", "value" , serviceTypes));
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
                using (UpdateSmsName SmsName = new UpdateSmsName())
                {
                    #region SetData
                    string serviceType = (!String.IsNullOrWhiteSpace(ddlServiceType.SelectedValue))
                                        ? ddlServiceType.SelectedValue.ToString()
                                        : string.Empty;
                    #endregion

                    /* SmsShortName */
                    UpdateSmsModel update = new UpdateSmsModel()
                    {
                        /* 呼叫 API */
                        ServiceMethod = HttpMethod.HttpPOST
                        , ApiUrl = "http://test.4128888card.com.tw/app/api/Collect"     // 呼叫 API

                        /* 登入 參數 */
                        , UserName = tbLoginAccount.Text
                        , ApiPassword = tbLoginPassword.Text

                        /* Ibon 參數 */
                        , Command = "SmsShortNameUpdate"                                // 交易代碼 (CVS COCS 變更簡訊名稱 固定帶入 SmsShortNameUpdate)
                        , CustomerId = tbCustomerId.Text                                // 契客代碼
                        , ServiceType = serviceType                                     // 金流服務類型。0：CVS 代收代付、1：COCS 線上刷卡
                        , SmsShortName = tbSmsShortName.Text                            // 調整後簡訊簡稱
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
                        ReturnToken token = GetToken(update.UserName, update.ApiPassword, ref enErrors);

                        if (String.IsNullOrWhiteSpace(token.Error))
                        {
                            // 成功登入帳號後，須回傳 Token 才可新增訂單
                            update.AccessToken = token.AccessToken;        // Token
                            update.TokenType = token.TokenType;            // Token 類型

                            ReturnBasic rtn = SmsName.UpdateSmsShortName(update);

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