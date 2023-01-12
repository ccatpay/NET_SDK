namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public partial class OrderModel : TokenModel
    {
        /// <summary>
        /// 交易代號
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 契客代號
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 契客訂單號碼（契約客戶自定之識別用號碼，該號碼在同一契約客戶代號下必須為唯一）
        /// </summary>
        public string CustomerOrderNo { get; set; }

        /// <summary>
        /// 代繳金額
        /// CVS：金額上限(含外加手續費) Ibon: 20,000、ATM: 30,000、三段式條碼 : 20,000
        /// COCS：訂單/交易金額上限以合約規範為主 (預設為 100,000)
        /// DPH：訂單/交易金額上限以合約規範為主 (預設為 100,000)
        /// </summary>
        public decimal? OrderAmount { get; set; }

        /// <summary>
        /// 取消交易（退貨、退款）金額（必須輸入，小於或等於訂單 /交易金額）
        /// </summary>
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// 指定請款金額
        /// </summary>
        public decimal? CrAmount { get; set; }

        /// <summary>
        /// APN指定傳送網址 (若沒有填寫則 使用 金流服務 中 的 APN網址 作為預設值)
        /// </summary>
        public string ApnUrl { get; set; }

        /// <summary>
        /// 繳款單明細
        /// </summary>
        public string OrderDetail { get; set; }
    }
}
