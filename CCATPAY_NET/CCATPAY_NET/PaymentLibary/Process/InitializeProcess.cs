using System.Web.UI.WebControls;

namespace CCatPay_Net
{
    public class InitializeProcess
    {
        private readonly UtilityProcess _utilityProcess; 

        public InitializeProcess()
        {
            _utilityProcess = new UtilityProcess();
        }

        #region 收單行 下拉式選單
        /// <summary>
        /// 取得收單行
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public ListItem[] SetAcquirerTypeDropDownList(string textType, string valueType, string serviceType)
        {
            ListItem[] listItems = null;

            switch (serviceType.ToUpper())
            {
                case "CVS":
                    listItems = SetCvsAcquirerTypeDropDownList(textType, valueType);
                    break;
                case "COCS":
                    listItems = SetCocsAcquirerTypeDropDownList(textType, valueType);
                    break;
                case "DPH":
                    listItems = SetDphAcquirerTypeDropDownList(textType, valueType);
                    break;
            }

            return listItems;
        }

        /// <summary>
        /// 取得 CVS 收單行
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        private ListItem[] SetCvsAcquirerTypeDropDownList(string textType, string valueType)
        {
            var acquirerTypeList = _utilityProcess.AllEnumToList<CvsAcquirerType>(textType, valueType);

            return acquirerTypeList.ToArray();
        }

        /// <summary>
        /// 取得 COCS 收單行
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        private ListItem[] SetCocsAcquirerTypeDropDownList(string textType, string valueType)
        {
            var acquirerTypeList = _utilityProcess.AllEnumToList<CocsAcquirerType>(textType, valueType);

            return acquirerTypeList.ToArray();
        }

        /// <summary>
        /// 取得 DPH 收單行
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        private ListItem[] SetDphAcquirerTypeDropDownList(string textType, string valueType)
        {
            var acquirerTypeList = _utilityProcess.AllEnumToList<DphAcquirerType>(textType, valueType);

            return acquirerTypeList.ToArray();
        }
        #endregion

        #region 載具類別 下拉式選單
        /// <summary>
        /// 取得 載具類別
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public ListItem[] SetVehicleTypeDropDownList(string textType, string valueType)
        {
            var acquirerTypeList = _utilityProcess.AllEnumToList<VehicleType>(textType, valueType);

            return acquirerTypeList.ToArray();
        }

        /// <summary>
        /// 自訂載具類別下拉式選單
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public ListItem[] SetCustomVehicleTypeDropDownList(string textType, string valueType, VehicleType[] vehicleArray)
        {
            var paymentTypeList = _utilityProcess.PartEnumToList(vehicleArray, textType, valueType);

            return paymentTypeList.ToArray();
        }
        #endregion

        #region 繳款方式 下拉式選單
        /// <summary>
        /// 取得繳款方式
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public ListItem[] SetPaymentTypeDropDownList(string textType, string valueType, string serviceType)
        {
            ListItem[] listItems = null;

            switch (serviceType.ToUpper())
            {
                case "CVS":
                    listItems = SetCvsPaymentTypeDropDownList(textType, valueType);
                    break;
                case "COCS":
                    listItems = SetCocsPaymentTypeDropDownList(textType, valueType);
                    break;
                case "DPH":
                    listItems = SetDphPaymentTypeDropDownList(textType, valueType);
                    break;
                default:
                    listItems = _utilityProcess.AllEnumToList<PaymentType>(textType, valueType).ToArray();
                    break;
            }

            return listItems;
        }

        /// <summary>
        /// 自訂繳款方式下拉式選單
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public ListItem[] SetCustomPaymentTypeDropDownList(string textType, string valueType, PaymentType[] paymentArray)
        {
            var paymentTypeList = _utilityProcess.PartEnumToList(paymentArray, textType, valueType);

            return paymentTypeList.ToArray();
        }

        /// <summary>
        /// 取得 CVS 繳款方式
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        private ListItem[] SetCvsPaymentTypeDropDownList(string textType, string valueType)
        {
            // 繳款方式 0: ibon繳款、1: ATM銀行轉帳、2:三段式條碼、9:三段式條碼(中信 即時 繳款通知，僅支援7-11繳費)
            PaymentType[] paymentTypeArray = new PaymentType[]
            { PaymentType.Ibon
            , PaymentType.Atm
            , PaymentType.Barcode
            , PaymentType.BarcodeImmediate};

            var paymentTypeList = _utilityProcess.PartEnumToList(paymentTypeArray, textType, valueType);

            return paymentTypeList.ToArray();
        }

        /// <summary>
        /// 取得 CVS 繳款方式
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        private ListItem[] SetCocsPaymentTypeDropDownList(string textType, string valueType)
        {
            // 繳款方式 3: 線上刷卡
            PaymentType[] paymentTypeArray = new PaymentType[]
            { PaymentType.CreditCard };

            var paymentTypeList = _utilityProcess.PartEnumToList(paymentTypeArray, textType, valueType);

            return paymentTypeList.ToArray();
        }

        /// <summary>
        /// 取得 CVS 繳款方式
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        private ListItem[] SetDphPaymentTypeDropDownList(string textType, string valueType)
        {
            // 繳款方式 7: 行動支付、10: 到所取貨收現
            PaymentType[] paymentTypeArray = new PaymentType[]
            { PaymentType.DPH
            , PaymentType.CashOnTCat};

            var paymentTypeList = _utilityProcess.PartEnumToList(paymentTypeArray, textType, valueType);

            return paymentTypeList.ToArray();
        }
        #endregion

        #region 金流服務類型 下拉式選單
        /// <summary>
        /// 金流服務類型
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public ListItem[] SetServiceTypeDropDownList(string textType, string valueType)
        {
            var paymentTypeList = _utilityProcess.AllEnumToList<ServiceType>(textType, valueType);

            return paymentTypeList.ToArray();
        }


        /// <summary>
        /// 自訂金流服務類型下拉式選單
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public ListItem[] SetCustomServiceTypeDropDownList(string textType, string valueType, ServiceType[] serviceArray)
        {
            var paymentTypeList = _utilityProcess.PartEnumToList(serviceArray, textType, valueType);

            return paymentTypeList.ToArray();
        }
        #endregion
    }
}