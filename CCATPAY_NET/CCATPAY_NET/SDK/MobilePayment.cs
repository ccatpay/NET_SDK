using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CCatPay_Net
{
    public class MobilePayment : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public MobilePayment()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region DPH 取消授權
        /// <summary>
        /// DPH 刷卡取消授權
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ReturnBasic DphOrderCancel(DphOrderModel order)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(order));

            ReturnBasic rtnOrder = new ReturnBasic();

            if (errList.Count > 0)
            {
                rtnOrder.Status = "ERROR";
                rtnOrder.Message = String.Join(", ", errList);
            }
            else
            {
                string jsonString = string.Empty;

                if (order != null)
                {
                    jsonString = JsonConvert.SerializeObject(new
                                 {
                                     cmd = order.Command
                                     , customer_order_no = order.CustomerOrderNo
                                     , order_amount = order.OrderAmount
                                     , acquirer_type = order.AcquirerType
                                     , send_time = order.SendTime
                                 });
                }

                rtnOrder = _utilityProcess.ReturnOrder<DphOrderModel , ReturnBasic>(order, jsonString, ref errList);
            }

            return rtnOrder;
        }
        #endregion

        #region DPH 取消交易
        /// <summary>
        /// DPH 取消交易，僅提供全額退款，不能指定退款金額
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ReturnOrder DphOrderRefund(DphOrderModel order)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(order));

            ReturnOrder rtnOrder = new ReturnOrder();

            if (errList.Count > 0)
            {
                rtnOrder.Status = "ERROR";
                rtnOrder.Message = String.Join(", ", errList);
            }
            else
            {
                string jsonString = string.Empty;

                if (order != null)
                {
                    jsonString = JsonConvert.SerializeObject(new
                                 {
                                     cmd = order.Command
                                     , customer_order_no = order.CustomerOrderNo
                                     , order_amount = order.OrderAmount
                                     , acquirer_type = order.AcquirerType
                                     , send_time = (order.SendTime.HasValue)
                                                    ? order.SendTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                    : string.Empty
                                 });
                }

                rtnOrder = _utilityProcess.ReturnOrder<DphOrderModel , ReturnOrder>(order, jsonString, ref errList);
            }

            return rtnOrder;
        }
        #endregion

        #region DPH 指定請款金額
        /// <summary>
        /// DPH 指定請款金額 (僅支援 OPEN 錢包)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ReturnOrder DphCashRequest(DphOrderModel order)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(order));

            ReturnOrder rtnOrder = new ReturnOrder();

            if (errList.Count > 0)
            {
                rtnOrder.Status = "ERROR";
                rtnOrder.Message = String.Join(", ", errList);
            }
            else
            {
                string jsonString = string.Empty;

                if (order != null)
                {
                    jsonString = JsonConvert.SerializeObject(new
                                 {
                                     cmd = order.Command
                                     , customer_order_no = order.CustomerOrderNo
                                     , order_amount = order.OrderAmount
                                     , cr_amount = order.CrAmount
                                     , send_time = order.SendTime
                                 });
                }

                rtnOrder = _utilityProcess.ReturnOrder<DphOrderModel, ReturnOrder>(order, jsonString, ref errList);
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