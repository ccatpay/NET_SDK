using CCatPay_Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SampleCode
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            List<string> enErrors = new List<string>();

            try
            {
                using (Token getToken = new Token())
                {
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
                        /* Token 參數 */
                        TokenModel model = new TokenModel()
                        {
                            ServiceMethod = HttpMethod.HttpPOST
                            , UserName = tbLoginAccount.Text
                            , ApiPassword = tbLoginPassword.Text
                            , ApiUrl = "http://test.4128888card.com.tw/app/token"
                        };

                        /* 取得 Token */
                        ReturnToken token = getToken.GetToken(model);

                        if (!String.IsNullOrWhiteSpace(token.Error))
                        {
                            if (token.ErrorDescription.Contains(","))
                            {
                                var errAry = token.ErrorDescription.Split(',');

                                foreach (var item in errAry)
                                {
                                    enErrors.Add(item);
                                }
                            }
                            else
                            {
                                enErrors.Add(token.ErrorDescription);
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
                    spSuccessMessage.InnerHtml = "登入成功";
                }
            }
        }
    }
}