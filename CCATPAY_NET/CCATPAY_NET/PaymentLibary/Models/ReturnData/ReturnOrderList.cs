using Newtonsoft.Json;
using System.Collections.Generic;

namespace CCatPay_Net
{
    /// <summary>
    /// ReturnCvsOrderList
    /// </summary>
    public class ReturnCvsOrderList : ReturnBasic
    {
        /// <summary>
        /// 多筆訂單
        /// </summary>
        [JsonProperty("order_list")]
        public List<ReturnCvsOrder> OrderList { get; set; }
    }

    /// <summary>
    /// ReturnCocsOrderList
    /// </summary>
    public class ReturnCocsOrderList : ReturnBasic
    {
        /// <summary>
        /// 多筆訂單
        /// </summary>
        [JsonProperty("order_list")]
        public List<ReturnCocsOrder> OrderList { get; set; }
    }

    /// <summary>
    /// ReturnDphOrderList
    /// </summary>
    public class ReturnDphOrderList : ReturnBasic
    {
        /// <summary>
        /// 多筆訂單
        /// </summary>
        [JsonProperty("order_list")]
        public List<ReturnDphOrder> OrderList { get; set; }
    }
}