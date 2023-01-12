using System.ComponentModel.DataAnnotations;

namespace CCatPay_Net
{
    #region 載具類別
    /// <summary>
    /// 載具類別
    /// </summary>
    public enum VehicleType
    {
        /// <summary>
        /// 會員載具
        /// </summary>
        [Display(Name = "會員載具")]
        Member = 1,
        /// <summary>
        /// 手機條碼
        /// </summary>
        [Display(Name = "手機條碼")]
        Mobile = 2,
        /// <summary>
        /// 自然人憑證
        /// </summary>
        [Display(Name = "自然人憑證")]
        Natural = 3,
    }
    #endregion
}