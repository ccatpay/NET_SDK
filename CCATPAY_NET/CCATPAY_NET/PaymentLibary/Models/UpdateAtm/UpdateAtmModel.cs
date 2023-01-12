using System;

namespace CCatPay_Net
{
    /// <summary>
    /// Token
    /// </summary>
    public class UpdateAtmModel : TokenModel
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
        /// 虛擬帳號
        /// </summary>
        public string VirtualAccount { get; set; }
    }
}