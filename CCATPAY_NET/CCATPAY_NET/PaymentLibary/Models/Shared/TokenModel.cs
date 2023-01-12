using System.ComponentModel.DataAnnotations;

namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public partial class TokenModel
    {
        /// <summary>
        /// 介接服務的方法(預設: POST, 除 QueryTradeInfo 方法提供 SOAP 呼叫 WebService 外，其餘皆使用 POST)。
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public HttpMethod ServiceMethod { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登入密碼
        /// </summary>
        public string ApiPassword { get; set; }

        /// <summary>
        /// Api 網址
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Token 類型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 全功能介接參數的建構式。
        /// </summary>
        public TokenModel()
        {
            this.ServiceMethod = HttpMethod.HttpPOST;
        }
    }
}
