using System.ComponentModel.DataAnnotations;

namespace CCatPay_Net
{
    /// <summary>
    /// 繳款方式
    /// </summary>
    public enum PaymentType : int
    {
        /// <summary>
        /// ibon繳款
        /// </summary>
        [Display(Name = "ibon繳款")]
        Ibon = 0,

        /// <summary>
        /// ATM銀行轉帳
        /// </summary>
        [Display(Name = "ATM銀行轉帳")]
        Atm = 1,

        /// <summary>
        /// 三段式條碼
        /// </summary>
        [Display(Name = "三段式條碼")]
        Barcode = 2,

        /// <summary>
        /// 線上刷卡
        /// </summary>
        [Display(Name = "線上刷卡")]
        CreditCard = 3,

        /// <summary>
        /// 貨到收現
        /// </summary>
        [Display(Name = "宅配貨到收現")]
        CashOnDemand = 4,

        /// <summary>
        /// 一般物流
        /// </summary>
        [Display(Name = "一般物流")]
        NormalCOD = 5,

        /// <summary>
        /// 快速到店收現
        /// </summary>
        [Display(Name = "快速到店收現")]
        CashOnB2S = 6,

        ///// <summary>
        ///// OPEN錢包
        ///// </summary>
        //[Display(Name = "OPEN錢包")]
        //OPW = 7,

        ///// <summary>
        ///// ICashPay
        ///// </summary>
        //[Display(Name = "ICashPay")]
        //ICP = 8,

        /// <summary>
        /// 行動支付
        /// </summary>
        [Display(Name = "行動支付")]
        DPH = 7,

        /// <summary>
        /// 三段式條碼(即時繳款通知)
        /// </summary>
        [Display(Name = "三段式條碼(即時繳款通知)")]
        BarcodeImmediate = 9,

        /// <summary>
        /// 到所取貨收現
        /// </summary>
        [Display(Name = "到所取貨收現")]
        CashOnTCat = 10,

        /// <summary>
        /// 不收款
        /// </summary>
        [Display(Name = "不收款")]
        General = 98,

        /// <summary>
        /// 未設定 (舊API才會有此狀態)
        /// </summary>
        [Display(Name = "None")]
        None = 99
    }
}