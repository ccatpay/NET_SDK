namespace CCatPay_Net
{
    /// <summary>
    /// 全功能介接參數的類別。
    /// </summary>
    public class InvoiceModel
    {
        public InvoiceModel()
        {
            B2C = "0";
        }

        /// <summary>
        /// 此訂單是否開立電子發票 0:不開立(預設) 1:開立
        /// </summary>
        public string B2C { get; set; }

        /// <summary>
        /// 商品名稱(b2c為 1 則必填)
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 是否列印紙本發票(b2c為 1 則必填) 0:不列印 1:列印
        /// </summary>
        public string PrintInvoice { get; set; }

        /// <summary>
        /// 載具類別 1:會員載具 2:手機條碼 3:自然人憑證
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 載具條碼
        /// </summary>
        public string VehicleBarcode { get; set; }

        /// <summary>
        /// 是否捐贈發票(b2c為 1 則必填) 0:不捐贈 1:捐贈
        /// </summary>
        public string DonateInvoice { get; set; }

        /// <summary>
        /// 愛心碼(選捐贈。愛心碼如不輸入，預設為創世基金會919)
        /// </summary>
        public string LoveCode { get; set; }

        /// <summary>
        /// 買方統一編號
        /// </summary>
        public string BuyerBillNo { get; set; }

        /// <summary>
        /// 發票抬頭
        /// </summary>
        public string BuyerInvoiceTitle { get; set; }


    }
}
