using System;

namespace CCatPay_Net
{
    /// <summary>
    /// Token
    /// </summary>
    public class UpdateIbonModel : TokenModel
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
        /// 契客訂單編號 (必須是已建立的繳款單)
        /// </summary>
        public string CustomerOrderNo { get; set; }

        /// <summary>
        /// 異動後的繳款單金額
        /// </summary>
        public decimal? OrderAmount { get; set; }

        /// <summary>
        /// 繳費到期日 (yyyy-MM-dd)
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 廠商代碼 (CCAT、BCAT) 
        /// </summary>
        public string IbonShopId { get; set; }

        /// <summary>
        /// ibon 代碼
        /// </summary>
        public string IbonCode { get; set; }

        /// <summary>
        /// 隨機數(10碼)
        /// 不會重覆的時間 + 亂數組合採用時分秒 + 亂數產生 :HHNNSSRRRR
        /// HHNNSS:主機時間的時分秒(6位數 )
        /// RRRR:四位數隨機亂數
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 檢核驗證碼比對
        /// checksum = MD5 (cust_order_no + ":" + order_amount +":" + nonce )
        /// HHNNSS:主機時間的時分秒(6位數 )
        /// RRRR:四位數隨機亂數
        /// </summary>
        public string CheckSum { get; set; }
    }
}