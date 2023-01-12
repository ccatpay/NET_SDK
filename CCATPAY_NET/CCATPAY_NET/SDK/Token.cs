using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace CCatPay_Net
{
    public class Token : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public Token()
        {
            _utilityProcess = new UtilityProcess();
        }

        public ReturnToken GetToken(TokenModel getToken)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(getToken));

            ReturnToken token = new ReturnToken();

            if (errList.Count == 0)
            {
                try
                {

                    StringBuilder str = new StringBuilder();
                    str = str.Append("grant_type=password");
                    str = str.Append("&username={0}");
                    str = str.Append("&password={1}");

                    string stringContent = string.Format(str.ToString()
                                                       , HttpUtility.UrlEncode(getToken.UserName)
                                                       , HttpUtility.UrlEncode(getToken.ApiPassword)
                                                       , Encoding.UTF8);

                    HttpClientModel httpClient = new HttpClientModel()
                    {
                        StringContent = stringContent
                       , Encoding = Encoding.UTF8
                       , MediaType = "application/x-www-form-urlencoded"
                       , RequestUrl = "Token"
                    };

                    string resultJSON = _utilityProcess.GetResponse(getToken, httpClient);

                    token = JsonConvert.DeserializeObject<ReturnToken>(resultJSON);

                    //TokenParseDateTime(ref token);
                }
                catch (Exception ex)
                {
                    errList.Add(ex.Message);
                }
            }

            if (errList.Count > 0)
            {
                token.Error = "error";
                token.ErrorDescription = String.Join(", ", errList);
            }

            return token;
        }

        #region 共用方法
        ///// <summary>
        ///// 特定欄位調整時區
        ///// </summary>
        ///// <param name="token"></param>
        //private void TokenParseDateTime(ref ReturnToken token)
        //{
        //    token.Expires = _utilityProcess.ChangeUtcTimeZone(token.Expires);
        //    token.Issued = _utilityProcess.ChangeUtcTimeZone(token.Issued);
        //}

        /// <summary>
        /// 執行與釋放 (Free)、釋放 (Release) 或重設 Unmanaged 資源相關聯之應用程式定義的工作。
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}