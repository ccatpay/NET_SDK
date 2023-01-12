using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CCatPay_Net
{
    public class UpdateCvsIbon : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public UpdateCvsIbon()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region CVS 變更金額 (Ibon)
        /// <summary>
        /// CVS 變更金額 (Ibon)
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public ReturnCvsOrder UpdateAmount(UpdateIbonModel update)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(update));

            ReturnCvsOrder cvsOrder = new ReturnCvsOrder();

            if (errList.Count > 0)
            {
                cvsOrder.Status = "ERROR";
                cvsOrder.Message = String.Join(", ", errList);
            }
            else
            {
                string jsonString = string.Empty;

                if (update != null)
                {
                    jsonString = JsonConvert.SerializeObject(new
                                 {
                                     cmd = update.Command
                                     , cust_id = update.CustomerId
                                     , cust_order_no = update.CustomerOrderNo
                                     , order_amount = update.OrderAmount
                                     , ibon_shopid = update.IbonShopId
                                     , ibon_code = update.IbonCode
                                 });
                }

                cvsOrder = _utilityProcess.ReturnOrder<UpdateIbonModel, ReturnCvsOrder>(update, jsonString, ref errList);
            }

            return cvsOrder;
        }
        #endregion

        #region CVS 變更繳款到期日 (Ibon)
        /// <summary>
        /// CVS 變更繳款到期日 (Ibon)
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public ReturnCvsOrder UpdateExpireDate(UpdateIbonModel update)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(update));

            ReturnCvsOrder cvsOrder = new ReturnCvsOrder();

            if (errList.Count > 0)
            {
                cvsOrder.Status = "ERROR";
                cvsOrder.Message = String.Join(", ", errList);
            }
            else
            {
                string jsonString = string.Empty;

                if (update != null)
                {
                    jsonString = JsonConvert.SerializeObject(new
                                 {
                                     cmd = update.Command
                                     , cust_id = update.CustomerId
                                     , cust_order_no = update.CustomerOrderNo
                                     , order_amount = update.OrderAmount
                                     , expire_date = update.ExpireDate
                                     , ibon_shopid = update.IbonShopId
                                     , ibon_code = update.IbonCode
                                     , nonce = update.Nonce
                                     , checksum = update.CheckSum
                                 });
                }

                cvsOrder = _utilityProcess.ReturnOrder<UpdateIbonModel, ReturnCvsOrder>(update, jsonString, ref errList);
            }

            return cvsOrder;
        }
        #endregion

        #region 共用方法
        /// <summary>
        /// 執行與釋放 (Free)、釋放 (Release) 或重設 Unmanaged 資源相關聯之應用程式定義的工作。
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion
    }
}