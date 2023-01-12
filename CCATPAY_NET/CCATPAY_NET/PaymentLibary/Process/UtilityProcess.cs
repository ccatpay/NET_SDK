using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.UI.WebControls;

namespace CCatPay_Net
{
    public class UtilityProcess
    {
        #region 回傳訂單資訊
        public V ReturnOrder<T , V>(T order, string jsonString, ref List<string> errList)
        {
            try
            {
                HttpClientModel httpClient = new HttpClientModel()
                {
                    StringContent = jsonString
                   , Encoding = Encoding.UTF8
                   , MediaType = "application/json"
                   , RequestUrl = "Collect"
                };

                string resultJSON = GetResponse(order, httpClient);

                V rtnOrder = JsonConvert.DeserializeObject<V>(resultJSON);

                return rtnOrder;
            }
            catch (Exception ex)
            {
                errList.Add(ex.Message);
            }

            V rtnErrorList = SetErrorList<V>(ref errList);

            return rtnErrorList;
        }
        #endregion

        #region 呼叫 API
        /// <summary>
        /// 呼叫 API
        /// </summary>
        /// <param name="query"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetResponse<T>(T query, HttpClientModel model)
        {
            var client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            #region 取得 Token Api Url
            string apiUrl = GetString<T>(query , "ApiUrl");

            client.BaseAddress = new Uri(apiUrl);
            #endregion

            #region 取得 Token
            if (!model.RequestUrl.Equals("Token"))
            {
                string accessToken = GetString<T>(query, "AccessToken");

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer " + accessToken);
            }
            #endregion

            StringContent jsonContent = new StringContent(model.StringContent , model.Encoding, model.MediaType);

            HttpResponseMessage response = client.PostAsync(model.RequestUrl , jsonContent).Result;

            return response.Content.ReadAsStringAsync().Result;
        }
        #endregion

        #region 泛型類別欄位取值
        /// <summary>
        /// 泛型類別欄位取值 (String)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="order"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetString<T>(T order , string columnName)
        {
            string getValue = string.Empty;

            var columnValue = typeof(T).GetProperty(columnName).GetValue(order);

            if (columnValue.GetType() == typeof(string))
                getValue = (string)columnValue;

            return getValue;
        }

        /// <summary>
        /// 泛型類別欄位取值 (decimal)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="order"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public decimal GetDecimal<T>(T order, string columnName)
        {
            decimal getValue = 0;

            var columnValue = typeof(T).GetProperty(columnName).GetValue(order);

            if (columnValue.GetType() == typeof(decimal))
                getValue = (decimal)columnValue;

            return getValue;
        }

        /// <summary>
        /// 泛型類別欄位取值 (DateTime)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="order"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public DateTime? GetDateTime<T>(T order, string columnName)
        {
            DateTime? getValue = null;

            var columnValue = typeof(T).GetProperty(columnName).GetValue(order);

            if (columnValue.GetType() == typeof(DateTime))
                getValue = (DateTime)columnValue;

            return getValue;
        }

        public PaymentType? GetPaymentType<T>(T order, string columnName)
        {
            PaymentType? getValue = null;

            var columnValue = typeof(T).GetProperty(columnName).GetValue(order);

            if (columnValue.GetType() == typeof(PaymentType))
                getValue = (PaymentType)columnValue;

            return getValue;
        }
        #endregion

        #region 回傳泛型類別轉型
        /// <summary>
        /// 回傳泛型類別轉型
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="errList"></param>
        /// <returns></returns>
        public V SetErrorList<V>(ref List<string> errList)
        {
            if (typeof(V) == typeof(ReturnBasic))
            {
                ReturnBasic returnBasic = new ReturnBasic();

                if (errList.Count > 0)
                {
                    returnBasic.Status = "ERROR";
                    returnBasic.Message = String.Join(", ", errList);
                }
                else
                {
                    returnBasic.Status = "OK";
                    returnBasic.Message = string.Empty;
                }

                return (V)Convert.ChangeType(returnBasic, typeof(V));
            }
            else if (typeof(V) == typeof(ReturnOrder))
            {
                ReturnOrder returnOrder = new ReturnOrder();

                if (errList.Count > 0)
                {
                    returnOrder.Status = "ERROR";
                    returnOrder.Message = String.Join(", ", errList);
                }
                else
                {
                    returnOrder.Status = "OK";
                    returnOrder.Message = string.Empty;
                }

                return (V)Convert.ChangeType(returnOrder, typeof(V));
            }
            else if (typeof(V) == typeof(ReturnCvsOrder))
            {
                ReturnCvsOrder returnCvsOrder = new ReturnCvsOrder();

                if (errList.Count > 0)
                {
                    returnCvsOrder.Status = "ERROR";
                    returnCvsOrder.Message = String.Join(", ", errList);
                }
                else
                {
                    returnCvsOrder.Status = "OK";
                    returnCvsOrder.Message = string.Empty;
                }

                return (V)Convert.ChangeType(returnCvsOrder, typeof(V));

            }
            else if (typeof(V) == typeof(ReturnCocsOrder))
            {
                ReturnCocsOrder returnCocsOrder = new ReturnCocsOrder();

                if (errList.Count > 0)
                {
                    returnCocsOrder.Status = "ERROR";
                    returnCocsOrder.Message = String.Join(", ", errList);
                }
                else
                {
                    returnCocsOrder.Status = "OK";
                    returnCocsOrder.Message = string.Empty;
                }

                return (V)Convert.ChangeType(returnCocsOrder, typeof(V));
            }
            else if (typeof(V) == typeof(ReturnDphOrder))
            {
                ReturnDphOrder returnDphOrder = new ReturnDphOrder();

                if (errList.Count > 0)
                {
                    returnDphOrder.Status = "ERROR";
                    returnDphOrder.Message = String.Join(", ", errList);
                }
                else
                {
                    returnDphOrder.Status = "OK";
                    returnDphOrder.Message = string.Empty;
                }

                return (V)Convert.ChangeType(returnDphOrder, typeof(V));
            }
            else if (typeof(V) == typeof(ReturnCvsOrderList))
            {
                ReturnCvsOrderList returnCvsOrder = new ReturnCvsOrderList();

                if (errList.Count > 0)
                {
                    returnCvsOrder.Status = "ERROR";
                    returnCvsOrder.Message = String.Join(", ", errList);
                }
                else
                {
                    returnCvsOrder.Status = "OK";
                    returnCvsOrder.Message = "Count:0";
                }

                returnCvsOrder.OrderList = new List<ReturnCvsOrder>();

                return (V)typeof(V).GetProperty("OrderList").GetValue(returnCvsOrder);

            }
            else if (typeof(V) == typeof(ReturnCocsOrderList))
            {
                ReturnCocsOrderList returnCocsOrder = new ReturnCocsOrderList();

                if (errList.Count > 0)
                {
                    returnCocsOrder.Status = "ERROR";
                    returnCocsOrder.Message = String.Join(", ", errList);
                }
                else
                {
                    returnCocsOrder.Status = "OK";
                    returnCocsOrder.Message = "Count:0";
                }

                returnCocsOrder.OrderList = new List<ReturnCocsOrder>();

                return (V)typeof(V).GetProperty("OrderList").GetValue(returnCocsOrder);
            }
            else
            {
                ReturnDphOrderList returnDphOrder = new ReturnDphOrderList();

                if (errList.Count > 0)
                {
                    returnDphOrder.Status = "ERROR";
                    returnDphOrder.Message = String.Join(", ", errList);
                }
                else
                {
                    returnDphOrder.Status = "OK";
                    returnDphOrder.Message = "Count:0";
                }

                returnDphOrder.OrderList = new List<ReturnDphOrder>();

                return (V)typeof(V).GetProperty("OrderList").GetValue(returnDphOrder);
            }
        }
        #endregion

        #region 取出多項錯誤
        /// <summary>
        /// 取出多項錯誤
        /// </summary>
        /// <param name="errStatus"></param>
        /// <param name="errMsg"></param>
        /// <param name="enErrors"></param>
        public List<string> SplitErrorMsg(string errStatus, string errMsg, List<string> enErrors)
        {
            List<string> returnErrors = new List<string>();

            foreach (var item in enErrors)
            {
                returnErrors.Add(item);
            }

            if (!String.IsNullOrWhiteSpace(errStatus))
            {
                if (errMsg.Contains(","))
                {
                    var errAry = errMsg.Split(',');

                    foreach (var item in errAry)
                    {
                        returnErrors.Add(item);
                    }
                }
                else
                {
                    returnErrors.Add(errMsg);
                }
            }

            return returnErrors;
        }
        #endregion

        #region 調整時區 (AddHours)
        /// <summary>
        /// 調整時區 (AddHours)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DateTime? ChangeTimeZone(DateTime? dt , double value)
        {
            if (value > 14 || value < -12)
                return dt;
            else
                return (dt.HasValue) ? dt.Value.AddHours(value) : dt;
        }
        #endregion

        #region 將列舉轉為 ListItem
        /// <summary>
        /// 將列舉轉為 ListItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public List<ListItem> AllEnumToList<T>(string textType , string valueType)
        {
            var enumList = (T[])Enum.GetValues(typeof(T)).Cast<T>();

            List<ListItem> ddls = null;

            for (int i = 0; i < enumList.Length; i++)
            {
                if (ddls == null)
                    ddls = new List<ListItem>();

                T enumValue = enumList[i];

                ddls.Add(new ListItem(GetListValue(enumValue, textType), GetListValue(enumValue, valueType)));
            }

            return ddls;
        }
        #endregion

        #region 將部分列舉轉為 ListItem
        /// <summary>
        /// 將部分列舉轉為 ListItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public List<ListItem> PartEnumToList<T>(T[] array, string textType, string valueType)
        {
            var enumList = (T[])Enum.GetValues(typeof(T)).Cast<T>();

            List<ListItem> ddls = null;

            for (int i = 0; i < enumList.Length; i++)
            {
                if (ddls == null)
                    ddls = new List<ListItem>();

                T enumValue = enumList[i];

                if (array.Contains(enumValue))
                    ddls.Add(new ListItem(GetListValue(enumValue ,textType), GetListValue(enumValue , valueType)));
            }

            return ddls;
        }
        #endregion

        #region 依照類型取值
        /// <summary>
        /// 依照類型取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetListValue<T>(T enumValue, string type)
        {
            string value = string.Empty;

            switch (type)
            {
                 case "value":
                     value = enumValue.GetHashCode().ToString();
                     break;
                 case "name":
                     value = enumValue.ToString();
                     break;
                 case "display":
                     value = enumValue.DisplayName();
                     break;
            }

            return value;
        }
        #endregion
    }
}