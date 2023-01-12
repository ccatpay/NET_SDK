using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CCatPay_Net
{
    public class UpdateCvsAtm : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public UpdateCvsAtm()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region CVS 變更金額 (ATM)
        /// <summary>
        /// CVS 變更金額 (ATM)
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public ReturnCvsOrder UpdateAmount(UpdateAtmModel update)
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
                                     , virtual_account = update.VirtualAccount
                                 });
                }

                cvsOrder = _utilityProcess.ReturnOrder<UpdateAtmModel, ReturnCvsOrder>(update, jsonString, ref errList);
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