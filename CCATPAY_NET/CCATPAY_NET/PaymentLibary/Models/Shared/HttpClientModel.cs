using System.Text;

namespace CCatPay_Net
{
    /// <summary>
    /// 網路連線
    /// </summary>
    public class HttpClientModel
    {
        /// <summary>
        /// 字串內文
        /// </summary>
        public string StringContent { get; set; }

        /// <summary>
        /// 文字編碼
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 傳輸型別
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// 呼叫方法
        /// </summary>
        public string RequestUrl { get; set; }
    }
}
