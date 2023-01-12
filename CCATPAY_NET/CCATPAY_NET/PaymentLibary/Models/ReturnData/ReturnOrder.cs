using Newtonsoft.Json;
using System;

namespace CCatPay_Net
{
    /// <summary>
    /// ReturnOrder
    /// </summary>
    public class ReturnOrder : ReturnBasic
    {
        /// <summary>
        /// 代繳金額
        /// </summary>
        [JsonProperty("order_amount")]
        public decimal? OrderAmount { get; set; }

        /// <summary>
        /// 繳費到期日 (yyyy-MM-dd)
        /// </summary>
        [JsonProperty("expire_date")]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// Ibon 代碼
        /// </summary>
        [JsonProperty("ibon_code")]
        public string IbonCode { get; set; }

        /// <summary>
        /// 安源 ibon 廠商代碼
        /// </summary>
        [JsonProperty("ibon_shopid")]
        public string IbonShopId { get; set; }

        /// <summary>
        /// 代繳帳號 (ATM轉帳帳號)
        /// </summary>
        [JsonProperty("virtual_account")]
        public string VirtualAccount { get; set; }

        /// <summary>
        /// 超商條碼 1
        /// </summary>
        [JsonProperty("st_barcode1")]
        public string STBarcode1 { get; set; }

        /// <summary>
        /// 超商條碼 2
        /// </summary>
        [JsonProperty("st_barcode2")]
        public string STBarcode2 { get; set; }

        /// <summary>
        /// 超商條碼 3
        /// </summary>
        [JsonProperty("st_barcode3")]
        public string STBarcode3 { get; set; }

        /// <summary>
        /// 帳單金額（需列印於帳單上之帳單金額）
        /// </summary>
        [JsonProperty("bill_amount")]
        public decimal? BillAmount { get; set; }

        /// <summary>
        /// 超商手續費
        /// </summary>
        [JsonProperty("cs_fee")]
        public decimal? CSFee { get; set; }

        /// <summary>
        /// 代繳資訊建立時間 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        [JsonProperty("create_time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 訂單程序狀態代碼
        /// </summary>
        [JsonProperty("process_code")]
        public string ProcessCode { get; set; }

        /// <summary>
        /// 訂單程序狀態變更日期 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        [JsonProperty("process_code_update_time")]
        public DateTime? ProcessCodeUpdateTime { get; set; }

        /// <summary>
        /// 繳款日期
        /// </summary>
        [JsonProperty("pay_date")]
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// 預計撥款金額
        /// </summary>
        [JsonProperty("grant_amount")]
        public decimal? GrantAmount { get; set; }

        /// <summary>
        /// 預計撥款日期 (yyyy-MM-dd)
        /// </summary>
        [JsonProperty("grant_date")]
        public DateTime? GrantDate { get; set; }

        /// <summary>
        /// 商店短網址
        /// </summary>
        [JsonProperty("short_url")]
        public string ShortUrl { get; set; }

        /// <summary>
        /// ibon繳款 門市店號
        /// </summary>
        [JsonProperty("storeId")]
        public string StoreId { get; set; }

        /// <summary>
        /// 是否列印紙本發票 0:不列印、1:列印
        /// </summary>
        [JsonProperty("print_invoice")]
        public string PrintInvoice { get; set; }

        /// <summary>
        /// 載具類別 1:會員載具、2:手機條碼、3:自然人憑證
        /// </summary>
        [JsonProperty("vehicle_type")]
        public string VehicleType { get; set; }

        /// <summary>
        /// 載具條碼
        /// </summary>
        [JsonProperty("vehicle_barcode")]
        public string VehicleBarcode { get; set; }

        /// <summary>
        /// 是否捐贈發票 0:不捐贈、1:捐贈
        /// </summary>
        [JsonProperty("donate_invoice")]
        public string DonateInvoice { get; set; }

        /// <summary>
        /// 愛心碼
        /// </summary>
        [JsonProperty("love_code")]
        public string LoveCode { get; set; }


        /// <summary>
        /// 發票號碼
        /// </summary>
        [JsonProperty("invoice_no")]
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 發票建立日期
        /// </summary>
        [JsonProperty("invoice_date")]
        public DateTime? InvoiceDate { get; set; }

        /// <summary>
        /// 隨機碼
        /// </summary>
        [JsonProperty("random_number")]
        public string RandomNumber { get; set; }

        /// <summary>
        /// 回報網址
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 退款金額
        /// </summary>
        [JsonProperty("refund_amount")]
        public string RefundAmount { get; set; }

        /// <summary>
        /// 指定請款金額
        /// </summary>
        [JsonProperty("cr_amount")]
        public string CrAmount { get; set; }
    }

    /// <summary>
    /// ReturnCvsOrder
    /// </summary>
    public class ReturnCvsOrder : ReturnOrder
    {
        /// <summary>
        /// Cvs收單行 (預設 0:玉山 ) 0:玉山、1:中信、2:安源 ibon
        /// </summary>
        [JsonProperty("cvs_acquirer_type")]
        public string CvsAcquirerType { get; set; }
    }

    /// <summary>
    /// ReturnCocsOrder
    /// </summary>
    public class ReturnCocsOrder : ReturnOrder
    {
        /// <summary>
        /// Cocs 收單行 0:玉山、1:中信
        /// </summary>
        [JsonProperty("acquirer_type")]
        public string CocsAcquirerType { get; set; }
    }

    /// <summary>
    /// ReturnDphOrder
    /// </summary>
    public class ReturnDphOrder : ReturnOrder
    {
        /// <summary>
        /// Dph 收單行 0：opw(OPEN錢包)、1：icp(icashPay)
        /// </summary>
        [JsonProperty("acquirer_type")]
        public string DphAcquirerType { get; set; }
    }
}