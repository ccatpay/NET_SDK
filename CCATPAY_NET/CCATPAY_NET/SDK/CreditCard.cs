using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CCatPay_Net
{
    public class CreditCard : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public CreditCard()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region COCS 刷卡取消授權
        /// <summary>
        /// COCS 刷卡取消授權
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ReturnBasic CocsOrderCancel(CocsOrderModel order)
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
                                     , cust_id = order.CustomerId
                                     , cust_order_no = order.CustomerOrderNo
                                     , order_amount = order.OrderAmount
                                     , acquirer_type = order.AcquirerType
                                     , send_time = order.SendTime
                                 });
                }

                rtnOrder = _utilityProcess.ReturnOrder<CocsOrderModel , ReturnBasic>(order, jsonString, ref errList);
            }

            return rtnOrder;
        }
        #endregion

        #region COCS 訂單取消交易 (退貨、退款 )
        /// <summary>
        /// COCS 訂單取消交易 (退貨、退款 )
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ReturnOrder CocsOrderRefund(CocsOrderModel order)
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
                    switch (order.Command)
                    {
                        case "CocsOrderRefund":
                            jsonString = CocsOrderRefundJsonString(order);
                            break;
                        case "CocsUnionpayRefund":
                            jsonString = CocsUnionpayRefundJsonString(order);
                            break;
                    }
                }

                rtnOrder = _utilityProcess.ReturnOrder<CocsOrderModel , ReturnOrder>(order, jsonString, ref errList);
            }

            return rtnOrder;
        }

        /// <summary>
        /// COCS 訂單取消交易 (退貨、退款 ) JSON Content
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string CocsOrderRefundJsonString(CocsOrderModel order)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                    cmd = order.Command
                                    , cust_id = order.CustomerId
                                    , cust_order_no = order.CustomerOrderNo
                                    , order_amount = order.OrderAmount
                                    , refund_amount = order.RefundAmount
                                    , acquirer_type = order.AcquirerType
                                    , send_time = (order.SendTime.HasValue)
                                                   ? order.SendTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                   : string.Empty
                                });

            return jsonString;
        }

        /// <summary>
        /// COCS 玉山銀聯卡 訂單取消交易 (退貨、退款 ) JSON Content
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string CocsUnionpayRefundJsonString(CocsOrderModel order)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = order.Command
                                     , cust_id = order.CustomerId
                                     , cust_order_no = order.CustomerOrderNo
                                     , order_amount = (order.OrderAmount.HasValue) 
                                                      ? order.OrderAmount.Value.ToString()
                                                      : string.Empty
                                     , refund_amount = order.RefundAmount
                                     , send_time = (order.SendTime.HasValue)
                                                   ? order.SendTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                   : string.Empty
                                });

            return jsonString;
        }
        #endregion

        #region COCS 指定請款金額
        /// <summary>
        /// COCS 指定請款金額
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ReturnOrder CocsCashRequest(CocsOrderModel order)
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
                                     , cust_id = order.CustomerId
                                     , cust_order_no = order.CustomerOrderNo
                                     , order_amount = order.OrderAmount
                                     , cr_amount = order.CrAmount
                                     , acquirer_type = order.AcquirerType
                                     , send_time = order.SendTime
                                 });
                }

                rtnOrder = _utilityProcess.ReturnOrder<CocsOrderModel, ReturnOrder>(order, jsonString, ref errList);
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