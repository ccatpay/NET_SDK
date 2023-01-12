using System.ComponentModel.DataAnnotations;

namespace CCatPay_Net
{
    #region CvsAcquirerType
    /// <summary>
    /// CvsAcquirerType
    /// </summary>
    public enum CvsAcquirerType
    {
        /// <summary>
        /// 玉山
        /// </summary>
        [Display(Name = "玉山")]
        esun = 1,

        /// <summary>
        /// 中信
        /// </summary>
        [Display(Name = "中信")]
        chinatrust = 2
    }
    #endregion

    #region CocsAcquirerType
    /// <summary>
    /// CocsAcquirerType
    /// </summary>
    public enum CocsAcquirerType
    {
        /// <summary>
        /// 玉山
        /// </summary>
        [Display(Name = "玉山")]
        esun,

        /// <summary>
        /// 中信
        /// </summary>
        [Display(Name = "中信")]
        chinatrust
    }
    #endregion

    #region DphAcquirerType
    /// <summary>
    /// DphAcquirerType
    /// </summary>
    public enum DphAcquirerType
    {
        /// <summary>
        /// OPEN錢包
        /// </summary>
        [Display(Name = "OP錢包")]
        opw = 1,

        /// <summary>
        /// ICashPay
        /// </summary>
        [Display(Name = "ICashPay")]
        icp = 2
    }
    #endregion
}