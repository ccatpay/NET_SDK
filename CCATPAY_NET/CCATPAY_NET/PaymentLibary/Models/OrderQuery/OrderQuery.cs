using System;

namespace CCatPay_Net
{
    /// <summary>
    /// Token
    /// </summary>
    public class OrderQueryModel : TokenModel
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
        /// 訂單日期起日 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime? OrderStartDate { get; set; }

        /// <summary>
        /// 訂單日期迄日 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime? OrderEndDate { get; set; }
    }
}