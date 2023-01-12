namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public class DphInvoiceModel : InvoiceModel
    {
        public DphInvoiceModel()
        {
            B2C = "0";
        }

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
    }
}
