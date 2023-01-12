using System;

namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public partial class DphOrderModel : OrderModel
    {
        /// <summary>
        /// 繳款人姓名 (b2c為 1 則 必填)
        /// </summary>
        public string PayerName { get; set; }

        /// <summary>
        /// 指定收單銀行 opw: OPEN錢包、icp: icash Pay
        /// </summary>
        public string AcquirerType { get; set; }

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
