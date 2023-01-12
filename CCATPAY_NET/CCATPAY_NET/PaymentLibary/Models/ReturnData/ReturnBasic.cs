using Newtonsoft.Json;

namespace CCatPay_Net
{
    /// <summary>
    /// ReturnOrder
    /// </summary>
    public class ReturnBasic
    {
        /// <summary>
        /// 訊息
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 回應狀態，僅有 OK 與 ERROR 兩種訊息狀態，OK 為處理成功， ERROR 為處理時發生異常狀況
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 契客訂單號碼
        /// </summary>
        [JsonProperty("cust_order_no")]
        public string CustomerOrderNo { get; set; }
    }
}