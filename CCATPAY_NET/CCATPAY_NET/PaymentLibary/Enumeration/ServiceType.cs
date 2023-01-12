using System.ComponentModel.DataAnnotations;

namespace CCatPay_Net
{
    #region 金流服務類型
    /// <summary>
    /// 金流服務類型
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// 代收代付
        /// </summary>
        [Display(Name = "代收代付")]
        CVS = 0,

        /// <summary>
        /// 線上刷卡
        /// </summary>
        [Display(Name = "線上刷卡")]
        COCS = 1,

        /// <summary>
        /// 貨到收現
        /// </summary>
        [Display(Name = "貨到收現")]
        COD = 2,

        /// <summary>
        /// 行動支付
        /// </summary>
        [Display(Name = "行動支付")]
        DPH = 3,

        /// <summary>
        /// 一般單
        /// </summary>
        [Display(Name = "速達一般單")]
        General = 4,

        /// <summary>
        /// 到所取貨收現
        /// </summary>
        [Display(Name = "到所取貨收現")]
        CashOnTCat = 5
    }
    #endregion
}