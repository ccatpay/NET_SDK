using System;

namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public partial class CocsOrderModel : OrderModel
    {
        /// <summary>
        /// 指定收單銀行 esun: 玉山銀行、chinatrust: 中國信託銀行
        /// </summary>
        public string AcquirerType { get; set; }

        /// <summary>
        /// 限定產品別（可不輸入，當此欄位為空時，意為允許所有產品別。
        /// 各產品別以” ”|”分隔。例如 esun.normal|esun.m3
        /// </summary>
        public string LimitProductId { get; set; }

        /// <summary>
        /// 傳送時間，必須為傳送時之最新時間，格式為 yyyy-MM-dd HH:mm:ss
        /// 例如 2017-07-18 07:17:25
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 訂單授權成功指定回傳URL
        /// 若此參數有傳入 URL 授權成功回覆，URL會帶入此網址
        /// 若沒有輸入會依客樂得金流服務設定中 授權 成功所指定網址 回傳
        /// </summary>
        public string SuccessUrl { get; set; }
    }
}
