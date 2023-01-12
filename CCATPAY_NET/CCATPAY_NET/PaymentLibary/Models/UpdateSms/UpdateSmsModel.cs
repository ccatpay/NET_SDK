using System;

namespace CCatPay_Net
{
    /// <summary>
    /// Token
    /// </summary>
    public class UpdateSmsModel : TokenModel
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
        /// 金流服務類型
        /// 0：CVS 代收代付
        /// 1：COCS 線上刷卡
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 調整後簡訊簡稱
        /// </summary>
        public string SmsShortName { get; set; }
    }
}