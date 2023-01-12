using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CCatPay_Net
{
    public class UpdateSmsName : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public UpdateSmsName()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region CVS COCS 變更簡訊名稱 (SMS)
        /// <summary>
        /// CVS COCS 變更簡訊名稱 (SMS)
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public ReturnBasic UpdateSmsShortName(UpdateSmsModel update)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(update));

            ReturnBasic rtnOrder = new ReturnBasic();

            if (errList.Count > 0)
            {
                rtnOrder.Status = "ERROR";
                rtnOrder.Message = String.Join(", ", errList);
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
                                     , service_type = update.ServiceType
                                     , sms_short_name = update.SmsShortName
                                 });
                }

                rtnOrder = _utilityProcess.ReturnOrder<UpdateSmsModel, ReturnBasic>(update, jsonString, ref errList);
            }

            return rtnOrder;
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