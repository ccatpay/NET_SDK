using CCatPay_Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SampleCode
{
    public partial class DphOrderQueryByDate : Page
    { 
        private readonly UtilityProcess _utilityProcess;

        public DphOrderQueryByDate()
        {
            _utilityProcess = new UtilityProcess();
        }

        /// <summary>
        /// CVS 訂單批次查詢 (日期區間)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!this.IsPostBack)
                //{

                //}
            }
            catch (Exception ex)
            {
                spErrorMessage.InnerHtml = ex.Message;
                spSuccessMessage.InnerHtml = string.Empty;
            }
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            List<string> enErrors = new List<string>();

            ReturnDphOrderList returnOrderList = new ReturnDphOrderList();

            try
            {
                using (AllOrderQuery AllQuery = new AllOrderQuery())
                {
                    #region SetData
                    DateTime dtNow = DateTime.UtcNow.AddHours(8);

                    DateTime dtOrderStartDate = DateTime.TryParse(tbOrderStartDate.Text, out dtOrderStartDate)
                                              ? dtOrderStartDate : dtNow.Date.AddMonths(-3);

                    DateTime dtOrderEndDate = DateTime.TryParse(tbOrderEndDate.Text, out dtOrderEndDate)
                                            ? dtOrderEndDate : dtNow.Date.AddDays(1).AddSeconds(-1);
                    #endregion

                    /* 批次查詢 */
                    OrderQueryModel order = new OrderQueryModel()
                    {
                        /* 呼叫 API */
                        ServiceMethod = HttpMethod.HttpPOST
                        , ApiUrl = "http://test.4128888card.com.tw/app/api/Collect"   // 呼叫 API

                        /* 登入 參數 */
                        , UserName = tbLoginAccount.Text
                        , ApiPassword = tbLoginPassword.Text

                        /* Order 參數 */
                        , Command = "DphOrderListQuery"                               // 交易代碼 (CVS 訂單批次查詢 固定帶入 DphOrderListQuery)
                        , CustomerId = tbCustomerId.Text                              // 契客代碼
                        //, CustomerOrderNo = tbCustomerOrderNo.Text                  // 契客訂單號碼
                        , OrderStartDate = dtOrderStartDate                           // 訂單日期起日 (yyyy-MM-dd HH:mm:ss)
                        , OrderEndDate = dtOrderEndDate                               // 訂單日期迄日 (yyyy-MM-dd HH:mm:ss)
                    };

                    if (String.IsNullOrWhiteSpace(tbLoginAccount.Text))
                    {
                        enErrors.Add("請輸入登入帳號");
                    }

                    if (String.IsNullOrWhiteSpace(tbLoginPassword.Text))
                    {
                        enErrors.Add("請輸入登入密碼");
                    }

                    if (enErrors.Count <= 0)
                    {
                        // 帳號登入
                        ReturnToken token = GetToken(order.UserName, order.ApiPassword, ref enErrors);

                        if (String.IsNullOrWhiteSpace(token.Error))
                        {
                            // 成功登入帳號後，須回傳 Token 才可新增訂單
                            order.AccessToken = token.AccessToken;        // Token
                            order.TokenType = token.TokenType;            // Token 類型

                            returnOrderList = AllQuery.OrderListQuery<ReturnDphOrderList>(order);

                            if (returnOrderList.Status.Contains("ERROR"))
                            {
                                enErrors.Add(returnOrderList.Message);

                                if (returnOrderList.OrderList != null)
                                {
                                    foreach (var item in returnOrderList.OrderList)
                                    {
                                        enErrors = _utilityProcess.SplitErrorMsg(item.Status, item.Message, enErrors);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {
                List<ReturnDphOrder> orderList = new List<ReturnDphOrder>();

                if (enErrors.Count() > 0)
                {
                    string szErrorMessage = String.Join("<br/>", enErrors);
                    spErrorMessage.InnerHtml = szErrorMessage;
                    spSuccessMessage.InnerHtml = string.Empty;
                    orderList.Clear();
                }
                else
                {
                    spErrorMessage.InnerHtml = string.Empty;
                    if (returnOrderList.OrderList != null)
                    {
                        spSuccessMessage.InnerHtml = "查詢訂單：" + returnOrderList.OrderList.Count + "筆";
                        orderList = returnOrderList.OrderList;
                    }
                    else
                    {
                        spSuccessMessage.InnerHtml = "查詢訂單：0筆";
                        orderList.Clear();
                    }
                }

                BindGridList(orderList);
            }
        }

        private ReturnToken GetToken(string userName, string password, ref List<string> enErrors)
        {
            ReturnToken token = new ReturnToken();

            try
            {
                using (Token loginToken = new Token())
                {
                    /* Token 參數 */
                    TokenModel model = new TokenModel()
                    {
                        ServiceMethod = HttpMethod.HttpPOST
                        , UserName = userName
                        , ApiPassword = password
                        , ApiUrl = "http://test.4128888card.com.tw/app/token"
                    };

                    /* 取得 Token */
                    token = loginToken.GetToken(model);

                    enErrors = _utilityProcess.SplitErrorMsg(token.Error, token.ErrorDescription, enErrors);
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }

            return token;
        }

        protected void gvOrder_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvOrder.PageIndex = e.NewPageIndex;
            btSubmit_Click(sender , e);
        }

        protected void BindGridList(List<ReturnDphOrder> orderList)
        {
            gvOrder.DataSource = orderList;
            gvOrder.DataBind();
        }
    }
}