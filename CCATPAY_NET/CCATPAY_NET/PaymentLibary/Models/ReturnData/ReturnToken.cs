using Newtonsoft.Json;
using System;

namespace CCatPay_Net
{
    /// <summary>
    /// Token
    /// </summary>
    public class ReturnToken
    {
        /// <summary>
        /// Token代碼
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 返回訊息
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Token到期秒數
        /// </summary>
        [JsonProperty("expires_in")]
        public int? ExpiresIn { get; set; }

        /// <summary>
        /// 帳號名稱
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Token取得時間
        /// </summary>
        [JsonProperty(".issued")]
        public DateTime? Issued { get; set; }

        /// <summary>
        /// Token到期時間
        /// </summary>
        [JsonProperty(".expires")]
        public DateTime? Expires { get; set; }

        /// <summary>
        /// 錯誤狀態
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// 錯誤描述
        /// </summary>
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}