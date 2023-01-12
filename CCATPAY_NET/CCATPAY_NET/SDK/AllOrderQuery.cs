using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CCatPay_Net
{
    public class AllOrderQuery : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public AllOrderQuery()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region 查詢訂單
        /// <summary>
        /// 單筆查詢訂單 (訂單號碼)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public T OrderQuery<T>(OrderQueryModel query)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(query));

            if (errList.Count == 0)
            {
                string jsonString = string.Empty;

                if (query != null)
                {
                    jsonString = JsonConvert.SerializeObject(new
                                 {
                                     cmd = query.Command
                                     , cust_id = query.CustomerId
                                     , cust_order_no = query.CustomerOrderNo
                                 });
                }

                var cvsOrder = _utilityProcess.ReturnOrder<OrderQueryModel, T>(query, jsonString, ref errList);

                return cvsOrder;
            }
            else
                return _utilityProcess.SetErrorList<T>(ref errList);
        }

        /// <summary>
        /// 批次查詢訂單 (日期)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public T OrderListQuery<T>(OrderQueryModel query)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(query));

            if (errList.Count == 0)
            {
                try
                {
                    if (query != null)
                    {
                        string jsonString = string.Empty;

                        if (query != null)
                        {
                            jsonString = JsonConvert.SerializeObject(new
                                               {
                                                   cmd = query.Command
                                                   , cust_id = query.CustomerId
                                                   , order_start_date = (query.OrderStartDate.HasValue) 
                                                                       ? query.OrderStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                                       : string.Empty
                                                   , order_end_date = (query.OrderEndDate.HasValue)
                                                                     ? query.OrderEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                                     : string.Empty
                                               });
                        }

                        T orderList = _utilityProcess.ReturnOrder<OrderQueryModel, T>(query, jsonString, ref errList);

                        if (typeof(T) == typeof(ReturnCvsOrderList)
                         || typeof(T) == typeof(ReturnCocsOrderList)
                         || typeof(T) == typeof(ReturnDphOrderList))
                        {
                            var value = typeof(T).GetProperty("OrderList").GetValue(orderList);

                            if (value.GetType() == typeof(List<ReturnCvsOrder>))
                            {
                                foreach (var item in (List<ReturnCvsOrder>)value)
                                {
                                    item.Status = "ERROR";
                                    item.Message = String.Join(", ", errList);
                                }
                            }
                            else if (value.GetType() == typeof(List<ReturnCocsOrder>))
                            {
                                foreach (var item in (List<ReturnCocsOrder>)value)
                                {
                                    item.Status = "ERROR";
                                    item.Message = String.Join(", ", errList);
                                }
                            }
                            else if (value.GetType() == typeof(List<ReturnDphOrder>))
                            {
                                foreach (var item in (List<ReturnDphOrder>)value)
                                {
                                    item.Status = "ERROR";
                                    item.Message = String.Join(", ", errList);
                                }
                            }
                        }

                        return orderList;
                    }
                }
                catch (Exception ex)
                {
                    errList.Add(ex.Message);
                }
            }

            return _utilityProcess.SetErrorList<T>(ref errList);
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