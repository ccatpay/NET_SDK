using System;

namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public partial class CvsOrderModel : OrderModel
    {
        /// <summary>
        /// 繳費到期日
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 繳款人姓名 (b2c為 1 則 必填)
        /// </summary>
        public string PayerName { get; set; }

        /// <summary>
        /// 繳款人郵遞區號
        /// </summary>
        public string PayerPostCode { get; set; }

        /// <summary>
        /// 繳款人地址
        /// </summary>
        public string PayerAddress { get; set; }

        /// <summary>
        /// 繳款人手機 (b2c為 1 則 必填)
        /// </summary>
        public string PayerMobile { get; set; }

        /// <summary>
        /// 繳款人電子郵件 (b2c為 1 則 必填)
        /// </summary>
        public string PayerEmail { get; set; }

        /// <summary>
        /// 繳款方式 
        /// 0:ibon繳款
        /// 1:ATM銀行轉帳
        /// 2:三段式條碼
        /// 9:三段式條碼(中信即時繳款通知，僅支援7-11繳費)
        /// </summary>
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// 收單行 0:玉山(預設) 1:中信
        /// </summary>
        public string PaymentAcquirerType { get; set; }
    }
}
