namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public class CvsInvoiceModel : InvoiceModel
    {
        public CvsInvoiceModel()
        {
            B2C = "0";
        }

        /// <summary>
        /// 買方統編發票寄送郵遞區號
        /// </summary>
        public string BuyerInvoiceZip { get; set; }

        /// <summary>
        /// 買方統編發票寄送地址
        /// </summary>
        public string BuyerInvoiceAddr { get; set; }
    }
}
