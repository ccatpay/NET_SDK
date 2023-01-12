using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CCatPay_Net
{
    public class AllCreateOrder : IDisposable
    {
        private readonly UtilityProcess _utilityProcess;

        public AllCreateOrder()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region CVS COCS DPH 產生訂單
        /// <summary>
        /// CVS COCS DPH 產生訂單
        /// </summary>
        /// <param name="order"></param>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public T CreateOrder<T,V,K>(V order, K invoice)
        {
            List<string> errList = new List<string>();

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(order));

            // 驗證服務參數。
            errList.AddRange(ServerValidator.Validate(invoice));

            if (errList.Count == 0)
            {

                string jsonString = string.Empty;

                if (order != null)
                {
                    string orderCommand = _utilityProcess.GetString<V>(order, "Command");

                    string invoiceB2C = "0";
                    if (invoice != null)
                        invoiceB2C = _utilityProcess.GetString<K>(invoice, "B2C");

                    switch (orderCommand)
                    {
                        case "CvsOrderAppend":
                            jsonString = (invoiceB2C == "1") ? CvsWithInvoiceJsonString<V, K>(order, invoice)
                                                             : CvsNoInvoiceJsonString<V>(order);
                            break;
                        case "CocsOrderAppend":
                            jsonString = (invoiceB2C == "1") ? CocsWithInvoiceJsonString<V, K>(order, invoice)
                                                             : CocsNoInvoiceJsonString<V>(order);
                            break;
                        case "CocsUnionpayAppend":
                            jsonString = (invoiceB2C == "1") ? CocsUnionpayWithInvoiceJsonString<V, K>(order, invoice)
                                                             : CocsUnionpayNoInvoiceJsonString<V>(order);
                            break;
                        case "DphOrderAppend":
                            jsonString = (invoiceB2C == "1") ? DphWithInvoiceJsonString<V, K>(order, invoice)
                                                             : DphNoInvoiceJsonString<V>(order);
                            break;
                        default:
                            break;
                    }
                }

                T rtnOrder = _utilityProcess.ReturnOrder<V, T>(order, jsonString, ref errList);

                return rtnOrder;
            }
            else
                return _utilityProcess.SetErrorList<T>(ref errList);
        }
        #endregion

        #region CVS 產生訂單 JSON String
        /// <summary>
        /// CVS 產生訂單 不含電子發票的 JSON String
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string CvsNoInvoiceJsonString<V>(V order)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = _utilityProcess.GetString<V>(order, "Command")
                                     , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                     , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                     , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                     , expire_date = (_utilityProcess.GetDateTime<V>(order, "ExpireDate").HasValue) 
                                                     ? _utilityProcess.GetDateTime<V>(order, "ExpireDate").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                     : string.Empty
                                     , payer_name = _utilityProcess.GetString<V>(order, "PayerName")
                                     , payer_postcode = _utilityProcess.GetString<V>(order, "PayerPostCode")
                                     , payer_address = _utilityProcess.GetString<V>(order, "PayerAddress")
                                     , payer_mobile = _utilityProcess.GetString<V>(order, "PayerMobile")
                                     , payer_email = _utilityProcess.GetString<V>(order, "PayerEmail")
                                     , payment_type = (_utilityProcess.GetPaymentType<V>(order, "PaymentType").HasValue)
                                                     ? _utilityProcess.GetPaymentType<V>(order, "PaymentType").Value.GetHashCode().ToString()
                                                     : string.Empty
                                     , payment_acquirerType = _utilityProcess.GetString<V>(order, "PaymentAcquirerType")
                                     , apn_url =  _utilityProcess.GetString<V>(order, "ApnUrl")
                                     , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                });

            return jsonString;
        }

        /// <summary>
        /// CVS 產生訂單 含電子發票的 JSON String
        /// </summary>
        /// <param name="order"></param>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private string CvsWithInvoiceJsonString<V,K>(V order, K invoice)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = _utilityProcess.GetString<V>(order, "Command")
                                     , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                     , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                     , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                     , expire_date = (_utilityProcess.GetDateTime<V>(order, "ExpireDate").HasValue) 
                                                     ? _utilityProcess.GetDateTime<V>(order, "ExpireDate").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                     : string.Empty
                                     , payer_name = _utilityProcess.GetString<V>(order, "PayerName")
                                     , payer_postcode = _utilityProcess.GetString<V>(order, "PayerPostCode")
                                     , payer_address = _utilityProcess.GetString<V>(order, "PayerAddress")
                                     , payer_mobile = _utilityProcess.GetString<V>(order, "PayerMobile")
                                     , payer_email = _utilityProcess.GetString<V>(order, "PayerEmail")
                                     , payment_type = (_utilityProcess.GetPaymentType<V>(order, "PaymentType").HasValue)
                                                     ? _utilityProcess.GetPaymentType<V>(order, "PaymentType").Value.GetHashCode().ToString()
                                                     : string.Empty
                                     , payment_acquirerType = _utilityProcess.GetString<V>(order, "PaymentAcquirerType")
                                     , apn_url =  _utilityProcess.GetString<V>(order, "ApnUrl")
                                     , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                     , b2c = _utilityProcess.GetString<K>(invoice, "B2C")
                                     , product_name = _utilityProcess.GetString<K>(invoice, "ProductName")
                                     , print_invoice = _utilityProcess.GetString<K>(invoice, "PrintInvoice")
                                     , vehicle_type = _utilityProcess.GetString<K>(invoice, "VehicleType")
                                     , vehicle_barcode = _utilityProcess.GetString<K>(invoice, "VehicleBarcode")
                                     , donate_invoice = _utilityProcess.GetString<K>(invoice, "DonateInvoice")
                                     , love_code = String.IsNullOrWhiteSpace(_utilityProcess.GetString<K>(invoice, "LoveCode")) 
                                                   ? (_utilityProcess.GetString<K>(invoice, "DonateInvoice") == "1") ? "919"
                                                                                                                     : string.Empty
                                                   : _utilityProcess.GetString<K>(invoice, "LoveCode")
                                     , buyer_bill_no = _utilityProcess.GetString<K>(invoice, "BuyerBillNo")
                                     , buyer_invoice_title = _utilityProcess.GetString<K>(invoice, "BuyerInvoiceTitle")
                                     , buyer_invoice_zip = _utilityProcess.GetString<K>(invoice, "BuyerInvoiceZip")
                                     , buyer_invoice_addr = _utilityProcess.GetString<K>(invoice, "BuyerInvoiceAddr")
                                });

            return jsonString;
        }
        #endregion

        #region COCS 產生訂單 JSON String
        /// <summary>
        /// COCS 產生訂單 不含電子發票的 JSON String
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string CocsNoInvoiceJsonString<V>(V order)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = _utilityProcess.GetString<V>(order, "Command")
                                     , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                     , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                     , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                     , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                     , acquirer_type = _utilityProcess.GetString<V>(order, "AcquirerType")
                                     , limit_product_id = _utilityProcess.GetString<V>(order, "LimitProductId")
                                     , send_time = (_utilityProcess.GetDateTime<V>(order, "SendTime").HasValue) 
                                                     ? _utilityProcess.GetDateTime<V>(order, "SendTime").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                     : string.Empty
                                     , success_url = _utilityProcess.GetString<V>(order, "SuccessUrl")
                                     , apn_url = _utilityProcess.GetString<V>(order, "ApnUrl")
                                });

            return jsonString;
        }

        /// <summary>
        /// COCS 產生訂單 含電子發票的 JSON String
        /// </summary>
        /// <param name="order"></param>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private string CocsWithInvoiceJsonString<V,K>(V order, K invoice)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = _utilityProcess.GetString<V>(order, "Command")
                                     , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                     , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                     , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                     , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                     , acquirer_type = _utilityProcess.GetString<V>(order, "AcquirerType")
                                     , limit_product_id = _utilityProcess.GetString<V>(order, "LimitProductId")
                                     , send_time = (_utilityProcess.GetDateTime<V>(order, "SendTime").HasValue) 
                                                     ? _utilityProcess.GetDateTime<V>(order, "SendTime").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                     : string.Empty
                                     , success_url = _utilityProcess.GetString<V>(order, "SuccessUrl")
                                     , apn_url = _utilityProcess.GetString<V>(order, "ApnUrl")
                                     , b2c = _utilityProcess.GetString<K>(invoice, "B2C")
                                     , product_name = _utilityProcess.GetString<K>(invoice, "ProductName")
                                     , print_invoice = _utilityProcess.GetString<K>(invoice, "PrintInvoice")
                                     , vehicle_type = _utilityProcess.GetString<K>(invoice, "VehicleType")
                                     , vehicle_barcode = _utilityProcess.GetString<K>(invoice, "VehicleBarcode")
                                     , donate_invoice = _utilityProcess.GetString<K>(invoice, "DonateInvoice")
                                     , love_code = String.IsNullOrWhiteSpace(_utilityProcess.GetString<K>(invoice, "LoveCode")) 
                                                   ? (_utilityProcess.GetString<K>(invoice, "DonateInvoice") == "1") ? "919"
                                                                                                                     : string.Empty
                                                   : _utilityProcess.GetString<K>(invoice, "LoveCode")
                                     , payer_name = _utilityProcess.GetString<K>(invoice, "PayerName")
                                     , payer_postcode = _utilityProcess.GetString<K>(invoice, "PayerPostCode")
                                     , payer_address = _utilityProcess.GetString<K>(invoice, "PayerAddress")
                                     , payer_mobile = _utilityProcess.GetString<K>(invoice, "PayerMobile")
                                     , payer_email = _utilityProcess.GetString<K>(invoice, "PayerEmail")
                                     , buyer_bill_no = _utilityProcess.GetString<K>(invoice, "BuyerBillNo")
                                     , buyer_invoice_title = _utilityProcess.GetString<K>(invoice, "BuyerInvoiceTitle")
                                     
                                });

            return jsonString;
        }

        /// <summary>
        /// COCS 玉山聯名卡產生訂單 不含電子發票的 JSON String
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string CocsUnionpayNoInvoiceJsonString<V>(V order)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = _utilityProcess.GetString<V>(order, "Command")
                                     , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                     , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                     , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                     , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                     , send_time = (_utilityProcess.GetDateTime<V>(order, "SendTime").HasValue) 
                                                     ? _utilityProcess.GetDateTime<V>(order, "SendTime").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                     : string.Empty
                                     , success_url = _utilityProcess.GetString<V>(order, "SuccessUrl")
                                     , apn_url = _utilityProcess.GetString<V>(order, "ApnUrl")
                                });

            return jsonString;
        }

        /// <summary>
        /// COCS 玉山聯名卡產生訂單 含電子發票的 JSON String
        /// </summary>
        /// <param name="order"></param>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private string CocsUnionpayWithInvoiceJsonString<V , K>(V order, K invoice)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = _utilityProcess.GetString<V>(order, "Command")
                                     , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                     , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                     , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                     , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                     , send_time = (_utilityProcess.GetDateTime<V>(order, "SendTime").HasValue) 
                                                     ? _utilityProcess.GetDateTime<V>(order, "SendTime").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                     : string.Empty
                                     , success_url = _utilityProcess.GetString<V>(order, "SuccessUrl")
                                     , apn_url = _utilityProcess.GetString<V>(order, "ApnUrl")
                                     , b2c = _utilityProcess.GetString<K>(invoice, "B2C")
                                     , product_name = _utilityProcess.GetString<K>(invoice, "ProductName")
                                     , print_invoice = _utilityProcess.GetString<K>(invoice, "PrintInvoice")
                                     , vehicle_type = _utilityProcess.GetString<K>(invoice, "VehicleType")
                                     , vehicle_barcode = _utilityProcess.GetString<K>(invoice, "VehicleBarcode")
                                     , donate_invoice = _utilityProcess.GetString<K>(invoice, "DonateInvoice")
                                     , love_code = String.IsNullOrWhiteSpace(_utilityProcess.GetString<K>(invoice, "LoveCode")) 
                                                   ? (_utilityProcess.GetString<K>(invoice, "DonateInvoice") == "1") ? "919"
                                                                                                                     : string.Empty
                                                   : _utilityProcess.GetString<K>(invoice, "LoveCode")
                                     , payer_name = _utilityProcess.GetString<K>(invoice, "PayerName")
                                     , payer_postcode = _utilityProcess.GetString<K>(invoice, "PayerPostCode")
                                     , payer_address = _utilityProcess.GetString<K>(invoice, "PayerAddress")
                                     , payer_mobile = _utilityProcess.GetString<K>(invoice, "PayerMobile")
                                     , payer_email = _utilityProcess.GetString<K>(invoice, "PayerEmail")
                                     , buyer_bill_no = _utilityProcess.GetString<K>(invoice, "BuyerBillNo")
                                     , buyer_invoice_title = _utilityProcess.GetString<K>(invoice, "BuyerInvoiceTitle")
                                });

            return jsonString;
        }
        #endregion

        #region DPH 產生訂單 JSON String
        /// <summary>
        /// DPH 產生訂單 不含電子發票的 JSON String
        /// </summary>
        /// <returns></returns>
        public string DphNoInvoiceJsonString<V>(V order)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                     cmd = _utilityProcess.GetString<V>(order, "Command")
                                     , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                     , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                     , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                     , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                     , payer_name = _utilityProcess.GetString<V>(order, "PayerName")
                                     , acquirer_type = _utilityProcess.GetString<V>(order, "AcquirerType")
                                     , send_time = (_utilityProcess.GetDateTime<V>(order, "SendTime").HasValue) 
                                                     ? _utilityProcess.GetDateTime<V>(order, "SendTime").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                     : string.Empty
                                     , success_url = _utilityProcess.GetString<V>(order, "SuccessUrl")
                                     , apn_url = _utilityProcess.GetString<V>(order, "ApnUrl")
                                });

            return jsonString;
        }

        /// <summary>
        /// DPH 產生訂單 含電子發票的 JSON String
        /// </summary>
        /// <returns></returns>
        private string DphWithInvoiceJsonString<V,K>(V order, K invoice)
        {
            string jsonString = JsonConvert.SerializeObject(new
                                {
                                      cmd = _utilityProcess.GetString<V>(order, "Command")
                                      , cust_id = _utilityProcess.GetString<V>(order, "CustomerId")
                                      , cust_order_no = _utilityProcess.GetString<V>(order, "CustomerOrderNo")
                                      , order_amount = _utilityProcess.GetDecimal<V>(order, "OrderAmount").ToString()
                                      , order_detail = _utilityProcess.GetString<V>(order, "OrderDetail")
                                      , payer_name = _utilityProcess.GetString<V>(order, "PayerName")
                                      , acquirer_type = _utilityProcess.GetString<V>(order, "AcquirerType")
                                      , send_time = (_utilityProcess.GetDateTime<V>(order, "SendTime").HasValue) 
                                                      ? _utilityProcess.GetDateTime<V>(order, "SendTime").Value.ToString("yyyy-MM-dd HH:mm:ss")
                                                      : string.Empty
                                      , success_url = _utilityProcess.GetString<V>(order, "SuccessUrl")
                                      , apn_url = _utilityProcess.GetString<V>(order, "ApnUrl")
                                      , b2c = _utilityProcess.GetString<K>(invoice, "B2C")
                                      , product_name = _utilityProcess.GetString<K>(invoice, "ProductName")
                                      , print_invoice = _utilityProcess.GetString<K>(invoice, "PrintInvoice")
                                      , vehicle_type = _utilityProcess.GetString<K>(invoice, "VehicleType")
                                      , vehicle_barcode = _utilityProcess.GetString<K>(invoice, "VehicleBarcode")
                                      , donate_invoice = _utilityProcess.GetString<K>(invoice, "DonateInvoice")
                                      , love_code = String.IsNullOrWhiteSpace(_utilityProcess.GetString<K>(invoice, "LoveCode")) 
                                                    ? (_utilityProcess.GetString<K>(invoice, "DonateInvoice") == "1") ? "919"
                                                                                                                      : string.Empty
                                                    : _utilityProcess.GetString<K>(invoice, "LoveCode")
                                      , payer_postcode = _utilityProcess.GetString<K>(invoice, "PayerPostCode")
                                      , payer_address = _utilityProcess.GetString<K>(invoice, "PayerAddress")
                                      , payer_mobile = _utilityProcess.GetString<K>(invoice, "PayerMobile")
                                      , payer_email = _utilityProcess.GetString<K>(invoice, "PayerEmail")
                                      , buyer_bill_no = _utilityProcess.GetString<K>(invoice, "BuyerBillNo")
                                      , buyer_invoice_title = _utilityProcess.GetString<K>(invoice, "BuyerInvoiceTitle")
                                });

            return jsonString;
        }
        #endregion

        #region 共用方法
        ///// <summary>
        ///// 特定欄位調整時區
        ///// </summary>
        ///// <param name="orderNoInvoice"></param>
        //private void OrderParseDateTime(ref ReturnOrder orderNoInvoice)
        //{
        //    orderNoInvoice.ExpireDate = _utilityProcess.ChangeUtcTimeZone(orderNoInvoice.ExpireDate);
        //    orderNoInvoice.GrantDate = _utilityProcess.ChangeUtcTimeZone(orderNoInvoice.GrantDate);
        //    orderNoInvoice.InvoiceDate = _utilityProcess.ChangeUtcTimeZone(orderNoInvoice.InvoiceDate);
        //    orderNoInvoice.PayDate = _utilityProcess.ChangeUtcTimeZone(orderNoInvoice.PayDate);
        //    orderNoInvoice.CreateTime = _utilityProcess.ChangeUtcTimeZone(orderNoInvoice.CreateTime);
        //    orderNoInvoice.ProcessCodeUpdateTime = _utilityProcess.ChangeUtcTimeZone(orderNoInvoice.ProcessCodeUpdateTime);
        //}

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